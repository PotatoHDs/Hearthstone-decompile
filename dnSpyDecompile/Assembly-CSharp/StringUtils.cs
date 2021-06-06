using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

// Token: 0x020009F4 RID: 2548
public static class StringUtils
{
	// Token: 0x060089F7 RID: 35319 RVA: 0x002C3A7E File Offset: 0x002C1C7E
	public static string StripNonNumbers(string str)
	{
		return Regex.Replace(str, "[^0-9]", string.Empty);
	}

	// Token: 0x060089F8 RID: 35320 RVA: 0x002C3A90 File Offset: 0x002C1C90
	public static string StripNewlines(string str)
	{
		return Regex.Replace(str, "[\\r\\n]", string.Empty);
	}

	// Token: 0x060089F9 RID: 35321 RVA: 0x002C3AA2 File Offset: 0x002C1CA2
	public static string[] SplitLines(string str)
	{
		return str.Split(StringUtils.SPLIT_LINES_CHARS_ARRAY, StringSplitOptions.RemoveEmptyEntries);
	}

	// Token: 0x060089FA RID: 35322 RVA: 0x002C3AB0 File Offset: 0x002C1CB0
	public static bool CompareIgnoreCase(string a, string b)
	{
		return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) == 0;
	}

	// Token: 0x060089FB RID: 35323 RVA: 0x002C3AC0 File Offset: 0x002C1CC0
	public static string ArrayToString(IEnumerable l)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("[");
		bool flag = true;
		foreach (object value in l)
		{
			if (flag)
			{
				flag = false;
			}
			else
			{
				stringBuilder.Append(", ");
			}
			stringBuilder.Append(value);
		}
		stringBuilder.Append("]");
		return stringBuilder.ToString();
	}

	// Token: 0x060089FC RID: 35324 RVA: 0x002C3B4C File Offset: 0x002C1D4C
	public static bool Contains(this string str, string val, StringComparison comparison)
	{
		return str.IndexOf(val, comparison) >= 0;
	}

	// Token: 0x060089FD RID: 35325 RVA: 0x002C3B5C File Offset: 0x002C1D5C
	public static bool Contains(this string s, char c)
	{
		return s.IndexOf(c) >= 0;
	}

	// Token: 0x04007354 RID: 29524
	public const string SPLIT_LINES_CHARS = "\n\r";

	// Token: 0x04007355 RID: 29525
	public static readonly char[] SPLIT_LINES_CHARS_ARRAY = "\n\r".ToCharArray();

	// Token: 0x04007356 RID: 29526
	public const string REGEX_RESERVED_CHARS = "\\*.+?^$()[]{}";

	// Token: 0x04007357 RID: 29527
	public static readonly char[] REGEX_RESERVED_CHARS_ARRAY = "\\*.+?^$()[]{}".ToCharArray();
}
