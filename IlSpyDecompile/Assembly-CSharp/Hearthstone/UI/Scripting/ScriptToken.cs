using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	public struct ScriptToken
	{
		public enum TokenType
		{
			Invalid,
			WhiteSpace,
			Literal,
			Numerical,
			Period,
			Has,
			Equal,
			NotEqual,
			Less,
			LessEqual,
			Greater,
			GreaterEqual,
			Plus,
			Minus,
			Star,
			ForwardSlash,
			Percent,
			Caret,
			Or,
			And,
			Colon,
			OpenRoundBrackets,
			ClosedRoundBrackets,
			OpenSquareBrackets,
			ClosedSquareBrackets,
			DoubleQuote,
			Comma,
			Method
		}

		public TokenType Type;

		public int StartIndex;

		public int EndIndex;

		public int Index;

		public string Value { get; private set; }

		public static List<ScriptToken> Tokenize(string str)
		{
			List<ScriptToken> list = new List<ScriptToken>();
			bool flag = false;
			for (int i = 0; i < str.Length; i++)
			{
				char c = str[i];
				if (IsWhiteSpace(str, i))
				{
					ScriptToken item = CreateToken(TokenType.WhiteSpace, str, ref i, IsWhiteSpace);
					if (flag)
					{
						list.Add(item);
					}
					continue;
				}
				if (flag && PeekNext(str, i, "\\") && i + 1 < str.Length)
				{
					list.Add(CreateToken(str, TokenType.Literal, ref i, 2));
					continue;
				}
				if (PeekNext(str, i, "has"))
				{
					list.Add(CreateToken(str, TokenType.Has, ref i, 3));
					continue;
				}
				if (PeekNext(str, i, "and"))
				{
					list.Add(CreateToken(str, TokenType.And, ref i, 3));
					continue;
				}
				if (PeekNext(str, i, "or"))
				{
					list.Add(CreateToken(str, TokenType.Or, ref i, 2));
					continue;
				}
				if (PeekNext(str, i, "not"))
				{
					list.Add(CreateToken(str, TokenType.NotEqual, ref i, 3));
					continue;
				}
				if (PeekNext(str, i, "<="))
				{
					list.Add(CreateToken(str, TokenType.LessEqual, ref i, 2));
					continue;
				}
				if (PeekNext(str, i, ">="))
				{
					list.Add(CreateToken(str, TokenType.GreaterEqual, ref i, 2));
					continue;
				}
				if (PeekNext(str, i, "is"))
				{
					list.Add(CreateToken(str, TokenType.Equal, ref i, 2));
					continue;
				}
				switch (c)
				{
				case '>':
					list.Add(CreateToken(str, TokenType.Greater, ref i, 1));
					continue;
				case '<':
					list.Add(CreateToken(str, TokenType.Less, ref i, 1));
					continue;
				case '+':
					list.Add(CreateToken(str, TokenType.Plus, ref i, 1));
					continue;
				case '*':
					list.Add(CreateToken(str, TokenType.Star, ref i, 1));
					continue;
				case '/':
					list.Add(CreateToken(str, TokenType.ForwardSlash, ref i, 1));
					continue;
				case '%':
					list.Add(CreateToken(str, TokenType.Percent, ref i, 1));
					continue;
				case '^':
					list.Add(CreateToken(str, TokenType.Caret, ref i, 1));
					continue;
				}
				if (IsMethodSignature(str, i, out var methodSymbol) && TryCreateMethodToken(methodSymbol, ref i, out var token))
				{
					list.Add(token);
					continue;
				}
				if (c == '$' || char.IsLetter(c) || c == '_')
				{
					list.Add(CreateToken(TokenType.Literal, str, ref i, IsLiteral));
					continue;
				}
				if (char.IsDigit(c) || (c == '-' && char.IsDigit(PeekNext(str, i))))
				{
					list.Add(CreateToken(TokenType.Numerical, str, ref i, IsDigit));
					continue;
				}
				switch (c)
				{
				case '-':
					list.Add(CreateToken(str, TokenType.Minus, ref i, 1));
					break;
				case '.':
					list.Add(CreateToken(str, TokenType.Period, ref i, 1));
					break;
				case ':':
					list.Add(CreateToken(str, TokenType.Colon, ref i, 1));
					break;
				case ',':
					list.Add(CreateToken(str, TokenType.Comma, ref i, 1));
					break;
				case '(':
					list.Add(CreateToken(str, TokenType.OpenRoundBrackets, ref i, 1));
					break;
				case ')':
					list.Add(CreateToken(str, TokenType.ClosedRoundBrackets, ref i, 1));
					break;
				case '[':
					list.Add(CreateToken(str, TokenType.OpenSquareBrackets, ref i, 1));
					break;
				case ']':
					list.Add(CreateToken(str, TokenType.ClosedSquareBrackets, ref i, 1));
					break;
				case '"':
					flag = !flag;
					list.Add(CreateToken(str, TokenType.DoubleQuote, ref i, 1));
					break;
				default:
					list.Add(CreateToken(str, TokenType.Invalid, ref i, 1));
					break;
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

		private static ScriptToken CreateToken(TokenType tokenType, string str, ref int index, Func<string, int, bool> predicate)
		{
			ScriptToken result = default(ScriptToken);
			result.Type = tokenType;
			result.StartIndex = index;
			int i;
			for (i = index + 1; i < str.Length && predicate(str, i); i++)
			{
			}
			result.EndIndex = i;
			result.Value = str.Substring(result.StartIndex, result.EndIndex - result.StartIndex);
			index = i - 1;
			return result;
		}

		private static ScriptToken CreateToken(string str, TokenType tokenType, ref int index, int size)
		{
			ScriptToken scriptToken = default(ScriptToken);
			scriptToken.Type = tokenType;
			scriptToken.StartIndex = index;
			scriptToken.EndIndex = index + size;
			scriptToken.Value = str.Substring(index, size);
			ScriptToken result = scriptToken;
			index += size - 1;
			return result;
		}

		private static bool TryCreateMethodToken(string methodSymbol, ref int index, out ScriptToken token)
		{
			token = default(ScriptToken);
			if (MethodScriptSyntaxTreeRule.TryGetMethodEvaluator(methodSymbol, out var _))
			{
				token.Type = TokenType.Method;
				token.StartIndex = index;
				token.EndIndex = index + methodSymbol.Length;
				token.Value = methodSymbol;
				index += methodSymbol.Length - 1;
				return true;
			}
			return false;
		}

		private static bool IsLiteral(string str, int index)
		{
			char c = str[index];
			if (!char.IsLetterOrDigit(c))
			{
				return c == '_';
			}
			return true;
		}

		private static bool IsDigit(string str, int index)
		{
			char c = str[index];
			if (!char.IsDigit(c))
			{
				if (c == '.')
				{
					return char.IsDigit(PeekNext(str, index));
				}
				return false;
			}
			return true;
		}

		private static bool IsWhiteSpace(string str, int index)
		{
			return char.IsWhiteSpace(str[index]);
		}

		private static bool IsMethodSignature(string str, int index, out string methodSymbol)
		{
			methodSymbol = null;
			if (!char.IsLetter(str[index]))
			{
				return false;
			}
			bool flag = false;
			int num = index + 1;
			int num2 = 1;
			while (num < str.Length)
			{
				if (!char.IsLetterOrDigit(str[num]))
				{
					if (str[num] == '(')
					{
						flag = true;
					}
					break;
				}
				num++;
				num2++;
			}
			if (!flag)
			{
				return false;
			}
			methodSymbol = str.Substring(index, num2);
			return true;
		}

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
				if (flag)
				{
					return char.IsWhiteSpace(str[num2]);
				}
				return false;
			}
			return flag;
		}

		private static char PeekNext(string str, int index)
		{
			if (index >= str.Length - 1)
			{
				return '\0';
			}
			return str[index + 1];
		}

		public static string TokenTypeToHumanReadableString(TokenType tokenType)
		{
			return tokenType switch
			{
				TokenType.WhiteSpace => "<whitespace>", 
				TokenType.Literal => "<name>", 
				TokenType.Numerical => "<number>", 
				TokenType.Equal => "is", 
				TokenType.NotEqual => "not", 
				TokenType.Less => "<", 
				TokenType.LessEqual => "<=", 
				TokenType.Greater => ">", 
				TokenType.GreaterEqual => ">=", 
				TokenType.Or => "or", 
				TokenType.And => "and", 
				TokenType.Period => ".", 
				TokenType.Colon => ":", 
				TokenType.OpenRoundBrackets => "(", 
				TokenType.ClosedRoundBrackets => ")", 
				TokenType.OpenSquareBrackets => "[", 
				TokenType.ClosedSquareBrackets => "]", 
				TokenType.DoubleQuote => "\"", 
				TokenType.Comma => ",", 
				TokenType.Has => "has", 
				TokenType.Method => "<method>", 
				_ => "unknown", 
			};
		}
	}
}
