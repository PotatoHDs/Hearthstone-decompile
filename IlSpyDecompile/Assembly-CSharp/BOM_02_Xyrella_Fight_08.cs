using System.Collections;
using System.Collections.Generic;

public class BOM_02_Xyrella_Fight_08 : BOM_02_Xyrella_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Death_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Death_01.prefab:43f518fbf34efbf4d99270cc9f7a77a6");

	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8EmoteResponse_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8EmoteResponse_01.prefab:5cad294295d046a41a4a8cfb445a7bcb");

	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8ExchangeA_02 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8ExchangeA_02.prefab:c5ce2ccd7c5ba554b939f6b1c5f8610f");

	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8ExchangeE_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8ExchangeE_01.prefab:bf5d4f843d8a0e64c8627fe9e7555c9e");

	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_01.prefab:2d6e853d5d63ef64280f75c901fb1fe7");

	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_02 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_02.prefab:3cbbfc6c744f52c4796be2104ec2fe9a");

	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_03 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_03.prefab:4468a374935555842959c268a43d2d4f");

	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_01.prefab:36503adb7ae026a49bf1b5885574f38b");

	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_02 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_02.prefab:ef0047eb852e9b8429fc423f52ad4edd");

	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_03 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_03.prefab:8722cd897f2df3f46af04dd61bc89e22");

	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Intro_02 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Intro_02.prefab:8b202003cfcfa8749818ebacc19f2388");

	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Loss_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Loss_01.prefab:a495b5af6a8f32c4584c95d532c4b588");

	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Victory_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Victory_01.prefab:e625a6730201f8949ae09027bdb8f113");

	private static readonly AssetReference VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission8Victory_02 = new AssetReference("VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission8Victory_02.prefab:0aa0bdf3d3333e64582f82595869b875");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8ExchangeA_01.prefab:612bbdaf98455d54facba8bbcc638e75");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8ExchangeD_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8ExchangeD_02.prefab:ceddb5d77c9092844a2cc8f84710232e");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8Intro_01.prefab:9d4c0dfb98a02b24083ea6757075bde9");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeB_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeB_01.prefab:833104168fe176c46aee47b8c8d60b0f");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeC_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeC_01.prefab:371fec3577a5664408d9524aabc6a318");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeE_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeE_02.prefab:a218db79c0239b841b308b1cbaadd907");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission8ExchangeD_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission8ExchangeD_01.prefab:89df0d79eb80033459462cbc887a579e");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_01, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_02, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_03 };

	private List<string> m_InGameBossIdleLines = new List<string> { VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_01, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_02, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Death_01, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8EmoteResponse_01, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8ExchangeA_02, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8ExchangeE_01, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_01, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_02, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_03, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_01, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_02, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_03,
			VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Intro_02, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Loss_01, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Victory_01, VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission8Victory_02, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8ExchangeA_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8ExchangeD_02, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8Intro_01, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeB_01, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeC_01, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeE_02,
			VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission8ExchangeD_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		m_OverrideMusicTrack = MusicPlaylistType.InGame_BRMAdventure;
		base.OnCreateGame();
	}

	public override List<string> GetBossIdleLines()
	{
		return m_InGameBossIdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_BossUsesHeroPowerLines;
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
			yield return MissionPlayVO(actor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8Intro_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Intro_02);
			break;
		case 506:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Victory_01);
			yield return MissionPlayVO("BOM_02_Tavish_01t", VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission8Victory_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 516:
			yield return MissionPlaySound(enemyActor, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Death_01);
			break;
		case 515:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8EmoteResponse_01);
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 3:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8ExchangeA_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8ExchangeA_02);
			break;
		case 7:
			yield return MissionPlayVO("BOM_02_Scabbs_06t", VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeB_01);
			break;
		case 11:
			yield return MissionPlayVO("BOM_02_Scabbs_06t", VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeC_01);
			break;
		case 13:
			yield return MissionPlayVO("BOM_02_Tavish_01t", VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission8ExchangeD_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8ExchangeD_02);
			break;
		}
	}
}
