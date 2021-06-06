using System;
using System.Collections.Generic;
using UnityEngine;

public class DuelsConfig
{
	public enum DuelsModeTypes
	{
		INVALID,
		CASUAL,
		HEROIC
	}

	public static string EARLY_ACCESS_EVENT = "pvpdr_early_access";

	public static string PAID_UNLOCKED_EVENT = "pvpdr_paid_unlocked";

	public static int PAID_GOLD_COST = 150;

	private static DuelsConfig m_instance;

	public static string DOOR_LEVEL_CLICKED = "DOOR_LEVER_CLICKED";

	public static string DOOR_OPENED_EVENT = "DOOR_OPENED";

	public static string LEVER_GLOW_STATE = "GLOW";

	public static string ANIMATE_PAID_STATE = "ANIMATE_PAID";

	public static string ANIMATE_FREE_STATE = "ANIMATE_FREE";

	private bool m_recentLoss;

	private bool m_recentWin;

	private bool m_recentRunEnd;

	private int m_lastRunWins;

	public int LastRunWins
	{
		get
		{
			return m_lastRunWins;
		}
		set
		{
			m_lastRunWins = value;
		}
	}

	public static DuelsConfig Get()
	{
		if (m_instance == null)
		{
			m_instance = new DuelsConfig();
		}
		return m_instance;
	}

	public void SetLastGameResult(TAG_PLAYSTATE lastGameState)
	{
		m_recentLoss = lastGameState == TAG_PLAYSTATE.LOST;
		m_recentWin = lastGameState == TAG_PLAYSTATE.WON;
	}

	public void ResetLastGameResult()
	{
		m_recentLoss = false;
		m_recentWin = false;
	}

	public bool HasRecentWin()
	{
		return m_recentWin;
	}

	public bool HasRecentLoss()
	{
		return m_recentLoss;
	}

	public void SetRecentEnd(bool value)
	{
		m_recentRunEnd = value;
	}

	public bool RunRecentlyEnded()
	{
		return m_recentRunEnd;
	}

	public NetCache.ProfileNoticeGenericRewardChest GetRewardNoticeToShow()
	{
		return (NetCache.ProfileNoticeGenericRewardChest)NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>().Notices.Find((NetCache.ProfileNotice obj) => obj.Type == NetCache.ProfileNotice.NoticeType.GENERIC_REWARD_CHEST && obj.Origin == NetCache.ProfileNotice.NoticeOrigin.NOTICE_ORIGIN_DUELS);
	}

	public bool IsReadyToShowRewards()
	{
		AdventureDungeonCrawlDisplay adventureDungeonCrawlDisplay = AdventureDungeonCrawlDisplay.Get();
		if (adventureDungeonCrawlDisplay != null && adventureDungeonCrawlDisplay.m_playMat != null)
		{
			return adventureDungeonCrawlDisplay.m_playMat.IsReadyToShowDuelsRewards();
		}
		return false;
	}

	public void ShowRewardsForNotice(NetCache.ProfileNoticeGenericRewardChest notice, Action doneCallback, Transform bone = null)
	{
		if (notice == null)
		{
			Log.All.PrintError("DuelsConfig.ShowRewards - Trying to display invalid reward notice");
			return;
		}
		RewardUtils.ShowRewardBoxes(RewardUtils.GetRewards(new List<NetCache.ProfileNotice> { notice }), delegate
		{
			AdventureDungeonCrawlDisplay adventureDungeonCrawlDisplay = AdventureDungeonCrawlDisplay.Get();
			if (adventureDungeonCrawlDisplay != null)
			{
				adventureDungeonCrawlDisplay.m_playMat.OnDuelsRewardsAccepted();
				adventureDungeonCrawlDisplay.EndDuelsSession(notice.NoticeID);
			}
			doneCallback();
		}, bone);
	}

	public static bool HasEarlyAccess()
	{
		return AccountLicenseMgr.Get().OwnsAccountLicense(NetCache.Get().GetDuelsEarlyAccessLicenseId());
	}

