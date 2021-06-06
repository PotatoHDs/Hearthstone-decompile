using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICC_09_Saurfang : ICC_MissionEntity
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_BloodBeastPlayedLines = new List<string> { "VO_ICC09_Saurfang_Male_Orc_BloodBeast_01.prefab:22df97f204e1c7c4d9da6054e44c304c", "VO_ICC09_Saurfang_Male_Orc_BloodBeast_02.prefab:ab300785de0d6c742b8bb1ddaf7a43d3", "VO_ICC09_Saurfang_Male_Orc_BloodBeast_03.prefab:d75e50a32069d2c4eaafc87582612455", "VO_ICC09_Saurfang_Male_Orc_BloodBeast_04.prefab:0768553521eebe54a9e193254c90cb16" };

	private List<string> m_BossAttackLines = new List<string> { "VO_ICC09_Saurfang_Male_Orc_AttacksPlayer_01.prefab:88b73ddbe3cf7a5469349ebc7af13287", "VO_ICC09_Saurfang_Male_Orc_AttacksPlayer_02.prefab:8c9316f13997085498fc911ec9a604c1", "VO_ICC09_Saurfang_Male_Orc_AttacksPlayer_03.prefab:4c2ba751e6074e8418e3c065b11c071e", "VO_ICC09_Saurfang_Male_Orc_AttacksPlayer_04.prefab:10240a88e0419594f9be30bb8620a3cf" };

	public override void PreloadAssets()
	{
		foreach (string item in new List<string>
		{
			"VO_ICC09_Saurfang_Male_Orc_Intro_01.prefab:4f9b5ee85b9fb754abb5a6eb426409bb", "VO_ICC09_Saurfang_Male_Orc_BloodBeast_01.prefab:22df97f204e1c7c4d9da6054e44c304c", "VO_ICC09_Saurfang_Male_Orc_BloodBeast_02.prefab:ab300785de0d6c742b8bb1ddaf7a43d3", "VO_ICC09_Saurfang_Male_Orc_BloodBeast_03.prefab:d75e50a32069d2c4eaafc87582612455", "VO_ICC09_Saurfang_Male_Orc_BloodBeast_04.prefab:0768553521eebe54a9e193254c90cb16", "VO_ICC09_Saurfang_Male_Orc_EquipWeapon_01.prefab:8bd83a3fd51352f448936c1e60c68ee7", "VO_ICC09_Saurfang_Male_Orc_AttackedByPlayer_01.prefab:8b07d4d9b9d768b468f6b4ff3c4d267e", "VO_ICC09_Saurfang_Male_Orc_AttacksPlayer_01.prefab:88b73ddbe3cf7a5469349ebc7af13287", "VO_ICC09_Saurfang_Male_Orc_AttacksPlayer_02.prefab:8c9316f13997085498fc911ec9a604c1", "VO_ICC09_Saurfang_Male_Orc_AttacksPlayer_03.prefab:4c2ba751e6074e8418e3c065b11c071e",
			"VO_ICC09_Saurfang_Male_Orc_AttacksPlayer_04.prefab:10240a88e0419594f9be30bb8620a3cf", "VO_ICC09_Saurfang_Male_Orc_Death_01.prefab:4543a7b852bd2f349b5dfb4092ebd1f6", "VO_ICC09_LichKing_Male_Human_Win_01.prefab:e3c063dfdadd1dc4dbafbc06502cd9f0", "VO_ICC09_LichKing_Male_Human_Lose_01.prefab:c5f2c5e213c4ad648be72f77a9afbb2f", "VO_ICC09_Saurfang_Male_Orc_EmoteResponse_01.prefab:090f98bf2b0554948adbff9c88e66652", "VO_ICC09_Saurfang_Male_Orc_WeaponDestroyed_01.prefab:a32f29795fe6bc54e88b4db71cb098eb", "VO_ICC09_Saurfang_Male_Orc_EquipWeapon_02.prefab:2db7d5695e680f04cacbb21261346542", "VO_ICC09_Saurfang_Male_Orc_LichKing_01.prefab:581038fd46cee1d469b16d7f1d8633a8", "VO_ICC09_LichKing_Male_Human_LichKing_02.prefab:4cbee07a087a91d4db9ed4a984bc132b", "VO_ICC09_Saurfang_Male_Orc_BloodBeastStolen_01.prefab:f17cc74435c0e9a4fba14a157c2e8ef2",
			"VO_ICC09_Saurfang_Male_Orc_DKTransform_01.prefab:035ff5d1b1a7af8428a8ef934440e476", "VO_ICC09_Saurfang_Male_Orc_CursedBlade_01.prefab:e6667792ec809c2418896ac658ccec4e", "VO_ICC09_Saurfang_Male_Orc_Doomerang_01.prefab:3b2491d21a7385c4583937d818425153"
		})
		{
			PreloadSound(item);
		}
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
						m_soundName = "VO_ICC09_Saurfang_Male_Orc_EmoteResponse_01.prefab:090f98bf2b0554948adbff9c88e66652",
						m_stringTag = "VO_ICC09_Saurfang_Male_Orc_EmoteResponse_01"
					}
				}
			},
			new EmoteResponseGroup
			{
				m_triggers = new List<EmoteType> { EmoteType.START },
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_ICC09_Saurfang_Male_Orc_Intro_01.prefab:4f9b5ee85b9fb754abb5a6eb426409bb",
						m_stringTag = "VO_ICC09_Saurfang_Male_Orc_Intro_01"
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
		if (m_playedLines.Contains(item))
		{
			yield break;
		}
		switch (missionEvent)
		{
		case 101:
			if (m_BloodBeastPlayedLines.Count != 0)
			{
				GameState.Get().SetBusy(busy: true);
				string text2 = m_BloodBeastPlayedLines[Random.Range(0, m_BloodBeastPlayedLines.Count)];
				m_BloodBeastPlayedLines.Remove(text2);
				yield return PlayLineOnlyOnce(actor, text2);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 102:
			m_playedLines.Add(item);
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(actor, "VO_ICC09_Saurfang_Male_Orc_EquipWeapon_01.prefab:8bd83a3fd51352f448936c1e60c68ee7");
			GameState.Get().SetBusy(busy: false);
			break;
		case 103:
			m_playedLines.Add(item);
			yield return PlayLineOnlyOnce(actor, "VO_ICC09_Saurfang_Male_Orc_AttackedByPlayer_01.prefab:8b07d4d9b9d768b468f6b4ff3c4d267e");
			break;
		case 104:
			if (m_BossAttackLines.Count != 0)
			{
				GameState.Get().SetBusy(busy: true);
				string text = m_BossAttackLines[Random.Range(0, m_BossAttackLines.Count)];
				m_BossAttackLines.Remove(text);
				yield return PlayLineOnlyOnce(actor, text);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 105:
			yield return PlayBossLine(actor, "VO_ICC09_Saurfang_Male_Orc_Death_01.prefab:4543a7b852bd2f349b5dfb4092ebd1f6");
			break;
		case 106:
			yield return PlayEasterEggLine(actor, "VO_ICC09_Saurfang_Male_Orc_WeaponDestroyed_01.prefab:a32f29795fe6bc54e88b4db71cb098eb");
			break;
		case 107:
			yield return PlayEasterEggLine(actor, "VO_ICC09_Saurfang_Male_Orc_BloodBeastStolen_01.prefab:f17cc74435c0e9a4fba14a157c2e8ef2");
			break;
		case 108:
			yield return PlayEasterEggLine(actor, "VO_ICC09_Saurfang_Male_Orc_CursedBlade_01.prefab:e6667792ec809c2418896ac658ccec4e");
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
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "ICC_314"))
		{
			if (cardId == "ICC_233")
			{
				yield return PlayEasterEggLine(enemyActor, "VO_ICC09_Saurfang_Male_Orc_Doomerang_01.prefab:3b2491d21a7385c4583937d818425153");
			}
		}
		else
		{
			yield return PlayEasterEggLine(enemyActor, "VO_ICC09_Saurfang_Male_Orc_LichKing_01.prefab:581038fd46cee1d469b16d7f1d8633a8");
			yield return PlayEasterEggLine(GetLichKingFriendlyMinion(), "VO_ICC09_LichKing_Male_Human_LichKing_02.prefab:4cbee07a087a91d4db9ed4a984bc132b");
		}
		if (entity.GetCardType() == TAG_CARDTYPE.WEAPON && entity.GetATK() > 5)
		{
			yield return PlayEasterEggLine(enemyActor, "VO_ICC09_Saurfang_Male_Orc_EquipWeapon_02.prefab:2db7d5695e680f04cacbb21261346542");
		}
		yield return IfPlayerPlaysDKHeroVO(entity, enemyActor, "VO_ICC09_Saurfang_Male_Orc_DKTransform_01.prefab:035ff5d1b1a7af8428a8ef934440e476");
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return PlayClosingLine("LichKing_Banner_Quote.prefab:d42a8f4f69919f449b3dd8ebceaf2a3c", "VO_ICC09_LichKing_Male_Human_Win_01.prefab:e3c063dfdadd1dc4dbafbc06502cd9f0");
		}
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			string soundPath = "VO_ICC09_LichKing_Male_Human_Lose_01.prefab:c5f2c5e213c4ad648be72f77a9afbb2f";
			if (!NotificationManager.Get().HasSoundPlayedThisSession(soundPath))
			{
				yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("LichKing_Banner_Quote.prefab:d42a8f4f69919f449b3dd8ebceaf2a3c", soundPath));
			}
		}
	}
}
