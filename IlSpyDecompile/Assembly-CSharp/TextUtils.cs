using System;
using System.Collections.Generic;
using System.Text;

public static class TextUtils
{
	private const int DEFAULT_STRING_BUILDER_CAPACITY_FUDGE = 10;

	public static string DecodeWhitespaces(string text)
	{
		text = text.Replace("\\n", "\n");
		text = text.Replace("\\t", "\t");
		return text;
	}

	public static string EncodeWhitespaces(string text)
	{
		text = text.Replace("\n", "\\n");
		text = text.Replace("\t", "\\t");
		return text;
	}

	public static string ComposeLineItemString(List<string> lines)
	{
		if (lines.Count == 0)
		{
			return "";
		}
		StringBuilder stringBuilder = new StringBuilder();
		foreach (string line in lines)
		{
			stringBuilder.AppendLine(line);
		}
		stringBuilder.Remove(stringBuilder.Length - 1, 1);
		return stringBuilder.ToString();
	}

	public static int CountCharInString(string s, char c)
	{
		int num = 0;
		for (int i = 0; i < s.Length; i++)
		{
			if (s[i] == c)
			{
				num++;
			}
		}
		return num;
	}

	public static string Slice(this string str, int start, int end)
	{
		int length = str.Length;
		if (start < 0)
		{
			start = length + start;
		}
		if (end < 0)
		{
			end = length + end;
		}
		int num = end - start;
		if (num <= 0)
		{
			return string.Empty;
		}
		int num2 = length - start;
		if (num > num2)
		{
			num = num2;
		}
		return str.Substring(start, num);
	}

	public static string Slice(this string str, int start)
	{
		return str.Slice(start, str.Length);
	}

	public static string Slice<T>(this string str)
	{
		return str.Slice(0, str.Length);
	}

	public static string TransformCardText(Entity entity, string text)
	{
		int damageBonus = entity.GetDamageBonus();
		int damageBonusDouble = entity.GetDamageBonusDouble();
		int healingDouble = entity.GetHealingDouble();
		return TransformCardText(damageBonus, damageBonusDouble, healingDouble, text);
	}

	public static string TransformCardText(string text)
	{
		return TransformCardText(0, 0, 0, text);
	}

	public static string TransformCardText(int damageBonus, int damageBonusDouble, int healingDouble, string powersText)
	{
		return GameStrings.ParseLanguageRules(TransformCardTextImpl(damageBonus, damageBonusDouble, healingDouble, powersText));
	}

	public static string ToHexString(this byte[] bytes)
	{
		char[] array = new char[bytes.Length * 2];
		for (int i = 0; i < bytes.Length; i++)
		{
			int num = bytes[i] >> 4;
			array[i * 2] = (char)(55 + num + ((num - 10 >> 31) & -7));
			num = bytes[i] & 0xF;
			array[i * 2 + 1] = (char)(55 + num + ((num - 10 >> 31) & -7));
		}
		return new string(array);
	}

	public static string ToHexString(string str)
	{
		return Encoding.UTF8.GetBytes(str).ToHexString();
	}

	public static string FromHexString(string str)
	{
		if (str.Length % 2 == 1)
		{
			throw new Exception("Hex string must have an even number of digits");
		}
		byte[] array = new byte[str.Length >> 1];
		for (int i = 0; i < str.Length >> 1; i++)
		{
			array[i] = (byte)((GetHexValue(str[i << 1]) << 4) + GetHexValue(str[(i << 1) + 1]));
		}
		return Encoding.UTF8.GetString(array);
	}

	private static int GetHexValue(char hex)
	{
		return hex - ((hex < ':') ? 48 : 55);
	}

	public static string HexDumpFromBytes(byte[] bytes, string separator = "\n")
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < bytes.Length; i++)
		{
			stringBuilder.Append($"{bytes[i]:X2}");
			if ((i + 1) % 4 == 0 && i < bytes.Length)
			{
				stringBuilder.Append(separator);
			}
		}
		return stringBuilder.ToString();
	}

	public static bool HasBonusDamage(string powersText)
	{
		return HasBonusToken(powersText, '$');
	}

	public static bool HasBonusHealing(string powersText)
	{
		return HasBonusToken(powersText, '#');
	}

	private static bool HasBonusToken(string powersText, char token)
	{
		if (powersText == null)
		{
			return false;
		}
		for (int i = 0; i < powersText.Length; i++)
		{
			if (powersText[i] != token)
			{
				continue;
			}
			int j;
			for (j = ++i; j < powersText.Length; j++)
			{
				char num = powersText[j];
				bool flag = char.IsDigit(num);
				bool flag2 = num == '@';
				if (!flag && !flag2)
				{
					break;
				}
			}
			if (j != i)
			{
				return true;
			}
		}
		return false;
	}

	private static string TransformCardTextImpl(int damageBonus, int damageBonusDouble, int healingDouble, string powersText)
	{
		if (powersText == null)
		{
			return string.Empty;
		}
		if (powersText == string.Empty)
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = new StringBuilder();
		bool flag = damageBonus != 0 || damageBonusDouble > 0;
		bool flag2 = healingDouble > 0;
		for (int i = 0; i < powersText.Length; i++)
		{
			char c = powersText[i];
			if (c != '$' && c != '#')
			{
				stringBuilder.Append(c);
				continue;
			}
			int j;
			for (j = ++i; j < powersText.Length; j++)
			{
				char c2 = powersText[j];
				if (c2 < '0' || c2 > '9')
				{
					break;
				}
			}
			if (j == i)
			{
				continue;
			}
			int num = Convert.ToInt32(powersText.Substring(i, j - i));
			switch (c)
			{
			case '$':
			{
				num += damageBonus;
				for (int l = 0; l < damageBonusDouble; l++)
				{
					num *= 2;
				}
				if (num < 0)
				{
					num = 0;
				}
				break;
			}
			case '#':
			{
				for (int k = 0; k < healingDouble; k++)
				{
					num *= 2;
				}
				break;
			}
			}
			if ((flag && c == '$') || (flag2 && c == '#'))
			{
				stringBuilder.Append('*');
				stringBuilder.Append(num);
				stringBuilder.Append('*');
			}
			else
			{
				stringBuilder.Append(num);
			}
			i = j - 1;
		}
		return stringBuilder.ToString();
	}
}
