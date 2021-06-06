using System;
using System.Collections.Generic;
using Hearthstone.Progression;
using UnityEngine;

namespace Hearthstone.DungeonCrawl
{
	// Token: 0x02001171 RID: 4465
	public class DungeonCrawlUtil
	{
		// Token: 0x0600C3F1 RID: 50161 RVA: 0x003B1BD0 File Offset: 0x003AFDD0
		public static DungeonCrawlServices CreateAdventureDungeonCrawlServices(AssetLoadingHelper assetLoadingHelper)
		{
			return new DungeonCrawlServices
			{
				DungeonCrawlData = new DungeonCrawlDataAdventureAdapter(),
				SubsceneController = new DungeonCrawlSubscenControllerAdapter(),
				AssetLoadingHelper = assetLoadingHelper
			};
		}

		// Token: 0x0600C3F2 RID: 50162 RVA: 0x003B1BF4 File Offset: 0x003AFDF4
		public static DungeonCrawlServices CreateTavernBrawlDungeonCrawlServices(AdventureDbId adventureId, AdventureModeDbId modeId, AssetLoadingHelper assetLoadingHelper)
		{
			ScenarioDbId missionForAdventure = DungeonCrawlUtil.GetMissionForAdventure(adventureId, modeId);
			WingDbfRecord wingRecordFromMissionId = GameUtils.GetWingRecordFromMissionId((int)missionForAdventure);
			DungeonCrawlDataTavernBrawl.DataSet data = new DungeonCrawlDataTavernBrawl.DataSet
			{
				m_selectedAdventure = adventureId,
				m_selectedMode = modeId,
				m_mission = missionForAdventure,
				m_selectableHeroPowersExist = AdventureUtils.SelectableHeroPowersExistForAdventure(adventureId),
				m_selectableDecksExist = AdventureUtils.SelectableDecksExistForAdventure(adventureId),
				m_selectableHeroPowersAndDecksExist = AdventureUtils.SelectableHeroPowersAndDecksExistForAdventure(adventureId),
				m_selectableLoadoutTreasuresExist = AdventureUtils.SelectableLoadoutTreasuresExistForAdventure(adventureId),
				m_bossesInRun = AdventureConfig.GetAdventureBossesInRun(wingRecordFromMissionId),
				m_heroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure = false,
				m_guestHeroes = DungeonCrawlUtil.GetGuestHeroesForTavernBrawl((int)missionForAdventure),
				m_requiresDeck = AdventureConfig.DoesMissionRequireDeck(missionForAdventure),
				m_gameSaveClientKey = DungeonCrawlUtil.GetClientSaveKey(adventureId, modeId)
			};
			return new DungeonCrawlServices
			{
				DungeonCrawlData = new DungeonCrawlDataTavernBrawl(data),
				SubsceneController = new DungoneCrawlTavernBrawlSubsceneController(),
				AssetLoadingHelper = assetLoadingHelper
			};
		}

