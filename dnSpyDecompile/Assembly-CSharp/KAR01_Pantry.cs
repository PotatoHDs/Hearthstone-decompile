using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000399 RID: 921
public class KAR01_Pantry : KAR_MissionEntity
{
	// Token: 0x06003503 RID: 13571 RVA: 0x0010DEE0 File Offset: 0x0010C0E0
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_SilverwareGolem_Male_SilverwareGolem_SilverwareEmoteResponse_01.prefab:79a423cdcbf24144e886d12dba0a5422");
		base.PreloadSound("VO_SilverwareGolem_Male_SilverwareGolem_SilverwareKnifeJuggler_01.prefab:0d1779295cc055244982e057e6656dd0");
		base.PreloadSound("VO_Moroes_Male_Human_SilverwareResponse_01.prefab:f0b8b095a9b178d4d9acaf294c95a172");
		base.PreloadSound("VO_Moroes_Male_Human_SilverwareTurn3_02.prefab:be3de0e6492393345a175bb487db70a1");
		base.PreloadSound("VO_SilverwareGolem_Male_SilverwareGolem_SilverwareForkedLightning_01.prefab:dff5ceda9dcf03741a2444f78f0f0e23");
		base.PreloadSound("VO_Moroes_Male_Human_MedivhSkinResponse_01.prefab:74cd0ae7e1f7b9c4ca755b74c156406d");
		base.PreloadSound("VO_Moroes_Male_Human_SilverwareWin_01.prefab:123fc6f36a45c6e468dcd3faeb80a109");
		base.PreloadSound("VO_SilverwareGolem_Male_SilverwareGolem_SilverwarePlateTossing_01.prefab:f2ff07a0e24c0ea4cbda1e154de5462b");
		base.PreloadSound("VO_SilverwareGolem_Male_SilverwareGolem_SilverwareHeroPower_01.prefab:1ae8be96b941b384aabb4487fc24fecb");
	}

	// Token: 0x06003504 RID: 13572 RVA: 0x0010DF50 File Offset: 0x0010C150
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
						m_soundName = "VO_SilverwareGolem_Male_SilverwareGolem_SilverwareEmoteResponse_01.prefab:79a423cdcbf24144e886d12dba0a5422",
						m_stringTag = "VO_SilverwareGolem_Male_SilverwareGolem_SilverwareEmoteResponse_01"
					}
				}
			}
		};
	}

	// Token: 0x06003505 RID: 13573 RVA: 0x0010DFAF File Offset: 0x0010C1AF
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
		if (missionEvent != 1)
		{
			if (missionEvent == 2)
			{
				yield return base.PlayMissionFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_SilverwareResponse_01.prefab:f0b8b095a9b178d4d9acaf294c95a172", 2.5f);
			}
		}
		else
		{
			yield return base.PlayEasterEggLine(actor, "VO_SilverwareGolem_Male_SilverwareGolem_SilverwareKnifeJuggler_01.prefab:0d1779295cc055244982e057e6656dd0", 2.5f);
		}
		yield break;
	}

	// Token: 0x06003506 RID: 13574 RVA: 0x0010DFC5 File Offset: 0x0010C1C5
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (turn == 1)
		{
			yield return base.PlayOpeningLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_SilverwareTurn3_02.prefab:be3de0e6492393345a175bb487db70a1", 2.5f);
		}
		yield break;
	}

	// Token: 0x06003507 RID: 13575 RVA: 0x0010DFDB File Offset: 0x0010C1DB
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
		if (!(cardId == "EX1_251"))
		{
			if (cardId == "CS2_034_H1")
			{
				yield return base.PlayEasterEggLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_MedivhSkinResponse_01.prefab:74cd0ae7e1f7b9c4ca755b74c156406d", 2.5f);
			}
		}
		else
		{
			yield return base.PlayEasterEggLine(actor, "VO_SilverwareGolem_Male_SilverwareGolem_SilverwareForkedLightning_01.prefab:dff5ceda9dcf03741a2444f78f0f0e23", 2.5f);
		}
		yield break;
	}

	// Token: 0x06003508 RID: 13576 RVA: 0x0010DFF1 File Offset: 0x0010C1F1
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
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "KAR_A02_13") && !(cardId == "KAR_A02_13H"))
		{
			if (cardId == "KAR_A02_11")
			{
				yield return base.PlayBossLine(actor, "VO_SilverwareGolem_Male_SilverwareGolem_SilverwarePlateTossing_01.prefab:f2ff07a0e24c0ea4cbda1e154de5462b", 2.5f);
			}
		}
		else
		{
			yield return base.PlayBossLine(actor, "VO_SilverwareGolem_Male_SilverwareGolem_SilverwareHeroPower_01.prefab:1ae8be96b941b384aabb4487fc24fecb", 2.5f);
		}
		yield break;
	}

	// Token: 0x06003509 RID: 13577 RVA: 0x0010E007 File Offset: 0x0010C207
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return base.PlayClosingLine("Moroes_Quote.prefab:ea3a21837aab2b0448ce4090103724cf", "VO_Moroes_Male_Human_SilverwareWin_01.prefab:123fc6f36a45c6e468dcd3faeb80a109", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001CD4 RID: 7380
	private HashSet<string> m_playedLines = new HashSet<string>();
}
