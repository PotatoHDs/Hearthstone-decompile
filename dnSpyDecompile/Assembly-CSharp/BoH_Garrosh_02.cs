using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000519 RID: 1305
public class BoH_Garrosh_02 : BoH_Garrosh_Dungeon
{
	// Token: 0x060046B1 RID: 18097 RVA: 0x0017CF14 File Offset: 0x0017B114
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Garrosh_02.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission2ExchangeC_02,
			BoH_Garrosh_02.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission2ExchangeA_01,
			BoH_Garrosh_02.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission2ExchangeB_01,
			BoH_Garrosh_02.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission2Intro_01,
			BoH_Garrosh_02.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission2Victory_01,
			BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2EmoteResponse_01,
			BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2ExchangeA_01,
			BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2ExchangeB_01,
			BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2ExchangeC_01,
			BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2HeroPower_01,
			BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2HeroPower_02,
			BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2HeroPower_03,
			BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Idle_01,
			BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Idle_02,
			BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Idle_03,
			BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Intro_01,
			BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Intro_02,
			BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Loss_01,
			BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Victory_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060046B2 RID: 18098 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060046B3 RID: 18099 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x060046B4 RID: 18100 RVA: 0x0017D0A8 File Offset: 0x0017B2A8
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_02.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission2Intro_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Intro_02, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060046B5 RID: 18101 RVA: 0x0017D0B7 File Offset: 0x0017B2B7
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2IdleLines;
	}

	// Token: 0x060046B6 RID: 18102 RVA: 0x0017D0BF File Offset: 0x0017B2BF
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2HeroPowerLines;
	}

	// Token: 0x060046B7 RID: 18103 RVA: 0x0017D0C7 File Offset: 0x0017B2C7
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2EmoteResponse_01;
	}

	// Token: 0x060046B8 RID: 18104 RVA: 0x0017D0E0 File Offset: 0x0017B2E0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060046B9 RID: 18105 RVA: 0x0017D164 File Offset: 0x0017B364
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 911)
		{
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(false);
			yield break;
		}
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 501)
		{
			if (missionEvent != 504)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BoH_Garrosh_02.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission2Victory_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Victory_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x060046BA RID: 18106 RVA: 0x0017D17A File Offset: 0x0017B37A
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

	// Token: 0x060046BB RID: 18107 RVA: 0x0017D190 File Offset: 0x0017B390
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

	// Token: 0x060046BC RID: 18108 RVA: 0x0017D1A6 File Offset: 0x0017B3A6
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (turn)
		{
		case 5:
			yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_02.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission2ExchangeA_01, 2.5f);
			break;
		case 7:
			yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2ExchangeB_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_02.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission2ExchangeB_01, 2.5f);
			break;
		case 9:
			yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_02.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission2ExchangeC_02, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2ExchangeC_01, 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x060046BD RID: 18109 RVA: 0x0017D1BC File Offset: 0x0017B3BC
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BRMAdventure);
	}

	// Token: 0x04003A10 RID: 14864
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission2ExchangeC_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission2ExchangeC_02.prefab:869bea22224da1445996eb61993a3782");

	// Token: 0x04003A11 RID: 14865
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission2ExchangeA_01.prefab:687968921db95754aac0242e8eb37022");

	// Token: 0x04003A12 RID: 14866
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission2ExchangeB_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission2ExchangeB_01.prefab:ed49af2df2aa3704c8259e07ab595677");

	// Token: 0x04003A13 RID: 14867
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission2Intro_01.prefab:227299eca6d30cd47b58ce1c74430997");

	// Token: 0x04003A14 RID: 14868
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission2Victory_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission2Victory_01.prefab:21a1375eaa3fd474daa0460e492293a7");

	// Token: 0x04003A15 RID: 14869
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2EmoteResponse_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2EmoteResponse_01.prefab:3f54be3babd4c774a87e34a25b465011");

	// Token: 0x04003A16 RID: 14870
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2ExchangeA_01.prefab:4bea4a44553a3e9489f96650a41823ee");

	// Token: 0x04003A17 RID: 14871
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2ExchangeB_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2ExchangeB_01.prefab:93de4687ad108e04b8580d160fa7ec93");

	// Token: 0x04003A18 RID: 14872
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2ExchangeC_01.prefab:7c274a42098bdaa46acff149e04d5de9");

	// Token: 0x04003A19 RID: 14873
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2HeroPower_01.prefab:62c830ac8ef650f4eb45441f5de6b85a");

	// Token: 0x04003A1A RID: 14874
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2HeroPower_02.prefab:b8ab4b1acd654404e88434cd8390222d");

	// Token: 0x04003A1B RID: 14875
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2HeroPower_03.prefab:c2c4765be650fce4eac968d182ff3875");

	// Token: 0x04003A1C RID: 14876
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Idle_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Idle_01.prefab:e45b053d508b2b04fbb6e6dae14c4571");

	// Token: 0x04003A1D RID: 14877
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Idle_02 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Idle_02.prefab:7de72d720b95ec14da04f4e5c16b6d29");

	// Token: 0x04003A1E RID: 14878
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Idle_03 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Idle_03.prefab:2a833e67e96b16440af9d7f78f8f11c3");

	// Token: 0x04003A1F RID: 14879
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Intro_01.prefab:848bac39ba97f25428b7de15ecb5d7fd");

	// Token: 0x04003A20 RID: 14880
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Intro_02 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Intro_02.prefab:ad44613f5980f6f43bacc46f162ee276");

	// Token: 0x04003A21 RID: 14881
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Loss_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Loss_01.prefab:5edb3d3489cd7af40ba0bfc9458fa0e4");

	// Token: 0x04003A22 RID: 14882
	private static readonly AssetReference VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Victory_01 = new AssetReference("VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Victory_01.prefab:50902668a081be54abde248907028f85");

	// Token: 0x04003A23 RID: 14883
	private List<string> m_VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2HeroPowerLines = new List<string>
	{
		BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2HeroPower_01,
		BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2HeroPower_02,
		BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2HeroPower_03
	};

	// Token: 0x04003A24 RID: 14884
	private List<string> m_VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2IdleLines = new List<string>
	{
		BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Idle_01,
		BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Idle_02,
		BoH_Garrosh_02.VO_Story_Hero_Rehgar_Male_Orc_Story_Garrosh_Mission2Idle_03
	};

	// Token: 0x04003A25 RID: 14885
	private HashSet<string> m_playedLines = new HashSet<string>();
}
