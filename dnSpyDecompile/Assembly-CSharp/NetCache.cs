using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bgs;
using bgs.types;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Blizzard.Telemetry.WTCG.Client;
using BobNetProto;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.Streaming;
using PegasusFSG;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000606 RID: 1542
public class NetCache : IService, IHasUpdate
{
	// Token: 0x060055ED RID: 21997 RVA: 0x001C1570 File Offset: 0x001BF770
	private static global::Map<GetAccountInfo.Request, Type> GetInvertTypeMap()
	{
		global::Map<GetAccountInfo.Request, Type> map = new global::Map<GetAccountInfo.Request, Type>();
		foreach (KeyValuePair<Type, GetAccountInfo.Request> keyValuePair in NetCache.m_getAccountInfoTypeMap)
		{
			map[keyValuePair.Value] = keyValuePair.Key;
		}
		return map;
	}

	// Token: 0x14000032 RID: 50
	// (add) Token: 0x060055EE RID: 21998 RVA: 0x001C15D8 File Offset: 0x001BF7D8
	// (remove) Token: 0x060055EF RID: 21999 RVA: 0x001C1610 File Offset: 0x001BF810
	public event NetCache.DelFavoriteCardBackChangedListener FavoriteCardBackChanged;

	// Token: 0x14000033 RID: 51
	// (add) Token: 0x060055F0 RID: 22000 RVA: 0x001C1648 File Offset: 0x001BF848
	// (remove) Token: 0x060055F1 RID: 22001 RVA: 0x001C1680 File Offset: 0x001BF880
	public event NetCache.DelFavoriteCoinChangedListener FavoriteCoinChanged;

	// Token: 0x1700051B RID: 1307
	// (get) Token: 0x060055F2 RID: 22002 RVA: 0x001C16B5 File Offset: 0x001BF8B5
	public bool HasReceivedInitialClientState
	{
		get
		{
			return this.m_receivedInitialClientState;
		}
	}

