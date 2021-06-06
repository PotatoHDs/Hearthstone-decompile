using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200050B RID: 1291
public class BTA_Prologue_Fight_03 : BTA_Prologue_Dungeon
{
	// Token: 0x060045A4 RID: 17828 RVA: 0x00178740 File Offset: 0x00176940
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Death_01,
			BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_EmoteResponse_01,
			BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_01_01,
			BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_02_01,
			BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_03_01,
			BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Idle_01_01,
			BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Idle_02_01,
			BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Idle_03_01,
			BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_ExchangeA_01,
			BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_ExchangeB_01_01,
			BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_ExchangeC_01_01,
			BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_Intro01_01,
			BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_Loss_01,
			BTA_Prologue_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_ExchangeB_02_01,
			BTA_Prologue_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_ExchangeC_02_01,
			BTA_Prologue_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_ExchangeD_02_01,
			BTA_Prologue_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Intro03_01,
			BTA_Prologue_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Turn1_02_01,
			BTA_Prologue_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Turn2_01_01,
			BTA_Prologue_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Victory_01_01,
			BTA_Prologue_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_PreMission3_Intro01_01,
			BTA_Prologue_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_PreMission3_Intro02_01,
			BTA_Prologue_Fight_03.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_ExchangeD_01_01,
			BTA_Prologue_Fight_03.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Turn1_01_01,
			BTA_Prologue_Fight_03.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Turn2_02_01,
			BTA_Prologue_Fight_03.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Victory_02_01,
			BTA_Prologue_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_PreMission4_Intro_01_01,
			BTA_Prologue_Fight_03.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_PreMission4_Intro_02_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060045A5 RID: 17829 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060045A6 RID: 17830 RVA: 0x00178964 File Offset: 0x00176B64
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Idle_Lines;
	}

	// Token: 0x060045A7 RID: 17831 RVA: 0x0017896C File Offset: 0x00176B6C
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_Lines;
	}

	// Token: 0x060045A8 RID: 17832 RVA: 0x00178974 File Offset: 0x00176B74
	protected override void OnBossHeroPowerPlayed(Entity entity)
	{
		float num = this.ChanceToPlayBossHeroPowerVOLine();
		float num2 = UnityEngine.Random.Range(0f, 1f);
		if (this.m_enemySpeaking)
		{
			return;
		}
		if (num < num2)
		{
			return;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (actor == null)
		{
			return;
		}
		List<string> bossHeroPowerRandomLines = this.GetBossHeroPowerRandomLines();
		string text = "";
		while (bossHeroPowerRandomLines.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, bossHeroPowerRandomLines.Count);
			text = bossHeroPowerRandomLines[index];
			bossHeroPowerRandomLines.RemoveAt(index);
			if (!NotificationManager.Get().HasSoundPlayedThisSession(text))
			{
				break;
			}
		}
		if (text == "")
		{
			return;
		}
		if (text == BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_Loss_01)
		{
			base.PlaySound(text, 1f, true, false);
			return;
		}
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce(text, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
	}

	// Token: 0x060045A9 RID: 17833 RVA: 0x00178A6B File Offset: 0x00176C6B
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Death_01;
	}

	// Token: 0x060045AA RID: 17834 RVA: 0x00178A84 File Offset: 0x00176C84
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060045AB RID: 17835 RVA: 0x00178AE2 File Offset: 0x00176CE2
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 501)
		{
			switch (missionEvent)
			{
			case 601:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(BTA_Prologue_Fight_03.MalfurionBrassRing, BTA_Prologue_Fight_03.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Turn1_01_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Turn1_02_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_4C7;
			case 602:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Turn2_01_01, 2.5f);
				yield return base.PlayLineAlways(BTA_Prologue_Fight_03.MalfurionBrassRing, BTA_Prologue_Fight_03.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Turn2_02_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_4C7;
			case 603:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(BTA_Prologue_Fight_03.MalfurionBrassRing, BTA_Prologue_Fight_03.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_ExchangeD_01_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_ExchangeD_02_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_4C7;
			case 605:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_ExchangeC_01_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_ExchangeC_02_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_4C7;
			case 606:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_ExchangeB_01_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_ExchangeB_02_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_4C7;
			case 607:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_ExchangeA_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_4C7;
			}
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Victory_01_01, 2.5f);
			yield return base.PlayLineAlways(BTA_Prologue_Fight_03.MalfurionBrassRing, BTA_Prologue_Fight_03.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Victory_02_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_PreMission4_Intro_01_01, 2.5f);
			yield return base.PlayLineAlways(BTA_Prologue_Fight_03.MalfurionBrassRing, BTA_Prologue_Fight_03.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_PreMission4_Intro_02_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		IL_4C7:
		yield break;
	}

	// Token: 0x060045AC RID: 17836 RVA: 0x00178AF8 File Offset: 0x00176CF8
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

	// Token: 0x060045AD RID: 17837 RVA: 0x00178B0E File Offset: 0x00176D0E
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

	// Token: 0x060045AE RID: 17838 RVA: 0x00178B24 File Offset: 0x00176D24
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x04003882 RID: 14466
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Death_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Death_01.prefab:b6a55707d36999646b64f240eedc8024");

	// Token: 0x04003883 RID: 14467
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_EmoteResponse_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_EmoteResponse_01.prefab:378dc07a23179714c8fe918124abc111");

	// Token: 0x04003884 RID: 14468
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_01_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_01_01.prefab:02dcb71f3b441f14ca1d5b9fd3ea548c");

	// Token: 0x04003885 RID: 14469
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_02_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_02_01.prefab:63a8af3bbd8c4184eacfe337af95f915");

	// Token: 0x04003886 RID: 14470
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_03_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_03_01.prefab:2c97029f2379e0942988e405391a3a94");

	// Token: 0x04003887 RID: 14471
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Idle_01_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Idle_01_01.prefab:7d53b1c9b32145c40857178a8e7557b9");

	// Token: 0x04003888 RID: 14472
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Idle_02_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Idle_02_01.prefab:cce53bb4c3100d847a8ee6f60aa7c2a9");

	// Token: 0x04003889 RID: 14473
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Idle_03_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Idle_03_01.prefab:dd95db906aa9ab64485eea53cec6c25f");

	// Token: 0x0400388A RID: 14474
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_ExchangeA_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_ExchangeA_01.prefab:14572c61115e4f6468f78106f4fecdf9");

	// Token: 0x0400388B RID: 14475
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_ExchangeB_01_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_ExchangeB_01_01.prefab:390deb6b13c6333458f7a76cfc88d859");

	// Token: 0x0400388C RID: 14476
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_ExchangeC_01_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_ExchangeC_01_01.prefab:8964e50a67f91bb4696c9789507728a1");

	// Token: 0x0400388D RID: 14477
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_Intro01_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_Intro01_01.prefab:5a3d6d17ae3583146be8ed89bf239eb2");

	// Token: 0x0400388E RID: 14478
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_Loss_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_Loss_01.prefab:97e8b9d8a1d2e044fa79f5fca38cc05e");

	// Token: 0x0400388F RID: 14479
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_ExchangeB_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_ExchangeB_02_01.prefab:8ad0c499e218b394abd8da1ae28b8088");

	// Token: 0x04003890 RID: 14480
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_ExchangeC_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_ExchangeC_02_01.prefab:ca3b4823ab2ef0847ac1566dc5c33aa0");

	// Token: 0x04003891 RID: 14481
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_ExchangeD_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_ExchangeD_02_01.prefab:5bbd35b5b51bf604c84daf23774d29f7");

	// Token: 0x04003892 RID: 14482
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Intro03_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Intro03_01.prefab:ef5a638c1cd33c44686f8a5feb8afc0f");

	// Token: 0x04003893 RID: 14483
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Turn1_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Turn1_02_01.prefab:2e986a1010f324142b518a5ed272c477");

	// Token: 0x04003894 RID: 14484
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Turn2_01_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Turn2_01_01.prefab:9be734993b596304abe45ac010e36dc3");

	// Token: 0x04003895 RID: 14485
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Victory_01_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Victory_01_01.prefab:050b81b9d5ac7d54882021ac63d8d3c5");

	// Token: 0x04003896 RID: 14486
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_PreMission3_Intro01_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_PreMission3_Intro01_01.prefab:f041d10dbcffa9c41bc6fbc17273f43b");

	// Token: 0x04003897 RID: 14487
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_PreMission3_Intro02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_PreMission3_Intro02_01.prefab:ad099ea97d81efd43b82ac57eafcb4b2");

	// Token: 0x04003898 RID: 14488
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_ExchangeD_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_ExchangeD_01_01.prefab:4fa0a3331b13c5a4896a15672f54aee5");

	// Token: 0x04003899 RID: 14489
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Turn1_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Turn1_01_01.prefab:bdb4321f03e9d444b9a97f2c31a4e47e");

	// Token: 0x0400389A RID: 14490
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Turn2_02_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Turn2_02_01.prefab:75c105dbc65af55428b800aaf3a4db9b");

	// Token: 0x0400389B RID: 14491
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Victory_02_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Victory_02_01.prefab:4641eccade5c2074fb147680a96912d0");

	// Token: 0x0400389C RID: 14492
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_PreMission4_Intro_01_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_PreMission4_Intro_01_01.prefab:edf38deebdf0f7242b98f20339352507");

	// Token: 0x0400389D RID: 14493
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_PreMission4_Intro_02_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_PreMission4_Intro_02_01.prefab:31c20bb2df92f2c4aba657f6b5cccf6c");

	// Token: 0x0400389E RID: 14494
	public static readonly AssetReference MalfurionBrassRing = new AssetReference("YoungMalfurion_Popup_BrassRing.prefab:5544ac85196277542a7fa0b1a9b578df");

	// Token: 0x0400389F RID: 14495
	private List<string> m_VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_Lines = new List<string>
	{
		BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_01_01,
		BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_02_01,
		BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_03_01,
		BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_Loss_01
	};

	// Token: 0x040038A0 RID: 14496
	private List<string> m_VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Idle_Lines = new List<string>
	{
		BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Idle_01_01,
		BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Idle_02_01,
		BTA_Prologue_Fight_03.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Idle_03_01
	};

	// Token: 0x040038A1 RID: 14497
	private HashSet<string> m_playedLines = new HashSet<string>();
}
