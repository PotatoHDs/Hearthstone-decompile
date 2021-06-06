using Assets;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event based on quest info.")]
	public class QuestEventAction : FsmStateAction
	{
		public FsmOwnerDefault m_QuestObject;

		public Achieve.Type m_AchievementType;

		public SpecialEventType m_ActivatedBySpecialEventType;

		public bool m_IsLegendary;

		public FsmEvent m_SendEventOnMatch;

		public FsmEvent m_SendEventOnNotMatch;

		public override void Reset()
		{
			m_QuestObject = null;
			m_AchievementType = Achieve.Type.INVALID;
			m_ActivatedBySpecialEventType = SpecialEventType.UNKNOWN;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_QuestObject != null)
			{
				Achievement achievement = null;
				GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_QuestObject);
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
						if (CheckAchieve(achievement))
						{
							if (m_SendEventOnMatch != null)
							{
								base.Fsm.Event(m_SendEventOnMatch);
							}
						}
						else if (m_SendEventOnNotMatch != null)
						{
							base.Fsm.Event(m_SendEventOnNotMatch);
						}
					}
					else
					{
						Debug.LogError("No Achievement found! (QuestTile or FriendlyChallengeDialog required in Parent chain of FsmOwnerDefault)");
					}
				}
			}
			Finish();
		}

		private bool CheckAchieve(Achievement achieve)
		{
			if (m_AchievementType != 0 && achieve.AchieveType != m_AchievementType)
			{
				return false;
			}
			if (m_IsLegendary && !achieve.IsLegendary)
			{
				return false;
			}
			if (m_ActivatedBySpecialEventType != SpecialEventType.UNKNOWN)
			{
				AchieveRegionDataDbfRecord currentRegionData = achieve.GetCurrentRegionData();
				if (currentRegionData == null)
				{
					return false;
				}
				if (EnumUtils.GetString(m_ActivatedBySpecialEventType) != currentRegionData.ActivateEvent)
				{
					return false;
				}
			}
			return true;
		}
	}
}
