using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000509 RID: 1289
public class BTA_Prologue_Fight_01 : BTA_Prologue_Dungeon
{
	// Token: 0x06004580 RID: 17792 RVA: 0x00177A94 File Offset: 0x00175C94
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Death_01,
			BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_EmoteResponse_01,
			BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_01_01,
			BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_02_01,
			BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_03_01,
			BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_01_01,
			BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_02_01,
			BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_03_01,
			BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_ExchangeA_01_01,
			BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_ExchangeB_01_01,
			BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Intro_01_01,
			BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Loss_01,
			BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Turn2_01,
			BTA_Prologue_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeA_02_01,
			BTA_Prologue_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeB_02_01,
			BTA_Prologue_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeC_02_01,
			BTA_Prologue_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan1_01,
			BTA_Prologue_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan2_01,
			BTA_Prologue_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan3_01,
			BTA_Prologue_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Intro_02_01,
			BTA_Prologue_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Turn1_02_01,
			BTA_Prologue_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Victory_02_01,
			BTA_Prologue_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission1_Turn1_Intro_01_01,
			BTA_Prologue_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission1_Victory_01_01,
			BTA_Prologue_Fight_01.VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission1_ExchangeC_01_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004581 RID: 17793 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004582 RID: 17794 RVA: 0x00177C88 File Offset: 0x00175E88
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_Lines;
	}

	// Token: 0x06004583 RID: 17795 RVA: 0x00177C90 File Offset: 0x00175E90
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_Lines;
	}

	// Token: 0x06004584 RID: 17796 RVA: 0x00177C98 File Offset: 0x00175E98
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Death_01;
	}

	// Token: 0x06004585 RID: 17797 RVA: 0x00177CB0 File Offset: 0x00175EB0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004586 RID: 17798 RVA: 0x00177D0E File Offset: 0x00175F0E
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 100)
		{
			if (missionEvent != 501)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(BTA_Prologue_Fight_01.MalfurionBrassRing, BTA_Prologue_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission1_Victory_01_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Victory_02_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan1_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004587 RID: 17799 RVA: 0x00177D24 File Offset: 0x00175F24
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

	// Token: 0x06004588 RID: 17800 RVA: 0x00177D3A File Offset: 0x00175F3A
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
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "Prologue_ChaosNova"))
		{
			if (cardId == "Prologue_ManaBurn")
			{
				yield return base.PlayLineAlways(actor, BTA_Prologue_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan2_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Prologue_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan3_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004589 RID: 17801 RVA: 0x00177D50 File Offset: 0x00175F50
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
			yield return base.PlayLineAlways(BTA_Prologue_Fight_01.MalfurionBrassRing, BTA_Prologue_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission1_Turn1_Intro_01_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Turn1_02_01, 2.5f);
			break;
		case 2:
			yield return base.PlayLineAlways(actor, BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Turn2_01, 2.5f);
			break;
		case 4:
			yield return base.PlayLineAlways(actor, BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_ExchangeA_01_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeA_02_01, 2.5f);
			break;
		case 6:
			yield return base.PlayLineAlways(actor, BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_ExchangeB_01_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeB_02_01, 2.5f);
			break;
		case 7:
			yield return base.PlayLineAlways(BTA_Prologue_Fight_01.TyrandeBrassRing, BTA_Prologue_Fight_01.VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission1_ExchangeC_01_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BTA_Prologue_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeC_02_01, 2.5f);
			break;
		case 11:
			yield return base.PlayLineAlways(actor, BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Loss_01, 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x04003841 RID: 14401
	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Death_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Death_01.prefab:b116d4fe7f1d2aa41a293473461f3e36");

	// Token: 0x04003842 RID: 14402
	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_EmoteResponse_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_EmoteResponse_01.prefab:18612a4888476a7458e21cc8cfec740a");

	// Token: 0x04003843 RID: 14403
	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_01_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_01_01.prefab:7ef550bfd0dcbfd468f7db8c81e35be0");

	// Token: 0x04003844 RID: 14404
	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_02_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_02_01.prefab:082aa46eeed9c4d41b8f2b5036350b09");

	// Token: 0x04003845 RID: 14405
	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_03_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_03_01.prefab:e5ba2f11c0263754aa8471fb979c4e36");

	// Token: 0x04003846 RID: 14406
	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_01_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_01_01.prefab:c8e06e81e3cb3914e9bbb7ec915f88d7");

	// Token: 0x04003847 RID: 14407
	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_02_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_02_01.prefab:5cdfc42bc4a478346ba84da891736536");

	// Token: 0x04003848 RID: 14408
	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_03_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_03_01.prefab:3b90ad121c4598846a05db76b9be6d42");

	// Token: 0x04003849 RID: 14409
	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_ExchangeA_01_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_ExchangeA_01_01.prefab:6a80e52701a77564694bc6eb45b2a134");

	// Token: 0x0400384A RID: 14410
	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_ExchangeB_01_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_ExchangeB_01_01.prefab:665469cd255111c43bbe8e7b36426483");

	// Token: 0x0400384B RID: 14411
	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Intro_01_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Intro_01_01.prefab:8935d821332350e43a354c33a69ceec3");

	// Token: 0x0400384C RID: 14412
	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Loss_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Loss_01.prefab:962b6f348de84524e84ce0f67d6383a0");

	// Token: 0x0400384D RID: 14413
	private static readonly AssetReference VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Turn2_01 = new AssetReference("VO_Prologue_Azzinoth_Male_Demon_Prologue_Mission1_Turn2_01.prefab:7c92ea0283e6be04593365633abdfdf6");

	// Token: 0x0400384E RID: 14414
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeA_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeA_02_01.prefab:131dfa98a79df9d43abf851b7b7c1028");

	// Token: 0x0400384F RID: 14415
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeB_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeB_02_01.prefab:2e77604e8c0d0b943aac30086296eb81");

	// Token: 0x04003850 RID: 14416
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeC_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_ExchangeC_02_01.prefab:65852e84660aadc4cadd8f214411102e");

	// Token: 0x04003851 RID: 14417
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan1_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan1_01.prefab:dcfa6e38dc5bc664f887779149def147");

	// Token: 0x04003852 RID: 14418
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan2_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan2_01.prefab:7a77c84195ba7d44b81515da355631fe");

	// Token: 0x04003853 RID: 14419
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan3_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Illidan3_01.prefab:4d8c91e0dd96d9d43a33cf245ebfc83e");

	// Token: 0x04003854 RID: 14420
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Intro_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Intro_02_01.prefab:ebb76883e6ac52047b1029f9ed3dbf97");

	// Token: 0x04003855 RID: 14421
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Turn1_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Turn1_02_01.prefab:4cc2dd12fa63f9c4c93a937bbcf6e1eb");

	// Token: 0x04003856 RID: 14422
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Victory_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission1_Victory_02_01.prefab:544f9254dde3b5540b56c0a737eebb66");

	// Token: 0x04003857 RID: 14423
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission1_Turn1_Intro_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission1_Turn1_Intro_01_01.prefab:277b830d6d8ac234da940152151e5678");

	// Token: 0x04003858 RID: 14424
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission1_Victory_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission1_Victory_01_01.prefab:fe25c39b7c29e854f8f2825afc5ca366");

	// Token: 0x04003859 RID: 14425
	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission1_ExchangeC_01_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission1_ExchangeC_01_01.prefab:09ee5546a47f33243bfb84de80de2bf1");

	// Token: 0x0400385A RID: 14426
	public static readonly AssetReference TyrandeBrassRing = new AssetReference("YoungTyrande_Popup_BrassRing.prefab:79f13833a3f5e97449ef744f460e9fbd");

	// Token: 0x0400385B RID: 14427
	public static readonly AssetReference MalfurionBrassRing = new AssetReference("YoungMalfurion_Popup_BrassRing.prefab:5544ac85196277542a7fa0b1a9b578df");

	// Token: 0x0400385C RID: 14428
	private List<string> m_VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_Lines = new List<string>
	{
		BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_01_01,
		BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_02_01,
		BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_HeroPower_03_01
	};

	// Token: 0x0400385D RID: 14429
	private List<string> m_VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_Lines = new List<string>
	{
		BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_01_01,
		BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_02_01,
		BTA_Prologue_Fight_01.VO_Prologue_Azzinoth_Male_Demon_Prologue_Azzinoth_Idle_03_01
	};

	// Token: 0x0400385E RID: 14430
	private HashSet<string> m_playedLines = new HashSet<string>();
}
