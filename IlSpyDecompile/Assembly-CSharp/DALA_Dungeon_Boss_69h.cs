using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_69h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_BossAzureDrake_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_BossAzureDrake_01.prefab:6cc1a6ff063a97840b236aaffdb48aff");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_BossSilence_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_BossSilence_01.prefab:9a7e18b0ac4e4934ea4db8a25a4632d2");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_BossSpellDamage_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_BossSpellDamage_01.prefab:d02e380a7e4ed93469ce733e2b4b8fde");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_Death_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_Death_01.prefab:954bb36183fba9348ae6218a48553ef8");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_DefeatPlayer_01.prefab:75d0908d4811de247b05a9a2bb3abae2");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_EmoteResponse_01.prefab:1266b52465a8ee646857c58d6b460cf7");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_Exposition_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_Exposition_01.prefab:8cfa53fd391a5224c9c6476adeb16df4");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_HeroPower_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_HeroPower_01.prefab:852f081f1a21a904dabc5f38bcac0b8f");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_HeroPower_02 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_HeroPower_02.prefab:c2573b16afd7a2644af0af154ce9ea27");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_HeroPower_03 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_HeroPower_03.prefab:ff87304ea203cf04ca9bdfb088932ea3");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_HeroPower_04 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_HeroPower_04.prefab:7aa587f1515026e41a3a1d1cb214a4c3");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_HeroPower_05 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_HeroPower_05.prefab:b0d284f9b15bfdb4cba7e22af7533739");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_HeroPower_06 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_HeroPower_06.prefab:d20d9293e7d205a4ab0e1fc7f5faad8e");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_Idle_01.prefab:729e04d54777ecd46b90828238bbdfef");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_Idle_02.prefab:62795ac5a4e1a6643953bf30a4c4ae56");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_Idle_03.prefab:e9b5267173d22d748a85763bbef413c5");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_Intro_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_Intro_01.prefab:237cac51b8815d34cb61af467e4862da");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_PlayerAlextrasza_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_PlayerAlextrasza_01.prefab:752a3f05d71d94f4abb7e2f26bec8c8d");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_PlayerArcaneSpell_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_PlayerArcaneSpell_01.prefab:65e9f802ca926de428d7f396cae61195");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_PlayerArcaneSpell_02 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_PlayerArcaneSpell_02.prefab:394d70654c373774fa5f2066f4d997d4");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_PlayerDeathwing_02 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_PlayerDeathwing_02.prefab:08858ca81fb84364ba22a8593876a895");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_PlayerDKJaina_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_PlayerDKJaina_01.prefab:191b63945f0a38d49bc3c5acab79f3e4");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_PlayerMageSpell_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_PlayerMageSpell_01.prefab:f73d5bdca35c64b47be7d95f51916241");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_PlayerMalygos_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_PlayerMalygos_01.prefab:b025960290d6efc419735c83e3a16199");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_PlayerNozdormu_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_PlayerNozdormu_01.prefab:5bc309a5664e5464b9bdc02a836e60a5");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_PlayerYsera_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_PlayerYsera_01.prefab:0aae3064d51890d45bc9592a6eee72a5");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_TurnOne_02 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_TurnOne_02.prefab:731ddb75bfb5c924096fc46ccfc4d538");

	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_TurnTwo_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_TurnTwo_01.prefab:f9e00d6b3bb3f2941aa78f55d59c351b");

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_69h_Male_Human_Idle_01, VO_DALA_BOSS_69h_Male_Human_Idle_02, VO_DALA_BOSS_69h_Male_Human_Idle_03 };

	private static List<string> m_PlayerArcaneSpell = new List<string> { VO_DALA_BOSS_69h_Male_Human_PlayerArcaneSpell_01, VO_DALA_BOSS_69h_Male_Human_PlayerArcaneSpell_02 };

	private static List<string> m_BossHeroPower = new List<string> { VO_DALA_BOSS_69h_Male_Human_HeroPower_01, VO_DALA_BOSS_69h_Male_Human_HeroPower_02, VO_DALA_BOSS_69h_Male_Human_HeroPower_03, VO_DALA_BOSS_69h_Male_Human_HeroPower_04, VO_DALA_BOSS_69h_Male_Human_HeroPower_05, VO_DALA_BOSS_69h_Male_Human_HeroPower_06 };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_69h_Male_Human_BossAzureDrake_01, VO_DALA_BOSS_69h_Male_Human_BossSilence_01, VO_DALA_BOSS_69h_Male_Human_BossSpellDamage_01, VO_DALA_BOSS_69h_Male_Human_Death_01, VO_DALA_BOSS_69h_Male_Human_DefeatPlayer_01, VO_DALA_BOSS_69h_Male_Human_EmoteResponse_01, VO_DALA_BOSS_69h_Male_Human_Exposition_01, VO_DALA_BOSS_69h_Male_Human_HeroPower_01, VO_DALA_BOSS_69h_Male_Human_HeroPower_02, VO_DALA_BOSS_69h_Male_Human_HeroPower_03,
			VO_DALA_BOSS_69h_Male_Human_HeroPower_04, VO_DALA_BOSS_69h_Male_Human_HeroPower_05, VO_DALA_BOSS_69h_Male_Human_HeroPower_06, VO_DALA_BOSS_69h_Male_Human_Idle_01, VO_DALA_BOSS_69h_Male_Human_Idle_02, VO_DALA_BOSS_69h_Male_Human_Idle_03, VO_DALA_BOSS_69h_Male_Human_Intro_01, VO_DALA_BOSS_69h_Male_Human_PlayerAlextrasza_01, VO_DALA_BOSS_69h_Male_Human_PlayerArcaneSpell_01, VO_DALA_BOSS_69h_Male_Human_PlayerArcaneSpell_02,
			VO_DALA_BOSS_69h_Male_Human_PlayerDeathwing_02, VO_DALA_BOSS_69h_Male_Human_PlayerDKJaina_01, VO_DALA_BOSS_69h_Male_Human_PlayerMageSpell_01, VO_DALA_BOSS_69h_Male_Human_PlayerMalygos_01, VO_DALA_BOSS_69h_Male_Human_PlayerNozdormu_01, VO_DALA_BOSS_69h_Male_Human_PlayerYsera_01, VO_DALA_BOSS_69h_Male_Human_TurnOne_02, VO_DALA_BOSS_69h_Male_Human_TurnTwo_01
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
		m_introLine = VO_DALA_BOSS_69h_Male_Human_Intro_01;
		m_deathLine = VO_DALA_BOSS_69h_Male_Human_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_69h_Male_Human_EmoteResponse_01;
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
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_69h_Male_Human_BossSilence_01);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_69h_Male_Human_BossSpellDamage_01);
			break;
		case 103:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_69h_Male_Human_TurnOne_02);
			break;
		case 104:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_69h_Male_Human_Exposition_01);
			break;
		case 105:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_69h_Male_Human_PlayerMageSpell_01);
			break;
		case 106:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossHeroPower);
			break;
		case 107:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_69h_Male_Human_TurnTwo_01);
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
			case "EX1_561":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_69h_Male_Human_PlayerAlextrasza_01);
				break;
			case "NEW1_030":
			case "OG_317":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_69h_Male_Human_PlayerDeathwing_02);
				break;
			case "ICC_833":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_69h_Male_Human_PlayerDKJaina_01);
				break;
			case "EX1_563":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_69h_Male_Human_PlayerMalygos_01);
				break;
			case "EX1_560":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_69h_Male_Human_PlayerNozdormu_01);
				break;
			case "EX1_572":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_69h_Male_Human_PlayerYsera_01);
				break;
			case "AT_004":
			case "CFM_623":
			case "CS2_023":
			case "CS2_025":
			case "DS1_185":
			case "EX1_277":
				yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_PlayerArcaneSpell);
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
			if (cardId == "EX1_284")
			{
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_69h_Male_Human_BossAzureDrake_01);
			}
		}
	}
}
