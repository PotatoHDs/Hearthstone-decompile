namespace FixedReward
{
	public class RewardMapIDToShow
	{
		public static readonly int NoAchieveID;

		public readonly int rewardMapID;

		public readonly int achieveID;

		public readonly int sortOrder;

		public RewardMapIDToShow(int rewardMapID, int achieveID, int sortOrder)
		{
			this.rewardMapID = rewardMapID;
			this.achieveID = achieveID;
			this.sortOrder = sortOrder;
		}

		public override bool Equals(object obj)
		{
			RewardMapIDToShow rewardMapIDToShow;
			if ((rewardMapIDToShow = obj as RewardMapIDToShow) == null)
			{
				return false;
			}
			return rewardMapID == rewardMapIDToShow.rewardMapID;
		}

		public override int GetHashCode()
		{
			return rewardMapID.GetHashCode();
		}
	}
}
