using System;

// Token: 0x02000665 RID: 1637
public class CardBackRewardData : RewardData
{
	// Token: 0x06005C10 RID: 23568 RVA: 0x001DEDC4 File Offset: 0x001DCFC4
	public CardBackRewardData() : this(0)
	{
	}

	// Token: 0x06005C11 RID: 23569 RVA: 0x001DEDCD File Offset: 0x001DCFCD
	public CardBackRewardData(int cardBackID) : this(cardBackID, "", "")
	{
	}

	// Token: 0x06005C12 RID: 23570 RVA: 0x001DEDE0 File Offset: 0x001DCFE0
	public CardBackRewardData(int cardBackID, string nameOverride, string descriptionOverride) : base(Reward.Type.CARD_BACK)
	{
		this.CardBackID = cardBackID;
		base.NameOverride = nameOverride;
		base.DescriptionOverride = descriptionOverride;
	}

	// Token: 0x17000568 RID: 1384
	// (get) Token: 0x06005C13 RID: 23571 RVA: 0x001DEDFE File Offset: 0x001DCFFE
	// (set) Token: 0x06005C14 RID: 23572 RVA: 0x001DEE06 File Offset: 0x001DD006
	public int CardBackID { get; set; }

	// Token: 0x06005C15 RID: 23573 RVA: 0x001DEE0F File Offset: 0x001DD00F
	public override string ToString()
	{
		return string.Format("[CardBackRewardData: CardBackID={0} Origin={1} OriginData={2}]", this.CardBackID, base.Origin, base.OriginData);
	}

	// Token: 0x06005C16 RID: 23574 RVA: 0x001DEE3C File Offset: 0x001DD03C
	protected override string GetAssetPath()
	{
		return "CardBackReward.prefab:78de3cb2a35ead84eb8ab14ad388708c";
	}
}
