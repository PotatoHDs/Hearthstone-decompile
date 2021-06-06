using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000385 RID: 901
public class BRM16_Atramedes : BRM_MissionEntity
{
	// Token: 0x0600346B RID: 13419 RVA: 0x0010C1C5 File Offset: 0x0010A3C5
	public override string GetAlternatePlayerName()
	{
		return GameStrings.Get("MISSION_NEFARIAN_TITLE");
	}

	// Token: 0x0600346C RID: 13420 RVA: 0x0010C1D4 File Offset: 0x0010A3D4
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_BRMA16_1_RESPONSE_03.prefab:ebc4ebb81b3a1b741b3895a371f72614");
		base.PreloadSound("VO_BRMA16_1_HERO_POWER_05.prefab:2facfeb30b95f49429ad143e643a3fe5");
		base.PreloadSound("VO_BRMA16_1_CARD_04.prefab:4c0923e1b9cbc854c9c78e549e2e62e4");
		base.PreloadSound("VO_BRMA16_1_GONG1_10.prefab:f36e1a59d28147749ad113da7831b5c6");
		base.PreloadSound("VO_BRMA16_1_GONG2_11.prefab:0064023a77f719646bea0ae472854c8b");
		base.PreloadSound("VO_BRMA16_1_GONG3_12.prefab:6d117262d495e6946aabe17ffff06c57");
		base.PreloadSound("VO_BRMA16_1_TRIGGER1_07.prefab:6651f227d949b2948b69f2317f29970c");
		base.PreloadSound("VO_BRMA16_1_TRIGGER2_08.prefab:97045358fdf509a42b86706dc0f3d477");
		base.PreloadSound("VO_BRMA16_1_TRIGGER3_09.prefab:6bd124e6a8a16fc4cacc8add95c429a6");
		base.PreloadSound("VO_BRMA16_1_TURN1_02.prefab:8edd557780fa3034c865f96650df136f");
	}

	// Token: 0x0600346D RID: 13421 RVA: 0x0010C250 File Offset: 0x0010A450
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
						m_soundName = "VO_BRMA16_1_RESPONSE_03.prefab:ebc4ebb81b3a1b741b3895a371f72614",
						m_stringTag = "VO_BRMA16_1_RESPONSE_03"
					}
				}
			}
		};
	}

	// Token: 0x0600346E RID: 13422 RVA: 0x0010C2AF File Offset: 0x0010A4AF
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
		if (!(cardId == "BRMA16_2") && !(cardId == "BRMA16_2H"))
		{
			if (cardId == "BRMA16_3")
			{
				if (this.m_cardLinePlayed)
				{
					yield break;
				}
				this.m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA16_1_CARD_04.prefab:4c0923e1b9cbc854c9c78e549e2e62e4", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			if (this.m_heroPowerLinePlayed)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA16_1_HERO_POWER_05.prefab:2facfeb30b95f49429ad143e643a3fe5", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x0600346F RID: 13423 RVA: 0x0010C2C5 File Offset: 0x0010A4C5
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn == 1)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA16_1_TURN1_02.prefab:8edd557780fa3034c865f96650df136f", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06003470 RID: 13424 RVA: 0x0010C2DB File Offset: 0x0010A4DB
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent != 1)
		{
			if (missionEvent == 2)
			{
				this.m_weaponLinePlayed++;
				switch (this.m_weaponLinePlayed)
				{
				case 1:
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA16_1_TRIGGER1_07.prefab:6651f227d949b2948b69f2317f29970c", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					break;
				case 2:
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA16_1_TRIGGER2_08.prefab:97045358fdf509a42b86706dc0f3d477", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					break;
				case 3:
					yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA16_1_TRIGGER3_09.prefab:6bd124e6a8a16fc4cacc8add95c429a6", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					break;
				}
			}
		}
		else
		{
			this.m_gongLinePlayed++;
			switch (this.m_gongLinePlayed)
			{
			case 1:
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA16_1_GONG1_10.prefab:f36e1a59d28147749ad113da7831b5c6", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				break;
			case 2:
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA16_1_GONG3_12.prefab:6d117262d495e6946aabe17ffff06c57", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				break;
			case 3:
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA16_1_GONG2_11.prefab:0064023a77f719646bea0ae472854c8b", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				break;
			}
		}
		yield break;
	}

	// Token: 0x06003471 RID: 13425 RVA: 0x0010C2F1 File Offset: 0x0010A4F1
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NefarianDragon_Quote.prefab:179fec888df7e4c02b8de3b7ad109a23", GameStrings.Get("VO_NEFARIAN_ATRAMEDES_DEATH_76"), "VO_NEFARIAN_ATRAMEDES_DEATH_76.prefab:7f23d65dd346a234fb410aeea9ec0d44", true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
		}
		yield break;
	}

	// Token: 0x04001CA6 RID: 7334
	private bool m_heroPowerLinePlayed;

	// Token: 0x04001CA7 RID: 7335
	private bool m_cardLinePlayed;

	// Token: 0x04001CA8 RID: 7336
	private int m_gongLinePlayed;

	// Token: 0x04001CA9 RID: 7337
	private int m_weaponLinePlayed;
}
