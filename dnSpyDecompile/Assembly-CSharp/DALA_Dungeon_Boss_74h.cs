using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000477 RID: 1143
public class DALA_Dungeon_Boss_74h : DALA_Dungeon
{
	// Token: 0x06003DE2 RID: 15842 RVA: 0x00145618 File Offset: 0x00143818
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_BossEmptyHand_01,
			DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_BossSoulfire_01,
			DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_Death_01,
			DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_DefeatPlayer_01,
			DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_EmoteResponse_01,
			DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_HeroPower_01,
			DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_HeroPower_02,
			DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_HeroPower_03,
			DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_HeroPower_04,
			DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_HeroPowerLarge_01,
			DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_HeroPowerLarge_02,
			DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_HeroPowerSmall_01,
			DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_HeroPowerSmall_02,
			DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_Idle_01,
			DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_Idle_02,
			DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_Idle_03,
			DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_Intro_01,
			DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_PlayerDemon_01,
			DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_PlayerDiscard_01,
			DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_PlayerDiscard_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003DE3 RID: 15843 RVA: 0x001457BC File Offset: 0x001439BC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_EmoteResponse_01;
	}

	// Token: 0x06003DE4 RID: 15844 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003DE5 RID: 15845 RVA: 0x001457F4 File Offset: 0x001439F4
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_74h.m_IdleLines;
	}

	// Token: 0x06003DE6 RID: 15846 RVA: 0x001457FB File Offset: 0x001439FB
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
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_74h.m_BossHeroPowerSmall);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_74h.m_BossHeroPower);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_74h.m_BossHeroPowerLarge);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_BossEmptyHand_01, 2.5f);
			break;
		case 105:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_PlayerDemon_01, 2.5f);
			break;
		case 106:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_74h.m_PlayerDiscard);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003DE7 RID: 15847 RVA: 0x00145811 File Offset: 0x00143A11
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
		yield break;
	}

	// Token: 0x06003DE8 RID: 15848 RVA: 0x00145827 File Offset: 0x00143A27
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
		if (cardId == "EX1_308")
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_BossSoulfire_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0400294F RID: 10575
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_BossEmptyHand_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_BossEmptyHand_01.prefab:6279c38e191bd6f40836841a135bc4a3");

	// Token: 0x04002950 RID: 10576
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_BossSoulfire_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_BossSoulfire_01.prefab:81610318de5ee41418f2a7f63691c0d6");

	// Token: 0x04002951 RID: 10577
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_Death_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_Death_01.prefab:550f70f20f886e343b994b051615a11f");

	// Token: 0x04002952 RID: 10578
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_DefeatPlayer_01.prefab:2a6cfb0036906b740be9d9ea89040241");

	// Token: 0x04002953 RID: 10579
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_EmoteResponse_01.prefab:a6444bb5a5063b54d96a1f2421c2899f");

	// Token: 0x04002954 RID: 10580
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_HeroPower_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_HeroPower_01.prefab:d0831452c2a17964fa2fdcbab8f1ce50");

	// Token: 0x04002955 RID: 10581
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_HeroPower_02 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_HeroPower_02.prefab:6b4eb949ad69e7641bd8801531956068");

	// Token: 0x04002956 RID: 10582
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_HeroPower_03 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_HeroPower_03.prefab:fe92466765ba592418d0f3e91bdf5a6e");

	// Token: 0x04002957 RID: 10583
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_HeroPower_04 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_HeroPower_04.prefab:010c3460ebe56b842accee8d3b164baa");

	// Token: 0x04002958 RID: 10584
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_HeroPowerLarge_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_HeroPowerLarge_01.prefab:d62f9bd347a604d418c35a414e45b6ee");

	// Token: 0x04002959 RID: 10585
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_HeroPowerLarge_02 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_HeroPowerLarge_02.prefab:a2d0d283167c27244a2758f594737696");

	// Token: 0x0400295A RID: 10586
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_HeroPowerSmall_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_HeroPowerSmall_01.prefab:812773881fe4f0b4bb4648dd840d160d");

	// Token: 0x0400295B RID: 10587
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_HeroPowerSmall_02 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_HeroPowerSmall_02.prefab:7f45c4c900d43214d890384045dd843f");

	// Token: 0x0400295C RID: 10588
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_Idle_01.prefab:144ce81414439bd4392c863e5f6f4859");

	// Token: 0x0400295D RID: 10589
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_Idle_02.prefab:23216cd59de29194fad9ce964168f9f9");

	// Token: 0x0400295E RID: 10590
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_Idle_03.prefab:1b45ff20c00d7f849a0f4157596902cc");

	// Token: 0x0400295F RID: 10591
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_Intro_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_Intro_01.prefab:6d244bdac3ea32a4184e5a900fb808ee");

	// Token: 0x04002960 RID: 10592
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_PlayerDemon_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_PlayerDemon_01.prefab:d9380cb44e4f34a44be70b098f40fb48");

	// Token: 0x04002961 RID: 10593
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_PlayerDiscard_01 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_PlayerDiscard_01.prefab:0c5b7454d1baed84dac0e313bae9fd51");

	// Token: 0x04002962 RID: 10594
	private static readonly AssetReference VO_DALA_BOSS_74h_Female_Human_PlayerDiscard_02 = new AssetReference("VO_DALA_BOSS_74h_Female_Human_PlayerDiscard_02.prefab:50f915c7be85e734bb7c4a2062e3df18");

	// Token: 0x04002963 RID: 10595
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04002964 RID: 10596
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_Idle_01,
		DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_Idle_02,
		DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_Idle_03
	};

	// Token: 0x04002965 RID: 10597
	private static List<string> m_BossHeroPower = new List<string>
	{
		DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_HeroPower_01,
		DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_HeroPower_02,
		DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_HeroPower_03,
		DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_HeroPower_04
	};

	// Token: 0x04002966 RID: 10598
	private static List<string> m_BossHeroPowerSmall = new List<string>
	{
		DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_HeroPowerSmall_01,
		DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_HeroPowerSmall_02
	};

	// Token: 0x04002967 RID: 10599
	private static List<string> m_BossHeroPowerLarge = new List<string>
	{
		DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_HeroPowerLarge_01,
		DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_HeroPowerLarge_02
	};

	// Token: 0x04002968 RID: 10600
	private static List<string> m_PlayerDiscard = new List<string>
	{
		DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_PlayerDiscard_01,
		DALA_Dungeon_Boss_74h.VO_DALA_BOSS_74h_Female_Human_PlayerDiscard_02
	};
}
