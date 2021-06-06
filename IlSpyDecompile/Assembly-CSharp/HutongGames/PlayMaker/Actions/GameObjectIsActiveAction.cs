using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Tests if a Game Object is active.")]
	public class GameObjectIsActiveAction : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to test.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Event to send if the GameObject is active.")]
		public FsmEvent trueEvent;

		[Tooltip("Event to send if the GameObject is NOT active.")]
		public FsmEvent falseEvent;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a bool variable.")]
		public FsmBool storeResult;

		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			trueEvent = null;
			falseEvent = null;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoIsActive();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoIsActive();
		}

		private void DoIsActive()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				bool activeInHierarchy = ownerDefaultTarget.activeInHierarchy;
				storeResult.Value = activeInHierarchy;
				base.Fsm.Event(activeInHierarchy ? trueEvent : falseEvent);
			}
			else
			{
				Debug.LogError("FSM GameObjectIsActive Error: GameObject is Null!");
			}
		}
	}
}
