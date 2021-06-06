using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICC_08_Finale : ICC_MissionEntity
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	private bool m_blurEnabled;

	private Vector3 m_startingCameraPosition;

	private HashSet<string> m_playedLines = new HashSet<string>();

	private EmoteResponseGroup m_mutedResponses;

	private List<string> m_LichKingIntroLines = new List<string> { "VO_ICC08_LichKing_Male_Human_Intro_01.prefab:b24caf7e6f9af4d439e08483dd182a90", "VO_ICC08_LichKing_Male_Human_Intro_03.prefab:228920e5af99a9c4e910eec87c5fb60a", "VO_ICC08_LichKing_Male_Human_Intro_04.prefab:731de36648f4af2428a8365544d8eaec", "VO_ICC08_LichKing_Male_Human_Intro_06.prefab:2b6211261beb98849b76625e5c8a8dad", "VO_ICC08_LichKing_Male_Human_Intro_07.prefab:f2607c560856c62429f4601ea72e60a1", "VO_ICC08_LichKing_Male_Human_Intro_08.prefab:262b262a4ed8eef4f9f003df8625b399", "VO_ICC08_LichKing_Male_Human_Intro_09.prefab:009c57cb5531f924cb50cc5f0065a435" };

	private List<string> m_LichKingRandomTurnLines = new List<string> { "VO_ICC08_LichKing_Male_Human_Turn6_01.prefab:9625ec58f43d07a4fa776a1fd28cd03b", "VO_ICC08_LichKing_Male_Human_Turn6_03.prefab:0849cc14d7accd54bab51ec07fac1c0d", "VO_ICC08_LichKing_Male_Human_Turn6_04.prefab:894659956f1c5984daa739c729ca95a8", "VO_ICC08_LichKing_Male_Human_Turn6_05.prefab:bc5520cf2889ce843836a9f428409301", "VO_ICC08_LichKing_Male_Human_Turn6_06.prefab:bb4b842360f2b384a8c731a33965f15d", "VO_ICC08_LichKing_Male_Human_Turn6_07.prefab:026da99d6a478b84ebed0b175a8d0827" };

	private List<string> m_FrostmournePlayLines = new List<string> { "VO_ICC08_LichKing_Male_Human_Frostmourne_01.prefab:10e7732d07bd84f4293dd5836fdc5b9f", "VO_ICC08_LichKing_Male_Human_Frostmourne_07.prefab:799e3d3b8fa353c40ac5f2507edfe4f7" };

	private List<string> m_GhostLines = new List<string> { "VO_ICCA08_033_Male_HumanGhost_Flavor_01.prefab:889d1d45b71ee164caf960cae87e9835", "VO_ICCA08_033_Male_HumanGhost_Flavor_02.prefab:f83e29d3903ede54aa72ff74d71ea245", "VO_ICCA08_033_Male_HumanGhost_Flavor_03.prefab:ad2f6e62803a52b498446ccb3cbdc89e" };

	private Map<TAG_CLASS, string[]> m_HeroVictoryLines = new Map<TAG_CLASS, string[]>
	{
		{
			TAG_CLASS.DRUID,
			new string[2] { "VO_ICC08_Malfurion_Male_NightElf_WinA_01.prefab:8a6417a39223ae44683deb0fef141b2d", "VO_ICC08_Malfurion_Male_NightElf_WinB_01.prefab:43a8eb73e5fbb614b88e66cb9e0ba74e" }
		},
		{
			TAG_CLASS.HUNTER,
			new string[2] { "VO_ICC08_Rexxar_Male_Orc_WinA_01.prefab:d518bccb3fa3efc41ab6922235160576", "VO_ICC08_Rexxar_Male_Orc_WinB_01.prefab:000fe9a4cb6ed8e49a90ea02053ee0c1" }
		},
		{
			TAG_CLASS.MAGE,
			new string[2] { "VO_ICC08_Jaina_Female_Human_WinA_01.prefab:6d61d1b95a6861b4e9718b6d634bb087", "VO_ICC08_Jaina_Female_Human_WinB_01.prefab:1e7c37f8dde322441aa7976d9f755ca8" }
		},
		{
			TAG_CLASS.PALADIN,
			new string[2] { "VO_ICC08_Uther_Male_Human_WinA_01.prefab:ab29d4c60fd014f49829de250dd1d080", "VO_ICC08_Uther_Male_Human_WinB_01.prefab:be07d20738f843b4ea6feb6c166186df" }
		},
		{
			TAG_CLASS.PRIEST,
			new string[2] { "VO_ICC08_Anduin_Male_Human_WinA_01.prefab:0adfd8d9372ba384aaea44aead60786d", "VO_ICC08_Anduin_Male_Human_WinB_01.prefab:d6f283fa97b38b84e8c26ef93886a59a" }
		},
		{
			TAG_CLASS.ROGUE,
			new string[2] { "VO_ICC08_Valeera_Female_Human_WinA_01.prefab:8251a1520d5802845a46a74bc0bb2c4d", "VO_ICC08_Valeera_Female_Human_WinB_01.prefab:0a920fa6de7e5874e80eb4545bb3e992" }
		},
		{
			TAG_CLASS.SHAMAN,
			new string[2] { "VO_ICC08_Thrall_Male_Orc_WinA_01.prefab:03463d35eb7e36c4b9612f3ddff0a339", "VO_ICC08_Thrall_Male_Orc_WinB_01.prefab:36cc93a5bddc43d4e8bec4f68740ce11" }
		},
		{
			TAG_CLASS.WARLOCK,
			new string[2] { "VO_ICC08_Guldan_Male_Orc_WinA_01.prefab:4c26bd9f011c7c9488eab431dc98b3e3", "VO_ICC08_Guldan_Male_Orc_WinB_01.prefab:0a5cabe002b4eb34da13390b0334f3e4" }
		},
		{
			TAG_CLASS.WARRIOR,
			new string[2] { "VO_ICC08_Garrosh_Male_Orc_WinA_01.prefab:a5e593c457d4a534ba92e4c9a6bb43d1", "VO_ICC08_Garrosh_Male_Orc_WinB_01.prefab:ce2eda768ba0a8540b4422e1c7dfba23" }
		}
	};

	private int PRIEST_GREETINGS_DISABLED_EMOTE_OVERRIDE_ID = 7;

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>();
	}

	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string> { 
		{
			GameEntityOption.VICTORY_SCREEN_PREFAB_PATH,
			"VictoryTwoScoop_ICCFinale.prefab:f26ea23934d4ea845911dd26cf342b10"
		} };
	}

	public ICC_08_Finale()
	{
		m_gameOptions.AddOptions(s_booleanOptions, s_stringOptions);
	}

	public override void PreloadAssets()
	{
		foreach (string item in new List<string>
		{
			"VO_ICC08_LichKing_Male_Human_Intro_01.prefab:b24caf7e6f9af4d439e08483dd182a90", "VO_ICC08_LichKing_Male_Human_Intro_03.prefab:228920e5af99a9c4e910eec87c5fb60a", "VO_ICC08_LichKing_Male_Human_Intro_04.prefab:731de36648f4af2428a8365544d8eaec", "VO_ICC08_LichKing_Male_Human_Intro_06.prefab:2b6211261beb98849b76625e5c8a8dad", "VO_ICC08_LichKing_Male_Human_Intro_07.prefab:f2607c560856c62429f4601ea72e60a1", "VO_ICC08_LichKing_Male_Human_Intro_08.prefab:262b262a4ed8eef4f9f003df8625b399", "VO_ICC08_LichKing_Male_Human_Intro_09.prefab:009c57cb5531f924cb50cc5f0065a435", "VO_ICC08_LichKing_Male_Human_Warrior_01.prefab:4e5cf13fe9eed3c41a24e254d9b27478", "VO_ICC08_LichKing_Male_Human_Shaman_01.prefab:7003d55c6554cca48a55c71490e2fa71", "VO_ICC08_LichKing_Male_Human_Rogue_01.prefab:4138e64fc05f5114ba2b34540f0a3ec5",
			"VO_ICC08_LichKing_Male_Human_Paladin_01.prefab:34b0a3dcd009efa45b06359422c8b368", "VO_ICC08_LichKing_Male_Human_Hunter_01.prefab:e2d9e2c74bf2a854fabd7324492aa47d", "VO_ICC08_LichKing_Male_Human_GenericStart_03.prefab:46e864c0ffc68d349bfd31e4b4c15a08", "VO_ICC08_LichKing_Male_Human_Warlock_01.prefab:0c577be64f38882468d65fd25d51d737", "VO_ICC08_LichKing_Male_Human_Mage_01.prefab:57e937f989d277e41b0e47890e7b47fb", "VO_ICC08_LichKing_Male_Human_Priest_01.prefab:3d968d835dffada4383d11d82fd47e0b", "VO_ICC08_LichKing_Male_Human_EndOfTurn4_01.prefab:6574626660e288f4eb796194bf6fea45", "VO_ICC08_LichKing_Male_Human_Turn6_01.prefab:9625ec58f43d07a4fa776a1fd28cd03b", "VO_ICC08_LichKing_Male_Human_Turn6_03.prefab:0849cc14d7accd54bab51ec07fac1c0d", "VO_ICC08_LichKing_Male_Human_Turn6_04.prefab:894659956f1c5984daa739c729ca95a8",
			"VO_ICC08_LichKing_Male_Human_Turn6_05.prefab:bc5520cf2889ce843836a9f428409301", "VO_ICC08_LichKing_Male_Human_Turn6_06.prefab:bb4b842360f2b384a8c731a33965f15d", "VO_ICC08_LichKing_Male_Human_Turn6_07.prefab:026da99d6a478b84ebed0b175a8d0827", "VO_ICC08_LichKing_Male_Human_Turn6_08.prefab:172183a2e12a5fe4ea780fc4ec93fec3", "VO_ICC08_LichKing_Male_Human_Frostmourne_01.prefab:10e7732d07bd84f4293dd5836fdc5b9f", "VO_ICC08_LichKing_Male_Human_Frostmourne_02.prefab:ae04eda3fb2d4124d9f669ff0a8900a4", "VO_ICC08_LichKing_Male_Human_Frostmourne_07.prefab:799e3d3b8fa353c40ac5f2507edfe4f7", "VO_ICC08_LichKing_Male_Human_PhaseTransition_02.prefab:7544481ca8382ad4a9969284813a029d", "VO_ICC08_LichKing_Male_Human_Phase03_01.prefab:a3a557c5ae7daff4abb5d9cdb7df9fc6", "VO_ICC08_LichKing_Male_Human_Wounded_02.prefab:c81d4dcd56e1b2e45a91576dd48688bd",
			"VO_ICC08_LichKing_Male_Human_Death_01.prefab:fca6e9173db669c49adcc3bb0a7a9cdc", "VO_ICC08_LichKing_Male_Human_Lose_03.prefab:41cdbee4c68e2a84b96710a94b454ea1", "VO_ICC08_Tirion_Male_Human_Win_01.prefab:e97467bae07fb0340a24b6772b048608", "VO_ICC08_LichKing_Male_Human_EmoteResponse_01.prefab:efd4eb3c650804d409009435dae67ac7", "VO_ICC08_Anduin_Male_Human_Muted_13.prefab:40469f3ca344fe44daebc0414141f034", "VO_ICC08_Anduin_Male_Human_Muted_02.prefab:6da6dabe0e1a9104fa0eec237a588772", "VO_ICC08_Anduin_Male_Human_Muted_11.prefab:022a5203f3c7c0a4aba9511627bd9bdf", "VO_ICC08_Anduin_Male_Human_Muted_06.prefab:8fee035bbf3c2444e9a46663f460245e", "VO_ICC08_Anduin_Male_Human_Muted_03.prefab:bcb66402843029e43b70321d8a603dff", "VO_ICC08_Anduin_Male_Human_Muted_07.prefab:41b07b89cf5261e4eb19cb1fde0ec7e8",
			"VO_ICC08_LichKing_Male_Human_LichKingMuted_01.prefab:c50c633fe82ad47409a14886eea1c4df", "VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_01.prefab:0a2f09b8e8899954d8804606243f11ed", "VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_02.prefab:5ecf8b80eeed9874bac3a12448bc418f", "VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_03.prefab:fa73d64fd0067af4889a4039a5524432", "VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_04.prefab:b13ebe4b70cacb34992e423377da3d9e", "VO_ICC08_LichKing_Male_Human_MutedResponse_01.prefab:0341324ad026b094fa7cfc23bf4f2b3e", "VO_ICC08_LichKing_Male_Human_MutedResponse_02.prefab:1190ddcc35269db4bb9992be86511742", "VO_ICC08_LichKing_Male_Human_MutedResponse_03.prefab:df170463932e5704d87f562b87b59eb5", "VO_ICC08_LichKing_Male_Human_MutedResponse_04.prefab:0767c71c4fc8a59499f9b4a9c9ba5946", "VO_ICC08_LichKing_Male_Human_MutedResponse_05.prefab:e8d6b6429763ccc47ab58a90a46ccae2",
			"VO_ICC08_LichKing_Male_Human_MutedResponse_06.prefab:d943ed1a74cb28e4f9af97fa163f0f90", "VO_ICC08_LichKing_Male_Human_LichKing_01.prefab:e721715f493c5c144a674274e3a835b7", "VO_ICC08_LichKing_Male_Human_LichKing_02.prefab:b6789fd334aa9cb458511335ef404341", "VO_ICC08_LichKing_Male_Human_TransformDK_01.prefab:a2c40f0880b9cbd4784f39d745d0d50e", "VO_ICC08_LichKing_Male_Human_Putricide_01.prefab:4dcd0d68d2594344a999c0011b83e880", "VO_ICC08_LichKing_Male_Human_Sindragosa_01.prefab:3f87312e12134dc4b9638d0ff88b3acd", "VO_ICC08_LichKing_Male_Human_Lanathel_01.prefab:7eb2a2125339ded459279f66de43ea76", "VO_ICC08_LichKing_Male_Human_Mograine_01.prefab:f6e5161911c3d4e4da50ae3106c41f98", "VO_ICC08_LichKing_Male_Human_Kelthuzad_01.prefab:3ddade3d8c1f22d4ca63960d8f4b4b05", "VO_ICC08_LichKing_Male_Human_Horsemen_01.prefab:318348d96afbbd149896e6d32ae03e5a",
			"VO_ICC08_LichKing_Male_Human_IndestructableWeapon_01.prefab:410f9f1ef41f1bd45b8565fc191a9443", "VO_ICC08_LichKing_Male_Human_Tirion_01.prefab:c20cf5d8a42d9f34faddc72368c39bef", "VO_ICC08_LichKing_Male_Human_Sylvanas_01.prefab:4589860c323b62548b030d542e0da276", "VO_ICC08_LichKing_Male_Human_Bolvar_01.prefab:9b9665e3f305ec34bbf6d482d0b8c85c", "VO_ICC08_LichKing_Male_Human_Archmage Antonidas_01.prefab:859e44d75d2591f4bafa299e89320a77", "VO_ICC08_LichKing_Male_Human_Arfas_01.prefab:c8c81d9c0e52c5b4fbe75016840f3582", "VO_ICC08_LichKing_Male_Human_LilLich_01.prefab:26542e402ab20f34d9aeecc6d684bd14", "VO_ICC08_LichKing_Male_Human_MageSpellReflect_01.prefab:4578c14e2604b9140b4c4259a384afe5", "VO_ICC08_LichKing_Male_Human_MageSpellReflect_02.prefab:47512997dd73e0943a941cbf069d7ce8", "VO_ICC08_LichKing_Male_Human_PowerReplace_01.prefab:de29c2c3a08b3314887c3f4ab1fd5942",
			"VO_LichKing_Male_Human_ResponseAnubaArak_01.prefab:f07f56be69a5482478280e7e5278bf6e", "VO_LichKing_Male_Human_ResponseMalganis_01.prefab:75606e89824ea54418eca3c2b75abfd0", "VO_ICC08_Malfurion_Male_NightElf_WinA_01.prefab:8a6417a39223ae44683deb0fef141b2d", "VO_ICC08_Malfurion_Male_NightElf_WinB_01.prefab:43a8eb73e5fbb614b88e66cb9e0ba74e", "VO_ICC08_Rexxar_Male_Orc_WinA_01.prefab:d518bccb3fa3efc41ab6922235160576", "VO_ICC08_Rexxar_Male_Orc_WinB_01.prefab:000fe9a4cb6ed8e49a90ea02053ee0c1", "VO_ICC08_Jaina_Female_Human_WinA_01.prefab:6d61d1b95a6861b4e9718b6d634bb087", "VO_ICC08_Jaina_Female_Human_WinB_01.prefab:1e7c37f8dde322441aa7976d9f755ca8", "VO_ICC08_Uther_Male_Human_WinA_01.prefab:ab29d4c60fd014f49829de250dd1d080", "VO_ICC08_Uther_Male_Human_WinB_01.prefab:be07d20738f843b4ea6feb6c166186df",
			"VO_ICC08_Anduin_Male_Human_WinA_01.prefab:0adfd8d9372ba384aaea44aead60786d", "VO_ICC08_Anduin_Male_Human_WinB_01.prefab:d6f283fa97b38b84e8c26ef93886a59a", "VO_ICC08_Valeera_Female_Human_WinA_01.prefab:8251a1520d5802845a46a74bc0bb2c4d", "VO_ICC08_Valeera_Female_Human_WinB_01.prefab:0a920fa6de7e5874e80eb4545bb3e992", "VO_ICC08_Thrall_Male_Orc_WinA_01.prefab:03463d35eb7e36c4b9612f3ddff0a339", "VO_ICC08_Thrall_Male_Orc_WinB_01.prefab:36cc93a5bddc43d4e8bec4f68740ce11", "VO_ICC08_Guldan_Male_Orc_WinA_01.prefab:4c26bd9f011c7c9488eab431dc98b3e3", "VO_ICC08_Guldan_Male_Orc_WinB_01.prefab:0a5cabe002b4eb34da13390b0334f3e4", "VO_ICC08_Garrosh_Male_Orc_WinA_01.prefab:a5e593c457d4a534ba92e4c9a6bb43d1", "VO_ICC08_Garrosh_Male_Orc_WinB_01.prefab:ce2eda768ba0a8540b4422e1c7dfba23",
			"VO_ICCA08_033_Male_HumanGhost_Flavor_01.prefab:889d1d45b71ee164caf960cae87e9835", "VO_ICCA08_033_Male_HumanGhost_Flavor_02.prefab:f83e29d3903ede54aa72ff74d71ea245", "VO_ICCA08_033_Male_HumanGhost_Flavor_03.prefab:ad2f6e62803a52b498446ccb3cbdc89e", "VO_ICCA08_033_Male_HumanGhost_Flavor_04.prefab:bb379b9e4548b4a4084ca109875ebabf", "VO_ICCA08_033_Male_HumanGhost_Flavor_05.prefab:1e614b8b56fe881468603687a4afe437", "VO_ICC08_LichKing_Male_Human_YoungArthas_02.prefab:851d35d99b9a08a4da37334369d85e29"
		})
		{
			PreloadSound(item);
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ICCLichKing);
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		Actor actor = opposingSidePlayer.GetHeroCard().GetActor();
		Entity hero = GameState.Get().GetFriendlySidePlayer().GetHero();
		if (emoteType == EmoteType.START && hero.GetCardId() == "HERO_04b")
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_ICC08_LichKing_Male_Human_YoungArthas_02.prefab:851d35d99b9a08a4da37334369d85e29", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (opposingSidePlayer.GetTag(GAME_TAG.OVERRIDE_EMOTE_0) == PRIEST_GREETINGS_DISABLED_EMOTE_OVERRIDE_ID)
		{
			if (m_mutedResponses.m_triggers.Contains(emoteType))
			{
				PlayNextEmoteResponse(m_mutedResponses, actor);
				CycleNextResponseGroupIndex(m_mutedResponses);
			}
		}
		else
		{
			base.PlayEmoteResponse(emoteType, emoteSpell);
		}
	}

	protected override void InitEmoteResponses()
	{
		string text = m_LichKingIntroLines[UnityEngine.Random.Range(0, m_LichKingIntroLines.Count)];
		m_mutedResponses = new EmoteResponseGroup
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
			m_responses = new List<EmoteResponse>
			{
				new EmoteResponse
				{
					m_soundName = "VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_01.prefab:0a2f09b8e8899954d8804606243f11ed",
					m_stringTag = "VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_01"
				},
				new EmoteResponse
				{
					m_soundName = "VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_02.prefab:5ecf8b80eeed9874bac3a12448bc418f",
					m_stringTag = "VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_02"
				},
				new EmoteResponse
				{
					m_soundName = "VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_03.prefab:fa73d64fd0067af4889a4039a5524432",
					m_stringTag = "VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_03"
				},
				new EmoteResponse
				{
					m_soundName = "VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_04.prefab:b13ebe4b70cacb34992e423377da3d9e",
					m_stringTag = "VO_ICC08_LichKing_Male_Human_LichKingMutedResponse_04"
				}
			}
		};
		m_emoteResponseGroups = new List<EmoteResponseGroup>
		{
			new EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>(MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS),
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_ICC08_LichKing_Male_Human_EmoteResponse_01.prefab:efd4eb3c650804d409009435dae67ac7",
						m_stringTag = "VO_ICC08_LichKing_Male_Human_EmoteResponse_01"
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
						m_soundName = text,
						m_stringTag = new AssetReference(text).GetLegacyAssetName()
					}
				}
			},
			new EmoteResponseGroup
			{
				m_triggers = new List<EmoteType> { EmoteType.DEATH_LINE },
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_ICC08_LichKing_Male_Human_Death_01.prefab:fca6e9173db669c49adcc3bb0a7a9cdc",
						m_stringTag = "VO_ICC08_LichKing_Male_Human_Death_01"
					}
				}
			},
			new EmoteResponseGroup
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
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_ICC08_LichKing_Male_Human_MutedResponse_01.prefab:0341324ad026b094fa7cfc23bf4f2b3e",
						m_stringTag = "VO_ICC08_LichKing_Male_Human_MutedResponse_01"
					},
					new EmoteResponse
					{
						m_soundName = "VO_ICC08_LichKing_Male_Human_MutedResponse_02.prefab:1190ddcc35269db4bb9992be86511742",
						m_stringTag = "VO_ICC08_LichKing_Male_Human_MutedResponse_02"
					},
					new EmoteResponse
					{
						m_soundName = "VO_ICC08_LichKing_Male_Human_MutedResponse_03.prefab:df170463932e5704d87f562b87b59eb5",
						m_stringTag = "VO_ICC08_LichKing_Male_Human_MutedResponse_03"
					},
					new EmoteResponse
					{
						m_soundName = "VO_ICC08_LichKing_Male_Human_MutedResponse_04.prefab:0767c71c4fc8a59499f9b4a9c9ba5946",
						m_stringTag = "VO_ICC08_LichKing_Male_Human_MutedResponse_04"
					},
					new EmoteResponse
					{
						m_soundName = "VO_ICC08_LichKing_Male_Human_MutedResponse_05.prefab:e8d6b6429763ccc47ab58a90a46ccae2",
						m_stringTag = "VO_ICC08_LichKing_Male_Human_MutedResponse_05"
					},
					new EmoteResponse
					{
						m_soundName = "VO_ICC08_LichKing_Male_Human_MutedResponse_06.prefab:d943ed1a74cb28e4f9af97fa163f0f90",
						m_stringTag = "VO_ICC08_LichKing_Male_Human_MutedResponse_06"
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (turn == 6)
		{
			string text = m_LichKingRandomTurnLines[UnityEngine.Random.Range(0, m_LichKingRandomTurnLines.Count)];
			GameState.Get().SetBusy(busy: true);
			if (text == "VO_ICC08_LichKing_Male_Human_Turn6_07")
			{
				yield return PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Turn6_07.prefab:026da99d6a478b84ebed0b175a8d0827");
				yield return PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Turn6_08.prefab:172183a2e12a5fe4ea780fc4ec93fec3");
			}
			else
			{
				yield return PlayLineOnlyOnce(enemyActor, text);
			}
			GameState.Get().SetBusy(busy: false);
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		while (m_enemySpeaking)
		{
			yield return null;
		}
		switch (missionEvent)
		{
		case 2:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_GenericStart_03.prefab:46e864c0ffc68d349bfd31e4b4c15a08");
			GameState.Get().SetBusy(busy: false);
			break;
		case 3:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Hunter_01.prefab:e2d9e2c74bf2a854fabd7324492aa47d");
			GameState.Get().SetBusy(busy: false);
			break;
		case 4:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Mage_01.prefab:57e937f989d277e41b0e47890e7b47fb");
			GameState.Get().SetBusy(busy: false);
			break;
		case 5:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Paladin_01.prefab:34b0a3dcd009efa45b06359422c8b368");
			GameState.Get().SetBusy(busy: false);
			break;
		case 6:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Priest_01.prefab:3d968d835dffada4383d11d82fd47e0b");
			GameState.Get().SetBusy(busy: false);
			break;
		case 7:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Rogue_01.prefab:4138e64fc05f5114ba2b34540f0a3ec5");
			GameState.Get().SetBusy(busy: false);
			break;
		case 8:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Shaman_01.prefab:7003d55c6554cca48a55c71490e2fa71");
			GameState.Get().SetBusy(busy: false);
			break;
		case 9:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Warlock_01.prefab:0c577be64f38882468d65fd25d51d737");
			GameState.Get().SetBusy(busy: false);
			break;
		case 10:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Warrior_01.prefab:4e5cf13fe9eed3c41a24e254d9b27478");
			GameState.Get().SetBusy(busy: false);
			break;
		case 101:
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_EndOfTurn4_01.prefab:6574626660e288f4eb796194bf6fea45");
			break;
		case 103:
		{
			if (m_GhostLines.Count == 0)
			{
				break;
			}
			List<Actor> trappedSoulActors = GetBossTrappedSoulActors();
			if (trappedSoulActors.Count != 0)
			{
				string randomGhostLine = m_GhostLines[UnityEngine.Random.Range(0, m_GhostLines.Count)];
				trappedSoulActors.Sort((Actor x, Actor y) => (!(UnityEngine.Random.value < 0.5f)) ? 1 : (-1));
				GameState.Get().SetBusy(busy: true);
				if (randomGhostLine == "VO_ICCA08_033_Male_HumanGhost_Flavor_03.prefab:ad2f6e62803a52b498446ccb3cbdc89e" && trappedSoulActors.Count >= 3)
				{
					yield return PlayLineOnlyOnce(trappedSoulActors[0], "VO_ICCA08_033_Male_HumanGhost_Flavor_03.prefab:ad2f6e62803a52b498446ccb3cbdc89e");
					yield return PlayLineOnlyOnce(trappedSoulActors[1], "VO_ICCA08_033_Male_HumanGhost_Flavor_04.prefab:bb379b9e4548b4a4084ca109875ebabf");
					yield return PlayLineOnlyOnce(trappedSoulActors[2], "VO_ICCA08_033_Male_HumanGhost_Flavor_05.prefab:1e614b8b56fe881468603687a4afe437");
					m_GhostLines.Remove(randomGhostLine);
				}
				if (randomGhostLine == "VO_ICCA08_033_Male_HumanGhost_Flavor_01.prefab:889d1d45b71ee164caf960cae87e9835" || randomGhostLine == "VO_ICCA08_033_Male_HumanGhost_Flavor_02.prefab:f83e29d3903ede54aa72ff74d71ea245")
				{
					yield return PlayLineOnlyOnce(trappedSoulActors[0], randomGhostLine);
					m_GhostLines.Remove(randomGhostLine);
				}
				GameState.Get().SetBusy(busy: false);
			}
			break;
		}
		case 105:
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_PhaseTransition_02.prefab:7544481ca8382ad4a9969284813a029d");
			break;
		case 107:
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Wounded_02.prefab:c81d4dcd56e1b2e45a91576dd48688bd");
			break;
		case 108:
			yield return PlayBossLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Death_01.prefab:fca6e9173db669c49adcc3bb0a7a9cdc");
			break;
		case 109:
			if (!m_playedLines.Contains("109"))
			{
				m_playedLines.Add("109");
				GameState.Get().SetBusy(busy: true);
				if (UnityEngine.Random.Range(0, 10) == 0)
				{
					yield return PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Frostmourne_02.prefab:ae04eda3fb2d4124d9f669ff0a8900a4");
				}
				else
				{
					yield return PlayLineOnlyOnce(enemyActor, m_FrostmournePlayLines[UnityEngine.Random.Range(0, m_FrostmournePlayLines.Count)]);
				}
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 110:
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC08_LichKing_Male_Human_Phase03_01.prefab:a3a557c5ae7daff4abb5d9cdb7df9fc6");
			break;
		case 111:
			yield return new WaitForSeconds(0.3f);
			yield return PlayBossLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Mograine_01.prefab:f6e5161911c3d4e4da50ae3106c41f98");
			break;
		case 112:
			if (isMissingeLastHorseMen())
			{
				yield return PlayBossLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Horsemen_01.prefab:318348d96afbbd149896e6d32ae03e5a");
			}
			break;
		case 113:
			yield return PlayBossLine(enemyActor, "VO_ICC08_LichKing_Male_Human_IndestructableWeapon_01.prefab:410f9f1ef41f1bd45b8565fc191a9443");
			break;
		case 114:
			yield return PlayBossLine(enemyActor, "VO_ICC08_LichKing_Male_Human_MageSpellReflect_02.prefab:47512997dd73e0943a941cbf069d7ce8");
			break;
		case 115:
			yield return new WaitForSeconds(1f);
			yield return PlayBossLine(enemyActor, "VO_ICC08_LichKing_Male_Human_MageSpellReflect_01.prefab:4578c14e2604b9140b4c4259a384afe5");
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
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "ICC_314":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_LichKing_01.prefab:e721715f493c5c144a674274e3a835b7");
				yield return PlayEasterEggLine(GetLichKingFriendlyMinion(), "VO_ICC08_LichKing_Male_Human_LichKing_02.prefab:b6789fd334aa9cb458511335ef404341");
				break;
			case "ICC_204":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Putricide_01.prefab:4dcd0d68d2594344a999c0011b83e880");
				break;
			case "ICC_838":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Sindragosa_01.prefab:3f87312e12134dc4b9638d0ff88b3acd");
				break;
			case "ICC_841":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Lanathel_01.prefab:7eb2a2125339ded459279f66de43ea76");
				break;
			case "FP1_013":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Kelthuzad_01.prefab:3ddade3d8c1f22d4ca63960d8f4b4b05");
				break;
			case "EX1_383":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Tirion_01.prefab:c20cf5d8a42d9f34faddc72368c39bef");
				break;
			case "EX1_016":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Sylvanas_01.prefab:4589860c323b62548b030d542e0da276");
				break;
			case "ICC_858":
			case "GVG_063":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Bolvar_01.prefab:9b9665e3f305ec34bbf6d482d0b8c85c");
				break;
			case "EX1_559":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Archmage Antonidas_01.prefab:859e44d75d2591f4bafa299e89320a77");
				break;
			case "ICC_854":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_Arfas_01.prefab:c8c81d9c0e52c5b4fbe75016840f3582");
				break;
			case "ICCA08_029":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_LichKingMuted_01.prefab:c50c633fe82ad47409a14886eea1c4df");
				break;
			case "ICC_902":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_PowerReplace_01.prefab:de29c2c3a08b3314887c3f4ab1fd5942");
				break;
			case "ICC_023":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC08_LichKing_Male_Human_LilLich_01.prefab:26542e402ab20f34d9aeecc6d684bd14");
				break;
			case "GVG_021":
				yield return PlayEasterEggLine(enemyActor, "VO_LichKing_Male_Human_ResponseMalganis_01.prefab:75606e89824ea54418eca3c2b75abfd0");
				break;
			case "AT_036":
				yield return PlayEasterEggLine(enemyActor, "VO_LichKing_Male_Human_ResponseAnubaArak_01.prefab:f07f56be69a5482478280e7e5278bf6e");
				break;
			}
			yield return IfPlayerPlaysDKHeroVO(entity, enemyActor, "VO_ICC08_LichKing_Male_Human_TransformDK_01.prefab:a2c40f0880b9cbd4784f39d745d0d50e");
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			string soundPath = "VO_ICC08_LichKing_Male_Human_Lose_03.prefab:41cdbee4c68e2a84b96710a94b454ea1";
			if (!NotificationManager.Get().HasSoundPlayedThisSession(soundPath))
			{
				yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("LichKing_Banner_Quote.prefab:d42a8f4f69919f449b3dd8ebceaf2a3c", soundPath));
			}
		}
	}

	private void EnableFullScreenBlur()
	{
		if (!m_blurEnabled)
		{
			m_blurEnabled = true;
			FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
			fullScreenFXMgr.SetBlurAmount(0.3f);
			fullScreenFXMgr.SetBlurBrightness(0.4f);
			fullScreenFXMgr.SetBlurDesaturation(0.5f);
			fullScreenFXMgr.Blur(1f, 0.5f, iTween.EaseType.easeOutCirc);
		}
	}

	private void DisableFullScreenBlur()
	{
		if (m_blurEnabled)
		{
			m_blurEnabled = false;
			FullScreenFXMgr.Get().StopBlur(0.5f, iTween.EaseType.easeOutCirc);
		}
	}

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

	private bool isMissingeLastHorseMen()
	{
		List<string> list = new List<string> { "ICC_829t5", "ICC_829t2", "ICC_829t3", "ICC_829t4" };
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		foreach (Card card in opposingSidePlayer.GetBattlefieldZone().GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetControllerId() == opposingSidePlayer.GetPlayerId() && list.Contains(entity.GetCardId()))
			{
				list.Remove(entity.GetCardId());
			}
		}
		if (list.Count == 1)
		{
			return true;
		}
		return false;
	}

	public IEnumerator PlayTirionVictoryVO()
	{
		bool finished = false;
		Notification notification = NotificationManager.Get().CreateBigCharacterQuoteWithText("Tirion_BigQuote.prefab:878fcebc1cddaf24f828c44edb07f7f8", NotificationManager.DEFAULT_CHARACTER_POS, "VO_ICC08_Tirion_Male_Human_Win_01.prefab:e97467bae07fb0340a24b6772b048608", GameStrings.Get("VO_ICC08_Tirion_Male_Human_Win_01"), 2f, null, useOverlayUI: false, Notification.SpeechBubbleDirection.BottomLeft);
		notification.OnFinishDeathState = (Action<int>)Delegate.Combine(notification.OnFinishDeathState, (Action<int>)delegate
		{
			finished = true;
		});
		while (!finished)
		{
			yield return null;
		}
	}

	public IEnumerator PlayFriendlyHeroVictoryVO(Actor actor, AudioSource preTransformationAudio, AudioSource midTransformationAudio)
	{
		TAG_CLASS @class = GameState.Get().GetFriendlySidePlayer().GetStartingHero()
			.GetClass();
		if (m_HeroVictoryLines.TryGetValue(@class, out var voLines))
		{
			float scale = (UniversalInputManager.UsePhoneUI ? 0.5f : 0.75f);
			if (preTransformationAudio != null)
			{
				SoundManager.Get().Play(preTransformationAudio);
			}
			yield return PlaySoundAndBlockSpeech(voLines[0], Notification.SpeechBubbleDirection.BottomLeft, actor, 2f, 1f, parentBubbleToActor: false, delayCardSoundSpells: false, scale);
			if (midTransformationAudio != null)
			{
				SoundManager.Get().Play(midTransformationAudio);
			}
			yield return new WaitForSeconds(0.5f);
			yield return PlaySoundAndBlockSpeech(voLines[1], Notification.SpeechBubbleDirection.BottomLeft, actor, 2f, 1f, parentBubbleToActor: false, delayCardSoundSpells: false, scale);
		}
	}
}
