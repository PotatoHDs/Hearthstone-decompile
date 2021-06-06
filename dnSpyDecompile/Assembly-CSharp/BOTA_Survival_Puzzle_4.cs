using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000421 RID: 1057
public class BOTA_Survival_Puzzle_4 : BOTA_MissionEntity
{
	// Token: 0x060039D3 RID: 14803 RVA: 0x001262B4 File Offset: 0x001244B4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Complete_01,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_EmoteResponse_01,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_EmoteResponse_02,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Idle_01,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Idle_02,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Idle_03,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Idle_05,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Idle_06,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Idle_07,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Idle_09,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Restart_01,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Restart_02,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Restart_03,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Restart_04,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Restart_06,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Return_01,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Puzzle_01,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Puzzle_02,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Puzzle_03,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Puzzle_04,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Intro_02,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing4_Complete_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060039D4 RID: 14804 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x060039D5 RID: 14805 RVA: 0x0012646C File Offset: 0x0012466C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Intro_02;
		BOTA_MissionEntity.s_returnLine = BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Return_01;
		this.s_victoryLine_1 = BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Puzzle_01;
		this.s_victoryLine_2 = BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Puzzle_02;
		this.s_victoryLine_3 = null;
		this.s_victoryLine_4 = BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Puzzle_03;
		this.s_victoryLine_5 = BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Puzzle_04;
		this.s_victoryLine_6 = null;
		this.s_victoryLine_7 = null;
		this.s_victoryLine_8 = null;
		this.s_victoryLine_9 = null;
		this.s_emoteLines = new List<string>
		{
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_EmoteResponse_01,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_EmoteResponse_02
		};
		this.s_idleLines = new List<string>
		{
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Idle_01,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Idle_02,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Idle_03,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Idle_05,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Idle_06,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Idle_07,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Idle_09
		};
		this.s_restartLines = new List<string>
		{
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Restart_01,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Restart_02,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Restart_03,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Restart_04,
			BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Restart_06
		};
	}

	// Token: 0x060039D6 RID: 14806 RVA: 0x00126601 File Offset: 0x00124801
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

	// Token: 0x060039D7 RID: 14807 RVA: 0x00126617 File Offset: 0x00124817
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(2f);
			yield return base.PlayClosingLine("DrBoom_Banner_Quote.prefab:ff8653a27a00c464ea5552e3c6b6dc5c", BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing4_Complete_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04001FFA RID: 8186
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Complete_01 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Complete_01.prefab:652de58b37a2cc44593424ef18e1c0e3");

	// Token: 0x04001FFB RID: 8187
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_EmoteResponse_01.prefab:fe1cbdd3e087f53448f8a6994e132243");

	// Token: 0x04001FFC RID: 8188
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_EmoteResponse_02.prefab:9abec6566dab8c247ae1b802239cbcdc");

	// Token: 0x04001FFD RID: 8189
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Idle_01 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Idle_01.prefab:78cfe5678170c684f946ce2a6de56e94");

	// Token: 0x04001FFE RID: 8190
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Idle_02 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Idle_02.prefab:e9dcb30c8c1e1854f8eb05ff0cae0e21");

	// Token: 0x04001FFF RID: 8191
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Idle_03 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Idle_03.prefab:7bc1b09ecbd8b4649b563356f2446c96");

	// Token: 0x04002000 RID: 8192
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Idle_05 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Idle_05.prefab:2568976f3c4a6544d85e35dbbd145325");

	// Token: 0x04002001 RID: 8193
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Idle_06 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Idle_06.prefab:19878fd22622c92488ff92448db8c8b4");

	// Token: 0x04002002 RID: 8194
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Idle_07 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Idle_07.prefab:25b8964a432f35f4480842930111ca45");

	// Token: 0x04002003 RID: 8195
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Idle_09 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Idle_09.prefab:52f153a746a56644d92ad1ed0483968f");

	// Token: 0x04002004 RID: 8196
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Restart_01 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Restart_01.prefab:5426d1b0dbbc20f4194fe44eef89e968");

	// Token: 0x04002005 RID: 8197
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Restart_02 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Restart_02.prefab:5671fb3cf6dab1e47a1abd0977e44d62");

	// Token: 0x04002006 RID: 8198
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Restart_03 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Restart_03.prefab:5aea3e7327483c544b766499e291ae15");

	// Token: 0x04002007 RID: 8199
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Restart_04 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Restart_04.prefab:c9fd07b42c8513c4793b09c065ab1345");

	// Token: 0x04002008 RID: 8200
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Restart_06 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Restart_06.prefab:60c118621f017f34eadf8675ba1d4ccc");

	// Token: 0x04002009 RID: 8201
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Return_01 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Return_01.prefab:e4e61ef196a1dc54d93654ca1931eeb4");

	// Token: 0x0400200A RID: 8202
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Puzzle_01.prefab:53a769c89b14e0648b1b90f7857da06a");

	// Token: 0x0400200B RID: 8203
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Puzzle_02.prefab:ad8baa4a98a84e9479e07a81db7217b1");

	// Token: 0x0400200C RID: 8204
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Puzzle_03.prefab:a5268202781ce5f41ba45fb11e1f3104");

	// Token: 0x0400200D RID: 8205
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Puzzle_04 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Puzzle_04.prefab:2fbfc68ed6374d248934858eb3d3c7ad");

	// Token: 0x0400200E RID: 8206
	private static readonly AssetReference VO_BOTA_BOSS_18h_Male_Golem_Intro_02 = new AssetReference("VO_BOTA_BOSS_18h_Male_Golem_Intro_02.prefab:217e6f991f7f0af4989e47a4ae139bdf");

	// Token: 0x0400200F RID: 8207
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing4_Complete_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing4_Complete_01.prefab:6946f82a36bc4064eac0d36169ca6976");

	// Token: 0x04002010 RID: 8208
	private string COMPLETE_LINE = BOTA_Survival_Puzzle_4.VO_BOTA_BOSS_18h_Male_Golem_Complete_01;
}
