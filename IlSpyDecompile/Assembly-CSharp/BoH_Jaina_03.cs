using System.Collections;
using System.Collections.Generic;

public class BoH_Jaina_03 : BoH_Jaina_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3EmoteResponse_01.prefab:97f6c76ca47678c4eb8b5304572c75c2");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeA_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeA_01.prefab:c5399267f9d5bcc48a4647db9089d1a3");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeB_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeB_01.prefab:602d0ae13ff1c7941b71ef8ebd20c2b7");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeD_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeD_01.prefab:54890be114137604996e1b2335a0f06c");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_01.prefab:c0202084e1d394f4d891f23137f7bfbf");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_02 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_02.prefab:649e928ba2cee174aa61cf99d3dede64");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_03 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_03.prefab:b40bbb664d4412c4482c040c91ef3093");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_01.prefab:794a6e637b82eb5428544728d95cca63");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_02 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_02.prefab:e9af642ef73b2994d83368f20783cbe9");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_03 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_03.prefab:f1974c03c2f627e46b9e83078418cb42");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Intro_01.prefab:c7b5889aed22f25418da2be0a749e04a");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Loss_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Loss_01.prefab:0c15b8bbeb2d178479bcdb854bc4a95c");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Victory_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Victory_01.prefab:5fbc1a1b366dd9946802bbabdc6b690e");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeA_01.prefab:e4472d6be29dc0045b68d11327fae335");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeB_01.prefab:23881a87aff08d84b867eaf06aea7cfd");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeC_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeC_01.prefab:71f4e5216ff00034aad6ac5943ca10fb");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeD_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeD_01.prefab:8adbcfc3ff610bd4bb963d8eb0db62e7");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3Intro_01.prefab:9f9a7586ab9b74941aeeca903c05af39");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3Victory_01.prefab:629b5af6a65213b4a8f7794dcdd62a54");

	private List<string> m_VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPowerLines = new List<string> { VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_01, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_02, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_03 };

	private List<string> m_VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3IdleLines = new List<string> { VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_01, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_02, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Jaina_03()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3EmoteResponse_01, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeA_01, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeB_01, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeD_01, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_01, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_02, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_03, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_01, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_02, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_03,
			VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Intro_01, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Loss_01, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Victory_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeA_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeB_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeC_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeD_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3Intro_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3Victory_01
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
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return PlayLineAlways(actor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3Intro_01);
		yield return PlayLineAlways(enemyActor, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Intro_01);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPowerLines;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
			yield return PlayLineAlways(actor, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Victory_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3Victory_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Loss_01);
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
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeA_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeA_01);
			break;
		case 5:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeB_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeB_01);
			break;
		case 7:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeC_01);
			break;
		case 9:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeD_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeD_01);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ICC);
	}
}
