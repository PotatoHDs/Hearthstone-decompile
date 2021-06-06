using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000577 RID: 1399
public class BOM_03_Guff_Fight_05 : BOM_03_Guff_Dungeon
{
	// Token: 0x06004DD0 RID: 19920 RVA: 0x0019B96C File Offset: 0x00199B6C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_03_Guff_Fight_05.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeC_Brukan_01,
			BOM_03_Guff_Fight_05.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeD_Brukan_01,
			BOM_03_Guff_Fight_05.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeD_Brukan_02,
			BOM_03_Guff_Fight_05.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeC_Dawngrasp_01,
			BOM_03_Guff_Fight_05.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeD_Dawngrasp_01,
			BOM_03_Guff_Fight_05.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeD_Dawngrasp_03,
			BOM_03_Guff_Fight_05.VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Death_01,
			BOM_03_Guff_Fight_05.VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5EmoteResponse_01,
			BOM_03_Guff_Fight_05.VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5ExchangeA_02,
			BOM_03_Guff_Fight_05.VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Idle_01,
			BOM_03_Guff_Fight_05.VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Intro_02,
			BOM_03_Guff_Fight_05.VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Loss_01,
			BOM_03_Guff_Fight_05.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeA_01,
			BOM_03_Guff_Fight_05.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeB_01,
			BOM_03_Guff_Fight_05.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeC_02,
			BOM_03_Guff_Fight_05.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Dawngrasp_02,
			BOM_03_Guff_Fight_05.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Rokara_02,
			BOM_03_Guff_Fight_05.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Tamsin_03,
			BOM_03_Guff_Fight_05.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5Intro_01,
			BOM_03_Guff_Fight_05.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5Victory_01,
			BOM_03_Guff_Fight_05.VO_Story_Hero_RelentlessAdventurer_Female_Tauren_Story_Guff_Mission5Intro_02,
			BOM_03_Guff_Fight_05.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission5ExchangeC_Rokara_01,
			BOM_03_Guff_Fight_05.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission5ExchangeD_Rokara_01,
			BOM_03_Guff_Fight_05.VO_Story_Hero_SneakyAdventurer_Female_Forsaken_Story_Guff_Mission5ExchangeB_03,
			BOM_03_Guff_Fight_05.VO_Story_Hero_SneakyAdventurer_Female_Forsaken_Story_Guff_Mission5Idle_02,
			BOM_03_Guff_Fight_05.VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5ExchangeB_02,
			BOM_03_Guff_Fight_05.VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5Idle_03,
			BOM_03_Guff_Fight_05.VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5Intro_02,
			BOM_03_Guff_Fight_05.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeC_Tamsin_01,
			BOM_03_Guff_Fight_05.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeD_Tamsin_01,
			BOM_03_Guff_Fight_05.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeD_Tamsin_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004DD1 RID: 19921 RVA: 0x0019A49C File Offset: 0x0019869C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
	}

	// Token: 0x06004DD2 RID: 19922 RVA: 0x0019BBC0 File Offset: 0x00199DC0
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 505)
		{
			if (missionEvent != 506)
			{
				switch (missionEvent)
				{
				case 514:
					yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_05.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5Intro_01);
					yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_05.VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Intro_02);
					break;
				case 515:
					yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_05.VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5EmoteResponse_01);
					break;
				case 516:
					yield return base.MissionPlaySound(enemyActor, BOM_03_Guff_Fight_05.VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Death_01);
					break;
				case 517:
				{
					float num = UnityEngine.Random.Range(0f, 3f);
					if (num < 1f && base.GetActorByCardId("WC_034t6"))
					{
						yield return base.MissionPlayVO("WC_034t6", BOM_03_Guff_Fight_05.VO_Story_Hero_SneakyAdventurer_Female_Forsaken_Story_Guff_Mission5Idle_02);
					}
					else if (num < 2f && base.GetActorByCardId("WC_034t8"))
					{
						yield return base.MissionPlayVO("WC_034t8", BOM_03_Guff_Fight_05.VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5Idle_03);
					}
					else
					{
						yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_05.VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Idle_01);
					}
					break;
				}
				default:
					yield return base.HandleMissionEventWithTiming(missionEvent);
					break;
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_05.VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Loss_01);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_05.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5Victory_01);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004DD3 RID: 19923 RVA: 0x0019BBD6 File Offset: 0x00199DD6
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x06004DD4 RID: 19924 RVA: 0x0019BBEC File Offset: 0x00199DEC
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x06004DD5 RID: 19925 RVA: 0x0019BC02 File Offset: 0x00199E02
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower().GetCard().GetActor();
		if (turn <= 5)
		{
			if (turn != 3)
			{
				if (turn == 5)
				{
					yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_05.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeB_01);
					yield return base.MissionPlayVO("WC_034t8", BOM_03_Guff_Fight_05.VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5ExchangeB_02);
					yield return base.MissionPlayVO("WC_034t6", BOM_03_Guff_Fight_05.VO_Story_Hero_SneakyAdventurer_Female_Forsaken_Story_Guff_Mission5ExchangeB_03);
				}
			}
			else
			{
				yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_05.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeA_01);
				yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_05.VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5ExchangeA_02);
			}
		}
		else if (turn != 9)
		{
			if (turn == 11)
			{
				if (this.HeroPowerBrukan)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_05.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeD_Brukan_01);
				}
				if (this.HeroPowerDawngrasp)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_05.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeD_Dawngrasp_01);
				}
				if (this.HeroPowerRokara)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_05.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission5ExchangeD_Rokara_01);
				}
				if (this.HeroPowerTamsin)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_05.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeD_Tamsin_01);
				}
				if (this.HeroPowerBrukan)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_05.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeD_Brukan_02);
				}
				if (this.HeroPowerDawngrasp)
				{
					yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_05.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Dawngrasp_02);
				}
				if (this.HeroPowerRokara)
				{
					yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_05.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Rokara_02);
				}
				if (this.HeroPowerTamsin)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_05.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeD_Tamsin_02);
				}
				if (this.HeroPowerDawngrasp)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_05.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeD_Dawngrasp_03);
				}
				if (this.HeroPowerTamsin)
				{
					yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_05.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Tamsin_03);
				}
			}
		}
		else
		{
			if (this.HeroPowerBrukan)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_05.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeC_Brukan_01);
			}
			if (this.HeroPowerDawngrasp)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_05.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeC_Dawngrasp_01);
			}
			if (this.HeroPowerRokara)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_05.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission5ExchangeC_Rokara_01);
			}
			if (this.HeroPowerTamsin)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_05.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeC_Tamsin_01);
			}
			yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_05.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeC_02);
		}
		yield break;
	}

	// Token: 0x04004459 RID: 17497
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeA_01.prefab:1bc16559d9255694ca52ccc0b163a798");

	// Token: 0x0400445A RID: 17498
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeC_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeC_Brukan_01.prefab:c5ea4dee636a6b647b96dfb1d8afc557");

	// Token: 0x0400445B RID: 17499
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeD_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeD_Brukan_01.prefab:94cf2a3468b79ab49afc2e844595ad45");

	// Token: 0x0400445C RID: 17500
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeD_Brukan_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission5ExchangeD_Brukan_02.prefab:d912c1f29bf219440afe08a062531036");

	// Token: 0x0400445D RID: 17501
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeC_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeC_Dawngrasp_01.prefab:413f0961ae860c746a89d0510b2326be");

	// Token: 0x0400445E RID: 17502
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeD_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeD_Dawngrasp_01.prefab:afe57944a76eff74e90c84c35407028a");

	// Token: 0x0400445F RID: 17503
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeD_Dawngrasp_03 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission5ExchangeD_Dawngrasp_03.prefab:075c38cd73c032248a2e7fa490a1c72a");

	// Token: 0x04004460 RID: 17504
	private static readonly AssetReference VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Death_01 = new AssetReference("VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Death_01.prefab:f64543a1723aa2e4a8faa77aecaf7fe4");

	// Token: 0x04004461 RID: 17505
	private static readonly AssetReference VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5EmoteResponse_01 = new AssetReference("VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5EmoteResponse_01.prefab:84123ec0b7bd24744a7535eb0763d6b4");

	// Token: 0x04004462 RID: 17506
	private static readonly AssetReference VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5ExchangeA_02 = new AssetReference("VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5ExchangeA_02.prefab:1b775572c0180c247ac94c1babf3715d");

	// Token: 0x04004463 RID: 17507
	private static readonly AssetReference VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Idle_01 = new AssetReference("VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Idle_01.prefab:7662f42cb4f4daa44a188083e34320fd");

	// Token: 0x04004464 RID: 17508
	private static readonly AssetReference VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Intro_02 = new AssetReference("VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Intro_02.prefab:e7b0001f6438eaa43b8b7c8748838c08");

	// Token: 0x04004465 RID: 17509
	private static readonly AssetReference VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Loss_01 = new AssetReference("VO_Story_Hero_DeadlyAdventurer_Female_Human_Story_Guff_Mission5Loss_01.prefab:d910392968aa4114bb6e0f183319c2d2");

	// Token: 0x04004466 RID: 17510
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeB_01.prefab:6850324a4800c9a468f3d17525a33a69");

	// Token: 0x04004467 RID: 17511
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeC_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeC_02.prefab:3e9f6f61ac1932c4ba985ed19b0d3d56");

	// Token: 0x04004468 RID: 17512
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Dawngrasp_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Dawngrasp_02.prefab:d07b217ede4d8ee4590618e5015a2cb6");

	// Token: 0x04004469 RID: 17513
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Rokara_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Rokara_02.prefab:8514c8fab20231943a39e1e194928d89");

	// Token: 0x0400446A RID: 17514
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Tamsin_03 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5ExchangeD_Tamsin_03.prefab:d2d563c4bbb1b44488fd843ffcfd2b29");

	// Token: 0x0400446B RID: 17515
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5Intro_01.prefab:3fe16f028568e074e88088325c0abda6");

	// Token: 0x0400446C RID: 17516
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission5Victory_01.prefab:15f90f4d0a4b2ee459caf32a02261a0e");

	// Token: 0x0400446D RID: 17517
	private static readonly AssetReference VO_Story_Hero_RelentlessAdventurer_Female_Tauren_Story_Guff_Mission5Intro_02 = new AssetReference("VO_Story_Hero_RelentlessAdventurer_Female_Tauren_Story_Guff_Mission5Intro_02.prefab:dbc0e1fddd5fbcd479a7406fe2540346");

	// Token: 0x0400446E RID: 17518
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission5ExchangeC_Rokara_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission5ExchangeC_Rokara_01.prefab:a117a7676a570054abdfd05d46133095");

	// Token: 0x0400446F RID: 17519
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission5ExchangeD_Rokara_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission5ExchangeD_Rokara_01.prefab:6bb7b32ea9d989c488a5c97a85cbfbc8");

	// Token: 0x04004470 RID: 17520
	private static readonly AssetReference VO_Story_Hero_SneakyAdventurer_Female_Forsaken_Story_Guff_Mission5ExchangeB_03 = new AssetReference("VO_Story_Hero_SneakyAdventurer_Female_Forsaken_Story_Guff_Mission5ExchangeB_03.prefab:0ac9808ad16640547879f4e244cc864e");

	// Token: 0x04004471 RID: 17521
	private static readonly AssetReference VO_Story_Hero_SneakyAdventurer_Female_Forsaken_Story_Guff_Mission5Idle_02 = new AssetReference("VO_Story_Hero_SneakyAdventurer_Female_Forsaken_Story_Guff_Mission5Idle_02.prefab:6cc4a231a1d53fa4db333db49280f96f");

	// Token: 0x04004472 RID: 17522
	private static readonly AssetReference VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5ExchangeB_02 = new AssetReference("VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5ExchangeB_02.prefab:2ff567b9921b0d144af7867a9ea27a64");

	// Token: 0x04004473 RID: 17523
	private static readonly AssetReference VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5Idle_03 = new AssetReference("VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5Idle_03.prefab:878d237e6533b5a41bef3650a52cbaa7");

	// Token: 0x04004474 RID: 17524
	private static readonly AssetReference VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5Intro_02 = new AssetReference("VO_Story_Hero_SwiftAdventurer_Male_NightElf_Story_Guff_Mission5Intro_02.prefab:66e82c5f372347d4baca5d3daa2f9451");

	// Token: 0x04004475 RID: 17525
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeC_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeC_Tamsin_01.prefab:b2a114c27e4e47644a87cd68e851387c");

	// Token: 0x04004476 RID: 17526
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeD_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeD_Tamsin_01.prefab:dea96413edd03cc4b850b343a2c25fff");

	// Token: 0x04004477 RID: 17527
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeD_Tamsin_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission5ExchangeD_Tamsin_02.prefab:589cee937ce60b44eaf559a8f4d7b330");

	// Token: 0x04004478 RID: 17528
	private HashSet<string> m_playedLines = new HashSet<string>();
}
