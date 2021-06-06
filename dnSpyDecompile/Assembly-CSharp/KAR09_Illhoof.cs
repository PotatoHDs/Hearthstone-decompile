using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003A1 RID: 929
public class KAR09_Illhoof : KAR_MissionEntity
{
	// Token: 0x0600353E RID: 13630 RVA: 0x0010ED48 File Offset: 0x0010CF48
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_Illhoof_Male_Demon_IllhoofSummonImps_01.prefab:cc3a062bf710c454ca87743d4bccc7d6");
		base.PreloadSound("VO_Illhoof_Male_Demon_IllhoofSummoningPortal_01.prefab:36097ec5b99abda439a1606c2270fced");
		base.PreloadSound("VO_Illhoof_Male_Demon_IllhoofEmoteResponse_01.prefab:c3681690f22db464d8e796bf98a02d57");
		base.PreloadSound("VO_Illhoof_Male_Demon_IllhoofWounded_01.prefab:b46f0340df9465e4f840d1303fc3b940");
		base.PreloadSound("VO_Illhoof_Male_Demon_IllhoofTurn1_01.prefab:bb152da0f208c2342baa7ea5bf44e68d");
		base.PreloadSound("VO_Illhoof_Male_Demon_IlhoofKilrek_01.prefab:57aeb9e4838443e4b9e25968c7db9045");
		base.PreloadSound("VO_Moroes_Male_Human_IllhoofKilrekResponse_01.prefab:dc74ecda46619da4abe6e30c0b555e12");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_IllhoofKilrekResponse_01.prefab:d0dc54e5fd4f0ca41a54a9f2d7e56a03");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_IllhoofSenseDemons_01.prefab:0f0c06c276d2a9443933b7ce3daa39a2");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_IllhoofWin_01.prefab:c088ea75a2aba3844b4a5676e5eac371");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_IllhoofTurn1_01.prefab:b4039f0c7dade924ba570485545f46cd");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_IllhoofTurn5_01.prefab:3f292bf97725dce4e9aa224975fc1ba0");
	}

	// Token: 0x0600353F RID: 13631 RVA: 0x0010EDDC File Offset: 0x0010CFDC
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
						m_soundName = "VO_Illhoof_Male_Demon_IllhoofEmoteResponse_01.prefab:c3681690f22db464d8e796bf98a02d57",
						m_stringTag = "VO_Illhoof_Male_Demon_IllhoofEmoteResponse_01"
					}
				}
			}
		};
	}

	// Token: 0x06003540 RID: 13632 RVA: 0x0010EE3B File Offset: 0x0010D03B
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(item))
		{
			yield break;
		}
		this.m_playedLines.Add(item);
		if (missionEvent == 1)
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayMissionFlavorLine(actor, "VO_Illhoof_Male_Demon_IllhoofWounded_01.prefab:b46f0340df9465e4f840d1303fc3b940", 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06003541 RID: 13633 RVA: 0x0010EE51 File Offset: 0x0010D051
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (turn == 1 && base.ShouldPlayOpeningLine("VO_Illhoof_Male_Demon_IllhoofTurn1_01.prefab:bb152da0f208c2342baa7ea5bf44e68d"))
		{
			yield return base.PlayOpeningLine(actor, "VO_Illhoof_Male_Demon_IllhoofTurn1_01.prefab:bb152da0f208c2342baa7ea5bf44e68d", 2.5f);
			yield return base.PlayMissionFlavorLine("Curator_BigQuote.prefab:f01875528133988418925bd870aa7b81", "VO_Curator_Male_ArcaneGolem_IllhoofTurn1_01.prefab:b4039f0c7dade924ba570485545f46cd", 2.5f);
		}
		yield break;
	}

	// Token: 0x06003542 RID: 13634 RVA: 0x0010EE67 File Offset: 0x0010D067
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
		if (!(cardId == "KARA_09_05") && !(cardId == "KARA_09_05heroic"))
		{
			if (cardId == "KARA_09_03" || cardId == "KARA_09_03heroic")
			{
				yield return base.PlayBossLine(actor, "VO_Illhoof_Male_Demon_IllhoofSummonImps_01.prefab:cc3a062bf710c454ca87743d4bccc7d6", 2.5f);
			}
		}
		else if (base.ShouldPlayBossLine("VO_Illhoof_Male_Demon_IlhoofKilrek_01.prefab:57aeb9e4838443e4b9e25968c7db9045"))
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayBossLine(actor, "VO_Illhoof_Male_Demon_IlhoofKilrek_01.prefab:57aeb9e4838443e4b9e25968c7db9045", 2.5f);
			yield return base.PlayMissionFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_IllhoofKilrekResponse_01.prefab:dc74ecda46619da4abe6e30c0b555e12", 2.5f);
			yield return base.PlayMissionFlavorLine("Curator_BigQuote.prefab:f01875528133988418925bd870aa7b81", "VO_Curator_Male_ArcaneGolem_IllhoofKilrekResponse_01.prefab:d0dc54e5fd4f0ca41a54a9f2d7e56a03", 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06003543 RID: 13635 RVA: 0x0010EE7D File Offset: 0x0010D07D
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
		if (!(cardId == "EX1_315"))
		{
			if (cardId == "EX1_317")
			{
				yield return base.PlayEasterEggLine("Curator_BigQuote.prefab:f01875528133988418925bd870aa7b81", "VO_Curator_Male_ArcaneGolem_IllhoofSenseDemons_01.prefab:0f0c06c276d2a9443933b7ce3daa39a2", 2.5f);
			}
		}
		else
		{
			yield return base.PlayEasterEggLine(actor, "VO_Illhoof_Male_Demon_IllhoofSummoningPortal_01.prefab:36097ec5b99abda439a1606c2270fced", 2.5f);
		}
		yield break;
	}

	// Token: 0x06003544 RID: 13636 RVA: 0x0010EE93 File Offset: 0x0010D093
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return base.PlayClosingLine("Curator_Quote.prefab:ab58be80382875e4cbaa766fda73cd39", "VO_Curator_Male_ArcaneGolem_IllhoofWin_01.prefab:c088ea75a2aba3844b4a5676e5eac371", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001CDB RID: 7387
	private HashSet<string> m_playedLines = new HashSet<string>();
}
