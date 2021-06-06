using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.DungeonCrawl;
using Hearthstone.Progression;
using PegasusUtil;
using UnityEngine;

public class AdventureUtils
{
	public delegate void FirstChapterFreePopupCompleteCallback();

	public static List<AdventureLoadoutTreasuresDbfRecord> GetLoadoutTreasuresForAdventureAndClass(AdventureDbId adventure, TAG_CLASS classId)
	{
		List<AdventureLoadoutTreasuresDbfRecord> records = GameDbf.AdventureLoadoutTreasures.GetRecords((AdventureLoadoutTreasuresDbfRecord r) => r.AdventureId == (int)adventure && r.ClassId == (int)classId);
		records.Sort((AdventureLoadoutTreasuresDbfRecord a, AdventureLoadoutTreasuresDbfRecord b) => a.SortOrder.CompareTo(b.SortOrder));
		return records;
	}

	public static List<AdventureHeroPowerDbfRecord> GetHeroPowersForAdventureAndClass(AdventureDbId adventure, TAG_CLASS classId)
	{
		List<AdventureHeroPowerDbfRecord> records = GameDbf.AdventureHeroPower.GetRecords((AdventureHeroPowerDbfRecord r) => r.AdventureId == (int)adventure && r.ClassId == (int)classId);
		records.Sort((AdventureHeroPowerDbfRecord a, AdventureHeroPowerDbfRecord b) => a.SortOrder.CompareTo(b.SortOrder));
		return records;
	}

	public static List<AdventureDeckDbfRecord> GetDecksForAdventureAndClass(AdventureDbId adventure, TAG_CLASS classId)
	{
		List<AdventureDeckDbfRecord> records = GameDbf.AdventureDeck.GetRecords((AdventureDeckDbfRecord r) => r.AdventureId == (int)adventure && r.ClassId == (int)classId);
		records.Sort((AdventureDeckDbfRecord a, AdventureDeckDbfRecord b) => a.SortOrder.CompareTo(b.SortOrder));
		return records;
	}

