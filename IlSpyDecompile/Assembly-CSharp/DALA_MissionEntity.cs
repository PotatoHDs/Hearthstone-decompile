using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DALA_MissionEntity : GenericDungeonMissionEntity
{
	public enum DALA_VOPoolType
	{
		INVALID = 0,
		TUTORIAL_PLAYER_FIRST_LOST = 900,
		TUTORIAL_BANK_INTRO_1 = 901,
		TUTORIAL_BANK_INTRO_2 = 903,
		TUTORIAL_MAGETOWER_INTRO_1 = 904,
		TUTORIAL_PRISON_INTRO_1 = 905,
		TUTORIAL_SEWERS_INTRO_1 = 906,
		TUTORIAL_SEWERS_INTRO_2 = 907,
		TUTORIAL_STREETS_INTRO_1 = 908,
		FIRST_TAVERN_DRUID = 909,
		FIRST_TAVERN_HUNTER = 910,
		FIRST_TAVERN_MAGE = 911,
		FIRST_TAVERN_PALADIN = 912,
		FIRST_TAVERN_PRIEST = 913,
		FIRST_TAVERN_ROGUE = 914,
		FIRST_TAVERN_SHAMAN = 915,
		FIRST_TAVERN_WARLOCK = 916,
		FIRST_TAVERN_WARRIOR = 917,
		INTRO_MAGE = 918,
		INTRO_SHAMAN = 919,
		INTRO_HUNTER = 920,
		INTRO_PRIEST = 921,
		INTRO_ROGUE = 922,
		INTRO_WARRIOR = 923,
		INTRO_DRUID = 924,
		INTRO_WARLOCK = 925,
		INTRO_PALADIN = 926
	}

	private static readonly AssetReference Rafaam_BigQuote = new AssetReference("Rafaam_popup_BrassRing_Quote.prefab:187724fae6d64cf49acf11aa53ca2087");

	private static readonly AssetReference KingTogwaggle_BigQuote = new AssetReference("Togwaggle_pop-up_BrassRing_Quote.prefab:99e68bee5c488cb45a212327619b0922");

	private static readonly AssetReference Hagatha_BrassRing_Quote = new AssetReference("Hagatha_Pop-up_BrassRing_Quote.prefab:82d8a1fd3b66a3c4da28e4dc34b42617");

	private static readonly AssetReference DrBoom_BrassRing_Quote = new AssetReference("Blastermaster_Boom_popup_BrassRing_Quote.prefab:71029fa93b8e9564bb2fa3003158ba08");

	private static readonly AssetReference Madam_Lazul_Popup_BrassRing_Quote = new AssetReference("Madam_Lazul_Popup_BrassRing_Quote.prefab:5fd991c28d0cc7842b99ae3ddb65aa0c");

	private static readonly AssetReference VO_DALA_Rafaam_Male_Ethereal_TUT_13_First_Defeat_01 = new AssetReference("VO_DALA_Rafaam_Male_Ethereal_TUT_13_First_Defeat_01.prefab:98997917ca51ac748b76fc292ed6b379");

	private static readonly AssetReference VO_DALA_Togwaggle_Male_Kobold_STORY_Bank_Turn2_01_01 = new AssetReference("VO_DALA_Togwaggle_Male_Kobold_STORY_Bank_Turn2_01_01.prefab:9423cc11e61f968458af8ccfd6dd538e");

	private static readonly AssetReference VO_DALA_Togwaggle_Male_Kobold_STORY_Bank_Turn2_02_01 = new AssetReference("VO_DALA_Togwaggle_Male_Kobold_STORY_Bank_Turn2_02_01.prefab:d7d29a9b3f544ff49bcd4e122db32ff5");

	private static readonly AssetReference VO_DALA_Rafaam_Male_Ethereal_STORY_MageTower_Turn2_01 = new AssetReference("VO_DALA_Rafaam_Male_Ethereal_STORY_MageTower_Turn2_01.prefab:f13015d799776d44dbb8106b38b02d51");

	private static readonly AssetReference VO_DALA_Hagatha_Female_Orc_STORY_Prison_Turn2_01 = new AssetReference("VO_DALA_Hagatha_Female_Orc_STORY_Prison_Turn2_01.prefab:eb7edcdc03f34e4428a97ebd7d6fb6ab");

	private static readonly AssetReference VO_DALA_DrBoom_Male_Goblin_STORY_Sewers_Turn2_01_01 = new AssetReference("VO_DALA_DrBoom_Male_Goblin_STORY_Sewers_Turn2_01_01.prefab:4b506e90cb13cfc448c98ca85cbccc3a");

	private static readonly AssetReference VO_DALA_DrBoom_Male_Goblin_STORY_Sewers_Turn2_02_01 = new AssetReference("VO_DALA_DrBoom_Male_Goblin_STORY_Sewers_Turn2_02_01.prefab:484423660a719ac4898444e883ec536a");

	private static readonly AssetReference VO_DALA_Lazul_Female_Troll_STORY_Streets_Turn2_01 = new AssetReference("VO_DALA_Lazul_Female_Troll_STORY_Streets_Turn2_01.prefab:82d9dc577f4363d4898cb9cb163d4294");

	private static readonly AssetReference VO_DALA_DrBoom_Male_Goblin_TUT_Hero_Barkeye_01 = new AssetReference("VO_DALA_DrBoom_Male_Goblin_TUT_Hero_Barkeye_01.prefab:4bf1e078e38e5a8499e067f2e82553d8");

	private static readonly AssetReference VO_DALA_DrBoom_Male_Goblin_TUT_Hero_Chu_01 = new AssetReference("VO_DALA_DrBoom_Male_Goblin_TUT_Hero_Chu_01.prefab:9ade2bce9cdd6bf4a9a917d8c35ab77f");

	private static readonly AssetReference VO_DALA_Hagatha_Female_Orc_TUT_Hero_Squeamlish_01 = new AssetReference("VO_DALA_Hagatha_Female_Orc_TUT_Hero_Squeamlish_01.prefab:0c28ffc8a7fa4fb468e2e3814fb2d4df");

	private static readonly AssetReference VO_DALA_Hagatha_Female_Orc_TUT_Hero_Vessina_01 = new AssetReference("VO_DALA_Hagatha_Female_Orc_TUT_Hero_Vessina_01.prefab:d4b062f7e7a37ae4d87a6c0c751b30e6");

	private static readonly AssetReference VO_DALA_Lazul_Female_Troll_TUT_Hero_Eudora_01 = new AssetReference("VO_DALA_Lazul_Female_Troll_TUT_Hero_Eudora_01.prefab:38afe0fc9447e6142be1ed5494521d54");

	private static readonly AssetReference VO_DALA_Lazul_Female_Troll_TUT_Hero_Kriziki_01 = new AssetReference("VO_DALA_Lazul_Female_Troll_TUT_Hero_Kriziki_01.prefab:213a37b74c609214da370eaa54ec2f85");

	private static readonly AssetReference VO_DALA_Rafaam_Male_Ethereal_TUT_Hero_George_01 = new AssetReference("VO_DALA_Rafaam_Male_Ethereal_TUT_Hero_George_01.prefab:e48bdcc75b4950f44b2cd019a696b5a5");

	private static readonly AssetReference VO_DALA_Rafaam_Male_Ethereal_TUT_Hero_Tekahn_01 = new AssetReference("VO_DALA_Rafaam_Male_Ethereal_TUT_Hero_Tekahn_01.prefab:a93c0cad823003449b25d86802a2b103");

	private static readonly AssetReference VO_DALA_Togwaggle_Male_Kobold_TUT_Hero_Rakanishu_01 = new AssetReference("VO_DALA_Togwaggle_Male_Kobold_TUT_Hero_Rakanishu_01.prefab:2bb54afd9f58ece42806e54b2ba3a04d");

	public override AdventureDbId GetAdventureID()
	{
		return AdventureDbId.DALARAN;
	}

	public override void PreloadAssets()
	{
		VOPool value = new VOPool(new List<string> { VO_DALA_Rafaam_Male_Ethereal_TUT_13_First_Defeat_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, Rafaam_BigQuote, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_VO);
		m_VOPools.Add(900, value);
		VOPool value2 = new VOPool(new List<string> { VO_DALA_Togwaggle_Male_Kobold_STORY_Bank_Turn2_01_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, KingTogwaggle_BigQuote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_BANK_INTRO_1_VO);
		m_VOPools.Add(901, value2);
		VOPool value3 = new VOPool(new List<string> { VO_DALA_Togwaggle_Male_Kobold_STORY_Bank_Turn2_02_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, KingTogwaggle_BigQuote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_BANK_INTRO_2_VO);
		m_VOPools.Add(903, value3);
		VOPool value4 = new VOPool(new List<string> { VO_DALA_Rafaam_Male_Ethereal_STORY_MageTower_Turn2_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, Rafaam_BigQuote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_MAGETOWER_INTRO_1_VO);
		m_VOPools.Add(904, value4);
		VOPool value5 = new VOPool(new List<string> { VO_DALA_Hagatha_Female_Orc_STORY_Prison_Turn2_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, Hagatha_BrassRing_Quote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_PRISON_INTRO_1_VO);
		m_VOPools.Add(905, value5);
		VOPool value6 = new VOPool(new List<string> { VO_DALA_DrBoom_Male_Goblin_STORY_Sewers_Turn2_01_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, DrBoom_BrassRing_Quote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_SEWERS_INTRO_1_VO);
		m_VOPools.Add(906, value6);
		VOPool value7 = new VOPool(new List<string> { VO_DALA_DrBoom_Male_Goblin_STORY_Sewers_Turn2_02_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, DrBoom_BrassRing_Quote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_SEWERS_INTRO_2_VO);
		m_VOPools.Add(907, value7);
		VOPool value8 = new VOPool(new List<string> { VO_DALA_Lazul_Female_Troll_STORY_Streets_Turn2_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, Madam_Lazul_Popup_BrassRing_Quote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_STREETS_INTRO_1_VO);
		m_VOPools.Add(908, value8);
		VOPool value9 = new VOPool(new List<string> { VO_DALA_Togwaggle_Male_Kobold_TUT_Hero_Rakanishu_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, KingTogwaggle_BigQuote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_MAGE_INTRO_1_VO);
		m_VOPools.Add(918, value9);
		VOPool value10 = new VOPool(new List<string> { VO_DALA_Hagatha_Female_Orc_TUT_Hero_Vessina_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, Hagatha_BrassRing_Quote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_SHAMAN_INTRO_1_VO);
		m_VOPools.Add(919, value10);
		VOPool value11 = new VOPool(new List<string> { VO_DALA_DrBoom_Male_Goblin_TUT_Hero_Barkeye_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, DrBoom_BrassRing_Quote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_HUNTER_INTRO_1_VO);
		m_VOPools.Add(920, value11);
		VOPool value12 = new VOPool(new List<string> { VO_DALA_Lazul_Female_Troll_TUT_Hero_Kriziki_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, Madam_Lazul_Popup_BrassRing_Quote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_PRIEST_INTRO_1_VO);
		m_VOPools.Add(921, value12);
		VOPool value13 = new VOPool(new List<string> { VO_DALA_Lazul_Female_Troll_TUT_Hero_Eudora_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, Madam_Lazul_Popup_BrassRing_Quote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_ROGUE_INTRO_1_VO);
		m_VOPools.Add(922, value13);
		VOPool value14 = new VOPool(new List<string> { VO_DALA_DrBoom_Male_Goblin_TUT_Hero_Chu_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, DrBoom_BrassRing_Quote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_WARRIOR_INTRO_1_VO);
		m_VOPools.Add(923, value14);
		VOPool value15 = new VOPool(new List<string> { VO_DALA_Hagatha_Female_Orc_TUT_Hero_Squeamlish_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, Hagatha_BrassRing_Quote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_DRUID_INTRO_1_VO);
		m_VOPools.Add(924, value15);
		VOPool value16 = new VOPool(new List<string> { VO_DALA_Rafaam_Male_Ethereal_TUT_Hero_Tekahn_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, Rafaam_BigQuote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_WARLOCK_INTRO_1_VO);
		m_VOPools.Add(925, value16);
		VOPool value17 = new VOPool(new List<string> { VO_DALA_Rafaam_Male_Ethereal_TUT_Hero_George_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, Rafaam_BigQuote, GameSaveKeySubkeyId.DAL_DUNGEON_HAS_SEEN_PALADIN_INTRO_1_VO);
		m_VOPools.Add(926, value17);
		base.PreloadAssets();
	}

	protected override bool CanPlayVOLines(Entity speakerEntity, VOSpeaker speaker)
	{
		if (speaker == VOSpeaker.FRIENDLY_HERO)
		{
			return speakerEntity.GetCardId().Contains("DALA_");
		}
		return base.CanPlayVOLines(speakerEntity, speaker);
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(HandleMissionEventWithTiming(900));
		}
	}

	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (turn == 1 && GameState.Get() != null && GameState.Get().GetFriendlySidePlayer() != null && GameState.Get().GetFriendlySidePlayer().GetHero() != null)
		{
			switch (GameMgr.Get().GetMissionId())
			{
			case 3005:
				yield return Gameplay.Get().StartCoroutine(HandleMissionEventWithTiming(901));
				yield return Gameplay.Get().StartCoroutine(HandleMissionEventWithTiming(903));
				break;
			case 3188:
				yield return Gameplay.Get().StartCoroutine(HandleMissionEventWithTiming(905));
				break;
			case 3189:
				yield return Gameplay.Get().StartCoroutine(HandleMissionEventWithTiming(908));
				break;
			case 3190:
				yield return Gameplay.Get().StartCoroutine(HandleMissionEventWithTiming(906));
				yield return Gameplay.Get().StartCoroutine(HandleMissionEventWithTiming(907));
				break;
			case 3191:
				yield return Gameplay.Get().StartCoroutine(HandleMissionEventWithTiming(904));
				break;
			}
		}
		if (turn == 3 && GameState.Get() != null && GameState.Get().GetFriendlySidePlayer() != null && GameState.Get().GetFriendlySidePlayer().GetHero() != null)
		{
			switch (GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCardId())
			{
			case "DALA_Rakanishu":
				yield return Gameplay.Get().StartCoroutine(HandleMissionEventWithTiming(918));
				break;
			case "DALA_Vessina":
				yield return Gameplay.Get().StartCoroutine(HandleMissionEventWithTiming(919));
				break;
			case "DALA_Barkeye":
				yield return Gameplay.Get().StartCoroutine(HandleMissionEventWithTiming(920));
				break;
			case "DALA_Kriziki":
				yield return Gameplay.Get().StartCoroutine(HandleMissionEventWithTiming(921));
				break;
			case "DALA_Eudora":
				yield return Gameplay.Get().StartCoroutine(HandleMissionEventWithTiming(922));
				break;
			case "DALA_Chu":
				yield return Gameplay.Get().StartCoroutine(HandleMissionEventWithTiming(923));
				break;
			case "DALA_Squeamlish":
				yield return Gameplay.Get().StartCoroutine(HandleMissionEventWithTiming(924));
				break;
			case "DALA_Tekahn":
				yield return Gameplay.Get().StartCoroutine(HandleMissionEventWithTiming(925));
				break;
			case "DALA_George":
				yield return Gameplay.Get().StartCoroutine(HandleMissionEventWithTiming(926));
				break;
			}
		}
	}

	public override void OnPlayThinkEmote()
	{
		if (m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (currentPlayer.IsFriendlySide() && !currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			EmoteType emoteType = EmoteType.THINK1;
			switch (Random.Range(1, 4))
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
			GameState.Get().GetCurrentPlayer().GetHeroCard()
				.PlayEmote(emoteType);
		}
	}

	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DALMulligan);
		}
	}

	private int GetDefeatedBossCountForFinalBoss()
	{
		int missionId = GameMgr.Get().GetMissionId();
		if (missionId == 3191 || missionId == 3332)
		{
			return 11;
		}
		return 7;
	}

	public override void StartGameplaySoundtracks()
	{
		if (GameUtils.GetDefeatedBossCount() == GetDefeatedBossCountForFinalBoss())
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DALFinalBoss);
		}
		else
		{
			base.StartGameplaySoundtracks();
		}
	}
}
