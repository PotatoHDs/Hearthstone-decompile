using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Blizzard.BlizzardErrorMobile.MiniJSON
{
	// Token: 0x0200121E RID: 4638
	public static class Json
	{
		// Token: 0x0600D03C RID: 53308 RVA: 0x003DF3B0 File Offset: 0x003DD5B0
		public static object Deserialize(string json)
		{
			object result;
			try
			{
				if (json == null)
				{
					result = null;
				}
				else
				{
					result = Json.Parser.Parse(json);
				}
			}
			catch (OverflowException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600D03D RID: 53309 RVA: 0x003DF3E4 File Offset: 0x003DD5E4
		public static string Serialize(object obj)
		{
			return Json.Serializer.Serialize(obj);
		}

		// Token: 0x0600D03E RID: 53310 RVA: 0x003DF3EC File Offset: 0x003DD5EC
		public static string FormatJson(string str)
		{
			int num = 0;
			bool flag = false;
			StringBuilder sb = new StringBuilder();
			int i = 0;
			Action<int> <>9__0;
			Action<int> <>9__1;
			Action<int> <>9__2;
			while (i < str.Length)
			{
				char c = str[i];
				if (c <= ':')
				{
					if (c != '"')
					{
						if (c != ',')
						{
							if (c != ':')
							{
								goto IL_1DF;
							}
							sb.Append(c);
							if (!flag)
							{
								sb.Append(" ");
							}
						}
						else
						{
							sb.Append(c);
							if (!flag)
							{
								sb.AppendLine();
								List<int> list = Enumerable.Range(0, num).ToList<int>();
								Action<int> action;
								if ((action = <>9__2) == null)
								{
									action = (<>9__2 = delegate(int item)
									{
										sb.Append("    ");
									});
								}
								list.ForEach(action);
							}
						}
					}
					else
					{
						sb.Append(c);
						bool flag2 = false;
						int num2 = i;
						while (num2 > 0 && str[--num2] == '\\')
						{
							flag2 = !flag2;
						}
						if (!flag2)
						{
							flag = !flag;
						}
					}
				}
				else
				{
					if (c <= ']')
					{
						if (c != '[')
						{
							if (c != ']')
							{
								goto IL_1DF;
							}
							goto IL_CE;
						}
					}
					else if (c != '{')
					{
						if (c != '}')
						{
							goto IL_1DF;
						}
						goto IL_CE;
					}
					sb.Append(c);
					if (!flag)
					{
						sb.AppendLine();
						List<int> list2 = Enumerable.Range(0, ++num).ToList<int>();
						Action<int> action2;
						if ((action2 = <>9__0) == null)
						{
							action2 = (<>9__0 = delegate(int item)
							{
								sb.Append("    ");
							});
						}
						list2.ForEach(action2);
						goto IL_1ED;
					}
					goto IL_1ED;
					IL_CE:
					if (!flag)
					{
						sb.AppendLine();
						List<int> list3 = Enumerable.Range(0, --num).ToList<int>();
						Action<int> action3;
						if ((action3 = <>9__1) == null)
						{
							action3 = (<>9__1 = delegate(int item)
							{
								sb.Append("    ");
							});
						}
						list3.ForEach(action3);
					}
					sb.Append(c);
				}
				IL_1ED:
				i++;
				continue;
				IL_1DF:
				sb.Append(c);
				goto IL_1ED;
			}
			return sb.ToString();
		}

		// Token: 0x0400A267 RID: 41575
		private const string INDENT_STRING = "    ";

		// Token: 0x0200295C RID: 10588
		private sealed class Parser : IDisposable
		{
			// Token: 0x06013EAB RID: 81579 RVA: 0x00540966 File Offset: 0x0053EB66
			public static bool IsWordBreak(char c)
			{
				return char.IsWhiteSpace(c) || "{}[],:\"".IndexOf(c) != -1;
			}

			// Token: 0x06013EAC RID: 81580 RVA: 0x00540983 File Offset: 0x0053EB83
			private Parser(string jsonString)
			{
				this.json = new StringReader(jsonString);
			}

			// Token: 0x06013EAD RID: 81581 RVA: 0x00540998 File Offset: 0x0053EB98
			public static object Parse(string jsonString)
			{
				object result;
				using (Json.Parser parser = new Json.Parser(jsonString))
				{
					result = parser.ParseValue();
				}
				return result;
			}

			// Token: 0x06013EAE RID: 81582 RVA: 0x005409D0 File Offset: 0x0053EBD0
			public void Dispose()
			{
				this.json.Dispose();
				this.json = null;
			}

			// Token: 0x06013EAF RID: 81583 RVA: 0x005409E4 File Offset: 0x0053EBE4
			private JsonNode ParseObject()
			{
				JsonNode jsonNode = new JsonNode();
				this.json.Read();
				for (;;)
				{
					Json.Parser.TOKEN nextToken = this.NextToken;
					if (nextToken == Json.Parser.TOKEN.NONE)
					{
						break;
					}
					if (nextToken == Json.Parser.TOKEN.CURLY_CLOSE)
					{
						return jsonNode;
					}
					if (nextToken != Json.Parser.TOKEN.COMMA)
					{
						string text = this.ParseString();
						if (text == null)
						{
							goto Block_4;
						}
						if (this.NextToken != Json.Parser.TOKEN.COLON)
						{
							goto Block_5;
						}
						this.json.Read();
						jsonNode[text] = this.ParseValue();
					}
				}
				return null;
				Block_4:
				return null;
				Block_5:
				return null;
			}

			// Token: 0x06013EB0 RID: 81584 RVA: 0x00540A4C File Offset: 0x0053EC4C
			private JsonList ParseArray()
			{
				JsonList jsonList = new JsonList();
				this.json.Read();
				bool flag = true;
				while (flag)
				{
					Json.Parser.TOKEN nextToken = this.NextToken;
					if (nextToken == Json.Parser.TOKEN.NONE)
					{
						return null;
					}
					if (nextToken != Json.Parser.TOKEN.SQUARED_CLOSE)
					{
						if (nextToken != Json.Parser.TOKEN.COMMA)
						{
							object obj = this.ParseByToken(nextToken);
							jsonList.Add(obj);
							if (obj == null)
							{
								this.json.Read();
							}
						}
					}
					else
					{
						flag = false;
					}
				}
				return jsonList;
			}

			// Token: 0x06013EB1 RID: 81585 RVA: 0x00540AAC File Offset: 0x0053ECAC
			private object ParseValue()
			{
				Json.Parser.TOKEN nextToken = this.NextToken;
				return this.ParseByToken(nextToken);
			}

			// Token: 0x06013EB2 RID: 81586 RVA: 0x00540AC8 File Offset: 0x0053ECC8
			private object ParseByToken(Json.Parser.TOKEN token)
			{
				switch (token)
				{
				case Json.Parser.TOKEN.CURLY_OPEN:
					return this.ParseObject();
				case Json.Parser.TOKEN.SQUARED_OPEN:
					return this.ParseArray();
				case Json.Parser.TOKEN.STRING:
					return this.ParseString();
				case Json.Parser.TOKEN.NUMBER:
					return this.ParseNumber();
				case Json.Parser.TOKEN.TRUE:
					return true;
				case Json.Parser.TOKEN.FALSE:
					return false;
				case Json.Parser.TOKEN.NULL:
					return null;
				}
				return null;
			}

			// Token: 0x06013EB3 RID: 81587 RVA: 0x00540B38 File Offset: 0x0053ED38
			private string ParseString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				this.json.Read();
				bool flag = true;
				while (flag)
				{
					if (this.json.Peek() == -1)
					{
						break;
					}
					char nextChar = this.NextChar;
					if (nextChar != '"')
					{
						if (nextChar != '\\')
						{
							stringBuilder.Append(nextChar);
						}
						else if (this.json.Peek() == -1)
						{
							flag = false;
						}
						else
						{
							nextChar = this.NextChar;
							if (nextChar <= '\\')
							{
								if (nextChar == '"' || nextChar == '/' || nextChar == '\\')
								{
									stringBuilder.Append(nextChar);
								}
							}
							else if (nextChar <= 'f')
							{
								if (nextChar != 'b')
								{
									if (nextChar == 'f')
									{
										stringBuilder.Append('\f');
									}
								}
								else
								{
									stringBuilder.Append('\b');
								}
							}
							else if (nextChar != 'n')
							{
								switch (nextChar)
								{
								case 'r':
									stringBuilder.Append('\r');
									break;
								case 't':
									stringBuilder.Append('\t');
									break;
								case 'u':
								{
									char[] array = new char[4];
									for (int i = 0; i < 4; i++)
									{
										array[i] = this.NextChar;
									}
									stringBuilder.Append((char)Convert.ToInt32(new string(array), 16));
									break;
								}
								}
							}
							else
							{
								stringBuilder.Append('\n');
							}
						}
					}
					else
					{
						flag = false;
					}
				}
				return stringBuilder.ToString();
			}

			// Token: 0x06013EB4 RID: 81588 RVA: 0x00540C88 File Offset: 0x0053EE88
			private object ParseNumber()
			{
				string nextWord = this.NextWord;
				if (nextWord.IndexOf('.') == -1)
				{
					long num;
					long.TryParse(nextWord, NumberStyles.Any, CultureInfo.InvariantCulture, out num);
					return num;
				}
				double num2;
				double.TryParse(nextWord, NumberStyles.Any, CultureInfo.InvariantCulture, out num2);
				return num2;
			}

			// Token: 0x06013EB5 RID: 81589 RVA: 0x00540CDA File Offset: 0x0053EEDA
			private void EatWhitespace()
			{
				while (char.IsWhiteSpace(this.PeekChar))
				{
					this.json.Read();
					if (this.json.Peek() == -1)
					{
						break;
					}
				}
			}

			// Token: 0x17002D83 RID: 11651
			// (get) Token: 0x06013EB6 RID: 81590 RVA: 0x00540D05 File Offset: 0x0053EF05
			private char PeekChar
			{
				get
				{
					return Convert.ToChar(this.json.Peek());
				}
			}

			// Token: 0x17002D84 RID: 11652
			// (get) Token: 0x06013EB7 RID: 81591 RVA: 0x00540D17 File Offset: 0x0053EF17
			private char NextChar
			{
				get
				{
					return Convert.ToChar(this.json.Read());
				}
			}

			// Token: 0x17002D85 RID: 11653
			// (get) Token: 0x06013EB8 RID: 81592 RVA: 0x00540D2C File Offset: 0x0053EF2C
			private string NextWord
			{
				get
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (!Json.Parser.IsWordBreak(this.PeekChar))
					{
						stringBuilder.Append(this.NextChar);
						if (this.json.Peek() == -1)
						{
							break;
						}
					}
					return stringBuilder.ToString();
				}
			}

			// Token: 0x17002D86 RID: 11654
			// (get) Token: 0x06013EB9 RID: 81593 RVA: 0x00540D70 File Offset: 0x0053EF70
			private Json.Parser.TOKEN NextToken
			{
				get
				{
					this.EatWhitespace();
					if (this.json.Peek() == -1)
					{
						return Json.Parser.TOKEN.NONE;
					}
					char peekChar = this.PeekChar;
					if (peekChar <= '[')
					{
						switch (peekChar)
						{
						case '"':
							return Json.Parser.TOKEN.STRING;
						case '#':
						case '$':
						case '%':
						case '&':
						case '\'':
						case '(':
						case ')':
						case '*':
						case '+':
						case '.':
						case '/':
							break;
						case ',':
							this.json.Read();
							return Json.Parser.TOKEN.COMMA;
						case '-':
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
							return Json.Parser.TOKEN.NUMBER;
						case ':':
							return Json.Parser.TOKEN.COLON;
						default:
							if (peekChar == '[')
							{
								return Json.Parser.TOKEN.SQUARED_OPEN;
							}
							break;
						}
					}
					else
					{
						if (peekChar == ']')
						{
							this.json.Read();
							return Json.Parser.TOKEN.SQUARED_CLOSE;
						}
						if (peekChar == '{')
						{
							return Json.Parser.TOKEN.CURLY_OPEN;
						}
						if (peekChar == '}')
						{
							this.json.Read();
							return Json.Parser.TOKEN.CURLY_CLOSE;
						}
					}
					string nextWord = this.NextWord;
					if (nextWord == "false")
					{
						return Json.Parser.TOKEN.FALSE;
					}
					if (nextWord == "true")
					{
						return Json.Parser.TOKEN.TRUE;
					}
					if (!(nextWord == "null"))
					{
						return Json.Parser.TOKEN.NONE;
					}
					return Json.Parser.TOKEN.NULL;
				}
			}

			// Token: 0x0400FCBC RID: 64700
			private const string WORD_BREAK = "{}[],:\"";

			// Token: 0x0400FCBD RID: 64701
			private StringReader json;

			// Token: 0x020029B4 RID: 10676
			private enum TOKEN
			{
				// Token: 0x0400FE26 RID: 65062
				NONE,
				// Token: 0x0400FE27 RID: 65063
				CURLY_OPEN,
				// Token: 0x0400FE28 RID: 65064
				CURLY_CLOSE,
				// Token: 0x0400FE29 RID: 65065
				SQUARED_OPEN,
				// Token: 0x0400FE2A RID: 65066
				SQUARED_CLOSE,
				// Token: 0x0400FE2B RID: 65067
				COLON,
				// Token: 0x0400FE2C RID: 65068
				COMMA,
				// Token: 0x0400FE2D RID: 65069
				STRING,
				// Token: 0x0400FE2E RID: 65070
				NUMBER,
				// Token: 0x0400FE2F RID: 65071
				TRUE,
				// Token: 0x0400FE30 RID: 65072
				FALSE,
				// Token: 0x0400FE31 RID: 65073
				NULL
			}
		}

		// Token: 0x0200295D RID: 10589
		private sealed class Serializer
		{
			// Token: 0x06013EBA RID: 81594 RVA: 0x00540E92 File Offset: 0x0053F092
			private Serializer()
			{
				this.builder = new StringBuilder();
			}

			// Token: 0x06013EBB RID: 81595 RVA: 0x00540EA5 File Offset: 0x0053F0A5
			public static string Serialize(object obj)
			{
				Json.Serializer serializer = new Json.Serializer();
				serializer.SerializeValue(obj);
				return serializer.builder.ToString();
			}

			// Token: 0x06013EBC RID: 81596 RVA: 0x00540EC0 File Offset: 0x0053F0C0
			private void SerializeValue(object value)
			{
				if (value == null)
				{
					this.builder.Append("null");
					return;
				}
				string str;
				if ((str = (value as string)) != null)
				{
					this.SerializeString(str);
					return;
				}
				if (value is bool)
				{
					this.builder.Append(((bool)value) ? "true" : "false");
					return;
				}
				JsonList anArray;
				if ((anArray = (value as JsonList)) != null)
				{
					this.SerializeArray(anArray);
					return;
				}
				JsonNode obj;
				if ((obj = (value as JsonNode)) != null)
				{
					this.SerializeObject(obj);
					return;
				}
				if (value is char)
				{
					this.SerializeString(new string((char)value, 1));
					return;
				}
				this.SerializeOther(value);
			}

			// Token: 0x06013EBD RID: 81597 RVA: 0x00540F64 File Offset: 0x0053F164
			private void SerializeObject(JsonNode obj)
			{
				bool flag = true;
				this.builder.Append('{');
				foreach (object obj2 in obj.Keys)
				{
					if (!flag)
					{
						this.builder.Append(',');
					}
					this.SerializeString(obj2.ToString());
					this.builder.Append(':');
					this.SerializeValue(obj[(string)obj2]);
					flag = false;
				}
				this.builder.Append('}');
			}

			// Token: 0x06013EBE RID: 81598 RVA: 0x00541010 File Offset: 0x0053F210
			private void SerializeArray(JsonList anArray)
			{
				this.builder.Append('[');
				bool flag = true;
				for (int i = 0; i < anArray.Count; i++)
				{
					object value = anArray[i];
					if (!flag)
					{
						this.builder.Append(',');
					}
					this.SerializeValue(value);
					flag = false;
				}
				this.builder.Append(']');
			}

			// Token: 0x06013EBF RID: 81599 RVA: 0x00541070 File Offset: 0x0053F270
			private void SerializeString(string str)
			{
				this.builder.Append('"');
				char[] array = str.ToCharArray();
				int i = 0;
				while (i < array.Length)
				{
					char c = array[i];
					switch (c)
					{
					case '\b':
						this.builder.Append("\\b");
						break;
					case '\t':
						this.builder.Append("\\t");
						break;
					case '\n':
						this.builder.Append("\\n");
						break;
					case '\v':
						goto IL_E0;
					case '\f':
						this.builder.Append("\\f");
						break;
					case '\r':
						this.builder.Append("\\r");
						break;
					default:
						if (c != '"')
						{
							if (c != '\\')
							{
								goto IL_E0;
							}
							this.builder.Append("\\\\");
						}
						else
						{
							this.builder.Append("\\\"");
						}
						break;
					}
					IL_129:
					i++;
					continue;
					IL_E0:
					int num = Convert.ToInt32(c);
					if (num >= 32 && num <= 126)
					{
						this.builder.Append(c);
						goto IL_129;
					}
					this.builder.Append("\\u");
					this.builder.Append(num.ToString("x4"));
					goto IL_129;
				}
				this.builder.Append('"');
			}

			// Token: 0x06013EC0 RID: 81600 RVA: 0x005411C4 File Offset: 0x0053F3C4
			private void SerializeOther(object value)
			{
				if (value is float)
				{
					this.builder.Append(((float)value).ToString("R", CultureInfo.InvariantCulture));
					return;
				}
				if (value is int || value is uint || value is long || value is sbyte || value is byte || value is short || value is ushort || value is ulong)
				{
					this.builder.Append(value);
					return;
				}
				if (value is double || value is decimal)
				{
					this.builder.Append(Convert.ToDouble(value).ToString("R", CultureInfo.InvariantCulture));
					return;
				}
				this.SerializeString(value.ToString());
			}

			// Token: 0x0400FCBE RID: 64702
			private StringBuilder builder;
		}
	}
}
