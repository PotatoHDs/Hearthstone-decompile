using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DC2 RID: 3522
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Set the value of an Object Variable in another FSM.")]
	public class SetFsmObject : FsmStateAction
	{
		// Token: 0x0600A5BB RID: 42427 RVA: 0x00347765 File Offset: 0x00345965
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.variableName = "";
			this.setValue = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A5BC RID: 42428 RVA: 0x0034779C File Offset: 0x0034599C
		public override void OnEnter()
		{
			this.DoSetFsmBool();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A5BD RID: 42429 RVA: 0x003477B4 File Offset: 0x003459B4
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
			FsmObject fsmObject = this.fsm.FsmVariables.GetFsmObject(this.variableName.Value);
			if (fsmObject != null)
			{
				fsmObject.Value = this.setValue.Value;
				return;
			}
			base.LogWarning("Could not find variable: " + this.variableName.Value);
		}

		// Token: 0x0600A5BE RID: 42430 RVA: 0x003478B1 File Offset: 0x00345AB1
		public override void OnUpdate()
		{
			this.DoSetFsmBool();
		}

		// Token: 0x04008C51 RID: 35921
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008C52 RID: 35922
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x04008C53 RID: 35923
		[RequiredField]
		[UIHint(UIHint.FsmObject)]
		[Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

		// Token: 0x04008C54 RID: 35924
		[Tooltip("Set the value of the variable.")]
		public FsmObject setValue;

		// Token: 0x04008C55 RID: 35925
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;

		// Token: 0x04008C56 RID: 35926
		private GameObject goLastFrame;

		// Token: 0x04008C57 RID: 35927
		private string fsmNameLastFrame;

		// Token: 0x04008C58 RID: 35928
		private PlayMakerFSM fsm;
	}
}
