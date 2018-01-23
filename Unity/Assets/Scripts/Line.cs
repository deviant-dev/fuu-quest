using System.Linq;
using Deviant.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Fuu {
	public class Line : MonoBehaviour {
		[SerializeField] private float m_FadeDuration = 1;
		[SerializeField] private int m_ScrollSpeed = 2;
		[SerializeField] private int m_HiddenBottomPadding = -10;

		private CanvasRenderer m_BackgroundRenderer;
		private Text m_Text;
		private CanvasRenderer m_TextRenderer;
		private LayoutGroup m_LayoutGroup;
		private int m_TargetBottom;
		private float m_Alpha;
		private bool m_Skip;

		private float Alpha {
			get { return m_Alpha; }
			set {
				m_Alpha = Mathf.Clamp01(value);
				m_TextRenderer.SetAlpha(m_Alpha);
				if (m_BackgroundRenderer) { m_BackgroundRenderer.SetAlpha(m_Alpha); }
			}
		}

		protected virtual void Awake() {
			m_Text = GetComponentInChildren<Text>();
			m_TextRenderer = m_Text.GetComponent<CanvasRenderer>();
			m_BackgroundRenderer = GetComponentsInChildren<CanvasRenderer>().LastOrDefault(r => r != m_TextRenderer);
			m_LayoutGroup = GetComponent<LayoutGroup>();
		}

		private void Start() {
			if (m_Skip) { return; }
			m_TargetBottom = m_LayoutGroup.padding.bottom;
			m_LayoutGroup.padding.bottom = m_HiddenBottomPadding;
			m_TextRenderer.SetAlpha(0);
			if (m_BackgroundRenderer) { m_BackgroundRenderer.SetAlpha(0); }
		}

		private void LateUpdate() {
			if (m_Skip) { return; }

			if (m_LayoutGroup.padding.bottom < m_TargetBottom) {
				m_LayoutGroup.padding.bottom = Mathf.Min(m_LayoutGroup.padding.bottom + m_ScrollSpeed, m_TargetBottom);
			}
			else if (Alpha < 1) {
				Alpha += Time.deltaTime * m_FadeDuration;
			}
			else { m_Skip = true; }
		}

		public void SetText(string text, bool skipTransition = false) {
			m_Text.text = CleanText(text);
			m_Skip = skipTransition;
		}

		public Tween Fade() {
			m_Skip = true;
			Alpha = 1;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(DOTween.To(() => Alpha, v => Alpha = v, 0, 0.5f).OnComplete(() => UnityUtils.DestroyObject(this)));
			sequence.Append(DOTween.To(() => m_LayoutGroup.padding.bottom, (v) => m_LayoutGroup.padding.bottom = v, m_HiddenBottomPadding, 0.5f));
			return sequence;
		}

		private static string CleanText(string text) { return text.ReplaceRegex(@"\<[^>]+\>", ""); }
	}
}