using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200037C RID: 892
public class BRM07_Omokk : BRM_MissionEntity
{
	// Token: 0x06003432 RID: 13362 RVA: 0x0010B78C File Offset: 0x0010998C
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_BRMA07_1_RESPONSE_03.prefab:b43d82ce7a9bb59438b594dd3c185050");
		base.PreloadSound("VO_BRMA07_1_HERO_POWER_05.prefab:10f8a1b1fc7c9374b8c2b741f27694be");
		base.PreloadSound("VO_BRMA07_1_CARD_04.prefab:f498bd13724f67d48a0f0bc55034c44b");
		base.PreloadSound("VO_BRMA07_1_TURN1_02.prefab:ac11bf2418c6e0f418f2216348b224c3");
		base.PreloadSound("VO_NEFARIAN_OMOKK1_44.prefab:82ad9b06a62bf044b9e5660054e5fae6");
		base.PreloadSound("VO_NEFARIAN_OMOKK2_45.prefab:bb20664f6b0c27149a6048f473ca0398");
	}

	// Token: 0x06003433 RID: 13363 RVA: 0x0010B7DC File Offset: 0x001099DC
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
						m_soundName = "VO_BRMA07_1_RESPONSE_03.prefab:b43d82ce7a9bb59438b594dd3c185050",
						m_stringTag = "VO_BRMA07_1_RESPONSE_03"
					}
				}
			}
		};
	}

	// Token: 0x06003434 RID: 13364 RVA: 0x0010B83B File Offset: 0x00109A3B
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
		if (!(cardId == "BRMA07_2") && !(cardId == "BRMA07_2H"))
		{
			if (cardId == "BRMA07_3")
			{
				if (this.m_cardLinePlayed)
				{
					yield break;
				}
				this.m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA07_1_CARD_04.prefab:f498bd13724f67d48a0f0bc55034c44b", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			if (this.m_heroPowerLinePlayed)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA07_1_HERO_POWER_05.prefab:10f8a1b1fc7c9374b8c2b741f27694be", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06003435 RID: 13365 RVA: 0x0010B851 File Offset: 0x00109A51
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Vector3 position = new Vector3(95f, NotificationManager.DEPTH, 36.8f);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn != 1)
		{
			if (turn != 4)
			{
				if (turn == 8)
				{
					NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", position, GameStrings.Get("VO_NEFARIAN_OMOKK2_45"), "VO_NEFARIAN_OMOKK2_45.prefab:bb20664f6b0c27149a6048f473ca0398", true, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
				}
			}
			else
			{
				NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", position, GameStrings.Get("VO_NEFARIAN_OMOKK1_44"), "VO_NEFARIAN_OMOKK1_44.prefab:82ad9b06a62bf044b9e5660054e5fae6", true, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
			}
		}
		else
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA07_1_TURN1_02.prefab:ac11bf2418c6e0f418f2216348b224c3", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06003436 RID: 13366 RVA: 0x0010B867 File Offset: 0x00109A67
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_OMOKK_DEAD_46"), "VO_NEFARIAN_OMOKK_DEAD_46.prefab:894d0e92341ab754281388682e449096", true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
		}
		yield break;
	}

	// Token: 0x04001C8E RID: 7310
	private bool m_heroPowerLinePlayed;

	// Token: 0x04001C8F RID: 7311
	private bool m_cardLinePlayed;
}
