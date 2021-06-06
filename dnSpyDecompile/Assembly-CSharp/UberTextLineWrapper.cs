using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000AAA RID: 2730
public class UberTextLineWrapper
{
	// Token: 0x06009219 RID: 37401 RVA: 0x002F6E78 File Offset: 0x002F5078
	public UberTextLineWrapper(UberTextRendering uberTextRendering)
	{
		this.m_uberTextRendering = uberTextRendering;
	}

	// Token: 0x0600921A RID: 37402 RVA: 0x002F6E9C File Offset: 0x002F509C
	public string WordWrapString(string text, WordWrapSettings settings, ref int lineCount, ref bool ellipsesWasAdded)
	{
		if (string.IsNullOrEmpty(text))
		{
			return string.Empty;
		}
		this.Reset();
		this.m_settings = settings;
		return this.Wrap(text, ref lineCount, ref ellipsesWasAdded);
	}

	// Token: 0x0600921B RID: 37403 RVA: 0x002F6EC4 File Offset: 0x002F50C4
	private string Wrap(string text, ref int lineCount, ref bool ellipsesWasAdded)
	{
		float num = this.m_settings.OriginalWidth;
		float originalWidth = this.m_settings.OriginalWidth;
		UberTextRendering.TransformBackup textMeshBackup = this.m_uberTextRendering.BackupTextMeshTransform();
		TextAnchor textAnchor = this.m_uberTextRendering.GetTextAnchor();
		string[] array = this.BreakStringIntoWords(text, originalWidth, this.m_settings.RichText);
		this.PrepareTextForUnderwear();
		int length = text.Length;
		bool flag = false;
		Bounds underwearLeft = default(Bounds);
		Bounds underwearRight = default(Bounds);
		if (this.m_settings.UseUnderwear)
		{
			flag = this.IsUnderwearNeeded(array, length, originalWidth);
			if (flag)
			{
				this.GetUnderwearBounds(out underwearLeft, out underwearRight);
				this.m_uberTextRendering.SetTextAnchor(TextAnchor.UpperCenter);
			}
		}
		int num2 = array.Length * 3 + length;
		StringBuilder stringBuilder = new StringBuilder(num2, num2);
		StringBuilder stringBuilder2 = new StringBuilder(length + 3, length + 3);
		string text2;
		UberText.RemoveTagsFromWord(array[array.Length - 1], out text2, this.m_settings.RichText);
		foreach (string text3 in array)
		{
			string text4;
			string value = UberText.RemoveTagsFromWord(text3, out text4, this.m_settings.RichText);
			stringBuilder2.Append(value);
			string text5 = stringBuilder2.ToString();
			this.m_uberTextRendering.SetText(text5);
			float x = this.m_uberTextRendering.GetTextMeshBounds().size.x;
			if (this.m_settings.UseUnderwear && flag)
			{
				num = this.GetFinalContainerWidth(stringBuilder.ToString(), originalWidth, underwearLeft, underwearRight);
			}
			if (x < num)
			{
				stringBuilder.Append(text3);
			}
			else
			{
				if (this.m_settings.Ellipsized)
				{
					this.m_uberTextRendering.SetText(stringBuilder.ToString() + "\n"[0].ToString());
					if (this.m_uberTextRendering.GetTextMeshBounds().size.y > this.m_settings.Height)
					{
						ellipsesWasAdded = true;
						string text6 = " ...";
						if (text2 != null)
						{
							text6 += text2;
						}
						stringBuilder.Append(text6);
						break;
					}
				}
				if (stringBuilder.Length > 2 && UberTextLineWrapper.EndsWith(stringBuilder.ToString(), "[d]"))
				{
					stringBuilder.Append('-');
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append("\n");
				}
				stringBuilder.Append(text3.TrimStart(new char[]
				{
					' '
				}));
				stringBuilder2.Length = 0;
				for (int j = 0; j < lineCount; j++)
				{
					stringBuilder2.Append("\n");
				}
				stringBuilder2.Append(value);
			}
		}
		this.RestoreTextMeshBackup(textMeshBackup, textAnchor);
		string text7;
		if (this.m_settings.RichText)
		{
			text7 = UberText.RemoveLineBreakTagsHardSpace(stringBuilder.ToString());
		}
		else
		{
			text7 = stringBuilder.ToString();
		}
		lineCount = UberText.LineCount(text7);
		return text7;
	}

