using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000587 RID: 1415
public class NAX05_Heigan : NAX_MissionEntity
{
	// Token: 0x06004EEC RID: 20204 RVA: 0x0019F2E6 File Offset: 0x0019D4E6
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_NAX5_01_HP_02.prefab:3f0aa9156dbf8c04198a38aea754c0bf");
		base.PreloadSound("VO_NAX5_01_CARD_03.prefab:877904b2ca125a24983e76dfb44929fb");
		base.PreloadSound("VO_NAX5_01_EMOTE_05.prefab:33680944c523f394e95245a6e4ea9f00");
	}

	// Token: 0x06004EED RID: 20205 RVA: 0x0019F30C File Offset: 0x0019D50C
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
						m_soundName = "VO_NAX5_01_EMOTE_05.prefab:33680944c523f394e95245a6e4ea9f00",
						m_stringTag = "VO_NAX5_01_EMOTE_05"
					}
				}
			}
		};
	}

	// Token: 0x06004EEE RID: 20206 RVA: 0x0019F36B File Offset: 0x0019D56B
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateKTQuote("VO_KT_HEIGAN2_55", "VO_KT_HEIGAN2_55.prefab:f465a1b0b2312764f92f4d86160c9dac", true);
		}
		yield break;
	}

	// Token: 0x06004EEF RID: 20207 RVA: 0x0019F37A File Offset: 0x0019D57A
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
		if (!(cardId == "NAX5_02"))
		{
			if (cardId == "NAX5_03")
			{
				if (this.m_cardLinePlayed)
				{
					yield break;
				}
				this.m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX5_01_CARD_03.prefab:877904b2ca125a24983e76dfb44929fb", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			if (this.m_heroPowerLinePlayed)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX5_01_HP_02.prefab:3f0aa9156dbf8c04198a38aea754c0bf", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x0400453A RID: 17722
	private bool m_cardLinePlayed;

	// Token: 0x0400453B RID: 17723
	private bool m_heroPowerLinePlayed;
}
