using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000449 RID: 1097
public class DALA_Dungeon_Boss_28h : DALA_Dungeon
{
	// Token: 0x06003BA6 RID: 15270 RVA: 0x001361FC File Offset: 0x001343FC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_9Cost_TurnNine_01,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_Death_02,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_DefeatPlayer_01,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_EmoteResponse_01,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerBoss_01,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerBoss_02,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerBoss_03,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerPlayer_01,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerPlayer_02,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerPlayer_03,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_Idle_01,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_Idle_02,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_Idle_03,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_Idle_04,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_Idle_05,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_Intro_01,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_IntroHunter_01,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_IntroWarlock_01,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_IntroWarrior_01,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_PlayerCoin_01,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_PlayerDarkness_01,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_PlayerLegendaryWeapon_01,
			DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_PlayerLegendaryWeapon_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003BA7 RID: 15271 RVA: 0x001363D0 File Offset: 0x001345D0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_EmoteResponse_01;
	}

	// Token: 0x06003BA8 RID: 15272 RVA: 0x00136408 File Offset: 0x00134608
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_28h.m_IdleLines;
	}

	// Token: 0x06003BA9 RID: 15273 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003BAA RID: 15274 RVA: 0x00136410 File Offset: 0x00134610
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Barkeye")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_IntroHunter_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Chu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_IntroWarrior_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_Tekahn")
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

	// Token: 0x06003BAB RID: 15275 RVA: 0x00136536 File Offset: 0x00134736
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
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_28h.m_BossHeroPower);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_28h.m_PlayerHeroPower);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_28h.m_PlayerLegendaryWeapon);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_9Cost_TurnNine_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003BAC RID: 15276 RVA: 0x0013654C File Offset: 0x0013474C
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
		if (!(cardId == "GAME_005") && !(cardId == "GVG_028t"))
		{
			if (cardId == "LOOT_526")
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_PlayerDarkness_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_PlayerCoin_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003BAD RID: 15277 RVA: 0x00136562 File Offset: 0x00134762
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
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
		yield break;
	}

	// Token: 0x04002492 RID: 9362
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_9Cost_TurnNine_01 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_9Cost_TurnNine_01.prefab:851c0528f4a2c6f47b91d4b94179037e");

	// Token: 0x04002493 RID: 9363
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_Death_02 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_Death_02.prefab:cc321e4037a345b44b1ed5a3ddbeaac4");

	// Token: 0x04002494 RID: 9364
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_DefeatPlayer_01.prefab:1ae91868c5f54514f8cb5b16083989c4");

	// Token: 0x04002495 RID: 9365
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_EmoteResponse_01.prefab:450a954b9b14c6744b175e0da9c96de4");

	// Token: 0x04002496 RID: 9366
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerBoss_01 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerBoss_01.prefab:4ea6893c98e286b44abbdd7398dc0486");

	// Token: 0x04002497 RID: 9367
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerBoss_02 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerBoss_02.prefab:924346856bee558488e9c5a084ffa17f");

	// Token: 0x04002498 RID: 9368
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerBoss_03 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerBoss_03.prefab:8f8e49619f8511d4992c4af34f222c40");

	// Token: 0x04002499 RID: 9369
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerPlayer_01 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerPlayer_01.prefab:2efbabf2c8fa1d04bbb07f1cf2f7d34e");

	// Token: 0x0400249A RID: 9370
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerPlayer_02 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerPlayer_02.prefab:c03a5b79e1177094ab994e98a884271e");

	// Token: 0x0400249B RID: 9371
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerPlayer_03 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerPlayer_03.prefab:5c0c723ad23b03a48aefef87a0c57dc7");

	// Token: 0x0400249C RID: 9372
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_Idle_01 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_Idle_01.prefab:2bf8a022c0c3e594e97c2797d064fc5c");

	// Token: 0x0400249D RID: 9373
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_Idle_02 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_Idle_02.prefab:fd67534e228e2a64f9f217f1532e85be");

	// Token: 0x0400249E RID: 9374
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_Idle_03 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_Idle_03.prefab:031ff6b2cc3c6a241843821a65b1ba65");

	// Token: 0x0400249F RID: 9375
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_Idle_04 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_Idle_04.prefab:0bf28d1ace312ed4db0344766c0a0623");

	// Token: 0x040024A0 RID: 9376
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_Idle_05 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_Idle_05.prefab:9df996bba71610c4ebdf8bf6c7fe095a");

	// Token: 0x040024A1 RID: 9377
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_Intro_01 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_Intro_01.prefab:38266a546ec3f1f4486c0c960a427053");

	// Token: 0x040024A2 RID: 9378
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_IntroHunter_01 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_IntroHunter_01.prefab:21524542671d7aa49b2bacff7332c2b8");

	// Token: 0x040024A3 RID: 9379
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_IntroWarlock_01 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_IntroWarlock_01.prefab:5b5ce988cd6843a46a6910cde4f2fbd7");

	// Token: 0x040024A4 RID: 9380
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_IntroWarrior_01 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_IntroWarrior_01.prefab:dac080b958fab404fb6b13e668aaa270");

	// Token: 0x040024A5 RID: 9381
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_PlayerCoin_01 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_PlayerCoin_01.prefab:1c1a88eca2769f94a8e6965bf41ec3ee");

	// Token: 0x040024A6 RID: 9382
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_PlayerDarkness_01 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_PlayerDarkness_01.prefab:c5d15d3a07fdbc744a96a8d1d6999f4e");

	// Token: 0x040024A7 RID: 9383
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_PlayerLegendaryWeapon_01 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_PlayerLegendaryWeapon_01.prefab:3d8d86f7a80cbed4191e4a799362a605");

	// Token: 0x040024A8 RID: 9384
	private static readonly AssetReference VO_DALA_BOSS_28h_Male_Ethereal_PlayerLegendaryWeapon_02 = new AssetReference("VO_DALA_BOSS_28h_Male_Ethereal_PlayerLegendaryWeapon_02.prefab:edae63901b06d8d45a039239ec757d0a");

	// Token: 0x040024A9 RID: 9385
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_Idle_01,
		DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_Idle_02,
		DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_Idle_03,
		DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_Idle_04,
		DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_Idle_05
	};

	// Token: 0x040024AA RID: 9386
	private static List<string> m_PlayerLegendaryWeapon = new List<string>
	{
		DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_PlayerLegendaryWeapon_01,
		DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_PlayerLegendaryWeapon_02
	};

	// Token: 0x040024AB RID: 9387
	private static List<string> m_PlayerHeroPower = new List<string>
	{
		DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerPlayer_01,
		DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerPlayer_02,
		DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerPlayer_03
	};

	// Token: 0x040024AC RID: 9388
	private static List<string> m_BossHeroPower = new List<string>
	{
		DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerBoss_01,
		DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerBoss_02,
		DALA_Dungeon_Boss_28h.VO_DALA_BOSS_28h_Male_Ethereal_HeroPowerBoss_03
	};

	// Token: 0x040024AD RID: 9389
	private HashSet<string> m_playedLines = new HashSet<string>();
}
