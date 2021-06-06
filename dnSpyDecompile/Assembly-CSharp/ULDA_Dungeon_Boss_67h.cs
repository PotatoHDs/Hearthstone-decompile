using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004BF RID: 1215
public class ULDA_Dungeon_Boss_67h : ULDA_Dungeon
{
	// Token: 0x06004127 RID: 16679 RVA: 0x0015BDDC File Offset: 0x00159FDC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossMomentumSwing_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerCataclysm_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerFireball_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerFlamestrike_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerForbiddenFlame_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerHellfire_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerObsidianDestroyer_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerPlagueofFlames_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerRakanishuDeath_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerRakanishuSummon_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerSoulfire_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_DeathALT_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_02,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_03,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_EmoteResponse_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_02,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_03,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_04,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_02,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_03,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_04,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_02,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_03,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_04,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_05,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_02,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_03,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_04,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_05,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IdleSpecial_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroFirstEncounter_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroGeneric_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1Retry_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1Retry_02,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase2Retry_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase3Retry_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase3Retry_03,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialBrann_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialBrann2_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialElise_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialElise2_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialFinley_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialFinley2_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialReno_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialReno2_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_PhaseChange1_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_PhaseChange2_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerJrExplorer_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerKingPhaoris_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerSkilledPathfinder_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerSunkeeperTarim_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerZephrystheGreat_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_Elise_vs_Tekahn_b_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_FinalPhase_a_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_FinalPhase_d_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_Brann_Male_Dwarf_TUT_Finale_Brann_Treasures_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_Elise_Female_NightElf_TUT_Finale_Elise_Treasures_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_Finley_Male_Murloc_TUT_Finale_Finley_Treasures_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_Reno_Male_Human_TUT_Finale_Reno_Treasures_01,
			ULDA_Dungeon_Boss_67h.VO_DALA_Rafaam_Male_Ethereal_STORY_Finale_FinalPhase_b_01,
			ULDA_Dungeon_Boss_67h.VO_DALA_Rafaam_Male_Ethereal_STORY_Finale_FinalPhase_c_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_Reno_Male_Human_Start_vs_Plague_Fire_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_Elise_Female_NightElf_Start_vs_Plague_Fire_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_Brann_Male_Dwarf_Start_vs_Plague_Fire_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_Finley_Male_Murloc_Start_vs_Plague_Fire_01,
			ULDA_Dungeon_Boss_67h.VO_ULDA_Elise_Female_NightElf_TUT_Finale_Final_Defeat_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004128 RID: 16680 RVA: 0x0015C2C0 File Offset: 0x0015A4C0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_EmoteResponse_01;
	}

	// Token: 0x06004129 RID: 16681 RVA: 0x0015C2E8 File Offset: 0x0015A4E8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Gameplay.Get().StartCoroutine(this.PlayMultipleVOLinesForEmotes(emoteType, emoteSpell));
	}

	// Token: 0x0600412A RID: 16682 RVA: 0x0015C2FD File Offset: 0x0015A4FD
	protected IEnumerator PlayMultipleVOLinesForEmotes(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			yield return base.PlayBossLine(actor, this.m_standardEmoteResponseLine, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600412B RID: 16683 RVA: 0x0015C313 File Offset: 0x0015A513
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (missionEvent != 10)
		{
			switch (missionEvent)
			{
			case 101:
				yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_HeroPower1Lines);
				break;
			case 102:
				yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_HeroPower2Lines);
				break;
			case 103:
				yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_HeroPower3Lines);
				break;
			case 104:
				yield return base.PlayLineOnlyOnce(enemyActor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerRakanishuSummon_01, 2.5f);
				break;
			case 105:
				yield return base.PlayLineOnlyOnce(enemyActor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerRakanishuDeath_01, 2.5f);
				break;
			case 106:
				yield return base.PlayBossLine(enemyActor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_PhaseChange1_01, 2.5f);
				break;
			case 107:
				yield return base.PlayBossLine(enemyActor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_PhaseChange2_01, 2.5f);
				break;
			case 108:
				yield return base.PlayLineOnlyOnce(enemyActor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossMomentumSwing_01, 2.5f);
				break;
			case 109:
				if (cardId == "ULDA_Reno")
				{
					yield return base.PlayLineOnlyOnce(ULDA_Dungeon_Boss_67h.Tombs_of_Terror_Reno_BrassRing_Quote, ULDA_Dungeon_Boss_67h.VO_ULDA_Reno_Male_Human_TUT_Finale_Reno_Treasures_01, 2.5f);
				}
				else if (cardId == "ULDA_Finley")
				{
					yield return base.PlayLineOnlyOnce(ULDA_Dungeon_Boss_67h.Tombs_of_Terror_Finley_BrassRing_Quote, ULDA_Dungeon_Boss_67h.VO_ULDA_Finley_Male_Murloc_TUT_Finale_Finley_Treasures_01, 2.5f);
				}
				else if (cardId == "ULDA_Brann")
				{
					yield return base.PlayLineOnlyOnce(ULDA_Dungeon_Boss_67h.Tombs_of_Terror_Brann_BrassRing_Quote, ULDA_Dungeon_Boss_67h.VO_ULDA_Brann_Male_Dwarf_TUT_Finale_Brann_Treasures_01, 2.5f);
				}
				else if (cardId == "ULDA_Elise")
				{
					yield return base.PlayLineOnlyOnce(ULDA_Dungeon_Boss_67h.Tombs_of_Terror_Elise_BrassRing_Quote, ULDA_Dungeon_Boss_67h.VO_ULDA_Elise_Female_NightElf_TUT_Finale_Elise_Treasures_01, 2.5f);
				}
				break;
			case 110:
				this.m_LinesPlaying = true;
				GameState.Get().SetBusy(true);
				yield return base.PlayLineOnlyOnce(enemyActor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_FinalPhase_a_01, 2.5f);
				yield return base.PlayLineOnlyOnce(ULDA_Dungeon_Boss_67h.Rafaam_popup_BrassRing_Quote, ULDA_Dungeon_Boss_67h.VO_DALA_Rafaam_Male_Ethereal_STORY_Finale_FinalPhase_b_01, 2.5f);
				yield return base.PlayLineOnlyOnce(ULDA_Dungeon_Boss_67h.Rafaam_popup_BrassRing_Quote, ULDA_Dungeon_Boss_67h.VO_DALA_Rafaam_Male_Ethereal_STORY_Finale_FinalPhase_c_01, 2.5f);
				yield return base.PlayLineOnlyOnce(enemyActor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_FinalPhase_d_01, 2.5f);
				GameState.Get().SetBusy(false);
				this.m_LinesPlaying = false;
				break;
			case 111:
				yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_DefeatHeroLines);
				break;
			case 112:
				this.m_LinesPlaying = true;
				GameState.Get().SetBusy(true);
				yield return base.PlayBossLine(enemyActor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_03, 2.5f);
				yield return base.PlayLineOnlyOnce(ULDA_Dungeon_Boss_67h.Tombs_of_Terror_Elise_BrassRing_Quote, ULDA_Dungeon_Boss_67h.VO_ULDA_Elise_Female_NightElf_TUT_Finale_Final_Defeat_01, 2.5f);
				GameState.Get().SetBusy(false);
				this.m_LinesPlaying = false;
				break;
			case 113:
			{
				int randomNumber = UnityEngine.Random.Range(1, 5);
				GameState.Get().SetBusy(true);
				if (randomNumber == 1)
				{
					this.m_IntroLines.Add(ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialReno_01);
				}
				if (randomNumber == 2)
				{
					this.m_IntroLines.Add(ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialBrann_01);
				}
				if (randomNumber == 3)
				{
					this.m_IntroLines.Add(ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialElise_01);
				}
				if (randomNumber == 4)
				{
					this.m_IntroLines.Add(ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialFinley_01);
				}
				this.m_introLine = base.PopRandomLine(this.m_IntroLines);
				if (this.m_introLine == ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialReno_01 || this.m_introLine == ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialBrann_01 || this.m_introLine == ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialElise_01 || this.m_introLine == ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialFinley_01)
				{
					yield return base.PlayBossLine(enemyActor, this.m_introLine, 2.5f);
					if (randomNumber == 1)
					{
						yield return base.PlayBossLine(ULDA_Dungeon_Boss_67h.Tombs_of_Terror_Reno_BrassRing_Quote, ULDA_Dungeon_Boss_67h.VO_ULDA_Reno_Male_Human_Start_vs_Plague_Fire_01, 2.5f);
					}
					if (randomNumber == 2)
					{
						yield return base.PlayBossLine(ULDA_Dungeon_Boss_67h.Tombs_of_Terror_Brann_BrassRing_Quote, ULDA_Dungeon_Boss_67h.VO_ULDA_Brann_Male_Dwarf_Start_vs_Plague_Fire_01, 2.5f);
					}
					else if (randomNumber == 3)
					{
						yield return base.PlayBossLine(ULDA_Dungeon_Boss_67h.Tombs_of_Terror_Elise_BrassRing_Quote, ULDA_Dungeon_Boss_67h.VO_ULDA_Elise_Female_NightElf_Start_vs_Plague_Fire_01, 2.5f);
					}
					else if (randomNumber == 4)
					{
						yield return base.PlayBossLine(ULDA_Dungeon_Boss_67h.Tombs_of_Terror_Finley_BrassRing_Quote, ULDA_Dungeon_Boss_67h.VO_ULDA_Finley_Male_Murloc_Start_vs_Plague_Fire_01, 2.5f);
					}
					if (this.m_introLine == ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialReno_01)
					{
						yield return base.PlayBossLine(enemyActor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialReno2_01, 2.5f);
					}
					else if (this.m_introLine == ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialBrann_01)
					{
						yield return base.PlayBossLine(enemyActor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialBrann2_01, 2.5f);
					}
					else if (this.m_introLine == ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialElise_01)
					{
						yield return base.PlayBossLine(enemyActor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialElise2_01, 2.5f);
					}
					else if (this.m_introLine == ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialFinley_01)
					{
						yield return base.PlayBossLine(enemyActor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialFinley2_01, 2.5f);
					}
				}
				else
				{
					if (randomNumber == 1)
					{
						yield return base.PlayBossLine(ULDA_Dungeon_Boss_67h.Tombs_of_Terror_Reno_BrassRing_Quote, ULDA_Dungeon_Boss_67h.VO_ULDA_Reno_Male_Human_Start_vs_Plague_Fire_01, 2.5f);
					}
					if (randomNumber == 2)
					{
						yield return base.PlayBossLine(ULDA_Dungeon_Boss_67h.Tombs_of_Terror_Brann_BrassRing_Quote, ULDA_Dungeon_Boss_67h.VO_ULDA_Brann_Male_Dwarf_Start_vs_Plague_Fire_01, 2.5f);
					}
					else if (randomNumber == 3)
					{
						yield return base.PlayBossLine(ULDA_Dungeon_Boss_67h.Tombs_of_Terror_Elise_BrassRing_Quote, ULDA_Dungeon_Boss_67h.VO_ULDA_Elise_Female_NightElf_Start_vs_Plague_Fire_01, 2.5f);
					}
					else if (randomNumber == 4)
					{
						yield return base.PlayBossLine(ULDA_Dungeon_Boss_67h.Tombs_of_Terror_Finley_BrassRing_Quote, ULDA_Dungeon_Boss_67h.VO_ULDA_Finley_Male_Murloc_Start_vs_Plague_Fire_01, 2.5f);
					}
					yield return base.PlayBossLine(enemyActor, this.m_introLine, 2.5f);
				}
				GameState.Get().SetBusy(false);
				break;
			}
			default:
				yield return base.HandleMissionEventWithTiming(missionEvent);
				break;
			}
		}
		else if (GameState.Get().IsFriendlySidePlayerTurn())
		{
			TurnStartManager.Get().BeginListeningForTurnEvents(false);
		}
		yield break;
	}

	// Token: 0x0600412C RID: 16684 RVA: 0x0015C329 File Offset: 0x0015A529
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
		if (num <= 3448728610U)
		{
			if (num <= 110374861U)
			{
				if (num != 98670474U)
				{
					if (num != 110374861U)
					{
						goto IL_33C;
					}
					if (!(cardId == "ULDA_018"))
					{
						goto IL_33C;
					}
				}
				else
				{
					if (!(cardId == "UNG_015"))
					{
						goto IL_33C;
					}
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerSunkeeperTarim_01, 2.5f);
					goto IL_33C;
				}
			}
			else if (num != 1193724526U)
			{
				if (num != 3448728610U)
				{
					goto IL_33C;
				}
				if (!(cardId == "ULD_003"))
				{
					goto IL_33C;
				}
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerZephrystheGreat_01, 2.5f);
				goto IL_33C;
			}
			else
			{
				if (!(cardId == "ULD_304"))
				{
					goto IL_33C;
				}
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerKingPhaoris_01, 2.5f);
				goto IL_33C;
			}
		}
		else if (num <= 4153677872U)
		{
			if (num != 3706754748U)
			{
				if (num != 4153677872U)
				{
					goto IL_33C;
				}
				if (!(cardId == "ULDA_017"))
				{
					goto IL_33C;
				}
			}
			else
			{
				if (!(cardId == "ULD_157"))
				{
					goto IL_33C;
				}
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerSkilledPathfinder_01, 2.5f);
				goto IL_33C;
			}
		}
		else if (num != 4170455491U)
		{
			if (num != 4187233110U)
			{
				goto IL_33C;
			}
			if (!(cardId == "ULDA_015"))
			{
				goto IL_33C;
			}
		}
		else if (!(cardId == "ULDA_016"))
		{
			goto IL_33C;
		}
		yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerJrExplorer_01, 2.5f);
		IL_33C:
		yield break;
	}

	// Token: 0x0600412D RID: 16685 RVA: 0x0015C33F File Offset: 0x0015A53F
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
		if (num <= 821823990U)
		{
			if (num <= 693699923U)
			{
				if (num != 316511230U)
				{
					if (num == 693699923U)
					{
						if (cardId == "ULDA_BOSS_67t")
						{
							yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_Elise_vs_Tekahn_b_01, 2.5f);
						}
					}
				}
				else if (cardId == "ULD_717")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerPlagueofFlames_01, 2.5f);
				}
			}
			else if (num != 767131743U)
			{
				if (num == 821823990U)
				{
					if (cardId == "OG_086")
					{
						yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerForbiddenFlame_01, 2.5f);
					}
				}
			}
			else if (cardId == "LOE_009")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerObsidianDestroyer_01, 2.5f);
			}
		}
		else if (num <= 3945181129U)
		{
			if (num != 1821421869U)
			{
				if (num == 3945181129U)
				{
					if (cardId == "CS2_029")
					{
						yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerFireball_01, 2.5f);
					}
				}
			}
			else if (cardId == "EX1_308")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerSoulfire_01, 2.5f);
			}
		}
		else if (num != 4162598628U)
		{
			if (num == 4196992509U)
			{
				if (cardId == "CS2_032")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerFlamestrike_01, 2.5f);
				}
			}
		}
		else if (cardId == "CS2_062")
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerHellfire_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600412E RID: 16686 RVA: 0x0015C358 File Offset: 0x0015A558
	public override void OnPlayThinkEmote()
	{
		if (this.m_enemySpeaking)
		{
			return;
		}
		if (this.m_LinesPlaying)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			return;
		}
		if (currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string line = base.PopRandomLine(this.m_IdleLinesCopy);
		if (this.m_IdleLinesCopy.Count == 0)
		{
			this.m_IdleLinesCopy = new List<string>(ULDA_Dungeon_Boss_67h.m_IdleLines);
		}
		Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, line, 2.5f));
	}

	// Token: 0x04002FA0 RID: 12192
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossMomentumSwing_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossMomentumSwing_01.prefab:816084397af586144a486cf364344106");

	// Token: 0x04002FA1 RID: 12193
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerCataclysm_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerCataclysm_01.prefab:03d38ed167c4ebb4fb722b36e1a68c1d");

	// Token: 0x04002FA2 RID: 12194
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerFireball_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerFireball_01.prefab:2658eca0ec276dc468bb4b186acecee1");

	// Token: 0x04002FA3 RID: 12195
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerFlamestrike_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerFlamestrike_01.prefab:a56895a29e1925c45b7e4e1988355d47");

	// Token: 0x04002FA4 RID: 12196
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerForbiddenFlame_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerForbiddenFlame_01.prefab:b2881bc87774be2409d33711b93433c8");

	// Token: 0x04002FA5 RID: 12197
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerHellfire_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerHellfire_01.prefab:8d10a78d6f1fc744d908b5ebe715a3e6");

	// Token: 0x04002FA6 RID: 12198
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerObsidianDestroyer_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerObsidianDestroyer_01.prefab:25e08154b6fe0364e96a34e794e18241");

	// Token: 0x04002FA7 RID: 12199
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerPlagueofFlames_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerPlagueofFlames_01.prefab:66e0eeafede90164c8319781de9f8f42");

	// Token: 0x04002FA8 RID: 12200
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerRakanishuDeath_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerRakanishuDeath_01.prefab:a71752c90b1fc1f43bb75e48cd259687");

	// Token: 0x04002FA9 RID: 12201
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerRakanishuSummon_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerRakanishuSummon_01.prefab:787044eb33b05b44392fd8c598ee18a0");

	// Token: 0x04002FAA RID: 12202
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerSoulfire_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerSoulfire_01.prefab:02992b091a683564f9524005f9918e6b");

	// Token: 0x04002FAB RID: 12203
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_DeathALT_01.prefab:fb1218e8ca32acb4fbd9a4b33b480371");

	// Token: 0x04002FAC RID: 12204
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_01.prefab:29c4248476d72844b9a8c208f9b540d1");

	// Token: 0x04002FAD RID: 12205
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_02 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_02.prefab:c069a4ebba7700a44b9ba35c722b0ca9");

	// Token: 0x04002FAE RID: 12206
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_03 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_03.prefab:82e4d0208d8d2ea4eb554d4f71e2c3f2");

	// Token: 0x04002FAF RID: 12207
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_EmoteResponse_01.prefab:3810ea78be89b374e8d29f0a9247b13f");

	// Token: 0x04002FB0 RID: 12208
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_01.prefab:9d1ce84d8e2304b42885d1180616dcd2");

	// Token: 0x04002FB1 RID: 12209
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_02 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_02.prefab:85df586f1cf3cd84e95ce2ac9ad5faf7");

	// Token: 0x04002FB2 RID: 12210
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_03 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_03.prefab:98eac91c25a56a14cb1f51c27e98c06c");

	// Token: 0x04002FB3 RID: 12211
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_04 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_04.prefab:b2a253fae9296894e9fba0b19bff2d68");

	// Token: 0x04002FB4 RID: 12212
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_01.prefab:e99648f6a1a1d3343bb36b75cbd07210");

	// Token: 0x04002FB5 RID: 12213
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_02 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_02.prefab:6f3bff53ba7b7944eb9fa10ae5b8c1e6");

	// Token: 0x04002FB6 RID: 12214
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_03 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_03.prefab:4da8708434d359e408fc1afae7439875");

	// Token: 0x04002FB7 RID: 12215
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_04 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_04.prefab:ae51e12f422e7a84f9cc5999f1d5c536");

	// Token: 0x04002FB8 RID: 12216
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_01.prefab:194b003e852e6c74a9d82fdacb3db1d5");

	// Token: 0x04002FB9 RID: 12217
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_02 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_02.prefab:3117a2db6d484764692abaf7d1db4ccb");

	// Token: 0x04002FBA RID: 12218
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_03 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_03.prefab:56b3ccc835f383d448db0c876c1cf48e");

	// Token: 0x04002FBB RID: 12219
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_04 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_04.prefab:95da71fde629b9a44b7b69bdf6d4ccc4");

	// Token: 0x04002FBC RID: 12220
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_05 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_05.prefab:bc4792e6296d61e40b14b59535b6186c");

	// Token: 0x04002FBD RID: 12221
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_01.prefab:1f5c178cde1a63a47be9795646318fa2");

	// Token: 0x04002FBE RID: 12222
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_02 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_02.prefab:c3032e38e784c3242ad7b3fd40203d11");

	// Token: 0x04002FBF RID: 12223
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_03 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_03.prefab:4201e41ed92e1f14ea49f878a286f1da");

	// Token: 0x04002FC0 RID: 12224
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_04 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_04.prefab:d8106260a8672ce41bdcd91f83b7da36");

	// Token: 0x04002FC1 RID: 12225
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_05 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_05.prefab:d1e099f03d91a40449a787e205662a9f");

	// Token: 0x04002FC2 RID: 12226
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IdleSpecial_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IdleSpecial_01.prefab:ddd69f71273eac147a90d2a6708ef77f");

	// Token: 0x04002FC3 RID: 12227
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroFirstEncounter_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroFirstEncounter_01.prefab:a0413350949dbe944b8eadb05dcdbbe0");

	// Token: 0x04002FC4 RID: 12228
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroGeneric_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroGeneric_01.prefab:d1ac9639c5615bd41ae8f4ed0d68c7a4");

	// Token: 0x04002FC5 RID: 12229
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1_01.prefab:2982cc607bb12c9499b0a7b6bab2d288");

	// Token: 0x04002FC6 RID: 12230
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1Retry_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1Retry_01.prefab:5d408ca9b9ac03b4ba8da2f251a62b09");

	// Token: 0x04002FC7 RID: 12231
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1Retry_02 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1Retry_02.prefab:ad5ed4e61852d8e459588cb7c85bb38e");

	// Token: 0x04002FC8 RID: 12232
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase2Retry_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase2Retry_01.prefab:7cef55607aab3964d9a12c9bd961fd31");

	// Token: 0x04002FC9 RID: 12233
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase3Retry_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase3Retry_01.prefab:6719959d771a07f44975aed2abbbed34");

	// Token: 0x04002FCA RID: 12234
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase3Retry_03 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase3Retry_03.prefab:5998b85acb10b5a4abe541a02eebaeb5");

	// Token: 0x04002FCB RID: 12235
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialBrann_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialBrann_01.prefab:df25e9123e451d247a441338d4c0d22e");

	// Token: 0x04002FCC RID: 12236
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialBrann2_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialBrann2_01.prefab:f0c0c501e6296ec44be7d1d6d5f33a98");

	// Token: 0x04002FCD RID: 12237
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialElise_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialElise_01.prefab:f8b3005946314c646b2b27920aa27536");

	// Token: 0x04002FCE RID: 12238
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialElise2_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialElise2_01.prefab:8efbfe3975972a2488ea91d9a8e095f1");

	// Token: 0x04002FCF RID: 12239
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialFinley_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialFinley_01.prefab:f19bb8621de37784796ae97019b38e41");

	// Token: 0x04002FD0 RID: 12240
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialFinley2_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialFinley2_01.prefab:cc63ac8b86153ca4ebb7c5109bae2402");

	// Token: 0x04002FD1 RID: 12241
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialReno_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialReno_01.prefab:6d8f7a16336fb9a4f8eb82f10a095302");

	// Token: 0x04002FD2 RID: 12242
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialReno2_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialReno2_01.prefab:526fbd609cb27594bb2c32df0908ee6a");

	// Token: 0x04002FD3 RID: 12243
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_PhaseChange1_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_PhaseChange1_01.prefab:f03b223b3fc21d74d830d67a22394750");

	// Token: 0x04002FD4 RID: 12244
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_PhaseChange2_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_PhaseChange2_01.prefab:9097a7cd2af1cae418b487911169efe3");

	// Token: 0x04002FD5 RID: 12245
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerJrExplorer_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerJrExplorer_01.prefab:7362d25550003de418965dd3d066a581");

	// Token: 0x04002FD6 RID: 12246
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerKingPhaoris_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerKingPhaoris_01.prefab:640358e8a8aba9a42a59c8a2cc944f82");

	// Token: 0x04002FD7 RID: 12247
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerSkilledPathfinder_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerSkilledPathfinder_01.prefab:54d2867b71e59d24fb40210971a0b52f");

	// Token: 0x04002FD8 RID: 12248
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerSunkeeperTarim_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerSunkeeperTarim_01.prefab:f157ae6fac6770544843a72c2d7a9111");

	// Token: 0x04002FD9 RID: 12249
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerZephrystheGreat_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerZephrystheGreat_01.prefab:d0179e10bce9d88439f73053e6c1daa4");

	// Token: 0x04002FDA RID: 12250
	private static readonly AssetReference Tombs_of_Terror_Brann_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Brann_BrassRing_Quote.prefab:d521a1fe41518e24da6e4252b97fbeb7");

	// Token: 0x04002FDB RID: 12251
	private static readonly AssetReference Tombs_of_Terror_Elise_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Elise_BrassRing_Quote.prefab:6e9e8faaae29f834183122d7ea9be68d");

	// Token: 0x04002FDC RID: 12252
	private static readonly AssetReference Tombs_of_Terror_Finley_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Finley_BrassRing_Quote.prefab:547ebc970764ec64da6eb3de26ed4698");

	// Token: 0x04002FDD RID: 12253
	private static readonly AssetReference Tombs_of_Terror_Reno_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Reno_BrassRing_Quote.prefab:4c0b79d4f597c464baabf02e06cf8ae7");

	// Token: 0x04002FDE RID: 12254
	private static readonly AssetReference Rafaam_popup_BrassRing_Quote = new AssetReference("Rafaam_popup_BrassRing_Quote.prefab:187724fae6d64cf49acf11aa53ca2087");

	// Token: 0x04002FDF RID: 12255
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_Elise_vs_Tekahn_b_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_Elise_vs_Tekahn_b_01.prefab:ee44a7efa3deff84e8c778c239a47cf4");

	// Token: 0x04002FE0 RID: 12256
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_TUT_Finale_Brann_Treasures_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_TUT_Finale_Brann_Treasures_01.prefab:75d3735ecc141cc409c426f0d519f149");

	// Token: 0x04002FE1 RID: 12257
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_TUT_Finale_Elise_Treasures_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_TUT_Finale_Elise_Treasures_01.prefab:e698826b7127cae429b490f8fa5e6b7f");

	// Token: 0x04002FE2 RID: 12258
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_TUT_Finale_Finley_Treasures_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_TUT_Finale_Finley_Treasures_01.prefab:a8c620ea58669a744b06dfd3b0b3ae94");

	// Token: 0x04002FE3 RID: 12259
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_TUT_Finale_Reno_Treasures_01 = new AssetReference("VO_ULDA_Reno_Male_Human_TUT_Finale_Reno_Treasures_01.prefab:1386554a1d31f2245b0f9e0a84c4968b");

	// Token: 0x04002FE4 RID: 12260
	private static readonly AssetReference VO_DALA_Rafaam_Male_Ethereal_STORY_Finale_FinalPhase_b_01 = new AssetReference("VO_DALA_Rafaam_Male_Ethereal_STORY_Finale_FinalPhase_b_01.prefab:8d25fab9fad46394894ed0001a36501f");

	// Token: 0x04002FE5 RID: 12261
	private static readonly AssetReference VO_DALA_Rafaam_Male_Ethereal_STORY_Finale_FinalPhase_c_01 = new AssetReference("VO_DALA_Rafaam_Male_Ethereal_STORY_Finale_FinalPhase_c_01.prefab:68c3a5a587ad7b64886ff1410b055d62");

	// Token: 0x04002FE6 RID: 12262
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_FinalPhase_a_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_FinalPhase_a_01.prefab:9c3335349390c3b4982a98bfbe3bb804");

	// Token: 0x04002FE7 RID: 12263
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_FinalPhase_d_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_FinalPhase_d_01.prefab:d3638361758c19d4793fc4a99414992b");

	// Token: 0x04002FE8 RID: 12264
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_Start_vs_Plague_Fire_01 = new AssetReference("VO_ULDA_Reno_Male_Human_Start_vs_Plague_Fire_01.prefab:ce58d269d5d3e634eb6b1a6ea38ccea3");

	// Token: 0x04002FE9 RID: 12265
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_Start_vs_Plague_Fire_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_Start_vs_Plague_Fire_01.prefab:3fdc759c78353af41aedd9805ecad104");

	// Token: 0x04002FEA RID: 12266
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_Start_vs_Plague_Fire_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_Start_vs_Plague_Fire_01.prefab:4b11912846eb2e04c95d83ddc3a4095a");

	// Token: 0x04002FEB RID: 12267
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_Start_vs_Plague_Fire_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_Start_vs_Plague_Fire_01.prefab:eca71404cedfc4e438ae240932f82537");

	// Token: 0x04002FEC RID: 12268
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_TUT_Finale_Final_Defeat_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_TUT_Finale_Final_Defeat_01.prefab:6df8872b16a0722449e24b1267325cb3");

	// Token: 0x04002FED RID: 12269
	private List<string> m_DefeatHeroLines = new List<string>
	{
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_01,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_02
	};

	// Token: 0x04002FEE RID: 12270
	private List<string> m_HeroPower1Lines = new List<string>
	{
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_01,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_02,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_03,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_04
	};

	// Token: 0x04002FEF RID: 12271
	private List<string> m_HeroPower2Lines = new List<string>
	{
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_01,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_02,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_03,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_04
	};

	// Token: 0x04002FF0 RID: 12272
	private List<string> m_HeroPower3Lines = new List<string>
	{
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_01,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_02,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_03,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_04,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_05
	};

	// Token: 0x04002FF1 RID: 12273
	private static List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_01,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_02,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_03,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_04,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_05
	};

	// Token: 0x04002FF2 RID: 12274
	private List<string> m_IntroLines = new List<string>
	{
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase3Retry_01,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase3Retry_03,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase2Retry_01,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1Retry_01,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1Retry_02,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroFirstEncounter_01,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroGeneric_01,
		ULDA_Dungeon_Boss_67h.VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1_01
	};

	// Token: 0x04002FF3 RID: 12275
	private List<string> m_IdleLinesCopy = new List<string>(ULDA_Dungeon_Boss_67h.m_IdleLines);

	// Token: 0x04002FF4 RID: 12276
	private bool m_LinesPlaying;

	// Token: 0x04002FF5 RID: 12277
	private HashSet<string> m_playedLines = new HashSet<string>();
}
