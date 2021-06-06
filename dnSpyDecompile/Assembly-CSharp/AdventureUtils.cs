using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.DungeonCrawl;
using Hearthstone.Progression;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000052 RID: 82
public class AdventureUtils
{
	// Token: 0x06000499 RID: 1177 RVA: 0x0001B5DC File Offset: 0x000197DC
	public static List<AdventureLoadoutTreasuresDbfRecord> GetLoadoutTreasuresForAdventureAndClass(AdventureDbId adventure, TAG_CLASS classId)
	{
		List<AdventureLoadoutTreasuresDbfRecord> records = GameDbf.AdventureLoadoutTreasures.GetRecords((AdventureLoadoutTreasuresDbfRecord r) => r.AdventureId == (int)adventure && r.ClassId == (int)classId, -1);
		records.Sort((AdventureLoadoutTreasuresDbfRecord a, AdventureLoadoutTreasuresDbfRecord b) => a.SortOrder.CompareTo(b.SortOrder));
		return records;
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x0001B63C File Offset: 0x0001983C
	public static List<AdventureHeroPowerDbfRecord> GetHeroPowersForAdventureAndClass(AdventureDbId adventure, TAG_CLASS classId)
	{
		List<AdventureHeroPowerDbfRecord> records = GameDbf.AdventureHeroPower.GetRecords((AdventureHeroPowerDbfRecord r) => r.AdventureId == (int)adventure && r.ClassId == (int)classId, -1);
		records.Sort((AdventureHeroPowerDbfRecord a, AdventureHeroPowerDbfRecord b) => a.SortOrder.CompareTo(b.SortOrder));
		return records;
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x0001B69C File Offset: 0x0001989C
	public static List<AdventureDeckDbfRecord> GetDecksForAdventureAndClass(AdventureDbId adventure, TAG_CLASS classId)
	{
		List<AdventureDeckDbfRecord> records = GameDbf.AdventureDeck.GetRecords((AdventureDeckDbfRecord r) => r.AdventureId == (int)adventure && r.ClassId == (int)classId, -1);
		records.Sort((AdventureDeckDbfRecord a, AdventureDeckDbfRecord b) => a.SortOrder.CompareTo(b.SortOrder));
		return records;
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x0001B6F9 File Offset: 0x000198F9
	public static bool AdventureHeroPowerIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureHeroPowerDbfRecord heroPowerRecord, out long unlockProgress, out bool hasUnlockCriteria)
	{
		return AdventureUtils.AdventureRewardIsUnlocked(gameSaveServerKey, (GameSaveKeySubkeyId)heroPowerRecord.UnlockGameSaveSubkey, heroPowerRecord.UnlockValue, heroPowerRecord.UnlockAchievement, out unlockProgress, out hasUnlockCriteria);
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x0001B715 File Offset: 0x00019915
	public static bool AdventureDeckIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureDeckDbfRecord deckRecord, out long unlockProgress, out bool hasUnlockCriteria)
	{
		return AdventureUtils.AdventureRewardIsUnlocked(gameSaveServerKey, (GameSaveKeySubkeyId)deckRecord.UnlockGameSaveSubkey, deckRecord.UnlockValue, 0, out unlockProgress, out hasUnlockCriteria);
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x0001B72C File Offset: 0x0001992C
	public static bool AdventureTreasureIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureLoadoutTreasuresDbfRecord treasureLoadoutRecord, out long unlockProgress, out bool hasUnlockCriteria)
	{
		return AdventureUtils.AdventureRewardIsUnlocked(gameSaveServerKey, (GameSaveKeySubkeyId)treasureLoadoutRecord.UnlockGameSaveSubkey, treasureLoadoutRecord.UnlockValue, treasureLoadoutRecord.UnlockAchievement, out unlockProgress, out hasUnlockCriteria);
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x0001B748 File Offset: 0x00019948
	public static bool AdventureTreasureIsUpgraded(GameSaveKeyId gameSaveServerKey, AdventureLoadoutTreasuresDbfRecord treasureLoadoutRecord, out long upgradeProgress)
	{
		bool flag;
		return AdventureUtils.AdventureRewardIsUnlocked(gameSaveServerKey, (GameSaveKeySubkeyId)treasureLoadoutRecord.UpgradeGameSaveSubkey, treasureLoadoutRecord.UpgradeValue, 0, out upgradeProgress, out flag);
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x0001B76C File Offset: 0x0001996C
	public static bool AdventureRewardIsUnlocked(GameSaveKeyId gameSaveServerKey, GameSaveKeySubkeyId unlockGameSaveSubkey, int unlockValue, int unlockAchievement, out long unlockProgress, out bool hasUnlockCriteria)
	{
		unlockProgress = 0L;
		hasUnlockCriteria = true;
		if (unlockAchievement <= 0 && unlockGameSaveSubkey <= (GameSaveKeySubkeyId)0)
		{
			hasUnlockCriteria = false;
			return true;
		}
		bool flag = false;
		int num = 0;
		if (unlockAchievement > 0)
		{
			flag = AchievementManager.Get().IsAchievementComplete(unlockAchievement);
			num = AchievementManager.Get().GetAchievementDataModel(unlockAchievement).Progress;
		}
		bool flag2 = false;
		if (unlockGameSaveSubkey > (GameSaveKeySubkeyId)0)
		{
			GameSaveDataManager.Get().GetSubkeyValue(gameSaveServerKey, unlockGameSaveSubkey, out unlockProgress);
			flag2 = (unlockProgress >= (long)unlockValue);
		}
		if (unlockGameSaveSubkey > (GameSaveKeySubkeyId)0 && unlockAchievement > 0)
		{
			unlockProgress += (long)num;
			return flag && flag2;
		}
		if (unlockAchievement > 0)
		{
			unlockProgress = (long)num;
			return flag;
		}
		return unlockGameSaveSubkey > (GameSaveKeySubkeyId)0 && flag2;
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x0001B7FC File Offset: 0x000199FC
	public static int GetFinalAdventureWing(int adventureId, bool excludeOwnedWings, bool excludeInactiveWings = false)
	{
		int num = -1;
		int result = 0;
		foreach (WingDbfRecord wingDbfRecord in GameDbf.Wing.GetRecords())
		{
			if (wingDbfRecord.AdventureId == adventureId && wingDbfRecord.UnlockOrder > num && (!excludeOwnedWings || !AdventureProgressMgr.Get().OwnsWing(wingDbfRecord.ID)) && (!excludeInactiveWings || AdventureProgressMgr.IsWingEventActive(wingDbfRecord.ID)))
			{
				num = wingDbfRecord.UnlockOrder;
				result = wingDbfRecord.ID;
			}
		}
		return result;
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x0001B898 File Offset: 0x00019A98
	public static bool IsAnomalyModeAvailable(AdventureDbId adventureDbId, AdventureModeDbId modeDbId, WingDbId wingDbId)
	{
		return !AdventureUtils.IsAnomalyModeLocked(adventureDbId, modeDbId) && AdventureUtils.IsAnomalyModeAllowed(wingDbId);
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x0001B8AC File Offset: 0x00019AAC
	public static bool IsAnomalyModeLocked(AdventureDbId adventureDbId, AdventureModeDbId modeDbId)
	{
		foreach (ScenarioDbfRecord scenarioDbfRecord in GameDbf.Scenario.GetRecords((ScenarioDbfRecord r) => r.AdventureId == (int)adventureDbId && r.ModeId == (int)modeDbId && r.WingId != 0, -1))
		{
			if (!AdventureProgressMgr.Get().OwnsWing(scenarioDbfRecord.WingId))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x0001B938 File Offset: 0x00019B38
	public static bool IsAnomalyModeAllowed(WingDbId wingDbId)
	{
		WingDbfRecord record = GameDbf.Wing.GetRecord((int)wingDbId);
		return record != null && record.AllowsAnomaly;
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x0001B960 File Offset: 0x00019B60
	public static AdventureDbId GetAdventureIdForWing(WingDbId wingDbId)
	{
		WingDbfRecord record = GameDbf.Wing.GetRecord((int)wingDbId);
		if (record == null)
		{
			return AdventureDbId.INVALID;
		}
		return (AdventureDbId)record.AdventureId;
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x0001B984 File Offset: 0x00019B84
	public static bool IsProductTypeAnAdventureWing(ProductType type)
	{
		return type - ProductType.PRODUCT_TYPE_NAXX <= 1 || type - ProductType.PRODUCT_TYPE_LOE <= 1;
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x0001B995 File Offset: 0x00019B95
	public static bool IsAdventureBundle(Network.Bundle bundle)
	{
		return bundle.Items.Any((Network.BundleItem item) => AdventureUtils.IsProductTypeAnAdventureWing(item.ItemType));
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x0001B9C4 File Offset: 0x00019BC4
	public static bool DoesBundleIncludeWingForAdventure(Network.Bundle bundle, AdventureDbId adventure)
	{
		return bundle.Items.Any((Network.BundleItem item) => AdventureUtils.IsProductTypeAnAdventureWing(item.ItemType) && AdventureUtils.GetAdventureIdForWing((WingDbId)item.ProductData) == adventure);
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x0001B9F8 File Offset: 0x00019BF8
	public static bool DoesBundleIncludeWing(Network.Bundle bundle, int wingId)
	{
		return bundle.Items.Any((Network.BundleItem item) => AdventureUtils.IsProductTypeAnAdventureWing(item.ItemType) && item.ProductData == wingId);
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x0001BA2C File Offset: 0x00019C2C
	public static string GetHeroCardIdFromClassForDungeonCrawl(IDungeonCrawlData dungeonCrawlData, TAG_CLASS cardClass)
	{
		List<int> list = (dungeonCrawlData != null) ? dungeonCrawlData.GetGuestHeroesForCurrentAdventure() : null;
		if (list != null && list.Count > 0)
		{
			if (cardClass == TAG_CLASS.INVALID)
			{
				cardClass = TAG_CLASS.NEUTRAL;
			}
			foreach (int num in list)
			{
				if (GameUtils.GetTagClassFromCardDbId(num) == cardClass)
				{
					CardDbfRecord record = GameDbf.Card.GetRecord(num);
					if (record != null)
					{
						return record.NoteMiniGuid;
					}
				}
			}
			return null;
		}
		NetCache.CardDefinition favoriteHero = CollectionManager.Get().GetFavoriteHero(cardClass);
		if (favoriteHero == null)
		{
			Log.Adventures.PrintError("GameUtils.GetHeroCardIdFromClassForAdventure - could not get Hero Card Id from {0}", new object[]
			{
				cardClass
			});
			return null;
		}
		return favoriteHero.Name;
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x0001BAF4 File Offset: 0x00019CF4
	public static void DisplayFirstChapterFreePopup(ChapterPageData chapterPageData, AdventureUtils.FirstChapterFreePopupCompleteCallback callback = null)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_ADVENTURE_ADVENTUREBOOK_DAL_FIRST_TIME_FLOW_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_ADVENTURE_ADVENTUREBOOK_DAL_FIRST_TIME_FLOW");
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
		popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
		{
			AdventureConfig.Get().MarkHasSeenFirstTimeFlowComplete();
			AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)AdventureConfig.Get().GetSelectedAdventure());
			if (record != null && record.MapPageHasButtonsToChapters)
			{
				AdventureBookPageManager.NavigateToMapPage();
			}
			else
			{
				AdventureConfig.AckCurrentWingProgress(chapterPageData.WingRecord.ID);
			}
			if (callback != null)
			{
				callback();
			}
		};
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x0001BB74 File Offset: 0x00019D74
	public static bool IsEntireAdventureFree(AdventureDbId adventureID)
	{
		return !GameDbf.Wing.HasRecord((WingDbfRecord r) => r.AdventureId == (int)adventureID && (r.PmtProductIdForSingleWingPurchase != 0 || r.PmtProductIdForThisAndRestOfAdventure != 0));
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x0001BBA7 File Offset: 0x00019DA7
	public static bool DoesAdventureRequireAllHeroesUnlocked(AdventureDbId adventureId)
	{
		return AdventureUtils.DoesAdventureRequireAllHeroesUnlocked(adventureId, AdventureConfig.GetDefaultModeDbIdForAdventure(adventureId));
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x0001BBB8 File Offset: 0x00019DB8
	public static bool DoesAdventureRequireAllHeroesUnlocked(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		if (adventureId == AdventureDbId.INVALID || modeId == AdventureModeDbId.INVALID)
		{
			return true;
		}
		AdventureDataDbfRecord adventureDataRecord = AdventureConfig.GetAdventureDataRecord(adventureId, modeId);
		return adventureDataRecord == null || !adventureDataRecord.IgnoreHeroUnlockRequirement;
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x0001BBE4 File Offset: 0x00019DE4
	public static List<int> GetGuestHeroesForAdventure(AdventureDbId currentAdventure)
	{
		List<AdventureGuestHeroesDbfRecord> sortedGuestHeroRecordsForAdventures = AdventureUtils.GetSortedGuestHeroRecordsForAdventures(currentAdventure);
		List<int> list = new List<int>();
		foreach (AdventureGuestHeroesDbfRecord adventureGuestHeroesDbfRecord in sortedGuestHeroRecordsForAdventures)
		{
			if (adventureGuestHeroesDbfRecord.GuestHeroId != 0)
			{
				list.Add(GameUtils.GetCardIdFromGuestHeroDbId(adventureGuestHeroesDbfRecord.GuestHeroId));
			}
		}
		return list;
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x0001BC50 File Offset: 0x00019E50
	public static List<AdventureGuestHeroesDbfRecord> GetSortedGuestHeroRecordsForAdventures(AdventureDbId currentAdventure)
	{
		List<AdventureGuestHeroesDbfRecord> records = GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)currentAdventure, -1);
		records.Sort((AdventureGuestHeroesDbfRecord a, AdventureGuestHeroesDbfRecord b) => a.SortOrder.CompareTo(b.SortOrder));
		return records;
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x0001BCA8 File Offset: 0x00019EA8
	public static CardListDataModel GetAvailableGuestHeroesAsCardListSortedByReleaseDate(AdventureDbId adventure)
	{
		CardListDataModel cardListDataModel = new CardListDataModel();
		List<AdventureGuestHeroesDbfRecord> records = GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)adventure && AdventureProgressMgr.IsWingEventActive(r.WingId), -1);
		DateTime defaultDateIfNoRecordFound = DateTime.MinValue;
		records.Sort((AdventureGuestHeroesDbfRecord a, AdventureGuestHeroesDbfRecord b) => DateTime.Compare(AdventureUtils.ReleaseDateForAdventureGuestHero(b, defaultDateIfNoRecordFound), AdventureUtils.ReleaseDateForAdventureGuestHero(a, defaultDateIfNoRecordFound)));
		foreach (AdventureGuestHeroesDbfRecord adventureGuestHeroesDbfRecord in records)
		{
			CardDbfRecord record = GameDbf.Card.GetRecord(GameUtils.GetCardIdFromGuestHeroDbId(adventureGuestHeroesDbfRecord.GuestHeroId));
			if (record != null)
			{
				CardDataModel item = new CardDataModel
				{
					CardId = record.NoteMiniGuid,
					Premium = TAG_PREMIUM.NORMAL
				};
				cardListDataModel.Cards.Add(item);
			}
		}
		return cardListDataModel;
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x0001BD7C File Offset: 0x00019F7C
	public static DateTime ReleaseDateForAdventureGuestHero(AdventureGuestHeroesDbfRecord adventureGuestHero, DateTime defaultDate)
	{
		if (adventureGuestHero.WingRecord == null)
		{
			return defaultDate;
		}
		SpecialEventType eventType = SpecialEventManager.GetEventType(adventureGuestHero.WingRecord.RequiredEvent);
		if (eventType == SpecialEventType.UNKNOWN)
		{
			return defaultDate;
		}
		DateTime? eventStartTimeUtc = SpecialEventManager.Get().GetEventStartTimeUtc(eventType);
		if (eventStartTimeUtc != null)
		{
			return eventStartTimeUtc.Value;
		}
		return defaultDate;
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x0001BDC8 File Offset: 0x00019FC8
	public static bool DoesAdventureShowNewlyUnlockedGuestHeroTreatment(AdventureDbId adventure)
	{
		if (adventure <= AdventureDbId.BLACKROCK_CRASH)
		{
			if (adventure == AdventureDbId.GIL)
			{
				return false;
			}
			if (adventure != AdventureDbId.BLACKROCK_CRASH)
			{
				return true;
			}
		}
		else if (adventure - AdventureDbId.LOE_REVIVAL > 1 && adventure - AdventureDbId.NAXX_CRASH > 1 && adventure != AdventureDbId.ROAD_TO_NORTHREND)
		{
			return true;
		}
		return false;
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x0001BE08 File Offset: 0x0001A008
	public static bool DoesAdventureHaveUnseenGuestHeroes(AdventureDbId adventure, AdventureModeDbId mode)
	{
		List<long> list = null;
		AdventureDataDbfRecord adventureDataRecord = AdventureConfig.GetAdventureDataRecord(adventure, mode);
		GameSaveDataManager.Get().GetSubkeyValue((GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_UNLOCKED_HEROES, out list);
		foreach (AdventureGuestHeroesDbfRecord adventureGuestHeroesDbfRecord in GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)adventure, -1))
		{
			if (adventureGuestHeroesDbfRecord.GuestHeroId != 0 && (list == null || !list.Contains((long)GameUtils.GetCardIdFromGuestHeroDbId(adventureGuestHeroesDbfRecord.GuestHeroId))) && AdventureProgressMgr.IsWingEventActive(adventureGuestHeroesDbfRecord.WingId) && AdventureProgressMgr.Get().OwnsWing(adventureGuestHeroesDbfRecord.WingId))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x0001BEE8 File Offset: 0x0001A0E8
	public static bool SelectableLoadoutTreasuresExistForAdventure(AdventureDbId adventure)
	{
		return GameDbf.AdventureLoadoutTreasures.HasRecord((AdventureLoadoutTreasuresDbfRecord r) => r.AdventureId == (int)adventure);
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x0001BF18 File Offset: 0x0001A118
	public static bool SelectableHeroPowersExistForAdventure(AdventureDbId adventure)
	{
		return GameDbf.AdventureHeroPower.HasRecord((AdventureHeroPowerDbfRecord r) => r.AdventureId == (int)adventure);
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x0001BF48 File Offset: 0x0001A148
	public static bool SelectableDecksExistForAdventure(AdventureDbId adventure)
	{
		return GameDbf.AdventureDeck.HasRecord((AdventureDeckDbfRecord r) => r.AdventureId == (int)adventure);
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x0001BF78 File Offset: 0x0001A178
	public static bool SelectableHeroPowersAndDecksExistForAdventure(AdventureDbId adventure)
	{
		bool flag = AdventureUtils.SelectableHeroPowersExistForAdventure(adventure);
		bool flag2 = AdventureUtils.SelectableDecksExistForAdventure(adventure);
		if (!flag && !flag2)
		{
			return false;
		}
		if (flag && flag2)
		{
			return true;
		}
		if (flag && SceneMgr.Get().GetMode() == SceneMgr.Mode.PVP_DUNGEON_RUN)
		{
			return true;
		}
		Debug.LogError(string.Format("Adventure {0} has ADVENTURE_HERO_POWER or ADVENTURE_DECK entries defined, but not both! This is not currently suported - you must have entries for both tables, so a Hero Power can be selected first, then a Deck.", adventure));
		return false;
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x0001BFCC File Offset: 0x0001A1CC
	public static bool IsMissionValidForAdventureMode(AdventureDbId adventureId, AdventureModeDbId modeId, ScenarioDbId missionId)
	{
		return (adventureId == AdventureDbId.PVPDR && missionId == ScenarioDbId.PVPDR_Tavern) || (adventureId != AdventureDbId.INVALID && modeId != AdventureModeDbId.INVALID && missionId != ScenarioDbId.INVALID && GameDbf.Scenario.GetRecord((ScenarioDbfRecord r) => r.ID == (int)missionId && r.AdventureId == (int)adventureId && r.ModeId == (int)modeId) != null);
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x0001C048 File Offset: 0x0001A248
	public static bool IsHeroClassValidForAdventure(AdventureDbId adventureId, TAG_CLASS heroClass)
	{
		if (heroClass == TAG_CLASS.INVALID)
		{
			return false;
		}
		foreach (int cardDbId in AdventureUtils.GetGuestHeroesForAdventure(adventureId))
		{
			if (heroClass == GameUtils.GetTagClassFromCardDbId(cardDbId))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x0001C0AC File Offset: 0x0001A2AC
	public static bool IsHeroPowerValidForClassAndAdventure(AdventureDbId adventureId, TAG_CLASS heroClass, int heroPowerDbId)
	{
		return adventureId != AdventureDbId.INVALID && heroPowerDbId > 0 && GameDbf.AdventureHeroPower.HasRecord((AdventureHeroPowerDbfRecord r) => r.CardId == heroPowerDbId && r.AdventureId == (int)adventureId && heroClass == (TAG_CLASS)r.ClassId);
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x0001C100 File Offset: 0x0001A300
	public static bool IsDeckValidForClassAndAdventure(AdventureDbId adventureId, TAG_CLASS heroClass, int deckDbId)
	{
		return adventureId != AdventureDbId.INVALID && deckDbId > 0 && GameDbf.AdventureDeck.HasRecord((AdventureDeckDbfRecord r) => r.DeckId == deckDbId && r.AdventureId == (int)adventureId && heroClass == (TAG_CLASS)r.ClassId);
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x0001C154 File Offset: 0x0001A354
	public static bool IsLoadoutTreasureValidForClassAndAdventure(AdventureDbId adventureId, TAG_CLASS heroClass, int treasureDbId)
	{
		return adventureId != AdventureDbId.INVALID && treasureDbId > 0 && GameDbf.AdventureLoadoutTreasures.HasRecord((AdventureLoadoutTreasuresDbfRecord r) => (r.CardId == treasureDbId || r.UpgradedCardId == treasureDbId) && r.AdventureId == (int)adventureId && heroClass == (TAG_CLASS)r.ClassId);
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x0001C1A8 File Offset: 0x0001A3A8
	public static bool IsValidLoadoutForSelectedAdventure(AdventureDbId adventureDbId, AdventureModeDbId modeDbId, ScenarioDbId scenarioDbId, TAG_CLASS heroClass, int heroPowerDbId, int deckDbId, int treasureDbId)
	{
		if (!AdventureUtils.IsMissionValidForAdventureMode(adventureDbId, modeDbId, scenarioDbId))
		{
			Debug.LogFormat("AdventureUtils.IsValidLoadoutForSelectedAdventure - invalid scenario ID: {0} for adventure ID: {1} with mode ID: {2}.", new object[]
			{
				scenarioDbId,
				adventureDbId,
				modeDbId
			});
			return false;
		}
		if (!AdventureUtils.IsHeroClassValidForAdventure(adventureDbId, heroClass))
		{
			Debug.LogFormat("AdventureUtils.IsValidLoadoutForSelectedAdventure - invalid hero class: {0} for adventure ID: {1}.", new object[]
			{
				heroClass,
				adventureDbId
			});
			return false;
		}
		if (AdventureUtils.SelectableHeroPowersAndDecksExistForAdventure(adventureDbId))
		{
			if (!AdventureUtils.IsHeroPowerValidForClassAndAdventure(adventureDbId, heroClass, heroPowerDbId))
			{
				Debug.LogFormat("AdventureUtils.IsValidLoadoutForSelectedAdventure - invalid loadout hero power ID: {0} for adventure ID: {1}.", new object[]
				{
					heroPowerDbId,
					adventureDbId
				});
				return false;
			}
			if (SceneMgr.Get().GetMode() != SceneMgr.Mode.PVP_DUNGEON_RUN && !AdventureUtils.IsDeckValidForClassAndAdventure(adventureDbId, heroClass, deckDbId))
			{
				Debug.LogFormat("AdventureUtils.IsValidLoadoutForSelectedAdventure - invalid loadout deck ID: {0} for adventure ID: {1}.", new object[]
				{
					deckDbId,
					adventureDbId
				});
				return false;
			}
		}
		if (AdventureUtils.SelectableLoadoutTreasuresExistForAdventure(adventureDbId) && !AdventureUtils.IsLoadoutTreasureValidForClassAndAdventure(adventureDbId, heroClass, treasureDbId))
		{
			Debug.LogFormat("AdventureUtils.IsValidLoadoutForSelectedAdventure - invalid loadout treasure ID: {0} for adventure ID: {1}.", new object[]
			{
				treasureDbId,
				adventureDbId
			});
			return false;
		}
		return true;
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x0001C2CC File Offset: 0x0001A4CC
	public static bool CanPlayWingOpenQuote(AdventureWingDef wingDef)
	{
		return wingDef != null && !string.IsNullOrEmpty(wingDef.m_OpenQuotePrefab) && !string.IsNullOrEmpty(wingDef.m_OpenQuoteVOLine) && (wingDef.m_PlayOpenQuoteInHeroic || !GameUtils.IsModeHeroic(AdventureConfig.Get().GetSelectedMode()));
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x0001C31C File Offset: 0x0001A51C
	public static bool CanPlayWingCompleteQuote(AdventureWingDef wingDef)
	{
		return wingDef != null && !string.IsNullOrEmpty(wingDef.m_CompleteQuotePrefab) && !string.IsNullOrEmpty(wingDef.m_CompleteQuoteVOLine) && (wingDef.m_PlayCompleteQuoteInHeroic || !GameUtils.IsModeHeroic(AdventureConfig.Get().GetSelectedMode()));
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x0001C36C File Offset: 0x0001A56C
	public static void PlayMissionQuote(AdventureBossDef bossDef, Vector3 position)
	{
		if (bossDef == null)
		{
			return;
		}
		string introLine = bossDef.GetIntroLine();
		if (string.IsNullOrEmpty(introLine))
		{
			return;
		}
		AdventureDef adventureDef = AdventureScene.Get().GetAdventureDef(AdventureConfig.Get().GetSelectedAdventure());
		string text = null;
		if (adventureDef != null)
		{
			text = adventureDef.m_DefaultQuotePrefab;
		}
		if (!string.IsNullOrEmpty(bossDef.m_quotePrefabOverride))
		{
			text = bossDef.m_quotePrefabOverride;
		}
		string legacyAssetName = new AssetReference(introLine).GetLegacyAssetName();
		if (!string.IsNullOrEmpty(text))
		{
			bool allowRepeatDuringSession = AdventureScene.Get() != null && AdventureScene.Get().IsDevMode;
			NotificationManager.Get().CreateCharacterQuote(text, position, GameStrings.Get(legacyAssetName), introLine, allowRepeatDuringSession, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
		}
	}

	// Token: 0x02001327 RID: 4903
	// (Invoke) Token: 0x0600D6A1 RID: 54945
	public delegate void FirstChapterFreePopupCompleteCallback();
}
