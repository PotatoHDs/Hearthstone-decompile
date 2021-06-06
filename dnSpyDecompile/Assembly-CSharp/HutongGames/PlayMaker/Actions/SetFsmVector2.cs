using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DC8 RID: 3528
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Set the value of a Vector2 Variable in another FSM.")]
	public class SetFsmVector2 : FsmStateAction
	{
		// Token: 0x0600A5D9 RID: 42457 RVA: 0x00347F76 File Offset: 0x00346176
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.setValue = null;
		}

		// Token: 0x0600A5DA RID: 42458 RVA: 0x00347F96 File Offset: 0x00346196
		public override void OnEnter()
		{
			this.DoSetFsmVector2();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A5DB RID: 42459 RVA: 0x00347FAC File Offset: 0x003461AC
		private void DoSetFsmVector2()
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
			FsmVector2 fsmVector = this.fsm.FsmVariables.GetFsmVector2(this.variableName.Value);
			if (fsmVector != null)
			{
				fsmVector.Value = this.setValue.Value;
				return;
			}
			base.LogWarning("Could not find variable: " + this.variableName.Value);
		}

		// Token: 0x0600A5DC RID: 42460 RVA: 0x003480A9 File Offset: 0x003462A9
		public override void OnUpdate()
		{
			this.DoSetFsmVector2();
		}

		// Token: 0x04008C83 RID: 35971
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008C84 RID: 35972
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x04008C85 RID: 35973
		[RequiredField]
		[UIHint(UIHint.FsmVector2)]
		[Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

		// Token: 0x04008C86 RID: 35974
		[RequiredField]
		[Tooltip("Set the value of the variable.")]
		public FsmVector2 setValue;

		// Token: 0x04008C87 RID: 35975
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;

		// Token: 0x04008C88 RID: 35976
		private GameObject goLastFrame;

		// Token: 0x04008C89 RID: 35977
		private string fsmNameLastFrame;

		// Token: 0x04008C8A RID: 35978
		private PlayMakerFSM fsm;
	}
}
