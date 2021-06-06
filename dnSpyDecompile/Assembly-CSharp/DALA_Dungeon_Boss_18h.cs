using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200043F RID: 1087
public class DALA_Dungeon_Boss_18h : DALA_Dungeon
{
	// Token: 0x06003B2C RID: 15148 RVA: 0x00133250 File Offset: 0x00131450
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_BossTotemicMight_01,
			DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_BossWindshearStormcaller_01,
			DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_Death_01,
			DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_DefeatPlayer_01,
			DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_EmoteResponse_01,
			DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_HeroPower_01,
			DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_HeroPower_02,
			DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_HeroPower_04,
			DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_Idle_01,
			DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_Idle_02,
			DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_Idle_03,
			DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_Intro_01,
			DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_IntroOlBarkeye_01,
			DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_IntroVessina_01,
			DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_PlayerPrimalTalisman_01,
			DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_PlayerTotemCrusher_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003B2D RID: 15149 RVA: 0x001333B4 File Offset: 0x001315B4
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_HeroPower_01,
			DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_HeroPower_02,
			DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_HeroPower_04
		};
	}

	// Token: 0x06003B2E RID: 15150 RVA: 0x001333EB File Offset: 0x001315EB
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_EmoteResponse_01;
	}

	// Token: 0x06003B2F RID: 15151 RVA: 0x00133423 File Offset: 0x00131623
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_18h.m_IdleLines;
	}

	// Token: 0x06003B30 RID: 15152 RVA: 0x0013342C File Offset: 0x0013162C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Barkeye")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_IntroOlBarkeye_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Vessina")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_IntroVessina_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003B31 RID: 15153 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003B32 RID: 15154 RVA: 0x00133545 File Offset: 0x00131745
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06003B33 RID: 15155 RVA: 0x0013355B File Offset: 0x0013175B
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
		if (!(cardId == "LOOT_344"))
		{
			if (cardId == "GIL_583")
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_PlayerTotemCrusher_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_PlayerPrimalTalisman_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003B34 RID: 15156 RVA: 0x00133571 File Offset: 0x00131771
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
		if (!(cardId == "EX1_244"))
		{
			if (cardId == "LOOT_518")
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_BossWindshearStormcaller_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_BossTotemicMight_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040023AA RID: 9130
	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_BossTotemicMight_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_BossTotemicMight_01.prefab:cb50cfe393848cf46a3f00f569b1727f");

	// Token: 0x040023AB RID: 9131
	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_BossWindshearStormcaller_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_BossWindshearStormcaller_01.prefab:014dc19dfaddbe047b4cb98356b51351");

	// Token: 0x040023AC RID: 9132
	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_Death_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_Death_01.prefab:9d0a745a6f8a71548ae3f7f1fb7bd144");

	// Token: 0x040023AD RID: 9133
	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_DefeatPlayer_01.prefab:d25caa7bf184f5942a4f807ea5fdeed3");

	// Token: 0x040023AE RID: 9134
	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_EmoteResponse_01.prefab:d0a61d9788efbd846b597d4b96f52a7e");

	// Token: 0x040023AF RID: 9135
	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_HeroPower_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_HeroPower_01.prefab:7b354bb68ea38014490ca7e4addd32ba");

	// Token: 0x040023B0 RID: 9136
	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_HeroPower_02 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_HeroPower_02.prefab:b535e9b0290069e4dbde8f165a376462");

	// Token: 0x040023B1 RID: 9137
	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_HeroPower_04 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_HeroPower_04.prefab:bd0f3eb73d546a741b059ebd6b0a96c6");

	// Token: 0x040023B2 RID: 9138
	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_Idle_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_Idle_01.prefab:330f0c6b5580aa84cbc914cd2a5490b2");

	// Token: 0x040023B3 RID: 9139
	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_Idle_02 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_Idle_02.prefab:0e0bf35478d59f842aa7c4dcc14812e3");

	// Token: 0x040023B4 RID: 9140
	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_Idle_03 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_Idle_03.prefab:f50f5918509b42740855b84f2daa09dd");

	// Token: 0x040023B5 RID: 9141
	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_Intro_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_Intro_01.prefab:6f158bba08ff3fa42b2490c10c128571");

	// Token: 0x040023B6 RID: 9142
	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_IntroOlBarkeye_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_IntroOlBarkeye_01.prefab:c88814d382ae33544b467635dc914b61");

	// Token: 0x040023B7 RID: 9143
	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_IntroVessina_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_IntroVessina_01.prefab:1722bf17e3dd1774cbc3ce74d8c17787");

	// Token: 0x040023B8 RID: 9144
	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_PlayerPrimalTalisman_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_PlayerPrimalTalisman_01.prefab:b202ec9a27f03594781ed49759413fb4");

	// Token: 0x040023B9 RID: 9145
	private static readonly AssetReference VO_DALA_BOSS_18h_Female_Draenei_PlayerTotemCrusher_01 = new AssetReference("VO_DALA_BOSS_18h_Female_Draenei_PlayerTotemCrusher_01.prefab:9a81ea28775d05347ad2dae910770238");

	// Token: 0x040023BA RID: 9146
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_Idle_01,
		DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_Idle_02,
		DALA_Dungeon_Boss_18h.VO_DALA_BOSS_18h_Female_Draenei_Idle_03
	};

	// Token: 0x040023BB RID: 9147
	private HashSet<string> m_playedLines = new HashSet<string>();
}
