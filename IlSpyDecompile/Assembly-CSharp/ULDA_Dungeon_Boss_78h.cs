using System.Collections.Generic;

public class ULDA_Dungeon_Boss_78h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_78h_Octosari_Death = new AssetReference("VO_ULDA_BOSS_78h_Octosari_Death.prefab:ad50902e1822c7f43b9afc99efcb0e84");

	private static readonly AssetReference VO_ULDA_BOSS_78h_Octosari_DefeatPlayer = new AssetReference("VO_ULDA_BOSS_78h_Octosari_DefeatPlayer.prefab:97391cfd5487cdb44a8fd0a67af589d8");

	private static readonly AssetReference VO_ULDA_BOSS_78h_Octosari_EmoteResponse = new AssetReference("VO_ULDA_BOSS_78h_Octosari_EmoteResponse.prefab:66a84be4d1ebacc47b89cc6e63ff7a32");

	private static readonly AssetReference VO_ULDA_BOSS_78h_Octosari_Intro = new AssetReference("VO_ULDA_BOSS_78h_Octosari_Intro.prefab:cff7e88ec4c0dde4a99502bd038678a5");

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string> { VO_ULDA_BOSS_78h_Octosari_Death, VO_ULDA_BOSS_78h_Octosari_DefeatPlayer, VO_ULDA_BOSS_78h_Octosari_EmoteResponse, VO_ULDA_BOSS_78h_Octosari_Intro };
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
		m_introLine = VO_ULDA_BOSS_78h_Octosari_Intro;
		m_deathLine = VO_ULDA_BOSS_78h_Octosari_Death;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_78h_Octosari_EmoteResponse;
	}
}
