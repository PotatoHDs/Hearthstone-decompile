using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000499 RID: 1177
public class ULDA_Dungeon_Boss_28h : ULDA_Dungeon
{
	// Token: 0x06003F77 RID: 16247 RVA: 0x00150824 File Offset: 0x0014EA24
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_BossTriggerAlAkir_01,
			ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_BossTriggerMurmuringElemental_01,
			ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_Death_01,
			ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_DefeatPlayer_01,
			ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_EmoteResponse_01,
			ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_01,
			ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_02,
			ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_03,
			ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_04,
			ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_05,
			ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_Idle_01,
			ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_Idle_03,
			ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_Intro_01,
			ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Sandstorm_Elemental_01,
			ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Siamat_01,
			ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Swarm_of_Locusts_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003F78 RID: 16248 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003F79 RID: 16249 RVA: 0x00150988 File Offset: 0x0014EB88
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003F7A RID: 16250 RVA: 0x00150990 File Offset: 0x0014EB90
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06003F7B RID: 16251 RVA: 0x00150998 File Offset: 0x0014EB98
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_EmoteResponse_01;
	}

	// Token: 0x06003F7C RID: 16252 RVA: 0x001509D0 File Offset: 0x0014EBD0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
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

	// Token: 0x06003F7D RID: 16253 RVA: 0x00150A59 File Offset: 0x0014EC59
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06003F7E RID: 16254 RVA: 0x00150A6F File Offset: 0x0014EC6F
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
		if (!(cardId == "ULD_158"))
		{
			if (!(cardId == "ULD_713"))
			{
				if (cardId == "ULD_178")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Siamat_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Swarm_of_Locusts_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Sandstorm_Elemental_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003F7F RID: 16255 RVA: 0x00150A85 File Offset: 0x0014EC85
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
		if (!(cardId == "NEW1_010"))
		{
			if (cardId == "LOOT_517")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_BossTriggerMurmuringElemental_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_BossTriggerAlAkir_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002C40 RID: 11328
	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_BossTriggerAlAkir_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_BossTriggerAlAkir_01.prefab:2170c1c365e9bd34bb5fe00a59990557");

	// Token: 0x04002C41 RID: 11329
	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_BossTriggerMurmuringElemental_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_BossTriggerMurmuringElemental_01.prefab:87d63cbce23f44a4ea88f8c067d8b7dd");

	// Token: 0x04002C42 RID: 11330
	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_Death_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_Death_01.prefab:60bfa7953200b27418356cb4317499ba");

	// Token: 0x04002C43 RID: 11331
	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_DefeatPlayer_01.prefab:d49d15d9a47db8d479fdc664e42cfe85");

	// Token: 0x04002C44 RID: 11332
	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_EmoteResponse_01.prefab:a09a7ebf7b3c6a8419cb06ef9ee33da3");

	// Token: 0x04002C45 RID: 11333
	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_01.prefab:3f0befd3bfb9126488242c1fb5b9df42");

	// Token: 0x04002C46 RID: 11334
	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_02.prefab:59b79fa742b1d8c4aa7a60d26f4dc92b");

	// Token: 0x04002C47 RID: 11335
	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_03.prefab:96255a10992be1f4ca765472dc954096");

	// Token: 0x04002C48 RID: 11336
	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_04.prefab:91ff1f35bc33f99448740d632e880aae");

	// Token: 0x04002C49 RID: 11337
	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_05.prefab:07a2b666d72066a409c9b4907a3e27ad");

	// Token: 0x04002C4A RID: 11338
	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_Idle_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_Idle_01.prefab:e1ee77c7cbde6cc4f900edc843d40333");

	// Token: 0x04002C4B RID: 11339
	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_Idle_03 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_Idle_03.prefab:647f4eee0f573ba4ab66d5d4e5ab8e1c");

	// Token: 0x04002C4C RID: 11340
	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_Intro_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_Intro_01.prefab:b2d4fa03f5cd4ca44889408bd45ce45e");

	// Token: 0x04002C4D RID: 11341
	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Sandstorm_Elemental_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Sandstorm_Elemental_01.prefab:2cfeb12312e30f04d9e69faeb0ecc1cb");

	// Token: 0x04002C4E RID: 11342
	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Siamat_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Siamat_01.prefab:3c29e7ed5727eb34e884af5158994141");

	// Token: 0x04002C4F RID: 11343
	private static readonly AssetReference VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Swarm_of_Locusts_01 = new AssetReference("VO_ULDA_BOSS_28h_Female_Revenant_PlayerTrigger_Swarm_of_Locusts_01.prefab:c8bee67a877784d46869eb20a987f669");

	// Token: 0x04002C50 RID: 11344
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_01,
		ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_02,
		ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_03,
		ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_04,
		ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_HeroPower_05
	};

	// Token: 0x04002C51 RID: 11345
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_Idle_01,
		ULDA_Dungeon_Boss_28h.VO_ULDA_BOSS_28h_Female_Revenant_Idle_03
	};

	// Token: 0x04002C52 RID: 11346
	private HashSet<string> m_playedLines = new HashSet<string>();
}
