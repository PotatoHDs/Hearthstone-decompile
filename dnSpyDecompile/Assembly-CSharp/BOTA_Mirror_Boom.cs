using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000417 RID: 1047
public class BOTA_Mirror_Boom : BOTA_MissionEntity
{
	// Token: 0x0600396D RID: 14701 RVA: 0x00122BC0 File Offset: 0x00120DC0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_01,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_02,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_04,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_05,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_06,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_01,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_02,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_03,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_04,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_05,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_06,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_02,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_04,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_06,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_11,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_18,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_12,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_17,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Mirror_01,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Mirror_02,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Mirror_03,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_UI_Boom_Coin3_End_01,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_14
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600396E RID: 14702 RVA: 0x0011F420 File Offset: 0x0011D620
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BOTFinalBoss);
	}

	// Token: 0x0600396F RID: 14703 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x06003970 RID: 14704 RVA: 0x00122D88 File Offset: 0x00120F88
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Mirror_01;
		BOTA_MissionEntity.s_returnLine = BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_14;
		this.s_victoryLine_1 = BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Mirror_02;
		this.s_victoryLine_2 = null;
		this.s_victoryLine_3 = BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Mirror_03;
		this.s_victoryLine_4 = BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_06;
		this.s_victoryLine_5 = null;
		this.s_victoryLine_6 = null;
		this.s_victoryLine_7 = null;
		this.s_victoryLine_8 = null;
		this.s_victoryLine_9 = null;
		this.s_emoteLines = new List<string>
		{
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_12,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_17
		};
		this.s_idleLines = new List<string>
		{
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_01,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_02,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_03,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_04,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_05,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_06
		};
		this.s_restartLines = new List<string>
		{
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_01,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_02,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_04,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_05,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_06,
			BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_18
		};
	}

	// Token: 0x06003971 RID: 14705 RVA: 0x00122F14 File Offset: 0x00121114
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield break;
	}

	// Token: 0x06003972 RID: 14706 RVA: 0x00122F1C File Offset: 0x0012111C
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return base.PlayClosingLine("DrBoom_Banner_Quote.prefab:ff8653a27a00c464ea5552e3c6b6dc5c", BOTA_Mirror_Boom.VO_BOTA_BOSS_20h_Male_Goblin_UI_Boom_Coin3_End_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04001F00 RID: 7936
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_01.prefab:b20d52247bbde0d42bbefc64782157b5");

	// Token: 0x04001F01 RID: 7937
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_02 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_02.prefab:52e836691533e3c4088fdb10776b729b");

	// Token: 0x04001F02 RID: 7938
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_04 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_04.prefab:2e4544fa8c22f884bac0ac39c8f532c2");

	// Token: 0x04001F03 RID: 7939
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_05 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_05.prefab:fe6e5edff239439468bbb9010eacd983");

	// Token: 0x04001F04 RID: 7940
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_06 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_06.prefab:e790bfa98d7ddb74c98291acd203a3aa");

	// Token: 0x04001F05 RID: 7941
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_01.prefab:828de7f730eb81b46888d3b574abbd08");

	// Token: 0x04001F06 RID: 7942
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_02 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_02.prefab:b6a9def10b3457f49b9af0e8a1a77a60");

	// Token: 0x04001F07 RID: 7943
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_03 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_03.prefab:f42a1a17fb2fde249a467e44d4ad212a");

	// Token: 0x04001F08 RID: 7944
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_04 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_04.prefab:649dafe9e6a4b4842a43754f6e28a5ef");

	// Token: 0x04001F09 RID: 7945
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_05 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_05.prefab:2c5259eedbae90d4783c3fec86f31445");

	// Token: 0x04001F0A RID: 7946
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_06 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_06.prefab:baa804a050242ce458f50095a1dae149");

	// Token: 0x04001F0B RID: 7947
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_02 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_02.prefab:96fa445d8b983f14a8411a2d6d34f5d8");

	// Token: 0x04001F0C RID: 7948
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_04 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_04.prefab:189f72a1be840df43a88b50b971327ee");

	// Token: 0x04001F0D RID: 7949
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_11 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_11.prefab:8adaa50a0eb7df349bc61382eb1c059c");

	// Token: 0x04001F0E RID: 7950
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_18 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_18.prefab:51395490635b9e5479b827925921d3fb");

	// Token: 0x04001F0F RID: 7951
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_12 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_12.prefab:d8a8edc6248318147bfd375b0204ee96");

	// Token: 0x04001F10 RID: 7952
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_17 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_17.prefab:3772b50d815a43c4bbeaf0f9f8204687");

	// Token: 0x04001F11 RID: 7953
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Mirror_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Mirror_01.prefab:a881f05efa02e4e4a867d489dcf8440b");

	// Token: 0x04001F12 RID: 7954
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Mirror_02 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Mirror_02.prefab:be99b4dfcaa2b884289b842a15d14dc1");

	// Token: 0x04001F13 RID: 7955
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Mirror_03 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Mirror_03.prefab:6e7bd2d34992650429c00b40fd1bece6");

	// Token: 0x04001F14 RID: 7956
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_UI_Boom_Coin3_End_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_UI_Boom_Coin3_End_01.prefab:2b3d5345a08f3484c84dd3ae79fc19f8");

	// Token: 0x04001F15 RID: 7957
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_06 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_06.prefab:f4389a18e8fd2e74688b38f30e0cd739");

	// Token: 0x04001F16 RID: 7958
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_14 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_14.prefab:4bab8d9c67c294c4da6d9c7c529ed818");
}
