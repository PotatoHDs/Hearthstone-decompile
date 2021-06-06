using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200058B RID: 1419
public class NAX09_Horsemen : NAX_MissionEntity
{
	// Token: 0x06004F04 RID: 20228 RVA: 0x0019F680 File Offset: 0x0019D880
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_NAX9_01_CUSTOM_02.prefab:aabdff7ec08cc1f44a7c8c391c744e2f");
		base.PreloadSound("VO_NAX9_01_EMOTE_04.prefab:1d1eb70ed25d60c429b27471ec10b191");
		base.PreloadSound("VO_FP1_031_EnterPlay_06.prefab:51754c9428cdf374882cb4020bbd5627");
		base.PreloadSound("VO_NAX9_02_CUSTOM_01.prefab:520d0daa9374bfa47ab3f380f0e1ef65");
		base.PreloadSound("VO_NAX9_03_CUSTOM_01.prefab:4fb7d8593f95c404f97ddd63c29e939c");
		base.PreloadSound("VO_NAX9_04_CUSTOM_01.prefab:9581debb360b7dd478f7ddfeeda6768e");
		base.PreloadSound("VO_FP1_031_Attack_07.prefab:b4c323c69c7f5cf418ec6b228b188c5d");
	}

	// Token: 0x06004F05 RID: 20229 RVA: 0x0019F6DC File Offset: 0x0019D8DC
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
						m_soundName = "VO_NAX9_01_EMOTE_04.prefab:1d1eb70ed25d60c429b27471ec10b191",
						m_stringTag = "VO_NAX9_01_EMOTE_04"
					}
				}
			}
		};
	}

	// Token: 0x06004F06 RID: 20230 RVA: 0x0019F73B File Offset: 0x0019D93B
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateKTQuote("VO_KT_BARON2_64", "VO_KT_BARON2_64.prefab:485607a6e18abc9458ba36d2b952d403", true);
		}
		yield break;
	}

	// Token: 0x06004F07 RID: 20231 RVA: 0x0019F74A File Offset: 0x0019D94A
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor baronActor;
		Actor blaumeuxActor;
		Actor thaneActor;
		if (turn == 1)
		{
			baronActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
			blaumeuxActor = null;
			thaneActor = null;
			Actor actor = null;
			foreach (Card card in GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone().GetCards())
			{
				string cardId = card.GetEntity().GetCardId();
				if (cardId == "NAX9_02")
				{
					blaumeuxActor = card.GetActor();
				}
				else if (cardId == "NAX9_03")
				{
					thaneActor = card.GetActor();
				}
				else if (cardId == "NAX9_04")
				{
					actor = card.GetActor();
				}
			}
			if (actor == null)
			{
				this.m_introSequenceComplete = true;
				yield break;
			}
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX9_02_CUSTOM_01.prefab:520d0daa9374bfa47ab3f380f0e1ef65", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			if (blaumeuxActor != null)
			{
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX9_03_CUSTOM_01.prefab:4fb7d8593f95c404f97ddd63c29e939c", Notification.SpeechBubbleDirection.TopRight, blaumeuxActor, 3f, 1f, true, false, 0f));
			}
			if (baronActor != null)
			{
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX9_01_CUSTOM_02.prefab:aabdff7ec08cc1f44a7c8c391c744e2f", Notification.SpeechBubbleDirection.TopRight, baronActor, 3f, 1f, true, false, 0f));
			}
			if (thaneActor != null)
			{
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX9_04_CUSTOM_01.prefab:9581debb360b7dd478f7ddfeeda6768e", Notification.SpeechBubbleDirection.TopRight, thaneActor, 3f, 1f, true, false, 0f));
			}
			this.m_introSequenceComplete = true;
		}
		baronActor = null;
		blaumeuxActor = null;
		thaneActor = null;
		yield break;
	}

	// Token: 0x06004F08 RID: 20232 RVA: 0x0019F760 File Offset: 0x0019D960
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		if (!this.m_introSequenceComplete)
		{
			yield break;
		}
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
		if (!(cardId == "NAX9_06"))
		{
			if (cardId == "NAX9_07")
			{
				if (this.m_cardLinePlayed)
				{
					yield break;
				}
				this.m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_FP1_031_Attack_07.prefab:b4c323c69c7f5cf418ec6b228b188c5d", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			if (this.m_heroPowerLinePlayed)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_FP1_031_EnterPlay_06.prefab:51754c9428cdf374882cb4020bbd5627", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x04004542 RID: 17730
	private bool m_heroPowerLinePlayed;

	// Token: 0x04004543 RID: 17731
	private bool m_cardLinePlayed;

	// Token: 0x04004544 RID: 17732
	private bool m_introSequenceComplete;
}
