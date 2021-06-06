using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007B6 RID: 1974
[Serializable]
public class DbfLocValue
{
	// Token: 0x06006D62 RID: 28002 RVA: 0x002345CC File Offset: 0x002327CC
	public DbfLocValue()
	{
	}

	// Token: 0x06006D63 RID: 28003 RVA: 0x002345F1 File Offset: 0x002327F1
	public DbfLocValue(bool hideDebugInfo)
	{
		this.m_hideDebugInfo = hideDebugInfo;
	}

	// Token: 0x06006D64 RID: 28004 RVA: 0x0023461D File Offset: 0x0023281D
	public string GetString(bool defaultToLoadOrder = true)
	{
		return this.GetString(Localization.GetLocale(), defaultToLoadOrder);
	}

	// Token: 0x06006D65 RID: 28005 RVA: 0x0023462C File Offset: 0x0023282C
	public string GetString(Locale loc, bool defaultToLoadOrder = true)
	{
		if (this.m_locales.Count > 0)
		{
			int num = this.m_locales.IndexOf(loc);
			if (num >= 0)
			{
				return this.m_locValues[num];
			}
			Locale[] loadOrder = Localization.GetLoadOrder(false);
			for (int i = 0; i < loadOrder.Length; i++)
			{
				num = this.m_locales.IndexOf(loadOrder[i]);
				if (num >= 0)
				{
					return this.m_locValues[num];
				}
			}
		}
		if (!this.m_hideDebugInfo)
		{
			return string.Format("ID={0} COLUMN={1}", this.m_recordId, this.m_recordColumn);
		}
		return string.Empty;
	}

	// Token: 0x06006D66 RID: 28006 RVA: 0x002346C3 File Offset: 0x002328C3
	public void SetCapacity(int count)
	{
		this.m_locales.Capacity = count;
		this.m_locValues.Capacity = count;
	}

	// Token: 0x06006D67 RID: 28007 RVA: 0x002346E0 File Offset: 0x002328E0
	public void SetString(Locale loc, string value)
	{
		int num = this.m_locales.IndexOf(loc);
		if (num >= 0)
		{
			this.m_locValues[num] = value;
			return;
		}
		this.m_locales.Add(loc);
		this.m_locValues.Add(value);
	}

	// Token: 0x06006D68 RID: 28008 RVA: 0x00234724 File Offset: 0x00232924
	public void SetString(string value)
	{
		this.SetString(Localization.GetLocale(), value);
	}

	// Token: 0x06006D69 RID: 28009 RVA: 0x00234732 File Offset: 0x00232932
	public void SetLocId(int locId)
	{
		this.m_locId = locId;
	}

	// Token: 0x06006D6A RID: 28010 RVA: 0x0023473B File Offset: 0x0023293B
	public int GetLocId()
	{
		return this.m_locId;
	}

	// Token: 0x06006D6B RID: 28011 RVA: 0x00234743 File Offset: 0x00232943
	public void SetDebugInfo(int recordId, string recordColumn)
	{
		this.m_recordId = recordId;
		this.m_recordColumn = recordColumn;
	}

	// Token: 0x06006D6C RID: 28012 RVA: 0x00234753 File Offset: 0x00232953
	public static implicit operator string(DbfLocValue v)
	{
		if (v == null)
		{
			return string.Empty;
		}
		return v.GetString(true);
	}

	// Token: 0x06006D6D RID: 28013 RVA: 0x00234768 File Offset: 0x00232968
	public void StripUnusedLocales()
	{
		if (this.m_locales.Count > 1)
		{
			Locale[] loadOrder = Localization.GetLoadOrder(false);
			List<Locale> list = new List<Locale>();
			List<string> list2 = new List<string>();
			for (int i = 0; i < loadOrder.Length; i++)
			{
				int num = this.m_locales.IndexOf(loadOrder[i]);
				if (num >= 0)
				{
					list.Add(this.m_locales[num]);
					list2.Add(this.m_locValues[num]);
				}
			}
			this.m_locales = list;
			this.m_locValues = list2;
		}
	}

	// Token: 0x040057F1 RID: 22513
	[SerializeField]
	private List<Locale> m_locales = new List<Locale>();

	// Token: 0x040057F2 RID: 22514
	[SerializeField]
	private List<string> m_locValues = new List<string>();

	// Token: 0x040057F3 RID: 22515
	[SerializeField]
	private int m_locId;

	// Token: 0x040057F4 RID: 22516
	private int m_recordId;

	// Token: 0x040057F5 RID: 22517
	private string m_recordColumn;

	// Token: 0x040057F6 RID: 22518
	private bool m_hideDebugInfo = true;
}
