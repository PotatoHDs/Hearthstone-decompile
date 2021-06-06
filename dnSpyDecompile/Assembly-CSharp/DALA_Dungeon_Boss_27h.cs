using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000448 RID: 1096
public class DALA_Dungeon_Boss_27h : DALA_Dungeon
{
	// Token: 0x06003B9A RID: 15258 RVA: 0x00135C2C File Offset: 0x00133E2C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_BossCopy_01,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_BossSteal_01,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_Death_01,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_DefeatPlayer_01,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_DemonDies_01,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_EmoteResponse_01,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_Exposition_01,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_01,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_02,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_03,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerLarge_01,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerLarge_02,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerMisc_01,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerSmall_01,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerSmall_02,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_Idle_01,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_Idle_02,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_Idle_03,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_Intro_01,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_IntroRakanishu_01,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_Misc_01,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_PlayerSelfDamage_01,
			DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_PlayerTreant_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003B9B RID: 15259 RVA: 0x00135E00 File Offset: 0x00134000
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_EmoteResponse_01;
	}

	// Token: 0x06003B9C RID: 15260 RVA: 0x00135E38 File Offset: 0x00134038
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_27h.m_IdleLines;
	}

	// Token: 0x06003B9D RID: 15261 RVA: 0x00135E40 File Offset: 0x00134040
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Rakanishu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_IntroRakanishu_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_Tekahn" && cardId != "DALA_Eudora" && cardId != "DALA_Chu")
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

	// Token: 0x06003B9E RID: 15262 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003B9F RID: 15263 RVA: 0x00135F44 File Offset: 0x00134144
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
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_27h.m_HeroPowerSmall);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_27h.m_HeroPower);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_27h.m_HeroPowerLarge);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_Exposition_01, 2.5f);
			break;
		case 105:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_PlayerSelfDamage_01, 2.5f);
			break;
		case 106:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_DemonDies_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003BA0 RID: 15264 RVA: 0x00135F5A File Offset: 0x0013415A
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
		if (cardId == "GIL_663t" || cardId == "FP1_019t" || cardId == "EX1_158t" || cardId == "DAL_256t2")
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_PlayerTreant_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003BA1 RID: 15265 RVA: 0x00135F70 File Offset: 0x00134170
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		if (!(cardId == "AT_033"))
		{
			if (cardId == "CS1_113")
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_BossSteal_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_BossCopy_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002476 RID: 9334
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_BossCopy_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_BossCopy_01.prefab:ae4d7d24b0d31cd4fa42a9152a036633");

	// Token: 0x04002477 RID: 9335
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_BossSteal_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_BossSteal_01.prefab:51efdc6a588e31e45ae65109fbb3a04f");

	// Token: 0x04002478 RID: 9336
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_Death_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_Death_01.prefab:1eca16b5c40208e46818c432959ed733");

	// Token: 0x04002479 RID: 9337
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_DefeatPlayer_01.prefab:c7a226840a233054d9610e3f4830a73e");

	// Token: 0x0400247A RID: 9338
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_DemonDies_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_DemonDies_01.prefab:b702f176cc486b3419048a416e01f919");

	// Token: 0x0400247B RID: 9339
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_EmoteResponse_01.prefab:aef7c50073de7e14680a7a553e3eb809");

	// Token: 0x0400247C RID: 9340
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_Exposition_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_Exposition_01.prefab:04094e304090c4347b42036c5958930b");

	// Token: 0x0400247D RID: 9341
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_01.prefab:3f8b7755d548e5446a44cb3e2f08c74f");

	// Token: 0x0400247E RID: 9342
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_02 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_02.prefab:aecd8b3b89fce5847ac7b7790420dfce");

	// Token: 0x0400247F RID: 9343
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_03 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_03.prefab:06e3896d1910e0d4c859dbab4d1f5234");

	// Token: 0x04002480 RID: 9344
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerLarge_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerLarge_01.prefab:c1db41f51500618498f81fa653ce764f");

	// Token: 0x04002481 RID: 9345
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerLarge_02 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerLarge_02.prefab:2d8dab1eb474c45429e70b50780c604e");

	// Token: 0x04002482 RID: 9346
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerMisc_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerMisc_01.prefab:1d1f7fc4f05687a409a71fb0c0717156");

	// Token: 0x04002483 RID: 9347
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerSmall_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerSmall_01.prefab:a26ef345b077852448e25711e2cebf75");

	// Token: 0x04002484 RID: 9348
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerSmall_02 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerSmall_02.prefab:20d6a03e0e4b64347a40babd869a6a22");

	// Token: 0x04002485 RID: 9349
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_Idle_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_Idle_01.prefab:932cb54619b385f43a26c946efc6b1e5");

	// Token: 0x04002486 RID: 9350
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_Idle_02 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_Idle_02.prefab:810832fb9e7259a4aaa85428cb01acb1");

	// Token: 0x04002487 RID: 9351
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_Idle_03 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_Idle_03.prefab:aa462d99f9421e1459102d9b1c18c6d9");

	// Token: 0x04002488 RID: 9352
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_Intro_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_Intro_01.prefab:df7ab420a70f3dc4ea6c66d4b5c028ab");

	// Token: 0x04002489 RID: 9353
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_IntroRakanishu_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_IntroRakanishu_01.prefab:2fc172ee9e2c8484c990f0c9287f388e");

	// Token: 0x0400248A RID: 9354
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_Misc_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_Misc_01.prefab:cf2f70db09005814ebd425414e58984a");

	// Token: 0x0400248B RID: 9355
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_PlayerSelfDamage_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_PlayerSelfDamage_01.prefab:b4ae5f0cd3ef43447b8724ad3df8d155");

	// Token: 0x0400248C RID: 9356
	private static readonly AssetReference VO_DALA_BOSS_27h_Male_BloodElf_PlayerTreant_01 = new AssetReference("VO_DALA_BOSS_27h_Male_BloodElf_PlayerTreant_01.prefab:29eb9af2b7c78f743880581f0e5014f0");

	// Token: 0x0400248D RID: 9357
	private static List<string> m_HeroPowerSmall = new List<string>
	{
		DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerSmall_01,
		DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerSmall_02,
		DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_Misc_01
	};

	// Token: 0x0400248E RID: 9358
	private static List<string> m_HeroPowerLarge = new List<string>
	{
		DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerLarge_01,
		DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_HeroPowerLarge_02,
		DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_Misc_01
	};

	// Token: 0x0400248F RID: 9359
	private static List<string> m_HeroPower = new List<string>
	{
		DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_01,
		DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_02,
		DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_HeroPower_03,
		DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_Misc_01
	};

	// Token: 0x04002490 RID: 9360
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_Idle_01,
		DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_Idle_02,
		DALA_Dungeon_Boss_27h.VO_DALA_BOSS_27h_Male_BloodElf_Idle_03
	};

	// Token: 0x04002491 RID: 9361
	private HashSet<string> m_playedLines = new HashSet<string>();
}
