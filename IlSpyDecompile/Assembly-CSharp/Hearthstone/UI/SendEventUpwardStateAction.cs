using Hearthstone.DataModels;
using Hearthstone.UI.Scripting;
using UnityEngine;

namespace Hearthstone.UI
{
	public class SendEventUpwardStateAction : StateActionImplementation
	{
		public override void Run(bool loadSynchronously = false)
		{
			GetOverride(0).RegisterReadyListener(HandleReady);
		}

		private void HandleReady(object unused)
		{
			GetOverride(0).RemoveReadyOrInactiveListener(HandleReady);
			Override @override = GetOverride(0);
			string @string = GetString(0);
			if (@override.Resolve(out var gameObject))
			{
				ScriptString script = GetScript(0);
				EventDataModel eventDataModel = null;
				if (!string.IsNullOrEmpty(script.Script))
				{
					string stateName = GetStateName();
					ScriptContext.EvaluationResults evaluationResults = new ScriptContext().Evaluate(script.Script, base.DataContext);
					eventDataModel = new EventDataModel();
					eventDataModel.Payload = evaluationResults.Value;
					eventDataModel.SourceName = stateName;
				}
				SendEventUpward(gameObject.gameObject, @string, eventDataModel);
			}
			Complete(success: true);
		}

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
						owningWidget.BindDataModel(eventData);
						widgetTemplate = owningWidget;
					}
				}
				IWidgetEventListener[] array = components;
				foreach (IWidgetEventListener widgetEventListener in array)
				{
					flag |= widgetEventListener.EventReceived(eventName).Consumed;
				}
				if (!flag)
				{
					transform = transform.parent;
					continue;
				}
				break;
			}
		}
	}
}
