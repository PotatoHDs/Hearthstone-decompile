using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000473 RID: 1139
public class DALA_Dungeon_Boss_70h : DALA_Dungeon
{
	// Token: 0x06003DB0 RID: 15792 RVA: 0x00144248 File Offset: 0x00142448
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_BossBombardmentAOE_02,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_BossBombardmentAOEFails_01,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_BossCannonBarrage_01,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_Death_02,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_DefeatPlayer_02,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_EmoteResponse_02,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_HeroPower_01,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_HeroPower_02,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_HeroPower_03,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_HeroPower_04,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_HeroPower_05,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_01,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_02,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_03,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_04,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_Idle_01,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_Idle_03,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_Idle_04,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_Intro_01,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_IntroEudora_01,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_PlayerFlyBy_01,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_PlayerPirate_01,
			DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_PlayerShipCannon_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003DB1 RID: 15793 RVA: 0x0014441C File Offset: 0x0014261C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_EmoteResponse_02;
	}

	// Token: 0x06003DB2 RID: 15794 RVA: 0x00144454 File Offset: 0x00142654
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_70h.m_IdleLines;
	}

	// Token: 0x06003DB3 RID: 15795 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003DB4 RID: 15796 RVA: 0x0014445C File Offset: 0x0014265C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Eudora")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003DB5 RID: 15797 RVA: 0x00144507 File Offset: 0x00142707
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_70h.m_HeroPower);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_70h.m_HeroPowerReloadingForOneTurn);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_70h.m_HeroPowerReloadingForTwoTurns);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_PlayerPirate_01, 2.5f);
			break;
		case 105:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_BossBombardmentAOE_02, 2.5f);
			break;
		case 106:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_BossBombardmentAOEFails_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003DB6 RID: 15798 RVA: 0x0014451D File Offset: 0x0014271D
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		if (!(cardId == "DALA_716"))
		{
			if (cardId == "GVG_075")
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_PlayerShipCannon_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_PlayerFlyBy_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003DB7 RID: 15799 RVA: 0x00144533 File Offset: 0x00142733
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "TRL_127")
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_BossCannonBarrage_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040028EC RID: 10476
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_BossBombardmentAOE_02 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_BossBombardmentAOE_02.prefab:1731bb7643ecc6b458489fa31d238996");

	// Token: 0x040028ED RID: 10477
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_BossBombardmentAOEFails_01 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_BossBombardmentAOEFails_01.prefab:a26d844748379854b9646e306cff803f");

	// Token: 0x040028EE RID: 10478
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_BossCannonBarrage_01 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_BossCannonBarrage_01.prefab:4bc804a986342e147bebd557ba52281b");

	// Token: 0x040028EF RID: 10479
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_Death_02 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_Death_02.prefab:45b8dc245a322da4c91474d136e418f8");

	// Token: 0x040028F0 RID: 10480
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_DefeatPlayer_02 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_DefeatPlayer_02.prefab:27ad771ecb0a10043a32806d46fa2bbb");

	// Token: 0x040028F1 RID: 10481
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_EmoteResponse_02 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_EmoteResponse_02.prefab:fa042e4963a6a76428643fa7bde36bdb");

	// Token: 0x040028F2 RID: 10482
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_HeroPower_01 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_HeroPower_01.prefab:d56cfe445ec8038438debd15b5d6e93f");

	// Token: 0x040028F3 RID: 10483
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_HeroPower_02 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_HeroPower_02.prefab:1e67d0ff0c0d80e438703a769cf95fea");

	// Token: 0x040028F4 RID: 10484
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_HeroPower_03 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_HeroPower_03.prefab:aaf53c2f41f581b49b00432abcc48c1c");

	// Token: 0x040028F5 RID: 10485
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_HeroPower_04 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_HeroPower_04.prefab:a47b195542e5bd24288db03e1c86d54c");

	// Token: 0x040028F6 RID: 10486
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_HeroPower_05 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_HeroPower_05.prefab:05718bf4fbd395a41b2f00c16b4794e7");

	// Token: 0x040028F7 RID: 10487
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_01 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_01.prefab:789e2c223b4c61141a38735de6d8af9b");

	// Token: 0x040028F8 RID: 10488
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_02 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_02.prefab:5897826543b6e7843a01508fe6c4dafc");

	// Token: 0x040028F9 RID: 10489
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_03 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_03.prefab:f3f9b7609ba28754b99575dbeec6c052");

	// Token: 0x040028FA RID: 10490
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_04 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_04.prefab:e0b6303a4c1c5264e8aab32421aebbe9");

	// Token: 0x040028FB RID: 10491
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_Idle_01.prefab:8ca5709e2c4daa946826958ed8d4ac9c");

	// Token: 0x040028FC RID: 10492
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_Idle_03.prefab:25f799d4e18a2e04a885700ec82d8031");

	// Token: 0x040028FD RID: 10493
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_Idle_04 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_Idle_04.prefab:909314d819475ff429cb3b13064add7b");

	// Token: 0x040028FE RID: 10494
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_Intro_01 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_Intro_01.prefab:9b04f5c16afac3942b67b61d605b2db1");

	// Token: 0x040028FF RID: 10495
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_IntroEudora_01 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_IntroEudora_01.prefab:9e0f04a3ae2d3ad41b8c6dd2c6154d8a");

	// Token: 0x04002900 RID: 10496
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_PlayerFlyBy_01 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_PlayerFlyBy_01.prefab:bcceb68d0f83e2f4db121f7ff2bdd734");

	// Token: 0x04002901 RID: 10497
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_PlayerPirate_01 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_PlayerPirate_01.prefab:fae4fd562e967f1459d41c8086d4e755");

	// Token: 0x04002902 RID: 10498
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_PlayerShipCannon_01 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_PlayerShipCannon_01.prefab:0d7551091576d0447b3bae2589bf567d");

	// Token: 0x04002903 RID: 10499
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_Idle_01,
		DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_Idle_03,
		DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_Idle_04
	};

	// Token: 0x04002904 RID: 10500
	private static List<string> m_HeroPower = new List<string>
	{
		DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_HeroPower_01,
		DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_HeroPower_02,
		DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_HeroPower_03,
		DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_HeroPower_04,
		DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_HeroPower_05
	};

	// Token: 0x04002905 RID: 10501
	private static List<string> m_HeroPowerReloadingForOneTurn = new List<string>
	{
		DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_01,
		DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_03,
		DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_04
	};

	// Token: 0x04002906 RID: 10502
	private static List<string> m_HeroPowerReloadingForTwoTurns = new List<string>
	{
		DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_02,
		DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_03,
		DALA_Dungeon_Boss_70h.VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_04
	};

	// Token: 0x04002907 RID: 10503
	private HashSet<string> m_playedLines = new HashSet<string>();
}
