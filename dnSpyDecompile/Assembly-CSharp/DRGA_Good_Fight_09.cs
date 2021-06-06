using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004E3 RID: 1251
public class DRGA_Good_Fight_09 : DRGA_Dungeon
{
	// Token: 0x06004305 RID: 17157 RVA: 0x0016A074 File Offset: 0x00168274
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Good_Fight_09.VO_DRGA_BOSS_15h_Male_Orc_Good_Fight_09_Kragg_EnterBattle_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Attack_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Concede_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Death_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Greetings_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Oops_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Thanks_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Threaten_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_WellPlayed_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Wow_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_EnemyLowHealth_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Error_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_01_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_02_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_03_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_GreaseMonkey_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_01_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_02_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_03_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Kragg_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Low_Cards_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_LowHealth_01_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_LowHealth_02_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_MissilePod_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Out_Of_Cards_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_01_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_02_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_03_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Thinking_01_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Thinking_02_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Thinking_03_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Time_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_UpgradedCannon_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_PlayerStart_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_BombFlinger_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_Death_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_DeathRay_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_EnemyLowHealth_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_01_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_02_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_03_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_01_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_02_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_03_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_01_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_02_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_03_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_01_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_02_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_03_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_RocketLauncher_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_UpgradedCannon_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_BossAttack_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_BossStart_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_EmoteResponse_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_01_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_02_01,
			DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_03_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004306 RID: 17158 RVA: 0x0016A478 File Offset: 0x00168678
	public override List<string> GetIdleLines()
	{
		return this.m_VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_IdleLines;
	}

