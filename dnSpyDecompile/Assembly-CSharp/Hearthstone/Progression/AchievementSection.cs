using System;
using System.Collections;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x02001105 RID: 4357
	[RequireComponent(typeof(WidgetTemplate))]
	public class AchievementSection : MonoBehaviour
	{
		// Token: 0x0600BEE0 RID: 48864 RVA: 0x003A2F60 File Offset: 0x003A1160
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_widget.RegisterEventListener(new Widget.EventListenerDelegate(this.HandleEvent));
			this.m_leftColumnListWidget.BindDataModel(this.m_leftAchievements, false);
			this.m_rightColumnListWidget.BindDataModel(this.m_rightAchievements, false);
			AchievementManager.Get().OnStatusChanged += this.OnStatusChanged;
			AchievementManager.Get().OnProgressChanged += this.OnProgressChanged;
			AchievementManager.Get().OnCompletedDateChanged += this.OnCompletedDateChanged;
		}

		// Token: 0x0600BEE1 RID: 48865 RVA: 0x000FD2FF File Offset: 0x000FB4FF
		private void OnDisable()
		{
			base.StopAllCoroutines();
		}

		// Token: 0x0600BEE2 RID: 48866 RVA: 0x003A2FF8 File Offset: 0x003A11F8
		private void OnDestroy()
		{
			if (AchievementManager.Get() != null)
			{
				AchievementManager.Get().OnStatusChanged -= this.OnStatusChanged;
				AchievementManager.Get().OnProgressChanged -= this.OnProgressChanged;
				AchievementManager.Get().OnCompletedDateChanged -= this.OnCompletedDateChanged;
			}
		}

		// Token: 0x140000CA RID: 202
		// (add) Token: 0x0600BEE3 RID: 48867 RVA: 0x003A3050 File Offset: 0x003A1250
		// (remove) Token: 0x0600BEE4 RID: 48868 RVA: 0x003A3088 File Offset: 0x003A1288
		public event Action<AchievementSectionDataModel> OnSectionChanged = delegate(AchievementSectionDataModel section)
		{
		};

		// Token: 0x140000CB RID: 203
		// (add) Token: 0x0600BEE5 RID: 48869 RVA: 0x003A30C0 File Offset: 0x003A12C0
		// (remove) Token: 0x0600BEE6 RID: 48870 RVA: 0x003A30F8 File Offset: 0x003A12F8
		public event Action<AchievementSectionDataModel> OnSectionShown = delegate(AchievementSectionDataModel section)
		{
		};

		// Token: 0x0600BEE7 RID: 48871 RVA: 0x003A312D File Offset: 0x003A132D
		private void HandleEvent(string eventName)
		{
			if (eventName == "CODE_SECTION_CHANGED")
			{
				this.HandleSectionChanged();
			}
		}

		// Token: 0x0600BEE8 RID: 48872 RVA: 0x003A3144 File Offset: 0x003A1344
		private void HandleSectionChanged()
		{
			AchievementSectionDataModel section = this.m_widget.GetDataModel<AchievementSectionDataModel>();
			if (section == null)
			{
				Debug.LogWarning("Unexpected state: no bound section");
				return;
			}
			base.StopAllCoroutines();
			this.HideHeader();
			this.UpdateAchievementLists(section);
			this.m_waitingForLeftColumnLayout = (this.m_leftAchievements.Achievements.Count > 0);
			this.m_waitingForRightColumnLayout = (this.m_rightAchievements.Achievements.Count > 0);
			this.OnSectionChanged(section);
			Listable componentInChildren = this.m_leftColumnListWidget.GetComponentInChildren<Listable>();
			if (componentInChildren != null)
			{
				componentInChildren.RegisterDoneChangingStatesListener(delegate(object _)
				{
					this.m_waitingForLeftColumnLayout = false;
					this.ShowIfReady(section);
				}, null, true, true);
			}
			Listable componentInChildren2 = this.m_rightColumnListWidget.GetComponentInChildren<Listable>();
			if (componentInChildren2 != null)
			{
				componentInChildren2.RegisterDoneChangingStatesListener(delegate(object _)
				{
					this.m_waitingForRightColumnLayout = false;
					this.ShowIfReady(section);
				}, null, true, true);
			}
		}

		// Token: 0x0600BEE9 RID: 48873 RVA: 0x003A3234 File Offset: 0x003A1434
		private void UpdateAchievementLists(AchievementSectionDataModel section)
		{
			if (this.m_widget.GetDataModel<AchievementSectionListDataModel>() == null)
			{
				Debug.LogWarning("Unexpected state: no bound section list");
				return;
			}
			DataModelList<AchievementDataModel> currentSortedAchievements = section.GetCurrentSortedAchievements();
			this.m_leftAchievements.Achievements = currentSortedAchievements.Where((AchievementDataModel _, int index) => index % 2 == 0).ToDataModelList<AchievementDataModel>();
			this.m_rightAchievements.Achievements = currentSortedAchievements.Where((AchievementDataModel _, int index) => index % 2 != 0).ToDataModelList<AchievementDataModel>();
		}

		// Token: 0x0600BEEA RID: 48874 RVA: 0x003A32CC File Offset: 0x003A14CC
		private void OnStatusChanged(int achievementId, AchievementManager.AchievementStatus status)
		{
			AchievementSectionDataModel dataModel = this.m_widget.GetDataModel<AchievementSectionDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state: no bound section");
				return;
			}
			AchievementDataModel achievementDataModel = dataModel.Achievements.Achievements.FirstOrDefault((AchievementDataModel element) => element.ID == achievementId);
			if (achievementDataModel == null)
			{
				return;
			}
			if (status == AchievementManager.AchievementStatus.REWARD_GRANTED || status == AchievementManager.AchievementStatus.REWARD_ACKED)
			{
				this.SelectNextTier(achievementDataModel);
			}
			achievementDataModel.Status = status;
		}

		// Token: 0x0600BEEB RID: 48875 RVA: 0x003A3338 File Offset: 0x003A1538
		private void OnProgressChanged(int achievementId, int progress)
		{
			AchievementDataModel achievementDataModel = this.FindAchievement(achievementId);
			if (achievementDataModel == null)
			{
				return;
			}
			achievementDataModel.Progress = progress;
			achievementDataModel.UpdateProgress();
		}

		// Token: 0x0600BEEC RID: 48876 RVA: 0x003A3360 File Offset: 0x003A1560
		private void OnCompletedDateChanged(int achievementId, long completedDate)
		{
			AchievementDataModel achievementDataModel = this.FindAchievement(achievementId);
			if (achievementDataModel == null)
			{
				return;
			}
			achievementDataModel.CompletionDate = ProgressUtils.FormatAchievementCompletionDate(completedDate);
		}

		// Token: 0x0600BEED RID: 48877 RVA: 0x003A3388 File Offset: 0x003A1588
		private AchievementDataModel FindAchievement(int achievementId)
		{
			AchievementSectionDataModel dataModel = this.m_widget.GetDataModel<AchievementSectionDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state: no bound section");
				return null;
			}
			return dataModel.Achievements.Achievements.FirstOrDefault((AchievementDataModel element) => element.ID == achievementId);
		}

		// Token: 0x0600BEEE RID: 48878 RVA: 0x003A33DC File Offset: 0x003A15DC
		private void SelectNextTier(AchievementDataModel achievement)
		{
			AchievementSectionDataModel dataModel = this.m_widget.GetDataModel<AchievementSectionDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state: no bound section");
				return;
			}
			AchievementDataModel achievementDataModel = achievement.FindNextAchievement(dataModel.Achievements.Achievements);
			if (achievementDataModel == null)
			{
				return;
			}
			AchievementSection.ReplaceAchievement(achievement, achievementDataModel, this.m_leftAchievements);
			AchievementSection.ReplaceAchievement(achievement, achievementDataModel, this.m_rightAchievements);
		}

		// Token: 0x0600BEEF RID: 48879 RVA: 0x003A3434 File Offset: 0x003A1634
		private static void ReplaceAchievement(AchievementDataModel oldAchievement, AchievementDataModel newAchievement, AchievementListDataModel list)
		{
			if (list == null)
			{
				return;
			}
			int num = list.Achievements.IndexOf(oldAchievement);
			if (num < 0)
			{
				return;
			}
			list.Achievements[num] = newAchievement;
		}

		// Token: 0x0600BEF0 RID: 48880 RVA: 0x003A3464 File Offset: 0x003A1664
		private void ShowIfReady(AchievementSectionDataModel section)
		{
			if (this.m_waitingForLeftColumnLayout)
			{
				return;
			}
			if (this.m_waitingForRightColumnLayout)
			{
				return;
			}
			base.StartCoroutine(this.WaitAndShowHeader(section));
		}

		// Token: 0x0600BEF1 RID: 48881 RVA: 0x003A3488 File Offset: 0x003A1688
		private void HideHeader()
		{
			this.m_widget.TriggerEvent("CODE_HIDE_ACHIEVEMENT_SECTION_HEADER", default(Widget.TriggerEventParameters));
		}

		// Token: 0x0600BEF2 RID: 48882 RVA: 0x003A34AF File Offset: 0x003A16AF
		private IEnumerator WaitAndShowHeader(AchievementSectionDataModel section)
		{
			yield return new WaitForSeconds(this.ComputeSectionDelay(section));
			this.m_widget.TriggerEvent("CODE_SHOW_ACHIEVEMENT_SECTION_HEADER", default(Widget.TriggerEventParameters));
			this.OnSectionShown(section);
			yield break;
		}

		// Token: 0x0600BEF3 RID: 48883 RVA: 0x003A34C5 File Offset: 0x003A16C5
		private float ComputeSectionDelay(AchievementSectionDataModel section)
		{
			return (float)(1 + Math.Min(section.PreviousTileCount, this.m_streamMax)) * this.m_streamDelay;
		}

		// Token: 0x04009B27 RID: 39719
		public const string SECTION_CHANGED = "CODE_SECTION_CHANGED";

		// Token: 0x04009B28 RID: 39720
		public const string HIDE_ACHIEVEMENT_SECTION_HEADER = "CODE_HIDE_ACHIEVEMENT_SECTION_HEADER";

		// Token: 0x04009B29 RID: 39721
		public const string SHOW_ACHIEVEMENT_SECTION_HEADER = "CODE_SHOW_ACHIEVEMENT_SECTION_HEADER";

		// Token: 0x04009B2A RID: 39722
		public Widget m_leftColumnListWidget;

		// Token: 0x04009B2B RID: 39723
		public Widget m_rightColumnListWidget;

		// Token: 0x04009B2C RID: 39724
		[Tooltip("Per-achievement delay to stagger the appearance of achievements within the list.")]
		public float m_streamDelay = 0.08f;

		// Token: 0x04009B2D RID: 39725
		[Tooltip("Max number of achievements to delay before revealing them all.")]
		public int m_streamMax = 10;

		// Token: 0x04009B2E RID: 39726
		private readonly AchievementListDataModel m_leftAchievements = new AchievementListDataModel();

		// Token: 0x04009B2F RID: 39727
		private readonly AchievementListDataModel m_rightAchievements = new AchievementListDataModel();

		// Token: 0x04009B30 RID: 39728
		private Widget m_widget;

		// Token: 0x04009B31 RID: 39729
		private bool m_waitingForLeftColumnLayout;

		// Token: 0x04009B32 RID: 39730
		private bool m_waitingForRightColumnLayout;
	}
}
