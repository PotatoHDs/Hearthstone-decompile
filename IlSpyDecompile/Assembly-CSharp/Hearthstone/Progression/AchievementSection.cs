using System;
using System.Collections;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	[RequireComponent(typeof(WidgetTemplate))]
	public class AchievementSection : MonoBehaviour
	{
		public const string SECTION_CHANGED = "CODE_SECTION_CHANGED";

		public const string HIDE_ACHIEVEMENT_SECTION_HEADER = "CODE_HIDE_ACHIEVEMENT_SECTION_HEADER";

		public const string SHOW_ACHIEVEMENT_SECTION_HEADER = "CODE_SHOW_ACHIEVEMENT_SECTION_HEADER";

		public Widget m_leftColumnListWidget;

		public Widget m_rightColumnListWidget;

		[Tooltip("Per-achievement delay to stagger the appearance of achievements within the list.")]
		public float m_streamDelay = 0.08f;

		[Tooltip("Max number of achievements to delay before revealing them all.")]
		public int m_streamMax = 10;

		private readonly AchievementListDataModel m_leftAchievements = new AchievementListDataModel();

		private readonly AchievementListDataModel m_rightAchievements = new AchievementListDataModel();

		private Widget m_widget;

		private bool m_waitingForLeftColumnLayout;

		private bool m_waitingForRightColumnLayout;

		public event Action<AchievementSectionDataModel> OnSectionChanged = delegate
		{
		};

		public event Action<AchievementSectionDataModel> OnSectionShown = delegate
		{
		};

		private void Awake()
		{
			m_widget = GetComponent<WidgetTemplate>();
			m_widget.RegisterEventListener(HandleEvent);
			m_leftColumnListWidget.BindDataModel(m_leftAchievements);
			m_rightColumnListWidget.BindDataModel(m_rightAchievements);
			AchievementManager.Get().OnStatusChanged += OnStatusChanged;
			AchievementManager.Get().OnProgressChanged += OnProgressChanged;
			AchievementManager.Get().OnCompletedDateChanged += OnCompletedDateChanged;
		}

		private void OnDisable()
		{
			StopAllCoroutines();
		}

		private void OnDestroy()
		{
			if (AchievementManager.Get() != null)
			{
				AchievementManager.Get().OnStatusChanged -= OnStatusChanged;
				AchievementManager.Get().OnProgressChanged -= OnProgressChanged;
				AchievementManager.Get().OnCompletedDateChanged -= OnCompletedDateChanged;
			}
		}

		private void HandleEvent(string eventName)
		{
			if (eventName == "CODE_SECTION_CHANGED")
			{
				HandleSectionChanged();
			}
		}

		private void HandleSectionChanged()
		{
			AchievementSectionDataModel section = m_widget.GetDataModel<AchievementSectionDataModel>();
			if (section == null)
			{
				Debug.LogWarning("Unexpected state: no bound section");
				return;
			}
			StopAllCoroutines();
			HideHeader();
			UpdateAchievementLists(section);
			m_waitingForLeftColumnLayout = m_leftAchievements.Achievements.Count > 0;
			m_waitingForRightColumnLayout = m_rightAchievements.Achievements.Count > 0;
			this.OnSectionChanged(section);
			Listable componentInChildren = m_leftColumnListWidget.GetComponentInChildren<Listable>();
			if (componentInChildren != null)
			{
				componentInChildren.RegisterDoneChangingStatesListener(delegate
				{
					m_waitingForLeftColumnLayout = false;
					ShowIfReady(section);
				}, null, callImmediatelyIfSet: true, doOnce: true);
			}
			Listable componentInChildren2 = m_rightColumnListWidget.GetComponentInChildren<Listable>();
			if (componentInChildren2 != null)
			{
				componentInChildren2.RegisterDoneChangingStatesListener(delegate
				{
					m_waitingForRightColumnLayout = false;
					ShowIfReady(section);
				}, null, callImmediatelyIfSet: true, doOnce: true);
			}
		}

		private void UpdateAchievementLists(AchievementSectionDataModel section)
		{
			if (m_widget.GetDataModel<AchievementSectionListDataModel>() == null)
			{
				Debug.LogWarning("Unexpected state: no bound section list");
				return;
			}
			DataModelList<AchievementDataModel> currentSortedAchievements = section.GetCurrentSortedAchievements();
			m_leftAchievements.Achievements = currentSortedAchievements.Where((AchievementDataModel _, int index) => index % 2 == 0).ToDataModelList();
			m_rightAchievements.Achievements = currentSortedAchievements.Where((AchievementDataModel _, int index) => index % 2 != 0).ToDataModelList();
		}

		private void OnStatusChanged(int achievementId, AchievementManager.AchievementStatus status)
		{
			AchievementSectionDataModel dataModel = m_widget.GetDataModel<AchievementSectionDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state: no bound section");
				return;
			}
			AchievementDataModel achievementDataModel = dataModel.Achievements.Achievements.FirstOrDefault((AchievementDataModel element) => element.ID == achievementId);
			if (achievementDataModel != null)
			{
				if (status == AchievementManager.AchievementStatus.REWARD_GRANTED || status == AchievementManager.AchievementStatus.REWARD_ACKED)
				{
					SelectNextTier(achievementDataModel);
				}
				achievementDataModel.Status = status;
			}
		}

		private void OnProgressChanged(int achievementId, int progress)
		{
			AchievementDataModel achievementDataModel = FindAchievement(achievementId);
			if (achievementDataModel != null)
			{
				achievementDataModel.Progress = progress;
				achievementDataModel.UpdateProgress();
			}
		}

		private void OnCompletedDateChanged(int achievementId, long completedDate)
		{
			AchievementDataModel achievementDataModel = FindAchievement(achievementId);
			if (achievementDataModel != null)
			{
				achievementDataModel.CompletionDate = ProgressUtils.FormatAchievementCompletionDate(completedDate);
			}
		}

		private AchievementDataModel FindAchievement(int achievementId)
		{
			AchievementSectionDataModel dataModel = m_widget.GetDataModel<AchievementSectionDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state: no bound section");
				return null;
			}
			return dataModel.Achievements.Achievements.FirstOrDefault((AchievementDataModel element) => element.ID == achievementId);
		}

		private void SelectNextTier(AchievementDataModel achievement)
		{
			AchievementSectionDataModel dataModel = m_widget.GetDataModel<AchievementSectionDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state: no bound section");
				return;
			}
			AchievementDataModel achievementDataModel = achievement.FindNextAchievement(dataModel.Achievements.Achievements);
			if (achievementDataModel != null)
			{
				ReplaceAchievement(achievement, achievementDataModel, m_leftAchievements);
				ReplaceAchievement(achievement, achievementDataModel, m_rightAchievements);
			}
		}

		private static void ReplaceAchievement(AchievementDataModel oldAchievement, AchievementDataModel newAchievement, AchievementListDataModel list)
		{
			if (list != null)
			{
				int num = list.Achievements.IndexOf(oldAchievement);
				if (num >= 0)
				{
					list.Achievements[num] = newAchievement;
				}
			}
		}

		private void ShowIfReady(AchievementSectionDataModel section)
		{
			if (!m_waitingForLeftColumnLayout && !m_waitingForRightColumnLayout)
			{
				StartCoroutine(WaitAndShowHeader(section));
			}
		}

		private void HideHeader()
		{
			m_widget.TriggerEvent("CODE_HIDE_ACHIEVEMENT_SECTION_HEADER");
		}

		private IEnumerator WaitAndShowHeader(AchievementSectionDataModel section)
		{
			yield return new WaitForSeconds(ComputeSectionDelay(section));
			m_widget.TriggerEvent("CODE_SHOW_ACHIEVEMENT_SECTION_HEADER");
			this.OnSectionShown(section);
		}

		private float ComputeSectionDelay(AchievementSectionDataModel section)
		{
			return (float)(1 + Math.Min(section.PreviousTileCount, m_streamMax)) * m_streamDelay;
		}
	}
}
