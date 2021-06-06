using Hearthstone.UI.Scripting;

namespace Hearthstone.UI
{
	public class BindDataModelStateAction : StateActionImplementation
	{
		private ScriptContext m_valueContext;

		public override void Run(bool loadSynchronously = false)
		{
			GetOverride(0).RegisterReadyListener(HandleReady);
		}

		private void HandleReady(object unused)
		{
			GetOverride(0).RemoveReadyOrInactiveListener(HandleReady);
			if (!GetOverride(0).Resolve(out var gameObject))
			{
				Complete(success: false);
				return;
			}
			ScriptString script = GetScript(0);
			if (string.IsNullOrEmpty(script.Script))
			{
				Complete(success: false);
				return;
			}
			if (m_valueContext == null)
			{
				m_valueContext = new ScriptContext();
			}
			ScriptContext.EvaluationResults evaluationResults = m_valueContext.Evaluate(script.Script, base.DataContext);
			if (evaluationResults.ErrorCode != 0 || evaluationResults.Value == null)
			{
				Complete(success: false);
				return;
			}
			IDataModelList dataModelList = evaluationResults.Value as IDataModelList;
			IDataModel dataModel = ((dataModelList == null) ? (evaluationResults.Value as IDataModel) : (dataModelList.GetElementAtIndex(0) as IDataModel));
			WidgetTemplate widgetTemplate = gameObject.GetComponent<WidgetTemplate>() ?? SceneUtils.FindComponentInParents<WidgetTemplate>(gameObject);
			if (dataModel == null || widgetTemplate == null)
			{
				Complete(success: false);
				return;
			}
			bool success = widgetTemplate.BindDataModel(dataModel, gameObject);
			Complete(success);
		}
	}
}
