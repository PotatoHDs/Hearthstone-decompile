using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003AB RID: 939
public class ICC_08_Finale : ICC_MissionEntity
{
	// Token: 0x060035A6 RID: 13734 RVA: 0x0010F549 File Offset: 0x0010D749
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>();
	}

	// Token: 0x060035A7 RID: 13735 RVA: 0x00110C5C File Offset: 0x0010EE5C
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>
		{
			{
				GameEntityOption.VICTORY_SCREEN_PREFAB_PATH,
				"VictoryTwoScoop_ICCFinale.prefab:f26ea23934d4ea845911dd26cf342b10"
			}
		};
	}

	// Token: 0x060035A8 RID: 13736 RVA: 0x00110C70 File Offset: 0x0010EE70
	public ICC_08_Finale()
	{
		this.m_gameOptions.AddOptions(ICC_08_Finale.s_booleanOptions, ICC_08_Finale.s_stringOptions);
	}

	// Token: 0x060035A9 RID: 13737 RVA: 0x00110EB0 File Offset: 0x0010F0B0
	public override void PreloadAssets()
	{
		foreach (string soundPath in new List<string>
		{
			"VO_ICC08_LichKing_Male_Human_Intro_01.prefab:b24caf7e6f9af4d439e08483dd182a90",
			"VO_ICC08_LichKing_Male_Human_Intro_03.prefab:228920e5af99a9c4e910eec87c5fb60a",
			"VO_ICC08_LichKing_Male_Human_Intro_04.prefab:731de36648f4af2428a8365544d8eaec",
			"VO_ICC08_LichKing_Male_Human_Intro_06.prefab:2b6211261beb98849b76625e5c8a8dad",
			"VO_ICC08_LichKing_Male_Human_Intro_07.prefab:f2607c560856c62429f4601ea72e60a1",
			"VO_ICC08_LichKing_Male_Human_Intro_08.prefab:262b262a4ed8eef4f9f003df8625b399",
			"VO_ICC08_LichKing_Male_Human_Intro_09.prefab:009c57cb5531f924cb50cc5f0065a435",
			"VO_ICC08_LichKing_Male_Human_Warrior_01.prefab:4e5cf13fe9eed3c41a24e254d9b27478",
			"VO_ICC08_LichKing_Male_Human_Shaman_01.prefab:7003d55c6554cca48a55c71490e2fa71",
			"VO_ICC08_LichKing_Male_Human_Rogue_01.prefab:4138e64fc05f5114ba2b34540f0a3ec5",
			"VO_ICC08_LichKing_Male_Human_Paladin_01.prefab:34b0a3dcd009efa45b06359422c8b368",
			"VO_ICC08_LichKing_Male_Human_Hunter_01.prefab:e2d9e2c74bf2a854fabd7324492aa47d",
			"VO_ICC08_LichKing_Male_Human_GenericStart_03.prefab:46e864c0ffc68d349bfd31e4b4c15a08",
			"VO_ICC08_LichKing_Male_Human_Warlock_01.prefab:0c577be64f38882468d65fd25d51d737",
			"VO_ICC08_LichKing_Male_Human_Mage_01.prefab:57e937f989d277e41b0e47890e7b47fb",
			"VO_ICC08_LichKing_Male_Human_Priest_01.prefab:3d968d835dffada4383d11d82fd47e0b",
			"VO_ICC08_LichKing_Male_Human_EndOfTurn4_01.prefab:6574626660e288f4eb796194bf6fea45",
			"VO_ICC08_LichKing_Male_Human_Turn6_01.prefab:9625ec58f43d07a4fa776a1fd28cd03b",
			"VO_ICC08_LichKing_Male_Human_Turn6_03.prefab:0849cc14d7accd54bab51ec07fac1c0d",
			"VO_ICC08_LichKing_Male_Human_Turn6_04.prefab:894659956f1c5984daa739c729ca95a8",
			"VO_ICC08_LichKing_Male_Human_Turn6_05.prefab:bc5520cf2889ce843836a9f428409301",
			"VO_ICC08_LichKing_Male_Human_Turn6_06.prefab:bb4b842360f2b384a8c731a33965f15d",
			"VO_ICC08_LichKing_Male_Human_Turn6_07.prefab:026da99d6a478b84ebed0b175a8d0827",
			"VO_ICC08_LichKing_Male_Human_Turn6_08.prefab:172183a2e12a5fe4ea780fc4ec93fec3",
			"VO_ICC08_LichKing_Male_Human_Frostmourne_01.prefab:10e7732d07bd84f4293dd5836fdc5b9f",
			"VO_ICC08_LichKing_Male_Human_Frostmourne_02.prefab:ae04eda3fb2d4124d9f669ff0a8900a4",
			"VO_ICC08_LichKing_Male_Human_Frostmourne_07.prefab:799e3d3b8fa353c40ac5f2507edfe4f7",
			"VO_ICC08_LichKing_Male_Human_PhaseTransition_02.prefab:7544481ca8382ad4a9969284813a029d",
			"VO_ICC08_LichKing_Male_Human_Phase03_01.prefab:a3a557c5ae7daff4abb5d9cdb7df9fc6",
			"VO_ICC08_LichKing_Male_Human_Wounded_02.prefab:c81d4dcd56e1b2e45a91576dd48688bd",
			"VO_ICC08_LichKing_Male_Human_Death_01.prefab:fca6e9173db669c49adcc3bb0a7a9cdc",
			"VO_ICC08_LichKing_Male_Human_Lose_03.prefab:41cdbee4c68e2a84b96710a94b454ea1",
			"VO_ICC08_Tirion_Male_Human_Win_01.prefab:e97467bae07fb0340a24b6772b048608",
			"VO_ICC08_LichKing_Male_Human_EmoteResponse_01.prefab:efd4eb3c650804d409009435dae67ac7",
			"VO_ICC08_Anduin_Male_Human_Muted_13.prefab:40469f3ca344fe44daebc0414141f034",
			"VO_ICC08_Anduin_Male_Human_Muted_02.prefab:6da6dabe0e1a9104fa0eec237a588772",
			"VO_ICC08_Anduin_Male_Human_Muted_11.prefab:022a5203f3c7c0a4aba9511627bd9bdf",
			"VO_ICC08_Anduin_Male_Human_Muted_06.prefab:8fee035bbf3c2444e9a46663f460245e",
			"VO_ICC08_Anduin_Male_Human_Muted_03.prefab:bcb66402843029e43b70321d8a603dff",
			"VO_ICC08_Anduin_Male_Human_Muted_07.prefab:41b07b89cf5261e4eb19cb1fde0ec7e8",
			"VO_ICC08_LichKing_Male_Human_LichKingMuted_01.prefab:c50c633fe82ad47409a14886eea1c4df",
			"VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_01.prefab:0a2f09b8e8899954d8804606243f11ed",
			"VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_02.prefab:5ecf8b80eeed9874bac3a12448bc418f",
			"VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_03.prefab:fa73d64fd0067af4889a4039a5524432",
			"VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_04.prefab:b13ebe4b70cacb34992e423377da3d9e",
			"VO_ICC08_LichKing_Male_Human_MutedResponse_01.prefab:0341324ad026b094fa7cfc23bf4f2b3e",
			"VO_ICC08_LichKing_Male_Human_MutedResponse_02.prefab:1190ddcc35269db4bb9992be86511742",
			"VO_ICC08_LichKing_Male_Human_MutedResponse_03.prefab:df170463932e5704d87f562b87b59eb5",
			"VO_ICC08_LichKing_Male_Human_MutedResponse_04.prefab:0767c71c4fc8a59499f9b4a9c9ba5946",
			"VO_ICC08_LichKing_Male_Human_MutedResponse_05.prefab:e8d6b6429763ccc47ab58a90a46ccae2",
			"VO_ICC08_LichKing_Male_Human_MutedResponse_06.prefab:d943ed1a74cb28e4f9af97fa163f0f90",
			"VO_ICC08_LichKing_Male_Human_LichKing_01.prefab:e721715f493c5c144a674274e3a835b7",
			"VO_ICC08_LichKing_Male_Human_LichKing_02.prefab:b6789fd334aa9cb458511335ef404341",
			"VO_ICC08_LichKing_Male_Human_TransformDK_01.prefab:a2c40f0880b9cbd4784f39d745d0d50e",
			"VO_ICC08_LichKing_Male_Human_Putricide_01.prefab:4dcd0d68d2594344a999c0011b83e880",
			"VO_ICC08_LichKing_Male_Human_Sindragosa_01.prefab:3f87312e12134dc4b9638d0ff88b3acd",
			"VO_ICC08_LichKing_Male_Human_Lanathel_01.prefab:7eb2a2125339ded459279f66de43ea76",
			"VO_ICC08_LichKing_Male_Human_Mograine_01.prefab:f6e5161911c3d4e4da50ae3106c41f98",
			"VO_ICC08_LichKing_Male_Human_Kelthuzad_01.prefab:3ddade3d8c1f22d4ca63960d8f4b4b05",
			"VO_ICC08_LichKing_Male_Human_Horsemen_01.prefab:318348d96afbbd149896e6d32ae03e5a",
			"VO_ICC08_LichKing_Male_Human_IndestructableWeapon_01.prefab:410f9f1ef41f1bd45b8565fc191a9443",
			"VO_ICC08_LichKing_Male_Human_Tirion_01.prefab:c20cf5d8a42d9f34faddc72368c39bef",
			"VO_ICC08_LichKing_Male_Human_Sylvanas_01.prefab:4589860c323b62548b030d542e0da276",
			"VO_ICC08_LichKing_Male_Human_Bolvar_01.prefab:9b9665e3f305ec34bbf6d482d0b8c85c",
			"VO_ICC08_LichKing_Male_Human_Archmage Antonidas_01.prefab:859e44d75d2591f4bafa299e89320a77",
			"VO_ICC08_LichKing_Male_Human_Arfas_01.prefab:c8c81d9c0e52c5b4fbe75016840f3582",
			"VO_ICC08_LichKing_Male_Human_LilLich_01.prefab:26542e402ab20f34d9aeecc6d684bd14",
			"VO_ICC08_LichKing_Male_Human_MageSpellReflect_01.prefab:4578c14e2604b9140b4c4259a384afe5",
			"VO_ICC08_LichKing_Male_Human_MageSpellReflect_02.prefab:47512997dd73e0943a941cbf069d7ce8",
			"VO_ICC08_LichKing_Male_Human_PowerReplace_01.prefab:de29c2c3a08b3314887c3f4ab1fd5942",
			"VO_LichKing_Male_Human_ResponseAnubaArak_01.prefab:f07f56be69a5482478280e7e5278bf6e",
			"VO_LichKing_Male_Human_ResponseMalganis_01.prefab:75606e89824ea54418eca3c2b75abfd0",
			"VO_ICC08_Malfurion_Male_NightElf_WinA_01.prefab:8a6417a39223ae44683deb0fef141b2d",
			"VO_ICC08_Malfurion_Male_NightElf_WinB_01.prefab:43a8eb73e5fbb614b88e66cb9e0ba74e",
			"VO_ICC08_Rexxar_Male_Orc_WinA_01.prefab:d518bccb3fa3efc41ab6922235160576",
			"VO_ICC08_Rexxar_Male_Orc_WinB_01.prefab:000fe9a4cb6ed8e49a90ea02053ee0c1",
			"VO_ICC08_Jaina_Female_Human_WinA_01.prefab:6d61d1b95a6861b4e9718b6d634bb087",
			"VO_ICC08_Jaina_Female_Human_WinB_01.prefab:1e7c37f8dde322441aa7976d9f755ca8",
			"VO_ICC08_Uther_Male_Human_WinA_01.prefab:ab29d4c60fd014f49829de250dd1d080",
			"VO_ICC08_Uther_Male_Human_WinB_01.prefab:be07d20738f843b4ea6feb6c166186df",
			"VO_ICC08_Anduin_Male_Human_WinA_01.prefab:0adfd8d9372ba384aaea44aead60786d",
			"VO_ICC08_Anduin_Male_Human_WinB_01.prefab:d6f283fa97b38b84e8c26ef93886a59a",
			"VO_ICC08_Valeera_Female_Human_WinA_01.prefab:8251a1520d5802845a46a74bc0bb2c4d",
			"VO_ICC08_Valeera_Female_Human_WinB_01.prefab:0a920fa6de7e5874e80eb4545bb3e992",
			"VO_ICC08_Thrall_Male_Orc_WinA_01.prefab:03463d35eb7e36c4b9612f3ddff0a339",
			"VO_ICC08_Thrall_Male_Orc_WinB_01.prefab:36cc93a5bddc43d4e8bec4f68740ce11",
			"VO_ICC08_Guldan_Male_Orc_WinA_01.prefab:4c26bd9f011c7c9488eab431dc98b3e3",
			"VO_ICC08_Guldan_Male_Orc_WinB_01.prefab:0a5cabe002b4eb34da13390b0334f3e4",
			"VO_ICC08_Garrosh_Male_Orc_WinA_01.prefab:a5e593c457d4a534ba92e4c9a6bb43d1",
			"VO_ICC08_Garrosh_Male_Orc_WinB_01.prefab:ce2eda768ba0a8540b4422e1c7dfba23",
			"VO_ICCA08_033_Male_HumanGhost_Flavor_01.prefab:889d1d45b71ee164caf960cae87e9835",
			"VO_ICCA08_033_Male_HumanGhost_Flavor_02.prefab:f83e29d3903ede54aa72ff74d71ea245",
			"VO_ICCA08_033_Male_HumanGhost_Flavor_03.prefab:ad2f6e62803a52b498446ccb3cbdc89e",
			"VO_ICCA08_033_Male_HumanGhost_Flavor_04.prefab:bb379b9e4548b4a4084ca109875ebabf",
			"VO_ICCA08_033_Male_HumanGhost_Flavor_05.prefab:1e614b8b56fe881468603687a4afe437",
			"VO_ICC08_LichKing_Male_Human_YoungArthas_02.prefab:851d35d99b9a08a4da37334369d85e29"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060035AA RID: 13738 RVA: 0x0010F5C8 File Offset: 0x0010D7C8
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ICCLichKing);
	}

	// Token: 0x060035AB RID: 13739 RVA: 0x00111324 File Offset: 0x0010F524
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		Actor actor = opposingSidePlayer.GetHeroCard().GetActor();
		Entity hero = GameState.Get().GetFriendlySidePlayer().GetHero();
		if (emoteType == EmoteType.START && hero.GetCardId() == "HERO_04b")
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_ICC08_LichKing_Male_Human_YoungArthas_02.prefab:851d35d99b9a08a4da37334369d85e29", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (opposingSidePlayer.GetTag(GAME_TAG.OVERRIDE_EMOTE_0) != this.PRIEST_GREETINGS_DISABLED_EMOTE_OVERRIDE_ID)
		{
			base.PlayEmoteResponse(emoteType, emoteSpell);
			return;
		}
		if (!this.m_mutedResponses.m_triggers.Contains(emoteType))
		{
			return;
		}
		base.PlayNextEmoteResponse(this.m_mutedResponses, actor);
		this.CycleNextResponseGroupIndex(this.m_mutedResponses);
	}

	// Token: 0x060035AC RID: 13740 RVA: 0x001113E4 File Offset: 0x0010F5E4
	protected override void InitEmoteResponses()
	{
		string text = this.m_LichKingIntroLines[UnityEngine.Random.Range(0, this.m_LichKingIntroLines.Count)];
		this.m_mutedResponses = new MissionEntity.EmoteResponseGroup
		{
			m_triggers = new List<EmoteType>
			{
				EmoteType.WELL_PLAYED,
				EmoteType.WELL_PLAYED_DISABLE,
				EmoteType.OOPS,
				EmoteType.OOPS_DISABLE,
				EmoteType.THREATEN,
				EmoteType.THREATEN_DISABLE,
				EmoteType.THANKS,
				EmoteType.THANKS_DISABLE,
				EmoteType.WOW,
				EmoteType.WOW_DISABLE,
				EmoteType.GREETINGS,
				EmoteType.GREETINGS_DISABLE
			},
			m_responses = new List<MissionEntity.EmoteResponse>
			{
				new MissionEntity.EmoteResponse
				{
					m_soundName = "VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_01.prefab:0a2f09b8e8899954d8804606243f11ed",
					m_stringTag = "VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_01"
				},
				new MissionEntity.EmoteResponse
				{
					m_soundName = "VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_02.prefab:5ecf8b80eeed9874bac3a12448bc418f",
					m_stringTag = "VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_02"
				},
				new MissionEntity.EmoteResponse
				{
					m_soundName = "VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_03.prefab:fa73d64fd0067af4889a4039a5524432",
					m_stringTag = "VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_03"
				},
				new MissionEntity.EmoteResponse
				{
					m_soundName = "VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_04.prefab:b13ebe4b70cacb34992e423377da3d9e",
					m_stringTag = "VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_04"
				}
			}
		};
		this.m_emoteResponseGroups = new List<MissionEntity.EmoteResponseGroup>
		{
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>(MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS),
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_ICC08_LichKing_Male_Human_EmoteResponse_01.prefab:efd4eb3c650804d409009435dae67ac7",
						m_stringTag = "VO_ICC08_LichKing_Male_Human_EmoteResponse_01"
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
						m_soundName = text,
						m_stringTag = new AssetReference(text).GetLegacyAssetName()
					}
				}
			},
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>
				{
					EmoteType.DEATH_LINE
				},
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_ICC08_LichKing_Male_Human_Death_01.prefab:fca6e9173db669c49adcc3bb0a7a9cdc",
						m_stringTag = "VO_ICC08_LichKing_Male_Human_Death_01"
					}
				}
			},
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>
				{
					EmoteType.WELL_PLAYED_DISABLE,
					EmoteType.OOPS_DISABLE,
					EmoteType.THREATEN_DISABLE,
					EmoteType.THANKS_DISABLE,
					EmoteType.WOW_DISABLE,
					EmoteType.GREETINGS_DISABLE
				},
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_ICC08_LichKing_Male_Human_MutedResponse_01.prefab:0341324ad026b094fa7cfc23bf4f2b3e",
						m_stringTag = "VO_ICC08_LichKing_Male_Human_MutedResponse_01"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_ICC08_LichKing_Male_Human_MutedResponse_02.prefab:1190ddcc35269db4bb9992be86511742",
						m_stringTag = "VO_ICC08_LichKing_Male_Human_MutedResponse_02"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_ICC08_LichKing_Male_Human_MutedResponse_03.prefab:df170463932e5704d87f562b87b59eb5",
						m_stringTag = "VO_ICC08_LichKing_Male_Human_MutedResponse_03"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_ICC08_LichKing_Male_Human_MutedResponse_04.prefab:0767c71c4fc8a59499f9b4a9c9ba5946",
						m_stringTag = "VO_ICC08_LichKing_Male_Human_MutedResponse_04"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_ICC08_LichKing_Male_Human_MutedResponse_05.prefab:e8d6b6429763ccc47ab58a90a46ccae2",
						m_stringTag = "VO_ICC08_LichKing_Male_Human_MutedResponse_05"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_ICC08_LichKing_Male_Human_MutedResponse_06.prefab:d943ed1a74cb28e4f9af97fa163f0f90",
						m_stringTag = "VO_ICC08_LichKing_Male_Human_MutedResponse_06"
					}
				}
			}
		};
	}

	// Token: 0x060035AD RID: 13741 RVA: 0x0011170C File Offset: 0x0010F90C
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn == 6)
		{
			string text = this.m_LichKingRandomTurnLines[UnityEngine.Random.Range(0, this.m_LichKingRandomTurnLines.Count)];
			GameState.Get().SetBusy(true);
			if (text == "VO_ICC08_LichKing_Male_Human_Turn6_07")
			{
				yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Turn6_07.prefab:026da99d6a478b84ebed0b175a8d0827", 2.5f);
				yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Turn6_08.prefab:172183a2e12a5fe4ea780fc4ec93fec3", 2.5f);
			}
			else
			{
				yield return base.PlayLineOnlyOnce(enemyActor, text, 2.5f);
			}
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x060035AE RID: 13742 RVA: 0x00111722 File Offset: 0x0010F922
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		List<Actor> trappedSoulActors;
		string randomGhostLine;
		switch (missionEvent)
		{
		case 2:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_GenericStart_03.prefab:46e864c0ffc68d349bfd31e4b4c15a08", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 3:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Hunter_01.prefab:e2d9e2c74bf2a854fabd7324492aa47d", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 4:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Mage_01.prefab:57e937f989d277e41b0e47890e7b47fb", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 5:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Paladin_01.prefab:34b0a3dcd009efa45b06359422c8b368", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 6:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Priest_01.prefab:3d968d835dffada4383d11d82fd47e0b", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 7:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Rogue_01.prefab:4138e64fc05f5114ba2b34540f0a3ec5", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 8:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Shaman_01.prefab:7003d55c6554cca48a55c71490e2fa71", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 9:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Warlock_01.prefab:0c577be64f38882468d65fd25d51d737", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 10:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Warrior_01.prefab:4e5cf13fe9eed3c41a24e254d9b27478", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		default:
			switch (missionEvent)
			{
			case 101:
				yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_EndOfTurn4_01.prefab:6574626660e288f4eb796194bf6fea45", 2.5f);
				break;
			case 102:
			case 104:
			case 106:
				break;
			case 103:
				if (this.m_GhostLines.Count != 0)
				{
					trappedSoulActors = this.GetBossTrappedSoulActors();
					if (trappedSoulActors.Count != 0)
					{
						randomGhostLine = this.m_GhostLines[UnityEngine.Random.Range(0, this.m_GhostLines.Count)];
						trappedSoulActors.Sort(delegate(Actor x, Actor y)
						{
							if (UnityEngine.Random.value >= 0.5f)
							{
								return 1;
							}
							return -1;
						});
						GameState.Get().SetBusy(true);
						if (randomGhostLine == "VO_ICCA08_033_Male_HumanGhost_Flavor_03.prefab:ad2f6e62803a52b498446ccb3cbdc89e" && trappedSoulActors.Count >= 3)
						{
							yield return base.PlayLineOnlyOnce(trappedSoulActors[0], "VO_ICCA08_033_Male_HumanGhost_Flavor_03.prefab:ad2f6e62803a52b498446ccb3cbdc89e", 2.5f);
							yield return base.PlayLineOnlyOnce(trappedSoulActors[1], "VO_ICCA08_033_Male_HumanGhost_Flavor_04.prefab:bb379b9e4548b4a4084ca109875ebabf", 2.5f);
							yield return base.PlayLineOnlyOnce(trappedSoulActors[2], "VO_ICCA08_033_Male_HumanGhost_Flavor_05.prefab:1e614b8b56fe881468603687a4afe437", 2.5f);
							this.m_GhostLines.Remove(randomGhostLine);
						}
						if (randomGhostLine == "VO_ICCA08_033_Male_HumanGhost_Flavor_01.prefab:889d1d45b71ee164caf960cae87e9835" || randomGhostLine == "VO_ICCA08_033_Male_HumanGhost_Flavor_02.prefab:f83e29d3903ede54aa72ff74d71ea245")
						{
							yield return base.PlayLineOnlyOnce(trappedSoulActors[0], randomGhostLine, 2.5f);
							this.m_GhostLines.Remove(randomGhostLine);
						}
						GameState.Get().SetBusy(false);
					}
				}
				break;
			case 105:
				yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_PhaseTransition_02.prefab:7544481ca8382ad4a9969284813a029d", 2.5f);
				break;
			case 107:
				yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Wounded_02.prefab:c81d4dcd56e1b2e45a91576dd48688bd", 2.5f);
				break;
			case 108:
				yield return base.PlayBossLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Death_01.prefab:fca6e9173db669c49adcc3bb0a7a9cdc", 2.5f);
				break;
			case 109:
				if (!this.m_playedLines.Contains("109"))
				{
					this.m_playedLines.Add("109");
					GameState.Get().SetBusy(true);
					if (UnityEngine.Random.Range(0, 10) == 0)
					{
						yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Frostmourne_02.prefab:ae04eda3fb2d4124d9f669ff0a8900a4", 2.5f);
					}
					else
					{
						yield return base.PlayLineOnlyOnce(enemyActor, this.m_FrostmournePlayLines[UnityEngine.Random.Range(0, this.m_FrostmournePlayLines.Count)], 2.5f);
					}
					GameState.Get().SetBusy(false);
				}
				break;
			case 110:
				yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Phase03_01.prefab:a3a557c5ae7daff4abb5d9cdb7df9fc6", 2.5f);
				break;
			case 111:
				yield return new WaitForSeconds(0.3f);
				yield return base.PlayBossLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Mograine_01.prefab:f6e5161911c3d4e4da50ae3106c41f98", 2.5f);
				break;
			case 112:
				if (this.isMissingeLastHorseMen())
				{
					yield return base.PlayBossLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Horsemen_01.prefab:318348d96afbbd149896e6d32ae03e5a", 2.5f);
				}
				break;
			case 113:
				yield return base.PlayBossLine(enemyActor, "VO_ICC08_LichKing_Male_Human_IndestructableWeapon_01.prefab:410f9f1ef41f1bd45b8565fc191a9443", 2.5f);
				break;
			case 114:
				yield return base.PlayBossLine(enemyActor, "VO_ICC08_LichKing_Male_Human_MageSpellReflect_02.prefab:47512997dd73e0943a941cbf069d7ce8", 2.5f);
				break;
			case 115:
				yield return new WaitForSeconds(1f);
				yield return base.PlayBossLine(enemyActor, "VO_ICC08_LichKing_Male_Human_MageSpellReflect_01.prefab:4578c14e2604b9140b4c4259a384afe5", 2.5f);
				break;
			default:
				if (missionEvent != 200)
				{
				}
				break;
			}
			break;
		}
		trappedSoulActors = null;
		randomGhostLine = null;
		yield break;
	}

	// Token: 0x060035AF RID: 13743 RVA: 0x00111738 File Offset: 0x0010F938
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 1655527787U)
		{
			if (num <= 564396772U)
			{
				if (num <= 363065344U)
				{
					if (num != 202362166U)
					{
						if (num != 363065344U)
						{
							goto IL_698;
						}
						if (!(cardId == "ICC_858"))
						{
							goto IL_698;
						}
					}
					else
					{
						if (!(cardId == "AT_036"))
						{
							goto IL_698;
						}
						yield return base.PlayEasterEggLine(enemyActor, "VO_LichKing_Male_Human_ResponseAnubaArak_01.prefab:f07f56be69a5482478280e7e5278bf6e", 2.5f);
						goto IL_698;
					}
				}
				else if (num != 431161558U)
				{
					if (num != 564396772U)
					{
						goto IL_698;
					}
					if (!(cardId == "ICC_854"))
					{
						goto IL_698;
					}
					yield return base.PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Arfas_01.prefab:c8c81d9c0e52c5b4fbe75016840f3582", 2.5f);
					goto IL_698;
				}
				else
				{
					if (!(cardId == "ICC_838"))
					{
						goto IL_698;
					}
					yield return base.PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Sindragosa_01.prefab:3f87312e12134dc4b9638d0ff88b3acd", 2.5f);
					goto IL_698;
				}
			}
			else if (num <= 1340049765U)
			{
				if (num != 835677842U)
				{
					if (num != 1340049765U)
					{
						goto IL_698;
					}
					if (!(cardId == "GVG_063"))
					{
						goto IL_698;
					}
				}
				else
				{
					if (!(cardId == "ICC_023"))
					{
						goto IL_698;
					}
					yield return base.PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_LilLich_01.prefab:26542e402ab20f34d9aeecc6d684bd14", 2.5f);
					goto IL_698;
				}
			}
			else if (num != 1440752425U)
			{
				if (num != 1655527787U)
				{
					goto IL_698;
				}
				if (!(cardId == "ICC_204"))
				{
					goto IL_698;
				}
				yield return base.PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Putricide_01.prefab:4dcd0d68d2594344a999c0011b83e880", 2.5f);
				goto IL_698;
			}
			else
			{
				if (!(cardId == "EX1_559"))
				{
					goto IL_698;
				}
				yield return base.PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Archmage Antonidas_01.prefab:859e44d75d2591f4bafa299e89320a77", 2.5f);
				goto IL_698;
			}
			yield return base.PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Bolvar_01.prefab:9b9665e3f305ec34bbf6d482d0b8c85c", 2.5f);
		}
		else if (num <= 2811172715U)
		{
			if (num <= 2566338463U)
			{
				if (num != 1780024521U)
				{
					if (num == 2566338463U)
					{
						if (cardId == "ICCA08_029")
						{
							yield return base.PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_LichKingMuted_01.prefab:c50c633fe82ad47409a14886eea1c4df", 2.5f);
						}
					}
				}
				else if (cardId == "ICC_314")
				{
					yield return base.PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_LichKing_01.prefab:e721715f493c5c144a674274e3a835b7", 2.5f);
					yield return base.PlayEasterEggLine(base.GetLichKingFriendlyMinion(), "VO_ICC08_LichKing_Male_Human_LichKing_02.prefab:b6789fd334aa9cb458511335ef404341", 2.5f);
				}
			}
			else if (num != 2728856718U)
			{
				if (num == 2811172715U)
				{
					if (cardId == "EX1_016")
					{
						yield return base.PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Sylvanas_01.prefab:4589860c323b62548b030d542e0da276", 2.5f);
					}
				}
			}
			else if (cardId == "ICC_841")
			{
				yield return base.PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Lanathel_01.prefab:7eb2a2125339ded459279f66de43ea76", 2.5f);
			}
		}
		else if (num <= 3309810497U)
		{
			if (num != 3139014260U)
			{
				if (num == 3309810497U)
				{
					if (cardId == "FP1_013")
					{
						yield return base.PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Kelthuzad_01.prefab:3ddade3d8c1f22d4ca63960d8f4b4b05", 2.5f);
					}
				}
			}
			else if (cardId == "ICC_902")
			{
				yield return base.PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_PowerReplace_01.prefab:de29c2c3a08b3314887c3f4ab1fd5942", 2.5f);
			}
		}
		else if (num != 3587559163U)
		{
			if (num == 4051462100U)
			{
				if (cardId == "EX1_383")
				{
					yield return base.PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Tirion_01.prefab:c20cf5d8a42d9f34faddc72368c39bef", 2.5f);
				}
			}
		}
		else if (cardId == "GVG_021")
		{
			yield return base.PlayEasterEggLine(enemyActor, "VO_LichKing_Male_Human_ResponseMalganis_01.prefab:75606e89824ea54418eca3c2b75abfd0", 2.5f);
		}
		IL_698:
		yield return base.IfPlayerPlaysDKHeroVO(entity, enemyActor, "VO_ICC08_LichKing_Male_Human_TransformDK_01.prefab:a2c40f0880b9cbd4784f39d745d0d50e");
		yield break;
	}

	// Token: 0x060035B0 RID: 13744 RVA: 0x0011174E File Offset: 0x0010F94E
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			string soundPath = "VO_ICC08_LichKing_Male_Human_Lose_03.prefab:41cdbee4c68e2a84b96710a94b454ea1";
			if (!NotificationManager.Get().HasSoundPlayedThisSession(soundPath))
			{
				yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("LichKing_Banner_Quote.prefab:d42a8f4f69919f449b3dd8ebceaf2a3c", soundPath, 0f, true, false));
			}
		}
		yield break;
	}

	// Token: 0x060035B1 RID: 13745 RVA: 0x00111764 File Offset: 0x0010F964
	private void EnableFullScreenBlur()
	{
		if (this.m_blurEnabled)
		{
			return;
		}
		this.m_blurEnabled = true;
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.SetBlurAmount(0.3f);
		fullScreenFXMgr.SetBlurBrightness(0.4f);
		fullScreenFXMgr.SetBlurDesaturation(0.5f);
		fullScreenFXMgr.Blur(1f, 0.5f, iTween.EaseType.easeOutCirc, null);
	}

	// Token: 0x060035B2 RID: 13746 RVA: 0x001117B9 File Offset: 0x0010F9B9
	private void DisableFullScreenBlur()
	{
		if (!this.m_blurEnabled)
		{
			return;
		}
		this.m_blurEnabled = false;
		FullScreenFXMgr.Get().StopBlur(0.5f, iTween.EaseType.easeOutCirc, null, false);
	}

	// Token: 0x060035B3 RID: 13747 RVA: 0x001117E0 File Offset: 0x0010F9E0
	private Actor GetActorbyCardId(string cardId)
	{
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		foreach (Card card in opposingSidePlayer.GetBattlefieldZone().GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetControllerId() == opposingSidePlayer.GetPlayerId() && entity.GetCardId() == cardId)
			{
				return entity.GetCard().GetActor();
			}
		}
		return null;
	}

	// Token: 0x060035B4 RID: 13748 RVA: 0x00111870 File Offset: 0x0010FA70
	private List<Actor> GetBossTrappedSoulActors()
	{
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		List<Actor> list = new List<Actor>();
		foreach (Card card in opposingSidePlayer.GetBattlefieldZone().GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetControllerId() == opposingSidePlayer.GetPlayerId() && entity.GetCardId() == "ICCA08_033")
			{
				list.Add(entity.GetCard().GetActor());
			}
		}
		return list;
	}

	// Token: 0x060035B5 RID: 13749 RVA: 0x0011190C File Offset: 0x0010FB0C
	private bool isMissingeLastHorseMen()
	{
		List<string> list = new List<string>
		{
			"ICC_829t5",
			"ICC_829t2",
			"ICC_829t3",
			"ICC_829t4"
		};
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		foreach (Card card in opposingSidePlayer.GetBattlefieldZone().GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetControllerId() == opposingSidePlayer.GetPlayerId() && list.Contains(entity.GetCardId()))
			{
				list.Remove(entity.GetCardId());
			}
		}
		return list.Count == 1;
	}

	// Token: 0x060035B6 RID: 13750 RVA: 0x001119D4 File Offset: 0x0010FBD4
	public IEnumerator PlayTirionVictoryVO()
	{
		bool finished = false;
		Notification notification = NotificationManager.Get().CreateBigCharacterQuoteWithText("Tirion_BigQuote.prefab:878fcebc1cddaf24f828c44edb07f7f8", NotificationManager.DEFAULT_CHARACTER_POS, "VO_ICC08_Tirion_Male_Human_Win_01.prefab:e97467bae07fb0340a24b6772b048608", GameStrings.Get("VO_ICC08_Tirion_Male_Human_Win_01"), 2f, null, false, Notification.SpeechBubbleDirection.BottomLeft, false, false);
		notification.OnFinishDeathState = (Action<int>)Delegate.Combine(notification.OnFinishDeathState, new Action<int>(delegate(int groupId)
		{
			finished = true;
		}));
		while (!finished)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x060035B7 RID: 13751 RVA: 0x001119DC File Offset: 0x0010FBDC
	public IEnumerator PlayFriendlyHeroVictoryVO(Actor actor, AudioSource preTransformationAudio, AudioSource midTransformationAudio)
	{
		TAG_CLASS @class = GameState.Get().GetFriendlySidePlayer().GetStartingHero().GetClass();
		string[] voLines;
		if (this.m_HeroVictoryLines.TryGetValue(@class, out voLines))
		{
			float scale = UniversalInputManager.UsePhoneUI ? 0.5f : 0.75f;
			if (preTransformationAudio != null)
			{
				SoundManager.Get().Play(preTransformationAudio, null, null, null);
			}
			yield return base.PlaySoundAndBlockSpeech(voLines[0], Notification.SpeechBubbleDirection.BottomLeft, actor, 2f, 1f, false, false, scale);
			if (midTransformationAudio != null)
			{
				SoundManager.Get().Play(midTransformationAudio, null, null, null);
			}
			yield return new WaitForSeconds(0.5f);
			yield return base.PlaySoundAndBlockSpeech(voLines[1], Notification.SpeechBubbleDirection.BottomLeft, actor, 2f, 1f, false, false, scale);
		}
		yield break;
	}

	// Token: 0x04001CFA RID: 7418
	private static Map<GameEntityOption, bool> s_booleanOptions = ICC_08_Finale.InitBooleanOptions();

	// Token: 0x04001CFB RID: 7419
	private static Map<GameEntityOption, string> s_stringOptions = ICC_08_Finale.InitStringOptions();

	// Token: 0x04001CFC RID: 7420
	private bool m_blurEnabled;

	// Token: 0x04001CFD RID: 7421
	private Vector3 m_startingCameraPosition;

	// Token: 0x04001CFE RID: 7422
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001CFF RID: 7423
	private MissionEntity.EmoteResponseGroup m_mutedResponses;

	// Token: 0x04001D00 RID: 7424
	private List<string> m_LichKingIntroLines = new List<string>
	{
		"VO_ICC08_LichKing_Male_Human_Intro_01.prefab:b24caf7e6f9af4d439e08483dd182a90",
		"VO_ICC08_LichKing_Male_Human_Intro_03.prefab:228920e5af99a9c4e910eec87c5fb60a",
		"VO_ICC08_LichKing_Male_Human_Intro_04.prefab:731de36648f4af2428a8365544d8eaec",
		"VO_ICC08_LichKing_Male_Human_Intro_06.prefab:2b6211261beb98849b76625e5c8a8dad",
		"VO_ICC08_LichKing_Male_Human_Intro_07.prefab:f2607c560856c62429f4601ea72e60a1",
		"VO_ICC08_LichKing_Male_Human_Intro_08.prefab:262b262a4ed8eef4f9f003df8625b399",
		"VO_ICC08_LichKing_Male_Human_Intro_09.prefab:009c57cb5531f924cb50cc5f0065a435"
	};

	// Token: 0x04001D01 RID: 7425
	private List<string> m_LichKingRandomTurnLines = new List<string>
	{
		"VO_ICC08_LichKing_Male_Human_Turn6_01.prefab:9625ec58f43d07a4fa776a1fd28cd03b",
		"VO_ICC08_LichKing_Male_Human_Turn6_03.prefab:0849cc14d7accd54bab51ec07fac1c0d",
		"VO_ICC08_LichKing_Male_Human_Turn6_04.prefab:894659956f1c5984daa739c729ca95a8",
		"VO_ICC08_LichKing_Male_Human_Turn6_05.prefab:bc5520cf2889ce843836a9f428409301",
		"VO_ICC08_LichKing_Male_Human_Turn6_06.prefab:bb4b842360f2b384a8c731a33965f15d",
		"VO_ICC08_LichKing_Male_Human_Turn6_07.prefab:026da99d6a478b84ebed0b175a8d0827"
	};

	// Token: 0x04001D02 RID: 7426
	private List<string> m_FrostmournePlayLines = new List<string>
	{
		"VO_ICC08_LichKing_Male_Human_Frostmourne_01.prefab:10e7732d07bd84f4293dd5836fdc5b9f",
		"VO_ICC08_LichKing_Male_Human_Frostmourne_07.prefab:799e3d3b8fa353c40ac5f2507edfe4f7"
	};

	// Token: 0x04001D03 RID: 7427
	private List<string> m_GhostLines = new List<string>
	{
		"VO_ICCA08_033_Male_HumanGhost_Flavor_01.prefab:889d1d45b71ee164caf960cae87e9835",
		"VO_ICCA08_033_Male_HumanGhost_Flavor_02.prefab:f83e29d3903ede54aa72ff74d71ea245",
		"VO_ICCA08_033_Male_HumanGhost_Flavor_03.prefab:ad2f6e62803a52b498446ccb3cbdc89e"
	};

	// Token: 0x04001D04 RID: 7428
	private Map<TAG_CLASS, string[]> m_HeroVictoryLines = new Map<TAG_CLASS, string[]>
	{
		{
			TAG_CLASS.DRUID,
			new string[]
			{
				"VO_ICC08_Malfurion_Male_NightElf_WinA_01.prefab:8a6417a39223ae44683deb0fef141b2d",
				"VO_ICC08_Malfurion_Male_NightElf_WinB_01.prefab:43a8eb73e5fbb614b88e66cb9e0ba74e"
			}
		},
		{
			TAG_CLASS.HUNTER,
			new string[]
			{
				"VO_ICC08_Rexxar_Male_Orc_WinA_01.prefab:d518bccb3fa3efc41ab6922235160576",
				"VO_ICC08_Rexxar_Male_Orc_WinB_01.prefab:000fe9a4cb6ed8e49a90ea02053ee0c1"
			}
		},
		{
			TAG_CLASS.MAGE,
			new string[]
			{
				"VO_ICC08_Jaina_Female_Human_WinA_01.prefab:6d61d1b95a6861b4e9718b6d634bb087",
				"VO_ICC08_Jaina_Female_Human_WinB_01.prefab:1e7c37f8dde322441aa7976d9f755ca8"
			}
		},
		{
			TAG_CLASS.PALADIN,
			new string[]
			{
				"VO_ICC08_Uther_Male_Human_WinA_01.prefab:ab29d4c60fd014f49829de250dd1d080",
				"VO_ICC08_Uther_Male_Human_WinB_01.prefab:be07d20738f843b4ea6feb6c166186df"
			}
		},
		{
			TAG_CLASS.PRIEST,
			new string[]
			{
				"VO_ICC08_Anduin_Male_Human_WinA_01.prefab:0adfd8d9372ba384aaea44aead60786d",
				"VO_ICC08_Anduin_Male_Human_WinB_01.prefab:d6f283fa97b38b84e8c26ef93886a59a"
			}
		},
		{
			TAG_CLASS.ROGUE,
			new string[]
			{
				"VO_ICC08_Valeera_Female_Human_WinA_01.prefab:8251a1520d5802845a46a74bc0bb2c4d",
				"VO_ICC08_Valeera_Female_Human_WinB_01.prefab:0a920fa6de7e5874e80eb4545bb3e992"
			}
		},
		{
			TAG_CLASS.SHAMAN,
			new string[]
			{
				"VO_ICC08_Thrall_Male_Orc_WinA_01.prefab:03463d35eb7e36c4b9612f3ddff0a339",
				"VO_ICC08_Thrall_Male_Orc_WinB_01.prefab:36cc93a5bddc43d4e8bec4f68740ce11"
			}
		},
		{
			TAG_CLASS.WARLOCK,
			new string[]
			{
				"VO_ICC08_Guldan_Male_Orc_WinA_01.prefab:4c26bd9f011c7c9488eab431dc98b3e3",
				"VO_ICC08_Guldan_Male_Orc_WinB_01.prefab:0a5cabe002b4eb34da13390b0334f3e4"
			}
		},
		{
			TAG_CLASS.WARRIOR,
			new string[]
			{
				"VO_ICC08_Garrosh_Male_Orc_WinA_01.prefab:a5e593c457d4a534ba92e4c9a6bb43d1",
				"VO_ICC08_Garrosh_Male_Orc_WinB_01.prefab:ce2eda768ba0a8540b4422e1c7dfba23"
			}
		}
	};

	// Token: 0x04001D05 RID: 7429
	private int PRIEST_GREETINGS_DISABLED_EMOTE_OVERRIDE_ID = 7;
}
