using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000419 RID: 1049
public class BOTA_Mirror_Puzzle_2 : BOTA_MissionEntity
{
	// Token: 0x0600397C RID: 14716 RVA: 0x00123708 File Offset: 0x00121908
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_BOTA_BOSS_03h_Female_Human_Intro_01.prefab:33cdf46c72a078449aa2ca87240b6c11",
			"VO_BOTA_BOSS_03h_Female_Human_Return_01.prefab:5b16dbfce90be3343bf838c1489adea5",
			"VO_BOTA_BOSS_03h_Female_Human_Puzzle_02.prefab:64c563b32b89fb247a4eef455d1083dd",
			"VO_BOTA_BOSS_03h_Female_Human_Puzzle_03.prefab:327c49e8477da8f4d95603e906a559f8",
			"VO_BOTA_BOSS_03h_Female_Human_Puzzle_05.prefab:8db5d59e5acdba842b2c54ad42444476",
			"VO_BOTA_BOSS_03h_Female_Human_Restart_01.prefab:34e61395faa19e842b0f08997e11edda",
			"VO_BOTA_BOSS_03h_Female_Human_Restart_02.prefab:84d45837a9dddd546823fc0784bf2cac",
			"VO_BOTA_BOSS_03h_Female_Human_Idle_01.prefab:cf3e6850ce335944f936826e2fbda741",
			"VO_BOTA_BOSS_03h_Female_Human_Idle_02.prefab:ae015b0701360fc49bbeefd9aed3c0d2",
			"VO_BOTA_BOSS_03h_Female_Human_Idle_03.prefab:a0bf9d5704490d34c82815174c3d51b2",
			"VO_BOTA_BOSS_03h_Female_Human_Idle_04.prefab:89c1a033555f06147b12bf7578c8069d",
			"VO_BOTA_BOSS_03h_Female_Human_Idle_05.prefab:8ca3562d5e0ba3f4da3d7f60b15a0eca",
			"VO_BOTA_BOSS_03h_Female_Human_Idle_06.prefab:2c07b51754b7ec64e841ca79e318ae25",
			"VO_BOTA_BOSS_03h_Female_Human_Idle_07.prefab:0ede560e0102db34cb5f8c9c074e1ef1",
			"VO_BOTA_BOSS_03h_Female_Human_Idle_08.prefab:b0d40f51c4fd0c248bb6df54f1063800",
			"VO_BOTA_BOSS_03h_Female_Human_EmoteResponse_01.prefab:5b06ed5a599501d45a948c662f8c83d3",
			"VO_BOTA_BOSS_03h_Female_Human_EmoteResponse_03.prefab:e9b2446a01059c54d95bc01bff887bf4",
			"VO_BOTA_BOSS_03h_Female_Human_EmoteResponse_04.prefab:486fd41ed86dba64db788e034ab17f9e",
			"VO_BOTA_BOSS_03h_Female_Human_Complete_01.prefab:a8948c42011bddb4f864e86cae932be7",
			BOTA_Mirror_Puzzle_2.VO_BOTA_BOSS_03h_Female_Human_Whoops_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600397D RID: 14717 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x0600397E RID: 14718 RVA: 0x00123844 File Offset: 0x00121A44
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = "VO_BOTA_BOSS_03h_Female_Human_Intro_01.prefab:33cdf46c72a078449aa2ca87240b6c11";
		BOTA_MissionEntity.s_returnLine = "VO_BOTA_BOSS_03h_Female_Human_Return_01.prefab:5b16dbfce90be3343bf838c1489adea5";
		this.s_victoryLine_1 = null;
		this.s_victoryLine_2 = "VO_BOTA_BOSS_03h_Female_Human_Puzzle_02.prefab:64c563b32b89fb247a4eef455d1083dd";
		this.s_victoryLine_3 = null;
		this.s_victoryLine_4 = "VO_BOTA_BOSS_03h_Female_Human_Puzzle_03.prefab:327c49e8477da8f4d95603e906a559f8";
		this.s_victoryLine_5 = null;
		this.s_victoryLine_6 = "VO_BOTA_BOSS_03h_Female_Human_Puzzle_05.prefab:8db5d59e5acdba842b2c54ad42444476";
		this.s_victoryLine_7 = null;
		this.s_victoryLine_8 = null;
		this.s_victoryLine_9 = null;
		this.s_emoteLines = new List<string>
		{
			"VO_BOTA_BOSS_03h_Female_Human_EmoteResponse_01.prefab:5b06ed5a599501d45a948c662f8c83d3",
			"VO_BOTA_BOSS_03h_Female_Human_EmoteResponse_03.prefab:e9b2446a01059c54d95bc01bff887bf4",
			"VO_BOTA_BOSS_03h_Female_Human_EmoteResponse_04.prefab:486fd41ed86dba64db788e034ab17f9e"
		};
		this.s_idleLines = new List<string>
		{
			"VO_BOTA_BOSS_03h_Female_Human_Idle_01.prefab:cf3e6850ce335944f936826e2fbda741",
			"VO_BOTA_BOSS_03h_Female_Human_Idle_02.prefab:ae015b0701360fc49bbeefd9aed3c0d2",
			"VO_BOTA_BOSS_03h_Female_Human_Idle_03.prefab:a0bf9d5704490d34c82815174c3d51b2",
			"VO_BOTA_BOSS_03h_Female_Human_Idle_04.prefab:89c1a033555f06147b12bf7578c8069d",
			"VO_BOTA_BOSS_03h_Female_Human_Idle_05.prefab:8ca3562d5e0ba3f4da3d7f60b15a0eca",
			"VO_BOTA_BOSS_03h_Female_Human_Idle_06.prefab:2c07b51754b7ec64e841ca79e318ae25",
			"VO_BOTA_BOSS_03h_Female_Human_Idle_07.prefab:0ede560e0102db34cb5f8c9c074e1ef1",
			"VO_BOTA_BOSS_03h_Female_Human_Idle_08.prefab:b0d40f51c4fd0c248bb6df54f1063800"
		};
		this.s_restartLines = new List<string>
		{
			"VO_BOTA_BOSS_03h_Female_Human_Restart_01.prefab:34e61395faa19e842b0f08997e11edda",
			"VO_BOTA_BOSS_03h_Female_Human_Restart_02.prefab:84d45837a9dddd546823fc0784bf2cac",
			BOTA_Mirror_Puzzle_2.VO_BOTA_BOSS_03h_Female_Human_Whoops_01
		};
	}

	// Token: 0x0600397F RID: 14719 RVA: 0x00123976 File Offset: 0x00121B76
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent == 99)
		{
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(true);
			yield return base.PlayBossLine(enemyActor, this.COMPLETE_LINE, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x04001F39 RID: 7993
	private static readonly AssetReference VO_BOTA_BOSS_03h_Female_Human_Whoops_01 = new AssetReference("VO_BOTA_BOSS_03h_Female_Human_Whoops_01.prefab:8d59edbb4fc892c40a0363a531acd1ab");

	// Token: 0x04001F3A RID: 7994
	private string COMPLETE_LINE = "VO_BOTA_BOSS_03h_Female_Human_Complete_01.prefab:a8948c42011bddb4f864e86cae932be7";
}
