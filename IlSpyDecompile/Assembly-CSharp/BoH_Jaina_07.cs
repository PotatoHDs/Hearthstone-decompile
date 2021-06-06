using System.Collections;
using System.Collections.Generic;

public class BoH_Jaina_07 : BoH_Jaina_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeA_01.prefab:1f19bfe8b5ddcd246aa050ae86508b09");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeB_01.prefab:38e7d6f401987a547a39180eb2072f6a");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeC_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeC_01.prefab:f9c93dd912750894e9ee8ef94b7fa479");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Intro_01.prefab:d9cef84e5f616ae468d0cc5979ffee9a");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_01.prefab:dc056acc266b425498a52076612375cd");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_02 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_02.prefab:c9cd52aef700a2b44a0e0b1ddc951ad6");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7EmoteResponse_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7EmoteResponse_01.prefab:0fad4ad6c9ce43528394167e0c695273");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeA_01.prefab:413f79be89324bd1a3656208a4ab8583");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeB_01.prefab:041c5edd5da146f79be49351b9d40f06");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_01.prefab:0c0d47fce9d640899d6f3de7387d0c3c");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_02.prefab:63efd08947604319825b373c3f6ef34d");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_03.prefab:0394ea7e8546499f99e131c81eb3838f");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_01.prefab:8806dda480c643ceaad85b1c17847688");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_02.prefab:90b47f4662f444e98605cfcd0bfd2650");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_03.prefab:c079ea3ffd0447aa8adf6f50b50258e4");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Intro_01.prefab:332526943cb8417b8aeedc7a711ff3ba");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Loss_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Loss_01.prefab:a8fc5d1186da47b6a71a5485e3df777b");

	private static readonly AssetReference VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_01 = new AssetReference("VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_01.prefab:4505771016b158046b160e46154bf301");

	private static readonly AssetReference VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_02 = new AssetReference("VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_02.prefab:3b7d18ca0cd88274f8731b794f044f80");

	public static readonly AssetReference KalecBrassRing = new AssetReference("Kalec_BrassRing_Quote.prefab:b96062478a5eccd47bd5e94f1ad7312f");

	private List<string> m_VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPowerLines = new List<string> { VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_01, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_02, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_03 };

	private List<string> m_VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7IdleLines = new List<string> { VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_01, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_02, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Jaina_07()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeA_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeB_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeC_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Intro_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_02, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7EmoteResponse_01, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeA_01, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeB_01, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_01,
			VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_02, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPower_03, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_01, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_02, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Idle_03, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Intro_01, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Loss_01, VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_01, VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_02
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
		yield return PlayLineAlways(actor, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Intro_01);
		yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Intro_01);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7EmoteResponse_01;
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
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(KalecBrassRing, VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_01);
			yield return PlayLineAlways(KalecBrassRing, VO_Story_Minion_Kalec_Male_Dragon_Story_Jaina_Mission7Victory_02);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7Victory_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7Loss_01);
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
		case 3:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeA_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeA_01);
			break;
		case 7:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission7ExchangeB_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeB_01);
			break;
		case 9:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission7ExchangeC_01);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DRG);
	}
}
