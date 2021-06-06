using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004A4 RID: 1188
public class ULDA_Dungeon_Boss_39h : ULDA_Dungeon
{
	// Token: 0x06004008 RID: 16392 RVA: 0x00154978 File Offset: 0x00152B78
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_BossMomentumSwing_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocDeath_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocNoise_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocNoise_02,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerCrushingHand_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerFishflinger_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerMurmy_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1CallintheFinishers_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1EveryfinisAwesome_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1TiptheScales_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_DeathALT_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_DefeatPlayer_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_DefeatPlayer_02,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_DefeatPlayer_03,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_EmoteResponse_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_02,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_03,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_04,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_02,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_03,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_04,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_02,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_03,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_04,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3Rare_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_02,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_03,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_04,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_05,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IdleRare_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IdleSpecialRare_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroFirstEncounter_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroGeneric_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1Retry_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1Retry_02,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2Retry_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2Retry_02,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_02,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_03,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialBrann_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialElise_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialFinley_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialReno_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_PhaseChange1_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_PhaseChange2_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerHungryCrab_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerMurloc_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerPlague_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerPlagueofMurlocs_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerZephrys_01,
			ULDA_Dungeon_Boss_39h.VO_ULDA_Reno_Male_Human_BossVeshPhase3HeroPower_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004009 RID: 16393 RVA: 0x00154D8C File Offset: 0x00152F8C
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x0600400A RID: 16394 RVA: 0x00154D94 File Offset: 0x00152F94
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_EmoteResponse_01;
	}

	// Token: 0x0600400B RID: 16395 RVA: 0x00154DBC File Offset: 0x00152FBC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		int currentHealth = GameState.Get().GetOpposingSidePlayer().GetCurrentHealth();
		if (emoteType == EmoteType.START)
		{
			if (currentHealth == 300)
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1_01);
			}
			if (currentHealth <= 299 && currentHealth >= 201)
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1Retry_01);
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1Retry_02);
			}
			if (currentHealth <= 200 && currentHealth >= 101)
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2Retry_01);
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2Retry_02);
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2_01);
			}
			if (currentHealth <= 100 && currentHealth >= 0)
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_01);
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_02);
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_03);
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3_01);
			}
			if (cardId == "ULDA_Reno")
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialReno_01);
			}
			if (cardId == "ULDA_Brann")
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialBrann_01);
			}
			if (cardId == "ULDA_Elise")
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialElise_01);
			}
			if (cardId == "ULDA_Finley")
			{
				this.m_IntroLines.Add(ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialFinley_01);
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

	// Token: 0x0600400C RID: 16396 RVA: 0x00155006 File Offset: 0x00153206
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
			GameState.Get().SetBusy(true);
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_PhaseChange1_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_273;
		case 102:
			GameState.Get().SetBusy(true);
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_PhaseChange2_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_273;
		case 103:
			yield return base.PlayRandomLineAlways(actor, this.m_HeroPower1TriggerLines);
			goto IL_273;
		case 104:
			yield return base.PlayRandomLineAlways(actor, this.m_HeroPower2Lines);
			goto IL_273;
		case 105:
			yield return base.PlayRandomLineAlways(actor, this.m_HeroPower3Lines);
			goto IL_273;
		case 109:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerMurloc_01, 2.5f);
			goto IL_273;
		case 110:
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_BossMomentumSwing_01, 2.5f);
			goto IL_273;
		case 111:
			yield return base.PlayRandomLineAlways(actor, this.m_BossMurlocNoiseLines);
			goto IL_273;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_273:
		yield break;
	}

	// Token: 0x0600400D RID: 16397 RVA: 0x0015501C File Offset: 0x0015321C
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
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 316364135U)
		{
			if (num != 232623135U)
			{
				if (num != 282955992U)
				{
					if (num != 316364135U)
					{
						goto IL_2D3;
					}
					if (!(cardId == "ULD_707"))
					{
						goto IL_2D3;
					}
				}
				else if (!(cardId == "ULD_715"))
				{
					goto IL_2D3;
				}
			}
			else if (!(cardId == "ULD_718"))
			{
				goto IL_2D3;
			}
		}
		else if (num <= 1466194159U)
		{
			if (num != 316511230U)
			{
				if (num != 1466194159U)
				{
					goto IL_2D3;
				}
				if (!(cardId == "NEW1_017"))
				{
					goto IL_2D3;
				}
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerHungryCrab_01, 2.5f);
				goto IL_2D3;
			}
			else if (!(cardId == "ULD_717"))
			{
				goto IL_2D3;
			}
		}
		else if (num != 1777725921U)
		{
			if (num != 3448728610U)
			{
				goto IL_2D3;
			}
			if (!(cardId == "ULD_003"))
			{
				goto IL_2D3;
			}
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerZephrys_01, 2.5f);
			goto IL_2D3;
		}
		else
		{
			if (!(cardId == "ULD_172"))
			{
				goto IL_2D3;
			}
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerPlagueofMurlocs_01, 2.5f);
			goto IL_2D3;
		}
		yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerPlague_01, 2.5f);
		IL_2D3:
		yield break;
	}

	// Token: 0x0600400E RID: 16398 RVA: 0x00155032 File Offset: 0x00153232
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
		if (num <= 1401670756U)
		{
			if (num <= 333288849U)
			{
				if (num != 114738517U)
				{
					if (num != 333288849U)
					{
						goto IL_3F0;
					}
					if (!(cardId == "ULD_716"))
					{
						goto IL_3F0;
					}
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1TiptheScales_01, 2.5f);
					goto IL_3F0;
				}
				else
				{
					if (!(cardId == "ULD_723"))
					{
						goto IL_3F0;
					}
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerMurmy_01, 2.5f);
					goto IL_3F0;
				}
			}
			else if (num != 831742763U)
			{
				if (num != 1401670756U)
				{
					goto IL_3F0;
				}
				if (!(cardId == "CFM_310"))
				{
					goto IL_3F0;
				}
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1CallintheFinishers_01, 2.5f);
				goto IL_3F0;
			}
			else if (!(cardId == "ULDA_BOSS_39px3"))
			{
				goto IL_3F0;
			}
		}
		else if (num <= 2353952147U)
		{
			if (num != 2264176430U)
			{
				if (num != 2353952147U)
				{
					goto IL_3F0;
				}
				if (!(cardId == "LOE_113"))
				{
					goto IL_3F0;
				}
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1EveryfinisAwesome_01, 2.5f);
				goto IL_3F0;
			}
			else
			{
				if (!(cardId == "LOOT_060"))
				{
					goto IL_3F0;
				}
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerCrushingHand_01, 2.5f);
				goto IL_3F0;
			}
		}
		else if (num != 2963965906U)
		{
			if (num != 3066613199U)
			{
				goto IL_3F0;
			}
			if (!(cardId == "ULDA_BOSS_39p3"))
			{
				goto IL_3F0;
			}
		}
		else
		{
			if (!(cardId == "ULD_289"))
			{
				goto IL_3F0;
			}
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerFishflinger_01, 2.5f);
			goto IL_3F0;
		}
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		if (GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId() == "ULDA_Reno")
		{
			yield return base.PlayLineOnlyOnce(actor2, ULDA_Dungeon_Boss_39h.VO_ULDA_Reno_Male_Human_BossVeshPhase3HeroPower_01, 2.5f);
		}
		IL_3F0:
		yield break;
	}

	// Token: 0x04002D7D RID: 11645
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossMomentumSwing_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossMomentumSwing_01.prefab:da9884341b1638b46971464ee387db5e");

	// Token: 0x04002D7E RID: 11646
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocDeath_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocDeath_01.prefab:b15ee32cbbdb2e74a93e96a07037ff6b");

	// Token: 0x04002D7F RID: 11647
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocNoise_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocNoise_01.prefab:3783a747e2f3fb04caa43b869108a256");

	// Token: 0x04002D80 RID: 11648
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocNoise_02 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocNoise_02.prefab:71c06c76b13dd6e428b2e7d9f3e7e117");

	// Token: 0x04002D81 RID: 11649
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerCrushingHand_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerCrushingHand_01.prefab:dcf4773ad1f532e49b50ef4ea760d1cf");

	// Token: 0x04002D82 RID: 11650
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerFishflinger_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerFishflinger_01.prefab:6da9cddd8a5566e40ab02cc659ffab09");

	// Token: 0x04002D83 RID: 11651
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerMurmy_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerMurmy_01.prefab:edd1e34b9ad4d1b4c8648ef04b9ba69a");

	// Token: 0x04002D84 RID: 11652
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1CallintheFinishers_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1CallintheFinishers_01.prefab:bb69b6e6c0ed6e34f8cc6d55b2382589");

	// Token: 0x04002D85 RID: 11653
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1EveryfinisAwesome_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1EveryfinisAwesome_01.prefab:e13b3999c6b45c34db84cea507f64d47");

	// Token: 0x04002D86 RID: 11654
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1TiptheScales_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1TiptheScales_01.prefab:b8e22f75dbe04784b8f71f781c19b056");

	// Token: 0x04002D87 RID: 11655
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_DeathALT_01.prefab:25d296dd91490a543bda494968932878");

	// Token: 0x04002D88 RID: 11656
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_DefeatPlayer_01.prefab:eca09c20f227f6c4ca46023c27764358");

	// Token: 0x04002D89 RID: 11657
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_DefeatPlayer_02 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_DefeatPlayer_02.prefab:9595c421f29db7546892d613bc8dd428");

	// Token: 0x04002D8A RID: 11658
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_DefeatPlayer_03 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_DefeatPlayer_03.prefab:36a111d57df822c4183603d7dc735d01");

	// Token: 0x04002D8B RID: 11659
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_EmoteResponse_01.prefab:1573e6c973abae94da58885e8fa1064e");

	// Token: 0x04002D8C RID: 11660
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_01.prefab:8914017a8dd19594bacfa1d6d36c56ea");

	// Token: 0x04002D8D RID: 11661
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_02 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_02.prefab:abae716f707c8ff42beabd11387378ff");

	// Token: 0x04002D8E RID: 11662
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_03 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_03.prefab:33aa503c2fcb7854d867dbc53df4004a");

	// Token: 0x04002D8F RID: 11663
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_04 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_04.prefab:e5a7425c7e8c73f4d96c2a6dfa03ff2c");

	// Token: 0x04002D90 RID: 11664
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_01.prefab:e644253664aab484bb4b2ac8446aa0a2");

	// Token: 0x04002D91 RID: 11665
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_02 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_02.prefab:74ce6f0660d883748a55db776d32c379");

	// Token: 0x04002D92 RID: 11666
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_03 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_03.prefab:689f6bac2a9d0b440a73e7d05999f18e");

	// Token: 0x04002D93 RID: 11667
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_04 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_04.prefab:ab4b6ee54def71746bb2d6dff703dae0");

	// Token: 0x04002D94 RID: 11668
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_01.prefab:87ca2ec24b309ed49884e7693ff7c2bb");

	// Token: 0x04002D95 RID: 11669
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_02 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_02.prefab:6d583948cab9a594bb4a141138ca1467");

	// Token: 0x04002D96 RID: 11670
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_03 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_03.prefab:ed61fabaebfc2a14fb4edd6925733f8a");

	// Token: 0x04002D97 RID: 11671
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_04 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_04.prefab:66c34dbc4499f914193df40f3d1a9d54");

	// Token: 0x04002D98 RID: 11672
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3Rare_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3Rare_01.prefab:896c87c3a1a12da45a09f94a82366eab");

	// Token: 0x04002D99 RID: 11673
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_01.prefab:09176d46c48be4947946551fe9ca7648");

	// Token: 0x04002D9A RID: 11674
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_02 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_02.prefab:af3f4f7d407051744bc2291d33368c7b");

	// Token: 0x04002D9B RID: 11675
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_03 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_03.prefab:3b8e0fb897c8f7b46b3b71716909ac98");

	// Token: 0x04002D9C RID: 11676
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_04 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_04.prefab:92a8a57e9faa56b4bb5eb3085434d18f");

	// Token: 0x04002D9D RID: 11677
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_05 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_05.prefab:afbab207bfcdc4948a6db5b06a6d7e9c");

	// Token: 0x04002D9E RID: 11678
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IdleRare_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IdleRare_01.prefab:3e0b6de0af4327d4692a63b9809747c8");

	// Token: 0x04002D9F RID: 11679
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IdleSpecialRare_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IdleSpecialRare_01.prefab:82b64ebace0bf254787027cca76578d1");

	// Token: 0x04002DA0 RID: 11680
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroFirstEncounter_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroFirstEncounter_01.prefab:4d9e92da5ada0254b8770372cf5f69d0");

	// Token: 0x04002DA1 RID: 11681
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroGeneric_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroGeneric_01.prefab:e33678a0d53b35847bf1650c6ec69898");

	// Token: 0x04002DA2 RID: 11682
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1_01.prefab:32e580df3f08b3a4894ef59e4acdc775");

	// Token: 0x04002DA3 RID: 11683
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1Retry_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1Retry_01.prefab:974bf1d807287ee4c925259c0c2332b3");

	// Token: 0x04002DA4 RID: 11684
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1Retry_02 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1Retry_02.prefab:089d1dd5b1a8d0e4897adfea38097949");

	// Token: 0x04002DA5 RID: 11685
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2_01.prefab:c4dd8b13696a0d846b320e7e0d58e4c9");

	// Token: 0x04002DA6 RID: 11686
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2Retry_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2Retry_01.prefab:aa4ae4ae89e5ed145b550ae73dca2151");

	// Token: 0x04002DA7 RID: 11687
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2Retry_02 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2Retry_02.prefab:92d94455f92a40441a699792d5e3a52a");

	// Token: 0x04002DA8 RID: 11688
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3_01.prefab:d5948a6c0fa76ea4da748faaebf8bff5");

	// Token: 0x04002DA9 RID: 11689
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_01.prefab:b6ea729de86c4c943b706f63ef80ee29");

	// Token: 0x04002DAA RID: 11690
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_02 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_02.prefab:5ff7a1da9befa154686263fc29c772d4");

	// Token: 0x04002DAB RID: 11691
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_03 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_03.prefab:cd683052bde0d0d4db377649312e90d1");

	// Token: 0x04002DAC RID: 11692
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialBrann_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialBrann_01.prefab:28a33199a9cff654a9e616631f2ea6c0");

	// Token: 0x04002DAD RID: 11693
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialElise_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialElise_01.prefab:fcd5f7b9d7f251841975183f92fee39c");

	// Token: 0x04002DAE RID: 11694
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialFinley_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialFinley_01.prefab:5ddea0f86c8cc844e968fc1e51873858");

	// Token: 0x04002DAF RID: 11695
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialReno_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialReno_01.prefab:130593c1d635dba4e9ae248665a39fcb");

	// Token: 0x04002DB0 RID: 11696
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_PhaseChange1_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_PhaseChange1_01.prefab:920c844b64972c54692931172d2c44f1");

	// Token: 0x04002DB1 RID: 11697
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_PhaseChange2_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_PhaseChange2_01.prefab:b90b4009ed0a5aa49a2acadad945ebc9");

	// Token: 0x04002DB2 RID: 11698
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerHungryCrab_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerHungryCrab_01.prefab:e623dfe81ce0c8c4cb6ecdc26e60e2d7");

	// Token: 0x04002DB3 RID: 11699
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerMurloc_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerMurloc_01.prefab:82f0d3c9f1b00024db90aec9684d53f7");

	// Token: 0x04002DB4 RID: 11700
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerPlague_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerPlague_01.prefab:6391a384b794aae49b718ac9e08f6f90");

	// Token: 0x04002DB5 RID: 11701
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerPlagueofMurlocs_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerPlagueofMurlocs_01.prefab:fab6117247fc2d94aacd06d59f643ac5");

	// Token: 0x04002DB6 RID: 11702
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerZephrys_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerZephrys_01.prefab:e606d109e460e1448bcad2d2b2d51268");

	// Token: 0x04002DB7 RID: 11703
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_BossVeshPhase3HeroPower_01 = new AssetReference("VO_ULDA_Reno_Male_Human_BossVeshPhase3HeroPower_01.prefab:9d0aecc4a40adde4d8f4688fdd0bbbc8");

	// Token: 0x04002DB8 RID: 11704
	private List<string> m_BossMurlocNoiseLines = new List<string>
	{
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocNoise_01,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocNoise_02
	};

	// Token: 0x04002DB9 RID: 11705
	private List<string> m_HeroPower1TriggerLines = new List<string>
	{
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_01,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_02,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_03,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_04
	};

	// Token: 0x04002DBA RID: 11706
	private List<string> m_HeroPower2Lines = new List<string>
	{
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_01,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_02,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_03,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_04
	};

	// Token: 0x04002DBB RID: 11707
	private List<string> m_HeroPower3Lines = new List<string>
	{
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_01,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_02,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_03,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_04,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_01,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_02,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_03,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_04,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_01,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_02,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_03,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_04,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3Rare_01
	};

	// Token: 0x04002DBC RID: 11708
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_01,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_02,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_03,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_04,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_05,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_01,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_02,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_03,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_04,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_05,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_01,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_02,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_03,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_04,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_05,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_01,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_02,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_03,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_04,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_05,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IdleRare_01,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IdleRare_01,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IdleSpecialRare_01
	};

	// Token: 0x04002DBD RID: 11709
	private List<string> m_IntroLines = new List<string>
	{
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroGeneric_01,
		ULDA_Dungeon_Boss_39h.VO_ULDA_BOSS_39h_Male_PlagueLord_IntroFirstEncounter_01
	};

	// Token: 0x04002DBE RID: 11710
	private HashSet<string> m_playedLines = new HashSet<string>();
}
