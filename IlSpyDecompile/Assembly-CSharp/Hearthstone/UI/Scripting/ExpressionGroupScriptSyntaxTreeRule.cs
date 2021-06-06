using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	public class ExpressionGroupScriptSyntaxTreeRule : GenericExpressionGroupScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>
	{
		private DataModelList<object> m_matchingElements = new DataModelList<object>();

		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object outValue)
		{
			outValue = null;
			if (node.Left != null && node.Left.Rule == ScriptSyntaxTreeRule<AccessorScriptSyntaxTreeRule>.Get())
			{
				if (node.Left == null || !node.Left.Evaluate(context, out var value) || !(value is DataModelProperty.QueryDelegate) || !context.CheckFeatureIsSupported(ScriptFeatureFlags.Methods))
				{
					return;
				}
				IDataModelList dataModelList = node.Left.Target as IDataModelList;
				if (dataModelList == null)
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Only collections are queryable!");
					context.Results.SetFailedNodeIfNoneExists(node, node.Left);
					return;
				}
				m_matchingElements.Clear();
				m_matchingElements.DontUpdateDataVersionOnChange();
				if (context.EncodingPolicy != 0 || context.EditMode)
				{
					dataModelList.DontUpdateDataVersionOnChange();
					dataModelList.Clear();
					dataModelList.AddDefaultValue();
					m_matchingElements.Add(dataModelList.GetElementAtIndex(0));
				}
				if (context.QueryObjects == null)
				{
					context.QueryObjects = new List<object>();
				}
				context.QueryObjects.Add(null);
				for (int i = 0; i < dataModelList.Count; i++)
				{
					object elementAtIndex = dataModelList.GetElementAtIndex(i);
					context.QueryObjects[context.QueryObjects.Count - 1] = elementAtIndex;
					if (node.Right == null)
					{
						EmitLambdaSuggestion(context);
						context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Missing query!");
						context.Results.SetFailedNodeIfNoneExists(node, node.Right);
						return;
					}
					if (!node.Right.Evaluate(context, out node.Value))
					{
						EmitLambdaSuggestion(context);
						context.Results.SetFailedNodeIfNoneExists(node, node.Right);
						return;
					}
					if (object.Equals(node.Value, true))
					{
						m_matchingElements.Add(elementAtIndex);
					}
				}
				context.QueryObjects.RemoveAt(context.QueryObjects.Count - 1);
				DataModelProperty.QueryDelegate queryDelegate = (DataModelProperty.QueryDelegate)node.Left.Value;
				node.Value = (outValue = queryDelegate(m_matchingElements));
				node.ValueType = node.Value.GetType();
			}
			else if (node.Right == null)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Expression group cannot be empty!");
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
			}
			else if (!node.Right.Evaluate(context, out node.Value))
			{
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
			}
			else
			{
				node.ValueType = ((node.Value != null) ? node.Value.GetType() : null);
				outValue = node.Value;
			}
		}

		private void EmitLambdaSuggestion(ScriptContext.EvaluationContext context)
		{
			_ = context.SuggestionsEnabled;
		}
	}
}
