using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	public class EventScriptSyntaxTreeRule : ScriptSyntaxTreeRule<EventScriptSyntaxTreeRule>
	{
		protected override HashSet<ScriptToken.TokenType> TokensInternal => new HashSet<ScriptToken.TokenType> { ScriptToken.TokenType.Colon };

		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal => new ScriptSyntaxTreeRule[2]
		{
			ScriptSyntaxTreeRule<ConditionalScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<EmptyScriptSyntaxTreeRule>.Get()
		};

		public override ParseResult Parse(ScriptToken[] tokens, ref int tokenIndex, out string parseErrorMessage, out ScriptSyntaxTreeNode node)
		{
			parseErrorMessage = null;
			node = null;
			if (tokenIndex >= tokens.Length - 1)
			{
				parseErrorMessage = "Event types expected: changed";
				return ParseResult.Failed;
			}
			ScriptToken scriptToken = tokens[++tokenIndex];
			if (scriptToken.Value != "changed")
			{
				parseErrorMessage = "Event types expected: changed";
				return ParseResult.Failed;
			}
			node = new ScriptSyntaxTreeNode(this);
			return ParseResult.Success;
		}

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
			if (node.Left != null && node.Left.Evaluate(context, out var value2) && node.Tokens[1].Value == "changed")
			{
				int num = 0;
				if (value2 != null)
				{
					num = (value2 as IDataModelProperties)?.GetPropertiesHashCode() ?? value2.GetHashCode();
				}
				value = false;
				if (!context.CachedNodeValues.TryGetValue(node, out var value3) && value2 != null)
				{
					Type type = value2.GetType();
					value = (type.IsPrimitive ? (num != Activator.CreateInstance(type).GetHashCode()) : (num != 0));
				}
				else
				{
					value = num != value3;
				}
				ref bool eventRaised = ref context.Results.EventRaised;
				eventRaised |= (bool)value;
				context.CachedNodeValues[node] = num;
			}
		}
	}
}