		// Token: 0x0600C3F3 RID: 50163 RVA: 0x003B1CC8 File Offset: 0x003AFEC8
		public static DungeonCrawlServices CreatePvPDungeonCrawlServices(AdventureDbId adventureId, AssetLoadingHelper assetLoadingHelper)
		{
			DungeonCrawlServices dungeonCrawlServices = new DungeonCrawlServices();
			AdventureModeDbId adventureModeDbId = AdventureModeDbId.DUNGEON_CRAWL;
			ScenarioDbId missionForAdventure = DungeonCrawlUtil.GetMissionForAdventure(adventureId, adventureModeDbId);
			WingDbfRecord wingRecordFromMissionId = GameUtils.GetWingRecordFromMissionId((int)missionForAdventure);
			DungeonCrawlDataPvP dungeonCrawlDataPvP = new DungeonCrawlDataPvP(new DungeonCrawlDataPvP.DataSet
			{
				m_selectedAdventure = adventureId,
				m_selectedMode = adventureModeDbId,
				m_mission = missionForAdventure,
				m_selectableHeroPowersExist = AdventureUtils.SelectableHeroPowersExistForAdventure(adventureId),
				m_selectableDecksExist = AdventureUtils.SelectableDecksExistForAdventure(adventureId),
				m_selectableHeroPowersAndDecksExist = AdventureUtils.SelectableHeroPowersAndDecksExistForAdventure(adventureId),
				m_selectableLoadoutTreasuresExist = AdventureUtils.SelectableLoadoutTreasuresExistForAdventure(adventureId),
				m_bossesInRun = AdventureConfig.GetAdventureBossesInRun(wingRecordFromMissionId),
				m_heroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure = true,
				m_guestHeroes = AdventureUtils.GetGuestHeroesForAdventure(adventureId),
				m_requiresDeck = true,
				m_gameSaveClientKey = DungeonCrawlUtil.GetClientSaveKey(adventureId, adventureModeDbId)
			});
			GameSaveKeyId gsdkeyForAdventure = PvPDungeonRunScene.Get().GetGSDKeyForAdventure();
			long num;
			GameSaveDataManager.Get().GetSubkeyValue(gsdkeyForAdventure, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_CLASS, out num);
			long selectedHeroPowerDbId;
			GameSaveDataManager.Get().GetSubkeyValue(gsdkeyForAdventure, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_POWER, out selectedHeroPowerDbId);
			long selectedLoadoutTreasureDbId;
			GameSaveDataManager.Get().GetSubkeyValue(gsdkeyForAdventure, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_LOADOUT_TREASURE_ID, out selectedLoadoutTreasureDbId);
			dungeonCrawlDataPvP.SelectedHeroClass = (TAG_CLASS)num;
			dungeonCrawlDataPvP.SelectedHeroPowerDbId = selectedHeroPowerDbId;
			dungeonCrawlDataPvP.SelectedLoadoutTreasureDbId = selectedLoadoutTreasureDbId;
			dungeonCrawlServices.DungeonCrawlData = dungeonCrawlDataPvP;
			dungeonCrawlServices.SubsceneController = new DungoneCrawlPvPSubsceneController();
			dungeonCrawlServices.AssetLoadingHelper = assetLoadingHelper;
			return dungeonCrawlServices;
		}

		// Token: 0x0600C3F4 RID: 50164 RVA: 0x003B1DFC File Offset: 0x003AFFFC
		public static void LoadDungeonRunPrefab(DungeonCrawlUtil.DungeonRunLoadCallback callback)
		{
			AssetReference assetRef2 = new AssetReference(DungeonCrawlUtil.DUNGEON_RUN_PREFAB_ASSET);
			AssetLoader.Get().InstantiatePrefab(assetRef2, delegate(AssetReference assetRef, GameObject go, object data)
			{
				if (callback != null)
				{
					callback(go);
				}
			}, null, AssetLoadingOptions.IgnorePrefabPosition);
		}

