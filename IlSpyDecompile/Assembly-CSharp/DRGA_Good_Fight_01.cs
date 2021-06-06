using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRGA_Good_Fight_01 : DRGA_Dungeon
{
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_02_01.prefab:c615497291aad6d4e9c78ad885519bdd");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_03_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_03_01.prefab:661d71cae3c252d4cb2333e23c8016eb");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_Turn2_Reno_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_Turn2_Reno_01.prefab:2752a53a5dba7bc4cb4274ed6674bc85");

	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_01_Turn3_01_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_01_Turn3_01_01.prefab:5933275eed2b8364ba93e68322fbb7d9");

	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_PlayerStart_01.prefab:a4da2de66b2a4ec4eaaccf9e699bdcd5");

	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_RenoCaptured_04_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_RenoCaptured_04_01.prefab:679fd7eb47e528b4d8e4c5fa423851db");

	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_Turn3_02_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_Turn3_02_01.prefab:7035ad03ffa34b2478c33b7ade3081d5");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_Death_01.prefab:949b7d9d2eedbc94685abfd8bb1ac887");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_01_01.prefab:6c03f8b373447e24a86aaac882f9ffcc");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_02_01.prefab:c79fb8b74a7371f428cb86590835ccd2");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_03_01.prefab:d20c69d50f0d36945909da3a456c24bf");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_04_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_04_01.prefab:0d6bb9e30d011484795a00216360088f");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossAttack_01.prefab:90e1edceee3731044b9ebb3c692da15c");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossStart_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossStart_01.prefab:41df9a4d0353265428a7778e293cae54");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossStartHeroic_01.prefab:66bc198ee1c355a41a8cda959d55af41");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_01_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_01_01.prefab:7a89c126984d2184e8b40b0e7aa2b62e");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_02_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_02_01.prefab:8e70ce6185c2ac648bc6e411c7d92e26");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_03_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_03_01.prefab:75ddc30cfe66cfe49a2713bbb17319cb");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_FUSE_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_FUSE_01.prefab:8af1072e5db259d43a8fb5d111ae5a4f");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_Lighterbot_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_Lighterbot_01.prefab:a21d84910671b0648a22318c8a6167c5");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_PilotedWhirlOTron_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_PilotedWhirlOTron_01.prefab:6383705f81805114995dbfc3de4c3aa8");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_Recyclebot_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_Recyclebot_01.prefab:f1a78d627e1963840836335c864deae5");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_EmoteResponse_01.prefab:83a99a93a71c1e8478348296c39cb81b");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_01_01.prefab:7389b40ea8f8ac14d870f73a64d53004");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_02_01.prefab:a434d952a1164d841954781a05ab3e44");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_03_01.prefab:690120117c943cd4f9062677f30385a8");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Bomb_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Bomb_01.prefab:378b56173f071864aba84ee64881b267");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoom_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoom_01.prefab:f8d2c419c67f7114a81a7ae25c25e969");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoomHero_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoomHero_01.prefab:9fcf1b8156baf7944b6faad9359da4cc");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoomsScheme_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoomsScheme_01.prefab:eb4bbd196f5ff5643bcc35f31a5072c2");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Invention_01_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Invention_01_01.prefab:03087bc3567ccc04793ae412293c2cfc");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Invention_02_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Invention_02_01.prefab:58644d40b7142e146a4382ace99a7d84");

	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_RenoCaptured_01_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_RenoCaptured_01_01.prefab:40008b7fc1a38b8448aa4f562b342a57");

	private List<string> m_VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPowerLines = new List<string> { VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_01_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_02_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_03_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_04_01 };

	private List<string> m_VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrageLines = new List<string> { VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_01_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_02_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_03_01 };

	private List<string> m_VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_IdleLines = new List<string> { VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_01_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_02_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_03_01 };

	private List<string> m_VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_InventionLines = new List<string> { VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Invention_01_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Invention_02_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_02_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_03_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_Turn2_Reno_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_01_Turn3_01_01, VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_PlayerStart_01, VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_RenoCaptured_04_01, VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_Turn3_02_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_Death_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_01_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_02_01,
			VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_03_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPower_04_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossAttack_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossStart_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossStartHeroic_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_01_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_02_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrage_03_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_FUSE_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_Lighterbot_01,
			VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_PilotedWhirlOTron_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_Recyclebot_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_EmoteResponse_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_01_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_02_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Idle_03_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Bomb_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoom_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoomHero_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoomsScheme_01,
			VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Invention_01_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Invention_02_01, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_RenoCaptured_01_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Boss_Death_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_RenoCaptured_04_01);
				yield return PlayLineAlways(DRGA_Dungeon.EliseBrassRing, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_01_Turn3_01_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_01_Turn3_02_01);
			}
			break;
		case 102:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_BossAttack_01);
			break;
		case 104:
			if (!m_Heroic)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("DRGA_001"), DRGA_Dungeon.RenoBrassRing, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_02_01);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 105:
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_Bomb_01);
			break;
		case 106:
			if (!m_Heroic)
			{
				yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("DRGA_001"), DRGA_Dungeon.RenoBrassRing, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_Turn2_Reno_01);
			}
			break;
		case 107:
			if (!m_Heroic)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("DRGA_001"), DRGA_Dungeon.RenoBrassRing, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_03_01);
				yield return new WaitForSeconds(3.25f);
				GameState.Get().SetBusy(busy: false);
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_RenoCaptured_01_01);
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
		switch (cardId)
		{
		case "GVG_110":
			if (m_Heroic)
			{
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoom_01);
			}
			break;
		case "BOT_238":
			if (m_Heroic)
			{
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoomHero_01);
			}
			break;
		case "DAL_008":
			if (m_Heroic)
			{
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_DrBoomsScheme_01);
			}
			break;
		case "DRGA_BOSS_05t":
		case "DRGA_BOSS_05t3":
		case "DRGA_BOSS_05t4":
		case "DRGA_BOSS_05t5":
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_Player_InventionLines);
			break;
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
			case "DRGA_BOSS_05t2":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_BoomBarrageLines);
				break;
			case "DRGA_BOSS_05t4":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_FUSE_01);
				break;
			case "DRGA_BOSS_05t3":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_Lighterbot_01);
				break;
			case "DRGA_BOSS_05t":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_PilotedWhirlOTron_01);
				break;
			case "DRGA_BOSS_05t5":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_DrBoom_Recyclebot_01);
				break;
			}
		}
	}
}
