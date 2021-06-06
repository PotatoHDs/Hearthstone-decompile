using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000561 RID: 1377
public class BOM_01_Rokara_04 : BoM_01_Rokara_Dungeon
{
	// Token: 0x06004C49 RID: 19529 RVA: 0x001936F4 File Offset: 0x001918F4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_01_Rokara_04.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4ExchangeA_Brukan_01,
			BOM_01_Rokara_04.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4ExchangeB_Brukan_01,
			BOM_01_Rokara_04.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4ExchangeC_Brukan_01,
			BOM_01_Rokara_04.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4Victory_01,
			BOM_01_Rokara_04.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4Victory_02,
			BOM_01_Rokara_04.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission4ExchangeC_01,
			BOM_01_Rokara_04.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission4ExchangeD_01,
			BOM_01_Rokara_04.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission4ExchangeD_02,
			BOM_01_Rokara_04.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission4ExchangeA_Guff_01,
			BOM_01_Rokara_04.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission4ExchangeB_Guff_01,
			BOM_01_Rokara_04.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission4ExchangeC_Guff_01,
			BOM_01_Rokara_04.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4ExchangeA_01,
			BOM_01_Rokara_04.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4ExchangeA_Brukan_02,
			BOM_01_Rokara_04.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4ExchangeA_Guff_02,
			BOM_01_Rokara_04.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4ExchangeA_Tamsin_02,
			BOM_01_Rokara_04.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4ExchangeD_03,
			BOM_01_Rokara_04.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4Intro_01,
			BOM_01_Rokara_04.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4ExchangeA_Tamsin_01,
			BOM_01_Rokara_04.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4ExchangeB_Tamsin_01,
			BOM_01_Rokara_04.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4ExchangeC_Tamsin_01,
			BOM_01_Rokara_04.UNG_010_SatedThreshadon_Attack,
			BOM_01_Rokara_04.UNG_010_SatedThreshadon_Death,
			BOM_01_Rokara_04.UNG_010_SatedThreshadon_Play
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004C4A RID: 19530 RVA: 0x001938C8 File Offset: 0x00191AC8
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
		this.m_deathLine = BOM_01_Rokara_04.UNG_010_SatedThreshadon_Death;
		this.m_standardEmoteResponseLine = BOM_01_Rokara_04.UNG_010_SatedThreshadon_Attack;
	}

	// Token: 0x06004C4B RID: 19531 RVA: 0x001938FB File Offset: 0x00191AFB
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower().GetCard().GetActor();
		if (missionEvent <= 504)
		{
			if (missionEvent != 100)
			{
				if (missionEvent == 101)
				{
					yield return base.MissionPlayVO("BOM_01_Dawngrasp_04t", this.Dawngrasp_BrassRing, BOM_01_Rokara_04.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission4ExchangeD_01);
					yield return base.MissionPlayVO("BOM_01_Dawngrasp_04t", this.Dawngrasp_BrassRing, BOM_01_Rokara_04.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission4ExchangeD_02);
					yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_04.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4ExchangeD_03);
					goto IL_452;
				}
				if (missionEvent == 504)
				{
					GameState.Get().SetBusy(true);
					if (this.HeroPowerIsBrukan)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_04.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4Victory_01);
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_04.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4Victory_02);
					}
					else
					{
						yield return base.MissionPlayVO(this.Brukan_BrassRing, BOM_01_Rokara_04.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4Victory_01);
						yield return base.MissionPlayVO(this.Brukan_BrassRing, BOM_01_Rokara_04.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4Victory_02);
					}
					GameState.Get().SetBusy(false);
					goto IL_452;
				}
			}
			else
			{
				yield return base.MissionPlayVO("BOM_01_Dawngrasp_04t", this.Dawngrasp_BrassRing, BOM_01_Rokara_04.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission4ExchangeC_01);
				if (this.HeroPowerIsBrukan)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_04.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4ExchangeC_Brukan_01);
				}
				if (this.HeroPowerIsGuff)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_04.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission4ExchangeC_Guff_01);
				}
				if (this.HeroPowerIsTamsin)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_04.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4ExchangeC_Tamsin_01);
					goto IL_452;
				}
				goto IL_452;
			}
		}
		else
		{
			if (missionEvent == 506)
			{
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_04.UNG_010_SatedThreshadon_Attack);
				goto IL_452;
			}
			if (missionEvent == 514)
			{
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_04.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4Intro_01);
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_04.UNG_010_SatedThreshadon_Play);
				goto IL_452;
			}
			if (missionEvent == 515)
			{
				yield return base.MissionPlayVO(enemyActor, this.m_standardEmoteResponseLine);
				goto IL_452;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_452:
		yield break;
	}

	// Token: 0x06004C4C RID: 19532 RVA: 0x00193911 File Offset: 0x00191B11
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
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

	// Token: 0x06004C4D RID: 19533 RVA: 0x00193927 File Offset: 0x00191B27
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

	// Token: 0x06004C4E RID: 19534 RVA: 0x0019393D File Offset: 0x00191B3D
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower().GetCard().GetActor();
		if (turn != 3)
		{
			if (turn == 7)
			{
				if (this.HeroPowerIsBrukan)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_04.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4ExchangeB_Brukan_01);
				}
				if (this.HeroPowerIsGuff)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_04.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission4ExchangeB_Guff_01);
				}
				if (this.HeroPowerIsTamsin)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_04.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4ExchangeB_Tamsin_01);
				}
			}
		}
		else
		{
			yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_04.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4ExchangeA_01);
			if (this.HeroPowerIsBrukan)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_04.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4ExchangeA_Brukan_01);
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_04.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4ExchangeA_Brukan_02);
			}
			if (this.HeroPowerIsGuff)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_04.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission4ExchangeA_Guff_01);
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_04.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4ExchangeA_Guff_02);
			}
			if (this.HeroPowerIsTamsin)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_04.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4ExchangeA_Tamsin_01);
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_04.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4ExchangeA_Tamsin_02);
			}
		}
		yield break;
	}

	// Token: 0x04004118 RID: 16664
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4ExchangeA_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4ExchangeA_Brukan_01.prefab:805a8c8f198a5c547adbc59d31cf459e");

	// Token: 0x04004119 RID: 16665
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4ExchangeB_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4ExchangeB_Brukan_01.prefab:7b900d515f4a2654f89f00123dc828e2");

	// Token: 0x0400411A RID: 16666
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4ExchangeC_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4ExchangeC_Brukan_01.prefab:d31cf1ee39e8c564cb216fa3d0121a47");

	// Token: 0x0400411B RID: 16667
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4Victory_01.prefab:c23831d5ddd01854299b41564c24f061");

	// Token: 0x0400411C RID: 16668
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4Victory_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4Victory_02.prefab:5fc92da67f8fd08498c75ce0b3e687d2");

	// Token: 0x0400411D RID: 16669
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission4ExchangeC_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission4ExchangeC_01.prefab:e92fbf563f6516749a0f736ee0ea4cec");

	// Token: 0x0400411E RID: 16670
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission4ExchangeD_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission4ExchangeD_01.prefab:2b4a1f10238bc1b4594e0dbcb7fa3245");

	// Token: 0x0400411F RID: 16671
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission4ExchangeD_02 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission4ExchangeD_02.prefab:a9c0db212d4457d42a3cfb7537f9ab16");

	// Token: 0x04004120 RID: 16672
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission4ExchangeA_Guff_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission4ExchangeA_Guff_01.prefab:a1f0a09f4a57d6447a13eb34a5444da0");

	// Token: 0x04004121 RID: 16673
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission4ExchangeB_Guff_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission4ExchangeB_Guff_01.prefab:f89c7961b989d904f958453d6531b12b");

	// Token: 0x04004122 RID: 16674
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission4ExchangeC_Guff_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission4ExchangeC_Guff_01.prefab:c35f1844a741a0948be6e1be14482039");

	// Token: 0x04004123 RID: 16675
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4ExchangeA_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4ExchangeA_01.prefab:fbb0d510b459b5b4f97b696fe6099218");

	// Token: 0x04004124 RID: 16676
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4ExchangeA_Brukan_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4ExchangeA_Brukan_02.prefab:4b6ef583a84bfb84dbbd3c82adb6f78d");

	// Token: 0x04004125 RID: 16677
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4ExchangeA_Guff_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4ExchangeA_Guff_02.prefab:e3990233feb46034c8c7ea28d4362c4a");

	// Token: 0x04004126 RID: 16678
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4ExchangeA_Tamsin_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4ExchangeA_Tamsin_02.prefab:9aa7efe0d9bdeb3418d4bba6fb8b39d9");

	// Token: 0x04004127 RID: 16679
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4ExchangeD_03 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4ExchangeD_03.prefab:a3c58fdd8d4c16542a1f31373f640e79");

	// Token: 0x04004128 RID: 16680
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4Intro_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission4Intro_01.prefab:7cd3ba0bb58ed054ebdb10596c445735");

	// Token: 0x04004129 RID: 16681
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4ExchangeA_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4ExchangeA_Tamsin_01.prefab:a62626585e2215f4b81087a0dc1cf9c9");

	// Token: 0x0400412A RID: 16682
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4ExchangeB_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4ExchangeB_Tamsin_01.prefab:73effbfabf8c6e44eb1f36aa08136aba");

	// Token: 0x0400412B RID: 16683
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4ExchangeC_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission4ExchangeC_Tamsin_01.prefab:cfaee2253ea461e4e893a00001c0a6d3");

	// Token: 0x0400412C RID: 16684
	private static readonly AssetReference UNG_010_SatedThreshadon_Attack = new AssetReference("UNG_010_SatedThreshadon_Attack.prefab:4debdb75af9a8374abb65325b7a6602c");

	// Token: 0x0400412D RID: 16685
	private static readonly AssetReference UNG_010_SatedThreshadon_Death = new AssetReference("UNG_010_SatedThreshadon_Death.prefab:d56a39571f3426e4daabaf0138df21a0");

	// Token: 0x0400412E RID: 16686
	private static readonly AssetReference UNG_010_SatedThreshadon_Play = new AssetReference("UNG_010_SatedThreshadon_Play.prefab:b24069ee423dca6468dc095fd1a47e9a");

	// Token: 0x0400412F RID: 16687
	private List<string> m_missionEventTriggerInGame_VictoryPostExplosionLines = new List<string>
	{
		BOM_01_Rokara_04.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4Victory_01,
		BOM_01_Rokara_04.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission4Victory_02
	};

	// Token: 0x04004130 RID: 16688
	private List<string> m_VO_Story_Hero_Dawngrasp_NB_BloodElf_Story_Rokara_Mission4ExchangeDLines = new List<string>
	{
		BOM_01_Rokara_04.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission4ExchangeD_01,
		BOM_01_Rokara_04.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission4ExchangeD_02
	};

	// Token: 0x04004131 RID: 16689
	private HashSet<string> m_playedLines = new HashSet<string>();
}
