using Deviant.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Fuu {
	public class Line : MonoBehaviour {
		[SerializeField] private float m_FadeDuration = 1;
		[SerializeField] private int m_ScrollSpeed = 2;
		[SerializeField] private int m_HiddenBottomPadding = -10;

		private CanvasRenderer[] m_Renderers;
		private Text m_Text;
		private LayoutGroup m_LayoutGroup;
		private int m_TargetBottom;
		private float m_Alpha;
		private bool m_Skip;

		private float Alpha {
			get { return m_Alpha; }
			set {
				m_Alpha = Mathf.Clamp01(value);
				Renderers.ForEach(r => r.SetAlpha(m_Alpha));
			}
		}

		private CanvasRenderer[] Renderers { get { return m_Renderers != null ? m_Renderers : (m_Renderers = GetComponentsInChildren<CanvasRenderer>()); } }
		private Text Text { get { return m_Text ? m_Text : (m_Text = GetComponentInChildren<Text>()); } }
		private LayoutGroup LayoutGroup { get { return m_LayoutGroup ? m_LayoutGroup : (m_LayoutGroup = GetComponent<LayoutGroup>()); } }

		private void Start() {
			if (m_Skip) { return; }

			m_TargetBottom = LayoutGroup.padding.bottom;
			LayoutGroup.padding.bottom = m_HiddenBottomPadding;
			Alpha = 0;
		}

		private void LateUpdate() {
			if (m_Skip) { return; }

			if (LayoutGroup.padding.bottom < m_TargetBottom) { LayoutGroup.padding.bottom = Mathf.Min(LayoutGroup.padding.bottom + m_ScrollSpeed, m_TargetBottom); }
			else if (Alpha < 1) { Alpha += Time.deltaTime * m_FadeDuration; }
			else { m_Skip = true; }
		}

		public void SetText(string text, bool skipTransition = false) {
			Text.text = CleanText(text);
			m_Skip = skipTransition;
		}

		public Tween Fade() {
			m_Skip = true;
			Alpha = 1;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(DOTween.To(() => Alpha, v => Alpha = v, 0, 0.5f).OnComplete(() => UnityUtils.DestroyObject(this)));
			sequence.Append(DOTween.To(() => LayoutGroup.padding.bottom, v => LayoutGroup.padding.bottom = v, m_HiddenBottomPadding, 0.5f));
			return sequence;
		}

		private static string CleanText(string text) { return text.ReplaceRegex(@"\<[^>]+\>", ""); }
	}
}