using System;
using System.Linq;
using Deviant.Utils;
using DG.Tweening;
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
		[SerializeField] private Line m_NPCTextPrefab;
		[SerializeField] private Line m_PlayerTextPrefab;
		[SerializeField] private Button m_ButtonPrefab;

		[Header("Stage")]

		[SerializeField] private Transform m_Backdrop;
		[SerializeField] private Transform m_OnStageLeft;
		[SerializeField] private Transform m_OnStageRight;
		[SerializeField] private Transform m_OffStageLeft;
		[SerializeField] private Transform m_OffStageRight;
		[SerializeField] private float m_SlideDuration = 1;

		private Story m_Story;
		private bool m_FirstLine;

		private void OnEnable() { StartStory(); }

		private void StartStory() {
			m_Story = new Story(m_InkJsonAsset.text);
			m_Canvas.transform.DestroyAllChildren();
			m_Backdrop.DestroyAllChildren();
			m_OnStageLeft.DestroyAllChildren();
			m_OnStageRight.DestroyAllChildren();
			m_OffStageLeft.DestroyAllChildren();
			m_OffStageRight.DestroyAllChildren();
			Refresh();
		}

		private void Refresh() {
			float delay = 0;
			foreach (Line line in m_Canvas.GetComponentsInChildren<Line>().Where(l => l)) {
				line.Fade().SetDelay(delay);
				delay += 0.25f;
			}

			Next();
		}

		private string GetTagValue(string tagKey) {
			string tagString = m_Story.currentTags.FirstOrDefault(t => t.StartsWith(tagKey + ":", StringComparison.OrdinalIgnoreCase));
			return tag.IsNullOrEmpty() ? null : tag.Split(':').ElementAtOrDefault(1);
		}

		private void Next() {
			if (m_Story.canContinue) {
				string text = m_Story.Continue().Trim();
				Line nextLine = CreateContentView(text, m_FirstLine);
				m_FirstLine = false;

				// Collect possible commands.
				string back = GetTagValue("back");
				string right = GetTagValue("right");
				string left = GetTagValue("left");

				Sequence sequence = DOTween.Sequence();

				// Handle background change.
				if (!back.IsNullOrEmpty()) {
					sequence.Append(ClearStage());

					sequence.AppendCallback(() => {
						m_Backdrop.DestroyAllChildren();
						if (m_Assets.Backdrops.ContainsKey(back)) {
							GameObject go = Instantiate(m_Assets.Backdrops[back], m_Backdrop);
							go.transform.ResetTransform();
						}
					});
				}

				// Handle characters leaving.
				else {
					if (!right.IsNullOrEmpty()) { sequence.Append(ClearStageRight()); }
					if (!left.IsNullOrEmpty()) { sequence.Append(ClearStageLeft()); }
				}

				// Handle right character returning.
				if (!right.IsNullOrEmpty()) {
					sequence.Append(EnterCharacter(right, m_OnStageRight, m_OffStageRight));
				}

				// Handle left character returning.
				if (!left.IsNullOrEmpty()) {
					sequence.Append(EnterCharacter(left, m_OnStageLeft, m_OffStageLeft));
				}

				TextFade textFade = nextLine.GetComponentInChildren<TextFade>();

				if (textFade) { textFade.OnComplete.AddListener(Next); }
				else { DelayTracker.DelayAction(m_NextDelay, Next); }

				return;
			}

			if (m_Story.currentChoices.Count > 0) {
				for (int i = 0; i < m_Story.currentChoices.Count; i++) {
					Choice choice = m_Story.currentChoices[i];
					Button button = CreateChoiceView(choice.text.Trim());
					button.onClick.AddListener(() => OnClickChoiceButton(button, choice));
				}
			} else {
				Button choice = CreateChoiceView("End of story.\nRestart?");
				choice.onClick.AddListener(StartStory);
			}
		}

		private Tween ClearStage() { return DOTween.Sequence().Insert(0, ClearStageRight()).Insert(0, ClearStageLeft()); }

		private Tween ClearStageRight() { return ClearStageSide(m_OnStageRight, m_OffStageRight); }
		private Tween ClearStageLeft() { return ClearStageSide(m_OnStageLeft, m_OffStageLeft); }

		private Tween ClearStageSide(Transform onStage, Transform offStage) {
			if (onStage.childCount == 1) {
				Transform child = onStage.GetChild(0);
				return child.DOMove(offStage.position, m_SlideDuration).SetEase(Ease.InSine);
			}

			return DOTween.Sequence();
		}

		private Tween EnterCharacter(string character, Transform onStage, Transform offStage) {
			onStage.DestroyAllChildren();

			if (m_Assets.Characters.ContainsKey(character)) {
				GameObject go = Instantiate(m_Assets.Characters[character], onStage);
				go.transform.ResetTransform();
				go.transform.position = offStage.position;
				return go.transform.DOLocalMove(Vector3.zero, m_SlideDuration).SetEase(Ease.OutSine);
			}

			Debug.LogWarning("Can't find character '" + character + "'", this);
			return DOTween.Sequence();
		}

		private void OnClickChoiceButton(Button sender, Choice choice) {
			UnityUtils.DestroyObject(sender);
			m_Story.ChooseChoiceIndex(choice.index);
			m_FirstLine = true;
			Refresh();
		}

		private Line CreateContentView(string text, bool skipTransition = false) {
			Line linePrefab = GetLinePrefab(text);
			Line storyLine = Instantiate(linePrefab, m_Canvas.transform, false);
			storyLine.SetText(text, skipTransition);
			return storyLine;
		}

		private Line GetLinePrefab(string text) {
			if (text.Contains("<h4")) { return m_SubtitleLinePrefab; }
			if (text.Contains("<h")) { return m_TitleLinePrefab; }
			if (text.StartsWith("(") || m_Story.currentTags.Contains("desc") || m_Story.currentTags.Contains("writing")) { return m_DefaultTextPrefab; }
			if (text.Contains("<i>")) { return m_PlayerTextPrefab; }
			return m_NPCTextPrefab;
		}

		private Button CreateChoiceView(string text) {
			Button choice = Instantiate(m_ButtonPrefab, m_Canvas.transform, false);
			Line line = choice.GetComponent<Line>();
			line.SetText(text);
			return choice;
		}
	}
}