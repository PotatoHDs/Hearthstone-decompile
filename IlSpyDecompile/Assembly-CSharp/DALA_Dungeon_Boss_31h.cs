using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_31h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_BossBlessingOfKings_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_BossBlessingOfKings_01.prefab:362431c121a53e3418baab30f3337cf6");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_Death_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_Death_01.prefab:52478d9fe390c3f479a2d4f672cbbdc1");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_DefeatPlayer_01.prefab:c11cc13c4de085642adca08c1a70c0c6");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_EmoteResponse_01.prefab:3493b456d393ef047b54699fc3dd471c");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_HeroPower_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_HeroPower_01.prefab:576e4e28c3a21d146a75f571e30b9783");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_HeroPower_02 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_HeroPower_02.prefab:1c80b0707d1fc2540beabd40f7586a50");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_HeroPower_03 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_HeroPower_03.prefab:97e90f36e963f5244902c4d6cf645fd6");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_HeroPower_04 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_HeroPower_04.prefab:b10e21ec99878454bbb70eae121cb6e1");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_01.prefab:a3be152294e00854dbe3b94eae2d03bd");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_02 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_02.prefab:2fe52071140e7784c8c5c3259c2d21e8");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_03 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_03.prefab:f4ffaa80dc382604fbd8236a7a019659");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_04 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_04.prefab:fcabae9b8e6859848b4a7035649b893b");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_Idle_01.prefab:a54d80ffdb6341e458c257ecaa4234a0");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_Idle_02.prefab:94a0d67503c61894a89aff041e6438d6");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_Idle_03.prefab:16a643a55b1f54a4f9f724fa89e2f104");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_Intro_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_Intro_01.prefab:f831100f9e60ff441ba8b1ae4f7bd2d9");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_IntroPlayerGeorge_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_IntroPlayerGeorge_01.prefab:8caa17fa6d7ae9d419cb6e95e03759b8");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_PlayerAuctioneer_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_PlayerAuctioneer_01.prefab:8e617c1e0daf89240a304e136d18840d");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_PlayerBlingtron3000_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_PlayerBlingtron3000_01.prefab:22b252b7230fea145b379812d7318d09");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_PlayerSpellstone_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_PlayerSpellstone_01.prefab:20ec9797c2cb578438ec1f484ee8f19e");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_PlayerTreasure_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_PlayerTreasure_01.prefab:0b23d6106ea13ee4897b1156562c51db");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_SummonCrystalSmithKangor_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_SummonCrystalSmithKangor_01.prefab:cb89c9ec4aa20d943bca91196dd449e9");

	private static readonly AssetReference VO_DALA_BOSS_31h_Male_Human_TurnOne_01 = new AssetReference("VO_DALA_BOSS_31h_Male_Human_TurnOne_01.prefab:c3bf8f01cb7a67f499e210311655aa12");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_31h_Male_Human_Idle_01, VO_DALA_BOSS_31h_Male_Human_Idle_02, VO_DALA_BOSS_31h_Male_Human_Idle_03 };

	private static List<string> m_HeroPower = new List<string> { VO_DALA_BOSS_31h_Male_Human_HeroPower_01, VO_DALA_BOSS_31h_Male_Human_HeroPower_02, VO_DALA_BOSS_31h_Male_Human_HeroPower_03, VO_DALA_BOSS_31h_Male_Human_HeroPower_04 };

	private static List<string> m_HeroPowerGolden = new List<string> { VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_01, VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_02, VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_03, VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_04 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_31h_Male_Human_BossBlessingOfKings_01, VO_DALA_BOSS_31h_Male_Human_Death_01, VO_DALA_BOSS_31h_Male_Human_DefeatPlayer_01, VO_DALA_BOSS_31h_Male_Human_EmoteResponse_01, VO_DALA_BOSS_31h_Male_Human_HeroPower_01, VO_DALA_BOSS_31h_Male_Human_HeroPower_02, VO_DALA_BOSS_31h_Male_Human_HeroPower_03, VO_DALA_BOSS_31h_Male_Human_HeroPower_04, VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_01, VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_02,
			VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_03, VO_DALA_BOSS_31h_Male_Human_HeroPowerGolden_04, VO_DALA_BOSS_31h_Male_Human_Idle_01, VO_DALA_BOSS_31h_Male_Human_Idle_02, VO_DALA_BOSS_31h_Male_Human_Idle_03, VO_DALA_BOSS_31h_Male_Human_Intro_01, VO_DALA_BOSS_31h_Male_Human_IntroPlayerGeorge_01, VO_DALA_BOSS_31h_Male_Human_PlayerAuctioneer_01, VO_DALA_BOSS_31h_Male_Human_PlayerBlingtron3000_01, VO_DALA_BOSS_31h_Male_Human_PlayerSpellstone_01,
			VO_DALA_BOSS_31h_Male_Human_PlayerTreasure_01, VO_DALA_BOSS_31h_Male_Human_SummonCrystalSmithKangor_01, VO_DALA_BOSS_31h_Male_Human_TurnOne_01
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
		m_introLine = VO_DALA_BOSS_31h_Male_Human_Intro_01;
		m_deathLine = VO_DALA_BOSS_31h_Male_Human_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_31h_Male_Human_EmoteResponse_01;
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
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_31h_Male_Human_IntroPlayerGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId != "DALA_Eudora")
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
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerGolden);
			break;
		case 103:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_31h_Male_Human_PlayerTreasure_01);
			break;
		case 104:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_31h_Male_Human_TurnOne_01);
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
			case "EX1_095":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_31h_Male_Human_PlayerAuctioneer_01);
				break;
			case "GVG_119":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_31h_Male_Human_PlayerBlingtron3000_01);
				break;
			case "BOT_236":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_31h_Male_Human_SummonCrystalSmithKangor_01);
				break;
			case "LOOT_043":
			case "LOOT_043t2":
			case "LOOT_043t3":
			case "LOOT_051":
			case "LOOT_051t1":
			case "LOOT_051t2":
			case "LOOT_064":
			case "LOOT_064t1":
			case "LOOT_064t2":
			case "LOOT_080":
			case "LOOT_080t2":
			case "LOOT_080t3":
			case "LOOT_091":
			case "LOOT_091t1":
			case "LOOT_091t2":
			case "LOOT_103":
			case "LOOT_103t1":
			case "LOOT_103t2":
			case "LOOT_203":
			case "LOOT_203t2":
			case "LOOT_203t3":
			case "LOOT_503":
			case "LOOT_503t":
			case "LOOT_503t2":
			case "LOOT_507":
			case "LOOT_507t":
			case "LOOT_507t2":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_31h_Male_Human_PlayerSpellstone_01);
				break;
			}
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
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
			if (cardId == "BOT_236")
			{
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_31h_Male_Human_SummonCrystalSmithKangor_01);
			}
		}
	}
}
