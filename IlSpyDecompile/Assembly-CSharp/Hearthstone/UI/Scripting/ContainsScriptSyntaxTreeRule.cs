using System;
using System.Collections;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	public class ContainsScriptSyntaxTreeRule : ScriptSyntaxTreeRule<ContainsScriptSyntaxTreeRule>
	{
		private HashSet<object> m_cachedHashSet = new HashSet<object>();

		private static Dictionary<Type, Type> s_collectionToGenericArgument = new Dictionary<Type, Type>();

		protected override HashSet<ScriptToken.TokenType> TokensInternal => new HashSet<ScriptToken.TokenType> { ScriptToken.TokenType.Has };

		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal => new ScriptSyntaxTreeRule[5]
		{
			ScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<NumberScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<StringScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get(),
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
			IList list;
			if ((list = value2 as IList) == null)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Only collections can be queried");
				context.Results.SetFailedNodeIfNoneExists(node, node.Left);
				return;
			}
			if (node.Right == null || !node.Right.Evaluate(context, out var value3) || value3 == null)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Expected one or more values on the right!");
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			Type collectionGenericArgument = GetCollectionGenericArgument(list);
			IList list2 = value3 as IList;
			if (list2 != null)
			{
				int num = 0;
				m_cachedHashSet.Clear();
				int count = list2.Count;
				for (int i = 0; i < count; i++)
				{
					DynamicValue dynamicValue = ((list2[i] is DynamicValue) ? ((DynamicValue)list2[i]) : default(DynamicValue));
					object obj = (dynamicValue.HasValidValue ? dynamicValue.Value : list2[i]);
					m_cachedHashSet.Add(((IConvertible)obj).ToType(collectionGenericArgument, null));
					num++;
				}
				int num2 = 0;
				int count2 = list.Count;
				for (int j = 0; j < count2; j++)
				{
					object item = list[j];
					if (m_cachedHashSet.Contains(item))
					{
						num2++;
					}
					if (num2 == num)
					{
						break;
					}
				}
				value = num == num2;
				return;
			}
			bool flag = false;
			object objB = ((IConvertible)value3).ToType(collectionGenericArgument, null);
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

		private static Type GetCollectionGenericArgument(IList collection)
		{
			Type type = collection.GetType();
			Type value = null;
			if (!s_collectionToGenericArgument.TryGetValue(type, out value))
			{
				value = type.GetGenericArguments()[0];
				s_collectionToGenericArgument[type] = value;
			}
			return value;
		}
	}
}
