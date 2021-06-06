using System.Collections.Generic;

public class ULDA_Dungeon_Boss_59h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_59h_Direbat_Death = new AssetReference("VO_ULDA_BOSS_59h_Direbat_Death.prefab:96c1eb6503063ec4e80c26b7a5f722e4");

	private static readonly AssetReference VO_ULDA_BOSS_59h_Direbat_Defeat = new AssetReference("VO_ULDA_BOSS_59h_Direbat_Defeat.prefab:49e88284ba7f8c940ab36228a7777596");

	private static readonly AssetReference VO_ULDA_BOSS_59h_Direbat_EmoteResponse = new AssetReference("VO_ULDA_BOSS_59h_Direbat_EmoteResponse.prefab:1a6fc68c44fc167419ec4fbd9d4fe76b");

	private static readonly AssetReference VO_ULDA_BOSS_59h_Direbat_Intro = new AssetReference("VO_ULDA_BOSS_59h_Direbat_Intro.prefab:17fcdafd96828c94a899de8668b95ce2");

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string> { VO_ULDA_BOSS_59h_Direbat_Death, VO_ULDA_BOSS_59h_Direbat_Defeat, VO_ULDA_BOSS_59h_Direbat_EmoteResponse, VO_ULDA_BOSS_59h_Direbat_Intro };
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
		m_introLine = VO_ULDA_BOSS_59h_Direbat_Intro;
		m_deathLine = VO_ULDA_BOSS_59h_Direbat_Death;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_59h_Direbat_EmoteResponse;
	}
}
