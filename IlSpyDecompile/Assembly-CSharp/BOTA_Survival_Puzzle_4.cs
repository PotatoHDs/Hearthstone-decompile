using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOTA_Survival_Puzzle_4 : BOTA_MissionEntity
{
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Complete_01 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Complete_01.prefab:652de58b37a2cc44593424ef18e1c0e3");

	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_EmoteResponse_01.prefab:fe1cbdd3e087f53448f8a6994e132243");

	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_EmoteResponse_02.prefab:9abec6566dab8c247ae1b802239cbcdc");

	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Idle_01 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Idle_01.prefab:78cfe5678170c684f946ce2a6de56e94");

	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Idle_02 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Idle_02.prefab:e9dcb30c8c1e1854f8eb05ff0cae0e21");

	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Idle_03 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Idle_03.prefab:7bc1b09ecbd8b4649b563356f2446c96");

	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Idle_05 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Idle_05.prefab:2568976f3c4a6544d85e35dbbd145325");

	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Idle_06 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Idle_06.prefab:19878fd22622c92488ff92448db8c8b4");

	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Idle_07 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Idle_07.prefab:25b8964a432f35f4480842930111ca45");

	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Idle_09 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Idle_09.prefab:52f153a746a56644d92ad1ed0483968f");

	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Restart_01 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Restart_01.prefab:5426d1b0dbbc20f4194fe44eef89e968");

	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Restart_02 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Restart_02.prefab:5671fb3cf6dab1e47a1abd0977e44d62");

	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Restart_03 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Restart_03.prefab:5aea3e7327483c544b766499e291ae15");

	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Restart_04 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Restart_04.prefab:c9fd07b42c8513c4793b09c065ab1345");

	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Restart_06 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Restart_06.prefab:60c118621f017f34eadf8675ba1d4ccc");

	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Return_01 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Return_01.prefab:e4e61ef196a1dc54d93654ca1931eeb4");

	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Puzzle_01.prefab:53a769c89b14e0648b1b90f7857da06a");

	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Puzzle_02.prefab:ad8baa4a98a84e9479e07a81db7217b1");

	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Puzzle_03.prefab:a5268202781ce5f41ba45fb11e1f3104");

	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Puzzle_04 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Puzzle_04.prefab:2fbfc68ed6374d248934858eb3d3c7ad");

	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Intro_02 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Intro_02.prefab:217e6f991f7f0af4989e47a4ae139bdf");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing4_Complete_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing4_Complete_01.prefab:6946f82a36bc4064eac0d36169ca6976");

	private string COMPLETE_LINE = VO_BOTA_BOSS_18h_Male_Golem_Complete_01;

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_BOTA_BOSS_18h_Male_Golem_Complete_01, VO_BOTA_BOSS_18h_Male_Golem_EmoteResponse_01, VO_BOTA_BOSS_18h_Male_Golem_EmoteResponse_02, VO_BOTA_BOSS_18h_Male_Golem_Idle_01, VO_BOTA_BOSS_18h_Male_Golem_Idle_02, VO_BOTA_BOSS_18h_Male_Golem_Idle_03, VO_BOTA_BOSS_18h_Male_Golem_Idle_05, VO_BOTA_BOSS_18h_Male_Golem_Idle_06, VO_BOTA_BOSS_18h_Male_Golem_Idle_07, VO_BOTA_BOSS_18h_Male_Golem_Idle_09,
			VO_BOTA_BOSS_18h_Male_Golem_Restart_01, VO_BOTA_BOSS_18h_Male_Golem_Restart_02, VO_BOTA_BOSS_18h_Male_Golem_Restart_03, VO_BOTA_BOSS_18h_Male_Golem_Restart_04, VO_BOTA_BOSS_18h_Male_Golem_Restart_06, VO_BOTA_BOSS_18h_Male_Golem_Return_01, VO_BOTA_BOSS_18h_Male_Golem_Puzzle_01, VO_BOTA_BOSS_18h_Male_Golem_Puzzle_02, VO_BOTA_BOSS_18h_Male_Golem_Puzzle_03, VO_BOTA_BOSS_18h_Male_Golem_Puzzle_04,
			VO_BOTA_BOSS_18h_Male_Golem_Intro_02, VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing4_Complete_01
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
		BOTA_MissionEntity.s_introLine = VO_BOTA_BOSS_18h_Male_Golem_Intro_02;
		BOTA_MissionEntity.s_returnLine = VO_BOTA_BOSS_18h_Male_Golem_Return_01;
		s_victoryLine_1 = VO_BOTA_BOSS_18h_Male_Golem_Puzzle_01;
		s_victoryLine_2 = VO_BOTA_BOSS_18h_Male_Golem_Puzzle_02;
		s_victoryLine_3 = null;
		s_victoryLine_4 = VO_BOTA_BOSS_18h_Male_Golem_Puzzle_03;
		s_victoryLine_5 = VO_BOTA_BOSS_18h_Male_Golem_Puzzle_04;
		s_victoryLine_6 = null;
		s_victoryLine_7 = null;
		s_victoryLine_8 = null;
		s_victoryLine_9 = null;
		s_emoteLines = new List<string> { VO_BOTA_BOSS_18h_Male_Golem_EmoteResponse_01, VO_BOTA_BOSS_18h_Male_Golem_EmoteResponse_02 };
		s_idleLines = new List<string> { VO_BOTA_BOSS_18h_Male_Golem_Idle_01, VO_BOTA_BOSS_18h_Male_Golem_Idle_02, VO_BOTA_BOSS_18h_Male_Golem_Idle_03, VO_BOTA_BOSS_18h_Male_Golem_Idle_05, VO_BOTA_BOSS_18h_Male_Golem_Idle_06, VO_BOTA_BOSS_18h_Male_Golem_Idle_07, VO_BOTA_BOSS_18h_Male_Golem_Idle_09 };
		s_restartLines = new List<string> { VO_BOTA_BOSS_18h_Male_Golem_Restart_01, VO_BOTA_BOSS_18h_Male_Golem_Restart_02, VO_BOTA_BOSS_18h_Male_Golem_Restart_03, VO_BOTA_BOSS_18h_Male_Golem_Restart_04, VO_BOTA_BOSS_18h_Male_Golem_Restart_06 };
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

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(2f);
			yield return PlayClosingLine("DrBoom_Banner_Quote.prefab:ff8653a27a00c464ea5552e3c6b6dc5c", VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing4_Complete_01);
		}
	}
}
