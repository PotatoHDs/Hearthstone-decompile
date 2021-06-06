using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BCD RID: 3021
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Set an item in an Array Variable in another FSM.")]
	public class SetFsmArrayItem : BaseFsmVariableIndexAction
	{
		// Token: 0x06009CA3 RID: 40099 RVA: 0x0032612D File Offset: 0x0032432D
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.value = null;
		}

		// Token: 0x06009CA4 RID: 40100 RVA: 0x0032614D File Offset: 0x0032434D
		public override void OnEnter()
		{
			this.DoSetFsmArray();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009CA5 RID: 40101 RVA: 0x00326164 File Offset: 0x00324364
		private void DoSetFsmArray()
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
			if (fsmArray.ElementType == this.value.NamedVar.VariableType)
			{
				this.value.UpdateValue();
				fsmArray.Set(this.index.Value, this.value.GetValue());
				return;
			}
			base.LogWarning("Incompatible variable type: " + this.variableName.Value);
		}

		// Token: 0x06009CA6 RID: 40102 RVA: 0x00326260 File Offset: 0x00324460
		public override void OnUpdate()
		{
			this.DoSetFsmArray();
		}

		// Token: 0x04008220 RID: 33312
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008221 RID: 33313
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object.")]
		public FsmString fsmName;

		// Token: 0x04008222 RID: 33314
		[RequiredField]
		[UIHint(UIHint.FsmArray)]
		[Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

		// Token: 0x04008223 RID: 33315
		[Tooltip("The index into the array.")]
		public FsmInt index;

		// Token: 0x04008224 RID: 33316
		[RequiredField]
		[Tooltip("Set the value of the array at the specified index.")]
		public FsmVar value;

		// Token: 0x04008225 RID: 33317
		[Tooltip("Repeat every frame. Useful if the value is changing.")]
		public bool everyFrame;
	}
}
