using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200046E RID: 1134
public class DALA_Dungeon_Boss_65h : DALA_Dungeon
{
	// Token: 0x06003D76 RID: 15734 RVA: 0x0014296C File Offset: 0x00140B6C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_BossEyeforanEye_01,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_BossHumility_01,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_BossLayOnHands_01,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_Death_02,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_DefeatPlayer_01,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_EmoteResponse_01,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_HeroPowerStopDeath_01,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_HeroPowerStopDeath_02,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_01,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_02,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_03,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_04,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_05,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_06,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_Idle_01,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_Idle_02,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_Idle_03,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_Intro_01,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_IntroEduora_01,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_IntroRakanishu_01,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_IntroSqueamlish_01,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_IntroTekahn_01,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_PlayerAlexstraza_01,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_01,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_02,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_03,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_PlayerOrbOfUntold_01,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_PlayerPlatedScarabDeathrattle_01,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_Misc_01,
			DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_Misc_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003D77 RID: 15735 RVA: 0x00142BB0 File Offset: 0x00140DB0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_EmoteResponse_01;
	}

	// Token: 0x06003D78 RID: 15736 RVA: 0x00142BE8 File Offset: 0x00140DE8
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_65h.m_IdleLines;
	}

	// Token: 0x06003D79 RID: 15737 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003D7A RID: 15738 RVA: 0x00142BF0 File Offset: 0x00140DF0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_DALA_Eudora")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_IntroEduora_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Rakanishu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_IntroRakanishu_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Squeamlish")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_IntroSqueamlish_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Tekahn")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_IntroTekahn_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId != "DALA_George" && cardId != "DALA_Chu")
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

	// Token: 0x06003D7B RID: 15739 RVA: 0x00142D9B File Offset: 0x00140F9B
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
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_65h.m_HeroPowerStopDeathLines);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_65h.m_HeroPowerTriggerLines);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_65h.m_PlayerGainArmorLines);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_PlayerAlexstraza_01, 2.5f);
			break;
		case 105:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_BossEyeforanEye_01, 2.5f);
			break;
		case 106:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_PlayerPlatedScarabDeathrattle_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003D7C RID: 15740 RVA: 0x00142DB1 File Offset: 0x00140FB1
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
		if (cardId == "DALA_712")
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_PlayerOrbOfUntold_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003D7D RID: 15741 RVA: 0x00142DC7 File Offset: 0x00140FC7
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "EX1_360"))
		{
			if (cardId == "EX1_354")
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_BossLayOnHands_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_BossHumility_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002866 RID: 10342
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_BossEyeforanEye_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_BossEyeforanEye_01.prefab:390e94c8e77ba9e4b9a347022a05a4db");

	// Token: 0x04002867 RID: 10343
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_BossHumility_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_BossHumility_01.prefab:671e50b1ce7674245beb986394fcc7dd");

	// Token: 0x04002868 RID: 10344
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_BossLayOnHands_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_BossLayOnHands_01.prefab:f461373f7b2adb04eaa37f90b9fe5729");

	// Token: 0x04002869 RID: 10345
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_Death_02 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_Death_02.prefab:5cf11e0a01c3f7e4b9dde240712415ec");

	// Token: 0x0400286A RID: 10346
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_DefeatPlayer_01.prefab:b67c48e548e24fa488a019cac4cf2dd1");

	// Token: 0x0400286B RID: 10347
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_EmoteResponse_01.prefab:3207f5882ae2c04469f29e3eea82da3d");

	// Token: 0x0400286C RID: 10348
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_HeroPowerStopDeath_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_HeroPowerStopDeath_01.prefab:c8930a487a536b544a0193601bccc199");

	// Token: 0x0400286D RID: 10349
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_HeroPowerStopDeath_02 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_HeroPowerStopDeath_02.prefab:2dcd16fd807729246911e911a40b49e2");

	// Token: 0x0400286E RID: 10350
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_01.prefab:70bff1535f9660f4eac0cffb62eec3cf");

	// Token: 0x0400286F RID: 10351
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_02 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_02.prefab:466206bb35d9a0e47a53868a9aa87009");

	// Token: 0x04002870 RID: 10352
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_03 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_03.prefab:39241a52aab0f9847ad4c98a2d303fb5");

	// Token: 0x04002871 RID: 10353
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_04 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_04.prefab:25906b21edd629b4ca2bc664447b8f15");

	// Token: 0x04002872 RID: 10354
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_05 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_05.prefab:e10fd41592c9c4c46897d5b668800419");

	// Token: 0x04002873 RID: 10355
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_06 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_06.prefab:7225083100d01b14684dcdd4108096f1");

	// Token: 0x04002874 RID: 10356
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_Idle_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_Idle_01.prefab:892e0a34f193fc646bd2a1bf97e91341");

	// Token: 0x04002875 RID: 10357
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_Idle_02 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_Idle_02.prefab:c252747806b75be4d9adf150ccba7561");

	// Token: 0x04002876 RID: 10358
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_Idle_03 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_Idle_03.prefab:c9daeb2708ce90a4987c7f91d7ebcf18");

	// Token: 0x04002877 RID: 10359
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_Intro_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_Intro_01.prefab:28e77ca8c33599547bf4597f3bd3fc91");

	// Token: 0x04002878 RID: 10360
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_IntroEduora_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_IntroEduora_01.prefab:1d02cd8a6b1b5fa4f8ff885e7a88110c");

	// Token: 0x04002879 RID: 10361
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_IntroRakanishu_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_IntroRakanishu_01.prefab:98cd56bee6a90fe46b7a0453cf76e2bc");

	// Token: 0x0400287A RID: 10362
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_IntroSqueamlish_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_IntroSqueamlish_01.prefab:153550a6d611e2a4ca6297c64b2b46c1");

	// Token: 0x0400287B RID: 10363
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_IntroTekahn_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_IntroTekahn_01.prefab:e960efa22176f0a4c88338b130a3132b");

	// Token: 0x0400287C RID: 10364
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_PlayerAlexstraza_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_PlayerAlexstraza_01.prefab:ba59d91762f89df43adcda7f91a07895");

	// Token: 0x0400287D RID: 10365
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_01.prefab:7400a91dd5fba7d40889d911652b9c34");

	// Token: 0x0400287E RID: 10366
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_02 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_02.prefab:bf1ea20e369dcd84fb91d3ffdf5e71f1");

	// Token: 0x0400287F RID: 10367
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_03 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_03.prefab:f0558e96733ebef4abdff3597d47f074");

	// Token: 0x04002880 RID: 10368
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_PlayerOrbOfUntold_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_PlayerOrbOfUntold_01.prefab:c5bfe1b284f452c41bcc45ba83c33e3e");

	// Token: 0x04002881 RID: 10369
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_PlayerPlatedScarabDeathrattle_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_PlayerPlatedScarabDeathrattle_01.prefab:207432fab37d49844bfb37bcf91798f7");

	// Token: 0x04002882 RID: 10370
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_Misc_01 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_Misc_01.prefab:4ce55a0f7c35aaf45b2f07311c29d975");

	// Token: 0x04002883 RID: 10371
	private static readonly AssetReference VO_DALA_BOSS_65h_Male_HighElf_Misc_02 = new AssetReference("VO_DALA_BOSS_65h_Male_HighElf_Misc_02.prefab:96a103f242de05d4d93b5d08ed5cddc4");

	// Token: 0x04002884 RID: 10372
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_Idle_01,
		DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_Idle_02,
		DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_Idle_03,
		DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_Misc_01
	};

	// Token: 0x04002885 RID: 10373
	private static List<string> m_HeroPowerStopDeathLines = new List<string>
	{
		DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_HeroPowerStopDeath_01,
		DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_HeroPowerStopDeath_02
	};

	// Token: 0x04002886 RID: 10374
	private static List<string> m_HeroPowerTriggerLines = new List<string>
	{
		DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_01,
		DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_02,
		DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_03,
		DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_04,
		DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_05,
		DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_HeroPowerTrigger_06,
		DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_Misc_02
	};

	// Token: 0x04002887 RID: 10375
	private static List<string> m_PlayerGainArmorLines = new List<string>
	{
		DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_01,
		DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_02,
		DALA_Dungeon_Boss_65h.VO_DALA_BOSS_65h_Male_HighElf_PlayerGainArmor_03
	};

	// Token: 0x04002888 RID: 10376
	private HashSet<string> m_playedLines = new HashSet<string>();
}
