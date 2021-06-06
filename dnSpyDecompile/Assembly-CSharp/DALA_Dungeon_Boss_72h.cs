using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000475 RID: 1141
public class DALA_Dungeon_Boss_72h : DALA_Dungeon
{
	// Token: 0x06003DC9 RID: 15817 RVA: 0x00144C1C File Offset: 0x00142E1C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_BossHellfire_01,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_BossHellfire_02,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_Death_01,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_DefeatPlayer_01,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_EmoteResponse_01,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_01,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_02,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_03,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_05,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_06,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_07,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_Idle_01,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_Idle_02,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_Idle_03,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_Intro_01,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_IntroChu_01,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_IntroGeorge_01,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_IntroTekahn_01,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_PlayerArmor_01,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_PlayerCrystalizer_01,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_PlayerSelfDamage_01,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_PlayerSelfDamage_02,
			DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_PlayerSelfDamage_04
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003DCA RID: 15818 RVA: 0x00144DF0 File Offset: 0x00142FF0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_EmoteResponse_01;
	}

	// Token: 0x06003DCB RID: 15819 RVA: 0x00144E28 File Offset: 0x00143028
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_72h.m_IdleLines;
	}

	// Token: 0x06003DCC RID: 15820 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003DCD RID: 15821 RVA: 0x00144E30 File Offset: 0x00143030
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Chu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_IntroChu_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (cardId == "DALA_Tekahn")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_IntroTekahn_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x06003DCE RID: 15822 RVA: 0x00144F85 File Offset: 0x00143185
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
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_72h.m_HeroPowerTriggers);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_72h.m_PlayerSelfDamage);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_PlayerArmor_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003DCF RID: 15823 RVA: 0x00144F9B File Offset: 0x0014319B
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
		if (cardId == "BOT_447")
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_PlayerCrystalizer_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003DD0 RID: 15824 RVA: 0x00144FB1 File Offset: 0x001431B1
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
		if (cardId == "CS2_062")
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_72h.m_BossHellfire);
		}
		yield break;
	}

	// Token: 0x0400291F RID: 10527
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_BossHellfire_01 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_BossHellfire_01.prefab:8988a82632401a140a15d8c1a660b4b4");

	// Token: 0x04002920 RID: 10528
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_BossHellfire_02 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_BossHellfire_02.prefab:3b8dd3526cb892244aa3b4ebba269eab");

	// Token: 0x04002921 RID: 10529
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_Death_01 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_Death_01.prefab:ce35bb6b5c7b393429b44bd08804d365");

	// Token: 0x04002922 RID: 10530
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_DefeatPlayer_01.prefab:500362ca05e78c344807b2accadc6fca");

	// Token: 0x04002923 RID: 10531
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_EmoteResponse_01.prefab:5be88a443b01d2642a381b4a160ffb38");

	// Token: 0x04002924 RID: 10532
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_01 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_01.prefab:abbf8555486928643821662eeb81389e");

	// Token: 0x04002925 RID: 10533
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_02 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_02.prefab:f4f991c53e2358445a892e731c654faa");

	// Token: 0x04002926 RID: 10534
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_03 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_03.prefab:dc090b44a77a8be4eb3ba785ff50fa38");

	// Token: 0x04002927 RID: 10535
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_05 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_05.prefab:f634efd6b5fe2584ab656871377ac8d1");

	// Token: 0x04002928 RID: 10536
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_06 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_06.prefab:a3ad8408b4dbb6c499a96fc6f0105caf");

	// Token: 0x04002929 RID: 10537
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_07 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_07.prefab:eedec59b161c03e4a820322fa1baef0c");

	// Token: 0x0400292A RID: 10538
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_Idle_01 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_Idle_01.prefab:be19e94b39060734393434f14fdd0178");

	// Token: 0x0400292B RID: 10539
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_Idle_02 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_Idle_02.prefab:04640dbd544b35d4e8ecaa76162857eb");

	// Token: 0x0400292C RID: 10540
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_Idle_03 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_Idle_03.prefab:16c74b8e5517fb5428f84203e8b06cf9");

	// Token: 0x0400292D RID: 10541
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_Intro_01 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_Intro_01.prefab:9438155b1c3ffa445a46c8f44981cd57");

	// Token: 0x0400292E RID: 10542
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_IntroChu_01 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_IntroChu_01.prefab:e0c7236fc90da914696de05a698688de");

	// Token: 0x0400292F RID: 10543
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_IntroGeorge_01.prefab:c2f8625463a268d489e4075236119797");

	// Token: 0x04002930 RID: 10544
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_IntroTekahn_01 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_IntroTekahn_01.prefab:6747b1561dc4d754284ce800882b2fa4");

	// Token: 0x04002931 RID: 10545
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_PlayerArmor_01 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_PlayerArmor_01.prefab:af04d08c416998f41bb6dc2736f5caab");

	// Token: 0x04002932 RID: 10546
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_PlayerCrystalizer_01 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_PlayerCrystalizer_01.prefab:96fcb64aa3048614d917a868a9704e10");

	// Token: 0x04002933 RID: 10547
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_PlayerSelfDamage_01 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_PlayerSelfDamage_01.prefab:c8af605cc72f4f942bc1a4b5f045582d");

	// Token: 0x04002934 RID: 10548
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_PlayerSelfDamage_02 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_PlayerSelfDamage_02.prefab:94b8acaa3d30a2345849afd1e2a66917");

	// Token: 0x04002935 RID: 10549
	private static readonly AssetReference VO_DALA_BOSS_72h_Male_Orc_PlayerSelfDamage_04 = new AssetReference("VO_DALA_BOSS_72h_Male_Orc_PlayerSelfDamage_04.prefab:e1e7f8f30939a534fbea91897c5f305a");

	// Token: 0x04002936 RID: 10550
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_Idle_01,
		DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_Idle_02,
		DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_Idle_03
	};

	// Token: 0x04002937 RID: 10551
	private static List<string> m_HeroPowerTriggers = new List<string>
	{
		DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_01,
		DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_02,
		DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_03,
		DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_05,
		DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_06,
		DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_HeroPowerTrigger_07
	};

	// Token: 0x04002938 RID: 10552
	private static List<string> m_PlayerSelfDamage = new List<string>
	{
		DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_PlayerSelfDamage_01,
		DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_PlayerSelfDamage_02,
		DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_PlayerSelfDamage_04
	};

	// Token: 0x04002939 RID: 10553
	private static List<string> m_BossHellfire = new List<string>
	{
		DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_BossHellfire_01,
		DALA_Dungeon_Boss_72h.VO_DALA_BOSS_72h_Male_Orc_BossHellfire_02
	};

	// Token: 0x0400293A RID: 10554
	private HashSet<string> m_playedLines = new HashSet<string>();
}
