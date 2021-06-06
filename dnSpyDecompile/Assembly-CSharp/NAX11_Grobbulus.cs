using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200058D RID: 1421
public class NAX11_Grobbulus : NAX_MissionEntity
{
	// Token: 0x06004F10 RID: 20240 RVA: 0x0019F896 File Offset: 0x0019DA96
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_NAX11_01_HP_02.prefab:5683ba1493ba8aa47ba0fcbf61f2a39c");
		base.PreloadSound("VO_NAX11_01_CARD_03.prefab:5bb409481ec9ba74bb7336fb88b82ea2");
		base.PreloadSound("VO_NAX11_01_EMOTE_04.prefab:c3dd01740e7b8a14d9149abe560f05aa");
	}

	// Token: 0x06004F11 RID: 20241 RVA: 0x0019F8BC File Offset: 0x0019DABC
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
						m_soundName = "VO_NAX11_01_EMOTE_04.prefab:c3dd01740e7b8a14d9149abe560f05aa",
						m_stringTag = "VO_NAX11_01_EMOTE_04"
					}
				}
			}
		};
	}

	// Token: 0x06004F12 RID: 20242 RVA: 0x0019F91B File Offset: 0x0019DB1B
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateKTQuote("VO_KT_GROBBULUS2_71", "VO_KT_GROBBULUS2_71.prefab:352919c0987145140bdf6144c220956c", true);
		}
		yield break;
	}

	// Token: 0x06004F13 RID: 20243 RVA: 0x0019F92A File Offset: 0x0019DB2A
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
		if (!(cardId == "NAX11_02"))
		{
			if (cardId == "NAX11_04")
			{
				if (this.m_cardLinePlayed)
				{
					yield break;
				}
				this.m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX11_01_CARD_03.prefab:5bb409481ec9ba74bb7336fb88b82ea2", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			if (this.m_heroPowerLinePlayed)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX11_01_HP_02.prefab:5683ba1493ba8aa47ba0fcbf61f2a39c", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x04004546 RID: 17734
	private bool m_cardLinePlayed;

	// Token: 0x04004547 RID: 17735
	private bool m_heroPowerLinePlayed;
}
