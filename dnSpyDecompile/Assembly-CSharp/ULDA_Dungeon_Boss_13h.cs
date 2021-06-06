using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200048A RID: 1162
public class ULDA_Dungeon_Boss_13h : ULDA_Dungeon
{
	// Token: 0x06003EC3 RID: 16067 RVA: 0x0014CEFC File Offset: 0x0014B0FC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_BossHyenaAlpha_01,
			ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_BossMarkedShot_01,
			ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_BossSnakeTrapTrigger_01,
			ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_Death_01,
			ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_DefeatPlayer_01,
			ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_EmoteResponse_01,
			ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_01,
			ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_02,
			ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_04,
			ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_05,
			ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_Idle_01,
			ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_Idle_02,
			ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_Idle_03,
			ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_Intro_01,
			ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_IntroBrannResponse_01,
			ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_PlayerBaku_GiantAnaconda_01,
			ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_PlayerSnakeTrap_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003EC4 RID: 16068 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003EC5 RID: 16069 RVA: 0x0014D070 File Offset: 0x0014B270
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003EC6 RID: 16070 RVA: 0x0014D078 File Offset: 0x0014B278
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06003EC7 RID: 16071 RVA: 0x0014D080 File Offset: 0x0014B280
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_EmoteResponse_01;
	}

	// Token: 0x06003EC8 RID: 16072 RVA: 0x0014D0B8 File Offset: 0x0014B2B8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Brann")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_IntroBrannResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "ULDA_Reno")
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

	// Token: 0x06003EC9 RID: 16073 RVA: 0x0014D19F File Offset: 0x0014B39F
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_BossSnakeTrapTrigger_01, 2.5f);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06003ECA RID: 16074 RVA: 0x0014D1B5 File Offset: 0x0014B3B5
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
		if (!(cardId == "ULD_154"))
		{
			if (!(cardId == "DAL_371"))
			{
				if (!(cardId == "GIL_826") && !(cardId == "UNG_086"))
				{
					if (cardId == "EX1_554")
					{
						yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_PlayerSnakeTrap_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_PlayerBaku_GiantAnaconda_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_BossMarkedShot_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_BossHyenaAlpha_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003ECB RID: 16075 RVA: 0x0014D1CB File Offset: 0x0014B3CB
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
		yield break;
	}

	// Token: 0x04002B41 RID: 11073
	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_BossHyenaAlpha_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_BossHyenaAlpha_01.prefab:fc14d47dc8353244d93b225bd0389c68");

	// Token: 0x04002B42 RID: 11074
	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_BossMarkedShot_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_BossMarkedShot_01.prefab:29f876551907be54a9db5b338b6c7bd2");

	// Token: 0x04002B43 RID: 11075
	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_BossSnakeTrapTrigger_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_BossSnakeTrapTrigger_01.prefab:ab431ecfefb581b4b89797fde7a7661c");

	// Token: 0x04002B44 RID: 11076
	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_Death_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_Death_01.prefab:bcad9230fd3554c408c6d5bdbbc8d2e9");

	// Token: 0x04002B45 RID: 11077
	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_DefeatPlayer_01.prefab:06ad541e8ea9f3048bf5edf41bc654c9");

	// Token: 0x04002B46 RID: 11078
	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_EmoteResponse_01.prefab:9a15c02ed8506bd42974df75bde1e269");

	// Token: 0x04002B47 RID: 11079
	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_01.prefab:b64af718a490c34448d870288a2c89d0");

	// Token: 0x04002B48 RID: 11080
	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_02.prefab:0abadc0695b27f74ea260c27ba85a77a");

	// Token: 0x04002B49 RID: 11081
	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_04.prefab:c3aac27f4d0a7124db98697baaf3a284");

	// Token: 0x04002B4A RID: 11082
	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_05.prefab:8badb20ec4bdc344488e0cb2f15621ab");

	// Token: 0x04002B4B RID: 11083
	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_Idle_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_Idle_01.prefab:1958c5c47397efc4bacecdb7a69cb021");

	// Token: 0x04002B4C RID: 11084
	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_Idle_02 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_Idle_02.prefab:868cda1fa725f9a429554cdfdf937c6e");

	// Token: 0x04002B4D RID: 11085
	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_Idle_03 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_Idle_03.prefab:ad7ff5e31b93cc34bb27cdf541147cba");

	// Token: 0x04002B4E RID: 11086
	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_Intro_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_Intro_01.prefab:d637f53e47dd1ca4f8252cf959248287");

	// Token: 0x04002B4F RID: 11087
	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_IntroBrannResponse_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_IntroBrannResponse_01.prefab:b0e46475ae10b1547ba3bfb3d2756cbf");

	// Token: 0x04002B50 RID: 11088
	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_PlayerBaku_GiantAnaconda_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_PlayerBaku_GiantAnaconda_01.prefab:f55b0076b4967ea419ad8781253b73ca");

	// Token: 0x04002B51 RID: 11089
	private static readonly AssetReference VO_ULDA_BOSS_13h_Male_Gnoll_PlayerSnakeTrap_01 = new AssetReference("VO_ULDA_BOSS_13h_Male_Gnoll_PlayerSnakeTrap_01.prefab:97f0e105405f8a04ea032721288798f1");

	// Token: 0x04002B52 RID: 11090
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_01,
		ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_02,
		ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_04,
		ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_HeroPower_05
	};

	// Token: 0x04002B53 RID: 11091
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_Idle_01,
		ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_Idle_02,
		ULDA_Dungeon_Boss_13h.VO_ULDA_BOSS_13h_Male_Gnoll_Idle_03
	};

	// Token: 0x04002B54 RID: 11092
	private HashSet<string> m_playedLines = new HashSet<string>();
}
