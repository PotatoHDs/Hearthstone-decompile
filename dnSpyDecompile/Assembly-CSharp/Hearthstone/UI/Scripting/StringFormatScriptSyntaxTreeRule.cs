using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x0200104A RID: 4170
	public class StringFormatScriptSyntaxTreeRule : MethodScriptSyntaxTreeRule.Evaluator<StringFormatScriptSyntaxTreeRule>
	{
		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x0600B4E2 RID: 46306 RVA: 0x00379FA8 File Offset: 0x003781A8
		protected override string MethodSymbolInternal
		{
			get
			{
				return "strformat";
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x0600B4E3 RID: 46307 RVA: 0x00379FAF File Offset: 0x003781AF
		protected override Type[] ExpectedArgsInternal
		{
			get
			{
				return new Type[]
				{
					typeof(string)
				};
			}
		}

		// Token: 0x0600B4E4 RID: 46308 RVA: 0x00379FC4 File Offset: 0x003781C4
		public override void Evaluate(List<object> args, ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object outValue)
		{
			outValue = string.Empty;
			node.ValueType = typeof(string);
			if (args[0] == null)
			{
				if (!context.EditMode)
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Format string cannot be null!", Array.Empty<object>());
					context.Results.SetFailedNodeIfNoneExists(node, node);
				}
				return;
			}
			try
			{
				string text = args[0] as string;
				object[] args2 = args.GetRange(1, args.Count - 1).ToArray();
				string text2;
				if (GameStrings.TryGet(text, out text2))
				{
					outValue = GameStrings.FormatLocalizedString(text2, args2);
				}
				else
				{
					outValue = GameStrings.FormatLocalizedString(text, args2);
				}
			}
			catch (FormatException)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Check that the format string matches the number of arguments passed in.", Array.Empty<object>());
				context.Results.SetFailedNodeIfNoneExists(node, node);
			}
			catch (Exception ex)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Unexpected error performing string format!\n" + ex.Message, Array.Empty<object>());
				context.Results.SetFailedNodeIfNoneExists(node, node);
			}
		}
	}
}
