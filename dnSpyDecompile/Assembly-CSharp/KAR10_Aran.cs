using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003A2 RID: 930
public class KAR10_Aran : KAR_MissionEntity
{
	// Token: 0x06003546 RID: 13638 RVA: 0x0010EEBC File Offset: 0x0010D0BC
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_Aran_Male_Shade_AranEmoteResponse_01.prefab:8daae2c5e8533c142b36966665234991");
		base.PreloadSound("VO_Aran_Male_Shade_AranCrackle_01.prefab:bc888dcf38fea67478451daed3a95b07");
		base.PreloadSound("VO_Aran_Male_Shade_AranHeroPower_01.prefab:efd33279a659c4b45b71371592ed8477");
		base.PreloadSound("VO_Aran_Male_Shade_AranTurn1_01.prefab:145fc99930a9bf94e9220b73440f603b");
		base.PreloadSound("VO_Aran_Male_Shade_AranTurn5_01.prefab:3f761320471ef964381b2eb26650cefd");
		base.PreloadSound("VO_Aran_Male_Shade_AranSpell_03.prefab:00d1e54e1dcdfbe4497ed8011d01f4fb");
		base.PreloadSound("VO_Aran_Male_Shade_AranSpell_04.prefab:95571da88393435459da14036a4f20f8");
		base.PreloadSound("VO_Aran_Male_Shade_AranMedivhSkin_01.prefab:2c2804e44bd75034a9e2c6a2fb86e1ba");
		base.PreloadSound("VO_Aran_Male_Shade_AranTrons_02.prefab:b37a84a69b76ea24993c7afbd4c0212c");
		base.PreloadSound("VO_Moroes_Male_Human_AranWin_01.prefab:4a6dccd0f23c83344b6bb8f61356f52b");
		base.PreloadSound("VO_Moroes_Male_Human_AranTurn1_01.prefab:e6314824bdc74644f88954866157c664");
		base.PreloadSound("VO_Moroes_Male_Human_AranTurn9_04.prefab:007f118344eac0446a75031c63ba1712");
	}

	// Token: 0x06003547 RID: 13639 RVA: 0x0010EF50 File Offset: 0x0010D150
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
						m_soundName = "VO_Aran_Male_Shade_AranEmoteResponse_01.prefab:8daae2c5e8533c142b36966665234991",
						m_stringTag = "VO_Aran_Male_Shade_AranEmoteResponse_01"
					}
				}
			}
		};
	}

	// Token: 0x06003548 RID: 13640 RVA: 0x0010EFAF File Offset: 0x0010D1AF
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(item))
		{
			yield break;
		}
		this.m_playedLines.Add(item);
		if (missionEvent != 1)
		{
			if (missionEvent == 2)
			{
				yield return base.PlayBossLine(actor, "VO_Aran_Male_Shade_AranHeroPower_01.prefab:efd33279a659c4b45b71371592ed8477", 2.5f);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayEasterEggLine(actor, "VO_Aran_Male_Shade_AranCrackle_01.prefab:bc888dcf38fea67478451daed3a95b07", 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06003549 RID: 13641 RVA: 0x0010EFC5 File Offset: 0x0010D1C5
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn != 1)
		{
			if (turn != 4)
			{
				if (turn == 8)
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_AranTurn9_04.prefab:007f118344eac0446a75031c63ba1712", 2.5f);
					GameState.Get().SetBusy(false);
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayMissionFlavorLine(actor, "VO_Aran_Male_Shade_AranTurn5_01.prefab:3f761320471ef964381b2eb26650cefd", 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else if (base.ShouldPlayOpeningLine("VO_Aran_Male_Shade_AranTurn1_01.prefab:145fc99930a9bf94e9220b73440f603b"))
		{
			yield return base.PlayOpeningLine(actor, "VO_Aran_Male_Shade_AranTurn1_01.prefab:145fc99930a9bf94e9220b73440f603b", 2.5f);
			yield return base.PlayMissionFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_AranTurn1_01.prefab:e6314824bdc74644f88954866157c664", 2.5f);
		}
		yield break;
	}

	// Token: 0x0600354A RID: 13642 RVA: 0x0010EFDB File Offset: 0x0010D1DB
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
		if (!(cardId == "CS2_028"))
		{
			if (cardId == "CS2_033")
			{
				yield return base.PlayBossLine(actor, "VO_Aran_Male_Shade_AranSpell_04.prefab:95571da88393435459da14036a4f20f8", 2.5f);
			}
		}
		else
		{
			yield return base.PlayBossLine(actor, "VO_Aran_Male_Shade_AranSpell_03.prefab:00d1e54e1dcdfbe4497ed8011d01f4fb", 2.5f);
		}
		yield break;
	}

	// Token: 0x0600354B RID: 13643 RVA: 0x0010EFF1 File Offset: 0x0010D1F1
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		if (!(cardId == "CS2_034_H1"))
		{
			if (cardId == "GVG_085" || cardId == "OG_145")
			{
				this.m_playedLines.Add("GVG_085");
				this.m_playedLines.Add("OG_145");
				yield return base.PlayEasterEggLine(actor, "VO_Aran_Male_Shade_AranTrons_02.prefab:b37a84a69b76ea24993c7afbd4c0212c", 2.5f);
			}
		}
		else
		{
			yield return base.PlayEasterEggLine(actor, "VO_Aran_Male_Shade_AranMedivhSkin_01.prefab:2c2804e44bd75034a9e2c6a2fb86e1ba", 2.5f);
		}
		yield break;
	}

	// Token: 0x0600354C RID: 13644 RVA: 0x0010F007 File Offset: 0x0010D207
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return base.PlayClosingLine("Moroes_Quote.prefab:ea3a21837aab2b0448ce4090103724cf", "VO_Moroes_Male_Human_AranWin_01.prefab:4a6dccd0f23c83344b6bb8f61356f52b", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001CDC RID: 7388
	private HashSet<string> m_playedLines = new HashSet<string>();
}
