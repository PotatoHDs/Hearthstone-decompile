using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x0200103F RID: 4159
	public class EventScriptSyntaxTreeRule : ScriptSyntaxTreeRule<EventScriptSyntaxTreeRule>
	{
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x0600B4A8 RID: 46248 RVA: 0x00378AD2 File Offset: 0x00376CD2
		protected override HashSet<ScriptToken.TokenType> TokensInternal
		{
			get
			{
				return new HashSet<ScriptToken.TokenType>
				{
					ScriptToken.TokenType.Colon
				};
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x0600B4A9 RID: 46249 RVA: 0x00378AE2 File Offset: 0x00376CE2
		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal
		{
			get
			{
				return new ScriptSyntaxTreeRule[]
				{
					ScriptSyntaxTreeRule<ConditionalScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<EmptyScriptSyntaxTreeRule>.Get()
				};
			}
		}

		// Token: 0x0600B4AA RID: 46250 RVA: 0x00378AFC File Offset: 0x00376CFC
		public override ScriptSyntaxTreeRule.ParseResult Parse(ScriptToken[] tokens, ref int tokenIndex, out string parseErrorMessage, out ScriptSyntaxTreeNode node)
		{
			parseErrorMessage = null;
			node = null;
			if (tokenIndex >= tokens.Length - 1)
			{
				parseErrorMessage = "Event types expected: changed";
				return ScriptSyntaxTreeRule.ParseResult.Failed;
			}
			int num = tokenIndex + 1;
			tokenIndex = num;
			ScriptToken scriptToken = tokens[num];
			if (scriptToken.Value != "changed")
			{
				parseErrorMessage = "Event types expected: changed";
				return ScriptSyntaxTreeRule.ParseResult.Failed;
			}
			node = new ScriptSyntaxTreeNode(this);
			return ScriptSyntaxTreeRule.ParseResult.Success;
		}

		// Token: 0x0600B4AB RID: 46251 RVA: 0x00378B58 File Offset: 0x00376D58
		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object value)
		{
			value = null;
			if (!context.CheckFeatureIsSupported(ScriptFeatureFlags.Events))
			{
				return;
			}
			if (context.CachedNodeValues == null)
			{
				context.CachedNodeValues = new Map<ScriptSyntaxTreeNode, int>();
			}
			object obj;
			if (node.Left != null && node.Left.Evaluate(context, out obj) && node.Tokens[1].Value == "changed")
			{
				int num = 0;
				if (obj != null)
				{
					IDataModelProperties dataModelProperties = obj as IDataModelProperties;
					num = ((dataModelProperties != null) ? dataModelProperties.GetPropertiesHashCode() : obj.GetHashCode());
				}
				value = false;
				int num2;
				if (!context.CachedNodeValues.TryGetValue(node, out num2) && obj != null)
				{
					Type type = obj.GetType();
					value = (type.IsPrimitive ? (num != Activator.CreateInstance(type).GetHashCode()) : (num != 0));
				}
				else
				{
					value = (num != num2);
				}
				context.Results.EventRaised = (context.Results.EventRaised | (bool)value);
				context.CachedNodeValues[node] = num;
			}
		}
	}
}
