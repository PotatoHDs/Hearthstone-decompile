using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200041D RID: 1053
public class BOTA_Survival_Boom : BOTA_MissionEntity
{
	// Token: 0x060039B6 RID: 14774 RVA: 0x00124D30 File Offset: 0x00122F30
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_01,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_02,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_04,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_05,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_06,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_01,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_02,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_03,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_04,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_05,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_06,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_02,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_04,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_08,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_11,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_18,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_12,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_17,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Survival_01,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Survival_02,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_UI_Boom_Coin4_Victory_01,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060039B7 RID: 14775 RVA: 0x0011F420 File Offset: 0x0011D620
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BOTFinalBoss);
	}

	// Token: 0x060039B8 RID: 14776 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x060039B9 RID: 14777 RVA: 0x00124EE8 File Offset: 0x001230E8
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_01;
		BOTA_MissionEntity.s_returnLine = BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_08;
		this.s_victoryLine_1 = null;
		this.s_victoryLine_2 = null;
		this.s_victoryLine_3 = BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Survival_02;
		this.s_victoryLine_4 = null;
		this.s_victoryLine_5 = BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Survival_01;
		this.s_victoryLine_6 = null;
		this.s_victoryLine_7 = null;
		this.s_victoryLine_8 = null;
		this.s_victoryLine_9 = null;
		this.s_emoteLines = new List<string>
		{
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_12,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_17
		};
		this.s_idleLines = new List<string>
		{
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_01,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_02,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_03,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_04,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_05,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_06,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_18
		};
		this.s_restartLines = new List<string>
		{
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_01,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_02,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_04,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_05,
			BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_06
		};
	}

	// Token: 0x060039BA RID: 14778 RVA: 0x0012506B File Offset: 0x0012326B
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield break;
	}

	// Token: 0x060039BB RID: 14779 RVA: 0x00125073 File Offset: 0x00123273
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return base.PlayClosingLine("DrBoom_Banner_Quote.prefab:ff8653a27a00c464ea5552e3c6b6dc5c", BOTA_Survival_Boom.VO_BOTA_BOSS_20h_Male_Goblin_UI_Boom_Coin4_Victory_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04001F95 RID: 8085
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_01.prefab:b20d52247bbde0d42bbefc64782157b5");

	// Token: 0x04001F96 RID: 8086
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_02 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_02.prefab:52e836691533e3c4088fdb10776b729b");

	// Token: 0x04001F97 RID: 8087
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_04 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_04.prefab:2e4544fa8c22f884bac0ac39c8f532c2");

	// Token: 0x04001F98 RID: 8088
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_05 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_05.prefab:fe6e5edff239439468bbb9010eacd983");

	// Token: 0x04001F99 RID: 8089
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_06 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_06.prefab:e790bfa98d7ddb74c98291acd203a3aa");

	// Token: 0x04001F9A RID: 8090
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_01.prefab:828de7f730eb81b46888d3b574abbd08");

	// Token: 0x04001F9B RID: 8091
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_02 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_02.prefab:b6a9def10b3457f49b9af0e8a1a77a60");

	// Token: 0x04001F9C RID: 8092
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_03 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_03.prefab:f42a1a17fb2fde249a467e44d4ad212a");

	// Token: 0x04001F9D RID: 8093
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_04 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_04.prefab:649dafe9e6a4b4842a43754f6e28a5ef");

	// Token: 0x04001F9E RID: 8094
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_05 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_05.prefab:2c5259eedbae90d4783c3fec86f31445");

	// Token: 0x04001F9F RID: 8095
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_06 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_06.prefab:baa804a050242ce458f50095a1dae149");

	// Token: 0x04001FA0 RID: 8096
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_02 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_02.prefab:96fa445d8b983f14a8411a2d6d34f5d8");

	// Token: 0x04001FA1 RID: 8097
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_04 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_04.prefab:189f72a1be840df43a88b50b971327ee");

	// Token: 0x04001FA2 RID: 8098
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_11 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_11.prefab:8adaa50a0eb7df349bc61382eb1c059c");

	// Token: 0x04001FA3 RID: 8099
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_18 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_18.prefab:51395490635b9e5479b827925921d3fb");

	// Token: 0x04001FA4 RID: 8100
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_12 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_12.prefab:d8a8edc6248318147bfd375b0204ee96");

	// Token: 0x04001FA5 RID: 8101
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_17 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_17.prefab:3772b50d815a43c4bbeaf0f9f8204687");

	// Token: 0x04001FA6 RID: 8102
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Survival_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Survival_01.prefab:89dfe02d2bbcdf94291beb2d0ed29c10");

	// Token: 0x04001FA7 RID: 8103
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Survival_02 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Survival_02.prefab:34fb848d6a6c0cd47bc6886ec765948a");

	// Token: 0x04001FA8 RID: 8104
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_UI_Boom_Coin4_Victory_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_UI_Boom_Coin4_Victory_01.prefab:214eb2193a8e2f74c939bc6660323069");

	// Token: 0x04001FA9 RID: 8105
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_08 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_08.prefab:456ba8208dd03b14bbaddb494d71b925");

	// Token: 0x04001FAA RID: 8106
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_01.prefab:fac7082fdc86c6f4b88f2541be028b90");
}
