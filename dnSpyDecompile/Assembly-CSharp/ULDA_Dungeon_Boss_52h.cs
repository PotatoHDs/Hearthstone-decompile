using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004B1 RID: 1201
public class ULDA_Dungeon_Boss_52h : ULDA_Dungeon
{
	// Token: 0x06004098 RID: 16536 RVA: 0x00158C44 File Offset: 0x00156E44
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerFreezePlayer_01,
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerFrostNova_01,
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerRayofFrost_01,
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerWaterElemental_01,
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_Death_01,
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_DefeatPlayer_01,
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_EmoteResponse_01,
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_01,
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_03,
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_05,
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_01,
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_02,
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_03,
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_Intro_01,
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_IntroResponseReno_01,
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerFreezeBoss_01,
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Arfus_01,
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Jar_Dealer_01,
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Kelthuzad_01,
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_LichKing_01,
			ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Psychopomp_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004099 RID: 16537 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600409A RID: 16538 RVA: 0x00158DF8 File Offset: 0x00156FF8
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x0600409B RID: 16539 RVA: 0x00158E00 File Offset: 0x00157000
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x0600409C RID: 16540 RVA: 0x00158E08 File Offset: 0x00157008
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_EmoteResponse_01;
	}

	// Token: 0x0600409D RID: 16541 RVA: 0x00158E40 File Offset: 0x00157040
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START && cardId != "ULDA_Reno")
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600409E RID: 16542 RVA: 0x00158EEB File Offset: 0x001570EB
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 101)
		{
			if (missionEvent == 102)
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerFreezePlayer_01, 2.5f);
			}
			else
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerFreezeBoss_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600409F RID: 16543 RVA: 0x00158F01 File Offset: 0x00157101
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
		if (!(cardId == "ICC_854"))
		{
			if (!(cardId == "ULD_282"))
			{
				if (!(cardId == "FP1_013"))
				{
					if (!(cardId == "ICC_314"))
					{
						if (cardId == "ULD_268")
						{
							yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Psychopomp_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_LichKing_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Kelthuzad_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Jar_Dealer_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Arfus_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060040A0 RID: 16544 RVA: 0x00158F17 File Offset: 0x00157117
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
		if (!(cardId == "CS2_026"))
		{
			if (!(cardId == "DAL_577"))
			{
				if (cardId == "CS2_033")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerWaterElemental_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerRayofFrost_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerFrostNova_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002EBB RID: 11963
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerFreezePlayer_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerFreezePlayer_01.prefab:159103d42df8b3b489c0e40cebbca7fc");

	// Token: 0x04002EBC RID: 11964
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerFrostNova_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerFrostNova_01.prefab:fbc9363a6cf9a3c46b15dd45d3874fa5");

	// Token: 0x04002EBD RID: 11965
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerRayofFrost_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerRayofFrost_01.prefab:eb728c7f61b17ad44a66b145fbf4fc1b");

	// Token: 0x04002EBE RID: 11966
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerWaterElemental_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_BossTriggerWaterElemental_01.prefab:1bd3a2230ec022841970069845dab07e");

	// Token: 0x04002EBF RID: 11967
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_Death_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_Death_01.prefab:de1fa7d300be606439a9668b59a167a8");

	// Token: 0x04002EC0 RID: 11968
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_DefeatPlayer_01.prefab:43e2f24e3185162459d0d113059f263b");

	// Token: 0x04002EC1 RID: 11969
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_EmoteResponse_01.prefab:588679b16be5a654b8ea21ffb5d8b360");

	// Token: 0x04002EC2 RID: 11970
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_01.prefab:e79b5719260d06a46a0a3c0e2552304c");

	// Token: 0x04002EC3 RID: 11971
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_03.prefab:c7d1cc7d5df059f46813b1c0c1f1b28d");

	// Token: 0x04002EC4 RID: 11972
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_05.prefab:8e1ae81702419cc498e8225e65bea36d");

	// Token: 0x04002EC5 RID: 11973
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_01.prefab:9a22082301cb4a04abeaa9f5b902202e");

	// Token: 0x04002EC6 RID: 11974
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_02 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_02.prefab:4486e499b77779e459d882ec683d7908");

	// Token: 0x04002EC7 RID: 11975
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_03 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_03.prefab:af72c89b9d5ae21429ec804375d0cefc");

	// Token: 0x04002EC8 RID: 11976
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_Intro_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_Intro_01.prefab:04ec01ffc764867469bc10aa1e86035e");

	// Token: 0x04002EC9 RID: 11977
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_IntroResponseReno_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_IntroResponseReno_01.prefab:a5da3c562d4e1e04b85298332f33834e");

	// Token: 0x04002ECA RID: 11978
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerFreezeBoss_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerFreezeBoss_01.prefab:be198bfd9ec5b38438b0d22f642441d7");

	// Token: 0x04002ECB RID: 11979
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Arfus_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Arfus_01.prefab:bb36b791d9954b8479ca6482a2273c07");

	// Token: 0x04002ECC RID: 11980
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Jar_Dealer_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Jar_Dealer_01.prefab:5690869bd409deb4cb006b55ef7dd03b");

	// Token: 0x04002ECD RID: 11981
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Kelthuzad_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Kelthuzad_01.prefab:c6515a9d41705494891a103ab2a27bed");

	// Token: 0x04002ECE RID: 11982
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_LichKing_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_LichKing_01.prefab:34191563ddc4a664982a1e96e4357c44");

	// Token: 0x04002ECF RID: 11983
	private static readonly AssetReference VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Psychopomp_01 = new AssetReference("VO_ULDA_BOSS_52h_Female_UndeadLich_PlayerTrigger_Psychopomp_01.prefab:2a6782f37af2f3444a2b81f9e43e697c");

	// Token: 0x04002ED0 RID: 11984
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_01,
		ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_03,
		ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_HeroPower_05
	};

	// Token: 0x04002ED1 RID: 11985
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_01,
		ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_02,
		ULDA_Dungeon_Boss_52h.VO_ULDA_BOSS_52h_Female_UndeadLich_Idle_03
	};

	// Token: 0x04002ED2 RID: 11986
	private HashSet<string> m_playedLines = new HashSet<string>();
}
