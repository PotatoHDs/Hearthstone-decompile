public class RandomCardRewardData : RewardData
{
	public TAG_RARITY Rarity { get; set; }

	public TAG_PREMIUM Premium { get; set; }

	public int Count { get; set; }

	public RandomCardRewardData()
		: this(TAG_RARITY.INVALID)
	{
	}

	public RandomCardRewardData(TAG_RARITY rarity)
		: this(rarity, TAG_PREMIUM.NORMAL)
	{
	}

	public RandomCardRewardData(TAG_RARITY rarity, TAG_PREMIUM premium)
		: this(rarity, premium, 1)
	{
	}

	public RandomCardRewardData(TAG_RARITY rarity, TAG_PREMIUM premium, int count)
		: base(Reward.Type.RANDOM_CARD)
	{
		Rarity = rarity;
		Premium = premium;
		Count = count;
	}

	public override string ToString()
	{
		return $"[RandomCardRewardData: Origin={base.Origin} OriginData={base.OriginData} Rarity={Rarity}, Premium={Premium} Count={Count}]";
	}

	protected override string GetAssetPath()
	{
		return "";
	}
}
