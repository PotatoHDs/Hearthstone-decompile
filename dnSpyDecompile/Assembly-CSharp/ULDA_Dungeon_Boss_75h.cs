using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004C7 RID: 1223
public class ULDA_Dungeon_Boss_75h : ULDA_Dungeon
{
	// Token: 0x06004189 RID: 16777 RVA: 0x0015E7E8 File Offset: 0x0015C9E8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerDesertSpear_01,
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerHuntersPack_01,
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerKillCommand_01,
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerWastelandScorpid_01,
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_Death_01,
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_DefeatPlayer_01,
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_EmoteResponse_01,
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_02,
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_03,
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_04,
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_05,
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_01,
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_02,
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_03,
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_Intro_01,
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Brann_01,
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Elise_01,
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Finley_01,
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Reno_01,
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_PlayerTrigger_Desert_Hare_01,
			ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_PlayerTrigger_Oasis_Surger_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600418A RID: 16778 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600418B RID: 16779 RVA: 0x0015E99C File Offset: 0x0015CB9C
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x0600418C RID: 16780 RVA: 0x0015E9A4 File Offset: 0x0015CBA4
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x0600418D RID: 16781 RVA: 0x0015E9AC File Offset: 0x0015CBAC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_EmoteResponse_01;
	}

	// Token: 0x0600418E RID: 16782 RVA: 0x0015E9E4 File Offset: 0x0015CBE4
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x0600418F RID: 16783 RVA: 0x0015E9FC File Offset: 0x0015CBFC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Reno")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Reno_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "ULDA_Elise")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Elise_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "ULDA_Brann")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Brann_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "ULDA_Finley")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Finley_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06004190 RID: 16784 RVA: 0x0015EB62 File Offset: 0x0015CD62
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
		if (!(cardId == "ULD_719"))
		{
			if (cardId == "ULD_292")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_PlayerTrigger_Oasis_Surger_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_PlayerTrigger_Desert_Hare_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004191 RID: 16785 RVA: 0x0015EB78 File Offset: 0x0015CD78
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
		if (!(cardId == "ULD_430"))
		{
			if (!(cardId == "EX1_539"))
			{
				if (!(cardId == "ULD_194"))
				{
					if (cardId == "ULD_429")
					{
						yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerHuntersPack_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerWastelandScorpid_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerKillCommand_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerDesertSpear_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0400307B RID: 12411
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerDesertSpear_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerDesertSpear_01.prefab:491b1d1633064b44ba4a511090155e3c");

	// Token: 0x0400307C RID: 12412
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerHuntersPack_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerHuntersPack_01.prefab:e94e4e7b982144240a94256d1cbdd21b");

	// Token: 0x0400307D RID: 12413
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerKillCommand_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerKillCommand_01.prefab:1c6773a7874e4ae42b6bf1c6f4e9656d");

	// Token: 0x0400307E RID: 12414
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerWastelandScorpid_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_BossTriggerWastelandScorpid_01.prefab:340a64243a4a72449b0bc9b0107322a1");

	// Token: 0x0400307F RID: 12415
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_Death_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_Death_01.prefab:f3a81f48488ba174eada659e4628135a");

	// Token: 0x04003080 RID: 12416
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_DefeatPlayer_01.prefab:c19e57d305dbfbd4d9ba2f2441cb69f7");

	// Token: 0x04003081 RID: 12417
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_EmoteResponse_01.prefab:7255cc75cecbad84298ad2ce7c71461a");

	// Token: 0x04003082 RID: 12418
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_02.prefab:55497f08bbd61ef4fb8874afc8c6973d");

	// Token: 0x04003083 RID: 12419
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_03.prefab:42c848f589a15a842b9d3fc6b2cc59e6");

	// Token: 0x04003084 RID: 12420
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_04.prefab:2883497e2dbda1e46bf8559e033287d8");

	// Token: 0x04003085 RID: 12421
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_05.prefab:a4c2c8264e48f8d44833d3be8f5d0f03");

	// Token: 0x04003086 RID: 12422
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_01.prefab:27304034fdcc65346a666ac4e6a1a289");

	// Token: 0x04003087 RID: 12423
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_02 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_02.prefab:53c305e0e1365c847a3c3691e726d5fd");

	// Token: 0x04003088 RID: 12424
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_03 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_03.prefab:c1223879320e4544cbb13ecc654b3b3a");

	// Token: 0x04003089 RID: 12425
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_Intro_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_Intro_01.prefab:28766bacda9490448aa113df3a8296ff");

	// Token: 0x0400308A RID: 12426
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Brann_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Brann_01.prefab:682d0bd60e694854b9e4b039ebb11899");

	// Token: 0x0400308B RID: 12427
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Elise_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Elise_01.prefab:17d50b6331e346d46abae7fe2cb7722a");

	// Token: 0x0400308C RID: 12428
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Finley_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Finley_01.prefab:d620619241697704bbd4a68f16121449");

	// Token: 0x0400308D RID: 12429
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Reno_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_IntroSpecial_Reno_01.prefab:3d2cff3f5aa90564f926633b8c81fa11");

	// Token: 0x0400308E RID: 12430
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_PlayerTrigger_Desert_Hare_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_PlayerTrigger_Desert_Hare_01.prefab:5f0e9bb4dacd41947b09e5a048309e7c");

	// Token: 0x0400308F RID: 12431
	private static readonly AssetReference VO_ULDA_BOSS_75h_Male_NefersetTolvir_PlayerTrigger_Oasis_Surger_01 = new AssetReference("VO_ULDA_BOSS_75h_Male_NefersetTolvir_PlayerTrigger_Oasis_Surger_01.prefab:bee07d18a9f80584abe5e5529dd4e006");

	// Token: 0x04003090 RID: 12432
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_02,
		ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_03,
		ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_04,
		ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_HeroPower_05
	};

	// Token: 0x04003091 RID: 12433
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_01,
		ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_02,
		ULDA_Dungeon_Boss_75h.VO_ULDA_BOSS_75h_Male_NefersetTolvir_Idle_03
	};

	// Token: 0x04003092 RID: 12434
	private HashSet<string> m_playedLines = new HashSet<string>();
}