		// Token: 0x0600C3F5 RID: 50165 RVA: 0x003B1E40 File Offset: 0x003B0040
		public static bool IsDungeonRunDataReady(AdventureDbId adventureId, AdventureModeDbId adventureModeId)
		{
			AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)adventureModeId);
			if (adventureDataRecord == null)
			{
				return false;
			}
			GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
			GameSaveKeyId gameSaveDataClientKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey;
			return GameSaveDataManager.Get().IsDataReady(gameSaveDataServerKey) && GameSaveDataManager.Get().IsDataReady(gameSaveDataClientKey);
		}

		// Token: 0x0600C3F6 RID: 50166 RVA: 0x003B1E84 File Offset: 0x003B0084
		public static void ClearDungeonRunServerData(AdventureDbId adventureId, AdventureModeDbId adventureModeId)
		{
			AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)adventureModeId);
			if (adventureDataRecord == null)
			{
				return;
			}
			GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
			GameSaveDataManager.Get().ClearLocalData(gameSaveDataServerKey);
		}

		// Token: 0x0600C3F7 RID: 50167 RVA: 0x003B1EB0 File Offset: 0x003B00B0
		public static void LoadDungeonRunData(AdventureDbId adventureId, AdventureModeDbId adventureModeId, DungeonCrawlUtil.DungeonRunDataLoadedCallback callback)
		{
			AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)adventureModeId);
			if (adventureDataRecord == null)
			{
				if (callback != null)
				{
					callback(false);
				}
				return;
			}
			GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
			GameSaveKeyId gameSaveDataClientKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey;
			bool clientDataRetrieved = false;
			bool serverDataRetrieved = false;
			bool resultSuccesfull = true;
			GameSaveDataManager.Get().Request(gameSaveDataServerKey, delegate(bool success)
			{
				serverDataRetrieved = true;
				resultSuccesfull = (resultSuccesfull && success);
				if (serverDataRetrieved & clientDataRetrieved)
				{
					callback(resultSuccesfull);
				}
			});
			GameSaveDataManager.Get().Request(gameSaveDataClientKey, delegate(bool success)
			{
				clientDataRetrieved = true;
				resultSuccesfull = (resultSuccesfull && success);
				if (serverDataRetrieved & clientDataRetrieved)
				{
					callback(resultSuccesfull);
				}
			});
		}

		// Token: 0x0600C3F8 RID: 50168 RVA: 0x003B1F3C File Offset: 0x003B013C
		public static bool IsDungeonRunInProgress(AdventureDbId adventureId, AdventureModeDbId adventureModeId)
		{
			AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)adventureModeId);
			if (adventureDataRecord == null)
			{
				return false;
			}
			GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
			if (!GameSaveDataManager.Get().ValidateIfKeyCanBeAccessed(gameSaveDataServerKey, adventureDataRecord.Name))
			{
				return false;
			}
			long num;
			GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_RETIRED, out num);
			if (num > 0L)
			{
				return false;
			}
			long num2;
			GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, out num2);
			if (num2 > 0L)
			{
				return true;
			}
			long num3 = 0L;
			long num4 = 0L;
			List<long> list = null;
			GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_LOST_TO, out num4);
			GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSSES_DEFEATED, out list);
			GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_LATEST_DUNGEON_RUN_COMPLETE, out num3);
			bool flag = num4 > 0L;
			bool flag2 = list != null && list.Count > 0;
			bool flag3 = flag || flag2;
			return num3 == 0L && flag3;
		}

		// Token: 0x0600C3F9 RID: 50169 RVA: 0x003B2010 File Offset: 0x003B0210
		private static ScenarioDbId GetMissionForAdventure(AdventureDbId adv, AdventureModeDbId mode)
		{
			List<ScenarioDbfRecord> records = GameDbf.Scenario.GetRecords((ScenarioDbfRecord r) => r.AdventureId == (int)adv && r.ModeId == (int)mode && r.WingId != 0, -1);
			if (records == null || records.Count < 1)
			{
				return ScenarioDbId.INVALID;
			}
			if (records.Count > 1)
			{
				Log.Adventures.PrintWarning("Unexpected number of scenarios found for adventure {0} mode {1}. There should be only one but found {2}", new object[]
				{
					adv,
					mode,
					records.Count
				});
			}
			return (ScenarioDbId)records[0].ID;
		}

		// Token: 0x0600C3FA RID: 50170 RVA: 0x003B20AC File Offset: 0x003B02AC
		private static GameSaveKeyId GetClientSaveKey(AdventureDbId adv, AdventureModeDbId mode)
		{
			AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adv, (int)mode);
			if (adventureDataRecord == null)
			{
				return GameSaveKeyId.INVALID;
			}
			return (GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey;
		}

		// Token: 0x0600C3FB RID: 50171 RVA: 0x003B20CC File Offset: 0x003B02CC
		public static List<int> GetGuestHeroesForTavernBrawl(int scenarioId)
		{
			List<ScenarioGuestHeroesDbfRecord> records = GameDbf.ScenarioGuestHeroes.GetRecords((ScenarioGuestHeroesDbfRecord r) => r.ScenarioId == scenarioId, -1);
			records.Sort((ScenarioGuestHeroesDbfRecord a, ScenarioGuestHeroesDbfRecord b) => a.SortOrder.CompareTo(b.SortOrder));
			List<int> list = new List<int>();
			foreach (ScenarioGuestHeroesDbfRecord scenarioGuestHeroesDbfRecord in records)
			{
				list.Add(GameUtils.GetCardIdFromGuestHeroDbId(scenarioGuestHeroesDbfRecord.GuestHeroId));
			}
			return list;
		}

		// Token: 0x0600C3FC RID: 50172 RVA: 0x003B2174 File Offset: 0x003B0374
		public static bool MigrateDungeonCrawlSubkeys(GameSaveKeyId clientKey, GameSaveKeyId serverKey)
		{
			if (!GameSaveDataManager.Get().ValidateIfKeyCanBeAccessed(clientKey, "MigrateDungeonCrawlSubkeys") || !GameSaveDataManager.Get().ValidateIfKeyCanBeAccessed(serverKey, "MigrateDungeonCrawlSubkeys"))
			{
				Log.Adventures.Print("MigrateDungeonCrawlSubkeys called but one or both keys cannot be migrated. Client key: {0}  Server key: {1}", new object[]
				{
					clientKey,
					serverKey
				});
				return false;
			}
			bool result = false;
			if (GameSaveDataManager.Get().MigrateSubkeyIntValue(clientKey, serverKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_LATEST_DUNGEON_RUN_COMPLETE, 0L))
			{
				Log.Adventures.Print("Migrated DUNGEON_CRAWL_HAS_SEEN_LATEST_DUNGEON_RUN_COMPLETE subkey from client to server key!", Array.Empty<object>());
				result = true;
			}
			if (GameSaveDataManager.Get().MigrateSubkeyIntValue(clientKey, serverKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_CLASS, 0L))
			{
				Log.Adventures.Print("Migrated DUNGEON_CRAWL_PLAYER_SELECTED_HERO_CLASS subkey from client to server key!", Array.Empty<object>());
				result = true;
			}
			return result;
		}

		// Token: 0x0600C3FD RID: 50173 RVA: 0x003B2228 File Offset: 0x003B0428
		public static bool IsDungeonRunActive(GameSaveKeyId serverKey)
		{
			long num;
			GameSaveDataManager.Get().GetSubkeyValue(serverKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, out num);
			long num2;
			GameSaveDataManager.Get().GetSubkeyValue(serverKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_RETIRED, out num2);
			return num > 0L && num2 <= 0L;
		}

		// Token: 0x0600C3FE RID: 50174 RVA: 0x003B2267 File Offset: 0x003B0467
		public static bool IsPVPDRFriendlyEncounter(int missionId)
		{
			return missionId == 3745;
		}

		// Token: 0x0600C3FF RID: 50175 RVA: 0x003B2274 File Offset: 0x003B0474
		public static List<DbfRecord> CheckForNewlyUnlockedGSDRewardsOfSpecificType(IEnumerable<DbfRecord> rewardDbfRecords, IDungeonCrawlData dungeonCrawlData, GameSaveKeyId gameSaveServerKey, GameSaveKeyId gameSaveClientKey, GameSaveKeySubkeyId awardedRewardsSubkey, GameSaveKeySubkeyId newlyUnlockedRewardsSubkey, List<GameSaveDataManager.SubkeySaveRequest> subkeyUpdates, bool checkForUpgrades = false)
		{
			List<DbfRecord> list = new List<DbfRecord>();
			List<long> list2 = new List<long>();
			List<long> list3;
			GameSaveDataManager.Get().GetSubkeyValue(gameSaveClientKey, awardedRewardsSubkey, out list3);
			foreach (DbfRecord dbfRecord in rewardDbfRecords)
			{
				int unlockAchievement = 0;
				long item;
				GameSaveKeySubkeyId unlockGameSaveSubkey;
				int unlockValue;
				if (dbfRecord is AdventureHeroPowerDbfRecord)
				{
					AdventureHeroPowerDbfRecord adventureHeroPowerDbfRecord = (AdventureHeroPowerDbfRecord)dbfRecord;
					item = (long)adventureHeroPowerDbfRecord.CardId;
					unlockGameSaveSubkey = (GameSaveKeySubkeyId)adventureHeroPowerDbfRecord.UnlockGameSaveSubkey;
					unlockValue = adventureHeroPowerDbfRecord.UnlockValue;
					unlockAchievement = adventureHeroPowerDbfRecord.UnlockAchievement;
				}
				else if (dbfRecord is AdventureDeckDbfRecord)
				{
					AdventureDeckDbfRecord adventureDeckDbfRecord = (AdventureDeckDbfRecord)dbfRecord;
					item = (long)adventureDeckDbfRecord.DeckId;
					unlockGameSaveSubkey = (GameSaveKeySubkeyId)adventureDeckDbfRecord.UnlockGameSaveSubkey;
					unlockValue = adventureDeckDbfRecord.UnlockValue;
				}
				else
				{
					if (!(dbfRecord is AdventureLoadoutTreasuresDbfRecord))
					{
						Log.Adventures.PrintWarning("Unsupported DbfRecord type in CheckForNewlyUnlockedRewardsOfSpecificType()!", Array.Empty<object>());
						return list;
					}
					AdventureLoadoutTreasuresDbfRecord adventureLoadoutTreasuresDbfRecord = (AdventureLoadoutTreasuresDbfRecord)dbfRecord;
					if (checkForUpgrades)
					{
						item = (long)adventureLoadoutTreasuresDbfRecord.UpgradedCardId;
						unlockGameSaveSubkey = (GameSaveKeySubkeyId)adventureLoadoutTreasuresDbfRecord.UpgradeGameSaveSubkey;
						unlockValue = adventureLoadoutTreasuresDbfRecord.UpgradeValue;
					}
					else
					{
						item = (long)adventureLoadoutTreasuresDbfRecord.CardId;
						unlockGameSaveSubkey = (GameSaveKeySubkeyId)adventureLoadoutTreasuresDbfRecord.UnlockGameSaveSubkey;
						unlockValue = adventureLoadoutTreasuresDbfRecord.UnlockValue;
					}
					unlockAchievement = adventureLoadoutTreasuresDbfRecord.UnlockAchievement;
				}
				long num;
				bool flag;
				if ((list3 == null || !list3.Contains(item)) && dungeonCrawlData.AdventureRewardIsUnlocked(gameSaveServerKey, unlockGameSaveSubkey, unlockValue, unlockAchievement, out num, out flag) && flag)
				{
					list.Add(dbfRecord);
					list2.Add(item);
				}
			}
			if (list.Count > 0)
			{
				if (awardedRewardsSubkey == GameSaveKeySubkeyId.DUNGEON_CRAWL_AWARDED_HERO_POWERS)
				{
					Log.Adventures.Print("Just Unlocked Hero Powers: {0}", new object[]
					{
						list
					});
				}
				else if (awardedRewardsSubkey == GameSaveKeySubkeyId.DUNGEON_CRAWL_AWARDED_DECKS)
				{
					Log.Adventures.Print("Just Unlocked Decks: {0}", new object[]
					{
						list
					});
				}
				else if (awardedRewardsSubkey == GameSaveKeySubkeyId.DUNGEON_CRAWL_AWARDED_LOADOUT_TREASURES)
				{
					Log.Adventures.Print("Just Unlocked or Upgraded Loadout Treasures: {0}", new object[]
					{
						list
					});
				}
				if (list3 == null)
				{
					list3 = new List<long>();
				}
				list3.AddRange(list2);
				DungeonCrawlUtil.AddSubkeyValuesToGameSaveDataUpdates(list3, ref subkeyUpdates, gameSaveClientKey, awardedRewardsSubkey);
				List<long> list4;
				GameSaveDataManager.Get().GetSubkeyValue(gameSaveClientKey, newlyUnlockedRewardsSubkey, out list4);
				if (list4 == null)
				{
					list4 = new List<long>();
				}
				foreach (long item2 in list2)
				{
					if (!list4.Contains(item2))
					{
						list4.Add(item2);
					}
				}
				DungeonCrawlUtil.AddSubkeyValuesToGameSaveDataUpdates(list4, ref subkeyUpdates, gameSaveClientKey, newlyUnlockedRewardsSubkey);
			}
			return list;
		}

		// Token: 0x0600C400 RID: 50176 RVA: 0x003B2510 File Offset: 0x003B0710
		private static void AddSubkeyValuesToGameSaveDataUpdates(List<long> valuesToAdd, ref List<GameSaveDataManager.SubkeySaveRequest> subkeyUpdates, GameSaveKeyId gameSaveKey, GameSaveKeySubkeyId gameSaveSubkey)
		{
			if (valuesToAdd == null || valuesToAdd.Count <= 0 || subkeyUpdates == null)
			{
				Debug.LogWarning("Invalid parameters passed to AddSubkeyValuesToGameSaveDataUpdates()!");
				return;
			}
			List<long> list = new List<long>(valuesToAdd);
			GameSaveDataManager.SubkeySaveRequest subkeySaveRequest = subkeyUpdates.Find((GameSaveDataManager.SubkeySaveRequest r) => r.Key == gameSaveKey && r.Subkey == gameSaveSubkey);
			if (subkeySaveRequest != null)
			{
				foreach (long item in subkeySaveRequest.Long_Values)
				{
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
				subkeyUpdates.Remove(subkeySaveRequest);
			}
			subkeyUpdates.Add(new GameSaveDataManager.SubkeySaveRequest(gameSaveKey, gameSaveSubkey, list.ToArray()));
		}

		// Token: 0x0600C401 RID: 50177 RVA: 0x003B25C4 File Offset: 0x003B07C4
		public static HashSet<int> GetAchievementsForRecentUnlocks(AdventureDbId adventureID, out List<long> unlockedHeroPowers, out List<long> unlockedTreasures)
		{
			HashSet<int> hashSet = new HashSet<int>();
			unlockedHeroPowers = new List<long>();
			unlockedTreasures = new List<long>();
			AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)adventureID);
			if (record != null)
			{
				foreach (AdventureHeroPowerDbfRecord adventureHeroPowerDbfRecord in record.AdventureHeroPowers)
				{
					AchievementManager.AchievementStatus achievementStatus = AchievementManager.AchievementStatus.UNKNOWN;
					if (adventureHeroPowerDbfRecord.UnlockAchievement > 0)
					{
						achievementStatus = AchievementManager.Get().GetAchievementDataModel(adventureHeroPowerDbfRecord.UnlockAchievement).Status;
					}
					if (achievementStatus == AchievementManager.AchievementStatus.REWARD_GRANTED)
					{
						hashSet.Add(adventureHeroPowerDbfRecord.UnlockAchievement);
						unlockedHeroPowers.Add((long)adventureHeroPowerDbfRecord.CardId);
					}
				}
				foreach (AdventureLoadoutTreasuresDbfRecord adventureLoadoutTreasuresDbfRecord in record.AdventureLoadoutTreasures)
				{
					AchievementManager.AchievementStatus achievementStatus2 = AchievementManager.AchievementStatus.UNKNOWN;
					if (adventureLoadoutTreasuresDbfRecord.UnlockAchievement > 0)
					{
						achievementStatus2 = AchievementManager.Get().GetAchievementDataModel(adventureLoadoutTreasuresDbfRecord.UnlockAchievement).Status;
					}
					if (achievementStatus2 == AchievementManager.AchievementStatus.REWARD_GRANTED)
					{
						hashSet.Add(adventureLoadoutTreasuresDbfRecord.UnlockAchievement);
						unlockedTreasures.Add((long)adventureLoadoutTreasuresDbfRecord.CardId);
					}
				}
			}
			return hashSet;
		}

		// Token: 0x0600C402 RID: 50178 RVA: 0x003B2700 File Offset: 0x003B0900
		public static void MarkUnlocksAsNew(AdventureDbId adventureID, AdventureModeDbId modeId, List<long> heroPowerUnlocks = null, List<long> loadoutTreasureUnlocks = null, List<long> deckUnlocks = null)
		{
			AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureID, (int)modeId);
			if (adventureDataRecord != null)
			{
				GameSaveKeyId gameSaveDataClientKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey;
				List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
				GameSaveDataManager.SubkeySaveRequest subkeySaveRequest = GameSaveDataManager.Get().GenerateSaveRequestToAddValuesToSubkeyIfTheyDoNotExist(gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEWLY_UNLOCKED_HERO_POWERS, heroPowerUnlocks);
				if (subkeySaveRequest != null)
				{
					list.Add(subkeySaveRequest);
				}
				GameSaveDataManager.SubkeySaveRequest subkeySaveRequest2 = GameSaveDataManager.Get().GenerateSaveRequestToAddValuesToSubkeyIfTheyDoNotExist(gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEWLY_UNLOCKED_LOADOUT_TREASURES, loadoutTreasureUnlocks);
				if (subkeySaveRequest2 != null)
				{
					list.Add(subkeySaveRequest2);
				}
				GameSaveDataManager.SubkeySaveRequest subkeySaveRequest3 = GameSaveDataManager.Get().GenerateSaveRequestToAddValuesToSubkeyIfTheyDoNotExist(gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEWLY_UNLOCKED_DECKS, deckUnlocks);
				if (subkeySaveRequest3 != null)
				{
					list.Add(subkeySaveRequest3);
				}
				GameSaveDataManager.Get().SaveSubkeys(list, delegate(bool success)
				{
					if (!success)
					{
						Log.Adventures.PrintError("AdventureUtils.MarkUnlocksAsNew: Unable to save data to mark new unlocks", Array.Empty<object>());
					}
				});
				return;
			}
			Log.Adventures.PrintError("AdventureUtils.MarkUnlocksAsNew: Unable to find data record for adventure ID {0}", new object[]
			{
				adventureID
			});
		}

		// Token: 0x0600C403 RID: 50179 RVA: 0x003B27CC File Offset: 0x003B09CC
		public static void AcknowledgeUnlocks(HashSet<int> achievementUnlocks)
		{
			foreach (int achievementId in achievementUnlocks)
			{
				AchievementManager.Get().AckAchievement(achievementId);
			}
		}

		// Token: 0x04009D04 RID: 40196
		private static readonly PlatformDependentValue<string> DUNGEON_RUN_PREFAB_ASSET = new PlatformDependentValue<string>(PlatformCategory.Screen)
		{
			PC = "AdventureDungeonCrawl.prefab:13231000c27ce7d4ebf2bff57e48b8c2",
			Phone = "AdventureDungeonCrawl_phone.prefab:f28e6d94ab29c6145a390a6a2346195a"
		};

		// Token: 0x02002931 RID: 10545
		// (Invoke) Token: 0x06013E3C RID: 81468
		public delegate void DungeonRunLoadCallback(GameObject go);

		// Token: 0x02002932 RID: 10546
		// (Invoke) Token: 0x06013E40 RID: 81472
		public delegate void DungeonRunDataLoadedCallback(bool succes);
	}
}
