using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KAR10_Aran : KAR_MissionEntity
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		PreloadSound("VO_Aran_Male_Shade_AranEmoteResponse_01.prefab:8daae2c5e8533c142b36966665234991");
		PreloadSound("VO_Aran_Male_Shade_AranCrackle_01.prefab:bc888dcf38fea67478451daed3a95b07");
		PreloadSound("VO_Aran_Male_Shade_AranHeroPower_01.prefab:efd33279a659c4b45b71371592ed8477");
		PreloadSound("VO_Aran_Male_Shade_AranTurn1_01.prefab:145fc99930a9bf94e9220b73440f603b");
		PreloadSound("VO_Aran_Male_Shade_AranTurn5_01.prefab:3f761320471ef964381b2eb26650cefd");
		PreloadSound("VO_Aran_Male_Shade_AranSpell_03.prefab:00d1e54e1dcdfbe4497ed8011d01f4fb");
		PreloadSound("VO_Aran_Male_Shade_AranSpell_04.prefab:95571da88393435459da14036a4f20f8");
		PreloadSound("VO_Aran_Male_Shade_AranMedivhSkin_01.prefab:2c2804e44bd75034a9e2c6a2fb86e1ba");
		PreloadSound("VO_Aran_Male_Shade_AranTrons_02.prefab:b37a84a69b76ea24993c7afbd4c0212c");
		PreloadSound("VO_Moroes_Male_Human_AranWin_01.prefab:4a6dccd0f23c83344b6bb8f61356f52b");
		PreloadSound("VO_Moroes_Male_Human_AranTurn1_01.prefab:e6314824bdc74644f88954866157c664");
		PreloadSound("VO_Moroes_Male_Human_AranTurn9_04.prefab:007f118344eac0446a75031c63ba1712");
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
						m_soundName = "VO_Aran_Male_Shade_AranEmoteResponse_01.prefab:8daae2c5e8533c142b36966665234991",
						m_stringTag = "VO_Aran_Male_Shade_AranEmoteResponse_01"
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (!m_playedLines.Contains(item))
		{
			m_playedLines.Add(item);
			switch (missionEvent)
			{
			case 1:
				GameState.Get().SetBusy(busy: true);
				yield return PlayEasterEggLine(actor, "VO_Aran_Male_Shade_AranCrackle_01.prefab:bc888dcf38fea67478451daed3a95b07");
				GameState.Get().SetBusy(busy: false);
				break;
			case 2:
				yield return PlayBossLine(actor, "VO_Aran_Male_Shade_AranHeroPower_01.prefab:efd33279a659c4b45b71371592ed8477");
				break;
			}
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			if (ShouldPlayOpeningLine("VO_Aran_Male_Shade_AranTurn1_01.prefab:145fc99930a9bf94e9220b73440f603b"))
			{
				yield return PlayOpeningLine(actor, "VO_Aran_Male_Shade_AranTurn1_01.prefab:145fc99930a9bf94e9220b73440f603b");
				yield return PlayMissionFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_AranTurn1_01.prefab:e6314824bdc74644f88954866157c664");
			}
			break;
		case 4:
			GameState.Get().SetBusy(busy: true);
			yield return PlayMissionFlavorLine(actor, "VO_Aran_Male_Shade_AranTurn5_01.prefab:3f761320471ef964381b2eb26650cefd");
			GameState.Get().SetBusy(busy: false);
			break;
		case 8:
			GameState.Get().SetBusy(busy: true);
			yield return PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_AranTurn9_04.prefab:007f118344eac0446a75031c63ba1712");
			GameState.Get().SetBusy(busy: false);
			break;
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
		if (!(cardId == "CS2_028"))
		{
			if (cardId == "CS2_033")
			{
				yield return PlayBossLine(actor, "VO_Aran_Male_Shade_AranSpell_04.prefab:95571da88393435459da14036a4f20f8");
			}
		}
		else
		{
			yield return PlayBossLine(actor, "VO_Aran_Male_Shade_AranSpell_03.prefab:00d1e54e1dcdfbe4497ed8011d01f4fb");
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
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			switch (cardId)
			{
			case "CS2_034_H1":
				yield return PlayEasterEggLine(actor, "VO_Aran_Male_Shade_AranMedivhSkin_01.prefab:2c2804e44bd75034a9e2c6a2fb86e1ba");
				break;
			case "GVG_085":
			case "OG_145":
				m_playedLines.Add("GVG_085");
				m_playedLines.Add("OG_145");
				yield return PlayEasterEggLine(actor, "VO_Aran_Male_Shade_AranTrons_02.prefab:b37a84a69b76ea24993c7afbd4c0212c");
				break;
			}
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return PlayClosingLine("Moroes_Quote.prefab:ea3a21837aab2b0448ce4090103724cf", "VO_Moroes_Male_Human_AranWin_01.prefab:4a6dccd0f23c83344b6bb8f61356f52b");
		}
	}
}
