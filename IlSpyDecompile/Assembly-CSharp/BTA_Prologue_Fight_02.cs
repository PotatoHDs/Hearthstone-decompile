using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_Prologue_Fight_02 : BTA_Prologue_Dungeon
{
	private static readonly AssetReference VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Sargeras01_01 = new AssetReference("VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Sargeras01_01.prefab:74be5edfc12439647ac7defe964d741a");

	private static readonly AssetReference VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Sargeras02_01 = new AssetReference("VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Sargeras02_01.prefab:bbdd09d8340330a4c98cc94d594dec8e");

	private static readonly AssetReference VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Victory03_01 = new AssetReference("VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Victory03_01.prefab:24378fc3895fba8428a62f8d7d2005cc");

	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_DemonClawsAlt_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_DemonClawsAlt_01.prefab:48951c1bf63e6e146af0cc7bde803be0");

	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeA_01_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeA_01_01.prefab:f7b32f91666775141808f2ec32b6f414");

	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeA_03_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeA_03_01.prefab:da74a964ef4efea4f977682fcce22760");

	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeB_01_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeB_01_01.prefab:01ebed9a93ec55748adf083ef2ff1fc2");

	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeC_02_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeC_02_01.prefab:047b9de97099ae640a6a6a151ebfd2d9");

	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeD_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeD_01.prefab:07670d1cc3eaecf43950b15ad5072b3b");

	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Intro_01_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Intro_01_01.prefab:a86daaaa3809c2e4da7d063b3c3ca9c3");

	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Intro_03_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Intro_03_01.prefab:6b609c6f5b9526047b08375b6f17caf7");

	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Turn2_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Turn2_01.prefab:9ff6e38ed8f2bbe43a71fae65e4abe21");

	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Victory01_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Victory01_01.prefab:5e892bc2f5e5c8f4cbcfb6f3bd39d9d0");

	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Death_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Death_01.prefab:cbac973d6f7c71f419f766cb251600ef");

	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_EmoteResponse_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_EmoteResponse_01.prefab:3dd18a8c149ed5f499d697c48d23f83a");

	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_01_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_01_01.prefab:859b20692e41891499fb35d42c97aeac");

	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_02_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_02_01.prefab:5ddd4a5abda7d57468029cc98c8b201b");

	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_03_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_03_01.prefab:e4a71c2d5cba9704396bc8232ac401dc");

	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_01_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_01_01.prefab:82b7793375cc52c4bb96fbfffdd0f4d8");

	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_02_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_02_01.prefab:1bd8e72a8e2fcec4e86cf569202059f2");

	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_03_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_03_01.prefab:ef069b11ece3b6a429d2522aeca40a75");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeA_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeA_02_01.prefab:37a3f6d52b4912f48adc553a9b5e79a4");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeB_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeB_02_01.prefab:08c7a0338aeee5d42aedeb121430bc1f");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeC_01_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeC_01_01.prefab:487d4490f2bf29a46b68c72939650211");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Intro_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Intro_02_01.prefab:1afba87c22e4b3543ae5af458a3688c2");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory02_01.prefab:cec94069b80d73b4298ee794421ff850");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory04_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory04_01.prefab:787ab7c773b6ab14d97f31a80e3441b4");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory05_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory05_01.prefab:9f6e88a19c3fb3e409eff69cb5c84b75");

	private static readonly AssetReference Prologue_Illidan_Transform = new AssetReference("Prologue_Illidan_Transform.prefab:f2e213201d6f63840b0f096592dd548e");

	protected Notification m_minionMoveTutorialNotification;

	protected bool m_shouldPlayMinionMoveTutorial = true;

	public static readonly AssetReference SargerasBrassRing = new AssetReference("Sargeras_Popup_BrassRing.prefab:df705ac0326836746af538133a79b587");

	private List<string> m_VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_Lines = new List<string> { VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_01_01, VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_02_01, VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_03_01 };

	private List<string> m_VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_Lines = new List<string> { VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_01_01, VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_02_01, VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_03_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Sargeras01_01, VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Sargeras02_01, VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Victory03_01, VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_DemonClawsAlt_01, VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeA_01_01, VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeA_03_01, VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeB_01_01, VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeC_02_01, VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeD_01, VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Intro_01_01,
			VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Intro_03_01, VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Turn2_01, VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Victory01_01, VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Death_01, VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_EmoteResponse_01, VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_01_01, VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_02_01, VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_03_01, VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_01_01, VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_02_01,
			VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_03_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeA_02_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeB_02_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeC_01_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Intro_02_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory02_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory04_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory05_01, Prologue_Illidan_Transform
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Idle_Lines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_Lines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Intro_03_01, Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (missionEvent == 911)
		{
			GameState.Get().SetBusy(busy: true);
			while (m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(busy: false);
			yield break;
		}
		switch (missionEvent)
		{
		case 100:
			yield return PlayLineAlways(enemyActor, VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_DemonClawsAlt_01);
			break;
		case 101:
			yield return PlayLineOnlyOnce(SargerasBrassRing, VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Sargeras02_01);
			break;
		case 102:
			yield return PlayLineOnlyOnce(SargerasBrassRing, VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Sargeras01_01);
			break;
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Victory01_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory02_01);
			yield return PlayLineAlways(SargerasBrassRing, VO_Prologue_Sargeras_Male_Demon_Prologue_Mission2_Victory03_01);
			PlaySound(Prologue_Illidan_Transform);
			GameState.Get().SetBusy(busy: false);
			break;
		case 502:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Victory05_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 503:
			yield return PlayLineAlways(enemyActor, VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Intro_01_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_Intro_02_01);
			yield return PlayLineAlways(enemyActor, VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Intro_03_01);
			break;
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_HeroPower_Lines);
			break;
		case 228:
			GameState.Get().SetBusy(busy: true);
			ShowMinionMoveTutorial();
			yield return new WaitForSeconds(3f);
			HideNotification(m_minionMoveTutorialNotification);
			GameState.Get().SetBusy(busy: false);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return base.RespondToPlayedCardWithTiming(entity);
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 2:
			yield return PlayLineAlways(enemyActor, VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_Turn2_01);
			break;
		case 3:
			m_shouldPlayMinionMoveTutorial = false;
			break;
		case 4:
			yield return PlayLineAlways(enemyActor, VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeA_01_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeA_02_01);
			yield return PlayLineAlways(enemyActor, VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeA_03_01);
			break;
		case 6:
			yield return PlayLineAlways(enemyActor, VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeB_01_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeB_02_01);
			break;
		case 8:
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission2_ExchangeC_01_01);
			yield return PlayLineAlways(enemyActor, VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeC_02_01);
			break;
		case 10:
			yield return PlayLineAlways(enemyActor, VO_Prologue_Xavius_Male_Satyr_Prologue_Mission2_ExchangeD_01);
			break;
		}
	}

	protected void ShowMinionMoveTutorial()
	{
		Card leftMostMinionInOpponentPlay = GetLeftMostMinionInOpponentPlay();
		if (!(leftMostMinionInOpponentPlay == null))
		{
			Vector3 position = leftMostMinionInOpponentPlay.transform.position;
			Vector3 position2 = ((!UniversalInputManager.UsePhoneUI) ? new Vector3(position.x, position.y, position.z + 2.5f) : new Vector3(position.x + 0.05f, position.y, position.z + 2.6f));
			string key = "DEMON_HUNTER_PROLOGUE_2_PORTAL_POPUP";
			m_minionMoveTutorialNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, position2, TutorialEntity.GetTextScale(), GameStrings.Get(key));
			m_minionMoveTutorialNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Down);
			m_minionMoveTutorialNotification.PulseReminderEveryXSeconds(2f);
		}
	}

	private IEnumerator ShowOrHideMoveMinionTutorial()
	{
		while (!InputManager.Get().GetHeldCard())
		{
			yield return null;
		}
		HideNotification(m_minionMoveTutorialNotification);
		while ((bool)InputManager.Get().GetHeldCard())
		{
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		if ((GetLeftMostMinionInOpponentPlay() != null || (bool)InputManager.Get().GetHeldCard()) && m_shouldPlayMinionMoveTutorial)
		{
			ShowMinionMoveTutorial();
			GameEntity.Coroutines.StartCoroutine(ShowOrHideMoveMinionTutorial());
		}
	}

	protected Card GetLeftMostMinionInFriendlyPlay()
	{
		foreach (Card card in GameState.Get().GetFriendlySidePlayer().GetBattlefieldZone()
			.GetCards())
		{
			if (card.GetEntity().GetTag(GAME_TAG.ZONE_POSITION) == 1)
			{
				return card;
			}
		}
		return null;
	}

	protected Card GetLeftMostMinionInOpponentPlay()
	{
		foreach (Card card in GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
			.GetCards())
		{
			if (card.GetEntity().GetTag(GAME_TAG.ZONE_POSITION) == 1)
			{
				return card;
			}
		}
		return null;
	}

	protected void HideNotification(Notification notification, bool hideImmediately = false)
	{
		if (notification != null)
		{
			if (hideImmediately)
			{
				NotificationManager.Get().DestroyNotificationNowWithNoAnim(notification);
			}
			else
			{
				NotificationManager.Get().DestroyNotification(notification, 0f);
			}
		}
	}
}
