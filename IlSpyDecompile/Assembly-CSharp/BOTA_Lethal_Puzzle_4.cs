using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOTA_Lethal_Puzzle_4 : BOTA_MissionEntity
{
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Complete_01 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Complete_01.prefab:12d1fca9662768a40b0f8d3b088c2865");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_01.prefab:9cff3eedf14ce5946979a7d15d103f21");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_02.prefab:5a51491774815bb40950ed12f915a127");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_03.prefab:dbaff8ad97f99d04cb5f4ac510ef9c1f");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Idle_01 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Idle_01.prefab:86fca2830272fe649be137a1e3e284bb");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Idle_02 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Idle_02.prefab:bc96a1a2437d4974587eeebf2906866c");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Idle_03 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Idle_03.prefab:277e6570ebc8d504abb65b008b44f7b6");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Idle_04 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Idle_04.prefab:68cd898569fcefb41aa7f3ced593b888");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Idle_06 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Idle_06.prefab:3cb0c90090550604d92f94194282f701");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Idle_07 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Idle_07.prefab:a02f96ec96c83ff46aba2c081160924c");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Idle_08 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Idle_08.prefab:36a855ef080019a43b917994ef4d72c6");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Intro_01 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Intro_01.prefab:4ce422ee10a2f6140a4894d02a44b6fa");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Restart_01 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Restart_01.prefab:a9159ffa8f5140241b8de7401d4eabc9");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Restart_02 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Restart_02.prefab:8257e81f91c42eb449b5f3d10e2370a6");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Restart_03 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Restart_03.prefab:77f2961c7cf36d6489e8df15c188cfda");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Restart_04 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Restart_04.prefab:db537736afd07ad44b2f099317737a46");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Restart_05 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Restart_05.prefab:3a4521c2c165e494c818a8c6878f6f78");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Restart_06 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Restart_06.prefab:242e8aaac384ecf418b3d864166ab0c7");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Return_01 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Return_01.prefab:223beb64577c2514eb4fd5e238c3f114");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_01.prefab:675c01f7b882dfa4fbffaf36b28d1736");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_02.prefab:8445ce14f00193a4c808b0bbc38550fd");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_03.prefab:977f48dbc23a3c445a50ef8f40b2ddee");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_04 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_04.prefab:7bf5c3a8c493d214d99c220228fa2d50");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_06 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_06.prefab:612076d12b7dfc8468ef99d3b0d0e3c0");

	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_07 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_07.prefab:b3c28276cc875e345ae48352a7e0dd55");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing1_Complete_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing1_Complete_01.prefab:b21dd5ba0d46a704db275efac82c7b67");

	private string COMPLETE_LINE = VO_BOTA_BOSS_13h_Female_Elemental_Complete_01;

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_BOTA_BOSS_13h_Female_Elemental_Complete_01, VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_01, VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_02, VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_03, VO_BOTA_BOSS_13h_Female_Elemental_Idle_01, VO_BOTA_BOSS_13h_Female_Elemental_Idle_02, VO_BOTA_BOSS_13h_Female_Elemental_Idle_03, VO_BOTA_BOSS_13h_Female_Elemental_Idle_04, VO_BOTA_BOSS_13h_Female_Elemental_Idle_06, VO_BOTA_BOSS_13h_Female_Elemental_Idle_07,
			VO_BOTA_BOSS_13h_Female_Elemental_Idle_08, VO_BOTA_BOSS_13h_Female_Elemental_Intro_01, VO_BOTA_BOSS_13h_Female_Elemental_Restart_01, VO_BOTA_BOSS_13h_Female_Elemental_Restart_02, VO_BOTA_BOSS_13h_Female_Elemental_Restart_03, VO_BOTA_BOSS_13h_Female_Elemental_Restart_04, VO_BOTA_BOSS_13h_Female_Elemental_Restart_05, VO_BOTA_BOSS_13h_Female_Elemental_Restart_06, VO_BOTA_BOSS_13h_Female_Elemental_Return_01, VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_01,
			VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_02, VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_03, VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_04, VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_06, VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_07, VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing1_Complete_01
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
		BOTA_MissionEntity.s_introLine = VO_BOTA_BOSS_13h_Female_Elemental_Intro_01;
		BOTA_MissionEntity.s_returnLine = VO_BOTA_BOSS_13h_Female_Elemental_Return_01;
		s_victoryLine_1 = VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_03;
		s_victoryLine_2 = VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_02;
		s_victoryLine_3 = VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_01;
		s_victoryLine_4 = VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_04;
		s_victoryLine_5 = VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_07;
		s_victoryLine_6 = VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_06;
		s_victoryLine_7 = null;
		s_victoryLine_8 = null;
		s_victoryLine_9 = null;
		s_emoteLines = new List<string> { VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_01, VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_02, VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_03 };
		s_idleLines = new List<string> { VO_BOTA_BOSS_13h_Female_Elemental_Idle_01, VO_BOTA_BOSS_13h_Female_Elemental_Idle_02, VO_BOTA_BOSS_13h_Female_Elemental_Idle_03, VO_BOTA_BOSS_13h_Female_Elemental_Idle_04, VO_BOTA_BOSS_13h_Female_Elemental_Idle_06, VO_BOTA_BOSS_13h_Female_Elemental_Idle_07, VO_BOTA_BOSS_13h_Female_Elemental_Idle_08 };
		s_restartLines = new List<string> { VO_BOTA_BOSS_13h_Female_Elemental_Restart_01, VO_BOTA_BOSS_13h_Female_Elemental_Restart_02, VO_BOTA_BOSS_13h_Female_Elemental_Restart_03, VO_BOTA_BOSS_13h_Female_Elemental_Restart_04, VO_BOTA_BOSS_13h_Female_Elemental_Restart_05, VO_BOTA_BOSS_13h_Female_Elemental_Restart_06 };
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(2f);
			yield return PlayClosingLine("DrBoom_Banner_Quote.prefab:ff8653a27a00c464ea5552e3c6b6dc5c", VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing1_Complete_01);
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
