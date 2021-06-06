using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200039F RID: 927
public class KAR07_Curator : KAR_MissionEntity
{
	// Token: 0x06003531 RID: 13617 RVA: 0x0010EAA0 File Offset: 0x0010CCA0
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorEmoteResponse_01.prefab:2bb55cfffb1c1b846a2693ec0d9897eb");
		base.PreloadSound("VO_Moroes_Male_Human_CuratorTurn3_01.prefab:867f021ff94828e449892e778800e0d9");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorTurn1_01.prefab:5ad59d406d00ab44d82384257b8fb4a4");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorTurn5_01.prefab:e59c87cd9998ae44f802ac7dd5e74b34");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorTurn9_01.prefab:df9344fd46b080545a70895065033838");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorArcaneGolem_01.prefab:44f96236a79304a4592eadbb92a60991");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorMedivhSkin_01.prefab:72f9a039470438e4c908f514f7b0112d");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorHarrison_02.prefab:d8882bd7ba7f51043975da6e9430a033");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorReno_02.prefab:805272a3ed96b0343b6c330b821777f5");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorMurlocs_01.prefab:25fe890f35a409e40b9ae5475c534eeb");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorPirates_01.prefab:ac13b5145f3830a46b852218464e30bf");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorBeasts_01.prefab:c169916328b5c75419231d5010294d85");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorDemons_01.prefab:51ea55c4a2dc2b648b80da79fdf44e19");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorMechs_01.prefab:f3c8f9a25842b6845b39f6de8a832c97");
		base.PreloadSound("VO_Curator_Male_ArcaneGolem_CuratorDragons_01.prefab:11288acd111088a43965361a5a3fde44");
		base.PreloadSound("VO_Moroes_Male_Human_CuratorWin_01.prefab:43c7e5f016e525a4babe41aeccb7f1bd");
	}

	// Token: 0x06003532 RID: 13618 RVA: 0x0010EB60 File Offset: 0x0010CD60
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
						m_soundName = "VO_Curator_Male_ArcaneGolem_CuratorEmoteResponse_01.prefab:2bb55cfffb1c1b846a2693ec0d9897eb",
						m_stringTag = "VO_Curator_Male_ArcaneGolem_CuratorEmoteResponse_01"
					}
				}
			}
		};
	}

	// Token: 0x06003533 RID: 13619 RVA: 0x0010EBBF File Offset: 0x0010CDBF
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (turn != 1)
		{
			if (turn != 8)
			{
				if (turn == 12)
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayMissionFlavorLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorTurn9_01.prefab:df9344fd46b080545a70895065033838", 2.5f);
					GameState.Get().SetBusy(false);
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayMissionFlavorLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorTurn5_01.prefab:e59c87cd9998ae44f802ac7dd5e74b34", 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			yield return base.PlayOpeningLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorTurn1_01.prefab:5ad59d406d00ab44d82384257b8fb4a4", 2.5f);
		}
		yield break;
	}

	// Token: 0x06003534 RID: 13620 RVA: 0x0010EBD5 File Offset: 0x0010CDD5
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
		if (!(cardId == "EX1_089"))
		{
			if (!(cardId == "CS2_034_H1"))
			{
				if (!(cardId == "EX1_558"))
				{
					if (cardId == "LOE_011")
					{
						yield return base.PlayEasterEggLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorReno_02.prefab:805272a3ed96b0343b6c330b821777f5", 2.5f);
					}
				}
				else
				{
					yield return base.PlayEasterEggLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorHarrison_02.prefab:d8882bd7ba7f51043975da6e9430a033", 2.5f);
				}
			}
			else
			{
				yield return base.PlayEasterEggLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorMedivhSkin_01.prefab:72f9a039470438e4c908f514f7b0112d", 2.5f);
			}
		}
		else
		{
			yield return base.PlayEasterEggLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorArcaneGolem_01.prefab:44f96236a79304a4592eadbb92a60991", 2.5f);
		}
		yield break;
	}

	// Token: 0x06003535 RID: 13621 RVA: 0x0010EBEB File Offset: 0x0010CDEB
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
		bool flag = false;
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 3538236405U)
		{
			if (num <= 1591518715U)
			{
				if (num != 575158210U)
				{
					if (num != 794844880U)
					{
						if (num != 1591518715U)
						{
							goto IL_42D;
						}
						if (!(cardId == "KARA_07_06heroic"))
						{
							goto IL_42D;
						}
						goto IL_366;
					}
					else
					{
						if (!(cardId == "KARA_07_07heroic"))
						{
							goto IL_42D;
						}
						goto IL_3AB;
					}
				}
				else
				{
					if (!(cardId == "KARA_07_05heroic"))
					{
						goto IL_42D;
					}
					goto IL_321;
				}
			}
			else
			{
				if (num != 1620102588U)
				{
					if (num != 3115444697U)
					{
						if (num != 3538236405U)
						{
							goto IL_42D;
						}
						if (!(cardId == "KARA_07_08"))
						{
							goto IL_42D;
						}
					}
					else if (!(cardId == "KARA_07_08heroic"))
					{
						goto IL_42D;
					}
					GameState.Get().SetBusy(true);
					yield return base.PlayBossLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorDragons_01.prefab:11288acd111088a43965361a5a3fde44", 2.5f);
					flag = true;
					GameState.Get().SetBusy(false);
					goto IL_42D;
				}
				if (!(cardId == "KARA_07_03heroic"))
				{
					goto IL_42D;
				}
			}
		}
		else
		{
			if (num > 3588569262U)
			{
				if (num != 3605346881U)
				{
					if (num != 3622124500U)
					{
						if (num != 3679558725U)
						{
							goto IL_42D;
						}
						if (!(cardId == "KARA_07_04heroic"))
						{
							goto IL_42D;
						}
					}
					else
					{
						if (!(cardId == "KARA_07_03"))
						{
							goto IL_42D;
						}
						goto IL_297;
					}
				}
				else if (!(cardId == "KARA_07_04"))
				{
					goto IL_42D;
				}
				GameState.Get().SetBusy(true);
				yield return base.PlayBossLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorPirates_01.prefab:ac13b5145f3830a46b852218464e30bf", 2.5f);
				flag = true;
				GameState.Get().SetBusy(false);
				goto IL_42D;
			}
			if (num != 3555014024U)
			{
				if (num != 3571791643U)
				{
					if (num != 3588569262U)
					{
						goto IL_42D;
					}
					if (!(cardId == "KARA_07_05"))
					{
						goto IL_42D;
					}
					goto IL_321;
				}
				else
				{
					if (!(cardId == "KARA_07_06"))
					{
						goto IL_42D;
					}
					goto IL_366;
				}
			}
			else
			{
				if (!(cardId == "KARA_07_07"))
				{
					goto IL_42D;
				}
				goto IL_3AB;
			}
		}
		IL_297:
		GameState.Get().SetBusy(true);
		yield return base.PlayBossLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorMurlocs_01.prefab:25fe890f35a409e40b9ae5475c534eeb", 2.5f);
		flag = true;
		GameState.Get().SetBusy(false);
		goto IL_42D;
		IL_321:
		GameState.Get().SetBusy(false);
		yield return base.PlayBossLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorBeasts_01.prefab:c169916328b5c75419231d5010294d85", 2.5f);
		flag = true;
		GameState.Get().SetBusy(false);
		goto IL_42D;
		IL_366:
		GameState.Get().SetBusy(true);
		yield return base.PlayBossLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorDemons_01.prefab:51ea55c4a2dc2b648b80da79fdf44e19", 2.5f);
		flag = true;
		GameState.Get().SetBusy(false);
		goto IL_42D;
		IL_3AB:
		GameState.Get().SetBusy(true);
		yield return base.PlayBossLine(actor, "VO_Curator_Male_ArcaneGolem_CuratorMechs_01.prefab:f3c8f9a25842b6845b39f6de8a832c97", 2.5f);
		flag = true;
		GameState.Get().SetBusy(false);
		IL_42D:
		if (flag && !this.m_playedLines.Contains("VO_Moroes_Male_Human_CuratorTurn3_01"))
		{
			yield return new WaitForSeconds(0.4f);
			GameState.Get().SetBusy(true);
			this.m_playedLines.Add("VO_Moroes_Male_Human_CuratorTurn3_01");
			yield return base.PlayMissionFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_CuratorTurn3_01.prefab:867f021ff94828e449892e778800e0d9", 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06003536 RID: 13622 RVA: 0x0010EC01 File Offset: 0x0010CE01
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return base.PlayClosingLine("Moroes_Quote.prefab:ea3a21837aab2b0448ce4090103724cf", "VO_Moroes_Male_Human_CuratorWin_01.prefab:43c7e5f016e525a4babe41aeccb7f1bd", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001CD9 RID: 7385
	private HashSet<string> m_playedLines = new HashSet<string>();
}
