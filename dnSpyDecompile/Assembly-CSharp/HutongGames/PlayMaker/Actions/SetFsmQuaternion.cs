using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DC3 RID: 3523
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Set the value of a Quaternion Variable in another FSM.")]
	public class SetFsmQuaternion : FsmStateAction
	{
		// Token: 0x0600A5C0 RID: 42432 RVA: 0x003478B9 File Offset: 0x00345AB9
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.variableName = "";
			this.setValue = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A5C1 RID: 42433 RVA: 0x003478F0 File Offset: 0x00345AF0
		public override void OnEnter()
		{
			this.DoSetFsmQuaternion();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A5C2 RID: 42434 RVA: 0x00347908 File Offset: 0x00345B08
		private void DoSetFsmQuaternion()
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
			FsmQuaternion fsmQuaternion = this.fsm.FsmVariables.GetFsmQuaternion(this.variableName.Value);
			if (fsmQuaternion != null)
			{
				fsmQuaternion.Value = this.setValue.Value;
				return;
			}
			base.LogWarning("Could not find variable: " + this.variableName.Value);
		}

		// Token: 0x0600A5C3 RID: 42435 RVA: 0x00347A05 File Offset: 0x00345C05
		public override void OnUpdate()
		{
			this.DoSetFsmQuaternion();
		}

		// Token: 0x04008C59 RID: 35929
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008C5A RID: 35930
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x04008C5B RID: 35931
		[RequiredField]
		[UIHint(UIHint.FsmQuaternion)]
		[Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

		// Token: 0x04008C5C RID: 35932
		[RequiredField]
		[Tooltip("Set the value of the variable.")]
		public FsmQuaternion setValue;

		// Token: 0x04008C5D RID: 35933
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;

		// Token: 0x04008C5E RID: 35934
		private GameObject goLastFrame;

		// Token: 0x04008C5F RID: 35935
		private string fsmNameLastFrame;

		// Token: 0x04008C60 RID: 35936
		private PlayMakerFSM fsm;
	}
}
