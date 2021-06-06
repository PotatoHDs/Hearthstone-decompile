using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C5D RID: 3165
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Get the value of an Object Variable from another FSM.")]
	public class GetFsmObject : FsmStateAction
	{
		// Token: 0x06009F28 RID: 40744 RVA: 0x0032DB09 File Offset: 0x0032BD09
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.variableName = "";
			this.storeValue = null;
			this.everyFrame = false;
		}

		// Token: 0x06009F29 RID: 40745 RVA: 0x0032DB40 File Offset: 0x0032BD40
		public override void OnEnter()
		{
			this.DoGetFsmVariable();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009F2A RID: 40746 RVA: 0x0032DB56 File Offset: 0x0032BD56
		public override void OnUpdate()
		{
			this.DoGetFsmVariable();
		}

		// Token: 0x06009F2B RID: 40747 RVA: 0x0032DB60 File Offset: 0x0032BD60
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
			FsmObject fsmObject = this.fsm.FsmVariables.GetFsmObject(this.variableName.Value);
			if (fsmObject != null)
			{
				this.storeValue.Value = fsmObject.Value;
			}
		}

		// Token: 0x040084AA RID: 33962
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040084AB RID: 33963
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x040084AC RID: 33964
		[RequiredField]
		[UIHint(UIHint.FsmObject)]
		public FsmString variableName;

		// Token: 0x040084AD RID: 33965
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmObject storeValue;

		// Token: 0x040084AE RID: 33966
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x040084AF RID: 33967
		private GameObject goLastFrame;

		// Token: 0x040084B0 RID: 33968
		private string fsmNameLastFrame;

		// Token: 0x040084B1 RID: 33969
		protected PlayMakerFSM fsm;
	}
}
