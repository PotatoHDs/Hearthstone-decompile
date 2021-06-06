using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200051A RID: 1306
public class BoH_Garrosh_03 : BoH_Garrosh_Dungeon
{
	// Token: 0x060046C3 RID: 18115 RVA: 0x0017D390 File Offset: 0x0017B590
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Garrosh_03.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3ExchangeA_01,
			BoH_Garrosh_03.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3ExchangeB_01,
			BoH_Garrosh_03.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Intro_01,
			BoH_Garrosh_03.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3PeonInfo_01,
			BoH_Garrosh_03.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_01,
			BoH_Garrosh_03.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_02,
			BoH_Garrosh_03.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_03,
			BoH_Garrosh_03.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Victory_01,
			BoH_Garrosh_03.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission3Victory_01,
			BoH_Garrosh_03.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission3Victory_02,
			BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Death_01,
			BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3EmoteResponse_01,
			BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3ExchangeA_01,
			BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3ExchangeB_01,
			BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_01,
			BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_02,
			BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_03,
			BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_01,
			BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_02,
			BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_03,
			BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Intro_01,
			BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Loss_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060046C4 RID: 18116 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060046C5 RID: 18117 RVA: 0x0017D554 File Offset: 0x0017B754
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_03.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060046C6 RID: 18118 RVA: 0x0017D563 File Offset: 0x0017B763
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3IdleLines;
	}

	// Token: 0x060046C7 RID: 18119 RVA: 0x0017D56B File Offset: 0x0017B76B
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPowerLines;
	}

	// Token: 0x060046C8 RID: 18120 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x060046C9 RID: 18121 RVA: 0x0017D573 File Offset: 0x0017B773
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3EmoteResponse_01;
	}

	// Token: 0x060046CA RID: 18122 RVA: 0x0017D58C File Offset: 0x0017B78C
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

	// Token: 0x060046CB RID: 18123 RVA: 0x0017D610 File Offset: 0x0017B810
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 501:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(BoH_Garrosh_03.ThrallBrassRing, BoH_Garrosh_03.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission3Victory_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_03.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Victory_01, 2.5f);
			yield return base.PlayLineAlways(BoH_Garrosh_03.ThrallBrassRing, BoH_Garrosh_03.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission3Victory_02, 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 502:
			yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_03.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3PeonInfo_01, 2.5f);
			break;
		case 503:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(friendlyActor, this.m_VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3SummonLines);
			break;
		case 504:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Loss_01, 2.5f);
			GameState.Get().SetBusy(false);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x060046CC RID: 18124 RVA: 0x0017D626 File Offset: 0x0017B826
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

	// Token: 0x060046CD RID: 18125 RVA: 0x0017D63C File Offset: 0x0017B83C
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

	// Token: 0x060046CE RID: 18126 RVA: 0x0017D652 File Offset: 0x0017B852
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 3)
		{
			if (turn != 6)
			{
				if (turn == 12)
				{
					yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3ExchangeB_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_03.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3ExchangeB_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_03.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3ExchangeA_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3ExchangeA_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_03.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3PeonInfo_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060046CF RID: 18127 RVA: 0x0011204E File Offset: 0x0011024E
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ICC);
	}

	// Token: 0x04003A26 RID: 14886
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3ExchangeA_01.prefab:fb92a428b9f4e844da8a8ad6b2541581");

	// Token: 0x04003A27 RID: 14887
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3ExchangeB_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3ExchangeB_01.prefab:f99ac4d129232824089b0a40f3750ab2");

	// Token: 0x04003A28 RID: 14888
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Intro_01.prefab:fe4c11db6a87e524281c824bb7a2660c");

	// Token: 0x04003A29 RID: 14889
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3PeonInfo_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3PeonInfo_01.prefab:4bcbd90aa9b1f69439d40b24123cb372");

	// Token: 0x04003A2A RID: 14890
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_01.prefab:30e3e1c737fd6c9488f14e86fb555fa3");

	// Token: 0x04003A2B RID: 14891
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_02.prefab:c3ed48d79fff96e4bb94d4dff56975d3");

	// Token: 0x04003A2C RID: 14892
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_03.prefab:4ca1ae8051631cc4bbc1ad2cf2e66022");

	// Token: 0x04003A2D RID: 14893
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Victory_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Victory_01.prefab:dc621d5d237b4444386bb2633291ca19");

	// Token: 0x04003A2E RID: 14894
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission3Victory_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission3Victory_01.prefab:b5f4771b45938c74da73cba402f6e036");

	// Token: 0x04003A2F RID: 14895
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission3Victory_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission3Victory_02.prefab:1f72a59963a802f4c861320b8f7a2eb0");

	// Token: 0x04003A30 RID: 14896
	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Death_01 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Death_01.prefab:3b59bf02e51569c49920c1c08446398e");

	// Token: 0x04003A31 RID: 14897
	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3EmoteResponse_01.prefab:96c6ee4af11cea44f8772f5ce784e5a7");

	// Token: 0x04003A32 RID: 14898
	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3ExchangeA_01 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3ExchangeA_01.prefab:a70a8873cf32f2c449edbf837c335f80");

	// Token: 0x04003A33 RID: 14899
	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3ExchangeB_01 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3ExchangeB_01.prefab:62e6a12eeadc09541aacb9d8f022d4f3");

	// Token: 0x04003A34 RID: 14900
	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_01 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_01.prefab:585a78675061e324a9c6cef981e703b6");

	// Token: 0x04003A35 RID: 14901
	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_02 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_02.prefab:1970ce556f8bba74d992d394446527ed");

	// Token: 0x04003A36 RID: 14902
	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_03 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_03.prefab:48fd3a86ef03f5748a4d712eeac9ff7d");

	// Token: 0x04003A37 RID: 14903
	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_01 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_01.prefab:cf186ca5b6437324a97a3bb787afae14");

	// Token: 0x04003A38 RID: 14904
	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_02 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_02.prefab:edb434646b5853345b719aed984b4225");

	// Token: 0x04003A39 RID: 14905
	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_03 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_03.prefab:2b074c522c8b08a40b810eeb1bb4320f");

	// Token: 0x04003A3A RID: 14906
	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Intro_01.prefab:1cadd5d82cb823144b97d42fc809cbe2");

	// Token: 0x04003A3B RID: 14907
	private static readonly AssetReference VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Loss_01 = new AssetReference("VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Loss_01.prefab:d09753c0867ecb0418dbaa51fa2c5105");

	// Token: 0x04003A3C RID: 14908
	public static readonly AssetReference ThrallBrassRing = new AssetReference("Thrall_BrassRing_Quote.prefab:962e58c9390b0f842a8b64d0d44cf7b4");

	// Token: 0x04003A3D RID: 14909
	private List<string> m_VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3SummonLines = new List<string>
	{
		BoH_Garrosh_03.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_01,
		BoH_Garrosh_03.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_02,
		BoH_Garrosh_03.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission3Summon_03
	};

	// Token: 0x04003A3E RID: 14910
	private List<string> m_VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPowerLines = new List<string>
	{
		BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_01,
		BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_02,
		BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3HeroPower_03
	};

	// Token: 0x04003A3F RID: 14911
	private List<string> m_VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3IdleLines = new List<string>
	{
		BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_01,
		BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_02,
		BoH_Garrosh_03.VO_Story_Hero_Zarzhet_Female_Nerubian_Story_Garrosh_Mission3Idle_03
	};

	// Token: 0x04003A40 RID: 14912
	private HashSet<string> m_playedLines = new HashSet<string>();
}
