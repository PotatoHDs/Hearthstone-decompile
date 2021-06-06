using System;

// Token: 0x02000655 RID: 1621
public class AdventureCompleteRewardData : RewardData
{
	// Token: 0x1700055B RID: 1371
	// (get) Token: 0x06005BA3 RID: 23459 RVA: 0x001DD929 File Offset: 0x001DBB29
	// (set) Token: 0x06005BA4 RID: 23460 RVA: 0x001DD931 File Offset: 0x001DBB31
	public AdventureModeDbId ModeId { get; set; }

	// Token: 0x1700055C RID: 1372
	// (get) Token: 0x06005BA5 RID: 23461 RVA: 0x001DD93A File Offset: 0x001DBB3A
	// (set) Token: 0x06005BA6 RID: 23462 RVA: 0x001DD942 File Offset: 0x001DBB42
	public string RewardObjectName { get; set; }

	// Token: 0x1700055D RID: 1373
	// (get) Token: 0x06005BA7 RID: 23463 RVA: 0x001DD94B File Offset: 0x001DBB4B
	// (set) Token: 0x06005BA8 RID: 23464 RVA: 0x001DD953 File Offset: 0x001DBB53
	public string BannerText { get; set; }

	// Token: 0x06005BA9 RID: 23465 RVA: 0x001DD95C File Offset: 0x001DBB5C
	public AdventureCompleteRewardData() : this(AdventureModeDbId.INVALID, "AdventureCompleteReward_Naxxramas", "")
	{
	}

	// Token: 0x06005BAA RID: 23466 RVA: 0x001DD96F File Offset: 0x001DBB6F
	public AdventureCompleteRewardData(AdventureModeDbId modeId, string rewardObjectName, string bannerText) : base(Reward.Type.CLASS_CHALLENGE)
	{
		this.ModeId = modeId;
		this.RewardObjectName = rewardObjectName;
		this.BannerText = bannerText;
	}

	// Token: 0x06005BAB RID: 23467 RVA: 0x001DD98D File Offset: 0x001DBB8D
	public override string ToString()
	{
		return string.Format("[AdventureCompleteRewardData: RewardObjectName={0} Origin={1} OriginData={2}]", this.RewardObjectName, base.Origin, base.OriginData);
	}

	// Token: 0x06005BAC RID: 23468 RVA: 0x001DD9B5 File Offset: 0x001DBBB5
	protected override string GetAssetPath()
	{
		return this.RewardObjectName;
	}

	// Token: 0x04004E40 RID: 20032
	private const string s_DefaultRewardObject = "AdventureCompleteReward_Naxxramas";
}
