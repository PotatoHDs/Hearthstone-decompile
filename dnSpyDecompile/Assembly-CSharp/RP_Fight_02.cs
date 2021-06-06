using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000595 RID: 1429
public class RP_Fight_02 : RP_Dungeon
{
	// Token: 0x06004F63 RID: 20323 RVA: 0x001A10F0 File Offset: 0x0019F2F0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			RP_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeA_02_01,
			RP_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeB_02_01,
			RP_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeC_01_01,
			RP_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Intro02_01,
			RP_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Turn01_Response_01,
			RP_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Turn02_Response_01,
			RP_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Victory02_01,
			RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeA_01_01,
			RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeB_01_01,
			RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeC_02_01,
			RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Intro01_01,
			RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Turn01_Intro_01,
			RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Turn02_Intro_01,
			RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Victory01_01,
			RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_EmoteResponse_01,
			RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_01_01,
			RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_02_01,
			RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_03_01,
			RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_Idle01_01,
			RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_Idle02_01,
			RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_Idle03_01,
			RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Loss_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004F64 RID: 20324 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004F65 RID: 20325 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004F66 RID: 20326 RVA: 0x001A12B4 File Offset: 0x0019F4B4
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_Lines;
	}

	// Token: 0x06004F67 RID: 20327 RVA: 0x001A0DF0 File Offset: 0x0019EFF0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
	}

	// Token: 0x06004F68 RID: 20328 RVA: 0x001A12BC File Offset: 0x0019F4BC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(RP_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Intro02_01, Notification.SpeechBubbleDirection.BottomLeft, actor2, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004F69 RID: 20329 RVA: 0x001A1362 File Offset: 0x0019F562
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
			if (missionEvent != 504)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Victory01_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, RP_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Victory02_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004F6A RID: 20330 RVA: 0x001A1378 File Offset: 0x0019F578
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

	// Token: 0x06004F6B RID: 20331 RVA: 0x001A138E File Offset: 0x0019F58E
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

	// Token: 0x06004F6C RID: 20332 RVA: 0x001A13A4 File Offset: 0x0019F5A4
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn <= 3)
		{
			if (turn != 1)
			{
				if (turn == 3)
				{
					yield return base.PlayLineAlways(enemyActor, RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Turn02_Intro_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, RP_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Turn02_Response_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(enemyActor, RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Turn01_Intro_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, RP_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Turn01_Response_01, 2.5f);
			}
		}
		else if (turn != 6)
		{
			if (turn != 11)
			{
				if (turn == 15)
				{
					yield return base.PlayLineAlways(friendlyActor, RP_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeC_01_01, 2.5f);
					yield return base.PlayLineAlways(enemyActor, RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeC_02_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(enemyActor, RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeB_01_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, RP_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeB_02_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(enemyActor, RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeA_01_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, RP_Fight_02.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeA_02_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0400457D RID: 17789
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeA_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeA_02_01.prefab:f92d9bdfed7a3be4db70f1ff18c9979a");

	// Token: 0x0400457E RID: 17790
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeB_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeB_02_01.prefab:e1861c2ee00fdbe4391f2c0c12328cb9");

	// Token: 0x0400457F RID: 17791
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeC_01_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_ExchangeC_01_01.prefab:c0adf84ac3175a149bdadf1fd310e7d5");

	// Token: 0x04004580 RID: 17792
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Intro02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Intro02_01.prefab:6192eff89b44d0a4092918c3157c8d7a");

	// Token: 0x04004581 RID: 17793
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Turn01_Response_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Turn01_Response_01.prefab:a6366b765125c4e4089263d512eeb732");

	// Token: 0x04004582 RID: 17794
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Turn02_Response_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Turn02_Response_01.prefab:8af7cef14d53b8349a5faa2085ae58ce");

	// Token: 0x04004583 RID: 17795
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Victory02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission2_Victory02_01.prefab:36e898b055d997944be789464952985f");

	// Token: 0x04004584 RID: 17796
	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeA_01_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeA_01_01.prefab:d240bd5183f961b488b41c92171e7e7b");

	// Token: 0x04004585 RID: 17797
	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeB_01_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeB_01_01.prefab:19f631e11326bca4f9ecbc4050ede113");

	// Token: 0x04004586 RID: 17798
	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeC_02_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_ExchangeC_02_01.prefab:24b785422e9c85f42a435d5ba3022d7d");

	// Token: 0x04004587 RID: 17799
	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Intro01_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Intro01_01.prefab:e284c48590589ce46b82999f59c80369");

	// Token: 0x04004588 RID: 17800
	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Turn01_Intro_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Turn01_Intro_01.prefab:634f4a13b95399c4d87cdaa39542c3c1");

	// Token: 0x04004589 RID: 17801
	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Turn02_Intro_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Turn02_Intro_01.prefab:b0120a614688b6e48ae8404190c4199d");

	// Token: 0x0400458A RID: 17802
	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Victory01_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Victory01_01.prefab:bfb880c645aca7c44a0333399e368d4c");

	// Token: 0x0400458B RID: 17803
	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_EmoteResponse_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_EmoteResponse_01.prefab:a95245d1ba9688b4585fe037f4739170");

	// Token: 0x0400458C RID: 17804
	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_01_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_01_01.prefab:4bbf74b2904c05b4a8fd550a45c4aa6f");

	// Token: 0x0400458D RID: 17805
	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_02_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_02_01.prefab:727b904e47db35a449cfd30509ce098e");

	// Token: 0x0400458E RID: 17806
	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_03_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_03_01.prefab:09925a9d93b0000429ee562d29bb9606");

	// Token: 0x0400458F RID: 17807
	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_Idle01_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_Idle01_01.prefab:a55b7fb2790954a41ba2548d019d4880");

	// Token: 0x04004590 RID: 17808
	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_Idle02_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_Idle02_01.prefab:569771892749dc348b69a9e98c485ba6");

	// Token: 0x04004591 RID: 17809
	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_Idle03_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_Idle03_01.prefab:502a7c83181dcc840a0a0083d95da5ce");

	// Token: 0x04004592 RID: 17810
	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Loss_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_RP_Mission2_Loss_01.prefab:6e01829211e285b4387383f483713b77");

	// Token: 0x04004593 RID: 17811
	private List<string> m_VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_Lines = new List<string>
	{
		RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_01_01,
		RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_02_01,
		RP_Fight_02.VO_TB_Hero_Tyrande_Female_NightElf_TB_Hero_Tyrande_HeroPower_03_01
	};

	// Token: 0x04004594 RID: 17812
	private HashSet<string> m_playedLines = new HashSet<string>();
}
