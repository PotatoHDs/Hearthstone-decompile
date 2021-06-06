using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;
using Hearthstone.Core;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

public class GameSaveDataManager
{
	public struct AdventureDungeonCrawlClassProgressSubkeys
	{
		public GameSaveKeySubkeyId bossWins;

		public GameSaveKeySubkeyId runWins;
	}

	public struct AdventureDungeonCrawlWingProgressSubkeys
	{
		public GameSaveKeySubkeyId heroCardWins;

		public GameSaveKeySubkeyId heroPowerWins;

		public GameSaveKeySubkeyId deckWins;

		public GameSaveKeySubkeyId treasureWins;
	}

	public struct GameSaveKeyTuple
	{
		public GameSaveKeyId Key;

		public GameSaveKeySubkeyId Subkey;

		public GameSaveKeyTuple(GameSaveKeyId key, GameSaveKeySubkeyId subkey)
		{
			Key = key;
			Subkey = subkey;
		}

		public override bool Equals(object obj)
		{
			if (obj is GameSaveKeyTuple)
			{
				return Equals((GameSaveKeyTuple)obj);
			}
			return false;
		}

		public bool Equals(GameSaveKeyTuple p)
		{
			if (Key == p.Key)
			{
				return Subkey == p.Subkey;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (int)Key ^ (int)Subkey;
		}
	}

	public class SubkeySaveRequest
	{
		public readonly GameSaveKeyId Key;

		public readonly GameSaveKeySubkeyId Subkey;

		public readonly long[] Long_Values;

		public readonly string[] String_Values;

		public SubkeySaveRequest(GameSaveKeyId key, GameSaveKeySubkeyId subkey, params long[] values)
		{
			Key = key;
			Subkey = subkey;
			Long_Values = values;
		}

		public SubkeySaveRequest(GameSaveKeyId key, GameSaveKeySubkeyId subkey, params string[] values)
		{
			Key = key;
			Subkey = subkey;
			String_Values = values;
		}
	}

	private class PendingRequestContext
	{
		public readonly List<GameSaveKeyId> AffectedKeys = new List<GameSaveKeyId>();

		public readonly OnRequestDataResponseDelegate RequestCallback;

		public readonly OnSaveDataResponseDelegate SaveCallback;

		public PendingRequestContext(List<GameSaveKeyId> requestedKeys, OnRequestDataResponseDelegate requestCallback)
		{
			AffectedKeys.AddRange(requestedKeys);
			RequestCallback = requestCallback;
		}

		public PendingRequestContext(List<SubkeySaveRequest> requests, OnSaveDataResponseDelegate saveCallback)
		{
			foreach (SubkeySaveRequest request in requests)
			{
				AffectedKeys.Add(request.Key);
			}
			SaveCallback = saveCallback;
		}
	}

	private class ServerOptionFlagMigrationData
	{
		public readonly GameSaveKeyId KeyId;

		public readonly GameSaveKeySubkeyId SubkeyId;

		public readonly int FlagTrueValue;

		public readonly int FlagFalseValue;

		public ServerOptionFlagMigrationData(GameSaveKeyId keyId, GameSaveKeySubkeyId subkeyId, int flagTrueValue = 1, int flagFalseValue = 0)
		{
			FlagTrueValue = flagTrueValue;
			FlagFalseValue = flagFalseValue;
			KeyId = keyId;
			SubkeyId = subkeyId;
		}
	}

	public delegate void OnRequestDataResponseDelegate(bool success);

	public delegate void OnSaveDataResponseDelegate(bool success);

	private static int s_clientToken = 0;

	private static readonly Map<TAG_CLASS, AdventureDungeonCrawlClassProgressSubkeys> AdventureDungeonCrawlClassToSubkeyMapping;

	private static readonly List<AdventureDungeonCrawlWingProgressSubkeys> ProgressSubkeysForDungeonCrawlWings;

	private const float BATCHED_SAVE_SUBKEY_REQUEST_RATE = 1f;

	private static GameSaveDataManager s_instance;

	private Map<GameSaveKeyId, Map<GameSaveKeySubkeyId, GameSaveDataValue>> m_gameSaveDataMapByKey = new Map<GameSaveKeyId, Map<GameSaveKeySubkeyId, GameSaveDataValue>>();

