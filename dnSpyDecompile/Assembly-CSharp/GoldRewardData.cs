using System;

// Token: 0x02000673 RID: 1651
public class GoldRewardData : RewardData
{
	// Token: 0x06005C82 RID: 23682 RVA: 0x001E0BA0 File Offset: 0x001DEDA0
	public GoldRewardData() : this(0L)
	{
	}

	// Token: 0x06005C83 RID: 23683 RVA: 0x001E0BAC File Offset: 0x001DEDAC
	public GoldRewardData(long amount) : this(amount, null)
	{
	}

	// Token: 0x06005C84 RID: 23684 RVA: 0x001E0BC9 File Offset: 0x001DEDC9
	public GoldRewardData(long amount, DateTime? date) : this(amount, date, "", "")
	{
	}

	// Token: 0x06005C85 RID: 23685 RVA: 0x001E0BDD File Offset: 0x001DEDDD
	public GoldRewardData(long amount, DateTime? date, string nameOverride, string descriptionOverride) : base(Reward.Type.GOLD)
	{
		this.Amount = amount;
		this.Date = date;
		base.NameOverride = nameOverride;
		base.DescriptionOverride = descriptionOverride;
	}

	// Token: 0x06005C86 RID: 23686 RVA: 0x001E0C03 File Offset: 0x001DEE03
	public GoldRewardData(GoldRewardData oldData) : base(Reward.Type.GOLD)
	{
		this.Amount = oldData.Amount;
		this.Date = oldData.Date;
		base.NameOverride = oldData.NameOverride;
		base.DescriptionOverride = oldData.DescriptionOverride;
	}

	// Token: 0x17000573 RID: 1395
	// (get) Token: 0x06005C87 RID: 23687 RVA: 0x001E0C3C File Offset: 0x001DEE3C
	// (set) Token: 0x06005C88 RID: 23688 RVA: 0x001E0C44 File Offset: 0x001DEE44
	public long Amount { get; set; }

	// Token: 0x17000574 RID: 1396
	// (get) Token: 0x06005C89 RID: 23689 RVA: 0x001E0C4D File Offset: 0x001DEE4D
	// (set) Token: 0x06005C8A RID: 23690 RVA: 0x001E0C55 File Offset: 0x001DEE55
	public DateTime? Date { get; set; }

	// Token: 0x06005C8B RID: 23691 RVA: 0x001E0C5E File Offset: 0x001DEE5E
	public override string ToString()
	{
		return string.Format("[GoldRewardData: Amount={0} Origin={1} OriginData={2}]", this.Amount, base.Origin, base.OriginData);
	}

	// Token: 0x06005C8C RID: 23692 RVA: 0x001E0C8B File Offset: 0x001DEE8B
	protected override string GetAssetPath()
	{
		return "GoldReward.prefab:8e5e9429ae51d8b4bac2a9fb3826e548";
	}
}
