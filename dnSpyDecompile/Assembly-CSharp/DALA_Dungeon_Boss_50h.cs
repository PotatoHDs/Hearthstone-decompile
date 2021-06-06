using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200045F RID: 1119
public class DALA_Dungeon_Boss_50h : DALA_Dungeon
{
	// Token: 0x06003CBA RID: 15546 RVA: 0x0013CE0C File Offset: 0x0013B00C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_Death_01,
			DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_DefeatPlayer_01,
			DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_EmoteResponse_01,
			DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_HeroicTrigger_01,
			DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_HeroicTrigger_02,
			DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_HeroicTrigger_03,
			DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_HeroPower_02,
			DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_HeroPower_03,
			DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_HeroPower_09,
			DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_Idle_02,
			DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_Idle_03,
			DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_Idle_05,
			DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_Intro_01,
			DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_PlayerDragon_01,
			DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_PlayerNozdormu_01,
			DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_PlayerTimeWarp_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003CBB RID: 15547 RVA: 0x0013CF70 File Offset: 0x0013B170
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_HeroPower_02,
			DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_HeroPower_03,
			DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_HeroPower_09
		};
	}

	// Token: 0x06003CBC RID: 15548 RVA: 0x0013CFA7 File Offset: 0x0013B1A7
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_50h.m_IdleLines;
	}

	// Token: 0x06003CBD RID: 15549 RVA: 0x0013CFAE File Offset: 0x0013B1AE
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_EmoteResponse_01;
	}

	// Token: 0x06003CBE RID: 15550 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003CBF RID: 15551 RVA: 0x0013CFE6 File Offset: 0x0013B1E6
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 101)
		{
			if (missionEvent != 102)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_BossHeroicTrigger);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_PlayerDragon_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003CC0 RID: 15552 RVA: 0x0013CFFC File Offset: 0x0013B1FC
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
		if (!(cardId == "EX1_560"))
		{
			if (cardId == "UNG_028t")
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_PlayerTimeWarp_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_PlayerNozdormu_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003CC1 RID: 15553 RVA: 0x0013D012 File Offset: 0x0013B212
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

	// Token: 0x04002688 RID: 9864
	private static readonly AssetReference VO_DALA_BOSS_50h_Female_Dragon_Death_01 = new AssetReference("VO_DALA_BOSS_50h_Female_Dragon_Death_01.prefab:cd20248dc32c0ce40a12c5e1b35db06e");

	// Token: 0x04002689 RID: 9865
	private static readonly AssetReference VO_DALA_BOSS_50h_Female_Dragon_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_50h_Female_Dragon_DefeatPlayer_01.prefab:6c501ac6d12542d4484cbc4e74b56263");

	// Token: 0x0400268A RID: 9866
	private static readonly AssetReference VO_DALA_BOSS_50h_Female_Dragon_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_50h_Female_Dragon_EmoteResponse_01.prefab:899064816af908f40ad51b82ab08387e");

	// Token: 0x0400268B RID: 9867
	private static readonly AssetReference VO_DALA_BOSS_50h_Female_Dragon_HeroicTrigger_01 = new AssetReference("VO_DALA_BOSS_50h_Female_Dragon_HeroicTrigger_01.prefab:7914715124540484aa92d9c27766874a");

	// Token: 0x0400268C RID: 9868
	private static readonly AssetReference VO_DALA_BOSS_50h_Female_Dragon_HeroicTrigger_02 = new AssetReference("VO_DALA_BOSS_50h_Female_Dragon_HeroicTrigger_02.prefab:9a8d640a3bab2974ba783592e90abf9c");

	// Token: 0x0400268D RID: 9869
	private static readonly AssetReference VO_DALA_BOSS_50h_Female_Dragon_HeroicTrigger_03 = new AssetReference("VO_DALA_BOSS_50h_Female_Dragon_HeroicTrigger_03.prefab:d1acebe4ad6e0dc4b95b783eead4b89a");

	// Token: 0x0400268E RID: 9870
	private static readonly AssetReference VO_DALA_BOSS_50h_Female_Dragon_HeroPower_02 = new AssetReference("VO_DALA_BOSS_50h_Female_Dragon_HeroPower_02.prefab:f6d989f13b306c043b7631968b784d24");

	// Token: 0x0400268F RID: 9871
	private static readonly AssetReference VO_DALA_BOSS_50h_Female_Dragon_HeroPower_03 = new AssetReference("VO_DALA_BOSS_50h_Female_Dragon_HeroPower_03.prefab:d77ca39562cf9c7429688fcb969eda08");

	// Token: 0x04002690 RID: 9872
	private static readonly AssetReference VO_DALA_BOSS_50h_Female_Dragon_HeroPower_09 = new AssetReference("VO_DALA_BOSS_50h_Female_Dragon_HeroPower_09.prefab:6df64686110d4f44790a3852ffe6caa1");

	// Token: 0x04002691 RID: 9873
	private static readonly AssetReference VO_DALA_BOSS_50h_Female_Dragon_Idle_02 = new AssetReference("VO_DALA_BOSS_50h_Female_Dragon_Idle_02.prefab:80e6c652c9243974588dc88b3569ab0d");

	// Token: 0x04002692 RID: 9874
	private static readonly AssetReference VO_DALA_BOSS_50h_Female_Dragon_Idle_03 = new AssetReference("VO_DALA_BOSS_50h_Female_Dragon_Idle_03.prefab:3a5ea39ac902e0b4aa81496597b35b16");

	// Token: 0x04002693 RID: 9875
	private static readonly AssetReference VO_DALA_BOSS_50h_Female_Dragon_Idle_05 = new AssetReference("VO_DALA_BOSS_50h_Female_Dragon_Idle_05.prefab:d7a38495b2a726f4f882998e0b146fca");

	// Token: 0x04002694 RID: 9876
	private static readonly AssetReference VO_DALA_BOSS_50h_Female_Dragon_Intro_01 = new AssetReference("VO_DALA_BOSS_50h_Female_Dragon_Intro_01.prefab:9aa5fa82bcface24e85281074c104bc5");

	// Token: 0x04002695 RID: 9877
	private static readonly AssetReference VO_DALA_BOSS_50h_Female_Dragon_PlayerDragon_01 = new AssetReference("VO_DALA_BOSS_50h_Female_Dragon_PlayerDragon_01.prefab:792291cfc262f524b85f6937b934ca0f");

	// Token: 0x04002696 RID: 9878
	private static readonly AssetReference VO_DALA_BOSS_50h_Female_Dragon_PlayerNozdormu_01 = new AssetReference("VO_DALA_BOSS_50h_Female_Dragon_PlayerNozdormu_01.prefab:c64936961dd2cea4e80948a1c8ed42ee");

	// Token: 0x04002697 RID: 9879
	private static readonly AssetReference VO_DALA_BOSS_50h_Female_Dragon_PlayerTimeWarp_01 = new AssetReference("VO_DALA_BOSS_50h_Female_Dragon_PlayerTimeWarp_01.prefab:437ac4f3cb4e35f4caa3a83aeaf0e046");

	// Token: 0x04002698 RID: 9880
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_Idle_02,
		DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_Idle_03,
		DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_Idle_05
	};

	// Token: 0x04002699 RID: 9881
	private List<string> m_BossHeroicTrigger = new List<string>
	{
		DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_HeroicTrigger_01,
		DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_HeroicTrigger_02,
		DALA_Dungeon_Boss_50h.VO_DALA_BOSS_50h_Female_Dragon_HeroicTrigger_03
	};

	// Token: 0x0400269A RID: 9882
	private HashSet<string> m_playedLines = new HashSet<string>();
}
