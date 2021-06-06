using System.Collections;
using System.Collections.Generic;

public class BOM_02_Xyrella_Fight_03 : BOM_02_Xyrella_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Death_01 = new AssetReference("VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Death_01.prefab:553284cfa6f1d08438489f4821b73959");

	private static readonly AssetReference VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3EmoteResponse_01.prefab:3dacab2bc8d18b44eacaf5a2c54a8b32");

	private static readonly AssetReference VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Intro_02 = new AssetReference("VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Intro_02.prefab:aa34d03773f3840439f7957deda6aa38");

	private static readonly AssetReference VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Loss_01 = new AssetReference("VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Loss_01.prefab:0da5c22213931e94d889caf9cb6807aa");

	private static readonly AssetReference VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission3Victory_01 = new AssetReference("VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission3Victory_01.prefab:691298fed9f3b1445980963ba41f0e4c");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeA_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeA_02.prefab:4ea018362cea40a4e9935a5f8ac76839");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeB_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeB_01.prefab:2059c62d543c40c48b65f5cb947c3fb0");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeC_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeC_02.prefab:8a83e11d3b77e8f4495118d8d37ab7af");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeD_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeD_01.prefab:d70b4ae4d2abbae44b5ae589137bcd6c");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeE_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeE_01.prefab:76000e866a1703a42994f4d532f49209");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3Intro_01.prefab:ea38f3e5de7cf7840b102e948eb25c85");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeA_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeA_01.prefab:225ced5594e5a934cacbacd4f0afe010");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeB_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeB_02.prefab:c5ad790cfc4e7744786a63f25ebd6336");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeC_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeC_01.prefab:45681a4188d755b458f980b08041955f");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeD_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeD_02.prefab:f49514e58ec3d2542af0e7b6bf8abe9c");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeF_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeF_01.prefab:361f89aed28b8cb49aa078a174a1424b");

	private List<string> m_IntroductionLines = new List<string> { VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3Intro_01, VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Intro_02 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Death_01, VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3EmoteResponse_01, VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Intro_02, VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Loss_01, VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission3Victory_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeA_02, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeB_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeC_02, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeD_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeE_01,
			VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3Intro_01, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeA_01, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeB_02, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeC_01, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeD_02, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeF_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
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
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 514:
			yield return MissionPlayVO(actor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3Intro_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Intro_02);
			break;
		case 506:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 505:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(Tavish_BrassRing, VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission3Victory_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 516:
			yield return MissionPlaySound(enemyActor, VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Death_01);
			break;
		case 515:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3EmoteResponse_01);
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
			yield return MissionPlayVO("BOM_02_Tavish_01t", VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeA_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeA_02);
			break;
		case 7:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeB_01);
			yield return MissionPlayVO("BOM_02_Tavish_01t", VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeB_02);
			break;
		case 9:
			yield return MissionPlayVO("BOM_02_Tavish_01t", VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeC_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeC_02);
			break;
		case 11:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeD_01);
			yield return MissionPlayVO("BOM_02_Tavish_01t", VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeD_02);
			break;
		case 13:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeE_01);
			yield return MissionPlayVO("BOM_02_Tavish_01t", VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeF_01);
			break;
		}
	}
}
