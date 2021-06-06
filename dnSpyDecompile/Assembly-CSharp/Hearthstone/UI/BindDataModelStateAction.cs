using System;
using Hearthstone.UI.Scripting;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x0200101E RID: 4126
	public class BindDataModelStateAction : StateActionImplementation
	{
		// Token: 0x0600B335 RID: 45877 RVA: 0x003731B6 File Offset: 0x003713B6
		public override void Run(bool loadSynchronously = false)
		{
			base.GetOverride(0).RegisterReadyListener(new Action<object>(this.HandleReady), null);
		}

		// Token: 0x0600B336 RID: 45878 RVA: 0x003731D4 File Offset: 0x003713D4
		private void HandleReady(object unused)
		{
			base.GetOverride(0).RemoveReadyOrInactiveListener(new Action<object>(this.HandleReady));
			GameObject gameObject;
			if (!base.GetOverride(0).Resolve(out gameObject))
			{
				base.Complete(false);
				return;
			}
			ScriptString script = base.GetScript(0);
			if (string.IsNullOrEmpty(script.Script))
			{
				base.Complete(false);
				return;
			}
			if (this.m_valueContext == null)
			{
				this.m_valueContext = new ScriptContext();
			}
			ScriptContext.EvaluationResults evaluationResults = this.m_valueContext.Evaluate(script.Script, base.DataContext);
			if (evaluationResults.ErrorCode != ScriptContext.ErrorCodes.Success || evaluationResults.Value == null)
			{
				base.Complete(false);
				return;
			}
			IDataModelList dataModelList = evaluationResults.Value as IDataModelList;
			IDataModel dataModel;
			if (dataModelList != null)
			{
				dataModel = (dataModelList.GetElementAtIndex(0) as IDataModel);
			}
			else
			{
				dataModel = (evaluationResults.Value as IDataModel);
			}
			WidgetTemplate widgetTemplate = gameObject.GetComponent<WidgetTemplate>() ?? SceneUtils.FindComponentInParents<WidgetTemplate>(gameObject);
			if (dataModel == null || widgetTemplate == null)
			{
				base.Complete(false);
				return;
			}
			bool success = widgetTemplate.BindDataModel(dataModel, gameObject, true, false);
			base.Complete(success);
		}

		// Token: 0x0400966D RID: 38509
		private ScriptContext m_valueContext;
	}
}
