using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C4C RID: 3148
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets the Child of a GameObject by Index.\nE.g., O to get the first child. HINT: Use this with an integer variable to iterate through children.")]
	public class GetChildNum : FsmStateAction
	{
		// Token: 0x06009ED8 RID: 40664 RVA: 0x0032C95F File Offset: 0x0032AB5F
		public override void Reset()
		{
			this.gameObject = null;
			this.childIndex = 0;
			this.store = null;
		}

		// Token: 0x06009ED9 RID: 40665 RVA: 0x0032C97B File Offset: 0x0032AB7B
		public override void OnEnter()
		{
			this.store.Value = this.DoGetChildNum(base.Fsm.GetOwnerDefaultTarget(this.gameObject));
			base.Finish();
		}

		// Token: 0x06009EDA RID: 40666 RVA: 0x0032C9A8 File Offset: 0x0032ABA8
		private GameObject DoGetChildNum(GameObject go)
		{
			if (go == null || go.transform.childCount == 0 || this.childIndex.Value < 0)
			{
				return null;
			}
			return go.transform.GetChild(this.childIndex.Value % go.transform.childCount).gameObject;
		}

		// Token: 0x04008433 RID: 33843
		[RequiredField]
		[Tooltip("The GameObject to search.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008434 RID: 33844
		[RequiredField]
		[Tooltip("The index of the child to find.")]
		public FsmInt childIndex;

		// Token: 0x04008435 RID: 33845
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the child in a GameObject variable.")]
		public FsmGameObject store;
	}
}
