using System;
using System.Collections.Generic;

// Token: 0x02000482 RID: 1154
public class ULDA_Dungeon_Boss_05h : ULDA_Dungeon
{
	// Token: 0x06003E76 RID: 15990 RVA: 0x0014B4A8 File Offset: 0x001496A8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_05h.VO_ULDA_BOSS_05h_GlacktheScorpid_Death_01,
			ULDA_Dungeon_Boss_05h.VO_ULDA_BOSS_05h_GlacktheScorpid_Defeat_01,
			ULDA_Dungeon_Boss_05h.VO_ULDA_BOSS_05h_GlacktheScorpid_EmoteResponse_01,
			ULDA_Dungeon_Boss_05h.VO_ULDA_BOSS_05h_GlacktheScorpid_Intro_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003E77 RID: 15991 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003E78 RID: 15992 RVA: 0x0014B54C File Offset: 0x0014974C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_05h.VO_ULDA_BOSS_05h_GlacktheScorpid_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_05h.VO_ULDA_BOSS_05h_GlacktheScorpid_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_05h.VO_ULDA_BOSS_05h_GlacktheScorpid_EmoteResponse_01;
	}

	// Token: 0x04002ACD RID: 10957
	private static readonly AssetReference VO_ULDA_BOSS_05h_GlacktheScorpid_Death_01 = new AssetReference("VO_ULDA_BOSS_05h_GlacktheScorpid_Death_01.prefab:80f4e0c700dc8ae4a9aaa1aee5c5f3d8");

	// Token: 0x04002ACE RID: 10958
	private static readonly AssetReference VO_ULDA_BOSS_05h_GlacktheScorpid_Defeat_01 = new AssetReference("VO_ULDA_BOSS_05h_GlacktheScorpid_Defeat_01.prefab:2c2aabfc5d4923449933ca8f1e85fae2");

	// Token: 0x04002ACF RID: 10959
	private static readonly AssetReference VO_ULDA_BOSS_05h_GlacktheScorpid_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_05h_GlacktheScorpid_EmoteResponse_01.prefab:272e4dfd2fe844e499eca3388e20d193");

	// Token: 0x04002AD0 RID: 10960
	private static readonly AssetReference VO_ULDA_BOSS_05h_GlacktheScorpid_Intro_01 = new AssetReference("VO_ULDA_BOSS_05h_GlacktheScorpid_Intro_01.prefab:96e665030602e2640a4af2a3e40701db");
}
