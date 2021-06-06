using System.Collections;
using System.Collections.Generic;

public class BoH_Uther_04 : BoH_Uther_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference Story_04_Darkportal_Death = new AssetReference("Story_04_Darkportal_Death.prefab:2457ed615b87f4644af4528837554e4e");

	private static readonly AssetReference Story_04_Darkportal_EmoteResponse = new AssetReference("Story_04_Darkportal_EmoteResponse.prefab:c1d824da692e02840a52d831053755a0");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeA_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeA_01.prefab:554a329cd72b44f41884fd1d13335aaf");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeB_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeB_01.prefab:d8ef4d696d3295544818b9e29dbc6849");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeD_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeD_01.prefab:ddb028020f81a45449aa25548c53df7f");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4Victory_01.prefab:ebe5c6adbe40a7441a8d115618aba3eb");

	private static readonly AssetReference VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeA_01 = new AssetReference("VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeA_01.prefab:7c183759d499460429614809e924efb2");

	private static readonly AssetReference VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeB_01 = new AssetReference("VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeB_01.prefab:6dcda9c2639256b47a3940d75fd0640d");

	private static readonly AssetReference VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeC_01 = new AssetReference("VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeC_01.prefab:9705efe18dcb35b4e8a05805b708f253");

	private static readonly AssetReference VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4Victory_01 = new AssetReference("VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4Victory_01.prefab:2845658c04680684bb569b5edc83941d");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4Intro_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4Intro_01.prefab:22bd16f3f61d4ff4b87ebbae46f20abe");

	private static readonly AssetReference VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4Intro_01 = new AssetReference("VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4Intro_01.prefab:582f47b3809509c48ab4257a6aff6cad");

	public static readonly AssetReference TuralyonBrassRing = new AssetReference("Turalyon_BrassRing_Quote.prefab:40afbe0d5b4da0643baf2ebf5756548d");

	private List<string> m_HeroPowerLines = new List<string> { Story_04_Darkportal_EmoteResponse };

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
			Story_04_Darkportal_Death, Story_04_Darkportal_EmoteResponse, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4Intro_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeA_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeB_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeD_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4Victory_01, VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeA_01, VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeB_01, VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeC_01,
			VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4Victory_01, VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4Intro_01
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
		yield return PlayLineAlways(TuralyonBrassRing, VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4Intro_01);
		yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4Intro_01);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_standardEmoteResponseLine = Story_04_Darkportal_EmoteResponse;
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
		case 504:
			yield return PlayLineAlways(actor, Story_04_Darkportal_EmoteResponse);
			break;
		case 502:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor2, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeD_01);
			GameState.Get().SetBusy(busy: true);
			break;
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor2, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4Victory_01);
			yield return PlayLineAlways(TuralyonBrassRing, VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4Victory_01);
			GameState.Get().SetBusy(busy: true);
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
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 3:
			yield return PlayLineAlways(TuralyonBrassRing, VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeA_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeA_01);
			break;
		case 9:
			yield return PlayLineAlways(TuralyonBrassRing, VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeB_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission4ExchangeB_01);
			break;
		case 15:
			yield return PlayLineAlways(TuralyonBrassRing, VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission4ExchangeC_01);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.Store_PacksBT);
	}
}
