using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000443 RID: 1091
public class DALA_Dungeon_Boss_22h : DALA_Dungeon
{
	// Token: 0x06003B5E RID: 15198 RVA: 0x001345DC File Offset: 0x001327DC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_BossDalaranLibrarian_02,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_BossSpell_01,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_BossSpell_02,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_Death_02,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_DefeatPlayer_01,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_EmoteResponse_01,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_HeroPower_01,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_HeroPower_02,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_HeroPower_03,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_HeroPower_04,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_Idle_01,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_Idle_02,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_Idle_03,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_Intro_01,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_IntroChu_02,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_IntroGeorge_01,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_IntroOlBarkeye_01,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_IntroRakanishu_01,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_IntroTekahn_01,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_PlayerBabblingBook_01,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_PlayerBattlecry_01,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_PlayerBattlecry_02,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_PlayerDalaranLibrarian_01,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_PlayerLegendary_01,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_PlayerLegendary_02,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_PlayerSilence_01,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_PlayerSilence_02,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_PlayerSilence_03
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003B5F RID: 15199 RVA: 0x00134800 File Offset: 0x00132A00
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_EmoteResponse_01;
	}

	// Token: 0x06003B60 RID: 15200 RVA: 0x00134838 File Offset: 0x00132A38
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_22h.m_IdleLines;
	}

	// Token: 0x06003B61 RID: 15201 RVA: 0x00134840 File Offset: 0x00132A40
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_HeroPower_01,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_HeroPower_02,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_HeroPower_03,
			DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_HeroPower_04
		};
	}

	// Token: 0x06003B62 RID: 15202 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003B63 RID: 15203 RVA: 0x00134894 File Offset: 0x00132A94
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Tekahn" && cardId != "DALA_Chu" && cardId != "DALA_Rakanishu" && cardId != "DALA_George" && cardId != "DALA_Barkeye")
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

	// Token: 0x06003B64 RID: 15204 RVA: 0x00134973 File Offset: 0x00132B73
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
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_22h.m_BossSpell);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_22h.m_PlayerBattleCry);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_22h.m_PlayerLegendary);
			break;
		case 104:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_22h.m_PlayerSilence);
			break;
		case 105:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_22h.m_HeroPowerTrigger);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003B65 RID: 15205 RVA: 0x00134989 File Offset: 0x00132B89
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
		if (!(cardId == "KAR_009"))
		{
			if (cardId == "DAL_735")
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_PlayerDalaranLibrarian_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_PlayerBabblingBook_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003B66 RID: 15206 RVA: 0x0013499F File Offset: 0x00132B9F
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
		if (cardId == "DAL_735")
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_BossDalaranLibrarian_02, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002408 RID: 9224
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_BossDalaranLibrarian_02 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_BossDalaranLibrarian_02.prefab:4e21f6e0fcb282545beb8896bd3428f0");

	// Token: 0x04002409 RID: 9225
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_BossSpell_01 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_BossSpell_01.prefab:3bade612bce93ff46806244b194fef80");

	// Token: 0x0400240A RID: 9226
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_BossSpell_02 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_BossSpell_02.prefab:e59f7c0e26b793e4c95c80a3904109e4");

	// Token: 0x0400240B RID: 9227
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_Death_02 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_Death_02.prefab:3d660e550aed7494c83c88b1201fa942");

	// Token: 0x0400240C RID: 9228
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_DefeatPlayer_01.prefab:32cbf6e0b63e4b4469756b1d1f647055");

	// Token: 0x0400240D RID: 9229
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_EmoteResponse_01.prefab:ceb932d66ad5d7243b9f8e984a1d9d99");

	// Token: 0x0400240E RID: 9230
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_HeroPower_01 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_HeroPower_01.prefab:079b6a94bbd59074798c8ffe5a0e79c9");

	// Token: 0x0400240F RID: 9231
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_HeroPower_02 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_HeroPower_02.prefab:428b17ba535d00241b124855acb17ced");

	// Token: 0x04002410 RID: 9232
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_HeroPower_03 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_HeroPower_03.prefab:b3f2e317a37bed746ba463948a2facdc");

	// Token: 0x04002411 RID: 9233
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_HeroPower_04 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_HeroPower_04.prefab:1028805b1d59c134782dd98186af0bba");

	// Token: 0x04002412 RID: 9234
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_Idle_01 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_Idle_01.prefab:558feecaaee57f642974936442a345e8");

	// Token: 0x04002413 RID: 9235
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_Idle_02 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_Idle_02.prefab:ffa907bf45be4f143af37e649776fcb4");

	// Token: 0x04002414 RID: 9236
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_Idle_03 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_Idle_03.prefab:3d8cf1fddd6e5674d86dc14e552bd95e");

	// Token: 0x04002415 RID: 9237
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_Intro_01 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_Intro_01.prefab:590f9e4748fd3164dbec90ca7b95701e");

	// Token: 0x04002416 RID: 9238
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_IntroChu_02 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_IntroChu_02.prefab:8c8247d303b4e13489f739a098cb43a8");

	// Token: 0x04002417 RID: 9239
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_IntroGeorge_01.prefab:b8a522ccaf6fa54459a3635790c5d028");

	// Token: 0x04002418 RID: 9240
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_IntroOlBarkeye_01 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_IntroOlBarkeye_01.prefab:05b6c32beb5249a4cac586832fea9118");

	// Token: 0x04002419 RID: 9241
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_IntroRakanishu_01 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_IntroRakanishu_01.prefab:d223e3790be9da847a3f3bc41d85716f");

	// Token: 0x0400241A RID: 9242
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_IntroTekahn_01 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_IntroTekahn_01.prefab:d54ca44fa2b84e84c8c87e0b5dba9c4f");

	// Token: 0x0400241B RID: 9243
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_PlayerBabblingBook_01 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_PlayerBabblingBook_01.prefab:e4fdcc886031cbe4c8b1c99eba888423");

	// Token: 0x0400241C RID: 9244
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_PlayerBattlecry_01 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_PlayerBattlecry_01.prefab:f9ae1c2749c5e4645a1c8a0f8ffd73c3");

	// Token: 0x0400241D RID: 9245
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_PlayerBattlecry_02 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_PlayerBattlecry_02.prefab:fbbeace78e271a04ab6fb411b1efdb67");

	// Token: 0x0400241E RID: 9246
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_PlayerDalaranLibrarian_01 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_PlayerDalaranLibrarian_01.prefab:3ba64882148a65d41a8b71b95e6d2c77");

	// Token: 0x0400241F RID: 9247
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_PlayerLegendary_01 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_PlayerLegendary_01.prefab:1a09311bfabae134c99a279f2aa472ec");

	// Token: 0x04002420 RID: 9248
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_PlayerLegendary_02 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_PlayerLegendary_02.prefab:4f0eec7a7bdcfa44c9c4ab09181c4a58");

	// Token: 0x04002421 RID: 9249
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_PlayerSilence_01 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_PlayerSilence_01.prefab:8d287d8dc77a45c429b8f992c7a3100b");

	// Token: 0x04002422 RID: 9250
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_PlayerSilence_02 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_PlayerSilence_02.prefab:4a7af8985582e504f92664f963698a41");

	// Token: 0x04002423 RID: 9251
	private static readonly AssetReference VO_DALA_BOSS_22h_Female_Pandaren_PlayerSilence_03 = new AssetReference("VO_DALA_BOSS_22h_Female_Pandaren_PlayerSilence_03.prefab:b9c7385116fc65d4088ec5335d048801");

	// Token: 0x04002424 RID: 9252
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_Idle_01,
		DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_Idle_02,
		DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_Idle_03
	};

	// Token: 0x04002425 RID: 9253
	private static List<string> m_PlayerSilence = new List<string>
	{
		DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_PlayerSilence_01,
		DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_PlayerSilence_02,
		DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_PlayerSilence_03
	};

	// Token: 0x04002426 RID: 9254
	private static List<string> m_HeroPowerTrigger = new List<string>
	{
		DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_HeroPower_01,
		DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_HeroPower_02,
		DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_HeroPower_03,
		DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_HeroPower_04
	};

	// Token: 0x04002427 RID: 9255
	private static List<string> m_PlayerBattleCry = new List<string>
	{
		DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_PlayerBattlecry_01,
		DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_PlayerBattlecry_02
	};

	// Token: 0x04002428 RID: 9256
	private static List<string> m_PlayerLegendary = new List<string>
	{
		DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_PlayerLegendary_01,
		DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_PlayerLegendary_02
	};

	// Token: 0x04002429 RID: 9257
	private static List<string> m_BossSpell = new List<string>
	{
		DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_BossSpell_01,
		DALA_Dungeon_Boss_22h.VO_DALA_BOSS_22h_Female_Pandaren_BossSpell_02
	};

	// Token: 0x0400242A RID: 9258
	private HashSet<string> m_playedLines = new HashSet<string>();
}
