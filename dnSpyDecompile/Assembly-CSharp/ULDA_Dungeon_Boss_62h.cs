using System;
using System.Collections.Generic;

// Token: 0x020004BB RID: 1211
public class ULDA_Dungeon_Boss_62h : ULDA_Dungeon
{
	// Token: 0x060040FE RID: 16638 RVA: 0x0015AEC0 File Offset: 0x001590C0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_62h.VO_ULDA_BOSS_62h_Oasis_Death,
			ULDA_Dungeon_Boss_62h.VO_ULDA_BOSS_62h_Oasis_PlayerDefeat,
			ULDA_Dungeon_Boss_62h.VO_ULDA_BOSS_62h_Oasis_EmoteResponse,
			ULDA_Dungeon_Boss_62h.VO_ULDA_BOSS_62h_Oasis_Intro
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060040FF RID: 16639 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004100 RID: 16640 RVA: 0x0015AF64 File Offset: 0x00159164
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_62h.VO_ULDA_BOSS_62h_Oasis_Intro;
		this.m_deathLine = ULDA_Dungeon_Boss_62h.VO_ULDA_BOSS_62h_Oasis_Death;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_62h.VO_ULDA_BOSS_62h_Oasis_EmoteResponse;
	}

	// Token: 0x04002F5A RID: 12122
	private static readonly AssetReference VO_ULDA_BOSS_62h_Oasis_Death = new AssetReference("VO_ULDA_BOSS_62h_Oasis_Death.prefab:f7d27f076147a514d981bcf0e6f2fa9e");

	// Token: 0x04002F5B RID: 12123
	private static readonly AssetReference VO_ULDA_BOSS_62h_Oasis_EmoteResponse = new AssetReference("VO_ULDA_BOSS_62h_Oasis_EmoteResponse.prefab:a695e273ec92bfa40bb2cb93bb4e0497");

	// Token: 0x04002F5C RID: 12124
	private static readonly AssetReference VO_ULDA_BOSS_62h_Oasis_Intro = new AssetReference("VO_ULDA_BOSS_62h_Oasis_Intro.prefab:13a45b003cbd89f4d91c55fb015ef597");

	// Token: 0x04002F5D RID: 12125
	private static readonly AssetReference VO_ULDA_BOSS_62h_Oasis_PlayerDefeat = new AssetReference("VO_ULDA_BOSS_62h_Oasis_PlayerDefeat.prefab:6a7f9856ae090194692ccba4cff62b47");
}
