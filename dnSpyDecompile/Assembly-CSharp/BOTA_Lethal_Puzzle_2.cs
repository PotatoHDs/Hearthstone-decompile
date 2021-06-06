using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000414 RID: 1044
public class BOTA_Lethal_Puzzle_2 : BOTA_MissionEntity
{
	// Token: 0x06003959 RID: 14681 RVA: 0x001219A0 File Offset: 0x0011FBA0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_01,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_02,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_03,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Idle_01,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Idle_02,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Idle_03,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Idle_04,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Idle_05,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Idle_06,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Idle_07,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Intro_01,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Restart_01,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Restart_02,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Restart_03,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Restart_04,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Restart_05,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Restart_06,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Return_01,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Puzzle_01,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Puzzle_02,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Puzzle_04,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Lethal_01,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Lethal_02,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Lethal_03,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Lethal_04,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Lethal_05
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600395A RID: 14682 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x0600395B RID: 14683 RVA: 0x00121B98 File Offset: 0x0011FD98
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Intro_01;
		BOTA_MissionEntity.s_returnLine = BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Return_01;
		this.s_victoryLine_1 = null;
		this.s_victoryLine_2 = BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Puzzle_01;
		this.s_victoryLine_3 = BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Puzzle_04;
		this.s_victoryLine_4 = null;
		this.s_victoryLine_5 = BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Puzzle_02;
		this.s_victoryLine_6 = null;
		this.s_victoryLine_7 = null;
		this.s_victoryLine_8 = null;
		this.s_victoryLine_9 = null;
		this.s_emoteLines = new List<string>
		{
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_01,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_02,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_03
		};
		this.s_idleLines = new List<string>
		{
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Idle_01,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Idle_02,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Idle_03,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Idle_04,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Idle_05,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Idle_06,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Idle_07
		};
		this.s_restartLines = new List<string>
		{
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Restart_01,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Restart_02,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Restart_03,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Restart_04,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Restart_05,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Restart_06
		};
		this.s_lethalCompleteLines = new List<string>
		{
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Lethal_01,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Lethal_02,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Lethal_03,
			BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Lethal_05
		};
	}

	// Token: 0x0600395C RID: 14684 RVA: 0x00121D8F File Offset: 0x0011FF8F
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent != 99)
		{
			if (missionEvent == 777)
			{
				string lethalCompleteLine = base.GetLethalCompleteLine();
				if (lethalCompleteLine != null)
				{
					yield return base.PlayBossLine(enemyActor, lethalCompleteLine, 2.5f);
				}
			}
		}
		else
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

	// Token: 0x04001EA9 RID: 7849
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_01.prefab:01668d8ad76337c4ea6ebf8f04946270");

	// Token: 0x04001EAA RID: 7850
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_02.prefab:11cf744c7e2a85d4ab507a965477876e");

	// Token: 0x04001EAB RID: 7851
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_EmoteResponse_03.prefab:3d73adb89efc1c346aec2b9bb3e283ee");

	// Token: 0x04001EAC RID: 7852
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Idle_01 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Idle_01.prefab:20f0ba7d7cc29e04eb6f8f0187a1c398");

	// Token: 0x04001EAD RID: 7853
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Idle_02 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Idle_02.prefab:3f30ed14b89a4e840b134cda792de80e");

	// Token: 0x04001EAE RID: 7854
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Idle_03 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Idle_03.prefab:5f20588d6ef530442850bb69e58718d0");

	// Token: 0x04001EAF RID: 7855
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Idle_04 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Idle_04.prefab:4b9ac5667759f7f499469c70a1aa1ff6");

	// Token: 0x04001EB0 RID: 7856
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Idle_05 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Idle_05.prefab:b95b1471ebad8944ea1ce83a392960a3");

	// Token: 0x04001EB1 RID: 7857
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Idle_06 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Idle_06.prefab:82dd5390618467542b4ac7139393d1db");

	// Token: 0x04001EB2 RID: 7858
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Idle_07 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Idle_07.prefab:482dfb36ff2161848b23e7a2e56d28a6");

	// Token: 0x04001EB3 RID: 7859
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Intro_01 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Intro_01.prefab:84bbbd933620e404a93764ccb790f527");

	// Token: 0x04001EB4 RID: 7860
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Restart_01 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Restart_01.prefab:275eec4051c77464b9a6f605fed0cb60");

	// Token: 0x04001EB5 RID: 7861
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Restart_02 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Restart_02.prefab:26d94195ce8669045a351488dd83ac53");

	// Token: 0x04001EB6 RID: 7862
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Restart_03 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Restart_03.prefab:65a355a7bb3a404489559f80ba32cf68");

	// Token: 0x04001EB7 RID: 7863
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Restart_04 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Restart_04.prefab:c0dcc936e8095e1408637855c433cb3d");

	// Token: 0x04001EB8 RID: 7864
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Restart_05 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Restart_05.prefab:a68c95018db31f94694fa59876b231db");

	// Token: 0x04001EB9 RID: 7865
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Restart_06 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Restart_06.prefab:44cc768909ef63c4fb33864cab80ecb8");

	// Token: 0x04001EBA RID: 7866
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Return_01 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Return_01.prefab:dfa5853bd9117ed4bb08726ddbfd8aa8");

	// Token: 0x04001EBB RID: 7867
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Puzzle_01.prefab:2652e8f87f7ff904cae02c6588c10ffd");

	// Token: 0x04001EBC RID: 7868
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Puzzle_02.prefab:69e16d58e05b2c749b6a04065f0c9db6");

	// Token: 0x04001EBD RID: 7869
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Puzzle_04 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Puzzle_04.prefab:4a785dd86a3e0b340aecbcc8c2a0739f");

	// Token: 0x04001EBE RID: 7870
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Lethal_01 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Lethal_01.prefab:c67181d3302889a4e9294ed5f1d68bff");

	// Token: 0x04001EBF RID: 7871
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Lethal_02 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Lethal_02.prefab:4850d07227229d9408911a6b82ac5719");

	// Token: 0x04001EC0 RID: 7872
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Lethal_03 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Lethal_03.prefab:7d0e11c1bebebb9448c610d656e0f4da");

	// Token: 0x04001EC1 RID: 7873
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Lethal_04 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Lethal_04.prefab:69fa5e9af397c0b4a865cf2ec5fb59cc");

	// Token: 0x04001EC2 RID: 7874
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Lethal_05 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Lethal_05.prefab:4d1fbfe5571c53d4fb2fba3d4ed8418f");

	// Token: 0x04001EC3 RID: 7875
	private string COMPLETE_LINE = BOTA_Lethal_Puzzle_2.VO_BOTA_BOSS_14h_Female_Undead_Lethal_04;
}
