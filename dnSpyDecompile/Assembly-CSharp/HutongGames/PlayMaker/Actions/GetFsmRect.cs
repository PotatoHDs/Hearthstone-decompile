using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C5F RID: 3167
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Get the value of a Rect Variable from another FSM.")]
	public class GetFsmRect : FsmStateAction
	{
		// Token: 0x06009F32 RID: 40754 RVA: 0x0032DD41 File Offset: 0x0032BF41
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.variableName = "";
			this.storeValue = null;
			this.everyFrame = false;
		}

		// Token: 0x06009F33 RID: 40755 RVA: 0x0032DD78 File Offset: 0x0032BF78
		public override void OnEnter()
		{
			this.DoGetFsmVariable();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009F34 RID: 40756 RVA: 0x0032DD8E File Offset: 0x0032BF8E
		public override void OnUpdate()
		{
			this.DoGetFsmVariable();
		}

		// Token: 0x06009F35 RID: 40757 RVA: 0x0032DD98 File Offset: 0x0032BF98
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
			FsmRect fsmRect = this.fsm.FsmVariables.GetFsmRect(this.variableName.Value);
			if (fsmRect != null)
			{
				this.storeValue.Value = fsmRect.Value;
			}
		}

		// Token: 0x040084BA RID: 33978
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040084BB RID: 33979
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x040084BC RID: 33980
		[RequiredField]
		[UIHint(UIHint.FsmRect)]
		public FsmString variableName;

		// Token: 0x040084BD RID: 33981
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmRect storeValue;

		// Token: 0x040084BE RID: 33982
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x040084BF RID: 33983
		private GameObject goLastFrame;

		// Token: 0x040084C0 RID: 33984
		private string fsmNameLastFrame;

		// Token: 0x040084C1 RID: 33985
		protected PlayMakerFSM fsm;
	}
}
