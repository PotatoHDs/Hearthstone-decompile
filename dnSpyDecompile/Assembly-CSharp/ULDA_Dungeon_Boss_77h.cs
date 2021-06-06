using System;
using System.Collections.Generic;

// Token: 0x020004C9 RID: 1225
public class ULDA_Dungeon_Boss_77h : ULDA_Dungeon
{
	// Token: 0x060041A5 RID: 16805 RVA: 0x0015F2C0 File Offset: 0x0015D4C0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_77h.VO_ULDA_BOSS_77h_Gorebite_Death,
			ULDA_Dungeon_Boss_77h.VO_ULDA_BOSS_77h_Gorebite_Defeat,
			ULDA_Dungeon_Boss_77h.VO_ULDA_BOSS_77h_Gorebite_EmoteResponse,
			ULDA_Dungeon_Boss_77h.VO_ULDA_BOSS_77h_Gorebite_Intro
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060041A6 RID: 16806 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060041A7 RID: 16807 RVA: 0x0015F364 File Offset: 0x0015D564
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_77h.VO_ULDA_BOSS_77h_Gorebite_Intro;
		this.m_deathLine = ULDA_Dungeon_Boss_77h.VO_ULDA_BOSS_77h_Gorebite_Death;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_77h.VO_ULDA_BOSS_77h_Gorebite_EmoteResponse;
	}

	// Token: 0x040030AD RID: 12461
	private static readonly AssetReference VO_ULDA_BOSS_77h_Gorebite_Death = new AssetReference("VO_ULDA_BOSS_77h_Gorebite_Death.prefab:bcf74f8cd0e88da44808ae3d950a1a8e");

	// Token: 0x040030AE RID: 12462
	private static readonly AssetReference VO_ULDA_BOSS_77h_Gorebite_Defeat = new AssetReference("VO_ULDA_BOSS_77h_Gorebite_Defeat.prefab:ff8a8435d7634fd449d35e21c17ba1b1");

	// Token: 0x040030AF RID: 12463
	private static readonly AssetReference VO_ULDA_BOSS_77h_Gorebite_EmoteResponse = new AssetReference("VO_ULDA_BOSS_77h_Gorebite_EmoteResponse.prefab:99bdf7ab67fea5c4381e1b85c2f56e8d");

	// Token: 0x040030B0 RID: 12464
	private static readonly AssetReference VO_ULDA_BOSS_77h_Gorebite_Intro = new AssetReference("VO_ULDA_BOSS_77h_Gorebite_Intro.prefab:c12fbd3ed62fc3c4dad6746ee04256d2");
}
