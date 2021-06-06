using System.Collections.Generic;

public class ULDA_Dungeon_Boss_79h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_79h_ArmadilloMech_Death = new AssetReference("VO_ULDA_BOSS_79h_ArmadilloMech_Death.prefab:5b96ddd8a993ecd46977e6ca4da892e9");

	private static readonly AssetReference VO_ULDA_BOSS_79h_ArmadilloMech_Defeat = new AssetReference("VO_ULDA_BOSS_79h_ArmadilloMech_Defeat.prefab:1ac5065faf456b24baf543d5bed95d2f");

	private static readonly AssetReference VO_ULDA_BOSS_79h_ArmadilloMech_EmoteResponse = new AssetReference("VO_ULDA_BOSS_79h_ArmadilloMech_EmoteResponse.prefab:efce6900921ed3a429cd4d831f7ea647");

	private static readonly AssetReference VO_ULDA_BOSS_79h_ArmadilloMech_Intro = new AssetReference("VO_ULDA_BOSS_79h_ArmadilloMech_Intro.prefab:cc70c478e01ec274393e37208438c89c");

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string> { VO_ULDA_BOSS_79h_ArmadilloMech_Death, VO_ULDA_BOSS_79h_ArmadilloMech_Defeat, VO_ULDA_BOSS_79h_ArmadilloMech_EmoteResponse, VO_ULDA_BOSS_79h_ArmadilloMech_Intro };
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
		m_introLine = VO_ULDA_BOSS_79h_ArmadilloMech_Intro;
		m_deathLine = VO_ULDA_BOSS_79h_ArmadilloMech_Death;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_79h_ArmadilloMech_EmoteResponse;
	}
}
