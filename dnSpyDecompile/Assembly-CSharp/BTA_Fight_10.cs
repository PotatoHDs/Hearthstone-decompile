using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004F4 RID: 1268
public class BTA_Fight_10 : BTA_Dungeon
{
	// Token: 0x06004427 RID: 17447 RVA: 0x0016DED1 File Offset: 0x0016C0D1
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.DO_OPENING_TAUNTS,
				false
			}
		};
	}

	// Token: 0x06004428 RID: 17448 RVA: 0x001713EC File Offset: 0x0016F5EC
	public BTA_Fight_10()
	{
		this.m_gameOptions.AddBooleanOptions(BTA_Fight_10.s_booleanOptions);
	}

	// Token: 0x06004429 RID: 17449 RVA: 0x00171490 File Offset: 0x0016F690
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_10.VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_02B_01,
			BTA_Fight_10.VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_02D_01,
			BTA_Fight_10.VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_03A_01,
			BTA_Fight_10.VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_03C_01,
			BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_AldrachiWarblades_01,
			BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_PriestessofFury_01,
			BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_Renew_01,
			BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_BossStart_01,
			BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Emote_Response_01,
			BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_ChaosStrike_01,
			BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_FungalFortunes_01,
			BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_FuriousFelfin_01,
			BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_01,
			BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_02,
			BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_03,
			BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPowerFace_04,
			BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleA_01,
			BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleB_01,
			BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleC_01,
			BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_VictoryA_01,
			BTA_Fight_10.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_10_Bonding_02C_01,
			BTA_Fight_10.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_10_Bonding_03B_01,
			BTA_Fight_10.VO_BTA_BOSS_10h2_Male_Sporelok_Mission_Fight_10_Bonding_02A_01,
			BTA_Fight_10.VO_BTA_BOSS_10h2_Male_Sporelok_Mission_Fight_10_VictoryB_01,
			BTA_Fight_10.VO_BTA_BOSS_10h2_Male_Sporelok_Start_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600442A RID: 17450 RVA: 0x00171684 File Offset: 0x0016F884
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_10h_IdleLines;
	}

	// Token: 0x0600442B RID: 17451 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x0600442C RID: 17452 RVA: 0x0016D56A File Offset: 0x0016B76A
	public override void OnCreateGame()
	{
		base.OnCreateGame();
	}

	// Token: 0x0600442D RID: 17453 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600442E RID: 17454 RVA: 0x0017168C File Offset: 0x0016F88C
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BTA_Fight_10.VO_BTA_BOSS_10h2_Male_Sporelok_Start_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_BossStart_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x0600442F RID: 17455 RVA: 0x0017169C File Offset: 0x0016F89C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004430 RID: 17456 RVA: 0x00171724 File Offset: 0x0016F924
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 501)
		{
			if (missionEvent != 507)
			{
				if (missionEvent != 508)
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
				else
				{
					yield return base.PlayLineOnlyOnce(actor, BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPowerFace_04, 2.5f);
				}
			}
			else
			{
				yield return base.PlayRandomLineAlways(actor, this.m_missionEventTrigger100_Lines);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_VictoryA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BTA_Fight_10.VO_BTA_BOSS_10h2_Male_Sporelok_Mission_Fight_10_VictoryB_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004431 RID: 17457 RVA: 0x0017173A File Offset: 0x0016F93A
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "BT_036"))
		{
			if (!(cardId == "BTA_BOSS_10t"))
			{
				if (cardId == "BT_491")
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_FuriousFelfin_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_FungalFortunes_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_ChaosStrike_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004432 RID: 17458 RVA: 0x00171750 File Offset: 0x0016F950
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "BT_252"))
		{
			if (!(cardId == "BT_493"))
			{
				if (cardId == "BT_921")
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_AldrachiWarblades_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_PriestessofFury_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_Renew_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004433 RID: 17459 RVA: 0x00171766 File Offset: 0x0016F966
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 5)
		{
			if (turn == 7)
			{
				yield return base.PlayLineAlways(BTA_Dungeon.ArannaBrassRingInTraining, BTA_Fight_10.VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_03A_01, 2.5f);
				yield return base.PlayLineAlways(BTA_Dungeon.IllidanBrassRing, BTA_Fight_10.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_10_Bonding_03B_01, 2.5f);
				yield return base.PlayLineAlways(BTA_Dungeon.ArannaBrassRingInTraining, BTA_Fight_10.VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_03C_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_10.VO_BTA_BOSS_10h2_Male_Sporelok_Mission_Fight_10_Bonding_02A_01, 2.5f);
			yield return base.PlayLineAlways(BTA_Dungeon.ArannaBrassRingInTraining, BTA_Fight_10.VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_02B_01, 2.5f);
			yield return base.PlayLineAlways(BTA_Dungeon.IllidanBrassRing, BTA_Fight_10.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_10_Bonding_02C_01, 2.5f);
			yield return base.PlayLineAlways(BTA_Dungeon.ArannaBrassRingInTraining, BTA_Fight_10.VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_02D_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003686 RID: 13958
	private static Map<GameEntityOption, bool> s_booleanOptions = BTA_Fight_10.InitBooleanOptions();

	// Token: 0x04003687 RID: 13959
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_02B_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_02B_01.prefab:cb5520ea953cb7b4ca314641b40ea4d9");

	// Token: 0x04003688 RID: 13960
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_02D_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_02D_01.prefab:763aead63794a6644bea4eda919afc06");

	// Token: 0x04003689 RID: 13961
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_03A_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_03A_01.prefab:d48b0991caa45874f8a8ebfe3a3ae4c4");

	// Token: 0x0400368A RID: 13962
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_03C_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_10_Bonding_03C_01.prefab:ba8210b3032710a49a5f6238e1f24bb4");

	// Token: 0x0400368B RID: 13963
	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_AldrachiWarblades_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_AldrachiWarblades_01.prefab:d964e4522d5cf3440a56fef3bccb0136");

	// Token: 0x0400368C RID: 13964
	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_PriestessofFury_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_PriestessofFury_01.prefab:b9f57720ca5438d4f8fb28806edb411d");

	// Token: 0x0400368D RID: 13965
	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_Renew_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Boss_Renew_01.prefab:b380391f0f1c5f6488843546cf3a0097");

	// Token: 0x0400368E RID: 13966
	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_BossStart_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_BossStart_01.prefab:ba553daa3e1084f489220fef2c3f7490");

	// Token: 0x0400368F RID: 13967
	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Emote_Response_01.prefab:7934cc668d79e1f448618b18ce626b4c");

	// Token: 0x04003690 RID: 13968
	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_ChaosStrike_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_ChaosStrike_01.prefab:845a8061f83e00040a99043d723b82b2");

	// Token: 0x04003691 RID: 13969
	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_FungalFortunes_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_FungalFortunes_01.prefab:b8f75c717b47df94bbf4c09f4313d8e9");

	// Token: 0x04003692 RID: 13970
	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_FuriousFelfin_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_Hero_FuriousFelfin_01.prefab:7efd376874f3dda439611f1e2dd8b100");

	// Token: 0x04003693 RID: 13971
	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_01.prefab:39610ca2882f8e64b84afbd20acdf28a");

	// Token: 0x04003694 RID: 13972
	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_02 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_02.prefab:7a4a107635a9af04dbbb7168b1484934");

	// Token: 0x04003695 RID: 13973
	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_03 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_03.prefab:dbdcbddbeedb50d46ad96f0da5494492");

	// Token: 0x04003696 RID: 13974
	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPowerFace_04 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPowerFace_04.prefab:a383e95229214f447a7bd4806963ba63");

	// Token: 0x04003697 RID: 13975
	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleA_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleA_01.prefab:506ac8b0409b2614aa2c8c1aee12a2db");

	// Token: 0x04003698 RID: 13976
	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleB_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleB_01.prefab:4ff2c046236f5c44fb2eb12747e7617e");

	// Token: 0x04003699 RID: 13977
	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleC_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleC_01.prefab:38414059929a0d94f81e7d315c654885");

	// Token: 0x0400369A RID: 13978
	private static readonly AssetReference VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_VictoryA_01 = new AssetReference("VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_VictoryA_01.prefab:931b0079d78c00845b2a29e96105fa55");

	// Token: 0x0400369B RID: 13979
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_10_Bonding_02C_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_10_Bonding_02C_01.prefab:c8a7bab0d2d33114386b97230d95ca3d");

	// Token: 0x0400369C RID: 13980
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_10_Bonding_03B_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_10_Bonding_03B_01.prefab:4ed12ea3fbae14f498812ebe5a730151");

	// Token: 0x0400369D RID: 13981
	private static readonly AssetReference VO_BTA_BOSS_10h2_Male_Sporelok_Mission_Fight_10_Bonding_02A_01 = new AssetReference("VO_BTA_BOSS_10h2_Male_Sporelok_Mission_Fight_10_Bonding_02A_01.prefab:a3cacdf37e880e54f9f1b2012b20501b");

	// Token: 0x0400369E RID: 13982
	private static readonly AssetReference VO_BTA_BOSS_10h2_Male_Sporelok_Mission_Fight_10_VictoryB_01 = new AssetReference("VO_BTA_BOSS_10h2_Male_Sporelok_Mission_Fight_10_VictoryB_01.prefab:ca7dc7abc3fa24a46a70aa193a5df3f5");

	// Token: 0x0400369F RID: 13983
	private static readonly AssetReference VO_BTA_BOSS_10h2_Male_Sporelok_Start_01 = new AssetReference("VO_BTA_BOSS_10h2_Male_Sporelok_Start_01.prefab:fb33f3d48876c414489f39b68bcb3b3a");

	// Token: 0x040036A0 RID: 13984
	private List<string> m_VO_BTA_BOSS_10h_IdleLines = new List<string>
	{
		BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleA_01,
		BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleB_01,
		BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_IdleC_01
	};

	// Token: 0x040036A1 RID: 13985
	private List<string> m_missionEventTrigger100_Lines = new List<string>
	{
		BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_01,
		BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_02,
		BTA_Fight_10.VO_BTA_BOSS_10h_Male_Orc_Mission_Fight_10_HeroPower_03
	};

	// Token: 0x040036A2 RID: 13986
	private HashSet<string> m_playedLines = new HashSet<string>();
}
