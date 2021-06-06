using System.Collections;
using System.Collections.Generic;

public class BoH_Malfurion_04 : BoH_Malfurion_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Death_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Death_01.prefab:7c3b0168dcf6b1f4c98413fcf42948b1");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5EmoteResponse_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5EmoteResponse_01.prefab:8fe4ef854d34eb642b887b2839e26477");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_01.prefab:004cc80b01319e949835075a088d4afd");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_02 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_02.prefab:bb49ea15214f8e942abd5a0acc34f0cd");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeB_01.prefab:1bbdd253bbb750744a650a2700c83477");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_01.prefab:eed6aad568b49994f9bf9586d892a5d7");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_02 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_02.prefab:c366fec51d4e1e84e90eefdeea718820");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_03 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_03.prefab:1f598960da1dc2c4ea8cebdd8212cb2b");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_01.prefab:5ee1e8614f9b8dd4cb13259a5bc6fe60");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_02 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_02.prefab:2ddce4251af42ba48af7da0a1053c204");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_03 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_03.prefab:b022e45ce90ad39459387cade279ecfa");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Intro_01.prefab:e3952b6e2e24c3a49abff1b0fe787061");

	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Loss_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Loss_01.prefab:7c86c24452e96ad45b62b1986f9beed0");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeA_01.prefab:6e661728092d62948b857a0e3597052b");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeB_01.prefab:026844931acc6da43820b3bdcce51d4e");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Malfurion_Mission4Intro_04 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Malfurion_Mission4Intro_04.prefab:3e4381fb042bdbb4c942de4834d2c636");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Malfurion_Mission4Intro_06 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Malfurion_Mission4Intro_06.prefab:4f32d1fcbef966741944fb3af4d80482");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeA_01.prefab:d20577e581019f54e9fcf7158162c757");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeB_01.prefab:a328ce4f273c4f446813a583f27174bd");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Victory_01.prefab:e7ed3771a3d428742939f8e3daf96b70");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeA_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeA_02.prefab:2d952be3a6137ac44bbc077bda581e71");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeA_03 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeA_03.prefab:89433a0909a0e064ab752c6f5d96f6d4");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeE_01 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeE_01.prefab:7d1cce3d5d500854cb8f1a0eb86ff332");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeE_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeE_02.prefab:9300638bb9d17294787e17582679829c");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeF_01 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeF_01.prefab:9878a7f2bca65f649ad3f3e972fdfe6c");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeF_03 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeF_03.prefab:fad18f4e23144b041bcd7d455af62130");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Intro_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Intro_02.prefab:3a8572329f45efc4fb1b21b7de229e9e");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Intro_05 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Intro_05.prefab:921ba69bde21815439f81d59336741bf");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Victory_01.prefab:fc46b20014f83a74ebba38379a3916e2");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Thrall_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Thrall_01.prefab:bf1e78dfc4187b94b860298f1f0a46a9");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Victory_01.prefab:c08db08b83d3282449ad5cd3fbc52acf");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Malfurion_Mission4ExchangeG_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Malfurion_Mission4ExchangeG_01.prefab:4ae5bd8f271cdba4d899e5bda57fb444");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Malfurion_Mission4Intro_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Malfurion_Mission4Intro_03.prefab:d4240388279d751409061fb0e1a986fe");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Malfurion_Mission4Intro_07 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Malfurion_Mission4Intro_07.prefab:a5c3a92c803a767438bd5d59859f000e");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4ThrallHelp_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4ThrallHelp_01.prefab:13d7b4dc4dd719f45992532dc0e29c75");

	private static readonly AssetReference VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission4ExchangeF_02 = new AssetReference("VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission4ExchangeF_02.prefab:328c0843ab357e64fbfaece1fecacbe0");

	private static readonly AssetReference VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission4Intro_01 = new AssetReference("VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission4Intro_01.prefab:5c9c0033ae0ab384ebb00ea25fc6edc7");

	public static readonly AssetReference JainaBrassRing = new AssetReference("JainaMid_BrassRing_Quote.prefab:7eba171d881f6764e81abddbb125bb19");

	public static readonly AssetReference ThrallBrassRing = new AssetReference("Thrall_BrassRing_Quote.prefab:962e58c9390b0f842a8b64d0d44cf7b4");

	public static readonly AssetReference TyrandeBrassRing = new AssetReference("YoungTyrande_Popup_BrassRing.prefab:79f13833a3f5e97449ef744f460e9fbd");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_01, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_02, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_03 };

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_01, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_02, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Malfurion_04()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Death_01, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5EmoteResponse_01, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_01, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_02, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeB_01, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_01, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_02, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_03, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_01, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_02,
			VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_03, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Intro_01, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Loss_01, VO_Story_Hero_Jaina_Female_Human_Story_Malfurion_Mission4Intro_04, VO_Story_Hero_Jaina_Female_Human_Story_Malfurion_Mission4Intro_06, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeA_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeB_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Victory_01, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeA_02, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeA_03,
			VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeE_01, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeE_02, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeF_01, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeF_03, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Intro_02, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Intro_05, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Victory_01, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Victory_01, VO_Story_Hero_Thrall_Male_Orc_Story_Malfurion_Mission4Intro_03, VO_Story_Hero_Thrall_Male_Orc_Story_Malfurion_Mission4Intro_07,
			VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission4Intro_01, VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission4ExchangeF_02
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
		yield return MissionPlayVO(TyrandeBrassRing, VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission4Intro_01);
		yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Intro_02);
		yield return MissionPlayVO(ThrallBrassRing, VO_Story_Hero_Thrall_Male_Orc_Story_Malfurion_Mission4Intro_03);
		yield return MissionPlayVO(JainaBrassRing, VO_Story_Hero_Jaina_Female_Human_Story_Malfurion_Mission4Intro_04);
		yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Intro_05);
		yield return MissionPlayVO(JainaBrassRing, VO_Story_Hero_Jaina_Female_Human_Story_Malfurion_Mission4Intro_06);
		yield return MissionPlayVO(ThrallBrassRing, VO_Story_Hero_Thrall_Male_Orc_Story_Malfurion_Mission4Intro_07);
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
		m_OverrideMusicTrack = MusicPlaylistType.InGame_DHPrologueBoss;
		base.OnCreateGame();
		m_deathLine = VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Death_01;
		m_standardEmoteResponseLine = VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5EmoteResponse_01;
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
			yield return PlayLineAlways(actor, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(JainaBrassRing, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Victory_01);
			yield return PlayLineAlways(ThrallBrassRing, VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Victory_01);
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
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Intro_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeA_02);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeA_03);
			break;
		case 5:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_02);
			yield return PlayLineAlways(JainaBrassRing, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeA_01);
			break;
		case 13:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeB_01);
			yield return PlayLineAlways(JainaBrassRing, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeB_01);
			break;
		case 1:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeE_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeE_02);
			break;
		case 9:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeF_01);
			yield return PlayLineAlways(TyrandeBrassRing, VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission4ExchangeF_02);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeF_03);
			break;
		}
	}
}
