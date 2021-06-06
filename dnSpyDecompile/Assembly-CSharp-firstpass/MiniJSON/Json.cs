using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace MiniJSON
{
	// Token: 0x020001E9 RID: 489
	public static class Json
	{
		// Token: 0x06001ED5 RID: 7893 RVA: 0x0006BC20 File Offset: 0x00069E20
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

		// Token: 0x06001ED6 RID: 7894 RVA: 0x0006BC54 File Offset: 0x00069E54
		public static string Serialize(object obj)
		{
			return Json.Serializer.Serialize(obj);
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x0006BC5C File Offset: 0x00069E5C
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

		// Token: 0x04000AFE RID: 2814
		private const string INDENT_STRING = "    ";

		// Token: 0x02000675 RID: 1653
		private sealed class Parser : IDisposable
		{
			// Token: 0x060061C2 RID: 25026 RVA: 0x00127286 File Offset: 0x00125486
			public static bool IsWordBreak(char c)
			{
				return char.IsWhiteSpace(c) || "{}[],:\"".IndexOf(c) != -1;
			}

			// Token: 0x060061C3 RID: 25027 RVA: 0x001272A3 File Offset: 0x001254A3
			private Parser(string jsonString)
			{
				this.json = new StringReader(jsonString);
			}

			// Token: 0x060061C4 RID: 25028 RVA: 0x001272B8 File Offset: 0x001254B8
			public static object Parse(string jsonString)
			{
				object result;
				using (Json.Parser parser = new Json.Parser(jsonString))
				{
					result = parser.ParseValue();
				}
				return result;
			}

			// Token: 0x060061C5 RID: 25029 RVA: 0x001272F0 File Offset: 0x001254F0
			public void Dispose()
			{
				this.json.Dispose();
				this.json = null;
			}

			// Token: 0x060061C6 RID: 25030 RVA: 0x00127304 File Offset: 0x00125504
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

			// Token: 0x060061C7 RID: 25031 RVA: 0x0012736C File Offset: 0x0012556C
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

			// Token: 0x060061C8 RID: 25032 RVA: 0x001273CC File Offset: 0x001255CC
			private object ParseValue()
			{
				Json.Parser.TOKEN nextToken = this.NextToken;
				return this.ParseByToken(nextToken);
			}

			// Token: 0x060061C9 RID: 25033 RVA: 0x001273E8 File Offset: 0x001255E8
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

			// Token: 0x060061CA RID: 25034 RVA: 0x00127458 File Offset: 0x00125658
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

			// Token: 0x060061CB RID: 25035 RVA: 0x001275A8 File Offset: 0x001257A8
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

			// Token: 0x060061CC RID: 25036 RVA: 0x001275FA File Offset: 0x001257FA
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

			// Token: 0x17001286 RID: 4742
			// (get) Token: 0x060061CD RID: 25037 RVA: 0x00127625 File Offset: 0x00125825
			private char PeekChar
			{
				get
				{
					return Convert.ToChar(this.json.Peek());
				}
			}

			// Token: 0x17001287 RID: 4743
			// (get) Token: 0x060061CE RID: 25038 RVA: 0x00127637 File Offset: 0x00125837
			private char NextChar
			{
				get
				{
					return Convert.ToChar(this.json.Read());
				}
			}

			// Token: 0x17001288 RID: 4744
			// (get) Token: 0x060061CF RID: 25039 RVA: 0x0012764C File Offset: 0x0012584C
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

			// Token: 0x17001289 RID: 4745
			// (get) Token: 0x060061D0 RID: 25040 RVA: 0x00127690 File Offset: 0x00125890
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

			// Token: 0x04002183 RID: 8579
			private const string WORD_BREAK = "{}[],:\"";

			// Token: 0x04002184 RID: 8580
			private StringReader json;

			// Token: 0x0200070A RID: 1802
			private enum TOKEN
			{
				// Token: 0x040022DA RID: 8922
				NONE,
				// Token: 0x040022DB RID: 8923
				CURLY_OPEN,
				// Token: 0x040022DC RID: 8924
				CURLY_CLOSE,
				// Token: 0x040022DD RID: 8925
				SQUARED_OPEN,
				// Token: 0x040022DE RID: 8926
				SQUARED_CLOSE,
				// Token: 0x040022DF RID: 8927
				COLON,
				// Token: 0x040022E0 RID: 8928
				COMMA,
				// Token: 0x040022E1 RID: 8929
				STRING,
				// Token: 0x040022E2 RID: 8930
				NUMBER,
				// Token: 0x040022E3 RID: 8931
				TRUE,
				// Token: 0x040022E4 RID: 8932
				FALSE,
				// Token: 0x040022E5 RID: 8933
				NULL
			}
		}

		// Token: 0x02000676 RID: 1654
		private sealed class Serializer
		{
			// Token: 0x060061D1 RID: 25041 RVA: 0x001277B2 File Offset: 0x001259B2
			private Serializer()
			{
				this.builder = new StringBuilder();
			}

			// Token: 0x060061D2 RID: 25042 RVA: 0x001277C5 File Offset: 0x001259C5
			public static string Serialize(object obj)
			{
				Json.Serializer serializer = new Json.Serializer();
				serializer.SerializeValue(obj);
				return serializer.builder.ToString();
			}

			// Token: 0x060061D3 RID: 25043 RVA: 0x001277E0 File Offset: 0x001259E0
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

			// Token: 0x060061D4 RID: 25044 RVA: 0x00127884 File Offset: 0x00125A84
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

			// Token: 0x060061D5 RID: 25045 RVA: 0x00127930 File Offset: 0x00125B30
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

			// Token: 0x060061D6 RID: 25046 RVA: 0x00127990 File Offset: 0x00125B90
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

			// Token: 0x060061D7 RID: 25047 RVA: 0x00127AE4 File Offset: 0x00125CE4
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

			// Token: 0x04002185 RID: 8581
			private StringBuilder builder;
		}
	}
}
