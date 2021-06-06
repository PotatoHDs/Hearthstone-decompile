using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x02001047 RID: 4167
	public class RootScriptSyntaxTreeRule : ScriptSyntaxTreeRule<RootScriptSyntaxTreeRule>
	{
		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x0600B4D2 RID: 46290 RVA: 0x00378AB6 File Offset: 0x00376CB6
		protected override HashSet<ScriptToken.TokenType> TokensInternal
		{
			get
			{
				return new HashSet<ScriptToken.TokenType>();
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x0600B4D3 RID: 46291 RVA: 0x00379EA4 File Offset: 0x003780A4
		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal
		{
			get
			{
				return new ScriptSyntaxTreeRule[]
				{
					ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<NumberScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<StringScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<EmptyScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<MethodScriptSyntaxTreeRule>.Get()
				};
			}
		}

		// Token: 0x0600B4D4 RID: 46292 RVA: 0x00379EDC File Offset: 0x003780DC
		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext evaluationContext, out object value)
		{
			value = null;
			if (node.Right != null)
			{
				node.Right.Evaluate(evaluationContext, out value);
			}
		}
	}
}
