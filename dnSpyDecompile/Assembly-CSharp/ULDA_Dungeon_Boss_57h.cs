using System;
using System.Collections.Generic;

// Token: 0x020004B6 RID: 1206
public class ULDA_Dungeon_Boss_57h : ULDA_Dungeon
{
	// Token: 0x060040CC RID: 16588 RVA: 0x00159D04 File Offset: 0x00157F04
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_57h.VO_ULDA_BOSS_57h_WingedGuardian_Death,
			ULDA_Dungeon_Boss_57h.VO_ULDA_BOSS_57h_WingedGuardian_Defeat,
			ULDA_Dungeon_Boss_57h.VO_ULDA_BOSS_57h_WingedGuardian_EmoteResponse,
			ULDA_Dungeon_Boss_57h.VO_ULDA_BOSS_57h_WingedGuardian_Intro
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060040CD RID: 16589 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060040CE RID: 16590 RVA: 0x00159DA8 File Offset: 0x00157FA8
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_57h.VO_ULDA_BOSS_57h_WingedGuardian_Intro;
		this.m_deathLine = ULDA_Dungeon_Boss_57h.VO_ULDA_BOSS_57h_WingedGuardian_Death;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_57h.VO_ULDA_BOSS_57h_WingedGuardian_EmoteResponse;
	}

	// Token: 0x04002F06 RID: 12038
	private static readonly AssetReference VO_ULDA_BOSS_57h_WingedGuardian_Death = new AssetReference("VO_ULDA_BOSS_57h_WingedGuardian_Death.prefab:a050f4e742a2f1f46a9e1c0af8fe68e4");

	// Token: 0x04002F07 RID: 12039
	private static readonly AssetReference VO_ULDA_BOSS_57h_WingedGuardian_Defeat = new AssetReference("VO_ULDA_BOSS_57h_WingedGuardian_Defeat.prefab:b561e60190b351f4a96bcdc32438cbe2");

	// Token: 0x04002F08 RID: 12040
	private static readonly AssetReference VO_ULDA_BOSS_57h_WingedGuardian_EmoteResponse = new AssetReference("VO_ULDA_BOSS_57h_WingedGuardian_EmoteResponse.prefab:2f09e01cf38c45549b5ac8e22dd5854d");

	// Token: 0x04002F09 RID: 12041
	private static readonly AssetReference VO_ULDA_BOSS_57h_WingedGuardian_Intro = new AssetReference("VO_ULDA_BOSS_57h_WingedGuardian_Intro.prefab:1f6de710b8037ba42a428c99c700b955");
}
