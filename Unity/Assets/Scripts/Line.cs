using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Line : MonoBehaviour {
	[SerializeField] private Text m_Text;
	[SerializeField] private CanvasRenderer m_Renderer;
	[SerializeField] private LayoutGroup m_LayoutGroup;
	[SerializeField] private float m_FadeDuration = 0.8f;

	public string Text {
		set {
			m_Text.text = value;
		}
	}

	private void OnEnable() {
		m_Renderer.SetAlpha(0);

		m_LayoutGroup.padding.bottom = -100;
		DOTween.To(() => m_LayoutGroup.padding.bottom, v => m_LayoutGroup.padding.bottom = v, 1, m_FadeDuration);
		DOTween.To(() => m_Renderer.GetAlpha(), m_Renderer.SetAlpha, 1, m_FadeDuration);
	}
}