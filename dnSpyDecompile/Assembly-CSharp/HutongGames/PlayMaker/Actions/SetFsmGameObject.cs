using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DBF RID: 3519
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Set the value of a Game Object Variable in another FSM. Accept null reference")]
	public class SetFsmGameObject : FsmStateAction
	{
		// Token: 0x0600A5AC RID: 42412 RVA: 0x003473A9 File Offset: 0x003455A9
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.setValue = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A5AD RID: 42413 RVA: 0x003473D0 File Offset: 0x003455D0
		public override void OnEnter()
		{
			this.DoSetFsmGameObject();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A5AE RID: 42414 RVA: 0x003473E8 File Offset: 0x003455E8
		private void DoSetFsmGameObject()
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
			if (this.fsm == null)
			{
				return;
			}
			FsmGameObject fsmGameObject = this.fsm.FsmVariables.FindFsmGameObject(this.variableName.Value);
			if (fsmGameObject != null)
			{
				fsmGameObject.Value = ((this.setValue == null) ? null : this.setValue.Value);
				return;
			}
			base.LogWarning("Could not find variable: " + this.variableName.Value);
		}

		// Token: 0x0600A5AF RID: 42415 RVA: 0x003474CC File Offset: 0x003456CC
		public override void OnUpdate()
		{
			this.DoSetFsmGameObject();
		}

		// Token: 0x04008C39 RID: 35897
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008C3A RID: 35898
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x04008C3B RID: 35899
		[RequiredField]
		[UIHint(UIHint.FsmGameObject)]
		[Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

		// Token: 0x04008C3C RID: 35900
		[Tooltip("Set the value of the variable.")]
		public FsmGameObject setValue;

		// Token: 0x04008C3D RID: 35901
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;

		// Token: 0x04008C3E RID: 35902
		private GameObject goLastFrame;

		// Token: 0x04008C3F RID: 35903
		private string fsmNameLastFrame;

		// Token: 0x04008C40 RID: 35904
		private PlayMakerFSM fsm;
	}
}
