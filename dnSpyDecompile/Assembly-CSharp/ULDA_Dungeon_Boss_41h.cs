using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004A6 RID: 1190
public class ULDA_Dungeon_Boss_41h : ULDA_Dungeon
{
	// Token: 0x06004021 RID: 16417 RVA: 0x001561FC File Offset: 0x001543FC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_BossDireFrenzy_01,
			ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_BossSecretPlan_01,
			ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_BossWanderingMonsterTrigger_01,
			ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_DeathALT_01,
			ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_DefeatPlayer_01,
			ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_EmoteResponse_01,
			ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_HeroPower_01,
			ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_HeroPower_03,
			ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_HeroPower_04,
			ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_HeroPowerBeast_01,
			ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_Idle_01,
			ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_Idle_02,
			ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_Idle_03,
			ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_Intro_01,
			ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_PlayerAldorPeacekeeper_01,
			ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_PlayerBazaarMugger_01,
			ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_TurnOne_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004022 RID: 16418 RVA: 0x00156370 File Offset: 0x00154570
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06004023 RID: 16419 RVA: 0x00156378 File Offset: 0x00154578
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_EmoteResponse_01;
	}

	// Token: 0x06004024 RID: 16420 RVA: 0x001563B0 File Offset: 0x001545B0
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

	// Token: 0x06004025 RID: 16421 RVA: 0x00156439 File Offset: 0x00154639
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
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_TurnOne_01, 2.5f);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_HeroPowerBeast_01, 2.5f);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerLines);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06004026 RID: 16422 RVA: 0x0015644F File Offset: 0x0015464F
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
		if (!(cardId == "EX1_382"))
		{
			if (cardId == "ULD_327")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_PlayerBazaarMugger_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_PlayerAldorPeacekeeper_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004027 RID: 16423 RVA: 0x00156465 File Offset: 0x00154665
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
		if (!(cardId == "GIL_828"))
		{
			if (!(cardId == "BOT_402"))
			{
				if (cardId == "LOOT_079")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_BossWanderingMonsterTrigger_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_BossSecretPlan_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_BossDireFrenzy_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002E00 RID: 11776
	private static readonly AssetReference VO_ULDA_BOSS_41h_Female_Worgen_BossDireFrenzy_01 = new AssetReference("VO_ULDA_BOSS_41h_Female_Worgen_BossDireFrenzy_01.prefab:a2ba893031e9c3e49ad4fff8edba1c03");

	// Token: 0x04002E01 RID: 11777
	private static readonly AssetReference VO_ULDA_BOSS_41h_Female_Worgen_BossSecretPlan_01 = new AssetReference("VO_ULDA_BOSS_41h_Female_Worgen_BossSecretPlan_01.prefab:c10e4ee33b24c5d4a8d5edc29466cd83");

	// Token: 0x04002E02 RID: 11778
	private static readonly AssetReference VO_ULDA_BOSS_41h_Female_Worgen_BossWanderingMonsterTrigger_01 = new AssetReference("VO_ULDA_BOSS_41h_Female_Worgen_BossWanderingMonsterTrigger_01.prefab:e35367dbb10d9854e919d883c0e73b67");

	// Token: 0x04002E03 RID: 11779
	private static readonly AssetReference VO_ULDA_BOSS_41h_Female_Worgen_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_41h_Female_Worgen_DeathALT_01.prefab:3c0c9f997c866b34d8cdf181e361cfff");

	// Token: 0x04002E04 RID: 11780
	private static readonly AssetReference VO_ULDA_BOSS_41h_Female_Worgen_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_41h_Female_Worgen_DefeatPlayer_01.prefab:f5944adbbace90b45a8e0545e1335700");

	// Token: 0x04002E05 RID: 11781
	private static readonly AssetReference VO_ULDA_BOSS_41h_Female_Worgen_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_41h_Female_Worgen_EmoteResponse_01.prefab:f294257c72044f641b993d1412b244fc");

	// Token: 0x04002E06 RID: 11782
	private static readonly AssetReference VO_ULDA_BOSS_41h_Female_Worgen_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_41h_Female_Worgen_HeroPower_01.prefab:9c0eb31129fac98428dffcdaf65653e9");

	// Token: 0x04002E07 RID: 11783
	private static readonly AssetReference VO_ULDA_BOSS_41h_Female_Worgen_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_41h_Female_Worgen_HeroPower_03.prefab:89758c4ccc1ff1246afff0ecb65c6edc");

	// Token: 0x04002E08 RID: 11784
	private static readonly AssetReference VO_ULDA_BOSS_41h_Female_Worgen_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_41h_Female_Worgen_HeroPower_04.prefab:d591c3de2e664434ab20239febff7d80");

	// Token: 0x04002E09 RID: 11785
	private static readonly AssetReference VO_ULDA_BOSS_41h_Female_Worgen_HeroPowerBeast_01 = new AssetReference("VO_ULDA_BOSS_41h_Female_Worgen_HeroPowerBeast_01.prefab:9fa688bbcb242ef4abf456d3dd4a6dbb");

	// Token: 0x04002E0A RID: 11786
	private static readonly AssetReference VO_ULDA_BOSS_41h_Female_Worgen_Idle_01 = new AssetReference("VO_ULDA_BOSS_41h_Female_Worgen_Idle_01.prefab:3a3d36299883a4f4595f18210eda093e");

	// Token: 0x04002E0B RID: 11787
	private static readonly AssetReference VO_ULDA_BOSS_41h_Female_Worgen_Idle_02 = new AssetReference("VO_ULDA_BOSS_41h_Female_Worgen_Idle_02.prefab:f8d2b77dd01f2ce479d4acc5a08b76cf");

	// Token: 0x04002E0C RID: 11788
	private static readonly AssetReference VO_ULDA_BOSS_41h_Female_Worgen_Idle_03 = new AssetReference("VO_ULDA_BOSS_41h_Female_Worgen_Idle_03.prefab:84ff7f9128664534fb0775be58ff001f");

	// Token: 0x04002E0D RID: 11789
	private static readonly AssetReference VO_ULDA_BOSS_41h_Female_Worgen_Intro_01 = new AssetReference("VO_ULDA_BOSS_41h_Female_Worgen_Intro_01.prefab:9aa0b82ee212b25438804f67cc46b672");

	// Token: 0x04002E0E RID: 11790
	private static readonly AssetReference VO_ULDA_BOSS_41h_Female_Worgen_PlayerAldorPeacekeeper_01 = new AssetReference("VO_ULDA_BOSS_41h_Female_Worgen_PlayerAldorPeacekeeper_01.prefab:c2c7bcc0da5ffd14fa24c87699117877");

	// Token: 0x04002E0F RID: 11791
	private static readonly AssetReference VO_ULDA_BOSS_41h_Female_Worgen_PlayerBazaarMugger_01 = new AssetReference("VO_ULDA_BOSS_41h_Female_Worgen_PlayerBazaarMugger_01.prefab:9abc1886e3503ab49a82aefd9e762aad");

	// Token: 0x04002E10 RID: 11792
	private static readonly AssetReference VO_ULDA_BOSS_41h_Female_Worgen_TurnOne_01 = new AssetReference("VO_ULDA_BOSS_41h_Female_Worgen_TurnOne_01.prefab:230edc73c690b4b42a34ad4583fce1fb");

	// Token: 0x04002E11 RID: 11793
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_HeroPower_01,
		ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_HeroPower_03,
		ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_HeroPower_04
	};

	// Token: 0x04002E12 RID: 11794
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_Idle_01,
		ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_Idle_02,
		ULDA_Dungeon_Boss_41h.VO_ULDA_BOSS_41h_Female_Worgen_Idle_03
	};

	// Token: 0x04002E13 RID: 11795
	private HashSet<string> m_playedLines = new HashSet<string>();
}
