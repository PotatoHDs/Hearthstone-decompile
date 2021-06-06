using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C57 RID: 3159
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Get the value of a Color Variable from another FSM.")]
	public class GetFsmColor : FsmStateAction
	{
		// Token: 0x06009F0A RID: 40714 RVA: 0x0032D4C3 File Offset: 0x0032B6C3
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.storeValue = null;
		}

		// Token: 0x06009F0B RID: 40715 RVA: 0x0032D4E3 File Offset: 0x0032B6E3
		public override void OnEnter()
		{
			this.DoGetFsmColor();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009F0C RID: 40716 RVA: 0x0032D4F9 File Offset: 0x0032B6F9
		public override void OnUpdate()
		{
			this.DoGetFsmColor();
		}

		// Token: 0x06009F0D RID: 40717 RVA: 0x0032D504 File Offset: 0x0032B704
		private void DoGetFsmColor()
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
			FsmColor fsmColor = this.fsm.FsmVariables.GetFsmColor(this.variableName.Value);
			if (fsmColor == null)
			{
				return;
			}
			this.storeValue.Value = fsmColor.Value;
		}

		// Token: 0x0400847A RID: 33914
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400847B RID: 33915
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x0400847C RID: 33916
		[RequiredField]
		[UIHint(UIHint.FsmColor)]
		public FsmString variableName;

		// Token: 0x0400847D RID: 33917
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmColor storeValue;

		// Token: 0x0400847E RID: 33918
		public bool everyFrame;

		// Token: 0x0400847F RID: 33919
		private GameObject goLastFrame;

		// Token: 0x04008480 RID: 33920
		private string fsmNameLastFrame;

		// Token: 0x04008481 RID: 33921
		private PlayMakerFSM fsm;
	}
}
