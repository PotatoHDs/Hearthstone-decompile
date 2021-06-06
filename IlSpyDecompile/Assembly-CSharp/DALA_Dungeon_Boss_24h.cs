using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_24h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_01.prefab:6a36eda969cd3f946b16d482b56dedbb");

	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_02 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_02.prefab:8a37aadc5c09c3c4f87885dab006c346");

	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_03 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_03.prefab:777eaafbaf4236e4fb47ead1b3dee92c");

	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_Death_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_Death_01.prefab:a0b167627eb5e7d419cc50e9dde15d3a");

	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_DefeatPlayer_01.prefab:b8f211e0524d8b945a64119cb46048c6");

	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_EmoteResponse_01.prefab:be047876ab90c24408f98f40a744ede3");

	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_HeroPower_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_HeroPower_01.prefab:93bd22a5b6e244f438184d26b617fae9");

	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_HeroPower_02 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_HeroPower_02.prefab:67c61953593b7d64fb28fba6ce2421ca");

	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_HeroPower_03 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_HeroPower_03.prefab:0ec1760c04951d545976746bdac7f41f");

	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_Idle_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_Idle_01.prefab:425a17cd7acc87747926e7b37d1b59fc");

	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_Idle_02 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_Idle_02.prefab:b2558d35a58b1dd46a67f968d831a745");

	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_Idle_03 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_Idle_03.prefab:1a48d1ce431298b41b993c64429dc5ef");

	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_Intro_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_Intro_01.prefab:d6cb4d700bfe51243a551a17d841a27d");

	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_IntroOlBarkeye_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_IntroOlBarkeye_01.prefab:750b3ff8c9c1a464fbea74c3c851d9f1");

	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_01.prefab:1289e53685264e34ca71d05443a4abf6");

	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_02 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_02.prefab:99d444d737ee78d40a93b0ad421acfeb");

	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_03 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_03.prefab:8f4cfcce5522d794e9469d37d593497d");

	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_PlayerBrannBronzebeard_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_PlayerBrannBronzebeard_01.prefab:a8cc0a1dfc75f6c4d9da1ac3b80fcb51");

	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_PlayerMurmuringElemental_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_PlayerMurmuringElemental_01.prefab:9887498ffd95d0440b8ed19d498334a9");

	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_PlayerShudderwock_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_PlayerShudderwock_01.prefab:a5ba7e14a1247fd48a61e17f65a5bde0");

	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_PlayerSpiritOfTheShark_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_PlayerSpiritOfTheShark_01.prefab:89c35834e23b0f244b246ebb57d8ff90");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_24h_Female_Orc_Idle_01, VO_DALA_BOSS_24h_Female_Orc_Idle_02, VO_DALA_BOSS_24h_Female_Orc_Idle_03 };

	private static List<string> m_BossBigBattlecry = new List<string> { VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_01, VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_02, VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_03 };

	private static List<string> m_PlayerBattlecry = new List<string> { VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_01, VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_02, VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_01, VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_02, VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_03, VO_DALA_BOSS_24h_Female_Orc_Death_01, VO_DALA_BOSS_24h_Female_Orc_DefeatPlayer_01, VO_DALA_BOSS_24h_Female_Orc_EmoteResponse_01, VO_DALA_BOSS_24h_Female_Orc_HeroPower_01, VO_DALA_BOSS_24h_Female_Orc_HeroPower_02, VO_DALA_BOSS_24h_Female_Orc_HeroPower_03, VO_DALA_BOSS_24h_Female_Orc_Idle_01,
			VO_DALA_BOSS_24h_Female_Orc_Idle_02, VO_DALA_BOSS_24h_Female_Orc_Idle_03, VO_DALA_BOSS_24h_Female_Orc_Intro_01, VO_DALA_BOSS_24h_Female_Orc_IntroOlBarkeye_01, VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_01, VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_02, VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_03, VO_DALA_BOSS_24h_Female_Orc_PlayerBrannBronzebeard_01, VO_DALA_BOSS_24h_Female_Orc_PlayerMurmuringElemental_01, VO_DALA_BOSS_24h_Female_Orc_PlayerShudderwock_01,
			VO_DALA_BOSS_24h_Female_Orc_PlayerSpiritOfTheShark_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_24h_Female_Orc_Intro_01;
		m_deathLine = VO_DALA_BOSS_24h_Female_Orc_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_24h_Female_Orc_EmoteResponse_01;
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { VO_DALA_BOSS_24h_Female_Orc_HeroPower_01, VO_DALA_BOSS_24h_Female_Orc_HeroPower_02, VO_DALA_BOSS_24h_Female_Orc_HeroPower_03 };
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Barkeye")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_24h_Female_Orc_IntroOlBarkeye_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId != "DALA_Squeamlish")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
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
		switch (missionEvent)
		{
		case 101:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossBigBattlecry);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerBattlecry);
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
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			switch (cardId)
			{
			case "LOE_077":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_24h_Female_Orc_PlayerBrannBronzebeard_01);
				break;
			case "LOOT_517":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_24h_Female_Orc_PlayerMurmuringElemental_01);
				break;
			case "GIL_820":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_24h_Female_Orc_PlayerShudderwock_01);
				break;
			case "TRL_092":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_24h_Female_Orc_PlayerSpiritOfTheShark_01);
				break;
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
		}
	}
}
