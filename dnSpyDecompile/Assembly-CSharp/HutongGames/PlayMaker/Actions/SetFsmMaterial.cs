using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DC1 RID: 3521
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Set the value of a Material Variable in another FSM.")]
	public class SetFsmMaterial : FsmStateAction
	{
		// Token: 0x0600A5B6 RID: 42422 RVA: 0x00347611 File Offset: 0x00345811
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.variableName = "";
			this.setValue = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A5B7 RID: 42423 RVA: 0x00347648 File Offset: 0x00345848
		public override void OnEnter()
		{
			this.DoSetFsmBool();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A5B8 RID: 42424 RVA: 0x00347660 File Offset: 0x00345860
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
			FsmMaterial fsmMaterial = this.fsm.FsmVariables.GetFsmMaterial(this.variableName.Value);
			if (fsmMaterial != null)
			{
				fsmMaterial.Value = this.setValue.Value;
				return;
			}
			base.LogWarning("Could not find variable: " + this.variableName.Value);
		}

		// Token: 0x0600A5B9 RID: 42425 RVA: 0x0034775D File Offset: 0x0034595D
		public override void OnUpdate()
		{
			this.DoSetFsmBool();
		}

		// Token: 0x04008C49 RID: 35913
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008C4A RID: 35914
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x04008C4B RID: 35915
		[RequiredField]
		[UIHint(UIHint.FsmMaterial)]
		[Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

		// Token: 0x04008C4C RID: 35916
		[RequiredField]
		[Tooltip("Set the value of the variable.")]
		public FsmMaterial setValue;

		// Token: 0x04008C4D RID: 35917
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;

		// Token: 0x04008C4E RID: 35918
		private GameObject goLastFrame;

		// Token: 0x04008C4F RID: 35919
		private string fsmNameLastFrame;

		// Token: 0x04008C50 RID: 35920
		private PlayMakerFSM fsm;
	}
}
