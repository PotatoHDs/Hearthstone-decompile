using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C65 RID: 3173
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Get the value of a Vector2 Variable from another FSM.")]
	public class GetFsmVector2 : FsmStateAction
	{
		// Token: 0x06009F52 RID: 40786 RVA: 0x0032E4D0 File Offset: 0x0032C6D0
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.storeValue = null;
		}

		// Token: 0x06009F53 RID: 40787 RVA: 0x0032E4F0 File Offset: 0x0032C6F0
		public override void OnEnter()
		{
			this.DoGetFsmVector2();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009F54 RID: 40788 RVA: 0x0032E506 File Offset: 0x0032C706
		public override void OnUpdate()
		{
			this.DoGetFsmVector2();
		}

		// Token: 0x06009F55 RID: 40789 RVA: 0x0032E510 File Offset: 0x0032C710
		private void DoGetFsmVector2()
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
			FsmVector2 fsmVector = this.fsm.FsmVariables.GetFsmVector2(this.variableName.Value);
			if (fsmVector == null)
			{
				return;
			}
			this.storeValue.Value = fsmVector.Value;
		}

		// Token: 0x040084EA RID: 34026
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x040084EB RID: 34027
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x040084EC RID: 34028
		[RequiredField]
		[UIHint(UIHint.FsmVector2)]
		public FsmString variableName;

		// Token: 0x040084ED RID: 34029
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector2 storeValue;

		// Token: 0x040084EE RID: 34030
		public bool everyFrame;

		// Token: 0x040084EF RID: 34031
		private GameObject goLastFrame;

		// Token: 0x040084F0 RID: 34032
		private string fsmNameLastFrame;

		// Token: 0x040084F1 RID: 34033
		private PlayMakerFSM fsm;
	}
}
