using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DC0 RID: 3520
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Set the value of an Integer Variable in another FSM.")]
	public class SetFsmInt : FsmStateAction
	{
		// Token: 0x0600A5B1 RID: 42417 RVA: 0x003474D4 File Offset: 0x003456D4
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.setValue = null;
		}

		// Token: 0x0600A5B2 RID: 42418 RVA: 0x003474F4 File Offset: 0x003456F4
		public override void OnEnter()
		{
			this.DoSetFsmInt();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A5B3 RID: 42419 RVA: 0x0034750C File Offset: 0x0034570C
		private void DoSetFsmInt()
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
			FsmInt fsmInt = this.fsm.FsmVariables.GetFsmInt(this.variableName.Value);
			if (fsmInt != null)
			{
				fsmInt.Value = this.setValue.Value;
				return;
			}
			base.LogWarning("Could not find variable: " + this.variableName.Value);
		}

		// Token: 0x0600A5B4 RID: 42420 RVA: 0x00347609 File Offset: 0x00345809
		public override void OnUpdate()
		{
			this.DoSetFsmInt();
		}

		// Token: 0x04008C41 RID: 35905
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008C42 RID: 35906
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x04008C43 RID: 35907
		[RequiredField]
		[UIHint(UIHint.FsmInt)]
		[Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

		// Token: 0x04008C44 RID: 35908
		[RequiredField]
		[Tooltip("Set the value of the variable.")]
		public FsmInt setValue;

		// Token: 0x04008C45 RID: 35909
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;

		// Token: 0x04008C46 RID: 35910
		private GameObject goLastFrame;

		// Token: 0x04008C47 RID: 35911
		private string fsmNameLastFrame;

		// Token: 0x04008C48 RID: 35912
		private PlayMakerFSM fsm;
	}
}
