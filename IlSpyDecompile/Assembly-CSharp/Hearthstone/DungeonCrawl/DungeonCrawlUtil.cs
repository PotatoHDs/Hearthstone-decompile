using System.Collections.Generic;
using Hearthstone.Progression;
using UnityEngine;

namespace Hearthstone.DungeonCrawl
{
	public class DungeonCrawlUtil
	{
		public delegate void DungeonRunLoadCallback(GameObject go);

		public delegate void DungeonRunDataLoadedCallback(bool succes);

		private static readonly PlatformDependentValue<string> DUNGEON_RUN_PREFAB_ASSET = new PlatformDependentValue<string>(PlatformCategory.Screen)
		{
			PC = "AdventureDungeonCrawl.prefab:13231000c27ce7d4ebf2bff57e48b8c2",
			Phone = "AdventureDungeonCrawl_phone.prefab:f28e6d94ab29c6145a390a6a2346195a"
		};

		public static DungeonCrawlServices CreateAdventureDungeonCrawlServices(AssetLoadingHelper assetLoadingHelper)
		{
			return new DungeonCrawlServices
			{
				DungeonCrawlData = new DungeonCrawlDataAdventureAdapter(),
				SubsceneController = new DungeonCrawlSubscenControllerAdapter(),
				AssetLoadingHelper = assetLoadingHelper
			};
		}

		public static DungeonCrawlServices CreateTavernBrawlDungeonCrawlServices(AdventureDbId adventureId, AdventureModeDbId modeId, AssetLoadingHelper assetLoadingHelper)
		{
			ScenarioDbId missionForAdventure = GetMissionForAdventure(adventureId, modeId);
			WingDbfRecord wingRecordFromMissionId = GameUtils.GetWingRecordFromMissionId((int)missionForAdventure);
			DungeonCrawlDataTavernBrawl.DataSet dataSet = default(DungeonCrawlDataTavernBrawl.DataSet);
			dataSet.m_selectedAdventure = adventureId;
			dataSet.m_selectedMode = modeId;
			dataSet.m_mission = missionForAdventure;
			dataSet.m_selectableHeroPowersExist = AdventureUtils.SelectableHeroPowersExistForAdventure(adventureId);
			dataSet.m_selectableDecksExist = AdventureUtils.SelectableDecksExistForAdventure(adventureId);
			dataSet.m_selectableHeroPowersAndDecksExist = AdventureUtils.SelectableHeroPowersAndDecksExistForAdventure(adventureId);
			dataSet.m_selectableLoadoutTreasuresExist = AdventureUtils.SelectableLoadoutTreasuresExistForAdventure(adventureId);
			dataSet.m_bossesInRun = AdventureConfig.GetAdventureBossesInRun(wingRecordFromMissionId);
			dataSet.m_heroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure = false;
			dataSet.m_guestHeroes = GetGuestHeroesForTavernBrawl((int)missionForAdventure);
			dataSet.m_requiresDeck = AdventureConfig.DoesMissionRequireDeck(missionForAdventure);
			dataSet.m_gameSaveClientKey = GetClientSaveKey(adventureId, modeId);
			DungeonCrawlDataTavernBrawl.DataSet data = dataSet;
			return new DungeonCrawlServices
			{
				DungeonCrawlData = new DungeonCrawlDataTavernBrawl(data),
				SubsceneController = new DungoneCrawlTavernBrawlSubsceneController(),
				AssetLoadingHelper = assetLoadingHelper
			};
		}

