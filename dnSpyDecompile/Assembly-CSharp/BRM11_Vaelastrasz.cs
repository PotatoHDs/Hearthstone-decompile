using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000380 RID: 896
public class BRM11_Vaelastrasz : BRM_MissionEntity
{
	// Token: 0x0600344B RID: 13387 RVA: 0x0010BB50 File Offset: 0x00109D50
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_BRMA11_1_RESPONSE_03.prefab:f776a76ac4474254fac47aa4c2860190");
		base.PreloadSound("VO_BRMA11_1_HERO_POWER_05.prefab:a15ff3617c584fc4dacf011685a21dd2");
		base.PreloadSound("VO_BRMA11_1_CARD_04.prefab:1a5c6eac59471434a84477c152b8aa0d");
		base.PreloadSound("VO_BRMA11_1_KILL_PLAYER_06.prefab:52ea6310c1553f147a1de7d2bca500ad");
		base.PreloadSound("VO_BRMA11_1_ALEXSTRAZA_07.prefab:8d0705fc4be8e4144b7764931784dbca");
		base.PreloadSound("VO_BRMA11_1_TURN1_02.prefab:dd79459ebc3fe2346858c23caea3b337");
	}

	// Token: 0x0600344C RID: 13388 RVA: 0x0010BBA0 File Offset: 0x00109DA0
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
						m_soundName = "VO_BRMA11_1_RESPONSE_03.prefab:f776a76ac4474254fac47aa4c2860190",
						m_stringTag = "VO_BRMA11_1_RESPONSE_03"
					}
				}
			}
		};
	}

	// Token: 0x0600344D RID: 13389 RVA: 0x0010BBFF File Offset: 0x00109DFF
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
		if (cardId == "BRMA11_3")
		{
			if (this.m_cardLinePlayed)
			{
				yield break;
			}
			this.m_cardLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA11_1_HERO_POWER_05.prefab:a15ff3617c584fc4dacf011685a21dd2", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x0600344E RID: 13390 RVA: 0x0010BC15 File Offset: 0x00109E15
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn != 1)
		{
			if (turn == 2)
			{
				while (this.m_enemySpeaking)
				{
					yield return null;
				}
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA11_1_CARD_04.prefab:1a5c6eac59471434a84477c152b8aa0d", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA11_1_TURN1_02.prefab:dd79459ebc3fe2346858c23caea3b337", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x0600344F RID: 13391 RVA: 0x0010BC2B File Offset: 0x00109E2B
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent != 1)
		{
			if (missionEvent == 2)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA11_1_ALEXSTRAZA_07.prefab:8d0705fc4be8e4144b7764931784dbca", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA11_1_KILL_PLAYER_06.prefab:52ea6310c1553f147a1de7d2bca500ad", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06003450 RID: 13392 RVA: 0x0010BC41 File Offset: 0x00109E41
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA11_1_KILL_PLAYER_06.prefab:52ea6310c1553f147a1de7d2bca500ad", 1f, true, false));
		}
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_VAEL_DEAD_57"), "VO_NEFARIAN_VAEL_DEAD_57.prefab:f0f295213070e68488bb5a0b35948f77", true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
		}
		yield break;
	}

	// Token: 0x04001C98 RID: 7320
	private bool m_cardLinePlayed;
}
