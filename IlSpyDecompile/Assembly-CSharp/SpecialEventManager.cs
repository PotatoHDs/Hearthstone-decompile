using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using PegasusUtil;

public class SpecialEventManager : IService
{
	private class EventTiming
	{
		public string Name { get; private set; }

		public long Id { get; private set; }

		public SpecialEventType Type { get; private set; }

		public DateTime? StartTimeUtc { get; private set; }

		public DateTime? EndTimeUtc { get; private set; }

		public EventTiming(string name, long id, SpecialEventType eventType, DateTime? startTimeUtc, DateTime? endTimeUtc)
		{
			Id = id;
			Name = name;
			Type = eventType;
			StartTimeUtc = startTimeUtc;
			EndTimeUtc = endTimeUtc;
			if (StartTimeUtc.HasValue && StartTimeUtc.Value.Kind != DateTimeKind.Utc)
			{
				StartTimeUtc = StartTimeUtc.Value.ToUniversalTime();
			}
			if (EndTimeUtc.HasValue && EndTimeUtc.Value.Kind != DateTimeKind.Utc)
			{
				EndTimeUtc = EndTimeUtc.Value.ToUniversalTime();
			}
		}

		public bool HasStarted(DateTime utcTimestamp)
		{
			if (!StartTimeUtc.HasValue)
			{
				return true;
			}
			if (utcTimestamp.Kind != DateTimeKind.Utc)
			{
				utcTimestamp = utcTimestamp.ToUniversalTime();
			}
			return utcTimestamp >= StartTimeUtc.Value;
		}

		public bool HasEnded(DateTime utcTimestamp)
		{
			if (!EndTimeUtc.HasValue)
			{
				return false;
			}
			if (utcTimestamp.Kind != DateTimeKind.Utc)
			{
				utcTimestamp = utcTimestamp.ToUniversalTime();
			}
			return utcTimestamp > EndTimeUtc.Value;
		}

		public bool IsActiveNow(DateTime utcTimestamp)
		{
			if (StartTimeUtc.HasValue && EndTimeUtc.HasValue && EndTimeUtc.Value < StartTimeUtc.Value)
			{
				return false;
			}
			if (utcTimestamp.Kind != DateTimeKind.Utc)
			{
				utcTimestamp = utcTimestamp.ToUniversalTime();
			}
			if (HasStarted(utcTimestamp))
			{
				return !HasEnded(utcTimestamp);
			}
			return false;
		}

		public bool IsStartTimeInTheFuture(DateTime utcTimestamp)
		{
			if (utcTimestamp.Kind != DateTimeKind.Utc)
			{
				utcTimestamp = utcTimestamp.ToUniversalTime();
			}
			if (StartTimeUtc.HasValue)
			{
				return StartTimeUtc.Value > utcTimestamp;
			}
			return false;
		}
	}

	private class SpecialEventTypeComparer : IEqualityComparer<SpecialEventType>
	{
		public bool Equals(SpecialEventType x, SpecialEventType y)
		{
			return x == y;
		}

		public int GetHashCode(SpecialEventType obj)
		{
			return (int)obj;
		}
	}

	public delegate void EventAddedCallback(object userData);

	private class EventAddedListener : EventListener<EventAddedCallback>
	{
		public void Fire()
		{
			m_callback(m_userData);
		}
	}

	public const long EVENT_TIMING_ID_INVALID = -1L;

	private const long EVENT_TIMING_ID_NEVER = 320L;

	private const long EVENT_TIMING_ID_ALWAYS = 650L;

	private Dictionary<SpecialEventType, List<EventAddedListener>> m_allEventAddedListeners = new Dictionary<SpecialEventType, List<EventAddedListener>>();

	private int m_nextEventTimingId = 10000000;

	private Map<string, SpecialEventType> m_eventTimingIdByEventName = new Map<string, SpecialEventType>();

	private Map<SpecialEventType, EventTiming> m_eventTimings = new Map<SpecialEventType, EventTiming>(new SpecialEventTypeComparer());

	private HashSet<SpecialEventType> m_forcedInactiveEvents;

	private HashSet<SpecialEventType> m_forcedActiveEvents;

	public HashSet<SpecialEventType> AllKnownEvents => new HashSet<SpecialEventType>(m_eventTimings.Keys);

	public bool HasReceivedEventTimingsFromServer { get; private set; }

	public long DevTimeOffsetSeconds { get; private set; }

	public SpecialEventVisualMgr Visuals { get; private set; }

