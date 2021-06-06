using System;
using System.Collections.Generic;

// Token: 0x02000489 RID: 1161
public class ULDA_Dungeon_Boss_12h : ULDA_Dungeon
{
	// Token: 0x06003EBE RID: 16062 RVA: 0x0014CDE0 File Offset: 0x0014AFE0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_12h.VO_ULDA_BOSS_12h_Pyramad_Death_01,
			ULDA_Dungeon_Boss_12h.VO_ULDA_BOSS_12h_Pyramad_Defeat_01,
			ULDA_Dungeon_Boss_12h.VO_ULDA_BOSS_12h_Pyramad_EmoteResponse_01,
			ULDA_Dungeon_Boss_12h.VO_ULDA_BOSS_12h_Pyramad_Intro_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003EBF RID: 16063 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003EC0 RID: 16064 RVA: 0x0014CE84 File Offset: 0x0014B084
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_12h.VO_ULDA_BOSS_12h_Pyramad_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_12h.VO_ULDA_BOSS_12h_Pyramad_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_12h.VO_ULDA_BOSS_12h_Pyramad_EmoteResponse_01;
	}

	// Token: 0x04002B3D RID: 11069
	private static readonly AssetReference VO_ULDA_BOSS_12h_Pyramad_Death_01 = new AssetReference("VO_ULDA_BOSS_12h_Pyramad_Death_01.prefab:bb52f62014ecf00469de65df3542ed24");

	// Token: 0x04002B3E RID: 11070
	private static readonly AssetReference VO_ULDA_BOSS_12h_Pyramad_Defeat_01 = new AssetReference("VO_ULDA_BOSS_12h_Pyramad_Defeat_01.prefab:f458e10d636144343a7c2c0bd4b679cf");

	// Token: 0x04002B3F RID: 11071
	private static readonly AssetReference VO_ULDA_BOSS_12h_Pyramad_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_12h_Pyramad_EmoteResponse_01.prefab:36182e8e51464de4a98dec5e14d53c57");

	// Token: 0x04002B40 RID: 11072
	private static readonly AssetReference VO_ULDA_BOSS_12h_Pyramad_Intro_01 = new AssetReference("VO_ULDA_BOSS_12h_Pyramad_Intro_01.prefab:5e899eb20b55dd545b6e41eef82efc79");
}
