using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x02001042 RID: 4162
	public class IdentifierScriptSyntaxTreeRule : ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>
	{
		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x0600B4B7 RID: 46263 RVA: 0x00379066 File Offset: 0x00377266
		protected override HashSet<ScriptToken.TokenType> TokensInternal
		{
			get
			{
				return new HashSet<ScriptToken.TokenType>
				{
					ScriptToken.TokenType.Literal
				};
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x0600B4B8 RID: 46264 RVA: 0x00379078 File Offset: 0x00377278
		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal
		{
			get
			{
				return new ScriptSyntaxTreeRule[]
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
			}
		}

		// Token: 0x0600B4B9 RID: 46265 RVA: 0x003790E0 File Offset: 0x003772E0
		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object outValue)
		{
			outValue = null;
			if (!context.CheckFeatureIsSupported(ScriptFeatureFlags.Identifiers))
			{
				return;
			}
			IDataModel dataModel;
			if (ScriptSyntaxTreeRule.Utilities.UsesNumericalEncoding(node))
			{
				int num;
				if (!ScriptSyntaxTreeRule.Utilities.TryParseNumericalIdentifier(node, out num))
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Identifier '{0}' is not valid because not numerical.", new object[]
					{
						node.Token.Value
					});
					return;
				}
				dataModel = context.GetDataModelById(num);
				if (dataModel == null)
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Model with ID '{0}' could not be found.", new object[]
					{
						num
					});
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
				object obj;
				if (ScriptKeywords.EvaluateKeyword(node.Token, out obj))
				{
					node.Value = obj;
					node.ValueType = ((obj != null) ? obj.GetType() : null);
					ScriptContext.EncodingPolicy encodingPolicy = context.EncodingPolicy;
					outValue = node.Value;
					return;
				}
				string value = node.Token.Value;
				dataModel = context.GetDataModelByDisplayName(value);
				if (dataModel == null)
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Model with name '{0}' could not be found.", new object[]
					{
						value
					});
					return;
				}
			}
			node.ValueType = dataModel.GetType();
			outValue = (node.Value = dataModel);
			ScriptContext.EncodingPolicy encodingPolicy2 = context.EncodingPolicy;
			if (encodingPolicy2 != ScriptContext.EncodingPolicy.Numerical)
			{
			}
		}
	}
}
