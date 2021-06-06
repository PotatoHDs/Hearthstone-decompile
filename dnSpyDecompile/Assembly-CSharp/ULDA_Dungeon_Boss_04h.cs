using System;
using System.Collections.Generic;

// Token: 0x02000481 RID: 1153
public class ULDA_Dungeon_Boss_04h : ULDA_Dungeon
{
	// Token: 0x06003E71 RID: 15985 RVA: 0x0014B384 File Offset: 0x00149584
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_04h.VO_ULDA_BOSS_04h_NashTheGreatworm_Death_01,
			ULDA_Dungeon_Boss_04h.VO_ULDA_BOSS_04h_NashTheGreatworm_Defeat_01,
			ULDA_Dungeon_Boss_04h.VO_ULDA_BOSS_04h_NashTheGreatworm_EmoteResponse_01,
			ULDA_Dungeon_Boss_04h.VO_ULDA_BOSS_04h_NashTheGreatworm_Intro_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003E72 RID: 15986 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003E73 RID: 15987 RVA: 0x0014B428 File Offset: 0x00149628
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_04h.VO_ULDA_BOSS_04h_NashTheGreatworm_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_04h.VO_ULDA_BOSS_04h_NashTheGreatworm_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_04h.VO_ULDA_BOSS_04h_NashTheGreatworm_EmoteResponse_01;
	}

	// Token: 0x04002AC9 RID: 10953
	private static readonly AssetReference VO_ULDA_BOSS_04h_NashTheGreatworm_Death_01 = new AssetReference("VO_ULDA_BOSS_04h_NashTheGreatworm_Death_01.prefab:1bed1cbf5c351ee44bc6fda69cfc9880");

	// Token: 0x04002ACA RID: 10954
	private static readonly AssetReference VO_ULDA_BOSS_04h_NashTheGreatworm_Defeat_01 = new AssetReference("VO_ULDA_BOSS_04h_NashTheGreatworm_Defeat_01.prefab:4c0cc44b1d322654faf9f293fa7362cb");

	// Token: 0x04002ACB RID: 10955
	private static readonly AssetReference VO_ULDA_BOSS_04h_NashTheGreatworm_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_04h_NashTheGreatworm_EmoteResponse_01.prefab:8a2a0a92b70af624580c315c763a8a0d");

	// Token: 0x04002ACC RID: 10956
	private static readonly AssetReference VO_ULDA_BOSS_04h_NashTheGreatworm_Intro_01 = new AssetReference("VO_ULDA_BOSS_04h_NashTheGreatworm_Intro_01.prefab:a44e660daa30d704395e929bf58f472f");
}
