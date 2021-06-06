using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Keep transform of a Game Object unchangable.")]
	public class SteadyTransform : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to take a global scale from.")]
		public FsmOwnerDefault rootFSMObject;

		[RequiredField]
		[Tooltip("The GameObject to set transform.")]
		public FsmOwnerDefault FSMObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("Use stored position Vector3 or an initial position of rootFSMObject will be used instead.")]
		public FsmVector3 positionVector;

		[Tooltip("Steady position.")]
		public bool steadyPosition;

		[Tooltip("Steady rotation.")]
		public bool steadyRotation;

		[Tooltip("Steady scale.")]
		public bool steadyScale;

		private Vector3 initialPosition;

		private bool isInitialPositionHasGot;

		public override void Reset()
		{
			rootFSMObject = null;
			FSMObject = null;
			positionVector = null;
			steadyPosition = false;
			steadyRotation = false;
			steadyScale = false;
		}

		public override void OnEnter()
		{
			DoTransform();
		}

		public override void OnLateUpdate()
		{
			DoTransform();
		}

		private void DoTransform()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(rootFSMObject);
			GameObject ownerDefaultTarget2 = base.Fsm.GetOwnerDefaultTarget(FSMObject);
			if (!(ownerDefaultTarget == null) && !(ownerDefaultTarget2 == null))
			{
				if (!isInitialPositionHasGot)
				{
					initialPosition = ownerDefaultTarget.transform.position;
					isInitialPositionHasGot = true;
				}
				initialPosition = (positionVector.IsNone ? initialPosition : positionVector.Value);
				if (steadyPosition)
				{
					ownerDefaultTarget2.transform.position = initialPosition;
				}
				if (steadyRotation)
				{
					ownerDefaultTarget2.transform.rotation = Quaternion.identity;
				}
				Vector3 lossyScale = ownerDefaultTarget.transform.lossyScale;
				ownerDefaultTarget2.transform.localScale = new Vector3(1f / lossyScale.x, 1f / lossyScale.y, 1f / lossyScale.z);
			}
		}
	}
}
