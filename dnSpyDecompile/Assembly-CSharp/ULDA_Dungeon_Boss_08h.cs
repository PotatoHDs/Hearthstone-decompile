using System;
using System.Collections.Generic;

// Token: 0x02000485 RID: 1157
public class ULDA_Dungeon_Boss_08h : ULDA_Dungeon
{
	// Token: 0x06003E95 RID: 16021 RVA: 0x0014BF4C File Offset: 0x0014A14C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_08h.VO_ULDA_BOSS_08h_Sinkhole_Death_01,
			ULDA_Dungeon_Boss_08h.VO_ULDA_BOSS_08h_Sinkhole_Defeat_01,
			ULDA_Dungeon_Boss_08h.VO_ULDA_BOSS_08h_Sinkhole_EmoteResponse_01,
			ULDA_Dungeon_Boss_08h.VO_ULDA_BOSS_08h_Sinkhole_Intro_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003E96 RID: 16022 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003E97 RID: 16023 RVA: 0x0014BFF0 File Offset: 0x0014A1F0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_08h.VO_ULDA_BOSS_08h_Sinkhole_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_08h.VO_ULDA_BOSS_08h_Sinkhole_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_08h.VO_ULDA_BOSS_08h_Sinkhole_EmoteResponse_01;
	}

	// Token: 0x04002AFC RID: 11004
	private static readonly AssetReference VO_ULDA_BOSS_08h_Sinkhole_Death_01 = new AssetReference("VO_ULDA_BOSS_08h_Sinkhole_Death_01.prefab:9e2b2e5c9540e944bab799821028f45e");

	// Token: 0x04002AFD RID: 11005
	private static readonly AssetReference VO_ULDA_BOSS_08h_Sinkhole_Defeat_01 = new AssetReference("VO_ULDA_BOSS_08h_Sinkhole_Defeat_01.prefab:b4a60e175c00cc34dabf890eb56ea2f9");

	// Token: 0x04002AFE RID: 11006
	private static readonly AssetReference VO_ULDA_BOSS_08h_Sinkhole_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_08h_Sinkhole_EmoteResponse_01.prefab:c5d7d9c3b51340f40bad0e4ca7ca46d4");

	// Token: 0x04002AFF RID: 11007
	private static readonly AssetReference VO_ULDA_BOSS_08h_Sinkhole_Intro_01 = new AssetReference("VO_ULDA_BOSS_08h_Sinkhole_Intro_01.prefab:3190fbf165c4a28419dc3315a20e69f7");
}
