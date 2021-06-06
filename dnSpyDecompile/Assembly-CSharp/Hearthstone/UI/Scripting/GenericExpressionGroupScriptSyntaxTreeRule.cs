using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x02001041 RID: 4161
	public abstract class GenericExpressionGroupScriptSyntaxTreeRule<T> : ScriptSyntaxTreeRule<T> where T : ScriptSyntaxTreeRule, new()
	{
		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x0600B4B0 RID: 46256 RVA: 0x00378F11 File Offset: 0x00377111
		protected override HashSet<ScriptToken.TokenType> TokensInternal
		{
			get
			{
				return new HashSet<ScriptToken.TokenType>
				{
					this.m_startToken
				};
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x0600B4B1 RID: 46257 RVA: 0x00378F25 File Offset: 0x00377125
		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal
		{
			get
			{
				return new ScriptSyntaxTreeRule[]
				{
					ScriptSyntaxTreeRule<ConditionalScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<IndexerScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<AccessorScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<RelationalScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<TupleScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<ArithmeticScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<EmptyScriptSyntaxTreeRule>.Get()
				};
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x0600B4B2 RID: 46258 RVA: 0x00378F65 File Offset: 0x00377165
		protected override IEnumerable<ScriptSyntaxTreeRule> NestedRulesInternal
		{
			get
			{
				return new ScriptSyntaxTreeRule[]
				{
					ScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<StringScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<NumberScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<MethodScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<EmptyScriptSyntaxTreeRule>.Get()
				};
			}
		}

		// Token: 0x0600B4B3 RID: 46259 RVA: 0x00378F9D File Offset: 0x0037719D
		public GenericExpressionGroupScriptSyntaxTreeRule()
		{
		}

		// Token: 0x0600B4B4 RID: 46260 RVA: 0x00378FB5 File Offset: 0x003771B5
		protected GenericExpressionGroupScriptSyntaxTreeRule(ScriptToken.TokenType startToken, ScriptToken.TokenType endToken)
		{
			this.m_startToken = startToken;
			this.m_endToken = endToken;
		}

		// Token: 0x0600B4B5 RID: 46261 RVA: 0x00378FDC File Offset: 0x003771DC
		public override ScriptSyntaxTreeRule.ParseResult Parse(ScriptToken[] tokens, ref int tokenIndex, out string parseErrorMessage, out ScriptSyntaxTreeNode node)
		{
			node = null;
			int num = 0;
			int num2 = tokenIndex;
			while (num2 < tokens.Length && (num > 0 || num2 == tokenIndex))
			{
				if (tokens[num2].Type == this.m_startToken)
				{
					num++;
				}
				else if (tokens[num2].Type == this.m_endToken)
				{
					num--;
				}
				num2++;
			}
			if (num == 0)
			{
				tokenIndex = num2 - 1;
				node = new ScriptSyntaxTreeNode(this);
				parseErrorMessage = null;
				return ScriptSyntaxTreeRule.ParseResult.Success;
			}
			parseErrorMessage = "Unbalanced brackets!";
			return ScriptSyntaxTreeRule.ParseResult.Failed;
		}

		// Token: 0x0600B4B6 RID: 46262 RVA: 0x00379057 File Offset: 0x00377257
		public override bool ParseStepInto(ScriptToken[] tokens, ref int startToken, ref int endToken)
		{
			startToken++;
			endToken--;
			return true;
		}

		// Token: 0x040096EF RID: 38639
		private ScriptToken.TokenType m_startToken = ScriptToken.TokenType.OpenRoundBrackets;

		// Token: 0x040096F0 RID: 38640
		private ScriptToken.TokenType m_endToken = ScriptToken.TokenType.ClosedRoundBrackets;
	}
}
