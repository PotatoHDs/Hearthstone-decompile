using System.Collections;
using System.Collections.Generic;

public class BOM_02_Xyrella_Fight_06 : BOM_02_Xyrella_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Scabbs_Male_Gnome_Story_Xyrella_Mission6Victory_01 = new AssetReference("VO_Story_Hero_Scabbs_Male_Gnome_Story_Xyrella_Mission6Victory_01.prefab:a1fc16514848d5641a7e7dce007d9ed2");

	private static readonly AssetReference VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Death_01 = new AssetReference("VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Death_01.prefab:09e1677e153cf50418020816f5f5cfc7");

	private static readonly AssetReference VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6EmoteResponse_01 = new AssetReference("VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6EmoteResponse_01.prefab:0704f6ff4b9ad8f46a8ea058878a2b7e");

	private static readonly AssetReference VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_01 = new AssetReference("VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_01.prefab:d69aaff20b2436c40a4086ed12cc0f2e");

	private static readonly AssetReference VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_02 = new AssetReference("VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_02.prefab:036bb61ac57cd89408c1a24a88f0ad8a");

	private static readonly AssetReference VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_03 = new AssetReference("VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_03.prefab:319b2ad12d1b2844ea5d4ba03b9620e8");

	private static readonly AssetReference VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_01 = new AssetReference("VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_01.prefab:23d79e2afc9274842a22b7d3f130bf41");

	private static readonly AssetReference VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_02 = new AssetReference("VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_02.prefab:501e0696e2698cd4d9fcfbc9509c497c");

	private static readonly AssetReference VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_03 = new AssetReference("VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_03.prefab:52860b615404ef447b5d08335fd4b2f0");

	private static readonly AssetReference VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Intro_02 = new AssetReference("VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Intro_02.prefab:1066e7d924814b241ba79d00ba6d2e37");

	private static readonly AssetReference VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Loss_01 = new AssetReference("VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Loss_01.prefab:e17ecbcf0163a3744a14ca0c13bea323");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeA_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeA_02.prefab:7164607a1c8c8044e8728b327c3f31ca");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeC_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeC_01.prefab:6a5ca7f24b8d19a419e38ac0b4664626");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeF_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeF_01.prefab:feb5cc73fee7572409f8fb6e5d9b1169");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6Intro_01.prefab:178f2f835dc377b4294e5d0ab2d4fe2b");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6Victory_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6Victory_02.prefab:f37054c6319be9f47832ec4dfc3c2346");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeA_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeA_01.prefab:c05d6e353d567404e845547b844a8370");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeB_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeB_01.prefab:476d4a12ee01cb14fb6469b9a79cab19");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeD_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeD_02.prefab:1514d4d8ba7386b4ea220beaf26ab763");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeE_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeE_02.prefab:374918a1a9d485848947d64661187b4c");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeF_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeF_02.prefab:ca21672a5fd2e4246bc74411a79cf98c");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeB_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeB_02.prefab:69d970118b7c76b409a6ea45dcead83a");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeD_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeD_01.prefab:841221a8530bd7040a66a0186453d7c1");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeE_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeE_01.prefab:3206ae84589b22b42a2a681e0628e301");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_01, VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_02, VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_03 };

	private List<string> m_InGameBossIdleLines = new List<string> { VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_01, VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_02, VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Scabbs_Male_Gnome_Story_Xyrella_Mission6Victory_01, VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Death_01, VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6EmoteResponse_01, VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_01, VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_02, VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_03, VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_01, VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_02, VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_03, VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Intro_02,
			VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Loss_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeA_02, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeC_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeF_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6Intro_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6Victory_02, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeA_01, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeB_01, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeD_02, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeE_02,
			VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeF_02, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeB_02, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeD_01, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeE_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetBossIdleLines()
	{
		return m_InGameBossIdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_BossUsesHeroPowerLines;
	}

	public override void OnCreateGame()
	{
		m_OverrideMusicTrack = MusicPlaylistType.InGame_BOT;
		base.OnCreateGame();
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
		case 514:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6Intro_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Intro_02);
			break;
		case 506:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 505:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO("BOM_02_Scabbs_06t", VO_Story_Hero_Scabbs_Male_Gnome_Story_Xyrella_Mission6Victory_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6Victory_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 516:
			yield return MissionPlaySound(enemyActor, VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Death_01);
			break;
		case 515:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6EmoteResponse_01);
			break;
		case 517:
			yield return MissionPlayVO(enemyActor, m_InGameBossIdleLines);
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
		case 3:
			yield return MissionPlayVO("BOM_02_Scabbs_06t", VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeA_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeA_02);
			break;
		case 7:
			yield return MissionPlayVO("BOM_02_Scabbs_06t", VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeB_01);
			yield return MissionPlayVO("BOM_02_Tavish_01t", VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeB_02);
			break;
		case 9:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeC_01);
			break;
		case 11:
			yield return MissionPlayVO("BOM_02_Tavish_01t", VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeD_01);
			yield return MissionPlayVO("BOM_02_Scabbs_06t", VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeD_02);
			break;
		case 15:
			yield return MissionPlayVO("BOM_02_Tavish_01t", VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeE_01);
			yield return MissionPlayVO("BOM_02_Scabbs_06t", VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeE_02);
			break;
		case 17:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeF_01);
			yield return MissionPlayVO("BOM_02_Scabbs_06t", VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeF_02);
			break;
		}
	}
}
