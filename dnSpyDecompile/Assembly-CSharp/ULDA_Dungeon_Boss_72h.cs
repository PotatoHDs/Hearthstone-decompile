using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004C4 RID: 1220
public class ULDA_Dungeon_Boss_72h : ULDA_Dungeon
{
	// Token: 0x06004160 RID: 16736 RVA: 0x0015DA10 File Offset: 0x0015BC10
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_BossTriggerDeathwing_01,
			ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_BossTriggerPlagueofDeath_01,
			ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_BossTriggerShadowWordDeath_01,
			ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_DeathALT_01,
			ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_DefeatPlayer_01,
			ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_EmoteResponse_01,
			ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_HeroPower_01,
			ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_HeroPower_02,
			ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_HeroPower_03,
			ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_HeroPower_04,
			ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_HeroPower_05,
			ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_Idle_01,
			ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_Idle_02,
			ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_Intro_01,
			ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_IntroSpecialFinley_01,
			ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_PlayerTrigger_Bone_Wraith_01,
			ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_PlayerTrigger_Murmy_01,
			ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_PlayerTrigger_Octosari_01,
			ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_PlayerTriggerDOOM_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004161 RID: 16737 RVA: 0x0015DBA4 File Offset: 0x0015BDA4
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06004162 RID: 16738 RVA: 0x0015DBAC File Offset: 0x0015BDAC
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06004163 RID: 16739 RVA: 0x0015DBB4 File Offset: 0x0015BDB4
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_EmoteResponse_01;
	}

	// Token: 0x06004164 RID: 16740 RVA: 0x0015DBEC File Offset: 0x0015BDEC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Finley")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_IntroSpecialFinley_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06004165 RID: 16741 RVA: 0x0015DCC6 File Offset: 0x0015BEC6
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06004166 RID: 16742 RVA: 0x0015DCDC File Offset: 0x0015BEDC
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
		if (!(cardId == "ULD_723"))
		{
			if (!(cardId == "ULD_177"))
			{
				if (cardId == "ULD_275")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_PlayerTrigger_Bone_Wraith_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_PlayerTrigger_Octosari_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_PlayerTrigger_Murmy_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004167 RID: 16743 RVA: 0x0015DCF2 File Offset: 0x0015BEF2
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
		if (!(cardId == "NEW1_030") && !(cardId == "OG_317"))
		{
			if (!(cardId == "ULD_718"))
			{
				if (!(cardId == "EX1_622"))
				{
					if (cardId == "OG_239")
					{
						yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_PlayerTriggerDOOM_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_BossTriggerShadowWordDeath_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_BossTriggerPlagueofDeath_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_BossTriggerDeathwing_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0400303B RID: 12347
	private static readonly AssetReference VO_ULDA_BOSS_72h_Male_TitanConstruct_BossTriggerDeathwing_01 = new AssetReference("VO_ULDA_BOSS_72h_Male_TitanConstruct_BossTriggerDeathwing_01.prefab:19b429d812aebc4488976a81b00a4112");

	// Token: 0x0400303C RID: 12348
	private static readonly AssetReference VO_ULDA_BOSS_72h_Male_TitanConstruct_BossTriggerPlagueofDeath_01 = new AssetReference("VO_ULDA_BOSS_72h_Male_TitanConstruct_BossTriggerPlagueofDeath_01.prefab:c02cbe6c127b41441bbfc2322d0bd31b");

	// Token: 0x0400303D RID: 12349
	private static readonly AssetReference VO_ULDA_BOSS_72h_Male_TitanConstruct_BossTriggerShadowWordDeath_01 = new AssetReference("VO_ULDA_BOSS_72h_Male_TitanConstruct_BossTriggerShadowWordDeath_01.prefab:312198ed68318ef43b9a1369a6637188");

	// Token: 0x0400303E RID: 12350
	private static readonly AssetReference VO_ULDA_BOSS_72h_Male_TitanConstruct_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_72h_Male_TitanConstruct_DeathALT_01.prefab:54587749ea1c49a4dab5703e6fdba274");

	// Token: 0x0400303F RID: 12351
	private static readonly AssetReference VO_ULDA_BOSS_72h_Male_TitanConstruct_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_72h_Male_TitanConstruct_DefeatPlayer_01.prefab:9a7cafac41cffc64e9fcdda124b3a9f5");

	// Token: 0x04003040 RID: 12352
	private static readonly AssetReference VO_ULDA_BOSS_72h_Male_TitanConstruct_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_72h_Male_TitanConstruct_EmoteResponse_01.prefab:6ec271111d0c9f440a810b18bd5b26bf");

	// Token: 0x04003041 RID: 12353
	private static readonly AssetReference VO_ULDA_BOSS_72h_Male_TitanConstruct_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_72h_Male_TitanConstruct_HeroPower_01.prefab:c07c5ffe5174d6d478c8b6b314efe161");

	// Token: 0x04003042 RID: 12354
	private static readonly AssetReference VO_ULDA_BOSS_72h_Male_TitanConstruct_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_72h_Male_TitanConstruct_HeroPower_02.prefab:bb5cb786f4c27f743ad58e247aa74785");

	// Token: 0x04003043 RID: 12355
	private static readonly AssetReference VO_ULDA_BOSS_72h_Male_TitanConstruct_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_72h_Male_TitanConstruct_HeroPower_03.prefab:0cc21ed1d7310544f8ba7311fb786cc9");

	// Token: 0x04003044 RID: 12356
	private static readonly AssetReference VO_ULDA_BOSS_72h_Male_TitanConstruct_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_72h_Male_TitanConstruct_HeroPower_04.prefab:507eb783c1a47734fb0c77054d793829");

	// Token: 0x04003045 RID: 12357
	private static readonly AssetReference VO_ULDA_BOSS_72h_Male_TitanConstruct_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_72h_Male_TitanConstruct_HeroPower_05.prefab:dfc9338a32aead8458423eb1af00473c");

	// Token: 0x04003046 RID: 12358
	private static readonly AssetReference VO_ULDA_BOSS_72h_Male_TitanConstruct_Idle_01 = new AssetReference("VO_ULDA_BOSS_72h_Male_TitanConstruct_Idle_01.prefab:f92b3584a49666046914da842c1b68ca");

	// Token: 0x04003047 RID: 12359
	private static readonly AssetReference VO_ULDA_BOSS_72h_Male_TitanConstruct_Idle_02 = new AssetReference("VO_ULDA_BOSS_72h_Male_TitanConstruct_Idle_02.prefab:e7fa0943f034d6d478f6c734b13ddb09");

	// Token: 0x04003048 RID: 12360
	private static readonly AssetReference VO_ULDA_BOSS_72h_Male_TitanConstruct_Intro_01 = new AssetReference("VO_ULDA_BOSS_72h_Male_TitanConstruct_Intro_01.prefab:e692437eb5f7f834881f0b1f803f802f");

	// Token: 0x04003049 RID: 12361
	private static readonly AssetReference VO_ULDA_BOSS_72h_Male_TitanConstruct_IntroSpecialFinley_01 = new AssetReference("VO_ULDA_BOSS_72h_Male_TitanConstruct_IntroSpecialFinley_01.prefab:16d3fa49cf161b548be82b30a6fd6768");

	// Token: 0x0400304A RID: 12362
	private static readonly AssetReference VO_ULDA_BOSS_72h_Male_TitanConstruct_PlayerTrigger_Bone_Wraith_01 = new AssetReference("VO_ULDA_BOSS_72h_Male_TitanConstruct_PlayerTrigger_Bone_Wraith_01.prefab:1f46442aab4a5b147854e2c2b6e3cc0f");

	// Token: 0x0400304B RID: 12363
	private static readonly AssetReference VO_ULDA_BOSS_72h_Male_TitanConstruct_PlayerTrigger_Murmy_01 = new AssetReference("VO_ULDA_BOSS_72h_Male_TitanConstruct_PlayerTrigger_Murmy_01.prefab:94b834339fcc71348b39968efae27cd5");

	// Token: 0x0400304C RID: 12364
	private static readonly AssetReference VO_ULDA_BOSS_72h_Male_TitanConstruct_PlayerTrigger_Octosari_01 = new AssetReference("VO_ULDA_BOSS_72h_Male_TitanConstruct_PlayerTrigger_Octosari_01.prefab:80bf4fa2f77eae043be25f22c9758f4d");

	// Token: 0x0400304D RID: 12365
	private static readonly AssetReference VO_ULDA_BOSS_72h_Male_TitanConstruct_PlayerTriggerDOOM_01 = new AssetReference("VO_ULDA_BOSS_72h_Male_TitanConstruct_PlayerTriggerDOOM_01.prefab:79f741af94b1e6949bfb887e91854bf0");

	// Token: 0x0400304E RID: 12366
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_HeroPower_01,
		ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_HeroPower_02,
		ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_HeroPower_03,
		ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_HeroPower_04,
		ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_HeroPower_05
	};

	// Token: 0x0400304F RID: 12367
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_Idle_01,
		ULDA_Dungeon_Boss_72h.VO_ULDA_BOSS_72h_Male_TitanConstruct_Idle_02
	};

	// Token: 0x04003050 RID: 12368
	private HashSet<string> m_playedLines = new HashSet<string>();
}
