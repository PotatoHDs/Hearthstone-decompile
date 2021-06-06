using System.Collections;
using System.Collections.Generic;

public class BOM_03_Guff_Fight_01 : BOM_03_Guff_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Death_01 = new AssetReference("VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Death_01.prefab:386bffea3459a7547a25a673235e3578");

	private static readonly AssetReference VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1EmoteResponse_01 = new AssetReference("VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1EmoteResponse_01.prefab:6e3c3fc142cb56343b598465cc1cf4b6");

	private static readonly AssetReference VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1HeroPower_01 = new AssetReference("VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1HeroPower_01.prefab:4ae2228bfa9439c4ab97658f23b54ce8");

	private static readonly AssetReference VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Idle_01 = new AssetReference("VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Idle_01.prefab:34166ad121ef7ce468e7c05a938f4d87");

	private static readonly AssetReference VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Intro_01 = new AssetReference("VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Intro_01.prefab:5a1b2de9babe078448562118b0e5c313");

	private static readonly AssetReference VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Loss_01 = new AssetReference("VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Loss_01.prefab:89c999f92557cf34a93c91c32c45fb5d");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeA_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeA_01.prefab:d5638acd9df669d4ab955006ce578974");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeD_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeD_02.prefab:c740e74c569331a43afa54efd6bffa9e");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeE_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeE_01.prefab:22dc5eb0ebd705a4299b19340ee03eee");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1Intro_01.prefab:1cce85f0a921c724f861e4edc1b0a41d");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1Victory_01.prefab:1c3bc5184090af4419cbebbd7ad83d23");

	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeA_02 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeA_02.prefab:9f08d480ec0c23447be5d7376fe71022");

	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeB_01.prefab:52586fe50f2b6324fb6071c8e3de4878");

	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeC_01.prefab:d77ccd44b150d0342a7b92e43b8a90d3");

	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeD_01 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeD_01.prefab:c378aacd545101e41ab289e662418e73");

	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeE_02 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeE_02.prefab:f0de5b7fe61e94742aab18e02e79aabc");

	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeF_01 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeF_01.prefab:6e6ab33262971dd47832035800074100");

	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeG_01 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeG_01.prefab:01e68067f62062a4e9aee7ca4ff559e4");

	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeH_01 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeH_01.prefab:8019e26487d4f40409bffee3c40b087a");

	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeJ_01 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeJ_01.prefab:db3ed0bb4a34e18489ed2592ec92b2b3");

	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1Victory_02 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1Victory_02.prefab:3671b0bc221dec043bbe8aeb78015a75");

	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission8Victory_02 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission8Victory_02.prefab:d6dbd7597ceb080499e2dcb862bc11f9");

	private static readonly AssetReference VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_PreMission1_01 = new AssetReference("VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_PreMission1_01.prefab:0da641ea5cc9d0d4eac18b364039c00f");

	private List<string> m_missionEventTriggerInGame_VictoryPreExplosionLines = new List<string> { VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1Victory_01, VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1Victory_02 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeA_01, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeD_02, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeE_01, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1Intro_01, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1Victory_01, VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeA_02, VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeB_01, VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeC_01, VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeD_01, VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeE_02,
			VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeF_01, VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeG_01, VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeH_01, VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeJ_01, VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1Victory_02, VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Death_01, VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1EmoteResponse_01, VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1HeroPower_01, VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Idle_01, VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Intro_01,
			VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Loss_01
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
		m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
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
			yield return MissionPlayVO(actor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1Intro_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Intro_01);
			break;
		case 515:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1EmoteResponse_01);
			break;
		case 516:
			yield return MissionPlaySound(enemyActor, VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Death_01);
			break;
		case 510:
			yield return MissionPlaySound(enemyActor, VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1HeroPower_01);
			break;
		case 517:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Idle_01);
			break;
		case 506:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_AngryTreant_Male_Treant_Story_Guff_Mission1Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 505:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(actor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1Victory_01);
			yield return MissionPlayVO(Hamuul_20_4_BrassRing_Quote, VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1Victory_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 100:
			yield return MissionPlayVO(Hamuul_20_4_BrassRing_Quote, VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeF_01);
			break;
		case 101:
			yield return MissionPlayVO(Hamuul_20_4_BrassRing_Quote, VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeG_01);
			break;
		case 102:
			yield return MissionPlayVO(Hamuul_20_4_BrassRing_Quote, VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeH_01);
			break;
		case 103:
			yield return MissionPlayVO(Hamuul_20_4_BrassRing_Quote, VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeJ_01);
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
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeA_01);
			yield return MissionPlayVO(Hamuul_20_4_BrassRing_Quote, VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeA_02);
			break;
		case 5:
			yield return MissionPlayVO(Hamuul_20_4_BrassRing_Quote, VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeB_01);
			break;
		case 7:
			yield return MissionPlayVO(Hamuul_20_4_BrassRing_Quote, VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeC_01);
			break;
		case 9:
			yield return MissionPlayVO(Hamuul_20_4_BrassRing_Quote, VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeD_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeD_02);
			break;
		case 13:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission1ExchangeE_01);
			yield return MissionPlayVO(Hamuul_20_4_BrassRing_Quote, VO_Story_Hero_Hamuul_Male_Tauren_Story_Guff_Mission1ExchangeE_02);
			break;
		}
	}
}
