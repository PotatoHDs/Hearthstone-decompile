using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000484 RID: 1156
public class ULDA_Dungeon_Boss_07h : ULDA_Dungeon
{
	// Token: 0x06003E87 RID: 16007 RVA: 0x0014BA74 File Offset: 0x00149C74
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_BossAttackHero_01,
			ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_BossAttackMinionKill_01,
			ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_BossBEES_01,
			ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_BossBiteSwipe_01,
			ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_Death_01,
			ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_DefeatPlayer_01,
			ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_EmoteResponse_01,
			ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_HeroPower_01,
			ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_HeroPower_02,
			ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_HeroPower_03,
			ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_HeroPower_04,
			ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_Idle_01,
			ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_Idle_02,
			ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_Idle_03,
			ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_Intro_01,
			ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_IntroElise_01,
			ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_PlayerAcornbearer_01,
			ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_PlayerForestsAid_PlayerForceofNature_01,
			ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_PlayerSpreadingPlague_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003E88 RID: 16008 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003E89 RID: 16009 RVA: 0x0014BC08 File Offset: 0x00149E08
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06003E8A RID: 16010 RVA: 0x0014BC10 File Offset: 0x00149E10
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06003E8B RID: 16011 RVA: 0x0014BC18 File Offset: 0x00149E18
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_EmoteResponse_01;
	}

	// Token: 0x06003E8C RID: 16012 RVA: 0x0014BC50 File Offset: 0x00149E50
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Elise")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_IntroElise_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "ULDA_Finley")
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

	// Token: 0x06003E8D RID: 16013 RVA: 0x0014BD37 File Offset: 0x00149F37
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
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_BossAttackMinionKill_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_BossAttackHero_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003E8E RID: 16014 RVA: 0x0014BD4D File Offset: 0x00149F4D
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
		if (!(cardId == "DAL_354"))
		{
			if (!(cardId == "ICC_054"))
			{
				if (cardId == "EX1_571" || cardId == "DAL_256" || cardId == "DAL_256ts")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_PlayerForestsAid_PlayerForceofNature_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_PlayerSpreadingPlague_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_PlayerAcornbearer_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003E8F RID: 16015 RVA: 0x0014BD63 File Offset: 0x00149F63
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
		if (!(cardId == "ULD_134"))
		{
			if (cardId == "CS2_012")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_BossBiteSwipe_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_BossBEES_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002AE6 RID: 10982
	private static readonly AssetReference VO_ULDA_BOSS_07h_Female_Kobold_BossAttackHero_01 = new AssetReference("VO_ULDA_BOSS_07h_Female_Kobold_BossAttackHero_01.prefab:a03258f014f319e4b8bd1a4fae9a4ea9");

	// Token: 0x04002AE7 RID: 10983
	private static readonly AssetReference VO_ULDA_BOSS_07h_Female_Kobold_BossAttackMinionKill_01 = new AssetReference("VO_ULDA_BOSS_07h_Female_Kobold_BossAttackMinionKill_01.prefab:33b9167dd5a37aa4697f74823060060c");

	// Token: 0x04002AE8 RID: 10984
	private static readonly AssetReference VO_ULDA_BOSS_07h_Female_Kobold_BossBEES_01 = new AssetReference("VO_ULDA_BOSS_07h_Female_Kobold_BossBEES_01.prefab:40db176871369ba4496b075af53cb2a7");

	// Token: 0x04002AE9 RID: 10985
	private static readonly AssetReference VO_ULDA_BOSS_07h_Female_Kobold_BossBiteSwipe_01 = new AssetReference("VO_ULDA_BOSS_07h_Female_Kobold_BossBiteSwipe_01.prefab:383c445752f316f45b13f8fc6021a6b4");

	// Token: 0x04002AEA RID: 10986
	private static readonly AssetReference VO_ULDA_BOSS_07h_Female_Kobold_Death_01 = new AssetReference("VO_ULDA_BOSS_07h_Female_Kobold_Death_01.prefab:18e20bb4303325c4c9fd9ec696f48539");

	// Token: 0x04002AEB RID: 10987
	private static readonly AssetReference VO_ULDA_BOSS_07h_Female_Kobold_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_07h_Female_Kobold_DefeatPlayer_01.prefab:231e2cd36d4a518458e793acabd56aee");

	// Token: 0x04002AEC RID: 10988
	private static readonly AssetReference VO_ULDA_BOSS_07h_Female_Kobold_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_07h_Female_Kobold_EmoteResponse_01.prefab:5b25a7c56a0aa1c4d914a3494ab8af10");

	// Token: 0x04002AED RID: 10989
	private static readonly AssetReference VO_ULDA_BOSS_07h_Female_Kobold_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_07h_Female_Kobold_HeroPower_01.prefab:4cda19fa34a7d06449425b5253a1d98e");

	// Token: 0x04002AEE RID: 10990
	private static readonly AssetReference VO_ULDA_BOSS_07h_Female_Kobold_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_07h_Female_Kobold_HeroPower_02.prefab:d755eb8db32782547bb0a2a74d3b57ba");

	// Token: 0x04002AEF RID: 10991
	private static readonly AssetReference VO_ULDA_BOSS_07h_Female_Kobold_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_07h_Female_Kobold_HeroPower_03.prefab:ac4212552f4f91a46b070a8f64fc3fa2");

	// Token: 0x04002AF0 RID: 10992
	private static readonly AssetReference VO_ULDA_BOSS_07h_Female_Kobold_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_07h_Female_Kobold_HeroPower_04.prefab:eb7a5b3975ecc6f428efaac41e6d6eae");

	// Token: 0x04002AF1 RID: 10993
	private static readonly AssetReference VO_ULDA_BOSS_07h_Female_Kobold_Idle_01 = new AssetReference("VO_ULDA_BOSS_07h_Female_Kobold_Idle_01.prefab:f791cf60a565f374783cb778490d3c15");

	// Token: 0x04002AF2 RID: 10994
	private static readonly AssetReference VO_ULDA_BOSS_07h_Female_Kobold_Idle_02 = new AssetReference("VO_ULDA_BOSS_07h_Female_Kobold_Idle_02.prefab:cb88a77feccd7ad4ca3e3d73c855ade8");

	// Token: 0x04002AF3 RID: 10995
	private static readonly AssetReference VO_ULDA_BOSS_07h_Female_Kobold_Idle_03 = new AssetReference("VO_ULDA_BOSS_07h_Female_Kobold_Idle_03.prefab:180628446bd285a4a952b14433feb90d");

	// Token: 0x04002AF4 RID: 10996
	private static readonly AssetReference VO_ULDA_BOSS_07h_Female_Kobold_Intro_01 = new AssetReference("VO_ULDA_BOSS_07h_Female_Kobold_Intro_01.prefab:4382039cf465d1747adfc522fdfcb53e");

	// Token: 0x04002AF5 RID: 10997
	private static readonly AssetReference VO_ULDA_BOSS_07h_Female_Kobold_IntroElise_01 = new AssetReference("VO_ULDA_BOSS_07h_Female_Kobold_IntroElise_01.prefab:7872ed7eeedeafe4ba3592411c7f9f68");

	// Token: 0x04002AF6 RID: 10998
	private static readonly AssetReference VO_ULDA_BOSS_07h_Female_Kobold_PlayerAcornbearer_01 = new AssetReference("VO_ULDA_BOSS_07h_Female_Kobold_PlayerAcornbearer_01.prefab:f569727d8074eac48a5502eb82f0e13f");

	// Token: 0x04002AF7 RID: 10999
	private static readonly AssetReference VO_ULDA_BOSS_07h_Female_Kobold_PlayerForestsAid_PlayerForceofNature_01 = new AssetReference("VO_ULDA_BOSS_07h_Female_Kobold_PlayerForestsAid_PlayerForceofNature_01.prefab:e1f23943c129e054d8e173a0a4ddb1ac");

	// Token: 0x04002AF8 RID: 11000
	private static readonly AssetReference VO_ULDA_BOSS_07h_Female_Kobold_PlayerSpreadingPlague_01 = new AssetReference("VO_ULDA_BOSS_07h_Female_Kobold_PlayerSpreadingPlague_01.prefab:1bf691f99e4400d4d9881e1102b70346");

	// Token: 0x04002AF9 RID: 11001
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_HeroPower_01,
		ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_HeroPower_02,
		ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_HeroPower_03,
		ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_HeroPower_04
	};

	// Token: 0x04002AFA RID: 11002
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_Idle_01,
		ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_Idle_02,
		ULDA_Dungeon_Boss_07h.VO_ULDA_BOSS_07h_Female_Kobold_Idle_03
	};

	// Token: 0x04002AFB RID: 11003
	private HashSet<string> m_playedLines = new HashSet<string>();
}
