using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DbfLocValue
{
	[SerializeField]
	private List<Locale> m_locales = new List<Locale>();

	[SerializeField]
	private List<string> m_locValues = new List<string>();

	[SerializeField]
	private int m_locId;

	private int m_recordId;

	private string m_recordColumn;

	private bool m_hideDebugInfo = true;

	public DbfLocValue()
	{
	}

	public DbfLocValue(bool hideDebugInfo)
	{
		m_hideDebugInfo = hideDebugInfo;
	}

	public string GetString(bool defaultToLoadOrder = true)
	{
		return GetString(Localization.GetLocale(), defaultToLoadOrder);
	}

	public string GetString(Locale loc, bool defaultToLoadOrder = true)
	{
		if (m_locales.Count > 0)
		{
			int num = m_locales.IndexOf(loc);
			if (num >= 0)
			{
				return m_locValues[num];
			}
			Locale[] loadOrder = Localization.GetLoadOrder();
			for (int i = 0; i < loadOrder.Length; i++)
			{
				num = m_locales.IndexOf(loadOrder[i]);
				if (num >= 0)
				{
					return m_locValues[num];
				}
			}
		}
		if (!m_hideDebugInfo)
		{
			return $"ID={m_recordId} COLUMN={m_recordColumn}";
		}
		return string.Empty;
	}

	public void SetCapacity(int count)
	{
		m_locales.Capacity = count;
		m_locValues.Capacity = count;
	}

	public void SetString(Locale loc, string value)
	{
		int num = m_locales.IndexOf(loc);
		if (num >= 0)
		{
			m_locValues[num] = value;
			return;
		}
		m_locales.Add(loc);
		m_locValues.Add(value);
	}

	public void SetString(string value)
	{
		SetString(Localization.GetLocale(), value);
	}

	public void SetLocId(int locId)
	{
		m_locId = locId;
	}

	public int GetLocId()
	{
		return m_locId;
	}

	public void SetDebugInfo(int recordId, string recordColumn)
	{
		m_recordId = recordId;
		m_recordColumn = recordColumn;
	}

	public static implicit operator string(DbfLocValue v)
	{
		if (v == null)
		{
			return string.Empty;
		}
		return v.GetString();
	}

	public void StripUnusedLocales()
	{
		if (m_locales.Count <= 1)
		{
			return;
		}
		Locale[] loadOrder = Localization.GetLoadOrder();
		List<Locale> list = new List<Locale>();
		List<string> list2 = new List<string>();
		for (int i = 0; i < loadOrder.Length; i++)
		{
			int num = m_locales.IndexOf(loadOrder[i]);
			if (num >= 0)
			{
				list.Add(m_locales[num]);
				list2.Add(m_locValues[num]);
			}
		}
		m_locales = list;
		m_locValues = list2;
	}
}
