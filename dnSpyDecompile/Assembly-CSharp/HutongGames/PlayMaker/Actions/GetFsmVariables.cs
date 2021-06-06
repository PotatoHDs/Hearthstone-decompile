using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C64 RID: 3172
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Get the values of multiple variables in another FSM and store in variables of the same name in this FSM.")]
	public class GetFsmVariables : FsmStateAction
	{
		// Token: 0x06009F4C RID: 40780 RVA: 0x0032E2FA File Offset: 0x0032C4FA
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.getVariables = null;
		}

		// Token: 0x06009F4D RID: 40781 RVA: 0x0032E31C File Offset: 0x0032C51C
		private void InitFsmVars()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			if (ownerDefaultTarget != this.cachedGO || this.cachedFsmName != this.fsmName.Value)
			{
				this.sourceVariables = new INamedVariable[this.getVariables.Length];
				this.targetVariables = new NamedVariable[this.getVariables.Length];
				for (int i = 0; i < this.getVariables.Length; i++)
				{
					string variableName = this.getVariables[i].variableName;
					this.sourceFsm = ActionHelpers.GetGameObjectFsm(ownerDefaultTarget, this.fsmName.Value);
					this.sourceVariables[i] = this.sourceFsm.FsmVariables.GetVariable(variableName);
					this.targetVariables[i] = base.Fsm.Variables.GetVariable(variableName);
					this.getVariables[i].Type = this.targetVariables[i].VariableType;
					if (!string.IsNullOrEmpty(variableName) && this.sourceVariables[i] == null)
					{
						base.LogWarning("Missing Variable: " + variableName);
					}
					this.cachedGO = ownerDefaultTarget;
					this.cachedFsmName = this.fsmName.Value;
				}
			}
		}

		// Token: 0x06009F4E RID: 40782 RVA: 0x0032E459 File Offset: 0x0032C659
		public override void OnEnter()
		{
			this.InitFsmVars();
			this.DoGetFsmVariables();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009F4F RID: 40783 RVA: 0x0032E475 File Offset: 0x0032C675
		public override void OnUpdate()
		{
			this.DoGetFsmVariables();
		}

		// Token: 0x06009F50 RID: 40784 RVA: 0x0032E480 File Offset: 0x0032C680
		private void DoGetFsmVariables()
		{
			this.InitFsmVars();
			for (int i = 0; i < this.getVariables.Length; i++)
			{
				this.getVariables[i].GetValueFrom(this.sourceVariables[i]);
				this.getVariables[i].ApplyValueTo(this.targetVariables[i]);
			}
		}

		// Token: 0x040084E1 RID: 34017
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040084E2 RID: 34018
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

		// Token: 0x040084E3 RID: 34019
		[RequiredField]
		[HideTypeFilter]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the values of the FsmVariables")]
		public FsmVar[] getVariables;

		// Token: 0x040084E4 RID: 34020
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x040084E5 RID: 34021
		private GameObject cachedGO;

		// Token: 0x040084E6 RID: 34022
		private string cachedFsmName;

		// Token: 0x040084E7 RID: 34023
		private PlayMakerFSM sourceFsm;

		// Token: 0x040084E8 RID: 34024
		private INamedVariable[] sourceVariables;

		// Token: 0x040084E9 RID: 34025
		private NamedVariable[] targetVariables;
	}
}
