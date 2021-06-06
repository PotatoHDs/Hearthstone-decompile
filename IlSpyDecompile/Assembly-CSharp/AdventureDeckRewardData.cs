public class AdventureDeckRewardData : RewardData
{
	public AdventureDeckDbfRecord DeckRecord { get; set; }

	public AdventureDeckRewardData()
		: this(null)
	{
	}

	public AdventureDeckRewardData(AdventureDeckDbfRecord deckRecord)
		: base(Reward.Type.ADVENTURE_DECK)
	{
		DeckRecord = deckRecord;
	}

	public override string ToString()
	{
		return $"[DeckRewardData: DeckRecord.ID={DeckRecord.ID} DeckRecord.DeckId={DeckRecord.DeckId} Origin={base.Origin} OriginData={base.OriginData}]";
	}

	protected override string GetAssetPath()
	{
		return "AdventureDeckReward.prefab:0c5995063a72bd64e95d0a857e36386b";
	}
}
