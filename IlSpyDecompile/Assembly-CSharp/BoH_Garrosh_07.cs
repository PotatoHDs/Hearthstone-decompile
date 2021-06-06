using System.Collections;
using System.Collections.Generic;

public class BoH_Garrosh_07 : BoH_Garrosh_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7EmoteResponse_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7EmoteResponse_01.prefab:a932c77d70720da4589486633ed1e7e9");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeB_01.prefab:9571179a848137b43a0458654e27365d");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_01.prefab:e043aa0297f133e4289883dcdd532ea8");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_02.prefab:b1d5670fbbb1e4649bb92078e71120c6");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_01.prefab:27e1ba1890eb9bb4fb374a12c35dd7a5");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_02.prefab:b5cf47dc7e289014988bee1b5eb5dd83");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_03 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_03.prefab:f5ff18556f3d32942820785cf22ea405");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_01.prefab:9a19642236c02fe418745e7e11602195");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_02.prefab:0dc91717346316847b1888503adf81ad");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_03 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_03.prefab:5c721ed34bf80b94ca22d377a903cfe8");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Intro_01.prefab:7d5847a2087e80941beddff135e0ce41");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Loss_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Loss_01.prefab:66f6e13da484def49ae357f5bddddf37");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_01.prefab:30f58743aba7f9e4e9c2c6c77ab5034d");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_02.prefab:e427713bab99d9e47ab6a960bb182063");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeB_01.prefab:6c5ffb3161419ae4490dbe6cfd1a15aa");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Intro_01.prefab:13f81fc1c76cba04e8b15ad99a81d423");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_01.prefab:5b39f80bab51d9c4b80eb758a6f72183");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_02.prefab:64e3347a98a90114d8089589e8f51b30");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_03.prefab:b29bb9bb3386f1d42a4b4b7e54173c39");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Victory_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Victory_01.prefab:eebbe511e35670b48bc483037a87dc6d");

	private List<string> m_VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPowerLines = new List<string> { VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_01, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_02, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_03 };

	private List<string> m_VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7IdleLines = new List<string> { VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_01, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_02, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_03 };

	private List<string> m_missionEventTrigger502Lines = new List<string> { VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_02, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7EmoteResponse_01, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeB_01, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_01, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_02, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_01, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_02, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPower_03, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_01, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_02, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Idle_03,
			VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Intro_01, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Loss_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_02, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeB_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Intro_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_02, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_03, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Victory_01
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return PlayLineAlways(actor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Intro_01);
		yield return PlayLineAlways(enemyActor, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Intro_01);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7HeroPowerLines;
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7EmoteResponse_01;
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
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor2, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Victory_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 112:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor2, m_missionEventTrigger502Lines);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Loss_01);
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
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_02);
			break;
		case 5:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeB_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeB_01);
			break;
		case 9:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_02);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DRG);
	}
}
