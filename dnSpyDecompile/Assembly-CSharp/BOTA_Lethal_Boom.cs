using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000412 RID: 1042
public class BOTA_Lethal_Boom : BOTA_MissionEntity
{
	// Token: 0x0600394A RID: 14666 RVA: 0x00120D88 File Offset: 0x0011EF88
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_01,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_02,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_04,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_05,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_06,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_01,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_02,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_03,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_04,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_05,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_06,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_02,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_04,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_11,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_18,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_12,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_17,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_03,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Lethal_01,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Lethal_02,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_19,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_UI_Boom_Coin2_End_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600394B RID: 14667 RVA: 0x0011F420 File Offset: 0x0011D620
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BOTFinalBoss);
	}

	// Token: 0x0600394C RID: 14668 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x0600394D RID: 14669 RVA: 0x00120F40 File Offset: 0x0011F140
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_19;
		BOTA_MissionEntity.s_returnLine = BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_03;
		this.s_victoryLine_1 = BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Lethal_01;
		this.s_victoryLine_2 = BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Lethal_02;
		this.s_victoryLine_3 = null;
		this.s_victoryLine_4 = null;
		this.s_victoryLine_5 = null;
		this.s_victoryLine_6 = null;
		this.s_victoryLine_7 = null;
		this.s_victoryLine_8 = null;
		this.s_victoryLine_9 = null;
		this.s_emoteLines = new List<string>
		{
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_12,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_17
		};
		this.s_idleLines = new List<string>
		{
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_01,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_02,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_03,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_04,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_05,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_06
		};
		this.s_restartLines = new List<string>
		{
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_01,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_02,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_04,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_05,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_06,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_18,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_11,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_02,
			BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_04
		};
	}

	// Token: 0x0600394E RID: 14670 RVA: 0x001210F3 File Offset: 0x0011F2F3
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 77)
		{
			GameState.Get().SetBusy(true);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x0600394F RID: 14671 RVA: 0x00121102 File Offset: 0x0011F302
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return base.PlayClosingLine("DrBoom_Banner_Quote.prefab:ff8653a27a00c464ea5552e3c6b6dc5c", BOTA_Lethal_Boom.VO_BOTA_BOSS_20h_Male_Goblin_UI_Boom_Coin2_End_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04001E6F RID: 7791
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_01.prefab:b20d52247bbde0d42bbefc64782157b5");

	// Token: 0x04001E70 RID: 7792
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_02 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_02.prefab:52e836691533e3c4088fdb10776b729b");

	// Token: 0x04001E71 RID: 7793
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_04 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_04.prefab:2e4544fa8c22f884bac0ac39c8f532c2");

	// Token: 0x04001E72 RID: 7794
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_05 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_05.prefab:fe6e5edff239439468bbb9010eacd983");

	// Token: 0x04001E73 RID: 7795
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_06 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_06.prefab:e790bfa98d7ddb74c98291acd203a3aa");

	// Token: 0x04001E74 RID: 7796
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_01.prefab:828de7f730eb81b46888d3b574abbd08");

	// Token: 0x04001E75 RID: 7797
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_02 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_02.prefab:b6a9def10b3457f49b9af0e8a1a77a60");

	// Token: 0x04001E76 RID: 7798
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_03 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_03.prefab:f42a1a17fb2fde249a467e44d4ad212a");

	// Token: 0x04001E77 RID: 7799
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_04 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_04.prefab:649dafe9e6a4b4842a43754f6e28a5ef");

	// Token: 0x04001E78 RID: 7800
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_05 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_05.prefab:2c5259eedbae90d4783c3fec86f31445");

	// Token: 0x04001E79 RID: 7801
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_06 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_06.prefab:baa804a050242ce458f50095a1dae149");

	// Token: 0x04001E7A RID: 7802
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_02 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_02.prefab:96fa445d8b983f14a8411a2d6d34f5d8");

	// Token: 0x04001E7B RID: 7803
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_04 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_04.prefab:189f72a1be840df43a88b50b971327ee");

	// Token: 0x04001E7C RID: 7804
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_11 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_11.prefab:8adaa50a0eb7df349bc61382eb1c059c");

	// Token: 0x04001E7D RID: 7805
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_18 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_18.prefab:51395490635b9e5479b827925921d3fb");

	// Token: 0x04001E7E RID: 7806
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_12 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_12.prefab:d8a8edc6248318147bfd375b0204ee96");

	// Token: 0x04001E7F RID: 7807
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_17 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_17.prefab:3772b50d815a43c4bbeaf0f9f8204687");

	// Token: 0x04001E80 RID: 7808
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_03 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_03.prefab:b7a9336d26f58444e8df454f27877f7f");

	// Token: 0x04001E81 RID: 7809
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Lethal_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Lethal_01.prefab:73a8fd8a3b781cf4a8473024416ccbb2");

	// Token: 0x04001E82 RID: 7810
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Lethal_02 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Lethal_02.prefab:b3b160fdcda484846ad8896c1287ea2d");

	// Token: 0x04001E83 RID: 7811
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_19 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_19.prefab:a163874a9380692408b66b9f8cdd9fe2");

	// Token: 0x04001E84 RID: 7812
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_UI_Boom_Coin2_End_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_UI_Boom_Coin2_End_01.prefab:e00310d5a450c9f4cb00e5905774e310");
}
