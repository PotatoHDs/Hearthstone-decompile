using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	public class StringFormatScriptSyntaxTreeRule : MethodScriptSyntaxTreeRule.Evaluator<StringFormatScriptSyntaxTreeRule>
	{
		protected override string MethodSymbolInternal => "strformat";

		protected override Type[] ExpectedArgsInternal => new Type[1] { typeof(string) };

		public override void Evaluate(List<object> args, ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object outValue)
		{
			outValue = string.Empty;
			node.ValueType = typeof(string);
			if (args[0] == null)
			{
				if (!context.EditMode)
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Format string cannot be null!");
					context.Results.SetFailedNodeIfNoneExists(node, node);
				}
				return;
			}
			try
			{
				string text = args[0] as string;
				object[] args2 = args.GetRange(1, args.Count - 1).ToArray();
				if (GameStrings.TryGet(text, out var localized))
				{
					outValue = GameStrings.FormatLocalizedString(localized, args2);
				}
				else
				{
					outValue = GameStrings.FormatLocalizedString(text, args2);
				}
			}
			catch (FormatException)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Check that the format string matches the number of arguments passed in.");
				context.Results.SetFailedNodeIfNoneExists(node, node);
			}
			catch (Exception ex2)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Unexpected error performing string format!\n" + ex2.Message);
				context.Results.SetFailedNodeIfNoneExists(node, node);
			}
		}
	}
}