	// Token: 0x0600921C RID: 37404 RVA: 0x002F7198 File Offset: 0x002F5398
	private string[] BreakStringIntoWords(string text, float width, bool richText)
	{
		this.m_charWidths = new float[text.Length];
		UberTextLineWrapper.s_wordBuilder.Length = 0;
		UberTextLineWrapper.s_wordList.Clear();
		bool flag = false;
		bool flag2 = false;
		float num = 0f;
		char c = text[0];
		UberTextLineWrapper.s_wordBuilder.Append(c);
		if (c == '<' && richText)
		{
			flag = true;
		}
		else
		{
			this.m_charWidths[0] = this.UpdateCharWidth(c);
			num += this.m_charWidths[0];
		}
		int i = 1;
		while (i < text.Length)
		{
			char c2 = text[i];
			if (text[i] != '[' || i + 2 >= text.Length)
			{
				goto IL_198;
			}
			if ((text[i + 1] == 'b' || text[i + 1] == 'd') && text[i + 2] == ']')
			{
				UberTextLineWrapper.s_wordBuilder.Append(text[i]);
				UberTextLineWrapper.s_wordBuilder.Append(text[i + 1]);
				UberTextLineWrapper.s_wordBuilder.Append(text[i + 2]);
				UberTextLineWrapper.s_wordList.Add(UberTextLineWrapper.s_wordBuilder.ToString());
				UberTextLineWrapper.s_wordBuilder.Length = 0;
				i += 2;
				num = 0f;
			}
			else
			{
				if (text[i + 1] != 'x' || text[i + 2] != ']')
				{
					goto IL_198;
				}
				flag2 = true;
				UberTextLineWrapper.s_wordBuilder.Append(text[i]);
				UberTextLineWrapper.s_wordBuilder.Append(text[i + 1]);
				UberTextLineWrapper.s_wordBuilder.Append(text[i + 2]);
				i += 2;
			}
			IL_319:
			i++;
			continue;
			IL_198:
			if (text[i] == '<' && richText)
			{
				flag = true;
				UberTextLineWrapper.s_wordBuilder.Append(text[i]);
				goto IL_319;
			}
			if (text[i] == '>')
			{
				flag = false;
				UberTextLineWrapper.s_wordBuilder.Append(text[i]);
				goto IL_319;
			}
			if (flag)
			{
				UberTextLineWrapper.s_wordBuilder.Append(text[i]);
				goto IL_319;
			}
			this.m_charWidths[i] = this.UpdateCharWidth(c2);
			num += this.m_charWidths[i];
			char lastChar = text[i - 1];
			char wideChar = text[i];
			char nextChar = '\0';
			if (i < text.Length - 1)
			{
				nextChar = text[i + 1];
			}
			if (!flag2 && this.CanWrapBetween(lastChar, wideChar, nextChar))
			{
				UberTextLineWrapper.s_wordList.Add(UberTextLineWrapper.s_wordBuilder.ToString());
				UberTextLineWrapper.s_wordBuilder.Length = 0;
				UberTextLineWrapper.s_wordBuilder.Append(c2);
				num = this.m_charWidths[i];
				goto IL_319;
			}
			if (num < width)
			{
				UberTextLineWrapper.s_wordBuilder.Append(c2);
				goto IL_319;
			}
			if ((!this.m_settings.ResizeToFit || this.m_settings.Ellipsized) && this.m_settings.ForceWrapLargeWords)
			{
				UberTextLineWrapper.s_wordList.Add(UberTextLineWrapper.s_wordBuilder.ToString());
				UberTextLineWrapper.s_wordBuilder.Length = 0;
				UberTextLineWrapper.s_wordBuilder.Append(c2);
				num = this.m_charWidths[i];
				goto IL_319;
			}
			UberTextLineWrapper.s_wordBuilder.Append(c2);
			goto IL_319;
		}
		UberTextLineWrapper.s_wordList.Add(UberTextLineWrapper.s_wordBuilder.ToString());
		return UberTextLineWrapper.s_wordList.ToArray();
	}

