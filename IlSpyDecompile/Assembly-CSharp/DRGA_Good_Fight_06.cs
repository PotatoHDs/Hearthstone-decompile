using System.Collections;
using System.Collections.Generic;

public class DRGA_Good_Fight_06 : DRGA_Dungeon
{
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_06_Backstory_01b_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_06_Backstory_01b_01.prefab:07fb5704f92f2a141a4cebb96b2c6630");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Death_01.prefab:dd7acb97dad260f42b2ee530cebe6d04");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_01_01.prefab:cfc2c34ed3f9d114b919eda95929f512");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_02_01.prefab:880f4643a2abaa24eb7c569120cd020c");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_03_01.prefab:25c6ebcc533e74d40b8f5302578e1226");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_04_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_04_01.prefab:8c4ace3d96b3b4941b2c823b807f65b6");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_05_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_05_01.prefab:0218774043411a044a7187fde0fb1e73");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_06_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_06_01.prefab:2bc8ec5740ed36d4992b9d15b5203f59");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WailOfTheShudderwock_01_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WailOfTheShudderwock_01_01.prefab:d20f43aa81ae75340ae76958f278baf8");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WailOfTheShudderwock_02_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WailOfTheShudderwock_02_01.prefab:40b50b50b6ab9754cb9faaf5d23b1e82");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Wormhole_01_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Wormhole_01_01.prefab:5d7c7e9743d61ec47b0e1be1751d56c9");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Wormhole_02_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Wormhole_02_01.prefab:348ee603c78c3524a94edd3084cc9743");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_BossAttack_01.prefab:b4813b2bd518b7945990fa5e386fcb08");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_BossStart_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_BossStart_01.prefab:94937513f9fd954469ee1cf9d60df53e");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_EmoteResponse_01.prefab:02592c2e441807e4a82b828d1c733ce5");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_EmoteResponseHeroic_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_EmoteResponseHeroic_01.prefab:5d1c0611bd74d444492b8998fbfea7d2");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_01_01.prefab:e8da6e64caaae854bbaeab1478623f90");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_02_01.prefab:bbde722c4668ff741813ce379840a0a1");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_03_01.prefab:75c59d7faf8900e4fb33a89f048e2630");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Alexstrasza_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Alexstrasza_01.prefab:932c5936f1e05d440867ac615d58871c");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Hagatha_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Hagatha_01.prefab:6e2943d76bdfcb149af604ab42ee2d3e");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_HagathasScheme_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_HagathasScheme_01.prefab:64b4eca11dfdbc34cb856bb5154f71ca");

	private static readonly AssetReference VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Shudderwock_01 = new AssetReference("VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Shudderwock_01.prefab:56708b2c88649a1498566e31a1ba03b9");

	private static readonly AssetReference VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Backstory_01a_01 = new AssetReference("VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Backstory_01a_01.prefab:f585e621ed631764481e767eed92a482");

	private static readonly AssetReference VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_01_01.prefab:ddb97a63484c90e43b9559f8bc844885");

	private static readonly AssetReference VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_02_01.prefab:dfc12eafb2e1bf242934273816060327");

	private static readonly AssetReference VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_03_01.prefab:9c189b0f8e5216e42b5d3827bc29d6ea");

	private List<string> m_VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPowerLines = new List<string> { VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_01_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_02_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_03_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_04_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_05_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_06_01 };

	private List<string> m_VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WailOfTheShudderwockLines = new List<string> { VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WailOfTheShudderwock_01_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WailOfTheShudderwock_02_01 };

	private List<string> m_VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WormholeLines = new List<string> { VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Wormhole_01_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Wormhole_02_01 };

	private List<string> m_VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_IdleLines = new List<string> { VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_01_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_02_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_03_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_06_Backstory_01b_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Death_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_01_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_02_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_03_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_04_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_05_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPower_06_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WailOfTheShudderwock_01_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WailOfTheShudderwock_02_01,
			VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Wormhole_01_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Wormhole_02_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_BossAttack_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_BossStart_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_EmoteResponse_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_EmoteResponseHeroic_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_01_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_02_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Idle_03_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Alexstrasza_01,
			VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Hagatha_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_HagathasScheme_01, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Shudderwock_01, VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Backstory_01a_01, VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_01_01, VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_02_01, VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_03_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_Death_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			if (!m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_EmoteResponseHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
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
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 100:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor2, VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Backstory_01a_01);
				yield return PlayLineAlways(DRGA_Dungeon.EliseBrassRing, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_06_Backstory_01b_01);
			}
			break;
		case 104:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor2, VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_01_01);
			}
			break;
		case 105:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor2, VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_02_01);
			}
			break;
		case 106:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor2, VO_DRGA_BOSS_17h_Female_Dragon_Good_Fight_06_Misc_03_01);
			}
			break;
		case 107:
			yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_BossAttack_01);
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
			switch (cardId)
			{
			case "EX1_561":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Alexstrasza_01);
				break;
			case "GIL_504":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Hagatha_01);
				break;
			case "DAL_009":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_HagathasScheme_01);
				break;
			case "GIL_820":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Player_Shudderwock_01);
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
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "DRGA_BOSS_06t"))
		{
			if (cardId == "DRGA_BOSS_06t2")
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WormholeLines);
			}
		}
		else
		{
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_DRGA_BOSS_06h_Female_Orc_Good_Fight_06_Boss_WailOfTheShudderwockLines);
		}
	}
}
