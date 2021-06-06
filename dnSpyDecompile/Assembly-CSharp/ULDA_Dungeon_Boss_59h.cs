using System;
using System.Collections.Generic;

// Token: 0x020004B8 RID: 1208
public class ULDA_Dungeon_Boss_59h : ULDA_Dungeon
{
	// Token: 0x060040DD RID: 16605 RVA: 0x0015A37C File Offset: 0x0015857C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_59h.VO_ULDA_BOSS_59h_Direbat_Death,
			ULDA_Dungeon_Boss_59h.VO_ULDA_BOSS_59h_Direbat_Defeat,
			ULDA_Dungeon_Boss_59h.VO_ULDA_BOSS_59h_Direbat_EmoteResponse,
			ULDA_Dungeon_Boss_59h.VO_ULDA_BOSS_59h_Direbat_Intro
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060040DE RID: 16606 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060040DF RID: 16607 RVA: 0x0015A420 File Offset: 0x00158620
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_59h.VO_ULDA_BOSS_59h_Direbat_Intro;
		this.m_deathLine = ULDA_Dungeon_Boss_59h.VO_ULDA_BOSS_59h_Direbat_Death;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_59h.VO_ULDA_BOSS_59h_Direbat_EmoteResponse;
	}

	// Token: 0x04002F25 RID: 12069
	private static readonly AssetReference VO_ULDA_BOSS_59h_Direbat_Death = new AssetReference("VO_ULDA_BOSS_59h_Direbat_Death.prefab:96c1eb6503063ec4e80c26b7a5f722e4");

	// Token: 0x04002F26 RID: 12070
	private static readonly AssetReference VO_ULDA_BOSS_59h_Direbat_Defeat = new AssetReference("VO_ULDA_BOSS_59h_Direbat_Defeat.prefab:49e88284ba7f8c940ab36228a7777596");

	// Token: 0x04002F27 RID: 12071
	private static readonly AssetReference VO_ULDA_BOSS_59h_Direbat_EmoteResponse = new AssetReference("VO_ULDA_BOSS_59h_Direbat_EmoteResponse.prefab:1a6fc68c44fc167419ec4fbd9d4fe76b");

	// Token: 0x04002F28 RID: 12072
	private static readonly AssetReference VO_ULDA_BOSS_59h_Direbat_Intro = new AssetReference("VO_ULDA_BOSS_59h_Direbat_Intro.prefab:17fcdafd96828c94a899de8668b95ce2");
}
