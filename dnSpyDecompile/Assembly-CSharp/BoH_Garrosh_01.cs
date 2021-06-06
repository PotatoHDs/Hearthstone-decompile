using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000518 RID: 1304
public class BoH_Garrosh_01 : BoH_Garrosh_Dungeon
{
	// Token: 0x0600469F RID: 18079 RVA: 0x0017CA44 File Offset: 0x0017AC44
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1EmoteResponse_01,
			BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeA_01,
			BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeB_01,
			BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeC_01,
			BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeD_01,
			BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_01,
			BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_02,
			BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_03,
			BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_01,
			BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_02,
			BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_03,
			BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Intro_01,
			BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Loss_01,
			BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Victory_01,
			BoH_Garrosh_01.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeB_01,
			BoH_Garrosh_01.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeC_01,
			BoH_Garrosh_01.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeD_01,
			BoH_Garrosh_01.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1Intro_01,
			BoH_Garrosh_01.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1Victory_01,
			BoH_Garrosh_01.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission1Victory_01,
			BoH_Garrosh_01.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission1Victory_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060046A0 RID: 18080 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060046A1 RID: 18081 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x060046A2 RID: 18082 RVA: 0x0017CBF8 File Offset: 0x0017ADF8
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_01.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060046A3 RID: 18083 RVA: 0x0017CC07 File Offset: 0x0017AE07
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1IdleLines;
	}

	// Token: 0x060046A4 RID: 18084 RVA: 0x0017CC0F File Offset: 0x0017AE0F
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPowerLines;
	}

	// Token: 0x060046A5 RID: 18085 RVA: 0x0017CC17 File Offset: 0x0017AE17
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1EmoteResponse_01;
	}

	// Token: 0x060046A6 RID: 18086 RVA: 0x0017CC30 File Offset: 0x0017AE30
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

	// Token: 0x060046A7 RID: 18087 RVA: 0x0017CCB4 File Offset: 0x0017AEB4
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
		if (missionEvent != 501)
		{
			if (missionEvent != 504)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Victory_01, 2.5f);
			yield return base.PlayLineAlways(BoH_Garrosh_01.ThrallBrassRing, BoH_Garrosh_01.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission1Victory_01, 2.5f);
			yield return base.PlayLineAlways(BoH_Garrosh_01.ThrallBrassRing, BoH_Garrosh_01.VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission1Victory_02, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_01.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1Victory_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x060046A8 RID: 18088 RVA: 0x0017CCCA File Offset: 0x0017AECA
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

	// Token: 0x060046A9 RID: 18089 RVA: 0x0017CCE0 File Offset: 0x0017AEE0
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

	// Token: 0x060046AA RID: 18090 RVA: 0x0017CCF6 File Offset: 0x0017AEF6
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
		case 1:
			yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeA_01, 2.5f);
			break;
		case 3:
			yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_01.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeB_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeB_01, 2.5f);
			break;
		case 5:
			yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeC_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_01.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeC_01, 2.5f);
			break;
		case 7:
			yield return base.PlayLineAlways(friendlyActor, BoH_Garrosh_01.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeD_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeD_01, 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x060046AB RID: 18091 RVA: 0x00176C37 File Offset: 0x00174E37
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BT);
	}

	// Token: 0x040039F7 RID: 14839
	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1EmoteResponse_01 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1EmoteResponse_01.prefab:a27359e78de50f542a3be9d0069cbf1c");

	// Token: 0x040039F8 RID: 14840
	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeA_01 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeA_01.prefab:3bbf297fecdf0db4e8f7726470d124e1");

	// Token: 0x040039F9 RID: 14841
	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeB_01 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeB_01.prefab:4164528d7f86add4198eacd5b97bfffd");

	// Token: 0x040039FA RID: 14842
	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeC_01 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeC_01.prefab:b99434a4f221f5047b1105152e8459f0");

	// Token: 0x040039FB RID: 14843
	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeD_01 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeD_01.prefab:f080169bbbe9e0d48b0246eacc37e652");

	// Token: 0x040039FC RID: 14844
	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_01 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_01.prefab:f4f5905fb2c47f244b8301770c215a07");

	// Token: 0x040039FD RID: 14845
	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_02 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_02.prefab:78de3c71b449b6149b9427935ade9130");

	// Token: 0x040039FE RID: 14846
	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_03 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_03.prefab:32530928a287e5242895725f471e1352");

	// Token: 0x040039FF RID: 14847
	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_01 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_01.prefab:1b5d7cdd0cd0e4840b1bb203f4b10bad");

	// Token: 0x04003A00 RID: 14848
	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_02 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_02.prefab:84252b2aeeca8c145a3e6a6b65821e63");

	// Token: 0x04003A01 RID: 14849
	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_03 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_03.prefab:695fdea4057e5bd408048471af990db1");

	// Token: 0x04003A02 RID: 14850
	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Intro_01 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Intro_01.prefab:7cf2773afbe47d446ba8c0ec745c379b");

	// Token: 0x04003A03 RID: 14851
	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Loss_01 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Loss_01.prefab:fe035b8f20618c44a90fcf452afba8e5");

	// Token: 0x04003A04 RID: 14852
	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Victory_01 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Victory_01.prefab:1c4d40b8cdc53f546b0778a75b80bace");

	// Token: 0x04003A05 RID: 14853
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeB_01.prefab:6646be24734b4c249813b2a66eea0480");

	// Token: 0x04003A06 RID: 14854
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeC_01.prefab:ec8c83688fdfa68419898000a1de5f2c");

	// Token: 0x04003A07 RID: 14855
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeD_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeD_01.prefab:2be3ba75ac5a4b44f9ec18e66e4aa22c");

	// Token: 0x04003A08 RID: 14856
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1Intro_01.prefab:4511ee3c405be304fb844ec17a2ba48d");

	// Token: 0x04003A09 RID: 14857
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission1Victory_01.prefab:83c3bf0b027dd714f97d8099be6bb4d8");

	// Token: 0x04003A0A RID: 14858
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission1Victory_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission1Victory_02.prefab:48bbb4aa8f1d0504483f4c171ee5ca96");

	// Token: 0x04003A0B RID: 14859
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1Victory_01.prefab:99e85ad1471145a448721a7888786b7b");

	// Token: 0x04003A0C RID: 14860
	public static readonly AssetReference ThrallBrassRing = new AssetReference("Thrall_BrassRing_Quote.prefab:962e58c9390b0f842a8b64d0d44cf7b4");

	// Token: 0x04003A0D RID: 14861
	private List<string> m_VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPowerLines = new List<string>
	{
		BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_01,
		BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_02,
		BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_03
	};

	// Token: 0x04003A0E RID: 14862
	private List<string> m_VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1IdleLines = new List<string>
	{
		BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_01,
		BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_02,
		BoH_Garrosh_01.VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_03
	};

	// Token: 0x04003A0F RID: 14863
	private HashSet<string> m_playedLines = new HashSet<string>();
}
