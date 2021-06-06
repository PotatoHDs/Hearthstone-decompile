using System;
using System.Collections.Generic;

// Token: 0x020004CB RID: 1227
public class ULDA_Dungeon_Boss_79h : ULDA_Dungeon
{
	// Token: 0x060041AF RID: 16815 RVA: 0x0015F4F8 File Offset: 0x0015D6F8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_79h.VO_ULDA_BOSS_79h_ArmadilloMech_Death,
			ULDA_Dungeon_Boss_79h.VO_ULDA_BOSS_79h_ArmadilloMech_Defeat,
			ULDA_Dungeon_Boss_79h.VO_ULDA_BOSS_79h_ArmadilloMech_EmoteResponse,
			ULDA_Dungeon_Boss_79h.VO_ULDA_BOSS_79h_ArmadilloMech_Intro
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060041B0 RID: 16816 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060041B1 RID: 16817 RVA: 0x0015F59C File Offset: 0x0015D79C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_79h.VO_ULDA_BOSS_79h_ArmadilloMech_Intro;
		this.m_deathLine = ULDA_Dungeon_Boss_79h.VO_ULDA_BOSS_79h_ArmadilloMech_Death;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_79h.VO_ULDA_BOSS_79h_ArmadilloMech_EmoteResponse;
	}

	// Token: 0x040030B5 RID: 12469
	private static readonly AssetReference VO_ULDA_BOSS_79h_ArmadilloMech_Death = new AssetReference("VO_ULDA_BOSS_79h_ArmadilloMech_Death.prefab:5b96ddd8a993ecd46977e6ca4da892e9");

	// Token: 0x040030B6 RID: 12470
	private static readonly AssetReference VO_ULDA_BOSS_79h_ArmadilloMech_Defeat = new AssetReference("VO_ULDA_BOSS_79h_ArmadilloMech_Defeat.prefab:1ac5065faf456b24baf543d5bed95d2f");

	// Token: 0x040030B7 RID: 12471
	private static readonly AssetReference VO_ULDA_BOSS_79h_ArmadilloMech_EmoteResponse = new AssetReference("VO_ULDA_BOSS_79h_ArmadilloMech_EmoteResponse.prefab:efce6900921ed3a429cd4d831f7ea647");

	// Token: 0x040030B8 RID: 12472
	private static readonly AssetReference VO_ULDA_BOSS_79h_ArmadilloMech_Intro = new AssetReference("VO_ULDA_BOSS_79h_ArmadilloMech_Intro.prefab:cc70c478e01ec274393e37208438c89c");
}
