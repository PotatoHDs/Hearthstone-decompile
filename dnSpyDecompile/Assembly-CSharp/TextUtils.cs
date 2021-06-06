using System;
using System.Collections.Generic;
using System.Text;

// Token: 0x020009F6 RID: 2550
public static class TextUtils
{
	// Token: 0x06008A0A RID: 35338 RVA: 0x002C3CE5 File Offset: 0x002C1EE5
	public static string DecodeWhitespaces(string text)
	{
		text = text.Replace("\\n", "\n");
		text = text.Replace("\\t", "\t");
		return text;
	}

	// Token: 0x06008A0B RID: 35339 RVA: 0x002C3D0C File Offset: 0x002C1F0C
	public static string EncodeWhitespaces(string text)
	{
		text = text.Replace("\n", "\\n");
		text = text.Replace("\t", "\\t");
		return text;
	}

	// Token: 0x06008A0C RID: 35340 RVA: 0x002C3D34 File Offset: 0x002C1F34
	public static string ComposeLineItemString(List<string> lines)
	{
		if (lines.Count == 0)
		{
			return "";
		}
		StringBuilder stringBuilder = new StringBuilder();
		foreach (string value in lines)
		{
			stringBuilder.AppendLine(value);
		}
		stringBuilder.Remove(stringBuilder.Length - 1, 1);
		return stringBuilder.ToString();
	}

	// Token: 0x06008A0D RID: 35341 RVA: 0x002C3DB0 File Offset: 0x002C1FB0
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

	// Token: 0x06008A0E RID: 35342 RVA: 0x002C3DE0 File Offset: 0x002C1FE0
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

	// Token: 0x06008A0F RID: 35343 RVA: 0x002C3E26 File Offset: 0x002C2026
	public static string Slice(this string str, int start)
	{
		return str.Slice(start, str.Length);
	}

	// Token: 0x06008A10 RID: 35344 RVA: 0x002C3E35 File Offset: 0x002C2035
	public static string Slice<T>(this string str)
	{
		return str.Slice(0, str.Length);
	}

	// Token: 0x06008A11 RID: 35345 RVA: 0x002C3E44 File Offset: 0x002C2044
	public static string TransformCardText(Entity entity, string text)
	{
		int damageBonus = entity.GetDamageBonus();
		int damageBonusDouble = entity.GetDamageBonusDouble();
		int healingDouble = entity.GetHealingDouble();
		return TextUtils.TransformCardText(damageBonus, damageBonusDouble, healingDouble, text);
	}

	// Token: 0x06008A12 RID: 35346 RVA: 0x002C3E6D File Offset: 0x002C206D
	public static string TransformCardText(string text)
	{
		return TextUtils.TransformCardText(0, 0, 0, text);
	}

	// Token: 0x06008A13 RID: 35347 RVA: 0x002C3E78 File Offset: 0x002C2078
	public static string TransformCardText(int damageBonus, int damageBonusDouble, int healingDouble, string powersText)
	{
		return GameStrings.ParseLanguageRules(TextUtils.TransformCardTextImpl(damageBonus, damageBonusDouble, healingDouble, powersText));
	}

	// Token: 0x06008A14 RID: 35348 RVA: 0x002C3E88 File Offset: 0x002C2088
	public static string ToHexString(this byte[] bytes)
	{
		char[] array = new char[bytes.Length * 2];
		for (int i = 0; i < bytes.Length; i++)
		{
			int num = bytes[i] >> 4;
			array[i * 2] = (char)(55 + num + (num - 10 >> 31 & -7));
			num = (int)(bytes[i] & 15);
			array[i * 2 + 1] = (char)(55 + num + (num - 10 >> 31 & -7));
		}
		return new string(array);
	}

	// Token: 0x06008A15 RID: 35349 RVA: 0x002C3EED File Offset: 0x002C20ED
	public static string ToHexString(string str)
	{
		return Encoding.UTF8.GetBytes(str).ToHexString();
	}

	// Token: 0x06008A16 RID: 35350 RVA: 0x002C3F00 File Offset: 0x002C2100
	public static string FromHexString(string str)
	{
		if (str.Length % 2 == 1)
		{
			throw new Exception("Hex string must have an even number of digits");
		}
		byte[] array = new byte[str.Length >> 1];
		for (int i = 0; i < str.Length >> 1; i++)
		{
			array[i] = (byte)((TextUtils.GetHexValue(str[i << 1]) << 4) + TextUtils.GetHexValue(str[(i << 1) + 1]));
		}
		return Encoding.UTF8.GetString(array);
	}

	// Token: 0x06008A17 RID: 35351 RVA: 0x002C3F74 File Offset: 0x002C2174
	private static int GetHexValue(char hex)
	{
		return (int)(hex - ((hex < ':') ? '0' : '7'));
	}

	// Token: 0x06008A18 RID: 35352 RVA: 0x002C3F84 File Offset: 0x002C2184
	public static string HexDumpFromBytes(byte[] bytes, string separator = "\n")
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < bytes.Length; i++)
		{
			stringBuilder.Append(string.Format("{0:X2}", bytes[i]));
			if ((i + 1) % 4 == 0 && i < bytes.Length)
			{
				stringBuilder.Append(separator);
			}
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06008A19 RID: 35353 RVA: 0x002C3FD9 File Offset: 0x002C21D9
	public static bool HasBonusDamage(string powersText)
	{
		return TextUtils.HasBonusToken(powersText, '$');
	}

	// Token: 0x06008A1A RID: 35354 RVA: 0x002C3FE3 File Offset: 0x002C21E3
	public static bool HasBonusHealing(string powersText)
	{
		return TextUtils.HasBonusToken(powersText, '#');
	}

	// Token: 0x06008A1B RID: 35355 RVA: 0x002C3FF0 File Offset: 0x002C21F0
	private static bool HasBonusToken(string powersText, char token)
	{
		if (powersText == null)
		{
			return false;
		}
		for (int i = 0; i < powersText.Length; i++)
		{
			if (powersText[i] == token)
			{
				int j;
				i = (j = i + 1);
				while (j < powersText.Length)
				{
					char c = powersText[j];
					bool flag = char.IsDigit(c);
					bool flag2 = c == '@';
					if (!flag && !flag2)
					{
						break;
					}
					j++;
				}
				if (j != i)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06008A1C RID: 35356 RVA: 0x002C4054 File Offset: 0x002C2254
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
			}
			else
			{
				int j;
				i = (j = i + 1);
				while (j < powersText.Length)
				{
					char c2 = powersText[j];
					if (c2 < '0' || c2 > '9')
					{
						break;
					}
					j++;
				}
				if (j != i)
				{
					int num = Convert.ToInt32(powersText.Substring(i, j - i));
					if (c == '$')
					{
						num += damageBonus;
						for (int k = 0; k < damageBonusDouble; k++)
						{
							num *= 2;
						}
						if (num < 0)
						{
							num = 0;
						}
					}
					else if (c == '#')
					{
						for (int l = 0; l < healingDouble; l++)
						{
							num *= 2;
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
			}
		}
		return stringBuilder.ToString();
	}

	// Token: 0x0400735C RID: 29532
	private const int DEFAULT_STRING_BUILDER_CAPACITY_FUDGE = 10;
}
