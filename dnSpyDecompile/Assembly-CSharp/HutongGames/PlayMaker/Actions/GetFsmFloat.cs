using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C59 RID: 3161
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Get the value of a Float Variable from another FSM.")]
	public class GetFsmFloat : FsmStateAction
	{
		// Token: 0x06009F14 RID: 40724 RVA: 0x0032D6D3 File Offset: 0x0032B8D3
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.storeValue = null;
		}

		// Token: 0x06009F15 RID: 40725 RVA: 0x0032D6F3 File Offset: 0x0032B8F3
		public override void OnEnter()
		{
			this.DoGetFsmFloat();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009F16 RID: 40726 RVA: 0x0032D709 File Offset: 0x0032B909
		public override void OnUpdate()
		{
			this.DoGetFsmFloat();
		}

		// Token: 0x06009F17 RID: 40727 RVA: 0x0032D714 File Offset: 0x0032B914
		private void DoGetFsmFloat()
		{
			if (this.storeValue.IsNone)
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
			FsmFloat fsmFloat = this.fsm.FsmVariables.GetFsmFloat(this.variableName.Value);
			if (fsmFloat == null)
			{
				return;
			}
			this.storeValue.Value = fsmFloat.Value;
		}

		// Token: 0x0400848A RID: 33930
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400848B RID: 33931
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x0400848C RID: 33932
		[RequiredField]
		[UIHint(UIHint.FsmFloat)]
		public FsmString variableName;

		// Token: 0x0400848D RID: 33933
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeValue;

		// Token: 0x0400848E RID: 33934
		public bool everyFrame;

		// Token: 0x0400848F RID: 33935
		private GameObject goLastFrame;

		// Token: 0x04008490 RID: 33936
		private string fsmNameLastFrame;

		// Token: 0x04008491 RID: 33937
		private PlayMakerFSM fsm;
	}
}
