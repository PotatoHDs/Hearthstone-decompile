public class MiniSetRewardData : RewardData
{
	public int MiniSetID { get; set; }

	public MiniSetRewardData(int cardsRewardId)
		: base(Reward.Type.MINI_SET)
	{
		MiniSetID = cardsRewardId;
	}

	public override string ToString()
	{
		return $"[MiniSetRewardData: CardsRewardID={MiniSetID} Origin={base.Origin} OriginData={base.OriginData}]";
	}

	protected override string GetAssetPath()
	{
		return "MiniSetReward.prefab:dc43a6807e16eb440a7db978dd95ab1f";
	}
}
