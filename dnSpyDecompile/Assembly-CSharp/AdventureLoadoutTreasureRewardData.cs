using System;

// Token: 0x0200065B RID: 1627
public class AdventureLoadoutTreasureRewardData : RewardData
{
	// Token: 0x06005BCA RID: 23498 RVA: 0x001DDD42 File Offset: 0x001DBF42
	public AdventureLoadoutTreasureRewardData() : this(null, false)
	{
	}

	// Token: 0x06005BCB RID: 23499 RVA: 0x001DDD4C File Offset: 0x001DBF4C
	public AdventureLoadoutTreasureRewardData(AdventureLoadoutTreasuresDbfRecord loadoutTreasureRecord, bool isUpgrade) : base(Reward.Type.ADVENTURE_HERO_POWER)
	{
		this.LoadoutTreasureRecord = loadoutTreasureRecord;
		this.IsUpgrade = isUpgrade;
	}

	// Token: 0x17000560 RID: 1376
	// (get) Token: 0x06005BCC RID: 23500 RVA: 0x001DDD64 File Offset: 0x001DBF64
	// (set) Token: 0x06005BCD RID: 23501 RVA: 0x001DDD6C File Offset: 0x001DBF6C
	public AdventureLoadoutTreasuresDbfRecord LoadoutTreasureRecord { get; set; }

	// Token: 0x17000561 RID: 1377
	// (get) Token: 0x06005BCE RID: 23502 RVA: 0x001DDD75 File Offset: 0x001DBF75
	// (set) Token: 0x06005BCF RID: 23503 RVA: 0x001DDD7D File Offset: 0x001DBF7D
	public bool IsUpgrade { get; set; }

	// Token: 0x06005BD0 RID: 23504 RVA: 0x001DDD88 File Offset: 0x001DBF88
	public override string ToString()
	{
		return string.Format("[LoadoutTreasureRewardData: LoadoutTreasureRecord.ID={0} LoadoutTreasureRecord.CardId={1} Origin={2} OriginData={3}]", new object[]
		{
			this.LoadoutTreasureRecord.ID,
			this.LoadoutTreasureRecord.CardId,
			base.Origin,
			base.OriginData
		});
	}

	// Token: 0x06005BD1 RID: 23505 RVA: 0x001DDDE7 File Offset: 0x001DBFE7
	protected override string GetAssetPath()
	{
		return "AdventureCardReward.prefab:a1f9fc20c145f4240a23991ee63220ff";
	}
}
