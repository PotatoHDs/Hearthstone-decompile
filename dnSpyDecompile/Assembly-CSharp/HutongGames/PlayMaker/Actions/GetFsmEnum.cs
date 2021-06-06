using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C58 RID: 3160
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Get the value of an Enum Variable from another FSM.")]
	public class GetFsmEnum : FsmStateAction
	{
		// Token: 0x06009F0F RID: 40719 RVA: 0x0032D5CB File Offset: 0x0032B7CB
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.storeValue = null;
		}

		// Token: 0x06009F10 RID: 40720 RVA: 0x0032D5EB File Offset: 0x0032B7EB
		public override void OnEnter()
		{
			this.DoGetFsmEnum();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009F11 RID: 40721 RVA: 0x0032D601 File Offset: 0x0032B801
		public override void OnUpdate()
		{
			this.DoGetFsmEnum();
		}

		// Token: 0x06009F12 RID: 40722 RVA: 0x0032D60C File Offset: 0x0032B80C
		private void DoGetFsmEnum()
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
			FsmEnum fsmEnum = this.fsm.FsmVariables.GetFsmEnum(this.variableName.Value);
			if (fsmEnum == null)
			{
				return;
			}
			this.storeValue.Value = fsmEnum.Value;
		}

		// Token: 0x04008482 RID: 33922
		[RequiredField]
		[Tooltip("The target FSM")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008483 RID: 33923
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x04008484 RID: 33924
		[RequiredField]
		[UIHint(UIHint.FsmBool)]
		public FsmString variableName;

		// Token: 0x04008485 RID: 33925
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmEnum storeValue;

		// Token: 0x04008486 RID: 33926
		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		// Token: 0x04008487 RID: 33927
		private GameObject goLastFrame;

		// Token: 0x04008488 RID: 33928
		private string fsmNameLastFrame;

		// Token: 0x04008489 RID: 33929
		private PlayMakerFSM fsm;
	}
}
