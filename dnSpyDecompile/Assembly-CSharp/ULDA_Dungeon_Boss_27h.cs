using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000498 RID: 1176
public class ULDA_Dungeon_Boss_27h : ULDA_Dungeon
{
	// Token: 0x06003F69 RID: 16233 RVA: 0x0015034C File Offset: 0x0014E54C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_BossCleverDisguise_01,
			ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_BossTriggerTreasure_01,
			ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_BossWastewanderSapper_01,
			ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_Death_01,
			ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_DefeatPlayer_01,
			ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_EmoteResponse_01,
			ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_HeroPower_01,
			ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_HeroPower_02,
			ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_HeroPower_03,
			ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_HeroPower_04,
			ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_HeroPower_05,
			ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_Idle_01,
			ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_Idle_02,
			ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_Idle_03,
			ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_Intro_01,
			ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_IntroElise_01,
			ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_PlayerHeroTreasure_01,
			ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_PlayerZephyrs_01,
			ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_TurnOne_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003F6A RID: 16234 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003F6B RID: 16235 RVA: 0x001504E0 File Offset: 0x0014E6E0
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003F6C RID: 16236 RVA: 0x001504E8 File Offset: 0x0014E6E8
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06003F6D RID: 16237 RVA: 0x001504F0 File Offset: 0x0014E6F0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_EmoteResponse_01;
	}

	// Token: 0x06003F6E RID: 16238 RVA: 0x00150528 File Offset: 0x0014E728
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Elise")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_IntroElise_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003F6F RID: 16239 RVA: 0x00150602 File Offset: 0x0014E802
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 101)
		{
			if (missionEvent != 102)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_BossTriggerTreasure_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_PlayerHeroTreasure_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003F70 RID: 16240 RVA: 0x00150618 File Offset: 0x0014E818
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
		if (!(cardId == "ULD_280"))
		{
			if (cardId == "ULD_003")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_PlayerZephyrs_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_BossWastewanderSapper_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003F71 RID: 16241 RVA: 0x0015062E File Offset: 0x0014E82E
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
		if (cardId == "ULD_328")
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_BossCleverDisguise_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002C2A RID: 11306
	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_BossCleverDisguise_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_BossCleverDisguise_01.prefab:a0e92a26ed675e64e8ba8bd8496808b8");

	// Token: 0x04002C2B RID: 11307
	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_BossTriggerTreasure_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_BossTriggerTreasure_01.prefab:08b367b7748041741983b215559e352f");

	// Token: 0x04002C2C RID: 11308
	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_BossWastewanderSapper_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_BossWastewanderSapper_01.prefab:e26267b4645fd38458eedd88e7b8b492");

	// Token: 0x04002C2D RID: 11309
	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_Death_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_Death_01.prefab:4d4e776a143b560458db00eaf822a775");

	// Token: 0x04002C2E RID: 11310
	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_DefeatPlayer_01.prefab:59d4e06534fcb1a4cad81d8ca7a891d2");

	// Token: 0x04002C2F RID: 11311
	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_EmoteResponse_01.prefab:16976ba8280b2d94e98ad8bdc8407f2d");

	// Token: 0x04002C30 RID: 11312
	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_HeroPower_01.prefab:97b1ad0e6f060fc4587c006b00ee462a");

	// Token: 0x04002C31 RID: 11313
	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_HeroPower_02.prefab:db88a8eed1eaf444f8035ef5f3543d32");

	// Token: 0x04002C32 RID: 11314
	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_HeroPower_03.prefab:2dec94d9616c3b74c82891a3f82b2d5d");

	// Token: 0x04002C33 RID: 11315
	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_HeroPower_04.prefab:c15f4f63560cb24409fd03673e94b673");

	// Token: 0x04002C34 RID: 11316
	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_HeroPower_05.prefab:9367762c641a5144087506c659b2b2bb");

	// Token: 0x04002C35 RID: 11317
	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_Idle_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_Idle_01.prefab:bea1a957007cb064bb71fe6dcb6a49d2");

	// Token: 0x04002C36 RID: 11318
	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_Idle_02 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_Idle_02.prefab:a5be605353dbde6419d49d9f0cbeb77f");

	// Token: 0x04002C37 RID: 11319
	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_Idle_03 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_Idle_03.prefab:c206e7eb72e943f47bbf15cfd538e5ad");

	// Token: 0x04002C38 RID: 11320
	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_Intro_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_Intro_01.prefab:13f3e889f97e3db4488c6b274d2d3707");

	// Token: 0x04002C39 RID: 11321
	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_IntroElise_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_IntroElise_01.prefab:1e9b7ee341f08bf4380638d01dc1e3ba");

	// Token: 0x04002C3A RID: 11322
	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_PlayerHeroTreasure_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_PlayerHeroTreasure_01.prefab:e553865a95475d14f90aac0bb6a27177");

	// Token: 0x04002C3B RID: 11323
	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_PlayerZephyrs_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_PlayerZephyrs_01.prefab:e29144619d9e37945b9f7efb5d8b852e");

	// Token: 0x04002C3C RID: 11324
	private static readonly AssetReference VO_ULDA_BOSS_27h_Male_Troll_TurnOne_01 = new AssetReference("VO_ULDA_BOSS_27h_Male_Troll_TurnOne_01.prefab:2a2cc3f2a11909d4797aa4f3f54aee6f");

	// Token: 0x04002C3D RID: 11325
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_HeroPower_01,
		ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_HeroPower_02,
		ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_HeroPower_03,
		ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_HeroPower_04,
		ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_HeroPower_05
	};

	// Token: 0x04002C3E RID: 11326
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_Idle_01,
		ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_Idle_02,
		ULDA_Dungeon_Boss_27h.VO_ULDA_BOSS_27h_Male_Troll_Idle_03
	};

	// Token: 0x04002C3F RID: 11327
	private HashSet<string> m_playedLines = new HashSet<string>();
}
