using System.Collections;
using System.Collections.Generic;

public class BoH_Rexxar_04 : BoH_Rexxar_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4Death_01 = new AssetReference("VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4Death_01.prefab:360502f78bbb40d4b66dffc00d88620f");

	private static readonly AssetReference VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4EmoteResponse_01 = new AssetReference("VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4EmoteResponse_01.prefab:21be207809e8454488283ee3034b15a0");

	private static readonly AssetReference VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_01 = new AssetReference("VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_01.prefab:bea44d02563343bc88aac8eeb164edb6");

	private static readonly AssetReference VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_02 = new AssetReference("VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_02.prefab:0b3385edc0ef43bea51732452138d73a");

	private static readonly AssetReference VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_03 = new AssetReference("VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_03.prefab:80a4e2d96e044b3d9dee1b7b8c261d38");

	private static readonly AssetReference VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4Loss_01 = new AssetReference("VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4Loss_01.prefab:e09871da9f6540228ef22387eef08672");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeA_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeA_01.prefab:c7c455d853c3ccc46a8efd8c2b2938ac");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeB_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeB_01.prefab:3b367750f86ea6a4cb6fb327fa0850e0");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeB_02 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeB_02.prefab:f6f55776113c2074796d9a84ba9ab200");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeC_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeC_01.prefab:887c44a315d32b04d8959966eead35ca");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeC_02 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeC_02.prefab:ea44beb27502b6047b9c773d1890c8d8");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Intro_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Intro_01.prefab:0f42d5c8bca12714697ccf9fdac306ba");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Intro_02 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Intro_02.prefab:716f1c8db14f5de47a80dacb52db60ac");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Victory_01.prefab:b928d9fa52037ac4b9aff0a3219783aa");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Victory_02 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Victory_02.prefab:f5642d0ae8a0dc64187beefbf4e2b122");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission4Victory_01.prefab:6572d5e1fae41f7419a45d90ba1de569");

	private static readonly AssetReference VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeA_01 = new AssetReference("VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeA_01.prefab:8fbbe66497f0c2444ace675879852a0d");

	private static readonly AssetReference VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeB_01 = new AssetReference("VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeB_01.prefab:e3bffeeb027339743bf02c29405b3d90");

	private static readonly AssetReference VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeC_01 = new AssetReference("VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeC_01.prefab:4da71e2a2ff621f439e1a6474870b989");

	private static readonly AssetReference VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeC_02 = new AssetReference("VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeC_02.prefab:36854aa8c6879c34ba95886be636ac26");

	private static readonly AssetReference VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeD_01 = new AssetReference("VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeD_01.prefab:e745a4332f1760a4a90e1fc019fd2f80");

	private static readonly AssetReference VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4Intro_01 = new AssetReference("VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4Intro_01.prefab:caa54a11af9c75049942a4d139fe06ff");

	public static readonly AssetReference ThrallBrassRing = new AssetReference("Thrall_BrassRing_Quote.prefab:962e58c9390b0f842a8b64d0d44cf7b4");

	public static readonly AssetReference MogrinBrassRing = new AssetReference("Mogrin_BrassRing_Quote.prefab:952755d4adf3dcb42900b423823b1c00");

	private List<string> m_VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPowerLines = new List<string> { VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_01, VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_02, VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Rexxar_04()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4Death_01, VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4EmoteResponse_01, VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_01, VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_02, VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPower_03, VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4Loss_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeA_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeB_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeB_02, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeC_01,
			VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeC_02, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Intro_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Intro_02, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Victory_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Victory_02, VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission4Victory_01, VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeA_01, VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeB_01, VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeC_01, VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeC_02,
			VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeD_01, VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4Intro_01
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
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Intro_01);
		yield return PlayLineAlways(MogrinBrassRing, VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4Intro_01);
		yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Intro_02);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4HeroPowerLines;
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4EmoteResponse_01;
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
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Victory_01);
			yield return PlayLineAlways(ThrallBrassRing, VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission4Victory_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4Victory_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 502:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeC_01);
			yield return PlayLineAlways(MogrinBrassRing, VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeD_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeC_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_02_Quilboar_Male_Quillboar_Story_Rexxar_Mission4Loss_01);
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
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			yield return PlayLineAlways(MogrinBrassRing, VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeA_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeA_01);
			break;
		case 3:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeB_01);
			yield return PlayLineAlways(MogrinBrassRing, VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeB_01);
			break;
		case 4:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission4ExchangeB_02);
			yield return PlayLineAlways(MogrinBrassRing, VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeC_01);
			yield return PlayLineAlways(MogrinBrassRing, VO_Story_Minion_Mogrin_Male_Orc_Story_Rexxar_Mission4ExchangeC_02);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BT);
	}
}
