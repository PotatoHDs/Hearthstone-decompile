using System.Collections;
using System.Collections.Generic;

public class BoH_Rexxar_02 : BoH_Rexxar_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2EmoteResponse_01 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2EmoteResponse_01.prefab:716fc9188ba94b643a1a6b4ce9b4894a");

	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeA_01.prefab:3bdc950307ffcc84c864080e4e5d4c7d");

	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeB_01 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeB_01.prefab:54ba83dfe63168546a893fb4cee9902b");

	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeC_01.prefab:accafbc5ca01b2446a0b1e7e9f9a4b02");

	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeC_02 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeC_02.prefab:5c6862cf543d5c9418dc47f770cb5c3d");

	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeD_01 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeD_01.prefab:a46c829fefa1ac84284b92951e8b78f0");

	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_01.prefab:c53048cb7e2fd2e44b9b5f11af6c37b5");

	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_02.prefab:109b460cf164c7e42a4534731c477fa6");

	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_03.prefab:8b61743bfeb947344ae76e89b2658cd1");

	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_01 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_01.prefab:dcb85e6893637f54c97a3459609e4994");

	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_02 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_02.prefab:6f1f8f107bc26fa4fb1813d29bd796b9");

	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_03 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_03.prefab:bd8d6fb605583c84b874e7006942e659");

	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Intro_01.prefab:19ac28e44145e7242ade5487dda28d35");

	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Loss_01 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Loss_01.prefab:62109ca64bf150c4cbcc3475fe9ee6ca");

	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Victory_01 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Victory_01.prefab:cc536b1ce6887ff42bc913cb74dbc219");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeA_01.prefab:d8a241fa7b4b5ed47883cb20546fba5b");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeB_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeB_01.prefab:f73a4d56ce8a3b348affb739780c7714");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeC_01.prefab:c1287116512150e46a142539b219ee4c");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Intro_01.prefab:7c920ff42bbefa24ca09101f5d66d154");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Intro_02 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Intro_02.prefab:1373a967381b1564d8f7a0ab0af2df6d");

	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Victory_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Victory_01.prefab:6076503bd77e06f4683ceff0c8a0003e");

	private List<string> m_VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPowerLines = new List<string> { VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_01, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_02, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_03 };

	private List<string> m_VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2IdleLines = new List<string> { VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_01, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_02, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Rexxar_02()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2EmoteResponse_01, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeA_01, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeB_01, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeC_01, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeC_02, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeD_01, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_01, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_02, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_03, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_01,
			VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_02, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_03, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Intro_01, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Loss_01, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Victory_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeA_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeB_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeC_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Intro_01, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Intro_02,
			VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Victory_01
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
		yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Intro_01);
		yield return PlayLineAlways(enemyActor, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Intro_01);
		yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Intro_02);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2EmoteResponse_01;
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
			yield return PlayLineAlways(actor, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Victory_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Victory_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Loss_01);
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
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeA_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeA_01);
			break;
		case 3:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeB_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeB_01);
			break;
		case 5:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeC_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeC_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeC_02);
			break;
		case 11:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeD_01);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BT);
	}
}
