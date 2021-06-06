using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BCA RID: 3018
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName", false)]
	[Tooltip("Copy an Array Variable from another FSM.")]
	public class GetFsmArray : BaseFsmVariableAction
	{
		// Token: 0x06009C96 RID: 40086 RVA: 0x00325D54 File Offset: 0x00323F54
		public override void Reset()
		{
			this.gameObject = null;
			this.fsmName = "";
			this.variableName = null;
			this.storeValue = null;
			this.copyValues = true;
		}

		// Token: 0x06009C97 RID: 40087 RVA: 0x00325D82 File Offset: 0x00323F82
		public override void OnEnter()
		{
			this.DoSetFsmArrayCopy();
			base.Finish();
		}

		// Token: 0x06009C98 RID: 40088 RVA: 0x00325D90 File Offset: 0x00323F90
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
			if (fsmArray.ElementType != this.storeValue.ElementType)
			{
				base.LogError(string.Concat(new object[]
				{
					"Can only copy arrays with the same elements type. Found <",
					fsmArray.ElementType,
					"> and <",
					this.storeValue.ElementType,
					">"
				}));
				return;
			}
			this.storeValue.Resize(0);
			if (this.copyValues)
			{
				this.storeValue.Values = (fsmArray.Values.Clone() as object[]);
			}
			else
			{
				this.storeValue.Values = fsmArray.Values;
			}
			this.storeValue.SaveChanges();
		}

		// Token: 0x04008210 RID: 33296
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008211 RID: 33297
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object.")]
		public FsmString fsmName;

		// Token: 0x04008212 RID: 33298
		[RequiredField]
		[UIHint(UIHint.FsmArray)]
		[Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

		// Token: 0x04008213 RID: 33299
		[RequiredField]
		[Tooltip("Get the content of the array variable.")]
		[UIHint(UIHint.Variable)]
		public FsmArray storeValue;

		// Token: 0x04008214 RID: 33300
		[Tooltip("If true, makes copies. if false, values share the same reference and editing one array item value will affect the source and vice versa. Warning, this only affect the current items of the source array. Adding or removing items doesn't affect other FsmArrays.")]
		public bool copyValues;
	}
}
