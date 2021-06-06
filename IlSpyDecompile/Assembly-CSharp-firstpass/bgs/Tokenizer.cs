using System;
using System.Text;

namespace bgs
{
	public class Tokenizer
	{
		private char[] m_chars;

		private int m_index;

		private const char NULLCHAR = '\0';

		public Tokenizer(string str)
		{
			m_chars = str.ToCharArray();
			m_index = 0;
		}

		private char NextChar()
		{
			if (m_index >= m_chars.Length)
			{
				return '\0';
			}
			char result = m_chars[m_index];
			m_index++;
			return result;
		}

		private void PrevChar()
		{
			if (m_index > 0)
			{
				m_index--;
			}
		}

		private char CurrentChar()
		{
			if (m_index >= m_chars.Length)
			{
				return '\0';
			}
			return m_chars[m_index];
		}

		private bool IsWhiteSpace(char c)
		{
			if (c != 0)
			{
				return c <= ' ';
			}
			return false;
		}

		private bool IsDecimal(char c)
		{
			if (c >= '0')
			{
				return c <= '9';
			}
			return false;
		}

		private bool IsEOF()
		{
			return m_index >= m_chars.Length;
		}

		private bool NextCharIsWhiteSpace()
		{
			if (m_index + 1 >= m_chars.Length)
			{
				return false;
			}
			return IsWhiteSpace(m_chars[m_index + 1]);
		}

		public void ClearWhiteSpace()
		{
			while (IsWhiteSpace(CurrentChar()))
			{
				NextChar();
			}
		}

		public string NextString()
		{
			ClearWhiteSpace();
			if (IsEOF())
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			while (true)
			{
				char c = CurrentChar();
				if (c == '\0' || IsWhiteSpace(c))
				{
					break;
				}
				stringBuilder.Append(c);
				NextChar();
			}
			return stringBuilder.ToString();
		}

		public void SkipUnknownToken()
		{
			ClearWhiteSpace();
			if (!IsEOF())
			{
				if (CurrentChar() == '"')
				{
					NextQuotedString();
				}
				else
				{
					NextString();
				}
			}
		}

		public string NextQuotedString()
		{
			ClearWhiteSpace();
			if (IsEOF())
			{
				return null;
			}
			char c = NextChar();
			if (c != '"')
			{
				throw new Exception($"Expected quoted string.  Found {c} instead of quote.");
			}
			StringBuilder stringBuilder = new StringBuilder();
			while (true)
			{
				char c2 = CurrentChar();
				switch (c2)
				{
				case '"':
					NextChar();
					return stringBuilder.ToString();
				case '\0':
					throw new Exception("Parsing ended before quoted string was completed.");
				}
				stringBuilder.Append(c2);
				NextChar();
			}
		}

		public void NextOpenBracket()
		{
			ClearWhiteSpace();
			if (CurrentChar() != '{')
			{
				throw new Exception("Expected open bracket.");
			}
			NextChar();
		}

		public uint NextUInt32()
		{
			ClearWhiteSpace();
			uint num = 0u;
			while (true)
			{
				char c = CurrentChar();
				if (c == '\0' || IsWhiteSpace(c))
				{
					break;
				}
				if (!IsDecimal(c))
				{
					throw new Exception($"Found a non-numeric value while parsing an int: {c}");
				}
				uint num2 = (uint)(c - 48);
				num *= 10;
				num += num2;
				NextChar();
			}
			return num;
		}

		public float NextFloat()
		{
			ClearWhiteSpace();
			float num = 1f;
			float num2 = 0f;
			if (CurrentChar() == '-')
			{
				num = -1f;
				NextChar();
			}
			bool flag = false;
			float num3 = 1f;
			while (true)
			{
				char c = CurrentChar();
				if (c == '\0' || IsWhiteSpace(c))
				{
					break;
				}
				switch (c)
				{
				case 'f':
					break;
				case '.':
					flag = true;
					NextChar();
					continue;
				default:
				{
					if (!IsDecimal(c))
					{
						throw new Exception($"Found a non-numeric value while parsing an int: {c}");
					}
					float num4 = (uint)(c - 48);
					if (!flag)
					{
						num2 *= 10f;
						num2 += num4;
					}
					else
					{
						float num5 = num4 * (float)Math.Pow(0.1, num3);
						num2 += num5;
						num3 += 1f;
					}
					NextChar();
					continue;
				}
				}
				NextChar();
				break;
			}
			return num * num2;
		}
	}
}
