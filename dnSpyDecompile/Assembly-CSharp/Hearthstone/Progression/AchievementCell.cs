using System;
using System.Collections;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.Progression
{
	// Token: 0x020010F9 RID: 4345
	[RequireComponent(typeof(WidgetTemplate))]
	public class AchievementCell : MonoBehaviour
	{
		// Token: 0x0600BE6E RID: 48750 RVA: 0x003A0E18 File Offset: 0x0039F018
		private void Awake()
		{
			this.m_widget = base.GetComponent<WidgetTemplate>();
			this.m_widget.RegisterEventListener(new Widget.EventListenerDelegate(this.HandleEvent));
			this.HideTile();
			AchievementSection componentInParent = base.GetComponentInParent<AchievementSection>();
			if (componentInParent != null)
			{
				componentInParent.OnSectionChanged += this.HandleSectionChanged;
				componentInParent.OnSectionShown += this.HandleSectionShown;
			}
		}

		// Token: 0x0600BE6F RID: 48751 RVA: 0x003A0E84 File Offset: 0x0039F084
		private void OnDestroy()
		{
			base.StopAllCoroutines();
			this.m_waitingForClaimResponse = false;
			this.m_waitingForClaimAnimation = false;
			AchievementSection componentInParent = base.GetComponentInParent<AchievementSection>();
			if (componentInParent != null)
			{
				componentInParent.OnSectionChanged -= this.HandleSectionChanged;
				componentInParent.OnSectionShown -= this.HandleSectionShown;
			}
		}

		// Token: 0x0600BE70 RID: 48752 RVA: 0x003A0EDC File Offset: 0x0039F0DC
		private void HandleEvent(string eventName)
		{
			if (eventName == "CODE_CLAIM_ACHIEVEMENT")
			{
				this.HandleClaimAchievement();
				return;
			}
			if (eventName == "CODE_ACHIEVEMENT_CHANGED")
			{
				this.HandleCellAchievementChanged();
				return;
			}
			if (eventName == "CODE_CLAIM_RESPONSE_RECEIVED")
			{
				this.HandleClaimResponseReceived();
				return;
			}
			if (!(eventName == "CODE_CLAIM_ANIMATION_COMPLETED"))
			{
				return;
			}
			this.HandleClaimAnimationCompleted();
		}

		// Token: 0x0600BE71 RID: 48753 RVA: 0x003A0F3C File Offset: 0x0039F13C
		private void HandleClaimAchievement()
		{
			if (!Network.IsLoggedIn())
			{
				ProgressUtils.ShowOfflinePopup();
				return;
			}
			AchievementDataModel dataModel = this.m_widget.GetDataModel<AchievementDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state: no bound achievement.");
				return;
			}
			this.StartClaimSequence(dataModel.ID);
		}

		// Token: 0x0600BE72 RID: 48754 RVA: 0x003A0F7C File Offset: 0x0039F17C
		private void StartClaimSequence(int achievementId)
		{
			if (AchievementManager.Get().ClaimAchievementReward(achievementId, 0))
			{
				this.m_waitingForClaimResponse = true;
				this.m_waitingForClaimAnimation = true;
			}
		}

		// Token: 0x0600BE73 RID: 48755 RVA: 0x003A0F9A File Offset: 0x0039F19A
		private void HandleSectionChanged(AchievementSectionDataModel section)
		{
			base.StopAllCoroutines();
			this.m_waitingForClaimResponse = false;
			this.m_waitingForClaimAnimation = false;
			this.HideTile();
		}

		// Token: 0x0600BE74 RID: 48756 RVA: 0x003A0FB6 File Offset: 0x0039F1B6
		private void HandleSectionShown(AchievementSectionDataModel section)
		{
			base.StartCoroutine(this.WaitAndShowTile(this.ComputeAchievementDelay(section)));
		}

		// Token: 0x0600BE75 RID: 48757 RVA: 0x003A0FCC File Offset: 0x0039F1CC
		private void HandleCellAchievementChanged()
		{
			this.m_waitingForClaimResponse = false;
			this.UpdateIfReady();
		}

		// Token: 0x0600BE76 RID: 48758 RVA: 0x003A0FCC File Offset: 0x0039F1CC
		private void HandleClaimResponseReceived()
		{
			this.m_waitingForClaimResponse = false;
			this.UpdateIfReady();
		}

		// Token: 0x0600BE77 RID: 48759 RVA: 0x003A0FDB File Offset: 0x0039F1DB
		private void HandleClaimAnimationCompleted()
		{
			this.m_waitingForClaimAnimation = false;
			this.UpdateIfReady();
		}

		// Token: 0x0600BE78 RID: 48760 RVA: 0x003A0FEA File Offset: 0x0039F1EA
		private void UpdateIfReady()
		{
			if (this.m_waitingForClaimResponse)
			{
				return;
			}
			if (this.m_waitingForClaimAnimation)
			{
				return;
			}
			this.UpdateTile();
		}

		// Token: 0x0600BE79 RID: 48761 RVA: 0x003A1004 File Offset: 0x0039F204
		private void UpdateTile()
		{
			AchievementTile componentInChildren = base.GetComponentInChildren<AchievementTile>();
			if (componentInChildren == null)
			{
				Debug.LogWarning("Unexpected state: achievement tile not found");
				return;
			}
			AchievementDataModel dataModel = this.m_widget.GetDataModel<AchievementDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state: no bound achievement");
				return;
			}
			componentInChildren.BindDataModel(dataModel.CloneDataModel<AchievementDataModel>());
		}

		// Token: 0x0600BE7A RID: 48762 RVA: 0x003A1054 File Offset: 0x0039F254
		private void HideTile()
		{
			this.m_widget.TriggerEvent("CODE_HIDE_ACHIEVEMENT_TILE", default(Widget.TriggerEventParameters));
		}

		// Token: 0x0600BE7B RID: 48763 RVA: 0x003A107B File Offset: 0x0039F27B
		private IEnumerator WaitAndShowTile(float delay)
		{
			yield return new WaitForSeconds(delay);
			this.m_widget.TriggerEvent("CODE_SHOW_ACHIEVEMENT_TILE", default(Widget.TriggerEventParameters));
			yield break;
		}

		// Token: 0x0600BE7C RID: 48764 RVA: 0x003A1094 File Offset: 0x0039F294
		private float ComputeAchievementDelay(AchievementSectionDataModel section)
		{
			AchievementDataModel dataModel = this.m_widget.GetDataModel<AchievementDataModel>();
			if (dataModel == null)
			{
				Debug.LogWarning("Unexpected state: no bound achievement");
				return 0f;
			}
			int num = Math.Min(section.PreviousTileCount + section.GetCurrentSortedAchievements().IndexOf(dataModel), this.m_streamMax);
			return (float)Mathf.Max(0, num - section.PreviousTileCount) * this.m_streamDelay;
		}

		// Token: 0x04009B03 RID: 39683
		public const string CLAIM_ACHIEVEMENT = "CODE_CLAIM_ACHIEVEMENT";

		// Token: 0x04009B04 RID: 39684
		public const string SECTION_CHANGED = "CODE_SECTION_CHANGED";

		// Token: 0x04009B05 RID: 39685
		public const string ACHIEVEMENT_CHANGED = "CODE_ACHIEVEMENT_CHANGED";

		// Token: 0x04009B06 RID: 39686
		public const string CLAIM_RESPONSE_RECEIVED = "CODE_CLAIM_RESPONSE_RECEIVED";

		// Token: 0x04009B07 RID: 39687
		public const string CLAIM_ANIMATION_COMPLETED = "CODE_CLAIM_ANIMATION_COMPLETED";

		// Token: 0x04009B08 RID: 39688
		public const string HIDE_ACHIEVEMENT_TILE = "CODE_HIDE_ACHIEVEMENT_TILE";

		// Token: 0x04009B09 RID: 39689
		public const string SHOW_ACHIEVEMENT_TILE = "CODE_SHOW_ACHIEVEMENT_TILE";

		// Token: 0x04009B0A RID: 39690
		[Tooltip("Per-achievement delay to stagger the appearance of achievements within the list.")]
		public float m_streamDelay = 0.08f;

		// Token: 0x04009B0B RID: 39691
		[Tooltip("Max number of achievements to delay before revealing them all.")]
		public int m_streamMax = 10;

		// Token: 0x04009B0C RID: 39692
		private Widget m_widget;

		// Token: 0x04009B0D RID: 39693
		private bool m_waitingForClaimResponse;

		// Token: 0x04009B0E RID: 39694
		private bool m_waitingForClaimAnimation;
	}
}
