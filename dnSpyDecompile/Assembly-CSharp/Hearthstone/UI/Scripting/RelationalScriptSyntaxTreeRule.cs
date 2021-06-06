using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x02001046 RID: 4166
	public class RelationalScriptSyntaxTreeRule : ScriptSyntaxTreeRule<RelationalScriptSyntaxTreeRule>
	{
		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x0600B4CC RID: 46284 RVA: 0x00379A00 File Offset: 0x00377C00
		protected override HashSet<ScriptToken.TokenType> TokensInternal
		{
			get
			{
				return new HashSet<ScriptToken.TokenType>
				{
					ScriptToken.TokenType.Equal,
					ScriptToken.TokenType.NotEqual,
					ScriptToken.TokenType.Greater,
					ScriptToken.TokenType.GreaterEqual,
					ScriptToken.TokenType.Less,
					ScriptToken.TokenType.LessEqual
				};
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x0600B4CD RID: 46285 RVA: 0x00379A3A File Offset: 0x00377C3A
		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal
		{
			get
			{
				return new ScriptSyntaxTreeRule[]
				{
					ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<NumberScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<StringScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<MethodScriptSyntaxTreeRule>.Get()
				};
			}
		}

		// Token: 0x0600B4CE RID: 46286 RVA: 0x00379A6C File Offset: 0x00377C6C
		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object value)
		{
			value = null;
			if (!context.CheckFeatureIsSupported(ScriptFeatureFlags.Relational))
			{
				return;
			}
			object obj;
			if (node.Left == null || !node.Left.Evaluate(context, out obj))
			{
				context.Results.SetFailedNodeIfNoneExists(node, node.Left);
				return;
			}
			ScriptContext.EncodingPolicy encodingPolicy = context.EncodingPolicy;
			if (node.Right == null)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Expected right-hand operand", Array.Empty<object>());
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			object obj2;
			if (!this.EvaluateRightHandValue(context, node, out obj2))
			{
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			bool flag = obj2 is string;
			bool flag2 = obj is string;
			object obj3 = obj2;
			int num = 0;
			if (flag == flag2)
			{
				IConvertible convertible = obj2 as IConvertible;
				if (convertible != null)
				{
					obj3 = ((obj is int) ? convertible.ToInt32(null) : ((obj is long) ? convertible.ToInt64(null) : ((obj is double) ? convertible.ToDouble(null) : ((obj is float) ? convertible.ToSingle(null) : obj2))));
				}
				IComparable comparable = obj as IComparable;
				num = ((comparable != null) ? comparable.CompareTo(obj3) : 0);
			}
			node.ValueType = typeof(bool);
			switch (node.Token.Type)
			{
			case ScriptToken.TokenType.Equal:
				value = (node.Value = object.Equals(obj, obj3));
				return;
			case ScriptToken.TokenType.NotEqual:
				value = (node.Value = !object.Equals(obj, obj3));
				return;
			case ScriptToken.TokenType.Less:
				value = (node.Value = (num < 0));
				return;
			case ScriptToken.TokenType.LessEqual:
				value = (node.Value = (num <= 0));
				return;
			case ScriptToken.TokenType.Greater:
				value = (node.Value = (num > 0));
				return;
			case ScriptToken.TokenType.GreaterEqual:
				value = (node.Value = (num >= 0));
				return;
			default:
				value = (node.Value = false);
				return;
			}
		}

		// Token: 0x0600B4CF RID: 46287 RVA: 0x00379C9C File Offset: 0x00377E9C
		private bool EvaluateRightHandValue(ScriptContext.EvaluationContext context, ScriptSyntaxTreeNode node, out object outValue)
		{
			outValue = null;
			if (node.Left.ValueType != null && node.Left.ValueType.IsEnum && node.Right.Rule == ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get())
			{
				string value = node.Right.Token.Value;
				Type valueType = node.Left.ValueType;
				try
				{
					outValue = Enum.Parse(valueType, value, true);
					node.Right.Value = outValue;
					node.Right.ValueType = node.Left.ValueType;
					ScriptContext.EncodingPolicy encodingPolicy = context.EncodingPolicy;
					if (encodingPolicy != ScriptContext.EncodingPolicy.Numerical)
					{
					}
					return true;
				}
				catch (Exception)
				{
				}
			}
			if (ScriptSyntaxTreeRule.Utilities.IsDynamicType(node.Left.ValueType))
			{
				bool flag = false;
				if (node.Right.Rule != ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get())
				{
					flag = node.Right.Evaluate(context, out outValue);
				}
				if (!flag && context.EncodingPolicy != ScriptContext.EncodingPolicy.None)
				{
					context.Results.ErrorCode = ScriptContext.ErrorCodes.Success;
				}
				return true;
			}
			return node.Right.Evaluate(context, out outValue);
		}

		// Token: 0x0600B4D0 RID: 46288 RVA: 0x00379DBC File Offset: 0x00377FBC
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
