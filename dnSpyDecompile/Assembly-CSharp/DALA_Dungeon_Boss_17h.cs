using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200043E RID: 1086
public class DALA_Dungeon_Boss_17h : DALA_Dungeon
{
	// Token: 0x06003B20 RID: 15136 RVA: 0x00132D30 File Offset: 0x00130F30
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_BossDruidOfThe_01,
			DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_BossNourish_01,
			DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_BossWardruidLoti_01,
			DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_Death_01,
			DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_DefeatPlayer_01,
			DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_EmoteResponse_01,
			DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_01,
			DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_02,
			DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_03,
			DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_04,
			DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_Idle_01,
			DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_Idle_02,
			DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_Idle_03,
			DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_Intro_01,
			DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_IntroGeorge_01,
			DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_IntroSqueamlish_01,
			DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_PlayerChooseOne_01,
			DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_PlayerFandralStaghelm_01,
			DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_PlayerKeeperStellaris_01,
			DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_PlayerTreants_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003B21 RID: 15137 RVA: 0x00132ED4 File Offset: 0x001310D4
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_17h.m_IdleLines;
	}

	// Token: 0x06003B22 RID: 15138 RVA: 0x00132EDB File Offset: 0x001310DB
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_EmoteResponse_01;
	}

	// Token: 0x06003B23 RID: 15139 RVA: 0x00132F14 File Offset: 0x00131114
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Squeamlish")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_IntroSqueamlish_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003B24 RID: 15140 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003B25 RID: 15141 RVA: 0x0013302D File Offset: 0x0013122D
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
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_PlayerChooseOne_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_BossHeroPowerTrigger);
		}
		yield break;
	}

	// Token: 0x06003B26 RID: 15142 RVA: 0x00133043 File Offset: 0x00131243
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
		if (!(cardId == "DAL_732"))
		{
			if (!(cardId == "OG_044"))
			{
				if (cardId == "GIL_663t" || cardId == "FP1_019t" || cardId == "EX1_158t" || cardId == "DAL_256t2")
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_PlayerTreants_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_PlayerFandralStaghelm_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_PlayerKeeperStellaris_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003B27 RID: 15143 RVA: 0x00133059 File Offset: 0x00131259
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 2232797698U)
		{
			if (num <= 751096846U)
			{
				if (num != 666768819U)
				{
					if (num != 751096846U)
					{
						goto IL_2A7;
					}
					if (!(cardId == "BRM_010"))
					{
						goto IL_2A7;
					}
				}
				else if (!(cardId == "ICC_051"))
				{
					goto IL_2A7;
				}
			}
			else if (num != 1196208231U)
			{
				if (num != 2232797698U)
				{
					goto IL_2A7;
				}
				if (!(cardId == "EX1_165"))
				{
					goto IL_2A7;
				}
			}
			else if (!(cardId == "GIL_188"))
			{
				goto IL_2A7;
			}
		}
		else if (num <= 2316091897U)
		{
			if (num != 2249575317U)
			{
				if (num != 2316091897U)
				{
					goto IL_2A7;
				}
				if (!(cardId == "AT_042"))
				{
					goto IL_2A7;
				}
			}
			else
			{
				if (!(cardId == "EX1_164"))
				{
					goto IL_2A7;
				}
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_BossNourish_01, 2.5f);
				goto IL_2A7;
			}
		}
		else if (num != 3502685330U)
		{
			if (num != 3834560214U)
			{
				goto IL_2A7;
			}
			if (!(cardId == "TRL_343"))
			{
				goto IL_2A7;
			}
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_BossWardruidLoti_01, 2.5f);
			goto IL_2A7;
		}
		else if (!(cardId == "GVG_080"))
		{
			goto IL_2A7;
		}
		yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_BossDruidOfThe_01, 2.5f);
		IL_2A7:
		yield break;
	}

	// Token: 0x04002393 RID: 9107
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_BossDruidOfThe_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_BossDruidOfThe_01.prefab:f816b776da34ae049bbd8c821c9cba18");

	// Token: 0x04002394 RID: 9108
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_BossNourish_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_BossNourish_01.prefab:eedfb8ad0daaa6a4685e981ed9cf9b6b");

	// Token: 0x04002395 RID: 9109
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_BossWardruidLoti_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_BossWardruidLoti_01.prefab:101d24d581760ac4fa3054168b69b413");

	// Token: 0x04002396 RID: 9110
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_Death_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_Death_01.prefab:84cf1ed3a4bf89e4eb349ba16ba89c12");

	// Token: 0x04002397 RID: 9111
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_DefeatPlayer_01.prefab:7706c0cbc2fb7894986e32412de2d0eb");

	// Token: 0x04002398 RID: 9112
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_EmoteResponse_01.prefab:eeb81a6f93411df4686ccc4b2c4f4299");

	// Token: 0x04002399 RID: 9113
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_01.prefab:eb0f6e31b2aaf0048b3d384fd6d533a1");

	// Token: 0x0400239A RID: 9114
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_02 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_02.prefab:4b74d0dab742b0f438fbc428de581fad");

	// Token: 0x0400239B RID: 9115
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_03 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_03.prefab:c8a67e4db1dc8fc4db5968b108f9efb2");

	// Token: 0x0400239C RID: 9116
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_04 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_04.prefab:4aaa07f800ad2c742bb2c65c57833b6e");

	// Token: 0x0400239D RID: 9117
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_Idle_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_Idle_01.prefab:7713b0bee3e8c994190fab9e47c65368");

	// Token: 0x0400239E RID: 9118
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_Idle_02 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_Idle_02.prefab:47dd7a4bcf701b240ba537d51d9fceb1");

	// Token: 0x0400239F RID: 9119
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_Idle_03 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_Idle_03.prefab:27ccac1585bb7f84789946cf4f7a5a0b");

	// Token: 0x040023A0 RID: 9120
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_Intro_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_Intro_01.prefab:f4b9aa6b5351e274ba3266e1d31f6990");

	// Token: 0x040023A1 RID: 9121
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_IntroGeorge_01.prefab:f7c7036e6164bab4abdbfc8e109b7aaf");

	// Token: 0x040023A2 RID: 9122
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_IntroSqueamlish_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_IntroSqueamlish_01.prefab:9c7936fca250f0940b21f8e9d2403a85");

	// Token: 0x040023A3 RID: 9123
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_PlayerChooseOne_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_PlayerChooseOne_01.prefab:51fdcd9e20919344a88a6d323fe418de");

	// Token: 0x040023A4 RID: 9124
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_PlayerFandralStaghelm_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_PlayerFandralStaghelm_01.prefab:0e457f95dacc78144a795b24e121b8dc");

	// Token: 0x040023A5 RID: 9125
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_PlayerKeeperStellaris_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_PlayerKeeperStellaris_01.prefab:99c85ed702fb28c41826195abfda5246");

	// Token: 0x040023A6 RID: 9126
	private static readonly AssetReference VO_DALA_BOSS_17h_Female_Tauren_PlayerTreants_01 = new AssetReference("VO_DALA_BOSS_17h_Female_Tauren_PlayerTreants_01.prefab:2069dd3394216d7428d7c2d2121b8944");

	// Token: 0x040023A7 RID: 9127
	private List<string> m_BossHeroPowerTrigger = new List<string>
	{
		DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_01,
		DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_02,
		DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_03,
		DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_HeroPowerTrigger_04
	};

	// Token: 0x040023A8 RID: 9128
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_Idle_01,
		DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_Idle_02,
		DALA_Dungeon_Boss_17h.VO_DALA_BOSS_17h_Female_Tauren_Idle_03
	};

	// Token: 0x040023A9 RID: 9129
	private HashSet<string> m_playedLines = new HashSet<string>();
}
