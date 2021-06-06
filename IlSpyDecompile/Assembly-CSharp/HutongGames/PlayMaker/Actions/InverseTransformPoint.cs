using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Transforms position from world space to a Game Object's local space. The opposite of TransformPoint.")]
	public class InverseTransformPoint : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		public FsmVector3 worldPosition;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeResult;

		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			worldPosition = null;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoInverseTransformPoint();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoInverseTransformPoint();
		}

		private void DoInverseTransformPoint()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!(ownerDefaultTarget == null))
			{
				storeResult.Value = ownerDefaultTarget.transform.InverseTransformPoint(worldPosition.Value);
			}
		}
	}
}
