using System;

// Token: 0x02000667 RID: 1639
public class CardRewardData : RewardData
{
	// Token: 0x06005C28 RID: 23592 RVA: 0x001DF903 File Offset: 0x001DDB03
	public CardRewardData() : this("", TAG_PREMIUM.NORMAL, 0)
	{
	}

	// Token: 0x06005C29 RID: 23593 RVA: 0x001DF912 File Offset: 0x001DDB12
	public CardRewardData(string cardID, TAG_PREMIUM premium, int count) : this(cardID, premium, count, "", "")
	{
	}

	// Token: 0x06005C2A RID: 23594 RVA: 0x001DF927 File Offset: 0x001DDB27
	public CardRewardData(string cardID, TAG_PREMIUM premium, int count, string nameOverride, string descriptionOverride) : base(Reward.Type.CARD)
	{
		this.CardID = cardID;
		this.Count = count;
		this.Premium = premium;
		base.NameOverride = nameOverride;
		base.DescriptionOverride = descriptionOverride;
		this.InnKeeperLine = CardRewardData.InnKeeperTrigger.NONE;
	}

	// Token: 0x17000569 RID: 1385
	// (get) Token: 0x06005C2B RID: 23595 RVA: 0x001DF95C File Offset: 0x001DDB5C
	// (set) Token: 0x06005C2C RID: 23596 RVA: 0x001DF964 File Offset: 0x001DDB64
	public string CardID { get; set; }

	// Token: 0x1700056A RID: 1386
	// (get) Token: 0x06005C2D RID: 23597 RVA: 0x001DF96D File Offset: 0x001DDB6D
	// (set) Token: 0x06005C2E RID: 23598 RVA: 0x001DF975 File Offset: 0x001DDB75
	public TAG_PREMIUM Premium { get; set; }

	// Token: 0x1700056B RID: 1387
	// (get) Token: 0x06005C2F RID: 23599 RVA: 0x001DF97E File Offset: 0x001DDB7E
	// (set) Token: 0x06005C30 RID: 23600 RVA: 0x001DF986 File Offset: 0x001DDB86
	public int Count { get; set; }

	// Token: 0x1700056C RID: 1388
	// (get) Token: 0x06005C31 RID: 23601 RVA: 0x001DF98F File Offset: 0x001DDB8F
	// (set) Token: 0x06005C32 RID: 23602 RVA: 0x001DF997 File Offset: 0x001DDB97
	public CardRewardData.InnKeeperTrigger InnKeeperLine { get; set; }

	// Token: 0x1700056D RID: 1389
	// (get) Token: 0x06005C33 RID: 23603 RVA: 0x001DF9A0 File Offset: 0x001DDBA0
	// (set) Token: 0x06005C34 RID: 23604 RVA: 0x001DF9A8 File Offset: 0x001DDBA8
	public FixedRewardMapDbfRecord FixedReward { get; set; }

	// Token: 0x06005C35 RID: 23605 RVA: 0x001DF9B4 File Offset: 0x001DDBB4
	public override string ToString()
	{
		EntityDef entityDef = DefLoader.Get().GetEntityDef(this.CardID);
		string text = (entityDef == null) ? "[UNKNOWN]" : entityDef.GetName();
		return string.Format("[CardRewardData: cardName={0} CardID={1}, Premium={2} Count={3} Origin={4} OriginData={5}]", new object[]
		{
			text,
			this.CardID,
			this.Premium,
			this.Count,
			base.Origin,
			base.OriginData
		});
	}

	// Token: 0x06005C36 RID: 23606 RVA: 0x001DFA38 File Offset: 0x001DDC38
	public bool Merge(CardRewardData other)
	{
		if (!this.CardID.Equals(other.CardID))
		{
			return false;
		}
		if (!this.Premium.Equals(other.Premium))
		{
			return false;
		}
		this.Count += other.Count;
		foreach (long noticeID in other.m_noticeIDs)
		{
			base.AddNoticeID(noticeID);
		}
		return true;
	}

	// Token: 0x06005C37 RID: 23607 RVA: 0x001DFAD8 File Offset: 0x001DDCD8
	protected override string GetAssetPath()
	{
		return "CardReward.prefab:1616f07db0f63a749b83b788f5a8c584";
	}

	// Token: 0x0200217C RID: 8572
	public enum InnKeeperTrigger
	{
		// Token: 0x0400E073 RID: 57459
		NONE,
		// Token: 0x0400E074 RID: 57460
		CORE_CLASS_SET_COMPLETE,
		// Token: 0x0400E075 RID: 57461
		SECOND_REWARD_EVER
	}
}
