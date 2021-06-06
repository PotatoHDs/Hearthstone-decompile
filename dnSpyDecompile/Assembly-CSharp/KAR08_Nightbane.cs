using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003A0 RID: 928
public class KAR08_Nightbane : KAR_MissionEntity
{
	// Token: 0x06003538 RID: 13624 RVA: 0x0010EC2C File Offset: 0x0010CE2C
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_NightbaneTurn1_01.prefab:f978a9a33588ae74ebe60e591e6cf238");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_NightbaneUnstablePortal_01.prefab:48506385c46091f47832e56cb3bb2628");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_NightbaneCorruption_01.prefab:0a9693596358c7b4dbcdf6d6eff5f09e");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_NightbaneWin_01.prefab:ffe915d9bd0f53540beb314b0df007a0");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_NightbaneTurn3_01.prefab:b9cb531bca9116d4f902e157611789e8");
		base.PreloadSound("VO_Moroes_Male_Human_NightbaneTurn3_01.prefab:f4d914a3415bb074c825a09dcc164d86");
		base.PreloadSound("VO_Moroes_Male_Human_NightbaneTurn7_01.prefab:6361ea4ac8ffcf646849993f464a6b06");
		base.PreloadSound("VO_Nightbane_Roar.prefab:d5f389f135f07b547b98e7d58a4fcd20");
	}

	// Token: 0x06003539 RID: 13625 RVA: 0x0010EC94 File Offset: 0x0010CE94
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
						m_soundName = "VO_Nightbane_Roar.prefab:d5f389f135f07b547b98e7d58a4fcd20",
						m_stringTag = "VO_Nightbane_Roar"
					}
				}
			}
		};
	}

	// Token: 0x0600353A RID: 13626 RVA: 0x0010ECF3 File Offset: 0x0010CEF3
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (turn != 1)
		{
			if (turn != 4)
			{
				if (turn == 8)
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_NightbaneTurn7_01.prefab:6361ea4ac8ffcf646849993f464a6b06", 2.5f);
					GameState.Get().SetBusy(false);
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_NightbaneTurn3_01.prefab:f4d914a3415bb074c825a09dcc164d86", 2.5f);
				yield return base.PlayAdventureFlavorLine("Curator_BigQuote.prefab:f01875528133988418925bd870aa7b81", "VO_Curator_Male_ArcaneGolem_NightbaneTurn3_01.prefab:b9cb531bca9116d4f902e157611789e8", 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			yield return base.PlayOpeningLine("Curator_BigQuote.prefab:f01875528133988418925bd870aa7b81", "VO_Curator_Male_ArcaneGolem_NightbaneTurn1_01.prefab:f978a9a33588ae74ebe60e591e6cf238", 2.5f);
		}
		yield break;
	}

	// Token: 0x0600353B RID: 13627 RVA: 0x0010ED09 File Offset: 0x0010CF09
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
		if (!(cardId == "GVG_003"))
		{
			if (cardId == "OG_133" || cardId == "OG_280" || cardId == "OG_134" || cardId == "OG_042")
			{
				this.m_playedLines.Add("OG_133");
				this.m_playedLines.Add("OG_280");
				this.m_playedLines.Add("OG_134");
				this.m_playedLines.Add("OG_042");
				yield return base.PlayEasterEggLine("Curator_BigQuote.prefab:f01875528133988418925bd870aa7b81", "VO_Curator_Male_ArcaneGolem_NightbaneCorruption_01.prefab:0a9693596358c7b4dbcdf6d6eff5f09e", 2.5f);
			}
		}
		else
		{
			yield return base.PlayEasterEggLine("Curator_BigQuote.prefab:f01875528133988418925bd870aa7b81", "VO_Curator_Male_ArcaneGolem_NightbaneUnstablePortal_01.prefab:48506385c46091f47832e56cb3bb2628", 2.5f);
		}
		yield break;
	}

	// Token: 0x0600353C RID: 13628 RVA: 0x0010ED1F File Offset: 0x0010CF1F
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return base.PlayClosingLine("Curator_Quote.prefab:ab58be80382875e4cbaa766fda73cd39", "VO_Curator_Male_ArcaneGolem_NightbaneWin_01.prefab:ffe915d9bd0f53540beb314b0df007a0", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001CDA RID: 7386
	private HashSet<string> m_playedLines = new HashSet<string>();
}
