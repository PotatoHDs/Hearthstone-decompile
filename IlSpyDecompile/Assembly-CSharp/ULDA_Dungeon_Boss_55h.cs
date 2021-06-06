using System.Collections.Generic;

public class ULDA_Dungeon_Boss_55h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_55h_WaterTotingMurlocs_Birth = new AssetReference("VO_ULDA_BOSS_55h_WaterTotingMurlocs_Birth.prefab:01476de4e3df6c043ba1294641446c14");

	private static readonly AssetReference VO_ULDA_BOSS_55h_WaterTotingMurlocs_Death = new AssetReference("VO_ULDA_BOSS_55h_WaterTotingMurlocs_Death.prefab:bccb469a8623a77459b83b9d9490edad");

	private static readonly AssetReference VO_ULDA_BOSS_55h_WaterTotingMurlocs_Defeat = new AssetReference("VO_ULDA_BOSS_55h_WaterTotingMurlocs_Defeat.prefab:b2bab368c56dcd44bb2d82f0f1067dd8");

	private static readonly AssetReference VO_ULDA_BOSS_55h_WaterTotingMurlocs_EmoteResponse = new AssetReference("VO_ULDA_BOSS_55h_WaterTotingMurlocs_EmoteResponse.prefab:dfaeb48349404bd498ce431ebc244cd4");

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string> { VO_ULDA_BOSS_55h_WaterTotingMurlocs_Birth, VO_ULDA_BOSS_55h_WaterTotingMurlocs_Death, VO_ULDA_BOSS_55h_WaterTotingMurlocs_Defeat, VO_ULDA_BOSS_55h_WaterTotingMurlocs_EmoteResponse };
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
		m_introLine = VO_ULDA_BOSS_55h_WaterTotingMurlocs_Birth;
		m_deathLine = VO_ULDA_BOSS_55h_WaterTotingMurlocs_Death;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_55h_WaterTotingMurlocs_EmoteResponse;
	}
}
