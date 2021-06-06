using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000683 RID: 1667
public class CheatsDebugWindow : DebuggerGuiWindow
{
	// Token: 0x06005D4D RID: 23885 RVA: 0x001E4FB8 File Offset: 0x001E31B8
	public CheatsDebugWindow(Vector2 guiSize) : base("Cheats", null, true, true)
	{
		this.m_GUISize = guiSize;
		this.m_OnGui = new DebuggerGui.LayoutGui(this.LayoutCheats);
		this.m_labelStyle = new GUIStyle("box")
		{
			alignment = TextAnchor.MiddleLeft,
			wordWrap = false,
			clipping = TextClipping.Clip,
			stretchWidth = false
		};
		Rect scaledScreen = base.GetScaledScreen();
		base.ResizeToFit(scaledScreen.width / 2f, scaledScreen.height / 2f);
	}

	// Token: 0x06005D4E RID: 23886 RVA: 0x001E5050 File Offset: 0x001E3250
	private void InitializeCheatsAsNecessary()
	{
		if (this.m_categories != null)
		{
			return;
		}
		CheatMgr cheatMgr = CheatMgr.Get();
		Options options = Options.Get();
		if (cheatMgr == null || options == null || cheatMgr.GetCheatCommands().Count<string>() == 0)
		{
			return;
		}
		this.m_categories = new Dictionary<string, CheatsDebugWindow.CheatCategory>();
		CheatsDebugWindow.CheatCategory cheatCategory = new CheatsDebugWindow.CheatCategory("options");
		this.m_categories.Add(cheatCategory.Path, cheatCategory);
		foreach (KeyValuePair<Option, string> keyValuePair in options.GetClientOptions())
		{
			string value = keyValuePair.Value;
			Option key = keyValuePair.Key;
			string text = options.GetOptionType(key).ToString();
			if (text.StartsWith("System."))
			{
				text = text.Remove(0, 7);
			}
			string text2 = cheatCategory.Path + ":" + text;
			CheatsDebugWindow.CheatCategory cheatCategory2 = null;
			if (!this.m_categories.TryGetValue(text2, out cheatCategory2))
			{
				cheatCategory2 = new CheatsDebugWindow.CheatCategory(text2);
				this.m_categories.Add(text2, cheatCategory2);
			}
			CheatsDebugWindow.CheatOption cheatOption = new CheatsDebugWindow.CheatOption(value, key);
			cheatOption.parent = cheatCategory2;
			cheatCategory2.children.Add(cheatOption);
		}
		foreach (string text3 in cheatMgr.GetCheatCommands())
		{
			string cheatCategory3 = cheatMgr.GetCheatCategory(text3);
			CheatsDebugWindow.CheatCategory cheatCategory4 = null;
			if (!this.m_categories.TryGetValue(cheatCategory3, out cheatCategory4) || cheatCategory4 == null)
			{
				cheatCategory4 = new CheatsDebugWindow.CheatCategory(cheatCategory3);
				this.m_categories.Add(cheatCategory3, cheatCategory4);
			}
			CheatsDebugWindow.CheatCommand cheatCommand = new CheatsDebugWindow.CheatCommand(text3);
			cheatCommand.parent = cheatCategory4;
			cheatCategory4.children.Add(cheatCommand);
		}
	}

