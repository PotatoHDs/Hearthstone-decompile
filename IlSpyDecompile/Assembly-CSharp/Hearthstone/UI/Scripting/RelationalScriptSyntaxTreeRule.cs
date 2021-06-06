using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hearthstone.UI.Scripting
{
	public class RelationalScriptSyntaxTreeRule : ScriptSyntaxTreeRule<RelationalScriptSyntaxTreeRule>
	{
		protected override HashSet<ScriptToken.TokenType> TokensInternal => new HashSet<ScriptToken.TokenType>
		{
			ScriptToken.TokenType.Equal,
			ScriptToken.TokenType.NotEqual,
			ScriptToken.TokenType.Greater,
			ScriptToken.TokenType.GreaterEqual,
			ScriptToken.TokenType.Less,
			ScriptToken.TokenType.LessEqual
		};

		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal => new ScriptSyntaxTreeRule[5]
		{
			ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<NumberScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<StringScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<MethodScriptSyntaxTreeRule>.Get()
		};

		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object value)
		{
			value = null;
			if (!context.CheckFeatureIsSupported(ScriptFeatureFlags.Relational))
			{
				return;
			}
			if (node.Left == null || !node.Left.Evaluate(context, out var value2))
			{
				context.Results.SetFailedNodeIfNoneExists(node, node.Left);
				return;
			}
			_ = context.EncodingPolicy;
			if (node.Right == null)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Expected right-hand operand");
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			if (!EvaluateRightHandValue(context, node, out var outValue))
			{
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			bool num = outValue is string;
			bool flag = value2 is string;
			object obj = outValue;
			int num2 = 0;
			if (num == flag)
			{
				IConvertible convertible = outValue as IConvertible;
				if (convertible != null)
				{
					obj = ((value2 is int) ? ((object)convertible.ToInt32(null)) : ((value2 is long) ? ((object)convertible.ToInt64(null)) : ((value2 is double) ? ((object)convertible.ToDouble(null)) : ((value2 is float) ? ((object)convertible.ToSingle(null)) : outValue))));
				}
				num2 = (value2 as IComparable)?.CompareTo(obj) ?? 0;
			}
			node.ValueType = typeof(bool);
			switch (node.Token.Type)
			{
			case ScriptToken.TokenType.Equal:
				value = (node.Value = object.Equals(value2, obj));
				break;
			case ScriptToken.TokenType.NotEqual:
				value = (node.Value = !object.Equals(value2, obj));
				break;
			case ScriptToken.TokenType.Greater:
				value = (node.Value = num2 > 0);
				break;
			case ScriptToken.TokenType.GreaterEqual:
				value = (node.Value = num2 >= 0);
				break;
			case ScriptToken.TokenType.Less:
				value = (node.Value = num2 < 0);
				break;
			case ScriptToken.TokenType.LessEqual:
				value = (node.Value = num2 <= 0);
				break;
			default:
				value = (node.Value = false);
				break;
			}
		}

		private bool EvaluateRightHandValue(ScriptContext.EvaluationContext context, ScriptSyntaxTreeNode node, out object outValue)
		{
			outValue = null;
			if (node.Left.ValueType != null && node.Left.ValueType.IsEnum && node.Right.Rule == ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get())
			{
				string value = node.Right.Token.Value;
				Type valueType = node.Left.ValueType;
				try
				{
					outValue = Enum.Parse(valueType, value, ignoreCase: true);
					node.Right.Value = outValue;
					node.Right.ValueType = node.Left.ValueType;
					ScriptContext.EncodingPolicy encodingPolicy = context.EncodingPolicy;
					if (encodingPolicy != ScriptContext.EncodingPolicy.Numerical)
					{
						_ = 2;
					}
					return true;
				}
				catch (Exception)
				{
				}
			}
			if (Utilities.IsDynamicType(node.Left.ValueType))
			{
				bool flag = false;
				if (node.Right.Rule != ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get())
				{
					flag = node.Right.Evaluate(context, out outValue);
				}
				if (!flag && context.EncodingPolicy != 0)
				{
					context.Results.ErrorCode = ScriptContext.ErrorCodes.Success;
				}
				return true;
			}
			return node.Right.Evaluate(context, out outValue);
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
