using System;
using System.Text;

namespace bgs
{
	// Token: 0x02000237 RID: 567
	public class Tokenizer
	{
		// Token: 0x060023B3 RID: 9139 RVA: 0x0007E04D File Offset: 0x0007C24D
		public Tokenizer(string str)
		{
			this.m_chars = str.ToCharArray();
			this.m_index = 0;
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x0007E068 File Offset: 0x0007C268
		private char NextChar()
		{
			if (this.m_index >= this.m_chars.Length)
			{
				return '\0';
			}
			char result = this.m_chars[this.m_index];
			this.m_index++;
			return result;
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x0007E097 File Offset: 0x0007C297
		private void PrevChar()
		{
			if (this.m_index > 0)
			{
				this.m_index--;
			}
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x0007E0B0 File Offset: 0x0007C2B0
		private char CurrentChar()
		{
			if (this.m_index >= this.m_chars.Length)
			{
				return '\0';
			}
			return this.m_chars[this.m_index];
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x0007E0D1 File Offset: 0x0007C2D1
		private bool IsWhiteSpace(char c)
		{
			return c != '\0' && c <= ' ';
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x0007E0E0 File Offset: 0x0007C2E0
		private bool IsDecimal(char c)
		{
			return c >= '0' && c <= '9';
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x0007E0F1 File Offset: 0x0007C2F1
		private bool IsEOF()
		{
			return this.m_index >= this.m_chars.Length;
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x0007E106 File Offset: 0x0007C306
		private bool NextCharIsWhiteSpace()
		{
			return this.m_index + 1 < this.m_chars.Length && this.IsWhiteSpace(this.m_chars[this.m_index + 1]);
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x0007E131 File Offset: 0x0007C331
		public void ClearWhiteSpace()
		{
			while (this.IsWhiteSpace(this.CurrentChar()))
			{
				this.NextChar();
			}
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x0007E14C File Offset: 0x0007C34C
		public string NextString()
		{
			this.ClearWhiteSpace();
			if (this.IsEOF())
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (;;)
			{
				char c = this.CurrentChar();
				if (c == '\0' || this.IsWhiteSpace(c))
				{
					break;
				}
				stringBuilder.Append(c);
				this.NextChar();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x0007E199 File Offset: 0x0007C399
		public void SkipUnknownToken()
		{
			this.ClearWhiteSpace();
			if (this.IsEOF())
			{
				return;
			}
			if (this.CurrentChar() == '"')
			{
				this.NextQuotedString();
				return;
			}
			this.NextString();
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x0007E1C4 File Offset: 0x0007C3C4
		public string NextQuotedString()
		{
			this.ClearWhiteSpace();
			if (this.IsEOF())
			{
				return null;
			}
			char c = this.NextChar();
			if (c != '"')
			{
				throw new Exception(string.Format("Expected quoted string.  Found {0} instead of quote.", c));
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (;;)
			{
				char c2 = this.CurrentChar();
				if (c2 == '"')
				{
					break;
				}
				if (c2 == '\0')
				{
					goto Block_4;
				}
				stringBuilder.Append(c2);
				this.NextChar();
			}
			this.NextChar();
			return stringBuilder.ToString();
			Block_4:
			throw new Exception("Parsing ended before quoted string was completed.");
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x0007E243 File Offset: 0x0007C443
		public void NextOpenBracket()
		{
			this.ClearWhiteSpace();
			if (this.CurrentChar() != '{')
			{
				throw new Exception("Expected open bracket.");
			}
			this.NextChar();
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x0007E268 File Offset: 0x0007C468
		public uint NextUInt32()
		{
			this.ClearWhiteSpace();
			uint num = 0U;
			char c;
			for (;;)
			{
				c = this.CurrentChar();
				if (c == '\0' || this.IsWhiteSpace(c))
				{
					return num;
				}
				if (!this.IsDecimal(c))
				{
					break;
				}
				uint num2 = (uint)(c - '0');
				num *= 10U;
				num += num2;
				this.NextChar();
			}
			throw new Exception(string.Format("Found a non-numeric value while parsing an int: {0}", c));
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x0007E2C8 File Offset: 0x0007C4C8
		public float NextFloat()
		{
			this.ClearWhiteSpace();
			float num = 1f;
			float num2 = 0f;
			if (this.CurrentChar() == '-')
			{
				num = -1f;
				this.NextChar();
			}
			bool flag = false;
			float num3 = 1f;
			char c;
			for (;;)
			{
				c = this.CurrentChar();
				if (c == '\0' || this.IsWhiteSpace(c))
				{
					goto IL_D8;
				}
				if (c == 'f')
				{
					break;
				}
				if (c == '.')
				{
					flag = true;
					this.NextChar();
				}
				else
				{
					if (!this.IsDecimal(c))
					{
						goto Block_6;
					}
					float num4 = c - '0';
					if (!flag)
					{
						num2 *= 10f;
						num2 += num4;
					}
					else
					{
						float num5 = num4 * (float)Math.Pow(0.1, (double)num3);
						num2 += num5;
						num3 += 1f;
					}
					this.NextChar();
				}
			}
			this.NextChar();
			goto IL_D8;
			Block_6:
			throw new Exception(string.Format("Found a non-numeric value while parsing an int: {0}", c));
			IL_D8:
			return num * num2;
		}

		// Token: 0x04000E9F RID: 3743
		private char[] m_chars;

		// Token: 0x04000EA0 RID: 3744
		private int m_index;

		// Token: 0x04000EA1 RID: 3745
		private const char NULLCHAR = '\0';
	}
}
