using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_58h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_BossDrawBomb_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_BossDrawBomb_01.prefab:c018edf5d81376645bae333230a0a84b");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_BossGoblinBombs_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_BossGoblinBombs_01.prefab:1e1e25e3e7ffa404d9d0fe5b0e11d820");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_BossShuffleBomb_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_BossShuffleBomb_01.prefab:5349b2330a4594f4e9372bcf06f73138");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_Death_02 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_Death_02.prefab:6727de2312a8965458798bf2fd5fb78d");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_DefeatPlayer_02 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_DefeatPlayer_02.prefab:b45759b23450ad041ae25605fb05ac12");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_EmoteResponse_01.prefab:39fc19e5c6f5dcb49b9b22fc72876d01");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_HeroPower_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_HeroPower_01.prefab:7716e39859a4f90468597e461b956413");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_HeroPower_02 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_HeroPower_02.prefab:446254a13756c4b48be1e54ebc16fe89");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_HeroPower_03 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_HeroPower_03.prefab:99e665ea05a93ad47930687edbe49124");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_HeroPower_04 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_HeroPower_04.prefab:15765d15daaf2434f91cf054ea69eb61");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_HeroPower_05 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_HeroPower_05.prefab:662cf91675ef18e4c961f042adc1018e");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_HeroPower_06 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_HeroPower_06.prefab:2f1ba1da1c417914f9c6981f805e8091");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_HeroPowerSilence_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_HeroPowerSilence_01.prefab:0216a6aac9cb52a4291f432aeaefdc97");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_Idle_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_Idle_01.prefab:d72ec50b5af6bfe41ac4ac1fb77c6afa");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_Idle_02 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_Idle_02.prefab:014362af9edf01c439dea0cd9be8b423");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_Idle_03 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_Idle_03.prefab:68f4e1db56e257b48a92fa185dee6f5d");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_Idle_04 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_Idle_04.prefab:210958be480c3e04ba84f2ba1932d8d7");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_Intro_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_Intro_01.prefab:19a499d9201d8e048972a0a3bd1963ad");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_IntroEudora_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_IntroEudora_01.prefab:ee8299fe7a2fda7419575752a0c6d340");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_IntroRakanishu_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_IntroRakanishu_01.prefab:4368deef56bdf014c9fb54c686cb6eea");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_PlayerBoombots_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_PlayerBoombots_01.prefab:1d1594903e88b6940a20d15cac6bd7ae");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_PlayerBoommasterFlark_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_PlayerBoommasterFlark_01.prefab:76b2c7b6d4892124fa1013a52e8b1106");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_PlayerBoomzooka_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_PlayerBoomzooka_01.prefab:5f0002ac7dd14d349a714be0066a4fc7");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_PlayerDrBoom_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_PlayerDrBoom_01.prefab:d4c69fc26993b2e48af11ba8d6b66c41");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_PlayerFlyBy_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_PlayerFlyBy_01.prefab:65ee7c7a61783cf4d8082efabad07474");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_PlayerGoblinBombs_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_PlayerGoblinBombs_01.prefab:4781d4c1863aaa0439e311246a28bd56");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_TurnFour_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_TurnFour_01.prefab:38ac059bf6795464fa02666f1a1ceb3f");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_TurnOne_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_TurnOne_01.prefab:a93153188d08dbb439d78e055aaca36d");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_TurnOne_02 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_TurnOne_02.prefab:dce4bf49dc723294cb2e38b92ccc1184");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_TurnSix_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_TurnSix_01.prefab:b16fcec3bc9d32c45921ddf858172d48");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_TurnTwo_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_TurnTwo_01.prefab:30ebb6945e952044e8db8480d9ffd79a");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_01 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_01.prefab:4840364699660be40af966effa48ae79");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_02 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_02.prefab:678a22ea088f77240b824d2d1adfd706");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_03 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_03.prefab:78f65f0bdbcdd864783f51d900641ee5");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_04 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_04.prefab:51d6db609bff30a4686b2c966c49a244");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_05 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_05.prefab:0972d7dddb7ec394e97b564429883d9b");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_08 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_08.prefab:819cdfc24f908d94a9e81f9f713dd378");

	private static readonly AssetReference VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_09 = new AssetReference("VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_09.prefab:a4ba172c63a39244e8e81fa3c21d120b");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_58h_Male_Goblin_Idle_01, VO_DALA_BOSS_58h_Male_Goblin_Idle_02, VO_DALA_BOSS_58h_Male_Goblin_Idle_03, VO_DALA_BOSS_58h_Male_Goblin_Idle_04 };

	private static List<string> m_WiresTrigger = new List<string> { VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_01, VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_02, VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_03, VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_04, VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_05, VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_08, VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_09 };

	private static List<string> m_TurnOne = new List<string> { VO_DALA_BOSS_58h_Male_Goblin_TurnOne_01, VO_DALA_BOSS_58h_Male_Goblin_TurnOne_02 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_58h_Male_Goblin_BossDrawBomb_01, VO_DALA_BOSS_58h_Male_Goblin_BossGoblinBombs_01, VO_DALA_BOSS_58h_Male_Goblin_BossShuffleBomb_01, VO_DALA_BOSS_58h_Male_Goblin_Death_02, VO_DALA_BOSS_58h_Male_Goblin_DefeatPlayer_02, VO_DALA_BOSS_58h_Male_Goblin_EmoteResponse_01, VO_DALA_BOSS_58h_Male_Goblin_HeroPower_01, VO_DALA_BOSS_58h_Male_Goblin_HeroPower_02, VO_DALA_BOSS_58h_Male_Goblin_HeroPower_03, VO_DALA_BOSS_58h_Male_Goblin_HeroPower_04,
			VO_DALA_BOSS_58h_Male_Goblin_HeroPower_05, VO_DALA_BOSS_58h_Male_Goblin_HeroPower_06, VO_DALA_BOSS_58h_Male_Goblin_HeroPowerSilence_01, VO_DALA_BOSS_58h_Male_Goblin_Idle_01, VO_DALA_BOSS_58h_Male_Goblin_Idle_02, VO_DALA_BOSS_58h_Male_Goblin_Idle_03, VO_DALA_BOSS_58h_Male_Goblin_Idle_04, VO_DALA_BOSS_58h_Male_Goblin_Intro_01, VO_DALA_BOSS_58h_Male_Goblin_IntroEudora_01, VO_DALA_BOSS_58h_Male_Goblin_IntroRakanishu_01,
			VO_DALA_BOSS_58h_Male_Goblin_PlayerBoombots_01, VO_DALA_BOSS_58h_Male_Goblin_PlayerBoommasterFlark_01, VO_DALA_BOSS_58h_Male_Goblin_PlayerBoomzooka_01, VO_DALA_BOSS_58h_Male_Goblin_PlayerDrBoom_01, VO_DALA_BOSS_58h_Male_Goblin_PlayerFlyBy_01, VO_DALA_BOSS_58h_Male_Goblin_PlayerGoblinBombs_01, VO_DALA_BOSS_58h_Male_Goblin_TurnFour_01, VO_DALA_BOSS_58h_Male_Goblin_TurnOne_01, VO_DALA_BOSS_58h_Male_Goblin_TurnOne_02, VO_DALA_BOSS_58h_Male_Goblin_TurnSix_01,
			VO_DALA_BOSS_58h_Male_Goblin_TurnTwo_01, VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_01, VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_02, VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_03, VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_04, VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_05, VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_08, VO_DALA_BOSS_58h_Male_Goblin_WiresTrigger_09
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
		m_introLine = VO_DALA_BOSS_58h_Male_Goblin_Intro_01;
		m_deathLine = VO_DALA_BOSS_58h_Male_Goblin_Death_02;
		m_standardEmoteResponseLine = VO_DALA_BOSS_58h_Male_Goblin_EmoteResponse_01;
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { VO_DALA_BOSS_58h_Male_Goblin_HeroPower_01, VO_DALA_BOSS_58h_Male_Goblin_HeroPower_02, VO_DALA_BOSS_58h_Male_Goblin_HeroPower_03, VO_DALA_BOSS_58h_Male_Goblin_HeroPower_04, VO_DALA_BOSS_58h_Male_Goblin_HeroPower_05, VO_DALA_BOSS_58h_Male_Goblin_HeroPower_06 };
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
			if (cardId == "DALA_Eudora")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_58h_Male_Goblin_IntroEudora_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId == "DALA_RAkanishu")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_58h_Male_Goblin_IntroRakanishu_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_TurnOne);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_58h_Male_Goblin_TurnTwo_01);
			break;
		case 103:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_58h_Male_Goblin_TurnFour_01);
			break;
		case 104:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_58h_Male_Goblin_TurnSix_01);
			break;
		case 105:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_58h_Male_Goblin_HeroPowerSilence_01);
			break;
		case 106:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_58h_Male_Goblin_BossDrawBomb_01);
			break;
		case 107:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_WiresTrigger);
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
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			switch (cardId)
			{
			case "LOOTA_838":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_58h_Male_Goblin_PlayerBoombots_01);
				break;
			case "BOT_034":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_58h_Male_Goblin_PlayerBoommasterFlark_01);
				break;
			case "BOT_429":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_58h_Male_Goblin_PlayerBoomzooka_01);
				break;
			case "GVG_110":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_58h_Male_Goblin_PlayerDrBoom_01);
				break;
			case "DALA_716":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_58h_Male_Goblin_PlayerFlyBy_01);
				break;
			case "BOT_031":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_58h_Male_Goblin_PlayerGoblinBombs_01);
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "BOT_031":
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_58h_Male_Goblin_BossGoblinBombs_01);
				break;
			case "BOT_511":
			case "DAL_060":
			case "GVG_056t":
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_58h_Male_Goblin_BossShuffleBomb_01);
				break;
			}
		}
	}
}