	// Token: 0x06004307 RID: 17159 RVA: 0x0016A480 File Offset: 0x00168680
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPowerLines;
	}

	// Token: 0x06004308 RID: 17160 RVA: 0x0016A488 File Offset: 0x00168688
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_Death_01;
	}

	// Token: 0x06004309 RID: 17161 RVA: 0x0016A4A0 File Offset: 0x001686A0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600430A RID: 17162 RVA: 0x0016A531 File Offset: 0x00168731
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 102:
			yield return base.PlayLineInOrderOnce(enemyActor, this.m_missionEventTrigger102Lines);
			goto IL_265;
		case 104:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_BossAttack_01, 2.5f);
			goto IL_265;
		case 105:
			yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("YOD_038"), null, DRGA_Good_Fight_09.VO_DRGA_BOSS_15h_Male_Orc_Good_Fight_09_Kragg_EnterBattle_01, 2.5f);
			goto IL_265;
		case 106:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_LowHealth_01_01, 2.5f);
				goto IL_265;
			}
			goto IL_265;
		case 107:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_LowHealth_02_01, 2.5f);
				yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_EnemyLowHealth_01, 2.5f);
				goto IL_265;
			}
			goto IL_265;
		case 108:
			if (!this.m_Heroic)
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPowerLines);
				goto IL_265;
			}
			goto IL_265;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_265:
		yield break;
	}

	// Token: 0x0600430B RID: 17163 RVA: 0x0016A547 File Offset: 0x00168747
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
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 890989798U)
		{
			if (num <= 857434560U)
			{
				if (num != 369446808U)
				{
					if (num != 857434560U)
					{
						goto IL_3D2;
					}
					if (!(cardId == "DRGA_BOSS_30t7"))
					{
						goto IL_3D2;
					}
					if (!this.m_Heroic)
					{
						yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannonLines);
						goto IL_3D2;
					}
					goto IL_3D2;
				}
				else if (!(cardId == "AT_070"))
				{
					goto IL_3D2;
				}
			}
			else if (num != 874212179U)
			{
				if (num != 890989798U)
				{
					goto IL_3D2;
				}
				if (!(cardId == "DRGA_BOSS_30t5"))
				{
					goto IL_3D2;
				}
				if (!this.m_Heroic)
				{
					yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_UpgradedCannon_01, 2.5f);
					goto IL_3D2;
				}
				goto IL_3D2;
			}
			else
			{
				if (!(cardId == "DRGA_BOSS_30t6"))
				{
					goto IL_3D2;
				}
				if (!this.m_Heroic)
				{
					yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_GreaseMonkey_01, 2.5f);
					goto IL_3D2;
				}
				goto IL_3D2;
			}
		}
		else if (num <= 924545036U)
		{
			if (num != 908518771U)
			{
				if (num != 924545036U)
				{
					goto IL_3D2;
				}
				if (!(cardId == "DRGA_BOSS_30t3"))
				{
					goto IL_3D2;
				}
				if (!this.m_Heroic)
				{
					yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_MissilePod_01, 2.5f);
					goto IL_3D2;
				}
				goto IL_3D2;
			}
			else if (!(cardId == "YOD_038"))
			{
				goto IL_3D2;
			}
		}
		else
		{
			if (num != 941322655U)
			{
				if (num != 1789921143U)
				{
					if (num == 2746178438U && !(cardId == "DRGA_BOSS_15t2"))
					{
						goto IL_3D2;
					}
					goto IL_3D2;
				}
				else if (!(cardId == "DRGA_BOSS_30t"))
				{
					goto IL_3D2;
				}
			}
			else if (!(cardId == "DRGA_BOSS_30t2"))
			{
				goto IL_3D2;
			}
			if (!this.m_Heroic)
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_PlayerPlaysCannon);
				goto IL_3D2;
			}
			goto IL_3D2;
		}
		if (!this.m_Heroic)
		{
			yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Kragg_01, 2.5f);
		}
		IL_3D2:
		yield break;
	}

	// Token: 0x0600430C RID: 17164 RVA: 0x0016A55D File Offset: 0x0016875D
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
		if (!(cardId == "DRGA_BOSS_30t4"))
		{
			if (!(cardId == "DRGA_BOSS_31t"))
			{
				if (!(cardId == "DRGA_BOSS_30t7"))
				{
					if (!(cardId == "DRGA_BOSS_30t2"))
					{
						if (!(cardId == "DRGA_BOSS_30t5"))
						{
							if (cardId == "DRGA_BOSS_30t")
							{
								yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_BossPlaysCannon);
							}
						}
						else
						{
							yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_UpgradedCannon_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_RocketLauncher_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannonLines);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_DeathRay_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_BombFlinger_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0400342C RID: 13356
	private static readonly AssetReference VO_DRGA_BOSS_15h_Male_Orc_Good_Fight_09_Kragg_EnterBattle_01 = new AssetReference("VO_DRGA_BOSS_15h_Male_Orc_Good_Fight_09_Kragg_EnterBattle_01.prefab:fffb0fc6a0990274d9dd7d7c1d4b20ab");

	// Token: 0x0400342D RID: 13357
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Attack_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Attack_01.prefab:b537043b22481b54bb4869e1fcc838df");

	// Token: 0x0400342E RID: 13358
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Concede_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Concede_01.prefab:0b5e7e72d9971ab4f9b3f5b16fc6f120");

	// Token: 0x0400342F RID: 13359
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Death_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Death_01.prefab:8b8fcd0b96015c94c881060522820b6a");

	// Token: 0x04003430 RID: 13360
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Greetings_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Greetings_01.prefab:82b4e2f8a4281a243b6f64359d6e7757");

	// Token: 0x04003431 RID: 13361
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Oops_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Oops_01.prefab:ac431971d8cf69f409d39bc1ddf7ea4a");

	// Token: 0x04003432 RID: 13362
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Thanks_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Thanks_01.prefab:1b2ddc06a2175044a82776fee162bf8a");

	// Token: 0x04003433 RID: 13363
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Threaten_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Threaten_01.prefab:d05926cebee474b449781e2dbfaafa28");

	// Token: 0x04003434 RID: 13364
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_WellPlayed_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_WellPlayed_01.prefab:45f531deb0246f24b878ff9740169ed5");

	// Token: 0x04003435 RID: 13365
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Wow_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Wow_01.prefab:189de760d67059e4fbb43f98c812f9c1");

	// Token: 0x04003436 RID: 13366
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_EnemyLowHealth_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_EnemyLowHealth_01.prefab:0ca0a87affc483f4393b8a6f6b7f9a1d");

	// Token: 0x04003437 RID: 13367
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Error_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Error_01.prefab:65320b93100d105448a88f7f1f29b65f");

	// Token: 0x04003438 RID: 13368
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_01_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_01_01.prefab:373a5792fae830747b1f713ff85e2ed0");

	// Token: 0x04003439 RID: 13369
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_02_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_02_01.prefab:d97d4dadbf61ea44d965351ae0d682bc");

	// Token: 0x0400343A RID: 13370
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_03_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_03_01.prefab:27075f1bd1ccc81478e2435a527d30b6");

	// Token: 0x0400343B RID: 13371
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_GreaseMonkey_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_GreaseMonkey_01.prefab:b25c61d94155d9a43ab65b15e8f3aee4");

	// Token: 0x0400343C RID: 13372
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_01_01.prefab:23f2fb8953fe11e48a76432fc517c83a");

	// Token: 0x0400343D RID: 13373
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_02_01.prefab:062631af973847c46b0c110530af2373");

	// Token: 0x0400343E RID: 13374
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_03_01.prefab:773cc7e2beadfad4f9aa5ae0c2c5d212");

	// Token: 0x0400343F RID: 13375
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Kragg_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Kragg_01.prefab:c8aaec728e8c8ec44b08272061a5ec46");

	// Token: 0x04003440 RID: 13376
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Low_Cards_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Low_Cards_01.prefab:6889044179f5cae448ba12a43302b5ba");

	// Token: 0x04003441 RID: 13377
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_LowHealth_01_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_LowHealth_01_01.prefab:723ac94ba447ea840b17cf1def17e46d");

	// Token: 0x04003442 RID: 13378
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_LowHealth_02_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_LowHealth_02_01.prefab:1c6ddf6b9176f9e49a77dadbd9018651");

	// Token: 0x04003443 RID: 13379
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_MissilePod_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_MissilePod_01.prefab:81d40d931d7c96e44a41f79f16397de9");

	// Token: 0x04003444 RID: 13380
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Out_Of_Cards_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Out_Of_Cards_01.prefab:ee8a53887728b794fb589c2b317ecca5");

	// Token: 0x04003445 RID: 13381
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_01_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_01_01.prefab:4d4ca3da54d83b1439a914c13234a944");

	// Token: 0x04003446 RID: 13382
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_02_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_02_01.prefab:f100a8f2cd83edd4c956b277774150c9");

	// Token: 0x04003447 RID: 13383
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_03_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_03_01.prefab:cb0d4530ac229e347b55868b28b51191");

	// Token: 0x04003448 RID: 13384
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Thinking_01_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Thinking_01_01.prefab:c10978340f3c10e4cb1e4dbfefab42e0");

	// Token: 0x04003449 RID: 13385
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Thinking_02_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Thinking_02_01.prefab:2c831508d53e2084a939bd03978f4c33");

	// Token: 0x0400344A RID: 13386
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Thinking_03_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Thinking_03_01.prefab:8de6b414ab27129449f83f7b22dd3cf3");

	// Token: 0x0400344B RID: 13387
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Time_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Time_01.prefab:85f5f6b7eff5b574fa0f6c6691687685");

	// Token: 0x0400344C RID: 13388
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_UpgradedCannon_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_UpgradedCannon_01.prefab:01b82109fdd401f4fb9b6627dc1ba2bb");

	// Token: 0x0400344D RID: 13389
	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_PlayerStart_01.prefab:2db1f376091d2594c84a99002b627d09");

	// Token: 0x0400344E RID: 13390
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_BombFlinger_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_BombFlinger_01.prefab:d39d02036316ed94a853ff58e82148fb");

	// Token: 0x0400344F RID: 13391
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_Death_01.prefab:8dc20268b81bb7040b046f92ef541261");

	// Token: 0x04003450 RID: 13392
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_DeathRay_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_DeathRay_01.prefab:15203c1471c631047afe96a822d41cb7");

	// Token: 0x04003451 RID: 13393
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_EnemyLowHealth_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_EnemyLowHealth_01.prefab:ed335347636b8a2458caa731e7db0d86");

	// Token: 0x04003452 RID: 13394
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_01_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_01_01.prefab:40b7977c46ca8dd48b3a2f126777a68f");

	// Token: 0x04003453 RID: 13395
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_02_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_02_01.prefab:c8ab749bed9d80241b974b7ed04cab3e");

	// Token: 0x04003454 RID: 13396
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_03_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_03_01.prefab:caaff3a1747dd0144b61c2dc05ceaee1");

	// Token: 0x04003455 RID: 13397
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_01_01.prefab:62783d55b1b3aee4b9bfe8c93ec0310a");

	// Token: 0x04003456 RID: 13398
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_02_01.prefab:b9a4c7d6ea936cd428ef6f406d8f969c");

	// Token: 0x04003457 RID: 13399
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_03_01.prefab:3b4e817a2e8e01b46b092b28a5d8b03c");

	// Token: 0x04003458 RID: 13400
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_01_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_01_01.prefab:ef9bc59c0fa2caa45b5301bcd26391d9");

	// Token: 0x04003459 RID: 13401
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_02_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_02_01.prefab:658a78813f902224684d56b83f7d83ad");

	// Token: 0x0400345A RID: 13402
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_03_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_03_01.prefab:46a491a104d9cfa4cb94268218e3281c");

	// Token: 0x0400345B RID: 13403
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_01_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_01_01.prefab:b04c10bd2c1cfa045ad7d5511c876791");

	// Token: 0x0400345C RID: 13404
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_02_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_02_01.prefab:0b328fd528e774f489c905614f84b04b");

	// Token: 0x0400345D RID: 13405
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_03_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_03_01.prefab:31bb8ed9e031fee419f587d484d0ba67");

	// Token: 0x0400345E RID: 13406
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_RocketLauncher_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_RocketLauncher_01.prefab:dcdb925deb73472489ba55aa8bd55628");

	// Token: 0x0400345F RID: 13407
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_UpgradedCannon_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_UpgradedCannon_01.prefab:2eb50fc4cefd7ef44990f71da845e0d7");

	// Token: 0x04003460 RID: 13408
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_BossAttack_01.prefab:06e5246839b69464d99eeea355d114b8");

	// Token: 0x04003461 RID: 13409
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_BossStart_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_BossStart_01.prefab:c6e85f2163a8be244aadb4a7ba2d7193");

	// Token: 0x04003462 RID: 13410
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_EmoteResponse_01.prefab:6760369615a9b7a46833491567ddcb1d");

	// Token: 0x04003463 RID: 13411
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_01_01.prefab:d6dd416cddcef4844a28b816a8e59058");

	// Token: 0x04003464 RID: 13412
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_02_01.prefab:5e218b92c5c4bc84694bf1b741096f6f");

	// Token: 0x04003465 RID: 13413
	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_03_01.prefab:725829b038db8544f97c955fd7f771ec");

	// Token: 0x04003466 RID: 13414
	private List<string> m_missionEventTrigger102Lines = new List<string>
	{
		DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_01_01,
		DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_02_01,
		DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_03_01
	};

	// Token: 0x04003467 RID: 13415
	private List<string> m_BossPlaysCannon = new List<string>
	{
		DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_01_01,
		DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_02_01,
		DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_03_01
	};

	// Token: 0x04003468 RID: 13416
	private List<string> m_PlayerPlaysCannon = new List<string>
	{
		DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_01_01,
		DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_02_01,
		DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_03_01
	};

	// Token: 0x04003469 RID: 13417
	private List<string> m_VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannonLines = new List<string>
	{
		DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_01_01,
		DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_02_01,
		DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_03_01
	};

	// Token: 0x0400346A RID: 13418
	private List<string> m_VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPowerLines = new List<string>
	{
		DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_01_01,
		DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_02_01,
		DRGA_Good_Fight_09.VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_03_01
	};

	// Token: 0x0400346B RID: 13419
	private List<string> m_VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannonLines = new List<string>
	{
		DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_01_01,
		DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_02_01,
		DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_03_01
	};

	// Token: 0x0400346C RID: 13420
	private List<string> m_VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPowerLines = new List<string>
	{
		DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_01_01,
		DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_02_01,
		DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_03_01
	};

	// Token: 0x0400346D RID: 13421
	private List<string> m_VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_IdleLines = new List<string>
	{
		DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_01_01,
		DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_02_01,
		DRGA_Good_Fight_09.VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_03_01
	};

	// Token: 0x0400346E RID: 13422
	private HashSet<string> m_playedLines = new HashSet<string>();
}
