using System.Collections;
using System.Collections.Generic;

public class BOTA_Lethal_Puzzle_2 : BOTA_MissionEntity
{
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_01.prefab:01668d8ad76337c4ea6ebf8f04946270");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_02.prefab:11cf744c7e2a85d4ab507a965477876e");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_03.prefab:3d73adb89efc1c346aec2b9bb3e283ee");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Idle_01 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Idle_01.prefab:20f0ba7d7cc29e04eb6f8f0187a1c398");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Idle_02 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Idle_02.prefab:3f30ed14b89a4e840b134cda792de80e");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Idle_03 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Idle_03.prefab:5f20588d6ef530442850bb69e58718d0");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Idle_04 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Idle_04.prefab:4b9ac5667759f7f499469c70a1aa1ff6");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Idle_05 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Idle_05.prefab:b95b1471ebad8944ea1ce83a392960a3");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Idle_06 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Idle_06.prefab:82dd5390618467542b4ac7139393d1db");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Idle_07 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Idle_07.prefab:482dfb36ff2161848b23e7a2e56d28a6");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Intro_01 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Intro_01.prefab:84bbbd933620e404a93764ccb790f527");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Restart_01 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Restart_01.prefab:275eec4051c77464b9a6f605fed0cb60");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Restart_02 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Restart_02.prefab:26d94195ce8669045a351488dd83ac53");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Restart_03 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Restart_03.prefab:65a355a7bb3a404489559f80ba32cf68");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Restart_04 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Restart_04.prefab:c0dcc936e8095e1408637855c433cb3d");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Restart_05 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Restart_05.prefab:a68c95018db31f94694fa59876b231db");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Restart_06 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Restart_06.prefab:44cc768909ef63c4fb33864cab80ecb8");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Return_01 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Return_01.prefab:dfa5853bd9117ed4bb08726ddbfd8aa8");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Puzzle_01.prefab:2652e8f87f7ff904cae02c6588c10ffd");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Puzzle_02.prefab:69e16d58e05b2c749b6a04065f0c9db6");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Puzzle_04 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Puzzle_04.prefab:4a785dd86a3e0b340aecbcc8c2a0739f");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Lethal_01 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Lethal_01.prefab:c67181d3302889a4e9294ed5f1d68bff");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Lethal_02 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Lethal_02.prefab:4850d07227229d9408911a6b82ac5719");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Lethal_03 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Lethal_03.prefab:7d0e11c1bebebb9448c610d656e0f4da");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Lethal_04 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Lethal_04.prefab:69fa5e9af397c0b4a865cf2ec5fb59cc");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Lethal_05 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Lethal_05.prefab:4d1fbfe5571c53d4fb2fba3d4ed8418f");

	private string COMPLETE_LINE = VO_BOTA_BOSS_14h_Female_Undead_Lethal_04;

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_01, VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_02, VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_03, VO_BOTA_BOSS_14h_Female_Undead_Idle_01, VO_BOTA_BOSS_14h_Female_Undead_Idle_02, VO_BOTA_BOSS_14h_Female_Undead_Idle_03, VO_BOTA_BOSS_14h_Female_Undead_Idle_04, VO_BOTA_BOSS_14h_Female_Undead_Idle_05, VO_BOTA_BOSS_14h_Female_Undead_Idle_06, VO_BOTA_BOSS_14h_Female_Undead_Idle_07,
			VO_BOTA_BOSS_14h_Female_Undead_Intro_01, VO_BOTA_BOSS_14h_Female_Undead_Restart_01, VO_BOTA_BOSS_14h_Female_Undead_Restart_02, VO_BOTA_BOSS_14h_Female_Undead_Restart_03, VO_BOTA_BOSS_14h_Female_Undead_Restart_04, VO_BOTA_BOSS_14h_Female_Undead_Restart_05, VO_BOTA_BOSS_14h_Female_Undead_Restart_06, VO_BOTA_BOSS_14h_Female_Undead_Return_01, VO_BOTA_BOSS_14h_Female_Undead_Puzzle_01, VO_BOTA_BOSS_14h_Female_Undead_Puzzle_02,
			VO_BOTA_BOSS_14h_Female_Undead_Puzzle_04, VO_BOTA_BOSS_14h_Female_Undead_Lethal_01, VO_BOTA_BOSS_14h_Female_Undead_Lethal_02, VO_BOTA_BOSS_14h_Female_Undead_Lethal_03, VO_BOTA_BOSS_14h_Female_Undead_Lethal_04, VO_BOTA_BOSS_14h_Female_Undead_Lethal_05
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
		BOTA_MissionEntity.s_introLine = VO_BOTA_BOSS_14h_Female_Undead_Intro_01;
		BOTA_MissionEntity.s_returnLine = VO_BOTA_BOSS_14h_Female_Undead_Return_01;
		s_victoryLine_1 = null;
		s_victoryLine_2 = VO_BOTA_BOSS_14h_Female_Undead_Puzzle_01;
		s_victoryLine_3 = VO_BOTA_BOSS_14h_Female_Undead_Puzzle_04;
		s_victoryLine_4 = null;
		s_victoryLine_5 = VO_BOTA_BOSS_14h_Female_Undead_Puzzle_02;
		s_victoryLine_6 = null;
		s_victoryLine_7 = null;
		s_victoryLine_8 = null;
		s_victoryLine_9 = null;
		s_emoteLines = new List<string> { VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_01, VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_02, VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_03 };
		s_idleLines = new List<string> { VO_BOTA_BOSS_14h_Female_Undead_Idle_01, VO_BOTA_BOSS_14h_Female_Undead_Idle_02, VO_BOTA_BOSS_14h_Female_Undead_Idle_03, VO_BOTA_BOSS_14h_Female_Undead_Idle_04, VO_BOTA_BOSS_14h_Female_Undead_Idle_05, VO_BOTA_BOSS_14h_Female_Undead_Idle_06, VO_BOTA_BOSS_14h_Female_Undead_Idle_07 };
		s_restartLines = new List<string> { VO_BOTA_BOSS_14h_Female_Undead_Restart_01, VO_BOTA_BOSS_14h_Female_Undead_Restart_02, VO_BOTA_BOSS_14h_Female_Undead_Restart_03, VO_BOTA_BOSS_14h_Female_Undead_Restart_04, VO_BOTA_BOSS_14h_Female_Undead_Restart_05, VO_BOTA_BOSS_14h_Female_Undead_Restart_06 };
		s_lethalCompleteLines = new List<string> { VO_BOTA_BOSS_14h_Female_Undead_Lethal_01, VO_BOTA_BOSS_14h_Female_Undead_Lethal_02, VO_BOTA_BOSS_14h_Female_Undead_Lethal_03, VO_BOTA_BOSS_14h_Female_Undead_Lethal_05 };
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (missionEvent)
		{
		case 99:
			while (m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(enemyActor, COMPLETE_LINE);
			GameState.Get().SetBusy(busy: false);
			break;
		case 777:
		{
			string lethalCompleteLine = GetLethalCompleteLine();
			if (lethalCompleteLine != null)
			{
				yield return PlayBossLine(enemyActor, lethalCompleteLine);
			}
			break;
		}
		}
	}
}