	public bool AddEventAddedListener(EventAddedCallback callback, SpecialEventType eventType, object userData = null)
	{
		if (callback == null)
		{
			return false;
		}
		EventAddedListener eventAddedListener = new EventAddedListener();
		eventAddedListener.SetCallback(callback);
		eventAddedListener.SetUserData(userData);
		if (!m_allEventAddedListeners.ContainsKey(eventType))
		{
			m_allEventAddedListeners[eventType] = new List<EventAddedListener>();
		}
		m_allEventAddedListeners[eventType].Add(eventAddedListener);
		return true;
	}

	private void FireEventAddedEvents(SpecialEventType eventType)
	{
		if (!m_allEventAddedListeners.TryGetValue(eventType, out var value))
		{
			return;
		}
		foreach (EventAddedListener item in value)
		{
			item.Fire();
		}
	}

	private SpecialEventType RegisterEventTimingName(string eventName)
	{
		SpecialEventType eventType = GetEventType(eventName);
		if (eventType != SpecialEventType.UNKNOWN)
		{
			return eventType;
		}
		foreach (SpecialEventType value in Enum.GetValues(typeof(SpecialEventType)))
		{
			if (EnumUtils.GetString(value) == eventName)
			{
				eventType = value;
				m_eventTimingIdByEventName[eventName] = eventType;
				return eventType;
			}
		}
		eventType = (SpecialEventType)(++m_nextEventTimingId);
		m_eventTimingIdByEventName[eventName] = eventType;
		return eventType;
	}

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		InstantiatePrefab loadVisuals = new InstantiatePrefab("SpecialEventVisualMgr.prefab:9e2d0e3e4eb236f418ecaf0fa12732e4");
		yield return loadVisuals;
		Visuals = loadVisuals.InstantiatedPrefab.GetComponent<SpecialEventVisualMgr>();
		HearthstoneApplication.Get().WillReset += WillReset;
		InitializeHardcodedEvents();
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(IAssetLoader) };
	}

	public void Shutdown()
	{
	}

	private void WillReset()
	{
		m_eventTimingIdByEventName.Clear();
		m_eventTimings.Clear();
		if (m_forcedInactiveEvents != null)
		{
			m_forcedInactiveEvents.Clear();
		}
		if (m_forcedActiveEvents != null)
		{
			m_forcedActiveEvents.Clear();
		}
		HasReceivedEventTimingsFromServer = false;
		m_allEventAddedListeners.Clear();
		InitializeHardcodedEvents();
	}

	public static SpecialEventManager Get()
	{
		return HearthstoneServices.Get<SpecialEventManager>();
	}

	private void InitializeHardcodedEvents()
	{
		foreach (SpecialEventType value in Enum.GetValues(typeof(SpecialEventType)))
		{
			if (value != SpecialEventType.UNKNOWN)
			{
				string @string = EnumUtils.GetString(value);
				m_eventTimingIdByEventName[@string] = value;
			}
		}
		DateTime utcNow = DateTime.UtcNow;
		m_eventTimings[SpecialEventType.SPECIAL_EVENT_ALWAYS] = new EventTiming(EnumUtils.GetString(SpecialEventType.SPECIAL_EVENT_ALWAYS), 650L, SpecialEventType.SPECIAL_EVENT_ALWAYS, null, null);
		m_eventTimings[SpecialEventType.SPECIAL_EVENT_NEVER] = new EventTiming(EnumUtils.GetString(SpecialEventType.SPECIAL_EVENT_NEVER), 320L, SpecialEventType.SPECIAL_EVENT_NEVER, utcNow.AddSeconds(-1.0), utcNow.AddSeconds(-2.0));
	}

	public void InitEventTimingsFromServer(long devTimeOffsetSeconds, IList<SpecialEventTiming> serverEventTimingList)
	{
		m_forcedActiveEvents = (m_forcedInactiveEvents = null);
		DevTimeOffsetSeconds = devTimeOffsetSeconds;
		m_eventTimingIdByEventName.Clear();
		m_eventTimings.Clear();
		InitializeHardcodedEvents();
		List<SpecialEventType> list = new List<SpecialEventType>();
		DateTime utcNow = DateTime.UtcNow;
		for (int i = 0; i < serverEventTimingList.Count; i++)
		{
			SpecialEventTiming specialEventTiming = serverEventTimingList[i];
			SpecialEventType specialEventType = RegisterEventTimingName(specialEventTiming.EventName);
			DateTime? startTimeUtc = null;
			if (specialEventTiming.HasSecondsToStart)
			{
				startTimeUtc = utcNow.AddSeconds(specialEventTiming.SecondsToStart);
			}
			DateTime? endTimeUtc = null;
			if (specialEventTiming.HasSecondsToEnd)
			{
				endTimeUtc = utcNow.AddSeconds(specialEventTiming.SecondsToEnd);
			}
			m_eventTimings[specialEventType] = new EventTiming(specialEventTiming.EventName, specialEventTiming.EventId, specialEventType, startTimeUtc, endTimeUtc);
			list.Add(specialEventType);
		}
		HasReceivedEventTimingsFromServer = true;
		foreach (SpecialEventType item in list)
		{
			FireEventAddedEvents(item);
		}
	}

	public DateTime? GetEventStartTimeUtc(SpecialEventType eventType)
	{
		if (m_eventTimings.TryGetValue(eventType, out var value) && value != null)
		{
			return value.StartTimeUtc;
		}
		return null;
	}

	public DateTime? GetEventEndTimeUtc(SpecialEventType eventType)
	{
		if (m_eventTimings.TryGetValue(eventType, out var value) && value != null)
		{
			return value.EndTimeUtc;
		}
		return null;
	}

	public bool GetEventRangeUtc(SpecialEventType eventType, out DateTime? start, out DateTime? end)
	{
		if (m_eventTimings.TryGetValue(eventType, out var value) && value != null)
		{
			start = value.StartTimeUtc;
			end = value.EndTimeUtc;
			return true;
		}
		start = null;
		end = null;
		return false;
	}

	public bool HasEventStarted(SpecialEventType eventType)
	{
		if (IsEventForcedInactive(eventType))
		{
			return false;
		}
		if (IsEventForcedActive(eventType))
		{
			return true;
		}
		if (!m_eventTimings.TryGetValue(eventType, out var value))
		{
			return false;
		}
		return value.HasStarted(DateTime.UtcNow);
	}

	public bool HasEventStarted(string eventName)
	{
		SpecialEventType eventType = GetEventType(eventName);
		return HasEventStarted(eventType);
	}

	public bool IsStartTimeInTheFuture(string eventName)
	{
		SpecialEventType eventType = GetEventType(eventName);
		if (eventType == SpecialEventType.UNKNOWN)
		{
			return false;
		}
		if (IsEventForcedInactive(eventType))
		{
			return false;
		}
		if (IsEventForcedActive(eventType))
		{
			return false;
		}
		if (!m_eventTimings.TryGetValue(eventType, out var value))
		{
			return false;
		}
		return value.IsStartTimeInTheFuture(DateTime.UtcNow);
	}

	public bool HasEventEnded(SpecialEventType eventType)
	{
		if (IsEventForcedInactive(eventType))
		{
			return false;
		}
		if (IsEventForcedActive(eventType))
		{
			return false;
		}
		if (!m_eventTimings.TryGetValue(eventType, out var value))
		{
			return false;
		}
		return value.HasEnded(DateTime.UtcNow);
	}

	public bool IsEventActive(SpecialEventType eventType, bool activeIfDoesNotExist)
	{
		return IsEventActive(eventType, activeIfDoesNotExist, DateTime.UtcNow);
	}

	private bool IsEventActive(SpecialEventType eventType, bool activeIfDoesNotExist, DateTime utcTimestamp)
	{
		return IsEventActive_Impl(eventType, activeIfDoesNotExist, utcTimestamp);
	}

	public bool IsEventActive_IgnoreSetRotation(SpecialEventType eventType, bool activeIfDoesNotExist)
	{
		return IsEventActive_Impl(eventType, activeIfDoesNotExist, DateTime.UtcNow);
	}

	public bool IsEventActive(string eventName, bool activeIfDoesNotExist)
	{
		return IsEventActive(eventName, activeIfDoesNotExist, DateTime.UtcNow);
	}

	public bool IsEventActive(string eventName, bool activeIfDoesNotExist, DateTime utcTimestamp)
	{
		if (string.IsNullOrEmpty(eventName))
		{
			return activeIfDoesNotExist;
		}
		SpecialEventType eventType = GetEventType(eventName);
		if (eventType == SpecialEventType.UNKNOWN)
		{
			return activeIfDoesNotExist;
		}
		return IsEventActive(eventType, activeIfDoesNotExist, utcTimestamp);
	}

	public static SpecialEventType GetEventType(string eventName)
	{
		if (eventName == null)
		{
			return SpecialEventType.UNKNOWN;
		}
		if (Get().m_eventTimingIdByEventName.TryGetValue(eventName, out var value))
		{
			return value;
		}
		return SpecialEventType.UNKNOWN;
	}

	public string GetName(SpecialEventType eventType)
	{
		if (m_eventTimings.TryGetValue(eventType, out var value) && value != null)
		{
			return value.Name;
		}
		return EnumUtils.GetString(eventType);
	}

	public void Cheat_OverrideSetRotationDate(DateTime date)
	{
		if (!m_eventTimings.ContainsKey(SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021) || !m_eventTimings.ContainsKey(SpecialEventType.SPECIAL_EVENT_PRE_SET_ROTATION_2021))
		{
			Error.AddWarning("Override Set Rotation Cheat Error", $"Event {SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021} or {SpecialEventType.SPECIAL_EVENT_PRE_SET_ROTATION_2021} does not exist in m_eventTimings!");
			return;
		}
		m_eventTimings[SpecialEventType.SPECIAL_EVENT_PRE_SET_ROTATION_2021] = new EventTiming(EnumUtils.GetString(SpecialEventType.SPECIAL_EVENT_PRE_SET_ROTATION_2021), -1L, SpecialEventType.SPECIAL_EVENT_PRE_SET_ROTATION_2021, m_eventTimings[SpecialEventType.SPECIAL_EVENT_PRE_SET_ROTATION_2021].StartTimeUtc, date);
		m_eventTimings[SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021] = new EventTiming(EnumUtils.GetString(SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021), -1L, SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021, date, m_eventTimings[SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021].EndTimeUtc);
	}

	private bool IsEventActive_Impl(SpecialEventType eventType, bool activeIfDoesNotExist, DateTime localTimestamp)
	{
		switch (eventType)
		{
		case SpecialEventType.SPECIAL_EVENT_ALWAYS:
			return true;
		case SpecialEventType.SPECIAL_EVENT_NEVER:
			return false;
		default:
		{
			if (IsEventForcedInactive(eventType))
			{
				return false;
			}
			if (IsEventForcedActive(eventType))
			{
				return true;
			}
			if (!m_eventTimings.TryGetValue(eventType, out var value))
			{
				return activeIfDoesNotExist;
			}
			return value.IsActiveNow(localTimestamp);
		}
		}
	}

	public bool IsEventForcedInactive(SpecialEventType eventType)
	{
		return IsEventTimingForced(eventType, "Events.ForceInactive", ref m_forcedInactiveEvents);
	}

	public bool IsEventForcedActive(SpecialEventType eventType)
	{
		return IsEventTimingForced(eventType, "Events.ForceActive", ref m_forcedActiveEvents);
	}

	public SpecialEventType GetActiveEventType()
	{
		if (IsEventActive(SpecialEventType.GVG_PROMOTION, activeIfDoesNotExist: false))
		{
			return SpecialEventType.GVG_PROMOTION;
		}
		if (IsEventActive(SpecialEventType.SPECIAL_EVENT_PRE_TAVERN_BRAWL, activeIfDoesNotExist: false))
		{
			return SpecialEventType.SPECIAL_EVENT_PRE_TAVERN_BRAWL;
		}
		return SpecialEventType.IGNORE;
	}

	private bool IsEventTimingForced(SpecialEventType eventType, string clientConfigVarKey, ref HashSet<SpecialEventType> forcedEventSet)
	{
		if (!HearthstoneApplication.IsInternal())
		{
			return false;
		}
		if (forcedEventSet == null)
		{
			forcedEventSet = new HashSet<SpecialEventType>(new SpecialEventTypeComparer());
			string str = Vars.Key(clientConfigVarKey).GetStr(null);
			if (string.IsNullOrEmpty(str))
			{
				return false;
			}
			string[] array = str.Split(new char[3] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				SpecialEventType eventType2 = GetEventType(array[i]);
				if (eventType2 != SpecialEventType.UNKNOWN)
				{
					forcedEventSet.Add(eventType2);
				}
			}
		}
		return forcedEventSet.Contains(eventType);
	}

	public long GetEventIdFromEventName(string eventName)
	{
		KeyValuePair<SpecialEventType, EventTiming> keyValuePair = m_eventTimings.Where((KeyValuePair<SpecialEventType, EventTiming> kvp) => kvp.Value.Name.Equals(eventName, StringComparison.OrdinalIgnoreCase)).First();
		if (keyValuePair.Value == null)
		{
			return -1L;
		}
		return keyValuePair.Value.Id;
	}
}
