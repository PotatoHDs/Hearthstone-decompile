using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x0200103E RID: 4158
	public class EmptyScriptSyntaxTreeRule : ScriptSyntaxTreeRule<EmptyScriptSyntaxTreeRule>
	{
		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x0600B4A4 RID: 46244 RVA: 0x00378AB6 File Offset: 0x00376CB6
		protected override HashSet<ScriptToken.TokenType> TokensInternal
		{
			get
			{
				return new HashSet<ScriptToken.TokenType>();
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x0600B4A5 RID: 46245 RVA: 0x00378ABD File Offset: 0x00376CBD
		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal
		{
			get
			{
				return new ScriptSyntaxTreeRule[0];
			}
		}

		// Token: 0x0600B4A6 RID: 46246 RVA: 0x00378AC5 File Offset: 0x00376CC5
		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext evaluationContext, out object value)
		{
			value = null;
		}
	}
}
