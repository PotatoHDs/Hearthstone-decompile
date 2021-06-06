using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000588 RID: 1416
public class NAX06_Loatheb : NAX_MissionEntity
{
	// Token: 0x06004EF1 RID: 20209 RVA: 0x0019F390 File Offset: 0x0019D590
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_NAX6_01_HP_02.prefab:3e1a96b32f62ce041a2fc526a59aa3d1");
		base.PreloadSound("VO_NAX6_01_CARD_03.prefab:a0f9ea501662eb344b224d4b1e79aa4c");
		base.PreloadSound("VO_NAX6_01_EMOTE_05.prefab:aa2803acd071a934d98b78ec49d51698");
	}

	// Token: 0x06004EF2 RID: 20210 RVA: 0x0019F3B4 File Offset: 0x0019D5B4
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
						m_soundName = "VO_NAX6_01_EMOTE_05.prefab:aa2803acd071a934d98b78ec49d51698",
						m_stringTag = "VO_NAX6_01_EMOTE_05"
					}
				}
			}
		};
	}

	// Token: 0x06004EF3 RID: 20211 RVA: 0x0019F413 File Offset: 0x0019D613
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateKTQuote("VO_KT_LOATHEB2_57", "VO_KT_LOATHEB2_57.prefab:decea68ad8178c34aad7172b1f7739ac", true);
		}
		yield break;
	}

	// Token: 0x06004EF4 RID: 20212 RVA: 0x0019F422 File Offset: 0x0019D622
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
		if (!(cardId == "NAX6_02"))
		{
			if (cardId == "NAX6_03")
			{
				if (this.m_cardLinePlayed)
				{
					yield break;
				}
				this.m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX6_01_CARD_03.prefab:a0f9ea501662eb344b224d4b1e79aa4c", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			if (this.m_heroPowerLinePlayed)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX6_01_HP_02.prefab:3e1a96b32f62ce041a2fc526a59aa3d1", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x0400453C RID: 17724
	private bool m_cardLinePlayed;

	// Token: 0x0400453D RID: 17725
	private bool m_heroPowerLinePlayed;
}
