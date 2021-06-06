using System.Collections;
using System.Collections.Generic;

public class DRGA_Evil_Fight_02 : DRGA_Dungeon
{
	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_PlayerStart_01.prefab:c291a4def8311994e9be1b03396f92cb");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_01_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_01_01.prefab:c8d372d5455a3bd48850c64518c30c09");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_02_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_02_01.prefab:4f2c978adbfece8479247c2306a361fa");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_03_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_03_01.prefab:e5b1dcbbdcd6b414c9235c49a606c78d");

	private static readonly AssetReference VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_04_01 = new AssetReference("VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_04_01.prefab:7c3e7aaccb8cd204e8537bf11ae68f15");

	private static readonly AssetReference VO_DRGA_BOSS_22h_Male_Gronn_Evil_Fight_02_Skruk_Awakened_01 = new AssetReference("VO_DRGA_BOSS_22h_Male_Gronn_Evil_Fight_02_Skruk_Awakened_01.prefab:92af804b9e3c7ea48888b7ce2025dd9e");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_DeathHeroic_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_DeathHeroic_01.prefab:a73b40e951f2ba243bbaa5b5c8816363");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_FreezeSpell_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_FreezeSpell_01.prefab:7ac56b867eb84064989e416a5f62c068");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_01_01.prefab:8271ab3a381e4b740bb2631bffb7ebc0");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_02_01.prefab:084a3324541241749b6d095237845635");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_03_01.prefab:d452ccf89651e4c4793224d4abf163f4");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_Moorabi_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_Moorabi_01.prefab:d2c3ffcb44544c94ba5d858ea4ddb8e7");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_SnowfuryGiant_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_SnowfuryGiant_01.prefab:a5ce63dda652e1e42898e380f1de3850");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossAttack_01.prefab:52cc26fe46c42594cac95c18c5c7012b");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossStart_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossStart_01.prefab:97a7947075b77f5458bfd5d7cbe6b644");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossStartHeroic_01.prefab:4204dcf8e3898964292fe047284a0f1f");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_EmoteResponse_01.prefab:df04e2a3ab57a714d87294fa9bf54cf6");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_01_01.prefab:c8690c4abb1e8b6408365bf6981b1c67");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_02_01.prefab:e0cfb9a1a25e84d47875a1fda0f5210a");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_03_01.prefab:742add37f038de741a1cf363737c3f0c");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Player_FreezeMinion_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Player_FreezeMinion_01.prefab:3976109773f78414db2dfcaec5de4df6");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Player_Misc01_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Player_Misc01_01.prefab:71979a371cc97964fbc4ab7dfd5023fc");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_PlayerHeroic_FrostLichJaina_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_PlayerHeroic_FrostLichJaina_01.prefab:6ce27385ff31b6b4d98a0633527295d1");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_PlayerHeroic_PyroblastFace_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_PlayerHeroic_PyroblastFace_01.prefab:b4abc210c97a8ac4daf409febce546c6");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Puppeteering_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Puppeteering_01.prefab:ec3c5b67bae912d42a294918a964e8ee");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_05_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_05_01.prefab:28a05e97f0e563646b8724eeecf540fa");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_06_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_06_01.prefab:9106889a7877a9c40901a2c3b3083d14");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_07_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_07_01.prefab:ffbfe2c550cfb2941aa8640d9483bc2f");

	private static readonly AssetReference VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_08_01 = new AssetReference("VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_08_01.prefab:092644dd1cf9cb84aa78d8616bdb7146");

	private List<string> m_VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPowerLines = new List<string> { VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_01_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_02_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_03_01 };

	private List<string> m_VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_IdleLines = new List<string> { VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_01_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_02_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_03_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_06_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_07_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_08_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_PlayerStart_01, VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_01_01, VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_02_01, VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_03_01, VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_04_01, VO_DRGA_BOSS_22h_Male_Gronn_Evil_Fight_02_Skruk_Awakened_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_DeathHeroic_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_FreezeSpell_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_01_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_02_01,
			VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPower_03_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_Moorabi_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_SnowfuryGiant_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossAttack_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossStart_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossStartHeroic_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_EmoteResponse_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_01_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_02_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Idle_03_01,
			VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Player_FreezeMinion_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Player_Misc01_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_PlayerHeroic_FrostLichJaina_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_PlayerHeroic_PyroblastFace_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Puppeteering_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_05_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_06_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_07_01, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_08_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_HeroPowerLines;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		if (!m_Heroic)
		{
			m_deathLine = VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_DeathHeroic_01;
		}
		if (m_Heroic)
		{
			m_deathLine = VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_DeathHeroic_01;
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 100:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_FreezeSpell_01);
			break;
		case 101:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Player_FreezeMinion_01);
			break;
		case 102:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Player_Misc01_01);
			}
			break;
		case 104:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_PlayerHeroic_PyroblastFace_01);
			break;
		case 105:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor, VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_01_01);
			}
			break;
		case 106:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor, VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_02_01);
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Story_05_01);
			}
			break;
		case 107:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor, VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_03_01);
			}
			break;
		case 113:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_BossAttack_01);
			break;
		case 114:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlwaysWithBrassRing(GetEnemyActorByCardId("DRGA_BOSS_25t"), null, VO_DRGA_BOSS_22h_Male_Gronn_Evil_Fight_02_Skruk_Awakened_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 115:
			if (!m_Heroic)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineAlways(actor, VO_DRGA_BOSS_09h_Female_Troll_Evil_Fight_02_Story_04_01);
				GameState.Get().SetBusy(busy: false);
				PlaySound(VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_DeathHeroic_01);
			}
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
		if (!(cardId == "DRGA_BOSS_09t"))
		{
			if (cardId == "ICC_833" && m_Heroic)
			{
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_PlayerHeroic_FrostLichJaina_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Puppeteering_01);
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
		if (!(cardId == "ICC_289"))
		{
			if (cardId == "ICC_090")
			{
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_SnowfuryGiant_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_25h_Female_Elemental_Evil_Fight_02_Boss_Moorabi_01);
		}
	}
}
