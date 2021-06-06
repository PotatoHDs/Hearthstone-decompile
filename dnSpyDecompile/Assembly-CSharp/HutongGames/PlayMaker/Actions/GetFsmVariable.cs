using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C63 RID: 3171
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Get the value of a variable in another FSM and store it in a variable of the same name in this FSM.")]
	public class GetFsmVariable : FsmStateAction
	{
		// Token: 0x06009F46 RID: 40774 RVA: 0x0032E169 File Offset: 0x0032C369
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.storeValue = new FsmVar();
		}

		// Token: 0x06009F47 RID: 40775 RVA: 0x0032E18D File Offset: 0x0032C38D
		public override void OnEnter()
		{
			this.InitFsmVar();
			this.DoGetFsmVariable();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009F48 RID: 40776 RVA: 0x0032E1A9 File Offset: 0x0032C3A9
		public override void OnUpdate()
		{
			this.DoGetFsmVariable();
		}

		// Token: 0x06009F49 RID: 40777 RVA: 0x0032E1B4 File Offset: 0x0032C3B4
		private void InitFsmVar()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			if (ownerDefaultTarget != this.cachedGO || this.cachedFsmName != this.fsmName.Value)
			{
				this.sourceFsm = ActionHelpers.GetGameObjectFsm(ownerDefaultTarget, this.fsmName.Value);
				this.sourceVariable = this.sourceFsm.FsmVariables.GetVariable(this.storeValue.variableName);
				this.targetVariable = base.Fsm.Variables.GetVariable(this.storeValue.variableName);
				this.storeValue.Type = this.targetVariable.VariableType;
				if (!string.IsNullOrEmpty(this.storeValue.variableName) && this.sourceVariable == null)
				{
					base.LogWarning("Missing Variable: " + this.storeValue.variableName);
				}
				this.cachedGO = ownerDefaultTarget;
				this.cachedFsmName = this.fsmName.Value;
			}
		}

		// Token: 0x06009F4A RID: 40778 RVA: 0x0032E2C2 File Offset: 0x0032C4C2
		private void DoGetFsmVariable()
		{
			if (this.storeValue.IsNone)
			{
				return;
			}
			this.InitFsmVar();
			this.storeValue.GetValueFrom(this.sourceVariable);
			this.storeValue.ApplyValueTo(this.targetVariable);
		}

		// Token: 0x040084D8 RID: 34008
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040084D9 RID: 34009
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x040084DA RID: 34010
		[RequiredField]
		[HideTypeFilter]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the value of the FsmVariable")]
		public FsmVar storeValue;

		// Token: 0x040084DB RID: 34011
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x040084DC RID: 34012
		private GameObject cachedGO;

		// Token: 0x040084DD RID: 34013
		private string cachedFsmName;

		// Token: 0x040084DE RID: 34014
		private PlayMakerFSM sourceFsm;

		// Token: 0x040084DF RID: 34015
		private INamedVariable sourceVariable;

		// Token: 0x040084E0 RID: 34016
		private NamedVariable targetVariable;
	}
}
