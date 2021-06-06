using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICC_07_Putricide : ICC_MissionEntity
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_BossEquipWeaponLines = new List<string> { "VO_ICC07_Putricide_Male_Undead_EquipWeapon_01.prefab:847d15d3eab3c6647a683c2e6929487a", "VO_ICC07_Putricide_Male_Undead_EquipWeapon_02.prefab:19a64b6261332d04086a3c7167c62225", "VO_ICC07_Putricide_Male_Undead_EquipWeapon_03.prefab:1876558a03207244698b004db6a64684", "VO_ICC07_Putricide_Male_Undead_EquipWeapon_04.prefab:be18761283a2c33439170e7eba68ed8e", "VO_ICC07_Putricide_Male_Undead_EquipWeapon_05.prefab:2bf4c57683e9a864985534c3db3ba512" };

	public override void PreloadAssets()
	{
		foreach (string item in new List<string>
		{
			"VO_ICC07_Putricide_Male_Undead_Intro_02.prefab:9382cba0880e10f428f337f1c0816d4d", "VO_ICC07_Putricide_Male_Undead_PhaseTransition_01.prefab:441c71684bcca9f4bb658400d06370e9", "VO_ICC07_Putricide_Male_Undead_PhaseTransition_03.prefab:2e3eff628125a9c4ea241f50988522ca", "VO_ICC07_Putricide_Male_Undead_EquipWeapon_01.prefab:847d15d3eab3c6647a683c2e6929487a", "VO_ICC07_Putricide_Male_Undead_EquipWeapon_02.prefab:19a64b6261332d04086a3c7167c62225", "VO_ICC07_Putricide_Male_Undead_EquipWeapon_03.prefab:1876558a03207244698b004db6a64684", "VO_ICC07_Putricide_Male_Undead_EquipWeapon_04.prefab:be18761283a2c33439170e7eba68ed8e", "VO_ICC07_Putricide_Male_Undead_EquipWeapon_05.prefab:2bf4c57683e9a864985534c3db3ba512", "VO_ICC07_Putricide_Male_Undead_Slime_01.prefab:9e073f1b94a779647bf7ce3fb2711b3a", "VO_ICC07_Putricide_Male_Undead_FestergutDeath_01.prefab:b0bcc0c83b89bb24a8848e25bb740135",
			"VO_ICC07_Putricide_Male_Undead_RotfaceDeath_01.prefab:cdcfd7d857ca6b047b5c0d7f58edeea3", "VO_ICC07_Putricide_Male_Undead_Death_01.prefab:e436c5c39a77fd349bcfd6fd2c2cc700", "VO_ICC07_LichKing_Male_Human_Win_02.prefab:4b77c4144ffd85041881e9f6d7cd990d", "VO_ICC07_LichKing_Male_Human_Lose_01.prefab:80e34f0495b56a64c8e15802081f992c", "VO_ICC07_Putricide_Male_Undead_EmoteResponse_01.prefab:59c8824204e7d42478a7e324fd1c352d", "VO_ICC07_Putricide_Male_Undead_Rotface_01.prefab:c35bec326cb86a44a9a1a859b133c2a5", "VO_ICC07_Rotface_Male_Abomination_Rotface_02.prefab:796c6d1def76b5a47981a86efadc74d6", "VO_ICC07_Putricide_Male_Undead_LichKing_01.prefab:a1d49e32bdcea914da88da91efd28994", "VO_ICC07_LichKing_Male_Human_LichKing_02.prefab:eeeef273f030049428c88807b35626eb", "VO_ICC07_Putricide_Male_Undead_Putricide_02.prefab:0bce52cef242298469f65bc6c5ba64ab",
			"VO_ICC07_Putricide_Male_Undead_Putricide_03.prefab:fd8b8337771ec274cab76629c87b2556", "VO_ICC07_Putricide_Male_Undead_Putricide_04.prefab:6bfefe13dd3e2a54fab302ae5d857371", "VO_ICC07_Putricide_Male_Undead_TransformDK_01.prefab:756546f22cd1da94297f9c2e8772c5d7", "VO_ICC07_Putricide_Male_Undead_Scientist_01.prefab:7994f79ffe2411a4180d81e61ecf3fe5", "VO_ICC07_Putricide_Male_Undead_EaterOfSecrets_01.prefab:08f7b48a747d22649b55199476abae2f", "VO_ICC07_Putricide_Male_Undead_TentaclesForArms_01.prefab:1ee3471132fd51546a602d2472f104b1", "VO_ICC07_Putricide_Male_Undead_EaglehornBow_01.prefab:c55d8caeef984fc469831991ce23d914", "VO_ICC07_Putricide_Male_Undead_Aviana_01.prefab:fe7bb6b5bfbdb9e4cae830360c8302a3", "VO_ICC07_Putricide_Male_Undead_CloakedHuntress_01.prefab:d9cfb7db8afd4d84fad5e5484396dd30", "VO_ICC07_Putricide_Male_Undead_EtherealArcanist_01.prefab:53b4b70a326e6c24c8b4a5d975361728",
			"VO_ICC07_Putricide_Male_Undead_MysteriousChallenger_01.prefab:79ef81254391a3e47aa761516d4d3039", "VO_ICC07_Putricide_Male_Undead_Ooze_01.prefab:22e667f52abb5c8428aea86401b4f96e"
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
						m_soundName = "VO_ICC07_Putricide_Male_Undead_EmoteResponse_01.prefab:59c8824204e7d42478a7e324fd1c352d",
						m_stringTag = "VO_ICC07_Putricide_Male_Undead_EmoteResponse_01"
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
						m_soundName = "VO_ICC07_Putricide_Male_Undead_Intro_02.prefab:9382cba0880e10f428f337f1c0816d4d",
						m_stringTag = "VO_ICC07_Putricide_Male_Undead_Intro_02"
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (m_playedLines.Contains(item))
		{
			yield break;
		}
		switch (missionEvent)
		{
		case 101:
			m_playedLines.Add(item);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(4f);
			if (!m_enemySpeaking)
			{
				yield return PlayLineOnlyOnce(enemyActor, "VO_ICC07_Putricide_Male_Undead_PhaseTransition_01.prefab:441c71684bcca9f4bb658400d06370e9");
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 102:
			m_playedLines.Add(item);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(3f);
			if (!m_enemySpeaking)
			{
				yield return PlayLineOnlyOnce(enemyActor, "VO_ICC07_Putricide_Male_Undead_PhaseTransition_03.prefab:2e3eff628125a9c4ea241f50988522ca");
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 103:
			if (m_BossEquipWeaponLines.Count != 0)
			{
				GameState.Get().SetBusy(busy: true);
				string text = m_BossEquipWeaponLines[Random.Range(0, m_BossEquipWeaponLines.Count)];
				m_BossEquipWeaponLines.Remove(text);
				yield return PlayLineOnlyOnce(enemyActor, text);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 104:
			m_playedLines.Add(item);
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC07_Putricide_Male_Undead_Slime_01.prefab:9e073f1b94a779647bf7ce3fb2711b3a");
			GameState.Get().SetBusy(busy: false);
			break;
		case 105:
			m_playedLines.Add(item);
			yield return new WaitForSeconds(1.5f);
			if (!m_enemySpeaking)
			{
				yield return PlayLineOnlyOnce(enemyActor, "VO_ICC07_Putricide_Male_Undead_FestergutDeath_01.prefab:b0bcc0c83b89bb24a8848e25bb740135");
			}
			break;
		case 106:
			m_playedLines.Add(item);
			yield return new WaitForSeconds(1.5f);
			if (!m_enemySpeaking)
			{
				yield return PlayLineOnlyOnce(enemyActor, "VO_ICC07_Putricide_Male_Undead_RotfaceDeath_01.prefab:cdcfd7d857ca6b047b5c0d7f58edeea3");
			}
			break;
		case 107:
			yield return PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_Death_01.prefab:e436c5c39a77fd349bcfd6fd2c2cc700");
			break;
		case 108:
			yield return PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_CloakedHuntress_01.prefab:d9cfb7db8afd4d84fad5e5484396dd30");
			break;
		case 109:
			yield return new WaitForSeconds(1f);
			if (!m_enemySpeaking)
			{
				yield return PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_Aviana_01.prefab:fe7bb6b5bfbdb9e4cae830360c8302a3");
			}
			break;
		case 110:
			yield return new WaitForSeconds(1f);
			if (!m_enemySpeaking)
			{
				yield return PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_TransformDK_01.prefab:756546f22cd1da94297f9c2e8772c5d7");
			}
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
			string cardID = entity.GetCardId();
			m_playedLines.Add(cardID);
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			yield return WaitForEntitySoundToFinish(entity);
			switch (cardID)
			{
			case "ICC_314":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_LichKing_01.prefab:a1d49e32bdcea914da88da91efd28994");
				yield return PlayEasterEggLine(GetLichKingFriendlyMinion(), "VO_ICC07_LichKing_Male_Human_LichKing_02.prefab:eeeef273f030049428c88807b35626eb");
				break;
			case "ICC_405":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_Rotface_01.prefab:c35bec326cb86a44a9a1a859b133c2a5");
				yield return PlayEasterEggLine(GetActorByCardId("ICC_405"), "VO_ICC07_Rotface_Male_Abomination_Rotface_02.prefab:796c6d1def76b5a47981a86efadc74d6");
				break;
			case "ICC_204":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_Putricide_02.prefab:0bce52cef242298469f65bc6c5ba64ab");
				yield return PlayEasterEggLine(GetActorByCardId("ICC_204"), "VO_ICC07_Putricide_Male_Undead_Putricide_03.prefab:fd8b8337771ec274cab76629c87b2556");
				yield return PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_Putricide_04.prefab:6bfefe13dd3e2a54fab302ae5d857371");
				break;
			case "FP1_004":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_Scientist_01.prefab:7994f79ffe2411a4180d81e61ecf3fe5");
				break;
			case "CFM_063":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_Scientist_01.prefab:7994f79ffe2411a4180d81e61ecf3fe5");
				break;
			case "OG_254":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_EaterOfSecrets_01.prefab:08f7b48a747d22649b55199476abae2f");
				break;
			case "OG_033":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_TentaclesForArms_01.prefab:1ee3471132fd51546a602d2472f104b1");
				break;
			case "EX1_274":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_EtherealArcanist_01.prefab:53b4b70a326e6c24c8b4a5d975361728");
				break;
			case "AT_079":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_MysteriousChallenger_01.prefab:79ef81254391a3e47aa761516d4d3039");
				break;
			case "EX1_066":
			case "FP1_003":
			case "UNG_946":
			case "CFM_655":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_Ooze_01.prefab:22e667f52abb5c8428aea86401b4f96e");
				break;
			case "EX1_323":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_TransformDK_01.prefab:756546f22cd1da94297f9c2e8772c5d7");
				break;
			case "OG_094":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_EaglehornBow_01.prefab:c55d8caeef984fc469831991ce23d914");
				break;
			}
			yield return IfPlayerPlaysDKHeroVO(entity, enemyActor, "VO_ICC07_Putricide_Male_Undead_TransformDK_01.prefab:756546f22cd1da94297f9c2e8772c5d7");
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return PlayClosingLine("LichKing_Banner_Quote.prefab:d42a8f4f69919f449b3dd8ebceaf2a3c", "VO_ICC07_LichKing_Male_Human_Win_02.prefab:4b77c4144ffd85041881e9f6d7cd990d");
		}
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			string soundPath = "VO_ICC07_LichKing_Male_Human_Lose_01.prefab:80e34f0495b56a64c8e15802081f992c";
			if (!NotificationManager.Get().HasSoundPlayedThisSession(soundPath))
			{
				yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("LichKing_Banner_Quote.prefab:d42a8f4f69919f449b3dd8ebceaf2a3c", soundPath));
			}
		}
	}
}
