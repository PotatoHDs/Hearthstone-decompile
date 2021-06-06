using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C5C RID: 3164
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Get the value of a Material Variable from another FSM.")]
	public class GetFsmMaterial : FsmStateAction
	{
		// Token: 0x06009F23 RID: 40739 RVA: 0x0032D9EF File Offset: 0x0032BBEF
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.variableName = "";
			this.storeValue = null;
			this.everyFrame = false;
		}

		// Token: 0x06009F24 RID: 40740 RVA: 0x0032DA26 File Offset: 0x0032BC26
		public override void OnEnter()
		{
			this.DoGetFsmVariable();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009F25 RID: 40741 RVA: 0x0032DA3C File Offset: 0x0032BC3C
		public override void OnUpdate()
		{
			this.DoGetFsmVariable();
		}

		// Token: 0x06009F26 RID: 40742 RVA: 0x0032DA44 File Offset: 0x0032BC44
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
			FsmMaterial fsmMaterial = this.fsm.FsmVariables.GetFsmMaterial(this.variableName.Value);
			if (fsmMaterial != null)
			{
				this.storeValue.Value = fsmMaterial.Value;
			}
		}

		// Token: 0x040084A2 RID: 33954
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040084A3 RID: 33955
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x040084A4 RID: 33956
		[RequiredField]
		[UIHint(UIHint.FsmMaterial)]
		public FsmString variableName;

		// Token: 0x040084A5 RID: 33957
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmMaterial storeValue;

		// Token: 0x040084A6 RID: 33958
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x040084A7 RID: 33959
		private GameObject goLastFrame;

		// Token: 0x040084A8 RID: 33960
		private string fsmNameLastFrame;

		// Token: 0x040084A9 RID: 33961
		protected PlayMakerFSM fsm;
	}
}
