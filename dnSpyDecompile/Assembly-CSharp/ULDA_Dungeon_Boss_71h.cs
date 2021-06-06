using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004C3 RID: 1219
public class ULDA_Dungeon_Boss_71h : ULDA_Dungeon
{
	// Token: 0x06004153 RID: 16723 RVA: 0x0015D568 File Offset: 0x0015B768
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_BossHiddenOasis_01,
			ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_BossNaturalize_01,
			ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_BossRampantGrowth_01,
			ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_DeathALT_01,
			ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_DefeatPlayer_01,
			ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_EmoteResponse_01,
			ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_01,
			ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_02,
			ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_03,
			ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_04,
			ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_01,
			ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_02,
			ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_03,
			ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_Intro_01,
			ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_IntroElise_01,
			ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTreant_01,
			ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTreeofLife_01,
			ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTriggerShadowWordDeath_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004154 RID: 16724 RVA: 0x0015D6EC File Offset: 0x0015B8EC
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06004155 RID: 16725 RVA: 0x0015D6F4 File Offset: 0x0015B8F4
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06004156 RID: 16726 RVA: 0x0015D6FC File Offset: 0x0015B8FC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_EmoteResponse_01;
	}

	// Token: 0x06004157 RID: 16727 RVA: 0x0015D734 File Offset: 0x0015B934
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Elise")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_IntroElise_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06004158 RID: 16728 RVA: 0x0015D80E File Offset: 0x0015BA0E
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06004159 RID: 16729 RVA: 0x0015D824 File Offset: 0x0015BA24
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
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 1373347759U)
		{
			if (num <= 409649590U)
			{
				if (num != 162915679U)
				{
					if (num != 409649590U)
					{
						goto IL_30C;
					}
					if (!(cardId == "ULD_137t"))
					{
						goto IL_30C;
					}
				}
				else if (!(cardId == "BOT_420"))
				{
					goto IL_30C;
				}
			}
			else if (num != 717358432U)
			{
				if (num != 1064003026U)
				{
					if (num != 1373347759U)
					{
						goto IL_30C;
					}
					if (!(cardId == "EX1_571"))
					{
						goto IL_30C;
					}
				}
				else
				{
					if (!(cardId == "EX1_622"))
					{
						goto IL_30C;
					}
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTriggerShadowWordDeath_01, 2.5f);
					goto IL_30C;
				}
			}
			else if (!(cardId == "EX1_tk9"))
			{
				goto IL_30C;
			}
		}
		else if (num <= 2417498602U)
		{
			if (num != 1791989495U)
			{
				if (num != 2417498602U)
				{
					goto IL_30C;
				}
				if (!(cardId == "EX1_158"))
				{
					goto IL_30C;
				}
			}
			else if (!(cardId == "GIL_663t"))
			{
				goto IL_30C;
			}
		}
		else if (num != 2431612764U)
		{
			if (num != 2747335539U)
			{
				if (num != 3621261496U)
				{
					goto IL_30C;
				}
				if (!(cardId == "GVG_033"))
				{
					goto IL_30C;
				}
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTreeofLife_01, 2.5f);
				goto IL_30C;
			}
			else if (!(cardId == "EX1_573t"))
			{
				goto IL_30C;
			}
		}
		else if (!(cardId == "DAL_256"))
		{
			goto IL_30C;
		}
		yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTreant_01, 2.5f);
		IL_30C:
		yield break;
	}

	// Token: 0x0600415A RID: 16730 RVA: 0x0015D83A File Offset: 0x0015BA3A
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
		if (!(cardId == "ULD_135"))
		{
			if (!(cardId == "EX1_161"))
			{
				if (cardId == "EX1_164a")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_BossRampantGrowth_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_BossNaturalize_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_BossHiddenOasis_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003026 RID: 12326
	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_BossHiddenOasis_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_BossHiddenOasis_01.prefab:9ab702164bdf9fd40a61ad7f55cf3cad");

	// Token: 0x04003027 RID: 12327
	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_BossNaturalize_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_BossNaturalize_01.prefab:ab112c9ee3a74c54db65ece8076b3680");

	// Token: 0x04003028 RID: 12328
	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_BossRampantGrowth_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_BossRampantGrowth_01.prefab:33a05918d3451e940bd69b07439a2928");

	// Token: 0x04003029 RID: 12329
	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_DeathALT_01.prefab:3402a6d59faa1a2449c313a293455e8a");

	// Token: 0x0400302A RID: 12330
	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_DefeatPlayer_01.prefab:d32314c7acbedfa46b959bd109eb52be");

	// Token: 0x0400302B RID: 12331
	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_EmoteResponse_01.prefab:0d45150628fcddf458ddf04468e7523c");

	// Token: 0x0400302C RID: 12332
	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_01.prefab:27e653f08ea5cc84e99b66d5fa984e92");

	// Token: 0x0400302D RID: 12333
	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_02.prefab:8613b0c830e8f03458c9f2915b0a21bc");

	// Token: 0x0400302E RID: 12334
	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_03.prefab:2b72d37ad26a81141a85120af4d5b25b");

	// Token: 0x0400302F RID: 12335
	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_04.prefab:4b76c1d2bf312f441ab03a37a6774723");

	// Token: 0x04003030 RID: 12336
	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_01.prefab:a75591ba5a2d49d4e9176ab048af0a3b");

	// Token: 0x04003031 RID: 12337
	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_02 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_02.prefab:da27890ccd9a93d4daa3c48518d5ccdc");

	// Token: 0x04003032 RID: 12338
	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_03 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_03.prefab:ed04416d6572afe4cb8483d34752e659");

	// Token: 0x04003033 RID: 12339
	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_Intro_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_Intro_01.prefab:65aa6b7b76277f343b80c00531b53863");

	// Token: 0x04003034 RID: 12340
	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_IntroElise_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_IntroElise_01.prefab:1091dccdaf4cc4342b79a377e428d641");

	// Token: 0x04003035 RID: 12341
	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTreant_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTreant_01.prefab:baffab126fabf3c4e92d42b6fb52132b");

	// Token: 0x04003036 RID: 12342
	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTreeofLife_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTreeofLife_01.prefab:8a0ae762684df394d9cc121866f1e34d");

	// Token: 0x04003037 RID: 12343
	private static readonly AssetReference VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTriggerShadowWordDeath_01 = new AssetReference("VO_ULDA_BOSS_71h_Male_TitanConstruct_PlayerTriggerShadowWordDeath_01.prefab:2bdc3ac2476557546923a49511ed6010");

	// Token: 0x04003038 RID: 12344
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_01,
		ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_02,
		ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_03,
		ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_HeroPower_04
	};

	// Token: 0x04003039 RID: 12345
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_01,
		ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_02,
		ULDA_Dungeon_Boss_71h.VO_ULDA_BOSS_71h_Male_TitanConstruct_Idle_03
	};

	// Token: 0x0400303A RID: 12346
	private HashSet<string> m_playedLines = new HashSet<string>();
}