	public static bool AdventureHeroPowerIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureHeroPowerDbfRecord heroPowerRecord, out long unlockProgress, out bool hasUnlockCriteria)
	{
		return AdventureRewardIsUnlocked(gameSaveServerKey, (GameSaveKeySubkeyId)heroPowerRecord.UnlockGameSaveSubkey, heroPowerRecord.UnlockValue, heroPowerRecord.UnlockAchievement, out unlockProgress, out hasUnlockCriteria);
	}

	public static bool AdventureDeckIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureDeckDbfRecord deckRecord, out long unlockProgress, out bool hasUnlockCriteria)
	{
		return AdventureRewardIsUnlocked(gameSaveServerKey, (GameSaveKeySubkeyId)deckRecord.UnlockGameSaveSubkey, deckRecord.UnlockValue, 0, out unlockProgress, out hasUnlockCriteria);
	}

	public static bool AdventureTreasureIsUnlocked(GameSaveKeyId gameSaveServerKey, AdventureLoadoutTreasuresDbfRecord treasureLoadoutRecord, out long unlockProgress, out bool hasUnlockCriteria)
	{
		return AdventureRewardIsUnlocked(gameSaveServerKey, (GameSaveKeySubkeyId)treasureLoadoutRecord.UnlockGameSaveSubkey, treasureLoadoutRecord.UnlockValue, treasureLoadoutRecord.UnlockAchievement, out unlockProgress, out hasUnlockCriteria);
	}

	public static bool AdventureTreasureIsUpgraded(GameSaveKeyId gameSaveServerKey, AdventureLoadoutTreasuresDbfRecord treasureLoadoutRecord, out long upgradeProgress)
	{
		bool hasUnlockCriteria;
		return AdventureRewardIsUnlocked(gameSaveServerKey, (GameSaveKeySubkeyId)treasureLoadoutRecord.UpgradeGameSaveSubkey, treasureLoadoutRecord.UpgradeValue, 0, out upgradeProgress, out hasUnlockCriteria);
	}

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
			flag2 = unlockProgress >= unlockValue;
		}
		if (unlockGameSaveSubkey > (GameSaveKeySubkeyId)0 && unlockAchievement > 0)
		{
			unlockProgress += num;
			return flag && flag2;
		}
		if (unlockAchievement > 0)
		{
			unlockProgress = num;
			return flag;
		}
		if (unlockGameSaveSubkey > (GameSaveKeySubkeyId)0)
		{
			return flag2;
		}
		return false;
	}

	public static int GetFinalAdventureWing(int adventureId, bool excludeOwnedWings, bool excludeInactiveWings = false)
	{
		int num = -1;
		int result = 0;
		foreach (WingDbfRecord record in GameDbf.Wing.GetRecords())
		{
			if (record.AdventureId == adventureId && record.UnlockOrder > num && (!excludeOwnedWings || !AdventureProgressMgr.Get().OwnsWing(record.ID)) && (!excludeInactiveWings || AdventureProgressMgr.IsWingEventActive(record.ID)))
			{
				num = record.UnlockOrder;
				result = record.ID;
			}
		}
		return result;
	}

	public static bool IsAnomalyModeAvailable(AdventureDbId adventureDbId, AdventureModeDbId modeDbId, WingDbId wingDbId)
	{
		if (!IsAnomalyModeLocked(adventureDbId, modeDbId))
		{
			return IsAnomalyModeAllowed(wingDbId);
		}
		return false;
	}

	public static bool IsAnomalyModeLocked(AdventureDbId adventureDbId, AdventureModeDbId modeDbId)
	{
		foreach (ScenarioDbfRecord record in GameDbf.Scenario.GetRecords((ScenarioDbfRecord r) => r.AdventureId == (int)adventureDbId && r.ModeId == (int)modeDbId && r.WingId != 0))
		{
			if (!AdventureProgressMgr.Get().OwnsWing(record.WingId))
			{
				return true;
			}
		}
		return false;
	}

	public static bool IsAnomalyModeAllowed(WingDbId wingDbId)
	{
		WingDbfRecord record = GameDbf.Wing.GetRecord((int)wingDbId);
		if (record == null || !record.AllowsAnomaly)
		{
			return false;
		}
		return true;
	}

	public static AdventureDbId GetAdventureIdForWing(WingDbId wingDbId)
	{
		return (AdventureDbId)(GameDbf.Wing.GetRecord((int)wingDbId)?.AdventureId ?? 0);
	}

	public static bool IsProductTypeAnAdventureWing(ProductType type)
	{
		if ((uint)(type - 3) <= 1u || (uint)(type - 7) <= 1u)
		{
			return true;
		}
		return false;
	}

	public static bool IsAdventureBundle(Network.Bundle bundle)
	{
		return bundle.Items.Any((Network.BundleItem item) => IsProductTypeAnAdventureWing(item.ItemType));
	}

	public static bool DoesBundleIncludeWingForAdventure(Network.Bundle bundle, AdventureDbId adventure)
	{
		return bundle.Items.Any((Network.BundleItem item) => IsProductTypeAnAdventureWing(item.ItemType) && GetAdventureIdForWing((WingDbId)item.ProductData) == adventure);
	}

	public static bool DoesBundleIncludeWing(Network.Bundle bundle, int wingId)
	{
		return bundle.Items.Any((Network.BundleItem item) => IsProductTypeAnAdventureWing(item.ItemType) && item.ProductData == wingId);
	}

	public static string GetHeroCardIdFromClassForDungeonCrawl(IDungeonCrawlData dungeonCrawlData, TAG_CLASS cardClass)
	{
		NetCache.CardDefinition cardDefinition = null;
		List<int> list = dungeonCrawlData?.GetGuestHeroesForCurrentAdventure();
		if (list == null || list.Count <= 0)
		{
			cardDefinition = CollectionManager.Get().GetFavoriteHero(cardClass);
			if (cardDefinition == null)
			{
				Log.Adventures.PrintError("GameUtils.GetHeroCardIdFromClassForAdventure - could not get Hero Card Id from {0}", cardClass);
				return null;
			}
			return cardDefinition.Name;
		}
		if (cardClass == TAG_CLASS.INVALID)
		{
			cardClass = TAG_CLASS.NEUTRAL;
		}
		foreach (int item in list)
		{
			if (GameUtils.GetTagClassFromCardDbId(item) == cardClass)
			{
				CardDbfRecord record = GameDbf.Card.GetRecord(item);
				if (record != null)
				{
					return record.NoteMiniGuid;
				}
			}
		}
		return null;
	}

	public static void DisplayFirstChapterFreePopup(ChapterPageData chapterPageData, FirstChapterFreePopupCompleteCallback callback = null)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_ADVENTURE_ADVENTUREBOOK_DAL_FIRST_TIME_FLOW_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_ADVENTURE_ADVENTUREBOOK_DAL_FIRST_TIME_FLOW");
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
		popupInfo.m_responseCallback = delegate
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

	public static bool IsEntireAdventureFree(AdventureDbId adventureID)
	{
		return !GameDbf.Wing.HasRecord((WingDbfRecord r) => r.AdventureId == (int)adventureID && (r.PmtProductIdForSingleWingPurchase != 0 || r.PmtProductIdForThisAndRestOfAdventure != 0));
	}

	public static bool DoesAdventureRequireAllHeroesUnlocked(AdventureDbId adventureId)
	{
		return DoesAdventureRequireAllHeroesUnlocked(adventureId, AdventureConfig.GetDefaultModeDbIdForAdventure(adventureId));
	}

	public static bool DoesAdventureRequireAllHeroesUnlocked(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		if (adventureId == AdventureDbId.INVALID || modeId == AdventureModeDbId.INVALID)
		{
			return true;
		}
		AdventureDataDbfRecord adventureDataRecord = AdventureConfig.GetAdventureDataRecord(adventureId, modeId);
		if (adventureDataRecord != null)
		{
			return !adventureDataRecord.IgnoreHeroUnlockRequirement;
		}
		return true;
	}

	public static List<int> GetGuestHeroesForAdventure(AdventureDbId currentAdventure)
	{
		List<AdventureGuestHeroesDbfRecord> sortedGuestHeroRecordsForAdventures = GetSortedGuestHeroRecordsForAdventures(currentAdventure);
		List<int> list = new List<int>();
		foreach (AdventureGuestHeroesDbfRecord item in sortedGuestHeroRecordsForAdventures)
		{
			if (item.GuestHeroId != 0)
			{
				list.Add(GameUtils.GetCardIdFromGuestHeroDbId(item.GuestHeroId));
			}
		}
		return list;
	}

	public static List<AdventureGuestHeroesDbfRecord> GetSortedGuestHeroRecordsForAdventures(AdventureDbId currentAdventure)
	{
		List<AdventureGuestHeroesDbfRecord> records = GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)currentAdventure);
		records.Sort((AdventureGuestHeroesDbfRecord a, AdventureGuestHeroesDbfRecord b) => a.SortOrder.CompareTo(b.SortOrder));
		return records;
	}

	public static CardListDataModel GetAvailableGuestHeroesAsCardListSortedByReleaseDate(AdventureDbId adventure)
	{
		CardListDataModel cardListDataModel = new CardListDataModel();
		List<AdventureGuestHeroesDbfRecord> records = GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)adventure && AdventureProgressMgr.IsWingEventActive(r.WingId));
		DateTime defaultDateIfNoRecordFound = DateTime.MinValue;
		records.Sort((AdventureGuestHeroesDbfRecord a, AdventureGuestHeroesDbfRecord b) => DateTime.Compare(ReleaseDateForAdventureGuestHero(b, defaultDateIfNoRecordFound), ReleaseDateForAdventureGuestHero(a, defaultDateIfNoRecordFound)));
		foreach (AdventureGuestHeroesDbfRecord item2 in records)
		{
			CardDbfRecord record = GameDbf.Card.GetRecord(GameUtils.GetCardIdFromGuestHeroDbId(item2.GuestHeroId));
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
		if (eventStartTimeUtc.HasValue)
		{
			return eventStartTimeUtc.Value;
		}
		return defaultDate;
	}

	public static bool DoesAdventureShowNewlyUnlockedGuestHeroTreatment(AdventureDbId adventure)
	{
		switch (adventure)
		{
		case AdventureDbId.GIL:
			return false;
		case AdventureDbId.BLACKROCK_CRASH:
		case AdventureDbId.LOE_REVIVAL:
		case AdventureDbId.TB_BUCKET_BRAWL:
		case AdventureDbId.NAXX_CRASH:
		case AdventureDbId.TEMPLE_OUTRUN:
		case AdventureDbId.ROAD_TO_NORTHREND:
			return false;
		default:
			return true;
		}
	}

	public static bool DoesAdventureHaveUnseenGuestHeroes(AdventureDbId adventure, AdventureModeDbId mode)
	{
		List<long> values = null;
		AdventureDataDbfRecord adventureDataRecord = AdventureConfig.GetAdventureDataRecord(adventure, mode);
		GameSaveDataManager.Get().GetSubkeyValue((GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_UNLOCKED_HEROES, out values);
		foreach (AdventureGuestHeroesDbfRecord record in GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)adventure))
		{
			if (record.GuestHeroId != 0 && (values == null || !values.Contains(GameUtils.GetCardIdFromGuestHeroDbId(record.GuestHeroId))) && AdventureProgressMgr.IsWingEventActive(record.WingId) && AdventureProgressMgr.Get().OwnsWing(record.WingId))
			{
				return true;
			}
		}
		return false;
	}

	public static bool SelectableLoadoutTreasuresExistForAdventure(AdventureDbId adventure)
	{
		return GameDbf.AdventureLoadoutTreasures.HasRecord((AdventureLoadoutTreasuresDbfRecord r) => r.AdventureId == (int)adventure);
	}

	public static bool SelectableHeroPowersExistForAdventure(AdventureDbId adventure)
	{
		return GameDbf.AdventureHeroPower.HasRecord((AdventureHeroPowerDbfRecord r) => r.AdventureId == (int)adventure);
	}

	public static bool SelectableDecksExistForAdventure(AdventureDbId adventure)
	{
		return GameDbf.AdventureDeck.HasRecord((AdventureDeckDbfRecord r) => r.AdventureId == (int)adventure);
	}

	public static bool SelectableHeroPowersAndDecksExistForAdventure(AdventureDbId adventure)
	{
		bool flag = SelectableHeroPowersExistForAdventure(adventure);
		bool flag2 = SelectableDecksExistForAdventure(adventure);
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
		Debug.LogError($"Adventure {adventure} has ADVENTURE_HERO_POWER or ADVENTURE_DECK entries defined, but not both! This is not currently suported - you must have entries for both tables, so a Hero Power can be selected first, then a Deck.");
		return false;
	}

	public static bool IsMissionValidForAdventureMode(AdventureDbId adventureId, AdventureModeDbId modeId, ScenarioDbId missionId)
	{
		if (adventureId == AdventureDbId.PVPDR && missionId == ScenarioDbId.PVPDR_Tavern)
		{
			return true;
		}
		if (adventureId == AdventureDbId.INVALID || modeId == AdventureModeDbId.INVALID || missionId == ScenarioDbId.INVALID)
		{
			return false;
		}
		if (GameDbf.Scenario.GetRecord((ScenarioDbfRecord r) => r.ID == (int)missionId && r.AdventureId == (int)adventureId && r.ModeId == (int)modeId) == null)
		{
			return false;
		}
		return true;
	}

	public static bool IsHeroClassValidForAdventure(AdventureDbId adventureId, TAG_CLASS heroClass)
	{
		if (heroClass == TAG_CLASS.INVALID)
		{
			return false;
		}
		foreach (int item in GetGuestHeroesForAdventure(adventureId))
		{
			if (heroClass == GameUtils.GetTagClassFromCardDbId(item))
			{
				return true;
			}
		}
		return false;
	}

	public static bool IsHeroPowerValidForClassAndAdventure(AdventureDbId adventureId, TAG_CLASS heroClass, int heroPowerDbId)
	{
		if (adventureId == AdventureDbId.INVALID || heroPowerDbId <= 0)
		{
			return false;
		}
		return GameDbf.AdventureHeroPower.HasRecord((AdventureHeroPowerDbfRecord r) => r.CardId == heroPowerDbId && r.AdventureId == (int)adventureId && heroClass == (TAG_CLASS)r.ClassId);
	}

	public static bool IsDeckValidForClassAndAdventure(AdventureDbId adventureId, TAG_CLASS heroClass, int deckDbId)
	{
		if (adventureId == AdventureDbId.INVALID || deckDbId <= 0)
		{
			return false;
		}
		return GameDbf.AdventureDeck.HasRecord((AdventureDeckDbfRecord r) => r.DeckId == deckDbId && r.AdventureId == (int)adventureId && heroClass == (TAG_CLASS)r.ClassId);
	}

	public static bool IsLoadoutTreasureValidForClassAndAdventure(AdventureDbId adventureId, TAG_CLASS heroClass, int treasureDbId)
	{
		if (adventureId == AdventureDbId.INVALID || treasureDbId <= 0)
		{
			return false;
		}
		return GameDbf.AdventureLoadoutTreasures.HasRecord((AdventureLoadoutTreasuresDbfRecord r) => (r.CardId == treasureDbId || r.UpgradedCardId == treasureDbId) && r.AdventureId == (int)adventureId && heroClass == (TAG_CLASS)r.ClassId);
	}

	public static bool IsValidLoadoutForSelectedAdventure(AdventureDbId adventureDbId, AdventureModeDbId modeDbId, ScenarioDbId scenarioDbId, TAG_CLASS heroClass, int heroPowerDbId, int deckDbId, int treasureDbId)
	{
		if (!IsMissionValidForAdventureMode(adventureDbId, modeDbId, scenarioDbId))
		{
			Debug.LogFormat("AdventureUtils.IsValidLoadoutForSelectedAdventure - invalid scenario ID: {0} for adventure ID: {1} with mode ID: {2}.", scenarioDbId, adventureDbId, modeDbId);
			return false;
		}
		if (!IsHeroClassValidForAdventure(adventureDbId, heroClass))
		{
			Debug.LogFormat("AdventureUtils.IsValidLoadoutForSelectedAdventure - invalid hero class: {0} for adventure ID: {1}.", heroClass, adventureDbId);
			return false;
		}
		if (SelectableHeroPowersAndDecksExistForAdventure(adventureDbId))
		{
			if (!IsHeroPowerValidForClassAndAdventure(adventureDbId, heroClass, heroPowerDbId))
			{
				Debug.LogFormat("AdventureUtils.IsValidLoadoutForSelectedAdventure - invalid loadout hero power ID: {0} for adventure ID: {1}.", heroPowerDbId, adventureDbId);
				return false;
			}
			if (SceneMgr.Get().GetMode() != SceneMgr.Mode.PVP_DUNGEON_RUN && !IsDeckValidForClassAndAdventure(adventureDbId, heroClass, deckDbId))
			{
				Debug.LogFormat("AdventureUtils.IsValidLoadoutForSelectedAdventure - invalid loadout deck ID: {0} for adventure ID: {1}.", deckDbId, adventureDbId);
				return false;
			}
		}
		if (SelectableLoadoutTreasuresExistForAdventure(adventureDbId) && !IsLoadoutTreasureValidForClassAndAdventure(adventureDbId, heroClass, treasureDbId))
		{
			Debug.LogFormat("AdventureUtils.IsValidLoadoutForSelectedAdventure - invalid loadout treasure ID: {0} for adventure ID: {1}.", treasureDbId, adventureDbId);
			return false;
		}
		return true;
	}

	public static bool CanPlayWingOpenQuote(AdventureWingDef wingDef)
	{
		if (wingDef != null && !string.IsNullOrEmpty(wingDef.m_OpenQuotePrefab) && !string.IsNullOrEmpty(wingDef.m_OpenQuoteVOLine))
		{
			if (!wingDef.m_PlayOpenQuoteInHeroic)
			{
				return !GameUtils.IsModeHeroic(AdventureConfig.Get().GetSelectedMode());
			}
			return true;
		}
		return false;
	}

	public static bool CanPlayWingCompleteQuote(AdventureWingDef wingDef)
	{
		if (wingDef != null && !string.IsNullOrEmpty(wingDef.m_CompleteQuotePrefab) && !string.IsNullOrEmpty(wingDef.m_CompleteQuoteVOLine))
		{
			if (!wingDef.m_PlayCompleteQuoteInHeroic)
			{
				return !GameUtils.IsModeHeroic(AdventureConfig.Get().GetSelectedMode());
			}
			return true;
		}
		return false;
	}

	public static void PlayMissionQuote(AdventureBossDef bossDef, Vector3 position)
	{
		if (bossDef == null)
		{
			return;
		}
		string introLine = bossDef.GetIntroLine();
		if (!string.IsNullOrEmpty(introLine))
		{
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
				NotificationManager.Get().CreateCharacterQuote(text, position, GameStrings.Get(legacyAssetName), introLine, allowRepeatDuringSession);
			}
		}
	}
}
