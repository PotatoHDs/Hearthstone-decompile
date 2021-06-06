using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KAR02_Mirror : KAR_MissionEntity
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		PreloadSound("VO_Mirror_Male_Mirror_MirrorEmoteResponse_01.prefab:11f1ede615326154fab38c0bc6a55b90");
		PreloadSound("VO_Mirror_Male_Mirror_MirrorWellPlayedResponse_01.prefab:5901ea5faab95e74aa79d4c5354d3cfe");
		PreloadSound("VO_Mirror_Male_Mirror_MirrorFirstCard_01.prefab:abb11971ce998394aab0bb5e4e9eee4a");
		PreloadSound("VO_Mirror_Male_Mirror_MirrorTurn1_01.prefab:b444a1efe9fa7ac4da92cc232f803abe");
		PreloadSound("VO_Mirror_Male_Mirror_MirrorTurn3_01.prefab:f0273f9553383c04fbe95034480cef93");
		PreloadSound("VO_Mirror_Male_Mirror_MirrorTurn3_03.prefab:56d9324d1a978c74ab39708c909dd16f");
		PreloadSound("VO_Mirror_Male_Mirror_MirrorTurn5_02.prefab:2d8f0ddf302831d4a9c0b5e815652981");
		PreloadSound("VO_Mirror_Male_Mirror_MirrorMirrorImage_01.prefab:3ff331329b643284ca06eb7a3fa0001d");
		PreloadSound("VO_Mirror_Male_Mirror_MirrorMedivhSkin_01.prefab:c9a9fce27cb32be46a0c6486d57bcdaf");
		PreloadSound("VO_Moroes_Male_Human_MirrorTurn5_01.prefab:5b9d0bea3bbe2df43a36cf4072a20586");
		PreloadSound("VO_Moroes_Male_Human_MirrorWin_02.prefab:ab4a3ef74dc68ec42b1d0538ce1caf14");
		PreloadSound("VO_Moroes_Male_Human_MirrorTurn3_01.prefab:b1dcc6a301543a04d91b532d9640255a");
	}

	protected override void InitEmoteResponses()
	{
		List<EmoteType> list = new List<EmoteType>(MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS);
		list.Remove(EmoteType.WELL_PLAYED);
		m_emoteResponseGroups = new List<EmoteResponseGroup>
		{
			new EmoteResponseGroup
			{
				m_triggers = list,
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_Mirror_Male_Mirror_MirrorEmoteResponse_01.prefab:11f1ede615326154fab38c0bc6a55b90",
						m_stringTag = "VO_Mirror_Male_Mirror_MirrorEmoteResponse_01"
					}
				}
			},
			new EmoteResponseGroup
			{
				m_triggers = new List<EmoteType> { EmoteType.WELL_PLAYED },
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_Mirror_Male_Mirror_MirrorWellPlayedResponse_01.prefab:5901ea5faab95e74aa79d4c5354d3cfe",
						m_stringTag = "VO_Mirror_Male_Mirror_MirrorWellPlayedResponse_01"
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
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (!m_playedLines.Contains(item))
		{
			m_playedLines.Add(item);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			if (missionEvent == 1)
			{
				yield return PlayMissionFlavorLine(actor, "VO_Mirror_Male_Mirror_MirrorFirstCard_01.prefab:abb11971ce998394aab0bb5e4e9eee4a");
			}
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			yield return PlayOpeningLine(enemyActor, "VO_Mirror_Male_Mirror_MirrorTurn1_01.prefab:b444a1efe9fa7ac4da92cc232f803abe");
			break;
		case 6:
			GameState.Get().SetBusy(busy: true);
			yield return PlayMissionFlavorLine(enemyActor, "VO_Mirror_Male_Mirror_MirrorTurn3_01.prefab:f0273f9553383c04fbe95034480cef93");
			yield return PlayMissionFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_MirrorTurn3_01.prefab:b1dcc6a301543a04d91b532d9640255a");
			yield return PlayMissionFlavorLine(enemyActor, "VO_Mirror_Male_Mirror_MirrorTurn3_03.prefab:56d9324d1a978c74ab39708c909dd16f");
			GameState.Get().SetBusy(busy: false);
			break;
		case 10:
			GameState.Get().SetBusy(busy: true);
			yield return PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_MirrorTurn5_01.prefab:5b9d0bea3bbe2df43a36cf4072a20586");
			yield return PlayAdventureFlavorLine(enemyActor, "VO_Mirror_Male_Mirror_MirrorTurn5_02.prefab:2d8f0ddf302831d4a9c0b5e815652981");
			GameState.Get().SetBusy(busy: false);
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
		if (!(cardId == "CS2_027"))
		{
			if (cardId == "CS2_034_H1")
			{
				yield return PlayEasterEggLine(actor, "VO_Mirror_Male_Mirror_MirrorMedivhSkin_01.prefab:c9a9fce27cb32be46a0c6486d57bcdaf");
			}
		}
		else
		{
			yield return PlayEasterEggLine(actor, "VO_Mirror_Male_Mirror_MirrorMirrorImage_01.prefab:3ff331329b643284ca06eb7a3fa0001d");
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return PlayClosingLine("Moroes_Quote.prefab:ea3a21837aab2b0448ce4090103724cf", "VO_Moroes_Male_Human_MirrorWin_02.prefab:ab4a3ef74dc68ec42b1d0538ce1caf14");
		}
	}
}
