using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x02001054 RID: 4180
	public struct ScriptToken
	{
		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x0600B50F RID: 46351 RVA: 0x0037AD41 File Offset: 0x00378F41
		// (set) Token: 0x0600B50E RID: 46350 RVA: 0x0037AD38 File Offset: 0x00378F38
		public string Value { get; private set; }

		// Token: 0x0600B510 RID: 46352 RVA: 0x0037AD4C File Offset: 0x00378F4C
		public static List<ScriptToken> Tokenize(string str)
		{
			List<ScriptToken> list = new List<ScriptToken>();
			bool flag = false;
			for (int i = 0; i < str.Length; i++)
			{
				char c = str[i];
				string methodSymbol;
				ScriptToken item2;
				if (ScriptToken.IsWhiteSpace(str, i))
				{
					ScriptToken item = ScriptToken.CreateToken(ScriptToken.TokenType.WhiteSpace, str, ref i, new Func<string, int, bool>(ScriptToken.IsWhiteSpace));
					if (flag)
					{
						list.Add(item);
					}
				}
				else if (flag && ScriptToken.PeekNext(str, i, "\\") && i + 1 < str.Length)
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.Literal, ref i, 2));
				}
				else if (ScriptToken.PeekNext(str, i, "has"))
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.Has, ref i, 3));
				}
				else if (ScriptToken.PeekNext(str, i, "and"))
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.And, ref i, 3));
				}
				else if (ScriptToken.PeekNext(str, i, "or"))
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.Or, ref i, 2));
				}
				else if (ScriptToken.PeekNext(str, i, "not"))
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.NotEqual, ref i, 3));
				}
				else if (ScriptToken.PeekNext(str, i, "<="))
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.LessEqual, ref i, 2));
				}
				else if (ScriptToken.PeekNext(str, i, ">="))
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.GreaterEqual, ref i, 2));
				}
				else if (ScriptToken.PeekNext(str, i, "is"))
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.Equal, ref i, 2));
				}
				else if (c == '>')
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.Greater, ref i, 1));
				}
				else if (c == '<')
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.Less, ref i, 1));
				}
				else if (c == '+')
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.Plus, ref i, 1));
				}
				else if (c == '*')
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.Star, ref i, 1));
				}
				else if (c == '/')
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.ForwardSlash, ref i, 1));
				}
				else if (c == '%')
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.Percent, ref i, 1));
				}
				else if (c == '^')
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.Caret, ref i, 1));
				}
				else if (ScriptToken.IsMethodSignature(str, i, out methodSymbol) && ScriptToken.TryCreateMethodToken(methodSymbol, ref i, out item2))
				{
					list.Add(item2);
				}
				else if (c == '$' || char.IsLetter(c) || c == '_')
				{
					list.Add(ScriptToken.CreateToken(ScriptToken.TokenType.Literal, str, ref i, new Func<string, int, bool>(ScriptToken.IsLiteral)));
				}
				else if (char.IsDigit(c) || (c == '-' && char.IsDigit(ScriptToken.PeekNext(str, i))))
				{
					list.Add(ScriptToken.CreateToken(ScriptToken.TokenType.Numerical, str, ref i, new Func<string, int, bool>(ScriptToken.IsDigit)));
				}
				else if (c == '-')
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.Minus, ref i, 1));
				}
				else if (c == '.')
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.Period, ref i, 1));
				}
				else if (c == ':')
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.Colon, ref i, 1));
				}
				else if (c == ',')
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.Comma, ref i, 1));
				}
				else if (c == '(')
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.OpenRoundBrackets, ref i, 1));
				}
				else if (c == ')')
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.ClosedRoundBrackets, ref i, 1));
				}
				else if (c == '[')
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.OpenSquareBrackets, ref i, 1));
				}
				else if (c == ']')
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.ClosedSquareBrackets, ref i, 1));
				}
				else if (c == '"')
				{
					flag = !flag;
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.DoubleQuote, ref i, 1));
				}
				else
				{
					list.Add(ScriptToken.CreateToken(str, ScriptToken.TokenType.Invalid, ref i, 1));
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				ScriptToken value = list[j];
				value.Index = j;
				list[j] = value;
			}
			return list;
		}

		// Token: 0x0600B511 RID: 46353 RVA: 0x0037B158 File Offset: 0x00379358
		private static ScriptToken CreateToken(ScriptToken.TokenType tokenType, string str, ref int index, Func<string, int, bool> predicate)
		{
			ScriptToken scriptToken = default(ScriptToken);
			scriptToken.Type = tokenType;
			scriptToken.StartIndex = index;
			int num = index + 1;
			while (num < str.Length && predicate(str, num))
			{
				num++;
			}
			scriptToken.EndIndex = num;
			scriptToken.Value = str.Substring(scriptToken.StartIndex, scriptToken.EndIndex - scriptToken.StartIndex);
			index = num - 1;
			return scriptToken;
		}

		// Token: 0x0600B512 RID: 46354 RVA: 0x0037B1CC File Offset: 0x003793CC
		private static ScriptToken CreateToken(string str, ScriptToken.TokenType tokenType, ref int index, int size)
		{
			ScriptToken result = new ScriptToken
			{
				Type = tokenType,
				StartIndex = index,
				EndIndex = index + size,
				Value = str.Substring(index, size)
			};
			index += size - 1;
			return result;
		}

		// Token: 0x0600B513 RID: 46355 RVA: 0x0037B218 File Offset: 0x00379418
		private static bool TryCreateMethodToken(string methodSymbol, ref int index, out ScriptToken token)
		{
			token = default(ScriptToken);
			MethodScriptSyntaxTreeRule.Evaluator evaluator;
			if (MethodScriptSyntaxTreeRule.TryGetMethodEvaluator(methodSymbol, out evaluator))
			{
				token.Type = ScriptToken.TokenType.Method;
				token.StartIndex = index;
				token.EndIndex = index + methodSymbol.Length;
				token.Value = methodSymbol;
				index += methodSymbol.Length - 1;
				return true;
			}
			return false;
		}

		// Token: 0x0600B514 RID: 46356 RVA: 0x0037B26C File Offset: 0x0037946C
		private static bool IsLiteral(string str, int index)
		{
			char c = str[index];
			return char.IsLetterOrDigit(c) || c == '_';
		}

		// Token: 0x0600B515 RID: 46357 RVA: 0x0037B290 File Offset: 0x00379490
		private static bool IsDigit(string str, int index)
		{
			char c = str[index];
			return char.IsDigit(c) || (c == '.' && char.IsDigit(ScriptToken.PeekNext(str, index)));
		}

		// Token: 0x0600B516 RID: 46358 RVA: 0x0037B2C2 File Offset: 0x003794C2
		private static bool IsWhiteSpace(string str, int index)
		{
			return char.IsWhiteSpace(str[index]);
		}

		// Token: 0x0600B517 RID: 46359 RVA: 0x0037B2D0 File Offset: 0x003794D0
		private static bool IsMethodSignature(string str, int index, out string methodSymbol)
		{
			methodSymbol = null;
			if (!char.IsLetter(str[index]))
			{
				return false;
			}
			bool flag = false;
			int i = index + 1;
			int num = 1;
			while (i < str.Length)
			{
				if (!char.IsLetterOrDigit(str[i]))
				{
					if (str[i] == '(')
					{
						flag = true;
						break;
					}
					break;
				}
				else
				{
					i++;
					num++;
				}
			}
			if (!flag)
			{
				return false;
			}
			methodSymbol = str.Substring(index, num);
			return true;
		}

		// Token: 0x0600B518 RID: 46360 RVA: 0x0037B338 File Offset: 0x00379538
		private static bool PeekNext(string str, int index, string search)
		{
			int num = 0;
			int num2 = index;
			while (num2 < str.Length && num < search.Length)
			{
				if (str[num2] != search[num])
				{
					return false;
				}
				num2++;
				num++;
			}
			bool flag = num == search.Length;
			if (num2 == str.Length)
			{
				return flag;
			}
			if (char.IsLetterOrDigit(search[search.Length - 1]))
			{
				return flag && char.IsWhiteSpace(str[num2]);
			}
			return flag;
		}

		// Token: 0x0600B519 RID: 46361 RVA: 0x0037B3B4 File Offset: 0x003795B4
		private static char PeekNext(string str, int index)
		{
			if (index >= str.Length - 1)
			{
				return '\0';
			}
			return str[index + 1];
		}

		// Token: 0x0600B51A RID: 46362 RVA: 0x0037B3CC File Offset: 0x003795CC
		public static string TokenTypeToHumanReadableString(ScriptToken.TokenType tokenType)
		{
			switch (tokenType)
			{
			case ScriptToken.TokenType.WhiteSpace:
				return "<whitespace>";
			case ScriptToken.TokenType.Literal:
				return "<name>";
			case ScriptToken.TokenType.Numerical:
				return "<number>";
			case ScriptToken.TokenType.Period:
				return ".";
			case ScriptToken.TokenType.Has:
				return "has";
			case ScriptToken.TokenType.Equal:
				return "is";
			case ScriptToken.TokenType.NotEqual:
				return "not";
			case ScriptToken.TokenType.Less:
				return "<";
			case ScriptToken.TokenType.LessEqual:
				return "<=";
			case ScriptToken.TokenType.Greater:
				return ">";
			case ScriptToken.TokenType.GreaterEqual:
				return ">=";
			case ScriptToken.TokenType.Or:
				return "or";
			case ScriptToken.TokenType.And:
				return "and";
			case ScriptToken.TokenType.Colon:
				return ":";
			case ScriptToken.TokenType.OpenRoundBrackets:
				return "(";
			case ScriptToken.TokenType.ClosedRoundBrackets:
				return ")";
			case ScriptToken.TokenType.OpenSquareBrackets:
				return "[";
			case ScriptToken.TokenType.ClosedSquareBrackets:
				return "]";
			case ScriptToken.TokenType.DoubleQuote:
				return "\"";
			case ScriptToken.TokenType.Comma:
				return ",";
			case ScriptToken.TokenType.Method:
				return "<method>";
			}
			return "unknown";
		}

		// Token: 0x04009717 RID: 38679
		public ScriptToken.TokenType Type;

		// Token: 0x04009718 RID: 38680
		public int StartIndex;

		// Token: 0x04009719 RID: 38681
		public int EndIndex;

		// Token: 0x0400971B RID: 38683
		public int Index;

		// Token: 0x02002864 RID: 10340
		public enum TokenType
		{
			// Token: 0x0400F975 RID: 63861
			Invalid,
			// Token: 0x0400F976 RID: 63862
			WhiteSpace,
			// Token: 0x0400F977 RID: 63863
			Literal,
			// Token: 0x0400F978 RID: 63864
			Numerical,
			// Token: 0x0400F979 RID: 63865
			Period,
			// Token: 0x0400F97A RID: 63866
			Has,
			// Token: 0x0400F97B RID: 63867
			Equal,
			// Token: 0x0400F97C RID: 63868
			NotEqual,
			// Token: 0x0400F97D RID: 63869
			Less,
			// Token: 0x0400F97E RID: 63870
			LessEqual,
			// Token: 0x0400F97F RID: 63871
			Greater,
			// Token: 0x0400F980 RID: 63872
			GreaterEqual,
			// Token: 0x0400F981 RID: 63873
			Plus,
			// Token: 0x0400F982 RID: 63874
			Minus,
			// Token: 0x0400F983 RID: 63875
			Star,
			// Token: 0x0400F984 RID: 63876
			ForwardSlash,
			// Token: 0x0400F985 RID: 63877
			Percent,
			// Token: 0x0400F986 RID: 63878
			Caret,
			// Token: 0x0400F987 RID: 63879
			Or,
			// Token: 0x0400F988 RID: 63880
			And,
			// Token: 0x0400F989 RID: 63881
			Colon,
			// Token: 0x0400F98A RID: 63882
			OpenRoundBrackets,
			// Token: 0x0400F98B RID: 63883
			ClosedRoundBrackets,
			// Token: 0x0400F98C RID: 63884
			OpenSquareBrackets,
			// Token: 0x0400F98D RID: 63885
			ClosedSquareBrackets,
			// Token: 0x0400F98E RID: 63886
			DoubleQuote,
			// Token: 0x0400F98F RID: 63887
			Comma,
			// Token: 0x0400F990 RID: 63888
			Method
		}
	}
}
