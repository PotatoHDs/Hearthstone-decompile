using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	public class RootScriptSyntaxTreeRule : ScriptSyntaxTreeRule<RootScriptSyntaxTreeRule>
	{
		protected override HashSet<ScriptToken.TokenType> TokensInternal => new HashSet<ScriptToken.TokenType>();

		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal => new ScriptSyntaxTreeRule[6]
		{
			ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<NumberScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<StringScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<EmptyScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<MethodScriptSyntaxTreeRule>.Get()
		};

		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext evaluationContext, out object value)
		{
			value = null;
			if (node.Right != null)
			{
				node.Right.Evaluate(evaluationContext, out value);
			}
		}
	}
}
