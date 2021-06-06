using System;

// Token: 0x02000657 RID: 1623
public class AdventureDeckRewardData : RewardData
{
	// Token: 0x06005BB2 RID: 23474 RVA: 0x001DDA45 File Offset: 0x001DBC45
	public AdventureDeckRewardData() : this(null)
	{
	}

	// Token: 0x06005BB3 RID: 23475 RVA: 0x001DDA4E File Offset: 0x001DBC4E
	public AdventureDeckRewardData(AdventureDeckDbfRecord deckRecord) : base(Reward.Type.ADVENTURE_DECK)
	{
		this.DeckRecord = deckRecord;
	}

	// Token: 0x1700055E RID: 1374
	// (get) Token: 0x06005BB4 RID: 23476 RVA: 0x001DDA5F File Offset: 0x001DBC5F
	// (set) Token: 0x06005BB5 RID: 23477 RVA: 0x001DDA67 File Offset: 0x001DBC67
	public AdventureDeckDbfRecord DeckRecord { get; set; }

	// Token: 0x06005BB6 RID: 23478 RVA: 0x001DDA70 File Offset: 0x001DBC70
	public override string ToString()
	{
		return string.Format("[DeckRewardData: DeckRecord.ID={0} DeckRecord.DeckId={1} Origin={2} OriginData={3}]", new object[]
		{
			this.DeckRecord.ID,
			this.DeckRecord.DeckId,
			base.Origin,
			base.OriginData
		});
	}

	// Token: 0x06005BB7 RID: 23479 RVA: 0x001DDACF File Offset: 0x001DBCCF
	protected override string GetAssetPath()
	{
		return "AdventureDeckReward.prefab:0c5995063a72bd64e95d0a857e36386b";
	}
}
