using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace bgs
{
	// Token: 0x02000252 RID: 594
	public class EnumUtils
	{
		// Token: 0x060024BD RID: 9405 RVA: 0x00081DD8 File Offset: 0x0007FFD8
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

		// Token: 0x060024BE RID: 9406 RVA: 0x00081E2C File Offset: 0x0008002C
		public static bool TryGetEnum<T>(string str, StringComparison comparisonType, out T result)
		{
			Type typeFromHandle = typeof(T);
			Dictionary<string, object> dictionary;
			EnumUtils.s_enumCache.TryGetValue(typeFromHandle, out dictionary);
			object obj;
			if (dictionary != null && dictionary.TryGetValue(str, out obj))
			{
				result = (T)((object)obj);
				return true;
			}
			foreach (object obj2 in Enum.GetValues(typeFromHandle))
			{
				T t = (T)((object)obj2);
				bool flag = false;
				if (EnumUtils.GetString<T>(t).Equals(str, comparisonType))
				{
					flag = true;
					result = t;
				}
				else
				{
					DescriptionAttribute[] array = (DescriptionAttribute[])t.GetType().GetField(t.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
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
					if (dictionary == null)
					{
						dictionary = new Dictionary<string, object>();
						EnumUtils.s_enumCache.Add(typeFromHandle, dictionary);
					}
					if (!dictionary.ContainsKey(str))
					{
						dictionary.Add(str, t);
					}
					result = t;
					return true;
				}
			}
			result = default(T);
			return false;
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x00081F88 File Offset: 0x00080188
		public static T GetEnum<T>(string str)
		{
			return EnumUtils.GetEnum<T>(str, StringComparison.Ordinal);
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x00081F94 File Offset: 0x00080194
		public static T GetEnum<T>(string str, StringComparison comparisonType)
		{
			T result;
			if (EnumUtils.TryGetEnum<T>(str, comparisonType, out result))
			{
				return result;
			}
			throw new ArgumentException(string.Format("EnumUtils.GetEnum() - \"{0}\" has no matching value in enum {1}", str, typeof(T)));
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x00081FC8 File Offset: 0x000801C8
		public static bool TryGetEnum<T>(string str, out T outVal)
		{
			return EnumUtils.TryGetEnum<T>(str, StringComparison.Ordinal, out outVal);
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x00081FD2 File Offset: 0x000801D2
		public static T Parse<T>(string str)
		{
			return (T)((object)Enum.Parse(typeof(T), str));
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x00081FEC File Offset: 0x000801EC
		public static T SafeParse<T>(string str)
		{
			T result;
			try
			{
				result = (T)((object)Enum.Parse(typeof(T), str));
			}
			catch (Exception)
			{
				result = default(T);
			}
			return result;
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x00082030 File Offset: 0x00080230
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

		// Token: 0x060024C5 RID: 9413 RVA: 0x0008206C File Offset: 0x0008026C
		public static int Length<T>()
		{
			return Enum.GetValues(typeof(T)).Length;
		}

		// Token: 0x04000F49 RID: 3913
		private static Dictionary<Type, Dictionary<string, object>> s_enumCache = new Dictionary<Type, Dictionary<string, object>>();
	}
}
