using System;
using System.Collections;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x0200103D RID: 4157
	public class ContainsScriptSyntaxTreeRule : ScriptSyntaxTreeRule<ContainsScriptSyntaxTreeRule>
	{
		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x0600B49E RID: 46238 RVA: 0x00378825 File Offset: 0x00376A25
		protected override HashSet<ScriptToken.TokenType> TokensInternal
		{
			get
			{
				return new HashSet<ScriptToken.TokenType>
				{
					ScriptToken.TokenType.Has
				};
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x0600B49F RID: 46239 RVA: 0x00378834 File Offset: 0x00376A34
		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal
		{
			get
			{
				return new ScriptSyntaxTreeRule[]
				{
					ScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<NumberScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<StringScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<MethodScriptSyntaxTreeRule>.Get()
				};
			}
		}

		// Token: 0x0600B4A0 RID: 46240 RVA: 0x00378864 File Offset: 0x00376A64
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
			IList list;
			if ((list = (obj as IList)) == null)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Only collections can be queried", Array.Empty<object>());
				context.Results.SetFailedNodeIfNoneExists(node, node.Left);
				return;
			}
			object obj2;
			if (node.Right == null || !node.Right.Evaluate(context, out obj2) || obj2 == null)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Expected one or more values on the right!", Array.Empty<object>());
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			Type collectionGenericArgument = ContainsScriptSyntaxTreeRule.GetCollectionGenericArgument(list);
			IList list2 = obj2 as IList;
			if (list2 != null)
			{
				int num = 0;
				this.m_cachedHashSet.Clear();
				int count = list2.Count;
				for (int i = 0; i < count; i++)
				{
					DynamicValue dynamicValue = (list2[i] is DynamicValue) ? ((DynamicValue)list2[i]) : default(DynamicValue);
					object obj3 = dynamicValue.HasValidValue ? dynamicValue.Value : list2[i];
					this.m_cachedHashSet.Add(((IConvertible)obj3).ToType(collectionGenericArgument, null));
					num++;
				}
				int num2 = 0;
				int count2 = list.Count;
				for (int j = 0; j < count2; j++)
				{
					object item = list[j];
					if (this.m_cachedHashSet.Contains(item))
					{
						num2++;
					}
					if (num2 == num)
					{
						break;
					}
				}
				value = (num == num2);
				return;
			}
			bool flag = false;
			object objB = ((IConvertible)obj2).ToType(collectionGenericArgument, null);
			int count3 = list.Count;
			for (int k = 0; k < count3; k++)
			{
				if (object.Equals(list[k], objB))
				{
					flag = true;
					break;
				}
			}
			value = flag;
		}

		// Token: 0x0600B4A1 RID: 46241 RVA: 0x00378A5C File Offset: 0x00376C5C
		private static Type GetCollectionGenericArgument(IList collection)
		{
			Type type = collection.GetType();
			Type type2 = null;
			if (!ContainsScriptSyntaxTreeRule.s_collectionToGenericArgument.TryGetValue(type, out type2))
			{
				type2 = type.GetGenericArguments()[0];
				ContainsScriptSyntaxTreeRule.s_collectionToGenericArgument[type] = type2;
			}
			return type2;
		}

		// Token: 0x040096EC RID: 38636
		private HashSet<object> m_cachedHashSet = new HashSet<object>();

		// Token: 0x040096ED RID: 38637
		private static Dictionary<Type, Type> s_collectionToGenericArgument = new Dictionary<Type, Type>();
	}
}
