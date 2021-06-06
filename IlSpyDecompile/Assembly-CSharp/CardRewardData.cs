public class CardRewardData : RewardData
{
	public enum InnKeeperTrigger
	{
		NONE,
		CORE_CLASS_SET_COMPLETE,
		SECOND_REWARD_EVER
	}

	public string CardID { get; set; }

	public TAG_PREMIUM Premium { get; set; }

	public int Count { get; set; }

	public InnKeeperTrigger InnKeeperLine { get; set; }

	public FixedRewardMapDbfRecord FixedReward { get; set; }

	public CardRewardData()
		: this("", TAG_PREMIUM.NORMAL, 0)
	{
	}

	public CardRewardData(string cardID, TAG_PREMIUM premium, int count)
		: this(cardID, premium, count, "", "")
	{
	}

	public CardRewardData(string cardID, TAG_PREMIUM premium, int count, string nameOverride, string descriptionOverride)
		: base(Reward.Type.CARD)
	{
		CardID = cardID;
		Count = count;
		Premium = premium;
		base.NameOverride = nameOverride;
		base.DescriptionOverride = descriptionOverride;
		InnKeeperLine = InnKeeperTrigger.NONE;
	}

	public override string ToString()
	{
		EntityDef entityDef = DefLoader.Get().GetEntityDef(CardID);
		string text = ((entityDef == null) ? "[UNKNOWN]" : entityDef.GetName());
		return $"[CardRewardData: cardName={text} CardID={CardID}, Premium={Premium} Count={Count} Origin={base.Origin} OriginData={base.OriginData}]";
	}

	public bool Merge(CardRewardData other)
	{
		if (!CardID.Equals(other.CardID))
		{
			return false;
		}
		if (!Premium.Equals(other.Premium))
		{
			return false;
		}
		Count += other.Count;
		foreach (long noticeID in other.m_noticeIDs)
		{
			AddNoticeID(noticeID);
		}
		return true;
	}

	protected override string GetAssetPath()
	{
		return "CardReward.prefab:1616f07db0f63a749b83b788f5a8c584";
	}
}
