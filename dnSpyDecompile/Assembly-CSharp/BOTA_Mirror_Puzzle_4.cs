using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200041B RID: 1051
public class BOTA_Mirror_Puzzle_4 : BOTA_MissionEntity
{
	// Token: 0x06003988 RID: 14728 RVA: 0x00123EB8 File Offset: 0x001220B8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Complete_03,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_EmoteResponse_01,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_EmoteResponse_02,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Idle_03,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Idle_04,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Idle_05,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Idle_07,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Idle_08,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Idle_09,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Idle_10,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Idle_11,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Intro_01,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_01,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_02,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_03,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Restart_01,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Restart_02,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Restart_03,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Restart_04,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Restart_06,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Return_01,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_02,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_03,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_04,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_05,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing2_Complete_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003989 RID: 14729 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x0600398A RID: 14730 RVA: 0x001240B0 File Offset: 0x001222B0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Intro_01;
		BOTA_MissionEntity.s_returnLine = BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Return_01;
		this.s_victoryLine_1 = null;
		this.s_victoryLine_2 = BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_01;
		this.s_victoryLine_3 = BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_03;
		this.s_victoryLine_4 = BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_02;
		this.s_victoryLine_5 = null;
		this.s_victoryLine_6 = null;
		this.s_victoryLine_7 = null;
		this.s_victoryLine_8 = null;
		this.s_victoryLine_9 = null;
		this.s_emoteLines = new List<string>
		{
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_EmoteResponse_01,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_EmoteResponse_02
		};
		this.s_idleLines = new List<string>
		{
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Idle_03,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Idle_04,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Idle_05,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Idle_07,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Idle_08,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Idle_09,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Idle_10,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Idle_11
		};
		this.s_restartLines = new List<string>
		{
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Restart_01,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Restart_02,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Restart_03,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Restart_04,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Restart_06,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_02,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_03,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_04,
			BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_05
		};
	}

	// Token: 0x0600398B RID: 14731 RVA: 0x0012428C File Offset: 0x0012248C
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(2f);
			yield return base.PlayClosingLine("DrBoom_Banner_Quote.prefab:ff8653a27a00c464ea5552e3c6b6dc5c", BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing2_Complete_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600398C RID: 14732 RVA: 0x001242A2 File Offset: 0x001224A2
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

	// Token: 0x04001F53 RID: 8019
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Complete_03 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Complete_03.prefab:56bf73cc367105042977eede5c8ed438");

	// Token: 0x04001F54 RID: 8020
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_EmoteResponse_01.prefab:2ae64c5e79e893440b7ba443c31d3019");

	// Token: 0x04001F55 RID: 8021
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_EmoteResponse_02.prefab:1f00e297ef6614944981c4515f8aadbf");

	// Token: 0x04001F56 RID: 8022
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Idle_03 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Idle_03.prefab:b2910bd1ad25de74eab928ca2f338521");

	// Token: 0x04001F57 RID: 8023
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Idle_04 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Idle_04.prefab:36b225a82667f794495572f88645a113");

	// Token: 0x04001F58 RID: 8024
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Idle_05 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Idle_05.prefab:a330ff7d29a40ed48a780f8886c97a1b");

	// Token: 0x04001F59 RID: 8025
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Idle_07 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Idle_07.prefab:f18d1461a03d59c4aa3947d145c809fb");

	// Token: 0x04001F5A RID: 8026
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Idle_08 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Idle_08.prefab:b20962b3f0c5c0949ac20cadda48acdd");

	// Token: 0x04001F5B RID: 8027
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Idle_09 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Idle_09.prefab:7427c8a1e32320645a2f2788ea5dbe87");

	// Token: 0x04001F5C RID: 8028
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Idle_10 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Idle_10.prefab:28db4963e1604284e8b854495763786b");

	// Token: 0x04001F5D RID: 8029
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Idle_11 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Idle_11.prefab:2931d439b21f5564291d700a24a03253");

	// Token: 0x04001F5E RID: 8030
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Intro_01 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Intro_01.prefab:47b3a84a44cc5424589469c0a42333be");

	// Token: 0x04001F5F RID: 8031
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_01.prefab:825243f99bb5ad146a9293a2ec7df469");

	// Token: 0x04001F60 RID: 8032
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_02.prefab:6df967b5a36b4d7489c9428d9c1f2f1f");

	// Token: 0x04001F61 RID: 8033
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Puzzle_03.prefab:25ffc8f599b1c4e469e174e53d322957");

	// Token: 0x04001F62 RID: 8034
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Restart_01 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Restart_01.prefab:1274e3e0226dc64459086143408289d6");

	// Token: 0x04001F63 RID: 8035
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Restart_02 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Restart_02.prefab:aa1fdcfdc0765374a8d103fae23a33a2");

	// Token: 0x04001F64 RID: 8036
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Restart_03 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Restart_03.prefab:9349911a79338b146b065ece1a5f6b03");

	// Token: 0x04001F65 RID: 8037
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Restart_04 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Restart_04.prefab:6844f0a28ea08a34380ec1f3e9076c6d");

	// Token: 0x04001F66 RID: 8038
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Restart_06 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Restart_06.prefab:31fce4636803bc9468115bd58d183161");

	// Token: 0x04001F67 RID: 8039
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Return_01 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Return_01.prefab:150f0142e9b329847b27fb356ec1ce9e");

	// Token: 0x04001F68 RID: 8040
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_02 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_02.prefab:9054f87af94c30648a5a3a66abf36224");

	// Token: 0x04001F69 RID: 8041
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_03 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_03.prefab:d92f0c99c70071b469f0640b050dc3e9");

	// Token: 0x04001F6A RID: 8042
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_04 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_04.prefab:b067a6a440b23c248998e201c6bd6d5e");

	// Token: 0x04001F6B RID: 8043
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_05 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Stuck_05.prefab:1ddc8aa1f85ad594fa6d7d1aa2a1c799");

	// Token: 0x04001F6C RID: 8044
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing2_Complete_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing2_Complete_01.prefab:c3965288127268b418a87a211694b606");

	// Token: 0x04001F6D RID: 8045
	private string COMPLETE_LINE = BOTA_Mirror_Puzzle_4.VO_BOTA_BOSS_05h_Male_Ethereal_Complete_03;
}
