using System;
using System.Collections.Generic;

// Token: 0x020004AB RID: 1195
public class ULDA_Dungeon_Boss_46h : ULDA_Dungeon
{
	// Token: 0x06004059 RID: 16473 RVA: 0x00157740 File Offset: 0x00155940
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_46h.VO_ULDA_BOSS_46h_Wildtooth_Death_01,
			ULDA_Dungeon_Boss_46h.VO_ULDA_BOSS_46h_Wildtooth_Defeat_01,
			ULDA_Dungeon_Boss_46h.VO_ULDA_BOSS_46h_Wildtooth_EmoteResponse_01,
			ULDA_Dungeon_Boss_46h.VO_ULDA_BOSS_46h_Wildtooth_Intro_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600405A RID: 16474 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600405B RID: 16475 RVA: 0x001577E4 File Offset: 0x001559E4
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_46h.VO_ULDA_BOSS_46h_Wildtooth_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_46h.VO_ULDA_BOSS_46h_Wildtooth_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_46h.VO_ULDA_BOSS_46h_Wildtooth_EmoteResponse_01;
	}

	// Token: 0x04002E5E RID: 11870
	private static readonly AssetReference VO_ULDA_BOSS_46h_Wildtooth_Death_01 = new AssetReference("VO_ULDA_BOSS_46h_Wildtooth_Death_01.prefab:7d6ad06fd86ea684086550ecf03c5e2d");

	// Token: 0x04002E5F RID: 11871
	private static readonly AssetReference VO_ULDA_BOSS_46h_Wildtooth_Defeat_01 = new AssetReference("VO_ULDA_BOSS_46h_Wildtooth_Defeat_01.prefab:1765447744be14f479c800f7db28e409");

	// Token: 0x04002E60 RID: 11872
	private static readonly AssetReference VO_ULDA_BOSS_46h_Wildtooth_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_46h_Wildtooth_EmoteResponse_01.prefab:05176adcc37f2624ababa70c786a6ab8");

	// Token: 0x04002E61 RID: 11873
	private static readonly AssetReference VO_ULDA_BOSS_46h_Wildtooth_Intro_01 = new AssetReference("VO_ULDA_BOSS_46h_Wildtooth_Intro_01.prefab:0ce9dbdc00dc2ff45b5b79858b68fd91");
}
