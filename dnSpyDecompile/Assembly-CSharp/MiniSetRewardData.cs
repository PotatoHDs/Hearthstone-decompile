using System;

// Token: 0x02000675 RID: 1653
public class MiniSetRewardData : RewardData
{
	// Token: 0x06005C91 RID: 23697 RVA: 0x001E0C92 File Offset: 0x001DEE92
	public MiniSetRewardData(int cardsRewardId) : base(Reward.Type.MINI_SET)
	{
		this.MiniSetID = cardsRewardId;
	}

	// Token: 0x17000575 RID: 1397
	// (get) Token: 0x06005C92 RID: 23698 RVA: 0x001E0CA3 File Offset: 0x001DEEA3
	// (set) Token: 0x06005C93 RID: 23699 RVA: 0x001E0CAB File Offset: 0x001DEEAB
	public int MiniSetID { get; set; }

	// Token: 0x06005C94 RID: 23700 RVA: 0x001E0CB4 File Offset: 0x001DEEB4
	public override string ToString()
	{
		return string.Format("[MiniSetRewardData: CardsRewardID={0} Origin={1} OriginData={2}]", this.MiniSetID, base.Origin, base.OriginData);
	}

	// Token: 0x06005C95 RID: 23701 RVA: 0x001E0CE1 File Offset: 0x001DEEE1
	protected override string GetAssetPath()
	{
		return "MiniSetReward.prefab:dc43a6807e16eb440a7db978dd95ab1f";
	}
}
