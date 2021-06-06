public class ClassChallengeUnlockData : RewardData
{
	public int WingID { get; set; }

	public ClassChallengeUnlockData()
		: this(0)
	{
	}

	public ClassChallengeUnlockData(int wingID)
		: base(Reward.Type.CLASS_CHALLENGE)
	{
		WingID = wingID;
	}

	public override string ToString()
	{
		return $"[ClassChallengeUnlockData: WingID={WingID} Origin={base.Origin} OriginData={base.OriginData}]";
	}

	protected override string GetAssetPath()
	{
		return "ClassChallengeUnlocked.prefab:b3e13ec75931a2d45a6265f3fafe0aa4";
	}
}
