public class CardBackRewardData : RewardData
{
	public int CardBackID { get; set; }

	public CardBackRewardData()
		: this(0)
	{
	}

	public CardBackRewardData(int cardBackID)
		: this(cardBackID, "", "")
	{
	}

	public CardBackRewardData(int cardBackID, string nameOverride, string descriptionOverride)
		: base(Reward.Type.CARD_BACK)
	{
		CardBackID = cardBackID;
		base.NameOverride = nameOverride;
		base.DescriptionOverride = descriptionOverride;
	}

	public override string ToString()
	{
		return $"[CardBackRewardData: CardBackID={CardBackID} Origin={base.Origin} OriginData={base.OriginData}]";
	}

	protected override string GetAssetPath()
	{
		return "CardBackReward.prefab:78de3cb2a35ead84eb8ab14ad388708c";
	}
}
