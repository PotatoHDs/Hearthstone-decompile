using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DC9 RID: 3529
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Set the value of a Vector3 Variable in another FSM.")]
	public class SetFsmVector3 : FsmStateAction
	{
		// Token: 0x0600A5DE RID: 42462 RVA: 0x003480B1 File Offset: 0x003462B1
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.setValue = null;
		}

		// Token: 0x0600A5DF RID: 42463 RVA: 0x003480D1 File Offset: 0x003462D1
		public override void OnEnter()
		{
			this.DoSetFsmVector3();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A5E0 RID: 42464 RVA: 0x003480E8 File Offset: 0x003462E8
		private void DoSetFsmVector3()
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
			FsmVector3 fsmVector = this.fsm.FsmVariables.GetFsmVector3(this.variableName.Value);
			if (fsmVector != null)
			{
				fsmVector.Value = this.setValue.Value;
				return;
			}
			base.LogWarning("Could not find variable: " + this.variableName.Value);
		}

		// Token: 0x0600A5E1 RID: 42465 RVA: 0x003481E5 File Offset: 0x003463E5
		public override void OnUpdate()
		{
			this.DoSetFsmVector3();
		}

		// Token: 0x04008C8B RID: 35979
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008C8C RID: 35980
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x04008C8D RID: 35981
		[RequiredField]
		[UIHint(UIHint.FsmVector3)]
		[Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

		// Token: 0x04008C8E RID: 35982
		[RequiredField]
		[Tooltip("Set the value of the variable.")]
		public FsmVector3 setValue;

		// Token: 0x04008C8F RID: 35983
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;

		// Token: 0x04008C90 RID: 35984
		private GameObject goLastFrame;

		// Token: 0x04008C91 RID: 35985
		private string fsmNameLastFrame;

		// Token: 0x04008C92 RID: 35986
		private PlayMakerFSM fsm;
	}
}
