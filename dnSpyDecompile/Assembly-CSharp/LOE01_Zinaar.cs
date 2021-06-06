using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200038A RID: 906
public class LOE01_Zinaar : LOE_MissionEntity
{
	// Token: 0x06003481 RID: 13441 RVA: 0x0010C5E0 File Offset: 0x0010A7E0
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_LOE_02_RESPONSE.prefab:81df49b8799ffe7408d1ca6d13a0b1a9");
		base.PreloadSound("VO_LOE_02_WISH.prefab:e58bbad31e7b0e944a7c5ae8c67a6837");
		base.PreloadSound("VO_LOE_02_START2.prefab:3f963f60c4ecb2341a001d4a8e80e4f0");
		base.PreloadSound("VO_LOE_02_START3.prefab:bf687ef7a2095b44bb25ff73d6795d90");
		base.PreloadSound("VO_LOE_02_TURN_6.prefab:d283d2a06fd1f4146a091e13e097492b");
		base.PreloadSound("VO_LOE_ZINAAR_TURN_6_CARTOGRAPHER_2.prefab:240f36f0d8777774387afe495f53b2d5");
		base.PreloadSound("VO_LOE_02_TURN_6_2.prefab:1bea540bd8f9f134c92c6841cd6e564d");
		base.PreloadSound("VO_LOE_ZINAAR_TURN_6_CARTOGRAPHER_2_ALT.prefab:13b73e3d2ea111f439b866c0da62773c");
		base.PreloadSound("VO_LOE_02_TURN_10.prefab:9c92e0752f28be44d902d98ea9617ab1");
		base.PreloadSound("VO_LOE_ZINAAR_TURN_10_CARTOGRAPHER_2.prefab:f4a2d6f5c7786cc4297a73fef98937fd");
		base.PreloadSound("VO_LOE_02_WIN.prefab:f311289fe2c9b5446943c3cb69f402da");
		base.PreloadSound("VO_LOE_02_MORE_WISHES.prefab:67291015ffb2c8f44b3e20553f25001b");
	}

	// Token: 0x06003482 RID: 13442 RVA: 0x0010C674 File Offset: 0x0010A874
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
						m_soundName = "VO_LOE_02_RESPONSE.prefab:81df49b8799ffe7408d1ca6d13a0b1a9",
						m_stringTag = "VO_LOE_02_RESPONSE"
					}
				}
			}
		};
	}

	// Token: 0x06003483 RID: 13443 RVA: 0x0010C6D3 File Offset: 0x0010A8D3
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 2)
		{
			GameState.Get().SetBusy(true);
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_02_WISH.prefab:e58bbad31e7b0e944a7c5ae8c67a6837", 3f, 1f, false, false));
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06003484 RID: 13444 RVA: 0x0010C6E9 File Offset: 0x0010A8E9
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		string cardId = entity.GetCardId();
		if (cardId == "LOEA02_06")
		{
			if (this.m_wishMoreWishesLinePlayed)
			{
				yield break;
			}
			this.m_wishMoreWishesLinePlayed = true;
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_02_MORE_WISHES.prefab:67291015ffb2c8f44b3e20553f25001b", 3f, 1f, false, false));
		}
		yield break;
	}

	// Token: 0x06003485 RID: 13445 RVA: 0x0010C6FF File Offset: 0x0010A8FF
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (turn <= 7)
		{
			if (turn != 1)
			{
				if (turn == 7)
				{
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_02_TURN_6.prefab:d283d2a06fd1f4146a091e13e097492b", 3f, 1f, false, false));
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_ZINAAR_TURN_6_CARTOGRAPHER_2.prefab:240f36f0d8777774387afe495f53b2d5", 3f, 1f, false, false));
				}
			}
			else
			{
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_02_START2.prefab:3f963f60c4ecb2341a001d4a8e80e4f0", 3f, 1f, false, false));
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_02_START3.prefab:bf687ef7a2095b44bb25ff73d6795d90", 3f, 1f, false, false));
			}
		}
		else if (turn != 9)
		{
			if (turn == 13)
			{
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_02_TURN_10.prefab:9c92e0752f28be44d902d98ea9617ab1", 3f, 1f, false, false));
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_ZINAAR_TURN_10_CARTOGRAPHER_2.prefab:f4a2d6f5c7786cc4297a73fef98937fd", 3f, 1f, false, false));
			}
		}
		else
		{
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_02_TURN_6_2.prefab:1bea540bd8f9f134c92c6841cd6e564d", 3f, 1f, false, false));
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_ZINAAR_TURN_6_CARTOGRAPHER_2_ALT.prefab:13b73e3d2ea111f439b866c0da62773c", 3f, 1f, false, false));
		}
		yield break;
	}

	// Token: 0x06003486 RID: 13446 RVA: 0x0010C715 File Offset: 0x0010A915
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("Reno_Quote.prefab:0a2e34fa6782a0747b4f5d5574d1331a", "VO_LOE_02_WIN.prefab:f311289fe2c9b5446943c3cb69f402da", 0f, false, false));
		}
		yield break;
	}

	// Token: 0x04001CB8 RID: 7352
	private bool m_wishMoreWishesLinePlayed;
}
