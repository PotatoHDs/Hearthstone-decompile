using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000376 RID: 886
public class BRM01_GrimGuzzler : BRM_MissionEntity
{
	// Token: 0x0600340B RID: 13323 RVA: 0x0010AFC0 File Offset: 0x001091C0
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_BRMA01_1_RESPONSE_03.prefab:6bce12880a0d1ef408aab318f0c0d699");
		base.PreloadSound("VO_BRMA01_1_HERO_POWER_04.prefab:ddd556f3fc3107642ba85ffa60e56efd");
		base.PreloadSound("VO_BRMA01_1_CARD_05.prefab:4d60a73b9cc4c3645a387eb198be2d8a");
		base.PreloadSound("VO_BRMA01_1_ETC_06.prefab:966f2e43e86303b4da0adc2529bd22a3");
		base.PreloadSound("VO_BRMA01_1_SUCCUBUS_08.prefab:6e04c1f0a2ce98d4187e5ee6499211a9");
		base.PreloadSound("VO_BRMA01_1_WARGOLEM_07.prefab:d45ef18ce5906d74e94ecb0e56323d37");
		base.PreloadSound("VO_BRMA01_1_TURN1_02.prefab:914cf2bf87da18c458f38f8fcbc98481");
	}

	// Token: 0x0600340C RID: 13324 RVA: 0x0010B01C File Offset: 0x0010921C
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
						m_soundName = "VO_BRMA01_1_RESPONSE_03.prefab:6bce12880a0d1ef408aab318f0c0d699",
						m_stringTag = "VO_BRMA01_1_RESPONSE_03"
					}
				}
			}
		};
	}

	// Token: 0x0600340D RID: 13325 RVA: 0x0010B07B File Offset: 0x0010927B
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
		if (!(cardId == "BRMA01_2") && !(cardId == "BRMA01_2H"))
		{
			if (cardId == "BRMA01_4")
			{
				if (this.m_cardLinePlayed)
				{
					yield break;
				}
				this.m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA01_1_CARD_05.prefab:4d60a73b9cc4c3645a387eb198be2d8a", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			if (this.m_heroPowerLinePlayed)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA01_1_HERO_POWER_04.prefab:ddd556f3fc3107642ba85ffa60e56efd", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x0600340E RID: 13326 RVA: 0x0010B091 File Offset: 0x00109291
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		switch (missionEvent)
		{
		case 1:
			if (!this.m_eTCLinePlayed && !this.m_disableSpecialCardVO)
			{
				this.m_eTCLinePlayed = true;
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA01_1_ETC_06.prefab:966f2e43e86303b4da0adc2529bd22a3", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				GameState.Get().SetBusy(false);
			}
			break;
		case 2:
			if (!this.m_succubusLinePlayed && !this.m_disableSpecialCardVO)
			{
				this.m_succubusLinePlayed = true;
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA01_1_SUCCUBUS_08.prefab:6e04c1f0a2ce98d4187e5ee6499211a9", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				GameState.Get().SetBusy(false);
			}
			break;
		case 3:
			if (!this.m_warGolemLinePlayed && !this.m_disableSpecialCardVO)
			{
				this.m_warGolemLinePlayed = true;
				GameState.Get().SetBusy(true);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA01_1_WARGOLEM_07.prefab:d45ef18ce5906d74e94ecb0e56323d37", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				GameState.Get().SetBusy(false);
			}
			break;
		}
		yield break;
	}

	// Token: 0x0600340F RID: 13327 RVA: 0x0010B0A7 File Offset: 0x001092A7
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn != 1)
		{
			if (turn == 3)
			{
				this.m_disableSpecialCardVO = false;
			}
		}
		else
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA01_1_TURN1_02.prefab:914cf2bf87da18c458f38f8fcbc98481", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06003410 RID: 13328 RVA: 0x0010B0BD File Offset: 0x001092BD
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_COREN_DEAD_28"), "VO_NEFARIAN_COREN_DEAD_28.prefab:0539437e9ff9ee9409bd7cd236d59d53", true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
		}
		yield break;
	}

	// Token: 0x04001C7F RID: 7295
	private bool m_heroPowerLinePlayed;

	// Token: 0x04001C80 RID: 7296
	private bool m_cardLinePlayed;

	// Token: 0x04001C81 RID: 7297
	private bool m_eTCLinePlayed;

	// Token: 0x04001C82 RID: 7298
	private bool m_succubusLinePlayed;

	// Token: 0x04001C83 RID: 7299
	private bool m_warGolemLinePlayed;

	// Token: 0x04001C84 RID: 7300
	private bool m_disableSpecialCardVO = true;
}
