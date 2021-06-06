using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000500 RID: 1280
public class BTA_Fight_22 : BTA_Dungeon_Heroic
{
	// Token: 0x060044E5 RID: 17637 RVA: 0x00175094 File Offset: 0x00173294
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_Attack_01,
			BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_DeathandDecay_01,
			BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_DeathGrip_01,
			BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_SiphonSoul_01,
			BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossDeathAlt_01,
			BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossStart_01,
			BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossStartIllidan_01,
			BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Emote_Response_01,
			BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Hero_DeathKnightHero_01,
			BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Hero_LichKing_01,
			BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_01,
			BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_02,
			BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_03,
			BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_04,
			BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleA_01,
			BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleB_01,
			BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleC_01,
			BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_UI_Mission_Fight_22_CoinSelect_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060044E6 RID: 17638 RVA: 0x00175218 File Offset: 0x00173418
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_22h_IdleLines;
	}

	// Token: 0x060044E7 RID: 17639 RVA: 0x00175220 File Offset: 0x00173420
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossDeathAlt_01;
		this.m_standardEmoteResponseLine = BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Emote_Response_01;
	}

	// Token: 0x060044E8 RID: 17640 RVA: 0x00175248 File Offset: 0x00173448
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "HERO_10" || cardId == "HERO_10a")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossStartIllidan_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x060044E9 RID: 17641 RVA: 0x00175348 File Offset: 0x00173548
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 500)
		{
			if (missionEvent != 507)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_missionEventTrigger507Lines);
			}
		}
		else
		{
			base.PlaySound(BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_Attack_01, 1f, true, false);
		}
		yield break;
	}

	// Token: 0x060044EA RID: 17642 RVA: 0x0017535E File Offset: 0x0017355E
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
		if (num <= 565382510U)
		{
			if (num <= 531827272U)
			{
				if (num != 515196748U)
				{
					if (num != 531827272U)
					{
						goto IL_2F4;
					}
					if (!(cardId == "ICC_832"))
					{
						goto IL_2F4;
					}
				}
				else if (!(cardId == "ICC_829"))
				{
					goto IL_2F4;
				}
			}
			else if (num != 531974367U)
			{
				if (num != 548604891U)
				{
					if (num != 565382510U)
					{
						goto IL_2F4;
					}
					if (!(cardId == "ICC_830"))
					{
						goto IL_2F4;
					}
				}
				else if (!(cardId == "ICC_833"))
				{
					goto IL_2F4;
				}
			}
			else if (!(cardId == "ICC_828"))
			{
				goto IL_2F4;
			}
		}
		else if (num <= 615862462U)
		{
			if (num != 582160129U)
			{
				if (num != 615862462U)
				{
					goto IL_2F4;
				}
				if (!(cardId == "ICC_827"))
				{
					goto IL_2F4;
				}
			}
			else if (!(cardId == "ICC_831"))
			{
				goto IL_2F4;
			}
		}
		else if (num != 632492986U)
		{
			if (num != 1780024521U)
			{
				if (num != 1999498950U)
				{
					goto IL_2F4;
				}
				if (!(cardId == "ICC_481"))
				{
					goto IL_2F4;
				}
			}
			else
			{
				if (!(cardId == "ICC_314"))
				{
					goto IL_2F4;
				}
				yield return base.PlayLineOnlyOnce(actor, BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Hero_LichKing_01, 2.5f);
				goto IL_2F4;
			}
		}
		else if (!(cardId == "ICC_834"))
		{
			goto IL_2F4;
		}
		yield return base.PlayLineOnlyOnce(actor, BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Hero_DeathKnightHero_01, 2.5f);
		IL_2F4:
		yield break;
	}

	// Token: 0x060044EB RID: 17643 RVA: 0x00175374 File Offset: 0x00173574
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
		if (!(cardId == "ICC_314t4"))
		{
			if (!(cardId == "ICC_314t5"))
			{
				if (cardId == "ICC_314t8")
				{
					yield return base.PlayLineOnlyOnce(actor, BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_DeathandDecay_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_SiphonSoul_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_DeathGrip_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060044EC RID: 17644 RVA: 0x0017538A File Offset: 0x0017358A
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

	// Token: 0x040037B8 RID: 14264
	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_Attack_01.prefab:5e2ee0089e6d2da49b73f3207c236af9");

	// Token: 0x040037B9 RID: 14265
	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_DeathandDecay_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_DeathandDecay_01.prefab:ff2f65b86ac9ba7409a577c212efd9c5");

	// Token: 0x040037BA RID: 14266
	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_DeathGrip_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_DeathGrip_01.prefab:fc70bc1102656c74b8003626be6cf1bb");

	// Token: 0x040037BB RID: 14267
	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_SiphonSoul_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Boss_SiphonSoul_01.prefab:eae6b8b3e2aab6041a96172296d697c0");

	// Token: 0x040037BC RID: 14268
	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossDeathAlt_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossDeathAlt_01.prefab:f0d128f0675fe1b47b6460e96a4d682e");

	// Token: 0x040037BD RID: 14269
	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossStart_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossStart_01.prefab:ce8940cb3601ab848a4e57ee44c23185");

	// Token: 0x040037BE RID: 14270
	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossStartIllidan_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_BossStartIllidan_01.prefab:6d92097ccf49b8841b802599db8cf03a");

	// Token: 0x040037BF RID: 14271
	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Emote_Response_01.prefab:24f7e4daae17ce349ac74edf6857220d");

	// Token: 0x040037C0 RID: 14272
	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Hero_DeathKnightHero_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Hero_DeathKnightHero_01.prefab:6a5f4f9cbae1f084089954a22ebbbaec");

	// Token: 0x040037C1 RID: 14273
	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Hero_LichKing_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_Hero_LichKing_01.prefab:8977f253905fd80479ddca63cd047ff4");

	// Token: 0x040037C2 RID: 14274
	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_01.prefab:a9395cd9495bf9444ba1875c33ba1d9f");

	// Token: 0x040037C3 RID: 14275
	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_02 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_02.prefab:0d8d0d883a997ca4bba2b3eb6e7672c0");

	// Token: 0x040037C4 RID: 14276
	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_03 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_03.prefab:531f90851e82b15449e6b3a94fdcdcb2");

	// Token: 0x040037C5 RID: 14277
	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_04 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_04.prefab:3931c237faf965f458570832c5a93c14");

	// Token: 0x040037C6 RID: 14278
	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleA_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleA_01.prefab:69a93c8681b5a0044ae3cdfeb65322c3");

	// Token: 0x040037C7 RID: 14279
	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleB_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleB_01.prefab:b7834b0c5235adf4f9b97e4259dc400c");

	// Token: 0x040037C8 RID: 14280
	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleC_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleC_01.prefab:0f46df05c85bf7a43a346b3e85a5fd50");

	// Token: 0x040037C9 RID: 14281
	private static readonly AssetReference VO_BTA_BOSS_22h_Male_Human_UI_Mission_Fight_22_CoinSelect_01 = new AssetReference("VO_BTA_BOSS_22h_Male_Human_UI_Mission_Fight_22_CoinSelect_01.prefab:d67906a7a549b754e99c79a963f502e1");

	// Token: 0x040037CA RID: 14282
	private List<string> m_missionEventTrigger507Lines = new List<string>
	{
		BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_01,
		BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_02,
		BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_03,
		BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_HeroPower_04
	};

	// Token: 0x040037CB RID: 14283
	private List<string> m_VO_BTA_BOSS_22h_IdleLines = new List<string>
	{
		BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleA_01,
		BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleB_01,
		BTA_Fight_22.VO_BTA_BOSS_22h_Male_Human_Mission_Fight_22_IdleC_01
	};

	// Token: 0x040037CC RID: 14284
	private HashSet<string> m_playedLines = new HashSet<string>();
}
