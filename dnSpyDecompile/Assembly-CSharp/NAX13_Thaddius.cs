using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200058F RID: 1423
public class NAX13_Thaddius : NAX_MissionEntity
{
	// Token: 0x06004F1B RID: 20251 RVA: 0x0019FA5F File Offset: 0x0019DC5F
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_NAX13_01_HP_02.prefab:cc9afdc24fabea54abc939924c34c7f8");
		base.PreloadSound("VO_NAX13_01_EMOTE_04.prefab:337ef024b2d71e84393f6da891bf83cc");
	}

	// Token: 0x06004F1C RID: 20252 RVA: 0x0019FA78 File Offset: 0x0019DC78
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
						m_soundName = "VO_NAX13_01_EMOTE_04.prefab:337ef024b2d71e84393f6da891bf83cc",
						m_stringTag = "VO_NAX13_01_EMOTE_04"
					}
				}
			}
		};
	}

	// Token: 0x06004F1D RID: 20253 RVA: 0x0019FAD7 File Offset: 0x0019DCD7
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateKTQuote("VO_KT_THADDIUS2_81", "VO_KT_THADDIUS2_81.prefab:47685f2ff524d944f90a9cb87b8e9861", true);
		}
		yield break;
	}

	// Token: 0x06004F1E RID: 20254 RVA: 0x0019FAE6 File Offset: 0x0019DCE6
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
		if (cardId == "NAX13_02")
		{
			if (this.m_heroPowerLinePlayed)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX13_01_HP_02.prefab:cc9afdc24fabea54abc939924c34c7f8", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x0400454B RID: 17739
	private bool m_heroPowerLinePlayed;
}
