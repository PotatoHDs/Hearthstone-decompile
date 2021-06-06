using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KAR03_Chess : KAR_MissionEntity
{
	public override void PreloadAssets()
	{
		PreloadSound("VO_BlackKing_Male_ChessPiece_ChessEmoteGreetings_01.prefab:af93c24116d4a9c4387a4c7fc6dbe797");
		PreloadSound("VO_BlackKing_Male_ChessPiece_ChessEmoteOops_01.prefab:cf417e924bb3f17468f2cec1b2b060f6");
		PreloadSound("VO_BlackKing_Male_ChessPiece_ChessEmoteSorry_01.prefab:83fdc0552286dd34fa1a9be0a125d009");
		PreloadSound("VO_BlackKing_Male_ChessPiece_ChessEmoteThreaten_01.prefab:a5ca00863ff98a04bbd8c70d8926925d");
		PreloadSound("VO_BlackKing_Male_ChessPiece_ChessEmoteWellPlayed_01.prefab:6fae9478fea1007448751938c913457a");
		PreloadSound("VO_BlackKing_Male_ChessPiece_ChessEmoteWow_01.prefab:eb749e8cc32ac2c4eaf1a4a14137a56f");
		PreloadSound("VO_BlackKing_Male_ChessPiece_ChessEmoteThanks_01.prefab:a31c697fbf3366343872136436de8d57");
		PreloadSound("VO_Moroes_Male_Human_ChessTurn1_03.prefab:47a1f79584c5bbc46a6cdde7803cb222");
		PreloadSound("VO_Moroes_Male_Human_ChessTurn3_01.prefab:51bdd3ae156f40d42b431ed9da78b72c");
		PreloadSound("VO_Moroes_Male_Human_ChessTurn5_01.prefab:5b7db2358e1445d46937866212f3f67c");
		PreloadSound("VO_Moroes_Male_Human_ChessTurn7_01.prefab:1509fd212e6437d47a64fd4d726b177b");
		PreloadSound("VO_Moroes_Male_Human_ChessTurn7_02.prefab:87979dfbe09bda841879df4febf3fd30");
		PreloadSound("VO_Moroes_Male_Human_Wing1Win_01.prefab:ed1edcfa0d516d445b6b0d65f91cb135");
	}

	protected override void InitEmoteResponses()
	{
		m_emoteResponseGroups = new List<EmoteResponseGroup>
		{
			new EmoteResponseGroup
			{
				m_triggers = new List<EmoteType> { EmoteType.GREETINGS },
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_BlackKing_Male_ChessPiece_ChessEmoteGreetings_01.prefab:af93c24116d4a9c4387a4c7fc6dbe797",
						m_stringTag = "VO_BlackKing_Male_ChessPiece_ChessEmoteGreetings_01"
					}
				}
			},
			new EmoteResponseGroup
			{
				m_triggers = new List<EmoteType> { EmoteType.OOPS },
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_BlackKing_Male_ChessPiece_ChessEmoteOops_01.prefab:cf417e924bb3f17468f2cec1b2b060f6",
						m_stringTag = "VO_BlackKing_Male_ChessPiece_ChessEmoteOops_01"
					}
				}
			},
			new EmoteResponseGroup
			{
				m_triggers = new List<EmoteType> { EmoteType.SORRY },
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_BlackKing_Male_ChessPiece_ChessEmoteSorry_01.prefab:83fdc0552286dd34fa1a9be0a125d009",
						m_stringTag = "VO_BlackKing_Male_ChessPiece_ChessEmoteSorry_01"
					}
				}
			},
			new EmoteResponseGroup
			{
				m_triggers = new List<EmoteType> { EmoteType.THREATEN },
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_BlackKing_Male_ChessPiece_ChessEmoteThreaten_01.prefab:a5ca00863ff98a04bbd8c70d8926925d",
						m_stringTag = "VO_BlackKing_Male_ChessPiece_ChessEmoteThreaten_01"
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
						m_soundName = "VO_BlackKing_Male_ChessPiece_ChessEmoteWellPlayed_01.prefab:6fae9478fea1007448751938c913457a",
						m_stringTag = "VO_BlackKing_Male_ChessPiece_ChessEmoteWellPlayed_01"
					}
				}
			},
			new EmoteResponseGroup
			{
				m_triggers = new List<EmoteType> { EmoteType.WOW },
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_BlackKing_Male_ChessPiece_ChessEmoteWow_01.prefab:eb749e8cc32ac2c4eaf1a4a14137a56f",
						m_stringTag = "VO_BlackKing_Male_ChessPiece_ChessEmoteWow_01"
					}
				}
			},
			new EmoteResponseGroup
			{
				m_triggers = new List<EmoteType> { EmoteType.THANKS },
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_BlackKing_Male_ChessPiece_ChessEmoteThanks_01.prefab:a31c697fbf3366343872136436de8d57",
						m_stringTag = "VO_BlackKing_Male_ChessPiece_ChessEmoteThanks_01"
					}
				}
			}
		};
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 1)
		{
			GameState.Get().SetBusy(busy: true);
			yield return PlayCriticalLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_ChessForfeit_02.prefab:541adc35065f4f24bafd3a9320601988");
			GameState.Get().SetBusy(busy: false);
		}
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
			yield return PlayOpeningLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_ChessTurn1_03.prefab:47a1f79584c5bbc46a6cdde7803cb222");
			break;
		case 3:
			yield return PlayMissionFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_ChessTurn3_01.prefab:51bdd3ae156f40d42b431ed9da78b72c");
			break;
		case 5:
			yield return PlayMissionFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_ChessTurn5_01.prefab:5b7db2358e1445d46937866212f3f67c");
			break;
		case 11:
			if (IsHeroic())
			{
				yield return PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_ChessTurn7_02.prefab:87979dfbe09bda841879df4febf3fd30");
			}
			else
			{
				yield return PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_ChessTurn7_01.prefab:1509fd212e6437d47a64fd4d726b177b");
			}
			break;
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return PlayClosingLine("Moroes_Quote.prefab:ea3a21837aab2b0448ce4090103724cf", "VO_Moroes_Male_Human_Wing1Win_01.prefab:ed1edcfa0d516d445b6b0d65f91cb135");
		}
	}
}
