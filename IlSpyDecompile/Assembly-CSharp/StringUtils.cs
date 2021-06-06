using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

public static class StringUtils
{
	public const string SPLIT_LINES_CHARS = "\n\r";

	public static readonly char[] SPLIT_LINES_CHARS_ARRAY = "\n\r".ToCharArray();

	public const string REGEX_RESERVED_CHARS = "\\*.+?^$()[]{}";

	public static readonly char[] REGEX_RESERVED_CHARS_ARRAY = "\\*.+?^$()[]{}".ToCharArray();

	public static string StripNonNumbers(string str)
	{
		return Regex.Replace(str, "[^0-9]", string.Empty);
	}

	public static string StripNewlines(string str)
	{
		return Regex.Replace(str, "[\\r\\n]", string.Empty);
	}

	public static string[] SplitLines(string str)
	{
		return str.Split(SPLIT_LINES_CHARS_ARRAY, StringSplitOptions.RemoveEmptyEntries);
	}

	public static bool CompareIgnoreCase(string a, string b)
	{
		return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) == 0;
	}

	public static string ArrayToString(IEnumerable l)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("[");
		bool flag = true;
		foreach (object item in l)
		{
			if (flag)
			{
				flag = false;
			}
			else
			{
				stringBuilder.Append(", ");
			}
			stringBuilder.Append(item);
		}
		stringBuilder.Append("]");
		return stringBuilder.ToString();
	}

	public static bool Contains(this string str, string val, StringComparison comparison)
	{
		return str.IndexOf(val, comparison) >= 0;
	}

	public static bool Contains(this string s, char c)
	{
		return s.IndexOf(c) >= 0;
	}
}
