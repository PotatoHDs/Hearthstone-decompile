using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200043C RID: 1084
public class DALA_Dungeon_Boss_15h : DALA_Dungeon
{
	// Token: 0x06003B07 RID: 15111 RVA: 0x001323DC File Offset: 0x001305DC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_Death_02,
			DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_DefeatPlayer_01,
			DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_EmoteResponse_01,
			DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_01,
			DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_02,
			DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_03,
			DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_04,
			DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_05,
			DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_06,
			DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_Idle_01,
			DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_Idle_02,
			DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_Idle_03,
			DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_Intro_01,
			DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_IntroKriziki_01,
			DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_PlayerDemon_01,
			DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_PlayerHeal_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003B08 RID: 15112 RVA: 0x00132540 File Offset: 0x00130740
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_EmoteResponse_01;
	}

	// Token: 0x06003B09 RID: 15113 RVA: 0x00132578 File Offset: 0x00130778
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_15h.m_IdleLines;
	}

	// Token: 0x06003B0A RID: 15114 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003B0B RID: 15115 RVA: 0x0013257F File Offset: 0x0013077F
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
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_PlayerHeal_01, 2.5f);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_PlayerDemon_01, 2.5f);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerLines);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003B0C RID: 15116 RVA: 0x00132598 File Offset: 0x00130798
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_George" && cardId != "DALA_Kriziki")
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

	// Token: 0x06003B0D RID: 15117 RVA: 0x00132650 File Offset: 0x00130850
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
		yield break;
	}

	// Token: 0x06003B0E RID: 15118 RVA: 0x00132666 File Offset: 0x00130866
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
		yield break;
	}

	// Token: 0x04002369 RID: 9065
	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_Death_02 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_Death_02.prefab:53e487fe547ce424a99d083996127889");

	// Token: 0x0400236A RID: 9066
	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_DefeatPlayer_01.prefab:88f00ff818b5b3c47b3f9ec33835bb85");

	// Token: 0x0400236B RID: 9067
	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_EmoteResponse_01.prefab:66243972da7da114499a411e6c434dd6");

	// Token: 0x0400236C RID: 9068
	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_01 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_01.prefab:4c63fdf26e33fef4cb6bddc573c47f7c");

	// Token: 0x0400236D RID: 9069
	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_02 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_02.prefab:a2dd6dbc3713ce34b9ee151c3d558904");

	// Token: 0x0400236E RID: 9070
	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_03 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_03.prefab:e269a1fece3444444ad4e7b456360bdd");

	// Token: 0x0400236F RID: 9071
	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_04 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_04.prefab:b34a00b601345cd4e964e86c60c275e1");

	// Token: 0x04002370 RID: 9072
	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_05 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_05.prefab:ac30df941bcd2e54e90b8b067dd2da60");

	// Token: 0x04002371 RID: 9073
	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_06 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_06.prefab:f89145871a2bb714284f733a22c1d5bf");

	// Token: 0x04002372 RID: 9074
	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_Idle_01 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_Idle_01.prefab:85e720bb252e5744c9ba0b19e6304f0f");

	// Token: 0x04002373 RID: 9075
	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_Idle_02 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_Idle_02.prefab:2f6bec85149accb47a69d143d0d7f1f1");

	// Token: 0x04002374 RID: 9076
	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_Idle_03 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_Idle_03.prefab:14ff1bc1a3a44ec4f91013198afa848c");

	// Token: 0x04002375 RID: 9077
	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_Intro_01 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_Intro_01.prefab:364d46ec967ce55459b6a6994209ab18");

	// Token: 0x04002376 RID: 9078
	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_IntroKriziki_01 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_IntroKriziki_01.prefab:86e00c690c422384ca8955a010ad5695");

	// Token: 0x04002377 RID: 9079
	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_PlayerDemon_01 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_PlayerDemon_01.prefab:8086f9ef6a886c04786bca5ca056dab2");

	// Token: 0x04002378 RID: 9080
	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_PlayerHeal_01 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_PlayerHeal_01.prefab:695a8269b3591be48ae676b8971a3210");

	// Token: 0x04002379 RID: 9081
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_Idle_01,
		DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_Idle_02,
		DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_Idle_03
	};

	// Token: 0x0400237A RID: 9082
	private List<string> m_HeroPowerLines = new List<string>
	{
		DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_01,
		DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_02,
		DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_03,
		DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_04,
		DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_05,
		DALA_Dungeon_Boss_15h.VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_06
	};

	// Token: 0x0400237B RID: 9083
	private HashSet<string> m_playedLines = new HashSet<string>();
}
