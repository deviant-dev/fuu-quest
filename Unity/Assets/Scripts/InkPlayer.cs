using Deviant.Utils;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Fuu {
	public class InkPlayer : MonoBehaviour {
		[SerializeField] private TextAsset m_InkJsonAsset;
		[SerializeField, QuickSelectAsset("t:StageAssets")] private StageAssets m_Assets;

		[Header("Settings")]

		[SerializeField] private float m_NextDelay = 1;

		[Header("UI")]

		[SerializeField] private Canvas m_Canvas;
		[SerializeField] private VerticalLayoutGroup m_LayoutGroup;
		[SerializeField] private Line m_TitleLinePrefab;
		[SerializeField] private Line m_SubtitleLinePrefab;
		[SerializeField] private Line m_DefaultTextPrefab;
		[SerializeField] private Line m_PlayerTextPrefab;
		[SerializeField] private Button m_ButtonPrefab;

		[Header("Stage")]

		[SerializeField] private Transform m_Backdrop;
		[SerializeField] private Transform m_OnStageLeft;
		[SerializeField] private Transform m_OnStageRight;
		[SerializeField] private Transform m_OffStageLeft;
		[SerializeField] private Transform m_OffStageRight;

		private Story m_Story;

		private void OnEnable() { StartStory(); }

		private void StartStory() {
			m_Story = new Story(m_InkJsonAsset.text);
			m_Backdrop.DestroyAllChildren();
			m_OnStageLeft.DestroyAllChildren();
			m_OnStageRight.DestroyAllChildren();
			Refresh();
		}

		private void Refresh() {
			m_Canvas.transform.DestroyAllChildren();
			Next();
		}

		private bool m_FirstLine = false;

		private void Next() {
			if (m_Story.canContinue) {
				string text = m_Story.Continue().Trim();
				CreateContentView(text, m_FirstLine);
				m_FirstLine = false;

				foreach (string t in m_Story.currentTags) {
					string[] pieces = t.Split(':');
					if (pieces.Length != 2) { continue; }

					if (pieces[0] == "back") {
						m_Backdrop.DestroyAllChildren();
						if (m_Assets.Backdrops.ContainsKey(pieces[1])) {
							GameObject go = Instantiate(m_Assets.Backdrops[pieces[1]], m_Backdrop.transform);
							go.transform.ResetTransform();
						}
					}

					if (pieces[0] == "right") {
						m_OnStageRight.DestroyAllChildren();
						if (m_Assets.Characters.ContainsKey(pieces[1])) {
							GameObject go = Instantiate(m_Assets.Characters[pieces[1]], m_OnStageRight.transform);
							go.transform.ResetTransform();
						}
					}

					if (pieces[0] == "left") {
						m_OnStageLeft.DestroyAllChildren();
						if (m_Assets.Characters.ContainsKey(pieces[1])) {
							GameObject go = Instantiate(m_Assets.Characters[pieces[1]], m_OnStageLeft.transform);
							go.transform.ResetTransform();
						}
					}
				}
				DelayTracker.DelayAction(m_NextDelay, Next);
				return;
			}

			if (m_Story.currentChoices.Count > 0) {
				for (int i = 0; i < m_Story.currentChoices.Count; i++) {
					Choice choice = m_Story.currentChoices[i];
					Button button = CreateChoiceView(choice.text.Trim());
					button.onClick.AddListener(delegate { OnClickChoiceButton(choice); });
				}
			}
			else {
				Button choice = CreateChoiceView("End of story.\nRestart?");
				choice.onClick.AddListener(StartStory);
			}
		}

		private void OnClickChoiceButton(Choice choice) {
			m_Story.ChooseChoiceIndex(choice.index);
			m_FirstLine = true;
			Refresh();
		}


		private void CreateContentView(string text, bool skipTransition = false) {
			Line linePrefab = GetLinePrefab(text);
			Line storyLine = Instantiate(linePrefab, m_Canvas.transform, false);
			storyLine.SetText(text, skipTransition);
		}

		private Line GetLinePrefab(string text) {
			if (text.Contains("<h4")) { return m_SubtitleLinePrefab; }
			if (text.Contains("<h")) { return m_TitleLinePrefab; }
			if (text.Contains("<i>") && !m_Story.currentTags.Contains("writing")) { return m_PlayerTextPrefab; }
			return m_DefaultTextPrefab;
		}

		private Button CreateChoiceView(string text) {
			Button choice = Instantiate(m_ButtonPrefab, m_Canvas.transform, false);
			Line line = choice.GetComponent<Line>();
			line.SetText(text);
			return choice;
		}
	}
}