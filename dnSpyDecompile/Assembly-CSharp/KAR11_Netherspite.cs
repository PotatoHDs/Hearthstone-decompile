using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003A3 RID: 931
public class KAR11_Netherspite : KAR_MissionEntity
{
	// Token: 0x0600354E RID: 13646 RVA: 0x0010F030 File Offset: 0x0010D230
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_Netherspite_Male_Dragon_NetherspiteEmoteResponse_01.prefab:f0a08435dd8aedb4b9d4b7f8b27a4d4f");
		base.PreloadSound("VO_Netherspite_Male_Dragon_NetherspiteTurn3_02.prefab:909c9170498e2ff458bb5e607ae35fe1");
		base.PreloadSound("VO_Netherspite_Male_Dragon_NetherspiteTurn5_01.prefab:5c5e1d24755bfe34f825ce62f9deb6fa");
		base.PreloadSound("VO_Netherspite_Male_Dragon_NetherspiteTurn7_01.prefab:714c8bbda55d73b4a96f6c08ad3f2372");
		base.PreloadSound("VO_Netherspite_Male_Dragon_NetherspiteEmpowerment_01.prefab:066618c460879fc4f95694560aede66a");
		base.PreloadSound("VO_Netherspite_Male_Dragon_NetherspiteShadowBreath_01.prefab:3ec25ccdaa47c06428c1e4119b00a6e0");
		base.PreloadSound("VO_Netherspite_Male_Dragon_NetherspiteUnstablePortal_02.prefab:59a7527262c5e2d45b25672b8a2150f8");
		base.PreloadSound("VO_Netherspite_Male_Dragon_NetherspiteAngryChicken_01.prefab:4364929242fd7864eb2f681df1ab4f9e");
		base.PreloadSound("VO_Moroes_Male_Human_NetherspiteWin_01.prefab:012fd06d66e4106409d1cc9179f3a25b");
		base.PreloadSound("VO_Moroes_Male_Human_NetherspiteTurn1_01.prefab:f5a7ea32cbace6448ba0c29b44018bbb");
		base.PreloadSound("VO_Moroes_Male_Human_NetherspiteTurn7_01.prefab:4ab83e21ea834994498395f0678806ea");
		base.PreloadSound("VO_Moroes_Male_Human_NetherspiteTurn5_01.prefab:19d16638e6ae56f4b8c915d28b0b882f");
	}

	// Token: 0x0600354F RID: 13647 RVA: 0x0010F0C4 File Offset: 0x0010D2C4
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
						m_soundName = "VO_Netherspite_Male_Dragon_NetherspiteEmoteResponse_01.prefab:f0a08435dd8aedb4b9d4b7f8b27a4d4f",
						m_stringTag = "VO_Netherspite_Male_Dragon_NetherspiteEmoteResponse_01"
					}
				}
			}
		};
	}

	// Token: 0x06003550 RID: 13648 RVA: 0x0010F123 File Offset: 0x0010D323
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn != 1)
		{
			if (turn != 6)
			{
				if (turn == 10)
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayAdventureFlavorLine(actor, "VO_Netherspite_Male_Dragon_NetherspiteTurn7_01.prefab:714c8bbda55d73b4a96f6c08ad3f2372", 2.5f);
					yield return base.PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_NetherspiteTurn7_01.prefab:4ab83e21ea834994498395f0678806ea", 2.5f);
					GameState.Get().SetBusy(false);
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				if (base.ShouldPlayMissionFlavorLine("VO_Netherspite_Male_Dragon_NetherspiteTurn5_01.prefab:5c5e1d24755bfe34f825ce62f9deb6fa"))
				{
					yield return base.PlayMissionFlavorLine(actor, "VO_Netherspite_Male_Dragon_NetherspiteTurn5_01.prefab:5c5e1d24755bfe34f825ce62f9deb6fa", 2.5f);
					yield return base.PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_NetherspiteTurn5_01.prefab:19d16638e6ae56f4b8c915d28b0b882f", 2.5f);
				}
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			yield return base.PlayOpeningLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_NetherspiteTurn1_01.prefab:f5a7ea32cbace6448ba0c29b44018bbb", 2.5f);
		}
		yield break;
	}

	// Token: 0x06003551 RID: 13649 RVA: 0x0010F139 File Offset: 0x0010D339
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "KARA_08_02") && !(cardId == "KARA_08_02H"))
		{
			if (cardId == "KARA_08_05" || cardId == "KARA_08_05H")
			{
				yield return base.PlayBossLine(enemyActor, "VO_Netherspite_Male_Dragon_NetherspiteShadowBreath_01.prefab:3ec25ccdaa47c06428c1e4119b00a6e0", 2.5f);
			}
		}
		else
		{
			yield return new WaitForSeconds(0.2f);
			GameState.Get().SetBusy(true);
			yield return base.PlayMissionFlavorLine(enemyActor, "VO_Netherspite_Male_Dragon_NetherspiteEmpowerment_01.prefab:066618c460879fc4f95694560aede66a", 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06003552 RID: 13650 RVA: 0x0010F14F File Offset: 0x0010D34F
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 1)
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayMissionFlavorLine(actor, "VO_Netherspite_Male_Dragon_NetherspiteTurn3_02.prefab:909c9170498e2ff458bb5e607ae35fe1", 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06003553 RID: 13651 RVA: 0x0010F165 File Offset: 0x0010D365
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
		if (!(cardId == "GVG_003"))
		{
			if (cardId == "EX1_009")
			{
				yield return base.PlayBossLine(actor, "VO_Netherspite_Male_Dragon_NetherspiteAngryChicken_01.prefab:4364929242fd7864eb2f681df1ab4f9e", 2.5f);
			}
		}
		else
		{
			yield return base.PlayBossLine(actor, "VO_Netherspite_Male_Dragon_NetherspiteUnstablePortal_02.prefab:59a7527262c5e2d45b25672b8a2150f8", 2.5f);
		}
		yield break;
	}

	// Token: 0x06003554 RID: 13652 RVA: 0x0010F17B File Offset: 0x0010D37B
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return base.PlayClosingLine("Moroes_Quote.prefab:ea3a21837aab2b0448ce4090103724cf", "VO_Moroes_Male_Human_NetherspiteWin_01.prefab:012fd06d66e4106409d1cc9179f3a25b", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001CDD RID: 7389
	private HashSet<string> m_playedLines = new HashSet<string>();
}
