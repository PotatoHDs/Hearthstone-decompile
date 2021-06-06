using System;
using System.Collections;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x0200104C RID: 4172
	public class TupleScriptSyntaxTreeRule : ScriptSyntaxTreeRule<TupleScriptSyntaxTreeRule>
	{
		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x0600B4EB RID: 46315 RVA: 0x0037A1AB File Offset: 0x003783AB
		protected override HashSet<ScriptToken.TokenType> TokensInternal
		{
			get
			{
				return new HashSet<ScriptToken.TokenType>
				{
					ScriptToken.TokenType.Comma
				};
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x0600B4EC RID: 46316 RVA: 0x003786CD File Offset: 0x003768CD
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

		// Token: 0x0600B4ED RID: 46317 RVA: 0x0037A1BC File Offset: 0x003783BC
		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object outValue)
		{
			outValue = null;
			if (!context.CheckFeatureIsSupported(ScriptFeatureFlags.Tuples))
			{
				return;
			}
			object obj;
			if (node.Left == null || !node.Left.Evaluate(context, out obj) || (obj == null && (node.Left.ValueType == null || node.Left.ValueType.IsValueType)))
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Expected left value", Array.Empty<object>());
				context.Results.SetFailedNodeIfNoneExists(node, node.Left);
				return;
			}
			object obj2;
			if (node.Right == null || !node.Right.Evaluate(context, out obj2) || (obj2 == null && (node.Right.ValueType == null || node.Left.ValueType.IsValueType)))
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Expected right value", Array.Empty<object>());
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			ArrayList arrayList = new ArrayList();
			ICollection collection = obj as ICollection;
			ICollection collection2 = obj2 as ICollection;
			if (collection != null)
			{
				using (IEnumerator enumerator = collection.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object value = enumerator.Current;
						arrayList.Add(value);
					}
					goto IL_14C;
				}
			}
			arrayList.Add(new DynamicValue(obj, node.Left.ValueType));
			IL_14C:
			if (collection2 != null)
			{
				using (IEnumerator enumerator = collection2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object value2 = enumerator.Current;
						arrayList.Add(value2);
					}
					goto IL_1AA;
				}
			}
			arrayList.Add(new DynamicValue(obj2, node.Right.ValueType));
			IL_1AA:
			outValue = arrayList;
		}
	}
}