	// Token: 0x0600921D RID: 37405 RVA: 0x002F74EF File Offset: 0x002F56EF
	private void Reset()
	{
		this.m_charWidthMap.Clear();
		this.m_font = this.m_uberTextRendering.GetFont();
		this.m_fontSize = this.m_uberTextRendering.GetFontSize();
		this.m_characterSize = this.m_uberTextRendering.GetCharacterSize();
	}

	// Token: 0x0600921E RID: 37406 RVA: 0x002F7530 File Offset: 0x002F5730
	private void PrepareTextForUnderwear()
	{
		this.m_uberTextRendering.SetTextMeshGameObjectParent(null);
		this.m_uberTextRendering.SetTextMeshGameObjectRotation(Quaternion.identity);
		this.m_uberTextRendering.SetTextMeshGameObjectPosition(new Vector3(0f, this.m_settings.Height * 0.25f, 0f));
		this.m_uberTextRendering.SetTextMeshGameObjectLocalScale(Vector3.one);
	}

	// Token: 0x0600921F RID: 37407 RVA: 0x002F7594 File Offset: 0x002F5794
	private void RestoreTextMeshBackup(UberTextRendering.TransformBackup textMeshBackup, TextAnchor originalAnchor)
	{
		this.m_uberTextRendering.SetTextMeshGameObjectParent(textMeshBackup.Parent);
		this.m_uberTextRendering.SetTextMeshGameObjectRotation(textMeshBackup.Rotation);
		this.m_uberTextRendering.SetTextMeshGameObjectPosition(textMeshBackup.Position);
		this.m_uberTextRendering.SetTextMeshGameObjectLocalScale(textMeshBackup.LocalScale);
		this.m_uberTextRendering.SetTextAnchor(originalAnchor);
	}

	// Token: 0x06009220 RID: 37408 RVA: 0x002F75F4 File Offset: 0x002F57F4
	private float UpdateCharWidth(char c)
	{
		float result;
		if (!this.m_charWidthMap.TryGetValue(c, out result))
		{
			this.m_font.GetCharacterInfo(c, out this.m_characterInfo, this.m_fontSize);
			float num = (float)this.m_characterInfo.advance * this.m_characterSize * 0.1f;
			this.m_charWidthMap.Add(c, num);
			return num;
		}
		return result;
	}

	// Token: 0x06009221 RID: 37409 RVA: 0x002F7654 File Offset: 0x002F5854
	private static int ConvertStringCharToUtf32(char c)
	{
		int num = -1;
		if (!UberTextLineWrapper.s_charUtf32Map.TryGetValue(c, out num))
		{
			num = char.ConvertToUtf32(c.ToString(), 0);
			UberTextLineWrapper.s_charUtf32Map.Add(c, num);
		}
		return num;
	}

