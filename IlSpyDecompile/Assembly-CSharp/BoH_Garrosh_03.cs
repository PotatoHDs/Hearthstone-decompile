using System.Collections;
using System.Collections.Generic;

public class BoH_Garrosh_03 : BoH_Garrosh_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3ExchangeA_01.prefab:fb92a428b9f4e844da8a8ad6b2541581");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3ExchangeB_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3ExchangeB_01.prefab:f99ac4d129232824089b0a40f3750ab2");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Intro_01.prefab:fe4c11db6a87e524281c824bb7a2660c");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3PeonInfo_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3PeonInfo_01.prefab:4bcbd90aa9b1f69439d40b24123cb372");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_01.prefab:30e3e1c737fd6c9488f14e86fb555fa3");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_02.prefab:c3ed48d79fff96e4bb94d4dff56975d3");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_03.prefab:4ca1ae8051631cc4bbc1ad2cf2e66022");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Victory_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Victory_01.prefab:dc621d5d237b4444386bb2633291ca19");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission3Victory_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission3Victory_01.prefab:b5f4771b45938c74da73cba402f6e036");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission3Victory_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission3Victory_02.prefab:1f72a59963a802f4c861320b8f7a2eb0");

	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Death_01 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Death_01.prefab:3b59bf02e51569c49920c1c08446398e");

	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3EmoteResponse_01.prefab:96c6ee4af11cea44f8772f5ce784e5a7");

	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3ExchangeA_01 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3ExchangeA_01.prefab:a70a8873cf32f2c449edbf837c335f80");

	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3ExchangeB_01 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3ExchangeB_01.prefab:62e6a12eeadc09541aacb9d8f022d4f3");

	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_01 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_01.prefab:585a78675061e324a9c6cef981e703b6");

	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_02 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_02.prefab:1970ce556f8bba74d992d394446527ed");

	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_03 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_03.prefab:48fd3a86ef03f5748a4d712eeac9ff7d");

	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_01 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_01.prefab:cf186ca5b6437324a97a3bb787afae14");

	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_02 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_02.prefab:edb434646b5853345b719aed984b4225");

	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_03 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_03.prefab:2b074c522c8b08a40b810eeb1bb4320f");

	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Intro_01.prefab:1cadd5d82cb823144b97d42fc809cbe2");

	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Loss_01 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Loss_01.prefab:d09753c0867ecb0418dbaa51fa2c5105");

	public static readonly AssetReference ThrallBrassRing = new AssetReference("Thrall_BrassRing_Quote.prefab:962e58c9390b0f842a8b64d0d44cf7b4");

	private List<string> m_VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3SummonLines = new List<string> { VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_02, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_03 };

	private List<string> m_VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPowerLines = new List<string> { VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_01, VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_02, VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_03 };

	private List<string> m_VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3IdleLines = new List<string> { VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_01, VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_02, VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3ExchangeA_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3ExchangeB_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Intro_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3PeonInfo_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_02, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_03, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Victory_01, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission3Victory_01, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission3Victory_02,
			VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Death_01, VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3EmoteResponse_01, VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3ExchangeA_01, VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3ExchangeB_01, VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_01, VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_02, VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_03, VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_01, VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_02, VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_03,
			VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Intro_01, VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Loss_01
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
		yield return PlayLineAlways(actor, VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Intro_01);
		yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Intro_01);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPowerLines;
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3EmoteResponse_01;
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
		if (missionEvent == 911)
		{
			GameState.Get().SetBusy(busy: true);
			while (m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(busy: false);
			yield break;
		}
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
			yield return PlayLineAlways(ThrallBrassRing, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission3Victory_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Victory_01);
			yield return PlayLineAlways(ThrallBrassRing, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission3Victory_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 502:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3PeonInfo_01);
			break;
		case 503:
			yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3SummonLines);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Loss_01);
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
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3PeonInfo_01);
			break;
		case 6:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3ExchangeA_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3ExchangeA_01);
			break;
		case 12:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3ExchangeB_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3ExchangeB_01);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ICC);
	}
}
