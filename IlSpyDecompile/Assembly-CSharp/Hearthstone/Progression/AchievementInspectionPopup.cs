using System;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	public class AchievementInspectionPopup : MonoBehaviour
	{
		private Widget m_widget;

		private void Awake()
		{
			m_widget = GetComponent<WidgetTemplate>();
			m_widget.RegisterEventListener(WidgetEventListener);
		}

		private void OnDisable()
		{
			m_widget.UnbindDataModel(222);
		}

		private void WidgetEventListener(string eventName)
		{
			if (!(eventName == "CODE_SELECT_PREVIOUS_ACHIEVEMENT"))
			{
				if (eventName == "CODE_SELECT_NEXT_ACHIEVEMENT")
				{
					SelectNextAchievement();
				}
			}
			else
			{
				SelectPreviousAchievement();
			}
		}

		private void SelectPreviousAchievement()
		{
			WithBoundAchievementAndSection(delegate(AchievementDataModel achievement, AchievementSectionDataModel section)
			{
				AchievementDataModel achievementDataModel = achievement.FindPreviousAchievement(section.Achievements.Achievements);
				if (achievementDataModel == null)
				{
					Debug.LogWarning($"No previous found for {achievement.ID}.");
				}
				else
				{
					m_widget.BindDataModel(achievementDataModel);
				}
			});
		}

		private void SelectNextAchievement()
		{
			WithBoundAchievementAndSection(delegate(AchievementDataModel achievement, AchievementSectionDataModel section)
			{
				AchievementDataModel achievementDataModel = achievement.FindNextAchievement(section.Achievements.Achievements);
				if (achievementDataModel == null)
				{
					Debug.LogWarning($"No next found for {achievement.ID}.");
				}
				else
				{
					m_widget.BindDataModel(achievementDataModel);
				}
			});
		}

		private void WithBoundAchievementAndSection(Action<AchievementDataModel, AchievementSectionDataModel> action)
		{
			AchievementDataModel dataModel = m_widget.GetDataModel<AchievementDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state: no bound achievement.");
				return;
			}
			AchievementDbfRecord record = GameDbf.Achievement.GetRecord(dataModel.ID);
			AchievementSectionDataModel achievementSectionDataModel = (from category in AchievementManager.Get().Categories.Categories
				where category.SelectedSubcategory != null
				select category.SelectedSubcategory).SelectMany((AchievementSubcategoryDataModel subcategory) => subcategory.Sections.Sections).FirstOrDefault((AchievementSectionDataModel element) => element.ID == record.AchievementSection);
			if (achievementSectionDataModel == null)
			{
				Debug.LogWarning($"Failed to find section for achievement {dataModel.ID}.");
			}
			else
			{
				action?.Invoke(dataModel, achievementSectionDataModel);
			}
		}
	}
}
