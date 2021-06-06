using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hearthstone.UI.Scripting
{
	public class ArithmeticScriptSyntaxTreeRule : ScriptSyntaxTreeRule<ArithmeticScriptSyntaxTreeRule>
	{
		protected override HashSet<ScriptToken.TokenType> TokensInternal => new HashSet<ScriptToken.TokenType>
		{
			ScriptToken.TokenType.Plus,
			ScriptToken.TokenType.Minus,
			ScriptToken.TokenType.Star,
			ScriptToken.TokenType.ForwardSlash,
			ScriptToken.TokenType.Percent,
			ScriptToken.TokenType.Caret
		};

		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal => new ScriptSyntaxTreeRule[4]
		{
			ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<NumberScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<MethodScriptSyntaxTreeRule>.Get()
		};

		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object value)
		{
			value = null;
			if (!context.CheckFeatureIsSupported(ScriptFeatureFlags.Relational))
			{
				return;
			}
			if (!EvaluateAsDouble(context, node.Left, out var outValue))
			{
				context.Results.SetFailedNodeIfNoneExists(node, node.Left);
				return;
			}
			_ = context.EncodingPolicy;
			if (!EvaluateAsDouble(context, node.Right, out var outValue2))
			{
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			node.ValueType = typeof(double);
			switch (node.Token.Type)
			{
			case ScriptToken.TokenType.Plus:
				value = (node.Value = outValue + outValue2);
				break;
			case ScriptToken.TokenType.Minus:
				value = (node.Value = outValue - outValue2);
				break;
			case ScriptToken.TokenType.Star:
				value = (node.Value = outValue * outValue2);
				break;
			case ScriptToken.TokenType.ForwardSlash:
				value = (node.Value = outValue / outValue2);
				break;
			case ScriptToken.TokenType.Percent:
				value = (node.Value = outValue % outValue2);
				break;
			case ScriptToken.TokenType.Caret:
				value = (node.Value = Math.Pow(outValue, outValue2));
				break;
			default:
				value = (node.Value = 0);
				break;
			}
		}

		private bool EvaluateAsDouble(ScriptContext.EvaluationContext context, ScriptSyntaxTreeNode node, out double outValue)
		{
			outValue = 0.0;
			if (node == null)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Missing operand!");
				return false;
			}
			if (!node.Evaluate(context, out var value))
			{
				return false;
			}
			try
			{
				outValue = Convert.ToDouble(value);
			}
			catch (InvalidCastException)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "'{0}' is not a number", value);
				return false;
			}
			catch (Exception ex2)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, context.EditMode ? ex2.ToString() : null);
				return false;
			}
			return true;
		}

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
				string[] names = Enum.GetNames(node.Left.ValueType);
				foreach (string text in names)
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
				Utilities.CollectSuggestionsInGlobalNamespace(context, list);
			}
			foreach (ScriptContext.SuggestionInfo item in list)
			{
				_ = item;
			}
		}
	}
}
