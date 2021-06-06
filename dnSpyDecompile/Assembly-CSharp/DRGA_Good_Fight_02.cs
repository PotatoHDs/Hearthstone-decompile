using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004DC RID: 1244
public class DRGA_Good_Fight_02 : DRGA_Dungeon
{
	// Token: 0x060042A8 RID: 17064 RVA: 0x00166EDC File Offset: 0x001650DC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Death_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Death_Alt_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Flavor_01_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_FreezeMinion_01_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_FreezeMinion_02_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_01_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_02_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_03_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_03_02,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_BossAttack_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_BossStart_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_EmoteResponse_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_01_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_02_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_03_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Player_Avalanche_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Player_LivingDragonbreath_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Boss_Flavor_02_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_ArchmageApprentice_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_ArchmageVargoth_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_AstralPortal_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Ending_01_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Ending_01_02,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_01_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_02_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_03_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Kalecgos_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_01_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_02_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_04_01,
			DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_PlayerStart_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060042A9 RID: 17065 RVA: 0x00167130 File Offset: 0x00165330
	public override List<string> GetIdleLines()
	{
		return this.m_VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_IdleLines;
	}

	// Token: 0x060042AA RID: 17066 RVA: 0x00167138 File Offset: 0x00165338
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPowerLines;
	}

	// Token: 0x060042AB RID: 17067 RVA: 0x00167140 File Offset: 0x00165340
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Death_01;
	}

	// Token: 0x060042AC RID: 17068 RVA: 0x00167158 File Offset: 0x00165358
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060042AD RID: 17069 RVA: 0x001671E9 File Offset: 0x001653E9
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 101:
			if (!this.m_Heroic)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayRandomLineAlways(friendlyActor, this.m_missionEventTrigger101Lines);
				GameState.Get().SetBusy(false);
				goto IL_CEB;
			}
			goto IL_CEB;
		case 102:
			if (!this.m_Heroic)
			{
				yield return base.PlayRandomLineAlways(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
				goto IL_CEB;
			}
			goto IL_CEB;
		case 103:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_01_01, 2.5f);
				goto IL_CEB;
			}
			goto IL_CEB;
		case 104:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_02_01, 2.5f);
				goto IL_CEB;
			}
			goto IL_CEB;
		case 105:
		case 106:
			break;
		case 107:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_missionEventTrigger107Lines);
			goto IL_CEB;
		case 108:
			yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_BossAttack_01, 2.5f);
			goto IL_CEB;
		case 109:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor, DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Flavor_01_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Boss_Flavor_02_01, 2.5f);
				goto IL_CEB;
			}
			goto IL_CEB;
		default:
			switch (missionEvent)
			{
			case 505:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			case 506:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			case 507:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			case 508:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			case 509:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			case 510:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			case 511:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			case 512:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			case 513:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			case 514:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			case 515:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			case 516:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			case 517:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			case 518:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			case 519:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			case 520:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			case 521:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			case 522:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			case 523:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			case 524:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			case 525:
				yield return base.PlayLineAlways(DRGA_Good_Fight_02.Wisdomball_Pop_up_BrassRing_Quote, DRGA_Good_Fight_02.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01, 2.5f);
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionKhadgarFloatingHeadResponses);
					goto IL_CEB;
				}
				goto IL_CEB;
			}
			break;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_CEB:
		yield break;
	}

	// Token: 0x060042AE RID: 17070 RVA: 0x001671FF File Offset: 0x001653FF
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
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 460940981U)
		{
			if (num != 122369965U)
			{
				if (num != 427385743U)
				{
					if (num == 460940981U)
					{
						if (cardId == "KARA_00_05")
						{
							if (!this.m_Heroic)
							{
								yield return base.PlayLineAlways(actor2, DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_04_01, 2.5f);
							}
						}
					}
				}
				else if (cardId == "KARA_00_07")
				{
					if (!this.m_Heroic)
					{
						yield return base.PlayLineOnlyOnce(actor2, DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_AstralPortal_01, 2.5f);
					}
				}
			}
			else if (cardId == "DRG_068")
			{
				yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Player_LivingDragonbreath_01, 2.5f);
			}
		}
		else if (num <= 985837770U)
		{
			if (num != 511273838U)
			{
				if (num == 985837770U)
				{
					if (cardId == "ICC_078")
					{
						yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Player_Avalanche_01, 2.5f);
					}
				}
			}
			else if (cardId == "KARA_00_08")
			{
				if (!this.m_Heroic)
				{
					yield return base.PlayLineOnlyOnce(actor2, DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_ArchmageApprentice_01, 2.5f);
				}
			}
		}
		else if (num != 1020708023U)
		{
			if (num == 3481427772U)
			{
				if (cardId == "DAL_609")
				{
					if (!this.m_Heroic)
					{
						yield return base.PlayLineOnlyOnce(actor2, DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Kalecgos_01, 2.5f);
					}
				}
			}
		}
		else if (cardId == "DAL_558")
		{
			if (!this.m_Heroic)
			{
				yield return base.PlayLineOnlyOnce(actor2, DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_ArchmageVargoth_01, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x060042AF RID: 17071 RVA: 0x00167215 File Offset: 0x00165415
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

	// Token: 0x0400331D RID: 13085
	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Death_01.prefab:da1b64ec5dcd7694eac0d67c240e4880");

	// Token: 0x0400331E RID: 13086
	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Death_Alt_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Death_Alt_01.prefab:daa92e634959fd844a8ff90d26cb542d");

	// Token: 0x0400331F RID: 13087
	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Flavor_01_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_Flavor_01_01.prefab:77b50b00c5814944e9e7d1750879d879");

	// Token: 0x04003320 RID: 13088
	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_FreezeMinion_01_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_FreezeMinion_01_01.prefab:ba2cb30279a95114ba3490f1c0083c41");

	// Token: 0x04003321 RID: 13089
	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_FreezeMinion_02_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_FreezeMinion_02_01.prefab:8b7cfaf8fff53124296c4c761a02233f");

	// Token: 0x04003322 RID: 13090
	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_01_01.prefab:dedf917f412f47542a9c050b21edd2a9");

	// Token: 0x04003323 RID: 13091
	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_02_01.prefab:466433cc1aa8ecc43969a24dd1d8c770");

	// Token: 0x04003324 RID: 13092
	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_03_01.prefab:f62ea41de2f121649a731cc0f546fddf");

	// Token: 0x04003325 RID: 13093
	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_03_02 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_03_02.prefab:056c5a4c611341e40836d37253810546");

	// Token: 0x04003326 RID: 13094
	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_BossAttack_01.prefab:484551f72cc900e478af602332b3a57e");

	// Token: 0x04003327 RID: 13095
	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_BossStart_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_BossStart_01.prefab:92219bb8f6e482f4ebe0ff4577c3ddfc");

	// Token: 0x04003328 RID: 13096
	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_EmoteResponse_01.prefab:145f4cbc25254034f9a5ad8fd60539ec");

	// Token: 0x04003329 RID: 13097
	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_01_01.prefab:849043988be8d9846a718f7fd239bc0e");

	// Token: 0x0400332A RID: 13098
	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_02_01.prefab:d85dc236aeb873d4fa10374d9300d21c");

	// Token: 0x0400332B RID: 13099
	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_03_01.prefab:6374771862d99fd4994ff3c74b2b9c36");

	// Token: 0x0400332C RID: 13100
	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Player_Avalanche_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Player_Avalanche_01.prefab:084f81aecfe06df449a572536d4eaa59");

	// Token: 0x0400332D RID: 13101
	private static readonly AssetReference VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Player_LivingDragonbreath_01 = new AssetReference("VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Player_LivingDragonbreath_01.prefab:1cedd3728cd8ff44ea197eb5647cff93");

	// Token: 0x0400332E RID: 13102
	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Boss_Flavor_02_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Boss_Flavor_02_01.prefab:4f6c3d04538a92d429c9f4d789b5eb07");

	// Token: 0x0400332F RID: 13103
	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_ArchmageApprentice_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_ArchmageApprentice_01.prefab:5ec2e3a6a6006d94dab3ed0d1cf93d1f");

	// Token: 0x04003330 RID: 13104
	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_ArchmageVargoth_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_ArchmageVargoth_01.prefab:b8ffffc14e96748449566ec8c313510e");

	// Token: 0x04003331 RID: 13105
	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_AstralPortal_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_AstralPortal_01.prefab:8d720509ea9e3824e87170ef9e9ab477");

	// Token: 0x04003332 RID: 13106
	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Ending_01_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Ending_01_01.prefab:6b325da46c713b346ad9543e8080278e");

	// Token: 0x04003333 RID: 13107
	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Ending_01_02 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Ending_01_02.prefab:21651a92447ed7446a7f9d94fe265409");

	// Token: 0x04003334 RID: 13108
	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_01_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_01_01.prefab:6e44668e738fee74a8e4bb3a3259847f");

	// Token: 0x04003335 RID: 13109
	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_02_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_02_01.prefab:0ba035d391033da47aaa12d9adcf4948");

	// Token: 0x04003336 RID: 13110
	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_03_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_03_01.prefab:739430c7762668c49952cd3e2064e848");

	// Token: 0x04003337 RID: 13111
	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Kalecgos_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Kalecgos_01.prefab:69e3d5df75cfe194c873300fbf4659ee");

	// Token: 0x04003338 RID: 13112
	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_01_01.prefab:ffdfb1dc1b8245449aec6c313217859f");

	// Token: 0x04003339 RID: 13113
	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_02_01.prefab:5d6ae6e4476436047bce3df63871e018");

	// Token: 0x0400333A RID: 13114
	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Misc_04_01.prefab:291af12de6ca34a4498f88f43cf2a53e");

	// Token: 0x0400333B RID: 13115
	private static readonly AssetReference VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_PlayerStart_01.prefab:045e2a1330885054cbff9c52418d5c5e");

	// Token: 0x0400333C RID: 13116
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01.prefab:bbb71f2dfac1c474da3209a15215f4ea");

	// Token: 0x0400333D RID: 13117
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01.prefab:ad01bc4d23eab3e4f86c994d722cf247");

	// Token: 0x0400333E RID: 13118
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01.prefab:688849bb5a4d2fd48a817159d1a224fa");

	// Token: 0x0400333F RID: 13119
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01.prefab:001c634fac4ab874eb6e16d050554b1f");

	// Token: 0x04003340 RID: 13120
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01.prefab:a5d093ea8ec90d64fb53834b44395720");

	// Token: 0x04003341 RID: 13121
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01.prefab:97a9c24cb50609347964387b02a62b3c");

	// Token: 0x04003342 RID: 13122
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01.prefab:feb7534214a13214bac9e2b8726a8cc8");

	// Token: 0x04003343 RID: 13123
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01.prefab:3fd9e692fe70e924fa0fb5bfabcf17bd");

	// Token: 0x04003344 RID: 13124
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01.prefab:701ed0ac22ab9a84daade7ed23403317");

	// Token: 0x04003345 RID: 13125
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01.prefab:f2eeac3ed0b6f554dbfbc9deea60739f");

	// Token: 0x04003346 RID: 13126
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01.prefab:f9054e8df8774e44d869a0f96ac07efa");

	// Token: 0x04003347 RID: 13127
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01.prefab:d0a3f9b5c01e04d458178ca8c5069d66");

	// Token: 0x04003348 RID: 13128
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01.prefab:ab486ac19b475c74f84999cc9a80b7a6");

	// Token: 0x04003349 RID: 13129
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01.prefab:8789714bb9a92d143bb2024188b8ddd0");

	// Token: 0x0400334A RID: 13130
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01.prefab:7fd61e2a38015f240bef703ee9f66e5c");

	// Token: 0x0400334B RID: 13131
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01.prefab:9273a8457f705514f9755153f0c7abf6");

	// Token: 0x0400334C RID: 13132
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01.prefab:cad9725371b04e943b07f43ecac56b32");

	// Token: 0x0400334D RID: 13133
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01.prefab:ce7a5a15de006d041ad515427fc6f72f");

	// Token: 0x0400334E RID: 13134
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01.prefab:92c7854b16b6919499ff3fe7e1e2a422");

	// Token: 0x0400334F RID: 13135
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01.prefab:3edd1b61bd705b6439dd75542dd6b442");

	// Token: 0x04003350 RID: 13136
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01.prefab:b9fae030ab3026a4bb17f592028c276d");

	// Token: 0x04003351 RID: 13137
	private static readonly AssetReference Wisdomball_Pop_up_BrassRing_Quote = new AssetReference("Wisdomball_Pop-up_BrassRing_Quote.prefab:896ee20514caff74db639aa7055838f6");

	// Token: 0x04003352 RID: 13138
	private List<string> m_missionEventTrigger101Lines = new List<string>
	{
		DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Ending_01_01,
		DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_Ending_01_02
	};

	// Token: 0x04003353 RID: 13139
	private List<string> m_missionKhadgarFloatingHeadResponses = new List<string>
	{
		DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_01_01,
		DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_02_01,
		DRGA_Good_Fight_02.VO_DRGA_BOSS_35h_Male_Human_Good_Fight_02_Khadgar_FloatingHead_03_01
	};

	// Token: 0x04003354 RID: 13140
	private List<string> m_missionEventTrigger107Lines = new List<string>
	{
		DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_FreezeMinion_01_01,
		DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_FreezeMinion_02_01
	};

	// Token: 0x04003355 RID: 13141
	private List<string> m_VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPowerLines = new List<string>
	{
		DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_01_01,
		DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_02_01,
		DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_03_01,
		DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Boss_HeroPower_03_02
	};

	// Token: 0x04003356 RID: 13142
	private List<string> m_VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_IdleLines = new List<string>
	{
		DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_01_01,
		DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_02_01,
		DRGA_Good_Fight_02.VO_DRGA_BOSS_10h_Male_Elemental_Good_Fight_02_Idle_03_01
	};

	// Token: 0x04003357 RID: 13143
	private HashSet<string> m_playedLines = new HashSet<string>();
}
