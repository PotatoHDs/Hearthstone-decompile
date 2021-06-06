using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000445 RID: 1093
public class DALA_Dungeon_Boss_24h : DALA_Dungeon
{
	// Token: 0x06003B76 RID: 15222 RVA: 0x00134E28 File Offset: 0x00133028
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_01,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_02,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_03,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_Death_01,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_DefeatPlayer_01,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_EmoteResponse_01,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_HeroPower_01,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_HeroPower_02,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_HeroPower_03,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_Idle_01,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_Idle_02,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_Idle_03,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_Intro_01,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_IntroOlBarkeye_01,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_01,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_02,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_03,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_PlayerBrannBronzebeard_01,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_PlayerMurmuringElemental_01,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_PlayerShudderwock_01,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_PlayerSpiritOfTheShark_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003B77 RID: 15223 RVA: 0x00134FDC File Offset: 0x001331DC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_EmoteResponse_01;
	}

	// Token: 0x06003B78 RID: 15224 RVA: 0x00135014 File Offset: 0x00133214
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_24h.m_IdleLines;
	}

	// Token: 0x06003B79 RID: 15225 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003B7A RID: 15226 RVA: 0x0013501B File Offset: 0x0013321B
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_HeroPower_01,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_HeroPower_02,
			DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_HeroPower_03
		};
	}

	// Token: 0x06003B7B RID: 15227 RVA: 0x00135054 File Offset: 0x00133254
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Barkeye")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_IntroOlBarkeye_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_Squeamlish")
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

	// Token: 0x06003B7C RID: 15228 RVA: 0x0013513B File Offset: 0x0013333B
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
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_24h.m_PlayerBattlecry);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_24h.m_BossBigBattlecry);
		}
		yield break;
	}

	// Token: 0x06003B7D RID: 15229 RVA: 0x00135151 File Offset: 0x00133351
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
		if (!(cardId == "LOE_077"))
		{
			if (!(cardId == "LOOT_517"))
			{
				if (!(cardId == "GIL_820"))
				{
					if (cardId == "TRL_092")
					{
						yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_PlayerSpiritOfTheShark_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_PlayerShudderwock_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_PlayerMurmuringElemental_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_PlayerBrannBronzebeard_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003B7E RID: 15230 RVA: 0x00135167 File Offset: 0x00133367
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

	// Token: 0x04002430 RID: 9264
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_01.prefab:6a36eda969cd3f946b16d482b56dedbb");

	// Token: 0x04002431 RID: 9265
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_02 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_02.prefab:8a37aadc5c09c3c4f87885dab006c346");

	// Token: 0x04002432 RID: 9266
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_03 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_03.prefab:777eaafbaf4236e4fb47ead1b3dee92c");

	// Token: 0x04002433 RID: 9267
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_Death_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_Death_01.prefab:a0b167627eb5e7d419cc50e9dde15d3a");

	// Token: 0x04002434 RID: 9268
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_DefeatPlayer_01.prefab:b8f211e0524d8b945a64119cb46048c6");

	// Token: 0x04002435 RID: 9269
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_EmoteResponse_01.prefab:be047876ab90c24408f98f40a744ede3");

	// Token: 0x04002436 RID: 9270
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_HeroPower_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_HeroPower_01.prefab:93bd22a5b6e244f438184d26b617fae9");

	// Token: 0x04002437 RID: 9271
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_HeroPower_02 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_HeroPower_02.prefab:67c61953593b7d64fb28fba6ce2421ca");

	// Token: 0x04002438 RID: 9272
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_HeroPower_03 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_HeroPower_03.prefab:0ec1760c04951d545976746bdac7f41f");

	// Token: 0x04002439 RID: 9273
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_Idle_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_Idle_01.prefab:425a17cd7acc87747926e7b37d1b59fc");

	// Token: 0x0400243A RID: 9274
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_Idle_02 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_Idle_02.prefab:b2558d35a58b1dd46a67f968d831a745");

	// Token: 0x0400243B RID: 9275
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_Idle_03 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_Idle_03.prefab:1a48d1ce431298b41b993c64429dc5ef");

	// Token: 0x0400243C RID: 9276
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_Intro_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_Intro_01.prefab:d6cb4d700bfe51243a551a17d841a27d");

	// Token: 0x0400243D RID: 9277
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_IntroOlBarkeye_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_IntroOlBarkeye_01.prefab:750b3ff8c9c1a464fbea74c3c851d9f1");

	// Token: 0x0400243E RID: 9278
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_01.prefab:1289e53685264e34ca71d05443a4abf6");

	// Token: 0x0400243F RID: 9279
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_02 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_02.prefab:99d444d737ee78d40a93b0ad421acfeb");

	// Token: 0x04002440 RID: 9280
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_03 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_03.prefab:8f4cfcce5522d794e9469d37d593497d");

	// Token: 0x04002441 RID: 9281
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_PlayerBrannBronzebeard_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_PlayerBrannBronzebeard_01.prefab:a8cc0a1dfc75f6c4d9da1ac3b80fcb51");

	// Token: 0x04002442 RID: 9282
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_PlayerMurmuringElemental_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_PlayerMurmuringElemental_01.prefab:9887498ffd95d0440b8ed19d498334a9");

	// Token: 0x04002443 RID: 9283
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_PlayerShudderwock_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_PlayerShudderwock_01.prefab:a5ba7e14a1247fd48a61e17f65a5bde0");

	// Token: 0x04002444 RID: 9284
	private static readonly AssetReference VO_DALA_BOSS_24h_Female_Orc_PlayerSpiritOfTheShark_01 = new AssetReference("VO_DALA_BOSS_24h_Female_Orc_PlayerSpiritOfTheShark_01.prefab:89c35834e23b0f244b246ebb57d8ff90");

	// Token: 0x04002445 RID: 9285
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_Idle_01,
		DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_Idle_02,
		DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_Idle_03
	};

	// Token: 0x04002446 RID: 9286
	private static List<string> m_BossBigBattlecry = new List<string>
	{
		DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_01,
		DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_02,
		DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_BossBigBattlecry_03
	};

	// Token: 0x04002447 RID: 9287
	private static List<string> m_PlayerBattlecry = new List<string>
	{
		DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_01,
		DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_02,
		DALA_Dungeon_Boss_24h.VO_DALA_BOSS_24h_Female_Orc_PlayerBattlecry_03
	};

	// Token: 0x04002448 RID: 9288
	private HashSet<string> m_playedLines = new HashSet<string>();
}
