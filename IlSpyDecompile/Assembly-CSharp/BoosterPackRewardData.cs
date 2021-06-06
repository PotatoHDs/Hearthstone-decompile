public class BoosterPackRewardData : RewardData
{
	public int Id { get; set; }

	public int Count { get; set; }

	public BoosterPackRewardData()
		: this(0, 0)
	{
	}

	public BoosterPackRewardData(int id, int count)
		: this(id, count, "", "")
	{
	}

	public BoosterPackRewardData(int id, int count, string nameOverride, string descriptionOverride)
		: base(Reward.Type.BOOSTER_PACK)
	{
		Id = id;
		Count = count;
		base.NameOverride = nameOverride;
		base.DescriptionOverride = descriptionOverride;
	}

	public BoosterPackRewardData(int id, int count, int? rewardChestBagNum)
		: base(Reward.Type.BOOSTER_PACK)
	{
		Id = id;
		Count = count;
		base.RewardChestBagNum = rewardChestBagNum;
	}

	public override string ToString()
	{
		return $"[BoosterPackRewardData: BoosterType={Id} Count={Count} Origin={base.Origin} OriginData={base.OriginData}]";
	}

	protected override string GetAssetPath()
	{
		return "BoosterPackReward.prefab:b3f2b69bf55efe2419ca6d55c46f7fa7";
	}
}
