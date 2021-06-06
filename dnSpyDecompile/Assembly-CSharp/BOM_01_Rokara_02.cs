using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200055F RID: 1375
public class BOM_01_Rokara_02 : BoM_01_Rokara_Dungeon
{
	// Token: 0x06004C2F RID: 19503 RVA: 0x00192C84 File Offset: 0x00190E84
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_01_Rokara_02.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2ExchangeD_02,
			BOM_01_Rokara_02.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeA_01,
			BOM_01_Rokara_02.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeA_03,
			BOM_01_Rokara_02.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeC_02,
			BOM_01_Rokara_02.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2Victory_01,
			BOM_01_Rokara_02.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2Victory_03,
			BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Death_01,
			BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2EmoteResponse_01,
			BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeA_02,
			BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeB_02,
			BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeD_01,
			BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_01,
			BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_02,
			BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_03,
			BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_01,
			BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_02,
			BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_03,
			BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Intro_02,
			BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Loss_01,
			BOM_01_Rokara_02.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeB_01,
			BOM_01_Rokara_02.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeB_03,
			BOM_01_Rokara_02.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeC_01,
			BOM_01_Rokara_02.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2Intro_01,
			BOM_01_Rokara_02.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2Victory_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004C30 RID: 19504 RVA: 0x00192E68 File Offset: 0x00191068
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines2;
	}

	// Token: 0x06004C31 RID: 19505 RVA: 0x00192E70 File Offset: 0x00191070
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004C32 RID: 19506 RVA: 0x00192E78 File Offset: 0x00191078
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
		this.m_deathLine = BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Death_01;
		this.m_standardEmoteResponseLine = BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2EmoteResponse_01;
	}

	// Token: 0x06004C33 RID: 19507 RVA: 0x00192EAB File Offset: 0x001910AB
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 506)
		{
			if (missionEvent == 504)
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(this.Guff_BrassRing, BOM_01_Rokara_02.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2Victory_01);
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_02.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2Victory_02);
				yield return base.MissionPlayVO(this.Guff_BrassRing, BOM_01_Rokara_02.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2Victory_03);
				GameState.Get().SetBusy(false);
				goto IL_25A;
			}
			if (missionEvent == 506)
			{
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Loss_01);
				goto IL_25A;
			}
		}
		else
		{
			if (missionEvent == 514)
			{
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_02.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2Intro_01);
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Intro_02);
				goto IL_25A;
			}
			if (missionEvent == 515)
			{
				yield return base.MissionPlayVO(enemyActor, this.m_standardEmoteResponseLine);
				goto IL_25A;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_25A:
		yield break;
	}

	// Token: 0x06004C34 RID: 19508 RVA: 0x00192EC1 File Offset: 0x001910C1
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

	// Token: 0x06004C35 RID: 19509 RVA: 0x00192ED7 File Offset: 0x001910D7
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

	// Token: 0x06004C36 RID: 19510 RVA: 0x00192EED File Offset: 0x001910ED
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
					yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_02.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeB_01);
					yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeB_02);
					yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_02.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeB_03);
				}
			}
			else
			{
				yield return base.MissionPlayVO("BOM_01_Guff_02t", this.Guff_BrassRing, BOM_01_Rokara_02.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeA_01);
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeA_02);
				yield return base.MissionPlayVO("BOM_01_Guff_02t", this.Guff_BrassRing, BOM_01_Rokara_02.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeA_03);
			}
		}
		else if (turn != 9)
		{
			if (turn == 13)
			{
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeD_01);
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_02.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2ExchangeD_02);
			}
		}
		else
		{
			yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_02.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeC_01);
			yield return base.MissionPlayVO("BOM_01_Guff_02t", this.Guff_BrassRing, BOM_01_Rokara_02.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeC_02);
		}
		yield break;
	}

	// Token: 0x040040DB RID: 16603
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2ExchangeD_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2ExchangeD_02.prefab:d31a95159c60fab4b936fc14eb97799d");

	// Token: 0x040040DC RID: 16604
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeA_01.prefab:0319689001b1ec44e84796681d7ba83e");

	// Token: 0x040040DD RID: 16605
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeA_03 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeA_03.prefab:bfbb391cf4b8f4e4fa6dbd5d526114a2");

	// Token: 0x040040DE RID: 16606
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeC_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeC_02.prefab:ee5e64a48e99ed94da8ce4177710d33a");

	// Token: 0x040040DF RID: 16607
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2Victory_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2Victory_01.prefab:4940ad74fbbd22d42bb9331fb92f64ca");

	// Token: 0x040040E0 RID: 16608
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2Victory_03 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2Victory_03.prefab:fe383ad089fac5a40a10dc2f6bc7e0b7");

	// Token: 0x040040E1 RID: 16609
	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Death_01 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Death_01.prefab:221c8690e2666f745b0273e44ae735ad");

	// Token: 0x040040E2 RID: 16610
	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2EmoteResponse_01 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2EmoteResponse_01.prefab:1d7bdd581c9543543948e42bbe638099");

	// Token: 0x040040E3 RID: 16611
	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeA_02 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeA_02.prefab:4c4d9f428bafe5240a28a6c1e3433575");

	// Token: 0x040040E4 RID: 16612
	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeB_02 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeB_02.prefab:d12f5ef52ebdc5742b03ca513799b920");

	// Token: 0x040040E5 RID: 16613
	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeD_01 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeD_01.prefab:08a0cac0d571183449701ec0aada45ae");

	// Token: 0x040040E6 RID: 16614
	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_01.prefab:6b2b2cd4eb8509b45b6439e956d1915b");

	// Token: 0x040040E7 RID: 16615
	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_02.prefab:10e9e6734ac6b7848aa719c881446586");

	// Token: 0x040040E8 RID: 16616
	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_03.prefab:3624cc666661b0644a1916290096e4a2");

	// Token: 0x040040E9 RID: 16617
	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_01 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_01.prefab:5faa6215bc596564ab19f153b3434d35");

	// Token: 0x040040EA RID: 16618
	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_02 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_02.prefab:9061673071052ba46bbf7d93afed7a29");

	// Token: 0x040040EB RID: 16619
	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_03 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_03.prefab:95d11bff2f77afc41a00ebfce1ddd3e0");

	// Token: 0x040040EC RID: 16620
	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Intro_02 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Intro_02.prefab:8e4ed929e024a1241b5c6d1280e66922");

	// Token: 0x040040ED RID: 16621
	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Loss_01 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Loss_01.prefab:6606ec7237e9f6a4a90e9c3b99af4b04");

	// Token: 0x040040EE RID: 16622
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeB_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeB_01.prefab:f1e2fe2989b6b034aae777f2302b19bc");

	// Token: 0x040040EF RID: 16623
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeB_03 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeB_03.prefab:7da1b3ea252090e498b5f3dab9b62980");

	// Token: 0x040040F0 RID: 16624
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeC_01.prefab:ee6ef62283f67d24fafe8f639631e492");

	// Token: 0x040040F1 RID: 16625
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2Intro_01.prefab:d6b4de7c0e6e15c49aa2847e1228096e");

	// Token: 0x040040F2 RID: 16626
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2Victory_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2Victory_02.prefab:36abe244d5d19014a9b084597fd6a926");

	// Token: 0x040040F3 RID: 16627
	private List<string> m_VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeALines = new List<string>
	{
		BOM_01_Rokara_02.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeA_01,
		BOM_01_Rokara_02.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeA_03
	};

	// Token: 0x040040F4 RID: 16628
	private List<string> m_missionEventTriggerInGame_VictoryPostExplosionLines = new List<string>
	{
		BOM_01_Rokara_02.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2Victory_01,
		BOM_01_Rokara_02.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2Victory_03,
		BOM_01_Rokara_02.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2Victory_02
	};

	// Token: 0x040040F5 RID: 16629
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_01,
		BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_02,
		BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_03
	};

	// Token: 0x040040F6 RID: 16630
	private List<string> m_BossIdleLines2 = new List<string>
	{
		BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_01,
		BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_02,
		BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_03
	};

	// Token: 0x040040F7 RID: 16631
	private List<string> m_IntroductionLines = new List<string>
	{
		BOM_01_Rokara_02.VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Intro_02,
		BOM_01_Rokara_02.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2Intro_01
	};

	// Token: 0x040040F8 RID: 16632
	private List<string> m_VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeBLines = new List<string>
	{
		BOM_01_Rokara_02.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeB_01,
		BOM_01_Rokara_02.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeB_03
	};

	// Token: 0x040040F9 RID: 16633
	private HashSet<string> m_playedLines = new HashSet<string>();
}
