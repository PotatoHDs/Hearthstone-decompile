using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200050C RID: 1292
public class BTA_Prologue_Fight_04 : BTA_Prologue_Dungeon
{
	// Token: 0x060045B4 RID: 17844 RVA: 0x00178D98 File Offset: 0x00176F98
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Death_01,
			BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_EmoteResponse_01,
			BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_01_01,
			BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_02_01,
			BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_03_01,
			BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_01_01,
			BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_02_01,
			BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_03_01,
			BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_VictoryDenied_01,
			BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Intro_01_01,
			BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Loss_01,
			BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Turn2_01_01,
			BTA_Prologue_Fight_04.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeA_01,
			BTA_Prologue_Fight_04.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeB_01,
			BTA_Prologue_Fight_04.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeC_02_01,
			BTA_Prologue_Fight_04.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Intro_02_01,
			BTA_Prologue_Fight_04.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Turn1_02_01,
			BTA_Prologue_Fight_04.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory01_01,
			BTA_Prologue_Fight_04.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory02_01,
			BTA_Prologue_Fight_04.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory03_01,
			BTA_Prologue_Fight_04.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory04_01,
			BTA_Prologue_Fight_04.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_PreMission4_Intro_01_01,
			BTA_Prologue_Fight_04.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_01_01,
			BTA_Prologue_Fight_04.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_02_01,
			BTA_Prologue_Fight_04.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_Shocked_at_Illidan_02_01,
			BTA_Prologue_Fight_04.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Turn1_01_01,
			BTA_Prologue_Fight_04.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_PreMission4_Intro_02_01,
			BTA_Prologue_Fight_04.VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_ExchangeC_01_01,
			BTA_Prologue_Fight_04.VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_Shocked_at_Illidan_01_01,
			BTA_Prologue_Fight_04.VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_Shocked_at_Illidan_02_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060045B5 RID: 17845 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060045B6 RID: 17846 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x060045B7 RID: 17847 RVA: 0x00178FDC File Offset: 0x001771DC
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_Lines;
	}

	// Token: 0x060045B8 RID: 17848 RVA: 0x00178FE4 File Offset: 0x001771E4
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_Lines;
	}

	// Token: 0x060045B9 RID: 17849 RVA: 0x00178FEC File Offset: 0x001771EC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Death_01;
	}

	// Token: 0x060045BA RID: 17850 RVA: 0x00179004 File Offset: 0x00177204
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060045BB RID: 17851 RVA: 0x00179062 File Offset: 0x00177262
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 100)
		{
			if (missionEvent != 101)
			{
				if (missionEvent != 501)
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
				else
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(actor, BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_VictoryDenied_01, 2.5f);
					yield return base.PlayLineAlways(BTA_Prologue_Fight_04.IllidanBrassRing, BTA_Prologue_Fight_04.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory03_01, 2.5f);
					yield return base.PlayLineAlways(BTA_Prologue_Fight_04.IllidanBrassRing, BTA_Prologue_Fight_04.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory04_01, 2.5f);
					GameState.Get().SetBusy(false);
				}
			}
			else
			{
				yield return base.PlayLineAlways(BTA_Prologue_Fight_04.MalfurionBrassRing, BTA_Prologue_Fight_04.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_02_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(BTA_Prologue_Fight_04.MalfurionBrassRing, BTA_Prologue_Fight_04.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_01_01, 2.5f);
			yield return base.PlayLineAlways(BTA_Prologue_Fight_04.MalfurionBrassRing, BTA_Prologue_Fight_04.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_02_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060045BC RID: 17852 RVA: 0x00179078 File Offset: 0x00177278
	public IEnumerator PlayVictoryLines()
	{
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BTA_Prologue_Fight_04.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory01_01, 2.5f);
		yield return base.PlayLineAlways(BTA_Prologue_Fight_04.IllidanBrassRing, BTA_Prologue_Fight_04.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory02_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060045BD RID: 17853 RVA: 0x00179087 File Offset: 0x00177287
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
		if (!(cardId == "BT_430"))
		{
			if (cardId == "BT_512")
			{
				yield return base.PlayLineAlways(BTA_Prologue_Fight_04.TyrandeBrassRing, BTA_Prologue_Fight_04.VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_Shocked_at_Illidan_02_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(BTA_Prologue_Fight_04.TyrandeBrassRing, BTA_Prologue_Fight_04.VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_Shocked_at_Illidan_01_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060045BE RID: 17854 RVA: 0x0017909D File Offset: 0x0017729D
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
		yield break;
	}

	// Token: 0x060045BF RID: 17855 RVA: 0x001790B3 File Offset: 0x001772B3
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (turn)
		{
		case 1:
			yield return base.PlayLineAlways(BTA_Prologue_Fight_04.MalfurionBrassRing, BTA_Prologue_Fight_04.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Turn1_01_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_04.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Turn1_02_01, 2.5f);
			break;
		case 5:
			yield return base.PlayLineAlways(BTA_Prologue_Fight_04.TyrandeBrassRing, BTA_Prologue_Fight_04.VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_ExchangeC_01_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_04.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeC_02_01, 2.5f);
			break;
		case 6:
			yield return base.PlayLineAlways(actor, BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Turn2_01_01, 2.5f);
			break;
		case 7:
			yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_04.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeA_01, 2.5f);
			break;
		case 9:
			yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_04.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeB_01, 2.5f);
			break;
		case 10:
			yield return base.PlayLineAlways(BTA_Prologue_Fight_04.MalfurionBrassRing, BTA_Prologue_Fight_04.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_Shocked_at_Illidan_02_01, 2.5f);
			break;
		case 11:
			yield return base.PlayLineAlways(actor, BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Loss_01, 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x060045C0 RID: 17856 RVA: 0x00173DAB File Offset: 0x00171FAB
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DHPrologueBoss);
	}

	// Token: 0x040038A2 RID: 14498
	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Death_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Death_01.prefab:bda78fc04409a744caf0eadf24de8ad3");

	// Token: 0x040038A3 RID: 14499
	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_EmoteResponse_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_EmoteResponse_01.prefab:a89ac351649b3ba4199cbf6ce5d98115");

	// Token: 0x040038A4 RID: 14500
	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_01_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_01_01.prefab:d709b0d41c836994ea8f9a306c4fb5fa");

	// Token: 0x040038A5 RID: 14501
	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_02_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_02_01.prefab:5ad535b39059dfd4a985822ddd8f14c3");

	// Token: 0x040038A6 RID: 14502
	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_03_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_03_01.prefab:3b6849fc7802c904ca47756574db5586");

	// Token: 0x040038A7 RID: 14503
	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_01_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_01_01.prefab:8667ecc5c7790f34690e30edbb02c5b6");

	// Token: 0x040038A8 RID: 14504
	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_02_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_02_01.prefab:3f5b4b42e76a61841ab189eba591467b");

	// Token: 0x040038A9 RID: 14505
	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_03_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_03_01.prefab:c41c0562aff85d84095a087388d058dd");

	// Token: 0x040038AA RID: 14506
	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_VictoryDenied_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_VictoryDenied_01.prefab:1c8dce2472478624c94c3e6724756b52");

	// Token: 0x040038AB RID: 14507
	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Intro_01_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Intro_01_01.prefab:2d65e89207967c345b5cc266d4bf1132");

	// Token: 0x040038AC RID: 14508
	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Loss_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Loss_01.prefab:a535ac5f5ed9a8b4e8914e953b5cdcab");

	// Token: 0x040038AD RID: 14509
	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Turn2_01_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Turn2_01_01.prefab:52445d793154e1349806280df9847140");

	// Token: 0x040038AE RID: 14510
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeA_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeA_01.prefab:6bd9109a56494b64ab5470b701c98e73");

	// Token: 0x040038AF RID: 14511
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeB_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeB_01.prefab:ffa43268a21de49448f0bf541efc757d");

	// Token: 0x040038B0 RID: 14512
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeC_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeC_02_01.prefab:1110d2427a78b1247a4a6f0808b92f90");

	// Token: 0x040038B1 RID: 14513
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Intro_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Intro_02_01.prefab:47a87745f40e6614aa04b9713ff41900");

	// Token: 0x040038B2 RID: 14514
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Turn1_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Turn1_02_01.prefab:025ebb519e1b1d0458fd38ae3d67c9b6");

	// Token: 0x040038B3 RID: 14515
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory01_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory01_01.prefab:02f5d3f496785184a9db2e7787320ff9");

	// Token: 0x040038B4 RID: 14516
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory02_01.prefab:0a74f62cf504d704f996f6a2c8c0a323");

	// Token: 0x040038B5 RID: 14517
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory03_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory03_01.prefab:e244e3714595a914fbde9bc4456e4fd4");

	// Token: 0x040038B6 RID: 14518
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory04_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Victory04_01.prefab:de7f2efcb115d784486d4efb31b6891f");

	// Token: 0x040038B7 RID: 14519
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_PreMission4_Intro_01_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_PreMission4_Intro_01_01.prefab:edf38deebdf0f7242b98f20339352507");

	// Token: 0x040038B8 RID: 14520
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_01_01.prefab:7e85d5356eb5ad9408188b73aba53220");

	// Token: 0x040038B9 RID: 14521
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_02_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_02_01.prefab:58fa059877f95cb469dd52c66e4193b9");

	// Token: 0x040038BA RID: 14522
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_Shocked_at_Illidan_02_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Malfurion_Shocked_at_Illidan_02_01.prefab:db6d711d63bd5694eb2053399da5ba66");

	// Token: 0x040038BB RID: 14523
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Turn1_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Turn1_01_01.prefab:d0ab7c13fe71848479f487e4464654e8");

	// Token: 0x040038BC RID: 14524
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_PreMission4_Intro_02_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_PreMission4_Intro_02_01.prefab:31c20bb2df92f2c4aba657f6b5cccf6c");

	// Token: 0x040038BD RID: 14525
	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_ExchangeC_01_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_ExchangeC_01_01.prefab:51c9801d61f6742438e62bd5fdddc970");

	// Token: 0x040038BE RID: 14526
	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_Shocked_at_Illidan_01_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_Shocked_at_Illidan_01_01.prefab:07cf70a55274df346b0d4313605f0926");

	// Token: 0x040038BF RID: 14527
	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_Shocked_at_Illidan_02_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_Shocked_at_Illidan_02_01.prefab:a4a6900d484fd754ca17f0e29f8a8be2");

	// Token: 0x040038C0 RID: 14528
	public static readonly AssetReference IllidanBrassRing = new AssetReference("DemonHunter_Illidan_Popup_BrassRing.prefab:8c007b8e8be417c4fbd9738960e6f7f0");

	// Token: 0x040038C1 RID: 14529
	public static readonly AssetReference TyrandeBrassRing = new AssetReference("YoungTyrande_Popup_BrassRing.prefab:79f13833a3f5e97449ef744f460e9fbd");

	// Token: 0x040038C2 RID: 14530
	public static readonly AssetReference MalfurionBrassRing = new AssetReference("YoungMalfurion_Popup_BrassRing.prefab:5544ac85196277542a7fa0b1a9b578df");

	// Token: 0x040038C3 RID: 14531
	private List<string> m_VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_Lines = new List<string>
	{
		BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_01_01,
		BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_02_01,
		BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_HeroPower_03_01
	};

	// Token: 0x040038C4 RID: 14532
	private List<string> m_VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_Lines = new List<string>
	{
		BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_01_01,
		BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_02_01,
		BTA_Prologue_Fight_04.VO_Prologue_Cenarius_Male_Demigod_Prologue_Cenarius_Idle_03_01
	};

	// Token: 0x040038C5 RID: 14533
	private HashSet<string> m_playedLines = new HashSet<string>();
}
