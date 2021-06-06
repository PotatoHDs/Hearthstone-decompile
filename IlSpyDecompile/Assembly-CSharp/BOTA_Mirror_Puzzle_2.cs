using System.Collections;
using System.Collections.Generic;

public class BOTA_Mirror_Puzzle_2 : BOTA_MissionEntity
{
	private static readonly AssetReference VO_BOTA_BOSS_03h_Female_Human_Whoops_01 = new AssetReference("VO_BOTA_BOSS_03h_Female_Human_Whoops_01.prefab:8d59edbb4fc892c40a0363a531acd1ab");

	private string COMPLETE_LINE = "VO_BOTA_BOSS_03h_Female_Human_Complete_01.prefab:a8948c42011bddb4f864e86cae932be7";

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			"VO_BOTA_BOSS_03h_Female_Human_Intro_01.prefab:33cdf46c72a078449aa2ca87240b6c11", "VO_BOTA_BOSS_03h_Female_Human_Return_01.prefab:5b16dbfce90be3343bf838c1489adea5", "VO_BOTA_BOSS_03h_Female_Human_Puzzle_02.prefab:64c563b32b89fb247a4eef455d1083dd", "VO_BOTA_BOSS_03h_Female_Human_Puzzle_03.prefab:327c49e8477da8f4d95603e906a559f8", "VO_BOTA_BOSS_03h_Female_Human_Puzzle_05.prefab:8db5d59e5acdba842b2c54ad42444476", "VO_BOTA_BOSS_03h_Female_Human_Restart_01.prefab:34e61395faa19e842b0f08997e11edda", "VO_BOTA_BOSS_03h_Female_Human_Restart_02.prefab:84d45837a9dddd546823fc0784bf2cac", "VO_BOTA_BOSS_03h_Female_Human_Idle_01.prefab:cf3e6850ce335944f936826e2fbda741", "VO_BOTA_BOSS_03h_Female_Human_Idle_02.prefab:ae015b0701360fc49bbeefd9aed3c0d2", "VO_BOTA_BOSS_03h_Female_Human_Idle_03.prefab:a0bf9d5704490d34c82815174c3d51b2",
			"VO_BOTA_BOSS_03h_Female_Human_Idle_04.prefab:89c1a033555f06147b12bf7578c8069d", "VO_BOTA_BOSS_03h_Female_Human_Idle_05.prefab:8ca3562d5e0ba3f4da3d7f60b15a0eca", "VO_BOTA_BOSS_03h_Female_Human_Idle_06.prefab:2c07b51754b7ec64e841ca79e318ae25", "VO_BOTA_BOSS_03h_Female_Human_Idle_07.prefab:0ede560e0102db34cb5f8c9c074e1ef1", "VO_BOTA_BOSS_03h_Female_Human_Idle_08.prefab:b0d40f51c4fd0c248bb6df54f1063800", "VO_BOTA_BOSS_03h_Female_Human_EmoteResponse_01.prefab:5b06ed5a599501d45a948c662f8c83d3", "VO_BOTA_BOSS_03h_Female_Human_EmoteResponse_03.prefab:e9b2446a01059c54d95bc01bff887bf4", "VO_BOTA_BOSS_03h_Female_Human_EmoteResponse_04.prefab:486fd41ed86dba64db788e034ab17f9e", "VO_BOTA_BOSS_03h_Female_Human_Complete_01.prefab:a8948c42011bddb4f864e86cae932be7", VO_BOTA_BOSS_03h_Female_Human_Whoops_01
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
		BOTA_MissionEntity.s_introLine = "VO_BOTA_BOSS_03h_Female_Human_Intro_01.prefab:33cdf46c72a078449aa2ca87240b6c11";
		BOTA_MissionEntity.s_returnLine = "VO_BOTA_BOSS_03h_Female_Human_Return_01.prefab:5b16dbfce90be3343bf838c1489adea5";
		s_victoryLine_1 = null;
		s_victoryLine_2 = "VO_BOTA_BOSS_03h_Female_Human_Puzzle_02.prefab:64c563b32b89fb247a4eef455d1083dd";
		s_victoryLine_3 = null;
		s_victoryLine_4 = "VO_BOTA_BOSS_03h_Female_Human_Puzzle_03.prefab:327c49e8477da8f4d95603e906a559f8";
		s_victoryLine_5 = null;
		s_victoryLine_6 = "VO_BOTA_BOSS_03h_Female_Human_Puzzle_05.prefab:8db5d59e5acdba842b2c54ad42444476";
		s_victoryLine_7 = null;
		s_victoryLine_8 = null;
		s_victoryLine_9 = null;
		s_emoteLines = new List<string> { "VO_BOTA_BOSS_03h_Female_Human_EmoteResponse_01.prefab:5b06ed5a599501d45a948c662f8c83d3", "VO_BOTA_BOSS_03h_Female_Human_EmoteResponse_03.prefab:e9b2446a01059c54d95bc01bff887bf4", "VO_BOTA_BOSS_03h_Female_Human_EmoteResponse_04.prefab:486fd41ed86dba64db788e034ab17f9e" };
		s_idleLines = new List<string> { "VO_BOTA_BOSS_03h_Female_Human_Idle_01.prefab:cf3e6850ce335944f936826e2fbda741", "VO_BOTA_BOSS_03h_Female_Human_Idle_02.prefab:ae015b0701360fc49bbeefd9aed3c0d2", "VO_BOTA_BOSS_03h_Female_Human_Idle_03.prefab:a0bf9d5704490d34c82815174c3d51b2", "VO_BOTA_BOSS_03h_Female_Human_Idle_04.prefab:89c1a033555f06147b12bf7578c8069d", "VO_BOTA_BOSS_03h_Female_Human_Idle_05.prefab:8ca3562d5e0ba3f4da3d7f60b15a0eca", "VO_BOTA_BOSS_03h_Female_Human_Idle_06.prefab:2c07b51754b7ec64e841ca79e318ae25", "VO_BOTA_BOSS_03h_Female_Human_Idle_07.prefab:0ede560e0102db34cb5f8c9c074e1ef1", "VO_BOTA_BOSS_03h_Female_Human_Idle_08.prefab:b0d40f51c4fd0c248bb6df54f1063800" };
		s_restartLines = new List<string> { "VO_BOTA_BOSS_03h_Female_Human_Restart_01.prefab:34e61395faa19e842b0f08997e11edda", "VO_BOTA_BOSS_03h_Female_Human_Restart_02.prefab:84d45837a9dddd546823fc0784bf2cac", VO_BOTA_BOSS_03h_Female_Human_Whoops_01 };
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
