using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004E4 RID: 1252
public class DRGA_Good_Fight_10 : DRGA_Dungeon
{
	// Token: 0x06004312 RID: 17170 RVA: 0x0016AAE0 File Offset: 0x00168CE0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_DeathHeroic_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_EyeforanEye_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_01_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_02_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_03_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_Weapon_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossAttack_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossStart_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossStartHeroic_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_EmoteResponse_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_EmoteResponseHeroic_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Ending_02_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_HealingSpell_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_01_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_01b_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_02_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_03_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_01_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_02_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_03_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_04_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_05_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Kobold_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_LostintheJungle_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Togwaggle_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Vinecleaver_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Ending_01_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_01_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_02_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_03_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_04_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_05_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_06_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Hero_Emote_Threaten_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_Karl_01,
			DRGA_Good_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_Karl_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004313 RID: 17171 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004314 RID: 17172 RVA: 0x0016AD84 File Offset: 0x00168F84
	public override List<string> GetIdleLines()
	{
		return this.m_VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_IdleLines;
	}

	// Token: 0x06004315 RID: 17173 RVA: 0x0016AD8C File Offset: 0x00168F8C
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPowerLines;
	}

	// Token: 0x06004316 RID: 17174 RVA: 0x0016AD94 File Offset: 0x00168F94
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		if (!this.m_Heroic)
		{
			this.m_VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_IdleLines.Add(DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_01_01);
		}
		if (this.m_Heroic)
		{
			this.m_deathLine = DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_DeathHeroic_01;
			this.m_VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_IdleLines.Add(DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_01b_01);
		}
	}

	// Token: 0x06004317 RID: 17175 RVA: 0x0016ADF4 File Offset: 0x00168FF4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
		else
		{
			if (!MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
			{
				return;
			}
			if (!this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_EmoteResponseHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
	}

	// Token: 0x06004318 RID: 17176 RVA: 0x0016AF05 File Offset: 0x00169105
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 100:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_EyeforanEye_01, 2.5f);
			goto IL_640;
		case 101:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_Weapon_01, 2.5f);
			goto IL_640;
		case 102:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossAttack_01, 2.5f);
			goto IL_640;
		case 104:
			yield return base.PlayLineAlways(enemyActor, DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_HealingSpell_01, 2.5f);
			goto IL_640;
		case 109:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_01_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_01_01, 2.5f);
				goto IL_640;
			}
			goto IL_640;
		case 111:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_03_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_03_01, 2.5f);
				goto IL_640;
			}
			goto IL_640;
		case 112:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_04_01, 2.5f);
				goto IL_640;
			}
			goto IL_640;
		case 114:
			if (!this.m_Heroic)
			{
				goto IL_640;
			}
			goto IL_640;
		case 115:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(enemyActor, DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_02_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_02_01, 2.5f);
				goto IL_640;
			}
			goto IL_640;
		case 116:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(enemyActor, DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_04_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_05_01, 2.5f);
				goto IL_640;
			}
			goto IL_640;
		case 117:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(enemyActor, DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_05_01, 2.5f);
				goto IL_640;
			}
			goto IL_640;
		case 120:
			if (!this.m_Heroic)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Hero_Emote_Threaten_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_640;
			}
			goto IL_640;
		case 121:
			if (!this.m_Heroic)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_01_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Ending_01_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_640;
			}
			goto IL_640;
		case 122:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_missionEventHeroPowerKarl);
			goto IL_640;
		case 123:
			if (!this.m_Heroic)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Ending_02_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_06_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_640;
			}
			goto IL_640;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_640:
		yield break;
	}

	// Token: 0x06004319 RID: 17177 RVA: 0x0016AF1B File Offset: 0x0016911B
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
		if (num <= 2096106050U)
		{
			if (num <= 888934466U)
			{
				if (num <= 636897723U)
				{
					if (num != 216571437U)
					{
						if (num != 636897723U)
						{
							goto IL_4D0;
						}
						if (!(cardId == "LOOT_998k"))
						{
							goto IL_4D0;
						}
					}
					else if (!(cardId == "LOOT_014"))
					{
						goto IL_4D0;
					}
				}
				else if (num != 734107143U)
				{
					if (num != 888934466U)
					{
						goto IL_4D0;
					}
					if (!(cardId == "OG_082"))
					{
						goto IL_4D0;
					}
				}
				else if (!(cardId == "LOOT_531"))
				{
					goto IL_4D0;
				}
			}
			else if (num <= 1340872041U)
			{
				if (num != 1110709447U)
				{
					if (num != 1340872041U)
					{
						goto IL_4D0;
					}
					if (!(cardId == "CS2_142"))
					{
						goto IL_4D0;
					}
				}
				else if (!(cardId == "LOOT_412"))
				{
					goto IL_4D0;
				}
			}
			else if (num != 1510711040U)
			{
				if (num != 2079328431U)
				{
					if (num != 2096106050U)
					{
						goto IL_4D0;
					}
					if (!(cardId == "LOOT_042"))
					{
						goto IL_4D0;
					}
				}
				else if (!(cardId == "LOOT_041"))
				{
					goto IL_4D0;
				}
			}
			else if (!(cardId == "ULD_184"))
			{
				goto IL_4D0;
			}
		}
		else if (num <= 2747171160U)
		{
			if (num <= 2234672673U)
			{
				if (num != 2230621192U)
				{
					if (num != 2234672673U)
					{
						goto IL_4D0;
					}
					if (!(cardId == "DRG_082"))
					{
						goto IL_4D0;
					}
				}
				else if (!(cardId == "LOOT_062"))
				{
					goto IL_4D0;
				}
			}
			else if (num != 2425706637U)
			{
				if (num != 2664405965U)
				{
					if (num != 2747171160U)
					{
						goto IL_4D0;
					}
					if (!(cardId == "LOOT_541"))
					{
						goto IL_4D0;
					}
					yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Togwaggle_01, 2.5f);
					goto IL_4D0;
				}
				else if (!(cardId == "TOT_033"))
				{
					goto IL_4D0;
				}
			}
			else
			{
				if (!(cardId == "UNG_960"))
				{
					goto IL_4D0;
				}
				yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_LostintheJungle_01, 2.5f);
				goto IL_4D0;
			}
		}
		else if (num <= 3450734459U)
		{
			if (num != 3333291126U)
			{
				if (num != 3450734459U)
				{
					goto IL_4D0;
				}
				if (!(cardId == "LOOT_382"))
				{
					goto IL_4D0;
				}
			}
			else if (!(cardId == "LOOT_389"))
			{
				goto IL_4D0;
			}
		}
		else if (num != 3500375768U)
		{
			if (num != 3598724010U)
			{
				if (num != 4137170870U)
				{
					goto IL_4D0;
				}
				if (!(cardId == "UNG_950"))
				{
					goto IL_4D0;
				}
				GameState.Get().SetBusy(true);
				yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Vinecleaver_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_4D0;
			}
			else if (!(cardId == "DAL_614"))
			{
				goto IL_4D0;
			}
		}
		else if (!(cardId == "LOOT_347"))
		{
			goto IL_4D0;
		}
		yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Kobold_01, 2.5f);
		IL_4D0:
		yield break;
	}

	// Token: 0x0600431A RID: 17178 RVA: 0x0016AF31 File Offset: 0x00169131
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

	// Token: 0x0400346F RID: 13423
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_DeathHeroic_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_DeathHeroic_01.prefab:50ad3af71651dee48b1c5238599d6df4");

	// Token: 0x04003470 RID: 13424
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_EyeforanEye_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_EyeforanEye_01.prefab:08019cbc4a328ed4f9d558b9ecb00481");

	// Token: 0x04003471 RID: 13425
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_01_01.prefab:ef13e0c9db32dd34d8cb7a622fff2e3d");

	// Token: 0x04003472 RID: 13426
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_02_01.prefab:9f21066db9f25ff46b1c65f88754a3e2");

	// Token: 0x04003473 RID: 13427
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_03_01.prefab:40510a55066c2f74699c40021a91efcc");

	// Token: 0x04003474 RID: 13428
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_Weapon_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_Weapon_01.prefab:1d7e42cf31e31e24983e5b9dbdecdf22");

	// Token: 0x04003475 RID: 13429
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossAttack_01.prefab:3f8c74f77cc11f643b2c74cfdeb4d1f9");

	// Token: 0x04003476 RID: 13430
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossStart_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossStart_01.prefab:2cc5ca550881eb841964d51ffe02e244");

	// Token: 0x04003477 RID: 13431
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_BossStartHeroic_01.prefab:468a554b1b975f04eaece5bf0e35d56a");

	// Token: 0x04003478 RID: 13432
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_EmoteResponse_01.prefab:0ab6b9d5f4387b04194855ba3a63d8ae");

	// Token: 0x04003479 RID: 13433
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_EmoteResponseHeroic_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_EmoteResponseHeroic_01.prefab:3bb9683d5c54a1644a7fc5cdb2b969d6");

	// Token: 0x0400347A RID: 13434
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Ending_02_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Ending_02_01.prefab:6fefb79902929c64487b54f940fa130a");

	// Token: 0x0400347B RID: 13435
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_HealingSpell_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_HealingSpell_01.prefab:52f6e165a24a0634cadfcacab3f6022d");

	// Token: 0x0400347C RID: 13436
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_01_01.prefab:606c56ef66b98e74a9be873b52d8ae3c");

	// Token: 0x0400347D RID: 13437
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_01b_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_01b_01.prefab:1ae53f0a3148ebf45a0b65355469bf97");

	// Token: 0x0400347E RID: 13438
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_02_01.prefab:566a52eddf171aa42ae3c0d1efda0c4e");

	// Token: 0x0400347F RID: 13439
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_03_01.prefab:f9c49e71ac3db36429fb193c1f706f09");

	// Token: 0x04003480 RID: 13440
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_01_01.prefab:7c56039b14fd28047bed95cc571dd27f");

	// Token: 0x04003481 RID: 13441
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_02_01.prefab:10019f7b141216b4e97a1838e337ef44");

	// Token: 0x04003482 RID: 13442
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_03_01.prefab:510e7486be1dee44ea7fe5701740e2fb");

	// Token: 0x04003483 RID: 13443
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_04_01.prefab:7bd4e45f65533a44caf048675c453d9c");

	// Token: 0x04003484 RID: 13444
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Misc_05_01.prefab:f4efc27bc8698a343b3c264fa9887228");

	// Token: 0x04003485 RID: 13445
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Kobold_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Kobold_01.prefab:8f0b2837e2a397c4ea842b914427da02");

	// Token: 0x04003486 RID: 13446
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_LostintheJungle_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_LostintheJungle_01.prefab:7d66c4d38a6f07d41bd0bbfe64518580");

	// Token: 0x04003487 RID: 13447
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Togwaggle_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Togwaggle_01.prefab:b13e078bb0706754e9c85ccd4448a369");

	// Token: 0x04003488 RID: 13448
	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Vinecleaver_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Player_Vinecleaver_01.prefab:d7ec92988cada1343b672f2349ef9f44");

	// Token: 0x04003489 RID: 13449
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Ending_01_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Ending_01_01.prefab:6b86b69b7f5d6054c97330f9f80a45d3");

	// Token: 0x0400348A RID: 13450
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_01_01.prefab:3c36c9b338e28ab47a1c4793ee228c3b");

	// Token: 0x0400348B RID: 13451
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_02_01.prefab:66d27281b0b79f546a411ad1d1f42237");

	// Token: 0x0400348C RID: 13452
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_03_01.prefab:ecda67edf924f9144b1fee415c1a5cd3");

	// Token: 0x0400348D RID: 13453
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_04_01.prefab:147a9e873b024cd46ba2542b5d506b71");

	// Token: 0x0400348E RID: 13454
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_05_01.prefab:5dc482fb65804e642b98ea2097cda4a4");

	// Token: 0x0400348F RID: 13455
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_06_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Misc_06_01.prefab:95dd01aabfee8154e9b8e6c637395c47");

	// Token: 0x04003490 RID: 13456
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Hero_Emote_Threaten_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Good_Fight_10_Hero_Emote_Threaten_01.prefab:c170b366458989f49bd736c3b00db00d");

	// Token: 0x04003491 RID: 13457
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_Karl_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_Karl_01.prefab:038cf01d36976394cb45c66da9ba7fa5");

	// Token: 0x04003492 RID: 13458
	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_Karl_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_Karl_01.prefab:77abd879f3c394643940e0227ffeadbd");

	// Token: 0x04003493 RID: 13459
	private List<string> m_VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPowerLines = new List<string>
	{
		DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_01_01,
		DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_02_01,
		DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Boss_HeroPower_03_01
	};

	// Token: 0x04003494 RID: 13460
	private List<string> m_VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_IdleLines = new List<string>
	{
		DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_02_01,
		DRGA_Good_Fight_10.VO_DRGA_BOSS_11h_Male_Human_Good_Fight_10_Idle_03_01
	};

	// Token: 0x04003495 RID: 13461
	private List<string> m_missionEventHeroPowerKarl = new List<string>
	{
		DRGA_Good_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_Karl_01,
		DRGA_Good_Fight_10.VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_Karl_01
	};

	// Token: 0x04003496 RID: 13462
	private HashSet<string> m_playedLines = new HashSet<string>();
}