	// Token: 0x06009222 RID: 37410 RVA: 0x002F7690 File Offset: 0x002F5890
	private bool CanWrapBetween(char lastChar, char wideChar, char nextChar)
	{
		int num = UberTextLineWrapper.ConvertStringCharToUtf32(lastChar);
		int num2 = UberTextLineWrapper.ConvertStringCharToUtf32(wideChar);
		int num3 = 0;
		if (nextChar != '\0')
		{
			num3 = UberTextLineWrapper.ConvertStringCharToUtf32(nextChar);
		}
		bool flag = char.IsWhiteSpace(wideChar);
		bool flag2 = char.IsWhiteSpace(lastChar);
		if ((Localization.GetLocale() == Locale.frFR || Localization.GetLocale() == Locale.deDE) && flag)
		{
			if (num3 <= 59)
			{
				if (num3 != 33 && num3 != 46 && num3 - 58 > 1)
				{
					goto IL_6B;
				}
			}
			else if (num3 != 63 && num3 != 171 && num3 != 187)
			{
				goto IL_6B;
			}
			return false;
			IL_6B:
			if (num == 171)
			{
				return false;
			}
		}
		if (num == 45)
		{
			return num2 < 48 || num2 > 57;
		}
		if (num == 59)
		{
			return true;
		}
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
		if (num <= 12304)
		{
			if (num <= 123)
			{
				if (num <= 40)
				{
					if (num != 36 && num != 40)
					{
						goto IL_1C7;
					}
				}
				else if (num - 91 > 1 && num != 123)
				{
					goto IL_1C7;
				}
			}
			else if (num <= 8220)
			{
				if (num != 8216 && num != 8220)
				{
					goto IL_1C7;
				}
			}
			else if (num != 8245)
			{
				switch (num)
				{
				case 12296:
				case 12298:
				case 12300:
				case 12302:
				case 12304:
					break;
				case 12297:
				case 12299:
				case 12301:
				case 12303:
					goto IL_1C7;
				default:
					goto IL_1C7;
				}
			}
		}
		else if (num <= 65284)
		{
			if (num <= 12317)
			{
				if (num != 12308 && num != 12317)
				{
					goto IL_1C7;
				}
			}
			else
			{
				switch (num)
				{
				case 65113:
				case 65115:
				case 65117:
					break;
				case 65114:
				case 65116:
					goto IL_1C7;
				default:
					if (num != 65284)
					{
						goto IL_1C7;
					}
					break;
				}
			}
		}
		else if (num <= 65339)
		{
			if (num != 65288 && num != 65339)
			{
				goto IL_1C7;
			}
		}
		else if (num != 65371 && num != 65505 && num - 65509 > 1)
		{
			goto IL_1C7;
		}
		return false;
		IL_1C7:
		if (num2 <= 8451)
		{
			if (num2 <= 125)
			{
				if (num2 <= 44)
				{
					if (num2 <= 37)
					{
						if (num2 != 33 && num2 != 37)
						{
							goto IL_442;
						}
					}
					else if (num2 != 41 && num2 != 44)
					{
						goto IL_442;
					}
				}
				else if (num2 <= 59)
				{
					if (num2 != 46 && num2 - 58 > 1)
					{
						goto IL_442;
					}
				}
				else if (num2 != 63 && num2 != 93 && num2 != 125)
				{
					goto IL_442;
				}
			}
			else if (num2 <= 8217)
			{
				if (num2 <= 183)
				{
					if (num2 != 176 && num2 != 183)
					{
						goto IL_442;
					}
				}
				else if (num2 - 8211 > 1 && num2 != 8217)
				{
					goto IL_442;
				}
			}
			else if (num2 <= 8226)
			{
				if (num2 != 8221 && num2 != 8226)
				{
					goto IL_442;
				}
			}
			else if (num2 - 8230 > 1 && num2 - 8242 > 1 && num2 != 8451)
			{
				goto IL_442;
			}
		}
		else if (num2 <= 65285)
		{
			if (num2 <= 12318)
			{
				if (num2 <= 12305)
				{
					if (num2 - 12289 > 1)
					{
						switch (num2)
						{
						case 12297:
						case 12299:
						case 12301:
						case 12303:
						case 12305:
							break;
						case 12298:
						case 12300:
						case 12302:
						case 12304:
							goto IL_442;
						default:
							goto IL_442;
						}
					}
				}
				else if (num2 != 12309 && num2 != 12318)
				{
					goto IL_442;
				}
			}
			else if (num2 <= 65072)
			{
				if (num2 != 12540 && num2 != 65072)
				{
					goto IL_442;
				}
			}
			else
			{
				switch (num2)
				{
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
					break;
				case 65107:
				case 65112:
				case 65113:
				case 65115:
				case 65117:
					goto IL_442;
				default:
					if (num2 != 65281 && num2 != 65285)
					{
						goto IL_442;
					}
					break;
				}
			}
		}
		else if (num2 <= 65311)
		{
			if (num2 <= 65292)
			{
				if (num2 != 65289 && num2 != 65292)
				{
					goto IL_442;
				}
			}
			else if (num2 != 65294 && num2 - 65306 > 1 && num2 != 65311)
			{
				goto IL_442;
			}
		}
		else if (num2 <= 65373)
		{
			if (num2 != 65341 && num2 != 65373)
			{
				goto IL_442;
			}
		}
		else if (num2 != 65392 && num2 - 65438 > 1 && num2 != 65504)
		{
			goto IL_442;
		}
		return false;
		IL_442:
		return num == 12290 || num == 65292 || ((Localization.GetLocale() != Locale.koKR || this.m_settings.Alignment != UberText.AlignmentOptions.Center) && ((num2 >= 4352 && num2 <= 4607) || (num2 >= 12288 && num2 <= 55215) || (num2 >= 63744 && num2 <= 64255) || (num2 >= 65280 && num2 <= 65439) || (num2 >= 65440 && num2 <= 65500)));
	}

