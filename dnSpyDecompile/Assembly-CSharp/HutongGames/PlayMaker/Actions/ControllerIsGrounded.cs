using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BF0 RID: 3056
	[ActionCategory(ActionCategory.Character)]
	[Tooltip("Tests if a Character Controller on a Game Object was touching the ground during the last move.")]
	public class ControllerIsGrounded : FsmStateAction
	{
		// Token: 0x06009D54 RID: 40276 RVA: 0x00328A6E File Offset: 0x00326C6E
		public override void Reset()
		{
			this.gameObject = null;
			this.trueEvent = null;
			this.falseEvent = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009D55 RID: 40277 RVA: 0x00328A93 File Offset: 0x00326C93
		public override void OnEnter()
		{
			this.DoControllerIsGrounded();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009D56 RID: 40278 RVA: 0x00328AA9 File Offset: 0x00326CA9
		public override void OnUpdate()
		{
			this.DoControllerIsGrounded();
		}

		// Token: 0x06009D57 RID: 40279 RVA: 0x00328AB4 File Offset: 0x00326CB4
		private void DoControllerIsGrounded()
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
			if (this.controller == null)
			{
				return;
			}
			bool isGrounded = this.controller.isGrounded;
			this.storeResult.Value = isGrounded;
			base.Fsm.Event(isGrounded ? this.trueEvent : this.falseEvent);
		}

		// Token: 0x040082BA RID: 33466
		[RequiredField]
		[CheckForComponent(typeof(CharacterController))]
		[Tooltip("The GameObject to check.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040082BB RID: 33467
		[Tooltip("Event to send if touching the ground.")]
		public FsmEvent trueEvent;

		// Token: 0x040082BC RID: 33468
		[Tooltip("Event to send if not touching the ground.")]
		public FsmEvent falseEvent;

		// Token: 0x040082BD RID: 33469
		[Tooltip("Store the result in a bool variable.")]
		[UIHint(UIHint.Variable)]
		public FsmBool storeResult;

		// Token: 0x040082BE RID: 33470
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		// Token: 0x040082BF RID: 33471
		private GameObject previousGo;

		// Token: 0x040082C0 RID: 33472
		private CharacterController controller;
	}
}
