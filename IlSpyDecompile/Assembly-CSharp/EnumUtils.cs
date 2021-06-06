using System;
using System.ComponentModel;

public class EnumUtils
{
	private static Map<Type, Map<string, object>> s_enumCache = new Map<Type, Map<string, object>>();

	public static string GetString<T>(T enumVal)
	{
		string text = enumVal.ToString();
		DescriptionAttribute[] array = (DescriptionAttribute[])enumVal.GetType().GetField(text).GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
		if (array.Length != 0)
		{
			return array[0].Description;
		}
		return text;
	}

	public static bool TryGetEnum<T>(string str, StringComparison comparisonType, out T result)
	{
		Type typeFromHandle = typeof(T);
		s_enumCache.TryGetValue(typeFromHandle, out var value);
		if (value != null && value.TryGetValue(str, out var value2))
		{
			result = (T)value2;
			return true;
		}
		string[] names = Enum.GetNames(typeFromHandle);
		foreach (string text in names)
		{
			T val = (T)Enum.Parse(typeFromHandle, text);
			bool flag = false;
			if (text.Equals(str, comparisonType))
			{
				flag = true;
				result = val;
			}
			else
			{
				DescriptionAttribute[] array = (DescriptionAttribute[])val.GetType().GetField(text).GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
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
				if (value == null)
				{
					value = new Map<string, object>();
					s_enumCache.Add(typeFromHandle, value);
				}
				if (!value.ContainsKey(str))
				{
					value.Add(str, val);
				}
				result = val;
				return true;
			}
		}
		result = default(T);
		return false;
	}

	public static T GetEnum<T>(string str)
	{
		return GetEnum<T>(str, StringComparison.Ordinal);
	}

	public static T GetEnum<T>(string str, StringComparison comparisonType)
	{
		if (TryGetEnum<T>(str, comparisonType, out var result))
		{
			return result;
		}
		throw new ArgumentException($"EnumUtils.GetEnum() - \"{str}\" has no matching value in enum {typeof(T)}");
	}

	public static bool TryGetEnum<T>(string str, out T outVal)
	{
		return TryGetEnum<T>(str, StringComparison.Ordinal, out outVal);
	}

	public static T Parse<T>(string str)
	{
		return (T)Enum.Parse(typeof(T), str);
	}

	public static T SafeParse<T>(string str, T defaultValue = default(T), bool ignoreCase = false)
	{
		try
		{
			T val = (T)Enum.Parse(typeof(T), str, ignoreCase);
			if (!Enum.IsDefined(typeof(T), val))
			{
				return defaultValue;
			}
			return val;
		}
		catch (Exception)
		{
			return defaultValue;
		}
	}

	public static bool TryCast<T>(object inVal, out T outVal)
	{
		outVal = default(T);
		try
		{
			outVal = (T)inVal;
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public static int Length<T>()
	{
		return Enum.GetValues(typeof(T)).Length;
	}
}
