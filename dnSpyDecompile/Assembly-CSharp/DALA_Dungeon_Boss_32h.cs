using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200044D RID: 1101
public class DALA_Dungeon_Boss_32h : DALA_Dungeon
{
	// Token: 0x06003BD7 RID: 15319 RVA: 0x001374EC File Offset: 0x001356EC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_BossAutoBarber_01,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_Death_02,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_DefeatPlayer_01,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_EmoteResponse_01,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPower_01,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPower_02,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPower_03,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPower_04,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPowerFade_01,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPowerFade_02,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPowerMohawk_01,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPowerMohawk_02,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPowerPompadour_01,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPowerPompadour_02,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPowerQuiff_01,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPowerQuiff_02,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPowerTwoheads_01,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_Idle_01,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_Idle_02,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_Idle_03,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_Intro_01,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_IntroEudora_01,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_IntroRakanishu_01,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_Misc_01,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_PlayerBloodRazor_01,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_PlayerSprint_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003BD8 RID: 15320 RVA: 0x001376F0 File Offset: 0x001358F0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_EmoteResponse_01;
	}

	// Token: 0x06003BD9 RID: 15321 RVA: 0x00137728 File Offset: 0x00135928
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPower_01,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPower_02,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPower_03,
			DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPower_04
		};
	}

	// Token: 0x06003BDA RID: 15322 RVA: 0x0013777A File Offset: 0x0013597A
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_32h.m_IdleLines;
	}

	// Token: 0x06003BDB RID: 15323 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003BDC RID: 15324 RVA: 0x00137784 File Offset: 0x00135984
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_Eudora")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_IntroEudora_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Rakanishu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_IntroRakanishu_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003BDD RID: 15325 RVA: 0x001378AA File Offset: 0x00135AAA
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_Misc_01, 2.5f);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPowerTwoheads_01, 2.5f);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_32h.m_HeroPowerFade);
			break;
		case 104:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_32h.m_HeroPowerMohawk);
			break;
		case 105:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_32h.m_HeroPowerPompadour);
			break;
		case 106:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_32h.m_HeroPowerQuiff);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003BDE RID: 15326 RVA: 0x001378C0 File Offset: 0x00135AC0
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		if (!(cardId == "ICC_064"))
		{
			if (cardId == "CS2_077")
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_PlayerSprint_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_PlayerBloodRazor_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003BDF RID: 15327 RVA: 0x001378D6 File Offset: 0x00135AD6
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
		if (cardId == "GVG_023")
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_BossAutoBarber_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040024E6 RID: 9446
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_BossAutoBarber_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_BossAutoBarber_01.prefab:828757ed7ab447a438e69cb9ef811c47");

	// Token: 0x040024E7 RID: 9447
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_Death_02 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_Death_02.prefab:96afa188faa4aaa4ba5a2c009f31bedf");

	// Token: 0x040024E8 RID: 9448
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_DefeatPlayer_01.prefab:ff7d5b7b50f0eba43a0a5b5f3725527f");

	// Token: 0x040024E9 RID: 9449
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_EmoteResponse_01.prefab:a4100c545b9a0a848b1c915c2680cd38");

	// Token: 0x040024EA RID: 9450
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPower_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPower_01.prefab:8688fa21c89f08145b0e70ca27ba8dec");

	// Token: 0x040024EB RID: 9451
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPower_02 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPower_02.prefab:672e74d253d03f44c959d2979d9d28ed");

	// Token: 0x040024EC RID: 9452
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPower_03 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPower_03.prefab:e658e6c6866676a46bd152c0c3918d7a");

	// Token: 0x040024ED RID: 9453
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPower_04 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPower_04.prefab:0a7205994ddbb2347a4359a8ce94ee9f");

	// Token: 0x040024EE RID: 9454
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPowerFade_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPowerFade_01.prefab:7638e2f3bf88dbb428f13952512791b1");

	// Token: 0x040024EF RID: 9455
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPowerFade_02 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPowerFade_02.prefab:04f99f14fb9e148458654cf03b07c5b6");

	// Token: 0x040024F0 RID: 9456
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPowerMohawk_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPowerMohawk_01.prefab:e53663687771726459b9041a68685244");

	// Token: 0x040024F1 RID: 9457
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPowerMohawk_02 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPowerMohawk_02.prefab:9e3f2d87cc729f24b9935303a9fd1415");

	// Token: 0x040024F2 RID: 9458
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPowerPompadour_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPowerPompadour_01.prefab:d0a9c3f2ea1c5af4ba71cc17585a8683");

	// Token: 0x040024F3 RID: 9459
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPowerPompadour_02 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPowerPompadour_02.prefab:8b703e12d0c771444906f6d38426f982");

	// Token: 0x040024F4 RID: 9460
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPowerQuiff_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPowerQuiff_01.prefab:6ab7d7a10d5bf6d4db3947456896ccea");

	// Token: 0x040024F5 RID: 9461
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPowerQuiff_02 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPowerQuiff_02.prefab:7c26b30be2fafe24da261fce511a06ab");

	// Token: 0x040024F6 RID: 9462
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_HeroPowerTwoheads_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_HeroPowerTwoheads_01.prefab:0c97196de5768d04c97700790cecaf01");

	// Token: 0x040024F7 RID: 9463
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_Idle_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_Idle_01.prefab:9b93b558194d5dd408af722e7e87a345");

	// Token: 0x040024F8 RID: 9464
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_Idle_02 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_Idle_02.prefab:e94aa27e2e842e142a52bfcd7174ba96");

	// Token: 0x040024F9 RID: 9465
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_Idle_03 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_Idle_03.prefab:ce78c7593e7482845862a8976178ab5a");

	// Token: 0x040024FA RID: 9466
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_Intro_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_Intro_01.prefab:055e362bca70b384d92a2cd9194f7ef6");

	// Token: 0x040024FB RID: 9467
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_IntroEudora_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_IntroEudora_01.prefab:fe8c572e3d46bd54e9e11f23928aa894");

	// Token: 0x040024FC RID: 9468
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_IntroRakanishu_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_IntroRakanishu_01.prefab:64c5f421192531244b6b49cfbe79ab1f");

	// Token: 0x040024FD RID: 9469
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_Misc_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_Misc_01.prefab:7c2f969c11847e64abd89b4dd0cb1f30");

	// Token: 0x040024FE RID: 9470
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_PlayerBloodRazor_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_PlayerBloodRazor_01.prefab:c0084f200ca37e049b0555f84e7fcfe3");

	// Token: 0x040024FF RID: 9471
	private static readonly AssetReference VO_DALA_BOSS_32h_Female_Goblin_PlayerSprint_01 = new AssetReference("VO_DALA_BOSS_32h_Female_Goblin_PlayerSprint_01.prefab:c33ee0b3d8457104692cd17c3ba0ab13");

	// Token: 0x04002500 RID: 9472
	private static List<string> m_HeroPowerFade = new List<string>
	{
		DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPowerFade_01,
		DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPowerFade_02
	};

	// Token: 0x04002501 RID: 9473
	private static List<string> m_HeroPowerMohawk = new List<string>
	{
		DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPowerMohawk_01,
		DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPowerMohawk_02
	};

	// Token: 0x04002502 RID: 9474
	private static List<string> m_HeroPowerPompadour = new List<string>
	{
		DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPowerPompadour_01,
		DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPowerPompadour_02
	};

	// Token: 0x04002503 RID: 9475
	private static List<string> m_HeroPowerQuiff = new List<string>
	{
		DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPowerQuiff_01,
		DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_HeroPowerQuiff_02
	};

	// Token: 0x04002504 RID: 9476
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_Idle_01,
		DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_Idle_02,
		DALA_Dungeon_Boss_32h.VO_DALA_BOSS_32h_Female_Goblin_Idle_03
	};

	// Token: 0x04002505 RID: 9477
	private HashSet<string> m_playedLines = new HashSet<string>();
}
