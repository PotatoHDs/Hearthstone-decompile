using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace bgs
{
	public class EnumUtils
	{
		private static Dictionary<Type, Dictionary<string, object>> s_enumCache = new Dictionary<Type, Dictionary<string, object>>();

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
			foreach (T value3 in Enum.GetValues(typeFromHandle))
			{
				bool flag = false;
				if (GetString(value3).Equals(str, comparisonType))
				{
					flag = true;
					result = value3;
				}
				else
				{
					DescriptionAttribute[] array = (DescriptionAttribute[])value3.GetType().GetField(value3.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].Description.Equals(str, comparisonType))
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
						value = new Dictionary<string, object>();
						s_enumCache.Add(typeFromHandle, value);
					}
					if (!value.ContainsKey(str))
					{
						value.Add(str, value3);
					}
					result = value3;
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

		public static T SafeParse<T>(string str)
		{
			try
			{
				return (T)Enum.Parse(typeof(T), str);
			}
			catch (Exception)
			{
				return default(T);
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
}