	// Token: 0x060055F3 RID: 22003 RVA: 0x001C16BD File Offset: 0x001BF8BD
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		serviceLocator.Get<Network>().RegisterThrottledPacketListener(new Network.ThrottledPacketListener(this.OnPacketThrottled));
		this.RegisterNetCacheHandlers();
		yield break;
	}

	// Token: 0x060055F4 RID: 22004 RVA: 0x001B7846 File Offset: 0x001B5A46
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(Network)
		};
	}

	// Token: 0x060055F5 RID: 22005 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x060055F6 RID: 22006 RVA: 0x001C16D3 File Offset: 0x001BF8D3
	public static NetCache Get()
	{
		return HearthstoneServices.Get<NetCache>();
	}

	// Token: 0x060055F7 RID: 22007 RVA: 0x001C16DC File Offset: 0x001BF8DC
	public T GetNetObject<T>()
	{
		Type typeFromHandle = typeof(T);
		object testData = this.GetTestData(typeFromHandle);
		if (testData != null)
		{
			return (T)((object)testData);
		}
		if (this.m_netCache.TryGetValue(typeof(T), out testData) && testData is T)
		{
			return (T)((object)testData);
		}
		return default(T);
	}

	// Token: 0x060055F8 RID: 22008 RVA: 0x001C1737 File Offset: 0x001BF937
	public bool IsNetObjectAvailable<T>()
	{
		return this.GetNetObject<T>() != null;
	}

	// Token: 0x060055F9 RID: 22009 RVA: 0x001C1748 File Offset: 0x001BF948
	private object GetTestData(Type type)
	{
		if (type == typeof(NetCache.NetCacheBoosters) && GameUtils.IsFakePackOpeningEnabled())
		{
			NetCache.NetCacheBoosters netCacheBoosters = new NetCache.NetCacheBoosters();
			int fakePackCount = GameUtils.GetFakePackCount();
			NetCache.BoosterStack item = new NetCache.BoosterStack
			{
				Id = 1,
				Count = fakePackCount
			};
			netCacheBoosters.BoosterStacks.Add(item);
			return netCacheBoosters;
		}
		return null;
	}

	// Token: 0x060055FA RID: 22010 RVA: 0x001C179C File Offset: 0x001BF99C
	public void UnloadNetObject<T>()
	{
		Type typeFromHandle = typeof(T);
		this.m_netCache[typeFromHandle] = null;
	}

	// Token: 0x060055FB RID: 22011 RVA: 0x001C17C1 File Offset: 0x001BF9C1
	public void ReloadNetObject<T>()
	{
		this.NetCacheReload_Internal(null, typeof(T));
	}

	// Token: 0x060055FC RID: 22012 RVA: 0x001C17D4 File Offset: 0x001BF9D4
	public void RefreshNetObject<T>()
	{
		this.RequestNetCacheObject(typeof(T));
	}

	// Token: 0x060055FD RID: 22013 RVA: 0x001C17E8 File Offset: 0x001BF9E8
	public long GetArcaneDustBalance()
	{
		NetCache.NetCacheArcaneDustBalance netObject = this.GetNetObject<NetCache.NetCacheArcaneDustBalance>();
		if (netObject == null)
		{
			return 0L;
		}
		if (CraftingManager.IsInitialized)
		{
			return netObject.Balance + CraftingManager.Get().GetUnCommitedArcaneDustChanges();
		}
		return netObject.Balance;
	}

	// Token: 0x060055FE RID: 22014 RVA: 0x001C1824 File Offset: 0x001BFA24
	public long GetGoldBalance()
	{
		NetCache.NetCacheGoldBalance netObject = this.GetNetObject<NetCache.NetCacheGoldBalance>();
		if (netObject == null)
		{
			return 0L;
		}
		return netObject.GetTotal();
	}

	// Token: 0x060055FF RID: 22015 RVA: 0x001C1844 File Offset: 0x001BFA44
	public int GetArenaTicketBalance()
	{
		NetCache.NetPlayerArenaTickets netObject = this.GetNetObject<NetCache.NetPlayerArenaTickets>();
		if (netObject == null)
		{
			return 0;
		}
		return netObject.Balance;
	}

	// Token: 0x06005600 RID: 22016 RVA: 0x001C1864 File Offset: 0x001BFA64
	private bool GetOption<T>(ServerOption type, out T ret) where T : NetCache.ClientOptionBase
	{
		ret = default(T);
		NetCache.NetCacheClientOptions netObject = NetCache.Get().GetNetObject<NetCache.NetCacheClientOptions>();
		if (!this.ClientOptionExists(type))
		{
			return false;
		}
		T t = netObject.ClientState[type] as T;
		if (t == null)
		{
			return false;
		}
		ret = t;
		return true;
	}

	// Token: 0x06005601 RID: 22017 RVA: 0x001C18B8 File Offset: 0x001BFAB8
	public int GetIntOption(ServerOption type)
	{
		NetCache.ClientOptionInt clientOptionInt = null;
		if (!this.GetOption<NetCache.ClientOptionInt>(type, out clientOptionInt))
		{
			return 0;
		}
		return clientOptionInt.OptionValue;
	}

	// Token: 0x06005602 RID: 22018 RVA: 0x001C18DC File Offset: 0x001BFADC
	public bool GetIntOption(ServerOption type, out int ret)
	{
		ret = 0;
		NetCache.ClientOptionInt clientOptionInt = null;
		if (!this.GetOption<NetCache.ClientOptionInt>(type, out clientOptionInt))
		{
			return false;
		}
		ret = clientOptionInt.OptionValue;
		return true;
	}

	// Token: 0x06005603 RID: 22019 RVA: 0x001C1904 File Offset: 0x001BFB04
	public long GetLongOption(ServerOption type)
	{
		NetCache.ClientOptionLong clientOptionLong = null;
		if (!this.GetOption<NetCache.ClientOptionLong>(type, out clientOptionLong))
		{
			return 0L;
		}
		return clientOptionLong.OptionValue;
	}

	// Token: 0x06005604 RID: 22020 RVA: 0x001C1928 File Offset: 0x001BFB28
	public bool GetLongOption(ServerOption type, out long ret)
	{
		ret = 0L;
		NetCache.ClientOptionLong clientOptionLong = null;
		if (!this.GetOption<NetCache.ClientOptionLong>(type, out clientOptionLong))
		{
			return false;
		}
		ret = clientOptionLong.OptionValue;
		return true;
	}

	// Token: 0x06005605 RID: 22021 RVA: 0x001C1954 File Offset: 0x001BFB54
	public float GetFloatOption(ServerOption type)
	{
		NetCache.ClientOptionFloat clientOptionFloat = null;
		if (!this.GetOption<NetCache.ClientOptionFloat>(type, out clientOptionFloat))
		{
			return 0f;
		}
		return clientOptionFloat.OptionValue;
	}

	// Token: 0x06005606 RID: 22022 RVA: 0x001C197C File Offset: 0x001BFB7C
	public bool GetFloatOption(ServerOption type, out float ret)
	{
		ret = 0f;
		NetCache.ClientOptionFloat clientOptionFloat = null;
		if (!this.GetOption<NetCache.ClientOptionFloat>(type, out clientOptionFloat))
		{
			return false;
		}
		ret = clientOptionFloat.OptionValue;
		return true;
	}

	// Token: 0x06005607 RID: 22023 RVA: 0x001C19A8 File Offset: 0x001BFBA8
	public ulong GetULongOption(ServerOption type)
	{
		NetCache.ClientOptionULong clientOptionULong = null;
		if (!this.GetOption<NetCache.ClientOptionULong>(type, out clientOptionULong))
		{
			return 0UL;
		}
		return clientOptionULong.OptionValue;
	}

	// Token: 0x06005608 RID: 22024 RVA: 0x001C19CC File Offset: 0x001BFBCC
	public bool GetULongOption(ServerOption type, out ulong ret)
	{
		ret = 0UL;
		NetCache.ClientOptionULong clientOptionULong = null;
		if (!this.GetOption<NetCache.ClientOptionULong>(type, out clientOptionULong))
		{
			return false;
		}
		ret = clientOptionULong.OptionValue;
		return true;
	}

	// Token: 0x06005609 RID: 22025 RVA: 0x001C19F8 File Offset: 0x001BFBF8
	public void RegisterUpdatedListener(Type type, Action listener)
	{
		if (listener == null)
		{
			return;
		}
		HashSet<Action> value;
		if (!this.m_updatedListeners.TryGetValue(type, out value))
		{
			value = new HashSet<Action>();
			this.m_updatedListeners[type] = value;
		}
		this.m_updatedListeners[type].Add(listener);
	}

	// Token: 0x0600560A RID: 22026 RVA: 0x001C1A40 File Offset: 0x001BFC40
	public void RemoveUpdatedListener(Type type, Action listener)
	{
		if (listener == null)
		{
			return;
		}
		HashSet<Action> hashSet;
		if (this.m_updatedListeners.TryGetValue(type, out hashSet))
		{
			hashSet.Remove(listener);
		}
	}

	// Token: 0x0600560B RID: 22027 RVA: 0x001C1A69 File Offset: 0x001BFC69
	public void RegisterNewNoticesListener(NetCache.DelNewNoticesListener listener)
	{
		if (this.m_newNoticesListeners.Contains(listener))
		{
			return;
		}
		this.m_newNoticesListeners.Add(listener);
	}

	// Token: 0x0600560C RID: 22028 RVA: 0x001C1A86 File Offset: 0x001BFC86
	public void RemoveNewNoticesListener(NetCache.DelNewNoticesListener listener)
	{
		this.m_newNoticesListeners.Remove(listener);
	}

	// Token: 0x0600560D RID: 22029 RVA: 0x001C1A98 File Offset: 0x001BFC98
	public bool RemoveNotice(long ID)
	{
		NetCache.NetCacheProfileNotices netCacheProfileNotices = this.m_netCache[typeof(NetCache.NetCacheProfileNotices)] as NetCache.NetCacheProfileNotices;
		if (netCacheProfileNotices == null)
		{
			Debug.LogWarning(string.Format("NetCache.RemoveNotice({0}) - profileNotices is null", ID));
			return false;
		}
		if (netCacheProfileNotices.Notices == null)
		{
			Debug.LogWarning(string.Format("NetCache.RemoveNotice({0}) - profileNotices.Notices is null", ID));
			return false;
		}
		NetCache.ProfileNotice profileNotice = netCacheProfileNotices.Notices.Find((NetCache.ProfileNotice obj) => obj.NoticeID == ID);
		if (profileNotice == null)
		{
			return false;
		}
		netCacheProfileNotices.Notices.Remove(profileNotice);
		this.m_ackedNotices.Add(profileNotice.NoticeID);
		return true;
	}

	// Token: 0x0600560E RID: 22030 RVA: 0x001C1B50 File Offset: 0x001BFD50
	public void NetCacheChanged<T>()
	{
		Type typeFromHandle = typeof(T);
		int num = 0;
		this.m_changeRequests.TryGetValue(typeFromHandle, out num);
		num++;
		this.m_changeRequests[typeFromHandle] = num;
		if (num > 1)
		{
			return;
		}
		while (this.m_changeRequests[typeFromHandle] > 0)
		{
			this.NetCacheChangedImpl<T>();
			this.m_changeRequests[typeFromHandle] = this.m_changeRequests[typeFromHandle] - 1;
		}
	}

	// Token: 0x0600560F RID: 22031 RVA: 0x001C1BC0 File Offset: 0x001BFDC0
	private void NetCacheChangedImpl<T>()
	{
		foreach (NetCache.NetCacheBatchRequest netCacheBatchRequest in this.m_cacheRequests.ToArray())
		{
			foreach (KeyValuePair<Type, NetCache.Request> keyValuePair in netCacheBatchRequest.m_requests)
			{
				if (!(keyValuePair.Key != typeof(T)))
				{
					this.NetCacheCheckRequest(netCacheBatchRequest);
					break;
				}
			}
		}
	}

	// Token: 0x06005610 RID: 22032 RVA: 0x001C1C4C File Offset: 0x001BFE4C
	public void CheckSeasonForRoll()
	{
		if (this.GetNetObject<NetCache.NetCacheProfileNotices>() == null)
		{
			return;
		}
		NetCache.NetCacheRewardProgress netObject = this.GetNetObject<NetCache.NetCacheRewardProgress>();
		if (netObject == null)
		{
			return;
		}
		DateTime utcNow = DateTime.UtcNow;
		DateTime dateTime = DateTime.FromFileTimeUtc(netObject.SeasonEndDate);
		if (dateTime >= utcNow)
		{
			return;
		}
		if (this.m_lastForceCheckedSeason == (long)netObject.Season)
		{
			return;
		}
		this.m_lastForceCheckedSeason = (long)netObject.Season;
		global::Log.Net.Print("NetCache.CheckSeasonForRoll oldSeason = {0} season end = {1} utc now = {2}", new object[]
		{
			this.m_lastForceCheckedSeason,
			dateTime,
			utcNow
		});
	}

	// Token: 0x06005611 RID: 22033 RVA: 0x001C1CDB File Offset: 0x001BFEDB
	public void RegisterGoldBalanceListener(NetCache.DelGoldBalanceListener listener)
	{
		if (this.m_goldBalanceListeners.Contains(listener))
		{
			return;
		}
		this.m_goldBalanceListeners.Add(listener);
	}

	// Token: 0x06005612 RID: 22034 RVA: 0x001C1CF8 File Offset: 0x001BFEF8
	public void RemoveGoldBalanceListener(NetCache.DelGoldBalanceListener listener)
	{
		this.m_goldBalanceListeners.Remove(listener);
	}

	// Token: 0x06005613 RID: 22035 RVA: 0x001C1D08 File Offset: 0x001BFF08
	public static void DefaultErrorHandler(NetCache.ErrorInfo info)
	{
		if (info.Error != NetCache.ErrorCode.TIMEOUT)
		{
			NetCache.ShowError(info, "GLOBAL_ERROR_NETWORK_GENERIC", Array.Empty<object>());
			return;
		}
		if (BreakingNews.SHOWS_BREAKING_NEWS)
		{
			string error = "GLOBAL_ERROR_NETWORK_UTIL_TIMEOUT";
			Network.Get().ShowBreakingNewsOrError(error, 0f);
			return;
		}
		NetCache.ShowError(info, "GLOBAL_ERROR_NETWORK_UTIL_TIMEOUT", Array.Empty<object>());
	}

	// Token: 0x06005614 RID: 22036 RVA: 0x001C1D5D File Offset: 0x001BFF5D
	public static void ShowError(NetCache.ErrorInfo info, string localizationKey, params object[] localizationArgs)
	{
		global::Error.AddFatal(FatalErrorReason.NET_CACHE, localizationKey, localizationArgs);
		Debug.LogError(NetCache.GetInternalErrorMessage(info, true));
	}

	// Token: 0x06005615 RID: 22037 RVA: 0x001C1D74 File Offset: 0x001BFF74
	public static string GetInternalErrorMessage(NetCache.ErrorInfo info, bool includeStackTrace = true)
	{
		global::Map<Type, object> netCache = NetCache.Get().m_netCache;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("NetCache Error: {0}", info.Error);
		stringBuilder.AppendFormat("\nFrom: {0}", info.RequestingFunction.Method.Name);
		stringBuilder.AppendFormat("\nRequested Data ({0}):", info.RequestedTypes.Count);
		foreach (KeyValuePair<Type, NetCache.Request> keyValuePair in info.RequestedTypes)
		{
			object obj = null;
			netCache.TryGetValue(keyValuePair.Key, out obj);
			if (obj == null)
			{
				stringBuilder.AppendFormat("\n[{0}] MISSING", keyValuePair.Key);
			}
			else
			{
				stringBuilder.AppendFormat("\n[{0}]", keyValuePair.Key);
			}
		}
		if (includeStackTrace)
		{
			stringBuilder.AppendFormat("\nStack Trace:\n{0}", info.RequestStackTrace);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06005616 RID: 22038 RVA: 0x001C1E7C File Offset: 0x001C007C
	private void NetCacheMakeBatchRequest(NetCache.NetCacheBatchRequest batchRequest)
	{
		List<GetAccountInfo.Request> list = new List<GetAccountInfo.Request>();
		List<GenericRequest> list2 = null;
		foreach (KeyValuePair<Type, NetCache.Request> keyValuePair in batchRequest.m_requests)
		{
			NetCache.Request value = keyValuePair.Value;
			if (value == null)
			{
				Debug.LogError(string.Format("NetUseBatchRequest Null request for {0}...SKIP", value.m_type.Name));
			}
			else if (NetCache.m_ServerInitiatedAccountInfoTypes.Contains(value.m_type))
			{
				if (value.m_reload)
				{
					global::Log.All.PrintWarning("Attempting to reload server-initiated NetCache request {0}. This is not valid - the server sends this data when it changes!", new object[]
					{
						value.m_type.FullName
					});
				}
			}
			else
			{
				if (value.m_reload)
				{
					this.m_netCache[value.m_type] = null;
				}
				if ((!this.m_netCache.ContainsKey(value.m_type) || this.m_netCache[value.m_type] == null) && !this.m_inTransitRequests.Contains(value.m_type))
				{
					value.m_result = NetCache.RequestResult.PENDING;
					this.m_inTransitRequests.Add(value.m_type);
					GetAccountInfo.Request item;
					int requestId;
					if (NetCache.m_getAccountInfoTypeMap.TryGetValue(value.m_type, out item))
					{
						list.Add(item);
					}
					else if (NetCache.m_genericRequestTypeMap.TryGetValue(value.m_type, out requestId))
					{
						if (list2 == null)
						{
							list2 = new List<GenericRequest>();
						}
						list2.Add(new GenericRequest
						{
							RequestId = requestId
						});
					}
					else
					{
						global::Log.Net.Print("NetCache: Unable to make request for type={0}", new object[]
						{
							value.m_type.FullName
						});
					}
				}
			}
		}
		if (list.Count > 0 || list2 != null)
		{
			Network.Get().RequestNetCacheObjectList(list, list2);
		}
		if (this.m_cacheRequests.FindIndex((NetCache.NetCacheBatchRequest o) => o.m_callback != null && o.m_callback == batchRequest.m_callback) >= 0)
		{
			global::Log.Net.PrintError("NetCache: detected multiple registrations for same callback! {0}.{1}", new object[]
			{
				batchRequest.m_callback.Target.GetType().Name,
				batchRequest.m_callback.Method.Name
			});
		}
		this.m_cacheRequests.Add(batchRequest);
		this.NetCacheCheckRequest(batchRequest);
	}

	// Token: 0x06005617 RID: 22039 RVA: 0x001C20F8 File Offset: 0x001C02F8
	private void NetCacheUse_Internal(NetCache.NetCacheBatchRequest request, Type type)
	{
		if (request != null && request.m_requests.ContainsKey(type))
		{
			global::Log.Net.Print(string.Format("NetCache ...SKIP {0}", type.Name), Array.Empty<object>());
			return;
		}
		if (this.m_netCache.ContainsKey(type) && this.m_netCache[type] != null)
		{
			global::Log.Net.Print(string.Format("NetCache ...USE {0}", type.Name), Array.Empty<object>());
			return;
		}
		global::Log.Net.Print(string.Format("NetCache <<<GET {0}", type.Name), Array.Empty<object>());
		this.RequestNetCacheObject(type);
	}

	// Token: 0x06005618 RID: 22040 RVA: 0x001C2198 File Offset: 0x001C0398
	private void RequestNetCacheObject(Type type)
	{
		if (this.m_inTransitRequests.Contains(type))
		{
			return;
		}
		this.m_inTransitRequests.Add(type);
		Network.Get().RequestNetCacheObject(NetCache.m_getAccountInfoTypeMap[type]);
	}

	// Token: 0x06005619 RID: 22041 RVA: 0x001C21CA File Offset: 0x001C03CA
	private void NetCacheReload_Internal(NetCache.NetCacheBatchRequest request, Type type)
	{
		this.m_netCache[type] = null;
		if (type == typeof(NetCache.NetCacheProfileNotices))
		{
			Debug.LogError("NetCacheReload_Internal - tried to issue request with type NetCacheProfileNotices - this is no longer allowed!");
			return;
		}
		this.NetCacheUse_Internal(request, type);
	}

	// Token: 0x0600561A RID: 22042 RVA: 0x001C2200 File Offset: 0x001C0400
	private void NetCacheCheckRequest(NetCache.NetCacheBatchRequest request)
	{
		foreach (KeyValuePair<Type, NetCache.Request> keyValuePair in request.m_requests)
		{
			if (!this.m_netCache.ContainsKey(keyValuePair.Key))
			{
				return;
			}
			if (this.m_netCache[keyValuePair.Key] == null)
			{
				return;
			}
		}
		request.m_canTimeout = false;
		if (request.m_callback != null)
		{
			request.m_callback();
		}
	}

	// Token: 0x0600561B RID: 22043 RVA: 0x001C2294 File Offset: 0x001C0494
	private void UpdateRequestNeedState(Type type, NetCache.RequestResult result)
	{
		foreach (NetCache.NetCacheBatchRequest netCacheBatchRequest in this.m_cacheRequests)
		{
			if (netCacheBatchRequest.m_requests.ContainsKey(type))
			{
				netCacheBatchRequest.m_requests[type].m_result = result;
			}
		}
	}

	// Token: 0x0600561C RID: 22044 RVA: 0x001C2300 File Offset: 0x001C0500
	private void OnNetCacheObjReceived<T>(T netCacheObject)
	{
		Type typeFromHandle = typeof(T);
		global::Log.Net.Print(string.Format("OnNetCacheObjReceived SAVE --> {0}", typeFromHandle.Name), Array.Empty<object>());
		this.UpdateRequestNeedState(typeFromHandle, NetCache.RequestResult.DATA_COMPLETE);
		this.m_netCache[typeFromHandle] = netCacheObject;
		this.m_inTransitRequests.Remove(typeFromHandle);
		this.NetCacheChanged<T>();
		HashSet<Action> source;
		if (this.m_updatedListeners.TryGetValue(typeFromHandle, out source))
		{
			Action[] array = source.ToArray<Action>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i]();
			}
		}
	}

	// Token: 0x0600561D RID: 22045 RVA: 0x001C2394 File Offset: 0x001C0594
	public void Clear()
	{
		global::Log.Net.PrintDebug("Clearing NetCache", Array.Empty<object>());
		this.m_netCache.Clear();
		this.m_prevHeroLevels = null;
		this.m_previousMedalInfo = null;
		this.m_changeRequests.Clear();
		this.m_cacheRequests.Clear();
		this.m_inTransitRequests.Clear();
		this.m_receivedInitialClientState = false;
		this.m_ackedNotices.Clear();
		this.m_queuedProfileNotices.Clear();
		this.m_receivedInitialProfileNotices = false;
		this.m_currencyVersion = 0L;
		this.m_initialCollectionVersion = 0L;
		this.m_expectedCardModifications.Clear();
		this.m_handledCardModifications.Clear();
		if (HearthstoneApplication.IsInternal())
		{
			SceneDebugger sceneDebugger;
			if (!HearthstoneServices.TryGet<SceneDebugger>(out sceneDebugger))
			{
				return;
			}
			sceneDebugger.SetPlayerId(null);
		}
	}

	// Token: 0x0600561E RID: 22046 RVA: 0x001C2459 File Offset: 0x001C0659
	public void ClearForNewAuroraConnection()
	{
		this.m_cacheRequests.Clear();
		this.m_inTransitRequests.Clear();
		this.m_receivedInitialClientState = false;
	}

	// Token: 0x0600561F RID: 22047 RVA: 0x001C2478 File Offset: 0x001C0678
	public void UnregisterNetCacheHandler(NetCache.NetCacheCallback handler)
	{
		this.m_cacheRequests.RemoveAll((NetCache.NetCacheBatchRequest o) => o.m_callback == handler);
	}

	// Token: 0x06005620 RID: 22048 RVA: 0x001C24AC File Offset: 0x001C06AC
	public void Update()
	{
		if (!Network.IsRunning())
		{
			return;
		}
		NetCache.NetCacheBatchRequest[] array = this.m_cacheRequests.ToArray();
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		foreach (NetCache.NetCacheBatchRequest netCacheBatchRequest in array)
		{
			if (netCacheBatchRequest.m_canTimeout && realtimeSinceStartup - netCacheBatchRequest.m_timeAdded >= Network.GetMaxDeferredWait() && !Network.Get().HaveUnhandledPackets())
			{
				netCacheBatchRequest.m_canTimeout = false;
				if (!NetCache.m_fatalErrorCodeSet)
				{
					NetCache.ErrorInfo errorInfo = new NetCache.ErrorInfo();
					errorInfo.Error = NetCache.ErrorCode.TIMEOUT;
					errorInfo.RequestingFunction = netCacheBatchRequest.m_requestFunc;
					errorInfo.RequestedTypes = new global::Map<Type, NetCache.Request>(netCacheBatchRequest.m_requests);
					errorInfo.RequestStackTrace = netCacheBatchRequest.m_requestStackTrace;
					string text = "CT";
					int num = 0;
					foreach (KeyValuePair<Type, NetCache.Request> keyValuePair in netCacheBatchRequest.m_requests)
					{
						NetCache.RequestResult result = keyValuePair.Value.m_result;
						if (result - NetCache.RequestResult.GENERIC_COMPLETE > 1)
						{
							string[] array3 = keyValuePair.Value.m_type.ToString().Split(new char[]
							{
								'+'
							});
							if (array3.GetLength(0) != 0)
							{
								string text2 = array3[array3.GetLength(0) - 1];
								text = string.Concat(new object[]
								{
									text,
									";",
									text2,
									"=",
									(int)keyValuePair.Value.m_result
								});
								num++;
							}
						}
						if (num >= 3)
						{
							break;
						}
					}
					FatalErrorMgr.Get().SetErrorCode("HS", text, null, null);
					NetCache.m_fatalErrorCodeSet = true;
					netCacheBatchRequest.m_errorCallback(errorInfo);
				}
			}
		}
		this.CheckSeasonForRoll();
	}

	// Token: 0x06005621 RID: 22049 RVA: 0x001C267C File Offset: 0x001C087C
	private void OnGenericResponse()
	{
		Network.GenericResponse genericResponse = Network.Get().GetGenericResponse();
		if (genericResponse == null)
		{
			Debug.LogError(string.Format("NetCache - GenericResponse parse error", Array.Empty<object>()));
			return;
		}
		if ((long)genericResponse.RequestId != 201L)
		{
			return;
		}
		Type key;
		if (!NetCache.m_requestTypeMap.TryGetValue((GetAccountInfo.Request)genericResponse.RequestSubId, out key))
		{
			Debug.LogError(string.Format("NetCache - Ignoring unexpected requestId={0}:{1}", genericResponse.RequestId, genericResponse.RequestSubId));
			return;
		}
		foreach (NetCache.NetCacheBatchRequest netCacheBatchRequest in this.m_cacheRequests.ToArray())
		{
			if (netCacheBatchRequest.m_requests.ContainsKey(key))
			{
				Network.GenericResponse.Result resultCode = genericResponse.ResultCode;
				if (resultCode != Network.GenericResponse.Result.RESULT_REQUEST_IN_PROCESS)
				{
					if (resultCode != Network.GenericResponse.Result.RESULT_REQUEST_COMPLETE)
					{
						if (resultCode != Network.GenericResponse.Result.RESULT_DATA_MIGRATION_REQUIRED)
						{
							Debug.LogError(string.Format("Unhandled failure code={0} {1} for requestId={2}:{3}", new object[]
							{
								(int)genericResponse.ResultCode,
								genericResponse.ResultCode.ToString(),
								genericResponse.RequestId,
								genericResponse.RequestSubId
							}));
							netCacheBatchRequest.m_requests[key].m_result = NetCache.RequestResult.ERROR;
							NetCache.ErrorInfo errorInfo = new NetCache.ErrorInfo();
							errorInfo.Error = NetCache.ErrorCode.SERVER;
							errorInfo.ServerError = (uint)genericResponse.ResultCode;
							errorInfo.RequestingFunction = netCacheBatchRequest.m_requestFunc;
							errorInfo.RequestedTypes = new global::Map<Type, NetCache.Request>(netCacheBatchRequest.m_requests);
							errorInfo.RequestStackTrace = netCacheBatchRequest.m_requestStackTrace;
							FatalErrorMgr.Get().SetErrorCode("HS", "CG" + genericResponse.ResultCode.ToString(), genericResponse.RequestId.ToString(), genericResponse.RequestSubId.ToString());
							netCacheBatchRequest.m_errorCallback(errorInfo);
						}
						else
						{
							netCacheBatchRequest.m_requests[key].m_result = NetCache.RequestResult.MIGRATION_REQUIRED;
							Debug.LogWarning(string.Format("GenericResponse player migration required code={0} {1} for requestId={2}:{3}", new object[]
							{
								(int)genericResponse.ResultCode,
								genericResponse.ResultCode.ToString(),
								genericResponse.RequestId,
								genericResponse.RequestSubId
							}));
						}
					}
					else
					{
						netCacheBatchRequest.m_requests[key].m_result = NetCache.RequestResult.GENERIC_COMPLETE;
						Debug.LogWarning(string.Format("GenericResponse Success for requestId={0}:{1}", genericResponse.RequestId, genericResponse.RequestSubId));
					}
				}
				else if (NetCache.RequestResult.PENDING == netCacheBatchRequest.m_requests[key].m_result)
				{
					netCacheBatchRequest.m_requests[key].m_result = NetCache.RequestResult.IN_PROCESS;
				}
			}
		}
	}

	// Token: 0x06005622 RID: 22050 RVA: 0x001C2938 File Offset: 0x001C0B38
	private void OnDBAction()
	{
		Network.DBAction dbAction = Network.Get().GetDbAction();
		if (Network.DBAction.ResultType.SUCCESS != dbAction.Result)
		{
			Debug.LogError(string.Format("Unhandled dbAction {0} with error {1}", dbAction.Action, dbAction.Result));
		}
	}

	// Token: 0x06005623 RID: 22051 RVA: 0x001C297E File Offset: 0x001C0B7E
	public void FakeInitialClientState()
	{
		this.OnInitialClientState();
	}

	// Token: 0x06005624 RID: 22052 RVA: 0x001C2988 File Offset: 0x001C0B88
	private void OnInitialClientState()
	{
		InitialClientState initialClientState = Network.Get().GetInitialClientState();
		if (initialClientState == null)
		{
			return;
		}
		this.m_receivedInitialClientState = true;
		if (initialClientState.HasGuardianVars)
		{
			this.OnGuardianVars(initialClientState.GuardianVars);
		}
		if (initialClientState.SpecialEventTiming.Count > 0)
		{
			long devTimeOffsetSeconds = initialClientState.HasDevTimeOffsetSeconds ? initialClientState.DevTimeOffsetSeconds : 0L;
			SpecialEventManager.Get().InitEventTimingsFromServer(devTimeOffsetSeconds, initialClientState.SpecialEventTiming);
		}
		if (initialClientState.HasClientOptions)
		{
			this.OnClientOptions(initialClientState.ClientOptions);
		}
		if (initialClientState.HasCollection)
		{
			this.OnCollection(initialClientState.Collection);
		}
		else
		{
			this.OnCollection(OfflineDataCache.GetCachedCollection());
		}
		if (initialClientState.HasAchievements)
		{
			AchieveManager.Get().OnInitialAchievements(initialClientState.Achievements);
		}
		if (initialClientState.HasNotices)
		{
			this.OnInitialClientState_ProfileNotices(initialClientState.Notices);
		}
		if (initialClientState.HasGameCurrencyStates)
		{
			this.OnCurrencyState(initialClientState.GameCurrencyStates);
		}
		if (initialClientState.HasBoosters)
		{
			this.OnBoosters(initialClientState.Boosters);
		}
		if (initialClientState.HasPlayerDraftTickets)
		{
			this.OnPlayerDraftTickets(initialClientState.PlayerDraftTickets);
		}
		foreach (TavernBrawlInfo body in initialClientState.TavernBrawlsList)
		{
			PegasusPacket packet = new PegasusPacket(316, 0, body);
			Network.Get().SimulateReceivedPacketFromServer(packet);
		}
		if (initialClientState.HasDisconnectedGame)
		{
			this.OnDisconnectedGame(initialClientState.DisconnectedGame);
		}
		if (initialClientState.HasArenaSession)
		{
			PegasusPacket packet2 = new PegasusPacket(351, 0, initialClientState.ArenaSession);
			Network.Get().SimulateReceivedPacketFromServer(packet2);
		}
		if (initialClientState.HasDisplayBanner)
		{
			this.OnDisplayBanner(initialClientState.DisplayBanner);
		}
		if (initialClientState.Decks != null)
		{
			this.OnReceivedDeckHeaders_InitialClientState(initialClientState.Decks, initialClientState.DeckContents, initialClientState.ValidCachedDeckIds);
		}
		if (initialClientState.MedalInfo != null)
		{
			this.OnMedalInfo(initialClientState.MedalInfo);
		}
		if (initialClientState.GameSaveData != null)
		{
			GameSaveDataManager.Get().SetGameSaveDataUpdateFromInitialClientState(initialClientState.GameSaveData);
		}
		if (HearthstoneApplication.IsInternal() && initialClientState.HasPlayerId)
		{
			SceneDebugger sceneDebugger;
			if (!HearthstoneServices.TryGet<SceneDebugger>(out sceneDebugger))
			{
				return;
			}
			sceneDebugger.SetPlayerId(new long?(initialClientState.PlayerId));
		}
		if (Network.Get() != null)
		{
			Network.Get().OnInitialClientStateProcessed();
		}
	}

	// Token: 0x06005625 RID: 22053 RVA: 0x001C2BC0 File Offset: 0x001C0DC0
	public void OnCollection(Collection collection)
	{
		this.m_initialCollectionVersion = collection.CollectionVersion;
		if (CollectionManager.Get() != null)
		{
			this.OnNetCacheObjReceived<NetCache.NetCacheCollection>(CollectionManager.Get().OnInitialCollectionReceived(collection));
		}
		OfflineDataCache.CacheCollection(collection);
	}

	// Token: 0x06005626 RID: 22054 RVA: 0x001C2BEC File Offset: 0x001C0DEC
	private void OnBoosters(Boosters boosters)
	{
		NetCache.NetCacheBoosters netCacheBoosters = new NetCache.NetCacheBoosters();
		for (int i = 0; i < boosters.List.Count; i++)
		{
			BoosterInfo boosterInfo = boosters.List[i];
			NetCache.BoosterStack item = new NetCache.BoosterStack
			{
				Id = boosterInfo.Type,
				Count = boosterInfo.Count,
				EverGrantedCount = boosterInfo.EverGrantedCount
			};
			netCacheBoosters.BoosterStacks.Add(item);
		}
		this.OnNetCacheObjReceived<NetCache.NetCacheBoosters>(netCacheBoosters);
	}

	// Token: 0x06005627 RID: 22055 RVA: 0x001C2C60 File Offset: 0x001C0E60
	public void OnPlayerDraftTickets(PlayerDraftTickets playerDraftTickets)
	{
		this.OnNetCacheObjReceived<NetCache.NetPlayerArenaTickets>(new NetCache.NetPlayerArenaTickets
		{
			Balance = playerDraftTickets.UnusedTicketBalance
		});
	}

	// Token: 0x06005628 RID: 22056 RVA: 0x001C2C88 File Offset: 0x001C0E88
	private void OnDisconnectedGame(GameConnectionInfo packet)
	{
		if (!packet.HasAddress)
		{
			return;
		}
		NetCache.NetCacheDisconnectedGame netCacheDisconnectedGame = new NetCache.NetCacheDisconnectedGame();
		netCacheDisconnectedGame.ServerInfo = new GameServerInfo();
		netCacheDisconnectedGame.ServerInfo.Address = packet.Address;
		netCacheDisconnectedGame.ServerInfo.GameHandle = (uint)packet.GameHandle;
		netCacheDisconnectedGame.ServerInfo.ClientHandle = packet.ClientHandle;
		netCacheDisconnectedGame.ServerInfo.Port = (uint)packet.Port;
		netCacheDisconnectedGame.ServerInfo.AuroraPassword = packet.AuroraPassword;
		netCacheDisconnectedGame.ServerInfo.Mission = packet.Scenario;
		netCacheDisconnectedGame.ServerInfo.BrawlLibraryItemId = packet.BrawlLibraryItemId;
		netCacheDisconnectedGame.ServerInfo.Version = BattleNet.GetVersion();
		netCacheDisconnectedGame.ServerInfo.Resumable = true;
		netCacheDisconnectedGame.GameType = packet.GameType;
		netCacheDisconnectedGame.FormatType = packet.FormatType;
		if (packet.HasLoadGameState)
		{
			netCacheDisconnectedGame.LoadGameState = packet.LoadGameState;
		}
		else
		{
			netCacheDisconnectedGame.LoadGameState = false;
		}
		this.OnNetCacheObjReceived<NetCache.NetCacheDisconnectedGame>(netCacheDisconnectedGame);
	}

	// Token: 0x06005629 RID: 22057 RVA: 0x001C2D80 File Offset: 0x001C0F80
	private void OnDisplayBanner(int displayBanner)
	{
		this.OnNetCacheObjReceived<NetCache.NetCacheDisplayBanner>(new NetCache.NetCacheDisplayBanner
		{
			Id = displayBanner
		});
	}

	// Token: 0x0600562A RID: 22058 RVA: 0x001C2DA4 File Offset: 0x001C0FA4
	private void OnReceivedDeckHeaders()
	{
		NetCache.NetCacheDecks deckHeaders = Network.Get().GetDeckHeaders();
		this.OnNetCacheObjReceived<NetCache.NetCacheDecks>(deckHeaders);
	}

	// Token: 0x0600562B RID: 22059 RVA: 0x001C2DC4 File Offset: 0x001C0FC4
	private void OnReceivedDeckHeaders_InitialClientState(List<DeckInfo> deckHeaders, List<DeckContents> deckContents, List<long> validCachedDeckIds)
	{
		foreach (DeckInfo item in OfflineDataCache.GetFakeDeckInfos())
		{
			deckHeaders.Add(item);
		}
		NetCache.NetCacheDecks deckHeaders2 = Network.GetDeckHeaders(deckHeaders);
		this.OnNetCacheObjReceived<NetCache.NetCacheDecks>(deckHeaders2);
		Network.Get().ReconcileDeckContentsForChangedOfflineDecks(deckHeaders, deckContents, validCachedDeckIds);
		CollectionManager.Get().OnInitialClientStateDeckContents(deckHeaders2, OfflineDataCache.GetLocalDeckContentsFromCache());
	}

	// Token: 0x0600562C RID: 22060 RVA: 0x001C2E44 File Offset: 0x001C1044
	public List<DeckInfo> GetDeckListFromNetCache()
	{
		return (from h in this.GetNetObject<NetCache.NetCacheDecks>().Decks
		select Network.GetDeckInfoFromDeckHeader(h)).ToList<DeckInfo>();
	}

	// Token: 0x0600562D RID: 22061 RVA: 0x001C2E7C File Offset: 0x001C107C
	private void OnCardValues()
	{
		NetCache.NetCacheCardValues netCacheCardValues = NetCache.Get().GetNetObject<NetCache.NetCacheCardValues>();
		if (netCacheCardValues == null)
		{
			netCacheCardValues = new NetCache.NetCacheCardValues();
		}
		CardValues cardValues = Network.Get().GetCardValues();
		if (cardValues != null)
		{
			foreach (PegasusUtil.CardValue cardValue in cardValues.Cards)
			{
				string text = GameUtils.TranslateDbIdToCardId(cardValue.Card.Asset, false);
				if (text == null)
				{
					global::Log.All.PrintError("NetCache.OnCardValues(): Cannot find card '{0}' in card manifest.  Confirm your card manifest matches your game server's database.", new object[]
					{
						cardValue.Card.Asset
					});
				}
				else
				{
					netCacheCardValues.Values.Add(new NetCache.CardDefinition
					{
						Name = text,
						Premium = (TAG_PREMIUM)cardValue.Card.Premium
					}, new NetCache.CardValue
					{
						BaseBuyValue = cardValue.Buy,
						BaseSellValue = cardValue.Sell,
						BuyValueOverride = (cardValue.HasBuyValueOverride ? cardValue.BuyValueOverride : 0),
						SellValueOverride = (cardValue.HasSellValueOverride ? cardValue.SellValueOverride : 0),
						OverrideEvent = (cardValue.HasOverrideEventName ? SpecialEventManager.GetEventType(cardValue.OverrideEventName) : SpecialEventType.SPECIAL_EVENT_NEVER)
					});
				}
			}
		}
		this.OnNetCacheObjReceived<NetCache.NetCacheCardValues>(netCacheCardValues);
	}

	// Token: 0x0600562E RID: 22062 RVA: 0x001C2FD4 File Offset: 0x001C11D4
	private void OnMedalInfo()
	{
		NetCache.NetCacheMedalInfo medalInfo = Network.Get().GetMedalInfo();
		if (this.m_previousMedalInfo != null)
		{
			medalInfo.PreviousMedalInfo = this.m_previousMedalInfo.Clone();
		}
		this.m_previousMedalInfo = medalInfo;
		this.OnNetCacheObjReceived<NetCache.NetCacheMedalInfo>(medalInfo);
	}

	// Token: 0x0600562F RID: 22063 RVA: 0x001C3014 File Offset: 0x001C1214
	private void OnMedalInfo(MedalInfo packet)
	{
		NetCache.NetCacheMedalInfo netCacheMedalInfo = new NetCache.NetCacheMedalInfo(packet);
		if (this.m_previousMedalInfo != null)
		{
			netCacheMedalInfo.PreviousMedalInfo = this.m_previousMedalInfo.Clone();
		}
		this.m_previousMedalInfo = netCacheMedalInfo;
		this.OnNetCacheObjReceived<NetCache.NetCacheMedalInfo>(netCacheMedalInfo);
	}

	// Token: 0x06005630 RID: 22064 RVA: 0x001C3050 File Offset: 0x001C1250
	private void OnBaconRatingInfo()
	{
		NetCache.NetCacheBaconRatingInfo baconRatingInfo = Network.Get().GetBaconRatingInfo();
		this.OnNetCacheObjReceived<NetCache.NetCacheBaconRatingInfo>(baconRatingInfo);
	}

	// Token: 0x06005631 RID: 22065 RVA: 0x001C3070 File Offset: 0x001C1270
	private void OnBaconPremiumStatus()
	{
		NetCache.NetCacheBaconPremiumStatus baconPremiumStatus = Network.Get().GetBaconPremiumStatus();
		this.OnNetCacheObjReceived<NetCache.NetCacheBaconPremiumStatus>(baconPremiumStatus);
	}

	// Token: 0x06005632 RID: 22066 RVA: 0x001C3090 File Offset: 0x001C1290
	public long GetBattlegroundsEarlyAccessLicenseId()
	{
		NetCache.NetCacheFeatures netObject = this.GetNetObject<NetCache.NetCacheFeatures>();
		if (netObject != null)
		{
			return (long)netObject.BattlegroundsEarlyAccessLicense;
		}
		return 50336L;
	}

	// Token: 0x06005633 RID: 22067 RVA: 0x001C30B8 File Offset: 0x001C12B8
	public long GetDuelsEarlyAccessLicenseId()
	{
		NetCache.NetCacheFeatures netObject = this.GetNetObject<NetCache.NetCacheFeatures>();
		if (netObject != null)
		{
			return (long)((ulong)netObject.DuelsEarlyAccessLicense);
		}
		return 77345L;
	}

	// Token: 0x06005634 RID: 22068 RVA: 0x001C30E0 File Offset: 0x001C12E0
	private void OnPVPDRStatsInfo()
	{
		NetCache.NetCachePVPDRStatsInfo pvpdrstatsInfo = Network.Get().GetPVPDRStatsInfo();
		this.OnNetCacheObjReceived<NetCache.NetCachePVPDRStatsInfo>(pvpdrstatsInfo);
	}

	// Token: 0x06005635 RID: 22069 RVA: 0x001C3100 File Offset: 0x001C1300
	private void OnGuardianVars()
	{
		GuardianVars guardianVars = Network.Get().GetGuardianVars();
		if (guardianVars == null)
		{
			return;
		}
		this.OnGuardianVars(guardianVars);
	}

	// Token: 0x06005636 RID: 22070 RVA: 0x001C3124 File Offset: 0x001C1324
	private void OnGuardianVars(GuardianVars packet)
	{
		NetCache.NetCacheFeatures netCacheFeatures = new NetCache.NetCacheFeatures();
		netCacheFeatures.Games.Tournament = (!packet.HasTourney || packet.Tourney);
		netCacheFeatures.Games.Practice = (!packet.HasPractice || packet.Practice);
		netCacheFeatures.Games.Casual = (!packet.HasCasual || packet.Casual);
		netCacheFeatures.Games.Forge = (!packet.HasForge || packet.Forge);
		netCacheFeatures.Games.Friendly = (!packet.HasFriendly || packet.Friendly);
		netCacheFeatures.Games.TavernBrawl = (!packet.HasTavernBrawl || packet.TavernBrawl);
		netCacheFeatures.Games.Battlegrounds = (!packet.HasBattlegrounds || packet.Battlegrounds);
		netCacheFeatures.Games.BattlegroundsFriendlyChallenge = (!packet.HasBattlegroundsFriendlyChallenge || packet.BattlegroundsFriendlyChallenge);
		netCacheFeatures.Games.BattlegroundsTutorial = (!packet.HasBattlegroundsTutorial || packet.BattlegroundsTutorial);
		netCacheFeatures.Games.ShowUserUI = (packet.HasShowUserUI ? packet.ShowUserUI : 0);
		netCacheFeatures.Games.Duels = (!packet.HasDuels || packet.Duels);
		netCacheFeatures.Games.PaidDuels = (!packet.HasPaidDuels || packet.PaidDuels);
		netCacheFeatures.Collection.Manager = (!packet.HasManager || packet.Manager);
		netCacheFeatures.Collection.Crafting = (!packet.HasCrafting || packet.Crafting);
		netCacheFeatures.Collection.DeckReordering = (!packet.HasDeckReordering || packet.DeckReordering);
		netCacheFeatures.Store.Store = (!packet.HasStore || packet.Store);
		netCacheFeatures.Store.BattlePay = (!packet.HasBattlePay || packet.BattlePay);
		netCacheFeatures.Store.BuyWithGold = (!packet.HasBuyWithGold || packet.BuyWithGold);
		netCacheFeatures.Store.SimpleCheckout = (!packet.HasSimpleCheckout || packet.SimpleCheckout);
		netCacheFeatures.Store.SoftAccountPurchasing = (!packet.HasSoftAccountPurchasing || packet.SoftAccountPurchasing);
		netCacheFeatures.Store.VirtualCurrencyEnabled = (packet.HasVirtualCurrencyEnabled && packet.VirtualCurrencyEnabled);
		netCacheFeatures.Store.NumClassicPacksUntilDeprioritize = (packet.HasNumClassicPacksUntilDeprioritize ? packet.NumClassicPacksUntilDeprioritize : -1);
		netCacheFeatures.Store.SimpleCheckoutIOS = (!packet.HasSimpleCheckoutIos || packet.SimpleCheckoutIos);
		netCacheFeatures.Store.SimpleCheckoutAndroidAmazon = (!packet.HasSimpleCheckoutAndroidAmazon || packet.SimpleCheckoutAndroidAmazon);
		netCacheFeatures.Store.SimpleCheckoutAndroidGoogle = (!packet.HasSimpleCheckoutAndroidGoogle || packet.SimpleCheckoutAndroidGoogle);
		netCacheFeatures.Store.SimpleCheckoutAndroidGlobal = (!packet.HasSimpleCheckoutAndroidGlobal || packet.SimpleCheckoutAndroidGlobal);
		netCacheFeatures.Store.SimpleCheckoutWin = (!packet.HasSimpleCheckoutWin || packet.SimpleCheckoutWin);
		netCacheFeatures.Store.SimpleCheckoutMac = (!packet.HasSimpleCheckoutMac || packet.SimpleCheckoutMac);
		netCacheFeatures.Store.BoosterRotatingSoonWarnDaysWithoutSale = (packet.HasBoosterRotatingSoonWarnDaysWithoutSale ? packet.BoosterRotatingSoonWarnDaysWithoutSale : 0);
		netCacheFeatures.Store.BoosterRotatingSoonWarnDaysWithSale = (packet.HasBoosterRotatingSoonWarnDaysWithSale ? packet.BoosterRotatingSoonWarnDaysWithSale : 0);
		netCacheFeatures.Store.VintageStore = (!packet.HasVintageStoreEnabled || packet.VintageStoreEnabled);
		netCacheFeatures.Store.BuyCardBacksFromCollectionManager = (!packet.HasBuyCardBacksFromCollectionManagerEnabled || packet.BuyCardBacksFromCollectionManagerEnabled);
		netCacheFeatures.Store.BuyHeroSkinsFromCollectionManager = (!packet.HasBuyHeroSkinsFromCollectionManagerEnabled || packet.BuyHeroSkinsFromCollectionManagerEnabled);
		netCacheFeatures.Heroes.Hunter = (!packet.HasHunter || packet.Hunter);
		netCacheFeatures.Heroes.Mage = (!packet.HasMage || packet.Mage);
		netCacheFeatures.Heroes.Paladin = (!packet.HasPaladin || packet.Paladin);
		netCacheFeatures.Heroes.Priest = (!packet.HasPriest || packet.Priest);
		netCacheFeatures.Heroes.Rogue = (!packet.HasRogue || packet.Rogue);
		netCacheFeatures.Heroes.Shaman = (!packet.HasShaman || packet.Shaman);
		netCacheFeatures.Heroes.Warlock = (!packet.HasWarlock || packet.Warlock);
		netCacheFeatures.Heroes.Warrior = (!packet.HasWarrior || packet.Warrior);
		netCacheFeatures.Misc.ClientOptionsUpdateIntervalSeconds = (packet.HasClientOptionsUpdateIntervalSeconds ? packet.ClientOptionsUpdateIntervalSeconds : 0);
		netCacheFeatures.Misc.AllowLiveFPSGathering = (packet.HasAllowLiveFpsGathering && packet.AllowLiveFpsGathering);
		netCacheFeatures.CaisEnabledNonMobile = (!packet.HasCaisEnabledNonMobile || packet.CaisEnabledNonMobile);
		netCacheFeatures.CaisEnabledMobileChina = (packet.HasCaisEnabledMobileChina && packet.CaisEnabledMobileChina);
		netCacheFeatures.CaisEnabledMobileSouthKorea = (packet.HasCaisEnabledMobileSouthKorea && packet.CaisEnabledMobileSouthKorea);
		netCacheFeatures.SendTelemetryPresence = (packet.HasSendTelemetryPresence && packet.SendTelemetryPresence);
		netCacheFeatures.WinsPerGold = packet.WinsPerGold;
		netCacheFeatures.GoldPerReward = packet.GoldPerReward;
		netCacheFeatures.MaxGoldPerDay = packet.MaxGoldPerDay;
		netCacheFeatures.XPSoloLimit = packet.XpSoloLimit;
		netCacheFeatures.MaxHeroLevel = packet.MaxHeroLevel;
		netCacheFeatures.SpecialEventTimingMod = packet.EventTimingMod;
		netCacheFeatures.FriendWeekConcederMaxDefense = packet.FriendWeekConcederMaxDefense;
		netCacheFeatures.FriendWeekConcededGameMinTotalTurns = packet.FriendWeekConcededGameMinTotalTurns;
		netCacheFeatures.FriendWeekAllowsTavernBrawlRecordUpdate = packet.FriendWeekAllowsTavernBrawlRecordUpdate;
		netCacheFeatures.FSGEnabled = (packet.HasFsgEnabled && packet.FsgEnabled);
		netCacheFeatures.FSGLoginScanEnabled = (packet.HasFsgLoginScanEnabled && packet.FsgLoginScanEnabled);
		netCacheFeatures.FSGAutoCheckinEnabled = (packet.HasFsgAutoCheckinEnabled && packet.FsgAutoCheckinEnabled);
		netCacheFeatures.FSGShowBetaLabel = (packet.HasFsgShowBetaLabel && packet.FsgShowBetaLabel);
		netCacheFeatures.FSGFriendListPatronCountLimit = (packet.HasFsgFriendListPatronCountLimit ? packet.FsgFriendListPatronCountLimit : -1);
		netCacheFeatures.ArenaClosedToNewSessionsSeconds = (packet.HasArenaClosedToNewSessionsSeconds ? packet.ArenaClosedToNewSessionsSeconds : 0U);
		netCacheFeatures.PVPDRClosedToNewSessionsSeconds = (packet.HasPvpdrClosedToNewSessionsSeconds ? packet.PvpdrClosedToNewSessionsSeconds : 0U);
		netCacheFeatures.FsgMaxPresencePubscribedPatronCount = (packet.HasFsgMaxPresencePubscribedPatronCount ? packet.FsgMaxPresencePubscribedPatronCount : -1);
		netCacheFeatures.QuickPackOpeningAllowed = (packet.HasQuickPackOpeningAllowed && packet.QuickPackOpeningAllowed);
		netCacheFeatures.ForceIosLowRes = (packet.HasAllowIosHighres && !packet.AllowIosHighres);
		netCacheFeatures.AllowOfflineClientActivity = (packet.HasAllowOfflineClientActivityDesktop && packet.AllowOfflineClientActivityDesktop);
		netCacheFeatures.EnableSmartDeckCompletion = (packet.HasEnableSmartDeckCompletion && packet.EnableSmartDeckCompletion);
		netCacheFeatures.AllowOfflineClientDeckDeletion = (packet.HasAllowOfflineClientDeckDeletion && packet.AllowOfflineClientDeckDeletion);
		netCacheFeatures.BattlegroundsEarlyAccessLicense = (packet.HasBattlegroundsEarlyAccessLicense ? packet.BattlegroundsEarlyAccessLicense : 0);
		netCacheFeatures.BattlegroundsMaxRankedPartySize = (packet.HasBattlegroundsMaxRankedPartySize ? packet.BattlegroundsMaxRankedPartySize : PartyManager.BATTLEGROUNDS_MAX_RANKED_PARTY_SIZE_FALLBACK);
		netCacheFeatures.ProgressionEnabled = packet.ProgressionEnabled;
		netCacheFeatures.JournalButtonDisabled = packet.JournalButtonDisabled;
		netCacheFeatures.AchievementToastDisabled = packet.AchievementToastDisabled;
		netCacheFeatures.DuelsEarlyAccessLicense = (packet.HasDuelsEarlyAccessLicense ? packet.DuelsEarlyAccessLicense : 0U);
		netCacheFeatures.ContentstackEnabled = (!packet.HasContentstackEnabled || packet.ContentstackEnabled);
		netCacheFeatures.AppRatingEnabled = (!packet.HasAppRatingEnabled || packet.AppRatingEnabled);
		netCacheFeatures.AppRatingSamplingPercentage = packet.AppRatingSamplingPercentage;
		if (packet.HasFsgEnabled && packet.FsgEnabled)
		{
			Network.Get().EnsureSubscribedTo(UtilSystemId.FIRESIDE_GATHERINGS);
		}
		this.OnNetCacheObjReceived<NetCache.NetCacheFeatures>(netCacheFeatures);
	}

	// Token: 0x06005637 RID: 22071 RVA: 0x001C38AC File Offset: 0x001C1AAC
	public void OnCurrencyState(GameCurrencyStates currencyState)
	{
		if (!currencyState.HasCurrencyVersion || this.m_currencyVersion > currencyState.CurrencyVersion)
		{
			global::Log.Net.PrintDebug("Ignoring currency state: {0}, (cached currency version: {1})", new object[]
			{
				currencyState.ToHumanReadableString(),
				this.m_currencyVersion
			});
			return;
		}
		global::Log.Net.PrintDebug("Caching currency state: {0}", new object[]
		{
			currencyState.ToHumanReadableString()
		});
		this.m_currencyVersion = currencyState.CurrencyVersion;
		if (currencyState.HasArcaneDustBalance)
		{
			NetCache.NetCacheArcaneDustBalance netCacheArcaneDustBalance = this.GetNetObject<NetCache.NetCacheArcaneDustBalance>();
			if (netCacheArcaneDustBalance == null)
			{
				netCacheArcaneDustBalance = new NetCache.NetCacheArcaneDustBalance();
			}
			netCacheArcaneDustBalance.Balance = currencyState.ArcaneDustBalance;
			this.OnNetCacheObjReceived<NetCache.NetCacheArcaneDustBalance>(netCacheArcaneDustBalance);
		}
		if (currencyState.HasCappedGoldBalance && currencyState.HasBonusGoldBalance)
		{
			NetCache.NetCacheGoldBalance netCacheGoldBalance = this.GetNetObject<NetCache.NetCacheGoldBalance>();
			if (netCacheGoldBalance == null)
			{
				netCacheGoldBalance = new NetCache.NetCacheGoldBalance();
			}
			netCacheGoldBalance.CappedBalance = currencyState.CappedGoldBalance;
			netCacheGoldBalance.BonusBalance = currencyState.BonusGoldBalance;
			if (currencyState.HasGoldCap)
			{
				netCacheGoldBalance.Cap = currencyState.GoldCap;
			}
			if (currencyState.HasGoldCapWarning)
			{
				netCacheGoldBalance.CapWarning = currencyState.GoldCapWarning;
			}
			this.OnNetCacheObjReceived<NetCache.NetCacheGoldBalance>(netCacheGoldBalance);
			NetCache.DelGoldBalanceListener[] array = this.m_goldBalanceListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i](netCacheGoldBalance);
			}
		}
		if (BnetBar.Get() != null)
		{
			BnetBar.Get().RefreshCurrency();
		}
	}

	// Token: 0x06005638 RID: 22072 RVA: 0x001C39F8 File Offset: 0x001C1BF8
	public void OnBoosterModifications(BoosterModifications packet)
	{
		NetCache.NetCacheBoosters netObject = NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>();
		if (netObject != null)
		{
			foreach (BoosterInfo boosterInfo in packet.Modifications)
			{
				NetCache.BoosterStack boosterStack = netObject.GetBoosterStack(boosterInfo.Type);
				if (boosterStack == null)
				{
					boosterStack = new NetCache.BoosterStack();
					boosterStack.Id = boosterInfo.Type;
					netObject.BoosterStacks.Add(boosterStack);
				}
				if (boosterInfo.Count < 0)
				{
					while (boosterInfo.Count < 0)
					{
						int num;
						if (boosterStack.LocallyPreConsumedCount > 0)
						{
							NetCache.BoosterStack boosterStack2 = boosterStack;
							num = boosterStack2.LocallyPreConsumedCount - 1;
							boosterStack2.LocallyPreConsumedCount = num;
						}
						else
						{
							NetCache.BoosterStack boosterStack3 = boosterStack;
							num = boosterStack3.Count - 1;
							boosterStack3.Count = num;
						}
						BoosterInfo boosterInfo2 = boosterInfo;
						num = boosterInfo2.Count + 1;
						boosterInfo2.Count = num;
					}
				}
				else
				{
					boosterStack.Count += boosterInfo.Count;
					boosterStack.EverGrantedCount += boosterInfo.EverGrantedCount;
				}
			}
			this.OnNetCacheObjReceived<NetCache.NetCacheBoosters>(netObject);
		}
	}

	// Token: 0x06005639 RID: 22073 RVA: 0x001C3B10 File Offset: 0x001C1D10
	public bool AddExpectedCollectionModification(long version)
	{
		if (!this.m_handledCardModifications.Contains(version))
		{
			this.m_expectedCardModifications.Add(version);
			return true;
		}
		return false;
	}

	// Token: 0x0600563A RID: 22074 RVA: 0x001C3B30 File Offset: 0x001C1D30
	public void OnCollectionModification(ClientStateNotification packet)
	{
		CollectionModifications collectionModifications = packet.CollectionModifications;
		if (this.m_handledCardModifications.Contains(collectionModifications.CollectionVersion) || this.m_initialCollectionVersion >= collectionModifications.CollectionVersion)
		{
			global::Log.Net.PrintDebug("Ignoring redundant coolection modification (modification was v.{0}; we are v.{1})", new object[]
			{
				collectionModifications.CollectionVersion,
				Math.Max(this.m_handledCardModifications.DefaultIfEmpty(0L).Max(), this.m_initialCollectionVersion)
			});
			return;
		}
		this.OnCollectionModificationInternal(collectionModifications);
		if (packet.HasAchievementNotifications)
		{
			AchieveManager.Get().OnAchievementNotifications(packet.AchievementNotifications.AchievementNotifications_);
		}
		if (packet.HasNoticeNotifications)
		{
			Network.Get().OnNoticeNotifications(packet.NoticeNotifications);
		}
		if (packet.HasBoosterModifications)
		{
			NetCache.Get().OnBoosterModifications(packet.BoosterModifications);
		}
		if (collectionModifications.CardModifications.Count > 0 && CollectionManager.Get().GetCollectibleDisplay() != null && CollectionManager.Get().GetCollectibleDisplay().m_pageManager != null)
		{
			CollectionManager.Get().GetCollectibleDisplay().m_pageManager.RefreshCurrentPageContents();
			CollectionManager.Get().GetCollectibleDisplay().UpdateCurrentPageCardLocks(false);
		}
	}

	// Token: 0x0600563B RID: 22075 RVA: 0x001C3C60 File Offset: 0x001C1E60
	private void OnCollectionModificationInternal(CollectionModifications packet)
	{
		this.m_handledCardModifications.Add(packet.CollectionVersion);
		this.m_expectedCardModifications.Remove(packet.CollectionVersion);
		foreach (CardModification cardModification in packet.CardModifications)
		{
			global::Log.Net.PrintDebug("Handling card collection modification (collection version {0}): {1}", new object[]
			{
				packet.CollectionVersion,
				cardModification.ToHumanReadableString()
			});
			string cardID = GameUtils.TranslateDbIdToCardId(cardModification.AssetCardId, false);
			if (cardModification.Quantity > 0)
			{
				int num = 0;
				int num2 = Math.Min(cardModification.AmountSeen, cardModification.Quantity);
				if (cardModification.AmountSeen > 0)
				{
					CollectionManager.Get().OnCardAdded(cardID, (TAG_PREMIUM)cardModification.Premium, num2, true);
					num = num2;
				}
				int num3 = cardModification.Quantity - num;
				if (num3 > 0)
				{
					CollectionManager.Get().OnCardAdded(cardID, (TAG_PREMIUM)cardModification.Premium, num3, false);
				}
			}
			else if (cardModification.Quantity < 0)
			{
				CollectionManager.Get().OnCardRemoved(cardID, (TAG_PREMIUM)cardModification.Premium, -1 * cardModification.Quantity);
			}
		}
		AchieveManager.Get().ValidateAchievesNow();
	}

	// Token: 0x0600563C RID: 22076 RVA: 0x001C3DA0 File Offset: 0x001C1FA0
	public void OnCardBackModifications(CardBackModifications packet)
	{
		NetCache.NetCacheCardBacks netObject = this.GetNetObject<NetCache.NetCacheCardBacks>();
		if (netObject == null)
		{
			Debug.LogWarning(string.Format("NetCache.OnCardBackModifications(): trying to access NetCacheCardBacks before it's been loaded", Array.Empty<object>()));
			return;
		}
		foreach (CardBackModification cardBackModification in packet.CardBackModifications_)
		{
			netObject.CardBacks.Add(cardBackModification.AssetCardBackId);
			if (cardBackModification.HasAutoSetAsFavorite && cardBackModification.AutoSetAsFavorite)
			{
				this.ProcessNewFavoriteCardBack(cardBackModification.AssetCardBackId);
			}
		}
	}

	// Token: 0x0600563D RID: 22077 RVA: 0x001C3E3C File Offset: 0x001C203C
	private void OnSetFavoriteCardBackResponse()
	{
		Network.CardBackResponse cardBackResponse = Network.Get().GetCardBackResponse();
		if (!cardBackResponse.Success)
		{
			global::Log.CardbackMgr.PrintError("SetFavoriteCardBack FAILED (cardBack = {0})", new object[]
			{
				cardBackResponse.CardBack
			});
			return;
		}
		this.ProcessNewFavoriteCardBack(cardBackResponse.CardBack);
	}

	// Token: 0x0600563E RID: 22078 RVA: 0x001C3E8C File Offset: 0x001C208C
	public void ProcessNewFavoriteCardBack(int newFavoriteCardBackID)
	{
		NetCache.NetCacheCardBacks netObject = this.GetNetObject<NetCache.NetCacheCardBacks>();
		if (netObject == null)
		{
			Debug.LogWarning(string.Format("NetCache.ProcessNewFavoriteCardBack(): trying to access NetCacheCardBacks before it's been loaded", Array.Empty<object>()));
			return;
		}
		if (netObject.FavoriteCardBack != newFavoriteCardBackID)
		{
			netObject.FavoriteCardBack = newFavoriteCardBackID;
			if (this.FavoriteCardBackChanged != null)
			{
				this.FavoriteCardBackChanged(newFavoriteCardBackID);
			}
		}
	}

	// Token: 0x0600563F RID: 22079 RVA: 0x001C3EDC File Offset: 0x001C20DC
	private void OnSetFavoriteCoinResponse()
	{
		Network.CoinResponse coinResponse = Network.Get().GetCoinResponse();
		if (!coinResponse.Success)
		{
			global::Log.Net.PrintError("SetFavoriteCardBack FAILED (coin = {0})", new object[]
			{
				coinResponse.Coin
			});
			return;
		}
		this.ProcessNewFavoriteCoin(coinResponse.Coin);
	}

	// Token: 0x06005640 RID: 22080 RVA: 0x001C3F2C File Offset: 0x001C212C
	public void ProcessNewFavoriteCoin(int newFavoriteCoinID)
	{
		NetCache.NetCacheCoins netObject = this.GetNetObject<NetCache.NetCacheCoins>();
		if (netObject == null)
		{
			Debug.LogWarning(string.Format("NetCache.ProcessNewFavoriteCoin(): trying to accessNetCacheCoins before it's been loaded", Array.Empty<object>()));
			return;
		}
		if (netObject.FavoriteCoin != newFavoriteCoinID)
		{
			netObject.FavoriteCoin = newFavoriteCoinID;
			if (this.FavoriteCoinChanged != null)
			{
				this.FavoriteCoinChanged(newFavoriteCoinID);
			}
		}
	}

	// Token: 0x06005641 RID: 22081 RVA: 0x001C3F7C File Offset: 0x001C217C
	private void OnGamesInfo()
	{
		NetCache.NetCacheGamesPlayed gamesInfo = Network.Get().GetGamesInfo();
		if (gamesInfo == null)
		{
			Debug.LogWarning("error getting games info");
			return;
		}
		this.OnNetCacheObjReceived<NetCache.NetCacheGamesPlayed>(gamesInfo);
	}

	// Token: 0x06005642 RID: 22082 RVA: 0x001C3FA9 File Offset: 0x001C21A9
	private void OnProfileProgress()
	{
		this.OnNetCacheObjReceived<NetCache.NetCacheProfileProgress>(Network.Get().GetProfileProgress());
	}

	// Token: 0x06005643 RID: 22083 RVA: 0x001C3FBB File Offset: 0x001C21BB
	private void OnHearthstoneUnavailableGame()
	{
		this.OnHearthstoneUnavailable(true);
	}

	// Token: 0x06005644 RID: 22084 RVA: 0x001C3FC4 File Offset: 0x001C21C4
	private void OnHearthstoneUnavailableUtil()
	{
		this.OnHearthstoneUnavailable(false);
	}

	// Token: 0x06005645 RID: 22085 RVA: 0x001C3FD0 File Offset: 0x001C21D0
	private void OnHearthstoneUnavailable(bool gamePacket)
	{
		Network.UnavailableReason hearthstoneUnavailable = Network.Get().GetHearthstoneUnavailable(gamePacket);
		Debug.Log("Hearthstone Unavailable!  Reason: " + hearthstoneUnavailable.mainReason);
		string mainReason = hearthstoneUnavailable.mainReason;
		if (mainReason == "VERSION")
		{
			ErrorParams errorParams = new ErrorParams();
			if (PlatformSettings.IsMobile() && GameDownloadManagerProvider.Get() != null && !GameDownloadManagerProvider.Get().IsNewMobileVersionReleased)
			{
				errorParams.m_message = GameStrings.Format("GLOBAL_ERROR_NETWORK_UNAVAILABLE_NEW_VERSION", Array.Empty<object>());
				errorParams.m_reason = FatalErrorReason.UNAVAILABLE_NEW_VERSION;
			}
			else
			{
				errorParams.m_message = GameStrings.Format("GLOBAL_ERROR_NETWORK_UNAVAILABLE_UPGRADE", Array.Empty<object>());
				if (global::Error.HAS_APP_STORE)
				{
					errorParams.m_redirectToStore = true;
				}
				errorParams.m_reason = FatalErrorReason.UNAVAILABLE_UPGRADE;
			}
			global::Error.AddFatal(errorParams);
			ReconnectMgr.Get().FullResetRequired = true;
			ReconnectMgr.Get().UpdateRequired = true;
			return;
		}
		if (!(mainReason == "OFFLINE"))
		{
			TelemetryManager.Client().SendNetworkError(NetworkError.ErrorType.SERVICE_UNAVAILABLE, string.Format("{0} - {1} - {2}", hearthstoneUnavailable.mainReason, hearthstoneUnavailable.subReason, hearthstoneUnavailable.extraData), 0);
			Network.Get().ShowConnectionFailureError("GLOBAL_ERROR_NETWORK_UNAVAILABLE_UNKNOWN");
			return;
		}
		Network.Get().ShowConnectionFailureError("GLOBAL_ERROR_NETWORK_UNAVAILABLE_OFFLINE");
	}

	// Token: 0x06005646 RID: 22086 RVA: 0x001C40F8 File Offset: 0x001C22F8
	private void OnCardBacks()
	{
		this.OnNetCacheObjReceived<NetCache.NetCacheCardBacks>(Network.Get().GetCardBacks());
		CardBacks cardBacksPacket = Network.Get().GetCardBacksPacket();
		if (cardBacksPacket == null)
		{
			return;
		}
		SetFavoriteCardBack setFavoriteCardBack = OfflineDataCache.GenerateSetFavoriteCardBackFromDiff(cardBacksPacket.FavoriteCardBack);
		if (setFavoriteCardBack != null)
		{
			Network.Get().SetFavoriteCardBack(setFavoriteCardBack.CardBack);
		}
		OfflineDataCache.ClearCardBackDirtyFlag();
		OfflineDataCache.CacheCardBacks(cardBacksPacket);
	}

	// Token: 0x06005647 RID: 22087 RVA: 0x001C4150 File Offset: 0x001C2350
	private void OnCoins()
	{
		this.OnNetCacheObjReceived<NetCache.NetCacheCoins>(Network.Get().GetCoins());
		Coins coinsPacket = Network.Get().GetCoinsPacket();
		if (coinsPacket == null)
		{
			return;
		}
		SetFavoriteCoin setFavoriteCoin = OfflineDataCache.GenerateSetFavoriteCoinFromDiff(coinsPacket.FavoriteCoin);
		if (setFavoriteCoin != null)
		{
			Network.Get().SetFavoriteCoin(setFavoriteCoin.Coin);
		}
		OfflineDataCache.ClearCoinDirtyFlag();
		OfflineDataCache.CacheCoins(coinsPacket);
	}

	// Token: 0x06005648 RID: 22088 RVA: 0x001C41A8 File Offset: 0x001C23A8
	private void OnPlayerRecords()
	{
		PlayerRecords playerRecordsPacket = Network.Get().GetPlayerRecordsPacket();
		this.OnPlayerRecordsPacket(playerRecordsPacket);
	}

	// Token: 0x06005649 RID: 22089 RVA: 0x001C41C7 File Offset: 0x001C23C7
	public void OnPlayerRecordsPacket(PlayerRecords packet)
	{
		this.OnNetCacheObjReceived<NetCache.NetCachePlayerRecords>(Network.GetPlayerRecords(packet));
	}

	// Token: 0x0600564A RID: 22090 RVA: 0x001C41D5 File Offset: 0x001C23D5
	private void OnRewardProgress()
	{
		this.OnNetCacheObjReceived<NetCache.NetCacheRewardProgress>(Network.Get().GetRewardProgress());
	}

	// Token: 0x0600564B RID: 22091 RVA: 0x001C41E8 File Offset: 0x001C23E8
	private NetCache.NetCacheHeroLevels GetAllHeroXP(HeroXP packet)
	{
		if (packet == null)
		{
			return new NetCache.NetCacheHeroLevels();
		}
		NetCache.NetCacheHeroLevels netCacheHeroLevels = new NetCache.NetCacheHeroLevels();
		for (int i = 0; i < packet.XpInfos.Count; i++)
		{
			HeroXPInfo heroXPInfo = packet.XpInfos[i];
			NetCache.HeroLevel heroLevel = new NetCache.HeroLevel();
			heroLevel.Class = (TAG_CLASS)heroXPInfo.ClassId;
			heroLevel.CurrentLevel.Level = heroXPInfo.Level;
			heroLevel.CurrentLevel.XP = heroXPInfo.CurrXp;
			heroLevel.CurrentLevel.MaxXP = heroXPInfo.MaxXp;
			netCacheHeroLevels.Levels.Add(heroLevel);
		}
		return netCacheHeroLevels;
	}

	// Token: 0x0600564C RID: 22092 RVA: 0x001C427C File Offset: 0x001C247C
	public void OnHeroXP(HeroXP packet)
	{
		NetCache.NetCacheHeroLevels allHeroXP = this.GetAllHeroXP(packet);
		if (this.m_prevHeroLevels != null)
		{
			using (List<NetCache.HeroLevel>.Enumerator enumerator = allHeroXP.Levels.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NetCache.HeroLevel newHeroLevel = enumerator.Current;
					NetCache.HeroLevel heroLevel = this.m_prevHeroLevels.Levels.Find((NetCache.HeroLevel obj) => obj.Class == newHeroLevel.Class);
					if (heroLevel != null)
					{
						if (newHeroLevel != null && newHeroLevel.CurrentLevel != null && newHeroLevel.CurrentLevel.Level != heroLevel.CurrentLevel.Level && (newHeroLevel.CurrentLevel.Level == 20 || newHeroLevel.CurrentLevel.Level == 30 || newHeroLevel.CurrentLevel.Level == 40 || newHeroLevel.CurrentLevel.Level == 50 || newHeroLevel.CurrentLevel.Level == 60))
						{
							if (newHeroLevel.Class == TAG_CLASS.DRUID)
							{
								BnetPresenceMgr.Get().SetGameField(5U, newHeroLevel.CurrentLevel.Level);
							}
							else if (newHeroLevel.Class == TAG_CLASS.HUNTER)
							{
								BnetPresenceMgr.Get().SetGameField(6U, newHeroLevel.CurrentLevel.Level);
							}
							else if (newHeroLevel.Class == TAG_CLASS.MAGE)
							{
								BnetPresenceMgr.Get().SetGameField(7U, newHeroLevel.CurrentLevel.Level);
							}
							else if (newHeroLevel.Class == TAG_CLASS.PALADIN)
							{
								BnetPresenceMgr.Get().SetGameField(8U, newHeroLevel.CurrentLevel.Level);
							}
							else if (newHeroLevel.Class == TAG_CLASS.PRIEST)
							{
								BnetPresenceMgr.Get().SetGameField(9U, newHeroLevel.CurrentLevel.Level);
							}
							else if (newHeroLevel.Class == TAG_CLASS.ROGUE)
							{
								BnetPresenceMgr.Get().SetGameField(10U, newHeroLevel.CurrentLevel.Level);
							}
							else if (newHeroLevel.Class == TAG_CLASS.SHAMAN)
							{
								BnetPresenceMgr.Get().SetGameField(11U, newHeroLevel.CurrentLevel.Level);
							}
							else if (newHeroLevel.Class == TAG_CLASS.WARLOCK)
							{
								BnetPresenceMgr.Get().SetGameField(12U, newHeroLevel.CurrentLevel.Level);
							}
							else if (newHeroLevel.Class == TAG_CLASS.WARRIOR)
							{
								BnetPresenceMgr.Get().SetGameField(13U, newHeroLevel.CurrentLevel.Level);
							}
						}
						newHeroLevel.PrevLevel = heroLevel.CurrentLevel;
					}
				}
			}
		}
		this.m_prevHeroLevels = allHeroXP;
		this.OnNetCacheObjReceived<NetCache.NetCacheHeroLevels>(allHeroXP);
	}

	// Token: 0x0600564D RID: 22093 RVA: 0x001C4580 File Offset: 0x001C2780
	private void OnAllHeroXP()
	{
		HeroXP heroXP = Network.Get().GetHeroXP();
		this.OnHeroXP(heroXP);
	}

	// Token: 0x0600564E RID: 22094 RVA: 0x001C45A0 File Offset: 0x001C27A0
	private void OnInitialClientState_ProfileNotices(ProfileNotices profileNotices)
	{
		List<NetCache.ProfileNotice> receivedNotices = new List<NetCache.ProfileNotice>();
		Network.Get().HandleProfileNotices(profileNotices.List, ref receivedNotices);
		this.m_receivedInitialProfileNotices = true;
		this.HandleIncomingProfileNotices(receivedNotices, true);
		this.HandleIncomingProfileNotices(this.m_queuedProfileNotices, true);
		this.m_queuedProfileNotices.Clear();
	}

	// Token: 0x0600564F RID: 22095 RVA: 0x001C45EC File Offset: 0x001C27EC
	public void HandleIncomingProfileNotices(List<NetCache.ProfileNotice> receivedNotices, bool isInitialNoticeList)
	{
		if (!this.m_receivedInitialProfileNotices)
		{
			this.m_queuedProfileNotices.AddRange(receivedNotices);
			return;
		}
		if (receivedNotices.Find((NetCache.ProfileNotice obj) => obj.Type == NetCache.ProfileNotice.NoticeType.GAINED_MEDAL) != null)
		{
			this.m_previousMedalInfo = null;
			NetCache.NetCacheMedalInfo netObject = this.GetNetObject<NetCache.NetCacheMedalInfo>();
			if (netObject != null)
			{
				netObject.PreviousMedalInfo = null;
			}
		}
		List<NetCache.ProfileNotice> list = this.FindNewNotices(receivedNotices);
		NetCache.NetCacheProfileNotices netCacheProfileNotices = this.GetNetObject<NetCache.NetCacheProfileNotices>();
		if (netCacheProfileNotices == null)
		{
			netCacheProfileNotices = new NetCache.NetCacheProfileNotices();
		}
		HashSet<long> hashSet = new HashSet<long>();
		for (int i = 0; i < netCacheProfileNotices.Notices.Count; i++)
		{
			hashSet.Add(netCacheProfileNotices.Notices[i].NoticeID);
		}
		for (int j = 0; j < receivedNotices.Count; j++)
		{
			if (!this.m_ackedNotices.Contains(receivedNotices[j].NoticeID) && !hashSet.Contains(receivedNotices[j].NoticeID))
			{
				netCacheProfileNotices.Notices.Add(receivedNotices[j]);
			}
		}
		this.OnNetCacheObjReceived<NetCache.NetCacheProfileNotices>(netCacheProfileNotices);
		NetCache.DelNewNoticesListener[] array = this.m_newNoticesListeners.ToArray();
		foreach (NetCache.ProfileNotice profileNotice in list)
		{
			global::Log.Achievements.Print("NetCache.OnProfileNotices() sending {0} to {1} listeners", new object[]
			{
				profileNotice,
				array.Length
			});
		}
		foreach (NetCache.DelNewNoticesListener delNewNoticesListener in array)
		{
			global::Log.Achievements.Print("NetCache.OnProfileNotices(): sending notices to {0}::{1}", new object[]
			{
				delNewNoticesListener.Method.ReflectedType.Name,
				delNewNoticesListener.Method.Name
			});
			delNewNoticesListener(list, isInitialNoticeList);
		}
	}

	// Token: 0x06005650 RID: 22096 RVA: 0x001C47CC File Offset: 0x001C29CC
	private List<NetCache.ProfileNotice> FindNewNotices(List<NetCache.ProfileNotice> receivedNotices)
	{
		List<NetCache.ProfileNotice> list = new List<NetCache.ProfileNotice>();
		NetCache.NetCacheProfileNotices netObject = this.GetNetObject<NetCache.NetCacheProfileNotices>();
		if (netObject == null)
		{
			list.AddRange(receivedNotices);
		}
		else
		{
			using (List<NetCache.ProfileNotice>.Enumerator enumerator = receivedNotices.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NetCache.ProfileNotice receivedNotice = enumerator.Current;
					if (netObject.Notices.Find((NetCache.ProfileNotice obj) => obj.NoticeID == receivedNotice.NoticeID) == null)
					{
						list.Add(receivedNotice);
					}
				}
			}
		}
		return list;
	}

	// Token: 0x06005651 RID: 22097 RVA: 0x001C4860 File Offset: 0x001C2A60
	public void OnClientOptions(ClientOptions packet)
	{
		NetCache.NetCacheClientOptions netCacheClientOptions = this.GetNetObject<NetCache.NetCacheClientOptions>();
		bool flag = netCacheClientOptions == null;
		if (flag)
		{
			netCacheClientOptions = new NetCache.NetCacheClientOptions();
		}
		if (packet.HasFailed && packet.Failed)
		{
			Debug.LogError("ReadClientOptions: packet.Failed=true. Unable to retrieve client options from UtilServer.");
			Network.Get().ShowConnectionFailureError("GLOBAL_ERROR_NETWORK_GENERIC");
			return;
		}
		foreach (PegasusUtil.ClientOption clientOption in packet.Options)
		{
			ServerOption index = (ServerOption)clientOption.Index;
			if (clientOption.HasAsInt32)
			{
				netCacheClientOptions.ClientState[index] = new NetCache.ClientOptionInt(clientOption.AsInt32);
			}
			else if (clientOption.HasAsInt64)
			{
				netCacheClientOptions.ClientState[index] = new NetCache.ClientOptionLong(clientOption.AsInt64);
			}
			else if (clientOption.HasAsFloat)
			{
				netCacheClientOptions.ClientState[index] = new NetCache.ClientOptionFloat(clientOption.AsFloat);
			}
			else if (clientOption.HasAsUint64)
			{
				netCacheClientOptions.ClientState[index] = new NetCache.ClientOptionULong(clientOption.AsUint64);
			}
		}
		netCacheClientOptions.UpdateServerState();
		this.OnNetCacheObjReceived<NetCache.NetCacheClientOptions>(netCacheClientOptions);
		if (flag)
		{
			OptionsMigration.UpgradeServerOptions();
		}
		netCacheClientOptions.RemoveInvalidOptions();
	}

	// Token: 0x06005652 RID: 22098 RVA: 0x001C499C File Offset: 0x001C2B9C
	private void SetClientOption(ServerOption type, NetCache.ClientOptionBase newVal)
	{
		Type typeFromHandle = typeof(NetCache.NetCacheClientOptions);
		object obj;
		if (!this.m_netCache.TryGetValue(typeFromHandle, out obj) || !(obj is NetCache.NetCacheClientOptions))
		{
			Debug.LogWarning("NetCache.OnClientOptions: Attempting to set an option before initializing the options cache.");
			return;
		}
		NetCache.NetCacheClientOptions netCacheClientOptions = (NetCache.NetCacheClientOptions)obj;
		netCacheClientOptions.ClientState[type] = newVal;
		netCacheClientOptions.CheckForDispatchToServer();
		this.NetCacheChanged<NetCache.NetCacheClientOptions>();
	}

	// Token: 0x06005653 RID: 22099 RVA: 0x001C49F5 File Offset: 0x001C2BF5
	public void SetIntOption(ServerOption type, int val)
	{
		this.SetClientOption(type, new NetCache.ClientOptionInt(val));
	}

	// Token: 0x06005654 RID: 22100 RVA: 0x001C4A04 File Offset: 0x001C2C04
	public void SetLongOption(ServerOption type, long val)
	{
		this.SetClientOption(type, new NetCache.ClientOptionLong(val));
	}

	// Token: 0x06005655 RID: 22101 RVA: 0x001C4A13 File Offset: 0x001C2C13
	public void SetFloatOption(ServerOption type, float val)
	{
		this.SetClientOption(type, new NetCache.ClientOptionFloat(val));
	}

	// Token: 0x06005656 RID: 22102 RVA: 0x001C4A22 File Offset: 0x001C2C22
	public void SetULongOption(ServerOption type, ulong val)
	{
		this.SetClientOption(type, new NetCache.ClientOptionULong(val));
	}

	// Token: 0x06005657 RID: 22103 RVA: 0x001C4A31 File Offset: 0x001C2C31
	public void DeleteClientOption(ServerOption type)
	{
		this.SetClientOption(type, null);
	}

	// Token: 0x06005658 RID: 22104 RVA: 0x001C4A3C File Offset: 0x001C2C3C
	public bool ClientOptionExists(ServerOption type)
	{
		NetCache.NetCacheClientOptions netObject = this.GetNetObject<NetCache.NetCacheClientOptions>();
		return netObject != null && netObject.ClientState.ContainsKey(type) && netObject.ClientState[type] != null;
	}

	// Token: 0x06005659 RID: 22105 RVA: 0x001C4A74 File Offset: 0x001C2C74
	public void DispatchClientOptionsToServer()
	{
		NetCache.NetCacheClientOptions netObject = NetCache.Get().GetNetObject<NetCache.NetCacheClientOptions>();
		if (netObject != null)
		{
			netObject.DispatchClientOptionsToServer();
		}
	}

	// Token: 0x0600565A RID: 22106 RVA: 0x001C4A98 File Offset: 0x001C2C98
	private void OnFavoriteHeroesResponse()
	{
		FavoriteHeroesResponse favoriteHeroesResponse = Network.Get().GetFavoriteHeroesResponse();
		NetCache.NetCacheFavoriteHeroes netCacheFavoriteHeroes = new NetCache.NetCacheFavoriteHeroes();
		foreach (FavoriteHero favoriteHero in favoriteHeroesResponse.FavoriteHeroes)
		{
			TAG_CLASS tag_CLASS;
			TAG_PREMIUM premium;
			if (!global::EnumUtils.TryCast<TAG_CLASS>(favoriteHero.ClassId, out tag_CLASS))
			{
				Debug.LogWarning(string.Format("NetCache.OnFavoriteHeroesResponse() unrecognized hero class {0}", favoriteHero.ClassId));
			}
			else if (!global::EnumUtils.TryCast<TAG_PREMIUM>(favoriteHero.Hero.Premium, out premium))
			{
				Debug.LogWarning(string.Format("NetCache.OnFavoriteHeroesResponse() unrecognized hero premium {0} for hero class {1}", favoriteHero.Hero.Premium, tag_CLASS));
			}
			else
			{
				NetCache.CardDefinition value = new NetCache.CardDefinition
				{
					Name = GameUtils.TranslateDbIdToCardId(favoriteHero.Hero.Asset, false),
					Premium = premium
				};
				netCacheFavoriteHeroes.FavoriteHeroes[tag_CLASS] = value;
			}
		}
		List<SetFavoriteHero> list = OfflineDataCache.GenerateSetFavoriteHeroFromDiff(netCacheFavoriteHeroes);
		if (list.Any<SetFavoriteHero>())
		{
			foreach (SetFavoriteHero setFavoriteHero in list)
			{
				NetCache.CardDefinition hero = new NetCache.CardDefinition
				{
					Name = GameUtils.TranslateDbIdToCardId(setFavoriteHero.FavoriteHero.Hero.Asset, false),
					Premium = (TAG_PREMIUM)setFavoriteHero.FavoriteHero.Hero.Premium
				};
				Network.Get().SetFavoriteHero((TAG_CLASS)setFavoriteHero.FavoriteHero.ClassId, hero);
			}
			OfflineDataCache.ClearFavoriteHeroesDirtyFlag();
		}
		this.OnNetCacheObjReceived<NetCache.NetCacheFavoriteHeroes>(netCacheFavoriteHeroes);
		OfflineDataCache.CacheFavoriteHeroes(favoriteHeroesResponse);
	}

	// Token: 0x0600565B RID: 22107 RVA: 0x001C4C58 File Offset: 0x001C2E58
	private void OnSetFavoriteHeroResponse()
	{
		Network.SetFavoriteHeroResponse setFavoriteHeroResponse = Network.Get().GetSetFavoriteHeroResponse();
		if (!setFavoriteHeroResponse.Success)
		{
			return;
		}
		if (TAG_CLASS.NEUTRAL == setFavoriteHeroResponse.HeroClass || setFavoriteHeroResponse.Hero == null)
		{
			Debug.LogWarning(string.Format("NetCache.OnSetFavoriteHeroResponse: setting hero was a success, but message contains invalid class ({0}) and/or hero ({1})", setFavoriteHeroResponse.HeroClass, setFavoriteHeroResponse.Hero));
			return;
		}
		NetCache.NetCacheFavoriteHeroes netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFavoriteHeroes>();
		if (netObject != null)
		{
			netObject.FavoriteHeroes[setFavoriteHeroResponse.HeroClass] = setFavoriteHeroResponse.Hero;
			Debug.LogWarning(string.Format("NetCache.OnSetFavoriteHeroResponse: favorite hero for class {0} updated to {1}", setFavoriteHeroResponse.HeroClass, setFavoriteHeroResponse.Hero));
		}
		PegasusShared.CardDef cardDef = new PegasusShared.CardDef
		{
			Asset = GameUtils.TranslateCardIdToDbId(setFavoriteHeroResponse.Hero.Name, false),
			Premium = (int)setFavoriteHeroResponse.Hero.Premium
		};
		OfflineDataCache.SetFavoriteHero((int)setFavoriteHeroResponse.HeroClass, cardDef, false);
	}

	// Token: 0x0600565C RID: 22108 RVA: 0x001C4D2C File Offset: 0x001C2F2C
	private void OnAccountLicensesInfoResponse()
	{
		AccountLicensesInfoResponse accountLicensesInfoResponse = Network.Get().GetAccountLicensesInfoResponse();
		NetCache.NetCacheAccountLicenses netCacheAccountLicenses = new NetCache.NetCacheAccountLicenses();
		foreach (AccountLicenseInfo accountLicenseInfo in accountLicensesInfoResponse.List)
		{
			netCacheAccountLicenses.AccountLicenses[accountLicenseInfo.License] = accountLicenseInfo;
		}
		this.OnNetCacheObjReceived<NetCache.NetCacheAccountLicenses>(netCacheAccountLicenses);
	}

	// Token: 0x0600565D RID: 22109 RVA: 0x001C4DA0 File Offset: 0x001C2FA0
	private void OnClientStaticAssetsResponse()
	{
		ClientStaticAssetsResponse clientStaticAssetsResponse = Network.Get().GetClientStaticAssetsResponse();
		if (clientStaticAssetsResponse == null)
		{
			return;
		}
		this.OnNetCacheObjReceived<ClientStaticAssetsResponse>(clientStaticAssetsResponse);
	}

	// Token: 0x0600565E RID: 22110 RVA: 0x001C4DC4 File Offset: 0x001C2FC4
	private void OnFSGFeatureConfig()
	{
		FSGFeatureConfig fsgfeatureConfig = Network.Get().GetFSGFeatureConfig();
		if (fsgfeatureConfig == null)
		{
			return;
		}
		this.OnNetCacheObjReceived<FSGFeatureConfig>(fsgfeatureConfig);
	}

	// Token: 0x0600565F RID: 22111 RVA: 0x001C4DE8 File Offset: 0x001C2FE8
	private void RegisterNetCacheHandlers()
	{
		Network network = Network.Get();
		network.RegisterNetHandler(DBAction.PacketID.ID, new Network.NetHandler(this.OnDBAction), null);
		network.RegisterNetHandler(GenericResponse.PacketID.ID, new Network.NetHandler(this.OnGenericResponse), null);
		network.RegisterNetHandler(InitialClientState.PacketID.ID, new Network.NetHandler(this.OnInitialClientState), null);
		network.RegisterNetHandler(MedalInfo.PacketID.ID, new Network.NetHandler(this.OnMedalInfo), null);
		network.RegisterNetHandler(BattlegroundsRatingInfoResponse.PacketID.ID, new Network.NetHandler(this.OnBaconRatingInfo), null);
		network.RegisterNetHandler(ProfileProgress.PacketID.ID, new Network.NetHandler(this.OnProfileProgress), null);
		network.RegisterNetHandler(GamesInfo.PacketID.ID, new Network.NetHandler(this.OnGamesInfo), null);
		network.RegisterNetHandler(CardValues.PacketID.ID, new Network.NetHandler(this.OnCardValues), null);
		network.RegisterNetHandler(GuardianVars.PacketID.ID, new Network.NetHandler(this.OnGuardianVars), null);
		network.RegisterNetHandler(PlayerRecords.PacketID.ID, new Network.NetHandler(this.OnPlayerRecords), null);
		network.RegisterNetHandler(RewardProgress.PacketID.ID, new Network.NetHandler(this.OnRewardProgress), null);
		network.RegisterNetHandler(HeroXP.PacketID.ID, new Network.NetHandler(this.OnAllHeroXP), null);
		network.RegisterNetHandler(CardBacks.PacketID.ID, new Network.NetHandler(this.OnCardBacks), null);
		network.RegisterNetHandler(SetFavoriteCardBackResponse.PacketID.ID, new Network.NetHandler(this.OnSetFavoriteCardBackResponse), null);
		network.RegisterNetHandler(FavoriteHeroesResponse.PacketID.ID, new Network.NetHandler(this.OnFavoriteHeroesResponse), null);
		network.RegisterNetHandler(SetFavoriteHeroResponse.PacketID.ID, new Network.NetHandler(this.OnSetFavoriteHeroResponse), null);
		network.RegisterNetHandler(AccountLicensesInfoResponse.PacketID.ID, new Network.NetHandler(this.OnAccountLicensesInfoResponse), null);
		network.RegisterNetHandler(DeckList.PacketID.ID, new Network.NetHandler(this.OnReceivedDeckHeaders), null);
		network.RegisterNetHandler(BattlegroundsPremiumStatusResponse.PacketID.ID, new Network.NetHandler(this.OnBaconPremiumStatus), null);
		network.RegisterNetHandler(PVPDRStatsInfoResponse.PacketID.ID, new Network.NetHandler(this.OnPVPDRStatsInfo), null);
		network.RegisterNetHandler(Coins.PacketID.ID, new Network.NetHandler(this.OnCoins), null);
		network.RegisterNetHandler(SetFavoriteCoinResponse.PacketID.ID, new Network.NetHandler(this.OnSetFavoriteCoinResponse), null);
		network.RegisterNetHandler(Deadend.PacketID.ID, new Network.NetHandler(this.OnHearthstoneUnavailableGame), null);
		network.RegisterNetHandler(DeadendUtil.PacketID.ID, new Network.NetHandler(this.OnHearthstoneUnavailableUtil), null);
		network.RegisterNetHandler(ClientStaticAssetsResponse.PacketID.ID, new Network.NetHandler(this.OnClientStaticAssetsResponse), null);
		network.RegisterNetHandler(FSGFeatureConfig.PacketID.ID, new Network.NetHandler(this.OnFSGFeatureConfig), null);
	}

	// Token: 0x06005660 RID: 22112 RVA: 0x001C5105 File Offset: 0x001C3305
	public void RegisterCollectionManager(NetCache.NetCacheCallback callback)
	{
		this.RegisterCollectionManager(callback, new NetCache.ErrorCallback(NetCache.DefaultErrorHandler));
	}

	// Token: 0x06005661 RID: 22113 RVA: 0x001C511C File Offset: 0x001C331C
	public void RegisterCollectionManager(NetCache.NetCacheCallback callback, NetCache.ErrorCallback errorCallback)
	{
		NetCache.NetCacheBatchRequest batchRequest = new NetCache.NetCacheBatchRequest(callback, errorCallback, new NetCache.RequestFunc(this.RegisterCollectionManager));
		this.AddCollectionManagerToRequest(ref batchRequest);
		this.NetCacheMakeBatchRequest(batchRequest);
	}

	// Token: 0x06005662 RID: 22114 RVA: 0x001C514C File Offset: 0x001C334C
	public void RegisterScreenCollectionManager(NetCache.NetCacheCallback callback)
	{
		this.RegisterScreenCollectionManager(callback, new NetCache.ErrorCallback(NetCache.DefaultErrorHandler));
	}

	// Token: 0x06005663 RID: 22115 RVA: 0x001C5164 File Offset: 0x001C3364
	public void RegisterScreenCollectionManager(NetCache.NetCacheCallback callback, NetCache.ErrorCallback errorCallback)
	{
		NetCache.NetCacheBatchRequest netCacheBatchRequest = new NetCache.NetCacheBatchRequest(callback, errorCallback, new NetCache.RequestFunc(this.RegisterScreenCollectionManager));
		this.AddCollectionManagerToRequest(ref netCacheBatchRequest);
		netCacheBatchRequest.AddRequests(new List<NetCache.Request>
		{
			new NetCache.Request(typeof(NetCache.NetCacheCollection), false),
			new NetCache.Request(typeof(NetCache.NetCacheFeatures), false),
			new NetCache.Request(typeof(NetCache.NetCacheHeroLevels), false)
		});
		this.NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	// Token: 0x06005664 RID: 22116 RVA: 0x001C51E1 File Offset: 0x001C33E1
	public void RegisterScreenForge(NetCache.NetCacheCallback callback)
	{
		this.RegisterScreenForge(callback, new NetCache.ErrorCallback(NetCache.DefaultErrorHandler));
	}

	// Token: 0x06005665 RID: 22117 RVA: 0x001C51F8 File Offset: 0x001C33F8
	public void RegisterScreenForge(NetCache.NetCacheCallback callback, NetCache.ErrorCallback errorCallback)
	{
		NetCache.NetCacheBatchRequest netCacheBatchRequest = new NetCache.NetCacheBatchRequest(callback, errorCallback, new NetCache.RequestFunc(this.RegisterScreenForge));
		this.AddCollectionManagerToRequest(ref netCacheBatchRequest);
		netCacheBatchRequest.AddRequests(new List<NetCache.Request>
		{
			new NetCache.Request(typeof(NetCache.NetCacheFeatures), false),
			new NetCache.Request(typeof(NetCache.NetCacheHeroLevels), false)
		});
		this.NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	// Token: 0x06005666 RID: 22118 RVA: 0x001C525F File Offset: 0x001C345F
	public void RegisterScreenTourneys(NetCache.NetCacheCallback callback)
	{
		this.RegisterScreenTourneys(callback, new NetCache.ErrorCallback(NetCache.DefaultErrorHandler));
	}

	// Token: 0x06005667 RID: 22119 RVA: 0x001C5274 File Offset: 0x001C3474
	public void RegisterScreenTourneys(NetCache.NetCacheCallback callback, NetCache.ErrorCallback errorCallback)
	{
		NetCache.NetCacheBatchRequest netCacheBatchRequest = new NetCache.NetCacheBatchRequest(callback, errorCallback, new NetCache.RequestFunc(this.RegisterScreenTourneys));
		netCacheBatchRequest.AddRequests(new List<NetCache.Request>
		{
			new NetCache.Request(typeof(NetCache.NetCachePlayerRecords), false),
			new NetCache.Request(typeof(NetCache.NetCacheDecks), false),
			new NetCache.Request(typeof(NetCache.NetCacheFeatures), false),
			new NetCache.Request(typeof(NetCache.NetCacheHeroLevels), false)
		});
		this.NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	// Token: 0x06005668 RID: 22120 RVA: 0x001C52FF File Offset: 0x001C34FF
	public void RegisterScreenFriendly(NetCache.NetCacheCallback callback)
	{
		this.RegisterScreenFriendly(callback, new NetCache.ErrorCallback(NetCache.DefaultErrorHandler));
	}

	// Token: 0x06005669 RID: 22121 RVA: 0x001C5314 File Offset: 0x001C3514
	public void RegisterScreenFriendly(NetCache.NetCacheCallback callback, NetCache.ErrorCallback errorCallback)
	{
		NetCache.NetCacheBatchRequest netCacheBatchRequest = new NetCache.NetCacheBatchRequest(callback, errorCallback, new NetCache.RequestFunc(this.RegisterScreenFriendly));
		netCacheBatchRequest.AddRequests(new List<NetCache.Request>
		{
			new NetCache.Request(typeof(NetCache.NetCacheDecks), false),
			new NetCache.Request(typeof(NetCache.NetCacheHeroLevels), false)
		});
		this.NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	// Token: 0x0600566A RID: 22122 RVA: 0x001C5373 File Offset: 0x001C3573
	public void RegisterScreenPractice(NetCache.NetCacheCallback callback)
	{
		this.RegisterScreenPractice(callback, new NetCache.ErrorCallback(NetCache.DefaultErrorHandler));
	}

	// Token: 0x0600566B RID: 22123 RVA: 0x001C5388 File Offset: 0x001C3588
	public void RegisterScreenPractice(NetCache.NetCacheCallback callback, NetCache.ErrorCallback errorCallback)
	{
		NetCache.NetCacheBatchRequest netCacheBatchRequest = new NetCache.NetCacheBatchRequest(callback, errorCallback, new NetCache.RequestFunc(this.RegisterScreenPractice));
		netCacheBatchRequest.AddRequests(new List<NetCache.Request>
		{
			new NetCache.Request(typeof(NetCache.NetCacheDecks), false),
			new NetCache.Request(typeof(NetCache.NetCacheFeatures), false),
			new NetCache.Request(typeof(NetCache.NetCacheHeroLevels), false),
			new NetCache.Request(typeof(NetCache.NetCacheRewardProgress), false)
		});
		this.NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	// Token: 0x0600566C RID: 22124 RVA: 0x001C5413 File Offset: 0x001C3613
	public void RegisterScreenEndOfGame(NetCache.NetCacheCallback callback)
	{
		this.RegisterScreenEndOfGame(callback, new NetCache.ErrorCallback(NetCache.DefaultErrorHandler));
	}

	// Token: 0x0600566D RID: 22125 RVA: 0x001C5428 File Offset: 0x001C3628
	public void RegisterScreenEndOfGame(NetCache.NetCacheCallback callback, NetCache.ErrorCallback errorCallback)
	{
		GameMgr gameMgr;
		if (HearthstoneServices.TryGet<GameMgr>(out gameMgr) && gameMgr.IsSpectator())
		{
			Processor.ScheduleCallback(0f, false, delegate(object userData)
			{
				callback();
			}, null);
			return;
		}
		NetCache.NetCacheBatchRequest netCacheBatchRequest = new NetCache.NetCacheBatchRequest(callback, errorCallback, new NetCache.RequestFunc(this.RegisterScreenEndOfGame));
		netCacheBatchRequest.AddRequests(new List<NetCache.Request>
		{
			new NetCache.Request(typeof(NetCache.NetCacheRewardProgress), false),
			new NetCache.Request(typeof(NetCache.NetCacheMedalInfo), true),
			new NetCache.Request(typeof(NetCache.NetCacheGamesPlayed), true),
			new NetCache.Request(typeof(NetCache.NetCachePlayerRecords), true),
			new NetCache.Request(typeof(NetCache.NetCacheHeroLevels), true)
		});
		this.NetCacheMakeBatchRequest(netCacheBatchRequest);
		PegasusShared.GameType gameType = (gameMgr != null) ? gameMgr.GetGameType() : PegasusShared.GameType.GT_UNKNOWN;
		bool flag = GameUtils.IsTavernBrawlGameType(gameType);
		if (gameType == PegasusShared.GameType.GT_VS_FRIEND && FriendChallengeMgr.Get().IsChallengeTavernBrawl())
		{
			NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
			if (netObject != null && netObject.FriendWeekAllowsTavernBrawlRecordUpdate && SpecialEventManager.Get().IsEventActive(SpecialEventType.FRIEND_WEEK, false))
			{
				flag = true;
			}
		}
		if (flag)
		{
			TavernBrawlManager.Get().RefreshPlayerRecord();
		}
		if (GameUtils.IsFiresideGatheringGameType(gameType))
		{
			Network.Get().RequestFSGPatronListUpdate();
		}
	}

	// Token: 0x0600566E RID: 22126 RVA: 0x001C5574 File Offset: 0x001C3774
	public void RegisterScreenPackOpening(NetCache.NetCacheCallback callback)
	{
		this.RegisterScreenPackOpening(callback, new NetCache.ErrorCallback(NetCache.DefaultErrorHandler));
	}

	// Token: 0x0600566F RID: 22127 RVA: 0x001C558C File Offset: 0x001C378C
	public void RegisterScreenPackOpening(NetCache.NetCacheCallback callback, NetCache.ErrorCallback errorCallback)
	{
		NetCache.NetCacheBatchRequest netCacheBatchRequest = new NetCache.NetCacheBatchRequest(callback, errorCallback, new NetCache.RequestFunc(this.RegisterScreenPackOpening));
		netCacheBatchRequest.AddRequest(new NetCache.Request(typeof(NetCache.NetCacheBoosters), false));
		this.NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	// Token: 0x06005670 RID: 22128 RVA: 0x001C55CA File Offset: 0x001C37CA
	public void RegisterScreenBox(NetCache.NetCacheCallback callback)
	{
		this.RegisterScreenBox(callback, new NetCache.ErrorCallback(NetCache.DefaultErrorHandler));
	}

	// Token: 0x06005671 RID: 22129 RVA: 0x001C55E0 File Offset: 0x001C37E0
	public void RegisterScreenBox(NetCache.NetCacheCallback callback, NetCache.ErrorCallback errorCallback)
	{
		NetCache.NetCacheBatchRequest netCacheBatchRequest = new NetCache.NetCacheBatchRequest(callback, errorCallback, new NetCache.RequestFunc(this.RegisterScreenBox));
		NetCache.NetCacheFeatures netObject = this.GetNetObject<NetCache.NetCacheFeatures>();
		Debug.Log("RegisterScreenBox tempGuardianVars=" + netObject);
		netCacheBatchRequest.AddRequests(new List<NetCache.Request>
		{
			new NetCache.Request(typeof(NetCache.NetCacheBoosters), false),
			new NetCache.Request(typeof(NetCache.NetCacheClientOptions), false),
			new NetCache.Request(typeof(NetCache.NetCacheProfileProgress), false),
			new NetCache.Request(typeof(NetCache.NetCacheFeatures), false),
			new NetCache.Request(typeof(NetCache.NetCacheMedalInfo), false),
			new NetCache.Request(typeof(NetCache.NetCacheHeroLevels), false)
		});
		this.NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	// Token: 0x06005672 RID: 22130 RVA: 0x001C56AE File Offset: 0x001C38AE
	public void RegisterScreenQuestLog(NetCache.NetCacheCallback callback)
	{
		this.RegisterScreenQuestLog(callback, new NetCache.ErrorCallback(NetCache.DefaultErrorHandler));
	}

	// Token: 0x06005673 RID: 22131 RVA: 0x001C56C4 File Offset: 0x001C38C4
	public void RegisterScreenQuestLog(NetCache.NetCacheCallback callback, NetCache.ErrorCallback errorCallback)
	{
		NetCache.NetCacheBatchRequest netCacheBatchRequest = new NetCache.NetCacheBatchRequest(callback, errorCallback, new NetCache.RequestFunc(this.RegisterScreenQuestLog));
		netCacheBatchRequest.AddRequests(new List<NetCache.Request>
		{
			new NetCache.Request(typeof(NetCache.NetCacheMedalInfo), false),
			new NetCache.Request(typeof(NetCache.NetCacheHeroLevels), false),
			new NetCache.Request(typeof(NetCache.NetCachePlayerRecords), false),
			new NetCache.Request(typeof(NetCache.NetCacheProfileProgress), true)
		});
		this.NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	// Token: 0x06005674 RID: 22132 RVA: 0x001C574F File Offset: 0x001C394F
	public void RegisterScreenStartup(NetCache.NetCacheCallback callback)
	{
		this.RegisterScreenStartup(callback, new NetCache.ErrorCallback(NetCache.DefaultErrorHandler));
	}

	// Token: 0x06005675 RID: 22133 RVA: 0x001C5764 File Offset: 0x001C3964
	public void RegisterScreenStartup(NetCache.NetCacheCallback callback, NetCache.ErrorCallback errorCallback)
	{
		NetCache.NetCacheBatchRequest netCacheBatchRequest = new NetCache.NetCacheBatchRequest(callback, errorCallback, new NetCache.RequestFunc(this.RegisterScreenStartup));
		netCacheBatchRequest.AddRequests(new List<NetCache.Request>
		{
			new NetCache.Request(typeof(NetCache.NetCacheProfileProgress), false)
		});
		this.NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	// Token: 0x06005676 RID: 22134 RVA: 0x001C57AD File Offset: 0x001C39AD
	public void RegisterScreenLogin(NetCache.NetCacheCallback callback)
	{
		this.RegisterScreenLogin(callback, new NetCache.ErrorCallback(NetCache.DefaultErrorHandler));
	}

	// Token: 0x06005677 RID: 22135 RVA: 0x001C57C4 File Offset: 0x001C39C4
	public void RegisterScreenLogin(NetCache.NetCacheCallback callback, NetCache.ErrorCallback errorCallback)
	{
		NetCache.NetCacheBatchRequest netCacheBatchRequest = new NetCache.NetCacheBatchRequest(callback, errorCallback, new NetCache.RequestFunc(this.RegisterScreenLogin));
		netCacheBatchRequest.AddRequests(new List<NetCache.Request>
		{
			new NetCache.Request(typeof(NetCache.NetCacheRewardProgress), false),
			new NetCache.Request(typeof(NetCache.NetCachePlayerRecords), false),
			new NetCache.Request(typeof(NetCache.NetCacheGoldBalance), false),
			new NetCache.Request(typeof(NetCache.NetCacheHeroLevels), false),
			new NetCache.Request(typeof(NetCache.NetCacheCardBacks), true),
			new NetCache.Request(typeof(NetCache.NetCacheFavoriteHeroes), true),
			new NetCache.Request(typeof(NetCache.NetCacheAccountLicenses), false),
			new NetCache.Request(typeof(ClientStaticAssetsResponse), false),
			new NetCache.Request(typeof(NetCache.NetCacheClientOptions), false),
			new NetCache.Request(typeof(NetCache.NetCacheCoins), false)
		});
		this.NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	// Token: 0x06005678 RID: 22136 RVA: 0x001C58D3 File Offset: 0x001C3AD3
	public void RegisterReconnectMgr(NetCache.NetCacheCallback callback)
	{
		this.RegisterReconnectMgr(callback, new NetCache.ErrorCallback(NetCache.DefaultErrorHandler));
	}

	// Token: 0x06005679 RID: 22137 RVA: 0x001C58E8 File Offset: 0x001C3AE8
	public void RegisterReconnectMgr(NetCache.NetCacheCallback callback, NetCache.ErrorCallback errorCallback)
	{
		NetCache.NetCacheBatchRequest netCacheBatchRequest = new NetCache.NetCacheBatchRequest(callback, errorCallback, new NetCache.RequestFunc(this.RegisterReconnectMgr));
		netCacheBatchRequest.AddRequest(new NetCache.Request(typeof(NetCache.NetCacheDisconnectedGame), false));
		this.NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	// Token: 0x0600567A RID: 22138 RVA: 0x001C5926 File Offset: 0x001C3B26
	public void RegisterTutorialEndGameScreen(NetCache.NetCacheCallback callback)
	{
		this.RegisterTutorialEndGameScreen(callback, new NetCache.ErrorCallback(NetCache.DefaultErrorHandler));
	}

	// Token: 0x0600567B RID: 22139 RVA: 0x001C593C File Offset: 0x001C3B3C
	public void RegisterTutorialEndGameScreen(NetCache.NetCacheCallback callback, NetCache.ErrorCallback errorCallback)
	{
		GameMgr gameMgr;
		if (HearthstoneServices.TryGet<GameMgr>(out gameMgr) && gameMgr.IsSpectator())
		{
			Processor.ScheduleCallback(0f, false, delegate(object userData)
			{
				callback();
			}, null);
			return;
		}
		NetCache.NetCacheBatchRequest netCacheBatchRequest = new NetCache.NetCacheBatchRequest(callback, errorCallback, new NetCache.RequestFunc(this.RegisterTutorialEndGameScreen));
		netCacheBatchRequest.AddRequests(new List<NetCache.Request>
		{
			new NetCache.Request(typeof(NetCache.NetCacheProfileProgress), false)
		});
		this.NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	// Token: 0x0600567C RID: 22140 RVA: 0x001C59C2 File Offset: 0x001C3BC2
	public void RegisterFriendChallenge(NetCache.NetCacheCallback callback)
	{
		this.RegisterFriendChallenge(callback, new NetCache.ErrorCallback(NetCache.DefaultErrorHandler));
	}

	// Token: 0x0600567D RID: 22141 RVA: 0x001C59D8 File Offset: 0x001C3BD8
	public void RegisterFriendChallenge(NetCache.NetCacheCallback callback, NetCache.ErrorCallback errorCallback)
	{
		NetCache.NetCacheBatchRequest netCacheBatchRequest = new NetCache.NetCacheBatchRequest(callback, errorCallback, new NetCache.RequestFunc(this.RegisterFriendChallenge));
		netCacheBatchRequest.AddRequest(new NetCache.Request(typeof(NetCache.NetCacheProfileProgress), false));
		this.NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	// Token: 0x0600567E RID: 22142 RVA: 0x001C5A16 File Offset: 0x001C3C16
	public void RegisterScreenBattlegrounds(NetCache.NetCacheCallback callback)
	{
		this.RegisterScreenBattlegrounds(callback, new NetCache.ErrorCallback(NetCache.DefaultErrorHandler));
	}

	// Token: 0x0600567F RID: 22143 RVA: 0x001C5A2C File Offset: 0x001C3C2C
	public void RegisterScreenBattlegrounds(NetCache.NetCacheCallback callback, NetCache.ErrorCallback errorCallback)
	{
		NetCache.NetCacheBatchRequest netCacheBatchRequest = new NetCache.NetCacheBatchRequest(callback, errorCallback, new NetCache.RequestFunc(this.RegisterScreenBattlegrounds));
		netCacheBatchRequest.AddRequests(new List<NetCache.Request>
		{
			new NetCache.Request(typeof(NetCache.NetCacheFeatures), false)
		});
		this.NetCacheMakeBatchRequest(netCacheBatchRequest);
	}

	// Token: 0x06005680 RID: 22144 RVA: 0x001C5A78 File Offset: 0x001C3C78
	private void AddCollectionManagerToRequest(ref NetCache.NetCacheBatchRequest request)
	{
		request.AddRequests(new List<NetCache.Request>
		{
			new NetCache.Request(typeof(NetCache.NetCacheProfileNotices), false),
			new NetCache.Request(typeof(NetCache.NetCacheDecks), false),
			new NetCache.Request(typeof(NetCache.NetCacheCollection), false),
			new NetCache.Request(typeof(NetCache.NetCacheCardValues), false),
			new NetCache.Request(typeof(NetCache.NetCacheArcaneDustBalance), false),
			new NetCache.Request(typeof(NetCache.NetCacheClientOptions), false)
		});
	}

	// Token: 0x06005681 RID: 22145 RVA: 0x001C5B18 File Offset: 0x001C3D18
	private void OnPacketThrottled(int packetID, long retryMillis)
	{
		if (packetID != 201)
		{
			return;
		}
		float timeAdded = Time.realtimeSinceStartup + (float)retryMillis / 1000f;
		foreach (NetCache.NetCacheBatchRequest netCacheBatchRequest in this.m_cacheRequests)
		{
			netCacheBatchRequest.m_timeAdded = timeAdded;
		}
	}

	// Token: 0x06005682 RID: 22146 RVA: 0x001C5B84 File Offset: 0x001C3D84
	public void Cheat_AddNotice(NetCache.ProfileNotice notice)
	{
		if (!HearthstoneApplication.IsInternal())
		{
			return;
		}
		this.UnloadNetObject<NetCache.NetCacheProfileNotices>();
		PopupDisplayManager.Get().ClearSeenNotices();
		notice.NoticeID = 9999L;
		this.m_ackedNotices.Remove(notice.NoticeID);
		this.HandleIncomingProfileNotices(new List<NetCache.ProfileNotice>
		{
			notice
		}, false);
	}

	// Token: 0x04004A7C RID: 19068
	private static readonly global::Map<Type, GetAccountInfo.Request> m_getAccountInfoTypeMap = new global::Map<Type, GetAccountInfo.Request>
	{
		{
			typeof(NetCache.NetCacheDecks),
			GetAccountInfo.Request.DECK_LIST
		},
		{
			typeof(NetCache.NetCacheMedalInfo),
			GetAccountInfo.Request.MEDAL_INFO
		},
		{
			typeof(NetCache.NetCacheCardBacks),
			GetAccountInfo.Request.CARD_BACKS
		},
		{
			typeof(NetCache.NetCachePlayerRecords),
			GetAccountInfo.Request.PLAYER_RECORD
		},
		{
			typeof(NetCache.NetCacheGamesPlayed),
			GetAccountInfo.Request.GAMES_PLAYED
		},
		{
			typeof(NetCache.NetCacheProfileProgress),
			GetAccountInfo.Request.CAMPAIGN_INFO
		},
		{
			typeof(NetCache.NetCacheCardValues),
			GetAccountInfo.Request.CARD_VALUES
		},
		{
			typeof(NetCache.NetCacheFeatures),
			GetAccountInfo.Request.FEATURES
		},
		{
			typeof(NetCache.NetCacheRewardProgress),
			GetAccountInfo.Request.REWARD_PROGRESS
		},
		{
			typeof(NetCache.NetCacheHeroLevels),
			GetAccountInfo.Request.HERO_XP
		},
		{
			typeof(NetCache.NetCacheFavoriteHeroes),
			GetAccountInfo.Request.FAVORITE_HEROES
		},
		{
			typeof(NetCache.NetCacheAccountLicenses),
			GetAccountInfo.Request.ACCOUNT_LICENSES
		},
		{
			typeof(NetCache.NetCacheCoins),
			GetAccountInfo.Request.COINS
		}
	};

	// Token: 0x04004A7D RID: 19069
	private static readonly global::Map<Type, int> m_genericRequestTypeMap = new global::Map<Type, int>
	{
		{
			typeof(ClientStaticAssetsResponse),
			340
		}
	};

	// Token: 0x04004A7E RID: 19070
	private static readonly List<Type> m_ServerInitiatedAccountInfoTypes = new List<Type>
	{
		typeof(NetCache.NetCacheCollection),
		typeof(NetCache.NetCacheClientOptions),
		typeof(NetCache.NetCacheArcaneDustBalance),
		typeof(NetCache.NetCacheGoldBalance),
		typeof(NetCache.NetCacheProfileNotices),
		typeof(NetCache.NetCacheBoosters),
		typeof(NetCache.NetCacheDecks)
	};

	// Token: 0x04004A7F RID: 19071
	private static readonly global::Map<GetAccountInfo.Request, Type> m_requestTypeMap = NetCache.GetInvertTypeMap();

	// Token: 0x04004A80 RID: 19072
	private global::Map<Type, object> m_netCache = new global::Map<Type, object>();

	// Token: 0x04004A81 RID: 19073
	private NetCache.NetCacheHeroLevels m_prevHeroLevels;

	// Token: 0x04004A82 RID: 19074
	private NetCache.NetCacheMedalInfo m_previousMedalInfo;

	// Token: 0x04004A83 RID: 19075
	private List<NetCache.DelNewNoticesListener> m_newNoticesListeners = new List<NetCache.DelNewNoticesListener>();

	// Token: 0x04004A84 RID: 19076
	private List<NetCache.DelGoldBalanceListener> m_goldBalanceListeners = new List<NetCache.DelGoldBalanceListener>();

	// Token: 0x04004A85 RID: 19077
	private global::Map<Type, HashSet<Action>> m_updatedListeners = new global::Map<Type, HashSet<Action>>();

	// Token: 0x04004A86 RID: 19078
	private global::Map<Type, int> m_changeRequests = new global::Map<Type, int>();

	// Token: 0x04004A87 RID: 19079
	private bool m_receivedInitialClientState;

	// Token: 0x04004A88 RID: 19080
	private HashSet<long> m_ackedNotices = new HashSet<long>();

	// Token: 0x04004A89 RID: 19081
	private List<NetCache.ProfileNotice> m_queuedProfileNotices = new List<NetCache.ProfileNotice>();

	// Token: 0x04004A8A RID: 19082
	private bool m_receivedInitialProfileNotices;

	// Token: 0x04004A8B RID: 19083
	private long m_currencyVersion;

	// Token: 0x04004A8C RID: 19084
	private long m_initialCollectionVersion;

	// Token: 0x04004A8D RID: 19085
	private HashSet<long> m_expectedCardModifications = new HashSet<long>();

	// Token: 0x04004A8E RID: 19086
	private HashSet<long> m_handledCardModifications = new HashSet<long>();

	// Token: 0x04004A91 RID: 19089
	private long m_lastForceCheckedSeason;

	// Token: 0x04004A92 RID: 19090
	private List<NetCache.NetCacheBatchRequest> m_cacheRequests = new List<NetCache.NetCacheBatchRequest>();

	// Token: 0x04004A93 RID: 19091
	private List<Type> m_inTransitRequests = new List<Type>();

	// Token: 0x04004A94 RID: 19092
	private static bool m_fatalErrorCodeSet = false;

	// Token: 0x020020BA RID: 8378
	// (Invoke) Token: 0x06011FD3 RID: 73683
	public delegate void DelNewNoticesListener(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList);

	// Token: 0x020020BB RID: 8379
	// (Invoke) Token: 0x06011FD7 RID: 73687
	public delegate void DelGoldBalanceListener(NetCache.NetCacheGoldBalance balance);

	// Token: 0x020020BC RID: 8380
	// (Invoke) Token: 0x06011FDB RID: 73691
	public delegate void DelFavoriteCardBackChangedListener(int newFavoriteCardBackID);

	// Token: 0x020020BD RID: 8381
	// (Invoke) Token: 0x06011FDF RID: 73695
	public delegate void DelFavoriteCoinChangedListener(int newFavoriteCoinID);

	// Token: 0x020020BE RID: 8382
	public class NetCacheGamesPlayed
	{
		// Token: 0x1700279C RID: 10140
		// (get) Token: 0x06011FE2 RID: 73698 RVA: 0x004FC38C File Offset: 0x004FA58C
		// (set) Token: 0x06011FE3 RID: 73699 RVA: 0x004FC394 File Offset: 0x004FA594
		public int GamesStarted { get; set; }

		// Token: 0x1700279D RID: 10141
		// (get) Token: 0x06011FE4 RID: 73700 RVA: 0x004FC39D File Offset: 0x004FA59D
		// (set) Token: 0x06011FE5 RID: 73701 RVA: 0x004FC3A5 File Offset: 0x004FA5A5
		public int GamesWon { get; set; }

		// Token: 0x1700279E RID: 10142
		// (get) Token: 0x06011FE6 RID: 73702 RVA: 0x004FC3AE File Offset: 0x004FA5AE
		// (set) Token: 0x06011FE7 RID: 73703 RVA: 0x004FC3B6 File Offset: 0x004FA5B6
		public int GamesLost { get; set; }

		// Token: 0x1700279F RID: 10143
		// (get) Token: 0x06011FE8 RID: 73704 RVA: 0x004FC3BF File Offset: 0x004FA5BF
		// (set) Token: 0x06011FE9 RID: 73705 RVA: 0x004FC3C7 File Offset: 0x004FA5C7
		public int FreeRewardProgress { get; set; }
	}

	// Token: 0x020020BF RID: 8383
	public class NetCacheFeatures
	{
		// Token: 0x170027A0 RID: 10144
		// (get) Token: 0x06011FEB RID: 73707 RVA: 0x004FC3D0 File Offset: 0x004FA5D0
		// (set) Token: 0x06011FEC RID: 73708 RVA: 0x004FC3D8 File Offset: 0x004FA5D8
		public NetCache.NetCacheFeatures.CacheMisc Misc { get; set; }

		// Token: 0x170027A1 RID: 10145
		// (get) Token: 0x06011FED RID: 73709 RVA: 0x004FC3E1 File Offset: 0x004FA5E1
		// (set) Token: 0x06011FEE RID: 73710 RVA: 0x004FC3E9 File Offset: 0x004FA5E9
		public NetCache.NetCacheFeatures.CacheGames Games { get; set; }

		// Token: 0x170027A2 RID: 10146
		// (get) Token: 0x06011FEF RID: 73711 RVA: 0x004FC3F2 File Offset: 0x004FA5F2
		// (set) Token: 0x06011FF0 RID: 73712 RVA: 0x004FC3FA File Offset: 0x004FA5FA
		public NetCache.NetCacheFeatures.CacheCollection Collection { get; set; }

		// Token: 0x170027A3 RID: 10147
		// (get) Token: 0x06011FF1 RID: 73713 RVA: 0x004FC403 File Offset: 0x004FA603
		// (set) Token: 0x06011FF2 RID: 73714 RVA: 0x004FC40B File Offset: 0x004FA60B
		public NetCache.NetCacheFeatures.CacheStore Store { get; set; }

		// Token: 0x170027A4 RID: 10148
		// (get) Token: 0x06011FF3 RID: 73715 RVA: 0x004FC414 File Offset: 0x004FA614
		// (set) Token: 0x06011FF4 RID: 73716 RVA: 0x004FC41C File Offset: 0x004FA61C
		public NetCache.NetCacheFeatures.CacheHeroes Heroes { get; set; }

		// Token: 0x170027A5 RID: 10149
		// (get) Token: 0x06011FF5 RID: 73717 RVA: 0x004FC425 File Offset: 0x004FA625
		// (set) Token: 0x06011FF6 RID: 73718 RVA: 0x004FC42D File Offset: 0x004FA62D
		public int WinsPerGold { get; set; }

		// Token: 0x170027A6 RID: 10150
		// (get) Token: 0x06011FF7 RID: 73719 RVA: 0x004FC436 File Offset: 0x004FA636
		// (set) Token: 0x06011FF8 RID: 73720 RVA: 0x004FC43E File Offset: 0x004FA63E
		public int GoldPerReward { get; set; }

		// Token: 0x170027A7 RID: 10151
		// (get) Token: 0x06011FF9 RID: 73721 RVA: 0x004FC447 File Offset: 0x004FA647
		// (set) Token: 0x06011FFA RID: 73722 RVA: 0x004FC44F File Offset: 0x004FA64F
		public int MaxGoldPerDay { get; set; }

		// Token: 0x170027A8 RID: 10152
		// (get) Token: 0x06011FFB RID: 73723 RVA: 0x004FC458 File Offset: 0x004FA658
		// (set) Token: 0x06011FFC RID: 73724 RVA: 0x004FC460 File Offset: 0x004FA660
		public int XPSoloLimit { get; set; }

		// Token: 0x170027A9 RID: 10153
		// (get) Token: 0x06011FFD RID: 73725 RVA: 0x004FC469 File Offset: 0x004FA669
		// (set) Token: 0x06011FFE RID: 73726 RVA: 0x004FC471 File Offset: 0x004FA671
		public int MaxHeroLevel { get; set; }

		// Token: 0x170027AA RID: 10154
		// (get) Token: 0x06011FFF RID: 73727 RVA: 0x004FC47A File Offset: 0x004FA67A
		// (set) Token: 0x06012000 RID: 73728 RVA: 0x004FC482 File Offset: 0x004FA682
		public float SpecialEventTimingMod { get; set; }

		// Token: 0x170027AB RID: 10155
		// (get) Token: 0x06012001 RID: 73729 RVA: 0x004FC48B File Offset: 0x004FA68B
		// (set) Token: 0x06012002 RID: 73730 RVA: 0x004FC493 File Offset: 0x004FA693
		public int FriendWeekConcederMaxDefense { get; set; }

		// Token: 0x170027AC RID: 10156
		// (get) Token: 0x06012003 RID: 73731 RVA: 0x004FC49C File Offset: 0x004FA69C
		// (set) Token: 0x06012004 RID: 73732 RVA: 0x004FC4A4 File Offset: 0x004FA6A4
		public int FriendWeekConcededGameMinTotalTurns { get; set; }

		// Token: 0x170027AD RID: 10157
		// (get) Token: 0x06012005 RID: 73733 RVA: 0x004FC4AD File Offset: 0x004FA6AD
		// (set) Token: 0x06012006 RID: 73734 RVA: 0x004FC4B5 File Offset: 0x004FA6B5
		public bool FriendWeekAllowsTavernBrawlRecordUpdate { get; set; }

		// Token: 0x170027AE RID: 10158
		// (get) Token: 0x06012007 RID: 73735 RVA: 0x004FC4BE File Offset: 0x004FA6BE
		// (set) Token: 0x06012008 RID: 73736 RVA: 0x004FC4C6 File Offset: 0x004FA6C6
		public bool FSGEnabled { get; set; }

		// Token: 0x170027AF RID: 10159
		// (get) Token: 0x06012009 RID: 73737 RVA: 0x004FC4CF File Offset: 0x004FA6CF
		// (set) Token: 0x0601200A RID: 73738 RVA: 0x004FC4D7 File Offset: 0x004FA6D7
		public bool FSGAutoCheckinEnabled { get; set; }

		// Token: 0x170027B0 RID: 10160
		// (get) Token: 0x0601200B RID: 73739 RVA: 0x004FC4E0 File Offset: 0x004FA6E0
		// (set) Token: 0x0601200C RID: 73740 RVA: 0x004FC4E8 File Offset: 0x004FA6E8
		public bool FSGLoginScanEnabled { get; set; }

		// Token: 0x170027B1 RID: 10161
		// (get) Token: 0x0601200D RID: 73741 RVA: 0x004FC4F1 File Offset: 0x004FA6F1
		// (set) Token: 0x0601200E RID: 73742 RVA: 0x004FC4F9 File Offset: 0x004FA6F9
		public bool FSGShowBetaLabel { get; set; }

		// Token: 0x170027B2 RID: 10162
		// (get) Token: 0x0601200F RID: 73743 RVA: 0x004FC502 File Offset: 0x004FA702
		// (set) Token: 0x06012010 RID: 73744 RVA: 0x004FC50A File Offset: 0x004FA70A
		public int FSGFriendListPatronCountLimit { get; set; }

		// Token: 0x170027B3 RID: 10163
		// (get) Token: 0x06012011 RID: 73745 RVA: 0x004FC513 File Offset: 0x004FA713
		// (set) Token: 0x06012012 RID: 73746 RVA: 0x004FC51B File Offset: 0x004FA71B
		public uint ArenaClosedToNewSessionsSeconds { get; set; }

		// Token: 0x170027B4 RID: 10164
		// (get) Token: 0x06012013 RID: 73747 RVA: 0x004FC524 File Offset: 0x004FA724
		// (set) Token: 0x06012014 RID: 73748 RVA: 0x004FC52C File Offset: 0x004FA72C
		public uint PVPDRClosedToNewSessionsSeconds { get; set; }

		// Token: 0x170027B5 RID: 10165
		// (get) Token: 0x06012015 RID: 73749 RVA: 0x004FC535 File Offset: 0x004FA735
		// (set) Token: 0x06012016 RID: 73750 RVA: 0x004FC53D File Offset: 0x004FA73D
		public int FsgMaxPresencePubscribedPatronCount { get; set; }

		// Token: 0x170027B6 RID: 10166
		// (get) Token: 0x06012017 RID: 73751 RVA: 0x004FC546 File Offset: 0x004FA746
		// (set) Token: 0x06012018 RID: 73752 RVA: 0x004FC54E File Offset: 0x004FA74E
		public bool QuickPackOpeningAllowed { get; set; }

		// Token: 0x170027B7 RID: 10167
		// (get) Token: 0x06012019 RID: 73753 RVA: 0x004FC557 File Offset: 0x004FA757
		// (set) Token: 0x0601201A RID: 73754 RVA: 0x004FC55F File Offset: 0x004FA75F
		public bool ForceIosLowRes { get; set; }

		// Token: 0x170027B8 RID: 10168
		// (get) Token: 0x0601201B RID: 73755 RVA: 0x004FC568 File Offset: 0x004FA768
		// (set) Token: 0x0601201C RID: 73756 RVA: 0x004FC570 File Offset: 0x004FA770
		public bool EnableSmartDeckCompletion { get; set; }

		// Token: 0x170027B9 RID: 10169
		// (get) Token: 0x0601201D RID: 73757 RVA: 0x004FC579 File Offset: 0x004FA779
		// (set) Token: 0x0601201E RID: 73758 RVA: 0x004FC581 File Offset: 0x004FA781
		public bool AllowOfflineClientActivity { get; set; }

		// Token: 0x170027BA RID: 10170
		// (get) Token: 0x0601201F RID: 73759 RVA: 0x004FC58A File Offset: 0x004FA78A
		// (set) Token: 0x06012020 RID: 73760 RVA: 0x004FC592 File Offset: 0x004FA792
		public bool AllowOfflineClientDeckDeletion { get; set; }

		// Token: 0x170027BB RID: 10171
		// (get) Token: 0x06012021 RID: 73761 RVA: 0x004FC59B File Offset: 0x004FA79B
		// (set) Token: 0x06012022 RID: 73762 RVA: 0x004FC5A3 File Offset: 0x004FA7A3
		public int BattlegroundsEarlyAccessLicense { get; set; }

		// Token: 0x170027BC RID: 10172
		// (get) Token: 0x06012023 RID: 73763 RVA: 0x004FC5AC File Offset: 0x004FA7AC
		// (set) Token: 0x06012024 RID: 73764 RVA: 0x004FC5B4 File Offset: 0x004FA7B4
		public int BattlegroundsMaxRankedPartySize { get; set; }

		// Token: 0x170027BD RID: 10173
		// (get) Token: 0x06012025 RID: 73765 RVA: 0x004FC5BD File Offset: 0x004FA7BD
		// (set) Token: 0x06012026 RID: 73766 RVA: 0x004FC5C5 File Offset: 0x004FA7C5
		public bool ProgressionEnabled { get; set; }

		// Token: 0x170027BE RID: 10174
		// (get) Token: 0x06012027 RID: 73767 RVA: 0x004FC5CE File Offset: 0x004FA7CE
		// (set) Token: 0x06012028 RID: 73768 RVA: 0x004FC5D6 File Offset: 0x004FA7D6
		public bool JournalButtonDisabled { get; set; }

		// Token: 0x170027BF RID: 10175
		// (get) Token: 0x06012029 RID: 73769 RVA: 0x004FC5DF File Offset: 0x004FA7DF
		// (set) Token: 0x0601202A RID: 73770 RVA: 0x004FC5E7 File Offset: 0x004FA7E7
		public bool AchievementToastDisabled { get; set; }

		// Token: 0x170027C0 RID: 10176
		// (get) Token: 0x0601202B RID: 73771 RVA: 0x004FC5F0 File Offset: 0x004FA7F0
		// (set) Token: 0x0601202C RID: 73772 RVA: 0x004FC5F8 File Offset: 0x004FA7F8
		public uint DuelsEarlyAccessLicense { get; set; }

		// Token: 0x170027C1 RID: 10177
		// (get) Token: 0x0601202D RID: 73773 RVA: 0x004FC601 File Offset: 0x004FA801
		// (set) Token: 0x0601202E RID: 73774 RVA: 0x004FC609 File Offset: 0x004FA809
		public bool ContentstackEnabled { get; set; }

		// Token: 0x170027C2 RID: 10178
		// (get) Token: 0x0601202F RID: 73775 RVA: 0x004FC612 File Offset: 0x004FA812
		// (set) Token: 0x06012030 RID: 73776 RVA: 0x004FC61A File Offset: 0x004FA81A
		public bool AppRatingEnabled { get; set; }

		// Token: 0x170027C3 RID: 10179
		// (get) Token: 0x06012031 RID: 73777 RVA: 0x004FC623 File Offset: 0x004FA823
		// (set) Token: 0x06012032 RID: 73778 RVA: 0x004FC62B File Offset: 0x004FA82B
		public float AppRatingSamplingPercentage { get; set; }

		// Token: 0x06012033 RID: 73779 RVA: 0x004FC634 File Offset: 0x004FA834
		public NetCacheFeatures()
		{
			this.Misc = new NetCache.NetCacheFeatures.CacheMisc();
			this.Games = new NetCache.NetCacheFeatures.CacheGames();
			this.Collection = new NetCache.NetCacheFeatures.CacheCollection();
			this.Store = new NetCache.NetCacheFeatures.CacheStore();
			this.Heroes = new NetCache.NetCacheFeatures.CacheHeroes();
		}

		// Token: 0x0400DE38 RID: 56888
		public bool CaisEnabledNonMobile;

		// Token: 0x0400DE39 RID: 56889
		public bool CaisEnabledMobileChina;

		// Token: 0x0400DE3A RID: 56890
		public bool CaisEnabledMobileSouthKorea;

		// Token: 0x0400DE3B RID: 56891
		public bool SendTelemetryPresence;

		// Token: 0x0200299A RID: 10650
		public class CacheMisc
		{
			// Token: 0x17002D9D RID: 11677
			// (get) Token: 0x06013F3B RID: 81723 RVA: 0x00541A63 File Offset: 0x0053FC63
			// (set) Token: 0x06013F3C RID: 81724 RVA: 0x00541A6B File Offset: 0x0053FC6B
			public int ClientOptionsUpdateIntervalSeconds { get; set; }

			// Token: 0x17002D9E RID: 11678
			// (get) Token: 0x06013F3D RID: 81725 RVA: 0x00541A74 File Offset: 0x0053FC74
			// (set) Token: 0x06013F3E RID: 81726 RVA: 0x00541A7C File Offset: 0x0053FC7C
			public bool AllowLiveFPSGathering { get; set; }
		}

		// Token: 0x0200299B RID: 10651
		public class CacheGames
		{
			// Token: 0x17002D9F RID: 11679
			// (get) Token: 0x06013F40 RID: 81728 RVA: 0x00541A85 File Offset: 0x0053FC85
			// (set) Token: 0x06013F41 RID: 81729 RVA: 0x00541A8D File Offset: 0x0053FC8D
			public bool Tournament { get; set; }

			// Token: 0x17002DA0 RID: 11680
			// (get) Token: 0x06013F42 RID: 81730 RVA: 0x00541A96 File Offset: 0x0053FC96
			// (set) Token: 0x06013F43 RID: 81731 RVA: 0x00541A9E File Offset: 0x0053FC9E
			public bool Practice { get; set; }

			// Token: 0x17002DA1 RID: 11681
			// (get) Token: 0x06013F44 RID: 81732 RVA: 0x00541AA7 File Offset: 0x0053FCA7
			// (set) Token: 0x06013F45 RID: 81733 RVA: 0x00541AAF File Offset: 0x0053FCAF
			public bool Casual { get; set; }

			// Token: 0x17002DA2 RID: 11682
			// (get) Token: 0x06013F46 RID: 81734 RVA: 0x00541AB8 File Offset: 0x0053FCB8
			// (set) Token: 0x06013F47 RID: 81735 RVA: 0x00541AC0 File Offset: 0x0053FCC0
			public bool Forge { get; set; }

			// Token: 0x17002DA3 RID: 11683
			// (get) Token: 0x06013F48 RID: 81736 RVA: 0x00541AC9 File Offset: 0x0053FCC9
			// (set) Token: 0x06013F49 RID: 81737 RVA: 0x00541AD1 File Offset: 0x0053FCD1
			public bool Friendly { get; set; }

			// Token: 0x17002DA4 RID: 11684
			// (get) Token: 0x06013F4A RID: 81738 RVA: 0x00541ADA File Offset: 0x0053FCDA
			// (set) Token: 0x06013F4B RID: 81739 RVA: 0x00541AE2 File Offset: 0x0053FCE2
			public bool TavernBrawl { get; set; }

			// Token: 0x17002DA5 RID: 11685
			// (get) Token: 0x06013F4C RID: 81740 RVA: 0x00541AEB File Offset: 0x0053FCEB
			// (set) Token: 0x06013F4D RID: 81741 RVA: 0x00541AF3 File Offset: 0x0053FCF3
			public bool Battlegrounds { get; set; }

			// Token: 0x17002DA6 RID: 11686
			// (get) Token: 0x06013F4E RID: 81742 RVA: 0x00541AFC File Offset: 0x0053FCFC
			// (set) Token: 0x06013F4F RID: 81743 RVA: 0x00541B04 File Offset: 0x0053FD04
			public bool BattlegroundsFriendlyChallenge { get; set; }

			// Token: 0x17002DA7 RID: 11687
			// (get) Token: 0x06013F50 RID: 81744 RVA: 0x00541B0D File Offset: 0x0053FD0D
			// (set) Token: 0x06013F51 RID: 81745 RVA: 0x00541B15 File Offset: 0x0053FD15
			public bool BattlegroundsTutorial { get; set; }

			// Token: 0x17002DA8 RID: 11688
			// (get) Token: 0x06013F52 RID: 81746 RVA: 0x00541B1E File Offset: 0x0053FD1E
			// (set) Token: 0x06013F53 RID: 81747 RVA: 0x00541B26 File Offset: 0x0053FD26
			public int ShowUserUI { get; set; }

			// Token: 0x17002DA9 RID: 11689
			// (get) Token: 0x06013F54 RID: 81748 RVA: 0x00541B2F File Offset: 0x0053FD2F
			// (set) Token: 0x06013F55 RID: 81749 RVA: 0x00541B37 File Offset: 0x0053FD37
			public bool Duels { get; set; }

			// Token: 0x17002DAA RID: 11690
			// (get) Token: 0x06013F56 RID: 81750 RVA: 0x00541B40 File Offset: 0x0053FD40
			// (set) Token: 0x06013F57 RID: 81751 RVA: 0x00541B48 File Offset: 0x0053FD48
			public bool PaidDuels { get; set; }

			// Token: 0x06013F58 RID: 81752 RVA: 0x00541B54 File Offset: 0x0053FD54
			public bool GetFeatureFlag(NetCache.NetCacheFeatures.CacheGames.FeatureFlags flag)
			{
				switch (flag)
				{
				case NetCache.NetCacheFeatures.CacheGames.FeatureFlags.Tournament:
					return this.Tournament;
				case NetCache.NetCacheFeatures.CacheGames.FeatureFlags.Practice:
					return this.Practice;
				case NetCache.NetCacheFeatures.CacheGames.FeatureFlags.Casual:
					return this.Casual;
				case NetCache.NetCacheFeatures.CacheGames.FeatureFlags.Forge:
					return this.Forge;
				case NetCache.NetCacheFeatures.CacheGames.FeatureFlags.Friendly:
					return this.Friendly;
				case NetCache.NetCacheFeatures.CacheGames.FeatureFlags.TavernBrawl:
					return this.TavernBrawl;
				case NetCache.NetCacheFeatures.CacheGames.FeatureFlags.Battlegrounds:
					return this.Battlegrounds;
				case NetCache.NetCacheFeatures.CacheGames.FeatureFlags.BattlegroundsFriendlyChallenge:
					return this.BattlegroundsFriendlyChallenge;
				case NetCache.NetCacheFeatures.CacheGames.FeatureFlags.BattlegroundsTutorial:
					return this.BattlegroundsTutorial;
				case NetCache.NetCacheFeatures.CacheGames.FeatureFlags.Duels:
					return this.Duels;
				case NetCache.NetCacheFeatures.CacheGames.FeatureFlags.PaidDuels:
					return this.PaidDuels;
				default:
					return false;
				}
			}

			// Token: 0x020029BC RID: 10684
			public enum FeatureFlags
			{
				// Token: 0x0400FE3E RID: 65086
				Invalid,
				// Token: 0x0400FE3F RID: 65087
				Tournament,
				// Token: 0x0400FE40 RID: 65088
				Practice,
				// Token: 0x0400FE41 RID: 65089
				Casual,
				// Token: 0x0400FE42 RID: 65090
				Forge,
				// Token: 0x0400FE43 RID: 65091
				Friendly,
				// Token: 0x0400FE44 RID: 65092
				TavernBrawl,
				// Token: 0x0400FE45 RID: 65093
				Battlegrounds,
				// Token: 0x0400FE46 RID: 65094
				BattlegroundsFriendlyChallenge,
				// Token: 0x0400FE47 RID: 65095
				BattlegroundsTutorial,
				// Token: 0x0400FE48 RID: 65096
				Duels,
				// Token: 0x0400FE49 RID: 65097
				PaidDuels
			}
		}

		// Token: 0x0200299C RID: 10652
		public class CacheCollection
		{
			// Token: 0x17002DAB RID: 11691
			// (get) Token: 0x06013F5A RID: 81754 RVA: 0x00541BE5 File Offset: 0x0053FDE5
			// (set) Token: 0x06013F5B RID: 81755 RVA: 0x00541BED File Offset: 0x0053FDED
			public bool Manager { get; set; }

			// Token: 0x17002DAC RID: 11692
			// (get) Token: 0x06013F5C RID: 81756 RVA: 0x00541BF6 File Offset: 0x0053FDF6
			// (set) Token: 0x06013F5D RID: 81757 RVA: 0x00541BFE File Offset: 0x0053FDFE
			public bool Crafting { get; set; }

			// Token: 0x17002DAD RID: 11693
			// (get) Token: 0x06013F5E RID: 81758 RVA: 0x00541C07 File Offset: 0x0053FE07
			// (set) Token: 0x06013F5F RID: 81759 RVA: 0x00541C0F File Offset: 0x0053FE0F
			public bool DeckReordering { get; set; }
		}

		// Token: 0x0200299D RID: 10653
		public class CacheStore
		{
			// Token: 0x17002DAE RID: 11694
			// (get) Token: 0x06013F61 RID: 81761 RVA: 0x00541C18 File Offset: 0x0053FE18
			// (set) Token: 0x06013F62 RID: 81762 RVA: 0x00541C20 File Offset: 0x0053FE20
			public bool Store { get; set; }

			// Token: 0x17002DAF RID: 11695
			// (get) Token: 0x06013F63 RID: 81763 RVA: 0x00541C29 File Offset: 0x0053FE29
			// (set) Token: 0x06013F64 RID: 81764 RVA: 0x00541C31 File Offset: 0x0053FE31
			public bool BattlePay { get; set; }

			// Token: 0x17002DB0 RID: 11696
			// (get) Token: 0x06013F65 RID: 81765 RVA: 0x00541C3A File Offset: 0x0053FE3A
			// (set) Token: 0x06013F66 RID: 81766 RVA: 0x00541C42 File Offset: 0x0053FE42
			public bool BuyWithGold { get; set; }

			// Token: 0x17002DB1 RID: 11697
			// (get) Token: 0x06013F67 RID: 81767 RVA: 0x00541C4B File Offset: 0x0053FE4B
			// (set) Token: 0x06013F68 RID: 81768 RVA: 0x00541C53 File Offset: 0x0053FE53
			public bool SimpleCheckout { get; set; }

			// Token: 0x17002DB2 RID: 11698
			// (get) Token: 0x06013F69 RID: 81769 RVA: 0x00541C5C File Offset: 0x0053FE5C
			// (set) Token: 0x06013F6A RID: 81770 RVA: 0x00541C64 File Offset: 0x0053FE64
			public bool SoftAccountPurchasing { get; set; }

			// Token: 0x17002DB3 RID: 11699
			// (get) Token: 0x06013F6B RID: 81771 RVA: 0x00541C6D File Offset: 0x0053FE6D
			// (set) Token: 0x06013F6C RID: 81772 RVA: 0x00541C75 File Offset: 0x0053FE75
			public bool VirtualCurrencyEnabled { get; set; }

			// Token: 0x17002DB4 RID: 11700
			// (get) Token: 0x06013F6D RID: 81773 RVA: 0x00541C7E File Offset: 0x0053FE7E
			// (set) Token: 0x06013F6E RID: 81774 RVA: 0x00541C86 File Offset: 0x0053FE86
			public int NumClassicPacksUntilDeprioritize { get; set; }

			// Token: 0x17002DB5 RID: 11701
			// (get) Token: 0x06013F6F RID: 81775 RVA: 0x00541C8F File Offset: 0x0053FE8F
			// (set) Token: 0x06013F70 RID: 81776 RVA: 0x00541C97 File Offset: 0x0053FE97
			public bool SimpleCheckoutIOS { get; set; }

			// Token: 0x17002DB6 RID: 11702
			// (get) Token: 0x06013F71 RID: 81777 RVA: 0x00541CA0 File Offset: 0x0053FEA0
			// (set) Token: 0x06013F72 RID: 81778 RVA: 0x00541CA8 File Offset: 0x0053FEA8
			public bool SimpleCheckoutAndroidAmazon { get; set; }

			// Token: 0x17002DB7 RID: 11703
			// (get) Token: 0x06013F73 RID: 81779 RVA: 0x00541CB1 File Offset: 0x0053FEB1
			// (set) Token: 0x06013F74 RID: 81780 RVA: 0x00541CB9 File Offset: 0x0053FEB9
			public bool SimpleCheckoutAndroidGoogle { get; set; }

			// Token: 0x17002DB8 RID: 11704
			// (get) Token: 0x06013F75 RID: 81781 RVA: 0x00541CC2 File Offset: 0x0053FEC2
			// (set) Token: 0x06013F76 RID: 81782 RVA: 0x00541CCA File Offset: 0x0053FECA
			public bool SimpleCheckoutAndroidGlobal { get; set; }

			// Token: 0x17002DB9 RID: 11705
			// (get) Token: 0x06013F77 RID: 81783 RVA: 0x00541CD3 File Offset: 0x0053FED3
			// (set) Token: 0x06013F78 RID: 81784 RVA: 0x00541CDB File Offset: 0x0053FEDB
			public bool SimpleCheckoutWin { get; set; }

			// Token: 0x17002DBA RID: 11706
			// (get) Token: 0x06013F79 RID: 81785 RVA: 0x00541CE4 File Offset: 0x0053FEE4
			// (set) Token: 0x06013F7A RID: 81786 RVA: 0x00541CEC File Offset: 0x0053FEEC
			public bool SimpleCheckoutMac { get; set; }

			// Token: 0x17002DBB RID: 11707
			// (get) Token: 0x06013F7B RID: 81787 RVA: 0x00541CF5 File Offset: 0x0053FEF5
			// (set) Token: 0x06013F7C RID: 81788 RVA: 0x00541CFD File Offset: 0x0053FEFD
			public int BoosterRotatingSoonWarnDaysWithoutSale { get; set; }

			// Token: 0x17002DBC RID: 11708
			// (get) Token: 0x06013F7D RID: 81789 RVA: 0x00541D06 File Offset: 0x0053FF06
			// (set) Token: 0x06013F7E RID: 81790 RVA: 0x00541D0E File Offset: 0x0053FF0E
			public int BoosterRotatingSoonWarnDaysWithSale { get; set; }

			// Token: 0x17002DBD RID: 11709
			// (get) Token: 0x06013F7F RID: 81791 RVA: 0x00541D17 File Offset: 0x0053FF17
			// (set) Token: 0x06013F80 RID: 81792 RVA: 0x00541D1F File Offset: 0x0053FF1F
			public bool VintageStore { get; set; }

			// Token: 0x17002DBE RID: 11710
			// (get) Token: 0x06013F81 RID: 81793 RVA: 0x00541D28 File Offset: 0x0053FF28
			// (set) Token: 0x06013F82 RID: 81794 RVA: 0x00541D30 File Offset: 0x0053FF30
			public bool BuyCardBacksFromCollectionManager { get; set; }

			// Token: 0x17002DBF RID: 11711
			// (get) Token: 0x06013F83 RID: 81795 RVA: 0x00541D39 File Offset: 0x0053FF39
			// (set) Token: 0x06013F84 RID: 81796 RVA: 0x00541D41 File Offset: 0x0053FF41
			public bool BuyHeroSkinsFromCollectionManager { get; set; }
		}

		// Token: 0x0200299E RID: 10654
		public class CacheHeroes
		{
			// Token: 0x17002DC0 RID: 11712
			// (get) Token: 0x06013F86 RID: 81798 RVA: 0x00541D4A File Offset: 0x0053FF4A
			// (set) Token: 0x06013F87 RID: 81799 RVA: 0x00541D52 File Offset: 0x0053FF52
			public bool Hunter { get; set; }

			// Token: 0x17002DC1 RID: 11713
			// (get) Token: 0x06013F88 RID: 81800 RVA: 0x00541D5B File Offset: 0x0053FF5B
			// (set) Token: 0x06013F89 RID: 81801 RVA: 0x00541D63 File Offset: 0x0053FF63
			public bool Mage { get; set; }

			// Token: 0x17002DC2 RID: 11714
			// (get) Token: 0x06013F8A RID: 81802 RVA: 0x00541D6C File Offset: 0x0053FF6C
			// (set) Token: 0x06013F8B RID: 81803 RVA: 0x00541D74 File Offset: 0x0053FF74
			public bool Paladin { get; set; }

			// Token: 0x17002DC3 RID: 11715
			// (get) Token: 0x06013F8C RID: 81804 RVA: 0x00541D7D File Offset: 0x0053FF7D
			// (set) Token: 0x06013F8D RID: 81805 RVA: 0x00541D85 File Offset: 0x0053FF85
			public bool Priest { get; set; }

			// Token: 0x17002DC4 RID: 11716
			// (get) Token: 0x06013F8E RID: 81806 RVA: 0x00541D8E File Offset: 0x0053FF8E
			// (set) Token: 0x06013F8F RID: 81807 RVA: 0x00541D96 File Offset: 0x0053FF96
			public bool Rogue { get; set; }

			// Token: 0x17002DC5 RID: 11717
			// (get) Token: 0x06013F90 RID: 81808 RVA: 0x00541D9F File Offset: 0x0053FF9F
			// (set) Token: 0x06013F91 RID: 81809 RVA: 0x00541DA7 File Offset: 0x0053FFA7
			public bool Shaman { get; set; }

			// Token: 0x17002DC6 RID: 11718
			// (get) Token: 0x06013F92 RID: 81810 RVA: 0x00541DB0 File Offset: 0x0053FFB0
			// (set) Token: 0x06013F93 RID: 81811 RVA: 0x00541DB8 File Offset: 0x0053FFB8
			public bool Warlock { get; set; }

			// Token: 0x17002DC7 RID: 11719
			// (get) Token: 0x06013F94 RID: 81812 RVA: 0x00541DC1 File Offset: 0x0053FFC1
			// (set) Token: 0x06013F95 RID: 81813 RVA: 0x00541DC9 File Offset: 0x0053FFC9
			public bool Warrior { get; set; }
		}
	}

	// Token: 0x020020C0 RID: 8384
	public class NetCacheArcaneDustBalance
	{
		// Token: 0x170027C4 RID: 10180
		// (get) Token: 0x06012034 RID: 73780 RVA: 0x004FC673 File Offset: 0x004FA873
		// (set) Token: 0x06012035 RID: 73781 RVA: 0x004FC67B File Offset: 0x004FA87B
		public long Balance { get; set; }
	}

	// Token: 0x020020C1 RID: 8385
	public class NetCacheGoldBalance
	{
		// Token: 0x170027C5 RID: 10181
		// (get) Token: 0x06012037 RID: 73783 RVA: 0x004FC684 File Offset: 0x004FA884
		// (set) Token: 0x06012038 RID: 73784 RVA: 0x004FC68C File Offset: 0x004FA88C
		public long CappedBalance { get; set; }

		// Token: 0x170027C6 RID: 10182
		// (get) Token: 0x06012039 RID: 73785 RVA: 0x004FC695 File Offset: 0x004FA895
		// (set) Token: 0x0601203A RID: 73786 RVA: 0x004FC69D File Offset: 0x004FA89D
		public long BonusBalance { get; set; }

		// Token: 0x170027C7 RID: 10183
		// (get) Token: 0x0601203B RID: 73787 RVA: 0x004FC6A6 File Offset: 0x004FA8A6
		// (set) Token: 0x0601203C RID: 73788 RVA: 0x004FC6AE File Offset: 0x004FA8AE
		public long Cap { get; set; }

		// Token: 0x170027C8 RID: 10184
		// (get) Token: 0x0601203D RID: 73789 RVA: 0x004FC6B7 File Offset: 0x004FA8B7
		// (set) Token: 0x0601203E RID: 73790 RVA: 0x004FC6BF File Offset: 0x004FA8BF
		public long CapWarning { get; set; }

		// Token: 0x0601203F RID: 73791 RVA: 0x004FC6C8 File Offset: 0x004FA8C8
		public long GetTotal()
		{
			return this.CappedBalance + this.BonusBalance;
		}
	}

	// Token: 0x020020C2 RID: 8386
	public class NetPlayerArenaTickets
	{
		// Token: 0x170027C9 RID: 10185
		// (get) Token: 0x06012041 RID: 73793 RVA: 0x004FC6D7 File Offset: 0x004FA8D7
		// (set) Token: 0x06012042 RID: 73794 RVA: 0x004FC6DF File Offset: 0x004FA8DF
		public int Balance { get; set; }
	}

	// Token: 0x020020C3 RID: 8387
	public class NetCacheSubscribeResponse
	{
		// Token: 0x0400DE61 RID: 56929
		public ulong FeaturesSupported;

		// Token: 0x0400DE62 RID: 56930
		public ulong Route;

		// Token: 0x0400DE63 RID: 56931
		public ulong KeepAliveDelay;

		// Token: 0x0400DE64 RID: 56932
		public ulong RequestMaxWaitSecs;
	}

	// Token: 0x020020C4 RID: 8388
	public class HeroLevel
	{
		// Token: 0x170027CA RID: 10186
		// (get) Token: 0x06012045 RID: 73797 RVA: 0x004FC6E8 File Offset: 0x004FA8E8
		// (set) Token: 0x06012046 RID: 73798 RVA: 0x004FC6F0 File Offset: 0x004FA8F0
		public TAG_CLASS Class { get; set; }

		// Token: 0x170027CB RID: 10187
		// (get) Token: 0x06012047 RID: 73799 RVA: 0x004FC6F9 File Offset: 0x004FA8F9
		// (set) Token: 0x06012048 RID: 73800 RVA: 0x004FC701 File Offset: 0x004FA901
		public NetCache.HeroLevel.LevelInfo PrevLevel { get; set; }

		// Token: 0x170027CC RID: 10188
		// (get) Token: 0x06012049 RID: 73801 RVA: 0x004FC70A File Offset: 0x004FA90A
		// (set) Token: 0x0601204A RID: 73802 RVA: 0x004FC712 File Offset: 0x004FA912
		public NetCache.HeroLevel.LevelInfo CurrentLevel { get; set; }

		// Token: 0x0601204B RID: 73803 RVA: 0x004FC71B File Offset: 0x004FA91B
		public HeroLevel()
		{
			this.Class = TAG_CLASS.INVALID;
			this.PrevLevel = null;
			this.CurrentLevel = new NetCache.HeroLevel.LevelInfo();
		}

		// Token: 0x0601204C RID: 73804 RVA: 0x004FC73C File Offset: 0x004FA93C
		public override string ToString()
		{
			return string.Format("[HeroLevel: Class={0}, PrevLevel={1}, CurrentLevel={2}]", this.Class, this.PrevLevel, this.CurrentLevel);
		}

		// Token: 0x0200299F RID: 10655
		public class LevelInfo
		{
			// Token: 0x17002DC8 RID: 11720
			// (get) Token: 0x06013F97 RID: 81815 RVA: 0x00541DD2 File Offset: 0x0053FFD2
			// (set) Token: 0x06013F98 RID: 81816 RVA: 0x00541DDA File Offset: 0x0053FFDA
			public int Level { get; set; }

			// Token: 0x17002DC9 RID: 11721
			// (get) Token: 0x06013F99 RID: 81817 RVA: 0x00541DE3 File Offset: 0x0053FFE3
			// (set) Token: 0x06013F9A RID: 81818 RVA: 0x00541DEB File Offset: 0x0053FFEB
			public int MaxLevel { get; set; }

			// Token: 0x17002DCA RID: 11722
			// (get) Token: 0x06013F9B RID: 81819 RVA: 0x00541DF4 File Offset: 0x0053FFF4
			// (set) Token: 0x06013F9C RID: 81820 RVA: 0x00541DFC File Offset: 0x0053FFFC
			public long XP { get; set; }

			// Token: 0x17002DCB RID: 11723
			// (get) Token: 0x06013F9D RID: 81821 RVA: 0x00541E05 File Offset: 0x00540005
			// (set) Token: 0x06013F9E RID: 81822 RVA: 0x00541E0D File Offset: 0x0054000D
			public long MaxXP { get; set; }

			// Token: 0x06013F9F RID: 81823 RVA: 0x00541E16 File Offset: 0x00540016
			public LevelInfo()
			{
				this.Level = 0;
				this.MaxLevel = 60;
				this.XP = 0L;
				this.MaxXP = 0L;
			}

			// Token: 0x06013FA0 RID: 81824 RVA: 0x00541E3D File Offset: 0x0054003D
			public bool IsMaxLevel()
			{
				return this.Level == this.MaxLevel;
			}

			// Token: 0x06013FA1 RID: 81825 RVA: 0x00541E4D File Offset: 0x0054004D
			public override string ToString()
			{
				return string.Format("[LevelInfo: Level={0}, XP={1}, MaxXP={2}]", this.Level, this.XP, this.MaxXP);
			}
		}
	}

	// Token: 0x020020C5 RID: 8389
	public class NetCacheHeroLevels
	{
		// Token: 0x0601204D RID: 73805 RVA: 0x004FC75F File Offset: 0x004FA95F
		public NetCacheHeroLevels()
		{
			this.Levels = new List<NetCache.HeroLevel>();
		}

		// Token: 0x0601204E RID: 73806 RVA: 0x004FC774 File Offset: 0x004FA974
		public override string ToString()
		{
			string text = "[START NetCacheHeroLevels]\n";
			foreach (NetCache.HeroLevel arg in this.Levels)
			{
				text += string.Format("{0}\n", arg);
			}
			text += "[END NetCacheHeroLevels]";
			return text;
		}

		// Token: 0x170027CD RID: 10189
		// (get) Token: 0x0601204F RID: 73807 RVA: 0x004FC7E8 File Offset: 0x004FA9E8
		// (set) Token: 0x06012050 RID: 73808 RVA: 0x004FC7F0 File Offset: 0x004FA9F0
		public List<NetCache.HeroLevel> Levels { get; set; }
	}

	// Token: 0x020020C6 RID: 8390
	public class NetCacheProfileProgress
	{
		// Token: 0x170027CE RID: 10190
		// (get) Token: 0x06012051 RID: 73809 RVA: 0x004FC7F9 File Offset: 0x004FA9F9
		// (set) Token: 0x06012052 RID: 73810 RVA: 0x004FC801 File Offset: 0x004FAA01
		public TutorialProgress CampaignProgress { get; set; }

		// Token: 0x170027CF RID: 10191
		// (get) Token: 0x06012053 RID: 73811 RVA: 0x004FC80A File Offset: 0x004FAA0A
		// (set) Token: 0x06012054 RID: 73812 RVA: 0x004FC812 File Offset: 0x004FAA12
		public int BestForgeWins { get; set; }

		// Token: 0x170027D0 RID: 10192
		// (get) Token: 0x06012055 RID: 73813 RVA: 0x004FC81B File Offset: 0x004FAA1B
		// (set) Token: 0x06012056 RID: 73814 RVA: 0x004FC823 File Offset: 0x004FAA23
		public long LastForgeDate { get; set; }
	}

	// Token: 0x020020C7 RID: 8391
	public class NetCacheDisplayBanner
	{
		// Token: 0x170027D1 RID: 10193
		// (get) Token: 0x06012058 RID: 73816 RVA: 0x004FC82C File Offset: 0x004FAA2C
		// (set) Token: 0x06012059 RID: 73817 RVA: 0x004FC834 File Offset: 0x004FAA34
		public int Id { get; set; }
	}

	// Token: 0x020020C8 RID: 8392
	public class NetCacheCardBacks
	{
		// Token: 0x0601205B RID: 73819 RVA: 0x004FC83D File Offset: 0x004FAA3D
		public NetCacheCardBacks()
		{
			this.CardBacks = new HashSet<int>();
		}

		// Token: 0x170027D2 RID: 10194
		// (get) Token: 0x0601205C RID: 73820 RVA: 0x004FC850 File Offset: 0x004FAA50
		// (set) Token: 0x0601205D RID: 73821 RVA: 0x004FC858 File Offset: 0x004FAA58
		public int FavoriteCardBack { get; set; }

		// Token: 0x170027D3 RID: 10195
		// (get) Token: 0x0601205E RID: 73822 RVA: 0x004FC861 File Offset: 0x004FAA61
		// (set) Token: 0x0601205F RID: 73823 RVA: 0x004FC869 File Offset: 0x004FAA69
		public HashSet<int> CardBacks { get; set; }
	}

	// Token: 0x020020C9 RID: 8393
	public class NetCacheCoins
	{
		// Token: 0x06012060 RID: 73824 RVA: 0x004FC872 File Offset: 0x004FAA72
		public NetCacheCoins()
		{
			this.Coins = new HashSet<int>();
		}

		// Token: 0x170027D4 RID: 10196
		// (get) Token: 0x06012061 RID: 73825 RVA: 0x004FC885 File Offset: 0x004FAA85
		// (set) Token: 0x06012062 RID: 73826 RVA: 0x004FC88D File Offset: 0x004FAA8D
		public int FavoriteCoin { get; set; }

		// Token: 0x170027D5 RID: 10197
		// (get) Token: 0x06012063 RID: 73827 RVA: 0x004FC896 File Offset: 0x004FAA96
		// (set) Token: 0x06012064 RID: 73828 RVA: 0x004FC89E File Offset: 0x004FAA9E
		public HashSet<int> Coins { get; set; }
	}

	// Token: 0x020020CA RID: 8394
	public class BoosterStack
	{
		// Token: 0x170027D6 RID: 10198
		// (get) Token: 0x06012065 RID: 73829 RVA: 0x004FC8A7 File Offset: 0x004FAAA7
		// (set) Token: 0x06012066 RID: 73830 RVA: 0x004FC8AF File Offset: 0x004FAAAF
		public int Id { get; set; }

		// Token: 0x170027D7 RID: 10199
		// (get) Token: 0x06012067 RID: 73831 RVA: 0x004FC8B8 File Offset: 0x004FAAB8
		// (set) Token: 0x06012068 RID: 73832 RVA: 0x004FC8C0 File Offset: 0x004FAAC0
		public int Count { get; set; }

		// Token: 0x170027D8 RID: 10200
		// (get) Token: 0x06012069 RID: 73833 RVA: 0x004FC8C9 File Offset: 0x004FAAC9
		// (set) Token: 0x0601206A RID: 73834 RVA: 0x004FC8D1 File Offset: 0x004FAAD1
		public int LocallyPreConsumedCount { get; set; }

		// Token: 0x170027D9 RID: 10201
		// (get) Token: 0x0601206B RID: 73835 RVA: 0x004FC8DA File Offset: 0x004FAADA
		// (set) Token: 0x0601206C RID: 73836 RVA: 0x004FC8E2 File Offset: 0x004FAAE2
		public int EverGrantedCount { get; set; }
	}

	// Token: 0x020020CB RID: 8395
	public class NetCacheBoosters
	{
		// Token: 0x0601206E RID: 73838 RVA: 0x004FC8EB File Offset: 0x004FAAEB
		public NetCacheBoosters()
		{
			this.BoosterStacks = new List<NetCache.BoosterStack>();
		}

		// Token: 0x170027DA RID: 10202
		// (get) Token: 0x0601206F RID: 73839 RVA: 0x004FC8FE File Offset: 0x004FAAFE
		// (set) Token: 0x06012070 RID: 73840 RVA: 0x004FC906 File Offset: 0x004FAB06
		public List<NetCache.BoosterStack> BoosterStacks { get; set; }

		// Token: 0x06012071 RID: 73841 RVA: 0x004FC910 File Offset: 0x004FAB10
		public NetCache.BoosterStack GetBoosterStack(int id)
		{
			return this.BoosterStacks.Find((NetCache.BoosterStack obj) => obj.Id == id);
		}

		// Token: 0x06012072 RID: 73842 RVA: 0x004FC944 File Offset: 0x004FAB44
		public int GetTotalNumBoosters()
		{
			int num = 0;
			foreach (NetCache.BoosterStack boosterStack in this.BoosterStacks)
			{
				num += boosterStack.Count;
			}
			return num;
		}
	}

	// Token: 0x020020CC RID: 8396
	public class DeckHeader
	{
		// Token: 0x170027DB RID: 10203
		// (get) Token: 0x06012073 RID: 73843 RVA: 0x004FC99C File Offset: 0x004FAB9C
		// (set) Token: 0x06012074 RID: 73844 RVA: 0x004FC9A4 File Offset: 0x004FABA4
		public long ID { get; set; }

		// Token: 0x170027DC RID: 10204
		// (get) Token: 0x06012075 RID: 73845 RVA: 0x004FC9AD File Offset: 0x004FABAD
		// (set) Token: 0x06012076 RID: 73846 RVA: 0x004FC9B5 File Offset: 0x004FABB5
		public string Name { get; set; }

		// Token: 0x170027DD RID: 10205
		// (get) Token: 0x06012077 RID: 73847 RVA: 0x004FC9BE File Offset: 0x004FABBE
		// (set) Token: 0x06012078 RID: 73848 RVA: 0x004FC9C6 File Offset: 0x004FABC6
		public int CardBack { get; set; }

		// Token: 0x170027DE RID: 10206
		// (get) Token: 0x06012079 RID: 73849 RVA: 0x004FC9CF File Offset: 0x004FABCF
		// (set) Token: 0x0601207A RID: 73850 RVA: 0x004FC9D7 File Offset: 0x004FABD7
		public string Hero { get; set; }

		// Token: 0x170027DF RID: 10207
		// (get) Token: 0x0601207B RID: 73851 RVA: 0x004FC9E0 File Offset: 0x004FABE0
		// (set) Token: 0x0601207C RID: 73852 RVA: 0x004FC9E8 File Offset: 0x004FABE8
		public TAG_PREMIUM HeroPremium { get; set; }

		// Token: 0x170027E0 RID: 10208
		// (get) Token: 0x0601207D RID: 73853 RVA: 0x004FC9F1 File Offset: 0x004FABF1
		// (set) Token: 0x0601207E RID: 73854 RVA: 0x004FC9F9 File Offset: 0x004FABF9
		public string UIHeroOverride { get; set; }

		// Token: 0x170027E1 RID: 10209
		// (get) Token: 0x0601207F RID: 73855 RVA: 0x004FCA02 File Offset: 0x004FAC02
		// (set) Token: 0x06012080 RID: 73856 RVA: 0x004FCA0A File Offset: 0x004FAC0A
		public TAG_PREMIUM UIHeroOverridePremium { get; set; }

		// Token: 0x170027E2 RID: 10210
		// (get) Token: 0x06012081 RID: 73857 RVA: 0x004FCA13 File Offset: 0x004FAC13
		// (set) Token: 0x06012082 RID: 73858 RVA: 0x004FCA1B File Offset: 0x004FAC1B
		public string HeroPower { get; set; }

		// Token: 0x170027E3 RID: 10211
		// (get) Token: 0x06012083 RID: 73859 RVA: 0x004FCA24 File Offset: 0x004FAC24
		// (set) Token: 0x06012084 RID: 73860 RVA: 0x004FCA2C File Offset: 0x004FAC2C
		public DeckType Type { get; set; }

		// Token: 0x170027E4 RID: 10212
		// (get) Token: 0x06012085 RID: 73861 RVA: 0x004FCA35 File Offset: 0x004FAC35
		// (set) Token: 0x06012086 RID: 73862 RVA: 0x004FCA3D File Offset: 0x004FAC3D
		public bool CardBackOverridden { get; set; }

		// Token: 0x170027E5 RID: 10213
		// (get) Token: 0x06012087 RID: 73863 RVA: 0x004FCA46 File Offset: 0x004FAC46
		// (set) Token: 0x06012088 RID: 73864 RVA: 0x004FCA4E File Offset: 0x004FAC4E
		public bool HeroOverridden { get; set; }

		// Token: 0x170027E6 RID: 10214
		// (get) Token: 0x06012089 RID: 73865 RVA: 0x004FCA57 File Offset: 0x004FAC57
		// (set) Token: 0x0601208A RID: 73866 RVA: 0x004FCA5F File Offset: 0x004FAC5F
		public int SeasonId { get; set; }

		// Token: 0x170027E7 RID: 10215
		// (get) Token: 0x0601208B RID: 73867 RVA: 0x004FCA68 File Offset: 0x004FAC68
		// (set) Token: 0x0601208C RID: 73868 RVA: 0x004FCA70 File Offset: 0x004FAC70
		public int BrawlLibraryItemId { get; set; }

		// Token: 0x170027E8 RID: 10216
		// (get) Token: 0x0601208D RID: 73869 RVA: 0x004FCA79 File Offset: 0x004FAC79
		// (set) Token: 0x0601208E RID: 73870 RVA: 0x004FCA81 File Offset: 0x004FAC81
		public bool NeedsName { get; set; }

		// Token: 0x170027E9 RID: 10217
		// (get) Token: 0x0601208F RID: 73871 RVA: 0x004FCA8A File Offset: 0x004FAC8A
		// (set) Token: 0x06012090 RID: 73872 RVA: 0x004FCA92 File Offset: 0x004FAC92
		public long SortOrder { get; set; }

		// Token: 0x170027EA RID: 10218
		// (get) Token: 0x06012091 RID: 73873 RVA: 0x004FCA9B File Offset: 0x004FAC9B
		// (set) Token: 0x06012092 RID: 73874 RVA: 0x004FCAA3 File Offset: 0x004FACA3
		public PegasusShared.FormatType FormatType { get; set; }

		// Token: 0x170027EB RID: 10219
		// (get) Token: 0x06012093 RID: 73875 RVA: 0x004FCAAC File Offset: 0x004FACAC
		// (set) Token: 0x06012094 RID: 73876 RVA: 0x004FCAB4 File Offset: 0x004FACB4
		public bool Locked { get; set; }

		// Token: 0x170027EC RID: 10220
		// (get) Token: 0x06012095 RID: 73877 RVA: 0x004FCABD File Offset: 0x004FACBD
		// (set) Token: 0x06012096 RID: 73878 RVA: 0x004FCAC5 File Offset: 0x004FACC5
		public DeckSourceType SourceType { get; set; }

		// Token: 0x170027ED RID: 10221
		// (get) Token: 0x06012097 RID: 73879 RVA: 0x004FCACE File Offset: 0x004FACCE
		// (set) Token: 0x06012098 RID: 73880 RVA: 0x004FCAD6 File Offset: 0x004FACD6
		public DateTime? CreateDate { get; set; }

		// Token: 0x170027EE RID: 10222
		// (get) Token: 0x06012099 RID: 73881 RVA: 0x004FCADF File Offset: 0x004FACDF
		// (set) Token: 0x0601209A RID: 73882 RVA: 0x004FCAE7 File Offset: 0x004FACE7
		public DateTime? LastModified { get; set; }

		// Token: 0x0601209B RID: 73883 RVA: 0x004FCAF0 File Offset: 0x004FACF0
		public override string ToString()
		{
			return string.Format("[DeckHeader: ID={0} Name={1} Hero={2} HeroPremium={3} HeroPower={4} DeckType={5} CardBack={6} CardBackOverridden={7} HeroOverridden={8} NeedsName={9} SortOrder={10} SourceType={11} LastModified={12} CreateDate={13}]", new object[]
			{
				this.ID,
				this.Name,
				this.Hero,
				this.HeroPremium,
				this.HeroPower,
				this.Type,
				this.CardBack,
				this.CardBackOverridden,
				this.HeroOverridden,
				this.NeedsName,
				this.SortOrder,
				this.SourceType,
				this.CreateDate,
				this.LastModified
			});
		}
	}

	// Token: 0x020020CD RID: 8397
	public class NetCacheDecks
	{
		// Token: 0x0601209D RID: 73885 RVA: 0x004FCBC8 File Offset: 0x004FADC8
		public NetCacheDecks()
		{
			this.Decks = new List<NetCache.DeckHeader>();
		}

		// Token: 0x170027EF RID: 10223
		// (get) Token: 0x0601209E RID: 73886 RVA: 0x004FCBDB File Offset: 0x004FADDB
		// (set) Token: 0x0601209F RID: 73887 RVA: 0x004FCBE3 File Offset: 0x004FADE3
		public List<NetCache.DeckHeader> Decks { get; set; }
	}

	// Token: 0x020020CE RID: 8398
	public class CardDefinition
	{
		// Token: 0x060120A0 RID: 73888 RVA: 0x004FCBEC File Offset: 0x004FADEC
		public override bool Equals(object obj)
		{
			NetCache.CardDefinition cardDefinition = obj as NetCache.CardDefinition;
			return cardDefinition != null && this.Premium == cardDefinition.Premium && this.Name.Equals(cardDefinition.Name);
		}

		// Token: 0x060120A1 RID: 73889 RVA: 0x004FCC24 File Offset: 0x004FAE24
		public override int GetHashCode()
		{
			return (int)(this.Name.GetHashCode() + this.Premium);
		}

		// Token: 0x060120A2 RID: 73890 RVA: 0x004FCC38 File Offset: 0x004FAE38
		public override string ToString()
		{
			return string.Format("[CardDefinition: Name={0}, Premium={1}]", this.Name, this.Premium);
		}

		// Token: 0x170027F0 RID: 10224
		// (get) Token: 0x060120A3 RID: 73891 RVA: 0x004FCC55 File Offset: 0x004FAE55
		// (set) Token: 0x060120A4 RID: 73892 RVA: 0x004FCC5D File Offset: 0x004FAE5D
		public string Name { get; set; }

		// Token: 0x170027F1 RID: 10225
		// (get) Token: 0x060120A5 RID: 73893 RVA: 0x004FCC66 File Offset: 0x004FAE66
		// (set) Token: 0x060120A6 RID: 73894 RVA: 0x004FCC6E File Offset: 0x004FAE6E
		public TAG_PREMIUM Premium { get; set; }
	}

	// Token: 0x020020CF RID: 8399
	public class CardValue
	{
		// Token: 0x170027F2 RID: 10226
		// (get) Token: 0x060120A8 RID: 73896 RVA: 0x004FCC77 File Offset: 0x004FAE77
		// (set) Token: 0x060120A9 RID: 73897 RVA: 0x004FCC7F File Offset: 0x004FAE7F
		public int BaseBuyValue { get; set; }

		// Token: 0x170027F3 RID: 10227
		// (get) Token: 0x060120AA RID: 73898 RVA: 0x004FCC88 File Offset: 0x004FAE88
		// (set) Token: 0x060120AB RID: 73899 RVA: 0x004FCC90 File Offset: 0x004FAE90
		public int BaseSellValue { get; set; }

		// Token: 0x170027F4 RID: 10228
		// (get) Token: 0x060120AC RID: 73900 RVA: 0x004FCC99 File Offset: 0x004FAE99
		// (set) Token: 0x060120AD RID: 73901 RVA: 0x004FCCA1 File Offset: 0x004FAEA1
		public int BuyValueOverride { get; set; }

		// Token: 0x170027F5 RID: 10229
		// (get) Token: 0x060120AE RID: 73902 RVA: 0x004FCCAA File Offset: 0x004FAEAA
		// (set) Token: 0x060120AF RID: 73903 RVA: 0x004FCCB2 File Offset: 0x004FAEB2
		public int SellValueOverride { get; set; }

		// Token: 0x170027F6 RID: 10230
		// (get) Token: 0x060120B0 RID: 73904 RVA: 0x004FCCBB File Offset: 0x004FAEBB
		// (set) Token: 0x060120B1 RID: 73905 RVA: 0x004FCCC3 File Offset: 0x004FAEC3
		public SpecialEventType OverrideEvent { get; set; }

		// Token: 0x060120B2 RID: 73906 RVA: 0x004FCCCC File Offset: 0x004FAECC
		public int GetBuyValue()
		{
			if (!this.IsOverrideActive())
			{
				return this.BaseBuyValue;
			}
			return this.BuyValueOverride;
		}

		// Token: 0x060120B3 RID: 73907 RVA: 0x004FCCE3 File Offset: 0x004FAEE3
		public int GetSellValue()
		{
			if (!this.IsOverrideActive())
			{
				return this.BaseSellValue;
			}
			return this.SellValueOverride;
		}

		// Token: 0x060120B4 RID: 73908 RVA: 0x004FCCFA File Offset: 0x004FAEFA
		public bool IsOverrideActive()
		{
			return SpecialEventManager.Get().IsEventActive(this.OverrideEvent, false);
		}
	}

	// Token: 0x020020D0 RID: 8400
	public class NetCacheCardValues
	{
		// Token: 0x170027F7 RID: 10231
		// (get) Token: 0x060120B6 RID: 73910 RVA: 0x004FCD0D File Offset: 0x004FAF0D
		// (set) Token: 0x060120B7 RID: 73911 RVA: 0x004FCD15 File Offset: 0x004FAF15
		public global::Map<NetCache.CardDefinition, NetCache.CardValue> Values { get; set; }

		// Token: 0x060120B8 RID: 73912 RVA: 0x004FCD1E File Offset: 0x004FAF1E
		public NetCacheCardValues()
		{
			this.Values = new global::Map<NetCache.CardDefinition, NetCache.CardValue>();
		}
	}

	// Token: 0x020020D1 RID: 8401
	public class NetCacheDisconnectedGame
	{
		// Token: 0x170027F8 RID: 10232
		// (get) Token: 0x060120B9 RID: 73913 RVA: 0x004FCD31 File Offset: 0x004FAF31
		// (set) Token: 0x060120BA RID: 73914 RVA: 0x004FCD39 File Offset: 0x004FAF39
		public GameServerInfo ServerInfo { get; set; }

		// Token: 0x170027F9 RID: 10233
		// (get) Token: 0x060120BB RID: 73915 RVA: 0x004FCD42 File Offset: 0x004FAF42
		// (set) Token: 0x060120BC RID: 73916 RVA: 0x004FCD4A File Offset: 0x004FAF4A
		public PegasusShared.GameType GameType { get; set; }

		// Token: 0x170027FA RID: 10234
		// (get) Token: 0x060120BD RID: 73917 RVA: 0x004FCD53 File Offset: 0x004FAF53
		// (set) Token: 0x060120BE RID: 73918 RVA: 0x004FCD5B File Offset: 0x004FAF5B
		public PegasusShared.FormatType FormatType { get; set; }

		// Token: 0x170027FB RID: 10235
		// (get) Token: 0x060120BF RID: 73919 RVA: 0x004FCD64 File Offset: 0x004FAF64
		// (set) Token: 0x060120C0 RID: 73920 RVA: 0x004FCD6C File Offset: 0x004FAF6C
		public bool LoadGameState { get; set; }
	}

	// Token: 0x020020D2 RID: 8402
	public class BoosterCard
	{
		// Token: 0x170027FC RID: 10236
		// (get) Token: 0x060120C2 RID: 73922 RVA: 0x004FCD75 File Offset: 0x004FAF75
		// (set) Token: 0x060120C3 RID: 73923 RVA: 0x004FCD7D File Offset: 0x004FAF7D
		public NetCache.CardDefinition Def { get; set; }

		// Token: 0x170027FD RID: 10237
		// (get) Token: 0x060120C4 RID: 73924 RVA: 0x004FCD86 File Offset: 0x004FAF86
		// (set) Token: 0x060120C5 RID: 73925 RVA: 0x004FCD8E File Offset: 0x004FAF8E
		public long Date { get; set; }

		// Token: 0x060120C6 RID: 73926 RVA: 0x004FCD97 File Offset: 0x004FAF97
		public BoosterCard()
		{
			this.Def = new NetCache.CardDefinition();
		}
	}

	// Token: 0x020020D3 RID: 8403
	public class CardStack
	{
		// Token: 0x170027FE RID: 10238
		// (get) Token: 0x060120C7 RID: 73927 RVA: 0x004FCDAA File Offset: 0x004FAFAA
		// (set) Token: 0x060120C8 RID: 73928 RVA: 0x004FCDB2 File Offset: 0x004FAFB2
		public NetCache.CardDefinition Def { get; set; }

		// Token: 0x170027FF RID: 10239
		// (get) Token: 0x060120C9 RID: 73929 RVA: 0x004FCDBB File Offset: 0x004FAFBB
		// (set) Token: 0x060120CA RID: 73930 RVA: 0x004FCDC3 File Offset: 0x004FAFC3
		public long Date { get; set; }

		// Token: 0x17002800 RID: 10240
		// (get) Token: 0x060120CB RID: 73931 RVA: 0x004FCDCC File Offset: 0x004FAFCC
		// (set) Token: 0x060120CC RID: 73932 RVA: 0x004FCDD4 File Offset: 0x004FAFD4
		public int Count { get; set; }

		// Token: 0x17002801 RID: 10241
		// (get) Token: 0x060120CD RID: 73933 RVA: 0x004FCDDD File Offset: 0x004FAFDD
		// (set) Token: 0x060120CE RID: 73934 RVA: 0x004FCDE5 File Offset: 0x004FAFE5
		public int NumSeen { get; set; }

		// Token: 0x060120CF RID: 73935 RVA: 0x004FCDEE File Offset: 0x004FAFEE
		public CardStack()
		{
			this.Def = new NetCache.CardDefinition();
		}
	}

	// Token: 0x020020D4 RID: 8404
	public class NetCacheCollection
	{
		// Token: 0x060120D0 RID: 73936 RVA: 0x004FCE04 File Offset: 0x004FB004
		public NetCacheCollection()
		{
			this.Stacks = new List<NetCache.CardStack>();
			foreach (object obj in Enum.GetValues(typeof(TAG_CLASS)))
			{
				TAG_CLASS key = (TAG_CLASS)obj;
				this.CoreCardsUnlockedPerClass[key] = new HashSet<string>();
			}
		}

		// Token: 0x17002802 RID: 10242
		// (get) Token: 0x060120D1 RID: 73937 RVA: 0x004FCE8C File Offset: 0x004FB08C
		// (set) Token: 0x060120D2 RID: 73938 RVA: 0x004FCE94 File Offset: 0x004FB094
		public List<NetCache.CardStack> Stacks { get; set; }

		// Token: 0x0400DE9E RID: 56990
		public int TotalCardsOwned;

		// Token: 0x0400DE9F RID: 56991
		public global::Map<TAG_CLASS, HashSet<string>> CoreCardsUnlockedPerClass = new global::Map<TAG_CLASS, HashSet<string>>();
	}

	// Token: 0x020020D5 RID: 8405
	public class PlayerRecord
	{
		// Token: 0x17002803 RID: 10243
		// (get) Token: 0x060120D3 RID: 73939 RVA: 0x004FCE9D File Offset: 0x004FB09D
		// (set) Token: 0x060120D4 RID: 73940 RVA: 0x004FCEA5 File Offset: 0x004FB0A5
		public PegasusShared.GameType RecordType { get; set; }

		// Token: 0x17002804 RID: 10244
		// (get) Token: 0x060120D5 RID: 73941 RVA: 0x004FCEAE File Offset: 0x004FB0AE
		// (set) Token: 0x060120D6 RID: 73942 RVA: 0x004FCEB6 File Offset: 0x004FB0B6
		public int Data { get; set; }

		// Token: 0x17002805 RID: 10245
		// (get) Token: 0x060120D7 RID: 73943 RVA: 0x004FCEBF File Offset: 0x004FB0BF
		// (set) Token: 0x060120D8 RID: 73944 RVA: 0x004FCEC7 File Offset: 0x004FB0C7
		public int Wins { get; set; }

		// Token: 0x17002806 RID: 10246
		// (get) Token: 0x060120D9 RID: 73945 RVA: 0x004FCED0 File Offset: 0x004FB0D0
		// (set) Token: 0x060120DA RID: 73946 RVA: 0x004FCED8 File Offset: 0x004FB0D8
		public int Losses { get; set; }

		// Token: 0x17002807 RID: 10247
		// (get) Token: 0x060120DB RID: 73947 RVA: 0x004FCEE1 File Offset: 0x004FB0E1
		// (set) Token: 0x060120DC RID: 73948 RVA: 0x004FCEE9 File Offset: 0x004FB0E9
		public int Ties { get; set; }
	}

	// Token: 0x020020D6 RID: 8406
	public class NetCachePlayerRecords
	{
		// Token: 0x060120DE RID: 73950 RVA: 0x004FCEF2 File Offset: 0x004FB0F2
		public NetCachePlayerRecords()
		{
			this.Records = new List<NetCache.PlayerRecord>();
		}

		// Token: 0x17002808 RID: 10248
		// (get) Token: 0x060120DF RID: 73951 RVA: 0x004FCF05 File Offset: 0x004FB105
		// (set) Token: 0x060120E0 RID: 73952 RVA: 0x004FCF0D File Offset: 0x004FB10D
		public List<NetCache.PlayerRecord> Records { get; set; }
	}

	// Token: 0x020020D7 RID: 8407
	public class NetCacheRewardProgress
	{
		// Token: 0x17002809 RID: 10249
		// (get) Token: 0x060120E1 RID: 73953 RVA: 0x004FCF16 File Offset: 0x004FB116
		// (set) Token: 0x060120E2 RID: 73954 RVA: 0x004FCF1E File Offset: 0x004FB11E
		public int Season { get; set; }

		// Token: 0x1700280A RID: 10250
		// (get) Token: 0x060120E3 RID: 73955 RVA: 0x004FCF27 File Offset: 0x004FB127
		// (set) Token: 0x060120E4 RID: 73956 RVA: 0x004FCF2F File Offset: 0x004FB12F
		public long SeasonEndDate { get; set; }

		// Token: 0x1700280B RID: 10251
		// (get) Token: 0x060120E5 RID: 73957 RVA: 0x004FCF38 File Offset: 0x004FB138
		// (set) Token: 0x060120E6 RID: 73958 RVA: 0x004FCF40 File Offset: 0x004FB140
		public long NextQuestCancelDate { get; set; }
	}

	// Token: 0x020020D8 RID: 8408
	public class NetCacheMedalInfo
	{
		// Token: 0x1700280C RID: 10252
		// (get) Token: 0x060120E8 RID: 73960 RVA: 0x004FCF49 File Offset: 0x004FB149
		// (set) Token: 0x060120E9 RID: 73961 RVA: 0x004FCF51 File Offset: 0x004FB151
		public NetCache.NetCacheMedalInfo PreviousMedalInfo { get; set; }

		// Token: 0x060120EA RID: 73962 RVA: 0x004FCF5A File Offset: 0x004FB15A
		public NetCacheMedalInfo()
		{
		}

		// Token: 0x060120EB RID: 73963 RVA: 0x004FCF70 File Offset: 0x004FB170
		public NetCacheMedalInfo(MedalInfo packet)
		{
			foreach (MedalInfoData medalInfoData in packet.MedalData)
			{
				this.MedalData.Add(medalInfoData.FormatType, medalInfoData);
			}
		}

		// Token: 0x060120EC RID: 73964 RVA: 0x004FCFE0 File Offset: 0x004FB1E0
		public NetCache.NetCacheMedalInfo Clone()
		{
			NetCache.NetCacheMedalInfo netCacheMedalInfo = new NetCache.NetCacheMedalInfo();
			foreach (KeyValuePair<PegasusShared.FormatType, MedalInfoData> keyValuePair in this.MedalData)
			{
				netCacheMedalInfo.MedalData.Add(keyValuePair.Key, NetCache.NetCacheMedalInfo.CloneMedalInfoData(keyValuePair.Value));
			}
			return netCacheMedalInfo;
		}

		// Token: 0x060120ED RID: 73965 RVA: 0x004FD054 File Offset: 0x004FB254
		public MedalInfoData GetMedalInfoData(PegasusShared.FormatType formatType)
		{
			MedalInfoData result;
			if (!this.MedalData.TryGetValue(formatType, out result))
			{
				Debug.LogError("NetCacheMedalInfo.GetMedalInfoData failed to find data for the format type " + formatType.ToString() + ". Returning null");
			}
			return result;
		}

		// Token: 0x060120EE RID: 73966 RVA: 0x004FD093 File Offset: 0x004FB293
		public global::Map<PegasusShared.FormatType, MedalInfoData>.ValueCollection GetAllMedalInfoData()
		{
			return this.MedalData.Values;
		}

		// Token: 0x060120EF RID: 73967 RVA: 0x004FD0A0 File Offset: 0x004FB2A0
		public static MedalInfoData CloneMedalInfoData(MedalInfoData original)
		{
			MedalInfoData medalInfoData = new MedalInfoData();
			medalInfoData.LeagueId = original.LeagueId;
			medalInfoData.SeasonWins = original.SeasonWins;
			medalInfoData.Stars = original.Stars;
			medalInfoData.Streak = original.Streak;
			medalInfoData.StarLevel = original.StarLevel;
			medalInfoData.HasLegendRank = original.HasLegendRank;
			medalInfoData.LegendRank = original.LegendRank;
			medalInfoData.HasBestStarLevel = original.HasBestStarLevel;
			medalInfoData.BestStarLevel = original.BestStarLevel;
			medalInfoData.HasSeasonGames = original.HasSeasonGames;
			medalInfoData.SeasonGames = original.SeasonGames;
			medalInfoData.StarsPerWin = original.StarsPerWin;
			if (original.HasRatingId)
			{
				medalInfoData.RatingId = original.RatingId;
			}
			if (original.HasSeasonId)
			{
				medalInfoData.SeasonId = original.SeasonId;
			}
			if (original.HasRating)
			{
				medalInfoData.Rating = original.Rating;
			}
			if (original.HasVariance)
			{
				medalInfoData.Variance = original.Variance;
			}
			if (original.HasBestStars)
			{
				medalInfoData.BestStars = original.BestStars;
			}
			if (original.HasBestEverLeagueId)
			{
				medalInfoData.BestEverLeagueId = original.BestEverLeagueId;
			}
			if (original.HasBestEverStarLevel)
			{
				medalInfoData.BestEverStarLevel = original.BestEverStarLevel;
			}
			if (original.HasBestRating)
			{
				medalInfoData.BestRating = original.BestRating;
			}
			if (original.HasPublicRating)
			{
				medalInfoData.PublicRating = original.PublicRating;
			}
			if (original.HasRatingAdjustment)
			{
				medalInfoData.RatingAdjustment = original.RatingAdjustment;
			}
			if (original.HasRatingAdjustmentWins)
			{
				medalInfoData.RatingAdjustmentWins = original.RatingAdjustmentWins;
			}
			if (original.HasFormatType)
			{
				medalInfoData.FormatType = original.FormatType;
			}
			return medalInfoData;
		}

		// Token: 0x060120F0 RID: 73968 RVA: 0x004FD234 File Offset: 0x004FB434
		public override string ToString()
		{
			return string.Format("[NetCacheMedalInfo] \n MedalData={0}", this.MedalData.ToString());
		}

		// Token: 0x0400DEA9 RID: 57001
		public global::Map<PegasusShared.FormatType, MedalInfoData> MedalData = new global::Map<PegasusShared.FormatType, MedalInfoData>();
	}

	// Token: 0x020020D9 RID: 8409
	public class NetCacheBaconRatingInfo
	{
		// Token: 0x1700280D RID: 10253
		// (get) Token: 0x060120F1 RID: 73969 RVA: 0x004FD24B File Offset: 0x004FB44B
		// (set) Token: 0x060120F2 RID: 73970 RVA: 0x004FD253 File Offset: 0x004FB453
		public int Rating { get; set; }

		// Token: 0x060120F3 RID: 73971 RVA: 0x004FD25C File Offset: 0x004FB45C
		public NetCache.NetCacheBaconRatingInfo Clone()
		{
			return new NetCache.NetCacheBaconRatingInfo
			{
				Rating = this.Rating
			};
		}

		// Token: 0x060120F4 RID: 73972 RVA: 0x004FD26F File Offset: 0x004FB46F
		public override string ToString()
		{
			return string.Format("[NetCacheBaconRatingInfo] \n Rating={0}", this.Rating);
		}
	}

	// Token: 0x020020DA RID: 8410
	public class NetCacheBaconPremiumStatus
	{
		// Token: 0x1700280E RID: 10254
		// (get) Token: 0x060120F6 RID: 73974 RVA: 0x004FD286 File Offset: 0x004FB486
		// (set) Token: 0x060120F7 RID: 73975 RVA: 0x004FD28E File Offset: 0x004FB48E
		public List<BattlegroundSeasonPremiumStatus> SeasonPremiumStatus { get; set; }

		// Token: 0x060120F8 RID: 73976 RVA: 0x004FD297 File Offset: 0x004FB497
		public override string ToString()
		{
			return string.Format("[NetCacheBaconPremiumStatus] \n SeasonPremiumStatus={0}", this.SeasonPremiumStatus);
		}
	}

	// Token: 0x020020DB RID: 8411
	public class NetCachePVPDRStatsInfo
	{
		// Token: 0x1700280F RID: 10255
		// (get) Token: 0x060120FA RID: 73978 RVA: 0x004FD2A9 File Offset: 0x004FB4A9
		// (set) Token: 0x060120FB RID: 73979 RVA: 0x004FD2B1 File Offset: 0x004FB4B1
		public int Rating { get; set; }

		// Token: 0x17002810 RID: 10256
		// (get) Token: 0x060120FC RID: 73980 RVA: 0x004FD2BA File Offset: 0x004FB4BA
		// (set) Token: 0x060120FD RID: 73981 RVA: 0x004FD2C2 File Offset: 0x004FB4C2
		public int PaidRating { get; set; }

		// Token: 0x17002811 RID: 10257
		// (get) Token: 0x060120FE RID: 73982 RVA: 0x004FD2CB File Offset: 0x004FB4CB
		// (set) Token: 0x060120FF RID: 73983 RVA: 0x004FD2D3 File Offset: 0x004FB4D3
		public int HighWatermark { get; set; }

		// Token: 0x06012100 RID: 73984 RVA: 0x004FD2DC File Offset: 0x004FB4DC
		public NetCache.NetCachePVPDRStatsInfo Clone()
		{
			return new NetCache.NetCachePVPDRStatsInfo
			{
				Rating = this.Rating,
				PaidRating = this.PaidRating,
				HighWatermark = this.HighWatermark
			};
		}

		// Token: 0x06012101 RID: 73985 RVA: 0x004FD307 File Offset: 0x004FB507
		public override string ToString()
		{
			return string.Format("[NetCachePVPDRStatsInfo] \n Rating={0} PaidRating={1} HighWatermark={2}", this.Rating, this.PaidRating, this.HighWatermark);
		}
	}

	// Token: 0x020020DC RID: 8412
	public abstract class ProfileNotice
	{
		// Token: 0x06012103 RID: 73987 RVA: 0x004FD334 File Offset: 0x004FB534
		protected ProfileNotice(NetCache.ProfileNotice.NoticeType init)
		{
			this.m_type = init;
			this.NoticeID = 0L;
			this.Origin = NetCache.ProfileNotice.NoticeOrigin.UNKNOWN;
			this.OriginData = 0L;
			this.Date = 0L;
		}

		// Token: 0x17002812 RID: 10258
		// (get) Token: 0x06012104 RID: 73988 RVA: 0x004FD362 File Offset: 0x004FB562
		// (set) Token: 0x06012105 RID: 73989 RVA: 0x004FD36A File Offset: 0x004FB56A
		public long NoticeID { get; set; }

		// Token: 0x17002813 RID: 10259
		// (get) Token: 0x06012106 RID: 73990 RVA: 0x004FD373 File Offset: 0x004FB573
		public NetCache.ProfileNotice.NoticeType Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17002814 RID: 10260
		// (get) Token: 0x06012107 RID: 73991 RVA: 0x004FD37B File Offset: 0x004FB57B
		// (set) Token: 0x06012108 RID: 73992 RVA: 0x004FD383 File Offset: 0x004FB583
		public NetCache.ProfileNotice.NoticeOrigin Origin { get; set; }

		// Token: 0x17002815 RID: 10261
		// (get) Token: 0x06012109 RID: 73993 RVA: 0x004FD38C File Offset: 0x004FB58C
		// (set) Token: 0x0601210A RID: 73994 RVA: 0x004FD394 File Offset: 0x004FB594
		public long OriginData { get; set; }

		// Token: 0x17002816 RID: 10262
		// (get) Token: 0x0601210B RID: 73995 RVA: 0x004FD39D File Offset: 0x004FB59D
		// (set) Token: 0x0601210C RID: 73996 RVA: 0x004FD3A5 File Offset: 0x004FB5A5
		public long Date { get; set; }

		// Token: 0x0601210D RID: 73997 RVA: 0x004FD3B0 File Offset: 0x004FB5B0
		public override string ToString()
		{
			return string.Format("[{0}: NoticeID={1}, Type={2}, Origin={3}, OriginData={4}, Date={5}]", new object[]
			{
				base.GetType(),
				this.NoticeID,
				this.Type,
				this.Origin,
				this.OriginData,
				this.Date
			});
		}

		// Token: 0x0400DEB0 RID: 57008
		private NetCache.ProfileNotice.NoticeType m_type;

		// Token: 0x020029A1 RID: 10657
		public enum NoticeType
		{
			// Token: 0x0400FDAF RID: 64943
			GAINED_MEDAL = 1,
			// Token: 0x0400FDB0 RID: 64944
			REWARD_BOOSTER,
			// Token: 0x0400FDB1 RID: 64945
			REWARD_CARD,
			// Token: 0x0400FDB2 RID: 64946
			DISCONNECTED_GAME,
			// Token: 0x0400FDB3 RID: 64947
			PRECON_DECK,
			// Token: 0x0400FDB4 RID: 64948
			REWARD_DUST,
			// Token: 0x0400FDB5 RID: 64949
			REWARD_MOUNT,
			// Token: 0x0400FDB6 RID: 64950
			REWARD_FORGE,
			// Token: 0x0400FDB7 RID: 64951
			REWARD_CURRENCY,
			// Token: 0x0400FDB8 RID: 64952
			PURCHASE,
			// Token: 0x0400FDB9 RID: 64953
			REWARD_CARD_BACK,
			// Token: 0x0400FDBA RID: 64954
			BONUS_STARS,
			// Token: 0x0400FDBB RID: 64955
			ADVENTURE_PROGRESS = 14,
			// Token: 0x0400FDBC RID: 64956
			HERO_LEVEL_UP,
			// Token: 0x0400FDBD RID: 64957
			ACCOUNT_LICENSE,
			// Token: 0x0400FDBE RID: 64958
			TAVERN_BRAWL_REWARDS,
			// Token: 0x0400FDBF RID: 64959
			TAVERN_BRAWL_TICKET,
			// Token: 0x0400FDC0 RID: 64960
			EVENT,
			// Token: 0x0400FDC1 RID: 64961
			GENERIC_REWARD_CHEST,
			// Token: 0x0400FDC2 RID: 64962
			LEAGUE_PROMOTION_REWARDS,
			// Token: 0x0400FDC3 RID: 64963
			CARD_REPLACEMENT,
			// Token: 0x0400FDC4 RID: 64964
			DISCONNECTED_GAME_NEW,
			// Token: 0x0400FDC5 RID: 64965
			FREE_DECK_CHOICE,
			// Token: 0x0400FDC6 RID: 64966
			DECK_REMOVED,
			// Token: 0x0400FDC7 RID: 64967
			DECK_GRANTED,
			// Token: 0x0400FDC8 RID: 64968
			MINI_SET_GRANTED,
			// Token: 0x0400FDC9 RID: 64969
			SELLABLE_DECK_GRANTED
		}

		// Token: 0x020029A2 RID: 10658
		public enum NoticeOrigin
		{
			// Token: 0x0400FDCB RID: 64971
			UNKNOWN = -1,
			// Token: 0x0400FDCC RID: 64972
			SEASON = 1,
			// Token: 0x0400FDCD RID: 64973
			BETA_REIMBURSE,
			// Token: 0x0400FDCE RID: 64974
			FORGE,
			// Token: 0x0400FDCF RID: 64975
			TOURNEY,
			// Token: 0x0400FDD0 RID: 64976
			PRECON_DECK,
			// Token: 0x0400FDD1 RID: 64977
			ACK,
			// Token: 0x0400FDD2 RID: 64978
			ACHIEVEMENT,
			// Token: 0x0400FDD3 RID: 64979
			LEVEL_UP,
			// Token: 0x0400FDD4 RID: 64980
			PURCHASE_COMPLETE = 10,
			// Token: 0x0400FDD5 RID: 64981
			PURCHASE_FAILED,
			// Token: 0x0400FDD6 RID: 64982
			PURCHASE_CANCELED,
			// Token: 0x0400FDD7 RID: 64983
			BLIZZCON,
			// Token: 0x0400FDD8 RID: 64984
			EVENT,
			// Token: 0x0400FDD9 RID: 64985
			DISCONNECTED_GAME,
			// Token: 0x0400FDDA RID: 64986
			OUT_OF_BAND_LICENSE,
			// Token: 0x0400FDDB RID: 64987
			IGR,
			// Token: 0x0400FDDC RID: 64988
			ADVENTURE_PROGRESS,
			// Token: 0x0400FDDD RID: 64989
			ADVENTURE_FLAGS,
			// Token: 0x0400FDDE RID: 64990
			TAVERN_BRAWL_REWARD,
			// Token: 0x0400FDDF RID: 64991
			ACCOUNT_LICENSE_FLAGS,
			// Token: 0x0400FDE0 RID: 64992
			FROM_PURCHASE,
			// Token: 0x0400FDE1 RID: 64993
			HOF_COMPENSATION,
			// Token: 0x0400FDE2 RID: 64994
			GENERIC_REWARD_CHEST_ACHIEVE,
			// Token: 0x0400FDE3 RID: 64995
			GENERIC_REWARD_CHEST,
			// Token: 0x0400FDE4 RID: 64996
			LEAGUE_PROMOTION,
			// Token: 0x0400FDE5 RID: 64997
			CARD_REPLACEMENT,
			// Token: 0x0400FDE6 RID: 64998
			NOTICE_ORIGIN_LEVEL_UP_MULTIPLE,
			// Token: 0x0400FDE7 RID: 64999
			NOTICE_ORIGIN_DUELS
		}
	}

	// Token: 0x020020DD RID: 8413
	public class ProfileNoticeFreeDeckChoice : NetCache.ProfileNotice
	{
		// Token: 0x0601210E RID: 73998 RVA: 0x004FD41C File Offset: 0x004FB61C
		public ProfileNoticeFreeDeckChoice() : base(NetCache.ProfileNotice.NoticeType.FREE_DECK_CHOICE)
		{
		}

		// Token: 0x17002817 RID: 10263
		// (get) Token: 0x0601210F RID: 73999 RVA: 0x004FD426 File Offset: 0x004FB626
		// (set) Token: 0x06012110 RID: 74000 RVA: 0x004FD42E File Offset: 0x004FB62E
		public long DeckID { get; set; }

		// Token: 0x17002818 RID: 10264
		// (get) Token: 0x06012111 RID: 74001 RVA: 0x004FD437 File Offset: 0x004FB637
		// (set) Token: 0x06012112 RID: 74002 RVA: 0x004FD43F File Offset: 0x004FB63F
		public int ClassID { get; set; }

		// Token: 0x06012113 RID: 74003 RVA: 0x004FD448 File Offset: 0x004FB648
		public override string ToString()
		{
			return string.Format("{0} [DeckID={1}, ClassID={2}]", base.ToString(), this.DeckID, this.ClassID);
		}
	}

	// Token: 0x020020DE RID: 8414
	public class ProfileNoticeMedal : NetCache.ProfileNotice
	{
		// Token: 0x06012114 RID: 74004 RVA: 0x004FD470 File Offset: 0x004FB670
		public ProfileNoticeMedal() : base(NetCache.ProfileNotice.NoticeType.GAINED_MEDAL)
		{
		}

		// Token: 0x17002819 RID: 10265
		// (get) Token: 0x06012115 RID: 74005 RVA: 0x004FD479 File Offset: 0x004FB679
		// (set) Token: 0x06012116 RID: 74006 RVA: 0x004FD481 File Offset: 0x004FB681
		public int LeagueId { get; set; }

		// Token: 0x1700281A RID: 10266
		// (get) Token: 0x06012117 RID: 74007 RVA: 0x004FD48A File Offset: 0x004FB68A
		// (set) Token: 0x06012118 RID: 74008 RVA: 0x004FD492 File Offset: 0x004FB692
		public int StarLevel { get; set; }

		// Token: 0x1700281B RID: 10267
		// (get) Token: 0x06012119 RID: 74009 RVA: 0x004FD49B File Offset: 0x004FB69B
		// (set) Token: 0x0601211A RID: 74010 RVA: 0x004FD4A3 File Offset: 0x004FB6A3
		public int LegendRank { get; set; }

		// Token: 0x1700281C RID: 10268
		// (get) Token: 0x0601211B RID: 74011 RVA: 0x004FD4AC File Offset: 0x004FB6AC
		// (set) Token: 0x0601211C RID: 74012 RVA: 0x004FD4B4 File Offset: 0x004FB6B4
		public int BestStarLevel { get; set; }

		// Token: 0x1700281D RID: 10269
		// (get) Token: 0x0601211D RID: 74013 RVA: 0x004FD4BD File Offset: 0x004FB6BD
		// (set) Token: 0x0601211E RID: 74014 RVA: 0x004FD4C5 File Offset: 0x004FB6C5
		public PegasusShared.FormatType FormatType { get; set; }

		// Token: 0x1700281E RID: 10270
		// (get) Token: 0x0601211F RID: 74015 RVA: 0x004FD4CE File Offset: 0x004FB6CE
		// (set) Token: 0x06012120 RID: 74016 RVA: 0x004FD4D6 File Offset: 0x004FB6D6
		public Network.RewardChest Chest { get; set; }

		// Token: 0x1700281F RID: 10271
		// (get) Token: 0x06012121 RID: 74017 RVA: 0x004FD4DF File Offset: 0x004FB6DF
		// (set) Token: 0x06012122 RID: 74018 RVA: 0x004FD4E7 File Offset: 0x004FB6E7
		public bool WasLimitedByBestEverStarLevel { get; set; }

		// Token: 0x06012123 RID: 74019 RVA: 0x004FD4F0 File Offset: 0x004FB6F0
		public override string ToString()
		{
			return string.Format("{0} [LeagueId={1} StarLevel={2}, LegendRank={3}, BestStarLevel={4}, FormatType={5}, Chest={6}, WasLimitedByBestEverStarLevel={7}]", new object[]
			{
				base.ToString(),
				this.LeagueId,
				this.StarLevel,
				this.LegendRank,
				this.BestStarLevel,
				this.FormatType,
				this.Chest,
				this.WasLimitedByBestEverStarLevel
			});
		}
	}

	// Token: 0x020020DF RID: 8415
	public class ProfileNoticeRewardBooster : NetCache.ProfileNotice
	{
		// Token: 0x06012124 RID: 74020 RVA: 0x004FD573 File Offset: 0x004FB773
		public ProfileNoticeRewardBooster() : base(NetCache.ProfileNotice.NoticeType.REWARD_BOOSTER)
		{
			this.Id = 0;
			this.Count = 0;
		}

		// Token: 0x17002820 RID: 10272
		// (get) Token: 0x06012125 RID: 74021 RVA: 0x004FD58A File Offset: 0x004FB78A
		// (set) Token: 0x06012126 RID: 74022 RVA: 0x004FD592 File Offset: 0x004FB792
		public int Id { get; set; }

		// Token: 0x17002821 RID: 10273
		// (get) Token: 0x06012127 RID: 74023 RVA: 0x004FD59B File Offset: 0x004FB79B
		// (set) Token: 0x06012128 RID: 74024 RVA: 0x004FD5A3 File Offset: 0x004FB7A3
		public int Count { get; set; }

		// Token: 0x06012129 RID: 74025 RVA: 0x004FD5AC File Offset: 0x004FB7AC
		public override string ToString()
		{
			return string.Format("{0} [Id={1}, Count={2}]", base.ToString(), this.Id, this.Count);
		}
	}

	// Token: 0x020020E0 RID: 8416
	public class ProfileNoticeRewardCard : NetCache.ProfileNotice
	{
		// Token: 0x0601212A RID: 74026 RVA: 0x004FD5D4 File Offset: 0x004FB7D4
		public ProfileNoticeRewardCard() : base(NetCache.ProfileNotice.NoticeType.REWARD_CARD)
		{
		}

		// Token: 0x17002822 RID: 10274
		// (get) Token: 0x0601212B RID: 74027 RVA: 0x004FD5DD File Offset: 0x004FB7DD
		// (set) Token: 0x0601212C RID: 74028 RVA: 0x004FD5E5 File Offset: 0x004FB7E5
		public string CardID { get; set; }

		// Token: 0x17002823 RID: 10275
		// (get) Token: 0x0601212D RID: 74029 RVA: 0x004FD5EE File Offset: 0x004FB7EE
		// (set) Token: 0x0601212E RID: 74030 RVA: 0x004FD5F6 File Offset: 0x004FB7F6
		public TAG_PREMIUM Premium { get; set; }

		// Token: 0x17002824 RID: 10276
		// (get) Token: 0x0601212F RID: 74031 RVA: 0x004FD5FF File Offset: 0x004FB7FF
		// (set) Token: 0x06012130 RID: 74032 RVA: 0x004FD607 File Offset: 0x004FB807
		public int Quantity { get; set; }

		// Token: 0x06012131 RID: 74033 RVA: 0x004FD610 File Offset: 0x004FB810
		public override string ToString()
		{
			return string.Format("{0} [CardID={1}, Premium={2}, Quantity={3}]", new object[]
			{
				base.ToString(),
				this.CardID,
				this.Premium,
				this.Quantity
			});
		}
	}

	// Token: 0x020020E1 RID: 8417
	public class ProfileNoticePreconDeck : NetCache.ProfileNotice
	{
		// Token: 0x06012132 RID: 74034 RVA: 0x004FD650 File Offset: 0x004FB850
		public ProfileNoticePreconDeck() : base(NetCache.ProfileNotice.NoticeType.PRECON_DECK)
		{
		}

		// Token: 0x17002825 RID: 10277
		// (get) Token: 0x06012133 RID: 74035 RVA: 0x004FD659 File Offset: 0x004FB859
		// (set) Token: 0x06012134 RID: 74036 RVA: 0x004FD661 File Offset: 0x004FB861
		public long DeckID { get; set; }

		// Token: 0x17002826 RID: 10278
		// (get) Token: 0x06012135 RID: 74037 RVA: 0x004FD66A File Offset: 0x004FB86A
		// (set) Token: 0x06012136 RID: 74038 RVA: 0x004FD672 File Offset: 0x004FB872
		public int HeroAsset { get; set; }

		// Token: 0x06012137 RID: 74039 RVA: 0x004FD67B File Offset: 0x004FB87B
		public override string ToString()
		{
			return string.Format("{0} [DeckID={1}, HeroAsset={2}]", base.ToString(), this.DeckID, this.HeroAsset);
		}
	}

	// Token: 0x020020E2 RID: 8418
	public class ProfileNoticeDeckRemoved : NetCache.ProfileNotice
	{
		// Token: 0x06012138 RID: 74040 RVA: 0x004FD6A3 File Offset: 0x004FB8A3
		public ProfileNoticeDeckRemoved() : base(NetCache.ProfileNotice.NoticeType.DECK_REMOVED)
		{
		}

		// Token: 0x17002827 RID: 10279
		// (get) Token: 0x06012139 RID: 74041 RVA: 0x004FD6AD File Offset: 0x004FB8AD
		// (set) Token: 0x0601213A RID: 74042 RVA: 0x004FD6B5 File Offset: 0x004FB8B5
		public long DeckID { get; set; }

		// Token: 0x0601213B RID: 74043 RVA: 0x004FD6BE File Offset: 0x004FB8BE
		public override string ToString()
		{
			return string.Format("{0} [DeckID={1}]", base.ToString(), this.DeckID);
		}
	}

	// Token: 0x020020E3 RID: 8419
	public class ProfileNoticeDeckGranted : NetCache.ProfileNotice
	{
		// Token: 0x0601213C RID: 74044 RVA: 0x004FD6DB File Offset: 0x004FB8DB
		public ProfileNoticeDeckGranted() : base(NetCache.ProfileNotice.NoticeType.DECK_GRANTED)
		{
		}

		// Token: 0x17002828 RID: 10280
		// (get) Token: 0x0601213D RID: 74045 RVA: 0x004FD6E5 File Offset: 0x004FB8E5
		// (set) Token: 0x0601213E RID: 74046 RVA: 0x004FD6ED File Offset: 0x004FB8ED
		public int DeckDbiID { get; set; }

		// Token: 0x17002829 RID: 10281
		// (get) Token: 0x0601213F RID: 74047 RVA: 0x004FD6F6 File Offset: 0x004FB8F6
		// (set) Token: 0x06012140 RID: 74048 RVA: 0x004FD6FE File Offset: 0x004FB8FE
		public int ClassId { get; set; }

		// Token: 0x1700282A RID: 10282
		// (get) Token: 0x06012141 RID: 74049 RVA: 0x004FD707 File Offset: 0x004FB907
		// (set) Token: 0x06012142 RID: 74050 RVA: 0x004FD70F File Offset: 0x004FB90F
		public long PlayerDeckID { get; set; }

		// Token: 0x06012143 RID: 74051 RVA: 0x004FD718 File Offset: 0x004FB918
		public override string ToString()
		{
			return string.Format("{0} [DeckDbiID={1}, ClassId={2}]", base.ToString(), this.DeckDbiID, this.ClassId);
		}
	}

	// Token: 0x020020E4 RID: 8420
	public class ProfileNoticeMiniSetGranted : NetCache.ProfileNotice
	{
		// Token: 0x06012144 RID: 74052 RVA: 0x004FD740 File Offset: 0x004FB940
		public ProfileNoticeMiniSetGranted() : base(NetCache.ProfileNotice.NoticeType.MINI_SET_GRANTED)
		{
		}

		// Token: 0x1700282B RID: 10283
		// (get) Token: 0x06012145 RID: 74053 RVA: 0x004FD74A File Offset: 0x004FB94A
		// (set) Token: 0x06012146 RID: 74054 RVA: 0x004FD752 File Offset: 0x004FB952
		public int MiniSetID { get; set; }

		// Token: 0x06012147 RID: 74055 RVA: 0x004FD75B File Offset: 0x004FB95B
		public override string ToString()
		{
			return string.Format("{0} [CardsRewardID={1}]", base.ToString(), this.MiniSetID);
		}
	}

	// Token: 0x020020E5 RID: 8421
	public class ProfileNoticeSellableDeckGranted : NetCache.ProfileNotice
	{
		// Token: 0x06012148 RID: 74056 RVA: 0x004FD778 File Offset: 0x004FB978
		public ProfileNoticeSellableDeckGranted() : base(NetCache.ProfileNotice.NoticeType.SELLABLE_DECK_GRANTED)
		{
		}

		// Token: 0x1700282C RID: 10284
		// (get) Token: 0x06012149 RID: 74057 RVA: 0x004FD782 File Offset: 0x004FB982
		// (set) Token: 0x0601214A RID: 74058 RVA: 0x004FD78A File Offset: 0x004FB98A
		public int SellableDeckID { get; set; }

		// Token: 0x1700282D RID: 10285
		// (get) Token: 0x0601214B RID: 74059 RVA: 0x004FD793 File Offset: 0x004FB993
		// (set) Token: 0x0601214C RID: 74060 RVA: 0x004FD79B File Offset: 0x004FB99B
		public bool WasDeckGranted { get; set; }

		// Token: 0x1700282E RID: 10286
		// (get) Token: 0x0601214D RID: 74061 RVA: 0x004FD7A4 File Offset: 0x004FB9A4
		// (set) Token: 0x0601214E RID: 74062 RVA: 0x004FD7AC File Offset: 0x004FB9AC
		public long PlayerDeckID { get; set; }

		// Token: 0x0601214F RID: 74063 RVA: 0x004FD7B5 File Offset: 0x004FB9B5
		public override string ToString()
		{
			return string.Format("{0} [SellableDeckRewardID={1}, WasDeckGranted={2}]", base.ToString(), this.SellableDeckID, this.WasDeckGranted);
		}
	}

	// Token: 0x020020E6 RID: 8422
	public class ProfileNoticeRewardDust : NetCache.ProfileNotice
	{
		// Token: 0x06012150 RID: 74064 RVA: 0x004FD7DD File Offset: 0x004FB9DD
		public ProfileNoticeRewardDust() : base(NetCache.ProfileNotice.NoticeType.REWARD_DUST)
		{
		}

		// Token: 0x1700282F RID: 10287
		// (get) Token: 0x06012151 RID: 74065 RVA: 0x004FD7E6 File Offset: 0x004FB9E6
		// (set) Token: 0x06012152 RID: 74066 RVA: 0x004FD7EE File Offset: 0x004FB9EE
		public int Amount { get; set; }

		// Token: 0x06012153 RID: 74067 RVA: 0x004FD7F7 File Offset: 0x004FB9F7
		public override string ToString()
		{
			return string.Format("{0} [Amount={1}]", base.ToString(), this.Amount);
		}
	}

	// Token: 0x020020E7 RID: 8423
	public class ProfileNoticeRewardMount : NetCache.ProfileNotice
	{
		// Token: 0x06012154 RID: 74068 RVA: 0x004FD814 File Offset: 0x004FBA14
		public ProfileNoticeRewardMount() : base(NetCache.ProfileNotice.NoticeType.REWARD_MOUNT)
		{
		}

		// Token: 0x17002830 RID: 10288
		// (get) Token: 0x06012155 RID: 74069 RVA: 0x004FD81D File Offset: 0x004FBA1D
		// (set) Token: 0x06012156 RID: 74070 RVA: 0x004FD825 File Offset: 0x004FBA25
		public int MountID { get; set; }

		// Token: 0x06012157 RID: 74071 RVA: 0x004FD82E File Offset: 0x004FBA2E
		public override string ToString()
		{
			return string.Format("{0} [MountID={1}]", base.ToString(), this.MountID);
		}
	}

	// Token: 0x020020E8 RID: 8424
	public class ProfileNoticeRewardForge : NetCache.ProfileNotice
	{
		// Token: 0x06012158 RID: 74072 RVA: 0x004FD84B File Offset: 0x004FBA4B
		public ProfileNoticeRewardForge() : base(NetCache.ProfileNotice.NoticeType.REWARD_FORGE)
		{
		}

		// Token: 0x17002831 RID: 10289
		// (get) Token: 0x06012159 RID: 74073 RVA: 0x004FD854 File Offset: 0x004FBA54
		// (set) Token: 0x0601215A RID: 74074 RVA: 0x004FD85C File Offset: 0x004FBA5C
		public int Quantity { get; set; }

		// Token: 0x0601215B RID: 74075 RVA: 0x004FD865 File Offset: 0x004FBA65
		public override string ToString()
		{
			return string.Format("{0} [Quantity={1}]", base.ToString(), this.Quantity);
		}
	}

	// Token: 0x020020E9 RID: 8425
	public class ProfileNoticeRewardCurrency : NetCache.ProfileNotice
	{
		// Token: 0x0601215C RID: 74076 RVA: 0x004FD882 File Offset: 0x004FBA82
		public ProfileNoticeRewardCurrency() : base(NetCache.ProfileNotice.NoticeType.REWARD_CURRENCY)
		{
		}

		// Token: 0x17002832 RID: 10290
		// (get) Token: 0x0601215D RID: 74077 RVA: 0x004FD88C File Offset: 0x004FBA8C
		// (set) Token: 0x0601215E RID: 74078 RVA: 0x004FD894 File Offset: 0x004FBA94
		public int Amount { get; set; }

		// Token: 0x17002833 RID: 10291
		// (get) Token: 0x0601215F RID: 74079 RVA: 0x004FD89D File Offset: 0x004FBA9D
		// (set) Token: 0x06012160 RID: 74080 RVA: 0x004FD8A5 File Offset: 0x004FBAA5
		public PegasusShared.CurrencyType CurrencyType { get; set; }

		// Token: 0x06012161 RID: 74081 RVA: 0x004FD8B0 File Offset: 0x004FBAB0
		public override string ToString()
		{
			return string.Format("{0} [CurrencyType={1}, Amount={2}]", base.ToString(), this.CurrencyType.ToString(), this.Amount);
		}
	}

	// Token: 0x020020EA RID: 8426
	public class ProfileNoticePurchase : NetCache.ProfileNotice
	{
		// Token: 0x06012162 RID: 74082 RVA: 0x004FD8EC File Offset: 0x004FBAEC
		public ProfileNoticePurchase() : base(NetCache.ProfileNotice.NoticeType.PURCHASE)
		{
		}

		// Token: 0x17002834 RID: 10292
		// (get) Token: 0x06012163 RID: 74083 RVA: 0x004FD8F6 File Offset: 0x004FBAF6
		// (set) Token: 0x06012164 RID: 74084 RVA: 0x004FD8FE File Offset: 0x004FBAFE
		public long? PMTProductID { get; set; }

		// Token: 0x17002835 RID: 10293
		// (get) Token: 0x06012165 RID: 74085 RVA: 0x004FD907 File Offset: 0x004FBB07
		// (set) Token: 0x06012166 RID: 74086 RVA: 0x004FD90F File Offset: 0x004FBB0F
		public string CurrencyCode { get; set; }

		// Token: 0x17002836 RID: 10294
		// (get) Token: 0x06012167 RID: 74087 RVA: 0x004FD918 File Offset: 0x004FBB18
		// (set) Token: 0x06012168 RID: 74088 RVA: 0x004FD920 File Offset: 0x004FBB20
		public long Data { get; set; }

		// Token: 0x06012169 RID: 74089 RVA: 0x004FD92C File Offset: 0x004FBB2C
		public override string ToString()
		{
			return string.Format("[ProfileNoticePurchase: NoticeID={0}, Type={1}, Origin={2}, OriginData={3}, Date={4} PMTProductID='{5}', Data={6} Currency={7}]", new object[]
			{
				base.NoticeID,
				base.Type,
				base.Origin,
				base.OriginData,
				base.Date,
				this.PMTProductID,
				this.Data,
				this.CurrencyCode
			});
		}
	}

	// Token: 0x020020EB RID: 8427
	public class ProfileNoticeRewardCardBack : NetCache.ProfileNotice
	{
		// Token: 0x0601216A RID: 74090 RVA: 0x004FD9B4 File Offset: 0x004FBBB4
		public ProfileNoticeRewardCardBack() : base(NetCache.ProfileNotice.NoticeType.REWARD_CARD_BACK)
		{
		}

		// Token: 0x17002837 RID: 10295
		// (get) Token: 0x0601216B RID: 74091 RVA: 0x004FD9BE File Offset: 0x004FBBBE
		// (set) Token: 0x0601216C RID: 74092 RVA: 0x004FD9C6 File Offset: 0x004FBBC6
		public int CardBackID { get; set; }

		// Token: 0x0601216D RID: 74093 RVA: 0x004FD9D0 File Offset: 0x004FBBD0
		public override string ToString()
		{
			return string.Format("[ProfileNoticePurchase: NoticeID={0}, Type={1}, Origin={2}, OriginData={3}, Date={4} CardBackID={5}]", new object[]
			{
				base.NoticeID,
				base.Type,
				base.Origin,
				base.OriginData,
				base.Date,
				this.CardBackID
			});
		}
	}

	// Token: 0x020020EC RID: 8428
	public class ProfileNoticeBonusStars : NetCache.ProfileNotice
	{
		// Token: 0x0601216E RID: 74094 RVA: 0x004FDA41 File Offset: 0x004FBC41
		public ProfileNoticeBonusStars() : base(NetCache.ProfileNotice.NoticeType.BONUS_STARS)
		{
		}

		// Token: 0x17002838 RID: 10296
		// (get) Token: 0x0601216F RID: 74095 RVA: 0x004FDA4B File Offset: 0x004FBC4B
		// (set) Token: 0x06012170 RID: 74096 RVA: 0x004FDA53 File Offset: 0x004FBC53
		public int StarLevel { get; set; }

		// Token: 0x17002839 RID: 10297
		// (get) Token: 0x06012171 RID: 74097 RVA: 0x004FDA5C File Offset: 0x004FBC5C
		// (set) Token: 0x06012172 RID: 74098 RVA: 0x004FDA64 File Offset: 0x004FBC64
		public int Stars { get; set; }

		// Token: 0x06012173 RID: 74099 RVA: 0x004FDA6D File Offset: 0x004FBC6D
		public override string ToString()
		{
			return string.Format("{0} [StarLevel={1}, Stars={2}]", base.ToString(), this.StarLevel, this.Stars);
		}
	}

	// Token: 0x020020ED RID: 8429
	public class ProfileNoticeEvent : NetCache.ProfileNotice
	{
		// Token: 0x06012174 RID: 74100 RVA: 0x004FDA95 File Offset: 0x004FBC95
		public ProfileNoticeEvent() : base(NetCache.ProfileNotice.NoticeType.EVENT)
		{
		}

		// Token: 0x1700283A RID: 10298
		// (get) Token: 0x06012175 RID: 74101 RVA: 0x004FDA9F File Offset: 0x004FBC9F
		// (set) Token: 0x06012176 RID: 74102 RVA: 0x004FDAA7 File Offset: 0x004FBCA7
		public int EventType { get; set; }

		// Token: 0x06012177 RID: 74103 RVA: 0x004FDAB0 File Offset: 0x004FBCB0
		public override string ToString()
		{
			return string.Format("[ProfileNoticeEvent: NoticeID={0}, Type={1}, Origin={2}, OriginData={3}, Date={4} EventType={5}]", new object[]
			{
				base.NoticeID,
				base.Type,
				base.Origin,
				base.OriginData,
				base.Date,
				this.EventType
			});
		}
	}

	// Token: 0x020020EE RID: 8430
	public class ProfileNoticeDisconnectedGame : NetCache.ProfileNotice
	{
		// Token: 0x06012178 RID: 74104 RVA: 0x004FDB21 File Offset: 0x004FBD21
		public ProfileNoticeDisconnectedGame() : base(NetCache.ProfileNotice.NoticeType.DISCONNECTED_GAME)
		{
		}

		// Token: 0x1700283B RID: 10299
		// (get) Token: 0x06012179 RID: 74105 RVA: 0x004FDB2A File Offset: 0x004FBD2A
		// (set) Token: 0x0601217A RID: 74106 RVA: 0x004FDB32 File Offset: 0x004FBD32
		public PegasusShared.GameType GameType { get; set; }

		// Token: 0x1700283C RID: 10300
		// (get) Token: 0x0601217B RID: 74107 RVA: 0x004FDB3B File Offset: 0x004FBD3B
		// (set) Token: 0x0601217C RID: 74108 RVA: 0x004FDB43 File Offset: 0x004FBD43
		public PegasusShared.FormatType FormatType { get; set; }

		// Token: 0x1700283D RID: 10301
		// (get) Token: 0x0601217D RID: 74109 RVA: 0x004FDB4C File Offset: 0x004FBD4C
		// (set) Token: 0x0601217E RID: 74110 RVA: 0x004FDB54 File Offset: 0x004FBD54
		public int MissionId { get; set; }

		// Token: 0x1700283E RID: 10302
		// (get) Token: 0x0601217F RID: 74111 RVA: 0x004FDB5D File Offset: 0x004FBD5D
		// (set) Token: 0x06012180 RID: 74112 RVA: 0x004FDB65 File Offset: 0x004FBD65
		public ProfileNoticeDisconnectedGameResult.GameResult GameResult { get; set; }

		// Token: 0x1700283F RID: 10303
		// (get) Token: 0x06012181 RID: 74113 RVA: 0x004FDB6E File Offset: 0x004FBD6E
		// (set) Token: 0x06012182 RID: 74114 RVA: 0x004FDB76 File Offset: 0x004FBD76
		public ProfileNoticeDisconnectedGameResult.PlayerResult YourResult { get; set; }

		// Token: 0x17002840 RID: 10304
		// (get) Token: 0x06012183 RID: 74115 RVA: 0x004FDB7F File Offset: 0x004FBD7F
		// (set) Token: 0x06012184 RID: 74116 RVA: 0x004FDB87 File Offset: 0x004FBD87
		public ProfileNoticeDisconnectedGameResult.PlayerResult OpponentResult { get; set; }

		// Token: 0x17002841 RID: 10305
		// (get) Token: 0x06012185 RID: 74117 RVA: 0x004FDB90 File Offset: 0x004FBD90
		// (set) Token: 0x06012186 RID: 74118 RVA: 0x004FDB98 File Offset: 0x004FBD98
		public int PlayerIndex { get; set; }

		// Token: 0x06012187 RID: 74119 RVA: 0x004FDBA4 File Offset: 0x004FBDA4
		public override string ToString()
		{
			return string.Format("{0} [GameType={1}, FormatType={2}, MissionId={3} GameResult={4}, YourResult={5}, OpponentResult={6}, PlayerIndex={7}]", new object[]
			{
				base.ToString(),
				this.GameType,
				this.FormatType,
				this.MissionId,
				this.GameResult,
				this.YourResult,
				this.OpponentResult,
				this.PlayerIndex
			});
		}
	}

	// Token: 0x020020EF RID: 8431
	public class ProfileNoticeAdventureProgress : NetCache.ProfileNotice
	{
		// Token: 0x06012188 RID: 74120 RVA: 0x004FDC2C File Offset: 0x004FBE2C
		public ProfileNoticeAdventureProgress() : base(NetCache.ProfileNotice.NoticeType.ADVENTURE_PROGRESS)
		{
		}

		// Token: 0x17002842 RID: 10306
		// (get) Token: 0x06012189 RID: 74121 RVA: 0x004FDC36 File Offset: 0x004FBE36
		// (set) Token: 0x0601218A RID: 74122 RVA: 0x004FDC3E File Offset: 0x004FBE3E
		public int Wing { get; set; }

		// Token: 0x17002843 RID: 10307
		// (get) Token: 0x0601218B RID: 74123 RVA: 0x004FDC47 File Offset: 0x004FBE47
		// (set) Token: 0x0601218C RID: 74124 RVA: 0x004FDC4F File Offset: 0x004FBE4F
		public int? Progress { get; set; }

		// Token: 0x17002844 RID: 10308
		// (get) Token: 0x0601218D RID: 74125 RVA: 0x004FDC58 File Offset: 0x004FBE58
		// (set) Token: 0x0601218E RID: 74126 RVA: 0x004FDC60 File Offset: 0x004FBE60
		public ulong? Flags { get; set; }

		// Token: 0x0601218F RID: 74127 RVA: 0x004FDC6C File Offset: 0x004FBE6C
		public override string ToString()
		{
			return string.Format("{0} [Wing={1}, Progress={2}, Flags={3}]", new object[]
			{
				base.ToString(),
				this.Wing,
				this.Progress,
				this.Flags
			});
		}
	}

	// Token: 0x020020F0 RID: 8432
	public class ProfileNoticeLevelUp : NetCache.ProfileNotice
	{
		// Token: 0x06012190 RID: 74128 RVA: 0x004FDCBC File Offset: 0x004FBEBC
		public ProfileNoticeLevelUp() : base(NetCache.ProfileNotice.NoticeType.HERO_LEVEL_UP)
		{
		}

		// Token: 0x17002845 RID: 10309
		// (get) Token: 0x06012191 RID: 74129 RVA: 0x004FDCC6 File Offset: 0x004FBEC6
		// (set) Token: 0x06012192 RID: 74130 RVA: 0x004FDCCE File Offset: 0x004FBECE
		public int HeroClass { get; set; }

		// Token: 0x17002846 RID: 10310
		// (get) Token: 0x06012193 RID: 74131 RVA: 0x004FDCD7 File Offset: 0x004FBED7
		// (set) Token: 0x06012194 RID: 74132 RVA: 0x004FDCDF File Offset: 0x004FBEDF
		public int NewLevel { get; set; }

		// Token: 0x17002847 RID: 10311
		// (get) Token: 0x06012195 RID: 74133 RVA: 0x004FDCE8 File Offset: 0x004FBEE8
		// (set) Token: 0x06012196 RID: 74134 RVA: 0x004FDCF0 File Offset: 0x004FBEF0
		public int TotalLevel { get; set; }

		// Token: 0x06012197 RID: 74135 RVA: 0x004FDCFC File Offset: 0x004FBEFC
		public override string ToString()
		{
			return string.Format("{0} [HeroClass={1}, NewLevel={2}], TotalLevel={3}", new object[]
			{
				base.ToString(),
				this.HeroClass,
				this.NewLevel,
				this.TotalLevel
			});
		}
	}

	// Token: 0x020020F1 RID: 8433
	public class ProfileNoticeAcccountLicense : NetCache.ProfileNotice
	{
		// Token: 0x06012198 RID: 74136 RVA: 0x004FDD4C File Offset: 0x004FBF4C
		public ProfileNoticeAcccountLicense() : base(NetCache.ProfileNotice.NoticeType.ACCOUNT_LICENSE)
		{
		}

		// Token: 0x17002848 RID: 10312
		// (get) Token: 0x06012199 RID: 74137 RVA: 0x004FDD56 File Offset: 0x004FBF56
		// (set) Token: 0x0601219A RID: 74138 RVA: 0x004FDD5E File Offset: 0x004FBF5E
		public long License { get; set; }

		// Token: 0x17002849 RID: 10313
		// (get) Token: 0x0601219B RID: 74139 RVA: 0x004FDD67 File Offset: 0x004FBF67
		// (set) Token: 0x0601219C RID: 74140 RVA: 0x004FDD6F File Offset: 0x004FBF6F
		public long CasID { get; set; }

		// Token: 0x0601219D RID: 74141 RVA: 0x004FDD78 File Offset: 0x004FBF78
		public override string ToString()
		{
			return string.Format("{0} [License={1}, CasID={2}]", base.ToString(), this.License, this.CasID);
		}
	}

	// Token: 0x020020F2 RID: 8434
	public class ProfileNoticeTavernBrawlRewards : NetCache.ProfileNotice
	{
		// Token: 0x0601219E RID: 74142 RVA: 0x004FDDA0 File Offset: 0x004FBFA0
		public ProfileNoticeTavernBrawlRewards() : base(NetCache.ProfileNotice.NoticeType.TAVERN_BRAWL_REWARDS)
		{
		}

		// Token: 0x1700284A RID: 10314
		// (get) Token: 0x0601219F RID: 74143 RVA: 0x004FDDAA File Offset: 0x004FBFAA
		// (set) Token: 0x060121A0 RID: 74144 RVA: 0x004FDDB2 File Offset: 0x004FBFB2
		public RewardChest Chest { get; set; }

		// Token: 0x1700284B RID: 10315
		// (get) Token: 0x060121A1 RID: 74145 RVA: 0x004FDDBB File Offset: 0x004FBFBB
		// (set) Token: 0x060121A2 RID: 74146 RVA: 0x004FDDC3 File Offset: 0x004FBFC3
		public int Wins { get; set; }

		// Token: 0x1700284C RID: 10316
		// (get) Token: 0x060121A3 RID: 74147 RVA: 0x004FDDCC File Offset: 0x004FBFCC
		// (set) Token: 0x060121A4 RID: 74148 RVA: 0x004FDDD4 File Offset: 0x004FBFD4
		public TavernBrawlMode Mode { get; set; }

		// Token: 0x060121A5 RID: 74149 RVA: 0x004FDDDD File Offset: 0x004FBFDD
		public override string ToString()
		{
			return string.Format("{0} [Chest={1}, Wins={2}, Mode={3}]", new object[]
			{
				base.ToString(),
				this.Chest,
				this.Wins,
				this.Mode
			});
		}
	}

	// Token: 0x020020F3 RID: 8435
	public class ProfileNoticeTavernBrawlTicket : NetCache.ProfileNotice
	{
		// Token: 0x060121A6 RID: 74150 RVA: 0x004FDE1D File Offset: 0x004FC01D
		public ProfileNoticeTavernBrawlTicket() : base(NetCache.ProfileNotice.NoticeType.TAVERN_BRAWL_TICKET)
		{
		}

		// Token: 0x1700284D RID: 10317
		// (get) Token: 0x060121A7 RID: 74151 RVA: 0x004FDE27 File Offset: 0x004FC027
		// (set) Token: 0x060121A8 RID: 74152 RVA: 0x004FDE2F File Offset: 0x004FC02F
		public int TicketType { get; set; }

		// Token: 0x1700284E RID: 10318
		// (get) Token: 0x060121A9 RID: 74153 RVA: 0x004FDE38 File Offset: 0x004FC038
		// (set) Token: 0x060121AA RID: 74154 RVA: 0x004FDE40 File Offset: 0x004FC040
		public int Quantity { get; set; }
	}

	// Token: 0x020020F4 RID: 8436
	public class ProfileNoticeGenericRewardChest : NetCache.ProfileNotice
	{
		// Token: 0x060121AB RID: 74155 RVA: 0x004FDE49 File Offset: 0x004FC049
		public ProfileNoticeGenericRewardChest() : base(NetCache.ProfileNotice.NoticeType.GENERIC_REWARD_CHEST)
		{
		}

		// Token: 0x1700284F RID: 10319
		// (get) Token: 0x060121AC RID: 74156 RVA: 0x004FDE53 File Offset: 0x004FC053
		// (set) Token: 0x060121AD RID: 74157 RVA: 0x004FDE5B File Offset: 0x004FC05B
		public int RewardChestAssetId { get; set; }

		// Token: 0x17002850 RID: 10320
		// (get) Token: 0x060121AE RID: 74158 RVA: 0x004FDE64 File Offset: 0x004FC064
		// (set) Token: 0x060121AF RID: 74159 RVA: 0x004FDE6C File Offset: 0x004FC06C
		public RewardChest RewardChest { get; set; }

		// Token: 0x17002851 RID: 10321
		// (get) Token: 0x060121B0 RID: 74160 RVA: 0x004FDE75 File Offset: 0x004FC075
		// (set) Token: 0x060121B1 RID: 74161 RVA: 0x004FDE7D File Offset: 0x004FC07D
		public uint RewardChestByteSize { get; set; }

		// Token: 0x17002852 RID: 10322
		// (get) Token: 0x060121B2 RID: 74162 RVA: 0x004FDE86 File Offset: 0x004FC086
		// (set) Token: 0x060121B3 RID: 74163 RVA: 0x004FDE8E File Offset: 0x004FC08E
		public byte[] RewardChestHash { get; set; }
	}

	// Token: 0x020020F5 RID: 8437
	public class NetCacheProfileNotices
	{
		// Token: 0x060121B4 RID: 74164 RVA: 0x004FDE97 File Offset: 0x004FC097
		public NetCacheProfileNotices()
		{
			this.Notices = new List<NetCache.ProfileNotice>();
		}

		// Token: 0x17002853 RID: 10323
		// (get) Token: 0x060121B5 RID: 74165 RVA: 0x004FDEAA File Offset: 0x004FC0AA
		// (set) Token: 0x060121B6 RID: 74166 RVA: 0x004FDEB2 File Offset: 0x004FC0B2
		public List<NetCache.ProfileNotice> Notices { get; set; }
	}

	// Token: 0x020020F6 RID: 8438
	public class ProfileNoticeLeaguePromotionRewards : NetCache.ProfileNotice
	{
		// Token: 0x060121B7 RID: 74167 RVA: 0x004FDEBB File Offset: 0x004FC0BB
		public ProfileNoticeLeaguePromotionRewards() : base(NetCache.ProfileNotice.NoticeType.LEAGUE_PROMOTION_REWARDS)
		{
		}

		// Token: 0x17002854 RID: 10324
		// (get) Token: 0x060121B8 RID: 74168 RVA: 0x004FDEC5 File Offset: 0x004FC0C5
		// (set) Token: 0x060121B9 RID: 74169 RVA: 0x004FDECD File Offset: 0x004FC0CD
		public RewardChest Chest { get; set; }

		// Token: 0x17002855 RID: 10325
		// (get) Token: 0x060121BA RID: 74170 RVA: 0x004FDED6 File Offset: 0x004FC0D6
		// (set) Token: 0x060121BB RID: 74171 RVA: 0x004FDEDE File Offset: 0x004FC0DE
		public int LeagueId { get; set; }

		// Token: 0x060121BC RID: 74172 RVA: 0x004FDEE7 File Offset: 0x004FC0E7
		public override string ToString()
		{
			return string.Format("{0} [Chest={1}, LeagueId={2}]", base.ToString(), this.Chest, this.LeagueId);
		}
	}

	// Token: 0x020020F7 RID: 8439
	public abstract class ClientOptionBase : ICloneable
	{
		// Token: 0x060121BD RID: 74173
		public abstract void PopulateIntoPacket(ServerOption type, SetOptions packet);

		// Token: 0x060121BE RID: 74174 RVA: 0x004FDF0A File Offset: 0x004FC10A
		public override bool Equals(object other)
		{
			return other != null && !(other.GetType() != base.GetType());
		}

		// Token: 0x060121BF RID: 74175 RVA: 0x00232D17 File Offset: 0x00230F17
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060121C0 RID: 74176 RVA: 0x002C15F4 File Offset: 0x002BF7F4
		public object Clone()
		{
			return base.MemberwiseClone();
		}
	}

	// Token: 0x020020F8 RID: 8440
	public class ClientOptionInt : NetCache.ClientOptionBase
	{
		// Token: 0x060121C2 RID: 74178 RVA: 0x004FDF27 File Offset: 0x004FC127
		public ClientOptionInt(int val)
		{
			this.OptionValue = val;
		}

		// Token: 0x17002856 RID: 10326
		// (get) Token: 0x060121C3 RID: 74179 RVA: 0x004FDF36 File Offset: 0x004FC136
		// (set) Token: 0x060121C4 RID: 74180 RVA: 0x004FDF3E File Offset: 0x004FC13E
		public int OptionValue { get; set; }

		// Token: 0x060121C5 RID: 74181 RVA: 0x004FDF48 File Offset: 0x004FC148
		public override void PopulateIntoPacket(ServerOption type, SetOptions packet)
		{
			PegasusUtil.ClientOption clientOption = new PegasusUtil.ClientOption();
			clientOption.Index = (int)type;
			clientOption.AsInt32 = this.OptionValue;
			packet.Options.Add(clientOption);
		}

		// Token: 0x060121C6 RID: 74182 RVA: 0x004FDF7A File Offset: 0x004FC17A
		public override bool Equals(object other)
		{
			return base.Equals(other) && ((NetCache.ClientOptionInt)other).OptionValue == this.OptionValue;
		}

		// Token: 0x060121C7 RID: 74183 RVA: 0x004FDF9C File Offset: 0x004FC19C
		public override int GetHashCode()
		{
			return this.OptionValue.GetHashCode();
		}
	}

	// Token: 0x020020F9 RID: 8441
	public class ClientOptionLong : NetCache.ClientOptionBase
	{
		// Token: 0x060121C8 RID: 74184 RVA: 0x004FDFB7 File Offset: 0x004FC1B7
		public ClientOptionLong(long val)
		{
			this.OptionValue = val;
		}

		// Token: 0x17002857 RID: 10327
		// (get) Token: 0x060121C9 RID: 74185 RVA: 0x004FDFC6 File Offset: 0x004FC1C6
		// (set) Token: 0x060121CA RID: 74186 RVA: 0x004FDFCE File Offset: 0x004FC1CE
		public long OptionValue { get; set; }

		// Token: 0x060121CB RID: 74187 RVA: 0x004FDFD8 File Offset: 0x004FC1D8
		public override void PopulateIntoPacket(ServerOption type, SetOptions packet)
		{
			PegasusUtil.ClientOption clientOption = new PegasusUtil.ClientOption();
			clientOption.Index = (int)type;
			clientOption.AsInt64 = this.OptionValue;
			packet.Options.Add(clientOption);
		}

		// Token: 0x060121CC RID: 74188 RVA: 0x004FE00A File Offset: 0x004FC20A
		public override bool Equals(object other)
		{
			return base.Equals(other) && ((NetCache.ClientOptionLong)other).OptionValue == this.OptionValue;
		}

		// Token: 0x060121CD RID: 74189 RVA: 0x004FE02C File Offset: 0x004FC22C
		public override int GetHashCode()
		{
			return this.OptionValue.GetHashCode();
		}
	}

	// Token: 0x020020FA RID: 8442
	public class ClientOptionFloat : NetCache.ClientOptionBase
	{
		// Token: 0x060121CE RID: 74190 RVA: 0x004FE047 File Offset: 0x004FC247
		public ClientOptionFloat(float val)
		{
			this.OptionValue = val;
		}

		// Token: 0x17002858 RID: 10328
		// (get) Token: 0x060121CF RID: 74191 RVA: 0x004FE056 File Offset: 0x004FC256
		// (set) Token: 0x060121D0 RID: 74192 RVA: 0x004FE05E File Offset: 0x004FC25E
		public float OptionValue { get; set; }

		// Token: 0x060121D1 RID: 74193 RVA: 0x004FE068 File Offset: 0x004FC268
		public override void PopulateIntoPacket(ServerOption type, SetOptions packet)
		{
			PegasusUtil.ClientOption clientOption = new PegasusUtil.ClientOption();
			clientOption.Index = (int)type;
			clientOption.AsFloat = this.OptionValue;
			packet.Options.Add(clientOption);
		}

		// Token: 0x060121D2 RID: 74194 RVA: 0x004FE09A File Offset: 0x004FC29A
		public override bool Equals(object other)
		{
			return base.Equals(other) && ((NetCache.ClientOptionFloat)other).OptionValue == this.OptionValue;
		}

		// Token: 0x060121D3 RID: 74195 RVA: 0x004FE0BC File Offset: 0x004FC2BC
		public override int GetHashCode()
		{
			return this.OptionValue.GetHashCode();
		}
	}

	// Token: 0x020020FB RID: 8443
	public class ClientOptionULong : NetCache.ClientOptionBase
	{
		// Token: 0x060121D4 RID: 74196 RVA: 0x004FE0D7 File Offset: 0x004FC2D7
		public ClientOptionULong(ulong val)
		{
			this.OptionValue = val;
		}

		// Token: 0x17002859 RID: 10329
		// (get) Token: 0x060121D5 RID: 74197 RVA: 0x004FE0E6 File Offset: 0x004FC2E6
		// (set) Token: 0x060121D6 RID: 74198 RVA: 0x004FE0EE File Offset: 0x004FC2EE
		public ulong OptionValue { get; set; }

		// Token: 0x060121D7 RID: 74199 RVA: 0x004FE0F8 File Offset: 0x004FC2F8
		public override void PopulateIntoPacket(ServerOption type, SetOptions packet)
		{
			PegasusUtil.ClientOption clientOption = new PegasusUtil.ClientOption();
			clientOption.Index = (int)type;
			clientOption.AsUint64 = this.OptionValue;
			packet.Options.Add(clientOption);
		}

		// Token: 0x060121D8 RID: 74200 RVA: 0x004FE12A File Offset: 0x004FC32A
		public override bool Equals(object other)
		{
			return base.Equals(other) && ((NetCache.ClientOptionULong)other).OptionValue == this.OptionValue;
		}

		// Token: 0x060121D9 RID: 74201 RVA: 0x004FE14C File Offset: 0x004FC34C
		public override int GetHashCode()
		{
			return this.OptionValue.GetHashCode();
		}
	}

	// Token: 0x020020FC RID: 8444
	public class NetCacheClientOptions
	{
		// Token: 0x060121DA RID: 74202 RVA: 0x004FE167 File Offset: 0x004FC367
		public NetCacheClientOptions()
		{
			this.ClientState = new global::Map<ServerOption, NetCache.ClientOptionBase>();
			this.ServerState = new global::Map<ServerOption, NetCache.ClientOptionBase>();
		}

		// Token: 0x060121DB RID: 74203 RVA: 0x004FE188 File Offset: 0x004FC388
		public void UpdateServerState()
		{
			foreach (KeyValuePair<ServerOption, NetCache.ClientOptionBase> keyValuePair in this.ClientState)
			{
				if (keyValuePair.Value != null)
				{
					this.ServerState[keyValuePair.Key] = (NetCache.ClientOptionBase)keyValuePair.Value.Clone();
				}
				else
				{
					this.ServerState[keyValuePair.Key] = null;
				}
			}
		}

		// Token: 0x1700285A RID: 10330
		// (get) Token: 0x060121DC RID: 74204 RVA: 0x004FE218 File Offset: 0x004FC418
		private int ClientOptionsUpdateIntervalSeconds
		{
			get
			{
				NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
				if (netObject != null && netObject.Misc != null)
				{
					return netObject.Misc.ClientOptionsUpdateIntervalSeconds;
				}
				return 180;
			}
		}

		// Token: 0x060121DD RID: 74205 RVA: 0x004FE24C File Offset: 0x004FC44C
		public void OnUpdateIntervalElasped(object userData)
		{
			this.m_currentScheduledDispatchTime = null;
			this.DispatchClientOptionsToServer();
		}

		// Token: 0x060121DE RID: 74206 RVA: 0x004FE260 File Offset: 0x004FC460
		public void CancelScheduledDispatchToServer()
		{
			Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.OnUpdateIntervalElasped), null);
			this.m_currentScheduledDispatchTime = null;
		}

		// Token: 0x060121DF RID: 74207 RVA: 0x004FE284 File Offset: 0x004FC484
		public void DispatchClientOptionsToServer()
		{
			this.CancelScheduledDispatchToServer();
			bool flag = false;
			SetOptions setOptions = new SetOptions();
			foreach (KeyValuePair<ServerOption, NetCache.ClientOptionBase> keyValuePair in this.ClientState)
			{
				NetCache.ClientOptionBase clientOptionBase;
				if (!this.ServerState.TryGetValue(keyValuePair.Key, out clientOptionBase))
				{
					flag = true;
					break;
				}
				if (keyValuePair.Value != null || clientOptionBase != null)
				{
					if ((keyValuePair.Value == null && clientOptionBase != null) || (keyValuePair.Value != null && clientOptionBase == null))
					{
						flag = true;
						break;
					}
					if (!clientOptionBase.Equals(keyValuePair.Value))
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				foreach (KeyValuePair<ServerOption, NetCache.ClientOptionBase> keyValuePair2 in this.ClientState)
				{
					if (keyValuePair2.Value != null)
					{
						keyValuePair2.Value.PopulateIntoPacket(keyValuePair2.Key, setOptions);
					}
				}
				Network.Get().SetClientOptions(setOptions);
				this.m_mostRecentDispatchToServer = new DateTime?(DateTime.UtcNow);
				this.UpdateServerState();
			}
		}

		// Token: 0x060121E0 RID: 74208 RVA: 0x004FE3B4 File Offset: 0x004FC5B4
		public void RemoveInvalidOptions()
		{
			List<ServerOption> list = new List<ServerOption>();
			foreach (KeyValuePair<ServerOption, NetCache.ClientOptionBase> keyValuePair in this.ClientState)
			{
				ServerOption key = keyValuePair.Key;
				NetCache.ClientOptionBase value = keyValuePair.Value;
				Type serverOptionType = Options.Get().GetServerOptionType(key);
				if (value != null)
				{
					Type type = value.GetType();
					if (serverOptionType == typeof(int))
					{
						if (type == typeof(NetCache.ClientOptionInt))
						{
							continue;
						}
					}
					else if (serverOptionType == typeof(long))
					{
						if (type == typeof(NetCache.ClientOptionLong))
						{
							continue;
						}
					}
					else if (serverOptionType == typeof(float))
					{
						if (type == typeof(NetCache.ClientOptionFloat))
						{
							continue;
						}
					}
					else if (serverOptionType == typeof(ulong) && type == typeof(NetCache.ClientOptionULong))
					{
						continue;
					}
					if (serverOptionType == null)
					{
						global::Log.Net.Print("NetCacheClientOptions.RemoveInvalidOptions() - Option {0} has type {1}, but value is type {2}. Removing it.", new object[]
						{
							key,
							serverOptionType,
							type
						});
					}
					else
					{
						global::Log.Net.Print("NetCacheClientOptions.RemoveInvalidOptions() - Option {0} has type {1}, but value is type {2}. Removing it.", new object[]
						{
							global::EnumUtils.GetString<ServerOption>(key),
							serverOptionType,
							type
						});
					}
				}
				list.Add(key);
			}
			foreach (ServerOption key2 in list)
			{
				this.ClientState.Remove(key2);
				this.ServerState.Remove(key2);
			}
		}

		// Token: 0x060121E1 RID: 74209 RVA: 0x004FE5B4 File Offset: 0x004FC7B4
		public void CheckForDispatchToServer()
		{
			float num = (float)this.ClientOptionsUpdateIntervalSeconds;
			if (num <= 0f)
			{
				return;
			}
			DateTime utcNow = DateTime.UtcNow;
			bool flag = false;
			bool flag2 = false;
			if (this.m_mostRecentDispatchToServer == null)
			{
				flag = true;
			}
			else if (this.m_currentScheduledDispatchTime == null)
			{
				TimeSpan timeSpan = utcNow - this.m_mostRecentDispatchToServer.Value;
				if (timeSpan.TotalSeconds >= (double)num)
				{
					flag = true;
				}
				else
				{
					flag2 = true;
					num -= (float)timeSpan.TotalSeconds;
				}
			}
			if (!flag && !flag2 && this.m_currentScheduledDispatchTime != null && (this.m_currentScheduledDispatchTime.Value - utcNow).TotalSeconds > (double)num)
			{
				flag2 = true;
			}
			if (flag || flag2)
			{
				float secondsToWait = flag ? 0f : num;
				this.m_currentScheduledDispatchTime = new DateTime?(utcNow);
				Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.OnUpdateIntervalElasped), null);
				Processor.ScheduleCallback(secondsToWait, true, new Processor.ScheduledCallback(this.OnUpdateIntervalElasped), null);
			}
		}

		// Token: 0x1700285B RID: 10331
		// (get) Token: 0x060121E2 RID: 74210 RVA: 0x004FE69F File Offset: 0x004FC89F
		// (set) Token: 0x060121E3 RID: 74211 RVA: 0x004FE6A7 File Offset: 0x004FC8A7
		public global::Map<ServerOption, NetCache.ClientOptionBase> ClientState { get; private set; }

		// Token: 0x1700285C RID: 10332
		// (get) Token: 0x060121E4 RID: 74212 RVA: 0x004FE6B0 File Offset: 0x004FC8B0
		// (set) Token: 0x060121E5 RID: 74213 RVA: 0x004FE6B8 File Offset: 0x004FC8B8
		private global::Map<ServerOption, NetCache.ClientOptionBase> ServerState { get; set; }

		// Token: 0x0400DEFA RID: 57082
		private DateTime? m_mostRecentDispatchToServer;

		// Token: 0x0400DEFB RID: 57083
		private DateTime? m_currentScheduledDispatchTime;
	}

	// Token: 0x020020FD RID: 8445
	public class NetCacheFavoriteHeroes
	{
		// Token: 0x1700285D RID: 10333
		// (get) Token: 0x060121E6 RID: 74214 RVA: 0x004FE6C1 File Offset: 0x004FC8C1
		// (set) Token: 0x060121E7 RID: 74215 RVA: 0x004FE6C9 File Offset: 0x004FC8C9
		public global::Map<TAG_CLASS, NetCache.CardDefinition> FavoriteHeroes { get; set; }

		// Token: 0x060121E8 RID: 74216 RVA: 0x004FE6D2 File Offset: 0x004FC8D2
		public NetCacheFavoriteHeroes()
		{
			this.FavoriteHeroes = new global::Map<TAG_CLASS, NetCache.CardDefinition>();
		}
	}

	// Token: 0x020020FE RID: 8446
	public class NetCacheAccountLicenses
	{
		// Token: 0x060121E9 RID: 74217 RVA: 0x004FE6E5 File Offset: 0x004FC8E5
		public NetCacheAccountLicenses()
		{
			this.AccountLicenses = new global::Map<long, AccountLicenseInfo>();
		}

		// Token: 0x1700285E RID: 10334
		// (get) Token: 0x060121EA RID: 74218 RVA: 0x004FE6F8 File Offset: 0x004FC8F8
		// (set) Token: 0x060121EB RID: 74219 RVA: 0x004FE700 File Offset: 0x004FC900
		public global::Map<long, AccountLicenseInfo> AccountLicenses { get; set; }
	}

	// Token: 0x020020FF RID: 8447
	// (Invoke) Token: 0x060121ED RID: 74221
	public delegate void ErrorCallback(NetCache.ErrorInfo info);

	// Token: 0x02002100 RID: 8448
	public enum ErrorCode
	{
		// Token: 0x0400DEFF RID: 57087
		NONE,
		// Token: 0x0400DF00 RID: 57088
		TIMEOUT,
		// Token: 0x0400DF01 RID: 57089
		SERVER
	}

	// Token: 0x02002101 RID: 8449
	public class ErrorInfo
	{
		// Token: 0x1700285F RID: 10335
		// (get) Token: 0x060121F0 RID: 74224 RVA: 0x004FE709 File Offset: 0x004FC909
		// (set) Token: 0x060121F1 RID: 74225 RVA: 0x004FE711 File Offset: 0x004FC911
		public NetCache.ErrorCode Error { get; set; }

		// Token: 0x17002860 RID: 10336
		// (get) Token: 0x060121F2 RID: 74226 RVA: 0x004FE71A File Offset: 0x004FC91A
		// (set) Token: 0x060121F3 RID: 74227 RVA: 0x004FE722 File Offset: 0x004FC922
		public uint ServerError { get; set; }

		// Token: 0x17002861 RID: 10337
		// (get) Token: 0x060121F4 RID: 74228 RVA: 0x004FE72B File Offset: 0x004FC92B
		// (set) Token: 0x060121F5 RID: 74229 RVA: 0x004FE733 File Offset: 0x004FC933
		public NetCache.RequestFunc RequestingFunction { get; set; }

		// Token: 0x17002862 RID: 10338
		// (get) Token: 0x060121F6 RID: 74230 RVA: 0x004FE73C File Offset: 0x004FC93C
		// (set) Token: 0x060121F7 RID: 74231 RVA: 0x004FE744 File Offset: 0x004FC944
		public global::Map<Type, NetCache.Request> RequestedTypes { get; set; }

		// Token: 0x17002863 RID: 10339
		// (get) Token: 0x060121F8 RID: 74232 RVA: 0x004FE74D File Offset: 0x004FC94D
		// (set) Token: 0x060121F9 RID: 74233 RVA: 0x004FE755 File Offset: 0x004FC955
		public string RequestStackTrace { get; set; }
	}

	// Token: 0x02002102 RID: 8450
	// (Invoke) Token: 0x060121FC RID: 74236
	public delegate void NetCacheCallback();

	// Token: 0x02002103 RID: 8451
	// (Invoke) Token: 0x06012200 RID: 74240
	public delegate void RequestFunc(NetCache.NetCacheCallback callback, NetCache.ErrorCallback errorCallback);

	// Token: 0x02002104 RID: 8452
	public enum RequestResult
	{
		// Token: 0x0400DF08 RID: 57096
		UNKNOWN,
		// Token: 0x0400DF09 RID: 57097
		PENDING,
		// Token: 0x0400DF0A RID: 57098
		IN_PROCESS,
		// Token: 0x0400DF0B RID: 57099
		GENERIC_COMPLETE,
		// Token: 0x0400DF0C RID: 57100
		DATA_COMPLETE,
		// Token: 0x0400DF0D RID: 57101
		ERROR,
		// Token: 0x0400DF0E RID: 57102
		MIGRATION_REQUIRED
	}

	// Token: 0x02002105 RID: 8453
	public class Request
	{
		// Token: 0x06012203 RID: 74243 RVA: 0x004FE75E File Offset: 0x004FC95E
		public Request(Type rt, bool rl = false)
		{
			this.m_type = rt;
			this.m_reload = rl;
			this.m_result = NetCache.RequestResult.UNKNOWN;
		}

		// Token: 0x0400DF0F RID: 57103
		public const bool RELOAD = true;

		// Token: 0x0400DF10 RID: 57104
		public Type m_type;

		// Token: 0x0400DF11 RID: 57105
		public bool m_reload;

		// Token: 0x0400DF12 RID: 57106
		public NetCache.RequestResult m_result;
	}

	// Token: 0x02002106 RID: 8454
	private class NetCacheBatchRequest
	{
		// Token: 0x06012204 RID: 74244 RVA: 0x004FE77C File Offset: 0x004FC97C
		public NetCacheBatchRequest(NetCache.NetCacheCallback reply, NetCache.ErrorCallback errorCallback, NetCache.RequestFunc requestFunc)
		{
			this.m_callback = reply;
			this.m_errorCallback = errorCallback;
			this.m_requestFunc = requestFunc;
			this.m_requestStackTrace = Environment.StackTrace;
		}

		// Token: 0x06012205 RID: 74245 RVA: 0x004FE7CC File Offset: 0x004FC9CC
		public void AddRequests(List<NetCache.Request> requests)
		{
			foreach (NetCache.Request r in requests)
			{
				this.AddRequest(r);
			}
		}

		// Token: 0x06012206 RID: 74246 RVA: 0x004FE81C File Offset: 0x004FCA1C
		public void AddRequest(NetCache.Request r)
		{
			if (!this.m_requests.ContainsKey(r.m_type))
			{
				this.m_requests.Add(r.m_type, r);
			}
		}

		// Token: 0x0400DF13 RID: 57107
		public global::Map<Type, NetCache.Request> m_requests = new global::Map<Type, NetCache.Request>();

		// Token: 0x0400DF14 RID: 57108
		public NetCache.NetCacheCallback m_callback;

		// Token: 0x0400DF15 RID: 57109
		public NetCache.ErrorCallback m_errorCallback;

		// Token: 0x0400DF16 RID: 57110
		public bool m_canTimeout = true;

		// Token: 0x0400DF17 RID: 57111
		public float m_timeAdded = Time.realtimeSinceStartup;

		// Token: 0x0400DF18 RID: 57112
		public NetCache.RequestFunc m_requestFunc;

		// Token: 0x0400DF19 RID: 57113
		public string m_requestStackTrace;
	}
}
