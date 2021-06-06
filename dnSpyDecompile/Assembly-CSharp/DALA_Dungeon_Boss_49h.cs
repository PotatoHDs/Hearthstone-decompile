using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200045E RID: 1118
public class DALA_Dungeon_Boss_49h : DALA_Dungeon
{
	// Token: 0x06003CAE RID: 15534 RVA: 0x0013C97C File Offset: 0x0013AB7C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_BossFrostSpell_01,
			DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_BossFrostSpell_02,
			DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_BossFrostSpell_04,
			DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_Death_01,
			DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_DefeatPlayer_02,
			DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_EmoteResponse_01,
			DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_HeroPower_01,
			DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_HeroPower_02,
			DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_HeroPower_03,
			DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_HeroPower_04,
			DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_Idle_01,
			DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_Idle_02,
			DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_Idle_03,
			DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_Intro_01,
			DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_Misc_01,
			DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_PlayerArcaneExplosion_01,
			DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_PlayerMalygos_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003CAF RID: 15535 RVA: 0x0013CAF0 File Offset: 0x0013ACF0
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_49h.m_IdleLines;
	}

	// Token: 0x06003CB0 RID: 15536 RVA: 0x0013CAF7 File Offset: 0x0013ACF7
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_EmoteResponse_01;
	}

	// Token: 0x06003CB1 RID: 15537 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003CB2 RID: 15538 RVA: 0x0013CB30 File Offset: 0x0013AD30
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Tekahn" && cardId != "DALA_Squeamlish")
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

	// Token: 0x06003CB3 RID: 15539 RVA: 0x0013CBE8 File Offset: 0x0013ADE8
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
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_49h.m_BossHeroPower);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_Misc_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003CB4 RID: 15540 RVA: 0x0013CBFE File Offset: 0x0013ADFE
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
		if (!(cardId == "CS2_025"))
		{
			if (cardId == "EX1_563")
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_PlayerMalygos_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_PlayerArcaneExplosion_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003CB5 RID: 15541 RVA: 0x0013CC14 File Offset: 0x0013AE14
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
		if (cardId == "CS2_024" || cardId == "CS2_026" || cardId == "CS2_037" || cardId == "DAL_577" || cardId == "EX1_179" || cardId == "CS2_031")
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_49h.m_BossFrostSpell);
		}
		yield break;
	}

	// Token: 0x04002673 RID: 9843
	private static readonly AssetReference VO_DALA_BOSS_49h_Female_Dragon_BossFrostSpell_01 = new AssetReference("VO_DALA_BOSS_49h_Female_Dragon_BossFrostSpell_01.prefab:aa25b431c7ba1d14aaa63a88ddcdbf2b");

	// Token: 0x04002674 RID: 9844
	private static readonly AssetReference VO_DALA_BOSS_49h_Female_Dragon_BossFrostSpell_02 = new AssetReference("VO_DALA_BOSS_49h_Female_Dragon_BossFrostSpell_02.prefab:14f7e34a19ab2e845b07cf758a53489e");

	// Token: 0x04002675 RID: 9845
	private static readonly AssetReference VO_DALA_BOSS_49h_Female_Dragon_BossFrostSpell_04 = new AssetReference("VO_DALA_BOSS_49h_Female_Dragon_BossFrostSpell_04.prefab:bf876403f6721c544a737b056e253dac");

	// Token: 0x04002676 RID: 9846
	private static readonly AssetReference VO_DALA_BOSS_49h_Female_Dragon_Death_01 = new AssetReference("VO_DALA_BOSS_49h_Female_Dragon_Death_01.prefab:72fd230d6c65a5045ba066c3834a5115");

	// Token: 0x04002677 RID: 9847
	private static readonly AssetReference VO_DALA_BOSS_49h_Female_Dragon_DefeatPlayer_02 = new AssetReference("VO_DALA_BOSS_49h_Female_Dragon_DefeatPlayer_02.prefab:ef721f400b460524aa4429a2009b5139");

	// Token: 0x04002678 RID: 9848
	private static readonly AssetReference VO_DALA_BOSS_49h_Female_Dragon_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_49h_Female_Dragon_EmoteResponse_01.prefab:ebb6ec89f444ee94bba1bb9266265dfc");

	// Token: 0x04002679 RID: 9849
	private static readonly AssetReference VO_DALA_BOSS_49h_Female_Dragon_HeroPower_01 = new AssetReference("VO_DALA_BOSS_49h_Female_Dragon_HeroPower_01.prefab:ada4fbddd58fe2a43b0bbeb44dc8b164");

	// Token: 0x0400267A RID: 9850
	private static readonly AssetReference VO_DALA_BOSS_49h_Female_Dragon_HeroPower_02 = new AssetReference("VO_DALA_BOSS_49h_Female_Dragon_HeroPower_02.prefab:4c209315128c1b1459fea1a9f99c6a46");

	// Token: 0x0400267B RID: 9851
	private static readonly AssetReference VO_DALA_BOSS_49h_Female_Dragon_HeroPower_03 = new AssetReference("VO_DALA_BOSS_49h_Female_Dragon_HeroPower_03.prefab:5617b5b8a9906e043a99945db0d6183d");

	// Token: 0x0400267C RID: 9852
	private static readonly AssetReference VO_DALA_BOSS_49h_Female_Dragon_HeroPower_04 = new AssetReference("VO_DALA_BOSS_49h_Female_Dragon_HeroPower_04.prefab:71a2407a5cf96a2439009baaa3626eff");

	// Token: 0x0400267D RID: 9853
	private static readonly AssetReference VO_DALA_BOSS_49h_Female_Dragon_Idle_01 = new AssetReference("VO_DALA_BOSS_49h_Female_Dragon_Idle_01.prefab:62f21d400545c114980599b44ba8950e");

	// Token: 0x0400267E RID: 9854
	private static readonly AssetReference VO_DALA_BOSS_49h_Female_Dragon_Idle_02 = new AssetReference("VO_DALA_BOSS_49h_Female_Dragon_Idle_02.prefab:a1654986a0955354297712d8d5feb989");

	// Token: 0x0400267F RID: 9855
	private static readonly AssetReference VO_DALA_BOSS_49h_Female_Dragon_Idle_03 = new AssetReference("VO_DALA_BOSS_49h_Female_Dragon_Idle_03.prefab:97f40a086caae7844bcafc1bfd36a42a");

	// Token: 0x04002680 RID: 9856
	private static readonly AssetReference VO_DALA_BOSS_49h_Female_Dragon_Intro_01 = new AssetReference("VO_DALA_BOSS_49h_Female_Dragon_Intro_01.prefab:f4777e851af7f004fa6d70ee6f1ca6fb");

	// Token: 0x04002681 RID: 9857
	private static readonly AssetReference VO_DALA_BOSS_49h_Female_Dragon_Misc_01 = new AssetReference("VO_DALA_BOSS_49h_Female_Dragon_Misc_01.prefab:be8fd937104d1d940b40ad0f2d385e0a");

	// Token: 0x04002682 RID: 9858
	private static readonly AssetReference VO_DALA_BOSS_49h_Female_Dragon_PlayerArcaneExplosion_01 = new AssetReference("VO_DALA_BOSS_49h_Female_Dragon_PlayerArcaneExplosion_01.prefab:6c686f3a65947894ea0c155198d18f67");

	// Token: 0x04002683 RID: 9859
	private static readonly AssetReference VO_DALA_BOSS_49h_Female_Dragon_PlayerMalygos_01 = new AssetReference("VO_DALA_BOSS_49h_Female_Dragon_PlayerMalygos_01.prefab:19af92f5f76ad414a9f1cd6f050a6b7e");

	// Token: 0x04002684 RID: 9860
	private static List<string> m_BossHeroPower = new List<string>
	{
		DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_HeroPower_01,
		DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_HeroPower_02,
		DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_HeroPower_03,
		DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_HeroPower_04
	};

	// Token: 0x04002685 RID: 9861
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_Idle_01,
		DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_Idle_02,
		DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_Idle_03
	};

	// Token: 0x04002686 RID: 9862
	private static List<string> m_BossFrostSpell = new List<string>
	{
		DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_BossFrostSpell_01,
		DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_BossFrostSpell_02,
		DALA_Dungeon_Boss_49h.VO_DALA_BOSS_49h_Female_Dragon_BossFrostSpell_04
	};

	// Token: 0x04002687 RID: 9863
	private HashSet<string> m_playedLines = new HashSet<string>();
}
