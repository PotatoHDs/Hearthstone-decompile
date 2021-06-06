using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C62 RID: 3170
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Get the value of a Texture Variable from another FSM.")]
	public class GetFsmTexture : FsmStateAction
	{
		// Token: 0x06009F41 RID: 40769 RVA: 0x0032E04F File Offset: 0x0032C24F
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.variableName = "";
			this.storeValue = null;
			this.everyFrame = false;
		}

		// Token: 0x06009F42 RID: 40770 RVA: 0x0032E086 File Offset: 0x0032C286
		public override void OnEnter()
		{
			this.DoGetFsmVariable();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009F43 RID: 40771 RVA: 0x0032E09C File Offset: 0x0032C29C
		public override void OnUpdate()
		{
			this.DoGetFsmVariable();
		}

		// Token: 0x06009F44 RID: 40772 RVA: 0x0032E0A4 File Offset: 0x0032C2A4
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
			FsmTexture fsmTexture = this.fsm.FsmVariables.GetFsmTexture(this.variableName.Value);
			if (fsmTexture != null)
			{
				this.storeValue.Value = fsmTexture.Value;
			}
		}

		// Token: 0x040084D0 RID: 34000
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040084D1 RID: 34001
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x040084D2 RID: 34002
		[RequiredField]
		[UIHint(UIHint.FsmTexture)]
		public FsmString variableName;

		// Token: 0x040084D3 RID: 34003
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmTexture storeValue;

		// Token: 0x040084D4 RID: 34004
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x040084D5 RID: 34005
		private GameObject goLastFrame;

		// Token: 0x040084D6 RID: 34006
		private string fsmNameLastFrame;

		// Token: 0x040084D7 RID: 34007
		protected PlayMakerFSM fsm;
	}
}
