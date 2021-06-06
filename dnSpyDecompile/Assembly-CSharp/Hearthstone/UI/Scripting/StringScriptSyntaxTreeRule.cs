using System;
using System.Collections.Generic;
using System.Text;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x0200104B RID: 4171
	public class StringScriptSyntaxTreeRule : ScriptSyntaxTreeRule<StringScriptSyntaxTreeRule>
	{
		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x0600B4E6 RID: 46310 RVA: 0x0037A0D4 File Offset: 0x003782D4
		protected override HashSet<ScriptToken.TokenType> TokensInternal
		{
			get
			{
				return new HashSet<ScriptToken.TokenType>
				{
					ScriptToken.TokenType.DoubleQuote
				};
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x0600B4E7 RID: 46311 RVA: 0x0037A0E4 File Offset: 0x003782E4
		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal
		{
			get
			{
				return new ScriptSyntaxTreeRule[]
				{
					ScriptSyntaxTreeRule<ConditionalScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<RelationalScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<TupleScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<EmptyScriptSyntaxTreeRule>.Get()
				};
			}
		}

		// Token: 0x0600B4E8 RID: 46312 RVA: 0x0037A10C File Offset: 0x0037830C
		public override ScriptSyntaxTreeRule.ParseResult Parse(ScriptToken[] tokens, ref int tokenIndex, out string parseErrorMessage, out ScriptSyntaxTreeNode node)
		{
			node = null;
			parseErrorMessage = null;
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			int i;
			for (i = tokenIndex; i < tokens.Length; i++)
			{
				bool flag2 = tokens[i].Type == ScriptToken.TokenType.DoubleQuote;
				if (!flag2)
				{
					stringBuilder.Append(tokens[i].Value);
				}
				if (flag2 && i > tokenIndex)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				parseErrorMessage = "";
				return ScriptSyntaxTreeRule.ParseResult.Failed;
			}
			tokenIndex = i;
			node = new ScriptSyntaxTreeNode(this)
			{
				Payload = stringBuilder.ToString()
			};
			return ScriptSyntaxTreeRule.ParseResult.Success;
		}

		// Token: 0x0600B4E9 RID: 46313 RVA: 0x0037A18D File Offset: 0x0037838D
		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext evaluationContext, out object value)
		{
			ScriptContext.EncodingPolicy encodingPolicy = evaluationContext.EncodingPolicy;
			value = (string)node.Payload;
		}
	}
}
