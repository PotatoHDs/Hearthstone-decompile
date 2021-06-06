using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003DD RID: 989
public class GIL_Dungeon_Bonus_Boss_61h : GIL_Dungeon
{
	// Token: 0x06003772 RID: 14194 RVA: 0x00118E06 File Offset: 0x00117006
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_GILFinalBoss);
	}

	// Token: 0x06003773 RID: 14195 RVA: 0x00118E18 File Offset: 0x00117018
	public override void PreloadAssets()
	{
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_61h_Female_Orc_IntroShaw_01.prefab:535335d8e2b34334a83374b62d03f750",
			"VO_GILA_BOSS_61h_Female_Orc_IntroTess_01.prefab:ffbef8020373f24478241e3ea517a6f2",
			"VO_GILA_BOSS_61h_Female_Orc_IntroCrowley_01.prefab:5f8fb394ab0494a48aff74ac1234ed6c",
			"VO_GILA_BOSS_61h_Female_Orc_IntroToki_01.prefab:bcb8a646f92003e4c9797323ddc44033",
			"VO_GILA_BOSS_61h_Female_Orc_EmoteResponse_01.prefab:bc6efd6494a855b419b46bf9643d4da6",
			"VO_GILA_BOSS_61h_Female_Orc_EmoteResponseTess_01.prefab:4c726930acd2ee44eb6eb33fce12f292",
			"VO_GILA_BOSS_61h_Female_Orc_EmoteResponseCrowley_01.prefab:4e95c68f2b8446049b05b91b866d803c",
			"VO_GILA_BOSS_61h_Female_Orc_EmoteResponseShaw_01.prefab:5b8a2ee25bd548849b1c6b20a7bd26e7",
			"VO_GILA_BOSS_61h_Female_Orc_EmoteResponseToki_01.prefab:7a7b36e367568ea408ccec1ff319360d",
			"VO_GILA_BOSS_61h_Female_Orc_Death_01.prefab:899a9fc58b40f764b93b4fd6312ce163",
			"VO_GILA_BOSS_61h_Female_Orc_DefeatPlayer_01.prefab:a9bd704b56a75d2409a12a4a74a2e314",
			"VO_GILA_BOSS_61h_Female_Orc_EventTessTurn2_01.prefab:e8c971c27f987ad428bac32802db5a4d",
			"VO_GILA_BOSS_61h_Female_Orc_EventTessTurn4_01.prefab:fd16b5c7e7fd68440a009ad805567ab4",
			"VO_GILA_BOSS_61h_Female_Orc_EventTessScavenges_01.prefab:d57755e79970d8149b3aae9dcbc8bae6",
			"VO_GILA_BOSS_61h_Female_Orc_EventTessScavenges_02.prefab:8374c14abcd95964b96a470015d8d02d",
			"VO_GILA_BOSS_61h_Female_Orc_EventTessScavenges_03.prefab:e523d8d15ed98844980081271374cfdf",
			"VO_GILA_BOSS_61h_Female_Orc_EventTessScavenges_04.prefab:70af279046a19c045b4fb6e0024d7710",
			"VO_GILA_BOSS_61h_Female_Orc_EventTessHagathaLow_01.prefab:e7c478d87d33ecc4b98f553226ae965b",
			"VO_GILA_BOSS_61h_Female_Orc_EventTessLow_01.prefab:590fb10488f3f3740869709826ab04e7",
			"VO_GILA_BOSS_61h_Female_Orc_EventCrowleyTurn2_01.prefab:1ab82acdfc1cb5a4bbb1b3e77bc33862",
			"VO_GILA_BOSS_61h_Female_Orc_EventCrowleyTurn4_01.prefab:61033769f4ce13c439008b3c6b0681b6",
			"VO_GILA_BOSS_61h_Female_Orc_EventCannonKill_01.prefab:0812eb3430eaef440939e80ef0d51cc6",
			"VO_GILA_BOSS_61h_Female_Orc_EventCannonKill_02.prefab:bf261abe347499f41bbeaf026e794dd8",
			"VO_GILA_BOSS_61h_Female_Orc_EventCannonKill_03.prefab:d2d067c9aee5d6c4fa366f9355afe827",
			"VO_GILA_BOSS_61h_Female_Orc_EventCannonKill_04.prefab:6b2a2f4c2c790cb4cbcea8913caf5eca",
			"VO_GILA_BOSS_61h_Female_Orc_EventCrowleyHagathaLow_01.prefab:98c3f1146ba002e46b47978c1e2bc2a5",
			"VO_GILA_BOSS_61h_Female_Orc_EventCrowleyLow_01.prefab:27ec9f0458cd3084d90caad86240eb46",
			"VO_GILA_BOSS_61h_Female_Orc_EventShawTurn2_01.prefab:9995bfad8f2e73a48b3f06cc12845f59",
			"VO_GILA_BOSS_61h_Female_Orc_EventShawTurn2_02.prefab:5f967425032b51a47958fc3cfdb76440",
			"VO_GILA_BOSS_61h_Female_Orc_EventShawTurn4_01.prefab:aa0aceaee3a6e9a4ab1b647cc1c83f43",
			"VO_GILA_BOSS_61h_Female_Orc_EventShawHoundKill_01.prefab:6900c0532641a1d4983aefa20416f62e",
			"VO_GILA_BOSS_61h_Female_Orc_EventShawHoundKill_02.prefab:19fe27faa78b9d845b74339047a765ad",
			"VO_GILA_BOSS_61h_Female_Orc_EventShawLow_01.prefab:6d08f00e0ff38144b815cb3c56143544",
			"VO_GILA_BOSS_61h_Female_Orc_EventTokiTurn2_01.prefab:c52baf63bce5dcc458b19b5489613787",
			"VO_GILA_BOSS_61h_Female_Orc_EventTokiTurn4_01.prefab:6e8ada4d386e47f429de1e95412509cf",
			"VO_GILA_BOSS_61h_Female_Orc_EventTokiHeroPower_01.prefab:7a469c7edc89af04aa1371af82f7b7b2",
			"VO_GILA_BOSS_61h_Female_Orc_EventTokiHeroPower_03.prefab:40a46a8079442f249accc382187e0616",
			"VO_GILA_BOSS_61h_Female_Orc_EventTokiHeroPower_04.prefab:52732a46c2c27644dac3d9116e3ca47d",
			"VO_GILA_BOSS_61h_Female_Orc_EventTokiHeroPower_05.prefab:2725c1590918fc1459f3542f7f1911d1",
			"VO_GILA_BOSS_61h_Female_Orc_EventTokiHagathaLow_01.prefab:02966241e760b9548973e9c7800b1de4",
			"VO_GILA_BOSS_61h_Female_Orc_EventTokiLow_01.prefab:29636424871b53141a77efe8c0ea54a6",
			"VO_GILA_BOSS_61h_Female_Orc_EventSummonCrowskin_01.prefab:5b0211815822d664bab58ae89af6caf5",
			"VO_GILA_BOSS_61h_Female_Orc_EventSummonGodfrey_01.prefab:e7c1c99447f410146a6591b07cfe10ea",
			"VO_GILA_500h3_Female_Human_EVENT_HAGATHA_SUB_IN_01.prefab:839ebea0d5b8ea14e938406369e5b872",
			"VO_GILA_500h3_Female_Human_EVENT_HAGATHA_TURN2_01.prefab:8841f3d2df72df64f8bf56c93dc6af71",
			"VO_GILA_500h3_Female_Human_EVENT_HAGATHA_TURN4_01.prefab:2ec7c605cae6f134bb91bb92abfb7c54",
			"VO_GILA_500h3_Female_Human_EVENT_HAGATHA_WOUNDED_01.prefab:d4997ebee335fcc4eab92dd382e250aa",
			"VO_GILA_600h_Male_Worgen_EVENT_HAGATHA_SUB_IN_01.prefab:78cbcc19eca877a4f9af73ad8a2afd0b",
			"VO_GILA_600h_Male_Worgen_EVENT_HAGATHA_TURN2_01.prefab:783d2a2edfcbce34cb2bc63bdcd3e826",
			"VO_GILA_600h_Male_Worgen_EVENT_HAGATHA_TURN4_01.prefab:a0951c1397e730e4798a67757267aa8c",
			"VO_GILA_600h_Male_Worgen_EVENT_HAGATHA_WOUNDED_01.prefab:942de80c17f6f934dae5ea383b24e1fa",
			"VO_GILA_400h_Male_Human_EVENT_HAGATHA_INTRO_01.prefab:aeeecbcb284c22847810924c4efab9dd",
			"VO_GILA_400h_Male_Human_EVENT_HAGATHA_TURN4_01.prefab:b36ade3a44d65fb4599d2cef127b0c2b",
			"VO_GILA_400h_Male_Human_EVENT_HAGATHA_LOWHEALTH_01.prefab:dee647cca1aeebb4792ef570f19ab1ff",
			"VO_GILA_400h_Male_Human_EVENT_HAGATHA_ALMOSTDEAD_02.prefab:0d614b21e4be49d41aee63cb5702a9d2",
			"VO_GILA_900h_Female_Gnome_EVENT_HAGATHA_SUB_IN_01.prefab:f8a2268225028954896396eaee3ed896",
			"VO_GILA_900h_Female_Gnome_EVENT_HAGATHA_TURN2_01.prefab:99821f787624bd448abb69b058de25a7",
			"VO_GILA_900h_Female_Gnome_EVENT_HAGATHA_WOUNDED_01.prefab:b9f2daf4ec0707348aa0206702a71e30",
			"VO_GILA_900h_Female_Gnome_EVENT_HAGATHA_ALMOST_DEAD_01.prefab:2e4efefea5deeb049ae03a4e868a7ef0"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003774 RID: 14196 RVA: 0x001190F4 File Offset: 0x001172F4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_61h_Female_Orc_IntroShaw_01.prefab:535335d8e2b34334a83374b62d03f750", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (!MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			return;
		}
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (cardId == "GILA_500h4")
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_61h_Female_Orc_EmoteResponseTess_01.prefab:4c726930acd2ee44eb6eb33fce12f292", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (cardId == "GILA_600h2")
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_61h_Female_Orc_EmoteResponseCrowley_01.prefab:4e95c68f2b8446049b05b91b866d803c", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (cardId == "GILA_900h2")
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_61h_Female_Orc_EmoteResponseToki_01.prefab:7a7b36e367568ea408ccec1ff319360d", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (!(cardId == "GILA_400h"))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_61h_Female_Orc_EmoteResponse_01.prefab:bc6efd6494a855b419b46bf9643d4da6", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_61h_Female_Orc_EmoteResponseShaw_01.prefab:5b8a2ee25bd548849b1c6b20a7bd26e7", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
	}

	// Token: 0x06003775 RID: 14197 RVA: 0x00119274 File Offset: 0x00117474
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_61h_Female_Orc_Death_01.prefab:899a9fc58b40f764b93b4fd6312ce163";
	}

	// Token: 0x06003776 RID: 14198 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003777 RID: 14199 RVA: 0x0011927B File Offset: 0x0011747B
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "GIL_618"))
		{
			if (cardId == "GIL_825")
			{
				yield return base.PlayLineOnlyOnce(actor, "VO_GILA_BOSS_61h_Female_Orc_EventSummonGodfrey_01.prefab:e7c1c99447f410146a6591b07cfe10ea", 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, "VO_GILA_BOSS_61h_Female_Orc_EventSummonCrowskin_01.prefab:5b0211815822d664bab58ae89af6caf5", 2.5f);
		}
		yield break;
	}

	// Token: 0x06003778 RID: 14200 RVA: 0x00119291 File Offset: 0x00117491
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "GILA_500h4"))
		{
			if (!(cardId == "GILA_600h2"))
			{
				if (cardId == "GILA_900h2")
				{
					yield return base.PlayLineOnlyOnce(actor, "VO_GILA_BOSS_61h_Female_Orc_IntroToki_01.prefab:bcb8a646f92003e4c9797323ddc44033", 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, "VO_GILA_BOSS_61h_Female_Orc_IntroCrowley_01.prefab:5f8fb394ab0494a48aff74ac1234ed6c", 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, "VO_GILA_BOSS_61h_Female_Orc_IntroTess_01.prefab:ffbef8020373f24478241e3ea517a6f2", 2.5f);
		}
		yield break;
	}

	// Token: 0x06003779 RID: 14201 RVA: 0x001192A7 File Offset: 0x001174A7
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		Actor playerActor = GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(item))
		{
			yield break;
		}
		switch (missionEvent)
		{
		case 101:
			this.randomLine = base.PopRandomLineWithChance(this.m_TessPowerLines);
			if (this.randomLine != null)
			{
				yield return base.PlayLineOnlyOnce(enemyActor, this.randomLine, 2.5f);
			}
			break;
		case 102:
			this.randomLine = base.PopRandomLineWithChance(this.m_DariusCannonLines);
			if (this.randomLine != null)
			{
				yield return base.PlayLineOnlyOnce(enemyActor, this.randomLine, 2.5f);
			}
			break;
		case 103:
			this.randomLine = base.PopRandomLineWithChance(this.m_ShawHoundLines);
			if (this.randomLine != null)
			{
				yield return base.PlayLineOnlyOnce(enemyActor, this.randomLine, 2.5f);
			}
			break;
		case 105:
		{
			string cardId = playerActor.GetEntity().GetCardId();
			if (!(cardId == "GILA_500h4"))
			{
				if (!(cardId == "GILA_600h2"))
				{
					if (!(cardId == "GILA_900h2"))
					{
						if (cardId == "GILA_400h")
						{
							yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventShawLow_01.prefab:6d08f00e0ff38144b815cb3c56143544", 2.5f);
							yield return base.PlayLineOnlyOnce(playerActor, "VO_GILA_400h_Male_Human_EVENT_HAGATHA_ALMOSTDEAD_02.prefab:0d614b21e4be49d41aee63cb5702a9d2", 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventTokiLow_01.prefab:29636424871b53141a77efe8c0ea54a6", 2.5f);
						yield return base.PlayLineOnlyOnce(playerActor, "VO_GILA_900h_Female_Gnome_EVENT_HAGATHA_ALMOST_DEAD_01.prefab:2e4efefea5deeb049ae03a4e868a7ef0", 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventCrowleyLow_01.prefab:27ec9f0458cd3084d90caad86240eb46", 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventTessLow_01.prefab:590fb10488f3f3740869709826ab04e7", 2.5f);
			}
			break;
		}
		case 106:
		{
			string cardId = playerActor.GetEntity().GetCardId();
			if (!(cardId == "GILA_500h4"))
			{
				if (!(cardId == "GILA_600h2"))
				{
					if (!(cardId == "GILA_900h2"))
					{
						if (cardId == "GILA_400h")
						{
							yield return base.PlayLineOnlyOnce(playerActor, "VO_GILA_400h_Male_Human_EVENT_HAGATHA_LOWHEALTH_01.prefab:dee647cca1aeebb4792ef570f19ab1ff", 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineOnlyOnce(playerActor, "VO_GILA_900h_Female_Gnome_EVENT_HAGATHA_WOUNDED_01.prefab:b9f2daf4ec0707348aa0206702a71e30", 2.5f);
						yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventTokiHagathaLow_01.prefab:02966241e760b9548973e9c7800b1de4", 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventCrowleyHagathaLow_01.prefab:98c3f1146ba002e46b47978c1e2bc2a5", 2.5f);
					yield return base.PlayLineOnlyOnce(playerActor, "VO_GILA_600h_Male_Worgen_EVENT_HAGATHA_WOUNDED_01.prefab:942de80c17f6f934dae5ea383b24e1fa", 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(playerActor, "VO_GILA_500h3_Female_Human_EVENT_HAGATHA_WOUNDED_01.prefab:d4997ebee335fcc4eab92dd382e250aa", 2.5f);
				yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventTessHagathaLow_01.prefab:e7c478d87d33ecc4b98f553226ae965b", 2.5f);
			}
			break;
		}
		}
		yield break;
	}

	// Token: 0x0600377A RID: 14202 RVA: 0x001192BD File Offset: 0x001174BD
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		Actor playerActor = GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		if (turn > 4 && turn % 2 == 0)
		{
			string cardId = playerActor.GetEntity().GetCardId();
			if (!(cardId == "GILA_500h4"))
			{
				if (!(cardId == "GILA_600h2"))
				{
					if (!(cardId == "GILA_900h2"))
					{
						if (cardId == "GILA_400h")
						{
							if (!this.hasPlayedShawTurnLine1 && !this.hasPlayedShawTurnLine2)
							{
								GameState.Get().SetBusy(true);
								yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventShawTurn2_01.prefab:9995bfad8f2e73a48b3f06cc12845f59", 2.5f);
								yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventShawTurn2_02.prefab:5f967425032b51a47958fc3cfdb76440", 2.5f);
								this.hasPlayedShawTurnLine1 = true;
								GameState.Get().SetBusy(false);
							}
							else if (this.hasPlayedShawTurnLine1 && !this.hasPlayedShawTurnLine2)
							{
								GameState.Get().SetBusy(true);
								yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventShawTurn4_01.prefab:aa0aceaee3a6e9a4ab1b647cc1c83f43", 2.5f);
								yield return base.PlayLineOnlyOnce(playerActor, "VO_GILA_400h_Male_Human_EVENT_HAGATHA_TURN4_01.prefab:b36ade3a44d65fb4599d2cef127b0c2b", 2.5f);
								this.hasPlayedShawTurnLine2 = true;
								GameState.Get().SetBusy(false);
							}
						}
					}
					else if (!this.hasPlayedTokiTurnLine1 && !this.hasPlayedTokiTurnLine2)
					{
						GameState.Get().SetBusy(true);
						yield return base.PlayLineOnlyOnce(playerActor, "VO_GILA_900h_Female_Gnome_EVENT_HAGATHA_TURN2_01.prefab:99821f787624bd448abb69b058de25a7", 2.5f);
						yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventTokiTurn2_01.prefab:c52baf63bce5dcc458b19b5489613787", 2.5f);
						this.hasPlayedTokiTurnLine1 = true;
						GameState.Get().SetBusy(false);
					}
					else if (this.hasPlayedTokiTurnLine1 && !this.hasPlayedTokiTurnLine2)
					{
						GameState.Get().SetBusy(true);
						yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventTokiTurn4_01.prefab:6e8ada4d386e47f429de1e95412509cf", 2.5f);
						this.hasPlayedTokiTurnLine2 = true;
						GameState.Get().SetBusy(false);
					}
				}
				else if (!this.hasPlayedDariusTurnLine1 && !this.hasPlayedDariusTurnLine2)
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayLineOnlyOnce(playerActor, "VO_GILA_600h_Male_Worgen_EVENT_HAGATHA_TURN2_01.prefab:783d2a2edfcbce34cb2bc63bdcd3e826", 2.5f);
					yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventCrowleyTurn2_01.prefab:1ab82acdfc1cb5a4bbb1b3e77bc33862", 2.5f);
					this.hasPlayedDariusTurnLine1 = true;
					GameState.Get().SetBusy(false);
				}
				else if (this.hasPlayedDariusTurnLine1 && !this.hasPlayedDariusTurnLine2)
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventCrowleyTurn4_01.prefab:61033769f4ce13c439008b3c6b0681b6", 2.5f);
					yield return base.PlayLineOnlyOnce(playerActor, "VO_GILA_600h_Male_Worgen_EVENT_HAGATHA_TURN4_01.prefab:a0951c1397e730e4798a67757267aa8c", 2.5f);
					this.hasPlayedDariusTurnLine2 = true;
					GameState.Get().SetBusy(false);
				}
			}
			else if (!this.hasPlayedTessTurnLine1 && !this.hasPlayedTessTurnLine2)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventTessTurn2_01.prefab:e8c971c27f987ad428bac32802db5a4d", 2.5f);
				yield return base.PlayLineOnlyOnce(playerActor, "VO_GILA_500h3_Female_Human_EVENT_HAGATHA_TURN2_01.prefab:8841f3d2df72df64f8bf56c93dc6af71", 2.5f);
				this.hasPlayedTessTurnLine1 = true;
				GameState.Get().SetBusy(false);
			}
			else if (this.hasPlayedTessTurnLine1 && !this.hasPlayedTessTurnLine2)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineOnlyOnce(playerActor, "VO_GILA_500h3_Female_Human_EVENT_HAGATHA_TURN4_01.prefab:2ec7c605cae6f134bb91bb92abfb7c54", 2.5f);
				yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventTessTurn4_01.prefab:fd16b5c7e7fd68440a009ad805567ab4", 2.5f);
				this.hasPlayedTessTurnLine2 = true;
				GameState.Get().SetBusy(false);
			}
		}
		yield break;
	}

	// Token: 0x0600377B RID: 14203 RVA: 0x001192D3 File Offset: 0x001174D3
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			string soundPath = "VO_GILA_BOSS_61h_Female_Orc_DefeatPlayer_01.prefab:a9bd704b56a75d2409a12a4a74a2e314";
			if (!NotificationManager.Get().HasSoundPlayedThisSession(soundPath))
			{
				yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("Hagatha_Banner_Quote.prefab:678033af721880948a86bc69b02ef1ac", soundPath, 0f, true, false));
			}
		}
		yield break;
	}

	// Token: 0x0600377C RID: 14204 RVA: 0x001192E9 File Offset: 0x001174E9
	protected override IEnumerator RespondToResetGameFinishedWithTiming(Entity entity)
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
		if (GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor().GetEntity().GetCardId() != "GILA_900h2")
		{
			yield break;
		}
		string cardId = entity.GetCardId();
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "GILA_900p")
		{
			this.randomLine = base.PopRandomLineWithChance(this.m_TokiPowerLines);
			if (this.randomLine != null)
			{
				yield return base.PlayLineOnlyOnce(actor, this.randomLine, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x04001D77 RID: 7543
	public const string TESS_HERO = "GILA_500h4";

	// Token: 0x04001D78 RID: 7544
	public const string DARIUS_HERO = "GILA_600h2";

	// Token: 0x04001D79 RID: 7545
	public const string TOKI_HERO = "GILA_900h2";

	// Token: 0x04001D7A RID: 7546
	public const string SHAW_HERO = "GILA_400h";

	// Token: 0x04001D7B RID: 7547
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D7C RID: 7548
	private bool hasPlayedTessTurnLine1;

	// Token: 0x04001D7D RID: 7549
	private bool hasPlayedTessTurnLine2;

	// Token: 0x04001D7E RID: 7550
	private bool hasPlayedDariusTurnLine1;

	// Token: 0x04001D7F RID: 7551
	private bool hasPlayedDariusTurnLine2;

	// Token: 0x04001D80 RID: 7552
	private bool hasPlayedTokiTurnLine1;

	// Token: 0x04001D81 RID: 7553
	private bool hasPlayedTokiTurnLine2;

	// Token: 0x04001D82 RID: 7554
	private bool hasPlayedShawTurnLine1;

	// Token: 0x04001D83 RID: 7555
	private bool hasPlayedShawTurnLine2;

	// Token: 0x04001D84 RID: 7556
	private string randomLine;

	// Token: 0x04001D85 RID: 7557
	private List<string> m_TessPowerLines = new List<string>
	{
		"VO_GILA_BOSS_61h_Female_Orc_EventTessScavenges_01.prefab:d57755e79970d8149b3aae9dcbc8bae6",
		"VO_GILA_BOSS_61h_Female_Orc_EventTessScavenges_02.prefab:8374c14abcd95964b96a470015d8d02d",
		"VO_GILA_BOSS_61h_Female_Orc_EventTessScavenges_03.prefab:e523d8d15ed98844980081271374cfdf",
		"VO_GILA_BOSS_61h_Female_Orc_EventTessScavenges_04.prefab:70af279046a19c045b4fb6e0024d7710"
	};

	// Token: 0x04001D86 RID: 7558
	private List<string> m_TokiPowerLines = new List<string>
	{
		"VO_GILA_BOSS_61h_Female_Orc_EventTokiHeroPower_01.prefab:7a469c7edc89af04aa1371af82f7b7b2",
		"VO_GILA_BOSS_61h_Female_Orc_EventTokiHeroPower_03.prefab:40a46a8079442f249accc382187e0616",
		"VO_GILA_BOSS_61h_Female_Orc_EventTokiHeroPower_04.prefab:52732a46c2c27644dac3d9116e3ca47d",
		"VO_GILA_BOSS_61h_Female_Orc_EventTokiHeroPower_05.prefab:2725c1590918fc1459f3542f7f1911d1"
	};

	// Token: 0x04001D87 RID: 7559
	private List<string> m_DariusCannonLines = new List<string>
	{
		"VO_GILA_BOSS_61h_Female_Orc_EventCannonKill_01.prefab:0812eb3430eaef440939e80ef0d51cc6",
		"VO_GILA_BOSS_61h_Female_Orc_EventCannonKill_02.prefab:bf261abe347499f41bbeaf026e794dd8",
		"VO_GILA_BOSS_61h_Female_Orc_EventCannonKill_03.prefab:d2d067c9aee5d6c4fa366f9355afe827",
		"VO_GILA_BOSS_61h_Female_Orc_EventCannonKill_04.prefab:6b2a2f4c2c790cb4cbcea8913caf5eca"
	};

	// Token: 0x04001D88 RID: 7560
	private List<string> m_ShawHoundLines = new List<string>
	{
		"VO_GILA_BOSS_61h_Female_Orc_EventShawHoundKill_01.prefab:6900c0532641a1d4983aefa20416f62e",
		"VO_GILA_BOSS_61h_Female_Orc_EventShawHoundKill_02.prefab:19fe27faa78b9d845b74339047a765ad"
	};
}
