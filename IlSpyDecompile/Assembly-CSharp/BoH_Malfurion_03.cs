using System.Collections;
using System.Collections.Generic;

public class BoH_Malfurion_03 : BoH_Malfurion_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Turn2_01_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Turn2_01_01.prefab:7bb08f07ba52f6648a829e02eb487881");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3_Victory_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3_Victory_01.prefab:c61b231da2cef824fb0f3fa7e084c47c");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3EmoteResponse_01.prefab:c185892269d345c4c9dbf7f21c9b6716");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3ExchangeB_02 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3ExchangeB_02.prefab:35a36132cfb8d4849925052626d573ae");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_01.prefab:aad833b61489dbf4685ebf2eeb0c3627");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_02 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_02.prefab:8f0d17e6d8c7c704da3e37e301bd8c60");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_03 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_03.prefab:876f1ba624edb9a458abf2d46b1b179f");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_01.prefab:1e77ec26a2325ac41bd55ebc19c670dc");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_02 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_02.prefab:3e2fed721a3834b4fbd36a16972c8089");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_03 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_03.prefab:26e8cf6f72bda7b47a3735ab4fe5c66f");

	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Loss_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Loss_01.prefab:9405c00a59e48b341af329064c19bcde");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3ExchangeA_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3ExchangeA_02.prefab:580dd5b8073ebfe4bbe9c958fd546ba0");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3ExchangeB_01 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3ExchangeB_01.prefab:88cec326552c7af4294ad484a1dc8366");

	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3Victory_03 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3Victory_03.prefab:b56d674b7a383d6458544f552cf32196");

	private static readonly AssetReference VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission3_Victory_02 = new AssetReference("VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission3_Victory_02.prefab:fc363b3ab82aa6546b5618fd880b7714");

	private static readonly AssetReference VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission3_Victory_04 = new AssetReference("VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission3_Victory_04.prefab:049de6375f4a59f43813de4ace958993");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeA_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeA_01.prefab:840bb7195ebb84a40b7904a3d736fe21");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeC_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeC_02_01.prefab:bd43645aeade43c4b9b16f5b15d8adb2");

	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Turn1_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Turn1_02_01.prefab:22e60a23f6686734b9f429258774f9de");

	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Turn1_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Turn1_01_01.prefab:8254a4f7f73afec47ab6b25d383b535b");

	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_ExchangeC_01_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_ExchangeC_01_01.prefab:0325e29a9def4273954bfd4593b840d3");

	public static readonly AssetReference TyrandeBrassRing = new AssetReference("YoungTyrande_Popup_BrassRing.prefab:79f13833a3f5e97449ef744f460e9fbd");

	public static readonly AssetReference CenariusBrassRing = new AssetReference("Cenarius_BrassRing_Quote.prefab:9157110d07b5b004fa0c0f651c71ef81");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_01, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_02, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_03 };

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_01, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_02, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Malfurion_03()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Turn2_01_01, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3_Victory_01, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3EmoteResponse_01, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3ExchangeB_02, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_01, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_02, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_03, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_01, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_02, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_03,
			VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Loss_01, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3ExchangeA_02, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3ExchangeB_01, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3Victory_03, VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission3_Victory_02, VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission3_Victory_04, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeA_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeC_02_01, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Turn1_02_01, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Turn1_01_01,
			VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_ExchangeC_01_01
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
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return PlayLineAlways(CenariusBrassRing, VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Turn2_01_01);
		yield return MissionPlayVO(friendlyActor, VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Turn1_01_01);
		yield return MissionPlayVO(enemyActor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Turn1_02_01);
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
		m_standardEmoteResponseLine = VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3EmoteResponse_01;
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
		case 507:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3_Victory_01);
			yield return PlayLineAlways(GetFriendlyActorByCardId("Story_08_PriestessMaiev"), VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission3_Victory_02);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3Victory_03);
			yield return PlayLineAlways(GetFriendlyActorByCardId("Story_08_PriestessMaiev"), VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission3_Victory_04);
			GameState.Get().SetBusy(busy: false);
			break;
		case 101:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3_Victory_01);
			yield return PlayLineAlways(GetFriendlyActorByCardId("Story_08_PriestessMaievFake"), VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission3_Victory_02);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3Victory_03);
			yield return PlayLineAlways(GetFriendlyActorByCardId("Story_08_PriestessMaievFake"), VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission3_Victory_04);
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
			yield return PlayLineAlways(TyrandeBrassRing, VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_ExchangeC_01_01);
			yield return PlayLineAlways(enemyActor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeC_02_01);
			break;
		case 9:
			yield return PlayLineAlways(enemyActor, VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeA_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3ExchangeA_02);
			break;
		case 5:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3ExchangeB_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3ExchangeB_02);
			break;
		}
	}
}
