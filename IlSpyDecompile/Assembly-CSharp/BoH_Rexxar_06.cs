using System.Collections;
using System.Collections.Generic;

public class BoH_Rexxar_06 : BoH_Rexxar_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Death_01 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Death_01.prefab:7399820ffe8358c4d9e62a99c7f4537a");

	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6EmoteResponse_01 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6EmoteResponse_01.prefab:e236430c43795544aad7a3cfee0dab7e");

	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6ExchangeB_01 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6ExchangeB_01.prefab:3b646d2702651c44cb1866c65a8922aa");

	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6ExchangeC_01 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6ExchangeC_01.prefab:1b569d23197f05742b82066210283a7e");

	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_01 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_01.prefab:036cc7e9be96f0a44818344b218288fd");

	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_02 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_02.prefab:c0e26a8cbacc4f9489868d741c6baf14");

	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_03 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_03.prefab:ed3cab9b40a442440a20ed4f0ed53afb");

	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_01 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_01.prefab:3fdcbfa6a3f475e46a88e8cacc1222ea");

	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_02 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_02.prefab:f683936bee3988645bea7c0a56aec9f6");

	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_03 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_03.prefab:e0ed4fbff1fd0e149b678f6a03b96ac3");

	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Intro_01 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Intro_01.prefab:61123b7c46905db4cb4e8476c1388d22");

	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Loss_01 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Loss_01.prefab:41b4841110cfa3d4eb0aabedc873fc70");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeA_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeA_01.prefab:d1e1dc742d7fec144bbb3d185f8004c8");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeB_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeB_01.prefab:69aca0cd9ccea0548bdadb969652f4c2");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeC_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeC_01.prefab:d5f9a3d8ee2f5b946933655f5059c6cc");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6Intro_01.prefab:8062f8f2b27a4d749a942ae31826cae7");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6Victory_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6Victory_01.prefab:3ae4b33ffab3b864caa9935c5cb61f1b");

	private static readonly AssetReference VO_Story_Minion_Baine_Male_Tauren_Story_Rexxar_Mission6ExchangeA_01 = new AssetReference("VO_Story_Minion_Baine_Male_Tauren_Story_Rexxar_Mission6ExchangeA_01.prefab:70c5f8b2a0d6e924a9189e97232a2344");

	private static readonly AssetReference VO_Story_Minion_Baine_Male_Tauren_Story_Rexxar_Mission6Victory_01 = new AssetReference("VO_Story_Minion_Baine_Male_Tauren_Story_Rexxar_Mission6Victory_01.prefab:1f7a8dd5eae9dfd4187243bf1f26de84");

	private List<string> m_VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPowerLines = new List<string> { VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_01, VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_02, VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_03 };

	private List<string> m_VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6IdleLines = new List<string> { VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_01, VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_02, VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Rexxar_06()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Death_01, VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6EmoteResponse_01, VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6ExchangeB_01, VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6ExchangeC_01, VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_01, VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_02, VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_03, VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_01, VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_02, VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_03,
			VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Intro_01, VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Loss_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeA_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeB_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeC_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6Intro_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6Victory_01, VO_Story_Minion_Baine_Male_Tauren_Story_Rexxar_Mission6ExchangeA_01, VO_Story_Minion_Baine_Male_Tauren_Story_Rexxar_Mission6Victory_01
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
		yield return PlayLineAlways(actor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6Intro_01);
		yield return PlayLineAlways(enemyActor, VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Intro_01);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType != EmoteType.START && MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
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
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 501:
		{
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor2, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6Victory_01);
			Actor enemyActorByCardId = GetEnemyActorByCardId("Story_02_Baine");
			if (enemyActorByCardId != null)
			{
				yield return PlayLineAlways(enemyActorByCardId, VO_Story_Minion_Baine_Male_Tauren_Story_Rexxar_Mission6Victory_01);
			}
			GameState.Get().SetBusy(busy: false);
			break;
		}
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Loss_01);
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
		case 1:
		{
			Actor enemyActorByCardId = GetEnemyActorByCardId("Story_02_Baine");
			if (enemyActorByCardId != null)
			{
				yield return PlayLineAlways(enemyActorByCardId, VO_Story_Minion_Baine_Male_Tauren_Story_Rexxar_Mission6ExchangeA_01);
			}
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeA_01);
			break;
		}
		case 3:
			yield return PlayLineAlways(actor, VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6ExchangeB_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeB_01);
			break;
		case 13:
			yield return PlayLineAlways(actor, VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6ExchangeC_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeC_01);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DRG);
	}
}
