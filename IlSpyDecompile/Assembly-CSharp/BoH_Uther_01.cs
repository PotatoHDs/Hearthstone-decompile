using System.Collections;
using System.Collections.Generic;

public class BoH_Uther_01 : BoH_Uther_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1EmoteResponse_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1EmoteResponse_01.prefab:6ebb0fea32ff4c349a5f8b7aae619843");

	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeA_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeA_01.prefab:2ba6e169726790d4081bb1cac793f094");

	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeB_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeB_01.prefab:b8152f45c74e36a45a9fa9c45aa1c557");

	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeC_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeC_01.prefab:f5dfede0433c44e419a78a6bf5fe4a15");

	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeD_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeD_01.prefab:7ff78212de1509b47b49209e8ebc8633");

	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeE_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeE_01.prefab:9741febdfc8cb7a479d151e11aec3fd2");

	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_01.prefab:b2acb68be5981164d9931948276c8890");

	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_02 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_02.prefab:208df8702b71e4b48993e93d43c4dfcc");

	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_03 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_03.prefab:39424ef839485854f82d6c7d6e72b488");

	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Intro_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Intro_01.prefab:3bbe739b088c05f44806dca970eac7bd");

	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_01.prefab:df701de681b58da459fc60154affd95e");

	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_02 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_02.prefab:6675540aeaa9e07449cdc618884bc1e4");

	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_03 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_03.prefab:4a74f92b61f445b448ee28e2472bd1a2");

	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Loss_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Loss_01.prefab:b85153205ec2b90428275f66eb2f8934");

	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Victory_01 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Victory_01.prefab:53b892d5f2654a64398284400c50f79d");

	private static readonly AssetReference VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Victory_02 = new AssetReference("VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Victory_02.prefab:e2eea205b5c49df4da4c4222644b215f");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1ExchangeA_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1ExchangeA_01.prefab:95a4e131e994da644bc27f987e6027a3");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1ExchangeB_01.prefab:1de7f27b13ca5c7429bbd315732a63f3");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1Victory_01.prefab:e1e380f1f4b12b848885890489d6eb9a");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1Intro_02 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1Intro_02.prefab:b686e817e1c779d43acb076469b6d72f");

	private List<string> m_HeroPowerLines = new List<string> { VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_01, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_02, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_03 };

	private List<string> m_IdleLines = new List<string> { VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_01, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_02, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_03 };

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
			VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1EmoteResponse_01, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeA_01, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeB_01, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeC_01, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeD_01, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeE_01, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_01, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_02, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1HeroPower_03, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_01,
			VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_02, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Idle_03, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Intro_01, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Loss_01, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Victory_01, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Victory_02, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1ExchangeA_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1ExchangeB_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1Victory_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1Intro_02
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return PlayLineAlways(actor, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Intro_01);
		yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1Intro_02);
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
		m_standardEmoteResponseLine = VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1EmoteResponse_01;
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
			yield return PlayLineAlways(enemyActor, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Victory_01);
			yield return PlayLineAlways(enemyActor, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Victory_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 502:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1Victory_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			yield return PlayLineAlways(enemyActor, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1Loss_01);
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "CS1_112"))
		{
			if (cardId == "GIL_134")
			{
				yield return PlayLineOnlyOnce(actor, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeD_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeE_01);
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
			yield return PlayLineAlways(enemyActor, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeA_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1ExchangeA_01);
			break;
		case 5:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission1ExchangeB_01);
			yield return PlayLineAlways(enemyActor, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeB_01);
			break;
		case 7:
			yield return PlayLineAlways(enemyActor, VO_Story_03_Alonsus_Male_Human_Story_Uther_Mission1ExchangeC_01);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_Default);
	}
}
