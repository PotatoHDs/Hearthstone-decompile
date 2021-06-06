using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C61 RID: 3169
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Get the value of a String Variable from another FSM.")]
	public class GetFsmString : FsmStateAction
	{
		// Token: 0x06009F3C RID: 40764 RVA: 0x0032DF4A File Offset: 0x0032C14A
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.storeValue = null;
		}

		// Token: 0x06009F3D RID: 40765 RVA: 0x0032DF6A File Offset: 0x0032C16A
		public override void OnEnter()
		{
			this.DoGetFsmString();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009F3E RID: 40766 RVA: 0x0032DF80 File Offset: 0x0032C180
		public override void OnUpdate()
		{
			this.DoGetFsmString();
		}

		// Token: 0x06009F3F RID: 40767 RVA: 0x0032DF88 File Offset: 0x0032C188
		private void DoGetFsmString()
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
			FsmString fsmString = this.fsm.FsmVariables.GetFsmString(this.variableName.Value);
			if (fsmString == null)
			{
				return;
			}
			this.storeValue.Value = fsmString.Value;
		}

		// Token: 0x040084C8 RID: 33992
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x040084C9 RID: 33993
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x040084CA RID: 33994
		[RequiredField]
		[UIHint(UIHint.FsmString)]
		public FsmString variableName;

		// Token: 0x040084CB RID: 33995
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString storeValue;

		// Token: 0x040084CC RID: 33996
		public bool everyFrame;

		// Token: 0x040084CD RID: 33997
		private GameObject goLastFrame;

		// Token: 0x040084CE RID: 33998
		private string fsmNameLastFrame;

		// Token: 0x040084CF RID: 33999
		private PlayMakerFSM fsm;
	}
}
