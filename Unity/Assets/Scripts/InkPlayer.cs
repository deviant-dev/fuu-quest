using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Fuu {
	public class InkPlayer : MonoBehaviour {
		[SerializeField] private TextAsset m_InkJsonAsset;
		[SerializeField] private Canvas m_Canvas;
		[SerializeField] private Transform m_Background;
		[SerializeField] private Transform m_LeftActor;
		[SerializeField] private Transform m_RightActor;
		[SerializeField] private Text m_TextPrefab;
		[SerializeField] private Button m_ButtonPrefab;

		private Story m_Story;

		private void Awake() { StartStory(); }

		private void StartStory() {
			m_Story = new Story(m_InkJsonAsset.text);
			RefreshView();
		}

		private void RefreshView() {
			RemoveChildren();

			while (m_Story.canContinue) {
				string text = m_Story.Continue().Trim();
				CreateContentView(text);
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
				choice.onClick.AddListener(delegate { StartStory(); });
			}
		}

		private void OnClickChoiceButton(Choice choice) {
			m_Story.ChooseChoiceIndex(choice.index);
			RefreshView();
		}

		private void CreateContentView(string text) {
			Text storyText = Instantiate(m_TextPrefab);
			storyText.text = text;
			storyText.transform.SetParent(m_Canvas.transform, false);
		}

		private Button CreateChoiceView(string text) {
			Button choice = Instantiate(m_ButtonPrefab);
			choice.transform.SetParent(m_Canvas.transform, false);

			Text choiceText = choice.GetComponentInChildren<Text>();
			choiceText.text = text;

			HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
			layoutGroup.childForceExpandHeight = false;

			return choice;
		}

		private void RemoveChildren() {
			int childCount = m_Canvas.transform.childCount;
			for (int i = childCount - 1; i >= 0; --i) { Destroy(m_Canvas.transform.GetChild(i).gameObject); }
		}
	}
}