using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000503 RID: 1283
public class BTA_Fight_25 : BTA_Dungeon_Heroic
{
	// Token: 0x0600450E RID: 17678 RVA: 0x00175F44 File Offset: 0x00174144
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_Alar_01,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_Attack_01,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_Flamestrike_01,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_GravityLapse_01,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_GravityLapse_02,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_VaporizeTrigger_01,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossDeath_01,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossStart_01,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossStartArthas_01,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossStartIllidan_01,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossStartJaina_01,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossStartLiadrin_01,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Emote_Response_01,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Hero_CounterSpell_01,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Hero_FrostSpell_01,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Hero_LichKing_01,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Hero_Treant_01,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_HeroPower_01,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_HeroPower_02,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_HeroPower_03,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_HeroPower_04,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_IdleA_01,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_IdleB_01,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_IdleC_01,
			BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_UI_Mission_Fight_25_CoinSelect_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600450F RID: 17679 RVA: 0x00176138 File Offset: 0x00174338
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_25h_IdleLines;
	}

	// Token: 0x06004510 RID: 17680 RVA: 0x00176140 File Offset: 0x00174340
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossDeath_01;
		this.m_standardEmoteResponseLine = BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Emote_Response_01;
	}

	// Token: 0x06004511 RID: 17681 RVA: 0x00176168 File Offset: 0x00174368
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "HERO_04a")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossStartLiadrin_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "HERO_04b")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossStartArthas_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "HERO_10" || cardId == "HERO_10a")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossStartIllidan_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "HERO_08" || cardId == "HERO_08c")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossStartJaina_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		else
		{
			if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
	}

	// Token: 0x06004512 RID: 17682 RVA: 0x0017632C File Offset: 0x0017452C
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 115)
		{
			if (missionEvent == 110)
			{
				yield return base.PlayLineOnlyOnce(actor, BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_VaporizeTrigger_01, 2.5f);
				goto IL_182;
			}
			if (missionEvent == 115)
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Hero_CounterSpell_01, 2.5f);
				goto IL_182;
			}
		}
		else
		{
			if (missionEvent == 120)
			{
				yield return base.PlayLineOnlyOnce(actor, BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Hero_Treant_01, 2.5f);
				goto IL_182;
			}
			if (missionEvent == 500)
			{
				base.PlaySound(BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_Attack_01, 1f, true, false);
				goto IL_182;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_182:
		yield break;
	}

	// Token: 0x06004513 RID: 17683 RVA: 0x00176342 File Offset: 0x00174542
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
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 3589412489U)
		{
			if (num <= 1613717541U)
			{
				if (num != 1104301928U)
				{
					if (num != 1613717541U)
					{
						goto IL_2C4;
					}
					if (!(cardId == "DRG_248"))
					{
						goto IL_2C4;
					}
				}
				else if (!(cardId == "DAL_577"))
				{
					goto IL_2C4;
				}
			}
			else if (num != 1780024521U)
			{
				if (num != 3589412489U)
				{
					goto IL_2C4;
				}
				if (!(cardId == "BT_072"))
				{
					goto IL_2C4;
				}
			}
			else
			{
				if (!(cardId == "ICC_314"))
				{
					goto IL_2C4;
				}
				yield return base.PlayLineOnlyOnce(actor, BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Hero_LichKing_01, 2.5f);
				goto IL_2C4;
			}
		}
		else if (num <= 4056078886U)
		{
			if (num != 3928403510U)
			{
				if (num != 4056078886U)
				{
					goto IL_2C4;
				}
				if (!(cardId == "EX1_275"))
				{
					goto IL_2C4;
				}
			}
			else if (!(cardId == "CS2_028"))
			{
				goto IL_2C4;
			}
		}
		else if (num != 4096179700U)
		{
			if (num != 4113104414U)
			{
				if (num != 4129734938U)
				{
					goto IL_2C4;
				}
				if (!(cardId == "CS2_024"))
				{
					goto IL_2C4;
				}
			}
			else if (!(cardId == "CS2_037"))
			{
				goto IL_2C4;
			}
		}
		else if (!(cardId == "CS2_026"))
		{
			goto IL_2C4;
		}
		yield return base.PlayLineOnlyOnce(actor, BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Hero_FrostSpell_01, 2.5f);
		IL_2C4:
		yield break;
	}

	// Token: 0x06004514 RID: 17684 RVA: 0x00176358 File Offset: 0x00174558
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
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "BTA_BOSS_25s"))
		{
			if (!(cardId == "BT_735"))
			{
				if (!(cardId == "CS2_032"))
				{
					if (cardId == "EX1_279")
					{
						yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_HeroPowerLines);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(actor, BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_Flamestrike_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_Alar_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_GravityLapseLines);
		}
		yield break;
	}

	// Token: 0x06004515 RID: 17685 RVA: 0x0017636E File Offset: 0x0017456E
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x040037F8 RID: 14328
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_Alar_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_Alar_01.prefab:4aef60dfaf30b284da28ca670afa55b0");

	// Token: 0x040037F9 RID: 14329
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_Attack_01.prefab:787a7dfe8c47dfe44a12934f2e8f0a40");

	// Token: 0x040037FA RID: 14330
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_Flamestrike_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_Flamestrike_01.prefab:2939a0f0b0f981b4e9c38402a754d70d");

	// Token: 0x040037FB RID: 14331
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_GravityLapse_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_GravityLapse_01.prefab:a379c6013128a8e4e85deff9774a316b");

	// Token: 0x040037FC RID: 14332
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_GravityLapse_02 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_GravityLapse_02.prefab:df8037736618d744b8de92b99fc23f21");

	// Token: 0x040037FD RID: 14333
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_VaporizeTrigger_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_VaporizeTrigger_01.prefab:83dbf0df0f3cc984aa2a347e9bcdeca0");

	// Token: 0x040037FE RID: 14334
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossDeath_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossDeath_01.prefab:5ec0485117847b14f8a6c27615ea7dae");

	// Token: 0x040037FF RID: 14335
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossStart_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossStart_01.prefab:8c365e844e8fbd942ae834384c6a9306");

	// Token: 0x04003800 RID: 14336
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossStartArthas_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossStartArthas_01.prefab:428703020b5b38944b94a3c33734c3c3");

	// Token: 0x04003801 RID: 14337
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossStartIllidan_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossStartIllidan_01.prefab:d03f1948124a1b64d9bf23c3398d8669");

	// Token: 0x04003802 RID: 14338
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossStartJaina_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossStartJaina_01.prefab:2c1802c42b8c882449a15adf4c402698");

	// Token: 0x04003803 RID: 14339
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossStartLiadrin_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_BossStartLiadrin_01.prefab:4a0147b3cc19e5947a7e6667fe8e682b");

	// Token: 0x04003804 RID: 14340
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Emote_Response_01.prefab:a9da02ca8273f2f4083bd7278ce10fe2");

	// Token: 0x04003805 RID: 14341
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Hero_CounterSpell_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Hero_CounterSpell_01.prefab:f0ef1375170df6944b07429733b3c37e");

	// Token: 0x04003806 RID: 14342
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Hero_FrostSpell_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Hero_FrostSpell_01.prefab:25750ef66cb48ca46a213e8e2665a0d8");

	// Token: 0x04003807 RID: 14343
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Hero_LichKing_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Hero_LichKing_01.prefab:877527192df02f64a934c620c2f52c87");

	// Token: 0x04003808 RID: 14344
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Hero_Treant_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Hero_Treant_01.prefab:2620c6ede6afc634d8767180bd30374a");

	// Token: 0x04003809 RID: 14345
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_HeroPower_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_HeroPower_01.prefab:b5f544133cfa14e46ab09f2dd879e0ae");

	// Token: 0x0400380A RID: 14346
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_HeroPower_02 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_HeroPower_02.prefab:9c325f9ef72a5394bbc0b3be1ce70204");

	// Token: 0x0400380B RID: 14347
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_HeroPower_03 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_HeroPower_03.prefab:bd8f21c60ae861f43b743e190cf66347");

	// Token: 0x0400380C RID: 14348
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_HeroPower_04 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_HeroPower_04.prefab:b4237b7c38f022647938e331d248d6aa");

	// Token: 0x0400380D RID: 14349
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_IdleA_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_IdleA_01.prefab:585b909af2b19cc4db4e57a491c7e90a");

	// Token: 0x0400380E RID: 14350
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_IdleB_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_IdleB_01.prefab:f5641b662f8b72747ad2ec9dfb1d7d6a");

	// Token: 0x0400380F RID: 14351
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_IdleC_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_IdleC_01.prefab:b7495f55053f07547b51f30dc4d37e87");

	// Token: 0x04003810 RID: 14352
	private static readonly AssetReference VO_BTA_BOSS_25h_Male_BloodElf_UI_Mission_Fight_25_CoinSelect_01 = new AssetReference("VO_BTA_BOSS_25h_Male_BloodElf_UI_Mission_Fight_25_CoinSelect_01.prefab:9c22bb165b089ac40a2c4e750f597cb6");

	// Token: 0x04003811 RID: 14353
	private List<string> m_VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_GravityLapseLines = new List<string>
	{
		BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_GravityLapse_01,
		BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_Boss_GravityLapse_02
	};

	// Token: 0x04003812 RID: 14354
	private List<string> m_VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_HeroPowerLines = new List<string>
	{
		BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_HeroPower_01,
		BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_HeroPower_02,
		BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_HeroPower_03,
		BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_HeroPower_04
	};

	// Token: 0x04003813 RID: 14355
	private List<string> m_VO_BTA_BOSS_25h_IdleLines = new List<string>
	{
		BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_IdleA_01,
		BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_IdleB_01,
		BTA_Fight_25.VO_BTA_BOSS_25h_Male_BloodElf_Mission_Fight_25_IdleC_01
	};

	// Token: 0x04003814 RID: 14356
	private HashSet<string> m_playedLines = new HashSet<string>();
}
