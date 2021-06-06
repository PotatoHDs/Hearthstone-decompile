using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000584 RID: 1412
public class NAX02_Faerlina : NAX_MissionEntity
{
	// Token: 0x06004ED8 RID: 20184 RVA: 0x0019EFC0 File Offset: 0x0019D1C0
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_NAX2_01_HP_04.prefab:c2607f2df2849a3478f64624ed464f45");
		base.PreloadSound("VO_NAX2_01_CARD_02.prefab:462f4054a3d870d44bffda29568fb42f");
		base.PreloadSound("VO_NAX2_01_EMOTE_06.prefab:67f66bba9b0da65448b6a54b6206a4a8");
		base.PreloadSound("VO_NAX2_01_CUSTOM_03.prefab:87a19007902dc794e88dd8c21dd1babe");
	}

	// Token: 0x06004ED9 RID: 20185 RVA: 0x0019EFF0 File Offset: 0x0019D1F0
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
						m_soundName = "VO_NAX2_01_EMOTE_06.prefab:67f66bba9b0da65448b6a54b6206a4a8",
						m_stringTag = "VO_NAX2_01_EMOTE_06"
					}
				}
			}
		};
	}

	// Token: 0x06004EDA RID: 20186 RVA: 0x0019F04F File Offset: 0x0019D24F
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateKTQuote("VO_KT_FAERLINA2_45", "VO_KT_FAERLINA2_45.prefab:d6270f942c734014d998b58c40f9a22d", true);
		}
		yield break;
	}

	// Token: 0x06004EDB RID: 20187 RVA: 0x0019F05E File Offset: 0x0019D25E
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
		if (!(cardId == "NAX2_03"))
		{
			if (cardId == "NAX2_05" || cardId == "NAX2_05H")
			{
				if (this.m_cardLinePlayed)
				{
					yield break;
				}
				this.m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX2_01_CARD_02.prefab:462f4054a3d870d44bffda29568fb42f", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			if (this.m_heroPowerLinePlayed)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX2_01_HP_04.prefab:c2607f2df2849a3478f64624ed464f45", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06004EDC RID: 20188 RVA: 0x0019F074 File Offset: 0x0019D274
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield return Gameplay.Get().StartCoroutine(base.HandleMissionEventWithTiming(missionEvent));
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 1)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX2_01_CUSTOM_03.prefab:87a19007902dc794e88dd8c21dd1babe", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x04004534 RID: 17716
	private bool m_cardLinePlayed;

	// Token: 0x04004535 RID: 17717
	private bool m_heroPowerLinePlayed;
}
