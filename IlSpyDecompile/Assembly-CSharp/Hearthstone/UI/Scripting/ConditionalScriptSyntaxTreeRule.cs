using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	public class ConditionalScriptSyntaxTreeRule : ScriptSyntaxTreeRule<ConditionalScriptSyntaxTreeRule>
	{
		protected override HashSet<ScriptToken.TokenType> TokensInternal => new HashSet<ScriptToken.TokenType>
		{
			ScriptToken.TokenType.Or,
			ScriptToken.TokenType.And
		};

		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal => new ScriptSyntaxTreeRule[5]
		{
			ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<NumberScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<StringScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<MethodScriptSyntaxTreeRule>.Get()
		};

		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext evaluationContext, out object value)
		{
			value = null;
			if (!evaluationContext.CheckFeatureIsSupported(ScriptFeatureFlags.Conditionals))
			{
				return;
			}
			if (node.Left == null || !node.Left.Evaluate(evaluationContext, out var value2))
			{
				evaluationContext.Results.SetFailedNodeIfNoneExists(node, node.Left);
				return;
			}
			_ = evaluationContext.EncodingPolicy;
			if (node.Right == null)
			{
				evaluationContext.EmitError(ScriptContext.ErrorCodes.EvaluationError, "");
				return;
			}
			if (!node.Right.Evaluate(evaluationContext, out var value3))
			{
				evaluationContext.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			bool flag = ((value3 is bool) ? ((bool)value3) : (value3 != null));
			bool flag2 = ((value2 is bool) ? ((bool)value2) : (value2 != null));
			node.ValueType = typeof(bool);
			switch (node.Token.Type)
			{
			case ScriptToken.TokenType.And:
				value = (node.Value = flag && flag2);
				break;
			case ScriptToken.TokenType.Or:
				value = (node.Value = flag || flag2);
				break;
			default:
				value = (node.Value = false);
				break;
			}
		}
	}
}