	// Token: 0x06005D4F RID: 23887 RVA: 0x001E5224 File Offset: 0x001E3424
	private Rect LayoutCheats(Rect space)
	{
		this.InitializeCheatsAsNecessary();
		if (this.m_categories == null)
		{
			return space;
		}
		if (this.m_currentlyDisplayedCheat == null)
		{
			space = this.LayoutFilteredCheats(space);
		}
		else
		{
			if (GUI.Button(new Rect(space.x, space.y, this.m_GUISize.x, this.m_GUISize.y), "Back"))
			{
				this.m_currentlyDisplayedCheat = null;
				return space;
			}
			if (GUI.Button(new Rect(space.xMax - this.m_GUISize.x, space.y, this.m_GUISize.x, this.m_GUISize.y), "Hide Console"))
			{
				CheatMgr.Get().HideConsole();
				return space;
			}
			space.yMin += this.m_GUISize.y;
			string text = this.m_currentlyDisplayedCheat.Title;
			if (!string.IsNullOrEmpty(this.m_currentlyDisplayedCheat.args))
			{
				text += string.Format(" {0}", this.m_currentlyDisplayedCheat.args);
			}
			GUI.Box(new Rect(space.xMin, space.yMin, space.width, this.m_GUISize.y), text, this.m_labelStyle);
			space.yMin += 1.1f * this.m_GUISize.y;
			if (!string.IsNullOrEmpty(this.m_currentlyDisplayedCheat.description))
			{
				GUI.Box(new Rect(space.xMin, space.yMin, space.width, this.m_GUISize.y), this.m_currentlyDisplayedCheat.description, this.m_labelStyle);
				space.yMin += 1.1f * this.m_GUISize.y;
			}
			if (!string.IsNullOrEmpty(this.m_currentlyDisplayedCheat.example))
			{
				GUI.Box(new Rect(space.xMin, space.yMin, space.width, this.m_GUISize.y), string.Format("Example: {0}", this.m_currentlyDisplayedCheat.example), this.m_labelStyle);
				space.yMin += 1.1f * this.m_GUISize.y;
			}
			GUI.Label(new Rect(space.min, this.m_GUISize), "History:");
			space.yMin += this.m_GUISize.y;
			string text2 = Options.Get().GetOption(Option.CHEAT_HISTORY).ToString();
			string text3 = ";" + this.m_currentlyDisplayedCheat.Title;
			for (int i = text2.IndexOf(this.m_currentlyDisplayedCheat.Title); i >= 0; i = text2.IndexOf(text3, i + text3.Length))
			{
				int num = text2.IndexOf(this.m_currentlyDisplayedCheat.Title, i);
				int num2 = text2.IndexOf(';', num);
				string text4;
				if (num2 > 0)
				{
					text4 = text2.Substring(num, num2 - num);
				}
				else
				{
					text4 = text2.Substring(num);
				}
				if (GUI.Button(new Rect(space.xMin, space.yMin, space.width, this.m_GUISize.y), text4, this.m_labelStyle))
				{
					CheatMgr.Get().ShowConsole();
					UniversalInputManager.Get().SetInputText(text4, true);
				}
				space.yMin += this.m_GUISize.y;
			}
		}
		return space;
	}

	// Token: 0x06005D50 RID: 23888 RVA: 0x001E559C File Offset: 0x001E379C
	private Rect LayoutFilteredCheats(Rect space)
	{
		float y = this.m_GUISize.y;
		GUI.Label(new Rect(space.xMin + 10f, space.yMin + 5f, y * 2f, y), "Filter:");
		this.m_cheatSearchTerm = GUI.TextField(new Rect(space.xMin + y * 2f, space.yMin, 0f, this.m_GUISize.y)
		{
			xMax = space.xMax
		}, this.m_cheatSearchTerm);
		space.yMin += y;
		List<CheatsDebugWindow.CheatEntry> list = this.CollectCheats(this.m_cheatSearchTerm);
		Rect position = space;
		float num = (PlatformSettings.IsMobile() ? 20f : 10f) - 5f;
		position.xMin += num;
		position.xMax -= num;
		position.yMax -= num;
		Rect viewRect = new Rect(0f, 0f, position.width - 18f, (float)list.Count * y);
		this.m_cheatScrollPosition = GUI.BeginScrollView(position, this.m_cheatScrollPosition, viewRect, false, true);
		float num2 = 0f;
		foreach (CheatsDebugWindow.CheatEntry cheatEntry in list)
		{
			Rect position2 = new Rect(0f, num2, viewRect.width, y);
			if (cheatEntry is CheatsDebugWindow.CheatCategory)
			{
				CheatsDebugWindow.CheatCategory cheatCategory = cheatEntry as CheatsDebugWindow.CheatCategory;
				position2.xMin += (float)cheatCategory.Depth * 15f;
				GUI.Label(position2, cheatCategory.Title);
			}
			else
			{
				int num3 = 0;
				string text = "";
				string text2 = "";
				Action action = null;
				if (cheatEntry is CheatsDebugWindow.CheatOption)
				{
					CheatsDebugWindow.CheatOption cheatOption = cheatEntry as CheatsDebugWindow.CheatOption;
					string optionName = cheatOption.Title;
					object obj = null;
					Option option = cheatOption.option;
					OptionDataTables.s_defaultsMap.TryGetValue(option, out obj);
					num3 = cheatOption.parent.Depth + 1;
					if (Options.Get().GetOptionType(option) == typeof(bool))
					{
						text = string.Format("{0}={1}", optionName, Options.Get().GetBool(option) ? "1" : "0");
						action = delegate()
						{
							Options.Get().SetBool(option, !Options.Get().GetBool(option));
						};
					}
					else
					{
						text = optionName;
						action = delegate()
						{
							CheatMgr.Get().ShowConsole();
							UniversalInputManager.Get().SetInputText(string.Format("set {0} ", optionName), true);
						};
					}
					object option2 = Options.Get().GetOption(option);
					text2 = string.Format("={0}", option2);
					if (obj != null)
					{
						text2 += string.Format(" (default={1})", option2, obj);
					}
				}
				else if (cheatEntry is CheatsDebugWindow.CheatCommand)
				{
					CheatsDebugWindow.CheatCommand command = cheatEntry as CheatsDebugWindow.CheatCommand;
					num3 = command.parent.Depth + 1;
					text = command.Title;
					action = delegate()
					{
						CheatMgr.Get().ShowConsole();
						UniversalInputManager.Get().SetInputText(command.Title, true);
						this.m_currentlyDisplayedCheat = command;
					};
					text2 = command.description;
				}
				position2.xMin += (float)num3 * 15f;
				Rect position3 = new Rect(position2.xMin, position2.yMin, 200f, y);
				if (GUI.Button(position3, text) && action != null)
				{
					action();
				}
				if (!string.IsNullOrEmpty(text2))
				{
					GUI.Box(new Rect(position3.xMax, position2.yMin, position2.xMax - position3.xMax, y), text2, this.m_labelStyle);
				}
			}
			num2 += y;
		}
		GUI.EndScrollView();
		space.yMin = position.yMax;
		return space;
	}

