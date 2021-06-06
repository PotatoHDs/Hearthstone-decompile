using System.Collections;
using System.Collections.Generic;

public class BOTA_Survival_Puzzle_3 : BOTA_MissionEntity
{
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Complete_01 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Complete_01.prefab:3c812cfba5f46754ea44b18080955ab9");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_01.prefab:7e5a5494219d7ed419a5991beb1495db");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_02.prefab:b551c0831c32f45408039a9cf3750d95");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_03.prefab:939582ad143baf346a211bc9c30558df");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Idle_01 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Idle_01.prefab:d049be4b509d5fe409293c8276c5eeee");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Idle_02 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Idle_02.prefab:a6a04b5b5c9c00243b7bd9bf39e38eb5");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Idle_03 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Idle_03.prefab:a3959fb59c2d66e48bbc6767e5cfa00e");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Idle_04 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Idle_04.prefab:66d3c90e8a2bd5b4f89db63fc1d6fa98");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Idle_05 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Idle_05.prefab:6451a5a90b0b7ee4bbffb87acc5fb74c");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Idle_06 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Idle_06.prefab:96efb8f844f1eca41bb80c1a410d14a8");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Idle_07 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Idle_07.prefab:30dc925ae684ae749bbba2c1fa47b74e");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Intro_01 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Intro_01.prefab:e05253ceaaf2e1b488c827f934f9bc65");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Restart_02 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Restart_02.prefab:0d15dc6c81a9f9a4c905491033ba5e89");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Restart_03 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Restart_03.prefab:4417be44c09d494489c32d2661af130a");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Restart_04 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Restart_04.prefab:8c97544373965914da7510603bb48223");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Restart_05 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Restart_05.prefab:1d7399b36c025ea4b86fb995adbc9df4");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Restart_06 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Restart_06.prefab:d1e94490c971c0b41ac6b837c0e9ef61");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Restart_07 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Restart_07.prefab:2b2f3ccb00bb5d3429c7b080ab1e790e");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Restart_08 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Restart_08.prefab:3f9da67399f2ad449b2e06e680bec62c");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Return_01 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Return_01.prefab:50d8e1b2fe33df54b9e89526f6502f03");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Puzzle_01.prefab:77323f7403d90a04fbe85068584bdf26");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Puzzle_03.prefab:33e97cb224b60ca4abd3ba36d43080e0");

	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Puzzle_04 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Puzzle_04.prefab:ca3c2f60b4dda074b8dc96e2624eb704");

	private string COMPLETE_LINE = VO_BOTA_BOSS_17h_Male_Mech_Complete_01;

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_BOTA_BOSS_17h_Male_Mech_Complete_01, VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_01, VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_02, VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_03, VO_BOTA_BOSS_17h_Male_Mech_Idle_01, VO_BOTA_BOSS_17h_Male_Mech_Idle_02, VO_BOTA_BOSS_17h_Male_Mech_Idle_03, VO_BOTA_BOSS_17h_Male_Mech_Idle_04, VO_BOTA_BOSS_17h_Male_Mech_Idle_05, VO_BOTA_BOSS_17h_Male_Mech_Idle_06,
			VO_BOTA_BOSS_17h_Male_Mech_Idle_07, VO_BOTA_BOSS_17h_Male_Mech_Intro_01, VO_BOTA_BOSS_17h_Male_Mech_Restart_02, VO_BOTA_BOSS_17h_Male_Mech_Restart_03, VO_BOTA_BOSS_17h_Male_Mech_Restart_04, VO_BOTA_BOSS_17h_Male_Mech_Restart_05, VO_BOTA_BOSS_17h_Male_Mech_Restart_06, VO_BOTA_BOSS_17h_Male_Mech_Restart_07, VO_BOTA_BOSS_17h_Male_Mech_Restart_08, VO_BOTA_BOSS_17h_Male_Mech_Return_01,
			VO_BOTA_BOSS_17h_Male_Mech_Puzzle_01, VO_BOTA_BOSS_17h_Male_Mech_Puzzle_03, VO_BOTA_BOSS_17h_Male_Mech_Puzzle_04
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
		BOTA_MissionEntity.s_introLine = VO_BOTA_BOSS_17h_Male_Mech_Intro_01;
		BOTA_MissionEntity.s_returnLine = VO_BOTA_BOSS_17h_Male_Mech_Return_01;
		s_victoryLine_1 = null;
		s_victoryLine_2 = VO_BOTA_BOSS_17h_Male_Mech_Puzzle_04;
		s_victoryLine_3 = null;
		s_victoryLine_4 = VO_BOTA_BOSS_17h_Male_Mech_Puzzle_01;
		s_victoryLine_5 = null;
		s_victoryLine_6 = VO_BOTA_BOSS_17h_Male_Mech_Puzzle_03;
		s_victoryLine_7 = null;
		s_victoryLine_8 = null;
		s_victoryLine_9 = null;
		s_emoteLines = new List<string> { VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_01, VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_02, VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_03 };
		s_idleLines = new List<string> { VO_BOTA_BOSS_17h_Male_Mech_Idle_01, VO_BOTA_BOSS_17h_Male_Mech_Idle_02, VO_BOTA_BOSS_17h_Male_Mech_Idle_03, VO_BOTA_BOSS_17h_Male_Mech_Idle_04, VO_BOTA_BOSS_17h_Male_Mech_Idle_05, VO_BOTA_BOSS_17h_Male_Mech_Idle_06, VO_BOTA_BOSS_17h_Male_Mech_Idle_07 };
		s_restartLines = new List<string> { VO_BOTA_BOSS_17h_Male_Mech_Restart_02, VO_BOTA_BOSS_17h_Male_Mech_Restart_03, VO_BOTA_BOSS_17h_Male_Mech_Restart_04, VO_BOTA_BOSS_17h_Male_Mech_Restart_05, VO_BOTA_BOSS_17h_Male_Mech_Restart_06, VO_BOTA_BOSS_17h_Male_Mech_Restart_07, VO_BOTA_BOSS_17h_Male_Mech_Restart_08 };
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
