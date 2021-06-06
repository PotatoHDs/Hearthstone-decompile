using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BCC RID: 3020
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Copy an Array Variable in another FSM.")]
	public class SetFsmArray : BaseFsmVariableAction
	{
		// Token: 0x06009C9F RID: 40095 RVA: 0x00325FE5 File Offset: 0x003241E5
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.variableName = null;
			this.setValue = null;
			this.copyValues = true;
		}

		// Token: 0x06009CA0 RID: 40096 RVA: 0x00326013 File Offset: 0x00324213
		public override void OnEnter()
		{
			this.DoSetFsmArrayCopy();
			base.Finish();
		}

		// Token: 0x06009CA1 RID: 40097 RVA: 0x00326024 File Offset: 0x00324224
		private void DoSetFsmArrayCopy()
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
			if (fsmArray.ElementType != this.setValue.ElementType)
			{
				base.LogError(string.Concat(new object[]
				{
					"Can only copy arrays with the same elements type. Found <",
					fsmArray.ElementType,
					"> and <",
					this.setValue.ElementType,
					">"
				}));
				return;
			}
			fsmArray.Resize(0);
			if (this.copyValues)
			{
				fsmArray.Values = (this.setValue.Values.Clone() as object[]);
			}
			else
			{
				fsmArray.Values = this.setValue.Values;
			}
			fsmArray.SaveChanges();
		}

		// Token: 0x0400821B RID: 33307
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400821C RID: 33308
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object.")]
		public FsmString fsmName;

		// Token: 0x0400821D RID: 33309
		[RequiredField]
		[UIHint(UIHint.FsmArray)]
		[Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

		// Token: 0x0400821E RID: 33310
		[RequiredField]
		[Tooltip("Set the content of the array variable.")]
		[UIHint(UIHint.Variable)]
		public FsmArray setValue;

		// Token: 0x0400821F RID: 33311
		[Tooltip("If true, makes copies. if false, values share the same reference and editing one array item value will affect the source and vice versa. Warning, this only affect the current items of the source array. Adding or removing items doesn't affect other FsmArrays.")]
		public bool copyValues;
	}
}
