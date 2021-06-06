using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004C1 RID: 1217
public class ULDA_Dungeon_Boss_69h : ULDA_Dungeon
{
	// Token: 0x06004139 RID: 16697 RVA: 0x0015CBD0 File Offset: 0x0015ADD0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_BossCounterSpellTrigger_01,
			ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_BossMirrorImage_01,
			ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_BossTwinSpellCast_01,
			ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_Death_01,
			ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_DefeatPlayer_01,
			ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_EmoteResponse_01,
			ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_HeroPower_01,
			ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_HeroPower_02,
			ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_HeroPower_03,
			ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_HeroPower_04,
			ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_HeroPower_05,
			ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_Idle_01,
			ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_Idle_02,
			ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_Intro_01,
			ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_IntroReno_01,
			ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_PlayerMalygos_01,
			ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_PlayerTitanDiscTreasure_01,
			ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_PlayerTriggerPyroblast_01,
			ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_TurnOne_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600413A RID: 16698 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600413B RID: 16699 RVA: 0x0015CD64 File Offset: 0x0015AF64
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x0600413C RID: 16700 RVA: 0x0015CD6C File Offset: 0x0015AF6C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_EmoteResponse_01;
	}

	// Token: 0x0600413D RID: 16701 RVA: 0x0015CDA4 File Offset: 0x0015AFA4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Reno")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_IntroReno_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x0600413E RID: 16702 RVA: 0x0015CE7E File Offset: 0x0015B07E
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
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerLines);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_BossCounterSpellTrigger_01, 2.5f);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_BossTwinSpellCast_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x0600413F RID: 16703 RVA: 0x0015CE94 File Offset: 0x0015B094
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
		if (!(cardId == "EX1_563"))
		{
			if (!(cardId == "ULDA_403"))
			{
				if (cardId == "EX1_279")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_PlayerTriggerPyroblast_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_PlayerTitanDiscTreasure_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_PlayerMalygos_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004140 RID: 16704 RVA: 0x0015CEAA File Offset: 0x0015B0AA
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
		if (cardId == "CS2_027")
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_BossMirrorImage_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002FFA RID: 12282
	private static readonly AssetReference VO_ULDA_BOSS_69h_Female_TitanConstruct_BossCounterSpellTrigger_01 = new AssetReference("VO_ULDA_BOSS_69h_Female_TitanConstruct_BossCounterSpellTrigger_01.prefab:bb2ebbe45205ce449bc9b823a559cb26");

	// Token: 0x04002FFB RID: 12283
	private static readonly AssetReference VO_ULDA_BOSS_69h_Female_TitanConstruct_BossMirrorImage_01 = new AssetReference("VO_ULDA_BOSS_69h_Female_TitanConstruct_BossMirrorImage_01.prefab:5d7f7e4a7ed524143b23a2de6c5a757a");

	// Token: 0x04002FFC RID: 12284
	private static readonly AssetReference VO_ULDA_BOSS_69h_Female_TitanConstruct_BossTwinSpellCast_01 = new AssetReference("VO_ULDA_BOSS_69h_Female_TitanConstruct_BossTwinSpellCast_01.prefab:5da20278e94be934ebc5cd77f1174eb7");

	// Token: 0x04002FFD RID: 12285
	private static readonly AssetReference VO_ULDA_BOSS_69h_Female_TitanConstruct_Death_01 = new AssetReference("VO_ULDA_BOSS_69h_Female_TitanConstruct_Death_01.prefab:05887be8c4fc6ab49b8533df44cda76e");

	// Token: 0x04002FFE RID: 12286
	private static readonly AssetReference VO_ULDA_BOSS_69h_Female_TitanConstruct_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_69h_Female_TitanConstruct_DefeatPlayer_01.prefab:f14e2ae9608c6a94e9817f6c199284fc");

	// Token: 0x04002FFF RID: 12287
	private static readonly AssetReference VO_ULDA_BOSS_69h_Female_TitanConstruct_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_69h_Female_TitanConstruct_EmoteResponse_01.prefab:2d781b3988de8b94a92147fcc1fb3a8f");

	// Token: 0x04003000 RID: 12288
	private static readonly AssetReference VO_ULDA_BOSS_69h_Female_TitanConstruct_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_69h_Female_TitanConstruct_HeroPower_01.prefab:c5e70422b9eded649b9d677743ad1af0");

	// Token: 0x04003001 RID: 12289
	private static readonly AssetReference VO_ULDA_BOSS_69h_Female_TitanConstruct_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_69h_Female_TitanConstruct_HeroPower_02.prefab:51380cf01ebd89b4e9e5825595e5e974");

	// Token: 0x04003002 RID: 12290
	private static readonly AssetReference VO_ULDA_BOSS_69h_Female_TitanConstruct_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_69h_Female_TitanConstruct_HeroPower_03.prefab:591bdf714d4c5a14da6f9240a9e1616a");

	// Token: 0x04003003 RID: 12291
	private static readonly AssetReference VO_ULDA_BOSS_69h_Female_TitanConstruct_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_69h_Female_TitanConstruct_HeroPower_04.prefab:d0ed7f49016f5f34586e060e4f6a1637");

	// Token: 0x04003004 RID: 12292
	private static readonly AssetReference VO_ULDA_BOSS_69h_Female_TitanConstruct_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_69h_Female_TitanConstruct_HeroPower_05.prefab:7e41020cd8dd48648933eecdd2935404");

	// Token: 0x04003005 RID: 12293
	private static readonly AssetReference VO_ULDA_BOSS_69h_Female_TitanConstruct_Idle_01 = new AssetReference("VO_ULDA_BOSS_69h_Female_TitanConstruct_Idle_01.prefab:a45067e046c88774490809c0aa16d6e8");

	// Token: 0x04003006 RID: 12294
	private static readonly AssetReference VO_ULDA_BOSS_69h_Female_TitanConstruct_Idle_02 = new AssetReference("VO_ULDA_BOSS_69h_Female_TitanConstruct_Idle_02.prefab:10691d5a7a0a5a84ab84d14c7694c87c");

	// Token: 0x04003007 RID: 12295
	private static readonly AssetReference VO_ULDA_BOSS_69h_Female_TitanConstruct_Intro_01 = new AssetReference("VO_ULDA_BOSS_69h_Female_TitanConstruct_Intro_01.prefab:4a6e76347d22ec04d91056b1b1805ecf");

	// Token: 0x04003008 RID: 12296
	private static readonly AssetReference VO_ULDA_BOSS_69h_Female_TitanConstruct_IntroReno_01 = new AssetReference("VO_ULDA_BOSS_69h_Female_TitanConstruct_IntroReno_01.prefab:34603d9007f39e746ab2f23cfb4070d9");

	// Token: 0x04003009 RID: 12297
	private static readonly AssetReference VO_ULDA_BOSS_69h_Female_TitanConstruct_PlayerMalygos_01 = new AssetReference("VO_ULDA_BOSS_69h_Female_TitanConstruct_PlayerMalygos_01.prefab:fdb4640b034264c4a85df389492aca56");

	// Token: 0x0400300A RID: 12298
	private static readonly AssetReference VO_ULDA_BOSS_69h_Female_TitanConstruct_PlayerTitanDiscTreasure_01 = new AssetReference("VO_ULDA_BOSS_69h_Female_TitanConstruct_PlayerTitanDiscTreasure_01.prefab:4b1e43b602234a44ba367a0ee9e28416");

	// Token: 0x0400300B RID: 12299
	private static readonly AssetReference VO_ULDA_BOSS_69h_Female_TitanConstruct_PlayerTriggerPyroblast_01 = new AssetReference("VO_ULDA_BOSS_69h_Female_TitanConstruct_PlayerTriggerPyroblast_01.prefab:d2e5b23edbe326744a607c09c3dbe57d");

	// Token: 0x0400300C RID: 12300
	private static readonly AssetReference VO_ULDA_BOSS_69h_Female_TitanConstruct_TurnOne_01 = new AssetReference("VO_ULDA_BOSS_69h_Female_TitanConstruct_TurnOne_01.prefab:adc2cbbca322bcc4e8e3d21adae56b9c");

	// Token: 0x0400300D RID: 12301
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_HeroPower_01,
		ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_HeroPower_02,
		ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_HeroPower_03,
		ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_HeroPower_04,
		ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_HeroPower_05
	};

	// Token: 0x0400300E RID: 12302
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_Idle_01,
		ULDA_Dungeon_Boss_69h.VO_ULDA_BOSS_69h_Female_TitanConstruct_Idle_02
	};

	// Token: 0x0400300F RID: 12303
	private HashSet<string> m_playedLines = new HashSet<string>();
}
