using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004A2 RID: 1186
public class ULDA_Dungeon_Boss_37h : ULDA_Dungeon
{
	// Token: 0x06003FEF RID: 16367 RVA: 0x00153290 File Offset: 0x00151490
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_BossMomentumSwing_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerDOOM_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerForbiddenWords_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerPlagueofDeath_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerPsychopomp_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowofDeath_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowWordDeath_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowWordPain_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_BossUniqueCard_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_DeathALT_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_02,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_03,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_EmoteResponse_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_02,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_03,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_04,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_05,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_02,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_03,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_04,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_02,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_03,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_04,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_05,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_02,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_03,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_04,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IdleRare_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IdleSpecial_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroFirstEncounter_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroGeneric_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1Retry_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1Retry_02,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase2Retry_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase2Retry_02,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_02,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_03,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialBrann_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialElise_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialFinley_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialReno_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_PhaseChange1_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_PhaseChange2_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerPlagueofDeath_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerBurgle_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerLichKing_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerShadowWordDeath_01,
			ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerSylvanas_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003FF0 RID: 16368 RVA: 0x00153664 File Offset: 0x00151864
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003FF1 RID: 16369 RVA: 0x0015366C File Offset: 0x0015186C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroGeneric_01;
		this.m_deathLine = ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_EmoteResponse_01;
	}

	// Token: 0x06003FF2 RID: 16370 RVA: 0x001536A4 File Offset: 0x001518A4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		int currentHealth = GameState.Get().GetOpposingSidePlayer().GetCurrentHealth();
		if (emoteType == EmoteType.START)
		{
			if (currentHealth == 300)
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1_01);
			}
			if (currentHealth <= 299 && currentHealth >= 201)
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1Retry_01);
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1Retry_02);
			}
			if (currentHealth <= 200 && currentHealth >= 101)
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase2Retry_01);
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase2Retry_02);
			}
			if (currentHealth <= 100 && currentHealth >= 0)
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_01);
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_02);
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_03);
			}
			if (cardId == "ULDA_Reno")
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialReno_01);
			}
			if (cardId == "ULDA_Brann")
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialBrann_01);
			}
			if (cardId == "ULDA_Elise")
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialElise_01);
			}
			if (cardId == "ULDA_Finley")
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialFinley_01);
			}
			this.m_introLine = base.PopRandomLine(this.m_IntroLines);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003FF3 RID: 16371 RVA: 0x001538C4 File Offset: 0x00151AC4
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
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_PhaseChange1_01, 2.5f);
			break;
		case 102:
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_PhaseChange2_01, 2.5f);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_DefeatPlayerLines);
			break;
		case 104:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPower1Lines);
			break;
		case 105:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPower2Lines);
			break;
		case 106:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPower3Lines);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003FF4 RID: 16372 RVA: 0x001538DA File Offset: 0x00151ADA
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
		if (!(cardId == "ULD_718"))
		{
			if (!(cardId == "AT_033") && !(cardId == "EX1_182"))
			{
				if (!(cardId == "ICC_314"))
				{
					if (!(cardId == "EX1_622"))
					{
						if (cardId == "EX1_016")
						{
							yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerSylvanas_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerShadowWordDeath_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerLichKing_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerBurgle_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerPlagueofDeath_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003FF5 RID: 16373 RVA: 0x001538F0 File Offset: 0x00151AF0
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
		if (num <= 2434013398U)
		{
			if (num <= 1064003026U)
			{
				if (num != 232623135U)
				{
					if (num == 1064003026U)
					{
						if (cardId == "EX1_622")
						{
							yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowWordDeath_01, 2.5f);
						}
					}
				}
				else if (cardId == "ULD_718")
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_BossPlagueOfDeath);
				}
			}
			else if (num != 1466445508U)
			{
				if (num == 2434013398U)
				{
					if (cardId == "OG_239")
					{
						yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerDOOM_01, 2.5f);
					}
				}
			}
			else if (cardId == "ULDA_BOSS_37t")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_BossUniqueCard_01, 2.5f);
			}
		}
		else if (num <= 3014298763U)
		{
			if (num != 2780397835U)
			{
				if (num == 3014298763U)
				{
					if (cardId == "ULD_286")
					{
						yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowofDeath_01, 2.5f);
					}
				}
			}
			else if (cardId == "ULD_268")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerPsychopomp_01, 2.5f);
			}
		}
		else if (num != 3258713493U)
		{
			if (num == 3820169915U)
			{
				if (cardId == "DAL_723")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerForbiddenWords_01, 2.5f);
				}
			}
		}
		else if (cardId == "CS2_234")
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowWordPain_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002CFF RID: 11519
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossMomentumSwing_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossMomentumSwing_01.prefab:30efd0cbba230e34c8321e2c0b19c90a");

	// Token: 0x04002D00 RID: 11520
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerDOOM_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerDOOM_01.prefab:767ddf9dc82dbee49a94936a7b4cfe64");

	// Token: 0x04002D01 RID: 11521
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerForbiddenWords_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerForbiddenWords_01.prefab:4b08d65c6439d0341a503275d3efba4e");

	// Token: 0x04002D02 RID: 11522
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerPlagueofDeath_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerPlagueofDeath_01.prefab:785e26c52f9e56945b9026d2838e9ce4");

	// Token: 0x04002D03 RID: 11523
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerPsychopomp_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerPsychopomp_01.prefab:1f276a3f90f33584f95aaa4f63ad684d");

	// Token: 0x04002D04 RID: 11524
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowofDeath_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowofDeath_01.prefab:5e05c5680cabe304bb57dbbc57edfd22");

	// Token: 0x04002D05 RID: 11525
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowWordDeath_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowWordDeath_01.prefab:04aa2bb88a833fc42a8c4569da461fc4");

	// Token: 0x04002D06 RID: 11526
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowWordPain_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowWordPain_01.prefab:44e81f1418cff0545aa0d3c7df362468");

	// Token: 0x04002D07 RID: 11527
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossUniqueCard_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossUniqueCard_01.prefab:dafca73a199865348b910fca55d6a2c6");

	// Token: 0x04002D08 RID: 11528
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_DeathALT_01.prefab:5c437d90222c27647ba633213d372ff9");

	// Token: 0x04002D09 RID: 11529
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_01.prefab:cebb8db8ec54e3b4ca079e77ec30e3c9");

	// Token: 0x04002D0A RID: 11530
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_02 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_02.prefab:f4e296a15892b8f4597cdbc4bed24d8d");

	// Token: 0x04002D0B RID: 11531
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_03 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_03.prefab:ac4b796862c48494183327fa71a924e6");

	// Token: 0x04002D0C RID: 11532
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_EmoteResponse_01.prefab:6bf90107695a95b41853415b8fd7ec65");

	// Token: 0x04002D0D RID: 11533
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_01.prefab:21abf67e3c956eb489160b264fc4d580");

	// Token: 0x04002D0E RID: 11534
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_02 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_02.prefab:652d00656c21a7b4b8e1a6512b682895");

	// Token: 0x04002D0F RID: 11535
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_03 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_03.prefab:e4072d24bdbae9443ad745518ea734c5");

	// Token: 0x04002D10 RID: 11536
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_04 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_04.prefab:c1397d7066f453f4abdb25656e0f0d94");

	// Token: 0x04002D11 RID: 11537
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_05 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_05.prefab:de616f2d19949184e8676e1d858b9aa6");

	// Token: 0x04002D12 RID: 11538
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_01.prefab:8c1d07c04936a72458c0a98a5f0f899d");

	// Token: 0x04002D13 RID: 11539
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_02 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_02.prefab:36b4014a7434769459bbd1df13eca168");

	// Token: 0x04002D14 RID: 11540
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_03 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_03.prefab:d2c3bace843c7ee44a9c6b66948b617b");

	// Token: 0x04002D15 RID: 11541
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_04 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_04.prefab:26322c7248e1bd14f9cc5b57c01db698");

	// Token: 0x04002D16 RID: 11542
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_01.prefab:2eb9651922e0fe94a8cb9f548b5243a2");

	// Token: 0x04002D17 RID: 11543
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_02 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_02.prefab:b9b5455a77d065546b19f15c255f8830");

	// Token: 0x04002D18 RID: 11544
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_03 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_03.prefab:81c52ebf0a96d9444ae250f7360cf01e");

	// Token: 0x04002D19 RID: 11545
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_04 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_04.prefab:1b118d7ea3d7d7c439a648ba9f92342e");

	// Token: 0x04002D1A RID: 11546
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_05 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_05.prefab:ccf16793723e95547bf374cee7adad3e");

	// Token: 0x04002D1B RID: 11547
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_01.prefab:43a0fbc639786274f9765fd8960943dc");

	// Token: 0x04002D1C RID: 11548
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_02 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_02.prefab:ed3d5c6013ccde54884341ff6c8133f4");

	// Token: 0x04002D1D RID: 11549
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_03 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_03.prefab:5c485dabe184bfe468dad99305563ea9");

	// Token: 0x04002D1E RID: 11550
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_04 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_04.prefab:d51ed2f37bc14644987bf9f1800fcc80");

	// Token: 0x04002D1F RID: 11551
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IdleRare_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IdleRare_01.prefab:c85572f053c1d10459eea5b41816981b");

	// Token: 0x04002D20 RID: 11552
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IdleSpecial_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IdleSpecial_01.prefab:01f95336e07ffd847a18acd0f864079a");

	// Token: 0x04002D21 RID: 11553
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroFirstEncounter_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroFirstEncounter_01.prefab:964987766f9f20a478454836999740a0");

	// Token: 0x04002D22 RID: 11554
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroGeneric_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroGeneric_01.prefab:33ac51b5728238c4d9253b6bd0c373d7");

	// Token: 0x04002D23 RID: 11555
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1_01.prefab:f1a43c53d10349b4f8cb71d168686437");

	// Token: 0x04002D24 RID: 11556
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1Retry_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1Retry_01.prefab:c9f47fb19f5238d4e8d97267fc602e70");

	// Token: 0x04002D25 RID: 11557
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1Retry_02 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1Retry_02.prefab:3f8533571205a284aaf70d3111fc4bd2");

	// Token: 0x04002D26 RID: 11558
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase2Retry_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase2Retry_01.prefab:3ead26d216897204889bf09275a44524");

	// Token: 0x04002D27 RID: 11559
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase2Retry_02 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase2Retry_02.prefab:6e39b5702d29d4f428834bf83e6e6cfd");

	// Token: 0x04002D28 RID: 11560
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_01.prefab:98e70f9dbc81f6140b2d5bfe6b898afa");

	// Token: 0x04002D29 RID: 11561
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_02 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_02.prefab:e8af0dfb823da6340acf59b0507aadca");

	// Token: 0x04002D2A RID: 11562
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_03 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_03.prefab:570fcef439125c84e8ae9a21fd89689e");

	// Token: 0x04002D2B RID: 11563
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialBrann_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialBrann_01.prefab:4b3ff177c8e6af44aa0ce734369a53d4");

	// Token: 0x04002D2C RID: 11564
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialElise_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialElise_01.prefab:5c6e9905abafe514cafdbd93d3c64e1e");

	// Token: 0x04002D2D RID: 11565
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialFinley_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialFinley_01.prefab:c90573ecab2ec9c4ea76b1d9239cfdbf");

	// Token: 0x04002D2E RID: 11566
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialReno_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialReno_01.prefab:368e6ec835b3d77408545636b752b429");

	// Token: 0x04002D2F RID: 11567
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_PhaseChange1_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_PhaseChange1_01.prefab:c22a7aba538d01242add3f4fcc90d89f");

	// Token: 0x04002D30 RID: 11568
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_PhaseChange2_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_PhaseChange2_01.prefab:d8de33988d71ce0419f67096fce01684");

	// Token: 0x04002D31 RID: 11569
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerPlagueofDeath_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerPlagueofDeath_01.prefab:2d4b99bf7f4bf4f41a6e543ab73c1934");

	// Token: 0x04002D32 RID: 11570
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerBurgle_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerBurgle_01.prefab:4722acb4877318b44adec789f6de3f57");

	// Token: 0x04002D33 RID: 11571
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerLichKing_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerLichKing_01.prefab:4e568e594b94acc46b000ebf244d0c53");

	// Token: 0x04002D34 RID: 11572
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerShadowWordDeath_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerShadowWordDeath_01.prefab:594d5109912d47f4188638e392f90136");

	// Token: 0x04002D35 RID: 11573
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerSylvanas_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerSylvanas_01.prefab:ede776e90cabdf74596da56f1d9e41f4");

	// Token: 0x04002D36 RID: 11574
	private List<string> m_DefeatPlayerLines = new List<string>
	{
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_01,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_02,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_03
	};

	// Token: 0x04002D37 RID: 11575
	private List<string> m_BossPlagueOfDeath = new List<string>
	{
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerPlagueofDeath_01,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_BossMomentumSwing_01
	};

	// Token: 0x04002D38 RID: 11576
	private List<string> m_IntroLines = new List<string>
	{
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroGeneric_01,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IntroFirstEncounter_01
	};

	// Token: 0x04002D39 RID: 11577
	private List<string> m_HeroPower1Lines = new List<string>
	{
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_01,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_02,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_03,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_04,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_05
	};

	// Token: 0x04002D3A RID: 11578
	private List<string> m_HeroPower2Lines = new List<string>
	{
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_01,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_02,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_03,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_04
	};

	// Token: 0x04002D3B RID: 11579
	private List<string> m_HeroPower3Lines = new List<string>
	{
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_01,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_02,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_03,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_04,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_05
	};

	// Token: 0x04002D3C RID: 11580
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_01,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_02,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_03,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_04,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_01,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_02,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_03,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_04,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_01,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_02,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_03,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_04,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_01,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_02,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_03,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_04,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IdleRare_01,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IdleRare_01,
		ULDA_Dungeon_Boss_37h.VO_ULDA_BOSS_37h_Female_PlagueLord_IdleSpecial_01
	};

	// Token: 0x04002D3D RID: 11581
	private HashSet<string> m_playedLines = new HashSet<string>();
}
