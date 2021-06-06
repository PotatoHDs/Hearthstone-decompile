using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	public abstract class GenericExpressionGroupScriptSyntaxTreeRule<T> : ScriptSyntaxTreeRule<T> where T : ScriptSyntaxTreeRule, new()
	{
		private ScriptToken.TokenType m_startToken = ScriptToken.TokenType.OpenRoundBrackets;

		private ScriptToken.TokenType m_endToken = ScriptToken.TokenType.ClosedRoundBrackets;

		protected override HashSet<ScriptToken.TokenType> TokensInternal => new HashSet<ScriptToken.TokenType> { m_startToken };

		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal => new ScriptSyntaxTreeRule[7]
		{
			ScriptSyntaxTreeRule<ConditionalScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<IndexerScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<AccessorScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<RelationalScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<TupleScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<ArithmeticScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<EmptyScriptSyntaxTreeRule>.Get()
		};

		protected override IEnumerable<ScriptSyntaxTreeRule> NestedRulesInternal => new ScriptSyntaxTreeRule[6]
		{
			ScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<StringScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<NumberScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<MethodScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<EmptyScriptSyntaxTreeRule>.Get()
		};

		public GenericExpressionGroupScriptSyntaxTreeRule()
		{
		}

		protected GenericExpressionGroupScriptSyntaxTreeRule(ScriptToken.TokenType startToken, ScriptToken.TokenType endToken)
		{
			m_startToken = startToken;
			m_endToken = endToken;
		}

		public override ParseResult Parse(ScriptToken[] tokens, ref int tokenIndex, out string parseErrorMessage, out ScriptSyntaxTreeNode node)
		{
			node = null;
			int num = 0;
			int i;
			for (i = tokenIndex; i < tokens.Length && (num > 0 || i == tokenIndex); i++)
			{
				if (tokens[i].Type == m_startToken)
				{
					num++;
				}
				else if (tokens[i].Type == m_endToken)
				{
					num--;
				}
			}
			if (num == 0)
			{
				tokenIndex = i - 1;
				node = new ScriptSyntaxTreeNode(this);
				parseErrorMessage = null;
				return ParseResult.Success;
			}
			parseErrorMessage = "Unbalanced brackets!";
			return ParseResult.Failed;
		}

		public override bool ParseStepInto(ScriptToken[] tokens, ref int startToken, ref int endToken)
		{
			startToken++;
			endToken--;
			return true;
		}
	}
}
