using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C56 RID: 3158
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Get the value of a Bool Variable from another FSM.")]
	public class GetFsmBool : FsmStateAction
	{
		// Token: 0x06009F05 RID: 40709 RVA: 0x0032D3BB File Offset: 0x0032B5BB
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.storeValue = null;
		}

		// Token: 0x06009F06 RID: 40710 RVA: 0x0032D3DB File Offset: 0x0032B5DB
		public override void OnEnter()
		{
			this.DoGetFsmBool();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009F07 RID: 40711 RVA: 0x0032D3F1 File Offset: 0x0032B5F1
		public override void OnUpdate()
		{
			this.DoGetFsmBool();
		}

		// Token: 0x06009F08 RID: 40712 RVA: 0x0032D3FC File Offset: 0x0032B5FC
		private void DoGetFsmBool()
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
			FsmBool fsmBool = this.fsm.FsmVariables.GetFsmBool(this.variableName.Value);
			if (fsmBool == null)
			{
				return;
			}
			this.storeValue.Value = fsmBool.Value;
		}

		// Token: 0x04008472 RID: 33906
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008473 RID: 33907
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x04008474 RID: 33908
		[RequiredField]
		[UIHint(UIHint.FsmBool)]
		public FsmString variableName;

		// Token: 0x04008475 RID: 33909
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmBool storeValue;

		// Token: 0x04008476 RID: 33910
		public bool everyFrame;

		// Token: 0x04008477 RID: 33911
		private GameObject goLastFrame;

		// Token: 0x04008478 RID: 33912
		private string fsmNameLastFrame;

		// Token: 0x04008479 RID: 33913
		private PlayMakerFSM fsm;
	}
}
