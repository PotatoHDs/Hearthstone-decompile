using System.Collections.Generic;

public class ULDA_Dungeon_Boss_23h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_23h_Skarik_Death_01 = new AssetReference("VO_ULDA_BOSS_23h_Skarik_Death_01.prefab:c0f0da96f4d215949848a8cf35f86e6e");

	private static readonly AssetReference VO_ULDA_BOSS_23h_Skarik_Defeat_01 = new AssetReference("VO_ULDA_BOSS_23h_Skarik_Defeat_01.prefab:3565dbf6f8e98624690caec3eb5cadcc");

	private static readonly AssetReference VO_ULDA_BOSS_23h_Skarik_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_23h_Skarik_EmoteResponse_01.prefab:74a952fdcb508214684b03c6304b4a91");

	private static readonly AssetReference VO_ULDA_BOSS_23h_Skarik_Intro_01 = new AssetReference("VO_ULDA_BOSS_23h_Skarik_Intro_01.prefab:209d4b2b6802c9045b2aef6ed9ee92e0");

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string> { VO_ULDA_BOSS_23h_Skarik_Death_01, VO_ULDA_BOSS_23h_Skarik_Defeat_01, VO_ULDA_BOSS_23h_Skarik_EmoteResponse_01, VO_ULDA_BOSS_23h_Skarik_Intro_01 };
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
		m_introLine = VO_ULDA_BOSS_23h_Skarik_Intro_01;
		m_deathLine = VO_ULDA_BOSS_23h_Skarik_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_23h_Skarik_EmoteResponse_01;
	}
}
