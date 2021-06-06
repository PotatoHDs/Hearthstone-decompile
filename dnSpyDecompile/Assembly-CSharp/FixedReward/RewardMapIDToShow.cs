using System;

namespace FixedReward
{
	// Token: 0x02000B50 RID: 2896
	public class RewardMapIDToShow
	{
		// Token: 0x06009A6F RID: 39535 RVA: 0x0031B168 File Offset: 0x00319368
		public RewardMapIDToShow(int rewardMapID, int achieveID, int sortOrder)
		{
			this.rewardMapID = rewardMapID;
			this.achieveID = achieveID;
			this.sortOrder = sortOrder;
		}

		// Token: 0x06009A70 RID: 39536 RVA: 0x0031B188 File Offset: 0x00319388
		public override bool Equals(object obj)
		{
			RewardMapIDToShow rewardMapIDToShow;
			return (rewardMapIDToShow = (obj as RewardMapIDToShow)) != null && this.rewardMapID == rewardMapIDToShow.rewardMapID;
		}

		// Token: 0x06009A71 RID: 39537 RVA: 0x0031B1B0 File Offset: 0x003193B0
		public override int GetHashCode()
		{
			return this.rewardMapID.GetHashCode();
		}

		// Token: 0x04008014 RID: 32788
		public static readonly int NoAchieveID;

		// Token: 0x04008015 RID: 32789
		public readonly int rewardMapID;

		// Token: 0x04008016 RID: 32790
		public readonly int achieveID;

		// Token: 0x04008017 RID: 32791
		public readonly int sortOrder;
	}
}