	private Dictionary<GameSaveKeyId, bool> m_isRequestPendingForKey;

	private Dictionary<int, PendingRequestContext> m_pendingRequestsByClientToken = new Dictionary<int, PendingRequestContext>();

	private List<GameSaveDataUpdate> m_batchedSaveUpdates = new List<GameSaveDataUpdate>();

	private List<SubkeySaveRequest> m_batchedSubkeySaveRequests = new List<SubkeySaveRequest>();

	private List<OnSaveDataResponseDelegate> m_batchedSaveUpdateCallbacks = new List<OnSaveDataResponseDelegate>();

	private DateTime m_timeOfLastSetGameSaveDataRequest;

	public static bool IsGameSaveKeyValid(GameSaveKeyId key)
	{
		if (GameSaveKeyId.INVALID != key)
		{
			return key != (GameSaveKeyId)0;
		}
		return false;
	}

	public bool IsDataReady(GameSaveKeyId key)
	{
		if (!IsGameSaveKeyValid(key))
		{
			Debug.LogWarning("GameSaveDataManager.IsDataReady() called with an invalid key ID!");
			return false;
		}
		return m_gameSaveDataMapByKey.ContainsKey(key);
	}

	public GameSaveDataManager()
	{
		Network.Get().RegisterNetHandler(GameSaveDataResponse.PacketID.ID, OnRequestGameSaveDataResponse);
		Network.Get().RegisterNetHandler(SetGameSaveDataResponse.PacketID.ID, OnSetGameSaveDataResponse);
		FatalErrorMgr.Get().AddErrorListener(OnFatalError);
		HandleGameSaveDataMigration();
		HearthstoneApplication.Get().WillReset += OnWillReset;
		m_timeOfLastSetGameSaveDataRequest = DateTime.Now;
	}

	private void OnRequestGameSaveDataResponse()
	{
		bool flag = false;
		GameSaveDataResponse gameSaveDataResponse = Network.Get().GetGameSaveDataResponse();
		if (gameSaveDataResponse.ErrorCode != 0)
		{
			Log.All.PrintError("GameSaveDataManager.OnRequestGameSaveDataResponse() - GameSaveDataResponse has error code {0} (error #{1})", gameSaveDataResponse.ErrorCode, (int)gameSaveDataResponse.ErrorCode);
			flag = true;
		}
		if (!flag)
		{
			ReadGameSaveDataUpdates(gameSaveDataResponse.Data);
		}
		if (m_pendingRequestsByClientToken.TryGetValue(gameSaveDataResponse.ClientToken, out var value))
		{
			m_pendingRequestsByClientToken.Remove(gameSaveDataResponse.ClientToken);
			if (value.RequestCallback != null)
			{
				value.RequestCallback(!flag);
			}
		}
	}

	private void OnSetGameSaveDataResponse()
	{
		bool flag = false;
		SetGameSaveDataResponse setGameSaveDataResponse = Network.Get().GetSetGameSaveDataResponse();
		if (setGameSaveDataResponse.ErrorCode != 0)
		{
			Log.All.PrintError("GameSaveDataManager.OnSetGameSaveDataResponse() - SetGameSaveDataResponse has error code {0}", setGameSaveDataResponse.ErrorCode);
			flag = true;
		}
		if (!flag)
		{
			ReadGameSaveDataUpdates(setGameSaveDataResponse.Data);
		}
		if (m_pendingRequestsByClientToken.TryGetValue(setGameSaveDataResponse.ClientToken, out var value))
		{
			m_pendingRequestsByClientToken.Remove(setGameSaveDataResponse.ClientToken);
			if (value.SaveCallback != null)
			{
				value.SaveCallback(!flag);
			}
		}
	}

