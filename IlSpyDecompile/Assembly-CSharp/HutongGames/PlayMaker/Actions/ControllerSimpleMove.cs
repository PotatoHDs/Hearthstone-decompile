using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Character)]
	[Tooltip("Moves a Game Object with a Character Controller. Velocity along the y-axis is ignored. Speed is in meters/s. Gravity is automatically applied.")]
	public class ControllerSimpleMove : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(CharacterController))]
		[Tooltip("The GameObject to move.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The movement vector.")]
		public FsmVector3 moveVector;

		[Tooltip("Multiply the movement vector by a speed factor.")]
		public FsmFloat speed;

		[Tooltip("Move in local or world space.")]
		public Space space;

		private GameObject previousGo;

		private CharacterController controller;

		public override void Reset()
		{
			gameObject = null;
			moveVector = new FsmVector3
			{
				UseVariable = true
			};
			speed = 1f;
			space = Space.World;
		}

		public override void OnUpdate()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!(ownerDefaultTarget == null))
			{
				if (ownerDefaultTarget != previousGo)
				{
					controller = ownerDefaultTarget.GetComponent<CharacterController>();
					previousGo = ownerDefaultTarget;
				}
				if (controller != null)
				{
					Vector3 vector = ((space == Space.World) ? moveVector.Value : ownerDefaultTarget.transform.TransformDirection(moveVector.Value));
					controller.SimpleMove(vector * speed.Value);
				}
			}
		}
	}
}
