using System;
using Deviant.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Line : MonoBehaviour {
	[SerializeField] private Text m_Text;
	[SerializeField] private CanvasRenderer m_TextRenderer;
	[SerializeField] private CanvasRenderer m_BackgroundRenderer;
	[SerializeField] private LayoutGroup m_LayoutGroup;
	[SerializeField] private float m_FadeDuration = 1;
	[SerializeField] private int m_ScrollSpeed = 2;
	[SerializeField] private int m_HiddenBottomPadding = -10;

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

	public Tween Fade() { return DOTween.To(() => Alpha, v => Alpha = v, 0, m_FadeDuration).OnComplete(() => UnityUtils.DestroyObject(this)); }

	private static string CleanText(string text) { return text.ReplaceRegex(@"\<[^>]+\>", ""); }
}