using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BF1 RID: 3057
	[ActionCategory(ActionCategory.Character)]
	[Tooltip("Moves a Game Object with a Character Controller. See also Controller Simple Move. NOTE: It is recommended that you make only one call to Move or SimpleMove per frame.")]
	public class ControllerMove : FsmStateAction
	{
		// Token: 0x06009D59 RID: 40281 RVA: 0x00328B41 File Offset: 0x00326D41
		public override void Reset()
		{
			this.gameObject = null;
			this.moveVector = new FsmVector3
			{
				UseVariable = true
			};
			this.space = Space.World;
			this.perSecond = true;
		}

		// Token: 0x06009D5A RID: 40282 RVA: 0x00328B70 File Offset: 0x00326D70
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
				Vector3 vector = (this.space == Space.World) ? this.moveVector.Value : ownerDefaultTarget.transform.TransformDirection(this.moveVector.Value);
				if (this.perSecond.Value)
				{
					this.controller.Move(vector * Time.deltaTime);
					return;
				}
				this.controller.Move(vector);
			}
		}

		// Token: 0x040082C1 RID: 33473
		[RequiredField]
		[CheckForComponent(typeof(CharacterController))]
		[Tooltip("The GameObject to move.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040082C2 RID: 33474
		[RequiredField]
		[Tooltip("The movement vector.")]
		public FsmVector3 moveVector;

		// Token: 0x040082C3 RID: 33475
		[Tooltip("Move in local or word space.")]
		public Space space;

		// Token: 0x040082C4 RID: 33476
		[Tooltip("Movement vector is defined in units per second. Makes movement frame rate independent.")]
		public FsmBool perSecond;

		// Token: 0x040082C5 RID: 33477
		private GameObject previousGo;

		// Token: 0x040082C6 RID: 33478
		private CharacterController controller;
	}
}
