using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Sends events based on Touch Phases. Optionally filter by a fingerID.")]
	public class TouchEvent : FsmStateAction
	{
		public FsmInt fingerId;

		public TouchPhase touchPhase;

		public FsmEvent sendEvent;

		[UIHint(UIHint.Variable)]
		public FsmInt storeFingerId;

		public override void Reset()
		{
			fingerId = new FsmInt
			{
				UseVariable = true
			};
			storeFingerId = null;
		}

		public override void OnUpdate()
		{
			if (Input.touchCount <= 0)
			{
				return;
			}
			Touch[] touches = Input.touches;
			for (int i = 0; i < touches.Length; i++)
			{
				Touch touch = touches[i];
				if ((fingerId.IsNone || touch.fingerId == fingerId.Value) && touch.phase == touchPhase)
				{
					storeFingerId.Value = touch.fingerId;
					base.Fsm.Event(sendEvent);
				}
			}
		}
	}
}
