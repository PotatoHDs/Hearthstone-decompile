using System.Collections;
using System.Collections.Generic;

public class BOTA_Mirror_Boom : BOTA_MissionEntity
{
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_01.prefab:b20d52247bbde0d42bbefc64782157b5");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_02 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_02.prefab:52e836691533e3c4088fdb10776b729b");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_04 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_04.prefab:2e4544fa8c22f884bac0ac39c8f532c2");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_05 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_05.prefab:fe6e5edff239439468bbb9010eacd983");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_06 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_06.prefab:e790bfa98d7ddb74c98291acd203a3aa");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_01.prefab:828de7f730eb81b46888d3b574abbd08");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_02 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_02.prefab:b6a9def10b3457f49b9af0e8a1a77a60");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_03 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_03.prefab:f42a1a17fb2fde249a467e44d4ad212a");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_04 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_04.prefab:649dafe9e6a4b4842a43754f6e28a5ef");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_05 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_05.prefab:2c5259eedbae90d4783c3fec86f31445");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_06 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_06.prefab:baa804a050242ce458f50095a1dae149");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_02 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_02.prefab:96fa445d8b983f14a8411a2d6d34f5d8");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_04 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_04.prefab:189f72a1be840df43a88b50b971327ee");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_11 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_11.prefab:8adaa50a0eb7df349bc61382eb1c059c");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_18 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_18.prefab:51395490635b9e5479b827925921d3fb");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_12 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_12.prefab:d8a8edc6248318147bfd375b0204ee96");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_17 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_17.prefab:3772b50d815a43c4bbeaf0f9f8204687");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Mirror_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Mirror_01.prefab:a881f05efa02e4e4a867d489dcf8440b");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Mirror_02 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Mirror_02.prefab:be99b4dfcaa2b884289b842a15d14dc1");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Mirror_03 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Mirror_03.prefab:6e7bd2d34992650429c00b40fd1bece6");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_UI_Boom_Coin3_End_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_UI_Boom_Coin3_End_01.prefab:2b3d5345a08f3484c84dd3ae79fc19f8");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_06 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_06.prefab:f4389a18e8fd2e74688b38f30e0cd739");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_14 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_14.prefab:4bab8d9c67c294c4da6d9c7c529ed818");

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_BOTA_BOSS_20h_Male_Goblin_Failure_01, VO_BOTA_BOSS_20h_Male_Goblin_Failure_02, VO_BOTA_BOSS_20h_Male_Goblin_Failure_04, VO_BOTA_BOSS_20h_Male_Goblin_Failure_05, VO_BOTA_BOSS_20h_Male_Goblin_Failure_06, VO_BOTA_BOSS_20h_Male_Goblin_Idle_01, VO_BOTA_BOSS_20h_Male_Goblin_Idle_02, VO_BOTA_BOSS_20h_Male_Goblin_Idle_03, VO_BOTA_BOSS_20h_Male_Goblin_Idle_04, VO_BOTA_BOSS_20h_Male_Goblin_Idle_05,
			VO_BOTA_BOSS_20h_Male_Goblin_Idle_06, VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_02, VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_04, VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_06, VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_11, VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_18, VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_12, VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_17, VO_BOTA_BOSS_20h_Male_Goblin_Mirror_01, VO_BOTA_BOSS_20h_Male_Goblin_Mirror_02,
			VO_BOTA_BOSS_20h_Male_Goblin_Mirror_03, VO_BOTA_BOSS_20h_Male_Goblin_UI_Boom_Coin3_End_01, VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_14
		})
		{
			PreloadSound(item);
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BOTFinalBoss);
	}

	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = VO_BOTA_BOSS_20h_Male_Goblin_Mirror_01;
		BOTA_MissionEntity.s_returnLine = VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_14;
		s_victoryLine_1 = VO_BOTA_BOSS_20h_Male_Goblin_Mirror_02;
		s_victoryLine_2 = null;
		s_victoryLine_3 = VO_BOTA_BOSS_20h_Male_Goblin_Mirror_03;
		s_victoryLine_4 = VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_06;
		s_victoryLine_5 = null;
		s_victoryLine_6 = null;
		s_victoryLine_7 = null;
		s_victoryLine_8 = null;
		s_victoryLine_9 = null;
		s_emoteLines = new List<string> { VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_12, VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_17 };
		s_idleLines = new List<string> { VO_BOTA_BOSS_20h_Male_Goblin_Idle_01, VO_BOTA_BOSS_20h_Male_Goblin_Idle_02, VO_BOTA_BOSS_20h_Male_Goblin_Idle_03, VO_BOTA_BOSS_20h_Male_Goblin_Idle_04, VO_BOTA_BOSS_20h_Male_Goblin_Idle_05, VO_BOTA_BOSS_20h_Male_Goblin_Idle_06 };
		s_restartLines = new List<string> { VO_BOTA_BOSS_20h_Male_Goblin_Failure_01, VO_BOTA_BOSS_20h_Male_Goblin_Failure_02, VO_BOTA_BOSS_20h_Male_Goblin_Failure_04, VO_BOTA_BOSS_20h_Male_Goblin_Failure_05, VO_BOTA_BOSS_20h_Male_Goblin_Failure_06, VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_18 };
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield break;
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return PlayClosingLine("DrBoom_Banner_Quote.prefab:ff8653a27a00c464ea5552e3c6b6dc5c", VO_BOTA_BOSS_20h_Male_Goblin_UI_Boom_Coin3_End_01);
		}
	}
}
