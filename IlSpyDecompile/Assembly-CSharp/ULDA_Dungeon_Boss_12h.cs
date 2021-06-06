using System.Collections.Generic;

public class ULDA_Dungeon_Boss_12h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_12h_Pyramad_Death_01 = new AssetReference("VO_ULDA_BOSS_12h_Pyramad_Death_01.prefab:bb52f62014ecf00469de65df3542ed24");

	private static readonly AssetReference VO_ULDA_BOSS_12h_Pyramad_Defeat_01 = new AssetReference("VO_ULDA_BOSS_12h_Pyramad_Defeat_01.prefab:f458e10d636144343a7c2c0bd4b679cf");

	private static readonly AssetReference VO_ULDA_BOSS_12h_Pyramad_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_12h_Pyramad_EmoteResponse_01.prefab:36182e8e51464de4a98dec5e14d53c57");

	private static readonly AssetReference VO_ULDA_BOSS_12h_Pyramad_Intro_01 = new AssetReference("VO_ULDA_BOSS_12h_Pyramad_Intro_01.prefab:5e899eb20b55dd545b6e41eef82efc79");

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string> { VO_ULDA_BOSS_12h_Pyramad_Death_01, VO_ULDA_BOSS_12h_Pyramad_Defeat_01, VO_ULDA_BOSS_12h_Pyramad_EmoteResponse_01, VO_ULDA_BOSS_12h_Pyramad_Intro_01 };
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
		m_introLine = VO_ULDA_BOSS_12h_Pyramad_Intro_01;
		m_deathLine = VO_ULDA_BOSS_12h_Pyramad_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_12h_Pyramad_EmoteResponse_01;
	}
}
