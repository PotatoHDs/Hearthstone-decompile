using System;
using Hearthstone.UI;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F9D RID: 3997
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event to the chosen game object. (No payload)")]
	public class WidgetSendEventAction : FsmStateAction
	{
		// Token: 0x0600AE07 RID: 44551 RVA: 0x00362F0D File Offset: 0x0036110D
		public override void Reset()
		{
			this.gameObject = null;
			this.eventName = null;
		}

		// Token: 0x0600AE08 RID: 44552 RVA: 0x00362F1D File Offset: 0x0036111D
		public override void OnEnter()
		{
			this.SendEvent();
			base.Finish();
		}

		// Token: 0x0600AE09 RID: 44553 RVA: 0x00362F2C File Offset: 0x0036112C
		private void SendEvent()
		{
			if (this.gameObject == null || this.gameObject.Value == null)
			{
				Debug.LogError("WidgetSendEventAction.SendEvent() - Game Object is null.");
				return;
			}
			if (this.eventName == null || this.eventName.Value == null)
			{
				Debug.LogError("WidgetSendEventAction.SendEvent() - Event Name is null.");
				return;
			}
			if (!EventFunctions.TriggerEvent(this.gameObject.Value.transform, this.eventName.Value, new Widget.TriggerEventParameters
			{
				SourceName = string.Format("Playmaker {0}: {1}", this.gameObject, base.State.Name),
				Payload = null,
				IgnorePlaymaker = true,
				NoDownwardPropagation = true
			}))
			{
				Debug.LogError(string.Format("WidgetSendEventAction.SendEvent() - Sending event '{0}' to '{1}' but no receivers were found", this.eventName, this.gameObject));
				return;
			}
		}

		// Token: 0x040094D1 RID: 38097
		[RequiredField]
		[Tooltip("Specify which game object to send the event.")]
		public FsmGameObject gameObject;

		// Token: 0x040094D2 RID: 38098
		[RequiredField]
		[Tooltip("Name of the event we're sending to the widget.")]
		public FsmString eventName;
	}
}
