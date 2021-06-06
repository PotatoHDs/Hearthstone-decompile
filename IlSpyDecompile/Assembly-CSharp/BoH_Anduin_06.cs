using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoH_Anduin_06 : BoH_Anduin_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6ExchangeA_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6ExchangeA_02.prefab:51c5ebd53930a8d4e9a6c3b6d5b9876a");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6Intro_01.prefab:217eba39f8ba076449a05458abf8c1a0");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6Victory_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6Victory_02.prefab:7730d01779261ef44a932ca6624afa99");

	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_01 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_01.prefab:4eea07bd68b1e9540b3f498f6ede1f0a");

	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_02 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_02.prefab:3bcbffa0845cb714ea9247bcbc5cb55e");

	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_03 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_03.prefab:7617c1faf3c22a74e8f2204e9b918292");

	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_04 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_04.prefab:611a9d36f6d6dea439f234e8fcbf1b0f");

	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6EmoteResponse_01 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6EmoteResponse_01.prefab:27a63b711947c674fa0ce9b4a209a1dd");

	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeA_01 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeA_01.prefab:ab61b46eea485a34a93c0319294c77c5");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6ExchangeA_03 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6ExchangeA_03.prefab:8714bda28aab0dd43ae0aa0eac022300");

	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeB_01 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeB_01.prefab:d8a909b386dcb464dba538622af9e864");

	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeC_01 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeC_01.prefab:4ecd2cf7956034d4e896660a6d55ce0d");

	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeD_01 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeD_01.prefab:e9fffd02eecbf9a4caf63a75526df23e");

	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeE_01 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeE_01.prefab:bd6c783406465b040bfed07196ec335c");

	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_01 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_01.prefab:a9f67b63767f9e6449fbf30679d58a72");

	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_02 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_02.prefab:511cbe78cde72ab4e979b594b27c3a36");

	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_03 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_03.prefab:e17cbdf2c68126649bfa5d6167130acc");

	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_01 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_01.prefab:5d10a608bdaf72747bb7ee755864cd7a");

	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_02 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_02.prefab:0c9ab276b34d2e14b9ec4cc4c84b5cc2");

	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_03 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_03.prefab:885fe8fe862376441aa6c7fe6f461015");

	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Intro_02 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Intro_02.prefab:7bd5b55d2aeb8014da6a4a049cdd1c25");

	private static readonly AssetReference VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Loss_01 = new AssetReference("VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Loss_01.prefab:dcfd078f359979d4b94d5449d53d0eb7");

	private static readonly AssetReference VO_Story_Minion_SamtheWise_Male_Pandaren_TriggerDone_01 = new AssetReference("VO_Story_Minion_SamtheWise_Male_Pandaren_TriggerDone_01.prefab:7d385f828f2fbd34c8b5765605c54154");

	protected Notification m_minionMoveTutorialNotification;

	protected bool m_shouldPlayMinionMoveTutorial = true;

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_01, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_02, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_03 };

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_01, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_02, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Anduin_06()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6ExchangeA_02, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6ExchangeA_03, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6Intro_01, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6Victory_02, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_01, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_02, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_03, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_04, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6EmoteResponse_01, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeA_01,
			VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeB_01, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeC_01, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeD_01, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeE_01, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_01, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_02, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6HeroPower_03, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_01, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_02, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Idle_03,
			VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Intro_02, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Loss_01, VO_Story_Minion_SamtheWise_Male_Pandaren_TriggerDone_01
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return MissionPlayVO(actor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6Intro_01);
		yield return MissionPlayVO(enemyActor, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Intro_02);
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
		m_OverrideMulliganMusicTrack = MusicPlaylistType.InGame_LOOT;
		m_standardEmoteResponseLine = VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6EmoteResponse_01;
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 507:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_03);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_02);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6_Victory_04);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6Victory_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 102:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeA_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6ExchangeA_02);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission6ExchangeA_03);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeB_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeC_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 103:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeD_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 105:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(GetEnemyActorByCardId("Story_05_SamtheWise"), VO_Story_Minion_SamtheWise_Male_Pandaren_TriggerDone_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 106:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_MonkeyKing_Male_Hozen_Story_Anduin_Mission6ExchangeE_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 228:
			GameState.Get().SetBusy(busy: true);
			ShowMinionMoveTutorial();
			yield return new WaitForSeconds(3f);
			HideNotification(m_minionMoveTutorialNotification);
			GameState.Get().SetBusy(busy: false);
			break;
		case 515:
			yield return MissionPlayVO(enemyActor, m_standardEmoteResponseLine);
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
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
	}

	protected void ShowMinionMoveTutorial()
	{
		Card leftMostMinionInOpponentPlay = GetLeftMostMinionInOpponentPlay();
		if (!(leftMostMinionInOpponentPlay == null))
		{
			Vector3 position = leftMostMinionInOpponentPlay.transform.position;
			Vector3 position2 = ((!UniversalInputManager.UsePhoneUI) ? new Vector3(position.x, position.y, position.z + 2.5f) : new Vector3(position.x + 0.05f, position.y, position.z + 2.6f));
			string key = "BOH_ANDUIN_06";
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
