using Hearthstone.UI;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event to the chosen game object. (No payload)")]
	public class WidgetSendEventAction : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Specify which game object to send the event.")]
		public FsmGameObject gameObject;

		[RequiredField]
		[Tooltip("Name of the event we're sending to the widget.")]
		public FsmString eventName;

		public override void Reset()
		{
			gameObject = null;
			eventName = null;
		}

		public override void OnEnter()
		{
			SendEvent();
			Finish();
		}

		private void SendEvent()
		{
			if (gameObject == null || gameObject.Value == null)
			{
				Debug.LogError("WidgetSendEventAction.SendEvent() - Game Object is null.");
			}
			else if (eventName == null || eventName.Value == null)
			{
				Debug.LogError("WidgetSendEventAction.SendEvent() - Event Name is null.");
			}
			else if (!EventFunctions.TriggerEvent(gameObject.Value.transform, eventName.Value, new Widget.TriggerEventParameters
			{
				SourceName = $"Playmaker {gameObject}: {base.State.Name}",
				Payload = null,
				IgnorePlaymaker = true,
				NoDownwardPropagation = true
			}))
			{
				Debug.LogError($"WidgetSendEventAction.SendEvent() - Sending event '{eventName}' to '{gameObject}' but no receivers were found");
			}
		}
	}
}
