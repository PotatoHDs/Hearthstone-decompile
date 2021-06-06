using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DC4 RID: 3524
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Set the value of a Rect Variable in another FSM.")]
	public class SetFsmRect : FsmStateAction
	{
		// Token: 0x0600A5C5 RID: 42437 RVA: 0x00347A0D File Offset: 0x00345C0D
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.variableName = "";
			this.setValue = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A5C6 RID: 42438 RVA: 0x00347A44 File Offset: 0x00345C44
		public override void OnEnter()
		{
			this.DoSetFsmBool();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A5C7 RID: 42439 RVA: 0x00347A5C File Offset: 0x00345C5C
		private void DoSetFsmBool()
		{
			if (this.setValue == null)
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
				base.LogWarning("Could not find FSM: " + this.fsmName.Value);
				return;
			}
			FsmRect fsmRect = this.fsm.FsmVariables.GetFsmRect(this.variableName.Value);
			if (fsmRect != null)
			{
				fsmRect.Value = this.setValue.Value;
				return;
			}
			base.LogWarning("Could not find variable: " + this.variableName.Value);
		}

		// Token: 0x0600A5C8 RID: 42440 RVA: 0x00347B59 File Offset: 0x00345D59
		public override void OnUpdate()
		{
			this.DoSetFsmBool();
		}

		// Token: 0x04008C61 RID: 35937
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008C62 RID: 35938
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x04008C63 RID: 35939
		[RequiredField]
		[UIHint(UIHint.FsmRect)]
		[Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

		// Token: 0x04008C64 RID: 35940
		[RequiredField]
		[Tooltip("Set the value of the variable.")]
		public FsmRect setValue;

		// Token: 0x04008C65 RID: 35941
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;

		// Token: 0x04008C66 RID: 35942
		private GameObject goLastFrame;

		// Token: 0x04008C67 RID: 35943
		private string fsmNameLastFrame;

		// Token: 0x04008C68 RID: 35944
		private PlayMakerFSM fsm;
	}
}
