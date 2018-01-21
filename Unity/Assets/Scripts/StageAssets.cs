using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Fuu {
	[CreateAssetMenu]
	public class StageAssets : ScriptableObject {
		[SerializeField] private GameObject[] m_Backdrops;
		[SerializeField] private GameObject[] m_Characters;

		private Dictionary<string, GameObject> m_BackdropLookup;
		private Dictionary<string, GameObject> m_CharacterLookup;

		private bool m_Init;

		public Dictionary<string, GameObject> Backdrops {
			get {
				CheckInit();
				return m_BackdropLookup;
			}
		}

		public Dictionary<string, GameObject> Characters {
			get {
				CheckInit();
				return m_CharacterLookup;
			}
		}

		private void CheckInit() {
			if (m_Init) {
				return;
			}

			m_Init = true;
			m_BackdropLookup = m_Backdrops.ToDictionary(go => go.name);
			m_CharacterLookup = m_Characters.ToDictionary(go => go.name);
		}
	}
}