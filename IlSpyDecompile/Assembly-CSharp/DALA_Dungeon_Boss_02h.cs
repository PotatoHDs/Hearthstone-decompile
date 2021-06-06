using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_02h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_Death_02 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_Death_02.prefab:1b50b7f37adbbb947939bec415a2bd92");

	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_DefeatPlayer_01.prefab:3e3f9e952283cde4593ed7caaa001e44");

	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_EmoteResponse_01.prefab:dd24979e7bcc4364da7e20844d88046c");

	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_HeroPower_04 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_HeroPower_04.prefab:45f67c748641b6647befbed904d8d44d");

	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_HeroPower_05 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_HeroPower_05.prefab:087fd15142f909844a74f5d49cff515b");

	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_02 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_02.prefab:6102ced58599bde41bb5496e62b5770b");

	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_04 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_04.prefab:bcdbe31471b89b743b89898822468a45");

	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_05 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_05.prefab:164f85a40f4fb0644a76c31333e1225e");

	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_Idle_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_Idle_01.prefab:8ed2a948ac75a2b44a7b98d748065829");

	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_Idle_02 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_Idle_02.prefab:2af709dc4aa48364f92c22bfe09500aa");

	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_Idle_03 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_Idle_03.prefab:323104a83847e9b49802bf634e3d1bc5");

	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_Intro_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_Intro_01.prefab:295b44c099df1d04ab737350cd56781c");

	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_IntroOlBarkeye_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_IntroOlBarkeye_01.prefab:4700ab3bd72c59b499d0c99a1754b981");

	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_IntroSqueamlish_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_IntroSqueamlish_01.prefab:85be90b1756f4ed479eb8cd6fc392df0");

	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_PlayerAxe_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_PlayerAxe_01.prefab:047a9199ec799d34ea7d2c8368738125");

	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_PlayerBanana_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_PlayerBanana_01.prefab:26f331e2886384942b26c24c4ec1dbbf");

	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_PlayerBananaSplit_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_PlayerBananaSplit_01.prefab:0804876260c58cd49bc6c7f06312d007");

	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_PlayerDagger_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_PlayerDagger_01.prefab:847cb52f1546b7848b947d0692765b0b");

	private static readonly AssetReference VO_DALA_BOSS_02h_Male_Troll_PlayerVendor_01 = new AssetReference("VO_DALA_BOSS_02h_Male_Troll_PlayerVendor_01.prefab:723c5f77b0f8b6d45b9b527463b7203c");

	private List<string> m_HeroPowerLines = new List<string> { VO_DALA_BOSS_02h_Male_Troll_HeroPower_04, VO_DALA_BOSS_02h_Male_Troll_HeroPower_05 };

	private List<string> m_HeroPowerFullLines = new List<string> { VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_02, VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_04, VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_05 };

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_02h_Male_Troll_Idle_01, VO_DALA_BOSS_02h_Male_Troll_Idle_02, VO_DALA_BOSS_02h_Male_Troll_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_02h_Male_Troll_Death_02, VO_DALA_BOSS_02h_Male_Troll_DefeatPlayer_01, VO_DALA_BOSS_02h_Male_Troll_EmoteResponse_01, VO_DALA_BOSS_02h_Male_Troll_HeroPower_04, VO_DALA_BOSS_02h_Male_Troll_HeroPower_05, VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_02, VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_04, VO_DALA_BOSS_02h_Male_Troll_HeroPowerFull_05, VO_DALA_BOSS_02h_Male_Troll_Idle_01, VO_DALA_BOSS_02h_Male_Troll_Idle_02,
			VO_DALA_BOSS_02h_Male_Troll_Idle_03, VO_DALA_BOSS_02h_Male_Troll_Intro_01, VO_DALA_BOSS_02h_Male_Troll_IntroOlBarkeye_01, VO_DALA_BOSS_02h_Male_Troll_IntroSqueamlish_01, VO_DALA_BOSS_02h_Male_Troll_PlayerAxe_01, VO_DALA_BOSS_02h_Male_Troll_PlayerBanana_01, VO_DALA_BOSS_02h_Male_Troll_PlayerBananaSplit_01, VO_DALA_BOSS_02h_Male_Troll_PlayerDagger_01, VO_DALA_BOSS_02h_Male_Troll_PlayerVendor_01
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
		m_introLine = VO_DALA_BOSS_02h_Male_Troll_Intro_01;
		m_deathLine = VO_DALA_BOSS_02h_Male_Troll_Death_02;
		m_standardEmoteResponseLine = VO_DALA_BOSS_02h_Male_Troll_EmoteResponse_01;
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
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_02h_Male_Troll_IntroOlBarkeye_01, Notification.SpeechBubbleDirection.TopRight, actor));
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

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
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
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerLines);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerFullLines);
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
			case "EX1_014t":
			case "TRL_509t":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_02h_Male_Troll_PlayerBanana_01);
				break;
			case "DALA_725":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_02h_Male_Troll_PlayerBananaSplit_01);
				break;
			case "CS2_106":
			case "EX1_247":
			case "EX1_411":
			case "GIL_653":
			case "TRL_304":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_02h_Male_Troll_PlayerAxe_01);
				break;
			case "UNG_061":
			case "CS2_080":
			case "TRL_074":
			case "BOT_286":
			case "EX1_133":
			case "ICC_850":
			case "LOOT_542":
			case "CS2_083b":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_02h_Male_Troll_PlayerDagger_01);
				break;
			case "AT_111":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_02h_Male_Troll_PlayerVendor_01);
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
		}
	}
}
