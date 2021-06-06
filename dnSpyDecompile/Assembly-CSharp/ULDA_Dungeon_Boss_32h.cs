using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200049D RID: 1181
public class ULDA_Dungeon_Boss_32h : ULDA_Dungeon
{
	// Token: 0x06003FAC RID: 16300 RVA: 0x00151908 File Offset: 0x0014FB08
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_BossTriggerDreamwayGuardians_01,
			ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_BossTriggerGroveTender_01,
			ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_BossTriggerKeeperoftheGrove_01,
			ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_Death_01,
			ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_DefeatPlayer_01,
			ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_EmoteResponse_01,
			ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_HeroPower_01,
			ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_HeroPower_02,
			ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_HeroPower_04,
			ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_HeroPower_05,
			ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_Idle_01,
			ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_Idle_02,
			ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_Idle_03,
			ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_Intro_01,
			ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_IntroResponseElise_01,
			ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_IntroSpecial_Finley_01,
			ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_PlayerTrigger_Desert_Hare_01,
			ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_PlayerTrigger_Pit_Crocolisk_01,
			ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_TurnOne_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003FAD RID: 16301 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003FAE RID: 16302 RVA: 0x00151A9C File Offset: 0x0014FC9C
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003FAF RID: 16303 RVA: 0x00151AA4 File Offset: 0x0014FCA4
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06003FB0 RID: 16304 RVA: 0x00151AAC File Offset: 0x0014FCAC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_EmoteResponse_01;
	}

	// Token: 0x06003FB1 RID: 16305 RVA: 0x00151AE4 File Offset: 0x0014FCE4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Finley")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_IntroSpecial_Finley_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "ULDA_Elise")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_IntroResponseElise_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003FB2 RID: 16306 RVA: 0x00151BFD File Offset: 0x0014FDFD
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_TurnOne_01, 2.5f);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06003FB3 RID: 16307 RVA: 0x00151C13 File Offset: 0x0014FE13
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
		if (!(cardId == "ULD_190"))
		{
			if (cardId == "ULD_719")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_PlayerTrigger_Desert_Hare_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_PlayerTrigger_Pit_Crocolisk_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003FB4 RID: 16308 RVA: 0x00151C29 File Offset: 0x0014FE29
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
		if (!(cardId == "DAL_733"))
		{
			if (!(cardId == "GVG_032"))
			{
				if (cardId == "EX1_166")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_BossTriggerKeeperoftheGrove_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_BossTriggerGroveTender_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_BossTriggerDreamwayGuardians_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002C8E RID: 11406
	private static readonly AssetReference VO_ULDA_BOSS_32h_Female_Dryad_BossTriggerDreamwayGuardians_01 = new AssetReference("VO_ULDA_BOSS_32h_Female_Dryad_BossTriggerDreamwayGuardians_01.prefab:5922f722b051c974dbe83c4e2318fcaf");

	// Token: 0x04002C8F RID: 11407
	private static readonly AssetReference VO_ULDA_BOSS_32h_Female_Dryad_BossTriggerGroveTender_01 = new AssetReference("VO_ULDA_BOSS_32h_Female_Dryad_BossTriggerGroveTender_01.prefab:dde8eae8cc992f24291f3373f7b88e9b");

	// Token: 0x04002C90 RID: 11408
	private static readonly AssetReference VO_ULDA_BOSS_32h_Female_Dryad_BossTriggerKeeperoftheGrove_01 = new AssetReference("VO_ULDA_BOSS_32h_Female_Dryad_BossTriggerKeeperoftheGrove_01.prefab:43e418c2f425ef74499558a3845ab164");

	// Token: 0x04002C91 RID: 11409
	private static readonly AssetReference VO_ULDA_BOSS_32h_Female_Dryad_Death_01 = new AssetReference("VO_ULDA_BOSS_32h_Female_Dryad_Death_01.prefab:adea5d33b8b88c946908a82b4eee3828");

	// Token: 0x04002C92 RID: 11410
	private static readonly AssetReference VO_ULDA_BOSS_32h_Female_Dryad_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_32h_Female_Dryad_DefeatPlayer_01.prefab:bd6b96434b051e84bb3fbc494f711e37");

	// Token: 0x04002C93 RID: 11411
	private static readonly AssetReference VO_ULDA_BOSS_32h_Female_Dryad_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_32h_Female_Dryad_EmoteResponse_01.prefab:71c60703ddf073b4d86bf76cd421646e");

	// Token: 0x04002C94 RID: 11412
	private static readonly AssetReference VO_ULDA_BOSS_32h_Female_Dryad_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_32h_Female_Dryad_HeroPower_01.prefab:c652311aff11b7c46bc19cfde1e6511b");

	// Token: 0x04002C95 RID: 11413
	private static readonly AssetReference VO_ULDA_BOSS_32h_Female_Dryad_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_32h_Female_Dryad_HeroPower_02.prefab:6f83fa87c2be53e42bc571e0bc874c6f");

	// Token: 0x04002C96 RID: 11414
	private static readonly AssetReference VO_ULDA_BOSS_32h_Female_Dryad_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_32h_Female_Dryad_HeroPower_04.prefab:769154c7b56df3843ba6d1c2f6473f7e");

	// Token: 0x04002C97 RID: 11415
	private static readonly AssetReference VO_ULDA_BOSS_32h_Female_Dryad_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_32h_Female_Dryad_HeroPower_05.prefab:50e69815bb85ef34897b69bf54fc7ba5");

	// Token: 0x04002C98 RID: 11416
	private static readonly AssetReference VO_ULDA_BOSS_32h_Female_Dryad_Idle_01 = new AssetReference("VO_ULDA_BOSS_32h_Female_Dryad_Idle_01.prefab:38dc9231669ec0e49bccedd3fca5748c");

	// Token: 0x04002C99 RID: 11417
	private static readonly AssetReference VO_ULDA_BOSS_32h_Female_Dryad_Idle_02 = new AssetReference("VO_ULDA_BOSS_32h_Female_Dryad_Idle_02.prefab:3d9fe1b16565e4e40a07e9be9f4204ec");

	// Token: 0x04002C9A RID: 11418
	private static readonly AssetReference VO_ULDA_BOSS_32h_Female_Dryad_Idle_03 = new AssetReference("VO_ULDA_BOSS_32h_Female_Dryad_Idle_03.prefab:c6614a4bb3105fd419229d1694b6d00d");

	// Token: 0x04002C9B RID: 11419
	private static readonly AssetReference VO_ULDA_BOSS_32h_Female_Dryad_Intro_01 = new AssetReference("VO_ULDA_BOSS_32h_Female_Dryad_Intro_01.prefab:7d3501d83239e3c45a51225c2b0ae57b");

	// Token: 0x04002C9C RID: 11420
	private static readonly AssetReference VO_ULDA_BOSS_32h_Female_Dryad_IntroResponseElise_01 = new AssetReference("VO_ULDA_BOSS_32h_Female_Dryad_IntroResponseElise_01.prefab:cc98403e13cb68b4c89206207c2cc830");

	// Token: 0x04002C9D RID: 11421
	private static readonly AssetReference VO_ULDA_BOSS_32h_Female_Dryad_IntroSpecial_Finley_01 = new AssetReference("VO_ULDA_BOSS_32h_Female_Dryad_IntroSpecial_Finley_01.prefab:1febfdf793f41ef44b03c3ce3dbe7e22");

	// Token: 0x04002C9E RID: 11422
	private static readonly AssetReference VO_ULDA_BOSS_32h_Female_Dryad_PlayerTrigger_Desert_Hare_01 = new AssetReference("VO_ULDA_BOSS_32h_Female_Dryad_PlayerTrigger_Desert_Hare_01.prefab:0ce977fda7ed0dc479176ed20f3308d4");

	// Token: 0x04002C9F RID: 11423
	private static readonly AssetReference VO_ULDA_BOSS_32h_Female_Dryad_PlayerTrigger_Pit_Crocolisk_01 = new AssetReference("VO_ULDA_BOSS_32h_Female_Dryad_PlayerTrigger_Pit_Crocolisk_01.prefab:8defb42f7b18b0347ba99ba6ecc8170e");

	// Token: 0x04002CA0 RID: 11424
	private static readonly AssetReference VO_ULDA_BOSS_32h_Female_Dryad_TurnOne_01 = new AssetReference("VO_ULDA_BOSS_32h_Female_Dryad_TurnOne_01.prefab:f5d63c5adb2131946b2d033ed5b0109c");

	// Token: 0x04002CA1 RID: 11425
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_HeroPower_01,
		ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_HeroPower_02,
		ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_HeroPower_04,
		ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_HeroPower_05
	};

	// Token: 0x04002CA2 RID: 11426
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_Idle_01,
		ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_Idle_02,
		ULDA_Dungeon_Boss_32h.VO_ULDA_BOSS_32h_Female_Dryad_Idle_03
	};

	// Token: 0x04002CA3 RID: 11427
	private HashSet<string> m_playedLines = new HashSet<string>();
}
