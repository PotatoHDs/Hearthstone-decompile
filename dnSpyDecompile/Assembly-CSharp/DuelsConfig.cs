using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000629 RID: 1577
public class DuelsConfig
{
	// Token: 0x06005838 RID: 22584 RVA: 0x001CD566 File Offset: 0x001CB766
	public static DuelsConfig Get()
	{
		if (DuelsConfig.m_instance == null)
		{
			DuelsConfig.m_instance = new DuelsConfig();
		}
		return DuelsConfig.m_instance;
	}

	// Token: 0x06005839 RID: 22585 RVA: 0x001CD57E File Offset: 0x001CB77E
	public void SetLastGameResult(TAG_PLAYSTATE lastGameState)
	{
		this.m_recentLoss = (lastGameState == TAG_PLAYSTATE.LOST);
		this.m_recentWin = (lastGameState == TAG_PLAYSTATE.WON);
	}

	// Token: 0x0600583A RID: 22586 RVA: 0x001CD594 File Offset: 0x001CB794
	public void ResetLastGameResult()
	{
		this.m_recentLoss = false;
		this.m_recentWin = false;
	}

	// Token: 0x0600583B RID: 22587 RVA: 0x001CD5A4 File Offset: 0x001CB7A4
	public bool HasRecentWin()
	{
		return this.m_recentWin;
	}

	// Token: 0x0600583C RID: 22588 RVA: 0x001CD5AC File Offset: 0x001CB7AC
	public bool HasRecentLoss()
	{
		return this.m_recentLoss;
	}

	// Token: 0x0600583D RID: 22589 RVA: 0x001CD5B4 File Offset: 0x001CB7B4
	public void SetRecentEnd(bool value)
	{
		this.m_recentRunEnd = value;
	}

	// Token: 0x0600583E RID: 22590 RVA: 0x001CD5BD File Offset: 0x001CB7BD
	public bool RunRecentlyEnded()
	{
		return this.m_recentRunEnd;
	}

	// Token: 0x17000529 RID: 1321
	// (get) Token: 0x0600583F RID: 22591 RVA: 0x001CD5C5 File Offset: 0x001CB7C5
	// (set) Token: 0x06005840 RID: 22592 RVA: 0x001CD5CD File Offset: 0x001CB7CD
	public int LastRunWins
	{
		get
		{
			return this.m_lastRunWins;
		}
		set
		{
			this.m_lastRunWins = value;
		}
	}

	// Token: 0x06005841 RID: 22593 RVA: 0x001CD5D6 File Offset: 0x001CB7D6
	public NetCache.ProfileNoticeGenericRewardChest GetRewardNoticeToShow()
	{
		return (NetCache.ProfileNoticeGenericRewardChest)NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>().Notices.Find((NetCache.ProfileNotice obj) => obj.Type == NetCache.ProfileNotice.NoticeType.GENERIC_REWARD_CHEST && obj.Origin == NetCache.ProfileNotice.NoticeOrigin.NOTICE_ORIGIN_DUELS);
	}

	// Token: 0x06005842 RID: 22594 RVA: 0x001CD610 File Offset: 0x001CB810
	public bool IsReadyToShowRewards()
	{
		AdventureDungeonCrawlDisplay adventureDungeonCrawlDisplay = AdventureDungeonCrawlDisplay.Get();
		return adventureDungeonCrawlDisplay != null && adventureDungeonCrawlDisplay.m_playMat != null && adventureDungeonCrawlDisplay.m_playMat.IsReadyToShowDuelsRewards();
	}

