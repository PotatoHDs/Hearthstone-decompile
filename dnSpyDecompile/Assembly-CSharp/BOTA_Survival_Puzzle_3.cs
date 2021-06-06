using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000420 RID: 1056
public class BOTA_Survival_Puzzle_3 : BOTA_MissionEntity
{
	// Token: 0x060039CD RID: 14797 RVA: 0x00125D98 File Offset: 0x00123F98
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Complete_01,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_01,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_02,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_03,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Idle_01,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Idle_02,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Idle_03,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Idle_04,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Idle_05,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Idle_06,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Idle_07,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Intro_01,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Restart_02,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Restart_03,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Restart_04,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Restart_05,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Restart_06,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Restart_07,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Restart_08,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Return_01,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Puzzle_01,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Puzzle_03,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Puzzle_04
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060039CE RID: 14798 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x060039CF RID: 14799 RVA: 0x00125F60 File Offset: 0x00124160
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Intro_01;
		BOTA_MissionEntity.s_returnLine = BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Return_01;
		this.s_victoryLine_1 = null;
		this.s_victoryLine_2 = BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Puzzle_04;
		this.s_victoryLine_3 = null;
		this.s_victoryLine_4 = BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Puzzle_01;
		this.s_victoryLine_5 = null;
		this.s_victoryLine_6 = BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Puzzle_03;
		this.s_victoryLine_7 = null;
		this.s_victoryLine_8 = null;
		this.s_victoryLine_9 = null;
		this.s_emoteLines = new List<string>
		{
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_01,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_02,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_03
		};
		this.s_idleLines = new List<string>
		{
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Idle_01,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Idle_02,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Idle_03,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Idle_04,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Idle_05,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Idle_06,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Idle_07
		};
		this.s_restartLines = new List<string>
		{
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Restart_02,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Restart_03,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Restart_04,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Restart_05,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Restart_06,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Restart_07,
			BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Restart_08
		};
	}

	// Token: 0x060039D0 RID: 14800 RVA: 0x0012611C File Offset: 0x0012431C
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

	// Token: 0x04001FE2 RID: 8162
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Complete_01 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Complete_01.prefab:3c812cfba5f46754ea44b18080955ab9");

	// Token: 0x04001FE3 RID: 8163
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_01.prefab:7e5a5494219d7ed419a5991beb1495db");

	// Token: 0x04001FE4 RID: 8164
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_02.prefab:b551c0831c32f45408039a9cf3750d95");

	// Token: 0x04001FE5 RID: 8165
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_EmoteResponse_03.prefab:939582ad143baf346a211bc9c30558df");

	// Token: 0x04001FE6 RID: 8166
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Idle_01 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Idle_01.prefab:d049be4b509d5fe409293c8276c5eeee");

	// Token: 0x04001FE7 RID: 8167
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Idle_02 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Idle_02.prefab:a6a04b5b5c9c00243b7bd9bf39e38eb5");

	// Token: 0x04001FE8 RID: 8168
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Idle_03 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Idle_03.prefab:a3959fb59c2d66e48bbc6767e5cfa00e");

	// Token: 0x04001FE9 RID: 8169
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Idle_04 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Idle_04.prefab:66d3c90e8a2bd5b4f89db63fc1d6fa98");

	// Token: 0x04001FEA RID: 8170
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Idle_05 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Idle_05.prefab:6451a5a90b0b7ee4bbffb87acc5fb74c");

	// Token: 0x04001FEB RID: 8171
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Idle_06 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Idle_06.prefab:96efb8f844f1eca41bb80c1a410d14a8");

	// Token: 0x04001FEC RID: 8172
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Idle_07 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Idle_07.prefab:30dc925ae684ae749bbba2c1fa47b74e");

	// Token: 0x04001FED RID: 8173
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Intro_01 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Intro_01.prefab:e05253ceaaf2e1b488c827f934f9bc65");

	// Token: 0x04001FEE RID: 8174
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Restart_02 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Restart_02.prefab:0d15dc6c81a9f9a4c905491033ba5e89");

	// Token: 0x04001FEF RID: 8175
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Restart_03 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Restart_03.prefab:4417be44c09d494489c32d2661af130a");

	// Token: 0x04001FF0 RID: 8176
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Restart_04 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Restart_04.prefab:8c97544373965914da7510603bb48223");

	// Token: 0x04001FF1 RID: 8177
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Restart_05 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Restart_05.prefab:1d7399b36c025ea4b86fb995adbc9df4");

	// Token: 0x04001FF2 RID: 8178
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Restart_06 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Restart_06.prefab:d1e94490c971c0b41ac6b837c0e9ef61");

	// Token: 0x04001FF3 RID: 8179
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Restart_07 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Restart_07.prefab:2b2f3ccb00bb5d3429c7b080ab1e790e");

	// Token: 0x04001FF4 RID: 8180
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Restart_08 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Restart_08.prefab:3f9da67399f2ad449b2e06e680bec62c");

	// Token: 0x04001FF5 RID: 8181
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Return_01 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Return_01.prefab:50d8e1b2fe33df54b9e89526f6502f03");

	// Token: 0x04001FF6 RID: 8182
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Puzzle_01.prefab:77323f7403d90a04fbe85068584bdf26");

	// Token: 0x04001FF7 RID: 8183
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Puzzle_03.prefab:33e97cb224b60ca4abd3ba36d43080e0");

	// Token: 0x04001FF8 RID: 8184
	private static readonly AssetReference VO_BOTA_BOSS_17h_Male_Mech_Puzzle_04 = new AssetReference("VO_BOTA_BOSS_17h_Male_Mech_Puzzle_04.prefab:ca3c2f60b4dda074b8dc96e2624eb704");

	// Token: 0x04001FF9 RID: 8185
	private string COMPLETE_LINE = BOTA_Survival_Puzzle_3.VO_BOTA_BOSS_17h_Male_Mech_Complete_01;
}
