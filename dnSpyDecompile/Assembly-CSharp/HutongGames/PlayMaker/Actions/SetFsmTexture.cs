using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DC6 RID: 3526
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Set the value of a Texture Variable in another FSM.")]
	public class SetFsmTexture : FsmStateAction
	{
		// Token: 0x0600A5CF RID: 42447 RVA: 0x00347C9D File Offset: 0x00345E9D
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.variableName = "";
			this.setValue = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A5D0 RID: 42448 RVA: 0x00347CD4 File Offset: 0x00345ED4
		public override void OnEnter()
		{
			this.DoSetFsmTexture();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A5D1 RID: 42449 RVA: 0x00347CEC File Offset: 0x00345EEC
		private void DoSetFsmTexture()
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
			FsmTexture fsmTexture = this.fsm.FsmVariables.FindFsmTexture(this.variableName.Value);
			if (fsmTexture != null)
			{
				fsmTexture.Value = this.setValue.Value;
				return;
			}
			base.LogWarning("Could not find variable: " + this.variableName.Value);
		}

		// Token: 0x0600A5D2 RID: 42450 RVA: 0x00347DE9 File Offset: 0x00345FE9
		public override void OnUpdate()
		{
			this.DoSetFsmTexture();
		}

		// Token: 0x04008C71 RID: 35953
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008C72 RID: 35954
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x04008C73 RID: 35955
		[RequiredField]
		[UIHint(UIHint.FsmTexture)]
		[Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

		// Token: 0x04008C74 RID: 35956
		[Tooltip("Set the value of the variable.")]
		public FsmTexture setValue;

		// Token: 0x04008C75 RID: 35957
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;

		// Token: 0x04008C76 RID: 35958
		private GameObject goLastFrame;

		// Token: 0x04008C77 RID: 35959
		private string fsmNameLastFrame;

		// Token: 0x04008C78 RID: 35960
		private PlayMakerFSM fsm;
	}
}
