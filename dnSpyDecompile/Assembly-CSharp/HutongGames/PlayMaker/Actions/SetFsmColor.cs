using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DBC RID: 3516
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Set the value of a Color Variable in another FSM.")]
	public class SetFsmColor : FsmStateAction
	{
		// Token: 0x0600A59D RID: 42397 RVA: 0x00346FF5 File Offset: 0x003451F5
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.setValue = null;
		}

		// Token: 0x0600A59E RID: 42398 RVA: 0x00347015 File Offset: 0x00345215
		public override void OnEnter()
		{
			this.DoSetFsmColor();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A59F RID: 42399 RVA: 0x0034702C File Offset: 0x0034522C
		private void DoSetFsmColor()
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
			FsmColor fsmColor = this.fsm.FsmVariables.GetFsmColor(this.variableName.Value);
			if (fsmColor != null)
			{
				fsmColor.Value = this.setValue.Value;
				return;
			}
			base.LogWarning("Could not find variable: " + this.variableName.Value);
		}

		// Token: 0x0600A5A0 RID: 42400 RVA: 0x00347129 File Offset: 0x00345329
		public override void OnUpdate()
		{
			this.DoSetFsmColor();
		}

		// Token: 0x04008C21 RID: 35873
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008C22 RID: 35874
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x04008C23 RID: 35875
		[RequiredField]
		[UIHint(UIHint.FsmColor)]
		[Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

		// Token: 0x04008C24 RID: 35876
		[RequiredField]
		[Tooltip("Set the value of the variable.")]
		public FsmColor setValue;

		// Token: 0x04008C25 RID: 35877
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;

		// Token: 0x04008C26 RID: 35878
		private GameObject goLastFrame;

		// Token: 0x04008C27 RID: 35879
		private string fsmNameLastFrame;

		// Token: 0x04008C28 RID: 35880
		private PlayMakerFSM fsm;
	}
}
