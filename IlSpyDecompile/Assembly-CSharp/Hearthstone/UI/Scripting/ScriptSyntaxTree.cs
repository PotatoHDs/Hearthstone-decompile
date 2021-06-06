using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Hearthstone.UI.Scripting
{
	public class ScriptSyntaxTree
	{
		public struct ParseResults
		{
			public enum ErrorCodes
			{
				Success,
				Error,
				UnexpectedToken,
				MissingToken
			}

			public ScriptToken[] Tokens;

			public ErrorCodes ErrorCode;

			public string ErrorMessage;

			public ScriptToken LastParsedToken;

			public ScriptToken FailedToken;

			public ScriptSyntaxTreeRule LastSuccessfulRule;

			public ScriptSyntaxTreeRule FailedRule;
		}

		private static Dictionary<string, ScriptSyntaxTree> s_syntaxTreeCache = new Dictionary<string, ScriptSyntaxTree>();

		private ParseResults m_results;

		public ScriptSyntaxTreeNode Root { get; private set; }

		public ParseResults Results => m_results;

		public static ScriptSyntaxTree Get(string script)
		{
			if (!s_syntaxTreeCache.TryGetValue(script, out var value))
			{
				value = new ScriptSyntaxTree();
				value.Parse(script);
				s_syntaxTreeCache[script] = value;
			}
			return value;
		}

		private ScriptSyntaxTree()
		{
		}

		private void Parse(string script)
		{
			m_results = default(ParseResults);
			m_results.ErrorCode = ParseResults.ErrorCodes.Success;
			m_results.Tokens = ScriptToken.Tokenize(script).ToArray();
			Root = new ScriptSyntaxTreeNode(ScriptSyntaxTreeRule<RootScriptSyntaxTreeRule>.Get())
			{
				Tokens = m_results.Tokens
			};
			ParseRecursive(m_results.Tokens, ScriptSyntaxTreeRule<RootScriptSyntaxTreeRule>.Get().ExpectedRules, Root);
		}

		private bool ParseRecursive(ScriptToken[] tokens, IEnumerable<ScriptSyntaxTreeRule> expectedRules, ScriptSyntaxTreeNode rootNode)
		{
			ScriptSyntaxTreeNode left = rootNode;
			int num = 0;
			ScriptSyntaxTreeRule scriptSyntaxTreeRule = null;
			while (num < tokens.Length && expectedRules != null)
			{
				ScriptToken scriptToken = tokens[num];
				ScriptSyntaxTreeRule scriptSyntaxTreeRule2 = null;
				foreach (ScriptSyntaxTreeRule expectedRule in expectedRules)
				{
					if (expectedRule.Tokens.Contains(scriptToken.Type))
					{
						scriptSyntaxTreeRule2 = expectedRule;
						break;
					}
				}
				if (scriptSyntaxTreeRule2 == null)
				{
					m_results.ErrorCode = ParseResults.ErrorCodes.UnexpectedToken;
					m_results.FailedToken = scriptToken;
					m_results.FailedRule = scriptSyntaxTreeRule;
					m_results.ErrorMessage = FormatGenericErrorMessage(expectedRules);
					return false;
				}
				scriptSyntaxTreeRule = scriptSyntaxTreeRule2;
				int startToken = num;
				int tokenIndex = startToken;
				string parseErrorMessage;
				ScriptSyntaxTreeNode node;
				ScriptSyntaxTreeRule.ParseResult num2 = scriptSyntaxTreeRule.Parse(tokens, ref tokenIndex, out parseErrorMessage, out node);
				num = tokenIndex + 1;
				if (num2 == ScriptSyntaxTreeRule.ParseResult.Failed)
				{
					m_results.ErrorCode = ParseResults.ErrorCodes.Error;
					m_results.FailedToken = scriptToken;
					m_results.FailedRule = scriptSyntaxTreeRule;
					m_results.ErrorMessage = parseErrorMessage;
					return false;
				}
				int num3 = num - startToken;
				bool flag = false;
				if (num3 > 0)
				{
					int endToken = num;
					flag = scriptSyntaxTreeRule.ParseStepInto(tokens, ref startToken, ref endToken);
					node.Tokens = new ScriptToken[endToken - startToken];
					for (int i = startToken; i < endToken; i++)
					{
						node.Tokens[i - startToken] = tokens[i];
					}
				}
				PushNodeToSyntaxTree(ref left, node, rootNode);
				m_results.LastParsedToken = scriptToken;
				m_results.LastSuccessfulRule = scriptSyntaxTreeRule;
				if (flag && !ParseRecursive(node.Tokens, scriptSyntaxTreeRule2.NestedRules, node))
				{
					return false;
				}
				expectedRules = scriptSyntaxTreeRule2.ExpectedRules;
			}
			return true;
		}

		private static string FormatGenericErrorMessage(IEnumerable<ScriptSyntaxTreeRule> expectedRules)
		{
			if (expectedRules == null)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Expected tokens: ");
			foreach (ScriptSyntaxTreeRule expectedRule in expectedRules)
			{
				foreach (ScriptToken.TokenType token in expectedRule.Tokens)
				{
					stringBuilder.Append(" '");
					stringBuilder.Append(ScriptToken.TokenTypeToHumanReadableString(token));
					stringBuilder.Append('\'');
				}
			}
			return stringBuilder.ToString();
		}

		private void PrintTree(ScriptSyntaxTreeNode scriptSyntaxTreeNode, string depth)
		{
			if (scriptSyntaxTreeNode != null)
			{
				Debug.Log(depth + scriptSyntaxTreeNode.Token.Value);
				PrintTree(scriptSyntaxTreeNode.Left, depth + "   ");
				PrintTree(scriptSyntaxTreeNode.Right, depth + "   ");
			}
		}

		private void PushNodeToSyntaxTree(ref ScriptSyntaxTreeNode left, ScriptSyntaxTreeNode newNode, ScriptSyntaxTreeNode rootNode)
		{
			while (left != rootNode && newNode.Priority >= left.Priority)
			{
				left = left.Parent;
			}
			newNode.Parent = left;
			newNode.Left = ((left != null) ? left.Right : null);
			if (left != null)
			{
				if (left.Right != null)
				{
					left.Right.Parent = newNode;
				}
				left.Right = newNode;
			}
			left = newNode;
		}
	}
}
