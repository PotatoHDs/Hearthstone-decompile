using System;

// Token: 0x0200066B RID: 1643
public class ClassChallengeUnlockData : RewardData
{
	// Token: 0x06005C56 RID: 23638 RVA: 0x001E0403 File Offset: 0x001DE603
	public ClassChallengeUnlockData() : this(0)
	{
	}

	// Token: 0x06005C57 RID: 23639 RVA: 0x001E040C File Offset: 0x001DE60C
	public ClassChallengeUnlockData(int wingID) : base(Reward.Type.CLASS_CHALLENGE)
	{
		this.WingID = wingID;
	}

	// Token: 0x1700056E RID: 1390
	// (get) Token: 0x06005C58 RID: 23640 RVA: 0x001E041C File Offset: 0x001DE61C
	// (set) Token: 0x06005C59 RID: 23641 RVA: 0x001E0424 File Offset: 0x001DE624
	public int WingID { get; set; }

	// Token: 0x06005C5A RID: 23642 RVA: 0x001E042D File Offset: 0x001DE62D
	public override string ToString()
	{
		return string.Format("[ClassChallengeUnlockData: WingID={0} Origin={1} OriginData={2}]", this.WingID, base.Origin, base.OriginData);
	}

	// Token: 0x06005C5B RID: 23643 RVA: 0x001E045A File Offset: 0x001DE65A
	protected override string GetAssetPath()
	{
		return "ClassChallengeUnlocked.prefab:b3e13ec75931a2d45a6265f3fafe0aa4";
	}
}
