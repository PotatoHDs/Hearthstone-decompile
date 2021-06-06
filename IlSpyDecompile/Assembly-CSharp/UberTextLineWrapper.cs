using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UberTextLineWrapper
{
	private class CharComparer : IEqualityComparer<char>
	{
		public bool Equals(char x, char y)
		{
			return x == y;
		}

		public int GetHashCode(char obj)
		{
			return obj;
		}
	}

	private static readonly Dictionary<char, int> s_charUtf32Map = new Dictionary<char, int>(256, new CharComparer());

	private static readonly StringBuilder s_wordBuilder = new StringBuilder(128);

	private static readonly List<string> s_wordList = new List<string>(256);

	private const int CharNotFoundIndex = -1;

	private const float CharWidthScaleFactor = 0.1f;

	private const float UnderwearScaleFactor = 0.2f;

	private readonly UberTextRendering m_uberTextRendering;

	private readonly Dictionary<char, float> m_charWidthMap = new Dictionary<char, float>(256, new CharComparer());

	private WordWrapSettings m_settings;

	private CharacterInfo m_characterInfo;

	private Font m_font;

	private int m_fontSize;

	private float m_characterSize;

	private float[] m_charWidths;

	public UberTextLineWrapper(UberTextRendering uberTextRendering)
	{
		m_uberTextRendering = uberTextRendering;
	}

	public string WordWrapString(string text, WordWrapSettings settings, ref int lineCount, ref bool ellipsesWasAdded)
	{
		if (string.IsNullOrEmpty(text))
		{
			return string.Empty;
		}
		Reset();
		m_settings = settings;
		return Wrap(text, ref lineCount, ref ellipsesWasAdded);
	}

	private string Wrap(string text, ref int lineCount, ref bool ellipsesWasAdded)
	{
		float num = m_settings.OriginalWidth;
		float originalWidth = m_settings.OriginalWidth;
		UberTextRendering.TransformBackup textMeshBackup = m_uberTextRendering.BackupTextMeshTransform();
		TextAnchor textAnchor = m_uberTextRendering.GetTextAnchor();
		string[] array = BreakStringIntoWords(text, originalWidth, m_settings.RichText);
		PrepareTextForUnderwear();
		int length = text.Length;
		bool flag = false;
		Bounds underwearLeft = default(Bounds);
		Bounds underwearRight = default(Bounds);
		if (m_settings.UseUnderwear)
		{
			flag = IsUnderwearNeeded(array, length, originalWidth);
			if (flag)
			{
				GetUnderwearBounds(out underwearLeft, out underwearRight);
				m_uberTextRendering.SetTextAnchor(TextAnchor.UpperCenter);
			}
		}
		int num2 = array.Length * 3 + length;
		StringBuilder stringBuilder = new StringBuilder(num2, num2);
		StringBuilder stringBuilder2 = new StringBuilder(length + 3, length + 3);
		UberText.RemoveTagsFromWord(array[array.Length - 1], out var trailingTag, m_settings.RichText);
		string[] array2 = array;
		foreach (string text2 in array2)
		{
			string trailingTag2;
			string value = UberText.RemoveTagsFromWord(text2, out trailingTag2, m_settings.RichText);
			stringBuilder2.Append(value);
			string text3 = stringBuilder2.ToString();
			m_uberTextRendering.SetText(text3);
			float x = m_uberTextRendering.GetTextMeshBounds().size.x;
			if (m_settings.UseUnderwear && flag)
			{
				num = GetFinalContainerWidth(stringBuilder.ToString(), originalWidth, underwearLeft, underwearRight);
			}
			if (x < num)
			{
				stringBuilder.Append(text2);
				continue;
			}
			if (m_settings.Ellipsized)
			{
				m_uberTextRendering.SetText(stringBuilder.ToString() + "\n"[0]);
				if (m_uberTextRendering.GetTextMeshBounds().size.y > m_settings.Height)
				{
					ellipsesWasAdded = true;
					string text4 = " ...";
					if (trailingTag != null)
					{
						text4 += trailingTag;
					}
					stringBuilder.Append(text4);
					break;
				}
			}
			if (stringBuilder.Length > 2 && EndsWith(stringBuilder.ToString(), "[d]"))
			{
				stringBuilder.Append('-');
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append("\n");
			}
			stringBuilder.Append(text2.TrimStart(' '));
			stringBuilder2.Length = 0;
			for (int j = 0; j < lineCount; j++)
			{
				stringBuilder2.Append("\n");
			}
			stringBuilder2.Append(value);
		}
		RestoreTextMeshBackup(textMeshBackup, textAnchor);
		string text5 = ((!m_settings.RichText) ? stringBuilder.ToString() : UberText.RemoveLineBreakTagsHardSpace(stringBuilder.ToString()));
		lineCount = UberText.LineCount(text5);
		return text5;
	}

	private string[] BreakStringIntoWords(string text, float width, bool richText)
	{
		m_charWidths = new float[text.Length];
		s_wordBuilder.Length = 0;
		s_wordList.Clear();
		bool flag = false;
		bool flag2 = false;
		float num = 0f;
		char c = text[0];
		s_wordBuilder.Append(c);
		if (c == '<' && richText)
		{
			flag = true;
		}
		else
		{
			m_charWidths[0] = UpdateCharWidth(c);
			num += m_charWidths[0];
		}
		for (int i = 1; i < text.Length; i++)
		{
			char c2 = text[i];
			if (text[i] == '[' && i + 2 < text.Length)
			{
				if ((text[i + 1] == 'b' || text[i + 1] == 'd') && text[i + 2] == ']')
				{
					s_wordBuilder.Append(text[i]);
					s_wordBuilder.Append(text[i + 1]);
					s_wordBuilder.Append(text[i + 2]);
					s_wordList.Add(s_wordBuilder.ToString());
					s_wordBuilder.Length = 0;
					i += 2;
					num = 0f;
					continue;
				}
				if (text[i + 1] == 'x' && text[i + 2] == ']')
				{
					flag2 = true;
					s_wordBuilder.Append(text[i]);
					s_wordBuilder.Append(text[i + 1]);
					s_wordBuilder.Append(text[i + 2]);
					i += 2;
					continue;
				}
			}
			if (text[i] == '<' && richText)
			{
				flag = true;
				s_wordBuilder.Append(text[i]);
				continue;
			}
			if (text[i] == '>')
			{
				flag = false;
				s_wordBuilder.Append(text[i]);
				continue;
			}
			if (flag)
			{
				s_wordBuilder.Append(text[i]);
				continue;
			}
			m_charWidths[i] = UpdateCharWidth(c2);
			num += m_charWidths[i];
			char lastChar = text[i - 1];
			char wideChar = text[i];
			char nextChar = '\0';
			if (i < text.Length - 1)
			{
				nextChar = text[i + 1];
			}
			if (!flag2 && CanWrapBetween(lastChar, wideChar, nextChar))
			{
				s_wordList.Add(s_wordBuilder.ToString());
				s_wordBuilder.Length = 0;
				s_wordBuilder.Append(c2);
				num = m_charWidths[i];
			}
			else if (num < width)
			{
				s_wordBuilder.Append(c2);
			}
			else if ((!m_settings.ResizeToFit || m_settings.Ellipsized) && m_settings.ForceWrapLargeWords)
			{
				s_wordList.Add(s_wordBuilder.ToString());
				s_wordBuilder.Length = 0;
				s_wordBuilder.Append(c2);
				num = m_charWidths[i];
			}
			else
			{
				s_wordBuilder.Append(c2);
			}
		}
		s_wordList.Add(s_wordBuilder.ToString());
		return s_wordList.ToArray();
	}

	private void Reset()
	{
		m_charWidthMap.Clear();
		m_font = m_uberTextRendering.GetFont();
		m_fontSize = m_uberTextRendering.GetFontSize();
		m_characterSize = m_uberTextRendering.GetCharacterSize();
	}

	private void PrepareTextForUnderwear()
	{
		m_uberTextRendering.SetTextMeshGameObjectParent(null);
		m_uberTextRendering.SetTextMeshGameObjectRotation(Quaternion.identity);
		m_uberTextRendering.SetTextMeshGameObjectPosition(new Vector3(0f, m_settings.Height * 0.25f, 0f));
		m_uberTextRendering.SetTextMeshGameObjectLocalScale(Vector3.one);
	}

	private void RestoreTextMeshBackup(UberTextRendering.TransformBackup textMeshBackup, TextAnchor originalAnchor)
	{
		m_uberTextRendering.SetTextMeshGameObjectParent(textMeshBackup.Parent);
		m_uberTextRendering.SetTextMeshGameObjectRotation(textMeshBackup.Rotation);
		m_uberTextRendering.SetTextMeshGameObjectPosition(textMeshBackup.Position);
		m_uberTextRendering.SetTextMeshGameObjectLocalScale(textMeshBackup.LocalScale);
		m_uberTextRendering.SetTextAnchor(originalAnchor);
	}

	private float UpdateCharWidth(char c)
	{
		if (!m_charWidthMap.TryGetValue(c, out var value))
		{
			m_font.GetCharacterInfo(c, out m_characterInfo, m_fontSize);
			float num = (float)m_characterInfo.advance * m_characterSize * 0.1f;
			m_charWidthMap.Add(c, num);
			return num;
		}
		return value;
	}

	private static int ConvertStringCharToUtf32(char c)
	{
		int value = -1;
		if (!s_charUtf32Map.TryGetValue(c, out value))
		{
			value = char.ConvertToUtf32(c.ToString(), 0);
			s_charUtf32Map.Add(c, value);
		}
		return value;
	}

	private bool CanWrapBetween(char lastChar, char wideChar, char nextChar)
	{
		int num = ConvertStringCharToUtf32(lastChar);
		int num2 = ConvertStringCharToUtf32(wideChar);
		int num3 = 0;
		if (nextChar != 0)
		{
			num3 = ConvertStringCharToUtf32(nextChar);
		}
		bool flag = char.IsWhiteSpace(wideChar);
		bool flag2 = char.IsWhiteSpace(lastChar);
		if ((Localization.GetLocale() == Locale.frFR || Localization.GetLocale() == Locale.deDE) && flag)
		{
			switch (num3)
			{
			case 33:
			case 46:
			case 58:
			case 59:
			case 63:
			case 171:
			case 187:
				return false;
			}
			if (num == 171)
			{
				return false;
			}
		}
		switch (num)
		{
		case 45:
			if (num2 >= 48 && num2 <= 57)
			{
				return false;
			}
			return true;
		case 59:
			return true;
		default:
			if (num2 == 124)
			{
				return true;
			}
			if (flag2)
			{
				return false;
			}
			if (flag)
			{
				return true;
			}
			switch (num)
			{
			case 36:
			case 40:
			case 91:
			case 92:
			case 123:
			case 8216:
			case 8220:
			case 8245:
			case 12296:
			case 12298:
			case 12300:
			case 12302:
			case 12304:
			case 12308:
			case 12317:
			case 65113:
			case 65115:
			case 65117:
			case 65284:
			case 65288:
			case 65339:
			case 65371:
			case 65505:
			case 65509:
			case 65510:
				return false;
			default:
				switch (num2)
				{
				case 33:
				case 37:
				case 41:
				case 44:
				case 46:
				case 58:
				case 59:
				case 63:
				case 93:
				case 125:
				case 176:
				case 183:
				case 8211:
				case 8212:
				case 8217:
				case 8221:
				case 8226:
				case 8230:
				case 8231:
				case 8242:
				case 8243:
				case 8451:
				case 12289:
				case 12290:
				case 12297:
				case 12299:
				case 12301:
				case 12303:
				case 12305:
				case 12309:
				case 12318:
				case 12540:
				case 65072:
				case 65104:
				case 65105:
				case 65106:
				case 65108:
				case 65109:
				case 65110:
				case 65111:
				case 65114:
				case 65116:
				case 65118:
				case 65281:
				case 65285:
				case 65289:
				case 65292:
				case 65294:
				case 65306:
				case 65307:
				case 65311:
				case 65341:
				case 65373:
				case 65392:
				case 65438:
				case 65439:
				case 65504:
					return false;
				default:
					if (num == 12290 || num == 65292)
					{
						return true;
					}
					if (Localization.GetLocale() == Locale.koKR && m_settings.Alignment == UberText.AlignmentOptions.Center)
					{
						return false;
					}
					if ((num2 >= 4352 && num2 <= 4607) || (num2 >= 12288 && num2 <= 55215) || (num2 >= 63744 && num2 <= 64255) || (num2 >= 65280 && num2 <= 65439) || (num2 >= 65440 && num2 <= 65500))
					{
						return true;
					}
					return false;
				}
			}
		}
	}

	private void GetUnderwearBounds(out Bounds underwearLeft, out Bounds underwearRight)
	{
		underwearLeft = default(Bounds);
		underwearRight = default(Bounds);
		if (m_settings.UseUnderwear)
		{
			Vector3 size = new Vector3(m_settings.Width * m_settings.UnderwearWidth * 0.5f, m_settings.Height * m_settings.UnderwearHeight * 0.5f, 1f);
			float num = 0f;
			num = ((!m_settings.UnderwearFlip) ? (num - m_settings.Height * 0.5f + m_settings.Height * m_settings.UnderwearHeight * 0.5f) : (num + m_settings.Height * 0.5f - m_settings.Height * m_settings.UnderwearHeight * 0.5f));
			Vector3 zero = Vector3.zero;
			zero.x = zero.x + m_settings.Width * 0.5f - m_settings.Width * 0.5f * m_settings.UnderwearWidth * 0.5f;
			zero.y = num;
			underwearLeft.center = zero;
			underwearLeft.size = size;
			Vector3 zero2 = Vector3.zero;
			zero2.x = zero2.x - m_settings.Width * 0.5f + m_settings.Width * 0.5f * m_settings.UnderwearWidth * 0.5f;
			zero2.y = num;
			underwearRight.center = zero2;
			underwearRight.size = size;
		}
	}

	private static bool EndsWith(string toSearch, string end)
	{
		if (string.IsNullOrEmpty(toSearch) || string.IsNullOrEmpty(end))
		{
			return false;
		}
		if (toSearch == end)
		{
			return true;
		}
		if (end.Length > toSearch.Length)
		{
			return false;
		}
		int num = toSearch.Length - 1;
		int num2 = end.Length - 1;
		while (num2 >= 0)
		{
			if (toSearch[num] != end[num2])
			{
				return false;
			}
			num2--;
			num--;
		}
		return true;
	}

	private static bool ContainsChar(string text, char target)
	{
		for (int i = 0; i < text.Length; i++)
		{
			if (text[i] == target)
			{
				return true;
			}
		}
		return false;
	}

	private float GetFinalContainerWidth(string text, float orgWidth, Bounds underwearLeft, Bounds underwearRight)
	{
		m_uberTextRendering.SetText(text);
		Bounds textMeshBounds = m_uberTextRendering.GetTextMeshBounds();
		float result = orgWidth;
		float y = textMeshBounds.size.y;
		if (m_settings.UnderwearFlip)
		{
			result = ((!(y - (m_settings.Height - y) * 0.2f < m_settings.UnderwearHeightLocaleAdjustment)) ? orgWidth : m_settings.UnderwearWidthLocaleAdjustment);
		}
		else if (textMeshBounds.Intersects(underwearLeft) || textMeshBounds.Intersects(underwearRight))
		{
			result = m_settings.UnderwearWidthLocaleAdjustment;
		}
		return result;
	}

	private bool IsUnderwearNeeded(string[] words, int textLength, float orgWidth)
	{
		bool result = m_settings.UseUnderwear && !m_settings.UnderwearFlip;
		if (m_settings.UseUnderwear && m_settings.UnderwearFlip)
		{
			StringBuilder stringBuilder = new StringBuilder(textLength, textLength);
			StringBuilder stringBuilder2 = new StringBuilder(textLength, textLength);
			foreach (string text in words)
			{
				string trailingTag;
				string value = UberText.RemoveTagsFromWord(text, out trailingTag, m_settings.RichText);
				stringBuilder2.Append(value);
				string text2 = stringBuilder2.ToString();
				m_uberTextRendering.SetText(text2);
				if (m_uberTextRendering.GetTextMeshBounds().size.x < orgWidth)
				{
					stringBuilder.Append(text);
					continue;
				}
				result = true;
				break;
			}
			if (ContainsChar(stringBuilder.ToString(), "\n"[0]))
			{
				result = true;
			}
		}
		return result;
	}
}
