using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DC5 RID: 3525
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Set the value of a String Variable in another FSM.")]
	public class SetFsmString : FsmStateAction
	{
		// Token: 0x0600A5CA RID: 42442 RVA: 0x00347B61 File Offset: 0x00345D61
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.setValue = null;
		}

		// Token: 0x0600A5CB RID: 42443 RVA: 0x00347B81 File Offset: 0x00345D81
		public override void OnEnter()
		{
			this.DoSetFsmString();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A5CC RID: 42444 RVA: 0x00347B98 File Offset: 0x00345D98
		private void DoSetFsmString()
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
			FsmString fsmString = this.fsm.FsmVariables.GetFsmString(this.variableName.Value);
			if (fsmString != null)
			{
				fsmString.Value = this.setValue.Value;
				return;
			}
			base.LogWarning("Could not find variable: " + this.variableName.Value);
		}

		// Token: 0x0600A5CD RID: 42445 RVA: 0x00347C95 File Offset: 0x00345E95
		public override void OnUpdate()
		{
			this.DoSetFsmString();
		}

		// Token: 0x04008C69 RID: 35945
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008C6A RID: 35946
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object.")]
		public FsmString fsmName;

		// Token: 0x04008C6B RID: 35947
		[RequiredField]
		[UIHint(UIHint.FsmString)]
		[Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

		// Token: 0x04008C6C RID: 35948
		[Tooltip("Set the value of the variable.")]
		public FsmString setValue;

		// Token: 0x04008C6D RID: 35949
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;

		// Token: 0x04008C6E RID: 35950
		private GameObject goLastFrame;

		// Token: 0x04008C6F RID: 35951
		private string fsmNameLastFrame;

		// Token: 0x04008C70 RID: 35952
		private PlayMakerFSM fsm;
	}
}
