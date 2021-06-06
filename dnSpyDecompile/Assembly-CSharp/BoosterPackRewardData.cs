using System;

// Token: 0x02000663 RID: 1635
public class BoosterPackRewardData : RewardData
{
	// Token: 0x06005BFF RID: 23551 RVA: 0x001DE9EB File Offset: 0x001DCBEB
	public BoosterPackRewardData() : this(0, 0)
	{
	}

	// Token: 0x06005C00 RID: 23552 RVA: 0x001DE9F5 File Offset: 0x001DCBF5
	public BoosterPackRewardData(int id, int count) : this(id, count, "", "")
	{
	}

	// Token: 0x06005C01 RID: 23553 RVA: 0x001DEA09 File Offset: 0x001DCC09
	public BoosterPackRewardData(int id, int count, string nameOverride, string descriptionOverride) : base(Reward.Type.BOOSTER_PACK)
	{
		this.Id = id;
		this.Count = count;
		base.NameOverride = nameOverride;
		base.DescriptionOverride = descriptionOverride;
	}

	// Token: 0x06005C02 RID: 23554 RVA: 0x001DEA2F File Offset: 0x001DCC2F
	public BoosterPackRewardData(int id, int count, int? rewardChestBagNum) : base(Reward.Type.BOOSTER_PACK)
	{
		this.Id = id;
		this.Count = count;
		base.RewardChestBagNum = rewardChestBagNum;
	}

	// Token: 0x17000566 RID: 1382
	// (get) Token: 0x06005C03 RID: 23555 RVA: 0x001DEA4D File Offset: 0x001DCC4D
	// (set) Token: 0x06005C04 RID: 23556 RVA: 0x001DEA55 File Offset: 0x001DCC55
	public int Id { get; set; }

	// Token: 0x17000567 RID: 1383
	// (get) Token: 0x06005C05 RID: 23557 RVA: 0x001DEA5E File Offset: 0x001DCC5E
	// (set) Token: 0x06005C06 RID: 23558 RVA: 0x001DEA66 File Offset: 0x001DCC66
	public int Count { get; set; }

	// Token: 0x06005C07 RID: 23559 RVA: 0x001DEA70 File Offset: 0x001DCC70
	public override string ToString()
	{
		return string.Format("[BoosterPackRewardData: BoosterType={0} Count={1} Origin={2} OriginData={3}]", new object[]
		{
			this.Id,
			this.Count,
			base.Origin,
			base.OriginData
		});
	}

	// Token: 0x06005C08 RID: 23560 RVA: 0x001DEAC5 File Offset: 0x001DCCC5
	protected override string GetAssetPath()
	{
		return "BoosterPackReward.prefab:b3f2b69bf55efe2419ca6d55c46f7fa7";
	}
}
