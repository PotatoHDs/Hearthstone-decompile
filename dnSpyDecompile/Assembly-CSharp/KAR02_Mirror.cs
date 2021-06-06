using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200039A RID: 922
public class KAR02_Mirror : KAR_MissionEntity
{
	// Token: 0x0600350B RID: 13579 RVA: 0x0010E030 File Offset: 0x0010C230
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_Mirror_Male_Mirror_MirrorEmoteResponse_01.prefab:11f1ede615326154fab38c0bc6a55b90");
		base.PreloadSound("VO_Mirror_Male_Mirror_MirrorWellPlayedResponse_01.prefab:5901ea5faab95e74aa79d4c5354d3cfe");
		base.PreloadSound("VO_Mirror_Male_Mirror_MirrorFirstCard_01.prefab:abb11971ce998394aab0bb5e4e9eee4a");
		base.PreloadSound("VO_Mirror_Male_Mirror_MirrorTurn1_01.prefab:b444a1efe9fa7ac4da92cc232f803abe");
		base.PreloadSound("VO_Mirror_Male_Mirror_MirrorTurn3_01.prefab:f0273f9553383c04fbe95034480cef93");
		base.PreloadSound("VO_Mirror_Male_Mirror_MirrorTurn3_03.prefab:56d9324d1a978c74ab39708c909dd16f");
		base.PreloadSound("VO_Mirror_Male_Mirror_MirrorTurn5_02.prefab:2d8f0ddf302831d4a9c0b5e815652981");
		base.PreloadSound("VO_Mirror_Male_Mirror_MirrorMirrorImage_01.prefab:3ff331329b643284ca06eb7a3fa0001d");
		base.PreloadSound("VO_Mirror_Male_Mirror_MirrorMedivhSkin_01.prefab:c9a9fce27cb32be46a0c6486d57bcdaf");
		base.PreloadSound("VO_Moroes_Male_Human_MirrorTurn5_01.prefab:5b9d0bea3bbe2df43a36cf4072a20586");
		base.PreloadSound("VO_Moroes_Male_Human_MirrorWin_02.prefab:ab4a3ef74dc68ec42b1d0538ce1caf14");
		base.PreloadSound("VO_Moroes_Male_Human_MirrorTurn3_01.prefab:b1dcc6a301543a04d91b532d9640255a");
	}

	// Token: 0x0600350C RID: 13580 RVA: 0x0010E0C4 File Offset: 0x0010C2C4
	protected override void InitEmoteResponses()
	{
		List<EmoteType> list = new List<EmoteType>(MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS);
		list.Remove(EmoteType.WELL_PLAYED);
		this.m_emoteResponseGroups = new List<MissionEntity.EmoteResponseGroup>
		{
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = list,
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_Mirror_Male_Mirror_MirrorEmoteResponse_01.prefab:11f1ede615326154fab38c0bc6a55b90",
						m_stringTag = "VO_Mirror_Male_Mirror_MirrorEmoteResponse_01"
					}
				}
			},
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>
				{
					EmoteType.WELL_PLAYED
				},
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_Mirror_Male_Mirror_MirrorWellPlayedResponse_01.prefab:5901ea5faab95e74aa79d4c5354d3cfe",
						m_stringTag = "VO_Mirror_Male_Mirror_MirrorWellPlayedResponse_01"
					}
				}
			}
		};
	}

	// Token: 0x0600350D RID: 13581 RVA: 0x0010E176 File Offset: 0x0010C376
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(item))
		{
			yield break;
		}
		this.m_playedLines.Add(item);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent == 1)
		{
			yield return base.PlayMissionFlavorLine(actor, "VO_Mirror_Male_Mirror_MirrorFirstCard_01.prefab:abb11971ce998394aab0bb5e4e9eee4a", 2.5f);
		}
		yield break;
	}

	// Token: 0x0600350E RID: 13582 RVA: 0x0010E18C File Offset: 0x0010C38C
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (turn != 1)
		{
			if (turn != 6)
			{
				if (turn == 10)
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_MirrorTurn5_01.prefab:5b9d0bea3bbe2df43a36cf4072a20586", 2.5f);
					yield return base.PlayAdventureFlavorLine(enemyActor, "VO_Mirror_Male_Mirror_MirrorTurn5_02.prefab:2d8f0ddf302831d4a9c0b5e815652981", 2.5f);
					GameState.Get().SetBusy(false);
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayMissionFlavorLine(enemyActor, "VO_Mirror_Male_Mirror_MirrorTurn3_01.prefab:f0273f9553383c04fbe95034480cef93", 2.5f);
				yield return base.PlayMissionFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_MirrorTurn3_01.prefab:b1dcc6a301543a04d91b532d9640255a", 2.5f);
				yield return base.PlayMissionFlavorLine(enemyActor, "VO_Mirror_Male_Mirror_MirrorTurn3_03.prefab:56d9324d1a978c74ab39708c909dd16f", 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			yield return base.PlayOpeningLine(enemyActor, "VO_Mirror_Male_Mirror_MirrorTurn1_01.prefab:b444a1efe9fa7ac4da92cc232f803abe", 2.5f);
		}
		yield break;
	}

	// Token: 0x0600350F RID: 13583 RVA: 0x0010E1A2 File Offset: 0x0010C3A2
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
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "CS2_027"))
		{
			if (cardId == "CS2_034_H1")
			{
				yield return base.PlayEasterEggLine(actor, "VO_Mirror_Male_Mirror_MirrorMedivhSkin_01.prefab:c9a9fce27cb32be46a0c6486d57bcdaf", 2.5f);
			}
		}
		else
		{
			yield return base.PlayEasterEggLine(actor, "VO_Mirror_Male_Mirror_MirrorMirrorImage_01.prefab:3ff331329b643284ca06eb7a3fa0001d", 2.5f);
		}
		yield break;
	}

	// Token: 0x06003510 RID: 13584 RVA: 0x0010E1B8 File Offset: 0x0010C3B8
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return base.PlayClosingLine("Moroes_Quote.prefab:ea3a21837aab2b0448ce4090103724cf", "VO_Moroes_Male_Human_MirrorWin_02.prefab:ab4a3ef74dc68ec42b1d0538ce1caf14", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001CD5 RID: 7381
	private HashSet<string> m_playedLines = new HashSet<string>();
}
