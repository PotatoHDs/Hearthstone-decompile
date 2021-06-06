using System.Collections.Generic;

public class ULDA_Dungeon_Boss_68h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_68h_WeaponizedWasp_Death = new AssetReference("VO_ULDA_BOSS_68h_WeaponizedWasp_Death.prefab:baa76151643f4c549a8e96cf03f40ea1");

	private static readonly AssetReference VO_ULDA_BOSS_68h_WeaponizedWasp_EmoteResponse = new AssetReference("VO_ULDA_BOSS_68h_WeaponizedWasp_EmoteResponse.prefab:e7376ecf5e990ee499abef9a55d0609e");

	private static readonly AssetReference VO_ULDA_BOSS_68h_WeaponizedWasp_Intro = new AssetReference("VO_ULDA_BOSS_68h_WeaponizedWasp_Intro.prefab:8819bc9647b9c2e45bb90e3e929fd7f1");

	private static readonly AssetReference VO_ULDA_BOSS_68h_WeaponizedWasp_PlayerDefeat = new AssetReference("VO_ULDA_BOSS_68h_WeaponizedWasp_PlayerDefeat.prefab:0be271f5e99692c49908ea8b0d93b16a");

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string> { VO_ULDA_BOSS_68h_WeaponizedWasp_Death, VO_ULDA_BOSS_68h_WeaponizedWasp_PlayerDefeat, VO_ULDA_BOSS_68h_WeaponizedWasp_EmoteResponse, VO_ULDA_BOSS_68h_WeaponizedWasp_Intro };
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_ULDA_BOSS_68h_WeaponizedWasp_Intro;
		m_deathLine = VO_ULDA_BOSS_68h_WeaponizedWasp_Death;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_68h_WeaponizedWasp_EmoteResponse;
	}
}
