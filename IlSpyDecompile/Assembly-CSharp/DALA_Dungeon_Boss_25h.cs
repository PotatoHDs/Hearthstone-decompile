using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_25h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_01.prefab:642b197412703f943a7bd6bae7fce20a");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_03 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_03.prefab:4965501287045dc499b89deea01e4516");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_04 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_04.prefab:ec1896aa4bde2e846aa4d9772eb30bf0");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_Death_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_Death_01.prefab:c489792aa9ab2c84f88e28236442f0d0");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_DefeatPlayer_01.prefab:71c9b741070ea484297de555cfa46dcd");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_EmoteResponse_01.prefab:35451f0b9b2cc604ca4fff8a4cd47465");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_HeroPowerBossTrigger_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_HeroPowerBossTrigger_01.prefab:7f057d778d09ab14bae2898fd86bf278");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_HeroPowerBossTrigger_02 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_HeroPowerBossTrigger_02.prefab:2a043615cf4c53c4389e3cb15ba23045");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_01.prefab:54ace46e444df87499b62107f27175e9");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_02 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_02.prefab:72d75b01b6cbd704faa2de79aa272986");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_03 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_03.prefab:3cb36417937c6e04aac616e148960e17");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_HeroPowerTrigger_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_HeroPowerTrigger_01.prefab:02fdba25bea00be478a5e912a5a95f9f");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_Idle_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_Idle_01.prefab:6590bda1aea1f534f987e95bc906b372");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_Idle_03 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_Idle_03.prefab:b7f30f385ea28cb4e880d5795eb78170");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_Idle_04 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_Idle_04.prefab:9bcc7c80ab32dd140aca06cf86646ea9");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_Intro_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_Intro_01.prefab:f2426e0cb62783f448f47c5ee6dcc91e");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_IntroGeorge_01.prefab:256912597be247a4390af888c7741a59");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerBlingtron6000_02 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerBlingtron6000_02.prefab:4c98caddb50c6164bb73f3e7408603a2");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerBurgle_02 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerBurgle_02.prefab:05c4a1ff34a46ba4cb53cf6855a16b50");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerGigglingInventor_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerGigglingInventor_01.prefab:33a4a93c38ec5d44189132f52b09dfe5");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerHordeofToys_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerHordeofToys_01.prefab:62c8ebeb4bbcb104fb2a0a3d4c09568a");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerJepettoJoybuzz_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerJepettoJoybuzz_01.prefab:dc8ae52ee7b83bf478509adc3be8e803");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerLackey_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerLackey_01.prefab:c0e950486a2c24549911b26b11514ce0");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerLegendary_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerLegendary_01.prefab:004d3f76f4b38704795b28fa4e8ff8a8");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerMadHatter_02 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerMadHatter_02.prefab:9ffaea17a417d414698f72aae7969d22");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerMossyHorror_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerMossyHorror_01.prefab:2322868af9f4f2b46bff916aa4d3c7b0");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerPogohopper_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerPogohopper_01.prefab:3e1c4653d620c8144b92ea7b47b6ee63");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerSkaterbot_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerSkaterbot_01.prefab:c290c7a4e6148554c88123750e1e4928");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerSylvanas_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerSylvanas_01.prefab:5dfbf8d0c5dabbd4a9452ca9ea7e4e35");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerVoodooDoll_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerVoodooDoll_01.prefab:fbc2098a05c78314f96a7419a08d1431");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_PlayerWrenchcalibur_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_PlayerWrenchcalibur_01.prefab:af1824e515f100e48a412fa5319ba959");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_StartOfGame_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_StartOfGame_01.prefab:08d019a47a171c845a30b8574b7de99a");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_TurnSeven_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_TurnSeven_01.prefab:14e0d01e64e9f4d46a2ca802e69257b0");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_TurnThree_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_TurnThree_01.prefab:dfc3ed17136b0b04795a33e0571b3c1a");

	private static readonly AssetReference VO_DALA_BOSS_25h_Male_Gnome_TurnTwo_01 = new AssetReference("VO_DALA_BOSS_25h_Male_Gnome_TurnTwo_01.prefab:58c2d75aa76c72f408572597ebea04b8");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_25h_Male_Gnome_Idle_01, VO_DALA_BOSS_25h_Male_Gnome_Idle_03, VO_DALA_BOSS_25h_Male_Gnome_Idle_04 };

	private static List<string> m_BossHordeOfToys = new List<string> { VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_01, VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_03, VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_04 };

	private static List<string> m_HeroPowerPlayerTrigger = new List<string> { VO_DALA_BOSS_25h_Male_Gnome_HeroPowerTrigger_01, VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_01, VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_02, VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_03 };

	private static List<string> m_HeroPowerBossTrigger = new List<string> { VO_DALA_BOSS_25h_Male_Gnome_HeroPowerTrigger_01, VO_DALA_BOSS_25h_Male_Gnome_HeroPowerBossTrigger_01, VO_DALA_BOSS_25h_Male_Gnome_HeroPowerBossTrigger_02 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_01, VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_03, VO_DALA_BOSS_25h_Male_Gnome_BossHordeofToys_04, VO_DALA_BOSS_25h_Male_Gnome_Death_01, VO_DALA_BOSS_25h_Male_Gnome_DefeatPlayer_01, VO_DALA_BOSS_25h_Male_Gnome_EmoteResponse_01, VO_DALA_BOSS_25h_Male_Gnome_HeroPowerBossTrigger_01, VO_DALA_BOSS_25h_Male_Gnome_HeroPowerBossTrigger_02, VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_01, VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_02,
			VO_DALA_BOSS_25h_Male_Gnome_HeroPowerPlayerTrigger_03, VO_DALA_BOSS_25h_Male_Gnome_HeroPowerTrigger_01, VO_DALA_BOSS_25h_Male_Gnome_Idle_01, VO_DALA_BOSS_25h_Male_Gnome_Idle_03, VO_DALA_BOSS_25h_Male_Gnome_Idle_04, VO_DALA_BOSS_25h_Male_Gnome_Intro_01, VO_DALA_BOSS_25h_Male_Gnome_IntroGeorge_01, VO_DALA_BOSS_25h_Male_Gnome_PlayerBlingtron6000_02, VO_DALA_BOSS_25h_Male_Gnome_PlayerBurgle_02, VO_DALA_BOSS_25h_Male_Gnome_PlayerGigglingInventor_01,
			VO_DALA_BOSS_25h_Male_Gnome_PlayerHordeofToys_01, VO_DALA_BOSS_25h_Male_Gnome_PlayerJepettoJoybuzz_01, VO_DALA_BOSS_25h_Male_Gnome_PlayerLackey_01, VO_DALA_BOSS_25h_Male_Gnome_PlayerLegendary_01, VO_DALA_BOSS_25h_Male_Gnome_PlayerMadHatter_02, VO_DALA_BOSS_25h_Male_Gnome_PlayerMossyHorror_01, VO_DALA_BOSS_25h_Male_Gnome_PlayerPogohopper_01, VO_DALA_BOSS_25h_Male_Gnome_PlayerSkaterbot_01, VO_DALA_BOSS_25h_Male_Gnome_PlayerSylvanas_01, VO_DALA_BOSS_25h_Male_Gnome_PlayerVoodooDoll_01,
			VO_DALA_BOSS_25h_Male_Gnome_PlayerWrenchcalibur_01, VO_DALA_BOSS_25h_Male_Gnome_StartOfGame_01, VO_DALA_BOSS_25h_Male_Gnome_TurnSeven_01, VO_DALA_BOSS_25h_Male_Gnome_TurnThree_01, VO_DALA_BOSS_25h_Male_Gnome_TurnTwo_01
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
		m_introLine = VO_DALA_BOSS_25h_Male_Gnome_Intro_01;
		m_deathLine = VO_DALA_BOSS_25h_Male_Gnome_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_25h_Male_Gnome_EmoteResponse_01;
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
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
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_25h_Male_Gnome_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId != "DALA_Squeamlish" && cardId != "DALA_Chu")
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
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_25h_Male_Gnome_StartOfGame_01);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_25h_Male_Gnome_TurnTwo_01);
			break;
		case 103:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_25h_Male_Gnome_TurnThree_01);
			break;
		case 104:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_25h_Male_Gnome_TurnSeven_01);
			break;
		case 105:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_25h_Male_Gnome_PlayerLegendary_01);
			break;
		case 106:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerPlayerTrigger);
			break;
		case 107:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerBossTrigger);
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
			case "GVG_119":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_25h_Male_Gnome_PlayerBlingtron6000_02);
				break;
			case "AT_033":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_25h_Male_Gnome_PlayerBurgle_02);
				break;
			case "BOT_270":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_25h_Male_Gnome_PlayerGigglingInventor_01);
				break;
			case "DALA_BOSS_25t":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_25h_Male_Gnome_PlayerHordeofToys_01);
				break;
			case "DAL_752":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_25h_Male_Gnome_PlayerJepettoJoybuzz_01);
				break;
			case "DAL_739":
			case "DAL_741":
			case "DAL_613":
			case "DAL_614":
			case "DAL_615":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_25h_Male_Gnome_PlayerLackey_01);
				break;
			case "GIL_125":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_25h_Male_Gnome_PlayerMadHatter_02);
				break;
			case "GIL_124":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_25h_Male_Gnome_PlayerMossyHorror_01);
				break;
			case "BOT_283":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_25h_Male_Gnome_PlayerPogohopper_01);
				break;
			case "BOT_020":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_25h_Male_Gnome_PlayerSkaterbot_01);
				break;
			case "HERO_05b":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_25h_Male_Gnome_PlayerSylvanas_01);
				break;
			case "GIL_614":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_25h_Male_Gnome_PlayerVoodooDoll_01);
				break;
			case "DAL_063":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_25h_Male_Gnome_PlayerWrenchcalibur_01);
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
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "DALA_BOSS_25t")
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossHordeOfToys);
			}
		}
	}
}
