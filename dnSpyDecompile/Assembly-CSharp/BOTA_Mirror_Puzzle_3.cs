using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200041A RID: 1050
public class BOTA_Mirror_Puzzle_3 : BOTA_MissionEntity
{
	// Token: 0x06003982 RID: 14722 RVA: 0x001239B0 File Offset: 0x00121BB0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Complete_01,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_EmoteResponse_01,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_EmoteResponse_02,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_EmoteResponse_03,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Idle_01,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Idle_03,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Idle_04,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Idle_05,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Idle_07,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Idle_08,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Idle_09,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Intro_01,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_01,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_02,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_03,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_04,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_05,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_06,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Restart_01,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Restart_02,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Restart_03,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Restart_05,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Return_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003983 RID: 14723 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x06003984 RID: 14724 RVA: 0x00123B78 File Offset: 0x00121D78
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Intro_01;
		BOTA_MissionEntity.s_returnLine = BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Return_01;
		this.s_victoryLine_1 = BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_01;
		this.s_victoryLine_2 = BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_04;
		this.s_victoryLine_3 = BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_03;
		this.s_victoryLine_4 = BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_02;
		this.s_victoryLine_5 = null;
		this.s_victoryLine_6 = BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_05;
		this.s_victoryLine_7 = BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_06;
		this.s_victoryLine_8 = null;
		this.s_victoryLine_9 = null;
		this.s_emoteLines = new List<string>
		{
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_EmoteResponse_01,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_EmoteResponse_02,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_EmoteResponse_03
		};
		this.s_idleLines = new List<string>
		{
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Idle_01,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Idle_03,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Idle_04,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Idle_05,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Idle_07,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Idle_08,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Idle_09
		};
		this.s_restartLines = new List<string>
		{
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Restart_01,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Restart_02,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Restart_03,
			BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Restart_05
		};
	}

	// Token: 0x06003985 RID: 14725 RVA: 0x00123D1F File Offset: 0x00121F1F
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

	// Token: 0x04001F3B RID: 7995
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_Complete_01 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_Complete_01.prefab:de9ecbc3f75cbdc4bac83192eca8f0f2");

	// Token: 0x04001F3C RID: 7996
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_EmoteResponse_01.prefab:b044edc26aafb9642a67961319817eca");

	// Token: 0x04001F3D RID: 7997
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_EmoteResponse_02.prefab:17d25e1b7dcbb8446a839739b8294b23");

	// Token: 0x04001F3E RID: 7998
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_EmoteResponse_03.prefab:536f84845df25264b99da8ac2669690e");

	// Token: 0x04001F3F RID: 7999
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_Idle_01 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_Idle_01.prefab:47b7fbd6fb558fd40bd354c426408aff");

	// Token: 0x04001F40 RID: 8000
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_Idle_03 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_Idle_03.prefab:00940d90da91b304f9ae498e0798bd18");

	// Token: 0x04001F41 RID: 8001
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_Idle_04 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_Idle_04.prefab:59582a9d62f15d748901e3e85af44986");

	// Token: 0x04001F42 RID: 8002
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_Idle_05 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_Idle_05.prefab:b3c79cdace023b74c8cc2355538bd3af");

	// Token: 0x04001F43 RID: 8003
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_Idle_07 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_Idle_07.prefab:f95327813868ad34bb46b14fb608bd2c");

	// Token: 0x04001F44 RID: 8004
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_Idle_08 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_Idle_08.prefab:296e7e89fd6a5c14f8068aef31656f93");

	// Token: 0x04001F45 RID: 8005
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_Idle_09 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_Idle_09.prefab:dbdb3b276936ec64993eb3d8944ec19d");

	// Token: 0x04001F46 RID: 8006
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_Intro_01 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_Intro_01.prefab:e0abe8e94b60cc94885b8a569eba2e23");

	// Token: 0x04001F47 RID: 8007
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_01.prefab:1da8155e6c2d6b742b29a2fc0a73ec01");

	// Token: 0x04001F48 RID: 8008
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_02.prefab:bada34dac1f65904386694d1dfc0adf5");

	// Token: 0x04001F49 RID: 8009
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_03.prefab:265f1f8901fc88945b262c73642bcf4c");

	// Token: 0x04001F4A RID: 8010
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_04 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_04.prefab:3158c3332ac67fc46af92b3310a84335");

	// Token: 0x04001F4B RID: 8011
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_05 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_05.prefab:07ceac8974cce294995ea36bb200c346");

	// Token: 0x04001F4C RID: 8012
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_06 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_Puzzle_06.prefab:748eeeaedee9c6741ba5db18f62ed6b7");

	// Token: 0x04001F4D RID: 8013
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_Restart_01 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_Restart_01.prefab:11119bcf728cd15498711b521abe4cc3");

	// Token: 0x04001F4E RID: 8014
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_Restart_02 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_Restart_02.prefab:aec95d129df680a4fb323d4892851a47");

	// Token: 0x04001F4F RID: 8015
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_Restart_03 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_Restart_03.prefab:11ae6160be493ea40a43fe9e5e4fd005");

	// Token: 0x04001F50 RID: 8016
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_Restart_05 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_Restart_05.prefab:8e10ad6004c7c51409b0aa0559bf1f04");

	// Token: 0x04001F51 RID: 8017
	private static readonly AssetReference VO_BOTA_BOSS_04h_Female_Draenei_Return_01 = new AssetReference("VO_BOTA_BOSS_04h_Female_Draenei_Return_01.prefab:b24c9edeebfe82a42af22aa150986528");

	// Token: 0x04001F52 RID: 8018
	private string COMPLETE_LINE = BOTA_Mirror_Puzzle_3.VO_BOTA_BOSS_04h_Female_Draenei_Complete_01;
}
