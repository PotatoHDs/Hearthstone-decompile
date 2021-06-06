using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x0200103C RID: 4156
	public class ConditionalScriptSyntaxTreeRule : ScriptSyntaxTreeRule<ConditionalScriptSyntaxTreeRule>
	{
		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x0600B49A RID: 46234 RVA: 0x003786B4 File Offset: 0x003768B4
		protected override HashSet<ScriptToken.TokenType> TokensInternal
		{
			get
			{
				return new HashSet<ScriptToken.TokenType>
				{
					ScriptToken.TokenType.Or,
					ScriptToken.TokenType.And
				};
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x0600B49B RID: 46235 RVA: 0x003786CD File Offset: 0x003768CD
		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal
		{
			get
			{
				return new ScriptSyntaxTreeRule[]
				{
					ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<NumberScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<StringScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<MethodScriptSyntaxTreeRule>.Get()
				};
			}
		}

		// Token: 0x0600B49C RID: 46236 RVA: 0x00378700 File Offset: 0x00376900
		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext evaluationContext, out object value)
		{
			value = null;
			if (!evaluationContext.CheckFeatureIsSupported(ScriptFeatureFlags.Conditionals))
			{
				return;
			}
			object obj;
			if (node.Left == null || !node.Left.Evaluate(evaluationContext, out obj))
			{
				evaluationContext.Results.SetFailedNodeIfNoneExists(node, node.Left);
				return;
			}
			ScriptContext.EncodingPolicy encodingPolicy = evaluationContext.EncodingPolicy;
			if (node.Right == null)
			{
				evaluationContext.EmitError(ScriptContext.ErrorCodes.EvaluationError, "", Array.Empty<object>());
				return;
			}
			object obj2;
			if (!node.Right.Evaluate(evaluationContext, out obj2))
			{
				evaluationContext.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			bool flag = (obj2 is bool) ? ((bool)obj2) : (obj2 != null);
			bool flag2 = (obj is bool) ? ((bool)obj) : (obj != null);
			node.ValueType = typeof(bool);
			ScriptToken.TokenType type = node.Token.Type;
			if (type == ScriptToken.TokenType.Or)
			{
				value = (node.Value = (flag || flag2));
				return;
			}
			if (type == ScriptToken.TokenType.And)
			{
				value = (node.Value = (flag && flag2));
				return;
			}
			value = (node.Value = false);
		}
	}
}
