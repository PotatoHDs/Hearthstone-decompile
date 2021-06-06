using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200047A RID: 1146
public class DALA_MissionEntity : GenericDungeonMissionEntity
{
	// Token: 0x06003E02 RID: 15874 RVA: 0x0014645E File Offset: 0x0014465E
	public override AdventureDbId GetAdventureID()
	{
		return AdventureDbId.DALARAN;
	}

	// Token: 0x06003E03 RID: 15875 RVA: 0x00146468 File Offset: 0x00144668
	public override void PreloadAssets()
	{
		GenericDungeonMissionEntity.VOPool value = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_MissionEntity.VO_DALA_Rafaam_Male_Ethereal_TUT_13_First_Defeat_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, DALA_MissionEntity.Rafaam_BigQuote, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_VO);
		this.m_VOPools.Add(900, value);
		GenericDungeonMissionEntity.VOPool value2 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_MissionEntity.VO_DALA_Togwaggle_Male_Kobold_STORY_Bank_Turn2_01_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, DALA_MissionEntity.KingTogwaggle_BigQuote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_BANK_INTRO_1_VO);
		this.m_VOPools.Add(901, value2);
		GenericDungeonMissionEntity.VOPool value3 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_MissionEntity.VO_DALA_Togwaggle_Male_Kobold_STORY_Bank_Turn2_02_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, DALA_MissionEntity.KingTogwaggle_BigQuote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_BANK_INTRO_2_VO);
		this.m_VOPools.Add(903, value3);
		GenericDungeonMissionEntity.VOPool value4 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_MissionEntity.VO_DALA_Rafaam_Male_Ethereal_STORY_MageTower_Turn2_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, DALA_MissionEntity.Rafaam_BigQuote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_MAGETOWER_INTRO_1_VO);
		this.m_VOPools.Add(904, value4);
		GenericDungeonMissionEntity.VOPool value5 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_MissionEntity.VO_DALA_Hagatha_Female_Orc_STORY_Prison_Turn2_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, DALA_MissionEntity.Hagatha_BrassRing_Quote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_PRISON_INTRO_1_VO);
		this.m_VOPools.Add(905, value5);
		GenericDungeonMissionEntity.VOPool value6 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_MissionEntity.VO_DALA_DrBoom_Male_Goblin_STORY_Sewers_Turn2_01_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, DALA_MissionEntity.DrBoom_BrassRing_Quote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_SEWERS_INTRO_1_VO);
		this.m_VOPools.Add(906, value6);
		GenericDungeonMissionEntity.VOPool value7 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_MissionEntity.VO_DALA_DrBoom_Male_Goblin_STORY_Sewers_Turn2_02_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, DALA_MissionEntity.DrBoom_BrassRing_Quote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_SEWERS_INTRO_2_VO);
		this.m_VOPools.Add(907, value7);
		GenericDungeonMissionEntity.VOPool value8 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_MissionEntity.VO_DALA_Lazul_Female_Troll_STORY_Streets_Turn2_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, DALA_MissionEntity.Madam_Lazul_Popup_BrassRing_Quote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_STREETS_INTRO_1_VO);
		this.m_VOPools.Add(908, value8);
		GenericDungeonMissionEntity.VOPool value9 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_MissionEntity.VO_DALA_Togwaggle_Male_Kobold_TUT_Hero_Rakanishu_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, DALA_MissionEntity.KingTogwaggle_BigQuote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_MAGE_INTRO_1_VO);
		this.m_VOPools.Add(918, value9);
		GenericDungeonMissionEntity.VOPool value10 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_MissionEntity.VO_DALA_Hagatha_Female_Orc_TUT_Hero_Vessina_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, DALA_MissionEntity.Hagatha_BrassRing_Quote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_SHAMAN_INTRO_1_VO);
		this.m_VOPools.Add(919, value10);
		GenericDungeonMissionEntity.VOPool value11 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_MissionEntity.VO_DALA_DrBoom_Male_Goblin_TUT_Hero_Barkeye_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, DALA_MissionEntity.DrBoom_BrassRing_Quote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_HUNTER_INTRO_1_VO);
		this.m_VOPools.Add(920, value11);
		GenericDungeonMissionEntity.VOPool value12 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_MissionEntity.VO_DALA_Lazul_Female_Troll_TUT_Hero_Kriziki_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, DALA_MissionEntity.Madam_Lazul_Popup_BrassRing_Quote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_PRIEST_INTRO_1_VO);
		this.m_VOPools.Add(921, value12);
		GenericDungeonMissionEntity.VOPool value13 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_MissionEntity.VO_DALA_Lazul_Female_Troll_TUT_Hero_Eudora_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, DALA_MissionEntity.Madam_Lazul_Popup_BrassRing_Quote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_ROGUE_INTRO_1_VO);
		this.m_VOPools.Add(922, value13);
		GenericDungeonMissionEntity.VOPool value14 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_MissionEntity.VO_DALA_DrBoom_Male_Goblin_TUT_Hero_Chu_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, DALA_MissionEntity.DrBoom_BrassRing_Quote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_WARRIOR_INTRO_1_VO);
		this.m_VOPools.Add(923, value14);
		GenericDungeonMissionEntity.VOPool value15 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_MissionEntity.VO_DALA_Hagatha_Female_Orc_TUT_Hero_Squeamlish_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, DALA_MissionEntity.Hagatha_BrassRing_Quote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_DRUID_INTRO_1_VO);
		this.m_VOPools.Add(924, value15);
		GenericDungeonMissionEntity.VOPool value16 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_MissionEntity.VO_DALA_Rafaam_Male_Ethereal_TUT_Hero_Tekahn_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, DALA_MissionEntity.Rafaam_BigQuote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_WARLOCK_INTRO_1_VO);
		this.m_VOPools.Add(925, value16);
		GenericDungeonMissionEntity.VOPool value17 = new GenericDungeonMissionEntity.VOPool(new List<string>
		{
			DALA_MissionEntity.VO_DALA_Rafaam_Male_Ethereal_TUT_Hero_George_01
		}, 1f, MissionEntity.ShouldPlayValue.Once, GenericDungeonMissionEntity.VOSpeaker.INVALID, DALA_MissionEntity.Rafaam_BigQuote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_PALADIN_INTRO_1_VO);
		this.m_VOPools.Add(926, value17);
		base.PreloadAssets();
	}

	// Token: 0x06003E04 RID: 15876 RVA: 0x001468F7 File Offset: 0x00144AF7
	protected override bool CanPlayVOLines(Entity speakerEntity, GenericDungeonMissionEntity.VOSpeaker speaker)
	{
		if (speaker == GenericDungeonMissionEntity.VOSpeaker.FRIENDLY_HERO)
		{
			return speakerEntity.GetCardId().Contains("DALA_");
		}
		return base.CanPlayVOLines(speakerEntity, speaker);
	}

	// Token: 0x06003E05 RID: 15877 RVA: 0x00146916 File Offset: 0x00144B16
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(this.HandleMissionEventWithTiming(900));
			yield break;
		}
		yield break;
	}

	// Token: 0x06003E06 RID: 15878 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	// Token: 0x06003E07 RID: 15879 RVA: 0x0014692C File Offset: 0x00144B2C
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (turn == 1 && GameState.Get() != null && GameState.Get().GetFriendlySidePlayer() != null && GameState.Get().GetFriendlySidePlayer().GetHero() != null)
		{
			ScenarioDbId missionId = (ScenarioDbId)GameMgr.Get().GetMissionId();
			if (missionId != ScenarioDbId.DALA_01_BANK)
			{
				switch (missionId)
				{
				case ScenarioDbId.DALA_02_VIOLET_HOLD:
					yield return Gameplay.Get().StartCoroutine(this.HandleMissionEventWithTiming(905));
					break;
				case ScenarioDbId.DALA_03_STREETS:
					yield return Gameplay.Get().StartCoroutine(this.HandleMissionEventWithTiming(908));
					break;
				case ScenarioDbId.DALA_04_UNDERBELLY:
					yield return Gameplay.Get().StartCoroutine(this.HandleMissionEventWithTiming(906));
					yield return Gameplay.Get().StartCoroutine(this.HandleMissionEventWithTiming(907));
					break;
				case ScenarioDbId.DALA_05_CITADEL:
					yield return Gameplay.Get().StartCoroutine(this.HandleMissionEventWithTiming(904));
					break;
				}
			}
			else
			{
				yield return Gameplay.Get().StartCoroutine(this.HandleMissionEventWithTiming(901));
				yield return Gameplay.Get().StartCoroutine(this.HandleMissionEventWithTiming(903));
			}
		}
		if (turn == 3)
		{
			if (GameState.Get() != null && GameState.Get().GetFriendlySidePlayer() != null && GameState.Get().GetFriendlySidePlayer().GetHero() != null)
			{
				string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
				uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
				if (num <= 1326263597U)
				{
					if (num <= 423479732U)
					{
						if (num != 177790363U)
						{
							if (num == 423479732U)
							{
								if (cardId == "DALA_Eudora")
								{
									yield return Gameplay.Get().StartCoroutine(this.HandleMissionEventWithTiming(922));
								}
							}
						}
						else if (cardId == "DALA_Tekahn")
						{
							yield return Gameplay.Get().StartCoroutine(this.HandleMissionEventWithTiming(925));
						}
					}
					else if (num != 795909273U)
					{
						if (num == 1326263597U)
						{
							if (cardId == "DALA_Vessina")
							{
								yield return Gameplay.Get().StartCoroutine(this.HandleMissionEventWithTiming(919));
							}
						}
					}
					else if (cardId == "DALA_Barkeye")
					{
						yield return Gameplay.Get().StartCoroutine(this.HandleMissionEventWithTiming(920));
					}
				}
				else if (num <= 2444157672U)
				{
					if (num != 2309279698U)
					{
						if (num == 2444157672U)
						{
							if (cardId == "DALA_Rakanishu")
							{
								yield return Gameplay.Get().StartCoroutine(this.HandleMissionEventWithTiming(918));
							}
						}
					}
					else if (cardId == "DALA_Squeamlish")
					{
						yield return Gameplay.Get().StartCoroutine(this.HandleMissionEventWithTiming(924));
					}
				}
				else if (num != 2663649824U)
				{
					if (num != 3522277015U)
					{
						if (num == 3565969811U)
						{
							if (cardId == "DALA_Kriziki")
							{
								yield return Gameplay.Get().StartCoroutine(this.HandleMissionEventWithTiming(921));
							}
						}
					}
					else if (cardId == "DALA_George")
					{
						yield return Gameplay.Get().StartCoroutine(this.HandleMissionEventWithTiming(926));
					}
				}
				else if (cardId == "DALA_Chu")
				{
					yield return Gameplay.Get().StartCoroutine(this.HandleMissionEventWithTiming(923));
				}
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x06003E08 RID: 15880 RVA: 0x00146944 File Offset: 0x00144B44
	public override void OnPlayThinkEmote()
	{
		if (this.m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			return;
		}
		if (currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		EmoteType emoteType = EmoteType.THINK1;
		switch (UnityEngine.Random.Range(1, 4))
		{
		case 1:
			emoteType = EmoteType.THINK1;
			break;
		case 2:
			emoteType = EmoteType.THINK2;
			break;
		case 3:
			emoteType = EmoteType.THINK3;
			break;
		}
		GameState.Get().GetCurrentPlayer().GetHeroCard().PlayEmote(emoteType);
	}

	// Token: 0x06003E09 RID: 15881 RVA: 0x001469C0 File Offset: 0x00144BC0
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DALMulligan);
		}
	}

	// Token: 0x06003E0A RID: 15882 RVA: 0x001469D8 File Offset: 0x00144BD8
	private int GetDefeatedBossCountForFinalBoss()
	{
		int missionId = GameMgr.Get().GetMissionId();
		if (missionId == 3191 || missionId == 3332)
		{
			return 11;
		}
		return 7;
	}

	// Token: 0x06003E0B RID: 15883 RVA: 0x00146A04 File Offset: 0x00144C04
	public override void StartGameplaySoundtracks()
	{
		if (GameUtils.GetDefeatedBossCount() == this.GetDefeatedBossCountForFinalBoss())
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DALFinalBoss);
			return;
		}
		base.StartGameplaySoundtracks();
	}

	// Token: 0x04002994 RID: 10644
	private static readonly AssetReference Rafaam_BigQuote = new AssetReference("Rafaam_popup_BrassRing_Quote.prefab:187724fae6d64cf49acf11aa53ca2087");

	// Token: 0x04002995 RID: 10645
	private static readonly AssetReference KingTogwaggle_BigQuote = new AssetReference("Togwaggle_pop-up_BrassRing_Quote.prefab:99e68bee5c488cb45a212327619b0922");

	// Token: 0x04002996 RID: 10646
	private static readonly AssetReference Hagatha_BrassRing_Quote = new AssetReference("Hagatha_Pop-up_BrassRing_Quote.prefab:82d8a1fd3b66a3c4da28e4dc34b42617");

	// Token: 0x04002997 RID: 10647
	private static readonly AssetReference DrBoom_BrassRing_Quote = new AssetReference("Blastermaster_Boom_popup_BrassRing_Quote.prefab:71029fa93b8e9564bb2fa3003158ba08");

	// Token: 0x04002998 RID: 10648
	private static readonly AssetReference Madam_Lazul_Popup_BrassRing_Quote = new AssetReference("Madam_Lazul_Popup_BrassRing_Quote.prefab:5fd991c28d0cc7842b99ae3ddb65aa0c");

	// Token: 0x04002999 RID: 10649
	private static readonly AssetReference VO_DALA_Rafaam_Male_Ethereal_TUT_13_First_Defeat_01 = new AssetReference("VO_DALA_Rafaam_Male_Ethereal_TUT_13_First_Defeat_01.prefab:98997917ca51ac748b76fc292ed6b379");

	// Token: 0x0400299A RID: 10650
	private static readonly AssetReference VO_DALA_Togwaggle_Male_Kobold_STORY_Bank_Turn2_01_01 = new AssetReference("VO_DALA_Togwaggle_Male_Kobold_STORY_Bank_Turn2_01_01.prefab:9423cc11e61f968458af8ccfd6dd538e");

	// Token: 0x0400299B RID: 10651
	private static readonly AssetReference VO_DALA_Togwaggle_Male_Kobold_STORY_Bank_Turn2_02_01 = new AssetReference("VO_DALA_Togwaggle_Male_Kobold_STORY_Bank_Turn2_02_01.prefab:d7d29a9b3f544ff49bcd4e122db32ff5");

	// Token: 0x0400299C RID: 10652
	private static readonly AssetReference VO_DALA_Rafaam_Male_Ethereal_STORY_MageTower_Turn2_01 = new AssetReference("VO_DALA_Rafaam_Male_Ethereal_STORY_MageTower_Turn2_01.prefab:f13015d799776d44dbb8106b38b02d51");

	// Token: 0x0400299D RID: 10653
	private static readonly AssetReference VO_DALA_Hagatha_Female_Orc_STORY_Prison_Turn2_01 = new AssetReference("VO_DALA_Hagatha_Female_Orc_STORY_Prison_Turn2_01.prefab:eb7edcdc03f34e4428a97ebd7d6fb6ab");

	// Token: 0x0400299E RID: 10654
	private static readonly AssetReference VO_DALA_DrBoom_Male_Goblin_STORY_Sewers_Turn2_01_01 = new AssetReference("VO_DALA_DrBoom_Male_Goblin_STORY_Sewers_Turn2_01_01.prefab:4b506e90cb13cfc448c98ca85cbccc3a");

	// Token: 0x0400299F RID: 10655
	private static readonly AssetReference VO_DALA_DrBoom_Male_Goblin_STORY_Sewers_Turn2_02_01 = new AssetReference("VO_DALA_DrBoom_Male_Goblin_STORY_Sewers_Turn2_02_01.prefab:484423660a719ac4898444e883ec536a");

	// Token: 0x040029A0 RID: 10656
	private static readonly AssetReference VO_DALA_Lazul_Female_Troll_STORY_Streets_Turn2_01 = new AssetReference("VO_DALA_Lazul_Female_Troll_STORY_Streets_Turn2_01.prefab:82d9dc577f4363d4898cb9cb163d4294");

	// Token: 0x040029A1 RID: 10657
	private static readonly AssetReference VO_DALA_DrBoom_Male_Goblin_TUT_Hero_Barkeye_01 = new AssetReference("VO_DALA_DrBoom_Male_Goblin_TUT_Hero_Barkeye_01.prefab:4bf1e078e38e5a8499e067f2e82553d8");

	// Token: 0x040029A2 RID: 10658
	private static readonly AssetReference VO_DALA_DrBoom_Male_Goblin_TUT_Hero_Chu_01 = new AssetReference("VO_DALA_DrBoom_Male_Goblin_TUT_Hero_Chu_01.prefab:9ade2bce9cdd6bf4a9a917d8c35ab77f");

	// Token: 0x040029A3 RID: 10659
	private static readonly AssetReference VO_DALA_Hagatha_Female_Orc_TUT_Hero_Squeamlish_01 = new AssetReference("VO_DALA_Hagatha_Female_Orc_TUT_Hero_Squeamlish_01.prefab:0c28ffc8a7fa4fb468e2e3814fb2d4df");

	// Token: 0x040029A4 RID: 10660
	private static readonly AssetReference VO_DALA_Hagatha_Female_Orc_TUT_Hero_Vessina_01 = new AssetReference("VO_DALA_Hagatha_Female_Orc_TUT_Hero_Vessina_01.prefab:d4b062f7e7a37ae4d87a6c0c751b30e6");

	// Token: 0x040029A5 RID: 10661
	private static readonly AssetReference VO_DALA_Lazul_Female_Troll_TUT_Hero_Eudora_01 = new AssetReference("VO_DALA_Lazul_Female_Troll_TUT_Hero_Eudora_01.prefab:38afe0fc9447e6142be1ed5494521d54");

	// Token: 0x040029A6 RID: 10662
	private static readonly AssetReference VO_DALA_Lazul_Female_Troll_TUT_Hero_Kriziki_01 = new AssetReference("VO_DALA_Lazul_Female_Troll_TUT_Hero_Kriziki_01.prefab:213a37b74c609214da370eaa54ec2f85");

	// Token: 0x040029A7 RID: 10663
	private static readonly AssetReference VO_DALA_Rafaam_Male_Ethereal_TUT_Hero_George_01 = new AssetReference("VO_DALA_Rafaam_Male_Ethereal_TUT_Hero_George_01.prefab:e48bdcc75b4950f44b2cd019a696b5a5");

	// Token: 0x040029A8 RID: 10664
	private static readonly AssetReference VO_DALA_Rafaam_Male_Ethereal_TUT_Hero_Tekahn_01 = new AssetReference("VO_DALA_Rafaam_Male_Ethereal_TUT_Hero_Tekahn_01.prefab:a93c0cad823003449b25d86802a2b103");

	// Token: 0x040029A9 RID: 10665
	private static readonly AssetReference VO_DALA_Togwaggle_Male_Kobold_TUT_Hero_Rakanishu_01 = new AssetReference("VO_DALA_Togwaggle_Male_Kobold_TUT_Hero_Rakanishu_01.prefab:2bb54afd9f58ece42806e54b2ba3a04d");

	// Token: 0x020019FF RID: 6655
	public enum DALA_VOPoolType
	{
		// Token: 0x0400BFD2 RID: 49106
		INVALID,
		// Token: 0x0400BFD3 RID: 49107
		TUTORIAL_PLAYER_FIRST_LOST = 900,
		// Token: 0x0400BFD4 RID: 49108
		TUTORIAL_BANK_INTRO_1,
		// Token: 0x0400BFD5 RID: 49109
		TUTORIAL_BANK_INTRO_2 = 903,
		// Token: 0x0400BFD6 RID: 49110
		TUTORIAL_MAGETOWER_INTRO_1,
		// Token: 0x0400BFD7 RID: 49111
		TUTORIAL_PRISON_INTRO_1,
		// Token: 0x0400BFD8 RID: 49112
		TUTORIAL_SEWERS_INTRO_1,
		// Token: 0x0400BFD9 RID: 49113
		TUTORIAL_SEWERS_INTRO_2,
		// Token: 0x0400BFDA RID: 49114
		TUTORIAL_STREETS_INTRO_1,
		// Token: 0x0400BFDB RID: 49115
		FIRST_TAVERN_DRUID,
		// Token: 0x0400BFDC RID: 49116
		FIRST_TAVERN_HUNTER,
		// Token: 0x0400BFDD RID: 49117
		FIRST_TAVERN_MAGE,
		// Token: 0x0400BFDE RID: 49118
		FIRST_TAVERN_PALADIN,
		// Token: 0x0400BFDF RID: 49119
		FIRST_TAVERN_PRIEST,
		// Token: 0x0400BFE0 RID: 49120
		FIRST_TAVERN_ROGUE,
		// Token: 0x0400BFE1 RID: 49121
		FIRST_TAVERN_SHAMAN,
		// Token: 0x0400BFE2 RID: 49122
		FIRST_TAVERN_WARLOCK,
		// Token: 0x0400BFE3 RID: 49123
		FIRST_TAVERN_WARRIOR,
		// Token: 0x0400BFE4 RID: 49124
		INTRO_MAGE,
		// Token: 0x0400BFE5 RID: 49125
		INTRO_SHAMAN,
		// Token: 0x0400BFE6 RID: 49126
		INTRO_HUNTER,
		// Token: 0x0400BFE7 RID: 49127
		INTRO_PRIEST,
		// Token: 0x0400BFE8 RID: 49128
		INTRO_ROGUE,
		// Token: 0x0400BFE9 RID: 49129
		INTRO_WARRIOR,
		// Token: 0x0400BFEA RID: 49130
		INTRO_DRUID,
		// Token: 0x0400BFEB RID: 49131
		INTRO_WARLOCK,
		// Token: 0x0400BFEC RID: 49132
		INTRO_PALADIN
	}
}
