using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004B2 RID: 1202
public class ULDA_Dungeon_Boss_53h : ULDA_Dungeon
{
	// Token: 0x060040A6 RID: 16550 RVA: 0x0015910C File Offset: 0x0015730C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerDaringEscape_01,
			ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerJarDealer_01,
			ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerWastelandSapper_01,
			ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_Death_01,
			ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_DefeatPlayer_01,
			ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_EmoteResponse_01,
			ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_01,
			ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_03,
			ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_04,
			ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_05,
			ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_Idle_01,
			ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_Idle_02,
			ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_Idle_03,
			ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_Intro_01,
			ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_IntroSpecial_Finley_01,
			ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_PlayerTrigger_Beaming_Sidekick_01,
			ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_PlayerTrigger_Weaponized_Wasp_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060040A7 RID: 16551 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060040A8 RID: 16552 RVA: 0x00159280 File Offset: 0x00157480
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x060040A9 RID: 16553 RVA: 0x00159288 File Offset: 0x00157488
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x060040AA RID: 16554 RVA: 0x00159290 File Offset: 0x00157490
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_Intro_01;
		this.m_deathLine = ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_Death_01;
		this.m_standardEmoteResponseLine = ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_EmoteResponse_01;
	}

	// Token: 0x060040AB RID: 16555 RVA: 0x001592C8 File Offset: 0x001574C8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Finley")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_IntroSpecial_Finley_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
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

	// Token: 0x060040AC RID: 16556 RVA: 0x001593A2 File Offset: 0x001575A2
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerWastelandSapper_01, 2.5f);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x060040AD RID: 16557 RVA: 0x001593B8 File Offset: 0x001575B8
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
		if (!(cardId == "ULD_191"))
		{
			if (cardId == "ULD_170")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_PlayerTrigger_Weaponized_Wasp_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_PlayerTrigger_Beaming_Sidekick_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060040AE RID: 16558 RVA: 0x001593CE File Offset: 0x001575CE
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
		if (!(cardId == "DAL_728"))
		{
			if (cardId == "ULD_282")
			{
				yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerJarDealer_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerDaringEscape_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002ED3 RID: 11987
	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerDaringEscape_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerDaringEscape_01.prefab:2637615ee3e2bd54bb27e0147bbb0d55");

	// Token: 0x04002ED4 RID: 11988
	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerJarDealer_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerJarDealer_01.prefab:deb4537b4785f7945a5604fe1a186582");

	// Token: 0x04002ED5 RID: 11989
	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerWastelandSapper_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_BossTriggerWastelandSapper_01.prefab:9afeaaf30bed4254084edb0e263ebb90");

	// Token: 0x04002ED6 RID: 11990
	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_Death_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_Death_01.prefab:ed3772c2829703b439df74f56395b95c");

	// Token: 0x04002ED7 RID: 11991
	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_DefeatPlayer_01.prefab:457fdc2378c5dc04788a66fac2e41d5e");

	// Token: 0x04002ED8 RID: 11992
	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_EmoteResponse_01.prefab:e9b9f218764cab74998c8b91cd5a1ae2");

	// Token: 0x04002ED9 RID: 11993
	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_01.prefab:886812266f013f2449ee10f150b76770");

	// Token: 0x04002EDA RID: 11994
	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_03.prefab:a374d46474d47fc4d88150288d825da8");

	// Token: 0x04002EDB RID: 11995
	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_04.prefab:eb39a55924edbd046a1d89bfac59fd17");

	// Token: 0x04002EDC RID: 11996
	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_05.prefab:d9725f3a83ca4d2459a9d603381691b3");

	// Token: 0x04002EDD RID: 11997
	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_Idle_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_Idle_01.prefab:cdf31f93b1d7c3e4eae1fbe12fdef9f2");

	// Token: 0x04002EDE RID: 11998
	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_Idle_02 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_Idle_02.prefab:330978ec179b6d14a887cf703cdff70e");

	// Token: 0x04002EDF RID: 11999
	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_Idle_03 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_Idle_03.prefab:281d51bf27743f54c8dfb4499fbf30c0");

	// Token: 0x04002EE0 RID: 12000
	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_Intro_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_Intro_01.prefab:a1479a14ba655d74d96f31de2b39b809");

	// Token: 0x04002EE1 RID: 12001
	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_IntroSpecial_Finley_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_IntroSpecial_Finley_01.prefab:694aa7613497d3d47ba7ff5189039063");

	// Token: 0x04002EE2 RID: 12002
	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_PlayerTrigger_Beaming_Sidekick_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_PlayerTrigger_Beaming_Sidekick_01.prefab:cddd130c437994045a53f31f504bdf4f");

	// Token: 0x04002EE3 RID: 12003
	private static readonly AssetReference VO_ULDA_BOSS_53h_Female_Vulpera_PlayerTrigger_Weaponized_Wasp_01 = new AssetReference("VO_ULDA_BOSS_53h_Female_Vulpera_PlayerTrigger_Weaponized_Wasp_01.prefab:7e927df9d85d9c946ad5785da33bcc84");

	// Token: 0x04002EE4 RID: 12004
	private List<string> m_HeroPowerLines = new List<string>
	{
		ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_01,
		ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_03,
		ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_04,
		ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_HeroPower_05
	};

	// Token: 0x04002EE5 RID: 12005
	private List<string> m_IdleLines = new List<string>
	{
		ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_Idle_01,
		ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_Idle_02,
		ULDA_Dungeon_Boss_53h.VO_ULDA_BOSS_53h_Female_Vulpera_Idle_03
	};

	// Token: 0x04002EE6 RID: 12006
	private HashSet<string> m_playedLines = new HashSet<string>();
}
