using System.Collections;
using System.Collections.Generic;

public class BoH_Uther_06 : BoH_Uther_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6ExchangeB_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6ExchangeB_01.prefab:6b4d78f2c08c3e24bb02bfcffb5c3dbf");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6ExchangeC_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6ExchangeC_01.prefab:6ed031e98908a7b419e5ea8bf58be0f7");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Victory_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Victory_01.prefab:2dd4ef73dc8f5d742a162ed09bd273de");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Victory_02 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Victory_02.prefab:d717febb3b9490447ad4ecace144c4db");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeA_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeA_01.prefab:46fce0cf2095c7a468a4dae72891cf18");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeB_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeB_01.prefab:58483dc4d172b1a4ca9780af0d9a87d9");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeB_02 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeB_02.prefab:43800732d733bb344a82e6fc81f36ad2");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeC_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeC_01.prefab:48e5641d70a242845b815fb020c714d6");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeC_02 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeC_02.prefab:6022b0d04fffefc4e84a92de7e09f334");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_PreMission7_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_PreMission7_01.prefab:31d91ac77b39a3846841b7dca75739ef");

	private static readonly AssetReference VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Death_01 = new AssetReference("VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Death_01.prefab:846e08becce366c4cbd0bde29a079d0a");

	private static readonly AssetReference VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6EmoteResponse_01 = new AssetReference("VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6EmoteResponse_01.prefab:be9d19bcf3bb9c244a03e29657f72892");

	private static readonly AssetReference VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_01 = new AssetReference("VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_01.prefab:57512907dc75f8647a3c2ae291b436ee");

	private static readonly AssetReference VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_02 = new AssetReference("VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_02.prefab:01c5d34766cc48248934085ec8e10d8c");

	private static readonly AssetReference VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_03 = new AssetReference("VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_03.prefab:da4f79c830369b645b5d3c67986c2e00");

	private static readonly AssetReference VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_01 = new AssetReference("VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_01.prefab:d7ed78ccc5ea9c34f93edf78061decd1");

	private static readonly AssetReference VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_02 = new AssetReference("VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_02.prefab:5fcaecfcb9e0024478171423617dd916");

	private static readonly AssetReference VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_03 = new AssetReference("VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_03.prefab:a5d589e7d9414694fbf9c1653ab4f303");

	private static readonly AssetReference VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Loss_01 = new AssetReference("VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Loss_01.prefab:8152d60c14fdad443999fd65abc4a66c");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6Intro_01.prefab:05b676e20eb09354cb9ef3bdd6cfc178");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Intro_01.prefab:8f8ede68cb0a7a4419aaaf20bad98871");

	private static readonly AssetReference VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Intro_01.prefab:1f658df40030a754a9e413d516c28d21");

	public static readonly AssetReference ArthasBrassRing = new AssetReference("Arthas_BrassRing_Quote.prefab:49bb0ac905ae3c54cbf3624451b62ab4");

	private List<string> m_HeroPowerLines = new List<string> { VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_01, VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_02, VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_03 };

	private List<string> m_IdleLines = new List<string> { VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_01, VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_02, VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6ExchangeB_01, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6ExchangeC_01, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Intro_01, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Victory_01, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Victory_02, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeA_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeB_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeB_02, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeC_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeC_02,
			VO_Story_Hero_Uther_Male_Human_Story_Uther_PreMission7_01, VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Death_01, VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6EmoteResponse_01, VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_01, VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_02, VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6HeroPower_03, VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_01, VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_02, VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Idle_03, VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Intro_01,
			VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Loss_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6Intro_01
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
		yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6Intro_01);
		yield return PlayLineAlways(ArthasBrassRing, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Intro_01);
		yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeA_01);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6EmoteResponse_01;
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
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 504:
			yield return PlayLineAlways(actor, VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Loss_01);
			break;
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(ArthasBrassRing, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Victory_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_PreMission7_01);
			yield return PlayLineAlways(ArthasBrassRing, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6Victory_02);
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
			yield return PlayLineAlways(actor, VO_Story_Hero_Venim_Male_Lich_Story_Uther_Mission6Intro_01);
			break;
		case 7:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeB_01);
			yield return PlayLineAlways(ArthasBrassRing, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6ExchangeB_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeB_02);
			break;
		case 13:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeC_01);
			yield return PlayLineAlways(ArthasBrassRing, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission6ExchangeC_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission6ExchangeC_02);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ICC);
	}
}
