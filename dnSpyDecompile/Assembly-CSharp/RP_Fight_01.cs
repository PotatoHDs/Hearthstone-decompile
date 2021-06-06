using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000594 RID: 1428
public class RP_Fight_01 : RP_Dungeon
{
	// Token: 0x06004F53 RID: 20307 RVA: 0x001A0C2C File Offset: 0x0019EE2C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			RP_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeA_02_01,
			RP_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeB_02_01,
			RP_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeC_01,
			RP_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Intro02_01,
			RP_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Turn01_Response_01,
			RP_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Turn02_Intro_01,
			RP_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Victory02_01,
			RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_ExchangeA_01_01,
			RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_ExchangeB_01_01,
			RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Intro01_01,
			RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Turn01_Intro_01,
			RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Turn02_Response_01,
			RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Victory01_01,
			RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_EmoteResponse_01,
			RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_01_01,
			RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_02_01,
			RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_03_01,
			RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_01_01,
			RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_02_01,
			RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_03_01,
			RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Loss_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004F54 RID: 20308 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004F55 RID: 20309 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004F56 RID: 20310 RVA: 0x001A0DE0 File Offset: 0x0019EFE0
	public override List<string> GetIdleLines()
	{
		return this.m_VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_Lines;
	}

	// Token: 0x06004F57 RID: 20311 RVA: 0x001A0DE8 File Offset: 0x0019EFE8
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_Lines;
	}

	// Token: 0x06004F58 RID: 20312 RVA: 0x001A0DF0 File Offset: 0x0019EFF0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
	}

	// Token: 0x06004F59 RID: 20313 RVA: 0x001A0DF8 File Offset: 0x0019EFF8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(RP_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Intro02_01, Notification.SpeechBubbleDirection.BottomLeft, actor2, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004F5A RID: 20314 RVA: 0x001A0E9E File Offset: 0x0019F09E
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
				yield return base.PlayLineAlways(actor, RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Victory01_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, RP_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Victory02_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004F5B RID: 20315 RVA: 0x001A0EB4 File Offset: 0x0019F0B4
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

	// Token: 0x06004F5C RID: 20316 RVA: 0x001A0ECA File Offset: 0x0019F0CA
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

	// Token: 0x06004F5D RID: 20317 RVA: 0x001A0EE0 File Offset: 0x0019F0E0
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
					yield return base.PlayLineAlways(friendlyActor, RP_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Turn02_Intro_01, 2.5f);
					yield return base.PlayLineAlways(enemyActor, RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Turn02_Response_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(enemyActor, RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Turn01_Intro_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, RP_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Turn01_Response_01, 2.5f);
			}
		}
		else if (turn != 6)
		{
			if (turn != 11)
			{
				if (turn == 15)
				{
					yield return base.PlayLineAlways(friendlyActor, RP_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeC_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(enemyActor, RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_ExchangeB_01_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, RP_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeB_02_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(enemyActor, RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_ExchangeA_01_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, RP_Fight_01.VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeA_02_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04004565 RID: 17765
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeA_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeA_02_01.prefab:a5b20eec3ba5adc4093b9005a1663244");

	// Token: 0x04004566 RID: 17766
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeB_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeB_02_01.prefab:284f8062fddb2484e9c1e60c4e450680");

	// Token: 0x04004567 RID: 17767
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeC_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_ExchangeC_01.prefab:7860c64a2106ec8498b3052a945f4de8");

	// Token: 0x04004568 RID: 17768
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Intro02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Intro02_01.prefab:424406dd090e8274192c8be6f9a3815c");

	// Token: 0x04004569 RID: 17769
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Turn01_Response_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Turn01_Response_01.prefab:e2f258f89e2a59a45ab47a967a244920");

	// Token: 0x0400456A RID: 17770
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Turn02_Intro_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Turn02_Intro_01.prefab:d3d7e71ad1e60f645a8a653b06f35998");

	// Token: 0x0400456B RID: 17771
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Victory02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_RP_Mission1_Victory02_01.prefab:171c013ea75af8a4fbadb9e6274f354c");

	// Token: 0x0400456C RID: 17772
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_ExchangeA_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_ExchangeA_01_01.prefab:8e2488eef3bac7f44b78761b488e5588");

	// Token: 0x0400456D RID: 17773
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_ExchangeB_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_ExchangeB_01_01.prefab:bff90af9a86e46546a7288f5a93b4272");

	// Token: 0x0400456E RID: 17774
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Intro01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Intro01_01.prefab:3cbaf7029a3940d4c806633d099443c4");

	// Token: 0x0400456F RID: 17775
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Turn01_Intro_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Turn01_Intro_01.prefab:a576a2f45112ab64b995f4cfed9cf08b");

	// Token: 0x04004570 RID: 17776
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Turn02_Response_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Turn02_Response_01.prefab:d1b0b0dccedbaca49a2e3ee7fd23d6cb");

	// Token: 0x04004571 RID: 17777
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Victory01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Victory01_01.prefab:bf01a3c975acddd428a09a320f6f0091");

	// Token: 0x04004572 RID: 17778
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_EmoteResponse_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_EmoteResponse_01.prefab:1ef60c386c190b445aa16997c878bec3");

	// Token: 0x04004573 RID: 17779
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_01_01.prefab:49a123fd372abb246b60ffabf35a625c");

	// Token: 0x04004574 RID: 17780
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_02_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_02_01.prefab:acef5ea7ccea5b54c8a3a6ed1da1f32f");

	// Token: 0x04004575 RID: 17781
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_03_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_03_01.prefab:f0115342f8307ca43a3132e7c7c30235");

	// Token: 0x04004576 RID: 17782
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_01_01.prefab:14e58cb84c672084ca79100a523cdfac");

	// Token: 0x04004577 RID: 17783
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_02_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_02_01.prefab:3e3429f2ada367c4f9fa4eb9aa110549");

	// Token: 0x04004578 RID: 17784
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_03_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_03_01.prefab:5e40fc7764481ad4db6e107f4b9c1386");

	// Token: 0x04004579 RID: 17785
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Loss_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_RP_Mission1_Loss_01.prefab:8b115fc72844cb34a820ba1aa0814228");

	// Token: 0x0400457A RID: 17786
	private List<string> m_VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_Lines = new List<string>
	{
		RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_01_01,
		RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_02_01,
		RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_HeroPower_03_01
	};

	// Token: 0x0400457B RID: 17787
	private List<string> m_VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_Lines = new List<string>
	{
		RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_01_01,
		RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_02_01,
		RP_Fight_01.VO_TB_Hero_Malfurion_Male_NightElf_TB_Hero_Malfurion_Idle_03_01
	};

	// Token: 0x0400457C RID: 17788
	private HashSet<string> m_playedLines = new HashSet<string>();
}
