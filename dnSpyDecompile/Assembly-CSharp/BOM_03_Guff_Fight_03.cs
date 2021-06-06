using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000575 RID: 1397
public class BOM_03_Guff_Fight_03 : BOM_03_Guff_Dungeon
{
	// Token: 0x06004DBA RID: 19898 RVA: 0x0019AFC0 File Offset: 0x001991C0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Death_01,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3EmoteResponse_01,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeA_01,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeC_01,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeD_01,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeE_01,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_01,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_02,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_03,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_01,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_02,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_03,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Intro_01,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Loss_01,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Victory_01,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeB_Brukan_01,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeE_Brukan_02,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeF_Brukan_01,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeB_Dawngrasp_01,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeE_Dawngrasp_02,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeF_Dawngrasp_01,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeA_02,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeB_02,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeC_02,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeD_02,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeF_02,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Intro_02,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Victory_02,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Victory_03,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeB_Rokara_01,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeE_Rokara_02,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeF_Rokara_01,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeB_Tamsin_01,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeE_Tamsin_02,
			BOM_03_Guff_Fight_03.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeF_Tamsin_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004DBB RID: 19899 RVA: 0x0019A49C File Offset: 0x0019869C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
	}

	// Token: 0x06004DBC RID: 19900 RVA: 0x0019B254 File Offset: 0x00199454
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower().GetCard().GetActor();
		if (missionEvent != 100)
		{
			switch (missionEvent)
			{
			case 504:
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Victory_01);
				GameState.Get().SetBusy(false);
				goto IL_46C;
			case 505:
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_03.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Victory_02);
				yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_03.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Victory_03);
				GameState.Get().SetBusy(false);
				goto IL_46C;
			case 506:
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Loss_01);
				GameState.Get().SetBusy(false);
				goto IL_46C;
			case 510:
				yield return base.MissionPlayVO(actor, this.m_InGame_BossUsesHeroPower);
				goto IL_46C;
			case 514:
				yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Intro_01);
				yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_03.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Intro_02);
				goto IL_46C;
			case 515:
				yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3EmoteResponse_01);
				goto IL_46C;
			case 516:
				yield return base.MissionPlaySound(actor, BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Death_01);
				goto IL_46C;
			case 517:
				yield return base.MissionPlayVO(actor, this.m_InGame_BossIdle);
				goto IL_46C;
			}
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		else
		{
			GameState.Get().SetBusy(true);
			if (this.HeroPowerBrukan)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_03.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeB_Brukan_01);
			}
			if (this.HeroPowerDawngrasp)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_03.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeB_Dawngrasp_01);
			}
			if (this.HeroPowerRokara)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_03.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeB_Rokara_01);
			}
			if (this.HeroPowerTamsin)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_03.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeB_Tamsin_01);
			}
			yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_03.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeB_02);
			GameState.Get().SetBusy(false);
		}
		IL_46C:
		yield break;
	}

	// Token: 0x06004DBD RID: 19901 RVA: 0x0019B26A File Offset: 0x0019946A
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

	// Token: 0x06004DBE RID: 19902 RVA: 0x0019B280 File Offset: 0x00199480
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

	// Token: 0x06004DBF RID: 19903 RVA: 0x0019B296 File Offset: 0x00199496
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower().GetCard().GetActor();
		switch (turn)
		{
		case 5:
			yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeA_01);
			yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_03.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeA_02);
			break;
		case 6:
		case 8:
		case 10:
			break;
		case 7:
			yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeC_01);
			yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_03.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeC_02);
			break;
		case 9:
			yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeD_01);
			yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_03.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeD_02);
			break;
		case 11:
			yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeE_01);
			if (this.HeroPowerBrukan)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_03.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeE_Brukan_02);
			}
			if (this.HeroPowerDawngrasp)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_03.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeE_Dawngrasp_02);
			}
			if (this.HeroPowerRokara)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_03.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeE_Rokara_02);
			}
			if (this.HeroPowerTamsin)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_03.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeE_Tamsin_02);
			}
			break;
		default:
			if (turn == 15)
			{
				if (this.HeroPowerBrukan)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_03.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeF_Brukan_01);
				}
				if (this.HeroPowerDawngrasp)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_03.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeF_Dawngrasp_01);
				}
				if (this.HeroPowerRokara)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_03.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeF_Rokara_01);
				}
				if (this.HeroPowerTamsin)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_03.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeF_Tamsin_01);
				}
				yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_03.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeF_02);
			}
			break;
		}
		yield break;
	}

	// Token: 0x04004419 RID: 17433
	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Death_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Death_01.prefab:cee9af5f93604f543ae52d62c22b7a25");

	// Token: 0x0400441A RID: 17434
	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3EmoteResponse_01.prefab:32fb8d759269a53448e1f4c340114377");

	// Token: 0x0400441B RID: 17435
	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeA_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeA_01.prefab:6f38b4201afb1564997c88ea4f83ce4a");

	// Token: 0x0400441C RID: 17436
	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeC_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeC_01.prefab:10e399729caffd44a8de0e0abbd3b91b");

	// Token: 0x0400441D RID: 17437
	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeD_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeD_01.prefab:a37d62b84f87f3b47a8dc75cd5fac52d");

	// Token: 0x0400441E RID: 17438
	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeE_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeE_01.prefab:00acdc842b6f21245acd3f69b6dcb69a");

	// Token: 0x0400441F RID: 17439
	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_01.prefab:d6db2227c9dcb7345a05de10aea7b262");

	// Token: 0x04004420 RID: 17440
	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_02 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_02.prefab:568e12955b775ba48b69f4dee0bd1c53");

	// Token: 0x04004421 RID: 17441
	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_03 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_03.prefab:708e426e1a5f7bc48b90d0dd6ba20a79");

	// Token: 0x04004422 RID: 17442
	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_01.prefab:2966bd6c1b4df3e4da8af264c7fa9b3c");

	// Token: 0x04004423 RID: 17443
	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_02 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_02.prefab:417d4a0429e188d43b084fcf6337def6");

	// Token: 0x04004424 RID: 17444
	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_03 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_03.prefab:107f51e08e2990a4aacbcda196c57ed3");

	// Token: 0x04004425 RID: 17445
	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Intro_01.prefab:8ce19f1faf9bcd4428b548c730ce9df7");

	// Token: 0x04004426 RID: 17446
	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Loss_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Loss_01.prefab:74097b99d6a97b34fb2aa5921fc44b9d");

	// Token: 0x04004427 RID: 17447
	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Victory_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Victory_01.prefab:a3b75749d72463a4ab2e9a28e9ea3c90");

	// Token: 0x04004428 RID: 17448
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeB_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeB_Brukan_01.prefab:c8fb9c3f4ad13394c947128840ddcdff");

	// Token: 0x04004429 RID: 17449
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeE_Brukan_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeE_Brukan_02.prefab:ab066736eb5110f4fb3dbca7a2978702");

	// Token: 0x0400442A RID: 17450
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeF_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeF_Brukan_01.prefab:8f5f8ab142f5af849a8cb3cf4c6de376");

	// Token: 0x0400442B RID: 17451
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeB_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeB_Dawngrasp_01.prefab:df598c66148541e438799120c962ee15");

	// Token: 0x0400442C RID: 17452
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeE_Dawngrasp_02 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeE_Dawngrasp_02.prefab:764961b1eea3dd742aaeb91350500d7b");

	// Token: 0x0400442D RID: 17453
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeF_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeF_Dawngrasp_01.prefab:e90b2b90f9760d743a5ad217a167d43c");

	// Token: 0x0400442E RID: 17454
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeA_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeA_02.prefab:29323b534b4df5d429b9fb8598d3ca4b");

	// Token: 0x0400442F RID: 17455
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeB_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeB_02.prefab:0a906a22de54fa04ea82aad5d263dad0");

	// Token: 0x04004430 RID: 17456
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeC_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeC_02.prefab:5e03a6199b31406439dafbff248532bc");

	// Token: 0x04004431 RID: 17457
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeD_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeD_02.prefab:a20adbfd29782e049bee7e3ef76be2b1");

	// Token: 0x04004432 RID: 17458
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeF_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeF_02.prefab:64a3d3b664750074ca89bc09a1920d4a");

	// Token: 0x04004433 RID: 17459
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Intro_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Intro_02.prefab:bad5f7eccc3015d4d890c6d5e5f38271");

	// Token: 0x04004434 RID: 17460
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Victory_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Victory_02.prefab:9068e16b9b4ef01458b4e8f3d7a373a9");

	// Token: 0x04004435 RID: 17461
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Victory_03 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Victory_03.prefab:5af3cccce52c3e3409f2400016fe2ece");

	// Token: 0x04004436 RID: 17462
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeB_Rokara_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeB_Rokara_01.prefab:b733e70b33b50ef4bb0ac21c34816314");

	// Token: 0x04004437 RID: 17463
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeE_Rokara_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeE_Rokara_02.prefab:5281b55d11c7b554ab4d0b7511154c90");

	// Token: 0x04004438 RID: 17464
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeF_Rokara_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeF_Rokara_01.prefab:a3821aa1d808dec438c6b547c74ec47a");

	// Token: 0x04004439 RID: 17465
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeB_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeB_Tamsin_01.prefab:fd364d8bf64045e4799b889a1238502b");

	// Token: 0x0400443A RID: 17466
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeE_Tamsin_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeE_Tamsin_02.prefab:81f792a5b47374b46881492f8b316f46");

	// Token: 0x0400443B RID: 17467
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeF_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeF_Tamsin_01.prefab:3deb367ee4af2e242859e9dab2cc7e8a");

	// Token: 0x0400443C RID: 17468
	private List<string> m_InGame_BossIdle = new List<string>
	{
		BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_01,
		BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_02,
		BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_03
	};

	// Token: 0x0400443D RID: 17469
	private List<string> m_InGame_BossUsesHeroPower = new List<string>
	{
		BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_01,
		BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_02,
		BOM_03_Guff_Fight_03.VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_03
	};

	// Token: 0x0400443E RID: 17470
	private HashSet<string> m_playedLines = new HashSet<string>();
}
