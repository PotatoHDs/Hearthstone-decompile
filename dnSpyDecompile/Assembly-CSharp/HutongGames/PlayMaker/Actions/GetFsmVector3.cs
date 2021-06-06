using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C66 RID: 3174
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Get the value of a Vector3 Variable from another FSM.")]
	public class GetFsmVector3 : FsmStateAction
	{
		// Token: 0x06009F57 RID: 40791 RVA: 0x0032E5D7 File Offset: 0x0032C7D7
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.storeValue = null;
		}

		// Token: 0x06009F58 RID: 40792 RVA: 0x0032E5F7 File Offset: 0x0032C7F7
		public override void OnEnter()
		{
			this.DoGetFsmVector3();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009F59 RID: 40793 RVA: 0x0032E60D File Offset: 0x0032C80D
		public override void OnUpdate()
		{
			this.DoGetFsmVector3();
		}

		// Token: 0x06009F5A RID: 40794 RVA: 0x0032E618 File Offset: 0x0032C818
		private void DoGetFsmVector3()
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
			FsmVector3 fsmVector = this.fsm.FsmVariables.GetFsmVector3(this.variableName.Value);
			if (fsmVector == null)
			{
				return;
			}
			this.storeValue.Value = fsmVector.Value;
		}

		// Token: 0x040084F2 RID: 34034
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x040084F3 RID: 34035
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x040084F4 RID: 34036
		[RequiredField]
		[UIHint(UIHint.FsmVector3)]
		public FsmString variableName;

		// Token: 0x040084F5 RID: 34037
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeValue;

		// Token: 0x040084F6 RID: 34038
		public bool everyFrame;

		// Token: 0x040084F7 RID: 34039
		private GameObject goLastFrame;

		// Token: 0x040084F8 RID: 34040
		private string fsmNameLastFrame;

		// Token: 0x040084F9 RID: 34041
		private PlayMakerFSM fsm;
	}
}
