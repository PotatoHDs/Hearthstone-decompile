using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheatsDebugWindow : DebuggerGuiWindow
{
	private class CheatEntry
	{
		public virtual string SearchString => Title.ToLowerInvariant();

		public string Title { get; protected set; }

		public CheatEntry(string title)
		{
			Title = title;
		}
	}

	private class CheatCategory : CheatEntry
	{
		public List<CheatEntry> children = new List<CheatEntry>();

		public override string SearchString => Path.ToLowerInvariant();

		public string Path { get; protected set; }

		public int Depth
		{
			get
			{
				int num = 0;
				int num2 = Path.IndexOf(':');
				while (num2 > 0 && num2 < Path.Length)
				{
					num++;
					num2 = Path.IndexOf(':', num2 + 1);
				}
				return num;
			}
		}

		public CheatCategory(string path)
			: base("")
		{
			Path = path;
			int num = path.LastIndexOf(':');
			base.Title = ((num > 0) ? path.Substring(num + 1) : path);
		}

		public static List<string> GetLineage(string fullPath)
		{
			List<string> list = new List<string>();
			for (int num = fullPath.IndexOf(':'); num > 0; num = fullPath.IndexOf(':', num + 1))
			{
				list.Add(fullPath.Substring(0, num));
			}
			list.Add(fullPath);
			return list;
		}
	}

	private class CheatCommand : CheatEntry
	{
		public string example = "";

		public string description = "";

		public string args = "";

		public CheatCategory parent;

		public override string SearchString => (base.Title + " " + description).ToLowerInvariant();

		public CheatCommand(string name)
			: base(name)
		{
			CheatMgr cheatMgr = CheatMgr.Get();
			if (cheatMgr != null)
			{
				cheatMgr.cheatArgs.TryGetValue(name, out args);
				cheatMgr.cheatDesc.TryGetValue(name, out description);
				cheatMgr.cheatExamples.TryGetValue(name, out example);
			}
		}
	}

	private class CheatOption : CheatEntry
	{
		public Option option;

		public CheatCategory parent;

		public CheatOption(string title, Option option)
			: base(title)
		{
			this.option = option;
		}
	}

	private Vector2 m_GUISize;

	private CheatCommand m_currentlyDisplayedCheat;

	private string m_cheatSearchTerm = "";

	private Vector2 m_cheatScrollPosition;

	private Dictionary<string, CheatCategory> m_categories;

	private GUIStyle m_labelStyle;

	public CheatsDebugWindow(Vector2 guiSize)
		: base("Cheats", null)
	{
		m_GUISize = guiSize;
		m_OnGui = LayoutCheats;
		m_labelStyle = new GUIStyle("box")
		{
			alignment = TextAnchor.MiddleLeft,
			wordWrap = false,
			clipping = TextClipping.Clip,
			stretchWidth = false
		};
		Rect scaledScreen = GetScaledScreen();
		ResizeToFit(scaledScreen.width / 2f, scaledScreen.height / 2f);
	}

	private void InitializeCheatsAsNecessary()
	{
		if (m_categories != null)
		{
			return;
		}
		CheatMgr cheatMgr = CheatMgr.Get();
		Options options = Options.Get();
		if (cheatMgr == null || options == null || cheatMgr.GetCheatCommands().Count() == 0)
		{
			return;
		}
		m_categories = new Dictionary<string, CheatCategory>();
		CheatCategory cheatCategory = new CheatCategory("options");
		m_categories.Add(cheatCategory.Path, cheatCategory);
		foreach (KeyValuePair<Option, string> clientOption in options.GetClientOptions())
		{
			string value = clientOption.Value;
			Option key = clientOption.Key;
			string text = options.GetOptionType(key).ToString();
			if (text.StartsWith("System."))
			{
				text = text.Remove(0, 7);
			}
			string text2 = cheatCategory.Path + ":" + text;
			CheatCategory value2 = null;
			if (!m_categories.TryGetValue(text2, out value2))
			{
				value2 = new CheatCategory(text2);
				m_categories.Add(text2, value2);
			}
			CheatOption cheatOption = new CheatOption(value, key);
			cheatOption.parent = value2;
			value2.children.Add(cheatOption);
		}
		foreach (string cheatCommand2 in cheatMgr.GetCheatCommands())
		{
			string cheatCategory2 = cheatMgr.GetCheatCategory(cheatCommand2);
			CheatCategory value3 = null;
			if (!m_categories.TryGetValue(cheatCategory2, out value3) || value3 == null)
			{
				value3 = new CheatCategory(cheatCategory2);
				m_categories.Add(cheatCategory2, value3);
			}
			CheatCommand cheatCommand = new CheatCommand(cheatCommand2);
			cheatCommand.parent = value3;
			value3.children.Add(cheatCommand);
		}
	}

	private Rect LayoutCheats(Rect space)
	{
		InitializeCheatsAsNecessary();
		if (m_categories == null)
		{
			return space;
		}
		if (m_currentlyDisplayedCheat == null)
		{
			space = LayoutFilteredCheats(space);
		}
		else
		{
			if (GUI.Button(new Rect(space.x, space.y, m_GUISize.x, m_GUISize.y), "Back"))
			{
				m_currentlyDisplayedCheat = null;
				return space;
			}
			if (GUI.Button(new Rect(space.xMax - m_GUISize.x, space.y, m_GUISize.x, m_GUISize.y), "Hide Console"))
			{
				CheatMgr.Get().HideConsole();
				return space;
			}
			space.yMin += m_GUISize.y;
			string text = m_currentlyDisplayedCheat.Title;
			if (!string.IsNullOrEmpty(m_currentlyDisplayedCheat.args))
			{
				text += $" {m_currentlyDisplayedCheat.args}";
			}
			GUI.Box(new Rect(space.xMin, space.yMin, space.width, m_GUISize.y), text, m_labelStyle);
			space.yMin += 1.1f * m_GUISize.y;
			if (!string.IsNullOrEmpty(m_currentlyDisplayedCheat.description))
			{
				GUI.Box(new Rect(space.xMin, space.yMin, space.width, m_GUISize.y), m_currentlyDisplayedCheat.description, m_labelStyle);
				space.yMin += 1.1f * m_GUISize.y;
			}
			if (!string.IsNullOrEmpty(m_currentlyDisplayedCheat.example))
			{
				GUI.Box(new Rect(space.xMin, space.yMin, space.width, m_GUISize.y), $"Example: {m_currentlyDisplayedCheat.example}", m_labelStyle);
				space.yMin += 1.1f * m_GUISize.y;
			}
			GUI.Label(new Rect(space.min, m_GUISize), "History:");
			space.yMin += m_GUISize.y;
			string text2 = Options.Get().GetOption(Option.CHEAT_HISTORY).ToString();
			string text3 = ";" + m_currentlyDisplayedCheat.Title;
			for (int num = text2.IndexOf(m_currentlyDisplayedCheat.Title); num >= 0; num = text2.IndexOf(text3, num + text3.Length))
			{
				int num2 = text2.IndexOf(m_currentlyDisplayedCheat.Title, num);
				int num3 = text2.IndexOf(';', num2);
				string text4 = null;
				text4 = ((num3 <= 0) ? text2.Substring(num2) : text2.Substring(num2, num3 - num2));
				if (GUI.Button(new Rect(space.xMin, space.yMin, space.width, m_GUISize.y), text4, m_labelStyle))
				{
					CheatMgr.Get().ShowConsole();
					UniversalInputManager.Get().SetInputText(text4, moveCursorToEnd: true);
				}
				space.yMin += m_GUISize.y;
			}
		}
		return space;
	}

	private Rect LayoutFilteredCheats(Rect space)
	{
		float y = m_GUISize.y;
		GUI.Label(new Rect(space.xMin + 10f, space.yMin + 5f, y * 2f, y), "Filter:");
		Rect position = new Rect(space.xMin + y * 2f, space.yMin, 0f, m_GUISize.y);
		position.xMax = space.xMax;
		m_cheatSearchTerm = GUI.TextField(position, m_cheatSearchTerm);
		space.yMin += y;
		List<CheatEntry> list = CollectCheats(m_cheatSearchTerm);
		Rect position2 = space;
		float num = (PlatformSettings.IsMobile() ? 20f : 10f) - 5f;
		position2.xMin += num;
		position2.xMax -= num;
		position2.yMax -= num;
		Rect viewRect = new Rect(0f, 0f, position2.width - 18f, (float)list.Count * y);
		m_cheatScrollPosition = GUI.BeginScrollView(position2, m_cheatScrollPosition, viewRect, alwaysShowHorizontal: false, alwaysShowVertical: true);
		float num2 = 0f;
		foreach (CheatEntry item in list)
		{
			Rect position3 = new Rect(0f, num2, viewRect.width, y);
			if (item is CheatCategory)
			{
				CheatCategory cheatCategory = item as CheatCategory;
				position3.xMin += (float)cheatCategory.Depth * 15f;
				GUI.Label(position3, cheatCategory.Title);
			}
			else
			{
				int num3 = 0;
				string text = "";
				string text2 = "";
				Action action = null;
				if (item is CheatOption)
				{
					CheatOption cheatOption = item as CheatOption;
					string optionName = cheatOption.Title;
					object value = null;
					Option option = cheatOption.option;
					OptionDataTables.s_defaultsMap.TryGetValue(option, out value);
					num3 = cheatOption.parent.Depth + 1;
					if (Options.Get().GetOptionType(option) == typeof(bool))
					{
						text = string.Format("{0}={1}", optionName, Options.Get().GetBool(option) ? "1" : "0");
						action = delegate
						{
							Options.Get().SetBool(option, !Options.Get().GetBool(option));
						};
					}
					else
					{
						text = optionName;
						action = delegate
						{
							CheatMgr.Get().ShowConsole();
							UniversalInputManager.Get().SetInputText($"set {optionName} ", moveCursorToEnd: true);
						};
					}
					object option2 = Options.Get().GetOption(option);
					text2 = $"={option2}";
					if (value != null)
					{
						text2 += string.Format(" (default={1})", option2, value);
					}
				}
				else if (item is CheatCommand)
				{
					CheatCommand command = item as CheatCommand;
					num3 = command.parent.Depth + 1;
					text = command.Title;
					action = delegate
					{
						CheatMgr.Get().ShowConsole();
						UniversalInputManager.Get().SetInputText(command.Title, moveCursorToEnd: true);
						m_currentlyDisplayedCheat = command;
					};
					text2 = command.description;
				}
				position3.xMin += (float)num3 * 15f;
				Rect position4 = new Rect(position3.xMin, position3.yMin, 200f, y);
				if (GUI.Button(position4, text))
				{
					action?.Invoke();
				}
				if (!string.IsNullOrEmpty(text2))
				{
					GUI.Box(new Rect(position4.xMax, position3.yMin, position3.xMax - position4.xMax, y), text2, m_labelStyle);
				}
			}
			num2 += y;
		}
		GUI.EndScrollView();
		space.yMin = position2.yMax;
		return space;
	}

	private List<CheatEntry> CollectCheats(string filter)
	{
		List<string> list = m_categories.Keys.ToList();
		list.Sort();
		List<CheatEntry> list2 = new List<CheatEntry>();
		string[] terms = filter.ToLowerInvariant().Split(' ');
		foreach (string item in list)
		{
			CheatCategory cheatCategory = m_categories[item];
			bool flag = false;
			List<CheatEntry> list3 = new List<CheatEntry>();
			if (CheatMatchesFilter(cheatCategory, terms))
			{
				flag = true;
				list3.AddRange(cheatCategory.children);
			}
			else
			{
				foreach (CheatEntry child in cheatCategory.children)
				{
					if (CheatMatchesFilter(child, terms))
					{
						flag = true;
						list3.Add(child);
					}
				}
			}
			if (flag)
			{
				foreach (string item2 in CheatCategory.GetLineage(cheatCategory.Path))
				{
					CheatCategory value = null;
					if (m_categories.TryGetValue(item2, out value) && !list2.Contains(value))
					{
						list2.Add(value);
					}
				}
			}
			foreach (CheatEntry item3 in list3)
			{
				list2.Add(item3);
			}
		}
		return list2;
	}

	private bool CheatMatchesFilter(CheatEntry cheat, string[] terms)
	{
		if (terms.Count() == 0)
		{
			return true;
		}
		string text = cheat?.SearchString;
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		foreach (string value in terms)
		{
			if (!text.Contains(value))
			{
				return false;
			}
		}
		return true;
	}
}
