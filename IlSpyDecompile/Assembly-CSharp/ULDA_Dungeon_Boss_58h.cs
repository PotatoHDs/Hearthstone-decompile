using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_58h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerLivingMonument_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerLivingMonument_01.prefab:10027f560e4353f4d8aaf3dd6a18898a");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerPsychopomp_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerPsychopomp_01.prefab:4f84aad90382bfe429a92583cb3f5c19");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerPuzzleboxofYoggSaron_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerPuzzleboxofYoggSaron_01.prefab:c94aa2c293a98cd4ba52096c408b075f");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_DeathALT_01.prefab:bf8def8371d6dc947b9b0f84f9941525");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_DefeatPlayer_01.prefab:6286d90ec77d5d54ea36d17dc176d52d");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_EmoteResponse_01.prefab:dfdff85207b88de4db6e4d7f5f991a1a");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_01.prefab:0ff622d4b56a962408408e8ff8ad68e9");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_02 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_02.prefab:8f117d7ee1ea7634db39d813a4d5c7ee");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_03 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_03.prefab:7e3d3d5ac718833458d094e2beae6957");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_04 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_04.prefab:3e9c1f2ec72adee45865d6bf9884dcee");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_05 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_05.prefab:637507084eebf5b4fba0aaa4a0734fb5");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_01.prefab:3b9ffa981367b8c42a0b1ef4434f7337");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_02 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_02.prefab:ab100961a96613d41a3607c52c769993");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_03 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_03.prefab:fb933ab3036871e4481670605b9b578e");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_04 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_04.prefab:a285c3db5303db948977a28bf209c635");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_Idle_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_Idle_01.prefab:748ded937e27a1d409ba2e9e45977ae8");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_Idle_02 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_Idle_02.prefab:a651e54904ed20740bce4c0c9dd9c9cd");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_Intro_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_Intro_01.prefab:839d799f179f6ca4ca49bd94098a4b19");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_IntroBrannResponse_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_IntroBrannResponse_01.prefab:c069f2b8d672ae94d8181c1995f29976");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_PlayerHigherLearning_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_PlayerHigherLearning_01.prefab:98aa17f8ca94965479d3d74122811e46");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerDiscover_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerDiscover_01.prefab:f3f68e8cdd765814e880ede7384e813f");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerReadingHieroglyphics_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerReadingHieroglyphics_01.prefab:15685b183c5435b4285acd10ac9cab6b");

	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerSecret_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerSecret_01.prefab:308c8b0f75b035f4fae01435d7a9f924");

	private List<string> m_HeroPowerTriggerCorrectLines = new List<string> { VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_01, VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_02, VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_03, VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_04, VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_05 };

	private List<string> m_HeroPowerTriggerIncorrectLines = new List<string> { VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_01, VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_02, VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_03, VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_04 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_58h_Male_Tolvir_Idle_01, VO_ULDA_BOSS_58h_Male_Tolvir_Idle_02 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerLivingMonument_01, VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerPsychopomp_01, VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerPuzzleboxofYoggSaron_01, VO_ULDA_BOSS_58h_Male_Tolvir_DeathALT_01, VO_ULDA_BOSS_58h_Male_Tolvir_DefeatPlayer_01, VO_ULDA_BOSS_58h_Male_Tolvir_EmoteResponse_01, VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_01, VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_02, VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_03, VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_04,
			VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_05, VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_01, VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_02, VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_03, VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_04, VO_ULDA_BOSS_58h_Male_Tolvir_Idle_01, VO_ULDA_BOSS_58h_Male_Tolvir_Idle_02, VO_ULDA_BOSS_58h_Male_Tolvir_Intro_01, VO_ULDA_BOSS_58h_Male_Tolvir_IntroBrannResponse_01, VO_ULDA_BOSS_58h_Male_Tolvir_PlayerHigherLearning_01,
			VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerDiscover_01, VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerReadingHieroglyphics_01, VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerSecret_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_ULDA_BOSS_58h_Male_Tolvir_Intro_01;
		m_deathLine = VO_ULDA_BOSS_58h_Male_Tolvir_DeathALT_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_58h_Male_Tolvir_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Elise")
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
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerTriggerCorrectLines);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerTriggerIncorrectLines);
			break;
		case 104:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerDiscover_01);
			break;
		case 103:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerSecret_01);
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "ULD_155")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerReadingHieroglyphics_01);
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "ULD_193":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerLivingMonument_01);
				break;
			case "ULD_268":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerPsychopomp_01);
				break;
			case "ULD_216":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerPuzzleboxofYoggSaron_01);
				break;
			}
		}
	}
}
