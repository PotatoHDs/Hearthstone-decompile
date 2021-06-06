using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	public class EmptyScriptSyntaxTreeRule : ScriptSyntaxTreeRule<EmptyScriptSyntaxTreeRule>
	{
		protected override HashSet<ScriptToken.TokenType> TokensInternal => new HashSet<ScriptToken.TokenType>();

		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal => new ScriptSyntaxTreeRule[0];

		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext evaluationContext, out object value)
		{
			value = null;
		}
	}
}
