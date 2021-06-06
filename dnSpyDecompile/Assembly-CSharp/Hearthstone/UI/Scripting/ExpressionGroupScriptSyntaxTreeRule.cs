using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x02001040 RID: 4160
	public class ExpressionGroupScriptSyntaxTreeRule : GenericExpressionGroupScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>
	{
		// Token: 0x0600B4AD RID: 46253 RVA: 0x00378C68 File Offset: 0x00376E68
		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object outValue)
		{
			outValue = null;
			if (node.Left != null && node.Left.Rule == ScriptSyntaxTreeRule<AccessorScriptSyntaxTreeRule>.Get())
			{
				object obj;
				if (node.Left == null || !node.Left.Evaluate(context, out obj) || !(obj is DataModelProperty.QueryDelegate))
				{
					return;
				}
				if (!context.CheckFeatureIsSupported(ScriptFeatureFlags.Methods))
				{
					return;
				}
				IDataModelList dataModelList = node.Left.Target as IDataModelList;
				if (dataModelList == null)
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Only collections are queryable!", Array.Empty<object>());
					context.Results.SetFailedNodeIfNoneExists(node, node.Left);
					return;
				}
				this.m_matchingElements.Clear();
				this.m_matchingElements.DontUpdateDataVersionOnChange();
				if (context.EncodingPolicy != ScriptContext.EncodingPolicy.None || context.EditMode)
				{
					dataModelList.DontUpdateDataVersionOnChange();
					dataModelList.Clear();
					dataModelList.AddDefaultValue();
					this.m_matchingElements.Add(dataModelList.GetElementAtIndex(0));
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
						this.EmitLambdaSuggestion(context);
						context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Missing query!", Array.Empty<object>());
						context.Results.SetFailedNodeIfNoneExists(node, node.Right);
						return;
					}
					if (!node.Right.Evaluate(context, out node.Value))
					{
						this.EmitLambdaSuggestion(context);
						context.Results.SetFailedNodeIfNoneExists(node, node.Right);
						return;
					}
					if (object.Equals(node.Value, true))
					{
						this.m_matchingElements.Add(elementAtIndex);
					}
				}
				context.QueryObjects.RemoveAt(context.QueryObjects.Count - 1);
				DataModelProperty.QueryDelegate queryDelegate = (DataModelProperty.QueryDelegate)node.Left.Value;
				object value;
				outValue = (value = queryDelegate(this.m_matchingElements));
				node.Value = value;
				node.ValueType = node.Value.GetType();
				return;
			}
			else
			{
				if (node.Right == null)
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Expression group cannot be empty!", Array.Empty<object>());
					context.Results.SetFailedNodeIfNoneExists(node, node.Right);
					return;
				}
				if (!node.Right.Evaluate(context, out node.Value))
				{
					context.Results.SetFailedNodeIfNoneExists(node, node.Right);
					return;
				}
				node.ValueType = ((node.Value != null) ? node.Value.GetType() : null);
				outValue = node.Value;
				return;
			}
		}

		// Token: 0x0600B4AE RID: 46254 RVA: 0x00378EF5 File Offset: 0x003770F5
		private void EmitLambdaSuggestion(ScriptContext.EvaluationContext context)
		{
			bool suggestionsEnabled = context.SuggestionsEnabled;
		}

		// Token: 0x040096EE RID: 38638
		private DataModelList<object> m_matchingElements = new DataModelList<object>();
	}
}
