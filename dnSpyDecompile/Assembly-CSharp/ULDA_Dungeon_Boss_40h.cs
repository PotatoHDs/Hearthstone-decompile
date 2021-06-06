using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004A5 RID: 1189
public class ULDA_Dungeon_Boss_40h : ULDA_Dungeon
{
	// Token: 0x06004014 RID: 16404 RVA: 0x0015572C File Offset: 0x0015392C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_BossMomentumSwing_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerArmagedillo_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerBattleRage_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerOctosari_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerPlagueofWrath_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerReinforcements_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerRestlessMummy_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerSulthraze_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerTempleBerserker_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerWoecleaver_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_DeathALT_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_DefeatPlayer_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_DefeatPlayer_02,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_DefeatPlayer_03,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_EmoteResponse_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_02,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_03,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_04,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_02,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_03,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_04,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_02,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_03,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_04,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_05,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_02,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_03,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_04,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_05,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IdleSpecial_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroFirstEncounter_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroGeneric_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1Retry_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1Retry_02,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase2Retry_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase2Retry_02,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_02,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_03,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialBrann_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialBrann2_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialElise_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialElise2_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialFinley_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialFinley2_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialReno_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialReno2_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_PhaseChange1_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_PhaseChange2_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerBeamingSidekick_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerDesertHare_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerInjuredTolvir_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerInnerRage_01,
			ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerPlagueofWrath_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004015 RID: 16405 RVA: 0x00155B40 File Offset: 0x00153D40
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06004016 RID: 16406 RVA: 0x00155B48 File Offset: 0x00153D48
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_EmoteResponse_01;
	}

	// Token: 0x06004017 RID: 16407 RVA: 0x00155B70 File Offset: 0x00153D70
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Gameplay.Get().StartCoroutine(this.PlayMultipleVOLinesForEmotes(emoteType, emoteSpell));
	}

	// Token: 0x06004018 RID: 16408 RVA: 0x00155B85 File Offset: 0x00153D85
	protected IEnumerator PlayMultipleVOLinesForEmotes(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		int currentHealth = GameState.Get().GetOpposingSidePlayer().GetCurrentHealth();
		if (emoteType == EmoteType.START)
		{
			if (currentHealth == 300)
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1_01);
			}
			if (currentHealth <= 299 && currentHealth >= 201)
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1Retry_01);
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1Retry_02);
			}
			if (currentHealth <= 200 && currentHealth >= 101)
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase2Retry_01);
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase2Retry_02);
			}
			if (currentHealth <= 100 && currentHealth >= 0)
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_01);
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_02);
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_03);
			}
			if (cardId == "ULDA_Reno")
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialReno_01);
			}
			if (cardId == "ULDA_Brann")
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialBrann_01);
			}
			if (cardId == "ULDA_Elise")
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialElise_01);
			}
			if (cardId == "ULDA_Finley")
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialFinley_01);
			}
			this.m_introLine = base.PopRandomLine(this.m_IntroLines);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			if (this.m_introLine == ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialReno_01)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialReno2_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			}
			else if (this.m_introLine == ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialBrann_01)
			{
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialBrann2_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			}
			else if (this.m_introLine == ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialElise_01)
			{
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialElise2_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			}
			else if (this.m_introLine == ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialFinley_01)
			{
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialFinley2_01, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			}
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06004019 RID: 16409 RVA: 0x00155B9B File Offset: 0x00153D9B
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
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_PhaseChange1_01, 2.5f);
			break;
		case 102:
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_PhaseChange2_01, 2.5f);
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
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IdleSpecial_01, 2.5f);
			break;
		case 107:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_BossMomentumSwing_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x0600401A RID: 16410 RVA: 0x00155BB1 File Offset: 0x00153DB1
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
		if (!(cardId == "ULD_191"))
		{
			if (!(cardId == "ULD_719"))
			{
				if (!(cardId == "ULD_271"))
				{
					if (!(cardId == "EX1_607"))
					{
						if (cardId == "ULD_707")
						{
							yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerPlagueofWrath_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerInnerRage_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerInjuredTolvir_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerDesertHare_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerBeamingSidekick_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600401B RID: 16411 RVA: 0x00155BC7 File Offset: 0x00153DC7
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
		if (num <= 1527488659U)
		{
			if (num <= 598866080U)
			{
				if (num != 316364135U)
				{
					if (num == 598866080U)
					{
						if (cardId == "ULD_258")
						{
							yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerArmagedillo_01, 2.5f);
						}
					}
				}
				else if (cardId == "ULD_707")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerPlagueofWrath_01, 2.5f);
				}
			}
			else if (num != 833752746U)
			{
				if (num == 1527488659U)
				{
					if (cardId == "ULD_185")
					{
						yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerTempleBerserker_01, 2.5f);
					}
				}
			}
			else if (cardId == "ULD_256")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerReinforcements_01, 2.5f);
			}
		}
		else if (num <= 1828058778U)
		{
			if (num != 1788088054U)
			{
				if (num == 1828058778U)
				{
					if (cardId == "ULD_177")
					{
						yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerOctosari_01, 2.5f);
					}
				}
			}
			else if (cardId == "TRL_325")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerSulthraze_01, 2.5f);
			}
		}
		else if (num != 2747136787U)
		{
			if (num != 3484289697U)
			{
				if (num == 4034537386U)
				{
					if (cardId == "EX1_392")
					{
						yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerBattleRage_01, 2.5f);
					}
				}
			}
			else if (cardId == "LOOT_380")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerWoecleaver_01, 2.5f);
			}
		}
		else if (cardId == "ULD_206")
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerRestlessMummy_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002DBF RID: 11711
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossMomentumSwing_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossMomentumSwing_01.prefab:22128e140844f7b4898e1ddf3d15e57a");

	// Token: 0x04002DC0 RID: 11712
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerArmagedillo_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerArmagedillo_01.prefab:0abf25da41474e446976c96f61dc2830");

	// Token: 0x04002DC1 RID: 11713
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerBattleRage_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerBattleRage_01.prefab:438b5d2c9ca70ee44b6c2aa2c743a55a");

	// Token: 0x04002DC2 RID: 11714
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerOctosari_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerOctosari_01.prefab:f7627e1971003f04198e0cbb0367efbb");

	// Token: 0x04002DC3 RID: 11715
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerPlagueofWrath_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerPlagueofWrath_01.prefab:c87066e231bf2744aa9838aebc7c16a6");

	// Token: 0x04002DC4 RID: 11716
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerReinforcements_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerReinforcements_01.prefab:356fa7feeaed8294599051eace96f4bb");

	// Token: 0x04002DC5 RID: 11717
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerRestlessMummy_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerRestlessMummy_01.prefab:cf1425e7633277a4ebad4133f160c3a1");

	// Token: 0x04002DC6 RID: 11718
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerSulthraze_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerSulthraze_01.prefab:118d6761f62926b49b0155900f483ef1");

	// Token: 0x04002DC7 RID: 11719
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerTempleBerserker_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerTempleBerserker_01.prefab:e2c1d8c39b6ff9c4fba71ddbfa8a1e7e");

	// Token: 0x04002DC8 RID: 11720
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerWoecleaver_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerWoecleaver_01.prefab:e70f55d669c095645ad071a69005ea05");

	// Token: 0x04002DC9 RID: 11721
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_DeathALT_01.prefab:ff39fbfbd2a24c04986e6674e129a828");

	// Token: 0x04002DCA RID: 11722
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_DefeatPlayer_01.prefab:20a1cbbc116156d40af93b77ddcb478e");

	// Token: 0x04002DCB RID: 11723
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_DefeatPlayer_02 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_DefeatPlayer_02.prefab:12d9fd05419612144b1fe78e24ef5dab");

	// Token: 0x04002DCC RID: 11724
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_DefeatPlayer_03 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_DefeatPlayer_03.prefab:f70fe24f458ce6244b3f03c868aae174");

	// Token: 0x04002DCD RID: 11725
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_EmoteResponse_01.prefab:34532f81a53839846ab836fd38cb7170");

	// Token: 0x04002DCE RID: 11726
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_01.prefab:eeb5086dca7304d478862186070c18a4");

	// Token: 0x04002DCF RID: 11727
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_02 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_02.prefab:60aba71588857cb4a954b3034fe37a38");

	// Token: 0x04002DD0 RID: 11728
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_03 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_03.prefab:0a1406b4c5c14fd4c993d2b4d0ce78ee");

	// Token: 0x04002DD1 RID: 11729
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_04 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_04.prefab:40a3470f4f2fcc84f8593cd190c69c11");

	// Token: 0x04002DD2 RID: 11730
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_01.prefab:81da2850da581a24085c8c0c577125c3");

	// Token: 0x04002DD3 RID: 11731
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_02 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_02.prefab:fd202243a29bacd40a3903e5a20a715b");

	// Token: 0x04002DD4 RID: 11732
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_03 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_03.prefab:7fccb2e3c2322a341b54550ade006cc1");

	// Token: 0x04002DD5 RID: 11733
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_04 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_04.prefab:b9a0404a600e1354d8922e7f8e78ba07");

	// Token: 0x04002DD6 RID: 11734
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_01.prefab:507b8a73fa8ac2045b68cfa5f7121383");

	// Token: 0x04002DD7 RID: 11735
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_02 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_02.prefab:4f57c5bdfe859594ba9c3f76d3f12783");

	// Token: 0x04002DD8 RID: 11736
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_03 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_03.prefab:cf37a5340dd2e0c4eb08dbd8042ff8ec");

	// Token: 0x04002DD9 RID: 11737
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_04 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_04.prefab:ae97d640d7d438a40af5a8df8a1d31bd");

	// Token: 0x04002DDA RID: 11738
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_05 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_05.prefab:9c584c31d039c524785420463d96e60b");

	// Token: 0x04002DDB RID: 11739
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_01.prefab:3fab3f3875411e942adde5152ba5b618");

	// Token: 0x04002DDC RID: 11740
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_02 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_02.prefab:7bc91430d6b6c06499099ed6f4ab5965");

	// Token: 0x04002DDD RID: 11741
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_03 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_03.prefab:e42cfd929f95e4942b99bb4efe22a57e");

	// Token: 0x04002DDE RID: 11742
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_04 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_04.prefab:defb38b8c3d352d439fc973ffb4e010b");

	// Token: 0x04002DDF RID: 11743
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_05 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_05.prefab:f75eae5bbc0d6974395ba00fb62eba6f");

	// Token: 0x04002DE0 RID: 11744
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IdleSpecial_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IdleSpecial_01.prefab:50ac2d697cbe24c44a5851f43b75cc57");

	// Token: 0x04002DE1 RID: 11745
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroFirstEncounter_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroFirstEncounter_01.prefab:ced3b5eb696c6f844a639ebe12e67ffc");

	// Token: 0x04002DE2 RID: 11746
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroGeneric_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroGeneric_01.prefab:9e5a3e1056dba874690cdf7655cf382c");

	// Token: 0x04002DE3 RID: 11747
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1_01.prefab:d987ba5f7d8820a41af367654ffcf79c");

	// Token: 0x04002DE4 RID: 11748
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1Retry_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1Retry_01.prefab:ff24d15a817472f4ba70f563c66c7ade");

	// Token: 0x04002DE5 RID: 11749
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1Retry_02 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1Retry_02.prefab:d2719de1288b8214f9eb6d89c8420f59");

	// Token: 0x04002DE6 RID: 11750
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase2Retry_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase2Retry_01.prefab:03b816d4e36310049b1939cdc34031ae");

	// Token: 0x04002DE7 RID: 11751
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase2Retry_02 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase2Retry_02.prefab:822aedac072bc2f4c89729eab8915ad7");

	// Token: 0x04002DE8 RID: 11752
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_01.prefab:fc1d8a10ff635ed4c9017c8b2776ab55");

	// Token: 0x04002DE9 RID: 11753
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_02 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_02.prefab:35030c812cdcbbb428d527b301f5f65a");

	// Token: 0x04002DEA RID: 11754
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_03 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_03.prefab:01b3edbf3170dd64b8fa2ae50642efa7");

	// Token: 0x04002DEB RID: 11755
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialBrann_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialBrann_01.prefab:158f0876a1f259c499a9085a2bb53eaf");

	// Token: 0x04002DEC RID: 11756
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialBrann2_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialBrann2_01.prefab:29b7a9a4f9fb3d24ead189c2edcf9d8c");

	// Token: 0x04002DED RID: 11757
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialElise_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialElise_01.prefab:d4d903f4ce480aa4a83c26ea6b5dadc8");

	// Token: 0x04002DEE RID: 11758
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialElise2_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialElise2_01.prefab:cbc2af44f2b1ed743957a9d4e14075b5");

	// Token: 0x04002DEF RID: 11759
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialFinley_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialFinley_01.prefab:eb614d24563e0be40b026713ef5bfdc9");

	// Token: 0x04002DF0 RID: 11760
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialFinley2_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialFinley2_01.prefab:6bfd6ad5574109f4b965aca3b1a33073");

	// Token: 0x04002DF1 RID: 11761
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialReno_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialReno_01.prefab:fb862538bdaeaea42b33fe2341db1376");

	// Token: 0x04002DF2 RID: 11762
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialReno2_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialReno2_01.prefab:b7ac8cb7bd58eae4c84f6424fbaef9a2");

	// Token: 0x04002DF3 RID: 11763
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_PhaseChange1_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_PhaseChange1_01.prefab:178cf0a10a501db45a1e18ea8d197c8e");

	// Token: 0x04002DF4 RID: 11764
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_PhaseChange2_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_PhaseChange2_01.prefab:58b7ebc4459507243bdc82c5f65d01cc");

	// Token: 0x04002DF5 RID: 11765
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerBeamingSidekick_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerBeamingSidekick_01.prefab:4cd5d06d4a0c16c4bbcc9037de792737");

	// Token: 0x04002DF6 RID: 11766
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerDesertHare_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerDesertHare_01.prefab:8fb332eca775e3b4e8d28d88f26998ab");

	// Token: 0x04002DF7 RID: 11767
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerInjuredTolvir_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerInjuredTolvir_01.prefab:1960acc178408d74098494575f9fce5a");

	// Token: 0x04002DF8 RID: 11768
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerInnerRage_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerInnerRage_01.prefab:0c2974a64a69f944e9cdff9128e5ba7b");

	// Token: 0x04002DF9 RID: 11769
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerPlagueofWrath_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerPlagueofWrath_01.prefab:c7cf27f9cea924d4a954cbeb845937d6");

	// Token: 0x04002DFA RID: 11770
	private List<string> m_HeroPower1Lines = new List<string>
	{
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_01,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_02,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_03,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_04
	};

	// Token: 0x04002DFB RID: 11771
	private List<string> m_HeroPower2Lines = new List<string>
	{
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_01,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_02,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_03,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_04
	};

	// Token: 0x04002DFC RID: 11772
	private List<string> m_HeroPower3Lines = new List<string>
	{
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_01,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_02,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_03,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_04,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_05
	};

	// Token: 0x04002DFD RID: 11773
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_01,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_02,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_03,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_04,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_05,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_01,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_02,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_03,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_04,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_05,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_01,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_02,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_03,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_04,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_05,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_01,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_02,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_03,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_04,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_05,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IdleSpecial_01
	};

	// Token: 0x04002DFE RID: 11774
	private List<string> m_IntroLines = new List<string>
	{
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroGeneric_01,
		ULDA_Dungeon_Boss_40h.VO_ULDA_BOSS_40h_Female_PlagueLord_IntroFirstEncounter_01
	};

	// Token: 0x04002DFF RID: 11775
	private HashSet<string> m_playedLines = new HashSet<string>();
}
