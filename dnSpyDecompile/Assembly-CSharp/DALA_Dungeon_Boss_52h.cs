using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000461 RID: 1121
public class DALA_Dungeon_Boss_52h : DALA_Dungeon
{
	// Token: 0x06003CD4 RID: 15572 RVA: 0x0013D960 File Offset: 0x0013BB60
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_BossBuffMinion_01,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_BossVioletSpellsword_01,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_BossVioletWarden_02,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_Death_02,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_DefeatPlayer_01,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_EmoteResponse_01,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_HeroPower_01,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_HeroPower_02,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_HeroPower_03,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_HeroPower_04,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_HeroPower_06,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_Idle_01,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_Idle_02,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_Idle_03,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_Intro_01,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_IntroChu_01,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_PlayerAnnoyohorn_01,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_PlayerAnnoyotron_01,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_PlayerMasterPlan_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003CD5 RID: 15573 RVA: 0x0013DAF4 File Offset: 0x0013BCF4
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_HeroPower_01,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_HeroPower_02,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_HeroPower_03,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_HeroPower_04,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_HeroPower_06,
			DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_BossBuffMinion_01
		};
	}

	// Token: 0x06003CD6 RID: 15574 RVA: 0x0013DB66 File Offset: 0x0013BD66
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_52h.m_IdleLines;
	}

	// Token: 0x06003CD7 RID: 15575 RVA: 0x0013DB6D File Offset: 0x0013BD6D
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_EmoteResponse_01;
	}

	// Token: 0x06003CD8 RID: 15576 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003CD9 RID: 15577 RVA: 0x0013DBA8 File Offset: 0x0013BDA8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Chu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_IntroChu_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_Squeamlish")
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

	// Token: 0x06003CDA RID: 15578 RVA: 0x0013DC8F File Offset: 0x0013BE8F
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06003CDB RID: 15579 RVA: 0x0013DCA5 File Offset: 0x0013BEA5
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
		if (!(cardId == "DALA_722"))
		{
			if (!(cardId == "GVG_085"))
			{
				if (cardId == "DALA_726")
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_PlayerMasterPlan_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_PlayerAnnoyotron_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_PlayerAnnoyohorn_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003CDC RID: 15580 RVA: 0x0013DCBB File Offset: 0x0013BEBB
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
		if (!(cardId == "DAL_095"))
		{
			if (cardId == "DAL_096")
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_BossVioletWarden_02, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_BossVioletSpellsword_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040026C8 RID: 9928
	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_BossBuffMinion_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_BossBuffMinion_01.prefab:cfa446a643d60a64ea09b62b59456d34");

	// Token: 0x040026C9 RID: 9929
	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_BossVioletSpellsword_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_BossVioletSpellsword_01.prefab:37f600da9b03cac40b2fc5fded125e4a");

	// Token: 0x040026CA RID: 9930
	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_BossVioletWarden_02 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_BossVioletWarden_02.prefab:816f980a808332c488cc5b0d9817b385");

	// Token: 0x040026CB RID: 9931
	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_Death_02 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_Death_02.prefab:6a6881dd6ac050940a6e0f0632b77934");

	// Token: 0x040026CC RID: 9932
	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_DefeatPlayer_01.prefab:1fb99876dc000524e81c8f1ce8a5567f");

	// Token: 0x040026CD RID: 9933
	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_EmoteResponse_01.prefab:e43b11a56a2f88f44bb46ff323af670d");

	// Token: 0x040026CE RID: 9934
	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_HeroPower_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_HeroPower_01.prefab:e933330d1634bab45aa1a4876f2d7489");

	// Token: 0x040026CF RID: 9935
	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_HeroPower_02 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_HeroPower_02.prefab:2830b09c31945764e9e565a3eb30a08d");

	// Token: 0x040026D0 RID: 9936
	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_HeroPower_03 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_HeroPower_03.prefab:f7c6859c8c9989c44a2aa0b8cc346ad8");

	// Token: 0x040026D1 RID: 9937
	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_HeroPower_04 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_HeroPower_04.prefab:e594193c4461de84fbfc611586ea54e1");

	// Token: 0x040026D2 RID: 9938
	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_HeroPower_06 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_HeroPower_06.prefab:5a83fe616fe20f4488b80b1cdc86aa64");

	// Token: 0x040026D3 RID: 9939
	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_Idle_01.prefab:a4fdbd70f32e84b4893689942008fca6");

	// Token: 0x040026D4 RID: 9940
	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_Idle_02.prefab:3e80a289b9ef37b4eb353f51f591cd51");

	// Token: 0x040026D5 RID: 9941
	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_Idle_03.prefab:f280da6703835b34190f7c55d17f6366");

	// Token: 0x040026D6 RID: 9942
	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_Intro_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_Intro_01.prefab:5879922272189e94792461b07a327cdd");

	// Token: 0x040026D7 RID: 9943
	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_IntroChu_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_IntroChu_01.prefab:49870e98d3bf026408f1f03630c4fe3c");

	// Token: 0x040026D8 RID: 9944
	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_PlayerAnnoyohorn_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_PlayerAnnoyohorn_01.prefab:f18b673fd046d5a4192696af12901f01");

	// Token: 0x040026D9 RID: 9945
	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_PlayerAnnoyotron_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_PlayerAnnoyotron_01.prefab:35262de6dd338884eac1cc2ece516582");

	// Token: 0x040026DA RID: 9946
	private static readonly AssetReference VO_DALA_BOSS_52h_Female_Human_PlayerMasterPlan_01 = new AssetReference("VO_DALA_BOSS_52h_Female_Human_PlayerMasterPlan_01.prefab:75175cb91086157479d32c468547a002");

	// Token: 0x040026DB RID: 9947
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_Idle_01,
		DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_Idle_02,
		DALA_Dungeon_Boss_52h.VO_DALA_BOSS_52h_Female_Human_Idle_03
	};

	// Token: 0x040026DC RID: 9948
	private HashSet<string> m_playedLines = new HashSet<string>();
}
