using System.Collections;
using System.Collections.Generic;

public class BOM_02_Xyrella_Fight_07 : BOM_02_Xyrella_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Death_01 = new AssetReference("VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Death_01.prefab:2785bc841d610b549a4f4e868b226db4");

	private static readonly AssetReference VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7EmoteResponse_01 = new AssetReference("VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7EmoteResponse_01.prefab:43b35f0e54999a145a9c60206232352c");

	private static readonly AssetReference VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_01 = new AssetReference("VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_01.prefab:911c91eea8a04dc4ab213687cb726ccb");

	private static readonly AssetReference VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_02 = new AssetReference("VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_02.prefab:bb82e530821b8564b89fa29b79b6506a");

	private static readonly AssetReference VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_03 = new AssetReference("VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_03.prefab:f94e4f9bd1a731c40bda668e95d0ee42");

	private static readonly AssetReference VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_01 = new AssetReference("VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_01.prefab:0b5d0918b550f1d49b0eb996ab71dac0");

	private static readonly AssetReference VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_02 = new AssetReference("VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_02.prefab:a6606ccf7bf663e4d96219c8e617a137");

	private static readonly AssetReference VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_03 = new AssetReference("VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_03.prefab:7ab93e64ad861d644b9079cb26a50b69");

	private static readonly AssetReference VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Intro_02 = new AssetReference("VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Intro_02.prefab:60b14b55b9e83854983a7f101855a31a");

	private static readonly AssetReference VO_Story_Hero_Scabbs_Male_Gnome_Story_Xyrella_Mission7Victory_02 = new AssetReference("VO_Story_Hero_Scabbs_Male_Gnome_Story_Xyrella_Mission7Victory_02.prefab:daf1699f5539a454281ebe83a0b0d04c");

	private static readonly AssetReference VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission7Loss_01 = new AssetReference("VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission7Loss_01.prefab:c23d06e87d09c3840bc8306ecd635d88");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeA_01.prefab:a9bbcdca6d682d84a8139ecb2d8cc0fc");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeB_01.prefab:db1bb406fde184a4c8a1d7ede25c14d2");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeD_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeD_01.prefab:39cea72b98ba90e4388bb999aa46d036");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeF_03 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeF_03.prefab:2674a36f28f955b4e93d23b50be9a067");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeJ_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeJ_01.prefab:238288c4b67cbda42be01705322d4c45");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeJ_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeJ_02.prefab:8b4c332fa00107244962e8d42eab7eb5");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7Intro_01.prefab:3866a8b1dc2edb14ba269737feae1c4a");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7Victory_03 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7Victory_03.prefab:b279a43ac5e607f44a26965aecfa3c20");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeA_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeA_02.prefab:46e359c07fdd9fc48b5de45448f10e75");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeB_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeB_02.prefab:be4cf4584f7b01e4a81681d610e78e9a");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeC_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeC_01.prefab:d35cde4756df6be48a6c4597fd4fcbb2");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeD_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeD_02.prefab:50c37180ca083be468b83d9e03c83faa");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeE_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeE_01.prefab:a711bd9a47c1e6e46aee2ceb52f9353f");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeE_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeE_02.prefab:a1d144f136489b441a0dcefc220b332b");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeF_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeF_01.prefab:d97e49208ecbac149bc88b96a4815739");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeH_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeH_01.prefab:1addf7e6cc01df444be97fb0ebbebfdd");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7ExchangeF_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7ExchangeF_02.prefab:bb948b43461e13b48807951069c3dfc2");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7ExchangeG_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7ExchangeG_01.prefab:62e7ec9745226ed409bdad62fce73b34");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7Victory_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7Victory_01.prefab:e6bba64e2009eba4eba57204a98d83ac");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_01, VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_02, VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_03 };

	private List<string> m_InGameBossIdleLines = new List<string> { VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_01, VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_02, VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_03 };

	private List<string> m_IntroductionLines = new List<string> { VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Intro_02, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7Intro_01 };

	private List<string> m_missionEventTriggerInGame_VictoryPreExplosionLines = new List<string> { VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7Victory_01, VO_Story_Hero_Scabbs_Male_Gnome_Story_Xyrella_Mission7Victory_02, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7Victory_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Death_01, VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7EmoteResponse_01, VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_01, VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_02, VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_03, VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_01, VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_02, VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_03, VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Intro_02, VO_Story_Hero_Scabbs_Male_Gnome_Story_Xyrella_Mission7Victory_02,
			VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission7Loss_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeA_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeB_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeD_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeF_03, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeJ_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeJ_02, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7Intro_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7Victory_03, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeA_02,
			VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeB_02, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeC_01, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeD_02, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeE_01, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeE_02, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeF_01, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeH_01, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7ExchangeF_02, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7ExchangeG_01, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7Victory_01
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
		m_OverrideMusicTrack = MusicPlaylistType.InGame_BT_FinalBoss;
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
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7Intro_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Intro_02);
			break;
		case 506:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO("BOM_02_Tavish_01t", VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission7Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeJ_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeJ_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 505:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO("BOM_02_Tavish_01t", VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7Victory_01);
			yield return MissionPlayVO("BOM_02_Scabbs_06t", VO_Story_Hero_Scabbs_Male_Gnome_Story_Xyrella_Mission7Victory_02);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7Victory_03);
			GameState.Get().SetBusy(busy: false);
			break;
		case 516:
			yield return MissionPlaySound(enemyActor, VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Death_01);
			break;
		case 515:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7EmoteResponse_01);
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
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeA_01);
			yield return MissionPlayVO("BOM_02_Scabbs_06t", VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeA_02);
			break;
		case 5:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeB_01);
			yield return MissionPlayVO("BOM_02_Scabbs_06t", VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeB_02);
			break;
		case 7:
			yield return MissionPlayVO("BOM_02_Scabbs_06t", VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeC_01);
			break;
		case 9:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeD_01);
			yield return MissionPlayVO("BOM_02_Scabbs_06t", VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeD_02);
			break;
		case 11:
			yield return MissionPlayVO("BOM_02_Scabbs_06t", VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeE_01);
			yield return MissionPlayVO("BOM_02_Scabbs_06t", VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeE_02);
			break;
		case 13:
			yield return MissionPlayVO("BOM_02_Scabbs_06t", VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeF_01);
			yield return MissionPlayVO("BOM_02_Tavish_01t", VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7ExchangeF_02);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeF_03);
			break;
		case 15:
			yield return MissionPlayVO("BOM_02_Tavish_01t", VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7ExchangeG_01);
			break;
		case 17:
			yield return MissionPlayVO("BOM_02_Scabbs_06t", VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeH_01);
			break;
		}
	}
}
