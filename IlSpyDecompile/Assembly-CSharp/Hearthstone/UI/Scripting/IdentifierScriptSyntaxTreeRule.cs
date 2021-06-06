using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	public class IdentifierScriptSyntaxTreeRule : ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>
	{
		protected override HashSet<ScriptToken.TokenType> TokensInternal => new HashSet<ScriptToken.TokenType> { ScriptToken.TokenType.Literal };

		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal => new ScriptSyntaxTreeRule[10]
		{
			ScriptSyntaxTreeRule<TupleScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<AccessorScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<ContainsScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<ConditionalScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<RelationalScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<ArithmeticScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<IndexerScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<EventScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<EmptyScriptSyntaxTreeRule>.Get()
		};

		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object outValue)
		{
			outValue = null;
			if (!context.CheckFeatureIsSupported(ScriptFeatureFlags.Identifiers))
			{
				return;
			}
			IDataModel dataModel;
			if (Utilities.UsesNumericalEncoding(node))
			{
				if (!Utilities.TryParseNumericalIdentifier(node, out var id))
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Identifier '{0}' is not valid because not numerical.", node.Token.Value);
					return;
				}
				dataModel = context.GetDataModelById(id);
				if (dataModel == null)
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Model with ID '{0}' could not be found.", id);
					return;
				}
			}
			else
			{
				if (context.QueryObjects != null && context.QueryObjects.Count > 0 && node.Token.Value == "x")
				{
					outValue = context.QueryObjects[context.QueryObjects.Count - 1];
					node.Value = outValue;
					node.ValueType = ((outValue != null) ? outValue.GetType() : null);
					return;
				}
				if (ScriptKeywords.EvaluateKeyword(node.Token, out var value))
				{
					node.Value = value;
					node.ValueType = value?.GetType();
					_ = context.EncodingPolicy;
					outValue = node.Value;
					return;
				}
				string value2 = node.Token.Value;
				dataModel = context.GetDataModelByDisplayName(value2);
				if (dataModel == null)
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Model with name '{0}' could not be found.", value2);
					return;
				}
			}
			node.ValueType = dataModel.GetType();
			outValue = (node.Value = dataModel);
			ScriptContext.EncodingPolicy encodingPolicy = context.EncodingPolicy;
			if (encodingPolicy != ScriptContext.EncodingPolicy.Numerical)
			{
				_ = 2;
			}
		}
	}
}
