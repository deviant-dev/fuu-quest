using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Deviant.Utils {
	[RequireComponent(typeof(Text))]
	public class TextFade : MonoBehaviour {
		[Tooltip("Number of seconds each character should take to fade up"), SerializeField]
		private float m_FadeDuration = 2f;

		[Tooltip("Speed the reveal travels along the text, in characters per second"), SerializeField]
		private float m_TravelSpeed = 8f;

		public UnityEvent OnComplete;

		// Cached reference to our Text object.
		private Text m_Text;
		private Coroutine m_Fade;

		// Lookup table for hex characters.
		private static readonly char[] s_NibbleToHex = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};

		// Use this for initialization
		private void Start() {
			m_Text = GetComponent<Text>();
			// If you don't want the text to fade right away, skip this line.
			FadeTo(m_Text.text);
		}

		private void FadeTo(string text) {
			// Abort a fade in progress, if any.
			StopFade();
			// Start fading, and keep track of the coroutine so we can interrupt if needed.
			m_Fade = StartCoroutine(FadeText(text));
		}

		private void StopFade() {
			if (m_Fade != null) { StopCoroutine(m_Fade); }
		}

		// Currently this expects a string of plain text,
		// and will not correctly handle rich text tags etc.
		private IEnumerator FadeText(string text) {
			int length = text.Length;
			// Build a character buffer of our desired text,
			// with a rich text "color" tag around every character.
			var builder = new StringBuilder(length * 26);
			Color32 color = m_Text.color;
			for (int i = 0; i < length; i++) {
				builder.Append("<color=#");
				builder.Append(s_NibbleToHex[color.r >> 4]);
				builder.Append(s_NibbleToHex[color.r & 0xF]);
				builder.Append(s_NibbleToHex[color.g >> 4]);
				builder.Append(s_NibbleToHex[color.g & 0xF]);
				builder.Append(s_NibbleToHex[color.b >> 4]);
				builder.Append(s_NibbleToHex[color.b & 0xF]);
				builder.Append("00>");
				builder.Append(text[i]);
				builder.Append("</color>");
			}

			// Each frame, update the alpha values along the fading frontier.
			float fadingProgress = 0f;
			int opaqueChars = -1;
			while (opaqueChars < length - 1) {
				yield return null;
				fadingProgress += Time.deltaTime;
				float leadingEdge = fadingProgress * m_TravelSpeed;
				int lastChar = Mathf.Min(length - 1, Mathf.FloorToInt(leadingEdge));
				int newOpaque = opaqueChars;
				for (int i = lastChar; i > opaqueChars; i--) {
					byte fade = (byte) (255f * Mathf.Clamp01((leadingEdge - i) / (m_TravelSpeed * m_FadeDuration)));
					builder[i * 26 + 14] = s_NibbleToHex[fade >> 4];
					builder[i * 26 + 15] = s_NibbleToHex[fade & 0xF];
					if (fade == 255) { newOpaque = Mathf.Max(newOpaque, i); }
				}

				opaqueChars = newOpaque;
				// This allocates a new string.
				m_Text.text = builder.ToString();
			}

			// Once all the characters are opaque, 
			// ditch the unnecessary markup and end the routine.
			m_Text.text = text;
			// Mark the fade transition as finished.
			// This can also fire an event/message if you want to signal UI.
			m_Fade = null;

			OnComplete.Invoke();
		}
	}
}