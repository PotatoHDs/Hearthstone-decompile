using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using PegasusUtil;

// Token: 0x02000960 RID: 2400
public class SpecialEventManager : IService
{
	// Token: 0x06008411 RID: 33809 RVA: 0x002AC06C File Offset: 0x002AA26C
	public bool AddEventAddedListener(SpecialEventManager.EventAddedCallback callback, SpecialEventType eventType, object userData = null)
	{
		if (callback == null)
		{
			return false;
		}
		SpecialEventManager.EventAddedListener eventAddedListener = new SpecialEventManager.EventAddedListener();
		eventAddedListener.SetCallback(callback);
		eventAddedListener.SetUserData(userData);
		if (!this.m_allEventAddedListeners.ContainsKey(eventType))
		{
			this.m_allEventAddedListeners[eventType] = new List<SpecialEventManager.EventAddedListener>();
		}
		this.m_allEventAddedListeners[eventType].Add(eventAddedListener);
		return true;
	}

	// Token: 0x06008412 RID: 33810 RVA: 0x002AC0C4 File Offset: 0x002AA2C4
	private void FireEventAddedEvents(SpecialEventType eventType)
	{
		List<SpecialEventManager.EventAddedListener> list;
		if (!this.m_allEventAddedListeners.TryGetValue(eventType, out list))
		{
			return;
		}
		foreach (SpecialEventManager.EventAddedListener eventAddedListener in list)
		{
			eventAddedListener.Fire();
		}
	}

	// Token: 0x1700077F RID: 1919
	// (get) Token: 0x06008413 RID: 33811 RVA: 0x002AC120 File Offset: 0x002AA320
	public HashSet<SpecialEventType> AllKnownEvents
	{
		get
		{
			return new HashSet<SpecialEventType>(this.m_eventTimings.Keys);
		}
	}

	// Token: 0x17000780 RID: 1920
	// (get) Token: 0x06008414 RID: 33812 RVA: 0x002AC132 File Offset: 0x002AA332
	// (set) Token: 0x06008415 RID: 33813 RVA: 0x002AC13A File Offset: 0x002AA33A
	public bool HasReceivedEventTimingsFromServer { get; private set; }

	// Token: 0x17000781 RID: 1921
	// (get) Token: 0x06008416 RID: 33814 RVA: 0x002AC143 File Offset: 0x002AA343
	// (set) Token: 0x06008417 RID: 33815 RVA: 0x002AC14B File Offset: 0x002AA34B
	public long DevTimeOffsetSeconds { get; private set; }

	// Token: 0x17000782 RID: 1922
	// (get) Token: 0x06008418 RID: 33816 RVA: 0x002AC154 File Offset: 0x002AA354
	// (set) Token: 0x06008419 RID: 33817 RVA: 0x002AC15C File Offset: 0x002AA35C
	public SpecialEventVisualMgr Visuals { get; private set; }

	// Token: 0x0600841A RID: 33818 RVA: 0x002AC168 File Offset: 0x002AA368
	private SpecialEventType RegisterEventTimingName(string eventName)
	{
		SpecialEventType specialEventType = SpecialEventManager.GetEventType(eventName);
		if (specialEventType != SpecialEventType.UNKNOWN)
		{
			return specialEventType;
		}
		foreach (object obj in Enum.GetValues(typeof(SpecialEventType)))
		{
			SpecialEventType specialEventType2 = (SpecialEventType)obj;
			if (EnumUtils.GetString<SpecialEventType>(specialEventType2) == eventName)
			{
				specialEventType = specialEventType2;
				this.m_eventTimingIdByEventName[eventName] = specialEventType;
				return specialEventType;
			}
		}
		int num = this.m_nextEventTimingId + 1;
		this.m_nextEventTimingId = num;
		specialEventType = (SpecialEventType)num;
		this.m_eventTimingIdByEventName[eventName] = specialEventType;
		return specialEventType;
	}

