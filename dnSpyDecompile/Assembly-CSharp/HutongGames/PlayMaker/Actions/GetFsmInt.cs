using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C5B RID: 3163
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Get the value of an Integer Variable from another FSM.")]
	public class GetFsmInt : FsmStateAction
	{
		// Token: 0x06009F1E RID: 40734 RVA: 0x0032D8E7 File Offset: 0x0032BAE7
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.storeValue = null;
		}

		// Token: 0x06009F1F RID: 40735 RVA: 0x0032D907 File Offset: 0x0032BB07
		public override void OnEnter()
		{
			this.DoGetFsmInt();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009F20 RID: 40736 RVA: 0x0032D91D File Offset: 0x0032BB1D
		public override void OnUpdate()
		{
			this.DoGetFsmInt();
		}

		// Token: 0x06009F21 RID: 40737 RVA: 0x0032D928 File Offset: 0x0032BB28
		private void DoGetFsmInt()
		{
			if (this.storeValue == null)
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
				return;
			}
			FsmInt fsmInt = this.fsm.FsmVariables.GetFsmInt(this.variableName.Value);
			if (fsmInt == null)
			{
				return;
			}
			this.storeValue.Value = fsmInt.Value;
		}

		// Token: 0x0400849A RID: 33946
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400849B RID: 33947
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x0400849C RID: 33948
		[RequiredField]
		[UIHint(UIHint.FsmInt)]
		public FsmString variableName;

		// Token: 0x0400849D RID: 33949
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt storeValue;

		// Token: 0x0400849E RID: 33950
		public bool everyFrame;

		// Token: 0x0400849F RID: 33951
		private GameObject goLastFrame;

		// Token: 0x040084A0 RID: 33952
		private string fsmNameLastFrame;

		// Token: 0x040084A1 RID: 33953
		private PlayMakerFSM fsm;
	}
}
