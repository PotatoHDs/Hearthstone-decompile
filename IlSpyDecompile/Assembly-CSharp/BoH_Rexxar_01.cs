using System.Collections;
using System.Collections.Generic;

public class BoH_Rexxar_01 : BoH_Rexxar_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1EmoteResponse_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1EmoteResponse_01.prefab:ef43dd1312c008e4a9b3dbb321c0e367");

	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeA_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeA_01.prefab:0dbcc192b3531174698b3277d91250d1");

	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeB_01.prefab:562df924ed754654d8be78383ad2dbc1");

	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeC_01.prefab:e291bd797129e9d48881956190f6d126");

	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeD_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeD_01.prefab:0af8cbc15d3f84e4f8a7c9863d2bfb57");

	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeE_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeE_01.prefab:45c6f8b97397bed4cb012212b63a857e");

	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_01.prefab:4366e37436d191b42b3834b96c6f68e0");

	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_02 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_02.prefab:e05e57faf5cc0184c9a70ac274813de7");

	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_03 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_03.prefab:99d914bb1e042ff4592b8b3892a07eeb");

	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_01.prefab:bc47c19f2c53eeb4381323e456080891");

	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_02 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_02.prefab:eb34d381066165b45853f2cea20ae637");

	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_03 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_03.prefab:c31e9af630472934c8cf3aa8e1168d7f");

	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Intro_01.prefab:d73e87daf7ab344418bafa48d1e7346b");

	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Loss_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Loss_01.prefab:3f056de75e46f9143b0f2adbcfdf4ec9");

	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Victory_01.prefab:0eb371f8f251d7c46b939f8fccb20f1e");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeB_01.prefab:fbf526cf04257c34082a45e5b37b86a5");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeC_01.prefab:21ff8edafd740c74eb259fddf9f6c232");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeD_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeD_01.prefab:85943387edc668a4197dd6d9ccccaac8");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1Intro_01.prefab:3f11648187f385e45af7a57734b3bdee");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1Victory_01.prefab:fff76b2ef452488488babc1e29116456");

	private List<string> m_VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPowerLines = new List<string> { VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_01, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_02, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_03 };

	private List<string> m_VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1IdleLines = new List<string> { VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_01, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_02, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Rexxar_01()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1EmoteResponse_01, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeA_01, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeB_01, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeC_01, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeD_01, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeE_01, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_01, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_02, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_03, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_01,
			VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_02, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_03, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Intro_01, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Loss_01, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Victory_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeB_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeC_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeD_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1Intro_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1Victory_01
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
		yield return PlayLineAlways(actor, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Intro_01);
		yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1Intro_01);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1EmoteResponse_01;
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1Victory_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Victory_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Loss_01);
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
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeA_01);
			break;
		case 3:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeB_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeB_01);
			break;
		case 7:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeC_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeC_01);
			break;
		case 9:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeD_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeD_01);
			break;
		case 11:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeE_01);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BT);
	}
}
