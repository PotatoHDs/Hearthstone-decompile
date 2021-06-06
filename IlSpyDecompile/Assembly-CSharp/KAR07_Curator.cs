using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KAR07_Curator : KAR_MissionEntity
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorEmoteResponse_01.prefab:2bb55cfffb1c1b846a2693ec0d9897eb");
		PreloadSound("VO_Moroes_Male_Human_CuratorTurn3_01.prefab:867f021ff94828e449892e778800e0d9");
		PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorTurn1_01.prefab:5ad59d406d00ab44d82384257b8fb4a4");
		PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorTurn5_01.prefab:e59c87cd9998ae44f802ac7dd5e74b34");
		PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorTurn9_01.prefab:df9344fd46b080545a70895065033838");
		PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorArcaneGolem_01.prefab:44f96236a79304a4592eadbb92a60991");
		PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorMedivhSkin_01.prefab:72f9a039470438e4c908f514f7b0112d");
		PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorHarrison_02.prefab:d8882bd7ba7f51043975da6e9430a033");
		PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorReno_02.prefab:805272a3ed96b0343b6c330b821777f5");
		PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorMurlocs_01.prefab:25fe890f35a409e40b9ae5475c534eeb");
		PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorPirates_01.prefab:ac13b5145f3830a46b852218464e30bf");
		PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorBeasts_01.prefab:c169916328b5c75419231d5010294d85");
		PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorDemons_01.prefab:51ea55c4a2dc2b648b80da79fdf44e19");
		PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorMechs_01.prefab:f3c8f9a25842b6845b39f6de8a832c97");
		PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorDragons_01.prefab:11288acd111088a43965361a5a3fde44");
		PreloadSound("VO_Moroes_Male_Human_CuratorWin_01.prefab:43c7e5f016e525a4babe41aeccb7f1bd");
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
						m_soundName = "VO_Curator_Male_ArcaneGolem_CuratorEmoteResponse_01.prefab:2bb55cfffb1c1b846a2693ec0d9897eb",
						m_stringTag = "VO_Curator_Male_ArcaneGolem_CuratorEmoteResponse_01"
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			yield return PlayOpeningLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorTurn1_01.prefab:5ad59d406d00ab44d82384257b8fb4a4");
			break;
		case 8:
			GameState.Get().SetBusy(busy: true);
			yield return PlayMissionFlavorLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorTurn5_01.prefab:e59c87cd9998ae44f802ac7dd5e74b34");
			GameState.Get().SetBusy(busy: false);
			break;
		case 12:
			GameState.Get().SetBusy(busy: true);
			yield return PlayMissionFlavorLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorTurn9_01.prefab:df9344fd46b080545a70895065033838");
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "EX1_089":
				yield return PlayEasterEggLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorArcaneGolem_01.prefab:44f96236a79304a4592eadbb92a60991");
				break;
			case "CS2_034_H1":
				yield return PlayEasterEggLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorMedivhSkin_01.prefab:72f9a039470438e4c908f514f7b0112d");
				break;
			case "EX1_558":
				yield return PlayEasterEggLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorHarrison_02.prefab:d8882bd7ba7f51043975da6e9430a033");
				break;
			case "LOE_011":
				yield return PlayEasterEggLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorReno_02.prefab:805272a3ed96b0343b6c330b821777f5");
				break;
			}
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
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			bool flag = false;
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "KARA_07_03":
			case "KARA_07_03heroic":
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorMurlocs_01.prefab:25fe890f35a409e40b9ae5475c534eeb");
				flag = true;
				GameState.Get().SetBusy(busy: false);
				break;
			case "KARA_07_04":
			case "KARA_07_04heroic":
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorPirates_01.prefab:ac13b5145f3830a46b852218464e30bf");
				flag = true;
				GameState.Get().SetBusy(busy: false);
				break;
			case "KARA_07_05":
			case "KARA_07_05heroic":
				GameState.Get().SetBusy(busy: false);
				yield return PlayBossLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorBeasts_01.prefab:c169916328b5c75419231d5010294d85");
				flag = true;
				GameState.Get().SetBusy(busy: false);
				break;
			case "KARA_07_06":
			case "KARA_07_06heroic":
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorDemons_01.prefab:51ea55c4a2dc2b648b80da79fdf44e19");
				flag = true;
				GameState.Get().SetBusy(busy: false);
				break;
			case "KARA_07_07":
			case "KARA_07_07heroic":
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorMechs_01.prefab:f3c8f9a25842b6845b39f6de8a832c97");
				flag = true;
				GameState.Get().SetBusy(busy: false);
				break;
			case "KARA_07_08":
			case "KARA_07_08heroic":
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorDragons_01.prefab:11288acd111088a43965361a5a3fde44");
				flag = true;
				GameState.Get().SetBusy(busy: false);
				break;
			}
			if (flag && !m_playedLines.Contains("VO_Moroes_Male_Human_CuratorTurn3_01"))
			{
				yield return new WaitForSeconds(0.4f);
				GameState.Get().SetBusy(busy: true);
				m_playedLines.Add("VO_Moroes_Male_Human_CuratorTurn3_01");
				yield return PlayMissionFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_CuratorTurn3_01.prefab:867f021ff94828e449892e778800e0d9");
				GameState.Get().SetBusy(busy: false);
			}
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return PlayClosingLine("Moroes_Quote.prefab:ea3a21837aab2b0448ce4090103724cf", "VO_Moroes_Male_Human_CuratorWin_01.prefab:43c7e5f016e525a4babe41aeccb7f1bd");
		}
	}
}
