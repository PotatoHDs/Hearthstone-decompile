using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000579 RID: 1401
public class BOM_03_Guff_Fight_07 : BOM_03_Guff_Dungeon
{
	// Token: 0x06004DE6 RID: 19942 RVA: 0x0019C1E0 File Offset: 0x0019A3E0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_03_Guff_Fight_07.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission7ExchangeA_01,
			BOM_03_Guff_Fight_07.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission7ExchangeC_Brukan_01,
			BOM_03_Guff_Fight_07.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission7ExchangeC_Dawngrasp_01,
			BOM_03_Guff_Fight_07.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission7ExchangeB_02,
			BOM_03_Guff_Fight_07.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission7ExchangeD_01,
			BOM_03_Guff_Fight_07.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission7Intro_02,
			BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Death_01,
			BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7EmoteResponse_01,
			BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7ExchangeB_01,
			BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7HeroPower_01,
			BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7HeroPower_01_alt,
			BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7HeroPower_02,
			BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7HeroPower_03,
			BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Idle_01,
			BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Idle_02,
			BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Intro_01,
			BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Loss_01,
			BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Victory_01,
			BOM_03_Guff_Fight_07.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission7ExchangeC_Rokara_01,
			BOM_03_Guff_Fight_07.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission7ExchangeA_02,
			BOM_03_Guff_Fight_07.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission7ExchangeC_Tamsin_01,
			BOM_03_Guff_Fight_07.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission7Victory_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004DE7 RID: 19943 RVA: 0x0019A49C File Offset: 0x0019869C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
	}

	// Token: 0x06004DE8 RID: 19944 RVA: 0x0019C3A4 File Offset: 0x0019A5A4
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 505:
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(this.Naralex_BrassRing, BOM_03_Guff_Fight_07.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission7Victory_02);
			GameState.Get().SetBusy(false);
			goto IL_293;
		case 506:
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Loss_01);
			GameState.Get().SetBusy(false);
			goto IL_293;
		case 510:
			yield return base.MissionPlayVO(actor, this.m_InGame_BossUsesHeroPower);
			goto IL_293;
		case 514:
			yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Intro_01);
			yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_07.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission7Intro_02);
			goto IL_293;
		case 515:
			yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7EmoteResponse_01);
			goto IL_293;
		case 516:
			yield return base.MissionPlaySound(actor, BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Victory_01);
			goto IL_293;
		case 517:
			yield return base.MissionPlayVO(actor, this.m_InGame_BossIdle);
			goto IL_293;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_293:
		yield break;
	}

	// Token: 0x06004DE9 RID: 19945 RVA: 0x0019C3BA File Offset: 0x0019A5BA
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

	// Token: 0x06004DEA RID: 19946 RVA: 0x0019C3D0 File Offset: 0x0019A5D0
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

	// Token: 0x06004DEB RID: 19947 RVA: 0x0019C3E6 File Offset: 0x0019A5E6
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower().GetCard().GetActor();
		if (turn <= 5)
		{
			if (turn != 3)
			{
				if (turn == 5)
				{
					yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7ExchangeB_01);
					yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_07.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission7ExchangeB_02);
				}
			}
			else
			{
				if (this.HeroPowerBrukan)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_07.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission7ExchangeA_01);
				}
				else
				{
					yield return base.MissionPlayVO(this.Brukan_BrassRing, BOM_03_Guff_Fight_07.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission7ExchangeA_01);
				}
				if (this.HeroPowerTamsin)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_07.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission7ExchangeA_02);
				}
				else
				{
					yield return base.MissionPlayVO(this.Tamsin_BrassRing, BOM_03_Guff_Fight_07.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission7ExchangeA_02);
				}
			}
		}
		else if (turn != 9)
		{
			if (turn == 13)
			{
				yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_07.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission7ExchangeD_01);
			}
		}
		else
		{
			if (this.HeroPowerBrukan)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_07.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission7ExchangeC_Brukan_01);
			}
			if (this.HeroPowerDawngrasp)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_07.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission7ExchangeC_Dawngrasp_01);
			}
			if (this.HeroPowerRokara)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_07.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission7ExchangeC_Rokara_01);
			}
			if (this.HeroPowerTamsin)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_03_Guff_Fight_07.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission7ExchangeC_Tamsin_01);
			}
		}
		yield break;
	}

	// Token: 0x04004492 RID: 17554
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission7ExchangeA_01.prefab:84b1d82b68cb74347a92c87ab78a04e9");

	// Token: 0x04004493 RID: 17555
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission7ExchangeC_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission7ExchangeC_Brukan_01.prefab:327b88880551431488a8fdec11b70644");

	// Token: 0x04004494 RID: 17556
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission7ExchangeC_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission7ExchangeC_Dawngrasp_01.prefab:fc9e9a61bd8f1a54a88283dd1efc16dc");

	// Token: 0x04004495 RID: 17557
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission7ExchangeB_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission7ExchangeB_02.prefab:eebd57b5c56b88642a147443465f68e6");

	// Token: 0x04004496 RID: 17558
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission7ExchangeD_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission7ExchangeD_01.prefab:013a0e275a8a4f142be585e2241fc95f");

	// Token: 0x04004497 RID: 17559
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission7Intro_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission7Intro_02.prefab:91d50ca8166d4114dad5b2e6dd22b4b0");

	// Token: 0x04004498 RID: 17560
	private static readonly AssetReference VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Death_01 = new AssetReference("VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Death_01.prefab:0cb325af761c01d4cb83b889b33fb061");

	// Token: 0x04004499 RID: 17561
	private static readonly AssetReference VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7EmoteResponse_01 = new AssetReference("VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7EmoteResponse_01.prefab:008192737c3582e48bb0dae0d4b05382");

	// Token: 0x0400449A RID: 17562
	private static readonly AssetReference VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7ExchangeB_01.prefab:6827d45dba179b0429d0b2007f0ce413");

	// Token: 0x0400449B RID: 17563
	private static readonly AssetReference VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7HeroPower_01 = new AssetReference("VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7HeroPower_01.prefab:30d7f99ebb85b864497dd1c49e7d99f9");

	// Token: 0x0400449C RID: 17564
	private static readonly AssetReference VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7HeroPower_01_alt = new AssetReference("VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7HeroPower_01_alt.prefab:d60b207145209084cae996be491364cf");

	// Token: 0x0400449D RID: 17565
	private static readonly AssetReference VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7HeroPower_02 = new AssetReference("VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7HeroPower_02.prefab:7a1b0b9daecef2444b01823a093f4906");

	// Token: 0x0400449E RID: 17566
	private static readonly AssetReference VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7HeroPower_03 = new AssetReference("VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7HeroPower_03.prefab:a7518ede0264f5446a3f9cf932d30ec9");

	// Token: 0x0400449F RID: 17567
	private static readonly AssetReference VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Idle_01 = new AssetReference("VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Idle_01.prefab:5b84c39a670647d4fa7ca8d2939a715b");

	// Token: 0x040044A0 RID: 17568
	private static readonly AssetReference VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Idle_02 = new AssetReference("VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Idle_02.prefab:c9b6ee59079b43842ad12454ad628272");

	// Token: 0x040044A1 RID: 17569
	private static readonly AssetReference VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Intro_01.prefab:39f9312ff0f9430408cf7a3a398a4bae");

	// Token: 0x040044A2 RID: 17570
	private static readonly AssetReference VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Loss_01 = new AssetReference("VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Loss_01.prefab:234cb7e606744ff4e9cb2cc359314d6a");

	// Token: 0x040044A3 RID: 17571
	private static readonly AssetReference VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Victory_01 = new AssetReference("VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Victory_01.prefab:9794e17d6e2206542850dc397b9ff535");

	// Token: 0x040044A4 RID: 17572
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission7ExchangeC_Rokara_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission7ExchangeC_Rokara_01.prefab:ba6685e9c9ad1a9459630660819e58cb");

	// Token: 0x040044A5 RID: 17573
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission7ExchangeA_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission7ExchangeA_02.prefab:f39f0766e1b9565469a60a353f281fe6");

	// Token: 0x040044A6 RID: 17574
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission7ExchangeC_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission7ExchangeC_Tamsin_01.prefab:f7d8b0dcf1d601f44a1689796c962cde");

	// Token: 0x040044A7 RID: 17575
	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission7Victory_02 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission7Victory_02.prefab:060bcede5ee29734c8ebb88a7e3ccd5f");

	// Token: 0x040044A8 RID: 17576
	private List<string> m_InGame_BossIdle = new List<string>
	{
		BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Idle_01,
		BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7Idle_02
	};

	// Token: 0x040044A9 RID: 17577
	private List<string> m_InGame_BossUsesHeroPower = new List<string>
	{
		BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7HeroPower_01,
		BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7HeroPower_01_alt,
		BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7HeroPower_02,
		BOM_03_Guff_Fight_07.VO_Story_Hero_Mutanus_Male_Murloc_Story_Guff_Mission7HeroPower_03
	};

	// Token: 0x040044AA RID: 17578
	private HashSet<string> m_playedLines = new HashSet<string>();
}
