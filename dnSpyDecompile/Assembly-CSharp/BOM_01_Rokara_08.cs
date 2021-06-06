using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000565 RID: 1381
public class BOM_01_Rokara_08 : BoM_01_Rokara_Dungeon
{
	// Token: 0x06004C7B RID: 19579 RVA: 0x00194C60 File Offset: 0x00192E60
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Death_01,
			BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8EmoteResponse_01,
			BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeA_01,
			BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeB_01,
			BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeB_03,
			BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeC_01,
			BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeD_01,
			BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeE_02,
			BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeF_01,
			BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeF_02,
			BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_01,
			BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_02,
			BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_03,
			BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_01,
			BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_02,
			BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_03,
			BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Intro_01,
			BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Loss_01,
			BOM_01_Rokara_08.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeA_02,
			BOM_01_Rokara_08.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeB_02,
			BOM_01_Rokara_08.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeC_02,
			BOM_01_Rokara_08.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeD_02,
			BOM_01_Rokara_08.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeE_01,
			BOM_01_Rokara_08.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8Intro_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004C7C RID: 19580 RVA: 0x00194E44 File Offset: 0x00193044
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines2;
	}

	// Token: 0x06004C7D RID: 19581 RVA: 0x00194E4C File Offset: 0x0019304C
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004C7E RID: 19582 RVA: 0x00194E54 File Offset: 0x00193054
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_TRLFinalBoss;
		this.m_deathLine = BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Death_01;
		this.m_standardEmoteResponseLine = BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8EmoteResponse_01;
	}

	// Token: 0x06004C7F RID: 19583 RVA: 0x00194E87 File Offset: 0x00193087
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 504)
		{
			if (missionEvent == 500)
			{
				yield return base.MissionPlayVOOnce(enemyActor, BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeC_01);
				yield return base.MissionPlayVOOnce(friendlyActor, BOM_01_Rokara_08.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeC_02);
				goto IL_29E;
			}
			if (missionEvent == 504)
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeF_01);
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeF_02);
				GameState.Get().SetBusy(false);
				goto IL_29E;
			}
		}
		else
		{
			if (missionEvent == 506)
			{
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Loss_01);
				goto IL_29E;
			}
			if (missionEvent == 514)
			{
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Intro_01);
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_08.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8Intro_02);
				goto IL_29E;
			}
			if (missionEvent == 515)
			{
				yield return base.MissionPlayVO(enemyActor, this.m_standardEmoteResponseLine);
				goto IL_29E;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_29E:
		yield break;
	}

	// Token: 0x06004C80 RID: 19584 RVA: 0x00194E9D File Offset: 0x0019309D
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

	// Token: 0x06004C81 RID: 19585 RVA: 0x00194EB3 File Offset: 0x001930B3
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

	// Token: 0x06004C82 RID: 19586 RVA: 0x00194EC9 File Offset: 0x001930C9
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn <= 7)
		{
			if (turn != 3)
			{
				if (turn == 7)
				{
					yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeB_01);
					yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_08.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeB_02);
					yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeB_03);
				}
			}
			else
			{
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeA_01);
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_08.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeA_02);
			}
		}
		else if (turn != 15)
		{
			if (turn == 19)
			{
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_08.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeE_01);
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeE_02);
			}
		}
		else
		{
			yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeD_01);
			yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_08.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeD_02);
		}
		yield break;
	}

	// Token: 0x0400419C RID: 16796
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Death_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Death_01.prefab:d57f499dd401e4f4c839e841707c9605");

	// Token: 0x0400419D RID: 16797
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8EmoteResponse_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8EmoteResponse_01.prefab:6d311296a889b904c9a29cb79dc089c2");

	// Token: 0x0400419E RID: 16798
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeA_01.prefab:18e711394b1278546a1c7ced7f50dd09");

	// Token: 0x0400419F RID: 16799
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeB_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeB_01.prefab:b130b40a08b2b1944b2b64fb1db64a7c");

	// Token: 0x040041A0 RID: 16800
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeB_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeB_03.prefab:40c9e53c2f1e50e4ebc51cd4375eebe2");

	// Token: 0x040041A1 RID: 16801
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeC_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeC_01.prefab:49b39039325ad3c4887d55ebb094baff");

	// Token: 0x040041A2 RID: 16802
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeD_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeD_01.prefab:57bfb32acf9ca8648a4cb862f01d19c7");

	// Token: 0x040041A3 RID: 16803
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeE_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeE_02.prefab:eb076d1bfa8e79b4f96b5dd8e910d986");

	// Token: 0x040041A4 RID: 16804
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeF_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeF_01.prefab:cbef19348ba154f4bbf2bba2704688a2");

	// Token: 0x040041A5 RID: 16805
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeF_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeF_02.prefab:2b284754b8d9e864a80ccc2229cfe166");

	// Token: 0x040041A6 RID: 16806
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_01.prefab:cb5ff3787df26514d80e731b04e6f16d");

	// Token: 0x040041A7 RID: 16807
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_02.prefab:61d04c10c9329a64f8a694ebae888320");

	// Token: 0x040041A8 RID: 16808
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_03.prefab:18360fe4696cd7b448cc10d2624c5556");

	// Token: 0x040041A9 RID: 16809
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_01.prefab:1a8a670ce5f8bf3428a8f3e41fd44217");

	// Token: 0x040041AA RID: 16810
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_02.prefab:39e9056e2c47fc74e9de0afa5ca9481d");

	// Token: 0x040041AB RID: 16811
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_03.prefab:3833873a266f11b4c9389705a24beed1");

	// Token: 0x040041AC RID: 16812
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Intro_01.prefab:a144fec3f78382f44a3d06c2b69c9ecb");

	// Token: 0x040041AD RID: 16813
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Loss_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Loss_01.prefab:c42ccb28859f6a24195679f062a5906f");

	// Token: 0x040041AE RID: 16814
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeA_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeA_02.prefab:a0d3e50809404814da5738a80f8c0e8a");

	// Token: 0x040041AF RID: 16815
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeB_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeB_02.prefab:4f4e12ba3fd558043b71cf14347ed9f8");

	// Token: 0x040041B0 RID: 16816
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeC_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeC_02.prefab:6e8322ae52f3fe547ab4742e307c2468");

	// Token: 0x040041B1 RID: 16817
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeD_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeD_02.prefab:93efbe7b4663c8a4b98db5de18bcb93b");

	// Token: 0x040041B2 RID: 16818
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeE_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeE_01.prefab:31e86b085b455794a81a4827d9c48dbe");

	// Token: 0x040041B3 RID: 16819
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8Intro_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8Intro_02.prefab:f55245828b083094da18790b620bab15");

	// Token: 0x040041B4 RID: 16820
	private List<string> m_VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeBLines = new List<string>
	{
		BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeB_01,
		BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeB_03
	};

	// Token: 0x040041B5 RID: 16821
	private List<string> m_VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeFLines = new List<string>
	{
		BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeF_01,
		BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeF_02
	};

	// Token: 0x040041B6 RID: 16822
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_01,
		BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_02,
		BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_03
	};

	// Token: 0x040041B7 RID: 16823
	private List<string> m_BossIdleLines2 = new List<string>
	{
		BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_01,
		BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_02,
		BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_03
	};

	// Token: 0x040041B8 RID: 16824
	private List<string> m_IntroductionLines = new List<string>
	{
		BOM_01_Rokara_08.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Intro_01,
		BOM_01_Rokara_08.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8Intro_02
	};

	// Token: 0x040041B9 RID: 16825
	private HashSet<string> m_playedLines = new HashSet<string>();
}