	public static bool IsEarlyAccess()
	{
		return SpecialEventManager.Get().IsEventActive(EARLY_ACCESS_EVENT, activeIfDoesNotExist: false);
	}

	public static bool IsFreeUnlocked()
	{
		return NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.GetFeatureFlag(NetCache.NetCacheFeatures.CacheGames.FeatureFlags.Duels);
	}

	public static bool IsPaidUnlocked()
	{
		if (NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.GetFeatureFlag(NetCache.NetCacheFeatures.CacheGames.FeatureFlags.PaidDuels))
		{
			return SpecialEventManager.Get().IsEventActive(PAID_UNLOCKED_EVENT, activeIfDoesNotExist: false);
		}
		return false;
	}

	public static bool IsCardLoadoutTreasure(string cardID)
	{
		if (SceneMgr.Get().IsInDuelsMode() && AdventureDungeonCrawlDisplay.Get() != null)
		{
			return AdventureDungeonCrawlDisplay.Get().IsCardLoadoutTreasure(cardID);
		}
		return false;
	}

	public static List<long> GetDraftHeroesFromGSD()
	{
		List<long> values = null;
		if (PvPDungeonRunScene.Get() != null)
		{
			GameSaveKeyId gSDKeyForAdventure = PvPDungeonRunScene.Get().GetGSDKeyForAdventure();
			GameSaveDataManager.Get().GetSubkeyValue(gSDKeyForAdventure, GameSaveKeySubkeyId.DUELS_DRAFT_HERO_CHOICES, out values);
		}
		return values;
	}

	public static PvpdrSeasonDbfRecord GetSeasonDBFRecord()
	{
		if (PvPDungeonRunScene.Get() != null)
		{
			return GameDbf.PvpdrSeason.GetRecord(PvPDungeonRunScene.Get().GetSeasonID());
		}
		return null;
	}

	public static bool IsInitialLoadoutComplete()
	{
		long value = 0L;
		long value2 = 0L;
		long value3 = 0L;
		long value4 = 0L;
		if (PvPDungeonRunScene.Get() != null)
		{
			GameSaveKeyId gSDKeyForAdventure = PvPDungeonRunScene.Get().GetGSDKeyForAdventure();
			GameSaveDataManager.Get().GetSubkeyValue(gSDKeyForAdventure, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, out value);
			GameSaveDataManager.Get().GetSubkeyValue(gSDKeyForAdventure, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_CLASS, out value2);
			GameSaveDataManager.Get().GetSubkeyValue(gSDKeyForAdventure, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_LOADOUT_TREASURE_ID, out value3);
			GameSaveDataManager.Get().GetSubkeyValue(gSDKeyForAdventure, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_POWER, out value4);
		}
		if (value <= 0)
		{
			if (value2 > 0 && value3 > 0)
			{
				return value4 > 0;
			}
			return false;
		}
		return true;
	}

	public static bool CanImportDecks()
	{
		return PvPDungeonRunScene.IsEditingDeck();
	}

	public static List<TAG_CARD_SET> GetDuelsSets()
	{
		List<TAG_CARD_SET> list = new List<TAG_CARD_SET>();
		DeckRuleset pVPDRRuleset = DeckRuleset.GetPVPDRRuleset();
		if (pVPDRRuleset != null)
		{
			HashSet<TAG_CARD_SET> allowedCardSets = pVPDRRuleset.GetAllowedCardSets();
			foreach (TAG_CARD_SET displayableCardSet in CollectionManager.Get().GetDisplayableCardSets())
			{
				if (allowedCardSets.Contains(displayableCardSet))
				{
					list.Add(displayableCardSet);
				}
			}
			list.Reverse();
		}
		return list;
	}

	public static int GetAdventureIdForSeason(int seasonId)
	{
		return GameDbf.PvpdrSeason.GetRecord(seasonId)?.AdventureId ?? 0;
	}
}
