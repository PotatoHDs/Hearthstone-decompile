using System.Collections;
using System.Collections.Generic;

public class BOM_01_Rokara_06 : BoM_01_Rokara_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission6ExchangeC_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission6ExchangeC_Brukan_01.prefab:5860080393630a34688bc48cfba0c13d");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission6ExchangeD_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission6ExchangeD_02.prefab:e5bc900cb4c5dc141abd3084625d3f66");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission6ExchangeC_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission6ExchangeC_Dawngrasp_01.prefab:adf54e9ead2d06f49a3f9df165503fb0");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission6ExchangeC_Guff_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission6ExchangeC_Guff_01.prefab:ee7d75a239a37e342bd79c82df28b763");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeA_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeA_01.prefab:48fa5f24a9e7632478cef26e990809ef");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeB_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeB_02.prefab:00591dc1e45462a4eb391b8d371e1398");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeD_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeD_01.prefab:3ec027ef1603da14d8c4fb94b77ecf8b");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6Intro_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6Intro_02.prefab:3e9aae236b90bb9448f4384f032a0d6c");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6Victory_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6Victory_01.prefab:582e7238727bfeb47b52ce011cb05ecf");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission6ExchangeC_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission6ExchangeC_Tamsin_01.prefab:25fe952211d929b449860e5738be041f");

	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Death_01 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Death_01.prefab:a3028e433d633df448cb292b5e43cb9e");

	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6EmoteResponse_01 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6EmoteResponse_01.prefab:e3fcafe501c01e343908f2461715e11c");

	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6ExchangeA_02 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6ExchangeA_02.prefab:319f093f72b58da46b540a4ff12d0e78");

	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6ExchangeB_01 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6ExchangeB_01.prefab:562143d64f9a09445be0a189449a2824");

	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6HeroPower_01 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6HeroPower_01.prefab:cc7946ba21796484b9eff7a499adf736");

	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6HeroPower_02 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6HeroPower_02.prefab:1457bfc23d454404786458c7f1a615f3");

	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_01 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_01.prefab:a1c23fd415b999340a5456c9cec61e75");

	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_02 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_02.prefab:02061417d6e6b5244ab2ea911e965694");

	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_03 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_03.prefab:435c9bcbc35299147a186bd6f4137fb7");

	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Intro_01.prefab:f59b701a91724504bb6eb712b81f9873");

	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Loss_01 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Loss_01.prefab:7ff1903f9c6cd824e98be7330725e3df");

	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Victory_02 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Victory_02.prefab:097e13f3eda817441b22ff5e68449ad4");

	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Victory_03 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Victory_03.prefab:5ec3738994efaa447bc62b0d313a9587");

	private List<string> m_IntroductionLines = new List<string> { VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6Intro_02, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Intro_01 };

	private List<string> m_missionEventTriggerInGame_VictoryPostExplosionLines = new List<string> { VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6Victory_01, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Victory_02, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Victory_03 };

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6HeroPower_01, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6HeroPower_02 };

	private List<string> m_BossIdleLines2 = new List<string> { VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_01, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_02, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission6ExchangeC_Brukan_01, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission6ExchangeD_02, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission6ExchangeC_Dawngrasp_01, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission6ExchangeC_Guff_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeA_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeB_02, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeD_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6Intro_02, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6Victory_01, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission6ExchangeC_Tamsin_01,
			VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Death_01, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6EmoteResponse_01, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6ExchangeA_02, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6ExchangeB_01, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6HeroPower_01, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6HeroPower_02, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_01, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_02, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_03, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Intro_01,
			VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Loss_01, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Victory_02, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Victory_03
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetBossIdleLines()
	{
		return m_BossIdleLines2;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_BossUsesHeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
		m_deathLine = VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Death_01;
		m_standardEmoteResponseLine = VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6EmoteResponse_01;
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
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Intro_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6Intro_02);
			break;
		case 506:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Loss_01);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6Victory_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Victory_02);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Victory_03);
			GameState.Get().SetBusy(busy: false);
			break;
		case 515:
			yield return MissionPlayVO(enemyActor, m_standardEmoteResponseLine);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
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
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 3:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeA_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6ExchangeA_02);
			break;
		case 7:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6ExchangeB_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeB_02);
			break;
		case 11:
			if (HeroPowerIsBrukan)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission6ExchangeC_Brukan_01);
			}
			if (HeroPowerIsGuff)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission6ExchangeC_Guff_01);
			}
			if (HeroPowerIsTamsin)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission6ExchangeC_Tamsin_01);
			}
			if (HeroPowerIsDawngrasp)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission6ExchangeC_Dawngrasp_01);
			}
			break;
		case 15:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeD_01);
			yield return MissionPlayVO(Brukan_BrassRing, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission6ExchangeD_02);
			break;
		}
	}
}
