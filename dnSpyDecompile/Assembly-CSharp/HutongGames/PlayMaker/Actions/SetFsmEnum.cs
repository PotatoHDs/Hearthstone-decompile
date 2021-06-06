using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DBD RID: 3517
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Set the value of a String Variable in another FSM.")]
	public class SetFsmEnum : FsmStateAction
	{
		// Token: 0x0600A5A2 RID: 42402 RVA: 0x00347131 File Offset: 0x00345331
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.setValue = null;
		}

		// Token: 0x0600A5A3 RID: 42403 RVA: 0x00347151 File Offset: 0x00345351
		public override void OnEnter()
		{
			this.DoSetFsmEnum();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A5A4 RID: 42404 RVA: 0x00347168 File Offset: 0x00345368
		private void DoSetFsmEnum()
		{
			if (this.setValue == null)
			{
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			if (ownerDefaultTarget != this.goLastFrame || this.fsmName.Value != this.fsmNameLastFrame)
			{
				this.goLastFrame = ownerDefaultTarget;
				this.fsmNameLastFrame = this.fsmName.Value;
				this.fsm = ActionHelpers.GetGameObjectFsm(ownerDefaultTarget, this.fsmName.Value);
			}
			if (this.fsm == null)
			{
				base.LogWarning("Could not find FSM: " + this.fsmName.Value);
				return;
			}
			FsmEnum fsmEnum = this.fsm.FsmVariables.GetFsmEnum(this.variableName.Value);
			if (fsmEnum != null)
			{
				fsmEnum.Value = this.setValue.Value;
				return;
			}
			base.LogWarning("Could not find variable: " + this.variableName.Value);
		}

		// Token: 0x0600A5A5 RID: 42405 RVA: 0x00347265 File Offset: 0x00345465
		public override void OnUpdate()
		{
			this.DoSetFsmEnum();
		}

		// Token: 0x04008C29 RID: 35881
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008C2A RID: 35882
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object.")]
		public FsmString fsmName;

		// Token: 0x04008C2B RID: 35883
		[RequiredField]
		[UIHint(UIHint.FsmEnum)]
		[Tooltip("Enum variable name needs to match the FSM variable name on Game Object.")]
		public FsmString variableName;

		// Token: 0x04008C2C RID: 35884
		[RequiredField]
		public FsmEnum setValue;

		// Token: 0x04008C2D RID: 35885
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;

		// Token: 0x04008C2E RID: 35886
		private GameObject goLastFrame;

		// Token: 0x04008C2F RID: 35887
		private string fsmNameLastFrame;

		// Token: 0x04008C30 RID: 35888
		private PlayMakerFSM fsm;
	}
}
