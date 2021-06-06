using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000525 RID: 1317
public class BoH_Jaina_04 : BoH_Jaina_Dungeon
{
	// Token: 0x0600478A RID: 18314 RVA: 0x0016DED1 File Offset: 0x0016C0D1
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.DO_OPENING_TAUNTS,
				false
			}
		};
	}

	// Token: 0x0600478B RID: 18315 RVA: 0x001803E8 File Offset: 0x0017E5E8
	public BoH_Jaina_04()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Jaina_04.s_booleanOptions);
	}

	// Token: 0x0600478C RID: 18316 RVA: 0x0018048C File Offset: 0x0017E68C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4EmoteResponse_01,
			BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4ExchangeA_01,
			BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4ExchangeB_01,
			BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4HeroPower_01,
			BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4HeroPower_02,
			BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4HeroPower_03,
			BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Idle_01,
			BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Idle_02,
			BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Idle_03,
			BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Intro_01,
			BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Intro_02,
			BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Loss_01,
			BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Victory_01,
			BoH_Jaina_04.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission4ExchangeA_01,
			BoH_Jaina_04.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission4ExchangeB_01,
			BoH_Jaina_04.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission4ExchangeC_01,
			BoH_Jaina_04.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission4Intro_01,
			BoH_Jaina_04.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission4Victory_01,
			BoH_Jaina_04.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4ExchangeC_01,
			BoH_Jaina_04.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4ThrallHelp_01,
			BoH_Jaina_04.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4ThrallHelp_02,
			BoH_Jaina_04.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4ThrallHelp_03,
			BoH_Jaina_04.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4Victory_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600478D RID: 18317 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600478E RID: 18318 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x0600478F RID: 18319 RVA: 0x00180660 File Offset: 0x0017E860
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(enemyActor, BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_04.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission4Intro_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Intro_02, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004790 RID: 18320 RVA: 0x0018066F File Offset: 0x0017E86F
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4IdleLines;
	}

	// Token: 0x06004791 RID: 18321 RVA: 0x00180677 File Offset: 0x0017E877
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4HeroPowerLines;
	}

	// Token: 0x06004792 RID: 18322 RVA: 0x00180680 File Offset: 0x0017E880
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Intro_02, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004793 RID: 18323 RVA: 0x0018073B File Offset: 0x0017E93B
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 501:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Victory_01, 2.5f);
			yield return base.PlayLineAlways(BoH_Jaina_04.ThrallBrassRing, BoH_Jaina_04.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4Victory_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_04.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission4Victory_01, 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 502:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(BoH_Jaina_04.ThrallBrassRing, BoH_Jaina_04.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4ExchangeC_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_04.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission4ExchangeC_01, 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 503:
			yield return base.PlayLineAlways(BoH_Jaina_04.ThrallBrassRing, BoH_Jaina_04.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4ThrallHelp_01, 2.5f);
			break;
		case 504:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Loss_01, 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 505:
			yield return base.PlayLineAlways(BoH_Jaina_04.ThrallBrassRing, BoH_Jaina_04.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4ThrallHelp_02, 2.5f);
			break;
		case 506:
			yield return base.PlayLineAlways(BoH_Jaina_04.ThrallBrassRing, BoH_Jaina_04.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4ThrallHelp_03, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06004794 RID: 18324 RVA: 0x00180751 File Offset: 0x0017E951
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
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x06004795 RID: 18325 RVA: 0x00180767 File Offset: 0x0017E967
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
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x06004796 RID: 18326 RVA: 0x0018077D File Offset: 0x0017E97D
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 3)
		{
			if (turn == 5)
			{
				yield return base.PlayLineAlways(actor, BoH_Jaina_04.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission4ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4ExchangeB_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BoH_Jaina_04.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission4ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004797 RID: 18327 RVA: 0x0017D1BC File Offset: 0x0017B3BC
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BRMAdventure);
	}

	// Token: 0x04003B05 RID: 15109
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Jaina_04.InitBooleanOptions();

	// Token: 0x04003B06 RID: 15110
	private static readonly AssetReference VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4EmoteResponse_01 = new AssetReference("VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4EmoteResponse_01.prefab:15c60d17bdabd674d80309293e5713eb");

	// Token: 0x04003B07 RID: 15111
	private static readonly AssetReference VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4ExchangeA_01 = new AssetReference("VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4ExchangeA_01.prefab:1117f7cfe3651354782f6f6789a18ac1");

	// Token: 0x04003B08 RID: 15112
	private static readonly AssetReference VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4ExchangeB_01 = new AssetReference("VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4ExchangeB_01.prefab:c726cf6310252d24d959f73837757809");

	// Token: 0x04003B09 RID: 15113
	private static readonly AssetReference VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4HeroPower_01 = new AssetReference("VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4HeroPower_01.prefab:c547253ef12be0d499954ede128b2a4d");

	// Token: 0x04003B0A RID: 15114
	private static readonly AssetReference VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4HeroPower_02 = new AssetReference("VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4HeroPower_02.prefab:5529c0a384efb944d94a85ee313d5e37");

	// Token: 0x04003B0B RID: 15115
	private static readonly AssetReference VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4HeroPower_03 = new AssetReference("VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4HeroPower_03.prefab:3c404f92997e9ee46afc39972f4c352e");

	// Token: 0x04003B0C RID: 15116
	private static readonly AssetReference VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Idle_01 = new AssetReference("VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Idle_01.prefab:2c71bf5eef1fbbb44907f8fca0d69803");

	// Token: 0x04003B0D RID: 15117
	private static readonly AssetReference VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Idle_02 = new AssetReference("VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Idle_02.prefab:1ef4a3214969abd408fbb322ba6d00c5");

	// Token: 0x04003B0E RID: 15118
	private static readonly AssetReference VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Idle_03 = new AssetReference("VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Idle_03.prefab:71919c7c8ffc2574091924c45848add9");

	// Token: 0x04003B0F RID: 15119
	private static readonly AssetReference VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Intro_01 = new AssetReference("VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Intro_01.prefab:a1e18bbde14d60948883d830ddfb059d");

	// Token: 0x04003B10 RID: 15120
	private static readonly AssetReference VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Intro_02 = new AssetReference("VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Intro_02.prefab:e96c65e01c544764daaba8d5fa283a02");

	// Token: 0x04003B11 RID: 15121
	private static readonly AssetReference VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Loss_01 = new AssetReference("VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Loss_01.prefab:63a2314eaaf97df47b6cb50c8987e5d4");

	// Token: 0x04003B12 RID: 15122
	private static readonly AssetReference VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Victory_01.prefab:64f3a2b31d85c0b4b8e3a83fa4d7f809");

	// Token: 0x04003B13 RID: 15123
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission4ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission4ExchangeA_01.prefab:0a5317a4515e4aa4ab34a65ec796264d");

	// Token: 0x04003B14 RID: 15124
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission4ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission4ExchangeB_01.prefab:6e536a6e1d138a149ab257e379ff2030");

	// Token: 0x04003B15 RID: 15125
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission4ExchangeC_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission4ExchangeC_01.prefab:61038aed51fb36c4cacdf8b1b7efe8bd");

	// Token: 0x04003B16 RID: 15126
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission4Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission4Intro_01.prefab:1ed5388a7190ad549aa3fab63b2ab0db");

	// Token: 0x04003B17 RID: 15127
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission4Victory_01.prefab:6d44386f1f852024dac446257a39a6a0");

	// Token: 0x04003B18 RID: 15128
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4ExchangeC_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4ExchangeC_01.prefab:0f5556d64e0743439c18c5c4d05d2b7e");

	// Token: 0x04003B19 RID: 15129
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4ThrallHelp_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4ThrallHelp_01.prefab:48dffc5ef0bb4118a1425d585e3f5063");

	// Token: 0x04003B1A RID: 15130
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4ThrallHelp_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4ThrallHelp_02.prefab:7c5eb9a511364f8da1a77958b764374b");

	// Token: 0x04003B1B RID: 15131
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4ThrallHelp_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4ThrallHelp_03.prefab:83503a35178640b7a6e304760c80e413");

	// Token: 0x04003B1C RID: 15132
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4Victory_01.prefab:6c989bfe86c64571a158f5ecb18073db");

	// Token: 0x04003B1D RID: 15133
	public static readonly AssetReference ThrallBrassRing = new AssetReference("Thrall_BrassRing_Quote.prefab:962e58c9390b0f842a8b64d0d44cf7b4");

	// Token: 0x04003B1E RID: 15134
	private List<string> m_VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4HeroPowerLines = new List<string>
	{
		BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4HeroPower_01,
		BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4HeroPower_02,
		BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4HeroPower_03
	};

	// Token: 0x04003B1F RID: 15135
	private List<string> m_VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4IdleLines = new List<string>
	{
		BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Idle_01,
		BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Idle_02,
		BoH_Jaina_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4Idle_03
	};

	// Token: 0x04003B20 RID: 15136
	private HashSet<string> m_playedLines = new HashSet<string>();
}
