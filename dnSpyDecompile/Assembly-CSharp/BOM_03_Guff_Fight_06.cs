using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000578 RID: 1400
public class BOM_03_Guff_Fight_06 : BOM_03_Guff_Dungeon
{
	// Token: 0x06004DDB RID: 19931 RVA: 0x0019BE0C File Offset: 0x0019A00C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Death_01,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6EmoteResponse_01,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeA_01,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeB_01,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeB_02,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeC_01,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeC_02,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeD_01,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6HeroPower_01,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Idle_01,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Idle_02,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Intro_02,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Loss_01,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Victory_01,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission6ExchangeD_Brukan_02,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission6ExchangeE_Brukan_01,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission6ExchangeD_Dawngrasp_02,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission6ExchangeE_Dawngrasp_01,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission6Intro_01,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission6ExchangeD_Rokara_02,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission6ExchangeE_Rokara_01,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission6ExchangeD_Tamsin_02,
			BOM_03_Guff_Fight_06.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission6ExchangeE_Tamsin_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004DDC RID: 19932 RVA: 0x0019A49C File Offset: 0x0019869C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
	}

	// Token: 0x06004DDD RID: 19933 RVA: 0x0019BFE0 File Offset: 0x0019A1E0
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 504)
		{
			if (missionEvent != 506)
			{
				switch (missionEvent)
				{
				case 510:
					yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6HeroPower_01);
					goto IL_2B2;
				case 514:
					yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_06.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission6Intro_01);
					yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Intro_02);
					goto IL_2B2;
				case 515:
					yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6EmoteResponse_01);
					goto IL_2B2;
				case 516:
					yield return base.MissionPlaySound(enemyActor, BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Death_01);
					goto IL_2B2;
				case 517:
					yield return base.MissionPlayVO(enemyActor, this.m_InGame_BossIdle);
					goto IL_2B2;
				}
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Loss_01);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Victory_01);
			GameState.Get().SetBusy(false);
		}
		IL_2B2:
		yield break;
	}

	// Token: 0x06004DDE RID: 19934 RVA: 0x0019BFF6 File Offset: 0x0019A1F6
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

	// Token: 0x06004DDF RID: 19935 RVA: 0x0019C00C File Offset: 0x0019A20C
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

	// Token: 0x06004DE0 RID: 19936 RVA: 0x0019C022 File Offset: 0x0019A222
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower().GetCard().GetActor();
		if (turn <= 7)
		{
			if (turn != 3)
			{
				if (turn == 7)
				{
					yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeB_01);
					yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeB_02);
				}
			}
			else
			{
				yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeA_01);
			}
		}
		else if (turn != 11)
		{
			if (turn != 15)
			{
				if (turn == 19)
				{
					if (this.HeroPowerBrukan)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_06.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission6ExchangeE_Brukan_01);
					}
					if (this.HeroPowerDawngrasp)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_06.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission6ExchangeE_Dawngrasp_01);
					}
					if (this.HeroPowerRokara)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_06.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission6ExchangeE_Rokara_01);
					}
					if (this.HeroPowerTamsin)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_06.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission6ExchangeE_Tamsin_01);
					}
				}
			}
			else
			{
				yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeD_01);
				if (this.HeroPowerBrukan)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_06.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission6ExchangeD_Brukan_02);
				}
				if (this.HeroPowerDawngrasp)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_06.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission6ExchangeD_Dawngrasp_02);
				}
				if (this.HeroPowerRokara)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_06.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission6ExchangeD_Rokara_02);
				}
				if (this.HeroPowerTamsin)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_06.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission6ExchangeD_Tamsin_02);
				}
			}
		}
		else
		{
			yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeC_01);
			yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeC_02);
		}
		yield break;
	}

	// Token: 0x04004479 RID: 17529
	private static readonly AssetReference VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Death_01 = new AssetReference("VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Death_01.prefab:c7b01c316c80c6a4c8d2938a5c81159c");

	// Token: 0x0400447A RID: 17530
	private static readonly AssetReference VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6EmoteResponse_01 = new AssetReference("VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6EmoteResponse_01.prefab:7e58f5bf25f72954f81e227ac51b7cf0");

	// Token: 0x0400447B RID: 17531
	private static readonly AssetReference VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeA_01 = new AssetReference("VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeA_01.prefab:4d5c53bf5b5f6a246aa00f2a1b6a5190");

	// Token: 0x0400447C RID: 17532
	private static readonly AssetReference VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeB_01 = new AssetReference("VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeB_01.prefab:60bdd8ed1d2fce643a46556fe20ffe07");

	// Token: 0x0400447D RID: 17533
	private static readonly AssetReference VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeB_02 = new AssetReference("VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeB_02.prefab:1350ad587f6b5cb4d9aec95e78003eb8");

	// Token: 0x0400447E RID: 17534
	private static readonly AssetReference VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeC_01 = new AssetReference("VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeC_01.prefab:cfbb53804f648a54ea764a33ff4ac769");

	// Token: 0x0400447F RID: 17535
	private static readonly AssetReference VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeC_02 = new AssetReference("VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeC_02.prefab:0c39b1de3ae27df46ba78d05c61824ef");

	// Token: 0x04004480 RID: 17536
	private static readonly AssetReference VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeD_01 = new AssetReference("VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6ExchangeD_01.prefab:94190167b4651564da952a7e481684f0");

	// Token: 0x04004481 RID: 17537
	private static readonly AssetReference VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6HeroPower_01 = new AssetReference("VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6HeroPower_01.prefab:75dc94ef2217bf743a84848508e60048");

	// Token: 0x04004482 RID: 17538
	private static readonly AssetReference VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Idle_01 = new AssetReference("VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Idle_01.prefab:53be7af3670453f44b3bacc948f22814");

	// Token: 0x04004483 RID: 17539
	private static readonly AssetReference VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Idle_02 = new AssetReference("VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Idle_02.prefab:8dbc28d9f58be3a47b36311534d7068b");

	// Token: 0x04004484 RID: 17540
	private static readonly AssetReference VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Intro_02 = new AssetReference("VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Intro_02.prefab:757ac176823ba3545bbcc84be3756a4e");

	// Token: 0x04004485 RID: 17541
	private static readonly AssetReference VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Loss_01 = new AssetReference("VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Loss_01.prefab:cd2f19d8dd10d3e4c9ecdd522ab6cc1d");

	// Token: 0x04004486 RID: 17542
	private static readonly AssetReference VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Victory_01 = new AssetReference("VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Victory_01.prefab:28270276c56053f4180b88411e9a2cfe");

	// Token: 0x04004487 RID: 17543
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission6ExchangeD_Brukan_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission6ExchangeD_Brukan_02.prefab:6fe17bb476aa09d4eb5dc1f350f13a53");

	// Token: 0x04004488 RID: 17544
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission6ExchangeE_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission6ExchangeE_Brukan_01.prefab:1e36ce2ca9d3edb43b2bf27fea634c34");

	// Token: 0x04004489 RID: 17545
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission6ExchangeD_Dawngrasp_02 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission6ExchangeD_Dawngrasp_02.prefab:145435b33a574e6409fe340596001524");

	// Token: 0x0400448A RID: 17546
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission6ExchangeE_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission6ExchangeE_Dawngrasp_01.prefab:84b1524d0976c564cb22d41961da6cd6");

	// Token: 0x0400448B RID: 17547
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission6Intro_01.prefab:8b30d639b829c05449276cdadbd506da");

	// Token: 0x0400448C RID: 17548
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission6ExchangeD_Rokara_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission6ExchangeD_Rokara_02.prefab:351dbf5ec39bb15469b03492e4b338b3");

	// Token: 0x0400448D RID: 17549
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission6ExchangeE_Rokara_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission6ExchangeE_Rokara_01.prefab:657f4989ad6b8034abf3b7922c398241");

	// Token: 0x0400448E RID: 17550
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission6ExchangeD_Tamsin_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission6ExchangeD_Tamsin_02.prefab:c4bad0adfb13aca45a3dcb201cbf61ea");

	// Token: 0x0400448F RID: 17551
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission6ExchangeE_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission6ExchangeE_Tamsin_01.prefab:386b737ab7275514086362e5a9cc2c90");

	// Token: 0x04004490 RID: 17552
	private List<string> m_InGame_BossIdle = new List<string>
	{
		BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Idle_01,
		BOM_03_Guff_Fight_06.VO_Story_Hero_Anacondra_Female_NightElf_Story_Guff_Mission6Idle_02
	};

	// Token: 0x04004491 RID: 17553
	private HashSet<string> m_playedLines = new HashSet<string>();
}
