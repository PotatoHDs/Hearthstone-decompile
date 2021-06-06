using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000562 RID: 1378
public class BOM_01_Rokara_05 : BoM_01_Rokara_Dungeon
{
	// Token: 0x06004C54 RID: 19540 RVA: 0x00193B30 File Offset: 0x00191D30
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_01_Rokara_05.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeA_Brukan_01,
			BOM_01_Rokara_05.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeC_Brukan_01,
			BOM_01_Rokara_05.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeD_Brukan_01,
			BOM_01_Rokara_05.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeA_Dawngrasp_01,
			BOM_01_Rokara_05.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeC_Dawngrasp_01,
			BOM_01_Rokara_05.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeD_Dawngrasp_01,
			BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5EmoteResponse_01,
			BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5ExchangeB_01,
			BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5ExchangeE_01,
			BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_01,
			BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_02,
			BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_03,
			BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Idle_01,
			BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Idle_02,
			BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Intro_02,
			BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Loss_01,
			BOM_01_Rokara_05.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeA_Guff_01,
			BOM_01_Rokara_05.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeC_Guff_01,
			BOM_01_Rokara_05.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeD_Guff_01,
			BOM_01_Rokara_05.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeA_01,
			BOM_01_Rokara_05.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeC_Brukan_02,
			BOM_01_Rokara_05.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeC_Guff_02,
			BOM_01_Rokara_05.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeD_Brukan_02,
			BOM_01_Rokara_05.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeD_Tamsin_01,
			BOM_01_Rokara_05.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Intro_01,
			BOM_01_Rokara_05.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Victory_01,
			BOM_01_Rokara_05.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5Victory_02,
			BOM_01_Rokara_05.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5Victory_03,
			BOM_01_Rokara_05.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Victory_04,
			BOM_01_Rokara_05.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeA_Tamsin_01,
			BOM_01_Rokara_05.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_Tamsin_01,
			BOM_01_Rokara_05.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_Tamsin_02,
			BOM_01_Rokara_05.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeD_Tamsin_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004C55 RID: 19541 RVA: 0x00193DA4 File Offset: 0x00191FA4
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines2;
	}

	// Token: 0x06004C56 RID: 19542 RVA: 0x00193DAC File Offset: 0x00191FAC
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004C57 RID: 19543 RVA: 0x00193DB4 File Offset: 0x00191FB4
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BT_FinalBoss;
		this.m_standardEmoteResponseLine = BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5EmoteResponse_01;
	}

	// Token: 0x06004C58 RID: 19544 RVA: 0x00193DD7 File Offset: 0x00191FD7
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower().GetCard().GetActor();
		if (missionEvent <= 505)
		{
			if (missionEvent == 100)
			{
				GameState.Get().SetBusy(true);
				if (this.HeroPowerIsBrukan)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_05.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeC_Brukan_01);
					yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_05.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeC_Brukan_02);
				}
				if (this.HeroPowerIsGuff)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_05.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeC_Guff_01);
					yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_05.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeC_Guff_02);
				}
				if (this.HeroPowerIsTamsin)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_05.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_Tamsin_01);
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_05.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_Tamsin_02);
				}
				if (this.HeroPowerIsDawngrasp)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_05.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeC_Dawngrasp_01);
				}
				GameState.Get().SetBusy(false);
				goto IL_4BD;
			}
			if (missionEvent == 505)
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_05.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Victory_01);
				if (this.HeroPowerIsDawngrasp)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_05.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5Victory_02);
				}
				else
				{
					yield return base.MissionPlayVO(this.Dawngrasp_BrassRing, BOM_01_Rokara_05.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5Victory_02);
				}
				if (this.HeroPowerIsTamsin)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_05.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5Victory_03);
				}
				else
				{
					yield return base.MissionPlayVO(this.Tamsin_BrassRing, BOM_01_Rokara_05.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5Victory_03);
				}
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_05.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Victory_04);
				GameState.Get().SetBusy(false);
				goto IL_4BD;
			}
		}
		else
		{
			if (missionEvent == 506)
			{
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Loss_01);
				goto IL_4BD;
			}
			if (missionEvent == 514)
			{
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_05.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Intro_01);
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Intro_02);
				goto IL_4BD;
			}
			if (missionEvent == 515)
			{
				yield return base.MissionPlayVO(enemyActor, this.m_standardEmoteResponseLine);
				goto IL_4BD;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_4BD:
		yield break;
	}

	// Token: 0x06004C59 RID: 19545 RVA: 0x00193DED File Offset: 0x00191FED
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

	// Token: 0x06004C5A RID: 19546 RVA: 0x00193E03 File Offset: 0x00192003
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

	// Token: 0x06004C5B RID: 19547 RVA: 0x00193E19 File Offset: 0x00192019
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower().GetCard().GetActor();
		if (turn <= 7)
		{
			if (turn != 3)
			{
				if (turn == 7)
				{
					yield return base.MissionPlayVO(actor, BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5ExchangeB_01);
				}
			}
			else
			{
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_05.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeA_01);
				if (this.HeroPowerIsBrukan)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_05.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeA_Brukan_01);
				}
				if (this.HeroPowerIsGuff)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_05.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeA_Guff_01);
				}
				if (this.HeroPowerIsTamsin)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_05.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeA_Tamsin_01);
				}
				if (this.HeroPowerIsDawngrasp)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_05.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeA_Dawngrasp_01);
				}
			}
		}
		else if (turn != 15)
		{
			if (turn == 19)
			{
				yield return base.MissionPlayVO(actor, BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5ExchangeE_01);
			}
		}
		else
		{
			if (this.HeroPowerIsBrukan)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_05.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeD_Brukan_01);
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_05.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeD_Brukan_02);
			}
			if (this.HeroPowerIsGuff)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_05.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeD_Guff_01);
			}
			if (this.HeroPowerIsTamsin)
			{
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_05.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeD_Tamsin_01);
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_05.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeD_Tamsin_02);
			}
			if (this.HeroPowerIsDawngrasp)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_05.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeD_Dawngrasp_01);
			}
		}
		yield break;
	}

	// Token: 0x04004132 RID: 16690
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeA_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeA_Brukan_01.prefab:231f93c2e80c3ec438ba92fd150eac2e");

	// Token: 0x04004133 RID: 16691
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeC_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeC_Brukan_01.prefab:12946e2ff09c0a2429a87e38777a0bf9");

	// Token: 0x04004134 RID: 16692
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeD_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission5ExchangeD_Brukan_01.prefab:15070937cb52c3448a058269217ca801");

	// Token: 0x04004135 RID: 16693
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeA_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeA_Dawngrasp_01.prefab:a2ebe84b953c1ee49858b63b10ffc983");

	// Token: 0x04004136 RID: 16694
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeC_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeC_Dawngrasp_01.prefab:e38f8abddbe777747a21f21273bcdff5");

	// Token: 0x04004137 RID: 16695
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeD_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5ExchangeD_Dawngrasp_01.prefab:beaf5d9b71aa9474b8537269ea31d589");

	// Token: 0x04004138 RID: 16696
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5Victory_02 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5Victory_02.prefab:0b0f4f34c752ee34aae263499513d988");

	// Token: 0x04004139 RID: 16697
	private static readonly AssetReference VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5EmoteResponse_01 = new AssetReference("VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5EmoteResponse_01.prefab:78ca1eaa1984b684d932032310e8115e");

	// Token: 0x0400413A RID: 16698
	private static readonly AssetReference VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5ExchangeB_01.prefab:e902d8c01b1902144a0928f584a37678");

	// Token: 0x0400413B RID: 16699
	private static readonly AssetReference VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5ExchangeE_01 = new AssetReference("VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5ExchangeE_01.prefab:76344eb5d80a4864ea1b79b77d6b28f0");

	// Token: 0x0400413C RID: 16700
	private static readonly AssetReference VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_01 = new AssetReference("VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_01.prefab:9e5a17bebd5baa7468761956a654704a");

	// Token: 0x0400413D RID: 16701
	private static readonly AssetReference VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_02 = new AssetReference("VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_02.prefab:6c844673b3a39104997f78f0666bcc51");

	// Token: 0x0400413E RID: 16702
	private static readonly AssetReference VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_03 = new AssetReference("VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_03.prefab:5ca9d14eaa956ab4ca7d2eaa14458e2e");

	// Token: 0x0400413F RID: 16703
	private static readonly AssetReference VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Idle_01 = new AssetReference("VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Idle_01.prefab:70eb5ba8c7e9876438a6c2f209ec9846");

	// Token: 0x04004140 RID: 16704
	private static readonly AssetReference VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Idle_02 = new AssetReference("VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Idle_02.prefab:1e9ad57c2a2ce1f42bc412fe931d9741");

	// Token: 0x04004141 RID: 16705
	private static readonly AssetReference VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Intro_02 = new AssetReference("VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Intro_02.prefab:ea4404ca8ec47184782ecfdde1065786");

	// Token: 0x04004142 RID: 16706
	private static readonly AssetReference VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Loss_01 = new AssetReference("VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Loss_01.prefab:afeb5fa211cced74298a899436edb989");

	// Token: 0x04004143 RID: 16707
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeA_Guff_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeA_Guff_01.prefab:c3350017627594c419c87c14a1004078");

	// Token: 0x04004144 RID: 16708
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeC_Guff_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeC_Guff_01.prefab:3f1626031808ba84d87dd4db6431a040");

	// Token: 0x04004145 RID: 16709
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeD_Guff_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission5ExchangeD_Guff_01.prefab:7e15044e00b9ade42a2e3be91078acbe");

	// Token: 0x04004146 RID: 16710
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeA_01.prefab:998e5255481cf2744acc637b708f6897");

	// Token: 0x04004147 RID: 16711
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeC_Brukan_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeC_Brukan_02.prefab:c3497360ce5d63246bc5aadd3cd3a59a");

	// Token: 0x04004148 RID: 16712
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeC_Guff_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeC_Guff_02.prefab:f50496add5f683d4da5388b828d5a6f6");

	// Token: 0x04004149 RID: 16713
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeD_Brukan_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeD_Brukan_02.prefab:18f8d8875ecdacf45bdbdc37304515a6");

	// Token: 0x0400414A RID: 16714
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeD_Tamsin_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5ExchangeD_Tamsin_01.prefab:86c55d44baea5424e9ff65efadb5706a");

	// Token: 0x0400414B RID: 16715
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Intro_01.prefab:340f19e07efb6c1438136301cce9bebe");

	// Token: 0x0400414C RID: 16716
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Victory_01.prefab:aa500ce4b343858428df036777820181");

	// Token: 0x0400414D RID: 16717
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Victory_04 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Victory_04.prefab:ec7f616a4b47c714d9f47d7aa901064a");

	// Token: 0x0400414E RID: 16718
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeA_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeA_Tamsin_01.prefab:990639ebaf0a14545add3e60bb9d2a1c");

	// Token: 0x0400414F RID: 16719
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_Tamsin_01.prefab:f771e15f76264df4eade262acebe233c");

	// Token: 0x04004150 RID: 16720
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_Tamsin_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_Tamsin_02.prefab:7aab2d252a97f824581c17897b684f90");

	// Token: 0x04004151 RID: 16721
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeD_Tamsin_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeD_Tamsin_02.prefab:173f1b584507e9943b24b0df899a5460");

	// Token: 0x04004152 RID: 16722
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5Victory_03 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5Victory_03.prefab:f9d86738ed34284459549db2fb1a40cc");

	// Token: 0x04004153 RID: 16723
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_01,
		BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_02,
		BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5HeroPower_03
	};

	// Token: 0x04004154 RID: 16724
	private List<string> m_BossIdleLines2 = new List<string>
	{
		BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Idle_01,
		BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Idle_02
	};

	// Token: 0x04004155 RID: 16725
	private List<string> m_IntroductionLines = new List<string>
	{
		BOM_01_Rokara_05.VO_Story_Hero_Golem_Male_Mech_Story_Rokara_Mission5Intro_02,
		BOM_01_Rokara_05.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Intro_01
	};

	// Token: 0x04004156 RID: 16726
	private List<string> m_missionEventTriggerInGame_VictoryPostExplosionLines = new List<string>
	{
		BOM_01_Rokara_05.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Victory_01,
		BOM_01_Rokara_05.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission5Victory_02,
		BOM_01_Rokara_05.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5Victory_03,
		BOM_01_Rokara_05.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission5Victory_04
	};

	// Token: 0x04004157 RID: 16727
	private List<string> m_VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_TamsinLines = new List<string>
	{
		BOM_01_Rokara_05.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_Tamsin_01,
		BOM_01_Rokara_05.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission5ExchangeC_Tamsin_02
	};

	// Token: 0x04004158 RID: 16728
	private HashSet<string> m_playedLines = new HashSet<string>();
}
