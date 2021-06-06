using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000684 RID: 1668
public class DebuggerGui
{
	// Token: 0x14000039 RID: 57
	// (add) Token: 0x06005D53 RID: 23891 RVA: 0x001E5BEC File Offset: 0x001E3DEC
	// (remove) Token: 0x06005D54 RID: 23892 RVA: 0x001E5C24 File Offset: 0x001E3E24
	public event Action OnChanged;

	// Token: 0x06005D55 RID: 23893 RVA: 0x001E5C59 File Offset: 0x001E3E59
	public DebuggerGui(string title, DebuggerGui.LayoutGui onGui, bool canCollapse = true, bool drawWindow = false)
	{
		this.Title = title;
		this.m_canCollapse = canCollapse;
		this.m_OnGui = onGui;
	}

	// Token: 0x1700058B RID: 1419
	// (get) Token: 0x06005D56 RID: 23894 RVA: 0x001E5C8B File Offset: 0x001E3E8B
	// (set) Token: 0x06005D57 RID: 23895 RVA: 0x001E5C93 File Offset: 0x001E3E93
	public string Title { get; set; }

	// Token: 0x1700058C RID: 1420
	// (get) Token: 0x06005D58 RID: 23896 RVA: 0x001E5C9C File Offset: 0x001E3E9C
	// (set) Token: 0x06005D59 RID: 23897 RVA: 0x001E5CA4 File Offset: 0x001E3EA4
	public bool IsExpanded
	{
		get
		{
			return this.m_isExpanded;
		}
		set
		{
			if (this.m_isExpanded != value)
			{
				this.m_isExpanded = value;
				this.InvokeOnChanged();
			}
		}
	}

	// Token: 0x1700058D RID: 1421
	// (get) Token: 0x06005D5A RID: 23898 RVA: 0x001E5CBC File Offset: 0x001E3EBC
	// (set) Token: 0x06005D5B RID: 23899 RVA: 0x001E5CC4 File Offset: 0x001E3EC4
	public bool IsShown
	{
		get
		{
			return this.m_isShown;
		}
		set
		{
			if (this.m_isShown != value)
			{
				this.m_isShown = value;
				this.InvokeOnChanged();
			}
		}
	}

	// Token: 0x06005D5C RID: 23900 RVA: 0x001E5CDC File Offset: 0x001E3EDC
	public virtual Rect Layout(Rect space)
	{
		if (!this.m_isShown)
		{
			return space;
		}
		space = this.LayoutHeader(space);
		return this.LayoutInternal(space);
	}

	// Token: 0x06005D5D RID: 23901 RVA: 0x001E5CF8 File Offset: 0x001E3EF8
	protected Rect LayoutHeader(Rect space)
	{
		Rect rect = new Rect(space.x, space.y, space.width, 24f);
		if (this.m_canCollapse && GUI.Button(new Rect(rect.xMin, rect.yMin, rect.height, rect.height), this.m_isExpanded ? '▼'.ToString() : '▶'.ToString()))
		{
			this.IsExpanded = !this.m_isExpanded;
		}
		GUI.Label(new Rect(rect.xMin + rect.height + 5f, rect.yMin, rect.width - rect.height * 2f - 5f, rect.height), this.Title);
		space.yMin += rect.height;
		return space;
	}

	// Token: 0x06005D5E RID: 23902 RVA: 0x001E5DED File Offset: 0x001E3FED
	protected Rect LayoutInternal(Rect space)
	{
		if (this.m_isExpanded && this.m_OnGui != null)
		{
			return this.m_OnGui(space);
		}
		return space;
	}

	// Token: 0x06005D5F RID: 23903 RVA: 0x001E5E0D File Offset: 0x001E400D
	protected void InvokeOnChanged()
	{
		if (this.OnChanged != null)
		{
			this.OnChanged();
		}
	}

	// Token: 0x06005D60 RID: 23904 RVA: 0x001E5E22 File Offset: 0x001E4022
	internal virtual string SerializeToString()
	{
		return DebuggerGui.SERIAL_ID + (this.IsShown ? "S" : "H") + (this.IsExpanded ? "E" : "C");
	}

	// Token: 0x06005D61 RID: 23905 RVA: 0x001E5E5C File Offset: 0x001E405C
	internal virtual void DeserializeFromString(string str)
	{
		int num = str.IndexOf(DebuggerGui.SERIAL_ID);
		if (num < 0)
		{
			return;
		}
		num += DebuggerGui.SERIAL_ID.Length;
		this.IsShown = (str.ElementAtOrDefault(num) == 'S');
		this.IsExpanded = (str.ElementAtOrDefault(num + 1) == 'E');
	}

	// Token: 0x06005D62 RID: 23906 RVA: 0x001E5EAC File Offset: 0x001E40AC
	public static void SaveConfig(List<DebuggerGui> guis)
	{
		List<string> list = new List<string>();
		foreach (DebuggerGui debuggerGui in guis)
		{
			string item = debuggerGui.SerializeToString();
			list.Add(item);
		}
		string val = string.Join(";", list.ToArray());
		Options.Get().SetString(Option.HUD_CONFIG, val);
	}

	// Token: 0x06005D63 RID: 23907 RVA: 0x001E5F24 File Offset: 0x001E4124
	public static void LoadConfig(List<DebuggerGui> guis)
	{
		string @string = Options.Get().GetString(Option.HUD_CONFIG);
		if (string.IsNullOrEmpty(@string))
		{
			return;
		}
		List<string> list = new List<string>();
		list.AddRange(@string.Split(new char[]
		{
			';'
		}));
		int i = 0;
		int num = Math.Min(guis.Count, list.Count);
		while (i < num)
		{
			guis[i].DeserializeFromString(list[i]);
			i++;
		}
	}

	// Token: 0x04004EEB RID: 20203
	protected bool m_canCollapse = true;

	// Token: 0x04004EEC RID: 20204
	protected bool m_isShown = true;

	// Token: 0x04004EED RID: 20205
	protected bool m_isExpanded = true;

	// Token: 0x04004EEE RID: 20206
	protected DebuggerGui.LayoutGui m_OnGui;

	// Token: 0x04004EF0 RID: 20208
	public const float HEADER_SIZE = 24f;

	// Token: 0x04004EF1 RID: 20209
	private const char DOWN_ARROW = '▼';

	// Token: 0x04004EF2 RID: 20210
	private const char RIGHT_ARROW = '▶';

	// Token: 0x04004EF3 RID: 20211
	internal static string SERIAL_ID = "[DG]";

	// Token: 0x020021A1 RID: 8609
	// (Invoke) Token: 0x06012424 RID: 74788
	public delegate Rect LayoutGui(Rect space);
}
