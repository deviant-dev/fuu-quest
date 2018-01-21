using Deviant.Utils;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Fuu {
	public class InkPlayer : MonoBehaviour {
		[SerializeField] private TextAsset m_InkJsonAsset;

		[Header("Settings")]

		[SerializeField] private float m_NextDelay = 1;

		[Header("UI")]

		[SerializeField] private Canvas m_Canvas;
		[SerializeField] private Text m_TextPrefab;
		[SerializeField] private Button m_ButtonPrefab;

		[Header("Stage")]

		[SerializeField] private Transform m_Backdrop;
		[SerializeField] private Transform m_OnStageLeft;
		[SerializeField] private Transform m_OnStageRight;
		[SerializeField] private Transform m_OffStageLeft;
		[SerializeField] private Transform m_OffStageRight;


		private Story m_Story;

		private void Awake() { StartStory(); }

		private void StartStory() {
			m_Story = new Story(m_InkJsonAsset.text);
			Refresh();
		}

		private void Refresh() {
			m_Canvas.transform.DestroyAllChildren();
			Next();
		}

		private void Next() {
			if (m_Story.canContinue) {
				string text = m_Story.Continue().Trim();
				CreateContentView(text);
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
			Refresh();
		}

		private void CreateContentView(string text) {
			Text storyText = Instantiate(m_TextPrefab);
			storyText.text = CleanText(text);
			storyText.transform.SetParent(m_Canvas.transform, false);
		}

		private static string CleanText(string text) {
			if (text.Contains("<i>") && !text.Contains("</i>")) { text += "</i>"; }
			text = text.ReplaceRegex(@"\<h3\>", "<size=30><b>");
			text = text.ReplaceRegex(@"\<h4\>", "<size=25><b>");
			text = text.ReplaceRegex(@"\</h[34]\>", "</b></size>");
			text = text.ReplaceRegex(@"\</?h[0-9]\>", "");
			return text;
		}

		private Button CreateChoiceView(string text) {
			Button choice = Instantiate(m_ButtonPrefab);
			choice.transform.SetParent(m_Canvas.transform, false);

			Text choiceText = choice.GetComponentInChildren<Text>();
			choiceText.text = CleanText(text);

			HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
			layoutGroup.childForceExpandHeight = false;

			return choice;
		}
	}
}