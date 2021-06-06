using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Blizzard.BlizzardErrorMobile.MiniJSON
{
	public static class Json
	{
		private sealed class Parser : IDisposable
		{
			private enum TOKEN
			{
				NONE,
				CURLY_OPEN,
				CURLY_CLOSE,
				SQUARED_OPEN,
				SQUARED_CLOSE,
				COLON,
				COMMA,
				STRING,
				NUMBER,
				TRUE,
				FALSE,
				NULL
			}

			private const string WORD_BREAK = "{}[],:\"";

			private StringReader json;

			private char PeekChar => Convert.ToChar(json.Peek());

			private char NextChar => Convert.ToChar(json.Read());

			private string NextWord
			{
				get
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (!IsWordBreak(PeekChar))
					{
						stringBuilder.Append(NextChar);
						if (json.Peek() == -1)
						{
							break;
						}
					}
					return stringBuilder.ToString();
				}
			}

			private TOKEN NextToken
			{
				get
				{
					EatWhitespace();
					if (json.Peek() == -1)
					{
						return TOKEN.NONE;
					}
					switch (PeekChar)
					{
					case '{':
						return TOKEN.CURLY_OPEN;
					case '}':
						json.Read();
						return TOKEN.CURLY_CLOSE;
					case '[':
						return TOKEN.SQUARED_OPEN;
					case ']':
						json.Read();
						return TOKEN.SQUARED_CLOSE;
					case ',':
						json.Read();
						return TOKEN.COMMA;
					case '"':
						return TOKEN.STRING;
					case ':':
						return TOKEN.COLON;
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
						return TOKEN.NUMBER;
					default:
						return NextWord switch
						{
							"false" => TOKEN.FALSE, 
							"true" => TOKEN.TRUE, 
							"null" => TOKEN.NULL, 
							_ => TOKEN.NONE, 
						};
					}
				}
			}

			public static bool IsWordBreak(char c)
			{
				if (!char.IsWhiteSpace(c))
				{
					return "{}[],:\"".IndexOf(c) != -1;
				}
				return true;
			}

			private Parser(string jsonString)
			{
				json = new StringReader(jsonString);
			}

			public static object Parse(string jsonString)
			{
				using Parser parser = new Parser(jsonString);
				return parser.ParseValue();
			}

			public void Dispose()
			{
				json.Dispose();
				json = null;
			}

			private JsonNode ParseObject()
			{
				JsonNode jsonNode = new JsonNode();
				json.Read();
				while (true)
				{
					switch (NextToken)
					{
					case TOKEN.COMMA:
						continue;
					case TOKEN.NONE:
						return null;
					case TOKEN.CURLY_CLOSE:
						return jsonNode;
					}
					string text = ParseString();
					if (text == null)
					{
						return null;
					}
					if (NextToken != TOKEN.COLON)
					{
						return null;
					}
					json.Read();
					jsonNode[text] = ParseValue();
				}
			}

			private JsonList ParseArray()
			{
				JsonList jsonList = new JsonList();
				json.Read();
				bool flag = true;
				while (flag)
				{
					TOKEN nextToken = NextToken;
					switch (nextToken)
					{
					case TOKEN.NONE:
						return null;
					case TOKEN.SQUARED_CLOSE:
						flag = false;
						continue;
					case TOKEN.COMMA:
						continue;
					}
					object obj = ParseByToken(nextToken);
					jsonList.Add(obj);
					if (obj == null)
					{
						json.Read();
					}
				}
				return jsonList;
			}

			private object ParseValue()
			{
				TOKEN nextToken = NextToken;
				return ParseByToken(nextToken);
			}

			private object ParseByToken(TOKEN token)
			{
				return token switch
				{
					TOKEN.STRING => ParseString(), 
					TOKEN.NUMBER => ParseNumber(), 
					TOKEN.CURLY_OPEN => ParseObject(), 
					TOKEN.SQUARED_OPEN => ParseArray(), 
					TOKEN.TRUE => true, 
					TOKEN.FALSE => false, 
					TOKEN.NULL => null, 
					_ => null, 
				};
			}

			private string ParseString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				json.Read();
				bool flag = true;
				while (flag)
				{
					if (json.Peek() == -1)
					{
						flag = false;
						break;
					}
					char nextChar = NextChar;
					switch (nextChar)
					{
					case '"':
						flag = false;
						break;
					case '\\':
						if (json.Peek() == -1)
						{
							flag = false;
							break;
						}
						nextChar = NextChar;
						switch (nextChar)
						{
						case '"':
						case '/':
						case '\\':
							stringBuilder.Append(nextChar);
							break;
						case 'b':
							stringBuilder.Append('\b');
							break;
						case 'f':
							stringBuilder.Append('\f');
							break;
						case 'n':
							stringBuilder.Append('\n');
							break;
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
								array[i] = NextChar;
							}
							stringBuilder.Append((char)Convert.ToInt32(new string(array), 16));
							break;
						}
						}
						break;
					default:
						stringBuilder.Append(nextChar);
						break;
					}
				}
				return stringBuilder.ToString();
			}

			private object ParseNumber()
			{
				string nextWord = NextWord;
				if (nextWord.IndexOf('.') == -1)
				{
					long.TryParse(nextWord, NumberStyles.Any, CultureInfo.InvariantCulture, out var result);
					return result;
				}
				double.TryParse(nextWord, NumberStyles.Any, CultureInfo.InvariantCulture, out var result2);
				return result2;
			}

			private void EatWhitespace()
			{
				while (char.IsWhiteSpace(PeekChar))
				{
					json.Read();
					if (json.Peek() == -1)
					{
						break;
					}
				}
			}
		}

		private sealed class Serializer
		{
			private StringBuilder builder;

			private Serializer()
			{
				builder = new StringBuilder();
			}

			public static string Serialize(object obj)
			{
				Serializer serializer = new Serializer();
				serializer.SerializeValue(obj);
				return serializer.builder.ToString();
			}

			private void SerializeValue(object value)
			{
				string str;
				JsonList anArray;
				JsonNode obj;
				if (value == null)
				{
					builder.Append("null");
				}
				else if ((str = value as string) != null)
				{
					SerializeString(str);
				}
				else if (value is bool)
				{
					builder.Append(((bool)value) ? "true" : "false");
				}
				else if ((anArray = value as JsonList) != null)
				{
					SerializeArray(anArray);
				}
				else if ((obj = value as JsonNode) != null)
				{
					SerializeObject(obj);
				}
				else if (value is char)
				{
					SerializeString(new string((char)value, 1));
				}
				else
				{
					SerializeOther(value);
				}
			}

			private void SerializeObject(JsonNode obj)
			{
				bool flag = true;
				builder.Append('{');
				foreach (string key in obj.Keys)
				{
					if (!flag)
					{
						builder.Append(',');
					}
					SerializeString(key.ToString());
					builder.Append(':');
					SerializeValue(obj[key]);
					flag = false;
				}
				builder.Append('}');
			}

			private void SerializeArray(JsonList anArray)
			{
				builder.Append('[');
				bool flag = true;
				for (int i = 0; i < anArray.Count; i++)
				{
					object value = anArray[i];
					if (!flag)
					{
						builder.Append(',');
					}
					SerializeValue(value);
					flag = false;
				}
				builder.Append(']');
			}

			private void SerializeString(string str)
			{
				builder.Append('"');
				char[] array = str.ToCharArray();
				foreach (char c in array)
				{
					switch (c)
					{
					case '"':
						builder.Append("\\\"");
						continue;
					case '\\':
						builder.Append("\\\\");
						continue;
					case '\b':
						builder.Append("\\b");
						continue;
					case '\f':
						builder.Append("\\f");
						continue;
					case '\n':
						builder.Append("\\n");
						continue;
					case '\r':
						builder.Append("\\r");
						continue;
					case '\t':
						builder.Append("\\t");
						continue;
					}
					int num = Convert.ToInt32(c);
					if (num >= 32 && num <= 126)
					{
						builder.Append(c);
						continue;
					}
					builder.Append("\\u");
					builder.Append(num.ToString("x4"));
				}
				builder.Append('"');
			}

			private void SerializeOther(object value)
			{
				if (value is float)
				{
					builder.Append(((float)value).ToString("R", CultureInfo.InvariantCulture));
				}
				else if (value is int || value is uint || value is long || value is sbyte || value is byte || value is short || value is ushort || value is ulong)
				{
					builder.Append(value);
				}
				else if (value is double || value is decimal)
				{
					builder.Append(Convert.ToDouble(value).ToString("R", CultureInfo.InvariantCulture));
				}
				else
				{
					SerializeString(value.ToString());
				}
			}
		}

		private const string INDENT_STRING = "    ";

		public static object Deserialize(string json)
		{
			try
			{
				if (json == null)
				{
					return null;
				}
				return Parser.Parse(json);
			}
			catch (OverflowException)
			{
				return null;
			}
		}

		public static string Serialize(object obj)
		{
			return Serializer.Serialize(obj);
		}

		public static string FormatJson(string str)
		{
			int num = 0;
			bool flag = false;
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < str.Length; i++)
			{
				char c = str[i];
				switch (c)
				{
				case '[':
				case '{':
					sb.Append(c);
					if (!flag)
					{
						sb.AppendLine();
						Enumerable.Range(0, ++num).ToList().ForEach(delegate
						{
							sb.Append("    ");
						});
					}
					break;
				case ']':
				case '}':
					if (!flag)
					{
						sb.AppendLine();
						Enumerable.Range(0, --num).ToList().ForEach(delegate
						{
							sb.Append("    ");
						});
					}
					sb.Append(c);
					break;
				case '"':
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
					break;
				}
				case ',':
					sb.Append(c);
					if (!flag)
					{
						sb.AppendLine();
						Enumerable.Range(0, num).ToList().ForEach(delegate
						{
							sb.Append("    ");
						});
					}
					break;
				case ':':
					sb.Append(c);
					if (!flag)
					{
						sb.Append(" ");
					}
					break;
				default:
					sb.Append(c);
					break;
				}
			}
			return sb.ToString();
		}
	}
}
