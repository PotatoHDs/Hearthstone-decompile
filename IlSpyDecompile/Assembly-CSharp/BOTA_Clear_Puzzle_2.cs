using System.Collections;
using System.Collections.Generic;

public class BOTA_Clear_Puzzle_2 : BOTA_MissionEntity
{
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Complete_02 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Complete_02.prefab:88c4e83709b3c364b8eed5486ae6068c");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_01.prefab:5f4e86e4317240747b6eaba7a2927278");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_03.prefab:cc3732dd8a47bca449d5ff5f7e079109");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_04 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_04.prefab:31440c4385f676545854642e92211e21");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Idle_01 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Idle_01.prefab:cec08e69f54e48446b845afa4451119a");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Idle_02 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Idle_02.prefab:6e32151936e1bfe498ef82276129f68b");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Idle_03 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Idle_03.prefab:f582137ccf02b224ca752a4599187fcb");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Idle_05 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Idle_05.prefab:c107688b86154744fb1b414758d35daa");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Idle_06 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Idle_06.prefab:60677e8cfc94a5d4dbdbf730e3eaf8eb");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Idle_07 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Idle_07.prefab:a358726abee05e447b9e9c7f194d57d3");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Idle_09 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Idle_09.prefab:22df2a4665cdde640a1d439a8af04b42");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Intro_01 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Intro_01.prefab:75dc11c0503b1a545a4c91453eee0823");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Restart_01 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Restart_01.prefab:a09a7b5c3e5a1ee4db67833b5c90d075");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Restart_02 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Restart_02.prefab:bf1745d887cf03349851e5c721aabb42");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Restart_03 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Restart_03.prefab:5520fe25b536dc448b8a15100103caa8");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Restart_04 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Restart_04.prefab:c32a06b894185eb4896016d7b9a6849e");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Restart_05 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Restart_05.prefab:ff847044bbc98bb4480c89d862a221ce");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Return_01 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Return_01.prefab:33d44670b3af8d94fba2764415232c5f");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_01.prefab:023846e52033fd545b5340a711e29827");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_02.prefab:e3a57f2f25cfccd408ce2616a06faad4");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_03.prefab:570ad1e08cb0f48448048bea319738a2");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Stuck_02 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Stuck_02.prefab:6904eed4811e7b84ab97185aa6c18b7b");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Stuck_03 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Stuck_03.prefab:3fb540bbaca7af5459842f1d1c69a0f4");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Stuck_04 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Stuck_04.prefab:e101d2e1ea2a61d47b4b26e38b6456ce");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Stuck_01 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Stuck_01.prefab:f78e26e97373ef94ea6ea15b5dde0dad");

	private string COMPLETE_LINE = VO_BOTA_BOSS_07h_Male_Ooze_Complete_02;

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_BOTA_BOSS_07h_Male_Ooze_Complete_02, VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_01, VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_03, VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_04, VO_BOTA_BOSS_07h_Male_Ooze_Idle_01, VO_BOTA_BOSS_07h_Male_Ooze_Idle_02, VO_BOTA_BOSS_07h_Male_Ooze_Idle_03, VO_BOTA_BOSS_07h_Male_Ooze_Idle_05, VO_BOTA_BOSS_07h_Male_Ooze_Idle_06, VO_BOTA_BOSS_07h_Male_Ooze_Idle_07,
			VO_BOTA_BOSS_07h_Male_Ooze_Idle_09, VO_BOTA_BOSS_07h_Male_Ooze_Intro_01, VO_BOTA_BOSS_07h_Male_Ooze_Restart_01, VO_BOTA_BOSS_07h_Male_Ooze_Restart_02, VO_BOTA_BOSS_07h_Male_Ooze_Restart_03, VO_BOTA_BOSS_07h_Male_Ooze_Restart_04, VO_BOTA_BOSS_07h_Male_Ooze_Restart_05, VO_BOTA_BOSS_07h_Male_Ooze_Return_01, VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_01, VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_02,
			VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_03, VO_BOTA_BOSS_07h_Male_Ooze_Stuck_02, VO_BOTA_BOSS_07h_Male_Ooze_Stuck_03, VO_BOTA_BOSS_07h_Male_Ooze_Stuck_04, VO_BOTA_BOSS_07h_Male_Ooze_Stuck_01
		})
		{
			PreloadSound(item);
		}
	}

	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = VO_BOTA_BOSS_07h_Male_Ooze_Intro_01;
		BOTA_MissionEntity.s_returnLine = VO_BOTA_BOSS_07h_Male_Ooze_Return_01;
		s_victoryLine_1 = null;
		s_victoryLine_2 = VO_BOTA_BOSS_07h_Male_Ooze_Stuck_01;
		s_victoryLine_3 = VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_01;
		s_victoryLine_4 = VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_02;
		s_victoryLine_5 = null;
		s_victoryLine_6 = VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_03;
		s_victoryLine_7 = null;
		s_victoryLine_8 = null;
		s_victoryLine_9 = null;
		s_emoteLines = new List<string> { VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_01, VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_03, VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_04 };
		s_idleLines = new List<string> { VO_BOTA_BOSS_07h_Male_Ooze_Idle_01, VO_BOTA_BOSS_07h_Male_Ooze_Idle_02, VO_BOTA_BOSS_07h_Male_Ooze_Idle_03, VO_BOTA_BOSS_07h_Male_Ooze_Idle_05, VO_BOTA_BOSS_07h_Male_Ooze_Idle_06, VO_BOTA_BOSS_07h_Male_Ooze_Idle_07, VO_BOTA_BOSS_07h_Male_Ooze_Idle_09 };
		s_restartLines = new List<string> { VO_BOTA_BOSS_07h_Male_Ooze_Restart_01, VO_BOTA_BOSS_07h_Male_Ooze_Restart_02, VO_BOTA_BOSS_07h_Male_Ooze_Restart_03, VO_BOTA_BOSS_07h_Male_Ooze_Restart_04, VO_BOTA_BOSS_07h_Male_Ooze_Restart_05, VO_BOTA_BOSS_07h_Male_Ooze_Stuck_02, VO_BOTA_BOSS_07h_Male_Ooze_Stuck_03, VO_BOTA_BOSS_07h_Male_Ooze_Stuck_04 };
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (missionEvent == 99)
		{
			while (m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(enemyActor, COMPLETE_LINE);
			GameState.Get().SetBusy(busy: false);
		}
	}
}
