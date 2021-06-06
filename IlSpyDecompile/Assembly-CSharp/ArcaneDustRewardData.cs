public class ArcaneDustRewardData : RewardData
{
	public int Amount { get; set; }

	public ArcaneDustRewardData()
		: this(0)
	{
	}

	public ArcaneDustRewardData(int amount)
		: this(amount, "", "")
	{
	}

	public ArcaneDustRewardData(int amount, string nameOverride, string descriptionOverride)
		: base(Reward.Type.ARCANE_DUST)
	{
		Amount = amount;
		base.NameOverride = nameOverride;
		base.DescriptionOverride = descriptionOverride;
	}

	public override string ToString()
	{
		return $"[ArcaneDustRewardData: Amount={Amount} Origin={base.Origin} OriginData={base.OriginData}]";
	}

	protected override string GetAssetPath()
	{
		return "ArcaneDustReward.prefab:606ad37f35d6c5642a5bd81c7f0aee77";
	}
}
