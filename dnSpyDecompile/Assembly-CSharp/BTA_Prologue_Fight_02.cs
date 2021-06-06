using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200050A RID: 1290
public class BTA_Prologue_Fight_02 : BTA_Prologue_Dungeon
{
	// Token: 0x0600458F RID: 17807 RVA: 0x00177FBC File Offset: 0x001761BC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Prologue_Fight_02.VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Sargeras01_01,
			BTA_Prologue_Fight_02.VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Sargeras02_01,
			BTA_Prologue_Fight_02.VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Victory03_01,
			BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_DemonClawsAlt_01,
			BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeA_01_01,
			BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeA_03_01,
			BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeB_01_01,
			BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeC_02_01,
			BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeD_01,
			BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Intro_01_01,
			BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Intro_03_01,
			BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Turn2_01,
			BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Victory01_01,
			BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Death_01,
			BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_EmoteResponse_01,
			BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_01_01,
			BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_02_01,
			BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_03_01,
			BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_01_01,
			BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_02_01,
			BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_03_01,
			BTA_Prologue_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeA_02_01,
			BTA_Prologue_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeB_02_01,
			BTA_Prologue_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeC_01_01,
			BTA_Prologue_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Intro_02_01,
			BTA_Prologue_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory02_01,
			BTA_Prologue_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory04_01,
			BTA_Prologue_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory05_01,
			BTA_Prologue_Fight_02.Prologue_Illidan_Transform
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004590 RID: 17808 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004591 RID: 17809 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004592 RID: 17810 RVA: 0x001781F0 File Offset: 0x001763F0
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_Lines;
	}

	// Token: 0x06004593 RID: 17811 RVA: 0x001781F8 File Offset: 0x001763F8
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_Lines;
	}

	// Token: 0x06004594 RID: 17812 RVA: 0x00178200 File Offset: 0x00176400
	public override void OnCreateGame()
	{
		base.OnCreateGame();
	}

	// Token: 0x06004595 RID: 17813 RVA: 0x00178208 File Offset: 0x00176408
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Intro_03_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004596 RID: 17814 RVA: 0x00178299 File Offset: 0x00176499
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 911)
		{
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(false);
			yield break;
		}
		switch (missionEvent)
		{
		case 100:
			yield return base.PlayLineAlways(enemyActor, BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_DemonClawsAlt_01, 2.5f);
			break;
		case 101:
			yield return base.PlayLineOnlyOnce(BTA_Prologue_Fight_02.SargerasBrassRing, BTA_Prologue_Fight_02.VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Sargeras02_01, 2.5f);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(BTA_Prologue_Fight_02.SargerasBrassRing, BTA_Prologue_Fight_02.VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Sargeras01_01, 2.5f);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_Lines);
			break;
		default:
			if (missionEvent != 228)
			{
				switch (missionEvent)
				{
				case 501:
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(enemyActor, BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Victory01_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory02_01, 2.5f);
					yield return base.PlayLineAlways(BTA_Prologue_Fight_02.SargerasBrassRing, BTA_Prologue_Fight_02.VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Victory03_01, 2.5f);
					base.PlaySound(BTA_Prologue_Fight_02.Prologue_Illidan_Transform, 1f, true, false);
					GameState.Get().SetBusy(false);
					break;
				case 502:
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory05_01, 2.5f);
					GameState.Get().SetBusy(false);
					break;
				case 503:
					yield return base.PlayLineAlways(enemyActor, BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Intro_01_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Intro_02_01, 2.5f);
					yield return base.PlayLineAlways(enemyActor, BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Intro_03_01, 2.5f);
					break;
				default:
					yield return base.HandleMissionEventWithTiming(missionEvent);
					break;
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				this.ShowMinionMoveTutorial();
				yield return new WaitForSeconds(3f);
				this.HideNotification(this.m_minionMoveTutorialNotification, false);
				GameState.Get().SetBusy(false);
			}
			break;
		}
		yield break;
	}

	// Token: 0x06004597 RID: 17815 RVA: 0x001782AF File Offset: 0x001764AF
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
		yield break;
	}

	// Token: 0x06004598 RID: 17816 RVA: 0x001782C5 File Offset: 0x001764C5
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
		yield break;
	}

	// Token: 0x06004599 RID: 17817 RVA: 0x001782DB File Offset: 0x001764DB
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (turn)
		{
		case 2:
			yield return base.PlayLineAlways(enemyActor, BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Turn2_01, 2.5f);
			break;
		case 3:
			this.m_shouldPlayMinionMoveTutorial = false;
			break;
		case 4:
			yield return base.PlayLineAlways(enemyActor, BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeA_01_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeA_02_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeA_03_01, 2.5f);
			break;
		case 6:
			yield return base.PlayLineAlways(enemyActor, BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeB_01_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeB_02_01, 2.5f);
			break;
		case 8:
			yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeC_01_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeC_02_01, 2.5f);
			break;
		case 10:
			yield return base.PlayLineAlways(enemyActor, BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeD_01, 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x0600459A RID: 17818 RVA: 0x001782F4 File Offset: 0x001764F4
	protected void ShowMinionMoveTutorial()
	{
		Card leftMostMinionInOpponentPlay = this.GetLeftMostMinionInOpponentPlay();
		if (leftMostMinionInOpponentPlay == null)
		{
			return;
		}
		Vector3 position = leftMostMinionInOpponentPlay.transform.position;
		Vector3 position2;
		if (UniversalInputManager.UsePhoneUI)
		{
			position2 = new Vector3(position.x + 0.05f, position.y, position.z + 2.6f);
		}
		else
		{
			position2 = new Vector3(position.x, position.y, position.z + 2.5f);
		}
		string key = "DEMON_HUNTER_PROLOGUE_2_PORTAL_POPUP";
		this.m_minionMoveTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key), true, NotificationManager.PopupTextType.BASIC);
		this.m_minionMoveTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
		this.m_minionMoveTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	// Token: 0x0600459B RID: 17819 RVA: 0x001783B1 File Offset: 0x001765B1
	private IEnumerator ShowOrHideMoveMinionTutorial()
	{
		while (!InputManager.Get().GetHeldCard())
		{
			yield return null;
		}
		this.HideNotification(this.m_minionMoveTutorialNotification, false);
		while (InputManager.Get().GetHeldCard())
		{
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		if ((this.GetLeftMostMinionInOpponentPlay() != null || InputManager.Get().GetHeldCard()) && this.m_shouldPlayMinionMoveTutorial)
		{
			this.ShowMinionMoveTutorial();
			GameEntity.Coroutines.StartCoroutine(this.ShowOrHideMoveMinionTutorial());
		}
		yield break;
	}

	// Token: 0x0600459C RID: 17820 RVA: 0x001783C0 File Offset: 0x001765C0
	protected Card GetLeftMostMinionInFriendlyPlay()
	{
		foreach (Card card in GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone().GetCards())
		{
			if (card.GetEntity().GetTag(GAME_TAG.ZONE_POSITION) == 1)
			{
				return card;
			}
		}
		return null;
	}

	// Token: 0x0600459D RID: 17821 RVA: 0x00178434 File Offset: 0x00176634
	protected Card GetLeftMostMinionInOpponentPlay()
	{
		foreach (Card card in GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().GetCards())
		{
			if (card.GetEntity().GetTag(GAME_TAG.ZONE_POSITION) == 1)
			{
				return card;
			}
		}
		return null;
	}

	// Token: 0x0600459E RID: 17822 RVA: 0x001784A8 File Offset: 0x001766A8
	protected void HideNotification(Notification notification, bool hideImmediately = false)
	{
		if (notification != null)
		{
			if (hideImmediately)
			{
				NotificationManager.Get().DestroyNotificationNowWithNoAnim(notification);
				return;
			}
			NotificationManager.Get().DestroyNotification(notification, 0f);
		}
	}

	// Token: 0x0400385F RID: 14431
	private static readonly AssetReference VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Sargeras01_01 = new AssetReference("VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Sargeras01_01.prefab:74be5edfc12439647ac7defe964d741a");

	// Token: 0x04003860 RID: 14432
	private static readonly AssetReference VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Sargeras02_01 = new AssetReference("VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Sargeras02_01.prefab:bbdd09d8340330a4c98cc94d594dec8e");

	// Token: 0x04003861 RID: 14433
	private static readonly AssetReference VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Victory03_01 = new AssetReference("VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Victory03_01.prefab:24378fc3895fba8428a62f8d7d2005cc");

	// Token: 0x04003862 RID: 14434
	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_DemonClawsAlt_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_DemonClawsAlt_01.prefab:48951c1bf63e6e146af0cc7bde803be0");

	// Token: 0x04003863 RID: 14435
	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeA_01_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeA_01_01.prefab:f7b32f91666775141808f2ec32b6f414");

	// Token: 0x04003864 RID: 14436
	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeA_03_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeA_03_01.prefab:da74a964ef4efea4f977682fcce22760");

	// Token: 0x04003865 RID: 14437
	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeB_01_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeB_01_01.prefab:01ebed9a93ec55748adf083ef2ff1fc2");

	// Token: 0x04003866 RID: 14438
	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeC_02_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeC_02_01.prefab:047b9de97099ae640a6a6a151ebfd2d9");

	// Token: 0x04003867 RID: 14439
	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeD_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeD_01.prefab:07670d1cc3eaecf43950b15ad5072b3b");

	// Token: 0x04003868 RID: 14440
	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Intro_01_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Intro_01_01.prefab:a86daaaa3809c2e4da7d063b3c3ca9c3");

	// Token: 0x04003869 RID: 14441
	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Intro_03_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Intro_03_01.prefab:6b609c6f5b9526047b08375b6f17caf7");

	// Token: 0x0400386A RID: 14442
	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Turn2_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Turn2_01.prefab:9ff6e38ed8f2bbe43a71fae65e4abe21");

	// Token: 0x0400386B RID: 14443
	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Victory01_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Victory01_01.prefab:5e892bc2f5e5c8f4cbcfb6f3bd39d9d0");

	// Token: 0x0400386C RID: 14444
	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Death_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Death_01.prefab:cbac973d6f7c71f419f766cb251600ef");

	// Token: 0x0400386D RID: 14445
	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_EmoteResponse_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_EmoteResponse_01.prefab:3dd18a8c149ed5f499d697c48d23f83a");

	// Token: 0x0400386E RID: 14446
	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_01_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_01_01.prefab:859b20692e41891499fb35d42c97aeac");

	// Token: 0x0400386F RID: 14447
	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_02_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_02_01.prefab:5ddd4a5abda7d57468029cc98c8b201b");

	// Token: 0x04003870 RID: 14448
	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_03_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_03_01.prefab:e4a71c2d5cba9704396bc8232ac401dc");

	// Token: 0x04003871 RID: 14449
	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_01_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_01_01.prefab:82b7793375cc52c4bb96fbfffdd0f4d8");

	// Token: 0x04003872 RID: 14450
	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_02_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_02_01.prefab:1bd8e72a8e2fcec4e86cf569202059f2");

	// Token: 0x04003873 RID: 14451
	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_03_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_03_01.prefab:ef069b11ece3b6a429d2522aeca40a75");

	// Token: 0x04003874 RID: 14452
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeA_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeA_02_01.prefab:37a3f6d52b4912f48adc553a9b5e79a4");

	// Token: 0x04003875 RID: 14453
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeB_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeB_02_01.prefab:08c7a0338aeee5d42aedeb121430bc1f");

	// Token: 0x04003876 RID: 14454
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeC_01_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeC_01_01.prefab:487d4490f2bf29a46b68c72939650211");

	// Token: 0x04003877 RID: 14455
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Intro_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Intro_02_01.prefab:1afba87c22e4b3543ae5af458a3688c2");

	// Token: 0x04003878 RID: 14456
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory02_01.prefab:cec94069b80d73b4298ee794421ff850");

	// Token: 0x04003879 RID: 14457
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory04_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory04_01.prefab:787ab7c773b6ab14d97f31a80e3441b4");

	// Token: 0x0400387A RID: 14458
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory05_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory05_01.prefab:9f6e88a19c3fb3e409eff69cb5c84b75");

	// Token: 0x0400387B RID: 14459
	private static readonly AssetReference Prologue_Illidan_Transform = new AssetReference("Prologue_Illidan_Transform.prefab:f2e213201d6f63840b0f096592dd548e");

	// Token: 0x0400387C RID: 14460
	protected Notification m_minionMoveTutorialNotification;

	// Token: 0x0400387D RID: 14461
	protected bool m_shouldPlayMinionMoveTutorial = true;

	// Token: 0x0400387E RID: 14462
	public static readonly AssetReference SargerasBrassRing = new AssetReference("Sargeras_Popup_BrassRing.prefab:df705ac0326836746af538133a79b587");

	// Token: 0x0400387F RID: 14463
	private List<string> m_VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_Lines = new List<string>
	{
		BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_01_01,
		BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_02_01,
		BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_03_01
	};

	// Token: 0x04003880 RID: 14464
	private List<string> m_VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_Lines = new List<string>
	{
		BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_01_01,
		BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_02_01,
		BTA_Prologue_Fight_02.VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_03_01
	};

	// Token: 0x04003881 RID: 14465
	private HashSet<string> m_playedLines = new HashSet<string>();
}
