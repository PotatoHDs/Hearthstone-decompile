using System.Collections;
using System.Collections.Generic;

public class BOM_02_Xyrella_Fight_02 : BOM_02_Xyrella_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Death_01 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Death_01.prefab:29971ca128573ea488eb4479810e57d9");

	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2EmoteResponse_01 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2EmoteResponse_01.prefab:a3b2b5e278c44794f9833088ecc84cda");

	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2ExchangeA_02 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2ExchangeA_02.prefab:2aa8528d86e08f5428b445cfca2ce3d8");

	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_01.prefab:d441233485d1c704a93bfb861a2323ec");

	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_02.prefab:88bcffef7c36ed04196478b1dda606f1");

	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_03.prefab:3b6f517ee3e5f00469259ab1f87a2c8e");

	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_04 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_04.prefab:9c1afe58d526d8940ad5d59660fbb504");

	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_05 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_05.prefab:abe095c594ea39e4a82e48844729f695");

	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_01 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_01.prefab:0e80df0d73bfc7341af096720ff8ef1d");

	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_02 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_02.prefab:55a078209836dbc4ebca1be4dbc25b84");

	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_03 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_03.prefab:2d48e5dd94b6fdd4485c8602738390a0");

	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Intro_02 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Intro_02.prefab:f598cb03ca6d3284e979c6a3dbe739d4");

	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Loss_01 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Loss_01.prefab:9374a8c18443f72489d46b0a0ab2ad42");

	private static readonly AssetReference VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission2Victory_01 = new AssetReference("VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission2Victory_01.prefab:ba447ffa89d528b4dbb32dc61d3baf64");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeA_01.prefab:91655efc0fe21a54e80ba0b9a0f187f6");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeB_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeB_02.prefab:0154141375140f242bb00e8958b9b0b6");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeC_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeC_02.prefab:699b8a5e3aab5e644a738e48a50e5920");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeD_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeD_01.prefab:673ba2478b2b49f429d612852699c386");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeE_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeE_01.prefab:8bd5ee971907ec144bbfaddc471aca87");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2Intro_01.prefab:92969a5db4073d648991314bdd8a7369");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2Victory_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2Victory_02.prefab:1300ac01e1a15224bbf4b87eb8094297");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeB_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeB_01.prefab:5ac8d5a0431830b4782343fcaf4dc014");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeC_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeC_01.prefab:6b403cd5654bf91438ed8804d5e57336");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeC_03 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeC_03.prefab:7ea62a0591cb9ea46a61a5649492d1d9");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeD_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeD_02.prefab:ecec765c0ed35d24ab8d12e0fa876af3");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeE_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeE_02.prefab:1906283882dbe67408943bc163c2423e");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_01, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_02, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_03, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_04, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_05 };

	private List<string> m_InGameBossIdleLines = new List<string> { VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_01, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_02, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Death_01, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2EmoteResponse_01, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2ExchangeA_02, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_01, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_02, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_03, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_04, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_05, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_01, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_02,
			VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_03, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Intro_02, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Loss_01, VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission2Victory_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeA_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeB_02, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeC_02, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeD_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeE_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2Intro_01,
			VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2Victory_02, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeB_01, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeC_01, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeC_03, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeD_02, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeE_02
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
		base.OnCreateGame();
		m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
		m_deathLine = VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Death_01;
		m_standardEmoteResponseLine = VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2EmoteResponse_01;
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
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2Intro_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Intro_02);
			break;
		case 506:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 505:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(Tavish_BrassRing, VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission2Victory_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2Victory_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 515:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2EmoteResponse_01);
			break;
		case 516:
			yield return MissionPlaySound(enemyActor, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Death_01);
			break;
		case 517:
			yield return MissionPlayVO(enemyActor, m_InGameBossIdleLines);
			break;
		case 510:
			yield return MissionPlayVO(enemyActor, m_BossUsesHeroPowerLines);
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
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeA_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2ExchangeA_02);
			break;
		case 5:
			yield return MissionPlayVO("BOM_02_Tavish_01t", VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeB_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeB_02);
			break;
		case 9:
			yield return MissionPlayVO("BOM_02_Tavish_01t", VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeC_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeC_02);
			yield return MissionPlayVO("BOM_02_Tavish_01t", VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeC_03);
			break;
		case 11:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeD_01);
			yield return MissionPlayVO("BOM_02_Tavish_01t", VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeD_02);
			break;
		case 13:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeE_01);
			yield return MissionPlayVO("BOM_02_Tavish_01t", VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeE_02);
			break;
		}
	}
}
