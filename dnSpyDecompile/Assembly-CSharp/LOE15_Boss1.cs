using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000395 RID: 917
public class LOE15_Boss1 : LOE_MissionEntity
{
	// Token: 0x060034E6 RID: 13542 RVA: 0x0010D868 File Offset: 0x0010BA68
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_LOE_15_RESPONSE.prefab:a8accce27ae78cb4fb4b02f58e9c3036");
		base.PreloadSound("VO_LOEA15_1_LOW_HEALTH_10.prefab:a948e1a494daa01429f9c8af98ba2ba7");
		base.PreloadSound("VO_LOEA15_1_TURN1_08.prefab:6a6f93dab22e2aa44bafd03d5da8597a");
		base.PreloadSound("VO_LOEA15_1_MAGMA_RAGER_09.prefab:86f34787a5e7d5441a6331443d36880b");
		base.PreloadSound("VO_LOEA15_1_LOSS_11.prefab:7a3d507088374a240a490fc098f6791b");
		base.PreloadSound("VO_LOEA15_1_WIN_12.prefab:bdad6f025366f8645a65655e1d8fc751");
		base.PreloadSound("VO_LOEA15_GOLDEN.prefab:c7173172b70c5ca40b1f3157ee3e5279");
		base.PreloadSound("VO_LOEA15_1_START_07.prefab:663467aee38cfa3429a9f89eee6177fe");
		base.PreloadSound("VO_LOE_15_SPARE.prefab:a25a36df79b66394ebb054c12e4a9441");
		base.PreloadSound("VO_ELISE_WEIRD_DECK_05.prefab:89915bf24c91ba642bf3ae842e267fe3");
	}

	// Token: 0x060034E7 RID: 13543 RVA: 0x0010D8E4 File Offset: 0x0010BAE4
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
						m_soundName = "VO_LOE_15_RESPONSE.prefab:a8accce27ae78cb4fb4b02f58e9c3036",
						m_stringTag = "VO_LOE_15_RESPONSE"
					}
				}
			}
		};
	}

	// Token: 0x060034E8 RID: 13544 RVA: 0x0010D944 File Offset: 0x0010BB44
	public override bool DoAlternateMulliganIntro()
	{
		GameState.Get().GetOpposingSidePlayer().GetDeckZone().SetVisibility(false);
		this.m_zonesToHide.Clear();
		this.m_zonesToHide.AddRange(ZoneMgr.Get().FindZonesForTag(TAG_ZONE.HAND));
		this.m_zonesToHide.AddRange(ZoneMgr.Get().FindZonesForTag(TAG_ZONE.DECK));
		foreach (Zone zone in this.m_zonesToHide)
		{
			foreach (Card card in zone.GetCards())
			{
				card.HideCard();
				card.SetDoNotSort(true);
			}
		}
		return false;
	}

	// Token: 0x060034E9 RID: 13545 RVA: 0x0010DA24 File Offset: 0x0010BC24
	public override IEnumerator DoActionsAfterIntroBeforeMulligan()
	{
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		Actor enemyActor = opposingSidePlayer.GetHeroCard().GetActor();
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("LOE_DeckTakeEvent.prefab:1d55e39305085094cbe8598e5fde37aa", AssetLoadingOptions.None);
		LOE_DeckTakeEvent deckTakeEvent = gameObject.GetComponent<LOE_DeckTakeEvent>();
		yield return new WaitForEndOfFrame();
		ZoneDeck deckZone = GameState.Get().GetOpposingSidePlayer().GetDeckZone();
		deckZone.SetVisibility(true);
		Gameplay.Get().SwapCardBacks();
		deckZone.SetVisibility(false);
		GameState.Get().GetFriendlySidePlayer().GetDeckZone().SetVisibility(false);
		Gameplay.Get().StartCoroutine(deckTakeEvent.PlayTakeDeckAnim());
		yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOEA15_1_START_07.prefab:663467aee38cfa3429a9f89eee6177fe", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
		while (deckTakeEvent.AnimIsPlaying())
		{
			yield return null;
		}
		Gameplay.Get().StartCoroutine(deckTakeEvent.PlayReplacementDeckAnim());
		yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_15_SPARE.prefab:a25a36df79b66394ebb054c12e4a9441", 3f, 1f, true, false));
		while (deckTakeEvent.AnimIsPlaying())
		{
			yield return null;
		}
		foreach (Zone zone in this.m_zonesToHide)
		{
			foreach (Card card in zone.GetCards())
			{
				card.ShowCard();
				card.SetDoNotSort(false);
			}
		}
		this.m_zonesToHide.Clear();
		yield break;
	}

	// Token: 0x060034EA RID: 13546 RVA: 0x0010DA33 File Offset: 0x0010BC33
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		Actor actor = opposingSidePlayer.GetHeroCard().GetActor();
		if (!this.m_lowHealth && opposingSidePlayer.GetHero().GetCurrentHealth() < 10 && GameState.Get().GetFriendlySidePlayer().GetHero().GetCurrentHealth() > 19)
		{
			this.m_lowHealth = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOEA15_1_LOW_HEALTH_10.prefab:a948e1a494daa01429f9c8af98ba2ba7", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			yield break;
		}
		if (turn != 1)
		{
			if (turn == 5)
			{
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_ELISE_WEIRD_DECK_05.prefab:89915bf24c91ba642bf3ae842e267fe3", 3f, 1f, false, false));
			}
		}
		else if (GameState.Get().GetGameEntity().GetCost() == 1)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOEA15_GOLDEN.prefab:c7173172b70c5ca40b1f3157ee3e5279", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		else
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOEA15_1_TURN1_08.prefab:6a6f93dab22e2aa44bafd03d5da8597a", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x060034EB RID: 13547 RVA: 0x0010DA49 File Offset: 0x0010BC49
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
		if (cardId == "CS2_118")
		{
			if (this.m_magmaRagerLinePlayed)
			{
				yield break;
			}
			this.m_magmaRagerLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOEA15_1_MAGMA_RAGER_09.prefab:86f34787a5e7d5441a6331443d36880b", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x060034EC RID: 13548 RVA: 0x0010DA5F File Offset: 0x0010BC5F
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (GameMgr.Get().IsClassChallengeMission())
		{
			yield break;
		}
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("Rafaam_wrap_Quote.prefab:d7100015bf618604ea93bad6b9f54f8b", "VO_LOEA15_1_LOSS_11.prefab:7a3d507088374a240a490fc098f6791b", 0f, false, false));
			yield break;
		}
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("Rafaam_wrap_Quote.prefab:d7100015bf618604ea93bad6b9f54f8b", "VO_LOEA15_1_WIN_12.prefab:bdad6f025366f8645a65655e1d8fc751", 0f, false, false));
			yield break;
		}
		yield break;
	}

	// Token: 0x04001CCE RID: 7374
	private bool m_magmaRagerLinePlayed;

	// Token: 0x04001CCF RID: 7375
	private bool m_lowHealth;

	// Token: 0x04001CD0 RID: 7376
	private List<Zone> m_zonesToHide = new List<Zone>();
}