	// Token: 0x0600841B RID: 33819 RVA: 0x002AC21C File Offset: 0x002AA41C
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		InstantiatePrefab loadVisuals = new InstantiatePrefab("SpecialEventVisualMgr.prefab:9e2d0e3e4eb236f418ecaf0fa12732e4");
		yield return loadVisuals;
		this.Visuals = loadVisuals.InstantiatedPrefab.GetComponent<SpecialEventVisualMgr>();
		HearthstoneApplication.Get().WillReset += this.WillReset;
		this.InitializeHardcodedEvents();
		yield break;
	}

	// Token: 0x0600841C RID: 33820 RVA: 0x002450CA File Offset: 0x002432CA
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(IAssetLoader)
		};
	}

	// Token: 0x0600841D RID: 33821 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x0600841E RID: 33822 RVA: 0x002AC22C File Offset: 0x002AA42C
	private void WillReset()
	{
		this.m_eventTimingIdByEventName.Clear();
		this.m_eventTimings.Clear();
		if (this.m_forcedInactiveEvents != null)
		{
			this.m_forcedInactiveEvents.Clear();
		}
		if (this.m_forcedActiveEvents != null)
		{
			this.m_forcedActiveEvents.Clear();
		}
		this.HasReceivedEventTimingsFromServer = false;
		this.m_allEventAddedListeners.Clear();
		this.InitializeHardcodedEvents();
	}

	// Token: 0x0600841F RID: 33823 RVA: 0x002AC28D File Offset: 0x002AA48D
	public static SpecialEventManager Get()
	{
		return HearthstoneServices.Get<SpecialEventManager>();
	}

	// Token: 0x06008420 RID: 33824 RVA: 0x002AC294 File Offset: 0x002AA494
	private void InitializeHardcodedEvents()
	{
		foreach (object obj in Enum.GetValues(typeof(SpecialEventType)))
		{
			SpecialEventType specialEventType = (SpecialEventType)obj;
			if (specialEventType != SpecialEventType.UNKNOWN)
			{
				string @string = EnumUtils.GetString<SpecialEventType>(specialEventType);
				this.m_eventTimingIdByEventName[@string] = specialEventType;
			}
		}
		DateTime utcNow = DateTime.UtcNow;
		this.m_eventTimings[SpecialEventType.SPECIAL_EVENT_ALWAYS] = new SpecialEventManager.EventTiming(EnumUtils.GetString<SpecialEventType>(SpecialEventType.SPECIAL_EVENT_ALWAYS), 650L, SpecialEventType.SPECIAL_EVENT_ALWAYS, null, null);
		this.m_eventTimings[SpecialEventType.SPECIAL_EVENT_NEVER] = new SpecialEventManager.EventTiming(EnumUtils.GetString<SpecialEventType>(SpecialEventType.SPECIAL_EVENT_NEVER), 320L, SpecialEventType.SPECIAL_EVENT_NEVER, new DateTime?(utcNow.AddSeconds(-1.0)), new DateTime?(utcNow.AddSeconds(-2.0)));
	}

	// Token: 0x06008421 RID: 33825 RVA: 0x002AC3A4 File Offset: 0x002AA5A4
	public void InitEventTimingsFromServer(long devTimeOffsetSeconds, IList<SpecialEventTiming> serverEventTimingList)
	{
		this.m_forcedActiveEvents = (this.m_forcedInactiveEvents = null);
		this.DevTimeOffsetSeconds = devTimeOffsetSeconds;
		this.m_eventTimingIdByEventName.Clear();
		this.m_eventTimings.Clear();
		this.InitializeHardcodedEvents();
		List<SpecialEventType> list = new List<SpecialEventType>();
		DateTime utcNow = DateTime.UtcNow;
		for (int i = 0; i < serverEventTimingList.Count; i++)
		{
			SpecialEventTiming specialEventTiming = serverEventTimingList[i];
			SpecialEventType specialEventType = this.RegisterEventTimingName(specialEventTiming.EventName);
			DateTime? startTimeUtc = null;
			if (specialEventTiming.HasSecondsToStart)
			{
				startTimeUtc = new DateTime?(utcNow.AddSeconds((double)specialEventTiming.SecondsToStart));
			}
			DateTime? endTimeUtc = null;
			if (specialEventTiming.HasSecondsToEnd)
			{
				endTimeUtc = new DateTime?(utcNow.AddSeconds((double)specialEventTiming.SecondsToEnd));
			}
			this.m_eventTimings[specialEventType] = new SpecialEventManager.EventTiming(specialEventTiming.EventName, specialEventTiming.EventId, specialEventType, startTimeUtc, endTimeUtc);
			list.Add(specialEventType);
		}
		this.HasReceivedEventTimingsFromServer = true;
		foreach (SpecialEventType eventType in list)
		{
			this.FireEventAddedEvents(eventType);
		}
	}

	// Token: 0x06008422 RID: 33826 RVA: 0x002AC4E8 File Offset: 0x002AA6E8
	public DateTime? GetEventStartTimeUtc(SpecialEventType eventType)
	{
		SpecialEventManager.EventTiming eventTiming;
		if (this.m_eventTimings.TryGetValue(eventType, out eventTiming) && eventTiming != null)
		{
			return eventTiming.StartTimeUtc;
		}
		return null;
	}

	// Token: 0x06008423 RID: 33827 RVA: 0x002AC518 File Offset: 0x002AA718
	public DateTime? GetEventEndTimeUtc(SpecialEventType eventType)
	{
		SpecialEventManager.EventTiming eventTiming;
		if (this.m_eventTimings.TryGetValue(eventType, out eventTiming) && eventTiming != null)
		{
			return eventTiming.EndTimeUtc;
		}
		return null;
	}

	// Token: 0x06008424 RID: 33828 RVA: 0x002AC548 File Offset: 0x002AA748
	public bool GetEventRangeUtc(SpecialEventType eventType, out DateTime? start, out DateTime? end)
	{
		SpecialEventManager.EventTiming eventTiming;
		if (this.m_eventTimings.TryGetValue(eventType, out eventTiming) && eventTiming != null)
		{
			start = eventTiming.StartTimeUtc;
			end = eventTiming.EndTimeUtc;
			return true;
		}
		start = null;
		end = null;
		return false;
	}

	// Token: 0x06008425 RID: 33829 RVA: 0x002AC594 File Offset: 0x002AA794
	public bool HasEventStarted(SpecialEventType eventType)
	{
		SpecialEventManager.EventTiming eventTiming;
		return !this.IsEventForcedInactive(eventType) && (this.IsEventForcedActive(eventType) || (this.m_eventTimings.TryGetValue(eventType, out eventTiming) && eventTiming.HasStarted(DateTime.UtcNow)));
	}

	// Token: 0x06008426 RID: 33830 RVA: 0x002AC5D4 File Offset: 0x002AA7D4
	public bool HasEventStarted(string eventName)
	{
		SpecialEventType eventType = SpecialEventManager.GetEventType(eventName);
		return this.HasEventStarted(eventType);
	}

	// Token: 0x06008427 RID: 33831 RVA: 0x002AC5F0 File Offset: 0x002AA7F0
	public bool IsStartTimeInTheFuture(string eventName)
	{
		SpecialEventType eventType = SpecialEventManager.GetEventType(eventName);
		SpecialEventManager.EventTiming eventTiming;
		return eventType != SpecialEventType.UNKNOWN && !this.IsEventForcedInactive(eventType) && !this.IsEventForcedActive(eventType) && this.m_eventTimings.TryGetValue(eventType, out eventTiming) && eventTiming.IsStartTimeInTheFuture(DateTime.UtcNow);
	}

	// Token: 0x06008428 RID: 33832 RVA: 0x002AC640 File Offset: 0x002AA840
	public bool HasEventEnded(SpecialEventType eventType)
	{
		SpecialEventManager.EventTiming eventTiming;
		return !this.IsEventForcedInactive(eventType) && !this.IsEventForcedActive(eventType) && this.m_eventTimings.TryGetValue(eventType, out eventTiming) && eventTiming.HasEnded(DateTime.UtcNow);
	}

	// Token: 0x06008429 RID: 33833 RVA: 0x002AC680 File Offset: 0x002AA880
	public bool IsEventActive(SpecialEventType eventType, bool activeIfDoesNotExist)
	{
		return this.IsEventActive(eventType, activeIfDoesNotExist, DateTime.UtcNow);
	}

	// Token: 0x0600842A RID: 33834 RVA: 0x002AC68F File Offset: 0x002AA88F
	private bool IsEventActive(SpecialEventType eventType, bool activeIfDoesNotExist, DateTime utcTimestamp)
	{
		return this.IsEventActive_Impl(eventType, activeIfDoesNotExist, utcTimestamp);
	}

	// Token: 0x0600842B RID: 33835 RVA: 0x002AC69A File Offset: 0x002AA89A
	public bool IsEventActive_IgnoreSetRotation(SpecialEventType eventType, bool activeIfDoesNotExist)
	{
		return this.IsEventActive_Impl(eventType, activeIfDoesNotExist, DateTime.UtcNow);
	}

	// Token: 0x0600842C RID: 33836 RVA: 0x002AC6A9 File Offset: 0x002AA8A9
	public bool IsEventActive(string eventName, bool activeIfDoesNotExist)
	{
		return this.IsEventActive(eventName, activeIfDoesNotExist, DateTime.UtcNow);
	}

	// Token: 0x0600842D RID: 33837 RVA: 0x002AC6B8 File Offset: 0x002AA8B8
	public bool IsEventActive(string eventName, bool activeIfDoesNotExist, DateTime utcTimestamp)
	{
		if (string.IsNullOrEmpty(eventName))
		{
			return activeIfDoesNotExist;
		}
		SpecialEventType eventType = SpecialEventManager.GetEventType(eventName);
		if (eventType == SpecialEventType.UNKNOWN)
		{
			return activeIfDoesNotExist;
		}
		return this.IsEventActive(eventType, activeIfDoesNotExist, utcTimestamp);
	}

	// Token: 0x0600842E RID: 33838 RVA: 0x002AC6E8 File Offset: 0x002AA8E8
	public static SpecialEventType GetEventType(string eventName)
	{
		if (eventName == null)
		{
			return SpecialEventType.UNKNOWN;
		}
		SpecialEventType result;
		if (SpecialEventManager.Get().m_eventTimingIdByEventName.TryGetValue(eventName, out result))
		{
			return result;
		}
		return SpecialEventType.UNKNOWN;
	}

	// Token: 0x0600842F RID: 33839 RVA: 0x002AC714 File Offset: 0x002AA914
	public string GetName(SpecialEventType eventType)
	{
		SpecialEventManager.EventTiming eventTiming;
		if (this.m_eventTimings.TryGetValue(eventType, out eventTiming) && eventTiming != null)
		{
			return eventTiming.Name;
		}
		return EnumUtils.GetString<SpecialEventType>(eventType);
	}

	// Token: 0x06008430 RID: 33840 RVA: 0x002AC744 File Offset: 0x002AA944
	public void Cheat_OverrideSetRotationDate(DateTime date)
	{
		if (!this.m_eventTimings.ContainsKey(SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021) || !this.m_eventTimings.ContainsKey(SpecialEventType.SPECIAL_EVENT_PRE_SET_ROTATION_2021))
		{
			Error.AddWarning("Override Set Rotation Cheat Error", string.Format("Event {0} or {1} does not exist in m_eventTimings!", SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021, SpecialEventType.SPECIAL_EVENT_PRE_SET_ROTATION_2021), Array.Empty<object>());
			return;
		}
		this.m_eventTimings[SpecialEventType.SPECIAL_EVENT_PRE_SET_ROTATION_2021] = new SpecialEventManager.EventTiming(EnumUtils.GetString<SpecialEventType>(SpecialEventType.SPECIAL_EVENT_PRE_SET_ROTATION_2021), -1L, SpecialEventType.SPECIAL_EVENT_PRE_SET_ROTATION_2021, this.m_eventTimings[SpecialEventType.SPECIAL_EVENT_PRE_SET_ROTATION_2021].StartTimeUtc, new DateTime?(date));
		this.m_eventTimings[SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021] = new SpecialEventManager.EventTiming(EnumUtils.GetString<SpecialEventType>(SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021), -1L, SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021, new DateTime?(date), this.m_eventTimings[SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021].EndTimeUtc);
	}

	// Token: 0x06008432 RID: 33842 RVA: 0x002AC84C File Offset: 0x002AAA4C
	private bool IsEventActive_Impl(SpecialEventType eventType, bool activeIfDoesNotExist, DateTime localTimestamp)
	{
		if (eventType == SpecialEventType.SPECIAL_EVENT_ALWAYS)
		{
			return true;
		}
		if (eventType == SpecialEventType.SPECIAL_EVENT_NEVER)
		{
			return false;
		}
		if (this.IsEventForcedInactive(eventType))
		{
			return false;
		}
		if (this.IsEventForcedActive(eventType))
		{
			return true;
		}
		SpecialEventManager.EventTiming eventTiming;
		if (!this.m_eventTimings.TryGetValue(eventType, out eventTiming))
		{
			return activeIfDoesNotExist;
		}
		return eventTiming.IsActiveNow(localTimestamp);
	}

	// Token: 0x06008433 RID: 33843 RVA: 0x002AC89C File Offset: 0x002AAA9C
	public bool IsEventForcedInactive(SpecialEventType eventType)
	{
		return this.IsEventTimingForced(eventType, "Events.ForceInactive", ref this.m_forcedInactiveEvents);
	}

	// Token: 0x06008434 RID: 33844 RVA: 0x002AC8B0 File Offset: 0x002AAAB0
	public bool IsEventForcedActive(SpecialEventType eventType)
	{
		return this.IsEventTimingForced(eventType, "Events.ForceActive", ref this.m_forcedActiveEvents);
	}

	// Token: 0x06008435 RID: 33845 RVA: 0x002AC8C4 File Offset: 0x002AAAC4
	public SpecialEventType GetActiveEventType()
	{
		if (this.IsEventActive(SpecialEventType.GVG_PROMOTION, false))
		{
			return SpecialEventType.GVG_PROMOTION;
		}
		if (this.IsEventActive(SpecialEventType.SPECIAL_EVENT_PRE_TAVERN_BRAWL, false))
		{
			return SpecialEventType.SPECIAL_EVENT_PRE_TAVERN_BRAWL;
		}
		return SpecialEventType.IGNORE;
	}

	// Token: 0x06008436 RID: 33846 RVA: 0x002AC8E4 File Offset: 0x002AAAE4
	private bool IsEventTimingForced(SpecialEventType eventType, string clientConfigVarKey, ref HashSet<SpecialEventType> forcedEventSet)
	{
		if (!HearthstoneApplication.IsInternal())
		{
			return false;
		}
		if (forcedEventSet == null)
		{
			forcedEventSet = new HashSet<SpecialEventType>(new SpecialEventManager.SpecialEventTypeComparer());
			string str = Vars.Key(clientConfigVarKey).GetStr(null);
			if (string.IsNullOrEmpty(str))
			{
				return false;
			}
			string[] array = str.Split(new char[]
			{
				' ',
				',',
				';'
			}, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				SpecialEventType eventType2 = SpecialEventManager.GetEventType(array[i]);
				if (eventType2 != SpecialEventType.UNKNOWN)
				{
					forcedEventSet.Add(eventType2);
				}
			}
		}
		return forcedEventSet.Contains(eventType);
	}

	// Token: 0x06008437 RID: 33847 RVA: 0x002AC968 File Offset: 0x002AAB68
	public long GetEventIdFromEventName(string eventName)
	{
		KeyValuePair<SpecialEventType, SpecialEventManager.EventTiming> keyValuePair = (from kvp in this.m_eventTimings
		where kvp.Value.Name.Equals(eventName, StringComparison.OrdinalIgnoreCase)
		select kvp).First<KeyValuePair<SpecialEventType, SpecialEventManager.EventTiming>>();
		if (keyValuePair.Value == null)
		{
			return -1L;
		}
		return keyValuePair.Value.Id;
	}

	// Token: 0x04006F16 RID: 28438
	public const long EVENT_TIMING_ID_INVALID = -1L;

	// Token: 0x04006F17 RID: 28439
	private const long EVENT_TIMING_ID_NEVER = 320L;

	// Token: 0x04006F18 RID: 28440
	private const long EVENT_TIMING_ID_ALWAYS = 650L;

	// Token: 0x04006F19 RID: 28441
	private Dictionary<SpecialEventType, List<SpecialEventManager.EventAddedListener>> m_allEventAddedListeners = new Dictionary<SpecialEventType, List<SpecialEventManager.EventAddedListener>>();

	// Token: 0x04006F1A RID: 28442
	private int m_nextEventTimingId = 10000000;

	// Token: 0x04006F1B RID: 28443
	private Map<string, SpecialEventType> m_eventTimingIdByEventName = new Map<string, SpecialEventType>();

	// Token: 0x04006F1C RID: 28444
	private Map<SpecialEventType, SpecialEventManager.EventTiming> m_eventTimings = new Map<SpecialEventType, SpecialEventManager.EventTiming>(new SpecialEventManager.SpecialEventTypeComparer());

	// Token: 0x04006F1D RID: 28445
	private HashSet<SpecialEventType> m_forcedInactiveEvents;

	// Token: 0x04006F1E RID: 28446
	private HashSet<SpecialEventType> m_forcedActiveEvents;

	// Token: 0x02002625 RID: 9765
	private class EventTiming
	{
		// Token: 0x060135E2 RID: 79330 RVA: 0x00531E18 File Offset: 0x00530018
		public EventTiming(string name, long id, SpecialEventType eventType, DateTime? startTimeUtc, DateTime? endTimeUtc)
		{
			this.Id = id;
			this.Name = name;
			this.Type = eventType;
			this.StartTimeUtc = startTimeUtc;
			this.EndTimeUtc = endTimeUtc;
			if (this.StartTimeUtc != null && this.StartTimeUtc.Value.Kind != DateTimeKind.Utc)
			{
				this.StartTimeUtc = new DateTime?(this.StartTimeUtc.Value.ToUniversalTime());
			}
			if (this.EndTimeUtc != null && this.EndTimeUtc.Value.Kind != DateTimeKind.Utc)
			{
				this.EndTimeUtc = new DateTime?(this.EndTimeUtc.Value.ToUniversalTime());
			}
		}

		// Token: 0x17002C1B RID: 11291
		// (get) Token: 0x060135E3 RID: 79331 RVA: 0x00531EE4 File Offset: 0x005300E4
		// (set) Token: 0x060135E4 RID: 79332 RVA: 0x00531EEC File Offset: 0x005300EC
		public string Name { get; private set; }

		// Token: 0x17002C1C RID: 11292
		// (get) Token: 0x060135E5 RID: 79333 RVA: 0x00531EF5 File Offset: 0x005300F5
		// (set) Token: 0x060135E6 RID: 79334 RVA: 0x00531EFD File Offset: 0x005300FD
		public long Id { get; private set; }

		// Token: 0x17002C1D RID: 11293
		// (get) Token: 0x060135E7 RID: 79335 RVA: 0x00531F06 File Offset: 0x00530106
		// (set) Token: 0x060135E8 RID: 79336 RVA: 0x00531F0E File Offset: 0x0053010E
		public SpecialEventType Type { get; private set; }

		// Token: 0x17002C1E RID: 11294
		// (get) Token: 0x060135E9 RID: 79337 RVA: 0x00531F17 File Offset: 0x00530117
		// (set) Token: 0x060135EA RID: 79338 RVA: 0x00531F1F File Offset: 0x0053011F
		public DateTime? StartTimeUtc { get; private set; }

		// Token: 0x17002C1F RID: 11295
		// (get) Token: 0x060135EB RID: 79339 RVA: 0x00531F28 File Offset: 0x00530128
		// (set) Token: 0x060135EC RID: 79340 RVA: 0x00531F30 File Offset: 0x00530130
		public DateTime? EndTimeUtc { get; private set; }

		// Token: 0x060135ED RID: 79341 RVA: 0x00531F3C File Offset: 0x0053013C
		public bool HasStarted(DateTime utcTimestamp)
		{
			if (this.StartTimeUtc == null)
			{
				return true;
			}
			if (utcTimestamp.Kind != DateTimeKind.Utc)
			{
				utcTimestamp = utcTimestamp.ToUniversalTime();
			}
			return utcTimestamp >= this.StartTimeUtc.Value;
		}

		// Token: 0x060135EE RID: 79342 RVA: 0x00531F84 File Offset: 0x00530184
		public bool HasEnded(DateTime utcTimestamp)
		{
			if (this.EndTimeUtc == null)
			{
				return false;
			}
			if (utcTimestamp.Kind != DateTimeKind.Utc)
			{
				utcTimestamp = utcTimestamp.ToUniversalTime();
			}
			return utcTimestamp > this.EndTimeUtc.Value;
		}

		// Token: 0x060135EF RID: 79343 RVA: 0x00531FCC File Offset: 0x005301CC
		public bool IsActiveNow(DateTime utcTimestamp)
		{
			if (this.StartTimeUtc != null && this.EndTimeUtc != null && this.EndTimeUtc.Value < this.StartTimeUtc.Value)
			{
				return false;
			}
			if (utcTimestamp.Kind != DateTimeKind.Utc)
			{
				utcTimestamp = utcTimestamp.ToUniversalTime();
			}
			return this.HasStarted(utcTimestamp) && !this.HasEnded(utcTimestamp);
		}

		// Token: 0x060135F0 RID: 79344 RVA: 0x00532048 File Offset: 0x00530248
		public bool IsStartTimeInTheFuture(DateTime utcTimestamp)
		{
			if (utcTimestamp.Kind != DateTimeKind.Utc)
			{
				utcTimestamp = utcTimestamp.ToUniversalTime();
			}
			return this.StartTimeUtc != null && this.StartTimeUtc.Value > utcTimestamp;
		}
	}

	// Token: 0x02002626 RID: 9766
	private class SpecialEventTypeComparer : IEqualityComparer<SpecialEventType>
	{
		// Token: 0x060135F1 RID: 79345 RVA: 0x003F15F5 File Offset: 0x003EF7F5
		public bool Equals(SpecialEventType x, SpecialEventType y)
		{
			return x == y;
		}

		// Token: 0x060135F2 RID: 79346 RVA: 0x0028AEDB File Offset: 0x002890DB
		public int GetHashCode(SpecialEventType obj)
		{
			return (int)obj;
		}
	}

	// Token: 0x02002627 RID: 9767
	// (Invoke) Token: 0x060135F5 RID: 79349
	public delegate void EventAddedCallback(object userData);

	// Token: 0x02002628 RID: 9768
	private class EventAddedListener : EventListener<SpecialEventManager.EventAddedCallback>
	{
		// Token: 0x060135F8 RID: 79352 RVA: 0x0053208E File Offset: 0x0053028E
		public void Fire()
		{
			this.m_callback(this.m_userData);
		}
	}
}
