using System.Collections;
using System.Collections.Generic;

public class BoH_Malfurion_06 : BoH_Malfurion_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeA_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeA_02.prefab:bb25de34e9808d942a93449fda3bed89");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeB_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeB_02.prefab:f49830ba77a76ff4389a70350fcc3b73");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeB_04 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeB_04.prefab:8093cc5d853617b42b883360513f4e53");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6Intro_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6Intro_02.prefab:19fd67bf211acb84ab265741fdde2900");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6Victory_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6Victory_02.prefab:70b41dea79be1a74fbf01d559bd97daf");

	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6_Victory_01 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6_Victory_01.prefab:5f29262c528e356428114f6ba56e3b71");

	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Death_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Death_01.prefab:e9436bdf88abc0d4e9333e1520229159");

	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6EmoteResponse_01 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6EmoteResponse_01.prefab:5f3e9d0d524ecad4bbff645e8e698936");

	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeC_01 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeC_01.prefab:bc96057748a532545a959c3c8243b6ba");

	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeD_01 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeD_01.prefab:0167f7261d7621040a1dcc088d4b2194");

	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeD_02 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeD_02.prefab:0810cc106b4284345acb8b87227696b0");

	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_01 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_01.prefab:9953b28ad0cb60c49b6e17fb9be382a3");

	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_02 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_02.prefab:20357308e663c404fa7772e3ea48dc6e");

	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_03 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_03.prefab:26be946be95128d45b37cf0556018d26");

	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_01 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_01.prefab:afd7cfb7a1e961647b7037525e2f2201");

	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_02 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_02.prefab:2ca3aeefa6bf7524882a27315c99e875");

	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_03 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_03.prefab:3b0c0d926d876ff4682e5d6244facb09");

	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Intro_01.prefab:9155ebe461da8ef44a3aa7cd31c7c1b2");

	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Loss_01 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Loss_01.prefab:16f68c87e82d48749abf8979da8b7e5d");

	private static readonly AssetReference VO_Story_Minion_Broll_Male_NightElf_Story_Malfurion_Mission6ExchangeB_01 = new AssetReference("VO_Story_Minion_Broll_Male_NightElf_Story_Malfurion_Mission6ExchangeB_01.prefab:7cd08f049449d1a4b89f07b1d4d488eb");

	private static readonly AssetReference VO_Story_Minion_Broll_Male_NightElf_Story_Malfurion_Mission6ExchangeB_03 = new AssetReference("VO_Story_Minion_Broll_Male_NightElf_Story_Malfurion_Mission6ExchangeB_03.prefab:95cad737db484074a90262d01e0c5c33");

	private static readonly AssetReference VO_Story_Minion_Ysera_Female_Dragon_Story_Malfurion_Mission6_Victory_03 = new AssetReference("VO_Story_Minion_Ysera_Female_Dragon_Story_Malfurion_Mission6_Victory_03.prefab:88ef7dd0e33ade24faef4589a21b4340");

	private static readonly AssetReference VO_Story_Minion_Ysera_Female_Dragon_Story_Malfurion_Mission6ExchangeA_01 = new AssetReference("VO_Story_Minion_Ysera_Female_Dragon_Story_Malfurion_Mission6ExchangeA_01.prefab:e54c45896f345024b8e4d788d32b74e8");

	public static readonly AssetReference YseraBrassRing = new AssetReference("Ysera_BrassRing_Quote.prefab:1b5ee7911e0cc0f48bff1d9ea60a95e1");

	public static readonly AssetReference BrollBrassRing = new AssetReference("Broll_BrassRing_Quote.prefab:1bfe5acde48846249b4b7716c3ff0d8c");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_01, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_02, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_03 };

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_01, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_02, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Malfurion_06()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeA_02, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeB_02, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeB_04, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6Intro_02, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6Victory_02, VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Death_01, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6_Victory_01, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6EmoteResponse_01, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeC_01, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeD_01,
			VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeD_02, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_01, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_02, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_03, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_01, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_02, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_03, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Intro_01, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Loss_01, VO_Story_Minion_Broll_Male_NightElf_Story_Malfurion_Mission6ExchangeB_01,
			VO_Story_Minion_Broll_Male_NightElf_Story_Malfurion_Mission6ExchangeB_03, VO_Story_Minion_Ysera_Female_Dragon_Story_Malfurion_Mission6_Victory_03, VO_Story_Minion_Ysera_Female_Dragon_Story_Malfurion_Mission6ExchangeA_01
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
		yield return MissionPlayVO(actor, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Intro_01);
		yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6Intro_02);
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
		m_OverrideMusicTrack = MusicPlaylistType.InGame_GILFinalBoss;
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6EmoteResponse_01;
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
		case 507:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6Victory_02);
			yield return PlayLineAlways(YseraBrassRing, VO_Story_Minion_Ysera_Female_Dragon_Story_Malfurion_Mission6_Victory_03);
			GameState.Get().SetBusy(busy: false);
			break;
		case 102:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6_Victory_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 101:
			yield return PlayLineAlways(BrollBrassRing, VO_Story_Minion_Broll_Male_NightElf_Story_Malfurion_Mission6ExchangeB_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeB_02);
			yield return PlayLineAlways(BrollBrassRing, VO_Story_Minion_Broll_Male_NightElf_Story_Malfurion_Mission6ExchangeB_03);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeB_04);
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
			yield return PlayLineAlways(YseraBrassRing, VO_Story_Minion_Ysera_Female_Dragon_Story_Malfurion_Mission6ExchangeA_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeA_02);
			break;
		case 5:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeC_01);
			break;
		case 7:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeD_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeD_02);
			break;
		}
	}
}
