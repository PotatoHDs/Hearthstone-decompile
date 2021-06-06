using System.Collections;
using System.Collections.Generic;

public class BoH_Uther_07 : BoH_Uther_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission7Victory_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission7Victory_01.prefab:338c563ff80f6ba43b049d188281b13c");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission7Victory_02 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission7Victory_02.prefab:0ab659ff0a0232244ac63a1d3289ca18");

	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Uther_Mission7Victory_03 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Uther_Mission7Victory_03.prefab:13b046e0cd1aad348b3540958ca141b3");

	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7EmoteResponse_01 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7EmoteResponse_01.prefab:af9a7c1b9fbe4ee4b988cec78c45b164");

	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeA_01.prefab:c9601bd05b1885b448f7316ed4644095");

	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeB_01.prefab:e743c19f6a349bf47aa975a1558551bb");

	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeC_01 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeC_01.prefab:909ff51ac63194b4b8554bd8bd45d1e4");

	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_01 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_01.prefab:ab2b1ab368a3ac744a6ced0488ffe589");

	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_02 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_02.prefab:d7b20f10c964cd641a0fedb34b7a966e");

	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_03 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_03.prefab:ebcffde9dfc93a448aed34359f97e770");

	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_01 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_01.prefab:b594a43bba164e84a9cfb527412d6d73");

	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_02 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_02.prefab:6228abd6176151b4880c880d86944086");

	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_03 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_03.prefab:3603a30ac8ae0b1499e5fc393c786082");

	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Loss_01 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Loss_01.prefab:5b959a6f3693532458355a428ad00a10");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7ExchangeA_01.prefab:6de5ab34a1457d641ad5a8c4c0d54446");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7ExchangeB_01.prefab:5f58f0b33877f8c42956366461eee5ef");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Victory_02 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Victory_02.prefab:e1ffbd73a32113943b73b413fe230366");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Victory_04 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Victory_04.prefab:608d3f5052fce9742bb46dcf0dc1f560");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Intro_01.prefab:817593da4dea5ce4ab755a16d4bf7a31");

	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Intro_01 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Intro_01.prefab:96e13fc13d48aa147961633212885376");

	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Intro_02 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Intro_02.prefab:4eebb5ccc0b19654ca84ea9e0bb7a58c");

	public static readonly AssetReference ArthasBrassRing = new AssetReference("Arthas_BrassRing_Quote.prefab:49bb0ac905ae3c54cbf3624451b62ab4");

	public static readonly AssetReference JainaBrassRing = new AssetReference("JainaMid_BrassRing_Quote.prefab:7eba171d881f6764e81abddbb125bb19");

	private List<string> m_HeroPowerLines = new List<string> { VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_01, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_02, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_03 };

	private List<string> m_IdleLines = new List<string> { VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_01, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_02, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_03 };

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
			VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission7Victory_01, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission7Victory_02, VO_Story_Hero_Jaina_Female_Human_Story_Uther_Mission7Victory_03, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7EmoteResponse_01, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeA_01, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeB_01, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeC_01, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_01, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_02, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_03,
			VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_01, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_02, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_03, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Intro_01, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Intro_02, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Loss_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7ExchangeA_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7ExchangeB_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Victory_02, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Victory_04,
			VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Intro_01
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
		yield return PlayLineAlways(enemyActor, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Intro_01);
		yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Intro_01);
		yield return PlayLineAlways(enemyActor, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Intro_02);
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
		m_standardEmoteResponseLine = VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7EmoteResponse_01;
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
		case 504:
			yield return PlayLineAlways(actor, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Loss_01);
			break;
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(ArthasBrassRing, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission7Victory_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Victory_02);
			yield return PlayLineAlways(ArthasBrassRing, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission7Victory_02);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Victory_04);
			yield return PlayLineAlways(JainaBrassRing, VO_Story_Hero_Jaina_Female_Human_Story_Uther_Mission7Victory_03);
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
			yield return PlayLineAlways(actor, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeA_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7ExchangeA_01);
			break;
		case 7:
			yield return PlayLineAlways(actor, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeB_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7ExchangeB_01);
			break;
		case 12:
			yield return PlayLineAlways(actor, VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeC_01);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DRG);
	}
}
