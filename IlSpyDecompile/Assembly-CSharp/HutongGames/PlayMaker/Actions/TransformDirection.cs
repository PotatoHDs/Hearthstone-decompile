using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Transforms a Direction from a Game Object's local space to world space.")]
	public class TransformDirection : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		public FsmVector3 localDirection;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeResult;

		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			localDirection = null;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoTransformDirection();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoTransformDirection();
		}

		private void DoTransformDirection()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!(ownerDefaultTarget == null))
			{
				storeResult.Value = ownerDefaultTarget.transform.TransformDirection(localDirection.Value);
			}
		}
	}
}
