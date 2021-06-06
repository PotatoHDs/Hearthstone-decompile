using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x02001052 RID: 4178
	public class ScriptSyntaxTree
	{
		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x0600B4FF RID: 46335 RVA: 0x0037A76D File Offset: 0x0037896D
		// (set) Token: 0x0600B4FE RID: 46334 RVA: 0x0037A764 File Offset: 0x00378964
		public ScriptSyntaxTreeNode Root { get; private set; }

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x0600B500 RID: 46336 RVA: 0x0037A775 File Offset: 0x00378975
		public ScriptSyntaxTree.ParseResults Results
		{
			get
			{
				return this.m_results;
			}
		}

		// Token: 0x0600B501 RID: 46337 RVA: 0x0037A780 File Offset: 0x00378980
		public static ScriptSyntaxTree Get(string script)
		{
			ScriptSyntaxTree scriptSyntaxTree;
			if (!ScriptSyntaxTree.s_syntaxTreeCache.TryGetValue(script, out scriptSyntaxTree))
			{
				scriptSyntaxTree = new ScriptSyntaxTree();
				scriptSyntaxTree.Parse(script);
				ScriptSyntaxTree.s_syntaxTreeCache[script] = scriptSyntaxTree;
			}
			return scriptSyntaxTree;
		}

		// Token: 0x0600B502 RID: 46338 RVA: 0x000052CE File Offset: 0x000034CE
		private ScriptSyntaxTree()
		{
		}

		// Token: 0x0600B503 RID: 46339 RVA: 0x0037A7B8 File Offset: 0x003789B8
		private void Parse(string script)
		{
			this.m_results = default(ScriptSyntaxTree.ParseResults);
			this.m_results.ErrorCode = ScriptSyntaxTree.ParseResults.ErrorCodes.Success;
			this.m_results.Tokens = ScriptToken.Tokenize(script).ToArray();
			this.Root = new ScriptSyntaxTreeNode(ScriptSyntaxTreeRule<RootScriptSyntaxTreeRule>.Get())
			{
				Tokens = this.m_results.Tokens
			};
			this.ParseRecursive(this.m_results.Tokens, ScriptSyntaxTreeRule<RootScriptSyntaxTreeRule>.Get().ExpectedRules, this.Root);
		}

		// Token: 0x0600B504 RID: 46340 RVA: 0x0037A838 File Offset: 0x00378A38
		private bool ParseRecursive(ScriptToken[] tokens, IEnumerable<ScriptSyntaxTreeRule> expectedRules, ScriptSyntaxTreeNode rootNode)
		{
			ScriptSyntaxTreeNode scriptSyntaxTreeNode = rootNode;
			int num = 0;
			ScriptSyntaxTreeRule scriptSyntaxTreeRule = null;
			while (num < tokens.Length && expectedRules != null)
			{
				ScriptToken scriptToken = tokens[num];
				ScriptSyntaxTreeRule scriptSyntaxTreeRule2 = null;
				foreach (ScriptSyntaxTreeRule scriptSyntaxTreeRule3 in expectedRules)
				{
					if (scriptSyntaxTreeRule3.Tokens.Contains(scriptToken.Type))
					{
						scriptSyntaxTreeRule2 = scriptSyntaxTreeRule3;
						break;
					}
				}
				if (scriptSyntaxTreeRule2 == null)
				{
					this.m_results.ErrorCode = ScriptSyntaxTree.ParseResults.ErrorCodes.UnexpectedToken;
					this.m_results.FailedToken = scriptToken;
					this.m_results.FailedRule = scriptSyntaxTreeRule;
					this.m_results.ErrorMessage = ScriptSyntaxTree.FormatGenericErrorMessage(expectedRules);
					return false;
				}
				scriptSyntaxTreeRule = scriptSyntaxTreeRule2;
				int num2 = num;
				int num3 = num2;
				string errorMessage;
				ScriptSyntaxTreeNode scriptSyntaxTreeNode2;
				bool flag = scriptSyntaxTreeRule.Parse(tokens, ref num3, out errorMessage, out scriptSyntaxTreeNode2) != ScriptSyntaxTreeRule.ParseResult.Failed;
				num = num3 + 1;
				if (!flag)
				{
					this.m_results.ErrorCode = ScriptSyntaxTree.ParseResults.ErrorCodes.Error;
					this.m_results.FailedToken = scriptToken;
					this.m_results.FailedRule = scriptSyntaxTreeRule;
					this.m_results.ErrorMessage = errorMessage;
					return false;
				}
				int num4 = num - num2;
				bool flag2 = false;
				if (num4 > 0)
				{
					int num5 = num;
					flag2 = scriptSyntaxTreeRule.ParseStepInto(tokens, ref num2, ref num5);
					scriptSyntaxTreeNode2.Tokens = new ScriptToken[num5 - num2];
					for (int i = num2; i < num5; i++)
					{
						scriptSyntaxTreeNode2.Tokens[i - num2] = tokens[i];
					}
				}
				this.PushNodeToSyntaxTree(ref scriptSyntaxTreeNode, scriptSyntaxTreeNode2, rootNode);
				this.m_results.LastParsedToken = scriptToken;
				this.m_results.LastSuccessfulRule = scriptSyntaxTreeRule;
				if (flag2 && !this.ParseRecursive(scriptSyntaxTreeNode2.Tokens, scriptSyntaxTreeRule2.NestedRules, scriptSyntaxTreeNode2))
				{
					return false;
				}
				expectedRules = scriptSyntaxTreeRule2.ExpectedRules;
			}
			return true;
		}

		// Token: 0x0600B505 RID: 46341 RVA: 0x0037A9E8 File Offset: 0x00378BE8
		private static string FormatGenericErrorMessage(IEnumerable<ScriptSyntaxTreeRule> expectedRules)
		{
			if (expectedRules == null)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Expected tokens: ");
			foreach (ScriptSyntaxTreeRule scriptSyntaxTreeRule in expectedRules)
			{
				foreach (ScriptToken.TokenType tokenType in scriptSyntaxTreeRule.Tokens)
				{
					stringBuilder.Append(" '");
					stringBuilder.Append(ScriptToken.TokenTypeToHumanReadableString(tokenType));
					stringBuilder.Append('\'');
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600B506 RID: 46342 RVA: 0x0037AAA8 File Offset: 0x00378CA8
		private void PrintTree(ScriptSyntaxTreeNode scriptSyntaxTreeNode, string depth)
		{
			if (scriptSyntaxTreeNode == null)
			{
				return;
			}
			Debug.Log(depth + scriptSyntaxTreeNode.Token.Value);
			this.PrintTree(scriptSyntaxTreeNode.Left, depth + "   ");
			this.PrintTree(scriptSyntaxTreeNode.Right, depth + "   ");
		}

		// Token: 0x0600B507 RID: 46343 RVA: 0x0037AB00 File Offset: 0x00378D00
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

		// Token: 0x0400970A RID: 38666
		private static Dictionary<string, ScriptSyntaxTree> s_syntaxTreeCache = new Dictionary<string, ScriptSyntaxTree>();

		// Token: 0x0400970C RID: 38668
		private ScriptSyntaxTree.ParseResults m_results;

		// Token: 0x02002863 RID: 10339
		public struct ParseResults
		{
			// Token: 0x0400F96D RID: 63853
			public ScriptToken[] Tokens;

			// Token: 0x0400F96E RID: 63854
			public ScriptSyntaxTree.ParseResults.ErrorCodes ErrorCode;

			// Token: 0x0400F96F RID: 63855
			public string ErrorMessage;

			// Token: 0x0400F970 RID: 63856
			public ScriptToken LastParsedToken;

			// Token: 0x0400F971 RID: 63857
			public ScriptToken FailedToken;

			// Token: 0x0400F972 RID: 63858
			public ScriptSyntaxTreeRule LastSuccessfulRule;

			// Token: 0x0400F973 RID: 63859
			public ScriptSyntaxTreeRule FailedRule;

			// Token: 0x020029B0 RID: 10672
			public enum ErrorCodes
			{
				// Token: 0x0400FE18 RID: 65048
				Success,
				// Token: 0x0400FE19 RID: 65049
				Error,
				// Token: 0x0400FE1A RID: 65050
				UnexpectedToken,
				// Token: 0x0400FE1B RID: 65051
				MissingToken
			}
		}
	}
}
