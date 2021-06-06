public class SimpleRewardData : RewardData
{
	public int Amount { get; set; }

	public string PrefabAssetPath { get; set; }

	public string RewardHeadlineText { get; set; }

	public SimpleRewardData(Reward.Type rewardType)
		: this(rewardType, 0)
	{
	}

	public SimpleRewardData(Reward.Type rewardType, int amount)
		: this(rewardType, amount, "", "")
	{
	}

	public SimpleRewardData(Reward.Type rewardType, int amount, string nameOverride, string descriptionOverride)
		: base(rewardType)
	{
		Amount = amount;
		base.NameOverride = nameOverride;
		base.DescriptionOverride = descriptionOverride;
		PrefabAssetPath = "SimpleReward.prefab:c1bb382528839b54e9da96bc1db806cf";
		RewardHeadlineText = "";
	}

	public override string ToString()
	{
		return $"[SimpleRewardData: Type={base.RewardType} Amount={Amount} Origin={base.Origin} OriginData={base.OriginData}]";
	}

	protected override string GetAssetPath()
	{
		return PrefabAssetPath;
	}
}
