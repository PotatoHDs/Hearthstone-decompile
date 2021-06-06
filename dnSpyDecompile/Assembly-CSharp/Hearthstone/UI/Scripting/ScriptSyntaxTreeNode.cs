using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x02001053 RID: 4179
	public class ScriptSyntaxTreeNode
	{
		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x0600B509 RID: 46345 RVA: 0x0037AB7C File Offset: 0x00378D7C
		public ScriptToken Token
		{
			get
			{
				if (this.Tokens != null && this.Tokens.Length != 0)
				{
					return this.Tokens[0];
				}
				return default(ScriptToken);
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x0600B50A RID: 46346 RVA: 0x0037ABB0 File Offset: 0x00378DB0
		public int Priority
		{
			get
			{
				return ScriptSyntaxTreeNode.s_priorities[this.Rule];
			}
		}

		// Token: 0x0600B50B RID: 46347 RVA: 0x0037ABC2 File Offset: 0x00378DC2
		public ScriptSyntaxTreeNode(ScriptSyntaxTreeRule rule)
		{
			this.Rule = rule;
		}

		// Token: 0x0600B50C RID: 46348 RVA: 0x0037ABD4 File Offset: 0x00378DD4
		public bool Evaluate(ScriptContext.EvaluationContext evaluationContext, out object value)
		{
			evaluationContext.Results.LastNode = this;
			this.Rule.Evaluate(this, evaluationContext, out value);
			if (evaluationContext.Results.ErrorCode != ScriptContext.ErrorCodes.Success)
			{
				value = null;
				this.Value = null;
				this.ValueType = null;
				this.Target = null;
				if (evaluationContext.Results.FailedNodeInfo == null)
				{
					evaluationContext.Results.SetFailedNodeIfNoneExists(this, this);
				}
				return false;
			}
			this.Value = value;
			if (this.ValueType == null)
			{
				this.ValueType = ((value != null) ? value.GetType() : null);
			}
			return true;
		}

		// Token: 0x0400970D RID: 38669
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

		// Token: 0x0400970E RID: 38670
		public readonly ScriptSyntaxTreeRule Rule;

		// Token: 0x0400970F RID: 38671
		public ScriptToken[] Tokens;

		// Token: 0x04009710 RID: 38672
		public ScriptSyntaxTreeNode Left;

		// Token: 0x04009711 RID: 38673
		public ScriptSyntaxTreeNode Right;

		// Token: 0x04009712 RID: 38674
		public ScriptSyntaxTreeNode Parent;

		// Token: 0x04009713 RID: 38675
		public object Value;

		// Token: 0x04009714 RID: 38676
		public Type ValueType;

		// Token: 0x04009715 RID: 38677
		public object Payload;

		// Token: 0x04009716 RID: 38678
		public object Target;
	}
}
