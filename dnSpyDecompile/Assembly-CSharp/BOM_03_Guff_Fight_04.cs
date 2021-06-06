using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000576 RID: 1398
public class BOM_03_Guff_Fight_04 : BOM_03_Guff_Dungeon
{
	// Token: 0x06004DC5 RID: 19909 RVA: 0x0019B55C File Offset: 0x0019975C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_03_Guff_Fight_04.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission4ExchangeB_Brukan_02,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission4ExchangeC_Brukan_02,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4ExchangeB_Dawngrasp_02,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4ExchangeC_Dawngrasp_02,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4Victory_02,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeA_02,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeC_01,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeD_01,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4Intro_02,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4Victory_01,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission4ExchangeB_Rokara_02,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission4ExchangeC_Rokara_02,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Death_01,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4EmoteResponse_01,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeA_01,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeB_01,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeD_02,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_01,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_02,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_03,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Intro_01,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Loss_01,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission4ExchangeB_Tamsin_02,
			BOM_03_Guff_Fight_04.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission4ExchangeC_Tamsin_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004DC6 RID: 19910 RVA: 0x0019A49C File Offset: 0x0019869C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
	}

	// Token: 0x06004DC7 RID: 19911 RVA: 0x0019B740 File Offset: 0x00199940
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 505)
		{
			if (missionEvent != 506)
			{
				switch (missionEvent)
				{
				case 514:
					yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_04.VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Intro_01);
					yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_04.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4Intro_02);
					break;
				case 515:
					yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_04.VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4EmoteResponse_01);
					break;
				case 516:
					yield return base.MissionPlaySound(actor, BOM_03_Guff_Fight_04.VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Death_01);
					break;
				case 517:
					yield return base.MissionPlayVO(actor, this.m_InGame_BossIdle);
					break;
				default:
					yield return base.HandleMissionEventWithTiming(missionEvent);
					break;
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_04.VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Loss_01);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_04.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4Victory_01);
			yield return base.MissionPlayVO(this.Dawngrasp_BrassRing, BOM_03_Guff_Fight_04.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4Victory_02);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004DC8 RID: 19912 RVA: 0x0019B756 File Offset: 0x00199956
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

	// Token: 0x06004DC9 RID: 19913 RVA: 0x0019B76C File Offset: 0x0019996C
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

	// Token: 0x06004DCA RID: 19914 RVA: 0x0019B782 File Offset: 0x00199982
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower().GetCard().GetActor();
		if (turn <= 7)
		{
			if (turn != 3)
			{
				if (turn == 7)
				{
					yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_04.VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeB_01);
					if (this.HeroPowerBrukan)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_04.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission4ExchangeB_Brukan_02);
					}
					if (this.HeroPowerDawngrasp)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_04.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4ExchangeB_Dawngrasp_02);
					}
					if (this.HeroPowerRokara)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_04.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission4ExchangeB_Rokara_02);
					}
					if (this.HeroPowerTamsin)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_04.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission4ExchangeB_Tamsin_02);
					}
				}
			}
			else
			{
				yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_04.VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeA_01);
				yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_04.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeA_02);
			}
		}
		else if (turn != 11)
		{
			if (turn == 15)
			{
				yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_04.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeD_01);
				yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_04.VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeD_02);
			}
		}
		else
		{
			yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_04.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeC_01);
			if (this.HeroPowerBrukan)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_04.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission4ExchangeC_Brukan_02);
			}
			if (this.HeroPowerDawngrasp)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_04.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4ExchangeC_Dawngrasp_02);
			}
			if (this.HeroPowerRokara)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_04.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission4ExchangeC_Rokara_02);
			}
			if (this.HeroPowerTamsin)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_04.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission4ExchangeC_Tamsin_02);
			}
		}
		yield break;
	}

	// Token: 0x0400443F RID: 17471
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission4ExchangeB_Brukan_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission4ExchangeB_Brukan_02.prefab:7d3cf9a21724aa948b62b48b572ba88b");

	// Token: 0x04004440 RID: 17472
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission4ExchangeC_Brukan_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission4ExchangeC_Brukan_02.prefab:def8d9f6f395e5e4d8a2c34843728a78");

	// Token: 0x04004441 RID: 17473
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4ExchangeB_Dawngrasp_02 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4ExchangeB_Dawngrasp_02.prefab:0da619274f0e2684891dd026063c7f79");

	// Token: 0x04004442 RID: 17474
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4ExchangeC_Dawngrasp_02 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4ExchangeC_Dawngrasp_02.prefab:3d72e40fa089ad6458aeb0ec5d531974");

	// Token: 0x04004443 RID: 17475
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4Victory_02 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4Victory_02.prefab:19307310b4515794ea4a7f13064263c8");

	// Token: 0x04004444 RID: 17476
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeA_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeA_02.prefab:aab793abb316d8848bcd997362016b27");

	// Token: 0x04004445 RID: 17477
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeC_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeC_01.prefab:91acd060b11441a44959f9c467a8bd07");

	// Token: 0x04004446 RID: 17478
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeD_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeD_01.prefab:ad7bbdd8a25df0a41b51163d32d0110b");

	// Token: 0x04004447 RID: 17479
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4Intro_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4Intro_02.prefab:794ab97e567476d46be759acf63b8fd9");

	// Token: 0x04004448 RID: 17480
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4Victory_01.prefab:e71541bc846b88f4581504649bca485f");

	// Token: 0x04004449 RID: 17481
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission4ExchangeB_Rokara_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission4ExchangeB_Rokara_02.prefab:d41c0ca211022c149a9b130f85e0faeb");

	// Token: 0x0400444A RID: 17482
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission4ExchangeC_Rokara_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission4ExchangeC_Rokara_02.prefab:ba0c583da1489e74f85941e05f564420");

	// Token: 0x0400444B RID: 17483
	private static readonly AssetReference VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Death_01 = new AssetReference("VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Death_01.prefab:1ab153c50c04a3c45b081d3ad484820f");

	// Token: 0x0400444C RID: 17484
	private static readonly AssetReference VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4EmoteResponse_01 = new AssetReference("VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4EmoteResponse_01.prefab:625ff46aa64cd6c4eb559220474b866a");

	// Token: 0x0400444D RID: 17485
	private static readonly AssetReference VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeA_01 = new AssetReference("VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeA_01.prefab:b5b1e59ae67bfac49a6557588b548765");

	// Token: 0x0400444E RID: 17486
	private static readonly AssetReference VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeB_01 = new AssetReference("VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeB_01.prefab:a82e0e7901fe42d4f8ffba8e3d07956c");

	// Token: 0x0400444F RID: 17487
	private static readonly AssetReference VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeD_02 = new AssetReference("VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeD_02.prefab:270f9df0e34e0124cb2ba893b4ce46ad");

	// Token: 0x04004450 RID: 17488
	private static readonly AssetReference VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_01 = new AssetReference("VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_01.prefab:a036a322a57967543b9cbcfc9f5028b7");

	// Token: 0x04004451 RID: 17489
	private static readonly AssetReference VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_02 = new AssetReference("VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_02.prefab:f8bcaeb4c3f950c4eb709ff091e5934c");

	// Token: 0x04004452 RID: 17490
	private static readonly AssetReference VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_03 = new AssetReference("VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_03.prefab:50f907e92a6bc9a4db8d62d1623f2a89");

	// Token: 0x04004453 RID: 17491
	private static readonly AssetReference VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Intro_01 = new AssetReference("VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Intro_01.prefab:4732afbaef78b8d48a718cdc9160daa0");

	// Token: 0x04004454 RID: 17492
	private static readonly AssetReference VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Loss_01 = new AssetReference("VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Loss_01.prefab:b8bf472ed310b254e988e33cdb12ef79");

	// Token: 0x04004455 RID: 17493
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission4ExchangeB_Tamsin_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission4ExchangeB_Tamsin_02.prefab:ae112cacad1a3974cb02568c72fa0b43");

	// Token: 0x04004456 RID: 17494
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission4ExchangeC_Tamsin_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission4ExchangeC_Tamsin_02.prefab:60d6ffc7d09ef4b47be1342ac2db5fdd");

	// Token: 0x04004457 RID: 17495
	private List<string> m_InGame_BossIdle = new List<string>
	{
		BOM_03_Guff_Fight_04.VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_01,
		BOM_03_Guff_Fight_04.VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_02,
		BOM_03_Guff_Fight_04.VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_03
	};

	// Token: 0x04004458 RID: 17496
	private HashSet<string> m_playedLines = new HashSet<string>();
}
