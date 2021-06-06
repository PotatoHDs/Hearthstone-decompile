using System.Collections;
using System.Collections.Generic;

public class BOM_01_Rokara_07 : BoM_01_Rokara_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission7ExchangeC_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission7ExchangeC_Brukan_01.prefab:f25be8a1cdfcdbc4aa71a52320d59e21");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission7Victory_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission7Victory_Brukan_01.prefab:1b5494ac121a8a54dae6d0d945899060");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission7ExchangeC_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission7ExchangeC_Dawngrasp_01.prefab:e3f0190a90aa4c641b3f594502f35515");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission7Victory_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission7Victory_Dawngrasp_01.prefab:5d53f81be6b17a340978ef51981ba03e");

	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Death_01 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Death_01.prefab:115722f12659b9a469ee286471bef94f");

	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7EmoteResponse_01 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7EmoteResponse_01.prefab:9561b6eb558ede84cb55d281ee2a82ee");

	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeA_02 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeA_02.prefab:4a2c6c5b2b4c32846933096dc7e4796f");

	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeB_02 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeB_02.prefab:b5352a23e76d4064c86565965adc133f");

	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeC_01 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeC_01.prefab:cf9bc02209fee5c4b8be08b282cfbf7b");

	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_01 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_01.prefab:1f7a0de05e54810409a0006b70a2139b");

	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_02 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_02.prefab:24c80419c8639184187e2ae32784c3c1");

	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_03 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_03.prefab:6ec079e762830fe40b28523f16b80a59");

	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_01 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_01.prefab:8433c079ab6ccae47828d976d7afc824");

	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_02 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_02.prefab:d7b4f337519d0de4890b7d6182b29b5b");

	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_03 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_03.prefab:6eb9ce89a24abbe4d835179fa4341a68");

	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Intro_02 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Intro_02.prefab:e04f83d03ba008840b7eb981ca481d09");

	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Loss_01 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Loss_01.prefab:fdc1209a793785d4cb860c057a3d5d25");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission7ExchangeC_Guff_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission7ExchangeC_Guff_01.prefab:ad3c1964160a83c43b8f8ccd6320660c");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission7Victory_Guff_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission7Victory_Guff_01.prefab:59e9b5c250a9aa84ebcee4333df4b3c2");

	private static readonly AssetReference VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeD_01 = new AssetReference("VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeD_01.prefab:e2823238bef66fd42ae7b91bee3e095c");

	private static readonly AssetReference VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeD_03 = new AssetReference("VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeD_03.prefab:933cf1c59a231c24194fcd9e91cc1abe");

	private static readonly AssetReference VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeE_01 = new AssetReference("VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeE_01.prefab:84fb08a1c49bb084a89a615cb715ddc8");

	private static readonly AssetReference VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeF_01 = new AssetReference("VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeF_01.prefab:ec993e5d45f25b941978a9dcd01d0e28");

	private static readonly AssetReference VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeG_01 = new AssetReference("VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeG_01.prefab:6da9b73e758d4f9449b24d5a547b02df");

	private static readonly AssetReference VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeG_03 = new AssetReference("VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeG_03.prefab:0ff411ed89e2e134c9dd7008bf11d979");

	private static readonly AssetReference VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7Victory_02 = new AssetReference("VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7Victory_02.prefab:86485dd69a38d134381f766b97944963");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeA_01.prefab:eddece9b68667e9459e89c9ab6edafeb");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeB_01.prefab:1678595af45de26429a4e54e0fa00080");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeD_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeD_02.prefab:a2a50b9e82e769a40bbd34d067786ad6");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeG_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeG_02.prefab:392530965d59c074da73e61a540199b0");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7Intro_01.prefab:35ef07e900094684dacc1fb4bab0f9a0");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7Victory_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7Victory_01.prefab:283423919171f1243b1d96ee930bd4e3");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission7ExchangeC_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission7ExchangeC_Tamsin_01.prefab:5c78c77539bf5a347a635bb8704d0e32");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission7Victory_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission7Victory_Tamsin_01.prefab:1a960d26618af4741a7131844a297c1a");

	private List<string> m_missionEventTriggerInGame_VictoryPostExplosionLines = new List<string> { VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission7Victory_Dawngrasp_01, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission7Victory_Guff_01, VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7Victory_02, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7Victory_01, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission7Victory_Tamsin_01 };

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_01, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_02, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_03 };

	private List<string> m_BossIdleLines2 = new List<string> { VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_01, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_02, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_03 };

	private List<string> m_IntroductionLines = new List<string> { VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7Intro_01, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Intro_02 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission7ExchangeC_Brukan_01, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission7Victory_Brukan_01, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission7ExchangeC_Dawngrasp_01, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission7Victory_Dawngrasp_01, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Death_01, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7EmoteResponse_01, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeA_02, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeB_02, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeC_01, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_01,
			VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_02, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_03, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_01, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_02, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_03, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Intro_02, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Loss_01, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission7ExchangeC_Guff_01, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission7Victory_Guff_01, VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeD_01,
			VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeD_03, VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeE_01, VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeF_01, VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeG_01, VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeG_03, VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7Victory_02, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeA_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeB_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeD_02, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeG_02,
			VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7Intro_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7Victory_01, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission7ExchangeC_Tamsin_01, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission7Victory_Tamsin_01
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
		m_deathLine = VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Death_01;
		m_standardEmoteResponseLine = VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7EmoteResponse_01;
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
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 514:
			yield return MissionPlayVO(actor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7Intro_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Intro_02);
			break;
		case 506:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 505:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(actor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7Victory_01);
			if (HeroPowerIsBrukan)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission7Victory_Brukan_01);
			}
			if (HeroPowerIsGuff)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission7Victory_Guff_01);
			}
			if (HeroPowerIsTamsin)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission7Victory_Tamsin_01);
			}
			if (HeroPowerIsDawngrasp)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission7Victory_Dawngrasp_01);
			}
			yield return MissionPlayVO(Kazakus_BrassRing, VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7Victory_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 515:
			yield return MissionPlayVO(enemyActor, m_standardEmoteResponseLine);
			break;
		case 516:
			yield return MissionPlaySound(enemyActor, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Death_01);
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
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 3:
			yield return MissionPlayVO(actor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeA_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeA_02);
			break;
		case 7:
			yield return MissionPlayVO(actor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeB_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeB_02);
			break;
		case 11:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeC_01);
			if (HeroPowerIsBrukan)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission7ExchangeC_Brukan_01);
			}
			if (HeroPowerIsGuff)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission7ExchangeC_Guff_01);
			}
			if (HeroPowerIsTamsin)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission7ExchangeC_Tamsin_01);
			}
			if (HeroPowerIsDawngrasp)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission7ExchangeC_Dawngrasp_01);
			}
			break;
		}
	}
}
