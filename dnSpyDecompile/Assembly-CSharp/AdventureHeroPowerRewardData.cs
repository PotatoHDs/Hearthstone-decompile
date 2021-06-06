using System;

// Token: 0x02000659 RID: 1625
public class AdventureHeroPowerRewardData : RewardData
{
	// Token: 0x06005BBF RID: 23487 RVA: 0x001DDC17 File Offset: 0x001DBE17
	public AdventureHeroPowerRewardData() : this(null)
	{
	}

	// Token: 0x06005BC0 RID: 23488 RVA: 0x001DDC20 File Offset: 0x001DBE20
	public AdventureHeroPowerRewardData(AdventureHeroPowerDbfRecord heroPowerRecord) : base(Reward.Type.ADVENTURE_HERO_POWER)
	{
		this.HeroPowerRecord = heroPowerRecord;
	}

	// Token: 0x1700055F RID: 1375
	// (get) Token: 0x06005BC1 RID: 23489 RVA: 0x001DDC31 File Offset: 0x001DBE31
	// (set) Token: 0x06005BC2 RID: 23490 RVA: 0x001DDC39 File Offset: 0x001DBE39
	public AdventureHeroPowerDbfRecord HeroPowerRecord { get; set; }

	// Token: 0x06005BC3 RID: 23491 RVA: 0x001DDC44 File Offset: 0x001DBE44
	public override string ToString()
	{
		return string.Format("[HeroPowerRewardData: HeroPowerRecord.ID={0} HeroPowerRecord.CardId={1} Origin={2} OriginData={3}]", new object[]
		{
			this.HeroPowerRecord.ID,
			this.HeroPowerRecord.CardId,
			base.Origin,
			base.OriginData
		});
	}

	// Token: 0x06005BC4 RID: 23492 RVA: 0x001DDCA3 File Offset: 0x001DBEA3
	protected override string GetAssetPath()
	{
		return "AdventureHeroPowerReward.prefab:e29f5f24b744e0648ab09119f324e781";
	}
}
