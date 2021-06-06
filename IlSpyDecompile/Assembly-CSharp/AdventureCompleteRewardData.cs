public class AdventureCompleteRewardData : RewardData
{
	private const string s_DefaultRewardObject = "AdventureCompleteReward_Naxxramas";

	public AdventureModeDbId ModeId { get; set; }

	public string RewardObjectName { get; set; }

	public string BannerText { get; set; }

	public AdventureCompleteRewardData()
		: this(AdventureModeDbId.INVALID, "AdventureCompleteReward_Naxxramas", "")
	{
	}

	public AdventureCompleteRewardData(AdventureModeDbId modeId, string rewardObjectName, string bannerText)
		: base(Reward.Type.CLASS_CHALLENGE)
	{
		ModeId = modeId;
		RewardObjectName = rewardObjectName;
		BannerText = bannerText;
	}

	public override string ToString()
	{
		return $"[AdventureCompleteRewardData: RewardObjectName={RewardObjectName} Origin={base.Origin} OriginData={base.OriginData}]";
	}

	protected override string GetAssetPath()
	{
		return RewardObjectName;
	}
}
