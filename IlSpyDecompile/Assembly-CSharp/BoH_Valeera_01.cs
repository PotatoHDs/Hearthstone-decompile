using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoH_Valeera_01 : BoH_Valeera_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1EmoteResponse_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1EmoteResponse_01.prefab:5d9cf0b805423da478761a58cef47914");

	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1ExchangeC_01.prefab:db8a83f5cca8bb8469f4cada7b47439e");

	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_01.prefab:36f7601eb51cc6649a2503e73bed10cf");

	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_02 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_02.prefab:7cc07d221d1f7f944b6844135d2bfadb");

	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_03 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_03.prefab:e6447c0151f5abd42bd38a04da2364e9");

	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_01.prefab:a17a7508d110aba44bb2c21803ce07ba");

	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_02 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_02.prefab:a8c84435745d07b4ead30ad81732eec3");

	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_03 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_03.prefab:ec1ece3ed34dc4a408c90d1b0fa2e2d0");

	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Intro_01.prefab:493c172ca022f474d8cf56e84f90689d");

	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Loss_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Loss_01.prefab:0145a837328961440970b4634ec34e60");

	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Victory_01.prefab:defbde5575a93594bbab37bec1f9ccda");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeA_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeA_02.prefab:0a3cb629344643640a852a832f173368");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeB_01.prefab:450ecca324931aa4d8f8765be1685151");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeB_03 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeB_03.prefab:c73c7d76f374d454a8c59c48306635ff");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeC_03 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeC_03.prefab:162fe4f02246bd4468cca2dab399d7c1");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1Intro_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1Intro_02.prefab:ffe05bfc7b8b10842b1c398d9aea332f");

	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1Victory_01.prefab:84540329437927547bd93fdcfa6972b4");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission1ExchangeB_02 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission1ExchangeB_02.prefab:c6d4281677d5d6244a5d8095cbcdbb1b");

	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission1ExchangeC_04 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission1ExchangeC_04.prefab:57e4290f7e1782f44a614c02c987a749");

	private static readonly AssetReference VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeA_01 = new AssetReference("VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeA_01.prefab:f05a8c6d92b8d6643be0a9b578e4e4ba");

	private static readonly AssetReference VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeA_03 = new AssetReference("VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeA_03.prefab:17257a2e278f1bf40b98c57e1e65d2d9");

	private static readonly AssetReference VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeC_02 = new AssetReference("VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeC_02.prefab:a9478d744fb1b2846810cba45b407d68");

	protected Notification m_minionMoveTutorialNotification;

	protected bool m_shouldPlayMinionMoveTutorial = true;

	public static readonly AssetReference LoGoshBrassRing = new AssetReference("LoGosh_BrassRing_Quote.prefab:266d95b9912642e43879f2bda9fa88ae");

	public static readonly AssetReference BrollBrassRing = new AssetReference("Broll_BrassRing_Quote.prefab:1bfe5acde48846249b4b7716c3ff0d8c");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_01, VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_02, VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_03 };

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_01, VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_02, VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1EmoteResponse_01, VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1ExchangeC_01, VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_01, VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_02, VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1HeroPower_03, VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_01, VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_02, VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Idle_03, VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Intro_01, VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Loss_01,
			VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Victory_01, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeA_02, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeB_01, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeB_03, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeC_03, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1Intro_02, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1Victory_01, VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission1ExchangeB_02, VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission1ExchangeC_04, VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeA_01,
			VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeA_03, VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeC_02
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

	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return MissionPlayVO(actor, VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Intro_01);
		yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1Intro_02);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetBossIdleLines()
	{
		return m_BossIdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_BossUsesHeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_OverrideMusicTrack = MusicPlaylistType.InGame_TRL;
		m_standardEmoteResponseLine = VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
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
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return PlayLineAlways(actor, VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Loss_01);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1Victory_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1Victory_01);
			GameState.Get().SetBusy(busy: false);
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
			GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
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
			GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 3:
			yield return PlayLineAlways(BrollBrassRing, VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeA_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeA_02);
			yield return PlayLineAlways(BrollBrassRing, VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeA_03);
			break;
		case 7:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeB_01);
			yield return PlayLineAlways(LoGoshBrassRing, VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission1ExchangeB_02);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeB_03);
			break;
		case 11:
			yield return PlayLineAlways(actor, VO_Story_Hero_Rehgar_Male_Orc_Story_Valeera_Mission1ExchangeC_01);
			yield return PlayLineAlways(BrollBrassRing, VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission1ExchangeC_02);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission1ExchangeC_03);
			yield return PlayLineAlways(LoGoshBrassRing, VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission1ExchangeC_04);
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
			string key = "BOH_VALEERA_01";
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
