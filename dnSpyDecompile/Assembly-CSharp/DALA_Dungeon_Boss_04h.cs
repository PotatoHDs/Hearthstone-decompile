using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000431 RID: 1073
public class DALA_Dungeon_Boss_04h : DALA_Dungeon
{
	// Token: 0x06003A7A RID: 14970 RVA: 0x0012DFD8 File Offset: 0x0012C1D8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_Death_01,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_DefeatPlayer_01,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_EmoteResponse_01,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPower_01,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPower_02,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPower_03,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPower_04,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPowerBossOnly_01,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPowerBoth_01,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPowerBoth_02,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPowerLose_01,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPowerLose_02,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPowerPlayerOnly_01,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPowerWin_01,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPowerWin_02,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_Idle_01,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_Idle_02,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_Idle_03,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_Intro_01,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_IntroSqueamlish_01,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_PlayerPortal_01,
			DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_PlayerPortal_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003A7B RID: 14971 RVA: 0x0012E19C File Offset: 0x0012C39C
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_04h.m_IdleLines;
	}

	// Token: 0x06003A7C RID: 14972 RVA: 0x0012E1A3 File Offset: 0x0012C3A3
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_EmoteResponse_01;
	}

	// Token: 0x06003A7D RID: 14973 RVA: 0x0012E1DC File Offset: 0x0012C3DC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Squeamlish")
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

	// Token: 0x06003A7E RID: 14974 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003A7F RID: 14975 RVA: 0x0012E287 File Offset: 0x0012C487
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
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerLines);
			break;
		case 102:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerWinLines);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerBothLines);
			break;
		case 104:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_HeroPowerLoseLines);
			break;
		case 105:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPowerPlayerOnly_01, 2.5f);
			break;
		case 106:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPowerBossOnly_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003A80 RID: 14976 RVA: 0x0012E29D File Offset: 0x0012C49D
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
		if (cardId == "GVG_003" || cardId == "KAR_073" || cardId == "KAR_075" || cardId == "KAR_076" || cardId == "KAR_077" || cardId == "KAR_091")
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_PlayerPortalLines);
		}
		yield break;
	}

	// Token: 0x06003A81 RID: 14977 RVA: 0x0012E2B3 File Offset: 0x0012C4B3
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

	// Token: 0x04002242 RID: 8770
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_Death_01 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_Death_01.prefab:46a0e0c00caa6e44888c2eace85bc99a");

	// Token: 0x04002243 RID: 8771
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_DefeatPlayer_01.prefab:76126d54a811dc04e93293e90792abe7");

	// Token: 0x04002244 RID: 8772
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_EmoteResponse_01.prefab:3aac8184734d5414dbf1e02434130751");

	// Token: 0x04002245 RID: 8773
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_HeroPower_01 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_HeroPower_01.prefab:8bb49a9fb35a2a34aaf7606d76db6f0c");

	// Token: 0x04002246 RID: 8774
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_HeroPower_02 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_HeroPower_02.prefab:d527a85b6751d584d99723594f0533ef");

	// Token: 0x04002247 RID: 8775
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_HeroPower_03 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_HeroPower_03.prefab:dcba7364df536c44aa8261c004aa672b");

	// Token: 0x04002248 RID: 8776
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_HeroPower_04 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_HeroPower_04.prefab:5eb3c02bcff8d164a804d2e12e58189d");

	// Token: 0x04002249 RID: 8777
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_HeroPowerBossOnly_01 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_HeroPowerBossOnly_01.prefab:02fbc3172008a6b4ba7afdfba782fee4");

	// Token: 0x0400224A RID: 8778
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_HeroPowerBoth_01 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_HeroPowerBoth_01.prefab:5bf1dd5e4ae72d6438d80263333205c6");

	// Token: 0x0400224B RID: 8779
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_HeroPowerBoth_02 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_HeroPowerBoth_02.prefab:0b9ddce9a968bb84f87c14196ccd8727");

	// Token: 0x0400224C RID: 8780
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_HeroPowerLose_01 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_HeroPowerLose_01.prefab:8b615dd50781cb5419ce7d0ceee9fb2a");

	// Token: 0x0400224D RID: 8781
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_HeroPowerLose_02 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_HeroPowerLose_02.prefab:a803cdde33698af44bfd6c0b878f5094");

	// Token: 0x0400224E RID: 8782
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_HeroPowerPlayerOnly_01 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_HeroPowerPlayerOnly_01.prefab:fe29a261abc694e4ebc6bddfb8ed5737");

	// Token: 0x0400224F RID: 8783
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_HeroPowerWin_01 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_HeroPowerWin_01.prefab:e803d23a1629058428b21c7acbc73dbf");

	// Token: 0x04002250 RID: 8784
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_HeroPowerWin_02 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_HeroPowerWin_02.prefab:bc306118e17e4e349b4d55bc6e1eeb74");

	// Token: 0x04002251 RID: 8785
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_Idle_01 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_Idle_01.prefab:77528513aa5105c44b8b1c88d4109cea");

	// Token: 0x04002252 RID: 8786
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_Idle_02 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_Idle_02.prefab:f08533aa89553c84bb8a7cdc75194783");

	// Token: 0x04002253 RID: 8787
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_Idle_03 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_Idle_03.prefab:28536f8e8afc7ec42bd6deb2fed2e854");

	// Token: 0x04002254 RID: 8788
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_Intro_01 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_Intro_01.prefab:c716f8fea2de9d14f81db9d6ad33b0b5");

	// Token: 0x04002255 RID: 8789
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_IntroSqueamlish_01 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_IntroSqueamlish_01.prefab:454f78f9863711b4dbe731012b075d0d");

	// Token: 0x04002256 RID: 8790
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_PlayerPortal_01 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_PlayerPortal_01.prefab:6fbd49194cbc21845ab17bc9da6b620f");

	// Token: 0x04002257 RID: 8791
	private static readonly AssetReference VO_DALA_BOSS_04h_Female_Undead_PlayerPortal_02 = new AssetReference("VO_DALA_BOSS_04h_Female_Undead_PlayerPortal_02.prefab:b578b27f60a2c944199c30eb2fd1c38d");

	// Token: 0x04002258 RID: 8792
	private List<string> m_HeroPowerLines = new List<string>
	{
		DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPower_01,
		DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPower_02,
		DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPower_03,
		DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPower_04
	};

	// Token: 0x04002259 RID: 8793
	private List<string> m_HeroPowerWinLines = new List<string>
	{
		DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPowerWin_01,
		DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPowerWin_02
	};

	// Token: 0x0400225A RID: 8794
	private List<string> m_HeroPowerBothLines = new List<string>
	{
		DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPowerBoth_01,
		DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPowerBoth_02
	};

	// Token: 0x0400225B RID: 8795
	private List<string> m_HeroPowerLoseLines = new List<string>
	{
		DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPowerLose_01,
		DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_HeroPowerLose_02
	};

	// Token: 0x0400225C RID: 8796
	private List<string> m_PlayerPortalLines = new List<string>
	{
		DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_PlayerPortal_01,
		DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_PlayerPortal_02
	};

	// Token: 0x0400225D RID: 8797
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_Idle_01,
		DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_Idle_02,
		DALA_Dungeon_Boss_04h.VO_DALA_BOSS_04h_Female_Undead_Idle_03
	};

	// Token: 0x0400225E RID: 8798
	private HashSet<string> m_playedLines = new HashSet<string>();
}
