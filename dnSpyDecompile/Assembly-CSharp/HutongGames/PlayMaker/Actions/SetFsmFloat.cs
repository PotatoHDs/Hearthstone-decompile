using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DBE RID: 3518
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Set the value of a Float Variable in another FSM.")]
	public class SetFsmFloat : FsmStateAction
	{
		// Token: 0x0600A5A7 RID: 42407 RVA: 0x0034726D File Offset: 0x0034546D
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.setValue = null;
		}

		// Token: 0x0600A5A8 RID: 42408 RVA: 0x0034728D File Offset: 0x0034548D
		public override void OnEnter()
		{
			this.DoSetFsmFloat();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A5A9 RID: 42409 RVA: 0x003472A4 File Offset: 0x003454A4
		private void DoSetFsmFloat()
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
			FsmFloat fsmFloat = this.fsm.FsmVariables.GetFsmFloat(this.variableName.Value);
			if (fsmFloat != null)
			{
				fsmFloat.Value = this.setValue.Value;
				return;
			}
			base.LogWarning("Could not find variable: " + this.variableName.Value);
		}

		// Token: 0x0600A5AA RID: 42410 RVA: 0x003473A1 File Offset: 0x003455A1
		public override void OnUpdate()
		{
			this.DoSetFsmFloat();
		}

		// Token: 0x04008C31 RID: 35889
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008C32 RID: 35890
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x04008C33 RID: 35891
		[RequiredField]
		[UIHint(UIHint.FsmFloat)]
		[Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

		// Token: 0x04008C34 RID: 35892
		[RequiredField]
		[Tooltip("Set the value of the variable.")]
		public FsmFloat setValue;

		// Token: 0x04008C35 RID: 35893
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;

		// Token: 0x04008C36 RID: 35894
		private GameObject goLastFrame;

		// Token: 0x04008C37 RID: 35895
		private string fsmNameLastFrame;

		// Token: 0x04008C38 RID: 35896
		private PlayMakerFSM fsm;
	}
}
