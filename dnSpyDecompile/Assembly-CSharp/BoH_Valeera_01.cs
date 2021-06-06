using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000554 RID: 1364
public class BoH_Valeera_01 : BoH_Valeera_Dungeon
{
	// Token: 0x06004B4B RID: 19275 RVA: 0x0018F0E0 File Offset: 0x0018D2E0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1EmoteResponse_01,
			BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1ExchangeC_01,
			BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_01,
			BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_02,
			BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_03,
			BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_01,
			BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_02,
			BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_03,
			BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Intro_01,
			BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Loss_01,
			BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Victory_01,
			BoH_Valeera_01.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeA_02,
			BoH_Valeera_01.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeB_01,
			BoH_Valeera_01.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeB_03,
			BoH_Valeera_01.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeC_03,
			BoH_Valeera_01.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1Intro_02,
			BoH_Valeera_01.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1Victory_01,
			BoH_Valeera_01.VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission1ExchangeB_02,
			BoH_Valeera_01.VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission1ExchangeC_04,
			BoH_Valeera_01.VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeA_01,
			BoH_Valeera_01.VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeA_03,
			BoH_Valeera_01.VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeC_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004B4C RID: 19276 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004B4D RID: 19277 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004B4E RID: 19278 RVA: 0x0018F2A4 File Offset: 0x0018D4A4
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Valeera_01.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1Intro_02);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004B4F RID: 19279 RVA: 0x0018F2B3 File Offset: 0x0018D4B3
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x06004B50 RID: 19280 RVA: 0x0018F2BB File Offset: 0x0018D4BB
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004B51 RID: 19281 RVA: 0x0018F2C3 File Offset: 0x0018D4C3
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_TRL;
		this.m_standardEmoteResponseLine = BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1EmoteResponse_01;
	}

	// Token: 0x06004B52 RID: 19282 RVA: 0x0018F2E8 File Offset: 0x0018D4E8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004B53 RID: 19283 RVA: 0x0018F36C File Offset: 0x0018D56C
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
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
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 101)
		{
			if (missionEvent != 228)
			{
				if (missionEvent != 504)
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
				else
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(actor, BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Victory_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_01.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1Victory_01, 2.5f);
					GameState.Get().SetBusy(false);
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
		}
		else
		{
			yield return base.PlayLineAlways(actor, BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Loss_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004B54 RID: 19284 RVA: 0x0018F382 File Offset: 0x0018D582
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
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x06004B55 RID: 19285 RVA: 0x0018F398 File Offset: 0x0018D598
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
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x06004B56 RID: 19286 RVA: 0x0018F3AE File Offset: 0x0018D5AE
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 3)
		{
			if (turn != 7)
			{
				if (turn == 11)
				{
					yield return base.PlayLineAlways(actor, BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1ExchangeC_01, 2.5f);
					yield return base.PlayLineAlways(BoH_Valeera_01.BrollBrassRing, BoH_Valeera_01.VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeC_02, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_01.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeC_03, 2.5f);
					yield return base.PlayLineAlways(BoH_Valeera_01.LoGoshBrassRing, BoH_Valeera_01.VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission1ExchangeC_04, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_01.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(BoH_Valeera_01.LoGoshBrassRing, BoH_Valeera_01.VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission1ExchangeB_02, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_01.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeB_03, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(BoH_Valeera_01.BrollBrassRing, BoH_Valeera_01.VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_01.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeA_02, 2.5f);
			yield return base.PlayLineAlways(BoH_Valeera_01.BrollBrassRing, BoH_Valeera_01.VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeA_03, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004B57 RID: 19287 RVA: 0x0018F3C4 File Offset: 0x0018D5C4
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
		string key = "BOH_VALEERA_01";
		this.m_minionMoveTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key), true, NotificationManager.PopupTextType.BASIC);
		this.m_minionMoveTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
		this.m_minionMoveTutorialNotification.PulseReminderEveryXSeconds(2f);
	}

	// Token: 0x06004B58 RID: 19288 RVA: 0x0018F481 File Offset: 0x0018D681
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

	// Token: 0x06004B59 RID: 19289 RVA: 0x0018F490 File Offset: 0x0018D690
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

	// Token: 0x06004B5A RID: 19290 RVA: 0x0018F504 File Offset: 0x0018D704
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

	// Token: 0x06004B5B RID: 19291 RVA: 0x001784A8 File Offset: 0x001766A8
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

	// Token: 0x04003FA6 RID: 16294
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1EmoteResponse_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1EmoteResponse_01.prefab:5d9cf0b805423da478761a58cef47914");

	// Token: 0x04003FA7 RID: 16295
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1ExchangeC_01.prefab:db8a83f5cca8bb8469f4cada7b47439e");

	// Token: 0x04003FA8 RID: 16296
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_01.prefab:36f7601eb51cc6649a2503e73bed10cf");

	// Token: 0x04003FA9 RID: 16297
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_02 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_02.prefab:7cc07d221d1f7f944b6844135d2bfadb");

	// Token: 0x04003FAA RID: 16298
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_03 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_03.prefab:e6447c0151f5abd42bd38a04da2364e9");

	// Token: 0x04003FAB RID: 16299
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_01.prefab:a17a7508d110aba44bb2c21803ce07ba");

	// Token: 0x04003FAC RID: 16300
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_02 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_02.prefab:a8c84435745d07b4ead30ad81732eec3");

	// Token: 0x04003FAD RID: 16301
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_03 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_03.prefab:ec1ece3ed34dc4a408c90d1b0fa2e2d0");

	// Token: 0x04003FAE RID: 16302
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Intro_01.prefab:493c172ca022f474d8cf56e84f90689d");

	// Token: 0x04003FAF RID: 16303
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Loss_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Loss_01.prefab:0145a837328961440970b4634ec34e60");

	// Token: 0x04003FB0 RID: 16304
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Victory_01.prefab:defbde5575a93594bbab37bec1f9ccda");

	// Token: 0x04003FB1 RID: 16305
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeA_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeA_02.prefab:0a3cb629344643640a852a832f173368");

	// Token: 0x04003FB2 RID: 16306
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeB_01.prefab:450ecca324931aa4d8f8765be1685151");

	// Token: 0x04003FB3 RID: 16307
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeB_03 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeB_03.prefab:c73c7d76f374d454a8c59c48306635ff");

	// Token: 0x04003FB4 RID: 16308
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeC_03 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeC_03.prefab:162fe4f02246bd4468cca2dab399d7c1");

	// Token: 0x04003FB5 RID: 16309
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1Intro_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1Intro_02.prefab:ffe05bfc7b8b10842b1c398d9aea332f");

	// Token: 0x04003FB6 RID: 16310
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1Victory_01.prefab:84540329437927547bd93fdcfa6972b4");

	// Token: 0x04003FB7 RID: 16311
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission1ExchangeB_02 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission1ExchangeB_02.prefab:c6d4281677d5d6244a5d8095cbcdbb1b");

	// Token: 0x04003FB8 RID: 16312
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission1ExchangeC_04 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission1ExchangeC_04.prefab:57e4290f7e1782f44a614c02c987a749");

	// Token: 0x04003FB9 RID: 16313
	private static readonly AssetReference VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeA_01 = new AssetReference("VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeA_01.prefab:f05a8c6d92b8d6643be0a9b578e4e4ba");

	// Token: 0x04003FBA RID: 16314
	private static readonly AssetReference VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeA_03 = new AssetReference("VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeA_03.prefab:17257a2e278f1bf40b98c57e1e65d2d9");

	// Token: 0x04003FBB RID: 16315
	private static readonly AssetReference VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeC_02 = new AssetReference("VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeC_02.prefab:a9478d744fb1b2846810cba45b407d68");

	// Token: 0x04003FBC RID: 16316
	protected Notification m_minionMoveTutorialNotification;

	// Token: 0x04003FBD RID: 16317
	protected bool m_shouldPlayMinionMoveTutorial = true;

	// Token: 0x04003FBE RID: 16318
	public static readonly AssetReference LoGoshBrassRing = new AssetReference("LoGosh_BrassRing_Quote.prefab:266d95b9912642e43879f2bda9fa88ae");

	// Token: 0x04003FBF RID: 16319
	public static readonly AssetReference BrollBrassRing = new AssetReference("Broll_BrassRing_Quote.prefab:1bfe5acde48846249b4b7716c3ff0d8c");

	// Token: 0x04003FC0 RID: 16320
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_01,
		BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_02,
		BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_03
	};

	// Token: 0x04003FC1 RID: 16321
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_01,
		BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_02,
		BoH_Valeera_01.VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_03
	};

	// Token: 0x04003FC2 RID: 16322
	private HashSet<string> m_playedLines = new HashSet<string>();
}
