using System.Collections;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	public class TupleScriptSyntaxTreeRule : ScriptSyntaxTreeRule<TupleScriptSyntaxTreeRule>
	{
		protected override HashSet<ScriptToken.TokenType> TokensInternal => new HashSet<ScriptToken.TokenType> { ScriptToken.TokenType.Comma };

		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal => new ScriptSyntaxTreeRule[5]
		{
			ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<NumberScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<StringScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<MethodScriptSyntaxTreeRule>.Get()
		};

		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object outValue)
		{
			outValue = null;
			if (!context.CheckFeatureIsSupported(ScriptFeatureFlags.Tuples))
			{
				return;
			}
			if (node.Left == null || !node.Left.Evaluate(context, out var value) || (value == null && (node.Left.ValueType == null || node.Left.ValueType.IsValueType)))
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Expected left value");
				context.Results.SetFailedNodeIfNoneExists(node, node.Left);
				return;
			}
			if (node.Right == null || !node.Right.Evaluate(context, out var value2) || (value2 == null && (node.Right.ValueType == null || node.Left.ValueType.IsValueType)))
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Expected right value");
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			ArrayList arrayList = new ArrayList();
			ICollection collection = value as ICollection;
			ICollection collection2 = value2 as ICollection;
			if (collection != null)
			{
				foreach (object item in collection)
				{
					arrayList.Add(item);
				}
			}
			else
			{
				arrayList.Add(new DynamicValue(value, node.Left.ValueType));
			}
			if (collection2 != null)
			{
				foreach (object item2 in collection2)
				{
					arrayList.Add(item2);
				}
			}
			else
			{
				arrayList.Add(new DynamicValue(value2, node.Right.ValueType));
			}
			outValue = arrayList;
		}
	}
}
