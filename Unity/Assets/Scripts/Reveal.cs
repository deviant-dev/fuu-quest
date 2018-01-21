using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Reveal : MonoBehaviour {
	[SerializeField] private CanvasRenderer m_Renderer;
	[SerializeField] private RectTransform m_RectTransform;
	[SerializeField] private LayoutElement m_Layout;
	[SerializeField] private float m_ScaleDuration = 0.2f;
	[SerializeField] private float m_FadeDuration = 0.8f;

	private void OnEnable() {
		float height = LayoutUtility.GetMinHeight(m_RectTransform);
		// m_Layout.preferredHeight = 0;
		m_Renderer.SetAlpha(0);
		FadeIn();
		// DOTween.To(() => m_Layout.minHeight, h => m_Layout.minHeight = h, height, m_ScaleDuration).OnComplete(FadeIn);
	}

	private void FadeIn() {
		DOTween.To(() => m_Renderer.GetAlpha(), m_Renderer.SetAlpha, 1, m_FadeDuration);
	}
}