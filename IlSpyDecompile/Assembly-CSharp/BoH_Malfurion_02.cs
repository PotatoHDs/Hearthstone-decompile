using System.Collections;
using System.Collections.Generic;

public class BoH_Malfurion_02 : BoH_Malfurion_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Death_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Death_01.prefab:b8a47da9b0bd4a6448d223bef43d0220");

	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_EmoteResponse_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_EmoteResponse_01.prefab:317e163701a77f84885c360265b7dde5");

	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_01_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_01_01.prefab:635024ae6a2db654e8c362c3872663d9");

	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_02_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_02_01.prefab:09d7a3dc443396146b14728f2a259a7f");

	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_03_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_03_01.prefab:caadce461e4d5f6449f0a22e3c7927e6");

	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_ExchangeA_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_ExchangeA_01.prefab:11b59196f3f88a14687333b92be8fecc");

	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_Loss_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_Loss_01.prefab:ee1e6da806ec47c2a9e815fb60456d1a");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2ExchangeA_02 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2ExchangeA_02.prefab:56b055f6adf889a40b58d21ebdae2830");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2ExchangeD_02 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2ExchangeD_02.prefab:a6061c992cd5f8441affa79c42bccdc5");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Intro_02 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Intro_02.prefab:b0d7066942a6ed440925d9657ac5cfb7");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Intro_04 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Intro_04.prefab:0c619a5fd68444f46858b47a7d3238c4");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeA_01.prefab:e283643f45dfd4646be4a091e3319229");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeB_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeB_02.prefab:2753323cad2b0944d988affc7ca63814");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeC_03 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeC_03.prefab:8430d6ada4b5b5c488f13ac2e3cc97e7");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeD_03 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeD_03.prefab:a8b82e26b654faf45924c4af1fe44902");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2Intro_03 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2Intro_03.prefab:8fe6ed1f5c3a0c54aa506b86fb53ff1a");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Turn1_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Turn1_01_01.prefab:99adf22deefe98f4bb271310cb564eb1");

	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2ExchangeC_01.prefab:10b01c598d4dc6549a1bef8bfcff6614");

	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_01.prefab:b43846f3a5ae7814f9b47a7138433885");

	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_02 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_02.prefab:b5e8ea72dde49d34b9fb738134caf50a");

	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_03 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_03.prefab:f0c1b712611c9064b9afa7bfdec2bf44");

	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Intro_01.prefab:be70851298e525844ad9cb3ed9cb4bf2");

	private static readonly AssetReference VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission2ExchangeE_01 = new AssetReference("VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission2ExchangeE_01.prefab:f1bb3cf4be596f44e8c11b51599e9104");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Turn1_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Turn1_02_01.prefab:bb40d6ba961eac94aa12326f5241b821");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Victory_01_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Victory_01_01.prefab:0c0de402265457f488187c4400c4b07b");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_PreMission4_Intro_01_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_PreMission4_Intro_01_01.prefab:f06c936a8b4b0ea4398b87b4f5b50df9");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Victory_03 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Victory_03.prefab:f3d3696044b7b2047986c9de7d4413cb");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Victory_04 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Victory_04.prefab:4b3b457b48d16f244959f6bd18901972");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Victory_02_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Victory_02_01.prefab:27a15072a8c732e46a10856d40ab1f86");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_PreMission4_Intro_02_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_PreMission4_Intro_02_01.prefab:883bced611fdb5c43bfed83b5949d416");

	public static readonly AssetReference IllidanBrassRing = new AssetReference("DemonHunter_Illidan_Popup_BrassRing.prefab:8c007b8e8be417c4fbd9738960e6f7f0");

	public static readonly AssetReference TyrandeBrassRing = new AssetReference("YoungTyrande_Popup_BrassRing.prefab:79f13833a3f5e97449ef744f460e9fbd");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_01_01, VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_02_01, VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_03_01 };

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_01, VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_02, VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Malfurion_02()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Death_01, VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_EmoteResponse_01, VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_01_01, VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_02_01, VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_03_01, VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_ExchangeA_01, VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_Loss_01, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2ExchangeA_02, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2ExchangeD_02, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Intro_02,
			VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Intro_04, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeA_01, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeB_02, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeC_03, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeD_03, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2Intro_03, VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2ExchangeC_01, VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_01, VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_02, VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_03,
			VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Intro_01, VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission2ExchangeE_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Turn1_02_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Victory_01_01, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Victory_03, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Victory_04, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Turn1_01_01, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Victory_02_01, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_PreMission4_Intro_02_01
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
		yield return MissionPlayVO(actor, VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Intro_01);
		yield return MissionPlayVO(GetFriendlyActorByCardId("Story_08_IllidanDormant"), VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Intro_02);
		yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2Intro_03);
		yield return MissionPlayVO(GetFriendlyActorByCardId("Story_08_IllidanDormant"), VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Intro_04);
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
		m_deathLine = VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Death_01;
		m_standardEmoteResponseLine = VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_EmoteResponse_01;
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
			yield return PlayLineAlways(actor, VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_Loss_01);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(GetFriendlyActorByCardId("Story_08_Illidan"), VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Victory_01_01);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Victory_02_01);
			yield return PlayLineAlways(GetFriendlyActorByCardId("Story_08_Illidan"), VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Victory_03);
			yield return PlayLineAlways(GetFriendlyActorByCardId("Story_08_Illidan"), VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Victory_04);
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_PreMission4_Intro_02_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 105:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(GetFriendlyActorByCardId("Story_08_Illidan"), VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2ExchangeD_02);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeD_03);
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
			yield return PlayLineAlways(friendlyActor, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Turn1_01_01);
			yield return PlayLineAlways(GetFriendlyActorByCardId("Story_08_IllidanDormant"), VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Turn1_02_01);
			break;
		case 3:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeA_01);
			yield return PlayLineAlways(GetFriendlyActorByCardId("Story_08_IllidanDormant"), VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2ExchangeA_02);
			break;
		case 5:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeB_02);
			break;
		case 7:
			yield return PlayLineAlways(actor, VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2ExchangeC_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeC_03);
			break;
		}
	}
}
