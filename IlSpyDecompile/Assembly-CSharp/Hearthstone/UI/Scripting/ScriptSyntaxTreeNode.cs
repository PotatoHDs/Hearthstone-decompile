using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	public class ScriptSyntaxTreeNode
	{
		private static Dictionary<ScriptSyntaxTreeRule, int> s_priorities = new Dictionary<ScriptSyntaxTreeRule, int>
		{
			{
				ScriptSyntaxTreeRule<EmptyScriptSyntaxTreeRule>.Get(),
				1
			},
			{
				ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get(),
				2
			},
			{
				ScriptSyntaxTreeRule<NumberScriptSyntaxTreeRule>.Get(),
				2
			},
			{
				ScriptSyntaxTreeRule<StringScriptSyntaxTreeRule>.Get(),
				2
			},
			{
				ScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>.Get(),
				3
			},
			{
				ScriptSyntaxTreeRule<IndexerScriptSyntaxTreeRule>.Get(),
				3
			},
			{
				ScriptSyntaxTreeRule<AccessorScriptSyntaxTreeRule>.Get(),
				3
			},
			{
				ScriptSyntaxTreeRule<MethodScriptSyntaxTreeRule>.Get(),
				4
			},
			{
				ScriptSyntaxTreeRule<EventScriptSyntaxTreeRule>.Get(),
				4
			},
			{
				ScriptSyntaxTreeRule<TupleScriptSyntaxTreeRule>.Get(),
				5
			},
			{
				ScriptSyntaxTreeRule<ContainsScriptSyntaxTreeRule>.Get(),
				6
			},
			{
				ScriptSyntaxTreeRule<ArithmeticScriptSyntaxTreeRule>.Get(),
				7
			},
			{
				ScriptSyntaxTreeRule<RelationalScriptSyntaxTreeRule>.Get(),
				8
			},
			{
				ScriptSyntaxTreeRule<ConditionalScriptSyntaxTreeRule>.Get(),
				9
			},
			{
				ScriptSyntaxTreeRule<RootScriptSyntaxTreeRule>.Get(),
				1000
			}
		};

		public readonly ScriptSyntaxTreeRule Rule;

		public ScriptToken[] Tokens;

		public ScriptSyntaxTreeNode Left;

		public ScriptSyntaxTreeNode Right;

		public ScriptSyntaxTreeNode Parent;

		public object Value;

		public Type ValueType;

		public object Payload;

		public object Target;

		public ScriptToken Token
		{
			get
			{
				if (Tokens != null && Tokens.Length != 0)
				{
					return Tokens[0];
				}
				return default(ScriptToken);
			}
		}

		public int Priority => s_priorities[Rule];

		public ScriptSyntaxTreeNode(ScriptSyntaxTreeRule rule)
		{
			Rule = rule;
		}

		public bool Evaluate(ScriptContext.EvaluationContext evaluationContext, out object value)
		{
			evaluationContext.Results.LastNode = this;
			Rule.Evaluate(this, evaluationContext, out value);
			if (evaluationContext.Results.ErrorCode != 0)
			{
				value = null;
				Value = null;
				ValueType = null;
				Target = null;
				if (evaluationContext.Results.FailedNodeInfo == null)
				{
					evaluationContext.Results.SetFailedNodeIfNoneExists(this, this);
				}
				return false;
			}
			Value = value;
			if (ValueType == null)
			{
				ValueType = ((value != null) ? value.GetType() : null);
			}
			return true;
		}
	}
}
