using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KAR08_Nightbane : KAR_MissionEntity
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		PreloadSound("VO_Curator_Male_ArcaneGolem_NightbaneTurn1_01.prefab:f978a9a33588ae74ebe60e591e6cf238");
		PreloadSound("VO_Curator_Male_ArcaneGolem_NightbaneUnstablePortal_01.prefab:48506385c46091f47832e56cb3bb2628");
		PreloadSound("VO_Curator_Male_ArcaneGolem_NightbaneCorruption_01.prefab:0a9693596358c7b4dbcdf6d6eff5f09e");
		PreloadSound("VO_Curator_Male_ArcaneGolem_NightbaneWin_01.prefab:ffe915d9bd0f53540beb314b0df007a0");
		PreloadSound("VO_Curator_Male_ArcaneGolem_NightbaneTurn3_01.prefab:b9cb531bca9116d4f902e157611789e8");
		PreloadSound("VO_Moroes_Male_Human_NightbaneTurn3_01.prefab:f4d914a3415bb074c825a09dcc164d86");
		PreloadSound("VO_Moroes_Male_Human_NightbaneTurn7_01.prefab:6361ea4ac8ffcf646849993f464a6b06");
		PreloadSound("VO_Nightbane_Roar.prefab:d5f389f135f07b547b98e7d58a4fcd20");
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
						m_soundName = "VO_Nightbane_Roar.prefab:d5f389f135f07b547b98e7d58a4fcd20",
						m_stringTag = "VO_Nightbane_Roar"
					}
				}
			}
		};
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		switch (turn)
		{
		case 1:
			yield return PlayOpeningLine("Curator_BigQuote.prefab:f01875528133988418925bd870aa7b81", "VO_Curator_Male_ArcaneGolem_NightbaneTurn1_01.prefab:f978a9a33588ae74ebe60e591e6cf238");
			break;
		case 4:
			GameState.Get().SetBusy(busy: true);
			yield return PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_NightbaneTurn3_01.prefab:f4d914a3415bb074c825a09dcc164d86");
			yield return PlayAdventureFlavorLine("Curator_BigQuote.prefab:f01875528133988418925bd870aa7b81", "VO_Curator_Male_ArcaneGolem_NightbaneTurn3_01.prefab:b9cb531bca9116d4f902e157611789e8");
			GameState.Get().SetBusy(busy: false);
			break;
		case 8:
			GameState.Get().SetBusy(busy: true);
			yield return PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_NightbaneTurn7_01.prefab:6361ea4ac8ffcf646849993f464a6b06");
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
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			switch (cardId)
			{
			case "GVG_003":
				yield return PlayEasterEggLine("Curator_BigQuote.prefab:f01875528133988418925bd870aa7b81", "VO_Curator_Male_ArcaneGolem_NightbaneUnstablePortal_01.prefab:48506385c46091f47832e56cb3bb2628");
				break;
			case "OG_133":
			case "OG_280":
			case "OG_134":
			case "OG_042":
				m_playedLines.Add("OG_133");
				m_playedLines.Add("OG_280");
				m_playedLines.Add("OG_134");
				m_playedLines.Add("OG_042");
				yield return PlayEasterEggLine("Curator_BigQuote.prefab:f01875528133988418925bd870aa7b81", "VO_Curator_Male_ArcaneGolem_NightbaneCorruption_01.prefab:0a9693596358c7b4dbcdf6d6eff5f09e");
				break;
			}
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return PlayClosingLine("Curator_Quote.prefab:ab58be80382875e4cbaa766fda73cd39", "VO_Curator_Male_ArcaneGolem_NightbaneWin_01.prefab:ffe915d9bd0f53540beb314b0df007a0");
		}
	}
}
