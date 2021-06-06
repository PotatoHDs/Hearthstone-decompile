using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C5E RID: 3166
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Get the value of a Quaternion Variable from another FSM.")]
	public class GetFsmQuaternion : FsmStateAction
	{
		// Token: 0x06009F2D RID: 40749 RVA: 0x0032DC25 File Offset: 0x0032BE25
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.variableName = "";
			this.storeValue = null;
			this.everyFrame = false;
		}

		// Token: 0x06009F2E RID: 40750 RVA: 0x0032DC5C File Offset: 0x0032BE5C
		public override void OnEnter()
		{
			this.DoGetFsmVariable();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009F2F RID: 40751 RVA: 0x0032DC72 File Offset: 0x0032BE72
		public override void OnUpdate()
		{
			this.DoGetFsmVariable();
		}

		// Token: 0x06009F30 RID: 40752 RVA: 0x0032DC7C File Offset: 0x0032BE7C
		private void DoGetFsmVariable()
		{
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
			if (this.fsm == null || this.storeValue == null)
			{
				return;
			}
			FsmQuaternion fsmQuaternion = this.fsm.FsmVariables.GetFsmQuaternion(this.variableName.Value);
			if (fsmQuaternion != null)
			{
				this.storeValue.Value = fsmQuaternion.Value;
			}
		}

		// Token: 0x040084B2 RID: 33970
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040084B3 RID: 33971
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x040084B4 RID: 33972
		[RequiredField]
		[UIHint(UIHint.FsmQuaternion)]
		public FsmString variableName;

		// Token: 0x040084B5 RID: 33973
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmQuaternion storeValue;

		// Token: 0x040084B6 RID: 33974
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x040084B7 RID: 33975
		private GameObject goLastFrame;

		// Token: 0x040084B8 RID: 33976
		private string fsmNameLastFrame;

		// Token: 0x040084B9 RID: 33977
		protected PlayMakerFSM fsm;
	}
}
