using System;
using System.Collections.Generic;

// Token: 0x0200048C RID: 1164
public class ULDA_Dungeon_Boss_15h : ULDA_Dungeon
{
	// Token: 0x06003EDE RID: 16094 RVA: 0x0014D800 File Offset: 0x0014BA00
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_15h.VO_ULDA_BOSS_15h_LtHerring_Death,
			ULDA_Dungeon_Boss_15h.VO_ULDA_BOSS_15h_LtHerring_Defeat,
			ULDA_Dungeon_Boss_15h.VO_ULDA_BOSS_15h_LtHerring_EmoteResponse,
			ULDA_Dungeon_Boss_15h.VO_ULDA_BOSS_15h_LtHerring_Intro
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003EDF RID: 16095 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003EE0 RID: 16096 RVA: 0x0014D8A4 File Offset: 0x0014BAA4
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_15h.VO_ULDA_BOSS_15h_LtHerring_Intro;
		this.m_deathLine = ULDA_Dungeon_Boss_15h.VO_ULDA_BOSS_15h_LtHerring_Death;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_15h.VO_ULDA_BOSS_15h_LtHerring_EmoteResponse;
	}

	// Token: 0x04002B6B RID: 11115
	private static readonly AssetReference VO_ULDA_BOSS_15h_LtHerring_Death = new AssetReference("VO_ULDA_BOSS_15h_LtHerring_Death.prefab:c83a9095f36dc6345a027dda71d4119b");

	// Token: 0x04002B6C RID: 11116
	private static readonly AssetReference VO_ULDA_BOSS_15h_LtHerring_Defeat = new AssetReference("VO_ULDA_BOSS_15h_LtHerring_Defeat.prefab:b2cf070e6c096e3449670a7e1c3cf9b4");

	// Token: 0x04002B6D RID: 11117
	private static readonly AssetReference VO_ULDA_BOSS_15h_LtHerring_EmoteResponse = new AssetReference("VO_ULDA_BOSS_15h_LtHerring_EmoteResponse.prefab:d00b38d0eaf13f74da257b97cf48b585");

	// Token: 0x04002B6E RID: 11118
	private static readonly AssetReference VO_ULDA_BOSS_15h_LtHerring_Intro = new AssetReference("VO_ULDA_BOSS_15h_LtHerring_Intro.prefab:420a245aa0c72d046bf5520b97619fa4");
}
