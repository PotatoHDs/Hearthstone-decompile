using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIL_Dungeon : GIL_MissionEntity
{
	private static readonly string m_GennGreymane_BigQuote = "Greymane_BrassRing_Quote.prefab:3e16b31a3b009ad468fa76462c5eda3b";

	private static readonly string m_GennGreymane_Quote = "Greymane_Banner_Quote.prefab:cee4fc7a3f6bdd1439db34d534f85d5c";

	private static readonly AssetReference VO_GIL_692_Male_Worgen_TUT_FightBegin1Crowley_01 = new AssetReference("VO_GIL_692_Male_Worgen_TUT_FightBegin1Crowley_01.prefab:3352fbe060719c646a4b143495a2d04e");

	private static readonly AssetReference VO_GIL_692_Male_Worgen_TUT_FightBegin1Shaw_01 = new AssetReference("VO_GIL_692_Male_Worgen_TUT_FightBegin1Shaw_01.prefab:7f2afa4db1da44549b15a22e58b3d1c0");

	private static readonly AssetReference VO_GIL_692_Male_Worgen_TUT_FightBegin1Tess_01 = new AssetReference("VO_GIL_692_Male_Worgen_TUT_FightBegin1Tess_01.prefab:ce366f4649081ed42aec86a6291f14b4");

	private static readonly AssetReference VO_GIL_692_Male_Worgen_TUT_FightBegin1Tess_02 = new AssetReference("VO_GIL_692_Male_Worgen_TUT_FightBegin1Tess_02.prefab:81cc5d4027d42c04aa6e95ebca7d858a");

	private static readonly AssetReference VO_GIL_692_Male_Worgen_TUT_FightBegin1Toki_01 = new AssetReference("VO_GIL_692_Male_Worgen_TUT_FightBegin1Toki_01.prefab:7514dbda27d99a0418a8f764d0c07d26");

	private static readonly AssetReference VO_GIL_692_Male_Worgen_TUT_FightBegin2_01 = new AssetReference("VO_GIL_692_Male_Worgen_TUT_FightBegin2_01.prefab:1f5d9fa8502dfdc46ad78744f2f9ea57");

	private static readonly AssetReference VO_GIL_692_Male_Worgen_TUT_Defeat1_01 = new AssetReference("VO_GIL_692_Male_Worgen_TUT_Defeat1_01.prefab:6e4828354338f134fa115aff3c02fb85");

	private const AdventureDbId AdventureId = AdventureDbId.GIL;

	private GameSaveKeyId m_gameSaveDataClientKey;

	private long m_hasSeenInGameLoseVO;

	private long m_hasSeenInGameMulliganVO;

	private List<GameSaveKeySubkeyId> m_inGameSubkeysToSave = new List<GameSaveKeySubkeyId>();

	public GIL_Dungeon()
	{
		AdventureDataDbfRecord record = GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == 423);
		m_gameSaveDataClientKey = (GameSaveKeyId)record.GameSaveDataClientKey;
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_VO, out m_hasSeenInGameLoseVO);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_1_VO, out m_hasSeenInGameMulliganVO);
	}

	public override void PreloadAssets()
	{
		if (!Options.Get().GetBool(Option.HAS_SEEN_PLAYED_DARIUS))
		{
			PreloadSound(VO_GIL_692_Male_Worgen_TUT_FightBegin1Crowley_01);
		}
		if (!Options.Get().GetBool(Option.HAS_SEEN_PLAYED_TESS))
		{
			PreloadSound(VO_GIL_692_Male_Worgen_TUT_FightBegin1Tess_01);
			PreloadSound(VO_GIL_692_Male_Worgen_TUT_FightBegin1Tess_02);
		}
		if (!Options.Get().GetBool(Option.HAS_SEEN_PLAYED_SHAW))
		{
			PreloadSound(VO_GIL_692_Male_Worgen_TUT_FightBegin1Shaw_01);
		}
		if (!Options.Get().GetBool(Option.HAS_SEEN_PLAYED_TOKI))
		{
			PreloadSound(VO_GIL_692_Male_Worgen_TUT_FightBegin1Toki_01);
		}
		if (m_hasSeenInGameMulliganVO == 0L)
		{
			PreloadSound(VO_GIL_692_Male_Worgen_TUT_FightBegin2_01);
		}
		if (!Options.Get().GetBool(Option.HAS_SEEN_LOOT_IN_GAME_LOSE_VO))
		{
			PreloadSound(VO_GIL_692_Male_Worgen_TUT_Defeat1_01);
		}
	}

	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_GILMulligan);
		}
	}

	public static GIL_Dungeon InstantiateGilDungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		switch (opposingHeroCardID)
		{
		case "GILA_BOSS_20h":
			return new GIL_Dungeon_Boss_20h();
		case "GILA_BOSS_21h":
			return new GIL_Dungeon_Boss_21h();
		case "GILA_BOSS_22h":
			return new GIL_Dungeon_Boss_22h();
		case "GILA_BOSS_23h":
			return new GIL_Dungeon_Boss_23h();
		case "GILA_BOSS_24h":
			return new GIL_Dungeon_Boss_24h();
		case "GILA_BOSS_25h":
			return new GIL_Dungeon_Boss_25h();
		case "GILA_BOSS_26h":
			return new GIL_Dungeon_Boss_26h();
		case "GILA_BOSS_27h":
			return new GIL_Dungeon_Boss_27h();
		case "GILA_BOSS_29h":
			return new GIL_Dungeon_Boss_29h();
		case "GILA_BOSS_30h":
			return new GIL_Dungeon_Boss_30h();
		case "GILA_BOSS_31h":
			return new GIL_Dungeon_Boss_31h();
		case "GILA_BOSS_32h":
			return new GIL_Dungeon_Boss_32h();
		case "GILA_BOSS_33h":
			return new GIL_Dungeon_Boss_33h();
		case "GILA_BOSS_34h":
			return new GIL_Dungeon_Boss_34h();
		case "GILA_BOSS_35h":
			return new GIL_Dungeon_Boss_35h();
		case "GILA_BOSS_36h":
			return new GIL_Dungeon_Boss_36h();
		case "GILA_BOSS_37h":
			return new GIL_Dungeon_Boss_37h();
		case "GILA_BOSS_38h":
			return new GIL_Dungeon_Boss_38h();
		case "GILA_BOSS_39h":
			return new GIL_Dungeon_Boss_39h();
		case "GILA_BOSS_40h":
			return new GIL_Dungeon_Boss_40h();
		case "GILA_BOSS_41h":
			return new GIL_Dungeon_Boss_41h();
		case "GILA_BOSS_42h":
			return new GIL_Dungeon_Boss_42h();
		case "GILA_BOSS_43h":
			return new GIL_Dungeon_Boss_43h();
		case "GILA_BOSS_44h":
			return new GIL_Dungeon_Boss_44h();
		case "GILA_BOSS_45h":
			return new GIL_Dungeon_Boss_45h();
		case "GILA_BOSS_46h":
			return new GIL_Dungeon_Boss_46h();
		case "GILA_BOSS_47h":
			return new GIL_Dungeon_Boss_47h();
		case "GILA_BOSS_48h":
			return new GIL_Dungeon_Boss_48h();
		case "GILA_BOSS_49h":
			return new GIL_Dungeon_Boss_49h();
		case "GILA_BOSS_50h":
			return new GIL_Dungeon_Boss_50h();
		case "GILA_BOSS_51h":
			return new GIL_Dungeon_Boss_51h();
		case "GILA_BOSS_52h":
			return new GIL_Dungeon_Boss_52h();
		case "GILA_BOSS_52h2":
			return new GIL_Dungeon_Boss_52h();
		case "GILA_BOSS_54h":
			return new GIL_Dungeon_Boss_54h();
		case "GILA_BOSS_55h":
			return new GIL_Dungeon_Boss_55h();
		case "GILA_BOSS_56h":
			return new GIL_Dungeon_Boss_56h();
		case "GILA_BOSS_57h":
			return new GIL_Dungeon_Boss_57h();
		case "GILA_BOSS_58h":
			return new GIL_Dungeon_Boss_58h();
		case "GILA_BOSS_59h":
			return new GIL_Dungeon_Boss_59h();
		case "GILA_BOSS_60h":
			return new GIL_Dungeon_Boss_60h();
		case "GILA_BOSS_62h":
			return new GIL_Dungeon_Boss_62h();
		case "GILA_BOSS_63h":
			return new GIL_Dungeon_Boss_63h();
		case "GILA_BOSS_64h":
			return new GIL_Dungeon_Boss_64h();
		case "GILA_BOSS_65h":
			return new GIL_Dungeon_Boss_65h();
		case "GILA_BOSS_66h":
			return new GIL_Dungeon_Boss_66h();
		case "GILA_BOSS_67h":
			return new GIL_Dungeon_Boss_67h();
		case "GILA_BOSS_68h":
			return new GIL_Dungeon_Boss_68h();
		case "GILA_BOSS_61h":
			return new GIL_Dungeon_Bonus_Boss_61h();
		default:
			Log.All.PrintError("GIL_Dungeon.InstantiateGILDungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", opposingHeroCardID);
			return new GIL_Dungeon();
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

	protected override float ChanceToPlayRandomVOLine()
	{
		return 0.5f;
	}

	protected virtual void OnBossHeroPowerPlayed(Entity entity)
	{
		float num = ChanceToPlayBossHeroPowerVOLine();
		float num2 = Random.Range(0f, 1f);
		if (m_enemySpeaking || num < num2)
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
		if (turn != 1 || GameState.Get() == null || GameState.Get().GetFriendlySidePlayer() == null || GameState.Get().GetFriendlySidePlayer().GetHero() == null)
		{
			yield break;
		}
		bool hasPlayedLineThisGame = false;
		switch (GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId())
		{
		case "GILA_500h3":
			if (!Options.Get().GetBool(Option.HAS_SEEN_PLAYED_TESS))
			{
				Options.Get().SetBool(Option.HAS_SEEN_PLAYED_TESS, val: true);
				hasPlayedLineThisGame = true;
				yield return PlayBossLine(m_GennGreymane_BigQuote, VO_GIL_692_Male_Worgen_TUT_FightBegin1Tess_01);
				yield return PlayBossLine(m_GennGreymane_BigQuote, VO_GIL_692_Male_Worgen_TUT_FightBegin1Tess_02);
			}
			break;
		case "GILA_600h":
			if (!Options.Get().GetBool(Option.HAS_SEEN_PLAYED_DARIUS))
			{
				Options.Get().SetBool(Option.HAS_SEEN_PLAYED_DARIUS, val: true);
				hasPlayedLineThisGame = true;
				yield return PlayBossLine(m_GennGreymane_BigQuote, VO_GIL_692_Male_Worgen_TUT_FightBegin1Crowley_01);
			}
			break;
		case "GILA_400h":
			if (!Options.Get().GetBool(Option.HAS_SEEN_PLAYED_SHAW))
			{
				Options.Get().SetBool(Option.HAS_SEEN_PLAYED_SHAW, val: true);
				hasPlayedLineThisGame = true;
				yield return PlayBossLine(m_GennGreymane_BigQuote, VO_GIL_692_Male_Worgen_TUT_FightBegin1Shaw_01);
			}
			break;
		case "GILA_900h":
			if (!Options.Get().GetBool(Option.HAS_SEEN_PLAYED_TOKI))
			{
				Options.Get().SetBool(Option.HAS_SEEN_PLAYED_TOKI, val: true);
				hasPlayedLineThisGame = true;
				yield return PlayBossLine(m_GennGreymane_BigQuote, VO_GIL_692_Male_Worgen_TUT_FightBegin1Toki_01);
			}
			break;
		}
		if (m_hasSeenInGameMulliganVO == 0L && !hasPlayedLineThisGame)
		{
			m_inGameSubkeysToSave.Add(GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_1_VO);
			yield return PlayBossLine(m_GennGreymane_BigQuote, VO_GIL_692_Male_Worgen_TUT_FightBegin2_01);
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
		if (gameResult == TAG_PLAYSTATE.LOST && m_hasSeenInGameLoseVO == 0L)
		{
			yield return new WaitForSeconds(5f);
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_VO, 1L));
			yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait(m_GennGreymane_Quote, VO_GIL_692_Male_Worgen_TUT_Defeat1_01));
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

	public override void NotifyOfResetGameFinished(Entity source, Entity oldGameEntity)
	{
		base.NotifyOfResetGameFinished(source, oldGameEntity);
		GIL_Dungeon gIL_Dungeon = oldGameEntity as GIL_Dungeon;
		if (gIL_Dungeon != null)
		{
			m_inGameSubkeysToSave = new List<GameSaveKeySubkeyId>(gIL_Dungeon.m_inGameSubkeysToSave);
		}
	}
}
