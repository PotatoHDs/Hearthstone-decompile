using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x0200103B RID: 4155
	public class ArithmeticScriptSyntaxTreeRule : ScriptSyntaxTreeRule<ArithmeticScriptSyntaxTreeRule>
	{
		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x0600B494 RID: 46228 RVA: 0x0037838A File Offset: 0x0037658A
		protected override HashSet<ScriptToken.TokenType> TokensInternal
		{
			get
			{
				return new HashSet<ScriptToken.TokenType>
				{
					ScriptToken.TokenType.Plus,
					ScriptToken.TokenType.Minus,
					ScriptToken.TokenType.Star,
					ScriptToken.TokenType.ForwardSlash,
					ScriptToken.TokenType.Percent,
					ScriptToken.TokenType.Caret
				};
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x0600B495 RID: 46229 RVA: 0x003783C7 File Offset: 0x003765C7
		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal
		{
			get
			{
				return new ScriptSyntaxTreeRule[]
				{
					ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<NumberScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<MethodScriptSyntaxTreeRule>.Get()
				};
			}
		}

		// Token: 0x0600B496 RID: 46230 RVA: 0x003783F0 File Offset: 0x003765F0
		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object value)
		{
			value = null;
			if (!context.CheckFeatureIsSupported(ScriptFeatureFlags.Relational))
			{
				return;
			}
			double num;
			if (!this.EvaluateAsDouble(context, node.Left, out num))
			{
				context.Results.SetFailedNodeIfNoneExists(node, node.Left);
				return;
			}
			ScriptContext.EncodingPolicy encodingPolicy = context.EncodingPolicy;
			double num2;
			if (!this.EvaluateAsDouble(context, node.Right, out num2))
			{
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			node.ValueType = typeof(double);
			switch (node.Token.Type)
			{
			case ScriptToken.TokenType.Plus:
				value = (node.Value = num + num2);
				return;
			case ScriptToken.TokenType.Minus:
				value = (node.Value = num - num2);
				return;
			case ScriptToken.TokenType.Star:
				value = (node.Value = num * num2);
				return;
			case ScriptToken.TokenType.ForwardSlash:
				value = (node.Value = num / num2);
				return;
			case ScriptToken.TokenType.Percent:
				value = (node.Value = num % num2);
				return;
			case ScriptToken.TokenType.Caret:
				value = (node.Value = Math.Pow(num, num2));
				return;
			default:
				value = (node.Value = 0);
				return;
			}
		}

		// Token: 0x0600B497 RID: 46231 RVA: 0x00378528 File Offset: 0x00376728
		private bool EvaluateAsDouble(ScriptContext.EvaluationContext context, ScriptSyntaxTreeNode node, out double outValue)
		{
			outValue = 0.0;
			if (node == null)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Missing operand!", Array.Empty<object>());
				return false;
			}
			object obj;
			if (!node.Evaluate(context, out obj))
			{
				return false;
			}
			try
			{
				outValue = Convert.ToDouble(obj);
			}
			catch (InvalidCastException)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "'{0}' is not a number", new object[]
				{
					obj
				});
				return false;
			}
			catch (Exception ex)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, context.EditMode ? ex.ToString() : null, Array.Empty<object>());
				return false;
			}
			return true;
		}

		// Token: 0x0600B498 RID: 46232 RVA: 0x003785CC File Offset: 0x003767CC
		[Conditional("UNITY_EDITOR")]
		private void EmitSuggestions(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context)
		{
			if (!context.SuggestionsEnabled)
			{
				return;
			}
			List<ScriptContext.SuggestionInfo> list = new List<ScriptContext.SuggestionInfo>();
			if (node.Left != null && node.Left.ValueType != null && node.Left.ValueType.IsEnum)
			{
				foreach (string text in Enum.GetNames(node.Left.ValueType))
				{
					list.Add(new ScriptContext.SuggestionInfo
					{
						Identifier = text.ToLower(),
						CandidateType = ScriptContext.SuggestionInfo.Types.Property,
						ValueType = node.Left.ValueType
					});
				}
			}
			else
			{
				ScriptSyntaxTreeRule.Utilities.CollectSuggestionsInGlobalNamespace(context, list);
			}
			foreach (ScriptContext.SuggestionInfo suggestionInfo in list)
			{
			}
		}
	}
}