	// Token: 0x06009223 RID: 37411 RVA: 0x002F7B5C File Offset: 0x002F5D5C
	private void GetUnderwearBounds(out Bounds underwearLeft, out Bounds underwearRight)
	{
		underwearLeft = default(Bounds);
		underwearRight = default(Bounds);
		if (!this.m_settings.UseUnderwear)
		{
			return;
		}
		Vector3 size = new Vector3(this.m_settings.Width * this.m_settings.UnderwearWidth * 0.5f, this.m_settings.Height * this.m_settings.UnderwearHeight * 0.5f, 1f);
		float num = 0f;
		if (this.m_settings.UnderwearFlip)
		{
			num = num + this.m_settings.Height * 0.5f - this.m_settings.Height * this.m_settings.UnderwearHeight * 0.5f;
		}
		else
		{
			num = num - this.m_settings.Height * 0.5f + this.m_settings.Height * this.m_settings.UnderwearHeight * 0.5f;
		}
		Vector3 zero = Vector3.zero;
		zero.x = zero.x + this.m_settings.Width * 0.5f - this.m_settings.Width * 0.5f * this.m_settings.UnderwearWidth * 0.5f;
		zero.y = num;
		underwearLeft.center = zero;
		underwearLeft.size = size;
		Vector3 zero2 = Vector3.zero;
		zero2.x = zero2.x - this.m_settings.Width * 0.5f + this.m_settings.Width * 0.5f * this.m_settings.UnderwearWidth * 0.5f;
		zero2.y = num;
		underwearRight.center = zero2;
		underwearRight.size = size;
	}

