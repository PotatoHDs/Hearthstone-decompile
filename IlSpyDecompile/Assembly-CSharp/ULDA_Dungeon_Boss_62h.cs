using System.Collections.Generic;

public class ULDA_Dungeon_Boss_62h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_62h_Oasis_Death = new AssetReference("VO_ULDA_BOSS_62h_Oasis_Death.prefab:f7d27f076147a514d981bcf0e6f2fa9e");

	private static readonly AssetReference VO_ULDA_BOSS_62h_Oasis_EmoteResponse = new AssetReference("VO_ULDA_BOSS_62h_Oasis_EmoteResponse.prefab:a695e273ec92bfa40bb2cb93bb4e0497");

	private static readonly AssetReference VO_ULDA_BOSS_62h_Oasis_Intro = new AssetReference("VO_ULDA_BOSS_62h_Oasis_Intro.prefab:13a45b003cbd89f4d91c55fb015ef597");

	private static readonly AssetReference VO_ULDA_BOSS_62h_Oasis_PlayerDefeat = new AssetReference("VO_ULDA_BOSS_62h_Oasis_PlayerDefeat.prefab:6a7f9856ae090194692ccba4cff62b47");

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string> { VO_ULDA_BOSS_62h_Oasis_Death, VO_ULDA_BOSS_62h_Oasis_PlayerDefeat, VO_ULDA_BOSS_62h_Oasis_EmoteResponse, VO_ULDA_BOSS_62h_Oasis_Intro };
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
		m_introLine = VO_ULDA_BOSS_62h_Oasis_Intro;
		m_deathLine = VO_ULDA_BOSS_62h_Oasis_Death;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_62h_Oasis_EmoteResponse;
	}
}
