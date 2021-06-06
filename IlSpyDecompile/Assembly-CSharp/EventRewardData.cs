public class EventRewardData : RewardData
{
	public int EventType { get; set; }

	public EventRewardData()
		: this(0)
	{
	}

	public EventRewardData(int eventType)
		: base(Reward.Type.EVENT)
	{
		EventType = eventType;
	}

	public override string ToString()
	{
		return $"[EventRewardData: EventType={EventType} Origin={base.Origin} OriginData={base.OriginData}]";
	}

	protected override string GetAssetPath()
	{
		return "EventReward.prefab:a01d4d190cfba7746840a786f5f5e4c0";
	}
}
