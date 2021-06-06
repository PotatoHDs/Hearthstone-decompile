using System;

// Token: 0x02000661 RID: 1633
public class BonusChallengeUnlockData : RewardData
{
	// Token: 0x06005BEF RID: 23535 RVA: 0x001DE3A7 File Offset: 0x001DC5A7
	public BonusChallengeUnlockData() : this("", "Card_DungeonCrawl_Boss.prefab:c7f700bb034424e46a7c2321e4621965")
	{
	}

	// Token: 0x06005BF0 RID: 23536 RVA: 0x001DE3B9 File Offset: 0x001DC5B9
	public BonusChallengeUnlockData(string prefabToDisplay, string bossCardActorPrefab) : base(Reward.Type.BONUS_CHALLENGE)
	{
		this.PrefabToDisplay = prefabToDisplay;
		this.BossCardActorPrefab = bossCardActorPrefab;
	}

	// Token: 0x17000564 RID: 1380
	// (get) Token: 0x06005BF1 RID: 23537 RVA: 0x001DE3D1 File Offset: 0x001DC5D1
	// (set) Token: 0x06005BF2 RID: 23538 RVA: 0x001DE3D9 File Offset: 0x001DC5D9
	public string PrefabToDisplay { get; set; }

	// Token: 0x17000565 RID: 1381
	// (get) Token: 0x06005BF3 RID: 23539 RVA: 0x001DE3E2 File Offset: 0x001DC5E2
	// (set) Token: 0x06005BF4 RID: 23540 RVA: 0x001DE3EA File Offset: 0x001DC5EA
	public string BossCardActorPrefab { get; set; }

	// Token: 0x06005BF5 RID: 23541 RVA: 0x001DE3F3 File Offset: 0x001DC5F3
	public override string ToString()
	{
		return string.Format("[BonusChallengeUnlockData: PrefabToDisplay={0} Origin={1} OriginData={2}]", this.PrefabToDisplay, base.Origin, base.OriginData);
	}

	// Token: 0x06005BF6 RID: 23542 RVA: 0x001DE41B File Offset: 0x001DC61B
	protected override string GetAssetPath()
	{
		return "BonusChallengeUnlocked.prefab:43116842520caa144a9e8b7c9bc418b9";
	}
}
