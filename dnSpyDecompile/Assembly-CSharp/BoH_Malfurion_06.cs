using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000531 RID: 1329
public class BoH_Malfurion_06 : BoH_Malfurion_Dungeon
{
	// Token: 0x06004871 RID: 18545 RVA: 0x0016DED1 File Offset: 0x0016C0D1
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.DO_OPENING_TAUNTS,
				false
			}
		};
	}

	// Token: 0x06004872 RID: 18546 RVA: 0x0018426C File Offset: 0x0018246C
	public BoH_Malfurion_06()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Malfurion_06.s_booleanOptions);
	}

	// Token: 0x06004873 RID: 18547 RVA: 0x00184310 File Offset: 0x00182510
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Malfurion_06.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeA_02,
			BoH_Malfurion_06.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeB_02,
			BoH_Malfurion_06.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeB_04,
			BoH_Malfurion_06.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6Intro_02,
			BoH_Malfurion_06.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6Victory_02,
			BoH_Malfurion_06.VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Death_01,
			BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6_Victory_01,
			BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6EmoteResponse_01,
			BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeC_01,
			BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeD_01,
			BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeD_02,
			BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_01,
			BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_02,
			BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_03,
			BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_01,
			BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_02,
			BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_03,
			BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Intro_01,
			BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Loss_01,
			BoH_Malfurion_06.VO_Story_Minion_Broll_Male_NightElf_Story_Malfurion_Mission6ExchangeB_01,
			BoH_Malfurion_06.VO_Story_Minion_Broll_Male_NightElf_Story_Malfurion_Mission6ExchangeB_03,
			BoH_Malfurion_06.VO_Story_Minion_Ysera_Female_Dragon_Story_Malfurion_Mission6_Victory_03,
			BoH_Malfurion_06.VO_Story_Minion_Ysera_Female_Dragon_Story_Malfurion_Mission6ExchangeA_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004874 RID: 18548 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004875 RID: 18549 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004876 RID: 18550 RVA: 0x001844E4 File Offset: 0x001826E4
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Malfurion_06.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6Intro_02);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004877 RID: 18551 RVA: 0x001844F3 File Offset: 0x001826F3
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x06004878 RID: 18552 RVA: 0x001844FB File Offset: 0x001826FB
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004879 RID: 18553 RVA: 0x00184503 File Offset: 0x00182703
	public override void OnCreateGame()
	{
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_GILFinalBoss;
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6EmoteResponse_01;
	}

	// Token: 0x0600487A RID: 18554 RVA: 0x00184528 File Offset: 0x00182728
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600487B RID: 18555 RVA: 0x001845AC File Offset: 0x001827AC
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 102)
		{
			if (missionEvent == 101)
			{
				yield return base.PlayLineAlways(BoH_Malfurion_06.BrollBrassRing, BoH_Malfurion_06.VO_Story_Minion_Broll_Male_NightElf_Story_Malfurion_Mission6ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_06.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeB_02, 2.5f);
				yield return base.PlayLineAlways(BoH_Malfurion_06.BrollBrassRing, BoH_Malfurion_06.VO_Story_Minion_Broll_Male_NightElf_Story_Malfurion_Mission6ExchangeB_03, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_06.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeB_04, 2.5f);
				goto IL_2D8;
			}
			if (missionEvent == 102)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6_Victory_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_2D8;
			}
		}
		else
		{
			if (missionEvent == 504)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_06.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6Victory_02, 2.5f);
				yield return base.PlayLineAlways(BoH_Malfurion_06.YseraBrassRing, BoH_Malfurion_06.VO_Story_Minion_Ysera_Female_Dragon_Story_Malfurion_Mission6_Victory_03, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_2D8;
			}
			if (missionEvent == 507)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_2D8;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_2D8:
		yield break;
	}

	// Token: 0x0600487C RID: 18556 RVA: 0x001845C2 File Offset: 0x001827C2
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

	// Token: 0x0600487D RID: 18557 RVA: 0x001845D8 File Offset: 0x001827D8
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

	// Token: 0x0600487E RID: 18558 RVA: 0x001845EE File Offset: 0x001827EE
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 1)
		{
			if (turn != 5)
			{
				if (turn == 7)
				{
					yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeD_01, 2.5f);
					yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeD_02, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeC_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(BoH_Malfurion_06.YseraBrassRing, BoH_Malfurion_06.VO_Story_Minion_Ysera_Female_Dragon_Story_Malfurion_Mission6ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_06.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeA_02, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003C3D RID: 15421
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Malfurion_06.InitBooleanOptions();

	// Token: 0x04003C3E RID: 15422
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeA_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeA_02.prefab:bb25de34e9808d942a93449fda3bed89");

	// Token: 0x04003C3F RID: 15423
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeB_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeB_02.prefab:f49830ba77a76ff4389a70350fcc3b73");

	// Token: 0x04003C40 RID: 15424
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeB_04 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6ExchangeB_04.prefab:8093cc5d853617b42b883360513f4e53");

	// Token: 0x04003C41 RID: 15425
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6Intro_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6Intro_02.prefab:19fd67bf211acb84ab265741fdde2900");

	// Token: 0x04003C42 RID: 15426
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6Victory_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission6Victory_02.prefab:70b41dea79be1a74fbf01d559bd97daf");

	// Token: 0x04003C43 RID: 15427
	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6_Victory_01 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6_Victory_01.prefab:5f29262c528e356428114f6ba56e3b71");

	// Token: 0x04003C44 RID: 15428
	private static readonly AssetReference VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Death_01 = new AssetReference("VO_Prologue_Xavius_Male_Satyr_Prologue_Xavius_Death_01.prefab:e9436bdf88abc0d4e9333e1520229159");

	// Token: 0x04003C45 RID: 15429
	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6EmoteResponse_01 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6EmoteResponse_01.prefab:5f3e9d0d524ecad4bbff645e8e698936");

	// Token: 0x04003C46 RID: 15430
	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeC_01 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeC_01.prefab:bc96057748a532545a959c3c8243b6ba");

	// Token: 0x04003C47 RID: 15431
	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeD_01 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeD_01.prefab:0167f7261d7621040a1dcc088d4b2194");

	// Token: 0x04003C48 RID: 15432
	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeD_02 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6ExchangeD_02.prefab:0810cc106b4284345acb8b87227696b0");

	// Token: 0x04003C49 RID: 15433
	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_01 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_01.prefab:9953b28ad0cb60c49b6e17fb9be382a3");

	// Token: 0x04003C4A RID: 15434
	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_02 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_02.prefab:20357308e663c404fa7772e3ea48dc6e");

	// Token: 0x04003C4B RID: 15435
	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_03 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_03.prefab:26be946be95128d45b37cf0556018d26");

	// Token: 0x04003C4C RID: 15436
	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_01 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_01.prefab:afd7cfb7a1e961647b7037525e2f2201");

	// Token: 0x04003C4D RID: 15437
	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_02 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_02.prefab:2ca3aeefa6bf7524882a27315c99e875");

	// Token: 0x04003C4E RID: 15438
	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_03 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_03.prefab:3b0c0d926d876ff4682e5d6244facb09");

	// Token: 0x04003C4F RID: 15439
	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Intro_01.prefab:9155ebe461da8ef44a3aa7cd31c7c1b2");

	// Token: 0x04003C50 RID: 15440
	private static readonly AssetReference VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Loss_01 = new AssetReference("VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Loss_01.prefab:16f68c87e82d48749abf8979da8b7e5d");

	// Token: 0x04003C51 RID: 15441
	private static readonly AssetReference VO_Story_Minion_Broll_Male_NightElf_Story_Malfurion_Mission6ExchangeB_01 = new AssetReference("VO_Story_Minion_Broll_Male_NightElf_Story_Malfurion_Mission6ExchangeB_01.prefab:7cd08f049449d1a4b89f07b1d4d488eb");

	// Token: 0x04003C52 RID: 15442
	private static readonly AssetReference VO_Story_Minion_Broll_Male_NightElf_Story_Malfurion_Mission6ExchangeB_03 = new AssetReference("VO_Story_Minion_Broll_Male_NightElf_Story_Malfurion_Mission6ExchangeB_03.prefab:95cad737db484074a90262d01e0c5c33");

	// Token: 0x04003C53 RID: 15443
	private static readonly AssetReference VO_Story_Minion_Ysera_Female_Dragon_Story_Malfurion_Mission6_Victory_03 = new AssetReference("VO_Story_Minion_Ysera_Female_Dragon_Story_Malfurion_Mission6_Victory_03.prefab:88ef7dd0e33ade24faef4589a21b4340");

	// Token: 0x04003C54 RID: 15444
	private static readonly AssetReference VO_Story_Minion_Ysera_Female_Dragon_Story_Malfurion_Mission6ExchangeA_01 = new AssetReference("VO_Story_Minion_Ysera_Female_Dragon_Story_Malfurion_Mission6ExchangeA_01.prefab:e54c45896f345024b8e4d788d32b74e8");

	// Token: 0x04003C55 RID: 15445
	public static readonly AssetReference YseraBrassRing = new AssetReference("Ysera_BrassRing_Quote.prefab:1b5ee7911e0cc0f48bff1d9ea60a95e1");

	// Token: 0x04003C56 RID: 15446
	public static readonly AssetReference BrollBrassRing = new AssetReference("Broll_BrassRing_Quote.prefab:1bfe5acde48846249b4b7716c3ff0d8c");

	// Token: 0x04003C57 RID: 15447
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_01,
		BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_02,
		BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6HeroPower_03
	};

	// Token: 0x04003C58 RID: 15448
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_01,
		BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_02,
		BoH_Malfurion_06.VO_Story_Hero_Xavius_Male_Satyr_Story_Malfurion_Mission6Idle_03
	};

	// Token: 0x04003C59 RID: 15449
	private HashSet<string> m_playedLines = new HashSet<string>();
}
