using System;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x02001043 RID: 4163
	public class IndexerScriptSyntaxTreeRule : GenericExpressionGroupScriptSyntaxTreeRule<IndexerScriptSyntaxTreeRule>
	{
		// Token: 0x0600B4BB RID: 46267 RVA: 0x00379261 File Offset: 0x00377461
		public IndexerScriptSyntaxTreeRule() : base(ScriptToken.TokenType.OpenSquareBrackets, ScriptToken.TokenType.ClosedSquareBrackets)
		{
		}

		// Token: 0x0600B4BC RID: 46268 RVA: 0x00379270 File Offset: 0x00377470
		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object outValue)
		{
			outValue = null;
			if (!context.CheckFeatureIsSupported(ScriptFeatureFlags.Identifiers))
			{
				return;
			}
			object obj;
			if (node.Left == null || !node.Left.Evaluate(context, out obj) || obj == null)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Element indexed is null.", Array.Empty<object>());
				context.Results.SetFailedNodeIfNoneExists(node, node.Left);
				return;
			}
			IDataModelList dataModelList = obj as IDataModelList;
			if (dataModelList == null)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Only collections can be indexed.", Array.Empty<object>());
				context.Results.SetFailedNodeIfNoneExists(node, node.Left);
				return;
			}
			if (context.EditMode)
			{
				dataModelList.DontUpdateDataVersionOnChange();
			}
			object obj2;
			if (node.Right == null || !node.Right.Evaluate(context, out obj2))
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Index contains errors.", Array.Empty<object>());
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			IConvertible convertible = obj2 as IConvertible;
			if (convertible == null)
			{
				return;
			}
			int num;
			try
			{
				num = convertible.ToInt32(null);
			}
			catch (Exception ex)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, context.EditMode ? ex.ToString() : null, Array.Empty<object>());
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			if (num < 0)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Index must be a positive integer.", Array.Empty<object>());
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			if (num >= dataModelList.Count)
			{
				return;
			}
			outValue = dataModelList.GetElementAtIndex(num);
			node.ValueType = ((outValue != null) ? outValue.GetType() : null);
		}
	}
}
