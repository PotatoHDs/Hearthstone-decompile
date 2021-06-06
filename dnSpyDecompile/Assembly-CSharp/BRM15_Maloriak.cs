using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000384 RID: 900
public class BRM15_Maloriak : BRM_MissionEntity
{
	// Token: 0x06003465 RID: 13413 RVA: 0x0010C0BC File Offset: 0x0010A2BC
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_BRMA15_1_RESPONSE_03.prefab:3fe1fdbbc2206a94dbc13d3e72a2eca4");
		base.PreloadSound("VO_BRMA15_1_HERO_POWER_06.prefab:27e3e01a28e94254a95c382f14bafe2c");
		base.PreloadSound("VO_BRMA15_1_CARD_05.prefab:459bd07b2915f1d448418b2ddcd89917");
		base.PreloadSound("VO_BRMA15_1_TURN1_02.prefab:b2e6fc2aa07c2c743844fad9a8e782bb");
		base.PreloadSound("VO_NEFARIAN_MALORIAK_TURN2_71.prefab:42b1e1e13b743b945a1f068acd056c21");
		base.PreloadSound("VO_NEFARIAN_MALORIAK_DEATH_PT1_72.prefab:a3baf4ca0cd4aff4db6a62ef44811c54");
		base.PreloadSound("VO_NEFARIAN_MALORIAK_DEATH_PT2_73.prefab:7b6518aa67d0a544598e6689904d9713");
		base.PreloadSound("VO_NEFARIAN_MALORIAK_DEATH_PT3_74.prefab:6f8c07be05efd4b458547b52559b61ae");
	}

	// Token: 0x06003466 RID: 13414 RVA: 0x0010C124 File Offset: 0x0010A324
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
						m_soundName = "VO_BRMA15_1_RESPONSE_03.prefab:3fe1fdbbc2206a94dbc13d3e72a2eca4",
						m_stringTag = "VO_BRMA15_1_RESPONSE_03"
					}
				}
			}
		};
	}

	// Token: 0x06003467 RID: 13415 RVA: 0x0010C183 File Offset: 0x0010A383
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
		if (!(cardId == "BRMA15_2") && !(cardId == "BRMA15_2H"))
		{
			if (cardId == "BRMA15_3")
			{
				if (this.m_cardLinePlayed)
				{
					yield break;
				}
				this.m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA15_1_CARD_05.prefab:459bd07b2915f1d448418b2ddcd89917", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			if (this.m_heroPowerLinePlayed)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA15_1_HERO_POWER_06.prefab:27e3e01a28e94254a95c382f14bafe2c", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06003468 RID: 13416 RVA: 0x0010C199 File Offset: 0x0010A399
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn != 1)
		{
			if (turn == 2)
			{
				NotificationManager.Get().CreateCharacterQuote("NefarianDragon_Quote.prefab:179fec888df7e4c02b8de3b7ad109a23", new Vector3(95f, NotificationManager.DEPTH, 36.8f), GameStrings.Get("VO_NEFARIAN_MALORIAK_TURN2_71"), "VO_NEFARIAN_MALORIAK_TURN2_71.prefab:42b1e1e13b743b945a1f068acd056c21", true, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
			}
		}
		else
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA15_1_TURN1_02.prefab:b2e6fc2aa07c2c743844fad9a8e782bb", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06003469 RID: 13417 RVA: 0x0010C1AF File Offset: 0x0010A3AF
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_MALORIAK_DEATH_PT2_73"), "", true, 5f, CanvasAnchor.BOTTOM_LEFT, false);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_NEFARIAN_MALORIAK_DEATH_PT2_73.prefab:7b6518aa67d0a544598e6689904d9713", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
			NotificationManager.Get().DestroyActiveQuote(0f, false);
			NotificationManager.Get().CreateCharacterQuote("NefarianDragon_Quote.prefab:179fec888df7e4c02b8de3b7ad109a23", GameStrings.Get("VO_NEFARIAN_MALORIAK_DEATH_PT3_74"), "", true, 7f, CanvasAnchor.BOTTOM_LEFT, false);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndWait("VO_NEFARIAN_MALORIAK_DEATH_PT3_74.prefab:6f8c07be05efd4b458547b52559b61ae", "", Notification.SpeechBubbleDirection.None, null, 1f, true, false, 3f, 0f));
			NotificationManager.Get().DestroyActiveQuote(0f, false);
		}
		yield break;
	}

	// Token: 0x04001CA4 RID: 7332
	private bool m_heroPowerLinePlayed;

	// Token: 0x04001CA5 RID: 7333
	private bool m_cardLinePlayed;
}
