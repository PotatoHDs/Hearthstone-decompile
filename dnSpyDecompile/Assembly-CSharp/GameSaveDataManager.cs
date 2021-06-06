using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;
using Hearthstone.Core;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x020008C7 RID: 2247
public class GameSaveDataManager
{
	// Token: 0x06007C1A RID: 31770 RVA: 0x002851B8 File Offset: 0x002833B8
	public static bool IsGameSaveKeyValid(GameSaveKeyId key)
	{
		return GameSaveKeyId.INVALID != key && key > (GameSaveKeyId)0;
	}

	// Token: 0x06007C1B RID: 31771 RVA: 0x002851C4 File Offset: 0x002833C4
	public bool IsDataReady(GameSaveKeyId key)
	{
		if (!GameSaveDataManager.IsGameSaveKeyValid(key))
		{
			Debug.LogWarning("GameSaveDataManager.IsDataReady() called with an invalid key ID!");
			return false;
		}
		return this.m_gameSaveDataMapByKey.ContainsKey(key);
	}

	// Token: 0x06007C1C RID: 31772 RVA: 0x002851E8 File Offset: 0x002833E8
	public GameSaveDataManager()
	{
		Network.Get().RegisterNetHandler(GameSaveDataResponse.PacketID.ID, new Network.NetHandler(this.OnRequestGameSaveDataResponse), null);
		Network.Get().RegisterNetHandler(SetGameSaveDataResponse.PacketID.ID, new Network.NetHandler(this.OnSetGameSaveDataResponse), null);
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		this.HandleGameSaveDataMigration();
		HearthstoneApplication.Get().WillReset += GameSaveDataManager.OnWillReset;
		this.m_timeOfLastSetGameSaveDataRequest = DateTime.Now;
	}

	// Token: 0x06007C1D RID: 31773 RVA: 0x002852B4 File Offset: 0x002834B4
	private void OnRequestGameSaveDataResponse()
	{
		bool flag = false;
		GameSaveDataResponse gameSaveDataResponse = Network.Get().GetGameSaveDataResponse();
		if (gameSaveDataResponse.ErrorCode != ErrorCode.ERROR_OK)
		{
			Log.All.PrintError("GameSaveDataManager.OnRequestGameSaveDataResponse() - GameSaveDataResponse has error code {0} (error #{1})", new object[]
			{
				gameSaveDataResponse.ErrorCode,
				(int)gameSaveDataResponse.ErrorCode
			});
			flag = true;
		}
		if (!flag)
		{
			this.ReadGameSaveDataUpdates(gameSaveDataResponse.Data);
		}
		GameSaveDataManager.PendingRequestContext pendingRequestContext;
		if (this.m_pendingRequestsByClientToken.TryGetValue(gameSaveDataResponse.ClientToken, out pendingRequestContext))
		{
			this.m_pendingRequestsByClientToken.Remove(gameSaveDataResponse.ClientToken);
			if (pendingRequestContext.RequestCallback != null)
			{
				pendingRequestContext.RequestCallback(!flag);
			}
		}
	}

	// Token: 0x06007C1E RID: 31774 RVA: 0x00285358 File Offset: 0x00283558
	private void OnSetGameSaveDataResponse()
	{
		bool flag = false;
		SetGameSaveDataResponse setGameSaveDataResponse = Network.Get().GetSetGameSaveDataResponse();
		if (setGameSaveDataResponse.ErrorCode != ErrorCode.ERROR_OK)
		{
			Log.All.PrintError("GameSaveDataManager.OnSetGameSaveDataResponse() - SetGameSaveDataResponse has error code {0}", new object[]
			{
				setGameSaveDataResponse.ErrorCode
			});
			flag = true;
		}
		if (!flag)
		{
			this.ReadGameSaveDataUpdates(setGameSaveDataResponse.Data);
		}
		GameSaveDataManager.PendingRequestContext pendingRequestContext;
		if (this.m_pendingRequestsByClientToken.TryGetValue(setGameSaveDataResponse.ClientToken, out pendingRequestContext))
		{
			this.m_pendingRequestsByClientToken.Remove(setGameSaveDataResponse.ClientToken);
			if (pendingRequestContext.SaveCallback != null)
			{
				pendingRequestContext.SaveCallback(!flag);
			}
		}
	}

