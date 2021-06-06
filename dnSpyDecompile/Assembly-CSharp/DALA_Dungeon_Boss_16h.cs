using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200043D RID: 1085
public class DALA_Dungeon_Boss_16h : DALA_Dungeon
{
	// Token: 0x06003B13 RID: 15123 RVA: 0x00132840 File Offset: 0x00130A40
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_BossDerrangedDoctor_01,
			DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_BossMojomasterZihi_01,
			DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_BossPotionVendor_01,
			DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_Death_02,
			DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_DefeatPlayer_01,
			DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_EmoteResponse_01,
			DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_HeroPower_01,
			DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_HeroPower_02,
			DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_HeroPower_03,
			DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_HeroPower_04,
			DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_HeroPowerSwapBack_01,
			DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_Idle_01,
			DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_Idle_03,
			DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_Idle_04,
			DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_Idle_06,
			DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_Intro_01,
			DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_IntroGeorge_01,
			DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_PlayerCrazedChemist_01,
			DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_PlayerMistressOfMixtures_01,
			DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_PlayerTreasurePotion_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003B14 RID: 15124 RVA: 0x001329E4 File Offset: 0x00130BE4
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_16h.m_IdleLines;
	}

	// Token: 0x06003B15 RID: 15125 RVA: 0x001329EB File Offset: 0x00130BEB
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_EmoteResponse_01;
	}

	// Token: 0x06003B16 RID: 15126 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003B17 RID: 15127 RVA: 0x00132A24 File Offset: 0x00130C24
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003B18 RID: 15128 RVA: 0x00132B0B File Offset: 0x00130D0B
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
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_HeroPowerSwapBack_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_16h.m_HeroPower);
		}
		yield break;
	}

	// Token: 0x06003B19 RID: 15129 RVA: 0x00132B21 File Offset: 0x00130D21
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
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 514586147U)
		{
			if (num != 248079585U)
			{
				if (num != 496822790U)
				{
					if (num != 514586147U)
					{
						goto IL_2B4;
					}
					if (!(cardId == "CFM_603"))
					{
						goto IL_2B4;
					}
				}
				else if (!(cardId == "CFM_662"))
				{
					goto IL_2B4;
				}
			}
			else if (!(cardId == "CFM_094"))
			{
				goto IL_2B4;
			}
		}
		else if (num <= 2379572673U)
		{
			if (num != 665584718U)
			{
				if (num != 2379572673U)
				{
					goto IL_2B4;
				}
				if (!(cardId == "CFM_065"))
				{
					goto IL_2B4;
				}
			}
			else if (!(cardId == "CFM_608"))
			{
				goto IL_2B4;
			}
		}
		else if (num != 2871062571U)
		{
			if (num != 4016873029U)
			{
				goto IL_2B4;
			}
			if (!(cardId == "CFM_120"))
			{
				goto IL_2B4;
			}
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_PlayerMistressOfMixtures_01, 2.5f);
			goto IL_2B4;
		}
		else
		{
			if (!(cardId == "BOT_576"))
			{
				goto IL_2B4;
			}
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_PlayerCrazedChemist_01, 2.5f);
			goto IL_2B4;
		}
		yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_PlayerTreasurePotion_02, 2.5f);
		IL_2B4:
		yield break;
	}

	// Token: 0x06003B1A RID: 15130 RVA: 0x00132B37 File Offset: 0x00130D37
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
		if (!(cardId == "GIL_118"))
		{
			if (!(cardId == "TRL_564"))
			{
				if (cardId == "DAL_544")
				{
					yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_BossPotionVendor_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_BossMojomasterZihi_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_BossDerrangedDoctor_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0400237C RID: 9084
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_BossDerrangedDoctor_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_BossDerrangedDoctor_01.prefab:e0f023472381f8c4d8734d97f1f465e1");

	// Token: 0x0400237D RID: 9085
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_BossMojomasterZihi_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_BossMojomasterZihi_01.prefab:01988e435a7b0a444b4339ae53b916c5");

	// Token: 0x0400237E RID: 9086
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_BossPotionVendor_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_BossPotionVendor_01.prefab:8f69bb92b2ece5744b87b0485c2ab6c9");

	// Token: 0x0400237F RID: 9087
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_Death_02 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_Death_02.prefab:e0bca3927c63efb468e86c3e3faec774");

	// Token: 0x04002380 RID: 9088
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_DefeatPlayer_01.prefab:71344262b29cbd24a8a5fe86825341b8");

	// Token: 0x04002381 RID: 9089
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_EmoteResponse_01.prefab:b9bdda4595d682c49a02d998664388f3");

	// Token: 0x04002382 RID: 9090
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_HeroPower_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_HeroPower_01.prefab:44f7e83f440796741b1c26adb962297c");

	// Token: 0x04002383 RID: 9091
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_HeroPower_02 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_HeroPower_02.prefab:c603b57656551d748a94cd5e838b4d91");

	// Token: 0x04002384 RID: 9092
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_HeroPower_03 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_HeroPower_03.prefab:0443ed55dcfad16429dd315074bae5d3");

	// Token: 0x04002385 RID: 9093
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_HeroPower_04 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_HeroPower_04.prefab:799eff561b0cd014a9f6f250dab1d5b6");

	// Token: 0x04002386 RID: 9094
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_HeroPowerSwapBack_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_HeroPowerSwapBack_01.prefab:3f321ebe40544ef4f8607e252de4df0e");

	// Token: 0x04002387 RID: 9095
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_Idle_01.prefab:c0cb64a69ce3d0741a56661738e59c32");

	// Token: 0x04002388 RID: 9096
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_Idle_03.prefab:dd9d835453ad8e548b004902191958da");

	// Token: 0x04002389 RID: 9097
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_Idle_04 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_Idle_04.prefab:671c4ec59be451c41890e9756ca5e37f");

	// Token: 0x0400238A RID: 9098
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_Idle_06 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_Idle_06.prefab:575e3b92c81024f45a3d24be773d183a");

	// Token: 0x0400238B RID: 9099
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_Intro_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_Intro_01.prefab:5539f00129dec4246a5d309dd53f3472");

	// Token: 0x0400238C RID: 9100
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_IntroGeorge_01.prefab:889864329637ec34daf0317c9c528c07");

	// Token: 0x0400238D RID: 9101
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_PlayerCrazedChemist_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_PlayerCrazedChemist_01.prefab:34ff3a32b8b9a1442ac76bd2b2ca77d6");

	// Token: 0x0400238E RID: 9102
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_PlayerMistressOfMixtures_01 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_PlayerMistressOfMixtures_01.prefab:6491b9d206f7868498df3d4628a70d54");

	// Token: 0x0400238F RID: 9103
	private static readonly AssetReference VO_DALA_BOSS_16h_Female_Human_PlayerTreasurePotion_02 = new AssetReference("VO_DALA_BOSS_16h_Female_Human_PlayerTreasurePotion_02.prefab:df38533ffaaa22646a2d34854ff6edae");

	// Token: 0x04002390 RID: 9104
	private static List<string> m_HeroPower = new List<string>
	{
		DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_HeroPower_01,
		DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_HeroPower_02,
		DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_HeroPower_03,
		DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_HeroPower_04
	};

	// Token: 0x04002391 RID: 9105
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_Idle_01,
		DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_Idle_03,
		DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_Idle_04,
		DALA_Dungeon_Boss_16h.VO_DALA_BOSS_16h_Female_Human_Idle_06
	};

	// Token: 0x04002392 RID: 9106
	private HashSet<string> m_playedLines = new HashSet<string>();
}
