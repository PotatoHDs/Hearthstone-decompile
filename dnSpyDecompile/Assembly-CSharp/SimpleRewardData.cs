using System;

// Token: 0x02000680 RID: 1664
public class SimpleRewardData : RewardData
{
	// Token: 0x06005D37 RID: 23863 RVA: 0x001E4C8D File Offset: 0x001E2E8D
	public SimpleRewardData(Reward.Type rewardType) : this(rewardType, 0)
	{
	}

	// Token: 0x06005D38 RID: 23864 RVA: 0x001E4C97 File Offset: 0x001E2E97
	public SimpleRewardData(Reward.Type rewardType, int amount) : this(rewardType, amount, "", "")
	{
	}

	// Token: 0x06005D39 RID: 23865 RVA: 0x001E4CAB File Offset: 0x001E2EAB
	public SimpleRewardData(Reward.Type rewardType, int amount, string nameOverride, string descriptionOverride) : base(rewardType)
	{
		this.Amount = amount;
		base.NameOverride = nameOverride;
		base.DescriptionOverride = descriptionOverride;
		this.PrefabAssetPath = "SimpleReward.prefab:c1bb382528839b54e9da96bc1db806cf";
		this.RewardHeadlineText = "";
	}

	// Token: 0x17000588 RID: 1416
	// (get) Token: 0x06005D3A RID: 23866 RVA: 0x001E4CE0 File Offset: 0x001E2EE0
	// (set) Token: 0x06005D3B RID: 23867 RVA: 0x001E4CE8 File Offset: 0x001E2EE8
	public int Amount { get; set; }

	// Token: 0x17000589 RID: 1417
	// (get) Token: 0x06005D3C RID: 23868 RVA: 0x001E4CF1 File Offset: 0x001E2EF1
	// (set) Token: 0x06005D3D RID: 23869 RVA: 0x001E4CF9 File Offset: 0x001E2EF9
	public string PrefabAssetPath { get; set; }

	// Token: 0x1700058A RID: 1418
	// (get) Token: 0x06005D3E RID: 23870 RVA: 0x001E4D02 File Offset: 0x001E2F02
	// (set) Token: 0x06005D3F RID: 23871 RVA: 0x001E4D0A File Offset: 0x001E2F0A
	public string RewardHeadlineText { get; set; }

	// Token: 0x06005D40 RID: 23872 RVA: 0x001E4D14 File Offset: 0x001E2F14
	public override string ToString()
	{
		return string.Format("[SimpleRewardData: Type={0} Amount={1} Origin={2} OriginData={3}]", new object[]
		{
			base.RewardType,
			this.Amount,
			base.Origin,
			base.OriginData
		});
	}

	// Token: 0x06005D41 RID: 23873 RVA: 0x001E4D69 File Offset: 0x001E2F69
	protected override string GetAssetPath()
	{
		return this.PrefabAssetPath;
	}
}
