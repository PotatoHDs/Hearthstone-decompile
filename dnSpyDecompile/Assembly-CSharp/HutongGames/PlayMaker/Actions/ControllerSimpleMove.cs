using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BF3 RID: 3059
	[ActionCategory(ActionCategory.Character)]
	[Tooltip("Moves a Game Object with a Character Controller. Velocity along the y-axis is ignored. Speed is in meters/s. Gravity is automatically applied.")]
	public class ControllerSimpleMove : FsmStateAction
	{
		// Token: 0x06009D61 RID: 40289 RVA: 0x00328DFD File Offset: 0x00326FFD
		public override void Reset()
		{
			this.gameObject = null;
			this.moveVector = new FsmVector3
			{
				UseVariable = true
			};
			this.speed = 1f;
			this.space = Space.World;
		}

		// Token: 0x06009D62 RID: 40290 RVA: 0x00328E30 File Offset: 0x00327030
		public override void OnUpdate()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			if (ownerDefaultTarget != this.previousGo)
			{
				this.controller = ownerDefaultTarget.GetComponent<CharacterController>();
				this.previousGo = ownerDefaultTarget;
			}
			if (this.controller != null)
			{
				Vector3 a = (this.space == Space.World) ? this.moveVector.Value : ownerDefaultTarget.transform.TransformDirection(this.moveVector.Value);
				this.controller.SimpleMove(a * this.speed.Value);
			}
		}

		// Token: 0x040082D1 RID: 33489
		[RequiredField]
		[CheckForComponent(typeof(CharacterController))]
		[Tooltip("The GameObject to move.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040082D2 RID: 33490
		[RequiredField]
		[Tooltip("The movement vector.")]
		public FsmVector3 moveVector;

		// Token: 0x040082D3 RID: 33491
		[Tooltip("Multiply the movement vector by a speed factor.")]
		public FsmFloat speed;

		// Token: 0x040082D4 RID: 33492
		[Tooltip("Move in local or world space.")]
		public Space space;

		// Token: 0x040082D5 RID: 33493
		private GameObject previousGo;

		// Token: 0x040082D6 RID: 33494
		private CharacterController controller;
	}
}
