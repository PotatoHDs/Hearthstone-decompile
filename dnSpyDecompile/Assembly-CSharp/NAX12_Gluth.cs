using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200058E RID: 1422
public class NAX12_Gluth : NAX_MissionEntity
{
	// Token: 0x06004F15 RID: 20245 RVA: 0x0019F940 File Offset: 0x0019DB40
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_NAX12_01_HP_01.prefab:f2dea305f5fc82340ac4a42cc9ba7d8e");
		base.PreloadSound("VO_NAX12_01_EMOTE_03.prefab:1cabacf1a4889364ca9abdeddf452cab");
		base.PreloadSound("VO_NAX12_01_EMOTE_02.prefab:afb88d065639fd04ba9d2af2203d1ec6");
		base.PreloadSound("VO_NAX12_01_EMOTE_01.prefab:b3cf5e1f56a2b3847bac8d02ae55be18");
		base.PreloadSound("VO_NAX12_01_CARD_01.prefab:ecf464bc8e10cb44bb2a5b7967f534aa");
	}

	// Token: 0x06004F16 RID: 20246 RVA: 0x0019F97C File Offset: 0x0019DB7C
	protected override void InitEmoteResponses()
	{
		this.m_emoteResponseGroups = new List<MissionEntity.EmoteResponseGroup>
		{
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>(MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS),
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_NAX12_01_EMOTE_01.prefab:b3cf5e1f56a2b3847bac8d02ae55be18",
						m_stringTag = "VO_NAX12_01_EMOTE_01"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_NAX12_01_EMOTE_02.prefab:afb88d065639fd04ba9d2af2203d1ec6",
						m_stringTag = "VO_NAX12_01_EMOTE_02"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_NAX12_01_EMOTE_03.prefab:1cabacf1a4889364ca9abdeddf452cab",
						m_stringTag = "VO_NAX12_01_EMOTE_03"
					}
				}
			}
		};
	}

	// Token: 0x06004F17 RID: 20247 RVA: 0x0019FA1D File Offset: 0x0019DC1D
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (turn != 1)
		{
			if (turn == 13)
			{
				this.m_achievementTauntPlayed = true;
				NotificationManager.Get().CreateKTQuote("VO_KT_GLUTH2_ALT_74", "VO_KT_GLUTH2_ALT_74.prefab:92a3d1d1ad601824b9bfd762584a7bbe", false);
			}
		}
		else
		{
			NotificationManager.Get().CreateKTQuote("VO_KT_GLUTH2_73", "VO_KT_GLUTH2_73.prefab:acc2443dca75c1d439e33556405919a5", false);
		}
		yield break;
	}

	// Token: 0x06004F18 RID: 20248 RVA: 0x0019FA33 File Offset: 0x0019DC33
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateKTQuote("VO_KT_GLUTH4_76", "VO_KT_GLUTH4_76.prefab:8287f5fc21b0b0942a6be5eb232bd187", true);
		}
		if (gameResult == TAG_PLAYSTATE.LOST && this.m_achievementTauntPlayed)
		{
			NotificationManager.Get().CreateKTQuote("VO_KT_GLUTH3_75", "VO_KT_GLUTH3_75.prefab:a53397fa1f87755499d3dcec8472c812", false);
		}
		yield break;
	}

	// Token: 0x06004F19 RID: 20249 RVA: 0x0019FA49 File Offset: 0x0019DC49
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		string cardId = entity.GetCardId();
		if (!(cardId == "NAX12_02"))
		{
			if (cardId == "NAX12_04")
			{
				if (this.m_cardLinePlayed)
				{
					yield break;
				}
				this.m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX12_01_CARD_01.prefab:ecf464bc8e10cb44bb2a5b7967f534aa", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				yield return new WaitForSeconds(1f);
				NotificationManager.Get().CreateKTQuote("VO_KT_GLUTH6_78", "VO_KT_GLUTH6_78.prefab:261d18f3504d8204bbca57e9a2ea89e4", false);
			}
		}
		else
		{
			if (this.m_heroPowerLinePlayed > 2)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed++;
			if (this.m_heroPowerLinePlayed == 1)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX12_01_HP_01.prefab:f2dea305f5fc82340ac4a42cc9ba7d8e", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			else
			{
				yield return new WaitForSeconds(2f);
				NotificationManager.Get().CreateKTQuote("VO_KT_GLUTH5_77", "VO_KT_GLUTH5_77.prefab:28d06adaae1843c43831aed6f1e1fb2f", false);
			}
		}
		yield break;
	}

	// Token: 0x04004548 RID: 17736
	private bool m_cardLinePlayed;

	// Token: 0x04004549 RID: 17737
	private int m_heroPowerLinePlayed;

	// Token: 0x0400454A RID: 17738
	private bool m_achievementTauntPlayed;
}
