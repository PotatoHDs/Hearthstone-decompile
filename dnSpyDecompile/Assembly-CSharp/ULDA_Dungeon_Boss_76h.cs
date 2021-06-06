using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004C8 RID: 1224
public class ULDA_Dungeon_Boss_76h : ULDA_Dungeon
{
	// Token: 0x06004197 RID: 16791 RVA: 0x0015ED7C File Offset: 0x0015CF7C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerAttackPlayerFace_01,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerDuel_01,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerPharoahsBlessing_01,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerSubdue_01,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerTruesilverChampion_01,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_Death_01,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_DefeatPlayer_01,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_EmoteResponse_01,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_01,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_02,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_03,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_05,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_01,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_02,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_03,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_Intro_01,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroBrann_01,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroEliseFirst_01,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroFinley_01,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroReno_01,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTrigger_Phalanx_Commander_01,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTrigger_Pressure_Plate_01,
			ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTriggerBrawl_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004198 RID: 16792 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004199 RID: 16793 RVA: 0x0015EF50 File Offset: 0x0015D150
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x0600419A RID: 16794 RVA: 0x0015EF58 File Offset: 0x0015D158
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x0600419B RID: 16795 RVA: 0x0015EF60 File Offset: 0x0015D160
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_EmoteResponse_01;
	}

	// Token: 0x0600419C RID: 16796 RVA: 0x0015EF98 File Offset: 0x0015D198
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Elise")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroEliseFirst_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x0600419D RID: 16797 RVA: 0x0015F072 File Offset: 0x0015D272
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerAttackPlayerFace_01, 2.5f);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x0600419E RID: 16798 RVA: 0x0015F088 File Offset: 0x0015D288
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
		if (!(cardId == "ULD_179"))
		{
			if (!(cardId == "ULD_152"))
			{
				if (cardId == "EX1_407")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTriggerBrawl_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTrigger_Pressure_Plate_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTrigger_Phalanx_Commander_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600419F RID: 16799 RVA: 0x0015F09E File Offset: 0x0015D29E
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
		if (!(cardId == "DAL_731"))
		{
			if (!(cardId == "ULD_143"))
			{
				if (!(cardId == "ULD_728"))
				{
					if (cardId == "CS2_097")
					{
						yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerTruesilverChampion_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerSubdue_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerPharoahsBlessing_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerDuel_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003093 RID: 12435
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerAttackPlayerFace_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerAttackPlayerFace_01.prefab:9b4c06729e6039c4780c734741b37d74");

	// Token: 0x04003094 RID: 12436
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerDuel_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerDuel_01.prefab:3bf7905f1cbcd4141b8f1e59da315c15");

	// Token: 0x04003095 RID: 12437
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerPharoahsBlessing_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerPharoahsBlessing_01.prefab:fa86f12201d231046833af91c1860bd4");

	// Token: 0x04003096 RID: 12438
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerSubdue_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerSubdue_01.prefab:8de52c1facb71234a9e3b3ee08a846ae");

	// Token: 0x04003097 RID: 12439
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerTruesilverChampion_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_BossTriggerTruesilverChampion_01.prefab:dd7819bb7c633554d9215357c51c01ed");

	// Token: 0x04003098 RID: 12440
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_Death_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_Death_01.prefab:a3f1d2d0d18732147b57a6de8bd7f49c");

	// Token: 0x04003099 RID: 12441
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_DefeatPlayer_01.prefab:62c754f4349233140bb7ce0b59a35645");

	// Token: 0x0400309A RID: 12442
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_EmoteResponse_01.prefab:df4d2803cef85a94eb991c35fbdab650");

	// Token: 0x0400309B RID: 12443
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_01.prefab:c7ab1d95a90160546b01bce377f11cd1");

	// Token: 0x0400309C RID: 12444
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_02.prefab:47f6e36096b69e44a9a0b9dd98d83cee");

	// Token: 0x0400309D RID: 12445
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_03.prefab:a036c2e85ae2fa54ea8df93102fe8578");

	// Token: 0x0400309E RID: 12446
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_05.prefab:f9aa8eb50f2f46542a91932685b20e49");

	// Token: 0x0400309F RID: 12447
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_01.prefab:41ef35d280ac8db419c8d0a5840a111c");

	// Token: 0x040030A0 RID: 12448
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_02 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_02.prefab:05e5998d4dfa6ad40af674e41707574a");

	// Token: 0x040030A1 RID: 12449
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_03 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_03.prefab:3d3ee22babf316c4999a4b644a4f6258");

	// Token: 0x040030A2 RID: 12450
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_Intro_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_Intro_01.prefab:a30c11b19fd170f4e82d06fcd4cd7343");

	// Token: 0x040030A3 RID: 12451
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroBrann_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroBrann_01.prefab:756d9a9b80d8c91448de51e1b67c3cf4");

	// Token: 0x040030A4 RID: 12452
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroEliseFirst_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroEliseFirst_01.prefab:7d486b9e0447d0e44a25fe90bac28290");

	// Token: 0x040030A5 RID: 12453
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroFinley_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroFinley_01.prefab:9397da5a8dd81e04ebf9283da03f7c63");

	// Token: 0x040030A6 RID: 12454
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroReno_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_IntroReno_01.prefab:e0c8919a5ab02814299c0e13f0604394");

	// Token: 0x040030A7 RID: 12455
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTrigger_Phalanx_Commander_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTrigger_Phalanx_Commander_01.prefab:a170e3c8678052540a514672a6006ccb");

	// Token: 0x040030A8 RID: 12456
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTrigger_Pressure_Plate_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTrigger_Pressure_Plate_01.prefab:a52a618cd4899e446858892f4aea6510");

	// Token: 0x040030A9 RID: 12457
	private static readonly AssetReference VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTriggerBrawl_01 = new AssetReference("VO_ULDA_BOSS_76h_Male_NefersetTolvir_PlayerTriggerBrawl_01.prefab:99471727957a2d04689c26ce214916a6");

	// Token: 0x040030AA RID: 12458
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_01,
		ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_02,
		ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_03,
		ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_HeroPower_05
	};

	// Token: 0x040030AB RID: 12459
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_01,
		ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_02,
		ULDA_Dungeon_Boss_76h.VO_ULDA_BOSS_76h_Male_NefersetTolvir_Idle_03
	};

	// Token: 0x040030AC RID: 12460
	private HashSet<string> m_playedLines = new HashSet<string>();
}
