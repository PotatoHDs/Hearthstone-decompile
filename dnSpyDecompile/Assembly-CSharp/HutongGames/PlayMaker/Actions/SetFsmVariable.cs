using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DC7 RID: 3527
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Set the value of a variable in another FSM.")]
	public class SetFsmVariable : FsmStateAction
	{
		// Token: 0x0600A5D4 RID: 42452 RVA: 0x00347DF1 File Offset: 0x00345FF1
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.setValue = new FsmVar();
		}

		// Token: 0x0600A5D5 RID: 42453 RVA: 0x00347E15 File Offset: 0x00346015
		public override void OnEnter()
		{
			this.DoSetFsmVariable();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A5D6 RID: 42454 RVA: 0x00347E2B File Offset: 0x0034602B
		public override void OnUpdate()
		{
			this.DoSetFsmVariable();
		}

		// Token: 0x0600A5D7 RID: 42455 RVA: 0x00347E34 File Offset: 0x00346034
		private void DoSetFsmVariable()
		{
			if (this.setValue.IsNone || string.IsNullOrEmpty(this.variableName.Value))
			{
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			if (ownerDefaultTarget != this.cachedGameObject || this.fsmName.Value != this.cachedFsmName)
			{
				this.targetFsm = ActionHelpers.GetGameObjectFsm(ownerDefaultTarget, this.fsmName.Value);
				if (this.targetFsm == null)
				{
					return;
				}
				this.cachedGameObject = ownerDefaultTarget;
				this.cachedFsmName = this.fsmName.Value;
			}
			if (this.variableName.Value != this.cachedVariableName)
			{
				this.targetVariable = this.targetFsm.FsmVariables.FindVariable(this.setValue.Type, this.variableName.Value);
				this.cachedVariableName = this.variableName.Value;
			}
			if (this.targetVariable == null)
			{
				base.LogWarning("Missing Variable: " + this.variableName.Value);
				return;
			}
			this.setValue.UpdateValue();
			this.setValue.ApplyValueTo(this.targetVariable);
		}

		// Token: 0x04008C79 RID: 35961
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008C7A RID: 35962
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x04008C7B RID: 35963
		[Tooltip("The name of the variable in the target FSM.")]
		public FsmString variableName;

		// Token: 0x04008C7C RID: 35964
		[RequiredField]
		public FsmVar setValue;

		// Token: 0x04008C7D RID: 35965
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x04008C7E RID: 35966
		private PlayMakerFSM targetFsm;

		// Token: 0x04008C7F RID: 35967
		private NamedVariable targetVariable;

		// Token: 0x04008C80 RID: 35968
		private GameObject cachedGameObject;

		// Token: 0x04008C81 RID: 35969
		private string cachedFsmName;

		// Token: 0x04008C82 RID: 35970
		private string cachedVariableName;
	}
}
