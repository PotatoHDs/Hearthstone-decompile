using System;
using System.ComponentModel;

// Token: 0x020009B7 RID: 2487
public class EnumUtils
{
	// Token: 0x06008721 RID: 34593 RVA: 0x002B9FCC File Offset: 0x002B81CC
	public static string GetString<T>(T enumVal)
	{
		string text = enumVal.ToString();
		DescriptionAttribute[] array = (DescriptionAttribute[])enumVal.GetType().GetField(text).GetCustomAttributes(typeof(DescriptionAttribute), false);
		if (array.Length != 0)
		{
			return array[0].Description;
		}
		return text;
	}

	// Token: 0x06008722 RID: 34594 RVA: 0x002BA020 File Offset: 0x002B8220
	public static bool TryGetEnum<T>(string str, StringComparison comparisonType, out T result)
	{
		Type typeFromHandle = typeof(T);
		Map<string, object> map;
		EnumUtils.s_enumCache.TryGetValue(typeFromHandle, out map);
		object obj;
		if (map != null && map.TryGetValue(str, out obj))
		{
			result = (T)((object)obj);
			return true;
		}
		foreach (string text in Enum.GetNames(typeFromHandle))
		{
			T t = (T)((object)Enum.Parse(typeFromHandle, text));
			bool flag = false;
			if (text.Equals(str, comparisonType))
			{
				flag = true;
				result = t;
			}
			else
			{
				DescriptionAttribute[] array = (DescriptionAttribute[])t.GetType().GetField(text).GetCustomAttributes(typeof(DescriptionAttribute), false);
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j].Description.Equals(str, comparisonType))
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				if (map == null)
				{
					map = new Map<string, object>();
					EnumUtils.s_enumCache.Add(typeFromHandle, map);
				}
				if (!map.ContainsKey(str))
				{
					map.Add(str, t);
				}
				result = t;
				return true;
			}
		}
		result = default(T);
		return false;
	}

	// Token: 0x06008723 RID: 34595 RVA: 0x002BA149 File Offset: 0x002B8349
	public static T GetEnum<T>(string str)
	{
		return EnumUtils.GetEnum<T>(str, StringComparison.Ordinal);
	}

	// Token: 0x06008724 RID: 34596 RVA: 0x002BA154 File Offset: 0x002B8354
	public static T GetEnum<T>(string str, StringComparison comparisonType)
	{
		T result;
		if (EnumUtils.TryGetEnum<T>(str, comparisonType, out result))
		{
			return result;
		}
		throw new ArgumentException(string.Format("EnumUtils.GetEnum() - \"{0}\" has no matching value in enum {1}", str, typeof(T)));
	}

	// Token: 0x06008725 RID: 34597 RVA: 0x002BA188 File Offset: 0x002B8388
	public static bool TryGetEnum<T>(string str, out T outVal)
	{
		return EnumUtils.TryGetEnum<T>(str, StringComparison.Ordinal, out outVal);
	}

	// Token: 0x06008726 RID: 34598 RVA: 0x002BA192 File Offset: 0x002B8392
	public static T Parse<T>(string str)
	{
		return (T)((object)Enum.Parse(typeof(T), str));
	}

	// Token: 0x06008727 RID: 34599 RVA: 0x002BA1AC File Offset: 0x002B83AC
	public static T SafeParse<T>(string str, T defaultValue = default(T), bool ignoreCase = false)
	{
		T result;
		try
		{
			T t = (T)((object)Enum.Parse(typeof(T), str, ignoreCase));
			if (!Enum.IsDefined(typeof(T), t))
			{
				result = defaultValue;
			}
			else
			{
				result = t;
			}
		}
		catch (Exception)
		{
			result = defaultValue;
		}
		return result;
	}

	// Token: 0x06008728 RID: 34600 RVA: 0x002BA208 File Offset: 0x002B8408
	public static bool TryCast<T>(object inVal, out T outVal)
	{
		outVal = default(T);
		bool result;
		try
		{
			outVal = (T)((object)inVal);
			result = true;
		}
		catch (Exception)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06008729 RID: 34601 RVA: 0x002BA244 File Offset: 0x002B8444
	public static int Length<T>()
	{
		return Enum.GetValues(typeof(T)).Length;
	}

	// Token: 0x0400723C RID: 29244
	private static Map<Type, Map<string, object>> s_enumCache = new Map<Type, Map<string, object>>();
}
