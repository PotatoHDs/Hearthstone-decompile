using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// Token: 0x02000687 RID: 1671
public class LoggerDebugWindow : DebuggerGuiWindow
{
	// Token: 0x06005D7C RID: 23932 RVA: 0x001E68F4 File Offset: 0x001E4AF4
	public LoggerDebugWindow(string title, Vector2 guiSize, IEnumerable<object> categories) : base(title, null, true, true)
	{
		this.m_title = title;
		this.m_categories = categories.ToList<object>();
		this.m_GUISize = guiSize;
		this.m_OnGui = new DebuggerGui.LayoutGui(this.LayoutMessages);
		this.m_categoryToggles = new Dictionary<object, bool>();
		this.m_entries = new QueueList<LoggerDebugWindow.LogEntry>();
		this.m_levels = new Dictionary<Log.LogLevel, int>();
		this.m_textStyle = new GUIStyle("box")
		{
			fontSize = 17,
			alignment = TextAnchor.UpperLeft,
			wordWrap = true,
			clipping = TextClipping.Clip,
			stretchWidth = true
		};
	}

	// Token: 0x06005D7D RID: 23933 RVA: 0x001E6998 File Offset: 0x001E4B98
	public void AddEntry(LoggerDebugWindow.LogEntry entry, bool addAlert = false)
	{
		if (entry.text.Length > 2100)
		{
			entry.text = entry.text.Substring(0, 2100);
		}
		this.m_entries.Enqueue(entry);
		if (this.m_levels.ContainsKey(entry.category))
		{
			Dictionary<Log.LogLevel, int> levels = this.m_levels;
			Log.LogLevel category = entry.category;
			int num = levels[category];
			levels[category] = num + 1;
		}
		else
		{
			this.m_levels.Add(entry.category, 1);
		}
		if (addAlert && this.AreLogsDisplayed(entry.category))
		{
			this.m_alertCount++;
			if (this.m_alertCount == 1)
			{
				base.IsShown = true;
				base.IsExpanded = true;
			}
		}
	}

	// Token: 0x06005D7E RID: 23934 RVA: 0x001E6A5B File Offset: 0x001E4C5B
	public void Clear()
	{
		this.m_entries.Clear();
		this.m_levels.Clear();
		this.m_alertCount = 0;
	}

	// Token: 0x06005D7F RID: 23935 RVA: 0x001E6A7A File Offset: 0x001E4C7A
	public int GetCount()
	{
		return this.m_entries.Count;
	}

	// Token: 0x06005D80 RID: 23936 RVA: 0x001E6A88 File Offset: 0x001E4C88
	public int GetCount(Log.LogLevel category)
	{
		int result;
		this.m_levels.TryGetValue(category, out result);
		return result;
	}

	// Token: 0x06005D81 RID: 23937 RVA: 0x001E6AA5 File Offset: 0x001E4CA5
	public IEnumerable<LoggerDebugWindow.LogEntry> GetEntries()
	{
		return this.m_entries;
	}

	// Token: 0x17000590 RID: 1424
	// (get) Token: 0x06005D82 RID: 23938 RVA: 0x001E6AAD File Offset: 0x001E4CAD
	// (set) Token: 0x06005D83 RID: 23939 RVA: 0x001E6AB5 File Offset: 0x001E4CB5
	public string FilterString { get; set; }

	// Token: 0x06005D84 RID: 23940 RVA: 0x001E6AC0 File Offset: 0x001E4CC0
	public void Update()
	{
		int num = Math.Min(this.m_entries.Count - 999, 100);
		while (num-- > 0)
		{
			LoggerDebugWindow.LogEntry logEntry = this.m_entries.Dequeue();
			if (this.m_levels.ContainsKey(logEntry.category))
			{
				Dictionary<Log.LogLevel, int> levels = this.m_levels;
				Log.LogLevel category = logEntry.category;
				int num2 = levels[category];
				levels[category] = num2 - 1;
			}
		}
		if (base.IsExpanded || this.m_entries.Count == 0)
		{
			base.Title = this.m_title;
			return;
		}
		base.Title = string.Format("{0} ({1})", this.m_title, this.GetCount());
	}

	// Token: 0x06005D85 RID: 23941 RVA: 0x001E6B70 File Offset: 0x001E4D70
	public void ToggleLogsDisplay(object category, bool display)
	{
		this.m_categoryToggles[category] = display;
		base.InvokeOnChanged();
	}

