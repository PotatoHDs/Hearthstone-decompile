using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003AA RID: 938
public class ICC_07_Putricide : ICC_MissionEntity
{
	// Token: 0x060035A0 RID: 13728 RVA: 0x0011095C File Offset: 0x0010EB5C
	public override void PreloadAssets()
	{
		foreach (string soundPath in new List<string>
		{
			"VO_ICC07_Putricide_Male_Undead_Intro_02.prefab:9382cba0880e10f428f337f1c0816d4d",
			"VO_ICC07_Putricide_Male_Undead_PhaseTransition_01.prefab:441c71684bcca9f4bb658400d06370e9",
			"VO_ICC07_Putricide_Male_Undead_PhaseTransition_03.prefab:2e3eff628125a9c4ea241f50988522ca",
			"VO_ICC07_Putricide_Male_Undead_EquipWeapon_01.prefab:847d15d3eab3c6647a683c2e6929487a",
			"VO_ICC07_Putricide_Male_Undead_EquipWeapon_02.prefab:19a64b6261332d04086a3c7167c62225",
			"VO_ICC07_Putricide_Male_Undead_EquipWeapon_03.prefab:1876558a03207244698b004db6a64684",
			"VO_ICC07_Putricide_Male_Undead_EquipWeapon_04.prefab:be18761283a2c33439170e7eba68ed8e",
			"VO_ICC07_Putricide_Male_Undead_EquipWeapon_05.prefab:2bf4c57683e9a864985534c3db3ba512",
			"VO_ICC07_Putricide_Male_Undead_Slime_01.prefab:9e073f1b94a779647bf7ce3fb2711b3a",
			"VO_ICC07_Putricide_Male_Undead_FestergutDeath_01.prefab:b0bcc0c83b89bb24a8848e25bb740135",
			"VO_ICC07_Putricide_Male_Undead_RotfaceDeath_01.prefab:cdcfd7d857ca6b047b5c0d7f58edeea3",
			"VO_ICC07_Putricide_Male_Undead_Death_01.prefab:e436c5c39a77fd349bcfd6fd2c2cc700",
			"VO_ICC07_LichKing_Male_Human_Win_02.prefab:4b77c4144ffd85041881e9f6d7cd990d",
			"VO_ICC07_LichKing_Male_Human_Lose_01.prefab:80e34f0495b56a64c8e15802081f992c",
			"VO_ICC07_Putricide_Male_Undead_EmoteResponse_01.prefab:59c8824204e7d42478a7e324fd1c352d",
			"VO_ICC07_Putricide_Male_Undead_Rotface_01.prefab:c35bec326cb86a44a9a1a859b133c2a5",
			"VO_ICC07_Rotface_Male_Abomination_Rotface_02.prefab:796c6d1def76b5a47981a86efadc74d6",
			"VO_ICC07_Putricide_Male_Undead_LichKing_01.prefab:a1d49e32bdcea914da88da91efd28994",
			"VO_ICC07_LichKing_Male_Human_LichKing_02.prefab:eeeef273f030049428c88807b35626eb",
			"VO_ICC07_Putricide_Male_Undead_Putricide_02.prefab:0bce52cef242298469f65bc6c5ba64ab",
			"VO_ICC07_Putricide_Male_Undead_Putricide_03.prefab:fd8b8337771ec274cab76629c87b2556",
			"VO_ICC07_Putricide_Male_Undead_Putricide_04.prefab:6bfefe13dd3e2a54fab302ae5d857371",
			"VO_ICC07_Putricide_Male_Undead_TransformDK_01.prefab:756546f22cd1da94297f9c2e8772c5d7",
			"VO_ICC07_Putricide_Male_Undead_Scientist_01.prefab:7994f79ffe2411a4180d81e61ecf3fe5",
			"VO_ICC07_Putricide_Male_Undead_EaterOfSecrets_01.prefab:08f7b48a747d22649b55199476abae2f",
			"VO_ICC07_Putricide_Male_Undead_TentaclesForArms_01.prefab:1ee3471132fd51546a602d2472f104b1",
			"VO_ICC07_Putricide_Male_Undead_EaglehornBow_01.prefab:c55d8caeef984fc469831991ce23d914",
			"VO_ICC07_Putricide_Male_Undead_Aviana_01.prefab:fe7bb6b5bfbdb9e4cae830360c8302a3",
			"VO_ICC07_Putricide_Male_Undead_CloakedHuntress_01.prefab:d9cfb7db8afd4d84fad5e5484396dd30",
			"VO_ICC07_Putricide_Male_Undead_EtherealArcanist_01.prefab:53b4b70a326e6c24c8b4a5d975361728",
			"VO_ICC07_Putricide_Male_Undead_MysteriousChallenger_01.prefab:79ef81254391a3e47aa761516d4d3039",
			"VO_ICC07_Putricide_Male_Undead_Ooze_01.prefab:22e667f52abb5c8428aea86401b4f96e"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060035A1 RID: 13729 RVA: 0x00110B10 File Offset: 0x0010ED10
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
						m_soundName = "VO_ICC07_Putricide_Male_Undead_EmoteResponse_01.prefab:59c8824204e7d42478a7e324fd1c352d",
						m_stringTag = "VO_ICC07_Putricide_Male_Undead_EmoteResponse_01"
					}
				}
			},
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>
				{
					EmoteType.START
				},
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_ICC07_Putricide_Male_Undead_Intro_02.prefab:9382cba0880e10f428f337f1c0816d4d",
						m_stringTag = "VO_ICC07_Putricide_Male_Undead_Intro_02"
					}
				}
			}
		};
	}

	// Token: 0x060035A2 RID: 13730 RVA: 0x00110BB8 File Offset: 0x0010EDB8
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(item))
		{
			yield break;
		}
		switch (missionEvent)
		{
		case 101:
			this.m_playedLines.Add(item);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(4f);
			if (!this.m_enemySpeaking)
			{
				yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC07_Putricide_Male_Undead_PhaseTransition_01.prefab:441c71684bcca9f4bb658400d06370e9", 2.5f);
				GameState.Get().SetBusy(false);
			}
			break;
		case 102:
			this.m_playedLines.Add(item);
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(3f);
			if (!this.m_enemySpeaking)
			{
				yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC07_Putricide_Male_Undead_PhaseTransition_03.prefab:2e3eff628125a9c4ea241f50988522ca", 2.5f);
				GameState.Get().SetBusy(false);
			}
			break;
		case 103:
			if (this.m_BossEquipWeaponLines.Count != 0)
			{
				GameState.Get().SetBusy(true);
				string text = this.m_BossEquipWeaponLines[UnityEngine.Random.Range(0, this.m_BossEquipWeaponLines.Count)];
				this.m_BossEquipWeaponLines.Remove(text);
				yield return base.PlayLineOnlyOnce(enemyActor, text, 2.5f);
				GameState.Get().SetBusy(false);
			}
			break;
		case 104:
			this.m_playedLines.Add(item);
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC07_Putricide_Male_Undead_Slime_01.prefab:9e073f1b94a779647bf7ce3fb2711b3a", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 105:
			this.m_playedLines.Add(item);
			yield return new WaitForSeconds(1.5f);
			if (!this.m_enemySpeaking)
			{
				yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC07_Putricide_Male_Undead_FestergutDeath_01.prefab:b0bcc0c83b89bb24a8848e25bb740135", 2.5f);
			}
			break;
		case 106:
			this.m_playedLines.Add(item);
			yield return new WaitForSeconds(1.5f);
			if (!this.m_enemySpeaking)
			{
				yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC07_Putricide_Male_Undead_RotfaceDeath_01.prefab:cdcfd7d857ca6b047b5c0d7f58edeea3", 2.5f);
			}
			break;
		case 107:
			yield return base.PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_Death_01.prefab:e436c5c39a77fd349bcfd6fd2c2cc700", 2.5f);
			break;
		case 108:
			yield return base.PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_CloakedHuntress_01.prefab:d9cfb7db8afd4d84fad5e5484396dd30", 2.5f);
			break;
		case 109:
			yield return new WaitForSeconds(1f);
			if (!this.m_enemySpeaking)
			{
				yield return base.PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_Aviana_01.prefab:fe7bb6b5bfbdb9e4cae830360c8302a3", 2.5f);
			}
			break;
		case 110:
			yield return new WaitForSeconds(1f);
			if (!this.m_enemySpeaking)
			{
				yield return base.PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_TransformDK_01.prefab:756546f22cd1da94297f9c2e8772c5d7", 2.5f);
			}
			break;
		}
		yield break;
	}

	// Token: 0x060035A3 RID: 13731 RVA: 0x00110BCE File Offset: 0x0010EDCE
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
		string cardID = entity.GetCardId();
		this.m_playedLines.Add(cardID);
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string text = cardID;
		uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
		if (num <= 2336331258U)
		{
			if (num <= 904873442U)
			{
				if (num != 252003475U)
				{
					if (num != 889081561U)
					{
						if (num == 904873442U)
						{
							if (text == "OG_033")
							{
								yield return base.PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_TentaclesForArms_01.prefab:1ee3471132fd51546a602d2472f104b1", 2.5f);
							}
						}
					}
					else if (text == "OG_094")
					{
						yield return base.PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_EaglehornBow_01.prefab:c55d8caeef984fc469831991ce23d914", 2.5f);
					}
				}
				else if (text == "AT_079")
				{
					yield return base.PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_MysteriousChallenger_01.prefab:79ef81254391a3e47aa761516d4d3039", 2.5f);
				}
			}
			else if (num <= 1780024521U)
			{
				if (num != 1655527787U)
				{
					if (num == 1780024521U)
					{
						if (text == "ICC_314")
						{
							yield return base.PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_LichKing_01.prefab:a1d49e32bdcea914da88da91efd28994", 2.5f);
							yield return base.PlayEasterEggLine(base.GetLichKingFriendlyMinion(), "VO_ICC07_LichKing_Male_Human_LichKing_02.prefab:eeeef273f030049428c88807b35626eb", 2.5f);
						}
					}
				}
				else if (text == "ICC_204")
				{
					yield return base.PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_Putricide_02.prefab:0bce52cef242298469f65bc6c5ba64ab", 2.5f);
					yield return base.PlayEasterEggLine(base.GetActorByCardId("ICC_204"), "VO_ICC07_Putricide_Male_Undead_Putricide_03.prefab:fd8b8337771ec274cab76629c87b2556", 2.5f);
					yield return base.PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_Putricide_04.prefab:6bfefe13dd3e2a54fab302ae5d857371", 2.5f);
				}
			}
			else if (num != 1905604154U)
			{
				if (num == 2336331258U)
				{
					if (text == "ICC_405")
					{
						yield return base.PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_Rotface_01.prefab:c35bec326cb86a44a9a1a859b133c2a5", 2.5f);
						yield return base.PlayEasterEggLine(base.GetActorByCardId("ICC_405"), "VO_ICC07_Rotface_Male_Abomination_Rotface_02.prefab:796c6d1def76b5a47981a86efadc74d6", 2.5f);
					}
				}
			}
			else if (text == "EX1_323")
			{
				yield return base.PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_TransformDK_01.prefab:756546f22cd1da94297f9c2e8772c5d7", 2.5f);
			}
		}
		else
		{
			if (num <= 2963574068U)
			{
				if (num <= 2508734288U)
				{
					if (num != 2413127911U)
					{
						if (num != 2508734288U)
						{
							goto IL_67A;
						}
						if (!(text == "EX1_066"))
						{
							goto IL_67A;
						}
					}
					else
					{
						if (!(text == "CFM_063"))
						{
							goto IL_67A;
						}
						yield return base.PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_Scientist_01.prefab:7994f79ffe2411a4180d81e61ecf3fe5", 2.5f);
						goto IL_67A;
					}
				}
				else if (num != 2585997707U)
				{
					if (num != 2963574068U)
					{
						goto IL_67A;
					}
					if (!(text == "CFM_655"))
					{
						goto IL_67A;
					}
				}
				else
				{
					if (!(text == "OG_254"))
					{
						goto IL_67A;
					}
					yield return base.PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_EaterOfSecrets_01.prefab:08f7b48a747d22649b55199476abae2f", 2.5f);
					goto IL_67A;
				}
			}
			else if (num <= 3427400925U)
			{
				if (num != 3309957592U)
				{
					if (num != 3427400925U)
					{
						goto IL_67A;
					}
					if (!(text == "FP1_004"))
					{
						goto IL_67A;
					}
					yield return base.PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_Scientist_01.prefab:7994f79ffe2411a4180d81e61ecf3fe5", 2.5f);
					goto IL_67A;
				}
				else if (!(text == "FP1_003"))
				{
					goto IL_67A;
				}
			}
			else if (num != 4072856505U)
			{
				if (num != 4204428441U)
				{
					goto IL_67A;
				}
				if (!(text == "UNG_946"))
				{
					goto IL_67A;
				}
			}
			else
			{
				if (!(text == "EX1_274"))
				{
					goto IL_67A;
				}
				yield return base.PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_EtherealArcanist_01.prefab:53b4b70a326e6c24c8b4a5d975361728", 2.5f);
				goto IL_67A;
			}
			yield return base.PlayEasterEggLine(enemyActor, "VO_ICC07_Putricide_Male_Undead_Ooze_01.prefab:22e667f52abb5c8428aea86401b4f96e", 2.5f);
		}
		IL_67A:
		yield return base.IfPlayerPlaysDKHeroVO(entity, enemyActor, "VO_ICC07_Putricide_Male_Undead_TransformDK_01.prefab:756546f22cd1da94297f9c2e8772c5d7");
		yield break;
	}

	// Token: 0x060035A4 RID: 13732 RVA: 0x00110BE4 File Offset: 0x0010EDE4
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return base.PlayClosingLine("LichKing_Banner_Quote.prefab:d42a8f4f69919f449b3dd8ebceaf2a3c", "VO_ICC07_LichKing_Male_Human_Win_02.prefab:4b77c4144ffd85041881e9f6d7cd990d", 2.5f);
		}
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			string soundPath = "VO_ICC07_LichKing_Male_Human_Lose_01.prefab:80e34f0495b56a64c8e15802081f992c";
			if (!NotificationManager.Get().HasSoundPlayedThisSession(soundPath))
			{
				yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("LichKing_Banner_Quote.prefab:d42a8f4f69919f449b3dd8ebceaf2a3c", soundPath, 0f, true, false));
			}
		}
		yield break;
	}

	// Token: 0x04001CF8 RID: 7416
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001CF9 RID: 7417
	private List<string> m_BossEquipWeaponLines = new List<string>
	{
		"VO_ICC07_Putricide_Male_Undead_EquipWeapon_01.prefab:847d15d3eab3c6647a683c2e6929487a",
		"VO_ICC07_Putricide_Male_Undead_EquipWeapon_02.prefab:19a64b6261332d04086a3c7167c62225",
		"VO_ICC07_Putricide_Male_Undead_EquipWeapon_03.prefab:1876558a03207244698b004db6a64684",
		"VO_ICC07_Putricide_Male_Undead_EquipWeapon_04.prefab:be18761283a2c33439170e7eba68ed8e",
		"VO_ICC07_Putricide_Male_Undead_EquipWeapon_05.prefab:2bf4c57683e9a864985534c3db3ba512"
	};
}
