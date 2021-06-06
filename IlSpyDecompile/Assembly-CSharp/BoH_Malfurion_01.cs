using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoH_Malfurion_01 : BoH_Malfurion_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_01_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_01_01.prefab:3af07d5f3716a8b4d90be31675c25b10");

	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_02_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_02_01.prefab:2f605dc353e7ab04b85e86976a8152f4");

	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1_Victory_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1_Victory_01.prefab:1ebf159a3a7e0284298406e09f72cdbc");

	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1_Victory_03 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1_Victory_03.prefab:3892ddfe34a5ed34ca00fa1b1aa6a58d");

	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1EmoteResponse_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1EmoteResponse_01.prefab:16b6e5701e26b404aa3c99508099f757");

	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeA_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeA_01.prefab:6a03c6fbd426db54b8ed7e21b2b780f6");

	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeB_01.prefab:cba0a850f450a7c4d8c6dbc049b19a97");

	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeB_02 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeB_02.prefab:ab0bfc8cf92454a48bf937ec5469bd46");

	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeC_01.prefab:e6addb8546ea48146bbb0c15c7dfd43d");

	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1HeroPower_03 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1HeroPower_03.prefab:b6e8025a1c97eea4fbad55ad3e21db25");

	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_01.prefab:bdc1efbb05ad3094c9e266dd49fe4585");

	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_02 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_02.prefab:3c4b57d3041d3bb4882097b5a17eb26c");

	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_03 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_03.prefab:6a38e59a6926c804b8759fb355b03763");

	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Intro_01.prefab:f50cf649fa8fd5f43be4d78d68b5822d");

	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Intro_03 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Intro_03.prefab:481d6ae805e2d8d47b5fdaa4af791883");

	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Loss_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Loss_01.prefab:866fe7090dd3dcb428ab1d6983c06775");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1_Victory_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1_Victory_02.prefab:209fdfafeffdbe34fa8ed8dd7a3848fc");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1ExchangeB_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1ExchangeB_02.prefab:3f6bdcf911d778a4ba709f0ef48c7b9c");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1ExchangeC_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1ExchangeC_02.prefab:d8d1eaa6b86441741b7ddadee6059cdc");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1Intro_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1Intro_02.prefab:76b8579081c17a043b936d9dc1126931");

	private Dictionary<int, string[]> m_popUpInfo = new Dictionary<int, string[]> { 
	{
		228,
		new string[1] { "BOH_MALFURION_01b" }
	} };

	private Player friendlySidePlayer;

	private Entity playerEntity;

	private float popUpScale = 1.25f;

	private Vector3 popUpPos;

	private Notification StartPopup;

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_01_01, VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_02_01, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1HeroPower_03 };

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_01, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_02, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private Notification m_turnCounter;

	private MineCartRushArt m_mineCartArt;

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Malfurion_01()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_01_01, VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_02_01, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1_Victory_01, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1_Victory_03, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1EmoteResponse_01, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeA_01, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeB_01, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeB_02, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeC_01, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1HeroPower_03,
			VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_01, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_02, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Idle_03, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Intro_01, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Intro_03, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Loss_01, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1_Victory_02, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1ExchangeB_02, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1ExchangeC_02, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1Intro_02
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
		yield return MissionPlayVO(enemyActor, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Intro_01);
		yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1Intro_02);
		yield return MissionPlayVO(enemyActor, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Intro_03);
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
		m_OverrideMusicTrack = MusicPlaylistType.InGame_DHPrologue;
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1EmoteResponse_01;
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
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1Loss_01);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1_Victory_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1_Victory_02);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1_Victory_03);
			GameState.Get().SetBusy(busy: false);
			break;
		case 228:
		{
			Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popUpScale, GameStrings.Get(m_popUpInfo[missionEvent][0]), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
			yield return new WaitForSeconds(3.5f);
			NotificationManager.Get().DestroyNotification(popup, 0f);
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
		switch (turn)
		{
		case 1:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeA_01);
			break;
		case 3:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeB_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeB_02);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1ExchangeB_02);
			break;
		case 5:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission1ExchangeC_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission1ExchangeC_02);
			break;
		}
	}

	public override void NotifyOfMulliganEnded()
	{
		base.NotifyOfMulliganEnded();
		InitVisuals();
	}

	private void InitVisuals()
	{
		int cost = GetCost();
		InitTurnCounter(cost);
	}

	public override void OnTagChanged(TagDelta change)
	{
		base.OnTagChanged(change);
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag == GAME_TAG.COST && change.newValue != change.oldValue)
		{
			UpdateVisuals(change.newValue);
		}
	}

	private void InitTurnCounter(int cost)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("LOE_Turn_Timer.prefab:b05530aa55868554fb8f0c66632b3c22");
		m_turnCounter = gameObject.GetComponent<Notification>();
		PlayMakerFSM component = m_turnCounter.GetComponent<PlayMakerFSM>();
		component.FsmVariables.GetFsmBool("RunningMan").Value = true;
		component.FsmVariables.GetFsmBool("MineCart").Value = false;
		component.FsmVariables.GetFsmBool("Airship").Value = false;
		component.FsmVariables.GetFsmBool("Destroyer").Value = false;
		component.SendEvent("Birth");
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		m_turnCounter.transform.parent = actor.gameObject.transform;
		m_turnCounter.transform.localPosition = new Vector3(-1.4f, 0.187f, -0.11f);
		m_turnCounter.transform.localScale = Vector3.one * 0.52f;
		UpdateTurnCounterText(cost);
	}

	private void UpdateVisuals(int cost)
	{
		UpdateTurnCounter(cost);
	}

	private void UpdateMineCartArt()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		m_mineCartArt.DoPortraitSwap(actor);
	}

	private void UpdateTurnCounter(int cost)
	{
		m_turnCounter.GetComponent<PlayMakerFSM>().SendEvent("Action");
		if (cost <= 0)
		{
			Object.Destroy(m_turnCounter.gameObject);
		}
		else
		{
			UpdateTurnCounterText(cost);
		}
	}

	private void UpdateTurnCounterText(int cost)
	{
		GameStrings.PluralNumber[] pluralNumbers = new GameStrings.PluralNumber[1]
		{
			new GameStrings.PluralNumber
			{
				m_index = 0,
				m_number = cost
			}
		};
		string headlineString = GameStrings.FormatPlurals("BOH_MALFURION_01", pluralNumbers);
		m_turnCounter.ChangeDialogText(headlineString, cost.ToString(), "", "");
	}

	private IEnumerator ShowPopup(string displayString)
	{
		StartPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale(), GameStrings.Get(displayString), convertLegacyPosition: false);
		NotificationManager.Get().DestroyNotification(StartPopup, 7f);
		GameState.Get().SetBusy(busy: true);
		yield return new WaitForSeconds(2f);
		GameState.Get().SetBusy(busy: false);
	}
}
