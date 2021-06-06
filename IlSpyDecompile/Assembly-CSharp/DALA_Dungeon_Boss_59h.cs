using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_59h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_BossFruitNinja_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_BossFruitNinja_01.prefab:ec31e5279366354429d30b5c681db473");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_BossLotusBruiser_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_BossLotusBruiser_01.prefab:3710a50596b794944abef0f421b58912");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_BossLotusBruiser_02 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_BossLotusBruiser_02.prefab:35dfcc9737ca6ee43bcd40626ac217d2");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_Death_02 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_Death_02.prefab:1badf88f612b34a45a2abc64838432f7");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_DefeatPlayer_01.prefab:a9e9b08790d28624e9bed2efb5e27462");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_EmoteResponse_01.prefab:b78bd5ebf93396e4ab2e77bd02a4ac7b");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_01.prefab:4ece487155b662d44a25041789151e42");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_02 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_02.prefab:7b09b2dd30821d241a7604798b3f438b");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_03 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_03.prefab:97cf1bf704850f84fa7456bda3345f0b");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_04 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_04.prefab:503b1106a44c6374faffaa6cd08ebd31");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_05 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_05.prefab:b9d8419a56e1df04487df03564ad4bc3");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_06 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_06.prefab:e4224acf79137b340893925abf78d893");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_HeroPowerTreasure_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_HeroPowerTreasure_01.prefab:fcda977d73ea6204e90660e8dabee4ff");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_HeroPowerTreasure_02 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_HeroPowerTreasure_02.prefab:54e068b6bb83dd5418756f2cd83a3a66");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_Idle_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_Idle_01.prefab:20ccde4f26275d547b4f3e775f3675a0");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_Idle_02 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_Idle_02.prefab:056214cd3bd86f549839a6b8bb381afb");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_Idle_03 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_Idle_03.prefab:f329d6771e55b6e45bb4da1fecedb945");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_Intro_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_Intro_01.prefab:90150f5850f1c70458b60a3a5a692a69");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_IntroChu_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_IntroChu_01.prefab:07aa21ca3d677234f908e2fc35c1f444");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_IntroGeorge_01.prefab:0e6678e82cad4344e86dc5c2a2d197ea");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_Misc_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_Misc_01.prefab:fe337316aa4d8c44d96da196f0377c56");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_Misc_02 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_Misc_02.prefab:9a101189f342fe645aa8f453ecde1667");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerAyaBlackpaw_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerAyaBlackpaw_01.prefab:3067d9d4b64e6ea419f26b1ceb393ccc");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerDonHancho_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerDonHancho_01.prefab:b1e4825c1303b8341b155ae81bc2bfc8");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_01.prefab:b854c9b3b5df2e342b132330b584956b");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_02 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_02.prefab:d7454efdb0e8c404da24f60c6ffd8617");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_03 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_03.prefab:cdf03091c740d6f4cbdaccef7f0af6a0");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_04 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_04.prefab:9ccafb707f361dd40baa56b74d0ba985");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_05 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_05.prefab:36156b8904570c94db7e26748da6a0f1");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_06 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_06.prefab:c38ec7773586e8b4490f58c9f5cec55a");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_01.prefab:9b851b64fecc6294ba29d137613e5b13");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_02 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_02.prefab:71d078a963d36ab4eb67be2e52b8fd32");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_03 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_03.prefab:298011133d7a1a542829d08ec7f1c10e");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerKazakus_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerKazakus_01.prefab:9e4e3076b4448e147b6800096e892f2e");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_PlayerMadamGoya_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_PlayerMadamGoya_01.prefab:e5b91d6a14110d148a72dd9aadaa82ab");

	private static readonly AssetReference VO_DALA_BOSS_59h_Female_Pandaren_StartOfGame_01 = new AssetReference("VO_DALA_BOSS_59h_Female_Pandaren_StartOfGame_01.prefab:4bfc8fa5b7288bb4c8e932aa21e1d223");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_59h_Female_Pandaren_Idle_01, VO_DALA_BOSS_59h_Female_Pandaren_Idle_02, VO_DALA_BOSS_59h_Female_Pandaren_Idle_03 };

	private static List<string> m_BossLotusBruiser = new List<string> { VO_DALA_BOSS_59h_Female_Pandaren_BossLotusBruiser_01, VO_DALA_BOSS_59h_Female_Pandaren_BossLotusBruiser_02 };

	private static List<string> m_HeroPower = new List<string> { VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_01, VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_02, VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_03, VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_04, VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_05, VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_06 };

	private static List<string> m_HeroPowerTreasure = new List<string> { VO_DALA_BOSS_59h_Female_Pandaren_HeroPowerTreasure_01, VO_DALA_BOSS_59h_Female_Pandaren_HeroPowerTreasure_02 };

	private static List<string> m_PlayerExtortion = new List<string> { VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_01, VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_02, VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_03, VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_04, VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_05, VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_06 };

	private static List<string> m_PlayerJade = new List<string> { VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_01, VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_02, VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_59h_Female_Pandaren_BossFruitNinja_01, VO_DALA_BOSS_59h_Female_Pandaren_BossLotusBruiser_01, VO_DALA_BOSS_59h_Female_Pandaren_BossLotusBruiser_02, VO_DALA_BOSS_59h_Female_Pandaren_Death_02, VO_DALA_BOSS_59h_Female_Pandaren_DefeatPlayer_01, VO_DALA_BOSS_59h_Female_Pandaren_EmoteResponse_01, VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_01, VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_02, VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_03, VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_04,
			VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_05, VO_DALA_BOSS_59h_Female_Pandaren_HeroPower_06, VO_DALA_BOSS_59h_Female_Pandaren_HeroPowerTreasure_01, VO_DALA_BOSS_59h_Female_Pandaren_HeroPowerTreasure_02, VO_DALA_BOSS_59h_Female_Pandaren_Idle_01, VO_DALA_BOSS_59h_Female_Pandaren_Idle_02, VO_DALA_BOSS_59h_Female_Pandaren_Idle_03, VO_DALA_BOSS_59h_Female_Pandaren_Intro_01, VO_DALA_BOSS_59h_Female_Pandaren_IntroChu_01, VO_DALA_BOSS_59h_Female_Pandaren_IntroGeorge_01,
			VO_DALA_BOSS_59h_Female_Pandaren_Misc_01, VO_DALA_BOSS_59h_Female_Pandaren_Misc_02, VO_DALA_BOSS_59h_Female_Pandaren_PlayerAyaBlackpaw_01, VO_DALA_BOSS_59h_Female_Pandaren_PlayerDonHancho_01, VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_01, VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_02, VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_03, VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_04, VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_05, VO_DALA_BOSS_59h_Female_Pandaren_PlayerExtortion_06,
			VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_01, VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_02, VO_DALA_BOSS_59h_Female_Pandaren_PlayerJade_03, VO_DALA_BOSS_59h_Female_Pandaren_PlayerKazakus_01, VO_DALA_BOSS_59h_Female_Pandaren_PlayerMadamGoya_01, VO_DALA_BOSS_59h_Female_Pandaren_StartOfGame_01
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
		m_introLine = VO_DALA_BOSS_59h_Female_Pandaren_Intro_01;
		m_deathLine = VO_DALA_BOSS_59h_Female_Pandaren_Death_02;
		m_standardEmoteResponseLine = VO_DALA_BOSS_59h_Female_Pandaren_EmoteResponse_01;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_59h_Female_Pandaren_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId != "DALA_Chu" && cardId != "DALA_Vessina" && cardId != "DALA_Squeamlish")
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
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_59h_Female_Pandaren_StartOfGame_01);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_59h_Female_Pandaren_Misc_01);
			break;
		case 103:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_59h_Female_Pandaren_Misc_02);
			break;
		case 104:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower);
			break;
		case 105:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerTreasure);
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
			case "CFM_312":
			case "CFM_343":
			case "CFM_691":
			case "CFM_715":
			case "CFM_602":
			case "CFM_690":
			case "CFM_707":
			case "CFM_713":
			case "CFM_717":
				yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_PlayerJade);
				break;
			case "DALA_BOSS_59t3":
				yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_PlayerExtortion);
				break;
			case "CFM_902":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_59h_Female_Pandaren_PlayerAyaBlackpaw_01);
				break;
			case "CFM_685":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_59h_Female_Pandaren_PlayerDonHancho_01);
				break;
			case "CFM_621":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_59h_Female_Pandaren_PlayerKazakus_01);
				break;
			case "CFM_672":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_59h_Female_Pandaren_PlayerMadamGoya_01);
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "DALA_BOSS_59t"))
		{
			if (cardId == "DALA_BOSS_59t2")
			{
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_59h_Female_Pandaren_BossFruitNinja_01);
			}
		}
		else
		{
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossLotusBruiser);
		}
	}
}
