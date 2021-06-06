using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200040D RID: 1037
public class BOTA_Clear_Boom : BOTA_MissionEntity
{
	// Token: 0x06003924 RID: 14628 RVA: 0x0011F258 File Offset: 0x0011D458
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_01,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_02,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_04,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_05,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_06,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_01,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_02,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_03,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_04,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_05,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_06,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_02,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_04,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_11,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_18,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_12,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_17,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Clear_02,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_01,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_UI_Boom_Coin1_End_01,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Trigger_Thermaplug_01,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_15,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_13
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003925 RID: 14629 RVA: 0x0011F420 File Offset: 0x0011D620
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BOTFinalBoss);
	}

	// Token: 0x06003926 RID: 14630 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x06003927 RID: 14631 RVA: 0x0011F434 File Offset: 0x0011D634
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_01;
		BOTA_MissionEntity.s_returnLine = BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_13;
		this.s_victoryLine_1 = null;
		this.s_victoryLine_2 = BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Clear_02;
		this.s_victoryLine_3 = null;
		this.s_victoryLine_4 = BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_15;
		this.s_victoryLine_5 = null;
		this.s_victoryLine_6 = null;
		this.s_victoryLine_7 = null;
		this.s_victoryLine_8 = null;
		this.s_victoryLine_9 = null;
		this.s_emoteLines = new List<string>
		{
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_12,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_17
		};
		this.s_idleLines = new List<string>
		{
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_01,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_02,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_03,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_04,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_05,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Idle_06
		};
		this.s_restartLines = new List<string>
		{
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_01,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_02,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_04,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_05,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Failure_06,
			BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_18
		};
	}

	// Token: 0x06003928 RID: 14632 RVA: 0x0011F5B7 File Offset: 0x0011D7B7
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield break;
	}

	// Token: 0x06003929 RID: 14633 RVA: 0x0011F5BF File Offset: 0x0011D7BF
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "GVG_116")
		{
			yield return base.PlayEasterEggLine(actor, BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_Trigger_Thermaplug_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600392A RID: 14634 RVA: 0x0011F5D5 File Offset: 0x0011D7D5
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return base.PlayClosingLine("DrBoom_Banner_Quote.prefab:ff8653a27a00c464ea5552e3c6b6dc5c", BOTA_Clear_Boom.VO_BOTA_BOSS_20h_Male_Goblin_UI_Boom_Coin1_End_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04001DEA RID: 7658
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_01.prefab:b20d52247bbde0d42bbefc64782157b5");

	// Token: 0x04001DEB RID: 7659
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_02 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_02.prefab:52e836691533e3c4088fdb10776b729b");

	// Token: 0x04001DEC RID: 7660
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_04 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_04.prefab:2e4544fa8c22f884bac0ac39c8f532c2");

	// Token: 0x04001DED RID: 7661
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_05 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_05.prefab:fe6e5edff239439468bbb9010eacd983");

	// Token: 0x04001DEE RID: 7662
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Failure_06 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Failure_06.prefab:e790bfa98d7ddb74c98291acd203a3aa");

	// Token: 0x04001DEF RID: 7663
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_01.prefab:828de7f730eb81b46888d3b574abbd08");

	// Token: 0x04001DF0 RID: 7664
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_02 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_02.prefab:b6a9def10b3457f49b9af0e8a1a77a60");

	// Token: 0x04001DF1 RID: 7665
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_03 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_03.prefab:f42a1a17fb2fde249a467e44d4ad212a");

	// Token: 0x04001DF2 RID: 7666
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_04 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_04.prefab:649dafe9e6a4b4842a43754f6e28a5ef");

	// Token: 0x04001DF3 RID: 7667
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_05 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_05.prefab:2c5259eedbae90d4783c3fec86f31445");

	// Token: 0x04001DF4 RID: 7668
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Idle_06 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Idle_06.prefab:baa804a050242ce458f50095a1dae149");

	// Token: 0x04001DF5 RID: 7669
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_02 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_02.prefab:96fa445d8b983f14a8411a2d6d34f5d8");

	// Token: 0x04001DF6 RID: 7670
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_04 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_04.prefab:189f72a1be840df43a88b50b971327ee");

	// Token: 0x04001DF7 RID: 7671
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_11 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_11.prefab:8adaa50a0eb7df349bc61382eb1c059c");

	// Token: 0x04001DF8 RID: 7672
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_18 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_18.prefab:51395490635b9e5479b827925921d3fb");

	// Token: 0x04001DF9 RID: 7673
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_12 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_12.prefab:d8a8edc6248318147bfd375b0204ee96");

	// Token: 0x04001DFA RID: 7674
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_17 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_17.prefab:3772b50d815a43c4bbeaf0f9f8204687");

	// Token: 0x04001DFB RID: 7675
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Clear_02 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Clear_02.prefab:9fb97d28f74b0e44a8f1d4650b753e05");

	// Token: 0x04001DFC RID: 7676
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_01.prefab:fac7082fdc86c6f4b88f2541be028b90");

	// Token: 0x04001DFD RID: 7677
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_UI_Boom_Coin1_End_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_UI_Boom_Coin1_End_01.prefab:a1877ed45c945ae4dbef3c731515cade");

	// Token: 0x04001DFE RID: 7678
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_15 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_15.prefab:bc29bc9c1caa41e49a3f209c8688164d");

	// Token: 0x04001DFF RID: 7679
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_13 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_One_Liner_13.prefab:37a96caf5c628f048b251cbd9891237d");

	// Token: 0x04001E00 RID: 7680
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_Trigger_Thermaplug_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_Trigger_Thermaplug_01.prefab:9532f6101d8da4044bd980f4fe0aa7bc");

	// Token: 0x04001E01 RID: 7681
	private HashSet<string> m_playedLines = new HashSet<string>();
}
