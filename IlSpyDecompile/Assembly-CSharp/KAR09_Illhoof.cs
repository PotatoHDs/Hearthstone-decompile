using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KAR09_Illhoof : KAR_MissionEntity
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		PreloadSound("VO_Illhoof_Male_Demon_IllhoofSummonImps_01.prefab:cc3a062bf710c454ca87743d4bccc7d6");
		PreloadSound("VO_Illhoof_Male_Demon_IllhoofSummoningPortal_01.prefab:36097ec5b99abda439a1606c2270fced");
		PreloadSound("VO_Illhoof_Male_Demon_IllhoofEmoteResponse_01.prefab:c3681690f22db464d8e796bf98a02d57");
		PreloadSound("VO_Illhoof_Male_Demon_IllhoofWounded_01.prefab:b46f0340df9465e4f840d1303fc3b940");
		PreloadSound("VO_Illhoof_Male_Demon_IllhoofTurn1_01.prefab:bb152da0f208c2342baa7ea5bf44e68d");
		PreloadSound("VO_Illhoof_Male_Demon_IlhoofKilrek_01.prefab:57aeb9e4838443e4b9e25968c7db9045");
		PreloadSound("VO_Moroes_Male_Human_IllhoofKilrekResponse_01.prefab:dc74ecda46619da4abe6e30c0b555e12");
		PreloadSound("VO_Curator_Male_ArcaneGolem_IllhoofKilrekResponse_01.prefab:d0dc54e5fd4f0ca41a54a9f2d7e56a03");
		PreloadSound("VO_Curator_Male_ArcaneGolem_IllhoofSenseDemons_01.prefab:0f0c06c276d2a9443933b7ce3daa39a2");
		PreloadSound("VO_Curator_Male_ArcaneGolem_IllhoofWin_01.prefab:c088ea75a2aba3844b4a5676e5eac371");
		PreloadSound("VO_Curator_Male_ArcaneGolem_IllhoofTurn1_01.prefab:b4039f0c7dade924ba570485545f46cd");
		PreloadSound("VO_Curator_Male_ArcaneGolem_IllhoofTurn5_01.prefab:3f292bf97725dce4e9aa224975fc1ba0");
	}

	protected override void InitEmoteResponses()
	{
		m_emoteResponseGroups = new List<EmoteResponseGroup>
		{
			new EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>(MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS),
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_Illhoof_Male_Demon_IllhoofEmoteResponse_01.prefab:c3681690f22db464d8e796bf98a02d57",
						m_stringTag = "VO_Illhoof_Male_Demon_IllhoofEmoteResponse_01"
					}
				}
			}
		};
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (!m_playedLines.Contains(item))
		{
			m_playedLines.Add(item);
			if (missionEvent == 1)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayMissionFlavorLine(actor, "VO_Illhoof_Male_Demon_IllhoofWounded_01.prefab:b46f0340df9465e4f840d1303fc3b940");
				GameState.Get().SetBusy(busy: false);
			}
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (turn == 1 && ShouldPlayOpeningLine("VO_Illhoof_Male_Demon_IllhoofTurn1_01.prefab:bb152da0f208c2342baa7ea5bf44e68d"))
		{
			yield return PlayOpeningLine(actor, "VO_Illhoof_Male_Demon_IllhoofTurn1_01.prefab:bb152da0f208c2342baa7ea5bf44e68d");
			yield return PlayMissionFlavorLine("Curator_BigQuote.prefab:f01875528133988418925bd870aa7b81", "VO_Curator_Male_ArcaneGolem_IllhoofTurn1_01.prefab:b4039f0c7dade924ba570485545f46cd");
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (cardId)
		{
		case "KARA_09_05":
		case "KARA_09_05heroic":
			if (ShouldPlayBossLine("VO_Illhoof_Male_Demon_IlhoofKilrek_01.prefab:57aeb9e4838443e4b9e25968c7db9045"))
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(actor, "VO_Illhoof_Male_Demon_IlhoofKilrek_01.prefab:57aeb9e4838443e4b9e25968c7db9045");
				yield return PlayMissionFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_IllhoofKilrekResponse_01.prefab:dc74ecda46619da4abe6e30c0b555e12");
				yield return PlayMissionFlavorLine("Curator_BigQuote.prefab:f01875528133988418925bd870aa7b81", "VO_Curator_Male_ArcaneGolem_IllhoofKilrekResponse_01.prefab:d0dc54e5fd4f0ca41a54a9f2d7e56a03");
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case "KARA_09_03":
		case "KARA_09_03heroic":
			yield return PlayBossLine(actor, "VO_Illhoof_Male_Demon_IllhoofSummonImps_01.prefab:cc3a062bf710c454ca87743d4bccc7d6");
			break;
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "EX1_315"))
		{
			if (cardId == "EX1_317")
			{
				yield return PlayEasterEggLine("Curator_BigQuote.prefab:f01875528133988418925bd870aa7b81", "VO_Curator_Male_ArcaneGolem_IllhoofSenseDemons_01.prefab:0f0c06c276d2a9443933b7ce3daa39a2");
			}
		}
		else
		{
			yield return PlayEasterEggLine(actor, "VO_Illhoof_Male_Demon_IllhoofSummoningPortal_01.prefab:36097ec5b99abda439a1606c2270fced");
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return PlayClosingLine("Curator_Quote.prefab:ab58be80382875e4cbaa766fda73cd39", "VO_Curator_Male_ArcaneGolem_IllhoofWin_01.prefab:c088ea75a2aba3844b4a5676e5eac371");
		}
	}
}
