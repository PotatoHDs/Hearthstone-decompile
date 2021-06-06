using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004A3 RID: 1187
public class ULDA_Dungeon_Boss_38h : ULDA_Dungeon
{
	// Token: 0x06003FFB RID: 16379 RVA: 0x00153F3C File Offset: 0x0015213C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_BossMomentumSwing_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerCleverDisguise_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerDevourMind_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerMindControl_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerPlagueofMadness_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerShadowMadness_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerSpreadingMadness_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerSurrendertoMadness_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerWastewanderSapper_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_DeathALT_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_DefeatPlayer_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_DefeatPlayer_02,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_DefeatPlayer_03,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_EmoteResponse_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_02,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_03,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_04,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_02,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_03,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_04,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_02,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_03,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_04,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_05,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_02,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_03,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_04,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_05,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IdleSpecial_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroFirstEncounter_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroGeneric_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1Retry_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1Retry_02,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase2Retry_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase2Retry_02,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_02,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_03,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialBrann_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialElise_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialFinley_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialReno_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_PhaseChange1_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_PhaseChange2_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerBlatantDecoy_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMassHysteria_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMindVision_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMoguCultist_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerPlagueofMadness_01,
			ULDA_Dungeon_Boss_38h.VO_ULDA_Elise_Female_NightElf_STORY_Reno_Goes_Mad_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003FFC RID: 16380 RVA: 0x00154310 File Offset: 0x00152510
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003FFD RID: 16381 RVA: 0x00154318 File Offset: 0x00152518
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_EmoteResponse_01;
	}

	// Token: 0x06003FFE RID: 16382 RVA: 0x00154340 File Offset: 0x00152540
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Gameplay.Get().StartCoroutine(this.PlayMultipleVOLinesForEmotes(emoteType, emoteSpell));
	}

	// Token: 0x06003FFF RID: 16383 RVA: 0x00154355 File Offset: 0x00152555
	protected IEnumerator PlayMultipleVOLinesForEmotes(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string playerHeroCardID = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		int currentHealth = GameState.Get().GetOpposingSidePlayer().GetCurrentHealth();
		if (emoteType == EmoteType.START)
		{
			if (currentHealth == 300)
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1_01);
			}
			if (currentHealth <= 299 && currentHealth >= 201)
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1Retry_01);
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1Retry_02);
			}
			if (currentHealth <= 200 && currentHealth >= 101)
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase2Retry_01);
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase2Retry_02);
			}
			if (currentHealth <= 100 && currentHealth >= 0)
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_01);
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_02);
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_03);
			}
			if (playerHeroCardID == "ULDA_Reno")
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialReno_01);
			}
			if (playerHeroCardID == "ULDA_Brann")
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialBrann_01);
			}
			if (playerHeroCardID == "ULDA_Elise")
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialElise_01);
			}
			if (playerHeroCardID == "ULDA_Finley")
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialFinley_01);
			}
			this.m_introLine = base.PopRandomLine(this.m_IntroLines);
			yield return base.PlayBossLine(enemyActor, this.m_introLine, 2.5f);
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			if (playerHeroCardID == "ULDA_Reno")
			{
				yield return base.PlayLineOnlyOnce(ULDA_Dungeon_Boss_38h.Tombs_of_Terror_Elise_BrassRing_Quote, ULDA_Dungeon_Boss_38h.VO_ULDA_Elise_Female_NightElf_STORY_Reno_Goes_Mad_01, 2.5f);
			}
			else
			{
				yield return base.PlayBossLine(enemyActor, this.m_standardEmoteResponseLine, 2.5f);
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06004000 RID: 16384 RVA: 0x0015436B File Offset: 0x0015256B
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
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_PhaseChange1_01, 2.5f);
			break;
		case 102:
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_PhaseChange2_01, 2.5f);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPower1Lines);
			break;
		case 104:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPower2Lines);
			break;
		case 105:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPower3Lines);
			break;
		case 106:
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IdleSpecial_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06004001 RID: 16385 RVA: 0x00154381 File Offset: 0x00152581
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "ULD_706"))
		{
			if (!(cardId == "TRL_258"))
			{
				if (!(cardId == "CS2_003"))
				{
					if (!(cardId == "ULD_705"))
					{
						if (cardId == "ULD_715")
						{
							yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerPlagueofMadness_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMoguCultist_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMindVision_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMassHysteria_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerBlatantDecoy_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004002 RID: 16386 RVA: 0x00154397 File Offset: 0x00152597
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 1672305406U)
		{
			if (num <= 494715900U)
			{
				if (num != 282955992U)
				{
					if (num == 494715900U)
					{
						if (cardId == "OG_116")
						{
							yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerSpreadingMadness_01, 2.5f);
						}
					}
				}
				else if (cardId == "ULD_715")
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_BossPlagueOfMadness);
				}
			}
			else if (num != 924988432U)
			{
				if (num == 1672305406U)
				{
					if (cardId == "ICC_207")
					{
						yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerDevourMind_01, 2.5f);
					}
				}
			}
			else if (cardId == "ULD_328")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerCleverDisguise_01, 2.5f);
			}
		}
		else if (num <= 2636961905U)
		{
			if (num != 1855124202U)
			{
				if (num == 2636961905U)
				{
					if (cardId == "TRL_500")
					{
						yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerSurrendertoMadness_01, 2.5f);
					}
				}
			}
			else if (cardId == "EX1_334")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerShadowMadness_01, 2.5f);
			}
		}
		else if (num != 3114964477U)
		{
			if (num == 4237075050U)
			{
				if (cardId == "CS1_113")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerMindControl_01, 2.5f);
				}
			}
		}
		else if (cardId == "ULD_280")
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerWastewanderSapper_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002D3E RID: 11582
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossMomentumSwing_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossMomentumSwing_01.prefab:5a3640ac1a8e6914abd67ef931c48e06");

	// Token: 0x04002D3F RID: 11583
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerCleverDisguise_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerCleverDisguise_01.prefab:923bb9ccd09f6a4458c0f8209125ef40");

	// Token: 0x04002D40 RID: 11584
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerDevourMind_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerDevourMind_01.prefab:4119495ef194bd745b8e4dfe43d28abd");

	// Token: 0x04002D41 RID: 11585
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerMindControl_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerMindControl_01.prefab:c0a19e05293fcb24fb27ea792948309b");

	// Token: 0x04002D42 RID: 11586
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerPlagueofMadness_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerPlagueofMadness_01.prefab:10e49746ada1c674cbaefdaa99d58aab");

	// Token: 0x04002D43 RID: 11587
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerShadowMadness_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerShadowMadness_01.prefab:71c83982df42acf458656cb35ded1352");

	// Token: 0x04002D44 RID: 11588
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerSpreadingMadness_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerSpreadingMadness_01.prefab:280983f4ac3d3bb4fa727890bbdc18a6");

	// Token: 0x04002D45 RID: 11589
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerSurrendertoMadness_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerSurrendertoMadness_01.prefab:a04dd942a51b5004faf816171e9453ee");

	// Token: 0x04002D46 RID: 11590
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerWastewanderSapper_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerWastewanderSapper_01.prefab:f116cf709c127f345982168a67031be9");

	// Token: 0x04002D47 RID: 11591
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_DeathALT_01.prefab:7e86996565b80264fbd2162de15f17c6");

	// Token: 0x04002D48 RID: 11592
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_DefeatPlayer_01.prefab:a5441912fe34df14cbe6be982ca03cb6");

	// Token: 0x04002D49 RID: 11593
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_DefeatPlayer_02 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_DefeatPlayer_02.prefab:35ec72459bd5f2640851721c75c3d5ab");

	// Token: 0x04002D4A RID: 11594
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_DefeatPlayer_03 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_DefeatPlayer_03.prefab:aedeccf3d2f6dc147861ab1997c1a305");

	// Token: 0x04002D4B RID: 11595
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_EmoteResponse_01.prefab:99cbf0bbdfc57194ba49029d5a9bdf3b");

	// Token: 0x04002D4C RID: 11596
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_01.prefab:9fd94ec5ab2f83a46a607bf657f8bb53");

	// Token: 0x04002D4D RID: 11597
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_02 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_02.prefab:b5be1d08ca34ee34eb489771e9ab517d");

	// Token: 0x04002D4E RID: 11598
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_03 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_03.prefab:4b4984ed23a12c2488caed8c405d12e9");

	// Token: 0x04002D4F RID: 11599
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_04 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_04.prefab:c294c76aa7b2ded489eabf5079f4a32d");

	// Token: 0x04002D50 RID: 11600
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_01.prefab:e807765f9b0b56946b553e5e5e85da82");

	// Token: 0x04002D51 RID: 11601
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_02 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_02.prefab:11f8fc2d8b51e254ca751ed84f401ff0");

	// Token: 0x04002D52 RID: 11602
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_03 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_03.prefab:13beadc2b3065e24485a42af64bc08aa");

	// Token: 0x04002D53 RID: 11603
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_04 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_04.prefab:384a4f55f17180249b1875cfc8ddee26");

	// Token: 0x04002D54 RID: 11604
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_01.prefab:f1a2a97caf82cb246adff4ce8ebd071b");

	// Token: 0x04002D55 RID: 11605
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_02 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_02.prefab:986e87a46b35f2b45bbba1900e529881");

	// Token: 0x04002D56 RID: 11606
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_03 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_03.prefab:6a8da03a3b26f57448864644a009980d");

	// Token: 0x04002D57 RID: 11607
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_04 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_04.prefab:a880df77698485f4ebafab5acc77c4f0");

	// Token: 0x04002D58 RID: 11608
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_05 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_05.prefab:2d9aee7d48de9b24891441114297ecf1");

	// Token: 0x04002D59 RID: 11609
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_01.prefab:41979b81a51560346adee6bb3b3253ec");

	// Token: 0x04002D5A RID: 11610
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_02 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_02.prefab:d358a93940501e54db74f8e41c2112bd");

	// Token: 0x04002D5B RID: 11611
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_03 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_03.prefab:f5bf3e6094154be4dbac592b5e4c974e");

	// Token: 0x04002D5C RID: 11612
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_04 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_04.prefab:b563e333f27d5894584b95709904580c");

	// Token: 0x04002D5D RID: 11613
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_05 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_05.prefab:43b947bf763c0e6408edad722edd02ef");

	// Token: 0x04002D5E RID: 11614
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IdleSpecial_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IdleSpecial_01.prefab:4e495d3ccc7e8794f940df95153a1695");

	// Token: 0x04002D5F RID: 11615
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroFirstEncounter_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroFirstEncounter_01.prefab:6a380d69ceff5464783ba3d3149bf11d");

	// Token: 0x04002D60 RID: 11616
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroGeneric_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroGeneric_01.prefab:5116372b2f600114aa4203b1240462f0");

	// Token: 0x04002D61 RID: 11617
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1_01.prefab:000aea2a4cbe58f4a813682df959d712");

	// Token: 0x04002D62 RID: 11618
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1Retry_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1Retry_01.prefab:1af50576ef843ac4c9beced8352a944f");

	// Token: 0x04002D63 RID: 11619
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1Retry_02 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1Retry_02.prefab:83b4ff029ac96624db2fb876dc62e1a0");

	// Token: 0x04002D64 RID: 11620
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase2Retry_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase2Retry_01.prefab:d019d84d20d774241a595a15d77d772d");

	// Token: 0x04002D65 RID: 11621
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase2Retry_02 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase2Retry_02.prefab:e92d4ed0cd41b1b49b5202aa3e369d96");

	// Token: 0x04002D66 RID: 11622
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_01.prefab:8f650589f3810a04594f72e57616c44f");

	// Token: 0x04002D67 RID: 11623
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_02 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_02.prefab:5f9aa2f0ba4181b4db8b6103c3a7b9f5");

	// Token: 0x04002D68 RID: 11624
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_03 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_03.prefab:7df11d6343016f943996e52018726c67");

	// Token: 0x04002D69 RID: 11625
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialBrann_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialBrann_01.prefab:291ad5302e29fbb4e8ed46cf2a6b1b34");

	// Token: 0x04002D6A RID: 11626
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialElise_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialElise_01.prefab:47f6cea5ca369124babc1e1e2dfbf939");

	// Token: 0x04002D6B RID: 11627
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialFinley_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialFinley_01.prefab:aa27b9060a0e0de45917e4fd3bd41664");

	// Token: 0x04002D6C RID: 11628
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialReno_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialReno_01.prefab:d6bd5abdee2df76459b0216498e47bbf");

	// Token: 0x04002D6D RID: 11629
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_PhaseChange1_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_PhaseChange1_01.prefab:b05667254eba01544ae2bdc8112a3c23");

	// Token: 0x04002D6E RID: 11630
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_PhaseChange2_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_PhaseChange2_01.prefab:8b80efed8d6e46f4b87f3aebce1c1e6e");

	// Token: 0x04002D6F RID: 11631
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerBlatantDecoy_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerBlatantDecoy_01.prefab:3927e0bafb038234ea1f2503ce924548");

	// Token: 0x04002D70 RID: 11632
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMassHysteria_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMassHysteria_01.prefab:a02a2ca93246838488dc559a16b93903");

	// Token: 0x04002D71 RID: 11633
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMindVision_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMindVision_01.prefab:66888c0ae26c2f04596b8e9697c36e9a");

	// Token: 0x04002D72 RID: 11634
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMoguCultist_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMoguCultist_01.prefab:1f3d8c649412af94bb1f2df5dae07f0a");

	// Token: 0x04002D73 RID: 11635
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerPlagueofMadness_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerPlagueofMadness_01.prefab:2a8fe3d79d4cc8046af64f8ca3c76dce");

	// Token: 0x04002D74 RID: 11636
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Reno_Goes_Mad_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Reno_Goes_Mad_01.prefab:fcba06b37e7f52243990b6c23649d5fe");

	// Token: 0x04002D75 RID: 11637
	private static readonly AssetReference Tombs_of_Terror_Elise_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Elise_BrassRing_Quote.prefab:6e9e8faaae29f834183122d7ea9be68d");

	// Token: 0x04002D76 RID: 11638
	private List<string> m_BossPlagueOfMadness = new List<string>
	{
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerPlagueofMadness_01,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_BossMomentumSwing_01
	};

	// Token: 0x04002D77 RID: 11639
	private List<string> m_HeroPower1Lines = new List<string>
	{
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_01,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_02,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_03,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_04
	};

	// Token: 0x04002D78 RID: 11640
	private List<string> m_HeroPower2Lines = new List<string>
	{
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_01,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_02,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_03,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_04
	};

	// Token: 0x04002D79 RID: 11641
	private List<string> m_HeroPower3Lines = new List<string>
	{
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_01,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_02,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_03,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_04,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_05
	};

	// Token: 0x04002D7A RID: 11642
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_01,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_02,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_03,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_04,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_05,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_01,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_02,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_03,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_04,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_05,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_01,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_02,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_03,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_04,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_05,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IdleSpecial_01
	};

	// Token: 0x04002D7B RID: 11643
	private List<string> m_IntroLines = new List<string>
	{
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroGeneric_01,
		ULDA_Dungeon_Boss_38h.VO_ULDA_BOSS_38h_Male_PlagueLord_IntroFirstEncounter_01
	};

	// Token: 0x04002D7C RID: 11644
	private HashSet<string> m_playedLines = new HashSet<string>();
}
