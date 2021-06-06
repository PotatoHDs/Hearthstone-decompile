using System;
using System.Collections.Generic;

// Token: 0x02000494 RID: 1172
public class ULDA_Dungeon_Boss_23h : ULDA_Dungeon
{
	// Token: 0x06003F43 RID: 16195 RVA: 0x0014F864 File Offset: 0x0014DA64
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_23h.VO_ULDA_BOSS_23h_Skarik_Death_01,
			ULDA_Dungeon_Boss_23h.VO_ULDA_BOSS_23h_Skarik_Defeat_01,
			ULDA_Dungeon_Boss_23h.VO_ULDA_BOSS_23h_Skarik_EmoteResponse_01,
			ULDA_Dungeon_Boss_23h.VO_ULDA_BOSS_23h_Skarik_Intro_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003F44 RID: 16196 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003F45 RID: 16197 RVA: 0x0014F908 File Offset: 0x0014DB08
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_23h.VO_ULDA_BOSS_23h_Skarik_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_23h.VO_ULDA_BOSS_23h_Skarik_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_23h.VO_ULDA_BOSS_23h_Skarik_EmoteResponse_01;
	}

	// Token: 0x04002BFA RID: 11258
	private static readonly AssetReference VO_ULDA_BOSS_23h_Skarik_Death_01 = new AssetReference("VO_ULDA_BOSS_23h_Skarik_Death_01.prefab:c0f0da96f4d215949848a8cf35f86e6e");

	// Token: 0x04002BFB RID: 11259
	private static readonly AssetReference VO_ULDA_BOSS_23h_Skarik_Defeat_01 = new AssetReference("VO_ULDA_BOSS_23h_Skarik_Defeat_01.prefab:3565dbf6f8e98624690caec3eb5cadcc");

	// Token: 0x04002BFC RID: 11260
	private static readonly AssetReference VO_ULDA_BOSS_23h_Skarik_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_23h_Skarik_EmoteResponse_01.prefab:74a952fdcb508214684b03c6304b4a91");

	// Token: 0x04002BFD RID: 11261
	private static readonly AssetReference VO_ULDA_BOSS_23h_Skarik_Intro_01 = new AssetReference("VO_ULDA_BOSS_23h_Skarik_Intro_01.prefab:209d4b2b6802c9045b2aef6ed9ee92e0");
}
