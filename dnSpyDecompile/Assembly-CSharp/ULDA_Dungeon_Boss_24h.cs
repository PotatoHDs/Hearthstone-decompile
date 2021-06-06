using System;
using System.Collections.Generic;

// Token: 0x02000495 RID: 1173
public class ULDA_Dungeon_Boss_24h : ULDA_Dungeon
{
	// Token: 0x06003F48 RID: 16200 RVA: 0x0014F980 File Offset: 0x0014DB80
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_24h.VO_ULDA_BOSS_24h_SuspiciousPalmTree_Death,
			ULDA_Dungeon_Boss_24h.VO_ULDA_BOSS_24h_SuspiciousPalmTree_Defeat,
			ULDA_Dungeon_Boss_24h.VO_ULDA_BOSS_24h_SuspiciousPalmTree_EmoteResponse,
			ULDA_Dungeon_Boss_24h.VO_ULDA_BOSS_24h_SuspiciousPalmTree_Intro
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003F49 RID: 16201 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003F4A RID: 16202 RVA: 0x0014FA24 File Offset: 0x0014DC24
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_24h.VO_ULDA_BOSS_24h_SuspiciousPalmTree_Intro;
		this.m_deathLine = ULDA_Dungeon_Boss_24h.VO_ULDA_BOSS_24h_SuspiciousPalmTree_Death;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_24h.VO_ULDA_BOSS_24h_SuspiciousPalmTree_EmoteResponse;
	}

	// Token: 0x04002BFE RID: 11262
	private static readonly AssetReference VO_ULDA_BOSS_24h_SuspiciousPalmTree_Death = new AssetReference("VO_ULDA_BOSS_24h_SuspiciousPalmTree_Death.prefab:9ec4a17f58bb4d6459cd0b3d4d180076");

	// Token: 0x04002BFF RID: 11263
	private static readonly AssetReference VO_ULDA_BOSS_24h_SuspiciousPalmTree_Defeat = new AssetReference("VO_ULDA_BOSS_24h_SuspiciousPalmTree_Defeat.prefab:1ebe6b8b74eb9b141a24ca6ceb8a0bb2");

	// Token: 0x04002C00 RID: 11264
	private static readonly AssetReference VO_ULDA_BOSS_24h_SuspiciousPalmTree_EmoteResponse = new AssetReference("VO_ULDA_BOSS_24h_SuspiciousPalmTree_EmoteResponse.prefab:8225a92d06879954f8830734c5f973a9");

	// Token: 0x04002C01 RID: 11265
	private static readonly AssetReference VO_ULDA_BOSS_24h_SuspiciousPalmTree_Intro = new AssetReference("VO_ULDA_BOSS_24h_SuspiciousPalmTree_Intro.prefab:dd27a8380dbcf394ab87ca1bdd26243e");
}