		public static DungeonCrawlServices CreatePvPDungeonCrawlServices(AdventureDbId adventureId, AssetLoadingHelper assetLoadingHelper)
		{
			DungeonCrawlServices dungeonCrawlServices = new DungeonCrawlServices();
			AdventureModeDbId adventureModeDbId = AdventureModeDbId.DUNGEON_CRAWL;
			ScenarioDbId missionForAdventure = GetMissionForAdventure(adventureId, adventureModeDbId);
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
				m_gameSaveClientKey = GetClientSaveKey(adventureId, adventureModeDbId)
			});
			GameSaveKeyId gSDKeyForAdventure = PvPDungeonRunScene.Get().GetGSDKeyForAdventure();
			GameSaveDataManager.Get().GetSubkeyValue(gSDKeyForAdventure, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_CLASS, out long value);
			GameSaveDataManager.Get().GetSubkeyValue(gSDKeyForAdventure, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_POWER, out long value2);
			GameSaveDataManager.Get().GetSubkeyValue(gSDKeyForAdventure, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_LOADOUT_TREASURE_ID, out long value3);
			dungeonCrawlDataPvP.SelectedHeroClass = (TAG_CLASS)value;
			dungeonCrawlDataPvP.SelectedHeroPowerDbId = value2;
			dungeonCrawlDataPvP.SelectedLoadoutTreasureDbId = value3;
			dungeonCrawlServices.DungeonCrawlData = dungeonCrawlDataPvP;
			dungeonCrawlServices.SubsceneController = new DungoneCrawlPvPSubsceneController();
			dungeonCrawlServices.AssetLoadingHelper = assetLoadingHelper;
			return dungeonCrawlServices;
		}

		public static void LoadDungeonRunPrefab(DungeonRunLoadCallback callback)
		{
			AssetReference assetRef2 = new AssetReference(DUNGEON_RUN_PREFAB_ASSET);
			AssetLoader.Get().InstantiatePrefab(assetRef2, delegate(AssetReference assetRef, GameObject go, object data)
			{
				if (callback != null)
				{
					callback(go);
				}
			}, null, AssetLoadingOptions.IgnorePrefabPosition);
		}

		public static bool IsDungeonRunDataReady(AdventureDbId adventureId, AdventureModeDbId adventureModeId)
		{
			AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)adventureModeId);
			if (adventureDataRecord == null)
			{
				return false;
			}
			GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
			GameSaveKeyId gameSaveDataClientKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey;
			if (GameSaveDataManager.Get().IsDataReady(gameSaveDataServerKey))
			{
				return GameSaveDataManager.Get().IsDataReady(gameSaveDataClientKey);
			}
			return false;
		}

		public static void ClearDungeonRunServerData(AdventureDbId adventureId, AdventureModeDbId adventureModeId)
		{
			AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)adventureModeId);
			if (adventureDataRecord != null)
			{
				GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
				GameSaveDataManager.Get().ClearLocalData(gameSaveDataServerKey);
			}
		}

		public static void LoadDungeonRunData(AdventureDbId adventureId, AdventureModeDbId adventureModeId, DungeonRunDataLoadedCallback callback)
		{
			AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)adventureModeId);
			if (adventureDataRecord == null)
			{
				if (callback != null)
				{
					callback(succes: false);
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
				resultSuccesfull &= success;
				if (serverDataRetrieved && clientDataRetrieved)
				{
					callback(resultSuccesfull);
				}
			});
			GameSaveDataManager.Get().Request(gameSaveDataClientKey, delegate(bool success)
			{
				clientDataRetrieved = true;
				resultSuccesfull &= success;
				if (serverDataRetrieved && clientDataRetrieved)
				{
					callback(resultSuccesfull);
				}
			});
		}

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
			GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_RETIRED, out long value);
			if (value > 0)
			{
				return false;
			}
			GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, out long value2);
			if (value2 > 0)
			{
				return true;
			}
			long value3 = 0L;
			long value4 = 0L;
			List<long> values = null;
			GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_LOST_TO, out value4);
			GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSSES_DEFEATED, out values);
			GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_LATEST_DUNGEON_RUN_COMPLETE, out value3);
			bool num = value4 > 0;
			bool flag = values != null && values.Count > 0;
			bool flag2 = num || flag;
			return value3 == 0 && flag2;
		}

		private static ScenarioDbId GetMissionForAdventure(AdventureDbId adv, AdventureModeDbId mode)
		{
			List<ScenarioDbfRecord> records = GameDbf.Scenario.GetRecords((ScenarioDbfRecord r) => r.AdventureId == (int)adv && r.ModeId == (int)mode && r.WingId != 0);
			if (records == null || records.Count < 1)
			{
				return ScenarioDbId.INVALID;
			}
			if (records.Count > 1)
			{
				Log.Adventures.PrintWarning("Unexpected number of scenarios found for adventure {0} mode {1}. There should be only one but found {2}", adv, mode, records.Count);
			}
			return (ScenarioDbId)records[0].ID;
		}

		private static GameSaveKeyId GetClientSaveKey(AdventureDbId adv, AdventureModeDbId mode)
		{
			return (GameSaveKeyId)(GameUtils.GetAdventureDataRecord((int)adv, (int)mode)?.GameSaveDataClientKey ?? (-1));
		}

		public static List<int> GetGuestHeroesForTavernBrawl(int scenarioId)
		{
			List<ScenarioGuestHeroesDbfRecord> records = GameDbf.ScenarioGuestHeroes.GetRecords((ScenarioGuestHeroesDbfRecord r) => r.ScenarioId == scenarioId);
			records.Sort((ScenarioGuestHeroesDbfRecord a, ScenarioGuestHeroesDbfRecord b) => a.SortOrder.CompareTo(b.SortOrder));
			List<int> list = new List<int>();
			foreach (ScenarioGuestHeroesDbfRecord item in records)
			{
				list.Add(GameUtils.GetCardIdFromGuestHeroDbId(item.GuestHeroId));
			}
			return list;
		}

		public static bool MigrateDungeonCrawlSubkeys(GameSaveKeyId clientKey, GameSaveKeyId serverKey)
		{
			if (!GameSaveDataManager.Get().ValidateIfKeyCanBeAccessed(clientKey, "MigrateDungeonCrawlSubkeys") || !GameSaveDataManager.Get().ValidateIfKeyCanBeAccessed(serverKey, "MigrateDungeonCrawlSubkeys"))
			{
				Log.Adventures.Print("MigrateDungeonCrawlSubkeys called but one or both keys cannot be migrated. Client key: {0}  Server key: {1}", clientKey, serverKey);
				return false;
			}
			bool result = false;
			if (GameSaveDataManager.Get().MigrateSubkeyIntValue(clientKey, serverKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_LATEST_DUNGEON_RUN_COMPLETE, 0L))
			{
				Log.Adventures.Print("Migrated DUNGEON_CRAWL_HAS_SEEN_LATEST_DUNGEON_RUN_COMPLETE subkey from client to server key!");
				result = true;
			}
			if (GameSaveDataManager.Get().MigrateSubkeyIntValue(clientKey, serverKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_CLASS, 0L))
			{
				Log.Adventures.Print("Migrated DUNGEON_CRAWL_PLAYER_SELECTED_HERO_CLASS subkey from client to server key!");
				result = true;
			}
			return result;
		}

		public static bool IsDungeonRunActive(GameSaveKeyId serverKey)
		{
			GameSaveDataManager.Get().GetSubkeyValue(serverKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, out long value);
			GameSaveDataManager.Get().GetSubkeyValue(serverKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_RETIRED, out long value2);
			if (value > 0)
			{
				return value2 <= 0;
			}
			return false;
		}

		public static bool IsPVPDRFriendlyEncounter(int missionId)
		{
			if (missionId == 3745)
			{
				return true;
			}
			return false;
		}

		public static List<DbfRecord> CheckForNewlyUnlockedGSDRewardsOfSpecificType(IEnumerable<DbfRecord> rewardDbfRecords, IDungeonCrawlData dungeonCrawlData, GameSaveKeyId gameSaveServerKey, GameSaveKeyId gameSaveClientKey, GameSaveKeySubkeyId awardedRewardsSubkey, GameSaveKeySubkeyId newlyUnlockedRewardsSubkey, List<GameSaveDataManager.SubkeySaveRequest> subkeyUpdates, bool checkForUpgrades = false)
		{
			List<DbfRecord> list = new List<DbfRecord>();
			List<long> list2 = new List<long>();
			GameSaveDataManager.Get().GetSubkeyValue(gameSaveClientKey, awardedRewardsSubkey, out List<long> values);
			bool hasUnlockCriteria = default(bool);
			foreach (DbfRecord rewardDbfRecord in rewardDbfRecords)
			{
				int unlockAchievement = 0;
				long item;
				GameSaveKeySubkeyId unlockGameSaveSubkey;
				int unlockValue;
				if (rewardDbfRecord is AdventureHeroPowerDbfRecord)
				{
					AdventureHeroPowerDbfRecord obj = (AdventureHeroPowerDbfRecord)rewardDbfRecord;
					item = obj.CardId;
					unlockGameSaveSubkey = (GameSaveKeySubkeyId)obj.UnlockGameSaveSubkey;
					unlockValue = obj.UnlockValue;
					unlockAchievement = obj.UnlockAchievement;
				}
				else if (rewardDbfRecord is AdventureDeckDbfRecord)
				{
					AdventureDeckDbfRecord obj2 = (AdventureDeckDbfRecord)rewardDbfRecord;
					item = obj2.DeckId;
					unlockGameSaveSubkey = (GameSaveKeySubkeyId)obj2.UnlockGameSaveSubkey;
					unlockValue = obj2.UnlockValue;
				}
				else
				{
					if (!(rewardDbfRecord is AdventureLoadoutTreasuresDbfRecord))
					{
						Log.Adventures.PrintWarning("Unsupported DbfRecord type in CheckForNewlyUnlockedRewardsOfSpecificType()!");
						return list;
					}
					AdventureLoadoutTreasuresDbfRecord adventureLoadoutTreasuresDbfRecord = (AdventureLoadoutTreasuresDbfRecord)rewardDbfRecord;
					if (checkForUpgrades)
					{
						item = adventureLoadoutTreasuresDbfRecord.UpgradedCardId;
						unlockGameSaveSubkey = (GameSaveKeySubkeyId)adventureLoadoutTreasuresDbfRecord.UpgradeGameSaveSubkey;
						unlockValue = adventureLoadoutTreasuresDbfRecord.UpgradeValue;
					}
					else
					{
						item = adventureLoadoutTreasuresDbfRecord.CardId;
						unlockGameSaveSubkey = (GameSaveKeySubkeyId)adventureLoadoutTreasuresDbfRecord.UnlockGameSaveSubkey;
						unlockValue = adventureLoadoutTreasuresDbfRecord.UnlockValue;
					}
					unlockAchievement = adventureLoadoutTreasuresDbfRecord.UnlockAchievement;
				}
				if (!(values?.Contains(item) ?? false) && dungeonCrawlData.AdventureRewardIsUnlocked(gameSaveServerKey, unlockGameSaveSubkey, unlockValue, unlockAchievement, out var _, out hasUnlockCriteria) && hasUnlockCriteria)
				{
					list.Add(rewardDbfRecord);
					list2.Add(item);
				}
			}
			if (list.Count > 0)
			{
				switch (awardedRewardsSubkey)
				{
				case GameSaveKeySubkeyId.DUNGEON_CRAWL_AWARDED_HERO_POWERS:
					Log.Adventures.Print("Just Unlocked Hero Powers: {0}", list);
					break;
				case GameSaveKeySubkeyId.DUNGEON_CRAWL_AWARDED_DECKS:
					Log.Adventures.Print("Just Unlocked Decks: {0}", list);
					break;
				case GameSaveKeySubkeyId.DUNGEON_CRAWL_AWARDED_LOADOUT_TREASURES:
					Log.Adventures.Print("Just Unlocked or Upgraded Loadout Treasures: {0}", list);
					break;
				}
				if (values == null)
				{
					values = new List<long>();
				}
				values.AddRange(list2);
				AddSubkeyValuesToGameSaveDataUpdates(values, ref subkeyUpdates, gameSaveClientKey, awardedRewardsSubkey);
				GameSaveDataManager.Get().GetSubkeyValue(gameSaveClientKey, newlyUnlockedRewardsSubkey, out List<long> values2);
				if (values2 == null)
				{
					values2 = new List<long>();
				}
				foreach (long item2 in list2)
				{
					if (!values2.Contains(item2))
					{
						values2.Add(item2);
					}
				}
				AddSubkeyValuesToGameSaveDataUpdates(values2, ref subkeyUpdates, gameSaveClientKey, newlyUnlockedRewardsSubkey);
			}
			return list;
		}

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
				long[] long_Values = subkeySaveRequest.Long_Values;
				foreach (long item in long_Values)
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

		public static HashSet<int> GetAchievementsForRecentUnlocks(AdventureDbId adventureID, out List<long> unlockedHeroPowers, out List<long> unlockedTreasures)
		{
			HashSet<int> hashSet = new HashSet<int>();
			unlockedHeroPowers = new List<long>();
			unlockedTreasures = new List<long>();
			AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)adventureID);
			if (record != null)
			{
				foreach (AdventureHeroPowerDbfRecord adventureHeroPower in record.AdventureHeroPowers)
				{
					AchievementManager.AchievementStatus achievementStatus = AchievementManager.AchievementStatus.UNKNOWN;
					if (adventureHeroPower.UnlockAchievement > 0)
					{
						achievementStatus = AchievementManager.Get().GetAchievementDataModel(adventureHeroPower.UnlockAchievement).Status;
					}
					if (achievementStatus == AchievementManager.AchievementStatus.REWARD_GRANTED)
					{
						hashSet.Add(adventureHeroPower.UnlockAchievement);
						unlockedHeroPowers.Add(adventureHeroPower.CardId);
					}
				}
				{
					foreach (AdventureLoadoutTreasuresDbfRecord adventureLoadoutTreasure in record.AdventureLoadoutTreasures)
					{
						AchievementManager.AchievementStatus achievementStatus2 = AchievementManager.AchievementStatus.UNKNOWN;
						if (adventureLoadoutTreasure.UnlockAchievement > 0)
						{
							achievementStatus2 = AchievementManager.Get().GetAchievementDataModel(adventureLoadoutTreasure.UnlockAchievement).Status;
						}
						if (achievementStatus2 == AchievementManager.AchievementStatus.REWARD_GRANTED)
						{
							hashSet.Add(adventureLoadoutTreasure.UnlockAchievement);
							unlockedTreasures.Add(adventureLoadoutTreasure.CardId);
						}
					}
					return hashSet;
				}
			}
			return hashSet;
		}

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
						Log.Adventures.PrintError("AdventureUtils.MarkUnlocksAsNew: Unable to save data to mark new unlocks");
					}
				});
			}
			else
			{
				Log.Adventures.PrintError("AdventureUtils.MarkUnlocksAsNew: Unable to find data record for adventure ID {0}", adventureID);
			}
		}

		public static void AcknowledgeUnlocks(HashSet<int> achievementUnlocks)
		{
			foreach (int achievementUnlock in achievementUnlocks)
			{
				AchievementManager.Get().AckAchievement(achievementUnlock);
			}
		}
	}
}