	// Token: 0x06005D51 RID: 23889 RVA: 0x001E59AC File Offset: 0x001E3BAC
	private List<CheatsDebugWindow.CheatEntry> CollectCheats(string filter)
	{
		List<string> list = this.m_categories.Keys.ToList<string>();
		list.Sort();
		List<CheatsDebugWindow.CheatEntry> list2 = new List<CheatsDebugWindow.CheatEntry>();
		string[] terms = filter.ToLowerInvariant().Split(new char[]
		{
			' '
		});
		foreach (string key in list)
		{
			CheatsDebugWindow.CheatCategory cheatCategory = this.m_categories[key];
			bool flag = false;
			List<CheatsDebugWindow.CheatEntry> list3 = new List<CheatsDebugWindow.CheatEntry>();
			if (this.CheatMatchesFilter(cheatCategory, terms))
			{
				flag = true;
				list3.AddRange(cheatCategory.children);
			}
			else
			{
				foreach (CheatsDebugWindow.CheatEntry cheatEntry in cheatCategory.children)
				{
					if (this.CheatMatchesFilter(cheatEntry, terms))
					{
						flag = true;
						list3.Add(cheatEntry);
					}
				}
			}
			if (flag)
			{
				foreach (string key2 in CheatsDebugWindow.CheatCategory.GetLineage(cheatCategory.Path))
				{
					CheatsDebugWindow.CheatCategory item = null;
					if (this.m_categories.TryGetValue(key2, out item) && !list2.Contains(item))
					{
						list2.Add(item);
					}
				}
			}
			foreach (CheatsDebugWindow.CheatEntry item2 in list3)
			{
				list2.Add(item2);
			}
		}
		return list2;
	}

