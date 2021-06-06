using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200041F RID: 1055
public class BOTA_Survival_Puzzle_2 : BOTA_MissionEntity
{
	// Token: 0x060039C5 RID: 14789 RVA: 0x001256E4 File Offset: 0x001238E4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Complete_02,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_EmoteResponse_01,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_EmoteResponse_02,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_EmoteResponse_03,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Idle_01,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Idle_02,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Idle_03,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Idle_04,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Idle_05,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Idle_06,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Idle_07,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Idle_08,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Idle_09,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Intro_01,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_PuzzleA_01,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_PuzzleB_01,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Restart_01,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Restart_02,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Restart_03,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Restart_04,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Restart_05,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Restart_06,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Restart_07,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Return_01,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Stuck_01,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Stuck_02,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Stuck_03,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Stuck_04,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Puzzle_02
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060039C6 RID: 14790 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x060039C7 RID: 14791 RVA: 0x0012590C File Offset: 0x00123B0C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Intro_01;
		BOTA_MissionEntity.s_returnLine = BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Return_01;
		this.s_victoryLine_1 = BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_PuzzleA_01;
		this.s_victoryLine_2 = null;
		this.s_victoryLine_3 = null;
		this.s_victoryLine_4 = BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Puzzle_02;
		this.s_victoryLine_5 = null;
		this.s_victoryLine_6 = null;
		this.s_victoryLine_7 = null;
		this.s_victoryLine_8 = null;
		this.s_victoryLine_9 = null;
		this.s_emoteLines = new List<string>
		{
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_EmoteResponse_01,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_EmoteResponse_02,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_EmoteResponse_03
		};
		this.s_idleLines = new List<string>
		{
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Idle_01,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Idle_02,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Idle_03,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Idle_04,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Idle_05,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Idle_06,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Idle_07,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Idle_08,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Idle_09
		};
		this.s_restartLines = new List<string>
		{
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Restart_01,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Restart_02,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Restart_03,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Restart_04,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Restart_05,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Restart_06,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Restart_07,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Stuck_01,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Stuck_02,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Stuck_03,
			BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Stuck_04
		};
	}

	// Token: 0x060039C8 RID: 14792 RVA: 0x00125B1F File Offset: 0x00123D1F
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

	// Token: 0x060039C9 RID: 14793 RVA: 0x00125B35 File Offset: 0x00123D35
	public override IEnumerator OnTurnStartManagerFinishedWithTiming()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		int tag = GameState.Get().GetFriendlySidePlayer().GetSecretZone().GetPuzzleEntity().GetTag(GAME_TAG.PUZZLE_PROGRESS);
		string puzzleVictoryLine = this.GetPuzzleVictoryLine(tag);
		if (puzzleVictoryLine != null)
		{
			if (puzzleVictoryLine == BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_PuzzleA_01)
			{
				yield return base.PlayBossLine(enemyActor, BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_PuzzleA_01, 2.5f);
				yield return new WaitForSeconds(0.8f);
				yield return base.PlayBossLine(enemyActor, BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_PuzzleB_01, 2.5f);
			}
			else
			{
				yield return base.PlayBossLine(enemyActor, puzzleVictoryLine, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x060039CA RID: 14794 RVA: 0x00125B44 File Offset: 0x00123D44
	private string GetPuzzleVictoryLine(int puzzleProgress)
	{
		switch (puzzleProgress)
		{
		case 1:
			return this.s_victoryLine_1;
		case 2:
			return this.s_victoryLine_2;
		case 3:
			return this.s_victoryLine_3;
		case 4:
			return this.s_victoryLine_4;
		case 5:
			return this.s_victoryLine_5;
		case 6:
			return this.s_victoryLine_6;
		case 7:
			return this.s_victoryLine_7;
		case 8:
			return this.s_victoryLine_8;
		case 9:
			return this.s_victoryLine_9;
		default:
			return null;
		}
	}

	// Token: 0x04001FC4 RID: 8132
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Complete_02 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Complete_02.prefab:ec34127b832ca334492d81beabe51ea2");

	// Token: 0x04001FC5 RID: 8133
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_EmoteResponse_01.prefab:dec59b2a83e84de4db1c9e0ee1f7695e");

	// Token: 0x04001FC6 RID: 8134
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_EmoteResponse_02.prefab:2703ab76dde328848a945ee8ffbe728b");

	// Token: 0x04001FC7 RID: 8135
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_EmoteResponse_03.prefab:976e13e6db6991146bd6fec7c4f2ccbf");

	// Token: 0x04001FC8 RID: 8136
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Idle_01 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Idle_01.prefab:f9a403c5224a68c48b14d1699d1ae383");

	// Token: 0x04001FC9 RID: 8137
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Idle_02 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Idle_02.prefab:78ca9f547a00fd54e915185e3d701f2f");

	// Token: 0x04001FCA RID: 8138
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Idle_03 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Idle_03.prefab:c166b33c7c3ece44cad6ba861401eadf");

	// Token: 0x04001FCB RID: 8139
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Idle_04 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Idle_04.prefab:6d76a3355f6bc354faa43b8f4383a77c");

	// Token: 0x04001FCC RID: 8140
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Idle_05 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Idle_05.prefab:c2175b5010148bb469ae2cc69fa0b7e2");

	// Token: 0x04001FCD RID: 8141
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Idle_06 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Idle_06.prefab:846d065380be8d449b9c3348d92fa1ca");

	// Token: 0x04001FCE RID: 8142
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Idle_07 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Idle_07.prefab:e136e229ab37a504883c229a1298241f");

	// Token: 0x04001FCF RID: 8143
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Idle_08 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Idle_08.prefab:6a295cf17c928ff40880e5043ce268c1");

	// Token: 0x04001FD0 RID: 8144
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Idle_09 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Idle_09.prefab:5eb01b739b48dc844b39696f206a7544");

	// Token: 0x04001FD1 RID: 8145
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Intro_01 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Intro_01.prefab:735d02a0910b9c54fb72c16da7c2c644");

	// Token: 0x04001FD2 RID: 8146
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_PuzzleA_01 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_PuzzleA_01.prefab:42f10ee5624a63c4db7c58cb813fcf68");

	// Token: 0x04001FD3 RID: 8147
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_PuzzleB_01 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_PuzzleB_01.prefab:aa241a50b597a804da2db166737c9b83");

	// Token: 0x04001FD4 RID: 8148
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Restart_01 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Restart_01.prefab:22352523a16d0de4397ef19bca949777");

	// Token: 0x04001FD5 RID: 8149
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Restart_02 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Restart_02.prefab:b6a4123faabc12a4eb180c5594ce7e9e");

	// Token: 0x04001FD6 RID: 8150
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Restart_03 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Restart_03.prefab:b708ab15a42fd6a488aa8a2e4279901d");

	// Token: 0x04001FD7 RID: 8151
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Restart_04 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Restart_04.prefab:1394d7e77382c0245b6438be7768628b");

	// Token: 0x04001FD8 RID: 8152
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Restart_05 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Restart_05.prefab:fdd7f44fe853eb94a92e0050a95a5216");

	// Token: 0x04001FD9 RID: 8153
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Restart_06 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Restart_06.prefab:731193994db2ab442965b205d004bccd");

	// Token: 0x04001FDA RID: 8154
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Restart_07 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Restart_07.prefab:e37ea594abe480b4abef18b2533e239a");

	// Token: 0x04001FDB RID: 8155
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Return_01 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Return_01.prefab:505a989d1afda44489910b10aab7fcb8");

	// Token: 0x04001FDC RID: 8156
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Stuck_01 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Stuck_01.prefab:a81e8fa906a5ea5488e8ee680a38c315");

	// Token: 0x04001FDD RID: 8157
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Stuck_02 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Stuck_02.prefab:c2a319a1319d73c4e9436900c00122e5");

	// Token: 0x04001FDE RID: 8158
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Stuck_03 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Stuck_03.prefab:6f86c230f2d07c94c9efcb167e20e549");

	// Token: 0x04001FDF RID: 8159
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Stuck_04 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Stuck_04.prefab:d341c3875b5abc048b27571788dde5ad");

	// Token: 0x04001FE0 RID: 8160
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Puzzle_02.prefab:2c4b9e774308eea48ab33b21bcb0d07c");

	// Token: 0x04001FE1 RID: 8161
	private string COMPLETE_LINE = BOTA_Survival_Puzzle_2.VO_BOTA_BOSS_16h_Male_Goblin_Complete_02;
}
