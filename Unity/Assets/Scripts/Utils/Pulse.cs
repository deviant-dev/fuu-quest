using UnityEngine;
using UnityEngine.UI;

namespace Deviant.Utils {
	[RequireComponent(typeof(Graphic))]
	public class Pulse : MonoBehaviour {
		[SerializeField] private AnimationCurve m_Curve = AnimationCurve.EaseInOut(0, 1, 1, 0);
		[SerializeField] private Graphic m_Graphic;

		private Color m_StartColor;

		private void Start() {
			m_Graphic = m_Graphic ? m_Graphic : (m_Graphic = GetComponent<Graphic>());
			if (m_Graphic) { m_StartColor = m_Graphic.color; }
			if (m_Curve.postWrapMode != WrapMode.Loop && m_Curve.postWrapMode != WrapMode.PingPong) { m_Curve.postWrapMode = WrapMode.PingPong; }
		}

		private void Update() {
			if (m_Graphic) { m_Graphic.color = new Color(m_StartColor.r, m_StartColor.g, m_StartColor.b, m_Curve.Evaluate(Time.time)); }
		}
	}
}