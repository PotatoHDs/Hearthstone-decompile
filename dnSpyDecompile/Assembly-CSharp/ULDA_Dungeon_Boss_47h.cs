using System;
using System.Collections.Generic;

// Token: 0x020004AC RID: 1196
public class ULDA_Dungeon_Boss_47h : ULDA_Dungeon
{
	// Token: 0x0600405E RID: 16478 RVA: 0x0015785C File Offset: 0x00155A5C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_47h.VO_ULDA_BOSS_47h_TotallyNormalJar_Death,
			ULDA_Dungeon_Boss_47h.VO_ULDA_BOSS_47h_TotallyNormalJar_Defeat,
			ULDA_Dungeon_Boss_47h.VO_ULDA_BOSS_47h_TotallyNormalJar_EmoteResponse,
			ULDA_Dungeon_Boss_47h.VO_ULDA_BOSS_47h_TotallyNormalJar_Intro
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600405F RID: 16479 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004060 RID: 16480 RVA: 0x00157900 File Offset: 0x00155B00
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_47h.VO_ULDA_BOSS_47h_TotallyNormalJar_Intro;
		this.m_deathLine = ULDA_Dungeon_Boss_47h.VO_ULDA_BOSS_47h_TotallyNormalJar_Death;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_47h.VO_ULDA_BOSS_47h_TotallyNormalJar_EmoteResponse;
	}

	// Token: 0x04002E62 RID: 11874
	private static readonly AssetReference VO_ULDA_BOSS_47h_TotallyNormalJar_Death = new AssetReference("VO_ULDA_BOSS_47h_TotallyNormalJar_Death.prefab:d3fe1495da0da89419f95e57ee3ec3a6");

	// Token: 0x04002E63 RID: 11875
	private static readonly AssetReference VO_ULDA_BOSS_47h_TotallyNormalJar_Defeat = new AssetReference("VO_ULDA_BOSS_47h_TotallyNormalJar_Defeat.prefab:7d565277f5790d14382ab754560d458f");

	// Token: 0x04002E64 RID: 11876
	private static readonly AssetReference VO_ULDA_BOSS_47h_TotallyNormalJar_EmoteResponse = new AssetReference("VO_ULDA_BOSS_47h_TotallyNormalJar_EmoteResponse.prefab:0dd98ef8348c0bc43bc619d5ca49f753");

	// Token: 0x04002E65 RID: 11877
	private static readonly AssetReference VO_ULDA_BOSS_47h_TotallyNormalJar_Intro = new AssetReference("VO_ULDA_BOSS_47h_TotallyNormalJar_Intro.prefab:753c137fc7681b142889fce3f6298c16");
}
