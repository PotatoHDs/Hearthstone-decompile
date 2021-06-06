using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200038F RID: 911
public class LOE08_Archaedas : LOE_MissionEntity
{
	// Token: 0x060034BE RID: 13502 RVA: 0x0010D1DC File Offset: 0x0010B3DC
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_LOE_08_RESPONSE.prefab:4efe606a6afa83b459855b0d4566f17e");
		base.PreloadSound("VO_LOEA08_TURN_1_BRANN.prefab:c48fbd5e80d73194c91ead50e9ee20ef");
		base.PreloadSound("VO_LOE_ARCHAEDAS_TURN_1_CARTOGRAPHER.prefab:1fbc6b6415f7c604cb00d0b88651e303");
		base.PreloadSound("VO_LOE_08_LANDSLIDE.prefab:c56764ff130183f4688c0dfb30eaf8b2");
		base.PreloadSound("VO_LOE_08_ANIMATE_STONE.prefab:75c31e408053e0748aae95242e662f27");
		base.PreloadSound("VO_LOE_08_WIN.prefab:d40a0f7dc56bcf74692815bb06710a00");
	}

	// Token: 0x060034BF RID: 13503 RVA: 0x0010D22C File Offset: 0x0010B42C
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
						m_soundName = "VO_LOE_08_RESPONSE.prefab:4efe606a6afa83b459855b0d4566f17e",
						m_stringTag = "VO_LOE_08_RESPONSE"
					}
				}
			}
		};
	}

	// Token: 0x060034C0 RID: 13504 RVA: 0x0010D28B File Offset: 0x0010B48B
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (turn == 1)
		{
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA08_TURN_1_BRANN.prefab:c48fbd5e80d73194c91ead50e9ee20ef", 3f, 1f, false, false));
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_ARCHAEDAS_TURN_1_CARTOGRAPHER.prefab:1fbc6b6415f7c604cb00d0b88651e303", 5f, 1f, false, false));
		}
		yield break;
	}

	// Token: 0x060034C1 RID: 13505 RVA: 0x0010D2A1 File Offset: 0x0010B4A1
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
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		string cardId = entity.GetCardId();
		if (!(cardId == "LOEA06_04"))
		{
			if (cardId == "LOEA06_03")
			{
				this.m_playedLines.Add(cardId);
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOE_08_ANIMATE_STONE.prefab:75c31e408053e0748aae95242e662f27", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			this.m_playedLines.Add(cardId);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOE_08_LANDSLIDE.prefab:c56764ff130183f4688c0dfb30eaf8b2", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x060034C2 RID: 13506 RVA: 0x0010D2B7 File Offset: 0x0010B4B7
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("Brann_Quote.prefab:2c11651ab7740924189734944b8d7089", "VO_LOE_08_WIN.prefab:d40a0f7dc56bcf74692815bb06710a00", 0f, false, false));
		}
		yield break;
	}

	// Token: 0x04001CC5 RID: 7365
	private HashSet<string> m_playedLines = new HashSet<string>();
}
