using System.Collections.Generic;

public class ULDA_Dungeon_Boss_57h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_57h_WingedGuardian_Death = new AssetReference("VO_ULDA_BOSS_57h_WingedGuardian_Death.prefab:a050f4e742a2f1f46a9e1c0af8fe68e4");

	private static readonly AssetReference VO_ULDA_BOSS_57h_WingedGuardian_Defeat = new AssetReference("VO_ULDA_BOSS_57h_WingedGuardian_Defeat.prefab:b561e60190b351f4a96bcdc32438cbe2");

	private static readonly AssetReference VO_ULDA_BOSS_57h_WingedGuardian_EmoteResponse = new AssetReference("VO_ULDA_BOSS_57h_WingedGuardian_EmoteResponse.prefab:2f09e01cf38c45549b5ac8e22dd5854d");

	private static readonly AssetReference VO_ULDA_BOSS_57h_WingedGuardian_Intro = new AssetReference("VO_ULDA_BOSS_57h_WingedGuardian_Intro.prefab:1f6de710b8037ba42a428c99c700b955");

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string> { VO_ULDA_BOSS_57h_WingedGuardian_Death, VO_ULDA_BOSS_57h_WingedGuardian_Defeat, VO_ULDA_BOSS_57h_WingedGuardian_EmoteResponse, VO_ULDA_BOSS_57h_WingedGuardian_Intro };
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
		m_introLine = VO_ULDA_BOSS_57h_WingedGuardian_Intro;
		m_deathLine = VO_ULDA_BOSS_57h_WingedGuardian_Death;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_57h_WingedGuardian_EmoteResponse;
	}
}
