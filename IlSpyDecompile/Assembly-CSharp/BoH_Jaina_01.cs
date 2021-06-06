using System.Collections;
using System.Collections.Generic;

public class BoH_Jaina_01 : BoH_Jaina_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1EmoteResponse_01 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1EmoteResponse_01.prefab:f1f2317b5b0ba184491b8941365e4fcf");

	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeB_01.prefab:7679f8bc10b261847a1bbedb56703fb9");

	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeC_01.prefab:85e128b518b235a4cbf3f6bdfe0b79d5");

	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeC_02 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeC_02.prefab:82e538569752834409aae1b9cec23bd4");

	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeD_01 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeD_01.prefab:7373bf47010e1b24e90794a979f8cd75");

	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_01 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_01.prefab:c3b674754ac433a4d824c8580bd8be03");

	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_02 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_02.prefab:54ec7e86cdc402544a65b4e0e5bbbf43");

	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_03 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_03.prefab:3079d3a5abd135140a4da7a737043ff8");

	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_01 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_01.prefab:876d12f76dc335b4cbe358410ea92107");

	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_02 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_02.prefab:271fb8e968323c7438d36ff3048c77b1");

	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_03 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_03.prefab:07882d79818737540960aff35d337c3a");

	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Intro_01.prefab:0c52ab1ee86d2b34fa39057070412b0b");

	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Loss_01 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Loss_01.prefab:d62e0c75e50c74c498b1ccbb2b3a4716");

	private static readonly AssetReference VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Victory_01.prefab:f289444beb58ca34e92119f050293976");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeA_01.prefab:bd746621980b018419f546ed29e7f7dc");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeB_01.prefab:bf22297af6482f4469fa0eb00727d364");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeC_01.prefab:b6226a591581f224fa612b470d5fd503");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeD_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeD_01.prefab:4cff05949da711947a68d497b972a232");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1Intro_01.prefab:64e77102b5930944094ca5ba65fef26b");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1Victory_01.prefab:d44807878b072b543a03f860064d5f54");

	private List<string> m_VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPowerLines = new List<string> { VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_01, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_02, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_03 };

	private List<string> m_VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1IdleLines = new List<string> { VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_01, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_02, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Jaina_01()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1EmoteResponse_01, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeB_01, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeC_01, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeC_02, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeD_01, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_01, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_02, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1HeroPower_03, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_01, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_02,
			VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Idle_03, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Intro_01, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Loss_01, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Victory_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeA_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeB_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeC_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeD_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1Intro_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1Victory_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
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
		yield return PlayLineAlways(actor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1Intro_01);
		yield return PlayLineAlways(enemyActor, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Intro_01);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1IdleLines;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
			yield return PlayLineAlways(actor, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Victory_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1Victory_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1Loss_01);
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
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "CS2_022")
			{
				yield return PlayLineAlways(actor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeB_01);
				yield return PlayLineAlways(enemyActor, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeB_01);
			}
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
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeA_01);
			break;
		case 4:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeC_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeC_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeC_02);
			break;
		case 8:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Antonidas_Male_Human_Story_Jaina_Mission1ExchangeD_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission1ExchangeD_01);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_LOOT);
	}
}
