using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOTA_Mirror_Puzzle_4 : BOTA_MissionEntity
{
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Complete_03 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Complete_03.prefab:56bf73cc367105042977eede5c8ed438");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_EmoteResponse_01.prefab:2ae64c5e79e893440b7ba443c31d3019");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_EmoteResponse_02.prefab:1f00e297ef6614944981c4515f8aadbf");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Idle_03 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Idle_03.prefab:b2910bd1ad25de74eab928ca2f338521");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Idle_04 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Idle_04.prefab:36b225a82667f794495572f88645a113");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Idle_05 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Idle_05.prefab:a330ff7d29a40ed48a780f8886c97a1b");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Idle_07 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Idle_07.prefab:f18d1461a03d59c4aa3947d145c809fb");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Idle_08 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Idle_08.prefab:b20962b3f0c5c0949ac20cadda48acdd");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Idle_09 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Idle_09.prefab:7427c8a1e32320645a2f2788ea5dbe87");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Idle_10 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Idle_10.prefab:28db4963e1604284e8b854495763786b");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Idle_11 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Idle_11.prefab:2931d439b21f5564291d700a24a03253");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Intro_01 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Intro_01.prefab:47b3a84a44cc5424589469c0a42333be");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_01.prefab:825243f99bb5ad146a9293a2ec7df469");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_02.prefab:6df967b5a36b4d7489c9428d9c1f2f1f");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_03.prefab:25ffc8f599b1c4e469e174e53d322957");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Restart_01 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Restart_01.prefab:1274e3e0226dc64459086143408289d6");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Restart_02 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Restart_02.prefab:aa1fdcfdc0765374a8d103fae23a33a2");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Restart_03 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Restart_03.prefab:9349911a79338b146b065ece1a5f6b03");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Restart_04 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Restart_04.prefab:6844f0a28ea08a34380ec1f3e9076c6d");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Restart_06 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Restart_06.prefab:31fce4636803bc9468115bd58d183161");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Return_01 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Return_01.prefab:150f0142e9b329847b27fb356ec1ce9e");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_02 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_02.prefab:9054f87af94c30648a5a3a66abf36224");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_03 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_03.prefab:d92f0c99c70071b469f0640b050dc3e9");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_04 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_04.prefab:b067a6a440b23c248998e201c6bd6d5e");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_05 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_05.prefab:1ddc8aa1f85ad594fa6d7d1aa2a1c799");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing2_Complete_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing2_Complete_01.prefab:c3965288127268b418a87a211694b606");

	private string COMPLETE_LINE = VO_BOTA_BOSS_05h_Male_Ethereal_Complete_03;

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_BOTA_BOSS_05h_Male_Ethereal_Complete_03, VO_BOTA_BOSS_05h_Male_Ethereal_EmoteResponse_01, VO_BOTA_BOSS_05h_Male_Ethereal_EmoteResponse_02, VO_BOTA_BOSS_05h_Male_Ethereal_Idle_03, VO_BOTA_BOSS_05h_Male_Ethereal_Idle_04, VO_BOTA_BOSS_05h_Male_Ethereal_Idle_05, VO_BOTA_BOSS_05h_Male_Ethereal_Idle_07, VO_BOTA_BOSS_05h_Male_Ethereal_Idle_08, VO_BOTA_BOSS_05h_Male_Ethereal_Idle_09, VO_BOTA_BOSS_05h_Male_Ethereal_Idle_10,
			VO_BOTA_BOSS_05h_Male_Ethereal_Idle_11, VO_BOTA_BOSS_05h_Male_Ethereal_Intro_01, VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_01, VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_02, VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_03, VO_BOTA_BOSS_05h_Male_Ethereal_Restart_01, VO_BOTA_BOSS_05h_Male_Ethereal_Restart_02, VO_BOTA_BOSS_05h_Male_Ethereal_Restart_03, VO_BOTA_BOSS_05h_Male_Ethereal_Restart_04, VO_BOTA_BOSS_05h_Male_Ethereal_Restart_06,
			VO_BOTA_BOSS_05h_Male_Ethereal_Return_01, VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_02, VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_03, VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_04, VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_05, VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing2_Complete_01
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
		BOTA_MissionEntity.s_introLine = VO_BOTA_BOSS_05h_Male_Ethereal_Intro_01;
		BOTA_MissionEntity.s_returnLine = VO_BOTA_BOSS_05h_Male_Ethereal_Return_01;
		s_victoryLine_1 = null;
		s_victoryLine_2 = VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_01;
		s_victoryLine_3 = VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_03;
		s_victoryLine_4 = VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_02;
		s_victoryLine_5 = null;
		s_victoryLine_6 = null;
		s_victoryLine_7 = null;
		s_victoryLine_8 = null;
		s_victoryLine_9 = null;
		s_emoteLines = new List<string> { VO_BOTA_BOSS_05h_Male_Ethereal_EmoteResponse_01, VO_BOTA_BOSS_05h_Male_Ethereal_EmoteResponse_02 };
		s_idleLines = new List<string> { VO_BOTA_BOSS_05h_Male_Ethereal_Idle_03, VO_BOTA_BOSS_05h_Male_Ethereal_Idle_04, VO_BOTA_BOSS_05h_Male_Ethereal_Idle_05, VO_BOTA_BOSS_05h_Male_Ethereal_Idle_07, VO_BOTA_BOSS_05h_Male_Ethereal_Idle_08, VO_BOTA_BOSS_05h_Male_Ethereal_Idle_09, VO_BOTA_BOSS_05h_Male_Ethereal_Idle_10, VO_BOTA_BOSS_05h_Male_Ethereal_Idle_11 };
		s_restartLines = new List<string> { VO_BOTA_BOSS_05h_Male_Ethereal_Restart_01, VO_BOTA_BOSS_05h_Male_Ethereal_Restart_02, VO_BOTA_BOSS_05h_Male_Ethereal_Restart_03, VO_BOTA_BOSS_05h_Male_Ethereal_Restart_04, VO_BOTA_BOSS_05h_Male_Ethereal_Restart_06, VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_02, VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_03, VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_04, VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_05 };
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(2f);
			yield return PlayClosingLine("DrBoom_Banner_Quote.prefab:ff8653a27a00c464ea5552e3c6b6dc5c", VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing2_Complete_01);
		}
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
