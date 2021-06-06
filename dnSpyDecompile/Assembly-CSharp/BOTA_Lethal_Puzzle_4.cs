using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000416 RID: 1046
public class BOTA_Lethal_Puzzle_4 : BOTA_MissionEntity
{
	// Token: 0x06003966 RID: 14694 RVA: 0x00122628 File Offset: 0x00120828
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Complete_01,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_01,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_02,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_03,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Idle_01,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Idle_02,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Idle_03,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Idle_04,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Idle_06,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Idle_07,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Idle_08,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Intro_01,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Restart_01,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Restart_02,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Restart_03,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Restart_04,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Restart_05,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Restart_06,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Return_01,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_01,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_02,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_03,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_04,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_06,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_07,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing1_Complete_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003967 RID: 14695 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x06003968 RID: 14696 RVA: 0x00122820 File Offset: 0x00120A20
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Intro_01;
		BOTA_MissionEntity.s_returnLine = BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Return_01;
		this.s_victoryLine_1 = BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_03;
		this.s_victoryLine_2 = BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_02;
		this.s_victoryLine_3 = BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_01;
		this.s_victoryLine_4 = BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_04;
		this.s_victoryLine_5 = BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_07;
		this.s_victoryLine_6 = BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_06;
		this.s_victoryLine_7 = null;
		this.s_victoryLine_8 = null;
		this.s_victoryLine_9 = null;
		this.s_emoteLines = new List<string>
		{
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_01,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_02,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_03
		};
		this.s_idleLines = new List<string>
		{
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Idle_01,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Idle_02,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Idle_03,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Idle_04,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Idle_06,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Idle_07,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Idle_08
		};
		this.s_restartLines = new List<string>
		{
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Restart_01,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Restart_02,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Restart_03,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Restart_04,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Restart_05,
			BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Restart_06
		};
	}

	// Token: 0x06003969 RID: 14697 RVA: 0x001229E7 File Offset: 0x00120BE7
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(2f);
			yield return base.PlayClosingLine("DrBoom_Banner_Quote.prefab:ff8653a27a00c464ea5552e3c6b6dc5c", BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing1_Complete_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600396A RID: 14698 RVA: 0x001229FD File Offset: 0x00120BFD
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

	// Token: 0x04001EE5 RID: 7909
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Complete_01 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Complete_01.prefab:12d1fca9662768a40b0f8d3b088c2865");

	// Token: 0x04001EE6 RID: 7910
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_01.prefab:9cff3eedf14ce5946979a7d15d103f21");

	// Token: 0x04001EE7 RID: 7911
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_02.prefab:5a51491774815bb40950ed12f915a127");

	// Token: 0x04001EE8 RID: 7912
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_EmoteResponse_03.prefab:dbaff8ad97f99d04cb5f4ac510ef9c1f");

	// Token: 0x04001EE9 RID: 7913
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Idle_01 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Idle_01.prefab:86fca2830272fe649be137a1e3e284bb");

	// Token: 0x04001EEA RID: 7914
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Idle_02 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Idle_02.prefab:bc96a1a2437d4974587eeebf2906866c");

	// Token: 0x04001EEB RID: 7915
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Idle_03 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Idle_03.prefab:277e6570ebc8d504abb65b008b44f7b6");

	// Token: 0x04001EEC RID: 7916
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Idle_04 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Idle_04.prefab:68cd898569fcefb41aa7f3ced593b888");

	// Token: 0x04001EED RID: 7917
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Idle_06 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Idle_06.prefab:3cb0c90090550604d92f94194282f701");

	// Token: 0x04001EEE RID: 7918
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Idle_07 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Idle_07.prefab:a02f96ec96c83ff46aba2c081160924c");

	// Token: 0x04001EEF RID: 7919
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Idle_08 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Idle_08.prefab:36a855ef080019a43b917994ef4d72c6");

	// Token: 0x04001EF0 RID: 7920
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Intro_01 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Intro_01.prefab:4ce422ee10a2f6140a4894d02a44b6fa");

	// Token: 0x04001EF1 RID: 7921
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Restart_01 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Restart_01.prefab:a9159ffa8f5140241b8de7401d4eabc9");

	// Token: 0x04001EF2 RID: 7922
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Restart_02 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Restart_02.prefab:8257e81f91c42eb449b5f3d10e2370a6");

	// Token: 0x04001EF3 RID: 7923
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Restart_03 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Restart_03.prefab:77f2961c7cf36d6489e8df15c188cfda");

	// Token: 0x04001EF4 RID: 7924
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Restart_04 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Restart_04.prefab:db537736afd07ad44b2f099317737a46");

	// Token: 0x04001EF5 RID: 7925
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Restart_05 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Restart_05.prefab:3a4521c2c165e494c818a8c6878f6f78");

	// Token: 0x04001EF6 RID: 7926
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Restart_06 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Restart_06.prefab:242e8aaac384ecf418b3d864166ab0c7");

	// Token: 0x04001EF7 RID: 7927
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Return_01 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Return_01.prefab:223beb64577c2514eb4fd5e238c3f114");

	// Token: 0x04001EF8 RID: 7928
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_01.prefab:675c01f7b882dfa4fbffaf36b28d1736");

	// Token: 0x04001EF9 RID: 7929
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_02.prefab:8445ce14f00193a4c808b0bbc38550fd");

	// Token: 0x04001EFA RID: 7930
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_03.prefab:977f48dbc23a3c445a50ef8f40b2ddee");

	// Token: 0x04001EFB RID: 7931
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_04 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_04.prefab:7bf5c3a8c493d214d99c220228fa2d50");

	// Token: 0x04001EFC RID: 7932
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_06 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_06.prefab:612076d12b7dfc8468ef99d3b0d0e3c0");

	// Token: 0x04001EFD RID: 7933
	private static readonly AssetReference VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_07 = new AssetReference("VO_BOTA_BOSS_13h_Female_Elemental_Puzzle_07.prefab:b3c28276cc875e345ae48352a7e0dd55");

	// Token: 0x04001EFE RID: 7934
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing1_Complete_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing1_Complete_01.prefab:b21dd5ba0d46a704db275efac82c7b67");

	// Token: 0x04001EFF RID: 7935
	private string COMPLETE_LINE = BOTA_Lethal_Puzzle_4.VO_BOTA_BOSS_13h_Female_Elemental_Complete_01;
}
