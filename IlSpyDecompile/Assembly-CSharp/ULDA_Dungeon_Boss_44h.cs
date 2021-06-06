using System.Collections.Generic;

public class ULDA_Dungeon_Boss_44h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_44h_TrapRoom_Death = new AssetReference("VO_ULDA_BOSS_44h_TrapRoom_Death.prefab:c367791e544f2bb468526474bd808762");

	private static readonly AssetReference VO_ULDA_BOSS_44h_TrapRoom_Defeat = new AssetReference("VO_ULDA_BOSS_44h_TrapRoom_Defeat.prefab:3953c061798e64743bb98bbf7ca37d30");

	private static readonly AssetReference VO_ULDA_BOSS_44h_TrapRoom_EmoteResponse = new AssetReference("VO_ULDA_BOSS_44h_TrapRoom_EmoteResponse.prefab:4f8b7ac1c85dab140a10c08793e0ee75");

	private static readonly AssetReference VO_ULDA_BOSS_44h_TrapRoom_Intro = new AssetReference("VO_ULDA_BOSS_44h_TrapRoom_Intro.prefab:a58d8dc07b57b3d4ca8f7e403461209b");

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string> { VO_ULDA_BOSS_44h_TrapRoom_Death, VO_ULDA_BOSS_44h_TrapRoom_Defeat, VO_ULDA_BOSS_44h_TrapRoom_EmoteResponse, VO_ULDA_BOSS_44h_TrapRoom_Intro };
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
		m_introLine = VO_ULDA_BOSS_44h_TrapRoom_Intro;
		m_deathLine = VO_ULDA_BOSS_44h_TrapRoom_Death;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_44h_TrapRoom_EmoteResponse;
	}
}
