using System;

// Token: 0x02000678 RID: 1656
public class RandomCardRewardData : RewardData
{
	// Token: 0x06005CA1 RID: 23713 RVA: 0x001E0EA3 File Offset: 0x001DF0A3
	public RandomCardRewardData() : this(TAG_RARITY.INVALID)
	{
	}

	// Token: 0x06005CA2 RID: 23714 RVA: 0x001E0EAC File Offset: 0x001DF0AC
	public RandomCardRewardData(TAG_RARITY rarity) : this(rarity, TAG_PREMIUM.NORMAL)
	{
	}

	// Token: 0x06005CA3 RID: 23715 RVA: 0x001E0EB6 File Offset: 0x001DF0B6
	public RandomCardRewardData(TAG_RARITY rarity, TAG_PREMIUM premium) : this(rarity, premium, 1)
	{
	}

	// Token: 0x06005CA4 RID: 23716 RVA: 0x001E0EC1 File Offset: 0x001DF0C1
	public RandomCardRewardData(TAG_RARITY rarity, TAG_PREMIUM premium, int count) : base(Reward.Type.RANDOM_CARD)
	{
		this.Rarity = rarity;
		this.Premium = premium;
		this.Count = count;
	}

	// Token: 0x17000577 RID: 1399
	// (get) Token: 0x06005CA5 RID: 23717 RVA: 0x001E0EE0 File Offset: 0x001DF0E0
	// (set) Token: 0x06005CA6 RID: 23718 RVA: 0x001E0EE8 File Offset: 0x001DF0E8
	public TAG_RARITY Rarity { get; set; }

	// Token: 0x17000578 RID: 1400
	// (get) Token: 0x06005CA7 RID: 23719 RVA: 0x001E0EF1 File Offset: 0x001DF0F1
	// (set) Token: 0x06005CA8 RID: 23720 RVA: 0x001E0EF9 File Offset: 0x001DF0F9
	public TAG_PREMIUM Premium { get; set; }

	// Token: 0x17000579 RID: 1401
	// (get) Token: 0x06005CA9 RID: 23721 RVA: 0x001E0F02 File Offset: 0x001DF102
	// (set) Token: 0x06005CAA RID: 23722 RVA: 0x001E0F0A File Offset: 0x001DF10A
	public int Count { get; set; }

	// Token: 0x06005CAB RID: 23723 RVA: 0x001E0F14 File Offset: 0x001DF114
	public override string ToString()
	{
		return string.Format("[RandomCardRewardData: Origin={0} OriginData={1} Rarity={2}, Premium={3} Count={4}]", new object[]
		{
			base.Origin,
			base.OriginData,
			this.Rarity,
			this.Premium,
			this.Count
		});
	}

	// Token: 0x06005CAC RID: 23724 RVA: 0x000D5239 File Offset: 0x000D3439
	protected override string GetAssetPath()
	{
		return "";
	}
}
