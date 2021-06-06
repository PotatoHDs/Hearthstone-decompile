using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoH_Thrall_02 : BoH_Thrall_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2EmoteResponse_01 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2EmoteResponse_01.prefab:45760608f8527b44eaffc7c3faaaf645");

	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeA_01.prefab:936e2824cc25be743951005073859f9e");

	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeA_03 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeA_03.prefab:65e484b5c767b0d49a73e4bbe6469c18");

	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeB_01 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeB_01.prefab:063ac3d9fa58c9d4eb360b6df3b7b375");

	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeB_02 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeB_02.prefab:d6ef8be8d8d9d3147be4b65b3f848a1d");

	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeC_01.prefab:e35ef0b4d834dca4da8fd27c714ea277");

	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeC_02 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeC_02.prefab:92697f3486be0c64c95deaa568cd0280");

	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeD_01 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeD_01.prefab:181be3f0c1202754ba8e27c61a0562a7");

	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeD_02 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeD_02.prefab:b2316c97595cb0549883b788db5509d8");

	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeE_01 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeE_01.prefab:b54f6b82db25df94aa2bf91e17884ba5");

	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeE_02 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeE_02.prefab:1a8315c158c145647ac941bc95d586a8");

	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_01 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_01.prefab:8d3ab17683c604647adc54ef4e9fb34f");

	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_02 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_02.prefab:a3f20b58278fca146a736092ca4a41cb");

	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_03 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_03.prefab:7f513ccea20b1a74ebb23bc7455bee96");

	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Intro_01 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Intro_01.prefab:188b6f7ad7eeef24885f0eb5924b91c6");

	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Loss_01 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Loss_01.prefab:f671bb3824222b74db29e0fef66ea72f");

	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Victory_01 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Victory_01.prefab:bd3c5e203a7071045afe78cf9bba65f9");

	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Victory_03 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Victory_03.prefab:b3a179713af44824f860e7ec024ff9b7");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeA_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeA_02.prefab:9eff29277873c7941a83c9587cf6f336");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeA_04 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeA_04.prefab:2cf50b221d22a4b4c9ee3435ef4a1326");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeB_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeB_02.prefab:7e5c28b8d4d0e254db0b5bfe7242be80");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeC_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeC_03.prefab:d39e182c2780fb842aeb985bc41c8f6e");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeD_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeD_03.prefab:be67a8a29a8660a4481306ce675c8723");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeE_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeE_03.prefab:d0f7fca9d962d9e4494c4dc21561a9f4");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2Intro_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2Intro_02.prefab:636c73e79e935a3498db0ab839058dac");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2Victory_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2Victory_02.prefab:6c73bf5960acf1841a264f6b4966acde");

	protected Notification m_minionMoveTutorialNotification;

	protected bool m_shouldPlayMinionMoveTutorial = true;

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_01, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_02, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Thrall_02()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2EmoteResponse_01, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeA_01, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeA_03, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeB_01, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeB_02, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeC_01, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeC_02, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeD_01, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeD_02, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeE_01,
			VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeE_02, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_01, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_02, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Idle_03, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Intro_01, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Loss_01, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Victory_01, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Victory_03, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeA_02, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeA_04,
			VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeB_02, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeC_03, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeD_03, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeE_03, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2Intro_02, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2Victory_02
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
		yield return MissionPlayVO(actor, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Intro_01);
		yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2Intro_02);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetBossIdleLines()
	{
		return m_BossIdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
		m_standardEmoteResponseLine = VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2EmoteResponse_01;
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
		switch (missionEvent)
		{
		case 101:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeB_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 102:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeB_02);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeB_02);
			break;
		case 103:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeC_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 104:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeC_02);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeC_03);
			break;
		case 105:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeD_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 106:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeD_02);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeD_03);
			break;
		case 107:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeE_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 108:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeE_02);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeE_03);
			break;
		case 109:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Victory_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 507:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2Victory_02);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2Victory_03);
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeA_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeA_02);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission2ExchangeA_03);
			break;
		case 3:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission2ExchangeA_04);
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
			string key = "BOH_THRALL_02";
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
