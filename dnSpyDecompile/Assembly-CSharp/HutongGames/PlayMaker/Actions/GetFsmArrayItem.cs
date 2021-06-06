using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BCB RID: 3019
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Gets an item in an Array Variable in another FSM.")]
	public class GetFsmArrayItem : BaseFsmVariableIndexAction
	{
		// Token: 0x06009C9A RID: 40090 RVA: 0x00325EAB File Offset: 0x003240AB
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.storeValue = null;
		}

		// Token: 0x06009C9B RID: 40091 RVA: 0x00325ECB File Offset: 0x003240CB
		public override void OnEnter()
		{
			this.DoGetFsmArray();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009C9C RID: 40092 RVA: 0x00325EE4 File Offset: 0x003240E4
		private void DoGetFsmArray()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget, this.fsmName.Value))
			{
				return;
			}
			FsmArray fsmArray = this.fsm.FsmVariables.GetFsmArray(this.variableName.Value);
			if (fsmArray == null)
			{
				base.DoVariableNotFound(this.variableName.Value);
				return;
			}
			if (this.index.Value < 0 || this.index.Value >= fsmArray.Length)
			{
				base.Fsm.Event(this.indexOutOfRange);
				base.Finish();
				return;
			}
			if (fsmArray.ElementType == this.storeValue.NamedVar.VariableType)
			{
				this.storeValue.SetValue(fsmArray.Get(this.index.Value));
				return;
			}
			base.LogWarning("Incompatible variable type: " + this.variableName.Value);
		}

		// Token: 0x06009C9D RID: 40093 RVA: 0x00325FD5 File Offset: 0x003241D5
		public override void OnUpdate()
		{
			this.DoGetFsmArray();
		}

		// Token: 0x04008215 RID: 33301
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008216 RID: 33302
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object.")]
		public FsmString fsmName;

		// Token: 0x04008217 RID: 33303
		[RequiredField]
		[UIHint(UIHint.FsmArray)]
		[Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

		// Token: 0x04008218 RID: 33304
		[Tooltip("The index into the array.")]
		public FsmInt index;

		// Token: 0x04008219 RID: 33305
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the value of the array at the specified index.")]
		public FsmVar storeValue;

		// Token: 0x0400821A RID: 33306
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;
	}
}
