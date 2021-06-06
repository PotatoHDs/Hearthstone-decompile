using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOOT_Dungeon : LOOT_MissionEntity
{
	private static readonly string m_KingTogwaggle_BigQuote = "KingTogwaggle_BigQuote.prefab:9416c71ab37ae184b8c4bfaaf3233882";

	private static readonly string m_KingTogwaggle_Quote = "KingTogwaggle_Quote.prefab:b20f7b1314c0a2d46a9de0e48e7ae6f5";

	private static readonly AssetReference VO_LOOT_541_Male_Kobold_TUT_Game1Begin1_01 = new AssetReference("VO_LOOT_541_Male_Kobold_TUT_Game1Begin1_01.prefab:4eb8b422c051369409d10b42a777ee1d");

	private static readonly AssetReference VO_LOOT_541_Male_Kobold_TUT_Game1Defeat_01 = new AssetReference("VO_LOOT_541_Male_Kobold_TUT_Game1Defeat_01.prefab:c16731bb4687d154a9a22c8e01eeabb2");

	private static readonly AssetReference VO_LOOT_541_Male_Kobold_TUT_Game1Victory_01 = new AssetReference("VO_LOOT_541_Male_Kobold_TUT_Game1Victory_01.prefab:bf40d91ec399f7e4c88b3a4c8c71bda5");

	private static readonly AssetReference VO_LOOT_541_Male_Kobold_TUT_Game2Begin1_01 = new AssetReference("VO_LOOT_541_Male_Kobold_TUT_Game2Begin1_01.prefab:7fcfd8f32efb5374aae312892eac84ff");

	private static readonly AssetReference VO_LOOT_541_Male_Kobold_TUT_Game2Begin2_01 = new AssetReference("VO_LOOT_541_Male_Kobold_TUT_Game2Begin2_01.prefab:90b80890ad0f972478294383cc02e233");

	private static readonly AssetReference VO_LOOT_541_Male_Kobold_TUT_GeneralDefeat1_01 = new AssetReference("VO_LOOT_541_Male_Kobold_TUT_GeneralDefeat1_01.prefab:9bb4ec22f68d90342a84fba2f3d7a100");

	private static readonly AssetReference VO_LOOT_541_Male_Kobold_TUT_GeneralDefeat2_01 = new AssetReference("VO_LOOT_541_Male_Kobold_TUT_GeneralDefeat2_01.prefab:6dfdf8edb59d4c14380b38e874769662");

	private static readonly AssetReference VO_LOOTA_829_Male_Human_Event_01 = new AssetReference("VO_LOOTA_829_Male_Human_Event_01.prefab:beb8a7cd19bc24f46a617b0c1774da48");

	private const AdventureDbId AdventureId = AdventureDbId.LOOT;

	private GameSaveKeyId m_gameSaveDataClientKey;

	private long m_hasSeenInGameWinVO;

	private long m_hasSeenInGameLoseVO;

	private long m_hasSeenInGameLose2VO;

	private long m_hasSeenInGameMulliganVO;

	private long m_hasSeenInGameMulligan2VO;

	private List<GameSaveKeySubkeyId> m_inGameSubkeysToSave = new List<GameSaveKeySubkeyId>();

	public LOOT_Dungeon()
	{
		AdventureDataDbfRecord record = GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == 414);
		m_gameSaveDataClientKey = (GameSaveKeyId)record.GameSaveDataClientKey;
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_WIN_VO, out m_hasSeenInGameWinVO);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_VO, out m_hasSeenInGameLoseVO);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_2_VO, out m_hasSeenInGameLose2VO);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_1_VO, out m_hasSeenInGameMulliganVO);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_2_VO, out m_hasSeenInGameMulligan2VO);
	}

	public override void PreloadAssets()
	{
		PreloadSound(VO_LOOT_541_Male_Kobold_TUT_Game1Victory_01);
		PreloadSound(VO_LOOT_541_Male_Kobold_TUT_Game1Defeat_01);
		PreloadSound(VO_LOOT_541_Male_Kobold_TUT_GeneralDefeat1_01);
		PreloadSound(VO_LOOT_541_Male_Kobold_TUT_GeneralDefeat2_01);
		PreloadSound(VO_LOOT_541_Male_Kobold_TUT_Game1Begin1_01);
		PreloadSound(VO_LOOT_541_Male_Kobold_TUT_Game2Begin1_01);
		PreloadSound(VO_LOOT_541_Male_Kobold_TUT_Game2Begin2_01);
		PreloadSound(VO_LOOTA_829_Male_Human_Event_01);
	}

	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_LOOTMulligan);
		}
	}

	public static LOOT_Dungeon InstantiateLootDungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		switch (opposingHeroCardID)
		{
		case "LOOTA_BOSS_04h":
			return new LOOT_Dungeon_BOSS_04h();
		case "LOOTA_BOSS_05h":
			return new LOOT_Dungeon_BOSS_05h();
		case "LOOTA_BOSS_06h":
			return new LOOT_Dungeon_BOSS_06h();
		case "LOOTA_BOSS_09h":
			return new LOOT_Dungeon_BOSS_09h();
		case "LOOTA_BOSS_10h":
			return new LOOT_Dungeon_BOSS_10h();
		case "LOOTA_BOSS_11h":
			return new LOOT_Dungeon_BOSS_11h();
		case "LOOTA_BOSS_12h":
			return new LOOT_Dungeon_BOSS_12h();
		case "LOOTA_BOSS_13h":
			return new LOOT_Dungeon_BOSS_13h();
		case "LOOTA_BOSS_15h":
			return new LOOT_Dungeon_BOSS_15h();
		case "LOOTA_BOSS_16h":
			return new LOOT_Dungeon_BOSS_16h();
		case "LOOTA_BOSS_17h":
			return new LOOT_Dungeon_BOSS_17h();
		case "LOOTA_BOSS_18h":
			return new LOOT_Dungeon_BOSS_18h();
		case "LOOTA_BOSS_19h":
			return new LOOT_Dungeon_BOSS_19h();
		case "LOOTA_BOSS_20h":
			return new LOOT_Dungeon_BOSS_20h();
		case "LOOTA_BOSS_21h":
			return new LOOT_Dungeon_BOSS_21h();
		case "LOOTA_BOSS_22h":
			return new LOOT_Dungeon_BOSS_22h();
		case "LOOTA_BOSS_23h":
			return new LOOT_Dungeon_BOSS_23h();
		case "LOOTA_BOSS_24h":
			return new LOOT_Dungeon_BOSS_24h();
		case "LOOTA_BOSS_25h":
			return new LOOT_Dungeon_BOSS_25h();
		case "LOOTA_BOSS_26h":
			return new LOOT_Dungeon_BOSS_26h();
		case "LOOTA_BOSS_27h":
			return new LOOT_Dungeon_BOSS_04h();
		case "LOOTA_BOSS_28h":
			return new LOOT_Dungeon_BOSS_05h();
		case "LOOTA_BOSS_29h":
			return new LOOT_Dungeon_BOSS_06h();
		case "LOOTA_BOSS_30h":
			return new LOOT_Dungeon_BOSS_11h();
		case "LOOTA_BOSS_31h":
			return new LOOT_Dungeon_BOSS_12h();
		case "LOOTA_BOSS_32h":
			return new LOOT_Dungeon_BOSS_15h();
		case "LOOTA_BOSS_33h":
			return new LOOT_Dungeon_BOSS_33h();
		case "LOOTA_BOSS_34h":
			return new LOOT_Dungeon_BOSS_34h();
		case "LOOTA_BOSS_35h":
			return new LOOT_Dungeon_BOSS_35h();
		case "LOOTA_BOSS_36h":
			return new LOOT_Dungeon_BOSS_36h();
		case "LOOTA_BOSS_37h":
			return new LOOT_Dungeon_BOSS_37h();
		case "LOOTA_BOSS_38h":
			return new LOOT_Dungeon_BOSS_38h();
		case "LOOTA_BOSS_39h":
			return new LOOT_Dungeon_BOSS_39h();
		case "LOOTA_BOSS_40h":
			return new LOOT_Dungeon_BOSS_40h();
		case "LOOTA_BOSS_41h":
			return new LOOT_Dungeon_BOSS_41h();
		case "LOOTA_BOSS_42h":
			return new LOOT_Dungeon_BOSS_42h();
		case "LOOTA_BOSS_43h":
			return new LOOT_Dungeon_BOSS_43h();
		case "LOOTA_BOSS_44h":
			return new LOOT_Dungeon_BOSS_44h();
		case "LOOTA_BOSS_45h":
			return new LOOT_Dungeon_BOSS_45h();
		case "LOOTA_BOSS_46h":
			return new LOOT_Dungeon_BOSS_46h();
		case "LOOTA_BOSS_47h":
			return new LOOT_Dungeon_BOSS_47h();
		case "LOOTA_BOSS_48h":
			return new LOOT_Dungeon_BOSS_48h();
		case "LOOTA_BOSS_49h":
			return new LOOT_Dungeon_BOSS_49h();
		case "LOOTA_BOSS_50h":
			return new LOOT_Dungeon_BOSS_50h();
		case "LOOTA_BOSS_51h":
			return new LOOT_Dungeon_BOSS_51h();
		case "LOOTA_BOSS_52h":
			return new LOOT_Dungeon_BOSS_52h();
		case "LOOTA_BOSS_53h":
			return new LOOT_Dungeon_BOSS_53h();
		case "LOOTA_BOSS_53h2":
			return new LOOT_Dungeon_BOSS_53h();
		case "LOOTA_BOSS_54h":
			return new LOOT_Dungeon_BOSS_54h();
		case "LOOTA_BOSS_99h":
			return new LOOT_Dungeon_BOSS_99h();
		default:
			Log.All.PrintError("LOOT_Dungeon.InstantiateLootDungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", opposingHeroCardID);
			return new LOOT_Dungeon();
		}
	}

	public override void OnPlayThinkEmote()
	{
		if (!m_enemySpeaking)
		{
			Player currentPlayer = GameState.Get().GetCurrentPlayer();
			if (currentPlayer.IsFriendlySide())
			{
				currentPlayer.GetHeroCard().HasActiveEmoteSound();
			}
		}
	}

	protected virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	protected virtual string GetBossDeathLine()
	{
		return null;
	}

	protected virtual bool GetShouldSupressDeathTextBubble()
	{
		return false;
	}

	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 0.5f;
	}

	protected virtual void OnBossHeroPowerPlayed(Entity entity)
	{
		float num = ChanceToPlayBossHeroPowerVOLine();
		float num2 = Random.Range(0f, 1f);
		if (num < num2)
		{
			return;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (actor == null)
		{
			return;
		}
		List<string> bossHeroPowerRandomLines = GetBossHeroPowerRandomLines();
		string text = "";
		while (bossHeroPowerRandomLines.Count > 0)
		{
			int index = Random.Range(0, bossHeroPowerRandomLines.Count);
			text = bossHeroPowerRandomLines[index];
			bossHeroPowerRandomLines.RemoveAt(index);
			if (!NotificationManager.Get().HasSoundPlayedThisSession(text))
			{
				break;
			}
		}
		if (!(text == ""))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce(text, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		if (!m_enemySpeaking && entity.GetCardType() != 0 && entity.GetCardType() == TAG_CARDTYPE.HERO_POWER && entity.GetControllerSide() == Player.Side.OPPOSING)
		{
			OnBossHeroPowerPlayed(entity);
		}
		yield break;
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (turn == 1)
		{
			if (m_hasSeenInGameMulliganVO == 0L && m_hasSeenInGameMulligan2VO == 0L)
			{
				m_inGameSubkeysToSave.Add(GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_1_VO);
				yield return PlayBigCharacterQuoteAndWait(m_KingTogwaggle_BigQuote, VO_LOOT_541_Male_Kobold_TUT_Game1Begin1_01);
			}
			else if (m_hasSeenInGameMulliganVO > 0 && m_hasSeenInGameMulligan2VO == 0L)
			{
				m_inGameSubkeysToSave.Add(GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_2_VO);
				yield return PlayBigCharacterQuoteAndWait(m_KingTogwaggle_BigQuote, VO_LOOT_541_Male_Kobold_TUT_Game2Begin1_01);
				yield return PlayBigCharacterQuoteAndWait(m_KingTogwaggle_BigQuote, VO_LOOT_541_Male_Kobold_TUT_Game2Begin2_01);
			}
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
		foreach (GameSaveKeySubkeyId item in m_inGameSubkeysToSave)
		{
			list.Add(new GameSaveDataManager.SubkeySaveRequest(m_gameSaveDataClientKey, item, 1L));
		}
		if (list.Count > 0)
		{
			GameSaveDataManager.Get().SaveSubkeys(list);
		}
		if (gameResult == TAG_PLAYSTATE.WON && m_hasSeenInGameWinVO == 0L)
		{
			yield return new WaitForSeconds(5f);
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_WIN_VO, 1L));
			yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait(m_KingTogwaggle_Quote, VO_LOOT_541_Male_Kobold_TUT_Game1Victory_01));
		}
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			if (m_hasSeenInGameLoseVO == 0L && m_hasSeenInGameLose2VO == 0L)
			{
				yield return new WaitForSeconds(5f);
				GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_VO, 1L));
				yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait(m_KingTogwaggle_Quote, VO_LOOT_541_Male_Kobold_TUT_Game1Defeat_01));
			}
			else if (m_hasSeenInGameLoseVO > 0 && m_hasSeenInGameLose2VO == 0L)
			{
				yield return new WaitForSeconds(5f);
				GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_2_VO, 1L));
				yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait(m_KingTogwaggle_Quote, VO_LOOT_541_Male_Kobold_TUT_GeneralDefeat1_01));
				yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait(m_KingTogwaggle_Quote, VO_LOOT_541_Male_Kobold_TUT_GeneralDefeat2_01));
			}
		}
	}

	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string bossDeathLine = GetBossDeathLine();
		if ((!m_enemySpeaking || string.IsNullOrEmpty(bossDeathLine)) && gameResult == TAG_PLAYSTATE.WON)
		{
			if (bossDeathLine == "VO_LOOTA_BOSS_51h_Male_Dwarf_Death_01.prefab:e5c8b619095374542bac028ed3654007")
			{
				PlaySound("RussellTheBard_Death_Underlay.prefab:8d76a143441379e40a36cb5b7c84b9b9");
			}
			if (GetShouldSupressDeathTextBubble())
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(bossDeathLine, Notification.SpeechBubbleDirection.None, actor));
			}
			else
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(bossDeathLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
	}

	private Actor GetEnemyLoyalSidekickActor()
	{
		foreach (Card card in GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
			.GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_2) == 1000 && entity.GetCardId() == "LOOTA_829")
			{
				return entity.GetCard().GetActor();
			}
		}
		return null;
	}

	public IEnumerator PlayLoyalSideKickBetrayal(int missionEvent)
	{
		if (missionEvent == 1000)
		{
			Actor loyalSideKick = GetEnemyLoyalSidekickActor();
			yield return WaitForEntitySoundToFinish(loyalSideKick.GetEntity());
			yield return PlayLineOnlyOnce(loyalSideKick, VO_LOOTA_829_Male_Human_Event_01);
		}
	}
}
