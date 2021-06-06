public class AdventureLoadoutTreasureRewardData : RewardData
{
	public AdventureLoadoutTreasuresDbfRecord LoadoutTreasureRecord { get; set; }

	public bool IsUpgrade { get; set; }

	public AdventureLoadoutTreasureRewardData()
		: this(null, isUpgrade: false)
	{
	}

	public AdventureLoadoutTreasureRewardData(AdventureLoadoutTreasuresDbfRecord loadoutTreasureRecord, bool isUpgrade)
		: base(Reward.Type.ADVENTURE_HERO_POWER)
	{
		LoadoutTreasureRecord = loadoutTreasureRecord;
		IsUpgrade = isUpgrade;
	}

	public override string ToString()
	{
		return $"[LoadoutTreasureRewardData: LoadoutTreasureRecord.ID={LoadoutTreasureRecord.ID} LoadoutTreasureRecord.CardId={LoadoutTreasureRecord.CardId} Origin={base.Origin} OriginData={base.OriginData}]";
	}

	protected override string GetAssetPath()
	{
		return "AdventureCardReward.prefab:a1f9fc20c145f4240a23991ee63220ff";
	}
}
