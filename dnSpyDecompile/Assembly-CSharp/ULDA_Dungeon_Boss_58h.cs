using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004B7 RID: 1207
public class ULDA_Dungeon_Boss_58h : ULDA_Dungeon
{
	// Token: 0x060040D1 RID: 16593 RVA: 0x00159E20 File Offset: 0x00158020
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerLivingMonument_01,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerPsychopomp_01,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerPuzzleboxofYoggSaron_01,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_DeathALT_01,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_DefeatPlayer_01,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_EmoteResponse_01,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_01,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_02,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_03,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_04,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_05,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_01,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_02,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_03,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_04,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_Idle_01,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_Idle_02,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_Intro_01,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_IntroBrannResponse_01,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_PlayerHigherLearning_01,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerDiscover_01,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerReadingHieroglyphics_01,
			ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerSecret_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060040D2 RID: 16594 RVA: 0x00159FF4 File Offset: 0x001581F4
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x060040D3 RID: 16595 RVA: 0x00159FFC File Offset: 0x001581FC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_DeathALT_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_EmoteResponse_01;
	}

	// Token: 0x060040D4 RID: 16596 RVA: 0x0015A034 File Offset: 0x00158234
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Elise")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060040D5 RID: 16597 RVA: 0x0015A0DF File Offset: 0x001582DF
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
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerTriggerCorrectLines);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerTriggerIncorrectLines);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerSecret_01, 2.5f);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerDiscover_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x060040D6 RID: 16598 RVA: 0x0015A0F5 File Offset: 0x001582F5
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
		if (cardId == "ULD_155")
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerReadingHieroglyphics_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060040D7 RID: 16599 RVA: 0x0015A10B File Offset: 0x0015830B
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
		if (!(cardId == "ULD_193"))
		{
			if (!(cardId == "ULD_268"))
			{
				if (cardId == "ULD_216")
				{
					yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerPuzzleboxofYoggSaron_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerPsychopomp_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerLivingMonument_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002F0A RID: 12042
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerLivingMonument_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerLivingMonument_01.prefab:10027f560e4353f4d8aaf3dd6a18898a");

	// Token: 0x04002F0B RID: 12043
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerPsychopomp_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerPsychopomp_01.prefab:4f84aad90382bfe429a92583cb3f5c19");

	// Token: 0x04002F0C RID: 12044
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerPuzzleboxofYoggSaron_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_BossTriggerPuzzleboxofYoggSaron_01.prefab:c94aa2c293a98cd4ba52096c408b075f");

	// Token: 0x04002F0D RID: 12045
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_DeathALT_01.prefab:bf8def8371d6dc947b9b0f84f9941525");

	// Token: 0x04002F0E RID: 12046
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_DefeatPlayer_01.prefab:6286d90ec77d5d54ea36d17dc176d52d");

	// Token: 0x04002F0F RID: 12047
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_EmoteResponse_01.prefab:dfdff85207b88de4db6e4d7f5f991a1a");

	// Token: 0x04002F10 RID: 12048
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_01.prefab:0ff622d4b56a962408408e8ff8ad68e9");

	// Token: 0x04002F11 RID: 12049
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_02 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_02.prefab:8f117d7ee1ea7634db39d813a4d5c7ee");

	// Token: 0x04002F12 RID: 12050
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_03 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_03.prefab:7e3d3d5ac718833458d094e2beae6957");

	// Token: 0x04002F13 RID: 12051
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_04 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_04.prefab:3e9c1f2ec72adee45865d6bf9884dcee");

	// Token: 0x04002F14 RID: 12052
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_05 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_05.prefab:637507084eebf5b4fba0aaa4a0734fb5");

	// Token: 0x04002F15 RID: 12053
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_01.prefab:3b9ffa981367b8c42a0b1ef4434f7337");

	// Token: 0x04002F16 RID: 12054
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_02 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_02.prefab:ab100961a96613d41a3607c52c769993");

	// Token: 0x04002F17 RID: 12055
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_03 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_03.prefab:fb933ab3036871e4481670605b9b578e");

	// Token: 0x04002F18 RID: 12056
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_04 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_04.prefab:a285c3db5303db948977a28bf209c635");

	// Token: 0x04002F19 RID: 12057
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_Idle_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_Idle_01.prefab:748ded937e27a1d409ba2e9e45977ae8");

	// Token: 0x04002F1A RID: 12058
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_Idle_02 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_Idle_02.prefab:a651e54904ed20740bce4c0c9dd9c9cd");

	// Token: 0x04002F1B RID: 12059
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_Intro_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_Intro_01.prefab:839d799f179f6ca4ca49bd94098a4b19");

	// Token: 0x04002F1C RID: 12060
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_IntroBrannResponse_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_IntroBrannResponse_01.prefab:c069f2b8d672ae94d8181c1995f29976");

	// Token: 0x04002F1D RID: 12061
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_PlayerHigherLearning_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_PlayerHigherLearning_01.prefab:98aa17f8ca94965479d3d74122811e46");

	// Token: 0x04002F1E RID: 12062
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerDiscover_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerDiscover_01.prefab:f3f68e8cdd765814e880ede7384e813f");

	// Token: 0x04002F1F RID: 12063
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerReadingHieroglyphics_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerReadingHieroglyphics_01.prefab:15685b183c5435b4285acd10ac9cab6b");

	// Token: 0x04002F20 RID: 12064
	private static readonly AssetReference VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerSecret_01 = new AssetReference("VO_ULDA_BOSS_58h_Male_Tolvir_PlayerTriggerSecret_01.prefab:308c8b0f75b035f4fae01435d7a9f924");

	// Token: 0x04002F21 RID: 12065
	private List<string> m_HeroPowerTriggerCorrectLines = new List<string>
	{
		ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_01,
		ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_02,
		ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_03,
		ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_04,
		ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerCorrect_05
	};

	// Token: 0x04002F22 RID: 12066
	private List<string> m_HeroPowerTriggerIncorrectLines = new List<string>
	{
		ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_01,
		ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_02,
		ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_03,
		ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_HeroPowerTriggerIncorrect_04
	};

	// Token: 0x04002F23 RID: 12067
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_Idle_01,
		ULDA_Dungeon_Boss_58h.VO_ULDA_BOSS_58h_Male_Tolvir_Idle_02
	};

	// Token: 0x04002F24 RID: 12068
	private HashSet<string> m_playedLines = new HashSet<string>();
}