	// Token: 0x06005843 RID: 22595 RVA: 0x001CD648 File Offset: 0x001CB848
	public void ShowRewardsForNotice(NetCache.ProfileNoticeGenericRewardChest notice, Action doneCallback, Transform bone = null)
	{
		if (notice == null)
		{
			Log.All.PrintError("DuelsConfig.ShowRewards - Trying to display invalid reward notice", Array.Empty<object>());
			return;
		}
		RewardUtils.ShowRewardBoxes(RewardUtils.GetRewards(new List<NetCache.ProfileNotice>
		{
			notice
		}), delegate
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

	// Token: 0x06005844 RID: 22596 RVA: 0x001CD6AE File Offset: 0x001CB8AE
	public static bool HasEarlyAccess()
	{
		return AccountLicenseMgr.Get().OwnsAccountLicense(NetCache.Get().GetDuelsEarlyAccessLicenseId());
	}

	// Token: 0x06005845 RID: 22597 RVA: 0x001CD6C4 File Offset: 0x001CB8C4
	public static bool IsEarlyAccess()
	{
		return SpecialEventManager.Get().IsEventActive(DuelsConfig.EARLY_ACCESS_EVENT, false);
	}

	// Token: 0x06005846 RID: 22598 RVA: 0x001CD6D6 File Offset: 0x001CB8D6
	public static bool IsFreeUnlocked()
	{
		return NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.GetFeatureFlag(NetCache.NetCacheFeatures.CacheGames.FeatureFlags.Duels);
	}

	// Token: 0x06005847 RID: 22599 RVA: 0x001CD6EE File Offset: 0x001CB8EE
	public static bool IsPaidUnlocked()
	{
		return NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Games.GetFeatureFlag(NetCache.NetCacheFeatures.CacheGames.FeatureFlags.PaidDuels) && SpecialEventManager.Get().IsEventActive(DuelsConfig.PAID_UNLOCKED_EVENT, false);
	}

	// Token: 0x06005848 RID: 22600 RVA: 0x001CD71A File Offset: 0x001CB91A
	public static bool IsCardLoadoutTreasure(string cardID)
	{
		return SceneMgr.Get().IsInDuelsMode() && AdventureDungeonCrawlDisplay.Get() != null && AdventureDungeonCrawlDisplay.Get().IsCardLoadoutTreasure(cardID);
	}

	// Token: 0x06005849 RID: 22601 RVA: 0x001CD744 File Offset: 0x001CB944
	public static List<long> GetDraftHeroesFromGSD()
	{
		List<long> result = null;
		if (PvPDungeonRunScene.Get() != null)
		{
			GameSaveKeyId gsdkeyForAdventure = PvPDungeonRunScene.Get().GetGSDKeyForAdventure();
			GameSaveDataManager.Get().GetSubkeyValue(gsdkeyForAdventure, GameSaveKeySubkeyId.DUELS_DRAFT_HERO_CHOICES, out result);
		}
		return result;
	}

	// Token: 0x0600584A RID: 22602 RVA: 0x001CD77F File Offset: 0x001CB97F
	public static PvpdrSeasonDbfRecord GetSeasonDBFRecord()
	{
		if (PvPDungeonRunScene.Get() != null)
		{
			return GameDbf.PvpdrSeason.GetRecord(PvPDungeonRunScene.Get().GetSeasonID());
		}
		return null;
	}

	// Token: 0x0600584B RID: 22603 RVA: 0x001CD7A4 File Offset: 0x001CB9A4
	public static bool IsInitialLoadoutComplete()
	{
		long num = 0L;
		long num2 = 0L;
		long num3 = 0L;
		long num4 = 0L;
		if (PvPDungeonRunScene.Get() != null)
		{
			GameSaveKeyId gsdkeyForAdventure = PvPDungeonRunScene.Get().GetGSDKeyForAdventure();
			GameSaveDataManager.Get().GetSubkeyValue(gsdkeyForAdventure, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, out num);
			GameSaveDataManager.Get().GetSubkeyValue(gsdkeyForAdventure, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_CLASS, out num2);
			GameSaveDataManager.Get().GetSubkeyValue(gsdkeyForAdventure, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_LOADOUT_TREASURE_ID, out num3);
			GameSaveDataManager.Get().GetSubkeyValue(gsdkeyForAdventure, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_POWER, out num4);
		}
		return num > 0L || (num2 > 0L && num3 > 0L && num4 > 0L);
	}

	// Token: 0x0600584C RID: 22604 RVA: 0x001CD83B File Offset: 0x001CBA3B
	public static bool CanImportDecks()
	{
		return PvPDungeonRunScene.IsEditingDeck();
	}

	// Token: 0x0600584D RID: 22605 RVA: 0x001CD844 File Offset: 0x001CBA44
	public static List<TAG_CARD_SET> GetDuelsSets()
	{
		List<TAG_CARD_SET> list = new List<TAG_CARD_SET>();
		DeckRuleset pvpdrruleset = DeckRuleset.GetPVPDRRuleset();
		if (pvpdrruleset != null)
		{
			HashSet<TAG_CARD_SET> allowedCardSets = pvpdrruleset.GetAllowedCardSets();
			foreach (TAG_CARD_SET item in CollectionManager.Get().GetDisplayableCardSets())
			{
				if (allowedCardSets.Contains(item))
				{
					list.Add(item);
				}
			}
			list.Reverse();
		}
		return list;
	}

	// Token: 0x0600584E RID: 22606 RVA: 0x001CD8C4 File Offset: 0x001CBAC4
	public static int GetAdventureIdForSeason(int seasonId)
	{
		PvpdrSeasonDbfRecord record = GameDbf.PvpdrSeason.GetRecord(seasonId);
		if (record == null)
		{
			return 0;
		}
		return record.AdventureId;
	}

	// Token: 0x04004BA7 RID: 19367
	public static string EARLY_ACCESS_EVENT = "pvpdr_early_access";

	// Token: 0x04004BA8 RID: 19368
	public static string PAID_UNLOCKED_EVENT = "pvpdr_paid_unlocked";

	// Token: 0x04004BA9 RID: 19369
	public static int PAID_GOLD_COST = 150;

	// Token: 0x04004BAA RID: 19370
	private static DuelsConfig m_instance;

	// Token: 0x04004BAB RID: 19371
	public static string DOOR_LEVEL_CLICKED = "DOOR_LEVER_CLICKED";

	// Token: 0x04004BAC RID: 19372
	public static string DOOR_OPENED_EVENT = "DOOR_OPENED";

	// Token: 0x04004BAD RID: 19373
	public static string LEVER_GLOW_STATE = "GLOW";

	// Token: 0x04004BAE RID: 19374
	public static string ANIMATE_PAID_STATE = "ANIMATE_PAID";

	// Token: 0x04004BAF RID: 19375
	public static string ANIMATE_FREE_STATE = "ANIMATE_FREE";

	// Token: 0x04004BB0 RID: 19376
	private bool m_recentLoss;

	// Token: 0x04004BB1 RID: 19377
	private bool m_recentWin;

	// Token: 0x04004BB2 RID: 19378
	private bool m_recentRunEnd;

	// Token: 0x04004BB3 RID: 19379
	private int m_lastRunWins;

	// Token: 0x0200212B RID: 8491
	public enum DuelsModeTypes
	{
		// Token: 0x0400DF67 RID: 57191
		INVALID,
		// Token: 0x0400DF68 RID: 57192
		CASUAL,
		// Token: 0x0400DF69 RID: 57193
		HEROIC
	}
}
