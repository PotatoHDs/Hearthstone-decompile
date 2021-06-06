using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200046B RID: 1131
public class DALA_Dungeon_Boss_62h : DALA_Dungeon
{
	// Token: 0x06003D4E RID: 15694 RVA: 0x00141874 File Offset: 0x0013FA74
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_BossHorror_01,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_BossNoAttack_01,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_BossStealMinion_01,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_BossStealMinion_02,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_Death_01,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_DefeatPlayer_01,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_EmoteResponse_01,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_HeroPower_01,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_HeroPower_02,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_HeroPower_03,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_HeroPower_04,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_HeroPower_05,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_Idle_01,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_Idle_02,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_Idle_03,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_Intro_01,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_IntroGeorge_01,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_IntroTekahn_01,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_PlayerEmbracetheShadow_01,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_PlayerLichKing_01,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_PlayerParagonofLight_01,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_PlayerRenounceDarkness_01,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_PlayerShadowform_01,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_PlayerSunwalker_01,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_PlayerTheDarkness_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003D4F RID: 15695 RVA: 0x00141A68 File Offset: 0x0013FC68
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_EmoteResponse_01;
	}

	// Token: 0x06003D50 RID: 15696 RVA: 0x00141AA0 File Offset: 0x0013FCA0
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_HeroPower_01,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_HeroPower_02,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_HeroPower_03,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_HeroPower_04,
			DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_HeroPower_05
		};
	}

	// Token: 0x06003D51 RID: 15697 RVA: 0x00141B02 File Offset: 0x0013FD02
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_62h.m_IdleLines;
	}

	// Token: 0x06003D52 RID: 15698 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003D53 RID: 15699 RVA: 0x00141B0C File Offset: 0x0013FD0C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_Tekahn")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
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

	// Token: 0x06003D54 RID: 15700 RVA: 0x00141BF3 File Offset: 0x0013FDF3
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_BossNoAttack_01, 2.5f);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06003D55 RID: 15701 RVA: 0x00141C09 File Offset: 0x0013FE09
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
		if (num <= 494862995U)
		{
			if (num != 124827869U)
			{
				if (num != 462703413U)
				{
					if (num == 494862995U)
					{
						if (cardId == "OG_104")
						{
							yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_PlayerEmbracetheShadow_01, 2.5f);
						}
					}
				}
				else if (cardId == "EX1_032")
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_PlayerSunwalker_01, 2.5f);
				}
			}
			else if (cardId == "GIL_685")
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_PlayerParagonofLight_01, 2.5f);
			}
		}
		else if (num <= 717476619U)
		{
			if (num != 595381614U)
			{
				if (num == 717476619U)
				{
					if (cardId == "LOOT_526")
					{
						yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_PlayerTheDarkness_01, 2.5f);
					}
				}
			}
			else if (cardId == "OG_118")
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_PlayerRenounceDarkness_01, 2.5f);
			}
		}
		else if (num != 980114931U)
		{
			if (num == 1780024521U)
			{
				if (cardId == "ICC_314")
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_PlayerLichKing_01, 2.5f);
				}
			}
		}
		else if (cardId == "EX1_625")
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_PlayerShadowform_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003D56 RID: 15702 RVA: 0x00141C1F File Offset: 0x0013FE1F
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
		if (!(cardId == "GIL_124") && !(cardId == "OG_142") && !(cardId == "OG_337") && !(cardId == "TRL_408"))
		{
			if (cardId == "CFM_603" || cardId == "CS1_113")
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_62h.m_BossStealMinion);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_BossHorror_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002815 RID: 10261
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_BossHorror_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_BossHorror_01.prefab:7e0c6049a9f1a824d9ef770f28b84baa");

	// Token: 0x04002816 RID: 10262
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_BossNoAttack_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_BossNoAttack_01.prefab:d19e6e93910fe994f83a1d6a18f1dd2e");

	// Token: 0x04002817 RID: 10263
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_BossStealMinion_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_BossStealMinion_01.prefab:cc256eba57b4b5b4dbeea00ecef8cb8b");

	// Token: 0x04002818 RID: 10264
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_BossStealMinion_02 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_BossStealMinion_02.prefab:718f6d2fcfb74fd439257088f8c1a628");

	// Token: 0x04002819 RID: 10265
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_Death_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_Death_01.prefab:0342ea44ad37295468518b25c89afb28");

	// Token: 0x0400281A RID: 10266
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_DefeatPlayer_01.prefab:379907e56504d784aba61da071f80fbb");

	// Token: 0x0400281B RID: 10267
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_EmoteResponse_01.prefab:e371d0bc8542d9f4da88845135ec44f4");

	// Token: 0x0400281C RID: 10268
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_HeroPower_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_HeroPower_01.prefab:b7e7e961893de844da4b1de329cdcd11");

	// Token: 0x0400281D RID: 10269
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_HeroPower_02 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_HeroPower_02.prefab:3ff60493cc8e98d4b814a5194f9f9ef8");

	// Token: 0x0400281E RID: 10270
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_HeroPower_03 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_HeroPower_03.prefab:81b7c5e0b1eb17d4da5fafefdfc87d40");

	// Token: 0x0400281F RID: 10271
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_HeroPower_04 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_HeroPower_04.prefab:26b73cba9899d9743987b46befd8713b");

	// Token: 0x04002820 RID: 10272
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_HeroPower_05 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_HeroPower_05.prefab:b14cda8388a313f4a80e9f5456c57dc3");

	// Token: 0x04002821 RID: 10273
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_Idle_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_Idle_01.prefab:c2afeb46d5739294da9ae3f4492ce2cd");

	// Token: 0x04002822 RID: 10274
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_Idle_02 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_Idle_02.prefab:b01df7a9456a5654abdef992e359290b");

	// Token: 0x04002823 RID: 10275
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_Idle_03 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_Idle_03.prefab:83d2359ec32509d4c883ff0d50434c8a");

	// Token: 0x04002824 RID: 10276
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_Intro_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_Intro_01.prefab:e2a046ae0d82015438e85d7016cde65d");

	// Token: 0x04002825 RID: 10277
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_IntroGeorge_01.prefab:d40dfbefe3e3d2d4bae000f7cd864b9a");

	// Token: 0x04002826 RID: 10278
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_IntroTekahn_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_IntroTekahn_01.prefab:81dbb4f41b35f0641b9dabffbb5f70ef");

	// Token: 0x04002827 RID: 10279
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_PlayerEmbracetheShadow_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_PlayerEmbracetheShadow_01.prefab:2d9dafe828fc4dd418d02cf4d9db7153");

	// Token: 0x04002828 RID: 10280
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_PlayerLichKing_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_PlayerLichKing_01.prefab:825cfd89a17aa3d47aff0b7f0ed426f9");

	// Token: 0x04002829 RID: 10281
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_PlayerParagonofLight_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_PlayerParagonofLight_01.prefab:8809b2ce463f6424ca925e951521be9d");

	// Token: 0x0400282A RID: 10282
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_PlayerRenounceDarkness_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_PlayerRenounceDarkness_01.prefab:68a809f9fffa2e2429d43128d54e3da1");

	// Token: 0x0400282B RID: 10283
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_PlayerShadowform_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_PlayerShadowform_01.prefab:c8bb0bd9761ce084dbc5b3738f95270c");

	// Token: 0x0400282C RID: 10284
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_PlayerSunwalker_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_PlayerSunwalker_01.prefab:8636b6c4366dff74482da32a49834328");

	// Token: 0x0400282D RID: 10285
	private static readonly AssetReference VO_DALA_BOSS_62h_Male_Tauren_PlayerTheDarkness_01 = new AssetReference("VO_DALA_BOSS_62h_Male_Tauren_PlayerTheDarkness_01.prefab:d3465a93e9e406b4e94e88d83440f89d");

	// Token: 0x0400282E RID: 10286
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_Idle_01,
		DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_Idle_02,
		DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_Idle_03
	};

	// Token: 0x0400282F RID: 10287
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04002830 RID: 10288
	private static List<string> m_BossStealMinion = new List<string>
	{
		DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_BossStealMinion_01,
		DALA_Dungeon_Boss_62h.VO_DALA_BOSS_62h_Male_Tauren_BossStealMinion_02
	};
}
