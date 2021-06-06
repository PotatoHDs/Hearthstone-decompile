using System;
using System.Collections.Generic;

// Token: 0x020004CA RID: 1226
public class ULDA_Dungeon_Boss_78h : ULDA_Dungeon
{
	// Token: 0x060041AA RID: 16810 RVA: 0x0015F3DC File Offset: 0x0015D5DC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_78h.VO_ULDA_BOSS_78h_Octosari_Death,
			ULDA_Dungeon_Boss_78h.VO_ULDA_BOSS_78h_Octosari_DefeatPlayer,
			ULDA_Dungeon_Boss_78h.VO_ULDA_BOSS_78h_Octosari_EmoteResponse,
			ULDA_Dungeon_Boss_78h.VO_ULDA_BOSS_78h_Octosari_Intro
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060041AB RID: 16811 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060041AC RID: 16812 RVA: 0x0015F480 File Offset: 0x0015D680
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_78h.VO_ULDA_BOSS_78h_Octosari_Intro;
		this.m_deathLine = ULDA_Dungeon_Boss_78h.VO_ULDA_BOSS_78h_Octosari_Death;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_78h.VO_ULDA_BOSS_78h_Octosari_EmoteResponse;
	}

	// Token: 0x040030B1 RID: 12465
	private static readonly AssetReference VO_ULDA_BOSS_78h_Octosari_Death = new AssetReference("VO_ULDA_BOSS_78h_Octosari_Death.prefab:ad50902e1822c7f43b9afc99efcb0e84");

	// Token: 0x040030B2 RID: 12466
	private static readonly AssetReference VO_ULDA_BOSS_78h_Octosari_DefeatPlayer = new AssetReference("VO_ULDA_BOSS_78h_Octosari_DefeatPlayer.prefab:97391cfd5487cdb44a8fd0a67af589d8");

	// Token: 0x040030B3 RID: 12467
	private static readonly AssetReference VO_ULDA_BOSS_78h_Octosari_EmoteResponse = new AssetReference("VO_ULDA_BOSS_78h_Octosari_EmoteResponse.prefab:66a84be4d1ebacc47b89cc6e63ff7a32");

	// Token: 0x040030B4 RID: 12468
	private static readonly AssetReference VO_ULDA_BOSS_78h_Octosari_Intro = new AssetReference("VO_ULDA_BOSS_78h_Octosari_Intro.prefab:cff7e88ec4c0dde4a99502bd038678a5");
}
