using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000451 RID: 1105
public class DALA_Dungeon_Boss_36h : DALA_Dungeon
{
	// Token: 0x06003C09 RID: 15369 RVA: 0x001386D8 File Offset: 0x001368D8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_BossBeast_02,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_BossBeast_03,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_Death_01,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_DefeatPlayer_01,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_EmoteResponse_01,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_01,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_02,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_03,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_04,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_05,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_06,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_Idle_01,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_Idle_02,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_Idle_03,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_Intro_01,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_IntroEudora_01,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_IntroKriziki_01,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_IntroOlBarkeye_01,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_IntroSqueamlish_01,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_Misc_01,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_01,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_02,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_03,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeastDeathrattle_01,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerBigBeast_01,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerBigBeast_02,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerHuffer_01,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerSmallBeast_01,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerSmallBeast_02,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerSSS_01,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerSSS_02,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_01,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_02,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_04,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_TurnStart_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003C0A RID: 15370 RVA: 0x0013896C File Offset: 0x00136B6C
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_01,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_02,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_03,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_04,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_05,
			DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_06
		};
	}

	// Token: 0x06003C0B RID: 15371 RVA: 0x001389DE File Offset: 0x00136BDE
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_EmoteResponse_01;
	}

	// Token: 0x06003C0C RID: 15372 RVA: 0x00138A16 File Offset: 0x00136C16
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_36h.m_IdleLines;
	}

	// Token: 0x06003C0D RID: 15373 RVA: 0x00138A20 File Offset: 0x00136C20
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Eudora")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_IntroEudora_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Kriziki")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_IntroKriziki_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Squeamlish")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_IntroSqueamlish_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Barkeye")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_IntroOlBarkeye_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		else
		{
			if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
	}

	// Token: 0x06003C0E RID: 15374 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003C0F RID: 15375 RVA: 0x00138BB1 File Offset: 0x00136DB1
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
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeastDeathrattle_01, 2.5f);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_36h.m_PlayerSmallBeast);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_36h.m_PlayerBeast);
			break;
		case 104:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_36h.m_PlayerBigBeast);
			break;
		case 105:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_36h.m_PlayerZombeast);
			break;
		case 106:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_36h.m_BossBeast);
			break;
		case 107:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_TurnStart_01, 2.5f);
			break;
		case 108:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_Misc_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003C10 RID: 15376 RVA: 0x00138BC7 File Offset: 0x00136DC7
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
		if (!(cardId == "NEW1_034"))
		{
			if (cardId == "DALA_704")
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_36h.m_PlayerSSS);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerHuffer_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003C11 RID: 15377 RVA: 0x00138BDD File Offset: 0x00136DDD
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
		yield break;
	}

	// Token: 0x04002534 RID: 9524
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_BossBeast_02 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_BossBeast_02.prefab:ba3fe70ad6249b54d888062bd52ed302");

	// Token: 0x04002535 RID: 9525
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_BossBeast_03 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_BossBeast_03.prefab:3cfd1a53963a591409e33c8b84c37d15");

	// Token: 0x04002536 RID: 9526
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_Death_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_Death_01.prefab:ab4af967e35860b46b347facdc1e5383");

	// Token: 0x04002537 RID: 9527
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_DefeatPlayer_01.prefab:f2ed950056a666c4cbfb4bb5bf94655c");

	// Token: 0x04002538 RID: 9528
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_EmoteResponse_01.prefab:926422888dfd13f48b1213ea18ee5801");

	// Token: 0x04002539 RID: 9529
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_01.prefab:95eb4c4cacf9d9c4a8c644820ed244c2");

	// Token: 0x0400253A RID: 9530
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_02 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_02.prefab:ddfa2c324fdc3a64eb919a25395b74cd");

	// Token: 0x0400253B RID: 9531
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_03 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_03.prefab:8f8469192fdc0214bb42ae50200edff0");

	// Token: 0x0400253C RID: 9532
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_04 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_04.prefab:f3b703916bb731a498cdf3358074a8fa");

	// Token: 0x0400253D RID: 9533
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_05 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_05.prefab:8740c8361ac74d7419173a88384535cb");

	// Token: 0x0400253E RID: 9534
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_06 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_HeroPower_06.prefab:19b5762e9cea0fd41a0109f73d510cf8");

	// Token: 0x0400253F RID: 9535
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_Idle_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_Idle_01.prefab:f0801adc980a4fc47ac6b84c889ad2c6");

	// Token: 0x04002540 RID: 9536
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_Idle_02 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_Idle_02.prefab:50448994fab7409419ad17dd5c601dce");

	// Token: 0x04002541 RID: 9537
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_Idle_03 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_Idle_03.prefab:036f8783eb6537c45ad6eaae2264fb9a");

	// Token: 0x04002542 RID: 9538
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_Intro_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_Intro_01.prefab:dfb744fdbf73d124dacb5779514f0a22");

	// Token: 0x04002543 RID: 9539
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_IntroEudora_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_IntroEudora_01.prefab:99a90329256d8c9479af79d0c03f5915");

	// Token: 0x04002544 RID: 9540
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_IntroKriziki_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_IntroKriziki_01.prefab:a77b9142eab443d4ba6e2d714b01c3ff");

	// Token: 0x04002545 RID: 9541
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_IntroOlBarkeye_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_IntroOlBarkeye_01.prefab:4c8d0d11d5b89db468a0d74d659e8f95");

	// Token: 0x04002546 RID: 9542
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_IntroSqueamlish_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_IntroSqueamlish_01.prefab:008b595d390cde04ab817bd4fbf555ad");

	// Token: 0x04002547 RID: 9543
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_Misc_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_Misc_01.prefab:bd476ea3e50613d4b900150d74b13f38");

	// Token: 0x04002548 RID: 9544
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_01.prefab:6981b902fcab85c42a0ac7c089160c32");

	// Token: 0x04002549 RID: 9545
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_02 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_02.prefab:0008ebc950ad2814abb328bba0b94383");

	// Token: 0x0400254A RID: 9546
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_03 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_03.prefab:14d2f1246194b3347883e2c5293d7dd2");

	// Token: 0x0400254B RID: 9547
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeastDeathrattle_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeastDeathrattle_01.prefab:199e6c7bf11f1eb4b8bb187cb261c5cd");

	// Token: 0x0400254C RID: 9548
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerBigBeast_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerBigBeast_01.prefab:3cc67530d93cc3f4b8b96a00803441b1");

	// Token: 0x0400254D RID: 9549
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerBigBeast_02 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerBigBeast_02.prefab:e97c1df1cd5bac5459646311b2603d8b");

	// Token: 0x0400254E RID: 9550
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerHuffer_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerHuffer_01.prefab:60ae5b903f68ffa4a885055bb2a7e960");

	// Token: 0x0400254F RID: 9551
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerSmallBeast_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerSmallBeast_01.prefab:352c3703e2428df4596948d5ff9cb142");

	// Token: 0x04002550 RID: 9552
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerSmallBeast_02 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerSmallBeast_02.prefab:81d67454d7b508d469b95d731cacd401");

	// Token: 0x04002551 RID: 9553
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerSSS_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerSSS_01.prefab:de8533db12fb958489a24b7e17113462");

	// Token: 0x04002552 RID: 9554
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerSSS_02 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerSSS_02.prefab:eba045b47e60a434086c1285cf9e890a");

	// Token: 0x04002553 RID: 9555
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_01.prefab:55e45142174377945b37e922591e1534");

	// Token: 0x04002554 RID: 9556
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_02 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_02.prefab:a26a8e004b300724885132d1123e4b31");

	// Token: 0x04002555 RID: 9557
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_04 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_04.prefab:01cb43de3a7012f409e923542311b376");

	// Token: 0x04002556 RID: 9558
	private static readonly AssetReference VO_DALA_BOSS_36h_Male_Dwarf_TurnStart_01 = new AssetReference("VO_DALA_BOSS_36h_Male_Dwarf_TurnStart_01.prefab:b453c7fa9eacf8745a5ceb10a0c571f8");

	// Token: 0x04002557 RID: 9559
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_Idle_01,
		DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_Idle_02,
		DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_Idle_03
	};

	// Token: 0x04002558 RID: 9560
	private static List<string> m_BossBeast = new List<string>
	{
		DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_BossBeast_02,
		DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_BossBeast_03
	};

	// Token: 0x04002559 RID: 9561
	private static List<string> m_PlayerBeast = new List<string>
	{
		DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_01,
		DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_02,
		DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerBeast_03
	};

	// Token: 0x0400255A RID: 9562
	private static List<string> m_PlayerBigBeast = new List<string>
	{
		DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerBigBeast_01,
		DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerBigBeast_02
	};

	// Token: 0x0400255B RID: 9563
	private static List<string> m_PlayerSmallBeast = new List<string>
	{
		DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerSmallBeast_01,
		DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerSmallBeast_02
	};

	// Token: 0x0400255C RID: 9564
	private static List<string> m_PlayerSSS = new List<string>
	{
		DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerSSS_01,
		DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerSSS_02
	};

	// Token: 0x0400255D RID: 9565
	private static List<string> m_PlayerZombeast = new List<string>
	{
		DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_01,
		DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_02,
		DALA_Dungeon_Boss_36h.VO_DALA_BOSS_36h_Male_Dwarf_PlayerZombeast_04
	};

	// Token: 0x0400255E RID: 9566
	private HashSet<string> m_playedLines = new HashSet<string>();
}
