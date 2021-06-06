using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000560 RID: 1376
public class BOM_01_Rokara_03 : BoM_01_Rokara_Dungeon
{
	// Token: 0x06004C3C RID: 19516 RVA: 0x001931CC File Offset: 0x001913CC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_01_Rokara_03.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3ExchangeB_Brukan_01,
			BOM_01_Rokara_03.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3ExchangeC_Brukan_01,
			BOM_01_Rokara_03.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_02,
			BOM_01_Rokara_03.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_03,
			BOM_01_Rokara_03.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3ExchangeB_Guff_01,
			BOM_01_Rokara_03.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3ExchangeC_Guff_01,
			BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Death_01,
			BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3EmoteResponse_01,
			BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3ExchangeA_02,
			BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_01,
			BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_02,
			BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_03,
			BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_01,
			BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_02,
			BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_03,
			BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Intro_02,
			BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Loss_01,
			BOM_01_Rokara_03.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3ExchangeB_03,
			BOM_01_Rokara_03.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3ExchangeC_03,
			BOM_01_Rokara_03.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3Intro_01,
			BOM_01_Rokara_03.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeA_01,
			BOM_01_Rokara_03.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeD_01,
			BOM_01_Rokara_03.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeD_02,
			BOM_01_Rokara_03.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3Victory_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004C3D RID: 19517 RVA: 0x001933B0 File Offset: 0x001915B0
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines2;
	}

	// Token: 0x06004C3E RID: 19518 RVA: 0x001933B8 File Offset: 0x001915B8
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004C3F RID: 19519 RVA: 0x001933C0 File Offset: 0x001915C0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
		this.m_SupressEnemyDeathTextBubble = true;
		this.m_deathLine = BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Death_01;
		this.m_standardEmoteResponseLine = BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3EmoteResponse_01;
	}

	// Token: 0x06004C40 RID: 19520 RVA: 0x001933FA File Offset: 0x001915FA
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower().GetCard().GetActor();
		if (missionEvent <= 506)
		{
			if (missionEvent == 505)
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(this.Tamsin_BrassRing, BOM_01_Rokara_03.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3Victory_01);
				if (this.HeroPowerIsBrukan)
				{
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_03.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_02);
					yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_03.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_03);
				}
				else
				{
					yield return base.MissionPlayVO(this.Brukan_BrassRing, BOM_01_Rokara_03.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_02);
					yield return base.MissionPlayVO(this.Brukan_BrassRing, BOM_01_Rokara_03.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_03);
				}
				GameState.Get().SetBusy(false);
				goto IL_2DB;
			}
			if (missionEvent == 506)
			{
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Loss_01);
				goto IL_2DB;
			}
		}
		else
		{
			if (missionEvent == 514)
			{
				yield return base.MissionPlayVO(actor, BOM_01_Rokara_03.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3Intro_01);
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Intro_02);
				goto IL_2DB;
			}
			if (missionEvent == 515)
			{
				yield return base.MissionPlayVO(enemyActor, this.m_standardEmoteResponseLine);
				goto IL_2DB;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_2DB:
		yield break;
	}

	// Token: 0x06004C41 RID: 19521 RVA: 0x00193410 File Offset: 0x00191610
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

	// Token: 0x06004C42 RID: 19522 RVA: 0x00193426 File Offset: 0x00191626
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

	// Token: 0x06004C43 RID: 19523 RVA: 0x0019343C File Offset: 0x0019163C
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
					if (this.HeroPowerIsBrukan)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_03.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3ExchangeB_Brukan_01);
					}
					if (this.HeroPowerIsGuff)
					{
						yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_03.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3ExchangeB_Guff_01);
					}
					yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_03.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3ExchangeB_03);
				}
			}
			else
			{
				yield return base.MissionPlayVO("BOM_01_Tamsin_03t", this.Tamsin_BrassRing, BOM_01_Rokara_03.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeA_01);
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3ExchangeA_02);
			}
		}
		else if (turn != 9)
		{
			if (turn == 11)
			{
				yield return base.MissionPlayVO("BOM_01_Tamsin_03t", this.Tamsin_BrassRing, BOM_01_Rokara_03.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeD_01);
				yield return base.MissionPlayVO("BOM_01_Tamsin_03t", this.Tamsin_BrassRing, BOM_01_Rokara_03.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeD_02);
			}
		}
		else
		{
			if (this.HeroPowerIsBrukan)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_03.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3ExchangeC_Brukan_01);
			}
			if (this.HeroPowerIsGuff)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_03.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3ExchangeC_Guff_01);
			}
			yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_03.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3ExchangeC_03);
		}
		yield break;
	}

	// Token: 0x040040FA RID: 16634
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3ExchangeB_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3ExchangeB_Brukan_01.prefab:db17c264335d9814c813bb19a750b4c2");

	// Token: 0x040040FB RID: 16635
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3ExchangeC_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3ExchangeC_Brukan_01.prefab:ce169ec3e1701f64dbb9434bcbe6e512");

	// Token: 0x040040FC RID: 16636
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_02.prefab:f4be4eeaeefddd14da2e1370d99ef7ba");

	// Token: 0x040040FD RID: 16637
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_03 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_03.prefab:2536f28dc7d608042b46c91dfd86cacc");

	// Token: 0x040040FE RID: 16638
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3ExchangeB_Guff_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3ExchangeB_Guff_01.prefab:19cbc0432217f6547bfbf89ae89566ea");

	// Token: 0x040040FF RID: 16639
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3ExchangeC_Guff_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3ExchangeC_Guff_01.prefab:f19ea73d8f592344a8fc49c2453554ab");

	// Token: 0x04004100 RID: 16640
	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Death_01 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Death_01.prefab:2d8727ce2f2d9bf49a9885d707a35435");

	// Token: 0x04004101 RID: 16641
	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3EmoteResponse_01.prefab:fa9b394093dfdf548b3c4c9e0cc35699");

	// Token: 0x04004102 RID: 16642
	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3ExchangeA_02 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3ExchangeA_02.prefab:005ebdc69d2fb7d4c96029f54e6be81a");

	// Token: 0x04004103 RID: 16643
	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_01 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_01.prefab:7dda034986b038f40bba160a167c4bea");

	// Token: 0x04004104 RID: 16644
	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_02 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_02.prefab:9a7138582dedeee438aab61141d9d457");

	// Token: 0x04004105 RID: 16645
	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_03 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_03.prefab:8b1a01542cdffd246a9c496d7b7871cd");

	// Token: 0x04004106 RID: 16646
	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_01 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_01.prefab:574ee39549d2f32469ced4ee36e45727");

	// Token: 0x04004107 RID: 16647
	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_02 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_02.prefab:a555e07ad916c634184491fd82335a1c");

	// Token: 0x04004108 RID: 16648
	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_03 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_03.prefab:bfa752deee9e30142973234733d22f64");

	// Token: 0x04004109 RID: 16649
	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Intro_02 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Intro_02.prefab:4ede0de7ace3c664f9ba6ac2ddd1c8df");

	// Token: 0x0400410A RID: 16650
	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Loss_01 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Loss_01.prefab:739063e37010c0b488cbb98222f7f1c3");

	// Token: 0x0400410B RID: 16651
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3ExchangeB_03 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3ExchangeB_03.prefab:ff6ca8274ea77f145b020306a9a75545");

	// Token: 0x0400410C RID: 16652
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3ExchangeC_03 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3ExchangeC_03.prefab:5d65f0c3b04617a4e985f13f70965c36");

	// Token: 0x0400410D RID: 16653
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3Intro_01.prefab:c47d620247ce8b944b1259f04a3b4c55");

	// Token: 0x0400410E RID: 16654
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeA_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeA_01.prefab:980e891e7c9f28741973916504794534");

	// Token: 0x0400410F RID: 16655
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeD_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeD_01.prefab:7802f98054a13054cb549bb017204eaf");

	// Token: 0x04004110 RID: 16656
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeD_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeD_02.prefab:1dd9acdc656ee154998143ca65d1ffbd");

	// Token: 0x04004111 RID: 16657
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3Victory_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3Victory_01.prefab:375355c25d8988b4da3f87cbd83f0888");

	// Token: 0x04004112 RID: 16658
	private List<string> m_missionEventTriggerInGame_VictoryPostExplosionLines = new List<string>
	{
		BOM_01_Rokara_03.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3Victory_01,
		BOM_01_Rokara_03.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_02,
		BOM_01_Rokara_03.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_03
	};

	// Token: 0x04004113 RID: 16659
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_01,
		BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_02,
		BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_03
	};

	// Token: 0x04004114 RID: 16660
	private List<string> m_BossIdleLines2 = new List<string>
	{
		BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_01,
		BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_02,
		BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_03
	};

	// Token: 0x04004115 RID: 16661
	private List<string> m_IntroductionLines = new List<string>
	{
		BOM_01_Rokara_03.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3Intro_01,
		BOM_01_Rokara_03.VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Intro_02
	};

	// Token: 0x04004116 RID: 16662
	private List<string> m_VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeDLines = new List<string>
	{
		BOM_01_Rokara_03.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeD_01,
		BOM_01_Rokara_03.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeD_02
	};

	// Token: 0x04004117 RID: 16663
	private HashSet<string> m_playedLines = new HashSet<string>();
}
