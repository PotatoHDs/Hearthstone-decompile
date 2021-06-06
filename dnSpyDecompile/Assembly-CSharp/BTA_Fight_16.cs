using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004FA RID: 1274
public class BTA_Fight_16 : BTA_Dungeon
{
	// Token: 0x0600448B RID: 17547 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x0600448C RID: 17548 RVA: 0x00173428 File Offset: 0x00171628
	public BTA_Fight_16()
	{
		this.m_gameOptions.AddBooleanOptions(BTA_Fight_16.s_booleanOptions);
	}

	// Token: 0x0600448D RID: 17549 RVA: 0x00173544 File Offset: 0x00171744
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_16.VO_BTA_01_Female_NightElf_Mission_Fight_16_MidpointB_01,
			BTA_Fight_16.VO_BTA_01_Female_NightElf_Mission_Fight_16_PlayerStart_01,
			BTA_Fight_16.VO_BTA_01_Female_NightElf_Mission_Fight_16_Turn1_Story_01,
			BTA_Fight_16.VO_BTA_01_Female_NightElf_Mission_Fight_16_VictoryB_01,
			BTA_Fight_16.VO_BTA_01_Female_NightElf_Mission_Fight_16_VictoryD_01,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_Attack_01,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_DarkPortal_01,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_Deteriorate_01,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_EndlessLegion_01,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_RustedFungalGiant_01,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_BossDeath_01,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_BossStart_01,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Emote_Response_01,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Blur_01,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Demon_01,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_HiddenAmulet_01,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_InnerDemon_01,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Sklibb_01,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_SpectralSight_01,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_01,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_02,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_03,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_01,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_02,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_03,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_IdleA_01,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_IdleB_01,
			BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_MidpointA_01,
			BTA_Fight_16.VO_BTA_BOSS_17h_Male_NightElf_Misc_01,
			BTA_Fight_16.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_16_VictoryA_01,
			BTA_Fight_16.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_16_VictoryC_01,
			BTA_Fight_16.VO_BTA_08_Male_Orc_Misc_03,
			BTA_Fight_16.VO_BTA_10_Female_Naga_Misc_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600448E RID: 17550 RVA: 0x001737B8 File Offset: 0x001719B8
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_16h_IdleLines;
	}

	// Token: 0x0600448F RID: 17551 RVA: 0x001737C0 File Offset: 0x001719C0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_BossDeath_01;
	}

	// Token: 0x06004490 RID: 17552 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004491 RID: 17553 RVA: 0x001737D8 File Offset: 0x001719D8
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BTA_Fight_16.VO_BTA_01_Female_NightElf_Mission_Fight_16_PlayerStart_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_BossStart_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004492 RID: 17554 RVA: 0x001737E8 File Offset: 0x001719E8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004493 RID: 17555 RVA: 0x00173870 File Offset: 0x00171A70
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 100:
			yield return base.PlayRandomLineAlways(actor, this.m_missionEventTrigger100_Lines);
			break;
		case 101:
			yield return base.PlayRandomLineAlways(actor, this.m_missionEventTrigger101_Lines);
			break;
		case 102:
			yield return base.PlayLineAlways(actor, BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_MidpointA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BTA_Fight_16.VO_BTA_01_Female_NightElf_Mission_Fight_16_MidpointB_01, 2.5f);
			break;
		case 103:
			yield return base.PlayLineAlways(actor, BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Demon_01, 2.5f);
			break;
		case 104:
			yield return base.PlayLineAlways(actor, BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_HiddenAmulet_01, 2.5f);
			break;
		case 105:
			yield return base.PlayLineAlways(BTA_Dungeon.IllidanBrassRing, BTA_Fight_16.VO_BTA_BOSS_17h_Male_NightElf_Misc_01, 2.5f);
			break;
		default:
			if (missionEvent != 500)
			{
				if (missionEvent != 501)
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
				else
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(BTA_Dungeon.IllidanBrassRing, BTA_Fight_16.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_16_VictoryA_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BTA_Fight_16.VO_BTA_01_Female_NightElf_Mission_Fight_16_VictoryB_01, 2.5f);
					yield return base.PlayLineAlways(BTA_Dungeon.IllidanBrassRing, BTA_Fight_16.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_16_VictoryC_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BTA_Fight_16.VO_BTA_01_Female_NightElf_Mission_Fight_16_VictoryD_01, 2.5f);
					GameState.Get().SetBusy(false);
				}
			}
			else
			{
				base.PlaySound(BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_Attack_01, 1f, true, false);
			}
			break;
		}
		yield break;
	}

	// Token: 0x06004494 RID: 17556 RVA: 0x00173886 File Offset: 0x00171A86
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
		if (!(cardId == "BT_491"))
		{
			if (!(cardId == "BT_512"))
			{
				if (!(cardId == "BT_752"))
				{
					if (!(cardId == "BTA_05") && !(cardId == "BTA_06"))
					{
						if (cardId == "BTA_BOSS_15s")
						{
							yield return base.PlayLineAlways(actor, BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_HiddenAmulet_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineAlways(actor, BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Sklibb_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Blur_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_InnerDemon_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_SpectralSight_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004495 RID: 17557 RVA: 0x0017389C File Offset: 0x00171A9C
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
		if (!(cardId == "BT_302"))
		{
			if (!(cardId == "BTA_13"))
			{
				if (!(cardId == "BTA_15"))
				{
					if (cardId == "BTA_16")
					{
						yield return base.PlayLineAlways(actor, BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_RustedFungalGiant_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_EndlessLegion_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_Deteriorate_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_DarkPortal_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004496 RID: 17558 RVA: 0x001738B2 File Offset: 0x00171AB2
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 1)
		{
			if (turn == 5)
			{
				yield return base.PlayLineAlways(BTA_Dungeon.IllidanBrassRing, BTA_Fight_16.VO_BTA_BOSS_17h_Male_NightElf_Misc_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_16.VO_BTA_01_Female_NightElf_Mission_Fight_16_Turn1_Story_01, 2.5f);
			yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("BTA_07"), BTA_Dungeon.KarnukBrassRingDemonHunter, BTA_Fight_16.VO_BTA_08_Male_Orc_Misc_03, 2.5f);
			yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("BTA_09"), BTA_Dungeon.ShaljaBrassRingDemonHunter, BTA_Fight_16.VO_BTA_10_Female_Naga_Misc_02, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004497 RID: 17559 RVA: 0x001738C8 File Offset: 0x00171AC8
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BT_FinalBoss);
	}

	// Token: 0x04003732 RID: 14130
	private static Map<GameEntityOption, bool> s_booleanOptions = BTA_Fight_16.InitBooleanOptions();

	// Token: 0x04003733 RID: 14131
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_16_MidpointB_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_16_MidpointB_01.prefab:f64b46a1aff11e843b96aa93b21ede1d");

	// Token: 0x04003734 RID: 14132
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_16_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_16_PlayerStart_01.prefab:e2dbfb9e1dc9c1b41ac180cbf545b59b");

	// Token: 0x04003735 RID: 14133
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_16_Turn1_Story_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_16_Turn1_Story_01.prefab:efd902b4f0990ca499a148afc18c7f44");

	// Token: 0x04003736 RID: 14134
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_16_VictoryB_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_16_VictoryB_01.prefab:bbaee13c2a2b7344e89dc6b03f7cae44");

	// Token: 0x04003737 RID: 14135
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_16_VictoryD_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_16_VictoryD_01.prefab:05e1d5d9a8d854641b15ebd3ef332b0a");

	// Token: 0x04003738 RID: 14136
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_Attack_01.prefab:9f4fdf57b47edc44fbd30866fe3b1dc3");

	// Token: 0x04003739 RID: 14137
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_DarkPortal_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_DarkPortal_01.prefab:7f6f762e90266d34e909f2d95afaefe3");

	// Token: 0x0400373A RID: 14138
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_Deteriorate_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_Deteriorate_01.prefab:5149d2b4dfd48e549a4a1ddfdb97041e");

	// Token: 0x0400373B RID: 14139
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_EndlessLegion_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_EndlessLegion_01.prefab:21d459a18887fb5429e5f31803f2737a");

	// Token: 0x0400373C RID: 14140
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_RustedFungalGiant_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Boss_RustedFungalGiant_01.prefab:ab46822842175544c9042eb73a2af8d7");

	// Token: 0x0400373D RID: 14141
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_BossDeath_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_BossDeath_01.prefab:706539eed8f118646a8c6d49a3f706c8");

	// Token: 0x0400373E RID: 14142
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_BossStart_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_BossStart_01.prefab:b31b801186a95ec47a8dfa5dc5073f5f");

	// Token: 0x0400373F RID: 14143
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Emote_Response_01.prefab:5cd807b17b5029c4782d0459ceef1428");

	// Token: 0x04003740 RID: 14144
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Blur_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Blur_01.prefab:85b57a6352934464eb4bdcc2b75f174b");

	// Token: 0x04003741 RID: 14145
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Demon_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Demon_01.prefab:62c2fd4daf05fb243a4315a1fb60f6b6");

	// Token: 0x04003742 RID: 14146
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_HiddenAmulet_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_HiddenAmulet_01.prefab:e8f593ffa739dee498d0227ba5df56ae");

	// Token: 0x04003743 RID: 14147
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_InnerDemon_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_InnerDemon_01.prefab:e57bf87b57088044a9f5aacae1cc24c7");

	// Token: 0x04003744 RID: 14148
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Sklibb_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_Sklibb_01.prefab:258a9924e626b884e9297bdc1a171013");

	// Token: 0x04003745 RID: 14149
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_SpectralSight_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_Hero_SpectralSight_01.prefab:0fc4409a36b989f41b161ab6bde3faab");

	// Token: 0x04003746 RID: 14150
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_01.prefab:deb0bf29ac4f84e4399e8dbed4a31296");

	// Token: 0x04003747 RID: 14151
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_02 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_02.prefab:eacb7147dd317774480528c83a68ce69");

	// Token: 0x04003748 RID: 14152
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_03 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_03.prefab:36f6d2629f891b14b8b772f1e8075f87");

	// Token: 0x04003749 RID: 14153
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_01.prefab:2c1c56acc0bd5884db2f6e50c2553bd1");

	// Token: 0x0400374A RID: 14154
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_02 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_02.prefab:f668fb355f4c178428ccd7a4f011cdda");

	// Token: 0x0400374B RID: 14155
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_03 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_03.prefab:98d83b8ae4554e9459345385a50e7b62");

	// Token: 0x0400374C RID: 14156
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_IdleA_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_IdleA_01.prefab:7100b25017ab8ee4b8fe844f64470f7f");

	// Token: 0x0400374D RID: 14157
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_IdleB_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_IdleB_01.prefab:bcec9f995795fe3439132fe644f6da0a");

	// Token: 0x0400374E RID: 14158
	private static readonly AssetReference VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_MidpointA_01 = new AssetReference("VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_MidpointA_01.prefab:b6d448c4b845ddc4da9ccb2f07f33739");

	// Token: 0x0400374F RID: 14159
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Misc_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Misc_01.prefab:1aa5cefd36c6a0440831c1307d997c34");

	// Token: 0x04003750 RID: 14160
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_16_VictoryA_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_16_VictoryA_01.prefab:59cc24da20e79ef40b1d1727b6b28738");

	// Token: 0x04003751 RID: 14161
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_16_VictoryC_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_16_VictoryC_01.prefab:b13583b14cfb088458c76ffcef37184d");

	// Token: 0x04003752 RID: 14162
	private static readonly AssetReference VO_BTA_10_Female_Naga_Misc_02 = new AssetReference("VO_BTA_10_Female_Naga_Misc_02.prefab:47b149080b9211f4a9d561372fc3ed57");

	// Token: 0x04003753 RID: 14163
	private static readonly AssetReference VO_BTA_08_Male_Orc_Misc_03 = new AssetReference("VO_BTA_08_Male_Orc_Misc_03.prefab:0beffd1b2e61ee24ea6ae52d8ea26fba");

	// Token: 0x04003754 RID: 14164
	private List<string> m_VO_BTA_BOSS_16h_IdleLines = new List<string>
	{
		BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_IdleA_01,
		BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_IdleB_01
	};

	// Token: 0x04003755 RID: 14165
	private List<string> m_missionEventTrigger501_Lines = new List<string>
	{
		BTA_Fight_16.VO_BTA_01_Female_NightElf_Mission_Fight_16_VictoryB_01,
		BTA_Fight_16.VO_BTA_01_Female_NightElf_Mission_Fight_16_VictoryD_01,
		BTA_Fight_16.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_16_VictoryA_01,
		BTA_Fight_16.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_16_VictoryC_01
	};

	// Token: 0x04003756 RID: 14166
	private List<string> m_missionEventTrigger100_Lines = new List<string>
	{
		BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_01,
		BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_02,
		BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerFelTrigger_03
	};

	// Token: 0x04003757 RID: 14167
	private List<string> m_missionEventTrigger101_Lines = new List<string>
	{
		BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_01,
		BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_02,
		BTA_Fight_16.VO_BTA_BOSS_16h_Male_Demon_Mission_Fight_16_HeroPowerHatredTrigger_03
	};

	// Token: 0x04003758 RID: 14168
	private HashSet<string> m_playedLines = new HashSet<string>();
}
