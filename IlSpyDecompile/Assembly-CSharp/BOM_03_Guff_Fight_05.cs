using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOM_03_Guff_Fight_05 : BOM_03_Guff_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeA_01.prefab:1bc16559d9255694ca52ccc0b163a798");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeC_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeC_Brukan_01.prefab:c5ea4dee636a6b647b96dfb1d8afc557");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeD_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeD_Brukan_01.prefab:94cf2a3468b79ab49afc2e844595ad45");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeD_Brukan_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeD_Brukan_02.prefab:d912c1f29bf219440afe08a062531036");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeC_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeC_Dawngrasp_01.prefab:413f0961ae860c746a89d0510b2326be");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeD_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeD_Dawngrasp_01.prefab:afe57944a76eff74e90c84c35407028a");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeD_Dawngrasp_03 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeD_Dawngrasp_03.prefab:075c38cd73c032248a2e7fa490a1c72a");

	private static readonly AssetReference VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Death_01 = new AssetReference("VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Death_01.prefab:f64543a1723aa2e4a8faa77aecaf7fe4");

	private static readonly AssetReference VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5EmoteResponse_01 = new AssetReference("VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5EmoteResponse_01.prefab:84123ec0b7bd24744a7535eb0763d6b4");

	private static readonly AssetReference VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5ExchangeA_02 = new AssetReference("VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5ExchangeA_02.prefab:1b775572c0180c247ac94c1babf3715d");

	private static readonly AssetReference VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Idle_01 = new AssetReference("VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Idle_01.prefab:7662f42cb4f4daa44a188083e34320fd");

	private static readonly AssetReference VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Intro_02 = new AssetReference("VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Intro_02.prefab:e7b0001f6438eaa43b8b7c8748838c08");

	private static readonly AssetReference VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Loss_01 = new AssetReference("VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Loss_01.prefab:d910392968aa4114bb6e0f183319c2d2");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeB_01.prefab:6850324a4800c9a468f3d17525a33a69");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeC_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeC_02.prefab:3e9f6f61ac1932c4ba985ed19b0d3d56");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Dawngrasp_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Dawngrasp_02.prefab:d07b217ede4d8ee4590618e5015a2cb6");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Rokara_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Rokara_02.prefab:8514c8fab20231943a39e1e194928d89");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Tamsin_03 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Tamsin_03.prefab:d2d563c4bbb1b44488fd843ffcfd2b29");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5Intro_01.prefab:3fe16f028568e074e88088325c0abda6");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5Victory_01.prefab:15f90f4d0a4b2ee459caf32a02261a0e");

	private static readonly AssetReference VO_Story_Hero_RelentlessAdventurer_Female_Tauren_Story_Guff_Mission5Intro_02 = new AssetReference("VO_Story_Hero_RelentlessAdventurer_Female_Tauren_Story_Guff_Mission5Intro_02.prefab:dbc0e1fddd5fbcd479a7406fe2540346");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission5ExchangeC_Rokara_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission5ExchangeC_Rokara_01.prefab:a117a7676a570054abdfd05d46133095");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission5ExchangeD_Rokara_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission5ExchangeD_Rokara_01.prefab:6bb7b32ea9d989c488a5c97a85cbfbc8");

	private static readonly AssetReference VO_Story_Hero_SneakyAdventurer_Female_Forsaken_Story_Guff_Mission5ExchangeB_03 = new AssetReference("VO_Story_Hero_SneakyAdventurer_Female_Forsaken_Story_Guff_Mission5ExchangeB_03.prefab:0ac9808ad16640547879f4e244cc864e");

	private static readonly AssetReference VO_Story_Hero_SneakyAdventurer_Female_Forsaken_Story_Guff_Mission5Idle_02 = new AssetReference("VO_Story_Hero_SneakyAdventurer_Female_Forsaken_Story_Guff_Mission5Idle_02.prefab:6cc4a231a1d53fa4db333db49280f96f");

	private static readonly AssetReference VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5ExchangeB_02 = new AssetReference("VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5ExchangeB_02.prefab:2ff567b9921b0d144af7867a9ea27a64");

	private static readonly AssetReference VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5Idle_03 = new AssetReference("VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5Idle_03.prefab:878d237e6533b5a41bef3650a52cbaa7");

	private static readonly AssetReference VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5Intro_02 = new AssetReference("VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5Intro_02.prefab:66e82c5f372347d4baca5d3daa2f9451");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeC_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeC_Tamsin_01.prefab:b2a114c27e4e47644a87cd68e851387c");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeD_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeD_Tamsin_01.prefab:dea96413edd03cc4b850b343a2c25fff");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeD_Tamsin_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeD_Tamsin_02.prefab:589cee937ce60b44eaf559a8f4d7b330");

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeC_Brukan_01, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeD_Brukan_01, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeD_Brukan_02, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeC_Dawngrasp_01, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeD_Dawngrasp_01, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeD_Dawngrasp_03, VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Death_01, VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5EmoteResponse_01, VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5ExchangeA_02, VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Idle_01,
			VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Intro_02, VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Loss_01, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeA_01, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeB_01, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeC_02, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Dawngrasp_02, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Rokara_02, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Tamsin_03, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5Intro_01, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5Victory_01,
			VO_Story_Hero_RelentlessAdventurer_Female_Tauren_Story_Guff_Mission5Intro_02, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission5ExchangeC_Rokara_01, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission5ExchangeD_Rokara_01, VO_Story_Hero_SneakyAdventurer_Female_Forsaken_Story_Guff_Mission5ExchangeB_03, VO_Story_Hero_SneakyAdventurer_Female_Forsaken_Story_Guff_Mission5Idle_02, VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5ExchangeB_02, VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5Idle_03, VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5Intro_02, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeC_Tamsin_01, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeD_Tamsin_01,
			VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeD_Tamsin_02
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
			yield return MissionPlayVO(actor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5Intro_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Intro_02);
			break;
		case 515:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5EmoteResponse_01);
			break;
		case 516:
			yield return MissionPlaySound(enemyActor, VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Death_01);
			break;
		case 506:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 505:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(actor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5Victory_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 517:
		{
			float num = Random.Range(0f, 3f);
			if (num < 1f && (bool)GetActorByCardId("WC_034t6"))
			{
				yield return MissionPlayVO("WC_034t6", VO_Story_Hero_SneakyAdventurer_Female_Forsaken_Story_Guff_Mission5Idle_02);
			}
			else if (num < 2f && (bool)GetActorByCardId("WC_034t8"))
			{
				yield return MissionPlayVO("WC_034t8", VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5Idle_03);
			}
			else
			{
				yield return MissionPlayVO(enemyActor, VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Idle_01);
			}
			break;
		}
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
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 3:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeA_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5ExchangeA_02);
			break;
		case 5:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeB_01);
			yield return MissionPlayVO("WC_034t8", VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5ExchangeB_02);
			yield return MissionPlayVO("WC_034t6", VO_Story_Hero_SneakyAdventurer_Female_Forsaken_Story_Guff_Mission5ExchangeB_03);
			break;
		case 9:
			if (HeroPowerBrukan)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeC_Brukan_01);
			}
			if (HeroPowerDawngrasp)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeC_Dawngrasp_01);
			}
			if (HeroPowerRokara)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission5ExchangeC_Rokara_01);
			}
			if (HeroPowerTamsin)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeC_Tamsin_01);
			}
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeC_02);
			break;
		case 11:
			if (HeroPowerBrukan)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeD_Brukan_01);
			}
			if (HeroPowerDawngrasp)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeD_Dawngrasp_01);
			}
			if (HeroPowerRokara)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission5ExchangeD_Rokara_01);
			}
			if (HeroPowerTamsin)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeD_Tamsin_01);
			}
			if (HeroPowerBrukan)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeD_Brukan_02);
			}
			if (HeroPowerDawngrasp)
			{
				yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Dawngrasp_02);
			}
			if (HeroPowerRokara)
			{
				yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Rokara_02);
			}
			if (HeroPowerTamsin)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeD_Tamsin_02);
			}
			if (HeroPowerDawngrasp)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeD_Dawngrasp_03);
			}
			if (HeroPowerTamsin)
			{
				yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Tamsin_03);
			}
			break;
		}
	}
}
