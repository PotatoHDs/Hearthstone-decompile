using System.Collections;
using System.Collections.Generic;

public class BOM_03_Guff_Fight_08 : BOM_03_Guff_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission8ExchangeF_03 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission8ExchangeF_03.prefab:d23ad907c0ff8344ca4f7b2c7967b517");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeE_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeE_02.prefab:cc6be32822b88f6458de143c32692733");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeH_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeH_02.prefab:9d2629aab0e6775489e961c88be764ca");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeJ_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeJ_01.prefab:c86b86508ba0b4040b493f27d4526401");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8Intro_01.prefab:5c644ecc59b529243a072fb708b823c9");

	private static readonly AssetReference VO_Story_Hero_Naralex_Male_NightElf_Story_Guff_Mission8ExchangeJ_01 = new AssetReference("VO_Story_Hero_Naralex_Male_NightElf_Story_Guff_Mission8ExchangeJ_01.prefab:4270937e624144343ae9747ac8e89cc8");

	private static readonly AssetReference VO_Story_Hero_Naralex_Male_NightElf_Story_Guff_Mission8ExchangeJ_02 = new AssetReference("VO_Story_Hero_Naralex_Male_NightElf_Story_Guff_Mission8ExchangeJ_02.prefab:039594ba909aed34fa2d6dd2754a3f54");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission8ExchangeG_03 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission8ExchangeG_03.prefab:843df793716cc214792ef126af736036");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission8ExchangeG_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission8ExchangeG_02.prefab:db2c3cd16f8453f469a3fa80c3a80a8c");

	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Death_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Death_01.prefab:ffc010f610157184d8d8f9e3e836dd47");

	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8EmoteResponse_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8EmoteResponse_01.prefab:3b84b68342feca34391861e115b20181");

	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeA_01.prefab:2acc05c129f5a3a45a9213721e1fb0c3");

	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeB_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeB_01.prefab:f7d84d4df1af6624889fad807e1bfada");

	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeC_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeC_01.prefab:ec121a0a63a21df47b1b695a452363fb");

	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeD_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeD_01.prefab:078753cb865fe1344b7149fb2cf7c74b");

	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeE_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeE_01.prefab:2320f3d381841374aa96b509894c6e51");

	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeF_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeF_01.prefab:38fc8de18f761b24488b5e252a9dea74");

	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeF_02 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeF_02.prefab:be1a7bc9d2eb9d1448dcdb918c4ef396");

	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeG_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeG_01.prefab:097385fff42b7904896e566086cd9762");

	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeH_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeH_01.prefab:e642e5142c1e8aa4c83cdefff4d50a82");

	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8HeroPower_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8HeroPower_01.prefab:6fc7331fb5c19774a9b32d99b5de0a48");

	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8HeroPower_02 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8HeroPower_02.prefab:9707c60ef7f33734aace07ac0f166090");

	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_01.prefab:4e0133870d1d7f046b5f9f26e60cd98b");

	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_02 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_02.prefab:5adab9a7a9e8b5241a49efba7582b8f8");

	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_03 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_03.prefab:0b9f3f26bb712f2428541d652458e7f8");

	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Intro_02 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Intro_02.prefab:2bd4d404600a9544184cdc4267aeb07f");

	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Loss_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Loss_01.prefab:4728b15dafb05fa40bd73273fa647d00");

	private List<string> m_InGame_BossIdle = new List<string> { VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_01, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_02, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_03 };

	private List<string> m_InGame_BossUsesHeroPower = new List<string> { VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8HeroPower_01, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8HeroPower_02 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission8ExchangeF_03, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeE_02, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeH_02, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeJ_01, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8Intro_01, VO_Story_Hero_Naralex_Male_NightElf_Story_Guff_Mission8ExchangeJ_01, VO_Story_Hero_Naralex_Male_NightElf_Story_Guff_Mission8ExchangeJ_02, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission8ExchangeG_03, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission8ExchangeG_02, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Death_01,
			VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8EmoteResponse_01, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeA_01, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeB_01, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeC_01, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeD_01, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeE_01, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeF_01, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeF_02, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeG_01, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeH_01,
			VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8HeroPower_01, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8HeroPower_02, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_01, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_02, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_03, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Intro_02, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Loss_01
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
		m_OverrideMusicTrack = MusicPlaylistType.InGame_DRGEVILBoss;
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
		case 514:
			yield return MissionPlayVO(actor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8Intro_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Intro_02);
			break;
		case 515:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8EmoteResponse_01);
			break;
		case 516:
			yield return MissionPlaySound(enemyActor, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Death_01);
			break;
		case 506:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			yield return MissionPlayVO(actor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeJ_01);
			break;
		case 505:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Naralex_Male_NightElf_Story_Guff_Mission8ExchangeJ_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Naralex_Male_NightElf_Story_Guff_Mission8ExchangeJ_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 517:
			yield return MissionPlayVO(enemyActor, m_InGame_BossIdle);
			break;
		case 510:
			yield return MissionPlayVO(enemyActor, m_InGame_BossUsesHeroPower);
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
			GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
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
			GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
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
		switch (turn)
		{
		case 3:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeA_01);
			break;
		case 5:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeB_01);
			break;
		case 7:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeC_01);
			break;
		case 9:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeD_01);
			break;
		case 11:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeE_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeE_02);
			break;
		case 13:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeF_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeF_02);
			yield return MissionPlayVO(Brukan_BrassRing, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission8ExchangeF_03);
			break;
		case 15:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeG_01);
			yield return MissionPlayVO(Tamsin_BrassRing, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission8ExchangeG_02);
			yield return MissionPlayVO(Rokara_B_BrassRing, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission8ExchangeG_03);
			break;
		case 17:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeH_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeH_02);
			break;
		}
	}
}
