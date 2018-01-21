using DG.Tweening;
using UnityEngine;

public class Reveal : MonoBehaviour {
	[SerializeField] private CanvasRenderer m_Renderer;
	[SerializeField] private float m_FadeDuration = 0.8f;

	private void OnEnable() {
		m_Renderer.SetAlpha(0);
		DOTween.To(() => m_Renderer.GetAlpha(), m_Renderer.SetAlpha, 1, m_FadeDuration);
	}
}