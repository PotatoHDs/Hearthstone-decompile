using System;

// Token: 0x0200065F RID: 1631
public class ForgeTicketRewardData : RewardData
{
	// Token: 0x06005BE2 RID: 23522 RVA: 0x001DE195 File Offset: 0x001DC395
	public ForgeTicketRewardData() : this(0)
	{
	}

	// Token: 0x06005BE3 RID: 23523 RVA: 0x001DE19E File Offset: 0x001DC39E
	public ForgeTicketRewardData(int quantity) : base(Reward.Type.FORGE_TICKET)
	{
		this.Quantity = quantity;
	}

	// Token: 0x17000563 RID: 1379
	// (get) Token: 0x06005BE4 RID: 23524 RVA: 0x001DE1AE File Offset: 0x001DC3AE
	// (set) Token: 0x06005BE5 RID: 23525 RVA: 0x001DE1B6 File Offset: 0x001DC3B6
	public int Quantity { get; set; }

	// Token: 0x06005BE6 RID: 23526 RVA: 0x001DE1BF File Offset: 0x001DC3BF
	public override string ToString()
	{
		return string.Format("[ForgeTicketRewardData: Quantity={0} Origin={1} OriginData={2}]", this.Quantity, base.Origin, base.OriginData);
	}

	// Token: 0x06005BE7 RID: 23527 RVA: 0x001DE1EC File Offset: 0x001DC3EC
	protected override string GetAssetPath()
	{
		return "ArenaTicketReward.prefab:4b2fc65ab7fe3d349bc0ce61ad3dd756";
	}
}
