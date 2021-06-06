public class ForgeTicketRewardData : RewardData
{
	public int Quantity { get; set; }

	public ForgeTicketRewardData()
		: this(0)
	{
	}

	public ForgeTicketRewardData(int quantity)
		: base(Reward.Type.FORGE_TICKET)
	{
		Quantity = quantity;
	}

	public override string ToString()
	{
		return $"[ForgeTicketRewardData: Quantity={Quantity} Origin={base.Origin} OriginData={base.OriginData}]";
	}

	protected override string GetAssetPath()
	{
		return "ArenaTicketReward.prefab:4b2fc65ab7fe3d349bc0ce61ad3dd756";
	}
}
