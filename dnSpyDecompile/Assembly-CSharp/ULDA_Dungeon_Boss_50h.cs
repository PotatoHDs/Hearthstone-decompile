using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004AF RID: 1199
public class ULDA_Dungeon_Boss_50h : ULDA_Dungeon
{
	// Token: 0x0600407E RID: 16510 RVA: 0x001582C8 File Offset: 0x001564C8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_BossHyenaAlpha_01,
			ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_BossKoboldSandtrooper_01,
			ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_BossMarkedShot_01,
			ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_DeathALT_01,
			ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_DefeatPlayer_01,
			ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_EmoteResponse_01,
			ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_01,
			ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_02,
			ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_03,
			ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_04,
			ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_05,
			ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_Idle_01,
			ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_Idle_02,
			ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_Idle_03,
			ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_Intro_01,
			ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_IntroBrann_01,
			ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_IntroFinley_01,
			ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_PlayerBlunderbussTreasure_01,
			ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_PlayerSwarmofLocusts_01,
			ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_PlayerUntamedBeastmaster_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600407F RID: 16511 RVA: 0x0015846C File Offset: 0x0015666C
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06004080 RID: 16512 RVA: 0x00158474 File Offset: 0x00156674
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_EmoteResponse_01;
	}

	// Token: 0x06004081 RID: 16513 RVA: 0x001584AC File Offset: 0x001566AC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Brann")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_IntroBrann_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "ULDA_Finley")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_IntroFinley_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06004082 RID: 16514 RVA: 0x001585C5 File Offset: 0x001567C5
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerLines);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06004083 RID: 16515 RVA: 0x001585DB File Offset: 0x001567DB
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
		if (!(cardId == "ULDA_401"))
		{
			if (!(cardId == "ULD_713"))
			{
				if (cardId == "TRL_405")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_PlayerUntamedBeastmaster_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_PlayerSwarmofLocusts_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_PlayerBlunderbussTreasure_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004084 RID: 16516 RVA: 0x001585F1 File Offset: 0x001567F1
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
		if (!(cardId == "ULD_154"))
		{
			if (!(cardId == "ULD_184"))
			{
				if (cardId == "DAL_371")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_BossMarkedShot_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_BossKoboldSandtrooper_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_BossHyenaAlpha_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002E90 RID: 11920
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_BossHyenaAlpha_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_BossHyenaAlpha_01.prefab:cf30112d4814caf43a0b382397f28fd7");

	// Token: 0x04002E91 RID: 11921
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_BossKoboldSandtrooper_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_BossKoboldSandtrooper_01.prefab:709a5199fa8579a4c95acfc00b4a4f38");

	// Token: 0x04002E92 RID: 11922
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_BossMarkedShot_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_BossMarkedShot_01.prefab:b3a000ae6f5784a4ca32df9b966aff01");

	// Token: 0x04002E93 RID: 11923
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_DeathALT_01.prefab:8d07bcbe136dc76468943479e8c9cc00");

	// Token: 0x04002E94 RID: 11924
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_DefeatPlayer_01.prefab:47ee05a5ccb26cb44985a261a88e34f6");

	// Token: 0x04002E95 RID: 11925
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_EmoteResponse_01.prefab:1b453b9dc0b9088469267002499654a8");

	// Token: 0x04002E96 RID: 11926
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_01.prefab:0179726d05f3fdf43a0ac711d17eb756");

	// Token: 0x04002E97 RID: 11927
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_02.prefab:d1e0cffd867979e41af4962c08bd8d61");

	// Token: 0x04002E98 RID: 11928
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_03.prefab:6641bf43c63d3414cbc04751abcbbf13");

	// Token: 0x04002E99 RID: 11929
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_04.prefab:6b0869d7cd1846f4cbb875f2e0422553");

	// Token: 0x04002E9A RID: 11930
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_05.prefab:20197153fb4e7b34bb54f8186cedbed9");

	// Token: 0x04002E9B RID: 11931
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_Idle_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_Idle_01.prefab:cceebdf2900f871498e93a209485865b");

	// Token: 0x04002E9C RID: 11932
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_Idle_02 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_Idle_02.prefab:e212f90192f2eb349b2da0d0c0202888");

	// Token: 0x04002E9D RID: 11933
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_Idle_03 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_Idle_03.prefab:46c802af5c80a8d4e85b0c5c8adcec31");

	// Token: 0x04002E9E RID: 11934
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_Intro_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_Intro_01.prefab:f2574e701b4bf29428035e8200398500");

	// Token: 0x04002E9F RID: 11935
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_IntroBrann_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_IntroBrann_01.prefab:47009177b2f223943a96e9ece415a063");

	// Token: 0x04002EA0 RID: 11936
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_IntroFinley_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_IntroFinley_01.prefab:7a69e245b94c09542b44d9d1aea884e3");

	// Token: 0x04002EA1 RID: 11937
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_PlayerBlunderbussTreasure_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_PlayerBlunderbussTreasure_01.prefab:70670ae1a682d5d448b93e26974153a5");

	// Token: 0x04002EA2 RID: 11938
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_PlayerSwarmofLocusts_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_PlayerSwarmofLocusts_01.prefab:bb405a98bf5a4834fb17c51385d15a85");

	// Token: 0x04002EA3 RID: 11939
	private static readonly AssetReference VO_ULDA_BOSS_50h_Male_Gnoll_PlayerUntamedBeastmaster_01 = new AssetReference("VO_ULDA_BOSS_50h_Male_Gnoll_PlayerUntamedBeastmaster_01.prefab:80c19b52f5284854190add70b9ba2f85");

	// Token: 0x04002EA4 RID: 11940
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_01,
		ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_02,
		ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_03,
		ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_04,
		ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_HeroPower_05
	};

	// Token: 0x04002EA5 RID: 11941
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_Idle_01,
		ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_Idle_02,
		ULDA_Dungeon_Boss_50h.VO_ULDA_BOSS_50h_Male_Gnoll_Idle_03
	};

	// Token: 0x04002EA6 RID: 11942
	private HashSet<string> m_playedLines = new HashSet<string>();
}
