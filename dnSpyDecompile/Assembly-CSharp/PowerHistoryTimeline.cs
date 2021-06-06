using System;
using System.Collections.Generic;

// Token: 0x0200033F RID: 831
public class PowerHistoryTimeline
{
	// Token: 0x06003011 RID: 12305 RVA: 0x000F5ED8 File Offset: 0x000F40D8
	public void AddTimelineEntry(int taskId, int slushTime)
	{
		PowerHistoryTimelineEntry powerHistoryTimelineEntry = new PowerHistoryTimelineEntry();
		powerHistoryTimelineEntry.taskId = taskId;
		powerHistoryTimelineEntry.expectedTime = slushTime;
		if (this.m_orderedEvents.Count == 0)
		{
			powerHistoryTimelineEntry.expectedStartOffset = 0;
		}
		else
		{
			PowerHistoryTimelineEntry powerHistoryTimelineEntry2 = this.m_orderedEvents[this.m_orderedEvents.Count - 1];
			powerHistoryTimelineEntry.expectedStartOffset = powerHistoryTimelineEntry2.expectedStartOffset + powerHistoryTimelineEntry2.expectedTime;
		}
		this.m_orderedEvents.Add(powerHistoryTimelineEntry);
		this.m_orderedEventIndexLookup.Add(taskId, this.m_orderedEvents.Count - 1);
	}

	// Token: 0x06003012 RID: 12306 RVA: 0x000F5F60 File Offset: 0x000F4160
	public PowerHistoryTimelineEntry GetTimelineEntry(int taskId)
	{
		if (this.m_orderedEventIndexLookup.ContainsKey(taskId))
		{
			int num = this.m_orderedEventIndexLookup[taskId];
			if (num < this.m_orderedEvents.Count)
			{
				return this.m_orderedEvents[num];
			}
		}
		return null;
	}

	// Token: 0x06003013 RID: 12307 RVA: 0x000F5FA4 File Offset: 0x000F41A4
	public PowerHistoryTimelineEntry GetCurrentExpectedTask(float time)
	{
		if (this.m_orderedEvents.Count == 0)
		{
			return null;
		}
		PowerHistoryTimelineEntry powerHistoryTimelineEntry = new PowerHistoryTimelineEntry();
		powerHistoryTimelineEntry.expectedStartOffset = (int)(time - this.m_startTime);
		int num = this.m_orderedEvents.BinarySearch(powerHistoryTimelineEntry, new PowerHistoryTimeline.PowerHistoryTimelineEntryComparer());
		if (num >= 0)
		{
			return this.m_orderedEvents[num];
		}
		int num2 = ~num;
		return this.m_orderedEvents[num2 - 1];
	}

	// Token: 0x04001AB9 RID: 6841
	public int m_firstTaskId;

	// Token: 0x04001ABA RID: 6842
	public int m_lastTaskId;

	// Token: 0x04001ABB RID: 6843
	public int m_slushTime;

	// Token: 0x04001ABC RID: 6844
	public float m_startTime;

	// Token: 0x04001ABD RID: 6845
	public float m_endTime;

	// Token: 0x04001ABE RID: 6846
	public List<PowerHistoryTimelineEntry> m_orderedEvents = new List<PowerHistoryTimelineEntry>();

	// Token: 0x04001ABF RID: 6847
	public Map<int, int> m_orderedEventIndexLookup = new Map<int, int>();

	// Token: 0x020016E0 RID: 5856
	public class PowerHistoryTimelineEntryComparer : IComparer<PowerHistoryTimelineEntry>
	{
		// Token: 0x0600E5EA RID: 58858 RVA: 0x0040F76D File Offset: 0x0040D96D
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
			else
			{
				if (b == null)
				{
					return 1;
				}
				return a.expectedStartOffset.CompareTo(b.expectedStartOffset);
			}
		}
	}
}
