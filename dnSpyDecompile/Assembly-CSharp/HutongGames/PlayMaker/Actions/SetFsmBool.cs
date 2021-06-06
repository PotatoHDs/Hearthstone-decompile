using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DBB RID: 3515
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Set the value of a Bool Variable in another FSM.")]
	public class SetFsmBool : FsmStateAction
	{
		// Token: 0x0600A598 RID: 42392 RVA: 0x00346EB9 File Offset: 0x003450B9
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.setValue = null;
		}

		// Token: 0x0600A599 RID: 42393 RVA: 0x00346ED9 File Offset: 0x003450D9
		public override void OnEnter()
		{
			this.DoSetFsmBool();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A59A RID: 42394 RVA: 0x00346EF0 File Offset: 0x003450F0
		private void DoSetFsmBool()
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
			FsmBool fsmBool = this.fsm.FsmVariables.FindFsmBool(this.variableName.Value);
			if (fsmBool != null)
			{
				fsmBool.Value = this.setValue.Value;
				return;
			}
			base.LogWarning("Could not find variable: " + this.variableName.Value);
		}

		// Token: 0x0600A59B RID: 42395 RVA: 0x00346FED File Offset: 0x003451ED
		public override void OnUpdate()
		{
			this.DoSetFsmBool();
		}

		// Token: 0x04008C19 RID: 35865
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008C1A RID: 35866
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x04008C1B RID: 35867
		[RequiredField]
		[UIHint(UIHint.FsmBool)]
		[Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

		// Token: 0x04008C1C RID: 35868
		[RequiredField]
		[Tooltip("Set the value of the variable.")]
		public FsmBool setValue;

		// Token: 0x04008C1D RID: 35869
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;

		// Token: 0x04008C1E RID: 35870
		private GameObject goLastFrame;

		// Token: 0x04008C1F RID: 35871
		private string fsmNameLastFrame;

		// Token: 0x04008C20 RID: 35872
		private PlayMakerFSM fsm;
	}
}
