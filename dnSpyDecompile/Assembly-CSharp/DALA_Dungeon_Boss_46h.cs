using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200045B RID: 1115
public class DALA_Dungeon_Boss_46h : DALA_Dungeon
{
	// Token: 0x06003C8A RID: 15498 RVA: 0x0013BC60 File Offset: 0x00139E60
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_BossHealingRain_01,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_BossTidalSurge_01,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_BossWaterElemental_01,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_BubblePop_01,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_BubblePop_02,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_BubblePop_03,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_Death_01,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_DefeatPlayer_01,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_EmoteResponse_01,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_HeroPower_01,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_HeroPower_02,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_HeroPower_03,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_Idle_01,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_Idle_02,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_Idle_03,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_Intro_01,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_IntroRakanishu_01,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_Misc_01,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_Misc_02,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_Misc_03,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_PlayerFireSpell_01,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_PlayerLightningSpell_01,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_PlayerMech_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003C8B RID: 15499 RVA: 0x0013BE34 File Offset: 0x0013A034
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_EmoteResponse_01;
	}

	// Token: 0x06003C8C RID: 15500 RVA: 0x0013BE6C File Offset: 0x0013A06C
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_46h.m_IdleLines;
	}

	// Token: 0x06003C8D RID: 15501 RVA: 0x0013BE73 File Offset: 0x0013A073
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_HeroPower_01,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_HeroPower_02,
			DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_HeroPower_03
		};
	}

	// Token: 0x06003C8E RID: 15502 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003C8F RID: 15503 RVA: 0x0013BEAC File Offset: 0x0013A0AC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Rakanishu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003C90 RID: 15504 RVA: 0x0013BF57 File Offset: 0x0013A157
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
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_46h.m_BubblePop);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_46h.m_HeroDamageGT5);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_PlayerMech_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003C91 RID: 15505 RVA: 0x0013BF6D File Offset: 0x0013A16D
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 1991843369U)
		{
			if (num <= 496822790U)
			{
				if (num <= 248079585U)
				{
					if (num != 24483057U)
					{
						if (num != 248079585U)
						{
							goto IL_438;
						}
						if (!(cardId == "CFM_094"))
						{
							goto IL_438;
						}
					}
					else if (!(cardId == "CFM_621t2"))
					{
						goto IL_438;
					}
				}
				else if (num != 270086284U)
				{
					if (num != 496822790U)
					{
						goto IL_438;
					}
					if (!(cardId == "CFM_662"))
					{
						goto IL_438;
					}
				}
				else if (!(cardId == "CFM_621t25"))
				{
					goto IL_438;
				}
			}
			else if (num <= 1253117486U)
			{
				if (num != 718321743U)
				{
					if (num != 1253117486U)
					{
						goto IL_438;
					}
					if (!(cardId == "LOOTA_827"))
					{
						goto IL_438;
					}
				}
				else if (!(cardId == "KAR_076"))
				{
					goto IL_438;
				}
			}
			else if (num != 1613606673U)
			{
				if (num != 1956011339U)
				{
					if (num != 1991843369U)
					{
						goto IL_438;
					}
					if (!(cardId == "EX1_238"))
					{
						goto IL_438;
					}
					goto IL_407;
				}
				else if (!(cardId == "TRL_313"))
				{
					goto IL_438;
				}
			}
			else if (!(cardId == "LOOTA_BOSS_26p6"))
			{
				goto IL_438;
			}
		}
		else if (num <= 2671992692U)
		{
			if (num <= 2047077705U)
			{
				if (num != 2043161964U)
				{
					if (num != 2047077705U)
					{
						goto IL_438;
					}
					if (!(cardId == "BOT_246"))
					{
						goto IL_438;
					}
					goto IL_407;
				}
				else
				{
					if (!(cardId == "EX1_259"))
					{
						goto IL_438;
					}
					goto IL_407;
				}
			}
			else if (num != 2177382916U)
			{
				if (num != 2264738792U)
				{
					if (num != 2671992692U)
					{
						goto IL_438;
					}
					if (!(cardId == "CFM_707"))
					{
						goto IL_438;
					}
					goto IL_407;
				}
				else
				{
					if (!(cardId == "BOTA_235"))
					{
						goto IL_438;
					}
					goto IL_407;
				}
			}
			else
			{
				if (!(cardId == "EX1_251"))
				{
					goto IL_438;
				}
				goto IL_407;
			}
		}
		else if (num <= 3159778034U)
		{
			if (num != 2702296586U)
			{
				if (num != 3159778034U)
				{
					goto IL_438;
				}
				if (!(cardId == "GIL_147"))
				{
					goto IL_438;
				}
			}
			else if (!(cardId == "CFM_621t16"))
			{
				goto IL_438;
			}
		}
		else if (num != 3945181129U)
		{
			if (num != 4066185238U)
			{
				if (num != 4196992509U)
				{
					goto IL_438;
				}
				if (!(cardId == "CS2_032"))
				{
					goto IL_438;
				}
			}
			else
			{
				if (!(cardId == "GIL_600"))
				{
					goto IL_438;
				}
				goto IL_407;
			}
		}
		else if (!(cardId == "CS2_029"))
		{
			goto IL_438;
		}
		yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_PlayerFireSpell_01, 2.5f);
		goto IL_438;
		IL_407:
		yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_PlayerLightningSpell_01, 2.5f);
		IL_438:
		yield break;
	}

	// Token: 0x06003C92 RID: 15506 RVA: 0x0013BF83 File Offset: 0x0013A183
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
		if (!(cardId == "LOOT_373"))
		{
			if (!(cardId == "UNG_817"))
			{
				if (cardId == "CS2_033")
				{
					yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_BossWaterElemental_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_BossTidalSurge_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_BossHealingRain_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002634 RID: 9780
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_BossHealingRain_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_BossHealingRain_01.prefab:7d073ff27c8508f45a833413a478be03");

	// Token: 0x04002635 RID: 9781
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_BossTidalSurge_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_BossTidalSurge_01.prefab:02c12b1253f9c6e4e89f61bae3fe0086");

	// Token: 0x04002636 RID: 9782
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_BossWaterElemental_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_BossWaterElemental_01.prefab:9d6a1f4c54be19a4f92cb74768c5514f");

	// Token: 0x04002637 RID: 9783
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_BubblePop_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_BubblePop_01.prefab:94196958f5cd37042b9d86e13f73ff0c");

	// Token: 0x04002638 RID: 9784
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_BubblePop_02 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_BubblePop_02.prefab:23ea3166544cd544cab6e50f64221455");

	// Token: 0x04002639 RID: 9785
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_BubblePop_03 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_BubblePop_03.prefab:08f6076197cb3fc41b816b708a8e327d");

	// Token: 0x0400263A RID: 9786
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_Death_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_Death_01.prefab:5f1465caf854da4469933eb35f1c9bb0");

	// Token: 0x0400263B RID: 9787
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_DefeatPlayer_01.prefab:451596d65fa5e6d4cb1ed0bce96a432a");

	// Token: 0x0400263C RID: 9788
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_EmoteResponse_01.prefab:7d364babbc8653441bc62d9d9707315c");

	// Token: 0x0400263D RID: 9789
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_HeroPower_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_HeroPower_01.prefab:0437664f676953f4d95361072710aa8d");

	// Token: 0x0400263E RID: 9790
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_HeroPower_02 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_HeroPower_02.prefab:a96246c1409d16243b57154d74133677");

	// Token: 0x0400263F RID: 9791
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_HeroPower_03 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_HeroPower_03.prefab:e4e937d03d308194d9ca90634c3f9882");

	// Token: 0x04002640 RID: 9792
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_Idle_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_Idle_01.prefab:73249301b84d43845a6edb921c17f003");

	// Token: 0x04002641 RID: 9793
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_Idle_02 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_Idle_02.prefab:f472d2ac36eb7c143869808c087bb22a");

	// Token: 0x04002642 RID: 9794
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_Idle_03 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_Idle_03.prefab:01c15d562f1753c4dbcd6720e56dc2d6");

	// Token: 0x04002643 RID: 9795
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_Intro_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_Intro_01.prefab:86e7f5cb0b765e147b8050594f6f788f");

	// Token: 0x04002644 RID: 9796
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_IntroRakanishu_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_IntroRakanishu_01.prefab:253ad7e35748ed34491af16e6c210e26");

	// Token: 0x04002645 RID: 9797
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_Misc_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_Misc_01.prefab:caafe958a1ae3b84c9496b0eeb3dd551");

	// Token: 0x04002646 RID: 9798
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_Misc_02 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_Misc_02.prefab:4c8e798d5133ca24091c7f8399ffe768");

	// Token: 0x04002647 RID: 9799
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_Misc_03 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_Misc_03.prefab:865c50de1960ffb46b18fb4f95cbf4b5");

	// Token: 0x04002648 RID: 9800
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_PlayerFireSpell_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_PlayerFireSpell_01.prefab:a20733a76b2e0874d86015e9f5050b4d");

	// Token: 0x04002649 RID: 9801
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_PlayerLightningSpell_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_PlayerLightningSpell_01.prefab:3526a390162c0a44b96f0605870d3e6c");

	// Token: 0x0400264A RID: 9802
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_PlayerMech_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_PlayerMech_01.prefab:afbd797449420d94e997a96537c93df2");

	// Token: 0x0400264B RID: 9803
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_Idle_01,
		DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_Idle_02,
		DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_Idle_03
	};

	// Token: 0x0400264C RID: 9804
	private static List<string> m_BubblePop = new List<string>
	{
		DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_BubblePop_01,
		DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_BubblePop_02,
		DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_BubblePop_03
	};

	// Token: 0x0400264D RID: 9805
	private static List<string> m_HeroDamageGT5 = new List<string>
	{
		DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_Misc_01,
		DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_Misc_02,
		DALA_Dungeon_Boss_46h.VO_DALA_BOSS_46h_Female_Revenant_Misc_03
	};

	// Token: 0x0400264E RID: 9806
	private HashSet<string> m_playedLines = new HashSet<string>();
}
