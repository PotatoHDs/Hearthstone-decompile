using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_52h : GIL_Dungeon
{
	private const string NORMAL_PETE_HERO = "GILA_BOSS_52h";

	private const string BEAST_PETE_HERO = "GILA_BOSS_52h2";

	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_RandomNormalLines = new List<string> { "VO_GILA_BOSS_52h_Male_Human_HeroPower_01.prefab:20c6fda40d4e41b44b9748d7e888dbce", "VO_GILA_BOSS_52h_Male_Human_HeroPower_02.prefab:7a0423953add01e4cb60c5c5bdb084bb", "VO_GILA_BOSS_52h_Male_Human_HeroPower_03.prefab:6ad9cf9211ca5864dae5f436a07e70f9", "VO_GILA_BOSS_52h_Male_Human_HeroPower_04.prefab:3e3f1410a3b4d324495952ece2a32bb2", "VO_GILA_BOSS_52h_Male_Human_HeroPower_05.prefab:6d4e5292c4b6e354dbf2ec8133d8c179" };

	private List<string> m_RandomBeastLines = new List<string> { "VO_GILA_BOSS_52h2_Male_Worgen_HeroPower_01.prefab:61b29626fa70f304ebdbbbebfba21d95", "VO_GILA_BOSS_52h2_Male_Worgen_HeroPower_02.prefab:e5d7c6407be90914f91d3ce724195a0a", "VO_GILA_BOSS_52h2_Male_Worgen_HeroPower_03.prefab:aa12d4c27d623034ea61afd98c2ac0b5" };

	private List<string> m_RandomStealthLines = new List<string> { "VO_GILA_BOSS_52h_Male_Human_EventPlayStealth_02.prefab:07cbc6d7222f1994bbe6cbd812a34f36", "VO_GILA_BOSS_52h_Male_Human_EventPlayStealth_03.prefab:84bc5431d2bc953478514fc509d7c7c8" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			"VO_GILA_BOSS_52h_Male_Human_IntroALT_02.prefab:188027863e77f9a45b98d167b20b4c84", "VO_GILA_BOSS_52h_Male_Human_EmoteResponse_02.prefab:7814835bce0a20b4c8f08b44b8f49df1", "VO_GILA_BOSS_52h_Male_Human_Death_01.prefab:cca528b7424987a4ba23789c274141ee", "VO_GILA_BOSS_52h_Male_Human_HeroPower_01.prefab:20c6fda40d4e41b44b9748d7e888dbce", "VO_GILA_BOSS_52h_Male_Human_HeroPower_02.prefab:7a0423953add01e4cb60c5c5bdb084bb", "VO_GILA_BOSS_52h_Male_Human_HeroPower_03.prefab:6ad9cf9211ca5864dae5f436a07e70f9", "VO_GILA_BOSS_52h_Male_Human_HeroPower_04.prefab:3e3f1410a3b4d324495952ece2a32bb2", "VO_GILA_BOSS_52h_Male_Human_HeroPower_05.prefab:6d4e5292c4b6e354dbf2ec8133d8c179", "VO_GILA_BOSS_52h_Male_Human_EventPlayStealth_02.prefab:07cbc6d7222f1994bbe6cbd812a34f36", "VO_GILA_BOSS_52h_Male_Human_EventPlayStealth_03.prefab:84bc5431d2bc953478514fc509d7c7c8",
			"VO_GILA_BOSS_52h_Male_Human_EventPlayRoyalGift_02.prefab:a4be1f544dcbac64cb60b482d491d21d", "VO_GILA_BOSS_52h_Male_Human_EventTransform_01.prefab:cde26ca6a55ffa141b921e42b957e435", "VO_GILA_BOSS_52h2_Male_Worgen_EmoteResponse_02.prefab:7b90e896be2eb4f42ad4d49d1e3b02f9", "VO_GILA_BOSS_52h2_Male_Worgen_HeroPower_01.prefab:61b29626fa70f304ebdbbbebfba21d95", "VO_GILA_BOSS_52h2_Male_Worgen_HeroPower_02.prefab:e5d7c6407be90914f91d3ce724195a0a", "VO_GILA_BOSS_52h2_Male_Worgen_HeroPower_03.prefab:aa12d4c27d623034ea61afd98c2ac0b5", "VO_GILA_BOSS_52h2_Male_Worgen_Death_01.prefab:1632ceca55b1d6648bdd4f462a96222e"
		})
		{
			PreloadSound(item);
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_52h_Male_Human_IntroALT_02.prefab:188027863e77f9a45b98d167b20b4c84", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			if (cardId == "GILA_BOSS_52h")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_52h_Male_Human_EmoteResponse_02.prefab:7814835bce0a20b4c8f08b44b8f49df1", Notification.SpeechBubbleDirection.TopRight, actor));
			}
			if (cardId == "GILA_BOSS_52h2")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_52h2_Male_Worgen_EmoteResponse_02.prefab:7b90e896be2eb4f42ad4d49d1e3b02f9", Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
	}

	protected override string GetBossDeathLine()
	{
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCardId();
		if (cardId == "GILA_BOSS_52h")
		{
			return "VO_GILA_BOSS_52h_Male_Human_Death_01.prefab:cca528b7424987a4ba23789c274141ee";
		}
		if (cardId == "GILA_BOSS_52h2")
		{
			return "VO_GILA_BOSS_52h2_Male_Worgen_Death_01.prefab:1632ceca55b1d6648bdd4f462a96222e";
		}
		return null;
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (m_playedLines.Contains(item))
		{
			yield break;
		}
		switch (missionEvent)
		{
		case 101:
		{
			string text = PopRandomLineWithChance(m_RandomNormalLines);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
			break;
		}
		case 102:
		{
			string text = PopRandomLineWithChance(m_RandomBeastLines);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
			break;
		}
		case 103:
			yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_52h_Male_Human_EventTransform_01.prefab:cde26ca6a55ffa141b921e42b957e435");
			break;
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (cardId == "GILA_824")
		{
			yield return PlayBossLine(enemyActor, "VO_GILA_BOSS_52h_Male_Human_EventPlayRoyalGift_02.prefab:a4be1f544dcbac64cb60b482d491d21d");
		}
		if (entity.HasTag(GAME_TAG.STEALTH))
		{
			string text = PopRandomLineWithChance(m_RandomStealthLines);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(enemyActor, text);
			}
		}
	}
}
