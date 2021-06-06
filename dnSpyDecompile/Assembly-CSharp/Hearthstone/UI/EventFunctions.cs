using System;
using Hearthstone.DataModels;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x0200100E RID: 4110
	public static class EventFunctions
	{
		// Token: 0x0600B2D8 RID: 45784 RVA: 0x00372734 File Offset: 0x00370934
		public static bool TriggerEvent(Transform target, string eventName, Widget.TriggerEventParameters parameters = default(Widget.TriggerEventParameters))
		{
			bool foundListeners = false;
			if (parameters.Payload != null)
			{
				EventDataModel eventDataModel = new EventDataModel();
				eventDataModel.SourceName = (parameters.SourceName ?? "UNKNOWN");
				eventDataModel.Payload = parameters.Payload;
				WidgetTemplate componentInParent = target.GetComponentInParent<WidgetTemplate>();
				if (componentInParent != null)
				{
					componentInParent.BindDataModel(eventDataModel, target.gameObject, true, false);
				}
			}
			SceneUtils.WalkSelfAndChildren(target, delegate(Transform child)
			{
				Component[] components = child.GetComponents<Component>();
				bool flag = false;
				bool flag2 = false;
				foreach (Component component in components)
				{
					IWidgetEventListener widgetEventListener = component as IWidgetEventListener;
					if (widgetEventListener != null)
					{
						flag |= widgetEventListener.EventReceived(eventName).Consumed;
						foundListeners = true;
					}
					if (!parameters.IgnorePlaymaker)
					{
						PlayMakerFSM playMakerFSM = component as PlayMakerFSM;
						if (playMakerFSM != null)
						{
							playMakerFSM.SendEvent(eventName);
							foundListeners = true;
						}
					}
					if (!flag)
					{
						WidgetInstance widgetInstance = component as WidgetInstance;
						if (widgetInstance != null)
						{
							flag2 = true;
							widgetInstance.TriggerEvent(eventName, parameters);
							foundListeners = true;
						}
					}
				}
				return !flag2 && !flag && !parameters.NoDownwardPropagation;
			});
			return foundListeners;
		}
	}
}
