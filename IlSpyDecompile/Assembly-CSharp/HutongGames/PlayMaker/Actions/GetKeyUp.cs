using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Sends an Event when a Key is released.")]
	public class GetKeyUp : FsmStateAction
	{
		[RequiredField]
		public KeyCode key;

		public FsmEvent sendEvent;

		[UIHint(UIHint.Variable)]
		public FsmBool storeResult;

		public override void Reset()
		{
			sendEvent = null;
			key = KeyCode.None;
			storeResult = null;
		}

		public override void OnUpdate()
		{
			bool keyUp = Input.GetKeyUp(key);
			if (keyUp)
			{
				base.Fsm.Event(sendEvent);
			}
			storeResult.Value = keyUp;
		}
	}
}
