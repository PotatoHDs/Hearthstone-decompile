using System.Collections;
using System.Collections.Generic;

public class DRGA_Evil_Fight_04 : DRGA_Dungeon
{
	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_Death_01.prefab:fb59050624bb4f742af20725bb5dac8d");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_Death_ALT_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_Death_ALT_01.prefab:ff9296d899b70494184c097abdf7fc0b");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_01_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_01_01.prefab:6b91b6060fd1cec4daa78557b3cc98e8");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_02_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_02_01.prefab:f92d24bfb2dde784cad973743444a8db");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_03_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_03_01.prefab:2338504811083cf4b940a6d1294eb318");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_01_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_01_01.prefab:9d8b703966c80d64cb401e71f276023d");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_02_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_02_01.prefab:11538486800e08d40a55dfd99a07c8fc");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_03_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_03_01.prefab:85b63efb2a5351b47aca246dc7d8758b");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossAttack_01.prefab:5d6ca739d5747e346857be286eea46e7");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossStart_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossStart_01.prefab:2f92b2564c37e6f4f94b7ed6d4f4a6ae");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossStartHeroic_01.prefab:78448df9d383f1847957f14931ce7d0b");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_EmoteResponse_01.prefab:20f6f5ab55241504bb078b133c26ba81");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_EmoteResponseHeroic_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_EmoteResponseHeroic_01.prefab:4c0459040ff59c74c87221af8f45754b");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_01_01.prefab:b12f3c72ae450fd49bf2e0ab2264ac49");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_02_01.prefab:694ddc3e96ccec14eb4f8cf86704362a");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_03_01.prefab:1f65c83f9b6ed4d4391720e59038e51c");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_01_01.prefab:edfe8ae778bebe94faafaa55e7a7e908");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_02_01.prefab:e0cbe4545aa8c4c46b864e7e42481d7f");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_03_01.prefab:474ffaae173517d478c2ae2365c92e9c");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_04_01.prefab:6066b6c1f9654d54cad48eaea607e252");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_05_01.prefab:441ecb1e7b084a542aacb7ba0836105c");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_01_01.prefab:74e206bc3bec69747b13f8e968d0021a");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_02_01.prefab:3962774ee11d62a4b822973849ebef77");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_03_01.prefab:e0745cb1fb7a4664588bcd85e2b38629");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_04_01.prefab:2c6a55e0d68f9f644abea2b11541f4ff");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_05_01.prefab:25b255118f60f2a4796f76e1c31a10d1");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Player_DragonAspect_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Player_DragonAspect_01.prefab:4d875c0bf7cf2f94ebf2602176c042f9");

	private static readonly AssetReference VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Player_Waxadred_01 = new AssetReference("VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Player_Waxadred_01.prefab:5fff2f88775e8e6489959428dfc16f31");

	private List<string> m_missionEventTrigger101Lines = new List<string> { VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_01_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_02_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_03_01 };

	private List<string> m_missionEventTrigger102Lines = new List<string> { VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_01_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_02_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_03_01 };

	private List<string> m_VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_IdleLines = new List<string> { VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_01_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_02_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_03_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_01_01, VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_02_01, VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_03_01, VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_04_01, VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_05_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_Death_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_Death_ALT_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_01_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_02_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower1Trigger_03_01,
			VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_01_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_02_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_HeroPower2Trigger_03_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossAttack_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossStart_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossStartHeroic_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_EmoteResponse_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_EmoteResponseHeroic_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_01_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_02_01,
			VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Idle_03_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_01_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_02_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_03_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_04_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_05_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Player_DragonAspect_01, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Player_Waxadred_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_IdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Boss_Death_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			if (!m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_EmoteResponseHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_missionEventTrigger101Lines);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_missionEventTrigger102Lines);
			break;
		case 104:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_01_01);
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_01_01);
			}
			break;
		case 105:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_02_01);
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_02_01);
			}
			break;
		case 106:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_03_01);
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_03_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_04_01);
			}
			break;
		case 108:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_08h_Male_Kobold_Evil_Fight_04_Misc_05_01);
			}
			break;
		case 109:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_BossAttack_01);
			break;
		case 110:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_04_01);
			break;
		case 111:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Misc_05_01);
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
			case "DRG_026":
			case "DRG_270":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Player_DragonAspect_01);
				break;
			case "DRG_036":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_26h_Female_Draenei_Evil_Fight_04_Player_Waxadred_01);
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
		}
	}
}
