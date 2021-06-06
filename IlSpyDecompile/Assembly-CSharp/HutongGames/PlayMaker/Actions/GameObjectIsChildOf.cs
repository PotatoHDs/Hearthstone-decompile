using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if a GameObject is a Child of another GameObject.")]
	public class GameObjectIsChildOf : FsmStateAction
	{
		[RequiredField]
		[Tooltip("GameObject to test.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("Is it a child of this GameObject?")]
		public FsmGameObject isChildOf;

		[Tooltip("Event to send if GameObject is a child.")]
		public FsmEvent trueEvent;

		[Tooltip("Event to send if GameObject is NOT a child.")]
		public FsmEvent falseEvent;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store result in a bool variable")]
		public FsmBool storeResult;

		public override void Reset()
		{
			gameObject = null;
			isChildOf = null;
			trueEvent = null;
			falseEvent = null;
			storeResult = null;
		}

		public override void OnEnter()
		{
			DoIsChildOf(base.Fsm.GetOwnerDefaultTarget(gameObject));
			Finish();
		}

		private void DoIsChildOf(GameObject go)
		{
			if (!(go == null) && isChildOf != null)
			{
				bool flag = go.transform.IsChildOf(isChildOf.Value.transform);
				storeResult.Value = flag;
				base.Fsm.Event(flag ? trueEvent : falseEvent);
			}
		}
	}
}
