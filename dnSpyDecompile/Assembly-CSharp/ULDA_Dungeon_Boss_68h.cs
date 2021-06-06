using System;
using System.Collections.Generic;

// Token: 0x020004C0 RID: 1216
public class ULDA_Dungeon_Boss_68h : ULDA_Dungeon
{
	// Token: 0x06004134 RID: 16692 RVA: 0x0015CAB4 File Offset: 0x0015ACB4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_68h.VO_ULDA_BOSS_68h_WeaponizedWasp_Death,
			ULDA_Dungeon_Boss_68h.VO_ULDA_BOSS_68h_WeaponizedWasp_PlayerDefeat,
			ULDA_Dungeon_Boss_68h.VO_ULDA_BOSS_68h_WeaponizedWasp_EmoteResponse,
			ULDA_Dungeon_Boss_68h.VO_ULDA_BOSS_68h_WeaponizedWasp_Intro
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004135 RID: 16693 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004136 RID: 16694 RVA: 0x0015CB58 File Offset: 0x0015AD58
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_68h.VO_ULDA_BOSS_68h_WeaponizedWasp_Intro;
		this.m_deathLine = ULDA_Dungeon_Boss_68h.VO_ULDA_BOSS_68h_WeaponizedWasp_Death;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_68h.VO_ULDA_BOSS_68h_WeaponizedWasp_EmoteResponse;
	}

	// Token: 0x04002FF6 RID: 12278
	private static readonly AssetReference VO_ULDA_BOSS_68h_WeaponizedWasp_Death = new AssetReference("VO_ULDA_BOSS_68h_WeaponizedWasp_Death.prefab:baa76151643f4c549a8e96cf03f40ea1");

	// Token: 0x04002FF7 RID: 12279
	private static readonly AssetReference VO_ULDA_BOSS_68h_WeaponizedWasp_EmoteResponse = new AssetReference("VO_ULDA_BOSS_68h_WeaponizedWasp_EmoteResponse.prefab:e7376ecf5e990ee499abef9a55d0609e");

	// Token: 0x04002FF8 RID: 12280
	private static readonly AssetReference VO_ULDA_BOSS_68h_WeaponizedWasp_Intro = new AssetReference("VO_ULDA_BOSS_68h_WeaponizedWasp_Intro.prefab:8819bc9647b9c2e45bb90e3e929fd7f1");

	// Token: 0x04002FF9 RID: 12281
	private static readonly AssetReference VO_ULDA_BOSS_68h_WeaponizedWasp_PlayerDefeat = new AssetReference("VO_ULDA_BOSS_68h_WeaponizedWasp_PlayerDefeat.prefab:0be271f5e99692c49908ea8b0d93b16a");
}
