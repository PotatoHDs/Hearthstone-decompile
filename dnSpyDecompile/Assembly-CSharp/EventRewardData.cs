using System;

// Token: 0x02000670 RID: 1648
public class EventRewardData : RewardData
{
	// Token: 0x06005C73 RID: 23667 RVA: 0x001E0754 File Offset: 0x001DE954
	public EventRewardData() : this(0)
	{
	}

	// Token: 0x06005C74 RID: 23668 RVA: 0x001E075D File Offset: 0x001DE95D
	public EventRewardData(int eventType) : base(Reward.Type.EVENT)
	{
		this.EventType = eventType;
	}

	// Token: 0x17000572 RID: 1394
	// (get) Token: 0x06005C75 RID: 23669 RVA: 0x001E076E File Offset: 0x001DE96E
	// (set) Token: 0x06005C76 RID: 23670 RVA: 0x001E0776 File Offset: 0x001DE976
	public int EventType { get; set; }

	// Token: 0x06005C77 RID: 23671 RVA: 0x001E077F File Offset: 0x001DE97F
	public override string ToString()
	{
		return string.Format("[EventRewardData: EventType={0} Origin={1} OriginData={2}]", this.EventType, base.Origin, base.OriginData);
	}

	// Token: 0x06005C78 RID: 23672 RVA: 0x001E07AC File Offset: 0x001DE9AC
	protected override string GetAssetPath()
	{
		return "EventReward.prefab:a01d4d190cfba7746840a786f5f5e4c0";
	}
}
