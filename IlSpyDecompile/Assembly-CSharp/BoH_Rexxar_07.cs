using System.Collections;
using System.Collections.Generic;

public class BoH_Rexxar_07 : BoH_Rexxar_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Death_01 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Death_01.prefab:eac17d6de0a8b7049ad6e627a7ba65d4");

	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7EmoteResponse_01 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7EmoteResponse_01.prefab:23beabab899cec5478bfc35ef1e4d536");

	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7ExchangeE_01 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7ExchangeE_01.prefab:fb1686f802950ab4db51108db96418a1");

	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7ExchangeF_01 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7ExchangeF_01.prefab:d9024ecd826512d48a1297887e39b63d");

	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_01 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_01.prefab:9cdf0adf803e3a14d8c925af347a88d6");

	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_02 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_02.prefab:b69f1824146d31e4d8cb427bf3cd3f80");

	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_03 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_03.prefab:98403e93fc6880a49aaf78f196ee5bac");

	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_01 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_01.prefab:8de282a89d2a537439fd8dd1a2870275");

	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_02 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_02.prefab:91c67075e501f544ebb49244b298e438");

	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_03 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_03.prefab:e243226a72ac3e844a48bc9fc4f6ead7");

	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Intro_01.prefab:e957ebe67fd8fc248ae8c47ef9fcd2ec");

	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Loss_01 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Loss_01.prefab:423295209b098444787d38d587233e1b");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission7ExchangeA_01.prefab:e0265f3093767e9469ee3f1d041fd0fc");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission7ExchangeA_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission7ExchangeA_02.prefab:22349d60ba8a971489aecf7533226ea2");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7ExchangeC_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7ExchangeC_01.prefab:18dd4908b2156fb47b585d1fb3350c51");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7Intro_01.prefab:1085697f9157d5d4daa1188985ae911e");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7Victory_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7Victory_01.prefab:b5409ed173944b3469080b0325db629c");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeA_01.prefab:6e90a9b4030b8f2479e26c02c6df59be");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeB_01.prefab:891132071bcfb5442844a3657ac7ee80");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeD_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeD_01.prefab:72641b8ca4cf1ff41861c7a84d8cf1d8");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7Intro_01.prefab:c893332be7adfc74cbc70b0534dce55f");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7Victory_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7Victory_01.prefab:bfc338f4beaae1d4da69bc43dd24b6cd");

	private static readonly AssetReference VO_Story_Minion_Cairne_Male_Tauren_Story_Rexxar_Mission7ExchangeC_01 = new AssetReference("VO_Story_Minion_Cairne_Male_Tauren_Story_Rexxar_Mission7ExchangeC_01.prefab:80dcfbb24d09d2343856009c02c33ad0");

	public static readonly AssetReference ThrallBrassRing = new AssetReference("Thrall_BrassRing_Quote.prefab:962e58c9390b0f842a8b64d0d44cf7b4");

	public static readonly AssetReference JainaBrassRing = new AssetReference("JainaMid_BrassRing_Quote.prefab:7eba171d881f6764e81abddbb125bb19");

	private List<string> m_VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPowerLines = new List<string> { VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_01, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_02, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_03 };

	private List<string> m_VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7IdleLines = new List<string> { VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_01, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_02, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Rexxar_07()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Death_01, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7EmoteResponse_01, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7ExchangeE_01, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7ExchangeF_01, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_01, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_02, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_03, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_01, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_02, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_03,
			VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Intro_01, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Loss_01, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission7ExchangeA_01, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission7ExchangeA_02, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7ExchangeC_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7Intro_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7Victory_01, VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeA_01, VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeB_01, VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeD_01,
			VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7Intro_01, VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7Victory_01, VO_Story_Minion_Cairne_Male_Tauren_Story_Rexxar_Mission7ExchangeC_01
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

	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return PlayLineAlways(actor, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Intro_01);
		yield return PlayLineAlways(ThrallBrassRing, VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7Intro_01);
		yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7Intro_01);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPowerLines;
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7EmoteResponse_01;
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
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(ThrallBrassRing, VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7Victory_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7Victory_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Loss_01);
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (cardId == "Story_02_Cairne")
		{
			Actor friendlyActorByCardId = GetFriendlyActorByCardId("Story_02_Cairne");
			if (friendlyActorByCardId != null)
			{
				yield return PlayLineAlways(friendlyActorByCardId, VO_Story_Minion_Cairne_Male_Tauren_Story_Rexxar_Mission7ExchangeC_01);
			}
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7ExchangeC_01);
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
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 5:
			yield return PlayLineAlways(JainaBrassRing, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission7ExchangeA_01);
			yield return PlayLineAlways(ThrallBrassRing, VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeA_01);
			yield return PlayLineAlways(JainaBrassRing, VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission7ExchangeA_02);
			break;
		case 7:
			yield return PlayLineAlways(ThrallBrassRing, VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeB_01);
			break;
		case 13:
			yield return PlayLineAlways(ThrallBrassRing, VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeD_01);
			break;
		case 15:
			yield return PlayLineAlways(actor, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7ExchangeE_01);
			break;
		case 19:
			yield return PlayLineAlways(actor, VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7ExchangeF_01);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_GILFinalBoss);
	}
}
