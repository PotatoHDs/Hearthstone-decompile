using System.Collections;
using System.Collections.Generic;

public class BoH_Malfurion_07 : BoH_Malfurion_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7ExchangeA_01.prefab:90d506f6e68ede3488176813d4550746");

	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7ExchangeD_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7ExchangeD_01.prefab:a295e1a4542d1eb469e341690f98346a");

	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7Intro_01.prefab:db6210202fa3cb7499242f5041938586");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission7ExchangeB_01.prefab:9df52d023443eaf4f837e011f96fa3e3");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission7Intro_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission7Intro_02.prefab:7e1b0afab07582c46ae8457112ab8347");

	private static readonly AssetReference VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7_Victory_01 = new AssetReference("VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7_Victory_01.prefab:8f66f1279f881604f92002ce0223cbff");

	private static readonly AssetReference VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7EmoteResponse_01 = new AssetReference("VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7EmoteResponse_01.prefab:e1b2f8901e118df4fa80583229331b18");

	private static readonly AssetReference VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_01 = new AssetReference("VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_01.prefab:e0bbad6ff4ddc914ba3e2b5db44a6742");

	private static readonly AssetReference VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_02 = new AssetReference("VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_02.prefab:5d0ca78992ff24e4fb0c36c5885d49ef");

	private static readonly AssetReference VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_03 = new AssetReference("VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_03.prefab:2627eb43a4e966d4896ed8924a72eca0");

	private static readonly AssetReference VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_01 = new AssetReference("VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_01.prefab:e3d10881bfe5c5c4982fcab1fc36165d");

	private static readonly AssetReference VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_02 = new AssetReference("VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_02.prefab:0cb81c58c81c0e54ca19b3f966425f29");

	private static readonly AssetReference VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_03 = new AssetReference("VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_03.prefab:0f8f072f141707344a10ebd0a9d8cdcc");

	private static readonly AssetReference VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Intro_04 = new AssetReference("VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Intro_04.prefab:eb94840e17b872b4ab6d7356ab162368");

	private static readonly AssetReference VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Loss_01 = new AssetReference("VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Loss_01.prefab:886b18ed9f44ef94e9f34e5d1962892b");

	private static readonly AssetReference VO_Story_Minion_Hamuul_Male_Tauren_Story_Malfurion_Mission7ExchangeC_01 = new AssetReference("VO_Story_Minion_Hamuul_Male_Tauren_Story_Malfurion_Mission7ExchangeC_01.prefab:07545a5fea43da744b2d570ebe9915c2");

	private static readonly AssetReference VO_Story_Minion_Hamuul_Male_Tauren_Story_Malfurion_Mission7Intro_03 = new AssetReference("VO_Story_Minion_Hamuul_Male_Tauren_Story_Malfurion_Mission7Intro_03.prefab:d48df2c560d6cb54288c15481e206f93");

	public static readonly AssetReference CenariusBrassRing = new AssetReference("Cenarius_BrassRing_Quote.prefab:9157110d07b5b004fa0c0f651c71ef81");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_01, VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_02, VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_03 };

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_01, VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_02, VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Malfurion_07()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7ExchangeA_01, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7ExchangeD_01, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7Intro_01, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission7ExchangeB_01, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission7Intro_02, VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7_Victory_01, VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7EmoteResponse_01, VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_01, VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_02, VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_03,
			VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_01, VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_02, VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_03, VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Intro_04, VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Loss_01, VO_Story_Minion_Hamuul_Male_Tauren_Story_Malfurion_Mission7ExchangeC_01, VO_Story_Minion_Hamuul_Male_Tauren_Story_Malfurion_Mission7Intro_03
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
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return MissionPlayVO(CenariusBrassRing, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7Intro_01);
		yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission7Intro_02);
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
		m_OverrideMusicTrack = MusicPlaylistType.InGame_BRMAdventure;
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7EmoteResponse_01;
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 507:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7_Victory_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 101:
			yield return PlayLineAlways(GetFriendlyActorByCardId("Story_08_Hamuul"), VO_Story_Minion_Hamuul_Male_Tauren_Story_Malfurion_Mission7ExchangeC_01);
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
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			yield return MissionPlayVO(GetFriendlyActorByCardId("Story_08_Hamuul"), VO_Story_Minion_Hamuul_Male_Tauren_Story_Malfurion_Mission7Intro_03);
			break;
		case 3:
			yield return PlayLineAlways(CenariusBrassRing, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7ExchangeA_01);
			break;
		case 7:
			yield return PlayLineAlways(actor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission7ExchangeB_01);
			break;
		case 11:
			yield return PlayLineAlways(CenariusBrassRing, VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7ExchangeD_01);
			break;
		}
	}
}