	// Token: 0x06007C1F RID: 31775 RVA: 0x002853EC File Offset: 0x002835EC
	private void HandleGameSaveDataMigration()
	{
		List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
		foreach (KeyValuePair<Option, GameSaveDataManager.ServerOptionFlagMigrationData> keyValuePair in new Dictionary<Option, GameSaveDataManager.ServerOptionFlagMigrationData>
		{
			{
				Option.HAS_SEEN_LOOT_BOSS_HERO_POWER,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_HERO_POWER_TUTORIAL_PROGRESS, 2, 0)
			},
			{
				Option.HAS_SEEN_LOOT_COMPLETE_ALL_CLASSES_VO,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_COMPLETE_ALL_CLASSES_VO, 1, 0)
			},
			{
				Option.HAS_SEEN_LATEST_DUNGEON_RUN_COMPLETE,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_SERVER_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_LATEST_DUNGEON_RUN_COMPLETE, 1, 0)
			},
			{
				Option.HAS_SEEN_LOOT_CHARACTER_SELECT_VO,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_CHARACTER_SELECT_VO, 1, 0)
			},
			{
				Option.HAS_SEEN_LOOT_WELCOME_BANNER_VO,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_WELCOME_BANNER_VO, 1, 0)
			},
			{
				Option.HAS_SEEN_LOOT_BOSS_FLIP_1_VO,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_1_VO, 1, 0)
			},
			{
				Option.HAS_SEEN_LOOT_BOSS_FLIP_2_VO,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_2_VO, 1, 0)
			},
			{
				Option.HAS_SEEN_LOOT_BOSS_FLIP_3_VO,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_3_VO, 1, 0)
			},
			{
				Option.HAS_SEEN_LOOT_OFFER_TREASURE_1_VO,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_OFFER_TREASURE_1_VO, 1, 0)
			},
			{
				Option.HAS_SEEN_LOOT_OFFER_LOOT_PACKS_1_VO,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_OFFER_LOOT_PACKS_1_VO, 1, 0)
			},
			{
				Option.HAS_SEEN_LOOT_OFFER_LOOT_PACKS_2_VO,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_OFFER_LOOT_PACKS_2_VO, 1, 0)
			},
			{
				Option.HAS_SEEN_LOOT_IN_GAME_WIN_VO,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_WIN_VO, 1, 0)
			},
			{
				Option.HAS_SEEN_LOOT_IN_GAME_LOSE_VO,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_VO, 1, 0)
			},
			{
				Option.HAS_SEEN_LOOT_IN_GAME_MULLIGAN_1_VO,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_1_VO, 1, 0)
			},
			{
				Option.HAS_SEEN_LOOT_IN_GAME_MULLIGAN_2_VO,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_2_VO, 1, 0)
			},
			{
				Option.HAS_SEEN_LOOT_IN_GAME_LOSE_2_VO,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_2_VO, 1, 0)
			},
			{
				Option.HAS_SEEN_NAXX,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_NAXX, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_ADVENTURE, 1, 0)
			},
			{
				Option.HAS_SEEN_BRM,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_BRM, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_ADVENTURE, 1, 0)
			},
			{
				Option.HAS_SEEN_LOE,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOE, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_ADVENTURE, 1, 0)
			},
			{
				Option.HAS_SEEN_KARA,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_KARA, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_ADVENTURE, 1, 0)
			},
			{
				Option.HAS_SEEN_ICC,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_ICC, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_ADVENTURE, 1, 0)
			},
			{
				Option.HAS_SEEN_LOOT,
				new GameSaveDataManager.ServerOptionFlagMigrationData(GameSaveKeyId.ADVENTURE_DATA_CLIENT_LOOT, GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_ADVENTURE, 1, 0)
			}
		})
		{
			Option key = keyValuePair.Key;
			GameSaveKeyId keyId = keyValuePair.Value.KeyId;
			GameSaveKeySubkeyId subkeyId = keyValuePair.Value.SubkeyId;
			int flagTrueValue = keyValuePair.Value.FlagTrueValue;
			int flagFalseValue = keyValuePair.Value.FlagFalseValue;
			if (Options.Get().HasOption(key))
			{
				int num = Options.Get().GetBool(key) ? flagTrueValue : flagFalseValue;
				list.Add(new GameSaveDataManager.SubkeySaveRequest(keyId, subkeyId, new long[]
				{
					(long)num
				}));
				Options.Get().DeleteOption(key);
			}
		}
		if (list.Count > 0)
		{
			this.SaveSubkeys(list, null);
		}
	}

	// Token: 0x06007C20 RID: 31776 RVA: 0x00285704 File Offset: 0x00283904
	public void SetGameSaveDataUpdateFromInitialClientState(List<GameSaveDataUpdate> gameSaveDataUpdate)
	{
		this.ReadGameSaveDataUpdates(gameSaveDataUpdate);
	}

	// Token: 0x06007C21 RID: 31777 RVA: 0x00285710 File Offset: 0x00283910
	private void ReadGameSaveDataUpdates(List<GameSaveDataUpdate> gameSaveDataUpdate)
	{
		foreach (GameSaveDataUpdate gameSaveDataUpdate2 in gameSaveDataUpdate)
		{
			if (gameSaveDataUpdate2.Tuple.Count < 1)
			{
				Log.All.PrintWarning("GameSaveDataManager.OnRequestGameSaveDataResponse() - Received response that contains no key", Array.Empty<object>());
			}
			else
			{
				GameSaveKeyId gameSaveKeyId = (GameSaveKeyId)gameSaveDataUpdate2.Tuple[0].Id;
				Map<GameSaveKeySubkeyId, GameSaveDataValue> map = new Map<GameSaveKeySubkeyId, GameSaveDataValue>();
				this.m_gameSaveDataMapByKey[gameSaveKeyId] = map;
				if (!gameSaveDataUpdate2.HasValue)
				{
					Log.All.Print("GameSaveDataManager.OnRequestGameSaveDataResponse() - Received response contains no data for the requested key {0}", new object[]
					{
						gameSaveKeyId
					});
				}
				else
				{
					int num = 0;
					while (num < gameSaveDataUpdate2.Value.MapKeys.Count && num < gameSaveDataUpdate2.Value.MapValues.Count)
					{
						GameSaveKeySubkeyId key = (GameSaveKeySubkeyId)gameSaveDataUpdate2.Value.MapKeys[num];
						map[key] = gameSaveDataUpdate2.Value.MapValues[num];
						num++;
					}
				}
			}
		}
	}

	// Token: 0x06007C22 RID: 31778 RVA: 0x00285834 File Offset: 0x00283A34
	private bool ValidateThereAreNoPendingRequestsForKey(GameSaveKeyId key, string loggingContext)
	{
		if (this.IsRequestPending(key))
		{
			Log.All.PrintError("GameSaveDataManager.{0}() - Detected pending operation for key {1}", new object[]
			{
				loggingContext,
				key
			});
			return false;
		}
		return true;
	}

	// Token: 0x06007C23 RID: 31779 RVA: 0x00285864 File Offset: 0x00283A64
	public void Request(GameSaveKeyId key, GameSaveDataManager.OnRequestDataResponseDelegate callback = null)
	{
		this.Request(new List<GameSaveKeyId>
		{
			key
		}, callback);
	}

	// Token: 0x06007C24 RID: 31780 RVA: 0x0028587C File Offset: 0x00283A7C
	public void Request(List<GameSaveKeyId> keys, GameSaveDataManager.OnRequestDataResponseDelegate callback = null)
	{
		List<long> list = new List<long>();
		int num = ++GameSaveDataManager.s_clientToken;
		foreach (GameSaveKeyId gameSaveKeyId in keys)
		{
			if (this.ValidateThereAreNoPendingRequestsForKey(gameSaveKeyId, "Request"))
			{
				list.Add((long)gameSaveKeyId);
			}
		}
		if (list.Count > 0)
		{
			this.m_pendingRequestsByClientToken.Add(num, new GameSaveDataManager.PendingRequestContext(keys, callback));
			Network.Get().RequestGameSaveData(list, num);
			return;
		}
		if (callback != null)
		{
			callback(false);
		}
	}

	// Token: 0x06007C25 RID: 31781 RVA: 0x00285920 File Offset: 0x00283B20
	public bool SaveSubkey(GameSaveDataManager.SubkeySaveRequest request, GameSaveDataManager.OnSaveDataResponseDelegate callback = null)
	{
		return this.SaveSubkeys(new List<GameSaveDataManager.SubkeySaveRequest>
		{
			request
		}, callback);
	}

	// Token: 0x06007C26 RID: 31782 RVA: 0x00285944 File Offset: 0x00283B44
	public bool SaveSubkeys(List<GameSaveDataManager.SubkeySaveRequest> requests, GameSaveDataManager.OnSaveDataResponseDelegate callback = null)
	{
		if (requests == null || requests.Count == 0)
		{
			Log.All.PrintError("GameSaveDataManager.SaveSubkeys() - No save requests specified", Array.Empty<object>());
			return false;
		}
		HashSet<GameSaveDataManager.GameSaveKeyTuple> hashSet = new HashSet<GameSaveDataManager.GameSaveKeyTuple>();
		foreach (GameSaveDataManager.SubkeySaveRequest subkeySaveRequest in requests)
		{
			GameSaveDataManager.GameSaveKeyTuple item = new GameSaveDataManager.GameSaveKeyTuple(subkeySaveRequest.Key, subkeySaveRequest.Subkey);
			if (hashSet.Contains(item))
			{
				Log.All.PrintError("GameSaveDataManager.SaveSubkeys() - Found multiple save requests for key {0} subkey {1}", new object[]
				{
					subkeySaveRequest.Key,
					subkeySaveRequest.Subkey
				});
				return false;
			}
			hashSet.Add(item);
		}
		List<GameSaveDataUpdate> list = new List<GameSaveDataUpdate>();
		foreach (GameSaveDataManager.SubkeySaveRequest subkeySaveRequest2 in requests)
		{
			if (this.ValidateThereAreNoPendingRequestsForKey(subkeySaveRequest2.Key, "SaveSubkeys"))
			{
				GameSaveDataUpdate gameSaveDataUpdate = new GameSaveDataUpdate();
				GameSaveDataValue value = new GameSaveDataValue();
				gameSaveDataUpdate.Tuple.Add(new GameSaveKey
				{
					Id = (long)subkeySaveRequest2.Key
				});
				gameSaveDataUpdate.Tuple.Add(new GameSaveKey
				{
					Id = (long)subkeySaveRequest2.Subkey
				});
				this.SetGameSaveDataValueFromRequest(subkeySaveRequest2, ref value);
				gameSaveDataUpdate.Value = value;
				list.Add(gameSaveDataUpdate);
				this.SaveSubkeyToLocalCache(subkeySaveRequest2);
				this.m_batchedSubkeySaveRequests.Add(subkeySaveRequest2);
			}
		}
		if (callback != null && list.Count > 0)
		{
			this.m_batchedSaveUpdateCallbacks.Add(callback);
		}
		this.BatchGameSaveUpdates(list);
		return list.Count > 0;
	}

	// Token: 0x06007C27 RID: 31783 RVA: 0x00285B10 File Offset: 0x00283D10
	private void SetGameSaveDataValueFromRequest(GameSaveDataManager.SubkeySaveRequest request, ref GameSaveDataValue value)
	{
		if (request.Long_Values != null && request.String_Values != null)
		{
			Log.All.PrintError("Error writing game save data: Attempting to write Long and String into the same key!", Array.Empty<object>());
			return;
		}
		if (request.Long_Values != null)
		{
			value.IntValue = request.Long_Values.ToList<long>();
			return;
		}
		if (request.String_Values != null)
		{
			value.StringValue = request.String_Values.ToList<string>();
		}
	}

	// Token: 0x06007C28 RID: 31784 RVA: 0x00285B78 File Offset: 0x00283D78
	private void BatchGameSaveUpdates(List<GameSaveDataUpdate> saveUpdates)
	{
		if (this.m_batchedSaveUpdates.Count == 0)
		{
			if ((DateTime.Now - this.m_timeOfLastSetGameSaveDataRequest).TotalSeconds > 1.0)
			{
				Processor.RunCoroutine(this.SendAllBatchedGameSaveUpdatesNextFrame(), null);
			}
			else
			{
				Processor.ScheduleCallback(1f, false, new Processor.ScheduledCallback(this.SendAllBatchedGameSaveDataUpdates), null);
			}
		}
		using (List<GameSaveDataUpdate>.Enumerator enumerator = saveUpdates.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				GameSaveDataUpdate update = enumerator.Current;
				GameSaveDataUpdate gameSaveDataUpdate = this.m_batchedSaveUpdates.FirstOrDefault((GameSaveDataUpdate u) => u.Tuple[0].Id == update.Tuple[0].Id && u.Tuple[1].Id == update.Tuple[1].Id);
				if (gameSaveDataUpdate != null)
				{
					this.m_batchedSaveUpdates.Remove(gameSaveDataUpdate);
				}
				this.m_batchedSaveUpdates.Add(update);
			}
		}
	}

	// Token: 0x06007C29 RID: 31785 RVA: 0x00285C5C File Offset: 0x00283E5C
	public GameSaveDataManager.SubkeySaveRequest GenerateSaveRequestToAddValuesToSubkeyIfTheyDoNotExist(GameSaveKeyId key, GameSaveKeySubkeyId subkeyId, List<long> valuesToAdd)
	{
		if (valuesToAdd == null)
		{
			return null;
		}
		List<long> list;
		this.GetSubkeyValue(key, subkeyId, out list);
		if (list == null)
		{
			list = new List<long>();
		}
		bool flag = false;
		foreach (long item in valuesToAdd)
		{
			if (!list.Contains(item))
			{
				list.Add(item);
				flag = true;
			}
		}
		if (!flag)
		{
			return null;
		}
		return new GameSaveDataManager.SubkeySaveRequest(key, subkeyId, list.ToArray());
	}

	// Token: 0x06007C2A RID: 31786 RVA: 0x00285CE4 File Offset: 0x00283EE4
	public GameSaveDataManager.SubkeySaveRequest GenerateSaveRequestToRemoveValueFromSubkeyIfItExists(GameSaveKeyId key, GameSaveKeySubkeyId subkeyId, long valueToRemove)
	{
		List<long> list;
		if (!this.GetSubkeyValue(key, subkeyId, out list))
		{
			return null;
		}
		if (list == null)
		{
			return null;
		}
		if (!list.Remove(valueToRemove))
		{
			return null;
		}
		return new GameSaveDataManager.SubkeySaveRequest(key, subkeyId, list.ToArray());
	}

	// Token: 0x06007C2B RID: 31787 RVA: 0x00285D1C File Offset: 0x00283F1C
	private IEnumerator SendAllBatchedGameSaveUpdatesNextFrame()
	{
		yield return new WaitForEndOfFrame();
		this.SendAllBatchedGameSaveDataUpdates(null);
		yield break;
	}

	// Token: 0x06007C2C RID: 31788 RVA: 0x00285D2C File Offset: 0x00283F2C
	private void SendAllBatchedGameSaveDataUpdates(object userdata)
	{
		int num = ++GameSaveDataManager.s_clientToken;
		foreach (GameSaveDataManager.OnSaveDataResponseDelegate saveCallback in this.m_batchedSaveUpdateCallbacks)
		{
			this.m_pendingRequestsByClientToken.Add(num, new GameSaveDataManager.PendingRequestContext(this.m_batchedSubkeySaveRequests, saveCallback));
		}
		Network.Get().SetGameSaveData(this.m_batchedSaveUpdates, num);
		this.m_timeOfLastSetGameSaveDataRequest = DateTime.Now;
		this.m_batchedSaveUpdates.Clear();
		this.m_batchedSubkeySaveRequests.Clear();
		this.m_batchedSaveUpdateCallbacks.Clear();
	}

	// Token: 0x06007C2D RID: 31789 RVA: 0x00285DDC File Offset: 0x00283FDC
	private void SaveSubkeyToLocalCache(GameSaveDataManager.SubkeySaveRequest request)
	{
		Map<GameSaveKeySubkeyId, GameSaveDataValue> map;
		if (!this.m_gameSaveDataMapByKey.TryGetValue(request.Key, out map))
		{
			map = new Map<GameSaveKeySubkeyId, GameSaveDataValue>();
			this.m_gameSaveDataMapByKey.Add(request.Key, map);
		}
		GameSaveDataValue value;
		if (!map.TryGetValue(request.Subkey, out value))
		{
			value = new GameSaveDataValue();
			map.Add(request.Subkey, value);
		}
		this.SetGameSaveDataValueFromRequest(request, ref value);
	}

	// Token: 0x06007C2E RID: 31790 RVA: 0x00285E44 File Offset: 0x00284044
	public bool GetSubkeyValue(GameSaveKeyId key, GameSaveKeySubkeyId subkeyId, out long value)
	{
		value = 0L;
		List<long> list;
		if (this.GetSubkeyValue(key, subkeyId, out list))
		{
			value = list[0];
			return true;
		}
		return false;
	}

	// Token: 0x06007C2F RID: 31791 RVA: 0x00285E70 File Offset: 0x00284070
	public bool GetSubkeyValue(GameSaveKeyId key, GameSaveKeySubkeyId subkeyId, out string value)
	{
		value = "";
		List<string> list;
		if (this.GetSubkeyValue(key, subkeyId, out list))
		{
			value = list[0];
			return true;
		}
		return false;
	}

	// Token: 0x06007C30 RID: 31792 RVA: 0x00285E9C File Offset: 0x0028409C
	public bool GetSubkeyValue(GameSaveKeyId key, GameSaveKeySubkeyId subkeyId, out List<long> values)
	{
		values = null;
		GameSaveDataValue subkeyValue = this.GetSubkeyValue(key, subkeyId);
		if (subkeyValue != null && subkeyValue.IntValue.Count > 0)
		{
			values = new List<long>(subkeyValue.IntValue);
			return true;
		}
		return false;
	}

	// Token: 0x06007C31 RID: 31793 RVA: 0x00285ED8 File Offset: 0x002840D8
	public bool GetSubkeyValue(GameSaveKeyId key, GameSaveKeySubkeyId subkeyId, out List<string> values)
	{
		values = null;
		GameSaveDataValue subkeyValue = this.GetSubkeyValue(key, subkeyId);
		if (subkeyValue != null && subkeyValue.StringValue.Count > 0)
		{
			values = new List<string>(subkeyValue.StringValue);
			return true;
		}
		return false;
	}

	// Token: 0x06007C32 RID: 31794 RVA: 0x00285F14 File Offset: 0x00284114
	public bool GetSubkeyValue(GameSaveKeyId key, GameSaveKeySubkeyId subkeyId, out List<double> values)
	{
		values = null;
		GameSaveDataValue subkeyValue = this.GetSubkeyValue(key, subkeyId);
		if (subkeyValue != null && subkeyValue.FloatValue.Count > 0)
		{
			values = new List<double>(subkeyValue.FloatValue);
			return true;
		}
		return false;
	}

	// Token: 0x06007C33 RID: 31795 RVA: 0x00285F50 File Offset: 0x00284150
	public List<GameSaveKeySubkeyId> GetAllSubkeysForKey(GameSaveKeyId key)
	{
		List<GameSaveKeySubkeyId> result = new List<GameSaveKeySubkeyId>();
		Map<GameSaveKeySubkeyId, GameSaveDataValue> map = null;
		if (this.m_gameSaveDataMapByKey.TryGetValue(key, out map))
		{
			result = new List<GameSaveKeySubkeyId>(map.Keys);
		}
		return result;
	}

	// Token: 0x06007C34 RID: 31796 RVA: 0x00285F82 File Offset: 0x00284182
	public void ClearLocalData(GameSaveKeyId key)
	{
		if (!this.ValidateThereAreNoPendingRequestsForKey(key, "ClearLocalData"))
		{
			return;
		}
		this.m_gameSaveDataMapByKey.Remove(key);
	}

	// Token: 0x06007C35 RID: 31797 RVA: 0x00285FA0 File Offset: 0x002841A0
	public bool ValidateIfKeyCanBeAccessed(GameSaveKeyId key, string loggingContext)
	{
		if (!GameSaveDataManager.IsGameSaveKeyValid(key))
		{
			Log.All.PrintWarning("GameSaveDataManager.ValidateKeyCanBeAccessed() called with invalid key ID {0}!  Context: {1}\nStack Trace:\n{2}", new object[]
			{
				key,
				loggingContext,
				StackTraceUtility.ExtractStackTrace()
			});
			return false;
		}
		if (this.IsRequestPending(key))
		{
			Log.All.PrintWarning("GameSaveDataManager.ValidateKeyCanBeAccessed() - Request for key {0} is pending!  Context: {1}\nStack Trace:\n{2}", new object[]
			{
				key,
				loggingContext,
				StackTraceUtility.ExtractStackTrace()
			});
			return false;
		}
		if (!this.IsDataReady(key))
		{
			Log.All.Print("GameSaveDataManager.ValidateKeyCanBeAccessed() - Key {0} has no data - it has either not been created yet, or has not been requested.  Context: {1}\nStack Trace:\n{2}", new object[]
			{
				key,
				loggingContext,
				StackTraceUtility.ExtractStackTrace()
			});
			return false;
		}
		return true;
	}

	// Token: 0x06007C36 RID: 31798 RVA: 0x0028604C File Offset: 0x0028424C
	public bool IsRequestPending(GameSaveKeyId key)
	{
		using (Dictionary<int, GameSaveDataManager.PendingRequestContext>.ValueCollection.Enumerator enumerator = this.m_pendingRequestsByClientToken.Values.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.AffectedKeys.IndexOf(key) >= 0)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06007C37 RID: 31799 RVA: 0x002860B4 File Offset: 0x002842B4
	public static GameSaveDataManager Get()
	{
		if (GameSaveDataManager.s_instance == null)
		{
			GameSaveDataManager.s_instance = new GameSaveDataManager();
		}
		return GameSaveDataManager.s_instance;
	}

	// Token: 0x06007C38 RID: 31800 RVA: 0x002860CC File Offset: 0x002842CC
	public static bool GetProgressSubkeysForDungeonCrawlWing(WingDbfRecord wingRecord, out GameSaveDataManager.AdventureDungeonCrawlWingProgressSubkeys progressSubkeys)
	{
		if (wingRecord == null)
		{
			Log.Adventures.PrintWarning("GetProgressSubkeysForDungeonCrawlWing: wingRecord is null!", Array.Empty<object>());
			progressSubkeys = default(GameSaveDataManager.AdventureDungeonCrawlWingProgressSubkeys);
			return false;
		}
		int sortedWingUnlockIndex = GameUtils.GetSortedWingUnlockIndex(wingRecord);
		if (sortedWingUnlockIndex < 0 || sortedWingUnlockIndex >= GameSaveDataManager.ProgressSubkeysForDungeonCrawlWings.Count)
		{
			Log.Adventures.PrintWarning("GetProgressSubkeysForDungeonCrawlWing: could not find a valid Sorted Wing Unlock Index for WingDbfRecord {0} - WingIndex: {1}!", new object[]
			{
				wingRecord.ID,
				sortedWingUnlockIndex
			});
			progressSubkeys = default(GameSaveDataManager.AdventureDungeonCrawlWingProgressSubkeys);
			return false;
		}
		progressSubkeys = GameSaveDataManager.ProgressSubkeysForDungeonCrawlWings[sortedWingUnlockIndex];
		return true;
	}

	// Token: 0x06007C39 RID: 31801 RVA: 0x00286158 File Offset: 0x00284358
	public static bool GetProgressSubkeyForDungeonCrawlClass(TAG_CLASS tagClass, out GameSaveDataManager.AdventureDungeonCrawlClassProgressSubkeys progressSubkeys)
	{
		if (GameSaveDataManager.AdventureDungeonCrawlClassToSubkeyMapping.ContainsKey(tagClass))
		{
			progressSubkeys = GameSaveDataManager.AdventureDungeonCrawlClassToSubkeyMapping[tagClass];
			return true;
		}
		progressSubkeys = default(GameSaveDataManager.AdventureDungeonCrawlClassProgressSubkeys);
		return false;
	}

	// Token: 0x06007C3A RID: 31802 RVA: 0x00286182 File Offset: 0x00284382
	public static List<TAG_CLASS> GetClassesFromDungeonCrawlProgressMap()
	{
		return GameSaveDataManager.AdventureDungeonCrawlClassToSubkeyMapping.Keys.ToList<TAG_CLASS>();
	}

	// Token: 0x06007C3B RID: 31803 RVA: 0x00286194 File Offset: 0x00284394
	public void Cheat_SaveSubkeyToLocalCache(GameSaveKeyId key, GameSaveKeySubkeyId subkey, params long[] values)
	{
		if (HearthstoneApplication.IsInternal())
		{
			GameSaveDataManager.SubkeySaveRequest request = new GameSaveDataManager.SubkeySaveRequest(key, subkey, values);
			this.SaveSubkeyToLocalCache(request);
		}
	}

	// Token: 0x06007C3C RID: 31804 RVA: 0x002861B8 File Offset: 0x002843B8
	public bool MigrateSubkeyIntValue(GameSaveKeyId sourceKey, GameSaveKeyId destinationKey, GameSaveKeySubkeyId subkeyId, long emptyValueForSource = 0L)
	{
		GameSaveDataValue subkeyValue = this.GetSubkeyValue(sourceKey, subkeyId);
		if (subkeyValue == null || subkeyValue.IntValue == null || subkeyValue.IntValue.Count < 1 || subkeyValue.IntValue[0] == emptyValueForSource)
		{
			return false;
		}
		long num = subkeyValue.IntValue[0];
		return this.SaveSubkeys(new List<GameSaveDataManager.SubkeySaveRequest>
		{
			new GameSaveDataManager.SubkeySaveRequest(destinationKey, subkeyId, new long[]
			{
				num
			}),
			new GameSaveDataManager.SubkeySaveRequest(sourceKey, subkeyId, new long[]
			{
				emptyValueForSource
			})
		}, null);
	}

	// Token: 0x06007C3D RID: 31805 RVA: 0x00286244 File Offset: 0x00284444
	private GameSaveDataValue GetSubkeyValue(GameSaveKeyId key, GameSaveKeySubkeyId subkeyId)
	{
		if (!this.IsDataReady(key))
		{
			Debug.LogErrorFormat("Attempting to get subkey {0} from key {1} failed, key not received by client yet", new object[]
			{
				subkeyId,
				key
			});
			return null;
		}
		Map<GameSaveKeySubkeyId, GameSaveDataValue> map;
		GameSaveDataValue result;
		if (this.m_gameSaveDataMapByKey.TryGetValue(key, out map) && map != null && map.TryGetValue(subkeyId, out result))
		{
			return result;
		}
		return null;
	}

	// Token: 0x06007C3E RID: 31806 RVA: 0x0028629F File Offset: 0x0028449F
	private static void OnWillReset()
	{
		HearthstoneApplication.Get().WillReset -= GameSaveDataManager.OnWillReset;
		GameSaveDataManager.s_instance = new GameSaveDataManager();
	}

	// Token: 0x06007C3F RID: 31807 RVA: 0x002862C1 File Offset: 0x002844C1
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		this.m_pendingRequestsByClientToken.Clear();
	}

	// Token: 0x04006534 RID: 25908
	private static int s_clientToken = 0;

	// Token: 0x04006535 RID: 25909
	private static readonly Map<TAG_CLASS, GameSaveDataManager.AdventureDungeonCrawlClassProgressSubkeys> AdventureDungeonCrawlClassToSubkeyMapping = new Map<TAG_CLASS, GameSaveDataManager.AdventureDungeonCrawlClassProgressSubkeys>
	{
		{
			TAG_CLASS.PALADIN,
			new GameSaveDataManager.AdventureDungeonCrawlClassProgressSubkeys
			{
				bossWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_PALADIN_BOSS_WINS,
				runWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_PALADIN_RUN_WINS
			}
		},
		{
			TAG_CLASS.HUNTER,
			new GameSaveDataManager.AdventureDungeonCrawlClassProgressSubkeys
			{
				bossWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HUNTER_BOSS_WINS,
				runWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HUNTER_RUN_WINS
			}
		},
		{
			TAG_CLASS.MAGE,
			new GameSaveDataManager.AdventureDungeonCrawlClassProgressSubkeys
			{
				bossWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_MAGE_BOSS_WINS,
				runWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_MAGE_RUN_WINS
			}
		},
		{
			TAG_CLASS.SHAMAN,
			new GameSaveDataManager.AdventureDungeonCrawlClassProgressSubkeys
			{
				bossWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_SHAMAN_BOSS_WINS,
				runWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_SHAMAN_RUN_WINS
			}
		},
		{
			TAG_CLASS.WARRIOR,
			new GameSaveDataManager.AdventureDungeonCrawlClassProgressSubkeys
			{
				bossWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_WARRIOR_BOSS_WINS,
				runWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_WARRIOR_RUN_WINS
			}
		},
		{
			TAG_CLASS.ROGUE,
			new GameSaveDataManager.AdventureDungeonCrawlClassProgressSubkeys
			{
				bossWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_ROGUE_BOSS_WINS,
				runWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_ROGUE_RUN_WINS
			}
		},
		{
			TAG_CLASS.WARLOCK,
			new GameSaveDataManager.AdventureDungeonCrawlClassProgressSubkeys
			{
				bossWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_WARLOCK_BOSS_WINS,
				runWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_WARLOCK_RUN_WINS
			}
		},
		{
			TAG_CLASS.PRIEST,
			new GameSaveDataManager.AdventureDungeonCrawlClassProgressSubkeys
			{
				bossWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_PRIEST_BOSS_WINS,
				runWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_PRIEST_RUN_WINS
			}
		},
		{
			TAG_CLASS.DRUID,
			new GameSaveDataManager.AdventureDungeonCrawlClassProgressSubkeys
			{
				bossWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_DRUID_BOSS_WINS,
				runWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_DRUID_RUN_WINS
			}
		},
		{
			TAG_CLASS.DEMONHUNTER,
			new GameSaveDataManager.AdventureDungeonCrawlClassProgressSubkeys
			{
				bossWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_DEMON_HUNTER_BOSS_WINS,
				runWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_DEMON_HUNTER_RUN_WINS
			}
		}
	};

	// Token: 0x04006536 RID: 25910
	private static readonly List<GameSaveDataManager.AdventureDungeonCrawlWingProgressSubkeys> ProgressSubkeysForDungeonCrawlWings = new List<GameSaveDataManager.AdventureDungeonCrawlWingProgressSubkeys>
	{
		new GameSaveDataManager.AdventureDungeonCrawlWingProgressSubkeys
		{
			heroCardWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_CARD_WING_1_WINS,
			deckWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_WING_1_WINS,
			heroPowerWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_POWER_WING_1_WINS,
			treasureWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_TREASURE_WING_1_WINS
		},
		new GameSaveDataManager.AdventureDungeonCrawlWingProgressSubkeys
		{
			heroCardWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_CARD_WING_2_WINS,
			deckWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_WING_2_WINS,
			heroPowerWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_POWER_WING_2_WINS,
			treasureWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_TREASURE_WING_2_WINS
		},
		new GameSaveDataManager.AdventureDungeonCrawlWingProgressSubkeys
		{
			heroCardWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_CARD_WING_3_WINS,
			deckWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_WING_3_WINS,
			heroPowerWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_POWER_WING_3_WINS,
			treasureWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_TREASURE_WING_3_WINS
		},
		new GameSaveDataManager.AdventureDungeonCrawlWingProgressSubkeys
		{
			heroCardWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_CARD_WING_4_WINS,
			deckWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_WING_4_WINS,
			heroPowerWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_POWER_WING_4_WINS,
			treasureWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_TREASURE_WING_4_WINS
		},
		new GameSaveDataManager.AdventureDungeonCrawlWingProgressSubkeys
		{
			heroCardWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_CARD_WING_5_WINS,
			deckWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_WING_5_WINS,
			heroPowerWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_POWER_WING_5_WINS,
			treasureWins = GameSaveKeySubkeyId.DUNGEON_CRAWL_TREASURE_WING_5_WINS
		}
	};

	// Token: 0x04006537 RID: 25911
	private const float BATCHED_SAVE_SUBKEY_REQUEST_RATE = 1f;

	// Token: 0x04006538 RID: 25912
	private static GameSaveDataManager s_instance = null;

	// Token: 0x04006539 RID: 25913
	private Map<GameSaveKeyId, Map<GameSaveKeySubkeyId, GameSaveDataValue>> m_gameSaveDataMapByKey = new Map<GameSaveKeyId, Map<GameSaveKeySubkeyId, GameSaveDataValue>>();

	// Token: 0x0400653A RID: 25914
	private Dictionary<GameSaveKeyId, bool> m_isRequestPendingForKey;

	// Token: 0x0400653B RID: 25915
	private Dictionary<int, GameSaveDataManager.PendingRequestContext> m_pendingRequestsByClientToken = new Dictionary<int, GameSaveDataManager.PendingRequestContext>();

	// Token: 0x0400653C RID: 25916
	private List<GameSaveDataUpdate> m_batchedSaveUpdates = new List<GameSaveDataUpdate>();

	// Token: 0x0400653D RID: 25917
	private List<GameSaveDataManager.SubkeySaveRequest> m_batchedSubkeySaveRequests = new List<GameSaveDataManager.SubkeySaveRequest>();

	// Token: 0x0400653E RID: 25918
	private List<GameSaveDataManager.OnSaveDataResponseDelegate> m_batchedSaveUpdateCallbacks = new List<GameSaveDataManager.OnSaveDataResponseDelegate>();

	// Token: 0x0400653F RID: 25919
	private DateTime m_timeOfLastSetGameSaveDataRequest;

	// Token: 0x02002540 RID: 9536
	public struct AdventureDungeonCrawlClassProgressSubkeys
	{
		// Token: 0x0400ED05 RID: 60677
		public GameSaveKeySubkeyId bossWins;

		// Token: 0x0400ED06 RID: 60678
		public GameSaveKeySubkeyId runWins;
	}

	// Token: 0x02002541 RID: 9537
	public struct AdventureDungeonCrawlWingProgressSubkeys
	{
		// Token: 0x0400ED07 RID: 60679
		public GameSaveKeySubkeyId heroCardWins;

		// Token: 0x0400ED08 RID: 60680
		public GameSaveKeySubkeyId heroPowerWins;

		// Token: 0x0400ED09 RID: 60681
		public GameSaveKeySubkeyId deckWins;

		// Token: 0x0400ED0A RID: 60682
		public GameSaveKeySubkeyId treasureWins;
	}

	// Token: 0x02002542 RID: 9538
	public struct GameSaveKeyTuple
	{
		// Token: 0x06013273 RID: 78451 RVA: 0x0052B004 File Offset: 0x00529204
		public GameSaveKeyTuple(GameSaveKeyId key, GameSaveKeySubkeyId subkey)
		{
			this.Key = key;
			this.Subkey = subkey;
		}

		// Token: 0x06013274 RID: 78452 RVA: 0x0052B014 File Offset: 0x00529214
		public override bool Equals(object obj)
		{
			return obj is GameSaveDataManager.GameSaveKeyTuple && this.Equals((GameSaveDataManager.GameSaveKeyTuple)obj);
		}

		// Token: 0x06013275 RID: 78453 RVA: 0x0052B02C File Offset: 0x0052922C
		public bool Equals(GameSaveDataManager.GameSaveKeyTuple p)
		{
			return this.Key == p.Key && this.Subkey == p.Subkey;
		}

		// Token: 0x06013276 RID: 78454 RVA: 0x0052B04C File Offset: 0x0052924C
		public override int GetHashCode()
		{
			return (int)(this.Key ^ (GameSaveKeyId)this.Subkey);
		}

		// Token: 0x0400ED0B RID: 60683
		public GameSaveKeyId Key;

		// Token: 0x0400ED0C RID: 60684
		public GameSaveKeySubkeyId Subkey;
	}

	// Token: 0x02002543 RID: 9539
	public class SubkeySaveRequest
	{
		// Token: 0x06013277 RID: 78455 RVA: 0x0052B05B File Offset: 0x0052925B
		public SubkeySaveRequest(GameSaveKeyId key, GameSaveKeySubkeyId subkey, params long[] values)
		{
			this.Key = key;
			this.Subkey = subkey;
			this.Long_Values = values;
		}

		// Token: 0x06013278 RID: 78456 RVA: 0x0052B078 File Offset: 0x00529278
		public SubkeySaveRequest(GameSaveKeyId key, GameSaveKeySubkeyId subkey, params string[] values)
		{
			this.Key = key;
			this.Subkey = subkey;
			this.String_Values = values;
		}

		// Token: 0x0400ED0D RID: 60685
		public readonly GameSaveKeyId Key;

		// Token: 0x0400ED0E RID: 60686
		public readonly GameSaveKeySubkeyId Subkey;

		// Token: 0x0400ED0F RID: 60687
		public readonly long[] Long_Values;

		// Token: 0x0400ED10 RID: 60688
		public readonly string[] String_Values;
	}

	// Token: 0x02002544 RID: 9540
	private class PendingRequestContext
	{
		// Token: 0x06013279 RID: 78457 RVA: 0x0052B095 File Offset: 0x00529295
		public PendingRequestContext(List<GameSaveKeyId> requestedKeys, GameSaveDataManager.OnRequestDataResponseDelegate requestCallback)
		{
			this.AffectedKeys.AddRange(requestedKeys);
			this.RequestCallback = requestCallback;
		}

		// Token: 0x0601327A RID: 78458 RVA: 0x0052B0BC File Offset: 0x005292BC
		public PendingRequestContext(List<GameSaveDataManager.SubkeySaveRequest> requests, GameSaveDataManager.OnSaveDataResponseDelegate saveCallback)
		{
			foreach (GameSaveDataManager.SubkeySaveRequest subkeySaveRequest in requests)
			{
				this.AffectedKeys.Add(subkeySaveRequest.Key);
			}
			this.SaveCallback = saveCallback;
		}

		// Token: 0x0400ED11 RID: 60689
		public readonly List<GameSaveKeyId> AffectedKeys = new List<GameSaveKeyId>();

		// Token: 0x0400ED12 RID: 60690
		public readonly GameSaveDataManager.OnRequestDataResponseDelegate RequestCallback;

		// Token: 0x0400ED13 RID: 60691
		public readonly GameSaveDataManager.OnSaveDataResponseDelegate SaveCallback;
	}

	// Token: 0x02002545 RID: 9541
	private class ServerOptionFlagMigrationData
	{
		// Token: 0x0601327B RID: 78459 RVA: 0x0052B12C File Offset: 0x0052932C
		public ServerOptionFlagMigrationData(GameSaveKeyId keyId, GameSaveKeySubkeyId subkeyId, int flagTrueValue = 1, int flagFalseValue = 0)
		{
			this.FlagTrueValue = flagTrueValue;
			this.FlagFalseValue = flagFalseValue;
			this.KeyId = keyId;
			this.SubkeyId = subkeyId;
		}

		// Token: 0x0400ED14 RID: 60692
		public readonly GameSaveKeyId KeyId;

		// Token: 0x0400ED15 RID: 60693
		public readonly GameSaveKeySubkeyId SubkeyId;

		// Token: 0x0400ED16 RID: 60694
		public readonly int FlagTrueValue;

		// Token: 0x0400ED17 RID: 60695
		public readonly int FlagFalseValue;
	}

	// Token: 0x02002546 RID: 9542
	// (Invoke) Token: 0x0601327D RID: 78461
	public delegate void OnRequestDataResponseDelegate(bool success);

	// Token: 0x02002547 RID: 9543
	// (Invoke) Token: 0x06013281 RID: 78465
	public delegate void OnSaveDataResponseDelegate(bool success);
}
