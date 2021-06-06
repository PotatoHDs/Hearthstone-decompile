using System.Collections.Generic;

public class PowerHistoryTimeline
{
	public class PowerHistoryTimelineEntryComparer : IComparer<PowerHistoryTimelineEntry>
	{
		public int Compare(PowerHistoryTimelineEntry a, PowerHistoryTimelineEntry b)
		{
			if (a == null)
			{
				if (b == null)
				{
					return 0;
				}
				return -1;
			}
			if (b == null)
			{
				return 1;
			}
			return a.expectedStartOffset.CompareTo(b.expectedStartOffset);
		}
	}

	public int m_firstTaskId;

	public int m_lastTaskId;

	public int m_slushTime;

	public float m_startTime;

	public float m_endTime;

	public List<PowerHistoryTimelineEntry> m_orderedEvents = new List<PowerHistoryTimelineEntry>();

	public Map<int, int> m_orderedEventIndexLookup = new Map<int, int>();

	public void AddTimelineEntry(int taskId, int slushTime)
	{
		PowerHistoryTimelineEntry powerHistoryTimelineEntry = new PowerHistoryTimelineEntry();
		powerHistoryTimelineEntry.taskId = taskId;
		powerHistoryTimelineEntry.expectedTime = slushTime;
		if (m_orderedEvents.Count == 0)
		{
			powerHistoryTimelineEntry.expectedStartOffset = 0;
		}
		else
		{
			PowerHistoryTimelineEntry powerHistoryTimelineEntry2 = m_orderedEvents[m_orderedEvents.Count - 1];
			powerHistoryTimelineEntry.expectedStartOffset = powerHistoryTimelineEntry2.expectedStartOffset + powerHistoryTimelineEntry2.expectedTime;
		}
		m_orderedEvents.Add(powerHistoryTimelineEntry);
		m_orderedEventIndexLookup.Add(taskId, m_orderedEvents.Count - 1);
	}

	public PowerHistoryTimelineEntry GetTimelineEntry(int taskId)
	{
		if (m_orderedEventIndexLookup.ContainsKey(taskId))
		{
			int num = m_orderedEventIndexLookup[taskId];
			if (num < m_orderedEvents.Count)
			{
				return m_orderedEvents[num];
			}
		}
		return null;
	}

	public PowerHistoryTimelineEntry GetCurrentExpectedTask(float time)
	{
		if (m_orderedEvents.Count == 0)
		{
			return null;
		}
		PowerHistoryTimelineEntry powerHistoryTimelineEntry = new PowerHistoryTimelineEntry();
		powerHistoryTimelineEntry.expectedStartOffset = (int)(time - m_startTime);
		int num = m_orderedEvents.BinarySearch(powerHistoryTimelineEntry, new PowerHistoryTimelineEntryComparer());
		if (num >= 0)
		{
			return m_orderedEvents[num];
		}
		int num2 = ~num;
		return m_orderedEvents[num2 - 1];
	}
}
