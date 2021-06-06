using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C50 RID: 3152
	[ActionCategory(ActionCategory.Character)]
	[Tooltip("Gets the Collision Flags from a Character Controller on a Game Object. Collision flags give you a broad overview of where the character collided with any other object.")]
	public class GetControllerCollisionFlags : FsmStateAction
	{
		// Token: 0x06009EEA RID: 40682 RVA: 0x0032CCAC File Offset: 0x0032AEAC
		public override void Reset()
		{
			this.gameObject = null;
			this.isGrounded = null;
			this.none = null;
			this.sides = null;
			this.above = null;
			this.below = null;
		}

		// Token: 0x06009EEB RID: 40683 RVA: 0x0032CCD8 File Offset: 0x0032AED8
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
				this.isGrounded.Value = this.controller.isGrounded;
				this.none.Value = ((this.controller.collisionFlags & CollisionFlags.None) > CollisionFlags.None);
				this.sides.Value = ((this.controller.collisionFlags & CollisionFlags.Sides) > CollisionFlags.None);
				this.above.Value = ((this.controller.collisionFlags & CollisionFlags.Above) > CollisionFlags.None);
				this.below.Value = ((this.controller.collisionFlags & CollisionFlags.Below) > CollisionFlags.None);
			}
		}

		// Token: 0x04008445 RID: 33861
		[RequiredField]
		[CheckForComponent(typeof(CharacterController))]
		[Tooltip("The GameObject with a Character Controller component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008446 RID: 33862
		[UIHint(UIHint.Variable)]
		[Tooltip("True if the Character Controller capsule is on the ground")]
		public FsmBool isGrounded;

		// Token: 0x04008447 RID: 33863
		[UIHint(UIHint.Variable)]
		[Tooltip("True if no collisions in last move.")]
		public FsmBool none;

		// Token: 0x04008448 RID: 33864
		[UIHint(UIHint.Variable)]
		[Tooltip("True if the Character Controller capsule was hit on the sides.")]
		public FsmBool sides;

		// Token: 0x04008449 RID: 33865
		[UIHint(UIHint.Variable)]
		[Tooltip("True if the Character Controller capsule was hit from above.")]
		public FsmBool above;

		// Token: 0x0400844A RID: 33866
		[UIHint(UIHint.Variable)]
		[Tooltip("True if the Character Controller capsule was hit from below.")]
		public FsmBool below;

		// Token: 0x0400844B RID: 33867
		private GameObject previousGo;

		// Token: 0x0400844C RID: 33868
		private CharacterController controller;
	}
}