	// Token: 0x06005D52 RID: 23890 RVA: 0x001E5B9C File Offset: 0x001E3D9C
	private bool CheatMatchesFilter(CheatsDebugWindow.CheatEntry cheat, string[] terms)
	{
		if (terms.Count<string>() == 0)
		{
			return true;
		}
		string text = (cheat != null) ? cheat.SearchString : null;
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

	// Token: 0x04004EE5 RID: 20197
	private Vector2 m_GUISize;

	// Token: 0x04004EE6 RID: 20198
	private CheatsDebugWindow.CheatCommand m_currentlyDisplayedCheat;

	// Token: 0x04004EE7 RID: 20199
	private string m_cheatSearchTerm = "";

	// Token: 0x04004EE8 RID: 20200
	private Vector2 m_cheatScrollPosition;

	// Token: 0x04004EE9 RID: 20201
	private Dictionary<string, CheatsDebugWindow.CheatCategory> m_categories;

	// Token: 0x04004EEA RID: 20202
	private GUIStyle m_labelStyle;

	// Token: 0x0200219B RID: 8603
	private class CheatEntry
	{
		// Token: 0x06012411 RID: 74769 RVA: 0x005026A1 File Offset: 0x005008A1
		public CheatEntry(string title)
		{
			this.Title = title;
		}

		// Token: 0x170028C2 RID: 10434
		// (get) Token: 0x06012412 RID: 74770 RVA: 0x005026B0 File Offset: 0x005008B0
		public virtual string SearchString
		{
			get
			{
				return this.Title.ToLowerInvariant();
			}
		}

		// Token: 0x170028C3 RID: 10435
		// (get) Token: 0x06012413 RID: 74771 RVA: 0x005026BD File Offset: 0x005008BD
		// (set) Token: 0x06012414 RID: 74772 RVA: 0x005026C5 File Offset: 0x005008C5
		public string Title { get; protected set; }
	}

	// Token: 0x0200219C RID: 8604
	private class CheatCategory : CheatsDebugWindow.CheatEntry
	{
		// Token: 0x06012415 RID: 74773 RVA: 0x005026D0 File Offset: 0x005008D0
		public CheatCategory(string path) : base("")
		{
			this.Path = path;
			int num = path.LastIndexOf(':');
			base.Title = ((num > 0) ? path.Substring(num + 1) : path);
		}

		// Token: 0x170028C4 RID: 10436
		// (get) Token: 0x06012416 RID: 74774 RVA: 0x00502719 File Offset: 0x00500919
		public override string SearchString
		{
			get
			{
				return this.Path.ToLowerInvariant();
			}
		}

		// Token: 0x170028C5 RID: 10437
		// (get) Token: 0x06012417 RID: 74775 RVA: 0x00502726 File Offset: 0x00500926
		// (set) Token: 0x06012418 RID: 74776 RVA: 0x0050272E File Offset: 0x0050092E
		public string Path { get; protected set; }

		// Token: 0x170028C6 RID: 10438
		// (get) Token: 0x06012419 RID: 74777 RVA: 0x00502738 File Offset: 0x00500938
		public int Depth
		{
			get
			{
				int num = 0;
				int num2 = this.Path.IndexOf(':');
				while (num2 > 0 && num2 < this.Path.Length)
				{
					num++;
					num2 = this.Path.IndexOf(':', num2 + 1);
				}
				return num;
			}
		}

		// Token: 0x0601241A RID: 74778 RVA: 0x00502780 File Offset: 0x00500980
		public static List<string> GetLineage(string fullPath)
		{
			List<string> list = new List<string>();
			for (int i = fullPath.IndexOf(':'); i > 0; i = fullPath.IndexOf(':', i + 1))
			{
				list.Add(fullPath.Substring(0, i));
			}
			list.Add(fullPath);
			return list;
		}

		// Token: 0x0400E0DF RID: 57567
		public List<CheatsDebugWindow.CheatEntry> children = new List<CheatsDebugWindow.CheatEntry>();
	}

	// Token: 0x0200219D RID: 8605
	private class CheatCommand : CheatsDebugWindow.CheatEntry
	{
		// Token: 0x0601241B RID: 74779 RVA: 0x005027C4 File Offset: 0x005009C4
		public CheatCommand(string name) : base(name)
		{
			CheatMgr cheatMgr = CheatMgr.Get();
			if (cheatMgr != null)
			{
				cheatMgr.cheatArgs.TryGetValue(name, out this.args);
				cheatMgr.cheatDesc.TryGetValue(name, out this.description);
				cheatMgr.cheatExamples.TryGetValue(name, out this.example);
			}
		}

		// Token: 0x170028C7 RID: 10439
		// (get) Token: 0x0601241C RID: 74780 RVA: 0x0050283B File Offset: 0x00500A3B
		public override string SearchString
		{
			get
			{
				return (base.Title + " " + this.description).ToLowerInvariant();
			}
		}

		// Token: 0x0400E0E1 RID: 57569
		public string example = "";

		// Token: 0x0400E0E2 RID: 57570
		public string description = "";

		// Token: 0x0400E0E3 RID: 57571
		public string args = "";

		// Token: 0x0400E0E4 RID: 57572
		public CheatsDebugWindow.CheatCategory parent;
	}

	// Token: 0x0200219E RID: 8606
	private class CheatOption : CheatsDebugWindow.CheatEntry
	{
		// Token: 0x0601241D RID: 74781 RVA: 0x00502858 File Offset: 0x00500A58
		public CheatOption(string title, Option option) : base(title)
		{
			this.option = option;
		}

		// Token: 0x0400E0E5 RID: 57573
		public Option option;

		// Token: 0x0400E0E6 RID: 57574
		public CheatsDebugWindow.CheatCategory parent;
	}
}
