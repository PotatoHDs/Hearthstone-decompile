using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000564 RID: 1380
public class BOM_01_Rokara_07 : BoM_01_Rokara_Dungeon
{
	// Token: 0x06004C6E RID: 19566 RVA: 0x00194618 File Offset: 0x00192818
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_01_Rokara_07.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission7ExchangeC_Brukan_01,
			BOM_01_Rokara_07.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission7Victory_Brukan_01,
			BOM_01_Rokara_07.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission7ExchangeC_Dawngrasp_01,
			BOM_01_Rokara_07.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission7Victory_Dawngrasp_01,
			BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Death_01,
			BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7EmoteResponse_01,
			BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeA_02,
			BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeB_02,
			BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeC_01,
			BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_01,
			BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_02,
			BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_03,
			BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_01,
			BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_02,
			BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_03,
			BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Intro_02,
			BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Loss_01,
			BOM_01_Rokara_07.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission7ExchangeC_Guff_01,
			BOM_01_Rokara_07.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission7Victory_Guff_01,
			BOM_01_Rokara_07.VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeD_01,
			BOM_01_Rokara_07.VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeD_03,
			BOM_01_Rokara_07.VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeE_01,
			BOM_01_Rokara_07.VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeF_01,
			BOM_01_Rokara_07.VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeG_01,
			BOM_01_Rokara_07.VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeG_03,
			BOM_01_Rokara_07.VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7Victory_02,
			BOM_01_Rokara_07.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeA_01,
			BOM_01_Rokara_07.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeB_01,
			BOM_01_Rokara_07.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeD_02,
			BOM_01_Rokara_07.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeG_02,
			BOM_01_Rokara_07.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7Intro_01,
			BOM_01_Rokara_07.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7Victory_01,
			BOM_01_Rokara_07.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission7ExchangeC_Tamsin_01,
			BOM_01_Rokara_07.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission7Victory_Tamsin_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004C6F RID: 19567 RVA: 0x0019489C File Offset: 0x00192A9C
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines2;
	}

	// Token: 0x06004C70 RID: 19568 RVA: 0x001948A4 File Offset: 0x00192AA4
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004C71 RID: 19569 RVA: 0x001948AC File Offset: 0x00192AAC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
		this.m_deathLine = BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Death_01;
		this.m_standardEmoteResponseLine = BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7EmoteResponse_01;
	}

	// Token: 0x06004C72 RID: 19570 RVA: 0x001948DF File Offset: 0x00192ADF
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower().GetCard().GetActor();
		if (missionEvent != 505)
		{
			if (missionEvent != 506)
			{
				switch (missionEvent)
				{
				case 514:
					yield return base.MissionPlayVO(actor, BOM_01_Rokara_07.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7Intro_01);
					yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Intro_02);
					break;
				case 515:
					yield return base.MissionPlayVO(enemyActor, this.m_standardEmoteResponseLine);
					break;
				case 516:
					yield return base.MissionPlaySound(enemyActor, BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Death_01);
					break;
				default:
					yield return base.HandleMissionEventWithTiming(missionEvent);
					break;
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Loss_01);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(actor, BOM_01_Rokara_07.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7Victory_01);
			if (this.HeroPowerIsBrukan)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_07.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission7Victory_Brukan_01);
			}
			if (this.HeroPowerIsGuff)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_07.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission7Victory_Guff_01);
			}
			if (this.HeroPowerIsTamsin)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_07.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission7Victory_Tamsin_01);
			}
			if (this.HeroPowerIsDawngrasp)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_07.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission7Victory_Dawngrasp_01);
			}
			yield return base.MissionPlayVO(this.Kazakus_BrassRing, BOM_01_Rokara_07.VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7Victory_02);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004C73 RID: 19571 RVA: 0x001948F5 File Offset: 0x00192AF5
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

	// Token: 0x06004C74 RID: 19572 RVA: 0x0019490B File Offset: 0x00192B0B
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

	// Token: 0x06004C75 RID: 19573 RVA: 0x00194921 File Offset: 0x00192B21
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower().GetCard().GetActor();
		if (turn != 3)
		{
			if (turn != 7)
			{
				if (turn == 11)
				{
					yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeC_01);
					if (this.HeroPowerIsBrukan)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_07.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission7ExchangeC_Brukan_01);
					}
					if (this.HeroPowerIsGuff)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_07.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission7ExchangeC_Guff_01);
					}
					if (this.HeroPowerIsTamsin)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_07.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission7ExchangeC_Tamsin_01);
					}
					if (this.HeroPowerIsDawngrasp)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_07.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission7ExchangeC_Dawngrasp_01);
					}
				}
			}
			else
			{
				yield return base.MissionPlayVO(actor, BOM_01_Rokara_07.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeB_01);
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeB_02);
			}
		}
		else
		{
			yield return base.MissionPlayVO(actor, BOM_01_Rokara_07.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeA_01);
			yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeA_02);
		}
		yield break;
	}

	// Token: 0x04004175 RID: 16757
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission7ExchangeC_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission7ExchangeC_Brukan_01.prefab:f25be8a1cdfcdbc4aa71a52320d59e21");

	// Token: 0x04004176 RID: 16758
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission7Victory_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission7Victory_Brukan_01.prefab:1b5494ac121a8a54dae6d0d945899060");

	// Token: 0x04004177 RID: 16759
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission7ExchangeC_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission7ExchangeC_Dawngrasp_01.prefab:e3f0190a90aa4c641b3f594502f35515");

	// Token: 0x04004178 RID: 16760
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission7Victory_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission7Victory_Dawngrasp_01.prefab:5d53f81be6b17a340978ef51981ba03e");

	// Token: 0x04004179 RID: 16761
	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Death_01 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Death_01.prefab:115722f12659b9a469ee286471bef94f");

	// Token: 0x0400417A RID: 16762
	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7EmoteResponse_01 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7EmoteResponse_01.prefab:9561b6eb558ede84cb55d281ee2a82ee");

	// Token: 0x0400417B RID: 16763
	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeA_02 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeA_02.prefab:4a2c6c5b2b4c32846933096dc7e4796f");

	// Token: 0x0400417C RID: 16764
	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeB_02 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeB_02.prefab:b5352a23e76d4064c86565965adc133f");

	// Token: 0x0400417D RID: 16765
	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeC_01 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7ExchangeC_01.prefab:cf9bc02209fee5c4b8be08b282cfbf7b");

	// Token: 0x0400417E RID: 16766
	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_01 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_01.prefab:1f7a0de05e54810409a0006b70a2139b");

	// Token: 0x0400417F RID: 16767
	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_02 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_02.prefab:24c80419c8639184187e2ae32784c3c1");

	// Token: 0x04004180 RID: 16768
	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_03 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_03.prefab:6ec079e762830fe40b28523f16b80a59");

	// Token: 0x04004181 RID: 16769
	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_01 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_01.prefab:8433c079ab6ccae47828d976d7afc824");

	// Token: 0x04004182 RID: 16770
	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_02 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_02.prefab:d7b4f337519d0de4890b7d6182b29b5b");

	// Token: 0x04004183 RID: 16771
	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_03 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_03.prefab:6eb9ce89a24abbe4d835179fa4341a68");

	// Token: 0x04004184 RID: 16772
	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Intro_02 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Intro_02.prefab:e04f83d03ba008840b7eb981ca481d09");

	// Token: 0x04004185 RID: 16773
	private static readonly AssetReference VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Loss_01 = new AssetReference("VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Loss_01.prefab:fdc1209a793785d4cb860c057a3d5d25");

	// Token: 0x04004186 RID: 16774
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission7ExchangeC_Guff_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission7ExchangeC_Guff_01.prefab:ad3c1964160a83c43b8f8ccd6320660c");

	// Token: 0x04004187 RID: 16775
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission7Victory_Guff_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission7Victory_Guff_01.prefab:59e9b5c250a9aa84ebcee4333df4b3c2");

	// Token: 0x04004188 RID: 16776
	private static readonly AssetReference VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeD_01 = new AssetReference("VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeD_01.prefab:e2823238bef66fd42ae7b91bee3e095c");

	// Token: 0x04004189 RID: 16777
	private static readonly AssetReference VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeD_03 = new AssetReference("VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeD_03.prefab:933cf1c59a231c24194fcd9e91cc1abe");

	// Token: 0x0400418A RID: 16778
	private static readonly AssetReference VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeE_01 = new AssetReference("VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeE_01.prefab:84fb08a1c49bb084a89a615cb715ddc8");

	// Token: 0x0400418B RID: 16779
	private static readonly AssetReference VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeF_01 = new AssetReference("VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeF_01.prefab:ec993e5d45f25b941978a9dcd01d0e28");

	// Token: 0x0400418C RID: 16780
	private static readonly AssetReference VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeG_01 = new AssetReference("VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeG_01.prefab:6da9b73e758d4f9449b24d5a547b02df");

	// Token: 0x0400418D RID: 16781
	private static readonly AssetReference VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeG_03 = new AssetReference("VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7ExchangeG_03.prefab:0ff411ed89e2e134c9dd7008bf11d979");

	// Token: 0x0400418E RID: 16782
	private static readonly AssetReference VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7Victory_02 = new AssetReference("VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7Victory_02.prefab:86485dd69a38d134381f766b97944963");

	// Token: 0x0400418F RID: 16783
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeA_01.prefab:eddece9b68667e9459e89c9ab6edafeb");

	// Token: 0x04004190 RID: 16784
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeB_01.prefab:1678595af45de26429a4e54e0fa00080");

	// Token: 0x04004191 RID: 16785
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeD_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeD_02.prefab:a2a50b9e82e769a40bbd34d067786ad6");

	// Token: 0x04004192 RID: 16786
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeG_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7ExchangeG_02.prefab:392530965d59c074da73e61a540199b0");

	// Token: 0x04004193 RID: 16787
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7Intro_01.prefab:35ef07e900094684dacc1fb4bab0f9a0");

	// Token: 0x04004194 RID: 16788
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7Victory_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7Victory_01.prefab:283423919171f1243b1d96ee930bd4e3");

	// Token: 0x04004195 RID: 16789
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission7ExchangeC_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission7ExchangeC_Tamsin_01.prefab:5c78c77539bf5a347a635bb8704d0e32");

	// Token: 0x04004196 RID: 16790
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission7Victory_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission7Victory_Tamsin_01.prefab:1a960d26618af4741a7131844a297c1a");

	// Token: 0x04004197 RID: 16791
	private List<string> m_missionEventTriggerInGame_VictoryPostExplosionLines = new List<string>
	{
		BOM_01_Rokara_07.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission7Victory_Dawngrasp_01,
		BOM_01_Rokara_07.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission7Victory_Guff_01,
		BOM_01_Rokara_07.VO_Story_Hero_Kazakus_Male_Troll_Story_Rokara_Mission7Victory_02,
		BOM_01_Rokara_07.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7Victory_01,
		BOM_01_Rokara_07.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission7Victory_Tamsin_01
	};

	// Token: 0x04004198 RID: 16792
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_01,
		BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_02,
		BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7HeroPower_03
	};

	// Token: 0x04004199 RID: 16793
	private List<string> m_BossIdleLines2 = new List<string>
	{
		BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_01,
		BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_02,
		BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Idle_03
	};

	// Token: 0x0400419A RID: 16794
	private List<string> m_IntroductionLines = new List<string>
	{
		BOM_01_Rokara_07.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission7Intro_01,
		BOM_01_Rokara_07.VO_Story_Hero_Feegly_Male_Dwarf_Story_Rokara_Mission7Intro_02
	};

	// Token: 0x0400419B RID: 16795
	private HashSet<string> m_playedLines = new HashSet<string>();
}
