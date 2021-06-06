using System;
using Assets;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F65 RID: 3941
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event based on quest info.")]
	public class QuestEventAction : FsmStateAction
	{
		// Token: 0x0600AD1D RID: 44317 RVA: 0x0035FF13 File Offset: 0x0035E113
		public override void Reset()
		{
			this.m_QuestObject = null;
			this.m_AchievementType = Achieve.Type.INVALID;
			this.m_ActivatedBySpecialEventType = SpecialEventType.UNKNOWN;
		}

		// Token: 0x0600AD1E RID: 44318 RVA: 0x0035FF2C File Offset: 0x0035E12C
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_QuestObject != null)
			{
				global::Achievement achievement = null;
				GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_QuestObject);
				if (ownerDefaultTarget != null)
				{
					QuestTile componentInParent = ownerDefaultTarget.GetComponentInParent<QuestTile>();
					if (componentInParent != null)
					{
						achievement = componentInParent.GetQuest();
					}
					if (achievement == null)
					{
						FriendlyChallengeDialog componentInParent2 = ownerDefaultTarget.GetComponentInParent<FriendlyChallengeDialog>();
						if (componentInParent2 != null)
						{
							achievement = componentInParent2.GetQuest();
						}
					}
					if (achievement != null)
					{
						if (this.CheckAchieve(achievement))
						{
							if (this.m_SendEventOnMatch != null)
							{
								base.Fsm.Event(this.m_SendEventOnMatch);
							}
						}
						else if (this.m_SendEventOnNotMatch != null)
						{
							base.Fsm.Event(this.m_SendEventOnNotMatch);
						}
					}
					else
					{
						Debug.LogError("No Achievement found! (QuestTile or FriendlyChallengeDialog required in Parent chain of FsmOwnerDefault)");
					}
				}
			}
			base.Finish();
		}

		// Token: 0x0600AD1F RID: 44319 RVA: 0x0035FFEC File Offset: 0x0035E1EC
		private bool CheckAchieve(global::Achievement achieve)
		{
			if (this.m_AchievementType != Achieve.Type.INVALID && achieve.AchieveType != this.m_AchievementType)
			{
				return false;
			}
			if (this.m_IsLegendary && !achieve.IsLegendary)
			{
				return false;
			}
			if (this.m_ActivatedBySpecialEventType != SpecialEventType.UNKNOWN)
			{
				AchieveRegionDataDbfRecord currentRegionData = achieve.GetCurrentRegionData();
				if (currentRegionData == null)
				{
					return false;
				}
				if (EnumUtils.GetString<SpecialEventType>(this.m_ActivatedBySpecialEventType) != currentRegionData.ActivateEvent)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040093FF RID: 37887
		public FsmOwnerDefault m_QuestObject;

		// Token: 0x04009400 RID: 37888
		public Achieve.Type m_AchievementType;

		// Token: 0x04009401 RID: 37889
		public SpecialEventType m_ActivatedBySpecialEventType;

		// Token: 0x04009402 RID: 37890
		public bool m_IsLegendary;

		// Token: 0x04009403 RID: 37891
		public FsmEvent m_SendEventOnMatch;

		// Token: 0x04009404 RID: 37892
		public FsmEvent m_SendEventOnNotMatch;
	}
}