	private void HandleGameSaveDataMigration()
	{
		List<SubkeySaveRequest> list = new List<SubkeySaveRequest>();
		foreach (KeyValuePair<Option, ServerOptionFlagMigrationData> item in new Dictionary<Option, ServerOptionFlagMigrationData>
		{
			{
				Option.HAS_SEEN_LOOT_BOSS_HERO_POWER,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_HERO_POWER_TUTORIAL_PROGRESS, 2)
			},
			{
				Option.HAS_SEEN_LOOT_COMPLETE_ALL_CLASSES_VO,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_COMPLETE_ALL_CLASSES_VO)
			},
			{
				Option.HAS_SEEN_LATEST_DUNGEON_RUN_COMPLETE,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_SERVER_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_LATEST_DUNGEON_RUN_COMPLETE)
			},
			{
				Option.HAS_SEEN_LOOT_CHARACTER_SELECT_VO,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_CHARACTER_SELECT_VO)
			},
			{
				Option.HAS_SEEN_LOOT_WELCOME_BANNER_VO,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_WELCOME_BANNER_VO)
			},
			{
				Option.HAS_SEEN_LOOT_BOSS_FLIP_1_VO,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_1_VO)
			},
			{
				Option.HAS_SEEN_LOOT_BOSS_FLIP_2_VO,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_2_VO)
			},
			{
				Option.HAS_SEEN_LOOT_BOSS_FLIP_3_VO,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_3_VO)
			},
			{
				Option.HAS_SEEN_LOOT_OFFER_TREASURE_1_VO,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_OFFER_TREASURE_1_VO)
			},
			{
				Option.HAS_SEEN_LOOT_OFFER_LOOT_PACKS_1_VO,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_OFFER_LOOT_PACKS_1_VO)
			},
			{
				Option.HAS_SEEN_LOOT_OFFER_LOOT_PACKS_2_VO,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_OFFER_LOOT_PACKS_2_VO)
			},
			{
				Option.HAS_SEEN_LOOT_IN_GAME_WIN_VO,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_WIN_VO)
			},
			{
				Option.HAS_SEEN_LOOT_IN_GAME_LOSE_VO,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_VO)
			},
			{
				Option.HAS_SEEN_LOOT_IN_GAME_MULLIGAN_1_VO,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_1_VO)
			},
			{
				Option.HAS_SEEN_LOOT_IN_GAME_MULLIGAN_2_VO,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_2_VO)
			},
			{
				Option.HAS_SEEN_LOOT_IN_GAME_LOSE_2_VO,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_2_VO)
			},
			{
				Option.HAS_SEEN_NAXX,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_NAXX, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_ADVENTURE)
			},
			{
				Option.HAS_SEEN_BRM,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_BRM, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_ADVENTURE)
			},
			{
				Option.HAS_SEEN_LOE,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOE, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_ADVENTURE)
			},
			{
				Option.HAS_SEEN_KARA,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_KARA, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_ADVENTURE)
			},
			{
				Option.HAS_SEEN_ICC,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_ICC, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_ADVENTURE)
			},
			{
				Option.HAS_SEEN_LOOT,
				new ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_ADVENTURE)
			}
		})
		{
			Option key = item.Key;
			GameSaveKeyId keyId = item.Value.KeyId;
			GameSaveKeySubkeyId subkeyId = item.Value.SubkeyId;
			int flagTrueValue = item.Value.FlagTrueValue;
			int flagFalseValue = item.Value.FlagFalseValue;
			if (Options.Get().HasOption(key))
			{
				int num = (Options.Get().GetBool(key) ? flagTrueValue : flagFalseValue);
				list.Add(new SubkeySaveRequest(keyId, subkeyId, num));
				Options.Get().DeleteOption(key);
			}
		}
		if (list.Count > 0)
		{
			SaveSubkeys(list);
		}
	}

	public void SetGameSaveDataUpdateFromInitialClientState(List<GameSaveDataUpdate> gameSaveDataUpdate)
	{
		ReadGameSaveDataUpdates(gameSaveDataUpdate);
	}

	private void ReadGameSaveDataUpdates(List<GameSaveDataUpdate> gameSaveDataUpdate)
	{
		foreach (GameSaveDataUpdate item in gameSaveDataUpdate)
		{
			if (item.Tuple.Count < 1)
			{
				Log.All.PrintWarning("GameSaveDataManager.OnRequestGameSaveDataResponse() - Received response that contains no key");
				continue;
			}
			GameSaveKeyId gameSaveKeyId = (GameSaveKeyId)item.Tuple[0].Id;
			Map<GameSaveKeySubkeyId, GameSaveDataValue> map = new Map<GameSaveKeySubkeyId, GameSaveDataValue>();
			m_gameSaveDataMapByKey[gameSaveKeyId] = map;
			if (!item.HasValue)
			{
				Log.All.Print("GameSaveDataManager.OnRequestGameSaveDataResponse() - Received response contains no data for the requested key {0}", gameSaveKeyId);
				continue;
			}
			for (int i = 0; i < item.Value.MapKeys.Count && i < item.Value.MapValues.Count; i++)
			{
				GameSaveKeySubkeyId key = (GameSaveKeySubkeyId)item.Value.MapKeys[i];
				map[key] = item.Value.MapValues[i];
			}
		}
	}

	private bool ValidateThereAreNoPendingRequestsForKey(GameSaveKeyId key, string loggingContext)
	{
		if (IsRequestPending(key))
		{
			Log.All.PrintError("GameSaveDataManager.{0}() - Detected pending operation for key {1}", loggingContext, key);
			return false;
		}
		return true;
	}

	public void Request(GameSaveKeyId key, OnRequestDataResponseDelegate callback = null)
	{
		Request(new List<GameSaveKeyId> { key }, callback);
	}

	public void Request(List<GameSaveKeyId> keys, OnRequestDataResponseDelegate callback = null)
	{
		List<long> list = new List<long>();
		int num = ++s_clientToken;
		foreach (GameSaveKeyId key in keys)
		{
			if (ValidateThereAreNoPendingRequestsForKey(key, "Request"))
			{
				list.Add((long)key);
			}
		}
		if (list.Count > 0)
		{
			m_pendingRequestsByClientToken.Add(num, new PendingRequestContext(keys, callback));
			Network.Get().RequestGameSaveData(list, num);
		}
		else
		{
			callback?.Invoke(success: false);
		}
	}

	public bool SaveSubkey(SubkeySaveRequest request, OnSaveDataResponseDelegate callback = null)
	{
		List<SubkeySaveRequest> list = new List<SubkeySaveRequest>();
		list.Add(request);
		return SaveSubkeys(list, callback);
	}

	public bool SaveSubkeys(List<SubkeySaveRequest> requests, OnSaveDataResponseDelegate callback = null)
	{
		if (requests == null || requests.Count == 0)
		{
			Log.All.PrintError("GameSaveDataManager.SaveSubkeys() - No save requests specified");
			return false;
		}
		HashSet<GameSaveKeyTuple> hashSet = new HashSet<GameSaveKeyTuple>();
		foreach (SubkeySaveRequest request in requests)
		{
			GameSaveKeyTuple item = new GameSaveKeyTuple(request.Key, request.Subkey);
			if (hashSet.Contains(item))
			{
				Log.All.PrintError("GameSaveDataManager.SaveSubkeys() - Found multiple save requests for key {0} subkey {1}", request.Key, request.Subkey);
				return false;
			}
			hashSet.Add(item);
		}
		List<GameSaveDataUpdate> list = new List<GameSaveDataUpdate>();
		foreach (SubkeySaveRequest request2 in requests)
		{
			if (ValidateThereAreNoPendingRequestsForKey(request2.Key, "SaveSubkeys"))
			{
				GameSaveDataUpdate gameSaveDataUpdate = new GameSaveDataUpdate();
				GameSaveDataValue value = new GameSaveDataValue();
				gameSaveDataUpdate.Tuple.Add(new GameSaveKey
				{
					Id = (long)request2.Key
				});
				gameSaveDataUpdate.Tuple.Add(new GameSaveKey
				{
					Id = (long)request2.Subkey
				});
				SetGameSaveDataValueFromRequest(request2, ref value);
				gameSaveDataUpdate.Value = value;
				list.Add(gameSaveDataUpdate);
				SaveSubkeyToLocalCache(request2);
				m_batchedSubkeySaveRequests.Add(request2);
			}
		}
		if (callback != null && list.Count > 0)
		{
			m_batchedSaveUpdateCallbacks.Add(callback);
		}
		BatchGameSaveUpdates(list);
		return list.Count > 0;
	}

	private void SetGameSaveDataValueFromRequest(SubkeySaveRequest request, ref GameSaveDataValue value)
	{
		if (request.Long_Values != null && request.String_Values != null)
		{
			Log.All.PrintError("Error writing game save data: Attempting to write Long and String into the same key!");
		}
		else if (request.Long_Values != null)
		{
			value.IntValue = request.Long_Values.ToList();
		}
		else if (request.String_Values != null)
		{
			value.StringValue = request.String_Values.ToList();
		}
	}

	private void BatchGameSaveUpdates(List<GameSaveDataUpdate> saveUpdates)
	{
		if (m_batchedSaveUpdates.Count == 0)
		{
			if ((DateTime.Now - m_timeOfLastSetGameSaveDataRequest).TotalSeconds > 1.0)
			{
				Processor.RunCoroutine(SendAllBatchedGameSaveUpdatesNextFrame());
			}
			else
			{
				Processor.ScheduleCallback(1f, realTime: false, SendAllBatchedGameSaveDataUpdates);
			}
		}
		foreach (GameSaveDataUpdate update in saveUpdates)
		{
			GameSaveDataUpdate gameSaveDataUpdate = m_batchedSaveUpdates.FirstOrDefault((GameSaveDataUpdate u) => u.Tuple[0].Id == update.Tuple[0].Id && u.Tuple[1].Id == update.Tuple[1].Id);
			if (gameSaveDataUpdate != null)
			{
				m_batchedSaveUpdates.Remove(gameSaveDataUpdate);
			}
			m_batchedSaveUpdates.Add(update);
		}
	}

	public SubkeySaveRequest GenerateSaveRequestToAddValuesToSubkeyIfTheyDoNotExist(GameSaveKeyId key, GameSaveKeySubkeyId subkeyId, List<long> valuesToAdd)
	{
		if (valuesToAdd == null)
		{
			return null;
		}
		GetSubkeyValue(key, subkeyId, out List<long> values);
		if (values == null)
		{
			values = new List<long>();
		}
		bool flag = false;
		foreach (long item in valuesToAdd)
		{
			if (!values.Contains(item))
			{
				values.Add(item);
				flag = true;
			}
		}
		if (!flag)
		{
			return null;
		}
		return new SubkeySaveRequest(key, subkeyId, values.ToArray());
	}

	public SubkeySaveRequest GenerateSaveRequestToRemoveValueFromSubkeyIfItExists(GameSaveKeyId key, GameSaveKeySubkeyId subkeyId, long valueToRemove)
	{
		if (!GetSubkeyValue(key, subkeyId, out List<long> values))
		{
			return null;
		}
		if (values == null)
		{
			return null;
		}
		if (!values.Remove(valueToRemove))
		{
			return null;
		}
		return new SubkeySaveRequest(key, subkeyId, values.ToArray());
	}

	private IEnumerator SendAllBatchedGameSaveUpdatesNextFrame()
	{
		yield return new WaitForEndOfFrame();
		SendAllBatchedGameSaveDataUpdates(null);
	}

	private void SendAllBatchedGameSaveDataUpdates(object userdata)
	{
		int num = ++s_clientToken;
		foreach (OnSaveDataResponseDelegate batchedSaveUpdateCallback in m_batchedSaveUpdateCallbacks)
		{
			m_pendingRequestsByClientToken.Add(num, new PendingRequestContext(m_batchedSubkeySaveRequests, batchedSaveUpdateCallback));
		}
		Network.Get().SetGameSaveData(m_batchedSaveUpdates, num);
		m_timeOfLastSetGameSaveDataRequest = DateTime.Now;
		m_batchedSaveUpdates.Clear();
		m_batchedSubkeySaveRequests.Clear();
		m_batchedSaveUpdateCallbacks.Clear();
	}

	private void SaveSubkeyToLocalCache(SubkeySaveRequest request)
	{
		if (!m_gameSaveDataMapByKey.TryGetValue(request.Key, out var value))
		{
			value = new Map<GameSaveKeySubkeyId, GameSaveDataValue>();
			m_gameSaveDataMapByKey.Add(request.Key, value);
		}
		if (!value.TryGetValue(request.Subkey, out var value2))
		{
			value2 = new GameSaveDataValue();
			value.Add(request.Subkey, value2);
		}
		SetGameSaveDataValueFromRequest(request, ref value2);
	}

	public bool GetSubkeyValue(GameSaveKeyId key, GameSaveKeySubkeyId subkeyId, out long value)
	{
		value = 0L;
		if (GetSubkeyValue(key, subkeyId, out List<long> values))
		{
			value = values[0];
			return true;
		}
		return false;
	}

	public bool GetSubkeyValue(GameSaveKeyId key, GameSaveKeySubkeyId subkeyId, out string value)
	{
		value = "";
		if (GetSubkeyValue(key, subkeyId, out List<string> values))
		{
			value = values[0];
			return true;
		}
		return false;
	}

	public bool GetSubkeyValue(GameSaveKeyId key, GameSaveKeySubkeyId subkeyId, out List<long> values)
	{
		values = null;
		GameSaveDataValue subkeyValue = GetSubkeyValue(key, subkeyId);
		if (subkeyValue != null && subkeyValue.IntValue.Count > 0)
		{
			values = new List<long>(subkeyValue.IntValue);
			return true;
		}
		return false;
	}

	public bool GetSubkeyValue(GameSaveKeyId key, GameSaveKeySubkeyId subkeyId, out List<string> values)
	{
		values = null;
		GameSaveDataValue subkeyValue = GetSubkeyValue(key, subkeyId);
		if (subkeyValue != null && subkeyValue.StringValue.Count > 0)
		{
			values = new List<string>(subkeyValue.StringValue);
			return true;
		}
		return false;
	}

	public bool GetSubkeyValue(GameSaveKeyId key, GameSaveKeySubkeyId subkeyId, out List<double> values)
	{
		values = null;
		GameSaveDataValue subkeyValue = GetSubkeyValue(key, subkeyId);
		if (subkeyValue != null && subkeyValue.FloatValue.Count > 0)
		{
			values = new List<double>(subkeyValue.FloatValue);
			return true;
		}
		return false;
	}

	public List<GameSaveKeySubkeyId> GetAllSubkeysForKey(GameSaveKeyId key)
	{
		List<GameSaveKeySubkeyId> result = new List<GameSaveKeySubkeyId>();
		Map<GameSaveKeySubkeyId, GameSaveDataValue> value = null;
		if (m_gameSaveDataMapByKey.TryGetValue(key, out value))
		{
			result = new List<GameSaveKeySubkeyId>(value.Keys);
		}
		return result;
	}

	public void ClearLocalData(GameSaveKeyId key)
	{
		if (ValidateThereAreNoPendingRequestsForKey(key, "ClearLocalData"))
		{
			m_gameSaveDataMapByKey.Remove(key);
		}
	}

	public bool ValidateIfKeyCanBeAccessed(GameSaveKeyId key, string loggingContext)
	{
		if (!IsGameSaveKeyValid(key))
		{
			Log.All.PrintWarning("GameSaveDataManager.ValidateKeyCanBeAccessed() called with invalid key ID {0}!  Context: {1}\nStack Trace:\n{2}", key, loggingContext, StackTraceUtility.ExtractStackTrace());
			return false;
		}
		if (IsRequestPending(key))
		{
			Log.All.PrintWarning("GameSaveDataManager.ValidateKeyCanBeAccessed() - Request for key {0} is pending!  Context: {1}\nStack Trace:\n{2}", key, loggingContext, StackTraceUtility.ExtractStackTrace());
			return false;
		}
		if (!IsDataReady(key))
		{
			Log.All.Print("GameSaveDataManager.ValidateKeyCanBeAccessed() - Key {0} has no data - it has either not been created yet, or has not been requested.  Context: {1}\nStack Trace:\n{2}", key, loggingContext, StackTraceUtility.ExtractStackTrace());
			return false;
		}
		return true;
	}

	public bool IsRequestPending(GameSaveKeyId key)
	{
		foreach (PendingRequestContext value in m_pendingRequestsByClientToken.Values)
		{
			if (value.AffectedKeys.IndexOf(key) >= 0)
			{
				return true;
			}
		}
		return false;
	}

	public static GameSaveDataManager Get()
	{
		if (s_instance == null)
		{
			s_instance = new GameSaveDataManager();
		}
		return s_instance;
	}

	public static bool GetProgressSubkeysForDungeonCrawlWing(WingDbfRecord wingRecord, out AdventureDungeonCrawlWingProgressSubkeys progressSubkeys)
	{
		if (wingRecord == null)
		{
			Log.Adventures.PrintWarning("GetProgressSubkeysForDungeonCrawlWing: wingRecord is null!");
			progressSubkeys = default(AdventureDungeonCrawlWingProgressSubkeys);
			return false;
		}
		int sortedWingUnlockIndex = GameUtils.GetSortedWingUnlockIndex(wingRecord);
		if (sortedWingUnlockIndex < 0 || sortedWingUnlockIndex >= ProgressSubkeysForDungeonCrawlWings.Count)
		{
			Log.Adventures.PrintWarning("GetProgressSubkeysForDungeonCrawlWing: could not find a valid Sorted Wing Unlock Index for WingDbfRecord {0} - WingIndex: {1}!", wingRecord.ID, sortedWingUnlockIndex);
			progressSubkeys = default(AdventureDungeonCrawlWingProgressSubkeys);
			return false;
		}
		progressSubkeys = ProgressSubkeysForDungeonCrawlWings[sortedWingUnlockIndex];
		return true;
	}

	public static bool GetProgressSubkeyForDungeonCrawlClass(TAG_CLASS tagClass, out AdventureDungeonCrawlClassProgressSubkeys progressSubkeys)
	{
		if (AdventureDungeonCrawlClassToSubkeyMapping.ContainsKey(tagClass))
		{
			progressSubkeys = AdventureDungeonCrawlClassToSubkeyMapping[tagClass];
			return true;
		}
		progressSubkeys = default(AdventureDungeonCrawlClassProgressSubkeys);
		return false;
	}

	public static List<TAG_CLASS> GetClassesFromDungeonCrawlProgressMap()
	{
		return AdventureDungeonCrawlClassToSubkeyMapping.Keys.ToList();
	}

	public void Cheat_SaveSubkeyToLocalCache(GameSaveKeyId key, GameSaveKeySubkeyId subkey, params long[] values)
	{
		if (HearthstoneApplication.IsInternal())
		{
			SubkeySaveRequest request = new SubkeySaveRequest(key, subkey, values);
			SaveSubkeyToLocalCache(request);
		}
	}

	public bool MigrateSubkeyIntValue(GameSaveKeyId sourceKey, GameSaveKeyId destinationKey, GameSaveKeySubkeyId subkeyId, long emptyValueForSource = 0L)
	{
		GameSaveDataValue subkeyValue = GetSubkeyValue(sourceKey, subkeyId);
		if (subkeyValue == null || subkeyValue.IntValue == null || subkeyValue.IntValue.Count < 1 || subkeyValue.IntValue[0] == emptyValueForSource)
		{
			return false;
		}
		long num = subkeyValue.IntValue[0];
		List<SubkeySaveRequest> list = new List<SubkeySaveRequest>();
		list.Add(new SubkeySaveRequest(destinationKey, subkeyId, num));
		list.Add(new SubkeySaveRequest(sourceKey, subkeyId, emptyValueForSource));
		return SaveSubkeys(list);
	}

	private GameSaveDataValue GetSubkeyValue(GameSaveKeyId key, GameSaveKeySubkeyId subkeyId)
	{
		if (!IsDataReady(key))
		{
			Debug.LogErrorFormat("Attempting to get subkey {0} from key {1} failed, key not received by client yet", subkeyId, key);
			return null;
		}
		if (m_gameSaveDataMapByKey.TryGetValue(key, out var value) && value != null && value.TryGetValue(subkeyId, out var value2))
		{
			return value2;
		}
		return null;
	}

	private static void OnWillReset()
	{
		HearthstoneApplication.Get().WillReset -= OnWillReset;
		s_instance = new GameSaveDataManager();
	}

	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		m_pendingRequestsByClientToken.Clear();
	}

	static GameSaveDataManager()
	{
		Map<TAG_CLASS, AdventureDungeonCrawlClassProgressSubkeys> map = new Map<TAG_CLASS, AdventureDungeonCrawlClassProgressSubkeys>();
		AdventureDungeonCrawlClassProgressSubkeys value = new AdventureDungeonCrawlClassProgressSubkeys
		{
			bossWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_PALADIN_BOSS_WINS,
			runWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_PALADIN_RUN_WINS
		};
		map.Add(TAG_CLASS.PALADIN, value);
		value = new AdventureDungeonCrawlClassProgressSubkeys
		{
			bossWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HUNTER_BOSS_WINS,
			runWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HUNTER_RUN_WINS
		};
		map.Add(TAG_CLASS.HUNTER, value);
		value = new AdventureDungeonCrawlClassProgressSubkeys
		{
			bossWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_MAGE_BOSS_WINS,
			runWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_MAGE_RUN_WINS
		};
		map.Add(TAG_CLASS.MAGE, value);
		value = new AdventureDungeonCrawlClassProgressSubkeys
		{
			bossWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_SHAMAN_BOSS_WINS,
			runWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_SHAMAN_RUN_WINS
		};
		map.Add(TAG_CLASS.SHAMAN, value);
		value = new AdventureDungeonCrawlClassProgressSubkeys
		{
			bossWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_WARRIOR_BOSS_WINS,
			runWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_WARRIOR_RUN_WINS
		};
		map.Add(TAG_CLASS.WARRIOR, value);
		value = new AdventureDungeonCrawlClassProgressSubkeys
		{
			bossWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_ROGUE_BOSS_WINS,
			runWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_ROGUE_RUN_WINS
		};
		map.Add(TAG_CLASS.ROGUE, value);
		value = new AdventureDungeonCrawlClassProgressSubkeys
		{
			bossWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_WARLOCK_BOSS_WINS,
			runWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_WARLOCK_RUN_WINS
		};
		map.Add(TAG_CLASS.WARLOCK, value);
		value = new AdventureDungeonCrawlClassProgressSubkeys
		{
			bossWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_PRIEST_BOSS_WINS,
			runWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_PRIEST_RUN_WINS
		};
		map.Add(TAG_CLASS.PRIEST, value);
		value = new AdventureDungeonCrawlClassProgressSubkeys
		{
			bossWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_DRUID_BOSS_WINS,
			runWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_DRUID_RUN_WINS
		};
		map.Add(TAG_CLASS.DRUID, value);
		value = new AdventureDungeonCrawlClassProgressSubkeys
		{
			bossWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_DEMON_HUNTER_BOSS_WINS,
			runWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_DEMON_HUNTER_RUN_WINS
		};
		map.Add(TAG_CLASS.DEMONHUNTER, value);
		AdventureDungeonCrawlClassToSubkeyMapping = map;
		List<AdventureDungeonCrawlWingProgressSubkeys> list = new List<AdventureDungeonCrawlWingProgressSubkeys>();
		AdventureDungeonCrawlWingProgressSubkeys item = new AdventureDungeonCrawlWingProgressSubkeys
		{
			heroCardWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_CARD_WING_1_WINS,
			deckWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_WING_1_WINS,
			heroPowerWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_POWER_WING_1_WINS,
			treasureWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_TREASURE_WING_1_WINS
		};
		list.Add(item);
		item = new AdventureDungeonCrawlWingProgressSubkeys
		{
			heroCardWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_CARD_WING_2_WINS,
			deckWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_WING_2_WINS,
			heroPowerWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_POWER_WING_2_WINS,
			treasureWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_TREASURE_WING_2_WINS
		};
		list.Add(item);
		item = new AdventureDungeonCrawlWingProgressSubkeys
		{
			heroCardWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_CARD_WING_3_WINS,
			deckWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_WING_3_WINS,
			heroPowerWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_POWER_WING_3_WINS,
			treasureWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_TREASURE_WING_3_WINS
		};
		list.Add(item);
		item = new AdventureDungeonCrawlWingProgressSubkeys
		{
			heroCardWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_CARD_WING_4_WINS,
			deckWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_WING_4_WINS,
			heroPowerWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_POWER_WING_4_WINS,
			treasureWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_TREASURE_WING_4_WINS
		};
		list.Add(item);
		item = new AdventureDungeonCrawlWingProgressSubkeys
		{
			heroCardWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_CARD_WING_5_WINS,
			deckWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_WING_5_WINS,
			heroPowerWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_POWER_WING_5_WINS,
			treasureWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_TREASURE_WING_5_WINS
		};
		list.Add(item);
		ProgressSubkeysForDungeonCrawlWings = list;
		s_instance = null;
	}
}