	// Token: 0x06005D86 RID: 23942 RVA: 0x001E6B88 File Offset: 0x001E4D88
	public bool AreLogsDisplayed(object category)
	{
		bool result;
		if (category == null || !this.m_categoryToggles.TryGetValue(category, out result))
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06005D87 RID: 23943 RVA: 0x001E6BAC File Offset: 0x001E4DAC
	public Rect LayoutLog(Rect space)
	{
		GUI.skin.settings.selectionColor = Color.blue;
		Rect position = space;
		Rect viewRect = new Rect(0f, 0f, space.width - 20f, 0f);
		string[] array = string.IsNullOrEmpty(this.FilterString) ? null : this.FilterString.ToLowerInvariant().Split(new char[]
		{
			' '
		});
		List<int> list = new List<int>();
		new StringBuilder();
		for (int i = 0; i < this.m_entries.Count; i++)
		{
			if (this.AreLogsDisplayed(this.m_entries[i].category))
			{
				if (array != null && array.Count<string>() > 0)
				{
					bool flag = true;
					string text = this.m_entries[i].text.ToLowerInvariant();
					foreach (string value in array)
					{
						if (!text.ToLowerInvariant().Contains(value))
						{
							flag = false;
							break;
						}
					}
					if (!flag)
					{
						goto IL_129;
					}
				}
				list.Add(i);
				viewRect.height += this.GetLogEntryHeight(this.m_entries[i], viewRect.width);
			}
			IL_129:;
		}
		float num = viewRect.height - position.height;
		if (this.m_autoScroll)
		{
			this.m_scrollPosition.y = num;
		}
		this.m_scrollPosition = GUI.BeginScrollView(position, this.m_scrollPosition, viewRect, false, true);
		this.m_autoScroll = (this.m_scrollPosition.y >= num);
		Rect position2 = new Rect(0f, 0f, viewRect.width - 60f, 0f);
		for (int k = 0; k < list.Count; k++)
		{
			int index = list[k];
			LoggerDebugWindow.LogEntry logEntry = this.m_entries.ElementAtOrDefault(index);
			position2.height = this.GetLogEntryHeight(logEntry, viewRect.width);
			GUI.TextArea(position2, logEntry.text, this.m_textStyle);
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

	// Token: 0x06005D88 RID: 23944 RVA: 0x001E6E35 File Offset: 0x001E5035
	private Rect LayoutMessages(Rect space)
	{
		if (this.CustomLayout != null)
		{
			return this.CustomLayout(space);
		}
		return this.LayoutLog(space);
	}

	// Token: 0x06005D89 RID: 23945 RVA: 0x001E6E53 File Offset: 0x001E5053
	private float GetLogEntryHeight(LoggerDebugWindow.LogEntry entry, float width)
	{
		return this.m_textStyle.CalcHeight(new GUIContent(entry.text), width) + 5f;
	}

	// Token: 0x06005D8A RID: 23946 RVA: 0x001E6E74 File Offset: 0x001E5074
	internal override string SerializeToString()
	{
		string text = LoggerDebugWindow.SERIAL_ID;
		List<string> list = new List<string>();
		foreach (object category in this.m_categories)
		{
			list.Add(this.AreLogsDisplayed(category) ? "1" : "0");
		}
		text += string.Join(",", list.ToArray());
		text += base.SerializeToString();
		return text;
	}

	// Token: 0x06005D8B RID: 23947 RVA: 0x001E6F0C File Offset: 0x001E510C
	internal override void DeserializeFromString(string str)
	{
		int num = str.IndexOf(LoggerDebugWindow.SERIAL_ID);
		int num2 = str.IndexOf(DebuggerGuiWindow.SERIAL_ID);
		if (num >= 0)
		{
			num += LoggerDebugWindow.SERIAL_ID.Length;
			List<string> source = str.Substring(num, (num2 > num) ? (num2 - num) : (str.Length - num)).Split(new char[]
			{
				','
			}).ToList<string>();
			for (int i = 0; i < this.m_categories.Count; i++)
			{
				string a = source.ElementAtOrDefault(i);
				object key = this.m_categories[i];
				this.m_categoryToggles[key] = (a != "0");
			}
		}
		if (num2 >= 0)
		{
			base.DeserializeFromString(str.Substring(num2));
		}
	}

	// Token: 0x04004F08 RID: 20232
	public DebuggerGui.LayoutGui CustomLayout;

	// Token: 0x04004F09 RID: 20233
	private const int MAX_ENTRIES = 999;

	// Token: 0x04004F0A RID: 20234
	internal new static string SERIAL_ID = "[LOG]";

	// Token: 0x04004F0B RID: 20235
	private Vector2 m_GUISize;

	// Token: 0x04004F0C RID: 20236
	private QueueList<LoggerDebugWindow.LogEntry> m_entries;

	// Token: 0x04004F0D RID: 20237
	private Dictionary<Log.LogLevel, int> m_levels;

	// Token: 0x04004F0E RID: 20238
	private string m_title;

	// Token: 0x04004F0F RID: 20239
	private GUIStyle m_textStyle;

	// Token: 0x04004F10 RID: 20240
	private List<object> m_categories;

	// Token: 0x04004F11 RID: 20241
	private Dictionary<object, bool> m_categoryToggles;

	// Token: 0x04004F12 RID: 20242
	private Vector2 m_scrollPosition;

	// Token: 0x04004F13 RID: 20243
	private bool m_autoScroll = true;

	// Token: 0x04004F14 RID: 20244
	private int m_alertCount;

	// Token: 0x020021A2 RID: 8610
	public class LogEntry
	{
		// Token: 0x06012427 RID: 74791 RVA: 0x005028E7 File Offset: 0x00500AE7
		public override string ToString()
		{
			return string.Format("[{0}] {1}", this.category, this.text);
		}

		// Token: 0x0400E0EB RID: 57579
		public Log.LogLevel category;

		// Token: 0x0400E0EC RID: 57580
		public string text;
	}
}
