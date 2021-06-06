using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004E5 RID: 1253
public class DRGA_Good_Fight_11 : DRGA_Dungeon
{
	// Token: 0x06004320 RID: 17184 RVA: 0x0016B224 File Offset: 0x00169424
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Good_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Misc_04_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_01_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_02_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_03_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Released_02_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Released_03_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Misc_03_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Misc_05_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Reno_Released_01_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_Death_01_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_Death_02_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_01_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_02_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_03_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_04_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_05_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_WaxLance_01_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_WaxLance_02_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossAttack_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossStart_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossStartHeroic_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_EmoteResponse_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_01_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_02_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_03_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Misc_01_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Misc_02_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Elise_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_EVIL_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_01_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_02_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_03_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_04_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Togwaggle_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Waxadred_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_WaxElemental_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Reno_Released_04_01,
			DRGA_Good_Fight_11.VO_ULDA_Reno_Male_Human_Concede_01,
			DRGA_Good_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Attack_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004321 RID: 17185 RVA: 0x0016B4F8 File Offset: 0x001696F8
	public override List<string> GetIdleLines()
	{
		return this.m_VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_IdleLines;
	}

	// Token: 0x06004322 RID: 17186 RVA: 0x0016B500 File Offset: 0x00169700
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPowerLines;
	}

	// Token: 0x06004323 RID: 17187 RVA: 0x0016B508 File Offset: 0x00169708
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_Death_01_01;
	}

	// Token: 0x06004324 RID: 17188 RVA: 0x0016B520 File Offset: 0x00169720
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004325 RID: 17189 RVA: 0x0016B5F0 File Offset: 0x001697F0
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
		case 100:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor, DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Misc_01_01, 2.5f);
				goto IL_3C3;
			}
			goto IL_3C3;
		case 101:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor, DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Misc_02_01, 2.5f);
				goto IL_3C3;
			}
			goto IL_3C3;
		case 104:
			if (!this.m_Heroic)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("DRGA_001"), DRGA_Dungeon.RenoBrassRing, DRGA_Good_Fight_11.VO_ULDA_Reno_Male_Human_Concede_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_11.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Misc_05_01, 2.5f);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("DRGA_001"), DRGA_Dungeon.RenoBrassRing, DRGA_Good_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Attack_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_3C3;
			}
			goto IL_3C3;
		case 106:
			yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossAttack_01, 2.5f);
			goto IL_3C3;
		case 107:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor, DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Reno_Released_04_01, 2.5f);
				goto IL_3C3;
			}
			goto IL_3C3;
		case 108:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineInOrderOnceWithBrassRing(base.GetEnemyActorByCardId("DRGA_001t"), DRGA_Dungeon.RenoBrassRing, this.m_missionEventTrigger108Lines);
				goto IL_3C3;
			}
			goto IL_3C3;
		case 109:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_11.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Reno_Released_01_01, 2.5f);
				goto IL_3C3;
			}
			goto IL_3C3;
		case 110:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(DRGA_Dungeon.RenoBrassRing, DRGA_Good_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Released_02_01, 2.5f);
				yield return base.PlayLineAlways(DRGA_Dungeon.RenoBrassRing, DRGA_Good_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Released_03_01, 2.5f);
				goto IL_3C3;
			}
			goto IL_3C3;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_3C3:
		yield break;
	}

	// Token: 0x06004326 RID: 17190 RVA: 0x0016B606 File Offset: 0x00169806
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
		if (num <= 2714263018U)
		{
			if (num <= 632763696U)
			{
				if (num != 121531322U)
				{
					if (num != 476329529U)
					{
						if (num != 632763696U)
						{
							goto IL_45F;
						}
						if (!(cardId == "LOE_079"))
						{
							goto IL_45F;
						}
					}
					else
					{
						if (!(cardId == "DAL_431"))
						{
							goto IL_45F;
						}
						goto IL_432;
					}
				}
				else
				{
					if (!(cardId == "DRG_036"))
					{
						goto IL_45F;
					}
					yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Waxadred_01, 2.5f);
					goto IL_45F;
				}
			}
			else if (num <= 963594556U)
			{
				if (num != 778032481U)
				{
					if (num != 963594556U)
					{
						goto IL_45F;
					}
					if (!(cardId == "GVG_110"))
					{
						goto IL_45F;
					}
					goto IL_432;
				}
				else
				{
					if (!(cardId == "DAL_417"))
					{
						goto IL_45F;
					}
					goto IL_432;
				}
			}
			else if (num != 2663783066U)
			{
				if (num != 2714263018U)
				{
					goto IL_45F;
				}
				if (!(cardId == "UNG_851"))
				{
					goto IL_45F;
				}
			}
			else if (!(cardId == "UNG_842"))
			{
				goto IL_45F;
			}
		}
		else if (num <= 3719504201U)
		{
			if (num != 2747171160U)
			{
				if (num != 3084683336U)
				{
					if (num != 3719504201U)
					{
						goto IL_45F;
					}
					if (!(cardId == "DAL_729"))
					{
						goto IL_45F;
					}
					goto IL_432;
				}
				else
				{
					if (!(cardId == "DRGA_BOSS_08t2"))
					{
						goto IL_45F;
					}
					yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandleLines);
					goto IL_45F;
				}
			}
			else
			{
				if (!(cardId == "LOOT_541"))
				{
					goto IL_45F;
				}
				yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Togwaggle_01, 2.5f);
				goto IL_45F;
			}
		}
		else if (num <= 4135230619U)
		{
			if (num != 3875516676U)
			{
				if (num != 4135230619U)
				{
					goto IL_45F;
				}
				if (!(cardId == "LOOT_117"))
				{
					goto IL_45F;
				}
				yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_WaxElemental_01, 2.5f);
				goto IL_45F;
			}
			else if (!(cardId == "ULD_139"))
			{
				goto IL_45F;
			}
		}
		else if (num != 4176367366U)
		{
			if (num != 4196772219U)
			{
				goto IL_45F;
			}
			if (!(cardId == "DAL_064"))
			{
				goto IL_45F;
			}
			goto IL_432;
		}
		else if (!(cardId == "UNG_068"))
		{
			goto IL_45F;
		}
		yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Elise_01, 2.5f);
		goto IL_45F;
		IL_432:
		yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_EVIL_01, 2.5f);
		IL_45F:
		yield break;
	}

	// Token: 0x06004327 RID: 17191 RVA: 0x0016B61C File Offset: 0x0016981C
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
		if (cardId == "DRGA_BOSS_08t")
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_WaxLanceLines);
		}
		yield break;
	}

	// Token: 0x06004328 RID: 17192 RVA: 0x0016B632 File Offset: 0x00169832
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return base.PlayLineAlways(DRGA_Dungeon.EliseBrassRing, DRGA_Good_Fight_11.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Misc_03_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003497 RID: 13463
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Misc_04_01.prefab:fbfe70bab9271e746adc673ebe4e8ab4");

	// Token: 0x04003498 RID: 13464
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_01_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_01_01.prefab:f74d8edc07731114989fe9757034209b");

	// Token: 0x04003499 RID: 13465
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_02_01.prefab:0a1a07429fb26c54fb1c62ed40cc8b78");

	// Token: 0x0400349A RID: 13466
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_03_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_03_01.prefab:894edfd6a3537c74cb45f8556aeb0da1");

	// Token: 0x0400349B RID: 13467
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Released_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Released_02_01.prefab:206d3738080618243bc7f3c06db1b883");

	// Token: 0x0400349C RID: 13468
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Released_03_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Released_03_01.prefab:df986bea55a081e479adecedfe69a161");

	// Token: 0x0400349D RID: 13469
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Misc_03_01.prefab:922d8f2382ebca54fbf72aafa8fae8da");

	// Token: 0x0400349E RID: 13470
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Misc_05_01.prefab:7cdc013b0c4944a4c8e9528828c891b0");

	// Token: 0x0400349F RID: 13471
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Reno_Released_01_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Reno_Released_01_01.prefab:b70ceab4d9979874e9b8d851b9353c7f");

	// Token: 0x040034A0 RID: 13472
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_Death_01_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_Death_01_01.prefab:6869eeb43168cd848889043266d7c25f");

	// Token: 0x040034A1 RID: 13473
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_Death_02_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_Death_02_01.prefab:bba5ecfc9c1fde141b0e0dd5e4b1f834");

	// Token: 0x040034A2 RID: 13474
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_01_01.prefab:220162e7b22893749a0b411c22b1934c");

	// Token: 0x040034A3 RID: 13475
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_02_01.prefab:159461fc0fa70df4393eab97799e5210");

	// Token: 0x040034A4 RID: 13476
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_03_01.prefab:4bc00c695fb8b484396fb9bb689f96ae");

	// Token: 0x040034A5 RID: 13477
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_04_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_04_01.prefab:b3def71dc86798c409c650cf773d3139");

	// Token: 0x040034A6 RID: 13478
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_05_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_05_01.prefab:79bd22a399d3ab44c8c4d4f393845add");

	// Token: 0x040034A7 RID: 13479
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_WaxLance_01_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_WaxLance_01_01.prefab:3e47bcacdd236f74d965267e175a0afd");

	// Token: 0x040034A8 RID: 13480
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_WaxLance_02_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_WaxLance_02_01.prefab:a8845bbc3c5959c4dacd0cef13fc4520");

	// Token: 0x040034A9 RID: 13481
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossAttack_01.prefab:481791c50354af841a6de7acc5012b9d");

	// Token: 0x040034AA RID: 13482
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossStart_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossStart_01.prefab:e5a3aff30a058104fa104f92e5908e27");

	// Token: 0x040034AB RID: 13483
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossStartHeroic_01.prefab:2d0847ccbc7c45845b7a889b958ff1f2");

	// Token: 0x040034AC RID: 13484
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_EmoteResponse_01.prefab:8beaf06a45daeca49a3cf2d33b2e2a67");

	// Token: 0x040034AD RID: 13485
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_01_01.prefab:b2d04decd9b813546a4dfabe16bbd089");

	// Token: 0x040034AE RID: 13486
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_02_01.prefab:5b880c8f7719f11448cbd1ace31e423f");

	// Token: 0x040034AF RID: 13487
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_03_01.prefab:542db798fb216774399849763b45f295");

	// Token: 0x040034B0 RID: 13488
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Misc_01_01.prefab:90a00acc56f54fe43b9215a22028e993");

	// Token: 0x040034B1 RID: 13489
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Misc_02_01.prefab:eaefd2d85494e5b458cdb91d03b75fa5");

	// Token: 0x040034B2 RID: 13490
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Elise_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Elise_01.prefab:d065d4c7f99ed79489450bffd7c050c0");

	// Token: 0x040034B3 RID: 13491
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_EVIL_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_EVIL_01.prefab:0d6bf612a8bf70f48a12ec2940abc737");

	// Token: 0x040034B4 RID: 13492
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_01_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_01_01.prefab:cbb06829f46aa2140b3eecaca2dea947");

	// Token: 0x040034B5 RID: 13493
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_02_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_02_01.prefab:d90f22b6ece2ded48baff8cb7040cd1d");

	// Token: 0x040034B6 RID: 13494
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_03_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_03_01.prefab:cfdd3a9168280bd41834d8e01b228edb");

	// Token: 0x040034B7 RID: 13495
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_04_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_04_01.prefab:796a06f5fbbf9b54d9346b3fcf6c0510");

	// Token: 0x040034B8 RID: 13496
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Togwaggle_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Togwaggle_01.prefab:cd1554cd9b1cdfe4cb4ce7edc65c831d");

	// Token: 0x040034B9 RID: 13497
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Waxadred_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Waxadred_01.prefab:e7e8732bf2daa15499d680d5797e7af8");

	// Token: 0x040034BA RID: 13498
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_WaxElemental_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_WaxElemental_01.prefab:1926bfd1e513e184891b03bf60017430");

	// Token: 0x040034BB RID: 13499
	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Reno_Released_04_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Reno_Released_04_01.prefab:eb7b1bc3f68a81540ac20ed0bfee152c");

	// Token: 0x040034BC RID: 13500
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_Concede_01 = new AssetReference("VO_ULDA_Reno_Male_Human_Concede_01.prefab:eaf3e0c057f25084284998291c6bb914");

	// Token: 0x040034BD RID: 13501
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Attack_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Attack_01.prefab:80a2b1d3517200846a4777a2733b5b60");

	// Token: 0x040034BE RID: 13502
	private List<string> m_missionEventTrigger108Lines = new List<string>
	{
		DRGA_Good_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_01_01,
		DRGA_Good_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_02_01,
		DRGA_Good_Fight_11.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_03_01
	};

	// Token: 0x040034BF RID: 13503
	private List<string> m_VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPowerLines = new List<string>
	{
		DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_01_01,
		DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_02_01,
		DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_03_01,
		DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_04_01,
		DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_05_01
	};

	// Token: 0x040034C0 RID: 13504
	private List<string> m_VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_WaxLanceLines = new List<string>
	{
		DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_WaxLance_01_01,
		DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_WaxLance_02_01
	};

	// Token: 0x040034C1 RID: 13505
	private List<string> m_VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_IdleLines = new List<string>
	{
		DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_01_01,
		DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_02_01,
		DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_03_01
	};

	// Token: 0x040034C2 RID: 13506
	private List<string> m_VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandleLines = new List<string>
	{
		DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_01_01,
		DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_02_01,
		DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_03_01,
		DRGA_Good_Fight_11.VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_04_01
	};

	// Token: 0x040034C3 RID: 13507
	private HashSet<string> m_playedLines = new HashSet<string>();
}
