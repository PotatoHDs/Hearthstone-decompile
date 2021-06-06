using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004BE RID: 1214
public class ULDA_Dungeon_Boss_66h : ULDA_Dungeon
{
	// Token: 0x0600411B RID: 16667 RVA: 0x0015B954 File Offset: 0x00159B54
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerBenevolentDjinnLowHealth_01,
			ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerLightningSpell_01,
			ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerPolymorph_01,
			ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_DeathALT_01,
			ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_DefeatPlayer_01,
			ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_EmoteResponse_01,
			ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_02,
			ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_03,
			ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_04,
			ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_05,
			ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_Idle1_01,
			ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_Idle2_02,
			ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_Idle3_03,
			ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_Idle4_04,
			ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_Intro_01,
			ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_IntroRenoResponse_01,
			ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_PlayerSiamat_01,
			ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_PlayerWishTreasure_01,
			ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_PlayerZephrys_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600411C RID: 16668 RVA: 0x0015BAE8 File Offset: 0x00159CE8
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x0600411D RID: 16669 RVA: 0x0015BAF0 File Offset: 0x00159CF0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_EmoteResponse_01;
	}

	// Token: 0x0600411E RID: 16670 RVA: 0x0015BB28 File Offset: 0x00159D28
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Reno")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_IntroRenoResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x0600411F RID: 16671 RVA: 0x0015BC02 File Offset: 0x00159E02
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
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_Idle1_01, 2.5f);
			break;
		case 102:
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_Idle2_02, 2.5f);
			break;
		case 103:
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_Idle3_03, 2.5f);
			break;
		case 104:
			yield return base.PlayBossLine(actor, ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_Idle4_04, 2.5f);
			break;
		case 105:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerBenevolentDjinnLowHealth_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06004120 RID: 16672 RVA: 0x0015BC18 File Offset: 0x00159E18
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
		if (!(cardId == "ULD_178"))
		{
			if (!(cardId == "LOOTA_814"))
			{
				if (cardId == "ULD_003")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_PlayerZephrys_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_PlayerWishTreasure_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_PlayerSiamat_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004121 RID: 16673 RVA: 0x0015BC2E File Offset: 0x00159E2E
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
		if (!(cardId == "EX1_251") && !(cardId == "CFM_707") && !(cardId == "EX1_238") && !(cardId == "EX1_259") && !(cardId == "OG_206"))
		{
			if (cardId == "CS2_022")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerPolymorph_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerLightningSpell_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002F8B RID: 12171
	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerBenevolentDjinnLowHealth_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerBenevolentDjinnLowHealth_01.prefab:341aacf7352bf024188831e4ab716f45");

	// Token: 0x04002F8C RID: 12172
	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerLightningSpell_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerLightningSpell_01.prefab:b7b7df2179a59b3418e88ba4e11acdb3");

	// Token: 0x04002F8D RID: 12173
	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerPolymorph_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_BossTriggerPolymorph_01.prefab:af97b4b2bc6b77f4eb3980fbac310e58");

	// Token: 0x04002F8E RID: 12174
	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_DeathALT_01.prefab:64a68f83742b9634bac6edc7854dbc6e");

	// Token: 0x04002F8F RID: 12175
	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_DefeatPlayer_01.prefab:3fbc7019ec1d7c843b5403a727edfd13");

	// Token: 0x04002F90 RID: 12176
	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_EmoteResponse_01.prefab:ac9e6e3eea022b44b902efd6d70ae1a8");

	// Token: 0x04002F91 RID: 12177
	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_02.prefab:fe361a03a2671384c9ebe4725f49ebca");

	// Token: 0x04002F92 RID: 12178
	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_03.prefab:7ff78a906890abc4d8cbf4ae191b2831");

	// Token: 0x04002F93 RID: 12179
	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_04.prefab:320c88ba420ade145a3be54220f9a62d");

	// Token: 0x04002F94 RID: 12180
	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_05.prefab:2767250aaae90114487ef3f34a240431");

	// Token: 0x04002F95 RID: 12181
	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_Idle1_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_Idle1_01.prefab:e6a8452fb2ca1fa4692bdeade843a628");

	// Token: 0x04002F96 RID: 12182
	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_Idle2_02 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_Idle2_02.prefab:fcded398091442c4ea930bc1bc016792");

	// Token: 0x04002F97 RID: 12183
	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_Idle3_03 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_Idle3_03.prefab:818c1915a901a5f43b5ffbe479f52757");

	// Token: 0x04002F98 RID: 12184
	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_Idle4_04 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_Idle4_04.prefab:23826da75864bb14c88332332926e3b0");

	// Token: 0x04002F99 RID: 12185
	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_Intro_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_Intro_01.prefab:3ba6f3d90baae2f4eb5a270f3414a1e8");

	// Token: 0x04002F9A RID: 12186
	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_IntroRenoResponse_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_IntroRenoResponse_01.prefab:0e3aa5202fdd39c44b168590f8813f6d");

	// Token: 0x04002F9B RID: 12187
	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_PlayerSiamat_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_PlayerSiamat_01.prefab:ecf4efdb0c1d6504da43ee67eaee854d");

	// Token: 0x04002F9C RID: 12188
	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_PlayerWishTreasure_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_PlayerWishTreasure_01.prefab:8c507084f7c11ef43969f0688e07bb05");

	// Token: 0x04002F9D RID: 12189
	private static readonly AssetReference VO_ULDA_BOSS_66h_Male_Djinn_PlayerZephrys_01 = new AssetReference("VO_ULDA_BOSS_66h_Male_Djinn_PlayerZephrys_01.prefab:685670079405c9347bc2e10681759c03");

	// Token: 0x04002F9E RID: 12190
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_02,
		ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_03,
		ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_04,
		ULDA_Dungeon_Boss_66h.VO_ULDA_BOSS_66h_Male_Djinn_HeroPower_05
	};

	// Token: 0x04002F9F RID: 12191
	private HashSet<string> m_playedLines = new HashSet<string>();
}
