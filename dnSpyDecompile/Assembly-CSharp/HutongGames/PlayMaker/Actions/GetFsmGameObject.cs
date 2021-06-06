using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C5A RID: 3162
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Get the value of a Game Object Variable from another FSM.")]
	public class GetFsmGameObject : FsmStateAction
	{
		// Token: 0x06009F19 RID: 40729 RVA: 0x0032D7E0 File Offset: 0x0032B9E0
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.storeValue = null;
		}

		// Token: 0x06009F1A RID: 40730 RVA: 0x0032D800 File Offset: 0x0032BA00
		public override void OnEnter()
		{
			this.DoGetFsmGameObject();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009F1B RID: 40731 RVA: 0x0032D816 File Offset: 0x0032BA16
		public override void OnUpdate()
		{
			this.DoGetFsmGameObject();
		}

		// Token: 0x06009F1C RID: 40732 RVA: 0x0032D820 File Offset: 0x0032BA20
		private void DoGetFsmGameObject()
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
			FsmGameObject fsmGameObject = this.fsm.FsmVariables.GetFsmGameObject(this.variableName.Value);
			if (fsmGameObject == null)
			{
				return;
			}
			this.storeValue.Value = fsmGameObject.Value;
		}

		// Token: 0x04008492 RID: 33938
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008493 RID: 33939
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x04008494 RID: 33940
		[RequiredField]
		[UIHint(UIHint.FsmGameObject)]
		public FsmString variableName;

		// Token: 0x04008495 RID: 33941
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeValue;

		// Token: 0x04008496 RID: 33942
		public bool everyFrame;

		// Token: 0x04008497 RID: 33943
		private GameObject goLastFrame;

		// Token: 0x04008498 RID: 33944
		private string fsmNameLastFrame;

		// Token: 0x04008499 RID: 33945
		private PlayMakerFSM fsm;
	}
}
