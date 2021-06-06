using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoH_Thrall_01 : BoH_Thrall_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1EmoteResponse_01 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1EmoteResponse_01.prefab:6b15ee78bdb7d3e4eb817730292f67a0");

	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1ExchangeA_01 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1ExchangeA_01.prefab:94a18e1a15b525644ae701a8933e9870");

	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1ExchangeB_01.prefab:ccf658130c95a974a819427341721210");

	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_01 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_01.prefab:48facd72d9af5094a8a0ad2fcbcb5e27");

	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_02 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_02.prefab:89e0881c8a41774478ac837d2f916209");

	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_03 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_03.prefab:ae94739cf04110447ba183a3247bb34c");

	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_01 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_01.prefab:56d7656451fd55048be2545881834b29");

	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_02 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_02.prefab:5989e805d24284d419afb8f9d2dd1268");

	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_03 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_03.prefab:409c200008c1eff44843cb59e1361a8b");

	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Intro_01.prefab:2edf498dc0494924e96950f57b527726");

	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Intro_02 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Intro_02.prefab:5a74c71b012609441b6cc49eb2385767");

	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Loss_01 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Loss_01.prefab:6cb3cb427aab3474e908bcc32171c183");

	private static readonly AssetReference VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Victory_01.prefab:1d0196bd7fb4a6d46a2ea6bd46eb6c0b");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1ExchangeC_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1ExchangeC_02.prefab:56ed419838689ba4b99553bf6261ac6e");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Intro_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Intro_02.prefab:a6004d0d5e0d2d5488a743006bf3c9a3");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Victory_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Victory_02.prefab:c0ae5c6e8818e4c4a8e4c3bf7e3f0b05");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Victory_04 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Victory_04.prefab:4ffbeba38c60d11449fa8fe528dbc1cf");

	private static readonly AssetReference VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1ExchangeC_01 = new AssetReference("VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1ExchangeC_01.prefab:31402b425176411196f5aea1ff637fee");

	private static readonly AssetReference VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1ExchangeC_03 = new AssetReference("VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1ExchangeC_03.prefab:0e8b98d41d4045429f5a8abf0726cfa4");

	private static readonly AssetReference VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1Victory_03 = new AssetReference("VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1Victory_03.prefab:f8d7924a89ce2c94a92c3e3da1d90b11");

	private static readonly AssetReference VO_Story_Minion_Taretha_Female_Human_TriggerThrallLowHP_01 = new AssetReference("VO_Story_Minion_Taretha_Female_Human_TriggerThrallLowHP_01.prefab:90d6a3dd52cb4406986eac02dcdcba5c");

	private static readonly AssetReference troll_crowd_play_reaction_positive_1 = new AssetReference("troll_crowd_play_reaction_positive_1.prefab:ccb1b6d185b1e2e4480ef813153f3c9f");

	private static readonly AssetReference troll_crowd_play_reaction_very_positive_1 = new AssetReference("troll_crowd_play_reaction_very_positive_1.prefab:f69658ac1e4cacc4b94acdb1e0c38911");

	private static readonly AssetReference Low_Drumroll = new AssetReference("Low_Drumroll.prefab:d678997d507dd9041a499af987d4ff76");

	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]>
	{
		{
			228,
			new string[1] { "BOH_THRALL_01" }
		},
		{
			328,
			new string[1] { "BOH_THRALL_01a" }
		},
		{
			428,
			new string[1] { "BOH_THRALL_01b" }
		}
	};

	private Player friendlySidePlayer;

	private Entity playerEntity;

	private float popUpScale = 1.25f;

	private Vector3 popUpPos;

	private Notification StartPopup;

	public static readonly AssetReference TarethaBrassRing = new AssetReference("Taretha_BrassRing_Quote.prefab:683cb9ffa15e9af4cbbe387d4afe900d");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_01, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_02, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_03 };

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_01, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_02, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Thrall_01()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1EmoteResponse_01, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1ExchangeA_01, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1ExchangeB_01, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_01, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_02, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1HeroPower_03, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_01, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_02, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Idle_03, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Intro_01,
			VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Intro_02, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Loss_01, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Victory_01, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1ExchangeC_02, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Intro_02, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Victory_02, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Victory_04, VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1ExchangeC_01, VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1ExchangeC_03, VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1Victory_03,
			VO_Story_Minion_Taretha_Female_Human_TriggerThrallLowHP_01, troll_crowd_play_reaction_positive_1, troll_crowd_play_reaction_very_positive_1, Low_Drumroll
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	private void Start()
	{
		friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
	}

	private void SetPopupPosition()
	{
		if (friendlySidePlayer.IsCurrentPlayer())
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				popUpPos.z = -66f;
			}
			else
			{
				popUpPos.z = -44f;
			}
		}
		else if ((bool)UniversalInputManager.UsePhoneUI)
		{
			popUpPos.z = 66f;
		}
		else
		{
			popUpPos.z = 44f;
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
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return MissionPlayVO(enemyActor, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Intro_01);
		yield return MissionPlayVO(enemyActor, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Intro_02);
		yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Intro_02);
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
		m_OverrideMusicTrack = MusicPlaylistType.InGame_GILFinalBoss;
		m_standardEmoteResponseLine = VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1EmoteResponse_01;
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
		popUpPos = new Vector3(0f, 0f, -40f);
		switch (missionEvent)
		{
		case 507:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1Victory_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Victory_02);
			yield return PlayLineAlways(TarethaBrassRing, VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1Victory_03);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1Victory_04);
			GameState.Get().SetBusy(busy: false);
			break;
		case 101:
			yield return PlayLineAlways(TarethaBrassRing, VO_Story_Minion_Taretha_Female_Human_TriggerThrallLowHP_01);
			break;
		case 328:
		{
			yield return new WaitForSeconds(2f);
			yield return MissionPlaySound(enemyActor, Low_Drumroll);
			Notification notification2 = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popUpScale, GameStrings.Get(m_popUpInfo[missionEvent][0]), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
			NotificationManager.Get().DestroyNotification(notification2, 3.5f);
			break;
		}
		case 428:
		{
			yield return new WaitForSeconds(2f);
			yield return MissionPlaySound(enemyActor, Low_Drumroll);
			Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popUpScale, GameStrings.Get(m_popUpInfo[missionEvent][0]), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
			NotificationManager.Get().DestroyNotification(notification, 3.5f);
			break;
		}
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
		int m_missionEventBannerID = 228;
		switch (turn)
		{
		case 1:
		{
			yield return MissionPlaySound(enemyActor, Low_Drumroll);
			Notification notification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popUpScale, GameStrings.Get(m_popUpInfo[m_missionEventBannerID][0]), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
			NotificationManager.Get().DestroyNotification(notification, 6.5f);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1ExchangeA_01);
			yield return MissionPlaySound(enemyActor, troll_crowd_play_reaction_positive_1);
			break;
		}
		case 5:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Blackmoore_Male_Human_Story_Thrall_Mission1ExchangeB_01);
			yield return MissionPlaySound(enemyActor, troll_crowd_play_reaction_very_positive_1);
			break;
		case 9:
			yield return PlayLineAlways(TarethaBrassRing, VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1ExchangeC_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission1ExchangeC_02);
			yield return PlayLineAlways(TarethaBrassRing, VO_Story_Minion_Taretha_Female_Human_Story_Thrall_Mission1ExchangeC_03);
			break;
		}
	}
}
