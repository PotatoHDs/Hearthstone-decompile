using System;

namespace HutongGames.PlayMaker.Actions
{
	[Obsolete("This action is obsolete; use Send Event with Event Target instead.")]
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sends an Event to all FSMs in the scene or to all FSMs on a Game Object.\nNOTE: This action won't work on the very first frame of the game...")]
	public class BroadcastEvent : FsmStateAction
	{
		[RequiredField]
		public FsmString broadcastEvent;

		[Tooltip("Optionally specify a game object to broadcast the event to all FSMs on that game object.")]
		public FsmGameObject gameObject;

		[Tooltip("Broadcast to all FSMs on the game object's children.")]
		public FsmBool sendToChildren;

		public FsmBool excludeSelf;

		public override void Reset()
		{
			broadcastEvent = null;
			gameObject = null;
			sendToChildren = false;
			excludeSelf = false;
		}

		public override void OnEnter()
		{
			if (!string.IsNullOrEmpty(broadcastEvent.Value))
			{
				if (gameObject.Value != null)
				{
					base.Fsm.BroadcastEventToGameObject(gameObject.Value, broadcastEvent.Value, sendToChildren.Value, excludeSelf.Value);
				}
				else
				{
					base.Fsm.BroadcastEvent(broadcastEvent.Value, excludeSelf.Value);
				}
			}
			Finish();
		}
	}
}
