using System;
using System.Collections.Generic;

// Token: 0x020004B5 RID: 1205
public class ULDA_Dungeon_Boss_56h : ULDA_Dungeon
{
	// Token: 0x060040C7 RID: 16583 RVA: 0x00159BE8 File Offset: 0x00157DE8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_56h.VO_ULDA_BOSS_56h_SandPufferFrog_Death,
			ULDA_Dungeon_Boss_56h.VO_ULDA_BOSS_56h_SandPufferFrog_Defeat,
			ULDA_Dungeon_Boss_56h.VO_ULDA_BOSS_56h_SandPufferFrog_EmoteResponse,
			ULDA_Dungeon_Boss_56h.VO_ULDA_BOSS_56h_SandPufferFrog_Intro
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060040C8 RID: 16584 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060040C9 RID: 16585 RVA: 0x00159C8C File Offset: 0x00157E8C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_56h.VO_ULDA_BOSS_56h_SandPufferFrog_Intro;
		this.m_deathLine = ULDA_Dungeon_Boss_56h.VO_ULDA_BOSS_56h_SandPufferFrog_Death;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_56h.VO_ULDA_BOSS_56h_SandPufferFrog_EmoteResponse;
	}

	// Token: 0x04002F02 RID: 12034
	private static readonly AssetReference VO_ULDA_BOSS_56h_SandPufferFrog_Death = new AssetReference("VO_ULDA_BOSS_56h_SandPufferFrog_Death.prefab:e5742d5456598d749bdb29f175198b88");

	// Token: 0x04002F03 RID: 12035
	private static readonly AssetReference VO_ULDA_BOSS_56h_SandPufferFrog_Defeat = new AssetReference("VO_ULDA_BOSS_56h_SandPufferFrog_Defeat.prefab:9f51d3d202e6a3649aa997c823a07a3a");

	// Token: 0x04002F04 RID: 12036
	private static readonly AssetReference VO_ULDA_BOSS_56h_SandPufferFrog_EmoteResponse = new AssetReference("VO_ULDA_BOSS_56h_SandPufferFrog_EmoteResponse.prefab:cd4e9275be8335b4a9d59199b2bd1529");

	// Token: 0x04002F05 RID: 12037
	private static readonly AssetReference VO_ULDA_BOSS_56h_SandPufferFrog_Intro = new AssetReference("VO_ULDA_BOSS_56h_SandPufferFrog_Intro.prefab:9abcb5b8aa24dab4aa82e517520983f0");
}
