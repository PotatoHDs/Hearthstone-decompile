using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_65h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_BossEyeforanEye_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_BossEyeforanEye_01.prefab:390e94c8e77ba9e4b9a347022a05a4db");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_BossHumility_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_BossHumility_01.prefab:671e50b1ce7674245beb986394fcc7dd");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_BossLayOnHands_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_BossLayOnHands_01.prefab:f461373f7b2adb04eaa37f90b9fe5729");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_Death_02 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_Death_02.prefab:5cf11e0a01c3f7e4b9dde240712415ec");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_DefeatPlayer_01.prefab:b67c48e548e24fa488a019cac4cf2dd1");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_EmoteResponse_01.prefab:3207f5882ae2c04469f29e3eea82da3d");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_HeroPowerStopDeath_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_HeroPowerStopDeath_01.prefab:c8930a487a536b544a0193601bccc199");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_HeroPowerStopDeath_02 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_HeroPowerStopDeath_02.prefab:2dcd16fd807729246911e911a40b49e2");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_01.prefab:70bff1535f9660f4eac0cffb62eec3cf");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_02 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_02.prefab:466206bb35d9a0e47a53868a9aa87009");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_03 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_03.prefab:39241a52aab0f9847ad4c98a2d303fb5");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_04 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_04.prefab:25906b21edd629b4ca2bc664447b8f15");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_05 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_05.prefab:e10fd41592c9c4c46897d5b668800419");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_06 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_06.prefab:7225083100d01b14684dcdd4108096f1");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_Idle_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_Idle_01.prefab:892e0a34f193fc646bd2a1bf97e91341");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_Idle_02 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_Idle_02.prefab:c252747806b75be4d9adf150ccba7561");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_Idle_03 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_Idle_03.prefab:c9daeb2708ce90a4987c7f91d7ebcf18");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_Intro_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_Intro_01.prefab:28e77ca8c33599547bf4597f3bd3fc91");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_IntroEduora_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_IntroEduora_01.prefab:1d02cd8a6b1b5fa4f8ff885e7a88110c");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_IntroRakanishu_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_IntroRakanishu_01.prefab:98cd56bee6a90fe46b7a0453cf76e2bc");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_IntroSqueamlish_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_IntroSqueamlish_01.prefab:153550a6d611e2a4ca6297c64b2b46c1");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_IntroTekahn_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_IntroTekahn_01.prefab:e960efa22176f0a4c88338b130a3132b");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_PlayerAlexstraza_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_PlayerAlexstraza_01.prefab:ba59d91762f89df43adcda7f91a07895");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_01.prefab:7400a91dd5fba7d40889d911652b9c34");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_02 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_02.prefab:bf1ea20e369dcd84fb91d3ffdf5e71f1");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_03 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_03.prefab:f0558e96733ebef4abdff3597d47f074");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_PlayerOrbOfUntold_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_PlayerOrbOfUntold_01.prefab:c5bfe1b284f452c41bcc45ba83c33e3e");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_PlayerPlatedScarabDeathrattle_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_PlayerPlatedScarabDeathrattle_01.prefab:207432fab37d49844bfb37bcf91798f7");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_Misc_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_Misc_01.prefab:4ce55a0f7c35aaf45b2f07311c29d975");

	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_Misc_02 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_Misc_02.prefab:96a103f242de05d4d93b5d08ed5cddc4");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_65h_Male_HighElf_Idle_01, VO_DALA_BOSS_65h_Male_HighElf_Idle_02, VO_DALA_BOSS_65h_Male_HighElf_Idle_03, VO_DALA_BOSS_65h_Male_HighElf_Misc_01 };

	private static List<string> m_HeroPowerStopDeathLines = new List<string> { VO_DALA_BOSS_65h_Male_HighElf_HeroPowerStopDeath_01, VO_DALA_BOSS_65h_Male_HighElf_HeroPowerStopDeath_02 };

	private static List<string> m_HeroPowerTriggerLines = new List<string> { VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_01, VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_02, VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_03, VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_04, VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_05, VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_06, VO_DALA_BOSS_65h_Male_HighElf_Misc_02 };

	private static List<string> m_PlayerGainArmorLines = new List<string> { VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_01, VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_02, VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_65h_Male_HighElf_BossEyeforanEye_01, VO_DALA_BOSS_65h_Male_HighElf_BossHumility_01, VO_DALA_BOSS_65h_Male_HighElf_BossLayOnHands_01, VO_DALA_BOSS_65h_Male_HighElf_Death_02, VO_DALA_BOSS_65h_Male_HighElf_DefeatPlayer_01, VO_DALA_BOSS_65h_Male_HighElf_EmoteResponse_01, VO_DALA_BOSS_65h_Male_HighElf_HeroPowerStopDeath_01, VO_DALA_BOSS_65h_Male_HighElf_HeroPowerStopDeath_02, VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_01, VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_02,
			VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_03, VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_04, VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_05, VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_06, VO_DALA_BOSS_65h_Male_HighElf_Idle_01, VO_DALA_BOSS_65h_Male_HighElf_Idle_02, VO_DALA_BOSS_65h_Male_HighElf_Idle_03, VO_DALA_BOSS_65h_Male_HighElf_Intro_01, VO_DALA_BOSS_65h_Male_HighElf_IntroEduora_01, VO_DALA_BOSS_65h_Male_HighElf_IntroRakanishu_01,
			VO_DALA_BOSS_65h_Male_HighElf_IntroSqueamlish_01, VO_DALA_BOSS_65h_Male_HighElf_IntroTekahn_01, VO_DALA_BOSS_65h_Male_HighElf_PlayerAlexstraza_01, VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_01, VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_02, VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_03, VO_DALA_BOSS_65h_Male_HighElf_PlayerOrbOfUntold_01, VO_DALA_BOSS_65h_Male_HighElf_PlayerPlatedScarabDeathrattle_01, VO_DALA_BOSS_65h_Male_HighElf_Misc_01, VO_DALA_BOSS_65h_Male_HighElf_Misc_02
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
		m_introLine = VO_DALA_BOSS_65h_Male_HighElf_Intro_01;
		m_deathLine = VO_DALA_BOSS_65h_Male_HighElf_Death_02;
		m_standardEmoteResponseLine = VO_DALA_BOSS_65h_Male_HighElf_EmoteResponse_01;
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
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
			switch (cardId)
			{
			case "DALA_DALA_Eudora":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_65h_Male_HighElf_IntroEduora_01, Notification.SpeechBubbleDirection.TopRight, actor));
				return;
			case "DALA_Rakanishu":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_65h_Male_HighElf_IntroRakanishu_01, Notification.SpeechBubbleDirection.TopRight, actor));
				return;
			case "DALA_Squeamlish":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_65h_Male_HighElf_IntroSqueamlish_01, Notification.SpeechBubbleDirection.TopRight, actor));
				return;
			case "DALA_Tekahn":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_65h_Male_HighElf_IntroTekahn_01, Notification.SpeechBubbleDirection.TopRight, actor));
				return;
			}
			if (cardId != "DALA_George" && cardId != "DALA_Chu")
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
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerStopDeathLines);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerTriggerLines);
			break;
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerGainArmorLines);
			break;
		case 104:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_65h_Male_HighElf_PlayerAlexstraza_01);
			break;
		case 105:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_65h_Male_HighElf_BossEyeforanEye_01);
			break;
		case 106:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_65h_Male_HighElf_PlayerPlatedScarabDeathrattle_01);
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
			if (cardId == "DALA_712")
			{
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_65h_Male_HighElf_PlayerOrbOfUntold_01);
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
		if (!(cardId == "EX1_360"))
		{
			if (cardId == "EX1_354")
			{
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_65h_Male_HighElf_BossLayOnHands_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_65h_Male_HighElf_BossHumility_01);
		}
	}
}
