using System.Collections;
using System.Collections.Generic;

public class BOM_01_Rokara_05 : BoM_01_Rokara_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeA_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeA_Brukan_01.prefab:231f93c2e80c3ec438ba92fd150eac2e");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeC_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeC_Brukan_01.prefab:12946e2ff09c0a2429a87e38777a0bf9");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeD_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeD_Brukan_01.prefab:15070937cb52c3448a058269217ca801");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeA_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeA_Dawngrasp_01.prefab:a2ebe84b953c1ee49858b63b10ffc983");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeC_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeC_Dawngrasp_01.prefab:e38f8abddbe777747a21f21273bcdff5");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeD_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeD_Dawngrasp_01.prefab:beaf5d9b71aa9474b8537269ea31d589");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5Victory_02 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5Victory_02.prefab:0b0f4f34c752ee34aae263499513d988");

	private static readonly AssetReference VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5EmoteResponse_01 = new AssetReference("VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5EmoteResponse_01.prefab:78ca1eaa1984b684d932032310e8115e");

	private static readonly AssetReference VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5ExchangeB_01.prefab:e902d8c01b1902144a0928f584a37678");

	private static readonly AssetReference VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5ExchangeE_01 = new AssetReference("VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5ExchangeE_01.prefab:76344eb5d80a4864ea1b79b77d6b28f0");

	private static readonly AssetReference VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_01 = new AssetReference("VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_01.prefab:9e5a17bebd5baa7468761956a654704a");

	private static readonly AssetReference VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_02 = new AssetReference("VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_02.prefab:6c844673b3a39104997f78f0666bcc51");

	private static readonly AssetReference VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_03 = new AssetReference("VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_03.prefab:5ca9d14eaa956ab4ca7d2eaa14458e2e");

	private static readonly AssetReference VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Idle_01 = new AssetReference("VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Idle_01.prefab:70eb5ba8c7e9876438a6c2f209ec9846");

	private static readonly AssetReference VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Idle_02 = new AssetReference("VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Idle_02.prefab:1e9ad57c2a2ce1f42bc412fe931d9741");

	private static readonly AssetReference VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Intro_02 = new AssetReference("VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Intro_02.prefab:ea4404ca8ec47184782ecfdde1065786");

	private static readonly AssetReference VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Loss_01 = new AssetReference("VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Loss_01.prefab:afeb5fa211cced74298a899436edb989");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeA_Guff_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeA_Guff_01.prefab:c3350017627594c419c87c14a1004078");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeC_Guff_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeC_Guff_01.prefab:3f1626031808ba84d87dd4db6431a040");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeD_Guff_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeD_Guff_01.prefab:7e15044e00b9ade42a2e3be91078acbe");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeA_01.prefab:998e5255481cf2744acc637b708f6897");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeC_Brukan_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeC_Brukan_02.prefab:c3497360ce5d63246bc5aadd3cd3a59a");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeC_Guff_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeC_Guff_02.prefab:f50496add5f683d4da5388b828d5a6f6");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeD_Brukan_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeD_Brukan_02.prefab:18f8d8875ecdacf45bdbdc37304515a6");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeD_Tamsin_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeD_Tamsin_01.prefab:86c55d44baea5424e9ff65efadb5706a");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Intro_01.prefab:340f19e07efb6c1438136301cce9bebe");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Victory_01.prefab:aa500ce4b343858428df036777820181");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Victory_04 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Victory_04.prefab:ec7f616a4b47c714d9f47d7aa901064a");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeA_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeA_Tamsin_01.prefab:990639ebaf0a14545add3e60bb9d2a1c");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_Tamsin_01.prefab:f771e15f76264df4eade262acebe233c");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_Tamsin_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_Tamsin_02.prefab:7aab2d252a97f824581c17897b684f90");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeD_Tamsin_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeD_Tamsin_02.prefab:173f1b584507e9943b24b0df899a5460");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5Victory_03 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5Victory_03.prefab:f9d86738ed34284459549db2fb1a40cc");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_01, VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_02, VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_03 };

	private List<string> m_BossIdleLines2 = new List<string> { VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Idle_01, VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Idle_02 };

	private List<string> m_IntroductionLines = new List<string> { VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Intro_02, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Intro_01 };

	private List<string> m_missionEventTriggerInGame_VictoryPostExplosionLines = new List<string> { VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Victory_01, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5Victory_02, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5Victory_03, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Victory_04 };

	private List<string> m_VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_TamsinLines = new List<string> { VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_Tamsin_01, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_Tamsin_02 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeA_Brukan_01, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeC_Brukan_01, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeD_Brukan_01, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeA_Dawngrasp_01, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeC_Dawngrasp_01, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeD_Dawngrasp_01, VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5EmoteResponse_01, VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5ExchangeB_01, VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5ExchangeE_01, VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_01,
			VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_02, VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_03, VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Idle_01, VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Idle_02, VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Intro_02, VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Loss_01, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeA_Guff_01, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeC_Guff_01, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeD_Guff_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeA_01,
			VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeC_Brukan_02, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeC_Guff_02, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeD_Brukan_02, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeD_Tamsin_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Intro_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Victory_01, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5Victory_02, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5Victory_03, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Victory_04, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeA_Tamsin_01,
			VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_Tamsin_01, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_Tamsin_02, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeD_Tamsin_02
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
		m_OverrideMusicTrack = MusicPlaylistType.InGame_BT_FinalBoss;
		m_standardEmoteResponseLine = VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5EmoteResponse_01;
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
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 514:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Intro_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Intro_02);
			break;
		case 506:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Loss_01);
			break;
		case 505:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Victory_01);
			if (HeroPowerIsDawngrasp)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5Victory_02);
			}
			else
			{
				yield return MissionPlayVO(Dawngrasp_BrassRing, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5Victory_02);
			}
			if (HeroPowerIsTamsin)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5Victory_03);
			}
			else
			{
				yield return MissionPlayVO(Tamsin_BrassRing, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5Victory_03);
			}
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Victory_04);
			GameState.Get().SetBusy(busy: false);
			break;
		case 515:
			yield return MissionPlayVO(enemyActor, m_standardEmoteResponseLine);
			break;
		case 100:
			GameState.Get().SetBusy(busy: true);
			if (HeroPowerIsBrukan)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeC_Brukan_01);
				yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeC_Brukan_02);
			}
			if (HeroPowerIsGuff)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeC_Guff_01);
				yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeC_Guff_02);
			}
			if (HeroPowerIsTamsin)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_Tamsin_01);
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_Tamsin_02);
			}
			if (HeroPowerIsDawngrasp)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeC_Dawngrasp_01);
			}
			GameState.Get().SetBusy(busy: false);
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
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
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeA_01);
			if (HeroPowerIsBrukan)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeA_Brukan_01);
			}
			if (HeroPowerIsGuff)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeA_Guff_01);
			}
			if (HeroPowerIsTamsin)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeA_Tamsin_01);
			}
			if (HeroPowerIsDawngrasp)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeA_Dawngrasp_01);
			}
			break;
		case 7:
			yield return MissionPlayVO(actor, VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5ExchangeB_01);
			break;
		case 15:
			if (HeroPowerIsBrukan)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeD_Brukan_01);
				yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeD_Brukan_02);
			}
			if (HeroPowerIsGuff)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeD_Guff_01);
			}
			if (HeroPowerIsTamsin)
			{
				yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeD_Tamsin_01);
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeD_Tamsin_02);
			}
			if (HeroPowerIsDawngrasp)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeD_Dawngrasp_01);
			}
			break;
		case 19:
			yield return MissionPlayVO(actor, VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5ExchangeE_01);
			break;
		}
	}
}
