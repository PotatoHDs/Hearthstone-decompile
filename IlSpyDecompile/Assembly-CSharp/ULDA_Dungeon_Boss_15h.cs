using System.Collections.Generic;

public class ULDA_Dungeon_Boss_15h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_15h_LtHerring_Death = new AssetReference("VO_ULDA_BOSS_15h_LtHerring_Death.prefab:c83a9095f36dc6345a027dda71d4119b");

	private static readonly AssetReference VO_ULDA_BOSS_15h_LtHerring_Defeat = new AssetReference("VO_ULDA_BOSS_15h_LtHerring_Defeat.prefab:b2cf070e6c096e3449670a7e1c3cf9b4");

	private static readonly AssetReference VO_ULDA_BOSS_15h_LtHerring_EmoteResponse = new AssetReference("VO_ULDA_BOSS_15h_LtHerring_EmoteResponse.prefab:d00b38d0eaf13f74da257b97cf48b585");

	private static readonly AssetReference VO_ULDA_BOSS_15h_LtHerring_Intro = new AssetReference("VO_ULDA_BOSS_15h_LtHerring_Intro.prefab:420a245aa0c72d046bf5520b97619fa4");

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string> { VO_ULDA_BOSS_15h_LtHerring_Death, VO_ULDA_BOSS_15h_LtHerring_Defeat, VO_ULDA_BOSS_15h_LtHerring_EmoteResponse, VO_ULDA_BOSS_15h_LtHerring_Intro };
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
		m_introLine = VO_ULDA_BOSS_15h_LtHerring_Intro;
		m_deathLine = VO_ULDA_BOSS_15h_LtHerring_Death;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_15h_LtHerring_EmoteResponse;
	}
}
