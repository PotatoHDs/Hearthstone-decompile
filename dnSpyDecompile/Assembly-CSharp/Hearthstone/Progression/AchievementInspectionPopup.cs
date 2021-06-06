using System;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x02001102 RID: 4354
	public class AchievementInspectionPopup : MonoBehaviour
	{
		// Token: 0x0600BEA9 RID: 48809 RVA: 0x003A1D27 File Offset: 0x0039FF27
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_widget.RegisterEventListener(new Widget.EventListenerDelegate(this.WidgetEventListener));
		}

		// Token: 0x0600BEAA RID: 48810 RVA: 0x003A1D4C File Offset: 0x0039FF4C
		private void OnDisable()
		{
			this.m_widget.UnbindDataModel(222);
		}

		// Token: 0x0600BEAB RID: 48811 RVA: 0x003A1D5E File Offset: 0x0039FF5E
		private void WidgetEventListener(string eventName)
		{
			if (eventName == "CODE_SELECT_PREVIOUS_ACHIEVEMENT")
			{
				this.SelectPreviousAchievement();
				return;
			}
			if (!(eventName == "CODE_SELECT_NEXT_ACHIEVEMENT"))
			{
				return;
			}
			this.SelectNextAchievement();
		}

		// Token: 0x0600BEAC RID: 48812 RVA: 0x003A1D88 File Offset: 0x0039FF88
		private void SelectPreviousAchievement()
		{
			this.WithBoundAchievementAndSection(delegate(AchievementDataModel achievement, AchievementSectionDataModel section)
			{
				AchievementDataModel achievementDataModel = achievement.FindPreviousAchievement(section.Achievements.Achievements);
				if (achievementDataModel == null)
				{
					Debug.LogWarning(string.Format("No previous found for {0}.", achievement.ID));
					return;
				}
				this.m_widget.BindDataModel(achievementDataModel, false);
			});
		}

		// Token: 0x0600BEAD RID: 48813 RVA: 0x003A1D9C File Offset: 0x0039FF9C
		private void SelectNextAchievement()
		{
			this.WithBoundAchievementAndSection(delegate(AchievementDataModel achievement, AchievementSectionDataModel section)
			{
				AchievementDataModel achievementDataModel = achievement.FindNextAchievement(section.Achievements.Achievements);
				if (achievementDataModel == null)
				{
					Debug.LogWarning(string.Format("No next found for {0}.", achievement.ID));
					return;
				}
				this.m_widget.BindDataModel(achievementDataModel, false);
			});
		}

		// Token: 0x0600BEAE RID: 48814 RVA: 0x003A1DB0 File Offset: 0x0039FFB0
		private void WithBoundAchievementAndSection(Action<AchievementDataModel, AchievementSectionDataModel> action)
		{
			AchievementDataModel dataModel = this.m_widget.GetDataModel<AchievementDataModel>();
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
				Debug.LogWarning(string.Format("Failed to find section for achievement {0}.", dataModel.ID));
				return;
			}
			if (action != null)
			{
				action(dataModel, achievementSectionDataModel);
			}
		}

		// Token: 0x04009B10 RID: 39696
		private Widget m_widget;
	}
}
