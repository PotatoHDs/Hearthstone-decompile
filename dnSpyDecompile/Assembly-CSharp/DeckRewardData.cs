using System;

// Token: 0x0200066C RID: 1644
public class DeckRewardData : RewardData
{
	// Token: 0x06005C5C RID: 23644 RVA: 0x001E0461 File Offset: 0x001DE661
	public DeckRewardData(int deckId, int classId) : base(Reward.Type.DECK)
	{
		this.DeckId = deckId;
		this.ClassId = classId;
		this.PrefabAssetPath = "DeckReward.prefab:8f715cc0ca835a444b1794ae1d5ab4a7";
	}

	// Token: 0x1700056F RID: 1391
	// (get) Token: 0x06005C5D RID: 23645 RVA: 0x001E0484 File Offset: 0x001DE684
	// (set) Token: 0x06005C5E RID: 23646 RVA: 0x001E048C File Offset: 0x001DE68C
	public int DeckId { get; set; }

	// Token: 0x17000570 RID: 1392
	// (get) Token: 0x06005C5F RID: 23647 RVA: 0x001E0495 File Offset: 0x001DE695
	// (set) Token: 0x06005C60 RID: 23648 RVA: 0x001E049D File Offset: 0x001DE69D
	public int ClassId { get; set; }

	// Token: 0x17000571 RID: 1393
	// (get) Token: 0x06005C61 RID: 23649 RVA: 0x001E04A6 File Offset: 0x001DE6A6
	// (set) Token: 0x06005C62 RID: 23650 RVA: 0x001E04AE File Offset: 0x001DE6AE
	public string PrefabAssetPath { get; set; }

	// Token: 0x06005C63 RID: 23651 RVA: 0x001E04B7 File Offset: 0x001DE6B7
	public override string ToString()
	{
		return string.Format("[DeckReward: DeckId={0}]", base.RewardType, this.DeckId);
	}

	// Token: 0x06005C64 RID: 23652 RVA: 0x001E04D9 File Offset: 0x001DE6D9
	protected override string GetAssetPath()
	{
		return this.PrefabAssetPath;
	}
}
