using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_61h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_01.prefab:e2b00cc4b7117444b8fb6fad50b81049");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_02 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_02.prefab:6db75ecf1068fc341ac0d2af8631b967");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_03 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_03.prefab:87a836e9e0e627044b9f7aba3f7f98fd");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_04 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_04.prefab:d25c6095327d36946bc09b2ceb989ae5");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_05 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_05.prefab:86b99f60dbf2b9044986a8d3e9349c3a");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpiderKill_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpiderKill_01.prefab:a0f0aba5f77a0514d9b3cde30888417c");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_BossSpreadingPlague_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_BossSpreadingPlague_01.prefab:12d3d8b3273863541bef7d00fe1ca97e");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_Death_02 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_Death_02.prefab:f74bcb8bfcd2b374faf232796fcfe708");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_DefeatPlayer_01.prefab:bac74142c865d294b905cedb4305199f");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_EmoteResponse_01.prefab:ce177fd3cdad44649a95033a505d3756");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_01.prefab:ef42a5249d098704a9c5677319866470");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_02 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_02.prefab:7be30312fdcfc8347ab269bbeac2ae11");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_03 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_03.prefab:a2a287b8a8992e3458598f3af2f77062");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_04 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_04.prefab:79747c90f7775394f8d51c080143696e");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_05 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_05.prefab:0ce87fc719e7a4d45b9533bfbbce090d");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerNothing_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerNothing_01.prefab:e2b220f2df88a25438a60f5ba74e02e2");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerNothing_02 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerNothing_02.prefab:d338bc84f9e7895439edde1b3ae4afee");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerPhaseSpider_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerPhaseSpider_01.prefab:1ba74c41b50fa994ea9d2541b3cf96f1");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_Idle_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_Idle_01.prefab:1d9d125fa4c28d147b73815fa14fece5");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_Idle_02 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_Idle_02.prefab:d2dd489c53f062c40b6dc1de5f316ee0");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_Idle_03 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_Idle_03.prefab:e39d4d53cf0fefc4eacdaaab670bb96d");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_Intro_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_Intro_01.prefab:342e742f2b674314f89b85cf43ac700e");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_PlayerBallOfSpiders_02 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_PlayerBallOfSpiders_02.prefab:13b7b135706cb2543a783ccb684e585c");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_PlayerPsychicScream_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_PlayerPsychicScream_01.prefab:64bfe9d1b5e9241498d27222a396be69");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_PlayerSprint_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_PlayerSprint_01.prefab:d41ff9a919cf3dd41a1a776f38c7c248");

	private static readonly AssetReference VO_DALA_BOSS_61h_Female_Aranasi_PlayerWebspinner_01 = new AssetReference("VO_DALA_BOSS_61h_Female_Aranasi_PlayerWebspinner_01.prefab:1adb258e203c9a84987fe9fdeb460644");

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_61h_Female_Aranasi_Idle_01, VO_DALA_BOSS_61h_Female_Aranasi_Idle_02, VO_DALA_BOSS_61h_Female_Aranasi_Idle_03 };

	private static List<string> m_BossPhaseSpider = new List<string> { VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_01, VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_02, VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_03, VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_04, VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_05 };

	private static List<string> m_BossHeroPower = new List<string> { VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_01, VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_02, VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_03, VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_04, VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_05 };

	private static List<string> m_BossHeroPowerNother = new List<string> { VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerNothing_01, VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerNothing_02 };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_01, VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_02, VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_03, VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_04, VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpider_05, VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpiderKill_01, VO_DALA_BOSS_61h_Female_Aranasi_BossSpreadingPlague_01, VO_DALA_BOSS_61h_Female_Aranasi_Death_02, VO_DALA_BOSS_61h_Female_Aranasi_DefeatPlayer_01, VO_DALA_BOSS_61h_Female_Aranasi_EmoteResponse_01,
			VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_01, VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_02, VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_03, VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_04, VO_DALA_BOSS_61h_Female_Aranasi_HeroPower_05, VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerNothing_01, VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerNothing_02, VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerPhaseSpider_01, VO_DALA_BOSS_61h_Female_Aranasi_Idle_01, VO_DALA_BOSS_61h_Female_Aranasi_Idle_02,
			VO_DALA_BOSS_61h_Female_Aranasi_Idle_03, VO_DALA_BOSS_61h_Female_Aranasi_Intro_01, VO_DALA_BOSS_61h_Female_Aranasi_PlayerBallOfSpiders_02, VO_DALA_BOSS_61h_Female_Aranasi_PlayerPsychicScream_01, VO_DALA_BOSS_61h_Female_Aranasi_PlayerSprint_01, VO_DALA_BOSS_61h_Female_Aranasi_PlayerWebspinner_01
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
		m_introLine = VO_DALA_BOSS_61h_Female_Aranasi_Intro_01;
		m_deathLine = VO_DALA_BOSS_61h_Female_Aranasi_Death_02;
		m_standardEmoteResponseLine = VO_DALA_BOSS_61h_Female_Aranasi_EmoteResponse_01;
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
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
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossHeroPower);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossHeroPowerNother);
			break;
		case 103:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_61h_Female_Aranasi_HeroPowerPhaseSpider_01);
			break;
		case 104:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_61h_Female_Aranasi_BossPhaseSpiderKill_01);
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
			case "AT_062":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_61h_Female_Aranasi_PlayerBallOfSpiders_02);
				break;
			case "LOOT_008":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_61h_Female_Aranasi_PlayerPsychicScream_01);
				break;
			case "CS2_077":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_61h_Female_Aranasi_PlayerSprint_01);
				break;
			case "FP1_011":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_61h_Female_Aranasi_PlayerWebspinner_01);
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
		if (!(cardId == "DALA_BOSS_61t"))
		{
			if (cardId == "ICC_054")
			{
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_61h_Female_Aranasi_BossSpreadingPlague_01);
			}
		}
		else
		{
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossPhaseSpider);
		}
	}
}