	// Token: 0x06009224 RID: 37412 RVA: 0x002F7D04 File Offset: 0x002F5F04
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
		int i = end.Length - 1;
		while (i >= 0)
		{
			if (toSearch[num] != end[i])
			{
				return false;
			}
			i--;
			num--;
		}
		return true;
	}

	// Token: 0x06009225 RID: 37413 RVA: 0x002F7D74 File Offset: 0x002F5F74
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

	// Token: 0x06009226 RID: 37414 RVA: 0x002F7DA4 File Offset: 0x002F5FA4
	private float GetFinalContainerWidth(string text, float orgWidth, Bounds underwearLeft, Bounds underwearRight)
	{
		this.m_uberTextRendering.SetText(text);
		Bounds textMeshBounds = this.m_uberTextRendering.GetTextMeshBounds();
		float result = orgWidth;
		float y = textMeshBounds.size.y;
		if (this.m_settings.UnderwearFlip)
		{
			if (y - (this.m_settings.Height - y) * 0.2f < this.m_settings.UnderwearHeightLocaleAdjustment)
			{
				result = this.m_settings.UnderwearWidthLocaleAdjustment;
			}
			else
			{
				result = orgWidth;
			}
		}
		else if (textMeshBounds.Intersects(underwearLeft) || textMeshBounds.Intersects(underwearRight))
		{
			result = this.m_settings.UnderwearWidthLocaleAdjustment;
		}
		return result;
	}

	// Token: 0x06009227 RID: 37415 RVA: 0x002F7E3C File Offset: 0x002F603C
	private bool IsUnderwearNeeded(string[] words, int textLength, float orgWidth)
	{
		bool result = this.m_settings.UseUnderwear && !this.m_settings.UnderwearFlip;
		if (this.m_settings.UseUnderwear && this.m_settings.UnderwearFlip)
		{
			StringBuilder stringBuilder = new StringBuilder(textLength, textLength);
			StringBuilder stringBuilder2 = new StringBuilder(textLength, textLength);
			foreach (string text in words)
			{
				string text2;
				string value = UberText.RemoveTagsFromWord(text, out text2, this.m_settings.RichText);
				stringBuilder2.Append(value);
				string text3 = stringBuilder2.ToString();
				this.m_uberTextRendering.SetText(text3);
				if (this.m_uberTextRendering.GetTextMeshBounds().size.x >= orgWidth)
				{
					result = true;
					break;
				}
				stringBuilder.Append(text);
			}
			if (UberTextLineWrapper.ContainsChar(stringBuilder.ToString(), "\n"[0]))
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x04007AAB RID: 31403
	private static readonly Dictionary<char, int> s_charUtf32Map = new Dictionary<char, int>(256, new UberTextLineWrapper.CharComparer());

	// Token: 0x04007AAC RID: 31404
	private static readonly StringBuilder s_wordBuilder = new StringBuilder(128);

	// Token: 0x04007AAD RID: 31405
	private static readonly List<string> s_wordList = new List<string>(256);

	// Token: 0x04007AAE RID: 31406
	private const int CharNotFoundIndex = -1;

	// Token: 0x04007AAF RID: 31407
	private const float CharWidthScaleFactor = 0.1f;

	// Token: 0x04007AB0 RID: 31408
	private const float UnderwearScaleFactor = 0.2f;

	// Token: 0x04007AB1 RID: 31409
	private readonly UberTextRendering m_uberTextRendering;

	// Token: 0x04007AB2 RID: 31410
	private readonly Dictionary<char, float> m_charWidthMap = new Dictionary<char, float>(256, new UberTextLineWrapper.CharComparer());

	// Token: 0x04007AB3 RID: 31411
	private WordWrapSettings m_settings;

	// Token: 0x04007AB4 RID: 31412
	private CharacterInfo m_characterInfo;

	// Token: 0x04007AB5 RID: 31413
	private Font m_font;

	// Token: 0x04007AB6 RID: 31414
	private int m_fontSize;

	// Token: 0x04007AB7 RID: 31415
	private float m_characterSize;

	// Token: 0x04007AB8 RID: 31416
	private float[] m_charWidths;

	// Token: 0x020026EC RID: 9964
	private class CharComparer : IEqualityComparer<char>
	{
		// Token: 0x060138A8 RID: 80040 RVA: 0x003F15F5 File Offset: 0x003EF7F5
		public bool Equals(char x, char y)
		{
			return x == y;
		}

		// Token: 0x060138A9 RID: 80041 RVA: 0x0028AEDB File Offset: 0x002890DB
		public int GetHashCode(char obj)
		{
			return (int)obj;
		}
	}
}
