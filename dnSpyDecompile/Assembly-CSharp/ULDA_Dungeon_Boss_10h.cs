using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000487 RID: 1159
public class ULDA_Dungeon_Boss_10h : ULDA_Dungeon
{
	// Token: 0x06003EA6 RID: 16038 RVA: 0x0014C43C File Offset: 0x0014A63C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_BossAcademicEspionage_01,
			ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_BossCleverDisguise_01,
			ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_BossPilfer_01,
			ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_DeathALT_01,
			ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_DefeatPlayer_01,
			ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_EmoteResponse_01,
			ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_HeroPower_01,
			ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_HeroPower_02,
			ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_HeroPower_03,
			ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_HeroPower_04,
			ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_HeroPower_05,
			ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_Idle_01,
			ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_Idle_02,
			ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_Idle_03,
			ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_Intro_01,
			ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_IntroElise_01,
			ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_IntroFinley_01,
			ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_PlayerEVILRecruiter_01,
			ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_PlayerHyenaAlpha_01,
			ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_PlayerSackofLamps_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003EA7 RID: 16039 RVA: 0x0014C5E0 File Offset: 0x0014A7E0
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003EA8 RID: 16040 RVA: 0x0014C5E8 File Offset: 0x0014A7E8
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06003EA9 RID: 16041 RVA: 0x0014C5F0 File Offset: 0x0014A7F0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_EmoteResponse_01;
	}

	// Token: 0x06003EAA RID: 16042 RVA: 0x0014C628 File Offset: 0x0014A828
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Elise")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_IntroElise_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "ULDA_Finley")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_IntroFinley_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003EAB RID: 16043 RVA: 0x0014C74E File Offset: 0x0014A94E
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06003EAC RID: 16044 RVA: 0x0014C764 File Offset: 0x0014A964
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
		if (!(cardId == "ULD_162"))
		{
			if (!(cardId == "ULD_154"))
			{
				if (cardId == "ULDA_040")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_PlayerSackofLamps_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_PlayerHyenaAlpha_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_PlayerEVILRecruiter_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003EAD RID: 16045 RVA: 0x0014C77A File Offset: 0x0014A97A
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
		if (!(cardId == "BOT_087"))
		{
			if (!(cardId == "ULD_328"))
			{
				if (cardId == "EX1_182")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_BossPilfer_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_BossCleverDisguise_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_BossAcademicEspionage_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002B12 RID: 11026
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_BossAcademicEspionage_01 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_BossAcademicEspionage_01.prefab:3eebb80606f52284a86b3dce9d5ec5bf");

	// Token: 0x04002B13 RID: 11027
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_BossCleverDisguise_01 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_BossCleverDisguise_01.prefab:f011e9085bae8184e9982faa617f6774");

	// Token: 0x04002B14 RID: 11028
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_BossPilfer_01 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_BossPilfer_01.prefab:f59853bffd2f57144924e29b11e71f5a");

	// Token: 0x04002B15 RID: 11029
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_DeathALT_01.prefab:39b91cc8dfd2f354f9b066e27c9f402d");

	// Token: 0x04002B16 RID: 11030
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_DefeatPlayer_01.prefab:a8eff39bd9feb0947bd2fc78373b363e");

	// Token: 0x04002B17 RID: 11031
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_EmoteResponse_01.prefab:602ab4a5c17f7044b864939949421e10");

	// Token: 0x04002B18 RID: 11032
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_HeroPower_01.prefab:07f72ecf6ace34a4f9fef973ba1f6fc6");

	// Token: 0x04002B19 RID: 11033
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_HeroPower_02.prefab:e0e7062cebe3fe9478a8c9ec7eee9c4a");

	// Token: 0x04002B1A RID: 11034
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_HeroPower_03.prefab:47f442e454443214a8c2c99c4d845bde");

	// Token: 0x04002B1B RID: 11035
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_HeroPower_04.prefab:04b6820f614bd894fa206681160ac409");

	// Token: 0x04002B1C RID: 11036
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_HeroPower_05.prefab:8e5ef34df5b3e1245840396f17eac880");

	// Token: 0x04002B1D RID: 11037
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_Idle_01 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_Idle_01.prefab:13dc47e2dd1943c4b8f65aa23979e17f");

	// Token: 0x04002B1E RID: 11038
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_Idle_02 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_Idle_02.prefab:e7f1fc9d5aeded44289e7d3c1a050cfa");

	// Token: 0x04002B1F RID: 11039
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_Idle_03 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_Idle_03.prefab:ede54ce4c96f67542a3ba31b0339468a");

	// Token: 0x04002B20 RID: 11040
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_Intro_01 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_Intro_01.prefab:adbea0c8ff17d69488f53c0f11e65df0");

	// Token: 0x04002B21 RID: 11041
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_IntroElise_01 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_IntroElise_01.prefab:2c723f8cea4717f4abf9143c628e989a");

	// Token: 0x04002B22 RID: 11042
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_IntroFinley_01 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_IntroFinley_01.prefab:48f59995ac2eb24449c9762d3c03a37f");

	// Token: 0x04002B23 RID: 11043
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_PlayerEVILRecruiter_01 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_PlayerEVILRecruiter_01.prefab:208faf9a40d42854c9dac0aa27f881f8");

	// Token: 0x04002B24 RID: 11044
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_PlayerHyenaAlpha_01 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_PlayerHyenaAlpha_01.prefab:0ba0ae053c833754bb2ef3bd42d839d8");

	// Token: 0x04002B25 RID: 11045
	private static readonly AssetReference VO_ULDA_BOSS_10h_Male_Sethrak_PlayerSackofLamps_01 = new AssetReference("VO_ULDA_BOSS_10h_Male_Sethrak_PlayerSackofLamps_01.prefab:66f57f09cfd1e29488556480c921f283");

	// Token: 0x04002B26 RID: 11046
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_HeroPower_01,
		ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_HeroPower_02,
		ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_HeroPower_03,
		ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_HeroPower_04,
		ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_HeroPower_05
	};

	// Token: 0x04002B27 RID: 11047
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_Idle_01,
		ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_Idle_02,
		ULDA_Dungeon_Boss_10h.VO_ULDA_BOSS_10h_Male_Sethrak_Idle_03
	};

	// Token: 0x04002B28 RID: 11048
	private HashSet<string> m_playedLines = new HashSet<string>();
}
