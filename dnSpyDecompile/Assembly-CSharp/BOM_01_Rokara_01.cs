using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200055E RID: 1374
public class BOM_01_Rokara_01 : BoM_01_Rokara_Dungeon
{
	// Token: 0x06004C22 RID: 19490 RVA: 0x001926C0 File Offset: 0x001908C0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Death_01,
			BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1EmoteResponse_01,
			BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeA_02,
			BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeB_02,
			BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeD_01,
			BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeD_03,
			BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeE_02,
			BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_01,
			BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_02,
			BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_03,
			BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_01,
			BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_02,
			BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_03,
			BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_01,
			BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_02,
			BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_03,
			BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Intro_02,
			BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Loss_01,
			BOM_01_Rokara_01.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1ExchangeA_01,
			BOM_01_Rokara_01.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1ExchangeC_02,
			BOM_01_Rokara_01.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1Victory_01,
			BOM_01_Rokara_01.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1Victory_03,
			BOM_01_Rokara_01.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeB_01,
			BOM_01_Rokara_01.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeC_01,
			BOM_01_Rokara_01.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeD_02,
			BOM_01_Rokara_01.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeE_01,
			BOM_01_Rokara_01.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1Intro_01,
			BOM_01_Rokara_01.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1Victory_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004C23 RID: 19491 RVA: 0x001928E4 File Offset: 0x00190AE4
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines2;
	}

	// Token: 0x06004C24 RID: 19492 RVA: 0x001928EC File Offset: 0x00190AEC
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004C25 RID: 19493 RVA: 0x001928F4 File Offset: 0x00190AF4
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_TRL;
		this.m_deathLine = BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Death_01;
		this.m_standardEmoteResponseLine = BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1EmoteResponse_01;
	}

	// Token: 0x06004C26 RID: 19494 RVA: 0x00192927 File Offset: 0x00190B27
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
				yield return base.MissionPlayVO(this.Garrosh_BrassRing, BOM_01_Rokara_01.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1Victory_01);
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_01.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1Victory_02);
				yield return base.MissionPlayVO(this.Garrosh_BrassRing, BOM_01_Rokara_01.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1Victory_03);
				GameState.Get().SetBusy(false);
				goto IL_297;
			}
			if (missionEvent == 506)
			{
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Loss_01);
				goto IL_297;
			}
		}
		else
		{
			if (missionEvent == 514)
			{
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_01.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1Intro_01);
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Intro_02);
				goto IL_297;
			}
			if (missionEvent == 515)
			{
				yield return base.MissionPlayVO(enemyActor, this.m_standardEmoteResponseLine);
				goto IL_297;
			}
			if (missionEvent == 58028)
			{
				yield return base.MissionPlayVOOnceInOrder(enemyActor, this.m_VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HitLines);
				goto IL_297;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_297:
		yield break;
	}

	// Token: 0x06004C27 RID: 19495 RVA: 0x0019293D File Offset: 0x00190B3D
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

	// Token: 0x06004C28 RID: 19496 RVA: 0x00192953 File Offset: 0x00190B53
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

	// Token: 0x06004C29 RID: 19497 RVA: 0x00192969 File Offset: 0x00190B69
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
					yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_01.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeB_01);
					yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeB_02);
				}
			}
			else
			{
				yield return base.MissionPlayVO(this.Garrosh_BrassRing, BOM_01_Rokara_01.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1ExchangeA_01);
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeA_02);
			}
		}
		else if (turn != 11)
		{
			if (turn != 15)
			{
				if (turn == 17)
				{
					yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_01.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeE_01);
					yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeE_02);
				}
			}
			else
			{
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeD_01);
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_01.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeD_02);
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeD_03);
			}
		}
		else
		{
			yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_01.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeC_01);
			yield return base.MissionPlayVO(this.Garrosh_BrassRing, BOM_01_Rokara_01.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1ExchangeC_02);
		}
		yield break;
	}

	// Token: 0x040040B8 RID: 16568
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Death_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Death_01.prefab:558fd71a615ee7649baff1608da94a61");

	// Token: 0x040040B9 RID: 16569
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1EmoteResponse_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1EmoteResponse_01.prefab:1e299a6d247708a488eb6d11f3d9a01d");

	// Token: 0x040040BA RID: 16570
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeA_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeA_02.prefab:8f3b42b3b2354eb4292a1abe39ab8e98");

	// Token: 0x040040BB RID: 16571
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeB_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeB_02.prefab:aab232a63fe10264a9e4bac58152a74c");

	// Token: 0x040040BC RID: 16572
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeD_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeD_01.prefab:25e6aa451cb3d374b9fcda4bed2d1609");

	// Token: 0x040040BD RID: 16573
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeD_03 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeD_03.prefab:39e2e513727070d4d98aa856d82f6fe7");

	// Token: 0x040040BE RID: 16574
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeE_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeE_02.prefab:edaa6063f7105b847ac2ebfdb5e2e94b");

	// Token: 0x040040BF RID: 16575
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_01.prefab:c0131a4f26cb87d44b4e547dac7dcadd");

	// Token: 0x040040C0 RID: 16576
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_02.prefab:46d446e0cec712141a452109deb8bdcc");

	// Token: 0x040040C1 RID: 16577
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_03 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_03.prefab:90cdff5762574f844aafbb195aa8c39d");

	// Token: 0x040040C2 RID: 16578
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_01.prefab:faf2d4c922898584a88d1fd38d2263a5");

	// Token: 0x040040C3 RID: 16579
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_02.prefab:182b3be79b0e0744c86f4773f671d72c");

	// Token: 0x040040C4 RID: 16580
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_03 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_03.prefab:c9c2b13f47ce1f64bba15e914767f404");

	// Token: 0x040040C5 RID: 16581
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_01.prefab:871c60506dc8f9b4683082b7629dc2eb");

	// Token: 0x040040C6 RID: 16582
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_02.prefab:40de8524b612fc44e8a4156a4217001a");

	// Token: 0x040040C7 RID: 16583
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_03 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_03.prefab:6b477cec73f4614468f2ef3366783b34");

	// Token: 0x040040C8 RID: 16584
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Intro_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Intro_02.prefab:0d71ed8d56164384eaa5d479ee364693");

	// Token: 0x040040C9 RID: 16585
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Loss_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Loss_01.prefab:b863a0ebc8636e24284d8ed96d542c80");

	// Token: 0x040040CA RID: 16586
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1ExchangeA_01.prefab:5b3a56f5e6db0d44fb5b1e1a90668c41");

	// Token: 0x040040CB RID: 16587
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1ExchangeC_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1ExchangeC_02.prefab:5a9171a6b57489f41b4ffe153bf882f3");

	// Token: 0x040040CC RID: 16588
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1Victory_01.prefab:d11dfdfc84ff48d4993763b12cb45447");

	// Token: 0x040040CD RID: 16589
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1Victory_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1Victory_03.prefab:9f6b6e3cd4318524baab74d128f390d7");

	// Token: 0x040040CE RID: 16590
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeB_01.prefab:9a6eb3154953edf4fad2519c48cd5093");

	// Token: 0x040040CF RID: 16591
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeC_01.prefab:bd7647c719bc4624a877d55bc2a14163");

	// Token: 0x040040D0 RID: 16592
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeD_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeD_02.prefab:2de8df243737b3c4fb00332062a04223");

	// Token: 0x040040D1 RID: 16593
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeE_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeE_01.prefab:c8853146456ebd34f97697d3b051ff48");

	// Token: 0x040040D2 RID: 16594
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1Intro_01.prefab:3965fb11169706840862c690ff3f9157");

	// Token: 0x040040D3 RID: 16595
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1Victory_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1Victory_02.prefab:2f45585fa7c0e8e4088ee39c21f5c2c9");

	// Token: 0x040040D4 RID: 16596
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_01,
		BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_02,
		BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_03
	};

	// Token: 0x040040D5 RID: 16597
	private List<string> m_VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HitLines = new List<string>
	{
		BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_01,
		BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_02,
		BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_03
	};

	// Token: 0x040040D6 RID: 16598
	private List<string> m_BossIdleLines2 = new List<string>
	{
		BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_01,
		BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_02,
		BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_03
	};

	// Token: 0x040040D7 RID: 16599
	private List<string> m_IntroductionLines = new List<string>
	{
		BOM_01_Rokara_01.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Intro_02,
		BOM_01_Rokara_01.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1Intro_01
	};

	// Token: 0x040040D8 RID: 16600
	private List<string> m_missionEventTriggerInGame_VictoryPostExplosionLines = new List<string>
	{
		BOM_01_Rokara_01.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1Victory_01,
		BOM_01_Rokara_01.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1Victory_02,
		BOM_01_Rokara_01.VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1Victory_03
	};

	// Token: 0x040040D9 RID: 16601
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x040040DA RID: 16602
	public const int InGame_HitResponse = 58028;
}
