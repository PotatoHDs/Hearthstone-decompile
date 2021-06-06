using System;

namespace Hearthstone.UI.Scripting
{
	public class IndexerScriptSyntaxTreeRule : GenericExpressionGroupScriptSyntaxTreeRule<IndexerScriptSyntaxTreeRule>
	{
		public IndexerScriptSyntaxTreeRule()
			: base(ScriptToken.TokenType.OpenSquareBrackets, ScriptToken.TokenType.ClosedSquareBrackets)
		{
		}

		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object outValue)
		{
			outValue = null;
			if (!context.CheckFeatureIsSupported(ScriptFeatureFlags.Identifiers))
			{
				return;
			}
			if (node.Left == null || !node.Left.Evaluate(context, out var value) || value == null)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Element indexed is null.");
				context.Results.SetFailedNodeIfNoneExists(node, node.Left);
				return;
			}
			IDataModelList dataModelList = value as IDataModelList;
			if (dataModelList == null)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Only collections can be indexed.");
				context.Results.SetFailedNodeIfNoneExists(node, node.Left);
				return;
			}
			if (context.EditMode)
			{
				dataModelList.DontUpdateDataVersionOnChange();
			}
			if (node.Right == null || !node.Right.Evaluate(context, out var value2))
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Index contains errors.");
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			IConvertible convertible = value2 as IConvertible;
			if (convertible != null)
			{
				int num;
				try
				{
					num = convertible.ToInt32(null);
				}
				catch (Exception ex)
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, context.EditMode ? ex.ToString() : null);
					context.Results.SetFailedNodeIfNoneExists(node, node.Right);
					return;
				}
				if (num < 0)
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Index must be a positive integer.");
					context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				}
				else if (num < dataModelList.Count)
				{
					outValue = dataModelList.GetElementAtIndex(num);
					node.ValueType = ((outValue != null) ? outValue.GetType() : null);
				}
			}
		}
	}
}
