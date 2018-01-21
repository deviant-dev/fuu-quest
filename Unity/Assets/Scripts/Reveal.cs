using DG.Tweening;
using UnityEngine;

public class Reveal : MonoBehaviour {
	[SerializeField] private CanvasRenderer m_Renderer;
	[SerializeField] private float m_ScaleDuration = 0.2f;
	[SerializeField] private float m_FadeDuration = 0.8f;

	private Vector3 m_TargetScale;
	private void Awake() { m_TargetScale = transform.localScale; }

	private void OnEnable() {
		transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
		m_Renderer.SetAlpha(0);
		transform.DOScale(m_TargetScale, m_ScaleDuration).OnComplete(FadeIn);
	}

	private void FadeIn() {
		DOTween.To(() => m_Renderer.GetAlpha(), m_Renderer.SetAlpha, 1, m_FadeDuration);
	}
}