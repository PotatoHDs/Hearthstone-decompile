public class AdventureHeroPowerRewardData : RewardData
{
	public AdventureHeroPowerDbfRecord HeroPowerRecord { get; set; }

	public AdventureHeroPowerRewardData()
		: this(null)
	{
	}

	public AdventureHeroPowerRewardData(AdventureHeroPowerDbfRecord heroPowerRecord)
		: base(Reward.Type.ADVENTURE_HERO_POWER)
	{
		HeroPowerRecord = heroPowerRecord;
	}

	public override string ToString()
	{
		return $"[HeroPowerRewardData: HeroPowerRecord.ID={HeroPowerRecord.ID} HeroPowerRecord.CardId={HeroPowerRecord.CardId} Origin={base.Origin} OriginData={base.OriginData}]";
	}

	protected override string GetAssetPath()
	{
		return "AdventureHeroPowerReward.prefab:e29f5f24b744e0648ab09119f324e781";
	}
}
