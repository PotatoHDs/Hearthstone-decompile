using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200039B RID: 923
public class KAR03_Chess : KAR_MissionEntity
{
	// Token: 0x06003512 RID: 13586 RVA: 0x0010E1E4 File Offset: 0x0010C3E4
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_BlackKing_Male_ChessPiece_ChessEmoteGreetings_01.prefab:af93c24116d4a9c4387a4c7fc6dbe797");
		base.PreloadSound("VO_BlackKing_Male_ChessPiece_ChessEmoteOops_01.prefab:cf417e924bb3f17468f2cec1b2b060f6");
		base.PreloadSound("VO_BlackKing_Male_ChessPiece_ChessEmoteSorry_01.prefab:83fdc0552286dd34fa1a9be0a125d009");
		base.PreloadSound("VO_BlackKing_Male_ChessPiece_ChessEmoteThreaten_01.prefab:a5ca00863ff98a04bbd8c70d8926925d");
		base.PreloadSound("VO_BlackKing_Male_ChessPiece_ChessEmoteWellPlayed_01.prefab:6fae9478fea1007448751938c913457a");
		base.PreloadSound("VO_BlackKing_Male_ChessPiece_ChessEmoteWow_01.prefab:eb749e8cc32ac2c4eaf1a4a14137a56f");
		base.PreloadSound("VO_BlackKing_Male_ChessPiece_ChessEmoteThanks_01.prefab:a31c697fbf3366343872136436de8d57");
		base.PreloadSound("VO_Moroes_Male_Human_ChessTurn1_03.prefab:47a1f79584c5bbc46a6cdde7803cb222");
		base.PreloadSound("VO_Moroes_Male_Human_ChessTurn3_01.prefab:51bdd3ae156f40d42b431ed9da78b72c");
		base.PreloadSound("VO_Moroes_Male_Human_ChessTurn5_01.prefab:5b7db2358e1445d46937866212f3f67c");
		base.PreloadSound("VO_Moroes_Male_Human_ChessTurn7_01.prefab:1509fd212e6437d47a64fd4d726b177b");
		base.PreloadSound("VO_Moroes_Male_Human_ChessTurn7_02.prefab:87979dfbe09bda841879df4febf3fd30");
		base.PreloadSound("VO_Moroes_Male_Human_Wing1Win_01.prefab:ed1edcfa0d516d445b6b0d65f91cb135");
	}

	// Token: 0x06003513 RID: 13587 RVA: 0x0010E280 File Offset: 0x0010C480
	protected override void InitEmoteResponses()
	{
		this.m_emoteResponseGroups = new List<MissionEntity.EmoteResponseGroup>
		{
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>
				{
					EmoteType.GREETINGS
				},
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_BlackKing_Male_ChessPiece_ChessEmoteGreetings_01.prefab:af93c24116d4a9c4387a4c7fc6dbe797",
						m_stringTag = "VO_BlackKing_Male_ChessPiece_ChessEmoteGreetings_01"
					}
				}
			},
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>
				{
					EmoteType.OOPS
				},
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_BlackKing_Male_ChessPiece_ChessEmoteOops_01.prefab:cf417e924bb3f17468f2cec1b2b060f6",
						m_stringTag = "VO_BlackKing_Male_ChessPiece_ChessEmoteOops_01"
					}
				}
			},
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>
				{
					EmoteType.SORRY
				},
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_BlackKing_Male_ChessPiece_ChessEmoteSorry_01.prefab:83fdc0552286dd34fa1a9be0a125d009",
						m_stringTag = "VO_BlackKing_Male_ChessPiece_ChessEmoteSorry_01"
					}
				}
			},
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>
				{
					EmoteType.THREATEN
				},
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_BlackKing_Male_ChessPiece_ChessEmoteThreaten_01.prefab:a5ca00863ff98a04bbd8c70d8926925d",
						m_stringTag = "VO_BlackKing_Male_ChessPiece_ChessEmoteThreaten_01"
					}
				}
			},
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>
				{
					EmoteType.WELL_PLAYED
				},
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_BlackKing_Male_ChessPiece_ChessEmoteWellPlayed_01.prefab:6fae9478fea1007448751938c913457a",
						m_stringTag = "VO_BlackKing_Male_ChessPiece_ChessEmoteWellPlayed_01"
					}
				}
			},
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>
				{
					EmoteType.WOW
				},
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_BlackKing_Male_ChessPiece_ChessEmoteWow_01.prefab:eb749e8cc32ac2c4eaf1a4a14137a56f",
						m_stringTag = "VO_BlackKing_Male_ChessPiece_ChessEmoteWow_01"
					}
				}
			},
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>
				{
					EmoteType.THANKS
				},
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_BlackKing_Male_ChessPiece_ChessEmoteThanks_01.prefab:a31c697fbf3366343872136436de8d57",
						m_stringTag = "VO_BlackKing_Male_ChessPiece_ChessEmoteThanks_01"
					}
				}
			}
		};
	}

	// Token: 0x06003514 RID: 13588 RVA: 0x0010E498 File Offset: 0x0010C698
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 1)
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayCriticalLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_ChessForfeit_02.prefab:541adc35065f4f24bafd3a9320601988", 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06003515 RID: 13589 RVA: 0x0010E4AE File Offset: 0x0010C6AE
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		switch (turn)
		{
		case 1:
			yield return base.PlayOpeningLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_ChessTurn1_03.prefab:47a1f79584c5bbc46a6cdde7803cb222", 2.5f);
			break;
		case 2:
		case 4:
			break;
		case 3:
			yield return base.PlayMissionFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_ChessTurn3_01.prefab:51bdd3ae156f40d42b431ed9da78b72c", 2.5f);
			break;
		case 5:
			yield return base.PlayMissionFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_ChessTurn5_01.prefab:5b7db2358e1445d46937866212f3f67c", 2.5f);
			break;
		default:
			if (turn == 11)
			{
				if (base.IsHeroic())
				{
					yield return base.PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_ChessTurn7_02.prefab:87979dfbe09bda841879df4febf3fd30", 2.5f);
				}
				else
				{
					yield return base.PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_ChessTurn7_01.prefab:1509fd212e6437d47a64fd4d726b177b", 2.5f);
				}
			}
			break;
		}
		yield break;
	}

	// Token: 0x06003516 RID: 13590 RVA: 0x0010E4C4 File Offset: 0x0010C6C4
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return base.PlayClosingLine("Moroes_Quote.prefab:ea3a21837aab2b0448ce4090103724cf", "VO_Moroes_Male_Human_Wing1Win_01.prefab:ed1edcfa0d516d445b6b0d65f91cb135", 2.5f);
		}
		yield break;
	}
}
