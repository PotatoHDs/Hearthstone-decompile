using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LoggerDebugWindow : DebuggerGuiWindow
{
	public class LogEntry
	{
		public Log.LogLevel category;

		public string text;

		public override string ToString()
		{
			return $"[{category}] {text}";
		}
	}

	public LayoutGui CustomLayout;

	private const int MAX_ENTRIES = 999;

	internal new static string SERIAL_ID = "[LOG]";

	private Vector2 m_GUISize;

	private QueueList<LogEntry> m_entries;

	private Dictionary<Log.LogLevel, int> m_levels;

	private string m_title;

	private GUIStyle m_textStyle;

	private List<object> m_categories;

	private Dictionary<object, bool> m_categoryToggles;

	private Vector2 m_scrollPosition;

	private bool m_autoScroll = true;

	private int m_alertCount;

	public string FilterString { get; set; }

	public LoggerDebugWindow(string title, Vector2 guiSize, IEnumerable<object> categories)
		: base(title, null)
	{
		m_title = title;
		m_categories = categories.ToList();
		m_GUISize = guiSize;
		m_OnGui = LayoutMessages;
		m_categoryToggles = new Dictionary<object, bool>();
		m_entries = new QueueList<LogEntry>();
		m_levels = new Dictionary<Log.LogLevel, int>();
		m_textStyle = new GUIStyle("box")
		{
			fontSize = 17,
			alignment = TextAnchor.UpperLeft,
			wordWrap = true,
			clipping = TextClipping.Clip,
			stretchWidth = true
		};
	}

	public void AddEntry(LogEntry entry, bool addAlert = false)
	{
		if (entry.text.Length > 2100)
		{
			entry.text = entry.text.Substring(0, 2100);
		}
		m_entries.Enqueue(entry);
		if (m_levels.ContainsKey(entry.category))
		{
			m_levels[entry.category]++;
		}
		else
		{
			m_levels.Add(entry.category, 1);
		}
		if (addAlert && AreLogsDisplayed(entry.category))
		{
			m_alertCount++;
			if (m_alertCount == 1)
			{
				base.IsShown = true;
				base.IsExpanded = true;
			}
		}
	}

	public void Clear()
	{
		m_entries.Clear();
		m_levels.Clear();
		m_alertCount = 0;
	}

	public int GetCount()
	{
		return m_entries.Count;
	}

	public int GetCount(Log.LogLevel category)
	{
		m_levels.TryGetValue(category, out var value);
		return value;
	}

	public IEnumerable<LogEntry> GetEntries()
	{
		return m_entries;
	}

	public void Update()
	{
		int num = Math.Min(m_entries.Count - 999, 100);
		while (num-- > 0)
		{
			LogEntry logEntry = m_entries.Dequeue();
			if (m_levels.ContainsKey(logEntry.category))
			{
				m_levels[logEntry.category]--;
			}
		}
		if (base.IsExpanded || m_entries.Count == 0)
		{
			base.Title = m_title;
		}
		else
		{
			base.Title = $"{m_title} ({GetCount()})";
		}
	}

	public void ToggleLogsDisplay(object category, bool display)
	{
		m_categoryToggles[category] = display;
		InvokeOnChanged();
	}

	public bool AreLogsDisplayed(object category)
	{
		if (category == null || !m_categoryToggles.TryGetValue(category, out var value))
		{
			return true;
		}
		return value;
	}

	public Rect LayoutLog(Rect space)
	{
		GUI.skin.settings.selectionColor = Color.blue;
		Rect position = space;
		Rect viewRect = new Rect(0f, 0f, space.width - 20f, 0f);
		string[] array = (string.IsNullOrEmpty(FilterString) ? null : FilterString.ToLowerInvariant().Split(' '));
		List<int> list = new List<int>();
		new StringBuilder();
		for (int i = 0; i < m_entries.Count; i++)
		{
			if (!AreLogsDisplayed(m_entries[i].category))
			{
				continue;
			}
			if (array != null && array.Count() > 0)
			{
				bool flag = true;
				string text = m_entries[i].text.ToLowerInvariant();
				string[] array2 = array;
				foreach (string value in array2)
				{
					if (!text.ToLowerInvariant().Contains(value))
					{
						flag = false;
						break;
					}
				}
				if (!flag)
				{
					continue;
				}
			}
			list.Add(i);
			viewRect.height += GetLogEntryHeight(m_entries[i], viewRect.width);
		}
		float num = viewRect.height - position.height;
		if (m_autoScroll)
		{
			m_scrollPosition.y = num;
		}
		m_scrollPosition = GUI.BeginScrollView(position, m_scrollPosition, viewRect, alwaysShowHorizontal: false, alwaysShowVertical: true);
		m_autoScroll = m_scrollPosition.y >= num;
		Rect position2 = new Rect(0f, 0f, viewRect.width - 60f, 0f);
		for (int k = 0; k < list.Count; k++)
		{
			int index = list[k];
			LogEntry logEntry = m_entries.ElementAtOrDefault(index);
			position2.height = GetLogEntryHeight(logEntry, viewRect.width);
			GUI.TextArea(position2, logEntry.text, m_textStyle);
			if (GUI.Button(new Rect(viewRect.width - 55f, position2.y, 55f, position2.height), "COPY"))
			{
				GUIUtility.systemCopyBuffer = logEntry.text;
			}
			position2.yMin += position2.height;
		}
		GUI.EndScrollView();
		space.yMin = space.yMax;
		return space;
	}

	private Rect LayoutMessages(Rect space)
	{
		if (CustomLayout != null)
		{
			return CustomLayout(space);
		}
		return LayoutLog(space);
	}

	private float GetLogEntryHeight(LogEntry entry, float width)
	{
		return m_textStyle.CalcHeight(new GUIContent(entry.text), width) + 5f;
	}

	internal override string SerializeToString()
	{
		string sERIAL_ID = SERIAL_ID;
		List<string> list = new List<string>();
		foreach (object category in m_categories)
		{
			list.Add(AreLogsDisplayed(category) ? "1" : "0");
		}
		sERIAL_ID += string.Join(",", list.ToArray());
		return sERIAL_ID + base.SerializeToString();
	}

	internal override void DeserializeFromString(string str)
	{
		int num = str.IndexOf(SERIAL_ID);
		int num2 = str.IndexOf(DebuggerGuiWindow.SERIAL_ID);
		if (num >= 0)
		{
			num += SERIAL_ID.Length;
			List<string> source = str.Substring(num, (num2 > num) ? (num2 - num) : (str.Length - num)).Split(',').ToList();
			for (int i = 0; i < m_categories.Count; i++)
			{
				string text = source.ElementAtOrDefault(i);
				object key = m_categories[i];
				m_categoryToggles[key] = text != "0";
			}
		}
		if (num2 >= 0)
		{
			base.DeserializeFromString(str.Substring(num2));
		}
	}
}
