using System;
using Hearthstone.DataModels;
using Hearthstone.UI.Scripting;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001025 RID: 4133
	public class SendEventUpwardStateAction : StateActionImplementation
	{
		// Token: 0x0600B34D RID: 45901 RVA: 0x00373784 File Offset: 0x00371984
		public override void Run(bool loadSynchronously = false)
		{
			base.GetOverride(0).RegisterReadyListener(new Action<object>(this.HandleReady), null);
		}

		// Token: 0x0600B34E RID: 45902 RVA: 0x003737A0 File Offset: 0x003719A0
		private void HandleReady(object unused)
		{
			base.GetOverride(0).RemoveReadyOrInactiveListener(new Action<object>(this.HandleReady));
			Override @override = base.GetOverride(0);
			string @string = base.GetString(0);
			GameObject gameObject;
			if (@override.Resolve(out gameObject))
			{
				ScriptString script = base.GetScript(0);
				EventDataModel eventDataModel = null;
				if (!string.IsNullOrEmpty(script.Script))
				{
					string stateName = base.GetStateName();
					ScriptContext.EvaluationResults evaluationResults = new ScriptContext().Evaluate(script.Script, base.DataContext);
					eventDataModel = new EventDataModel();
					eventDataModel.Payload = evaluationResults.Value;
					eventDataModel.SourceName = stateName;
				}
				SendEventUpwardStateAction.SendEventUpward(gameObject.gameObject, @string, eventDataModel);
			}
			base.Complete(true);
		}

		// Token: 0x0600B34F RID: 45903 RVA: 0x00373844 File Offset: 0x00371A44
		public static void SendEventUpward(GameObject baseObject, string eventName, EventDataModel eventData = null)
		{
			Transform transform = baseObject.transform;
			WidgetTemplate widgetTemplate = null;
			while (transform != null)
			{
				IWidgetEventListener[] components = transform.GetComponents<IWidgetEventListener>();
				bool flag = false;
				if (components.Length != 0 && components[0] != null)
				{
					WidgetTemplate owningWidget = components[0].OwningWidget;
					if (eventData != null && owningWidget != null)
					{
						if (widgetTemplate != null)
						{
							widgetTemplate.UnbindDataModel(120);
						}
						owningWidget.BindDataModel(eventData, false);
						widgetTemplate = owningWidget;
					}
				}
				foreach (IWidgetEventListener widgetEventListener in components)
				{
					flag |= widgetEventListener.EventReceived(eventName).Consumed;
				}
				if (flag)
				{
					break;
				}
				transform = transform.parent;
			}
		}
	}
}
