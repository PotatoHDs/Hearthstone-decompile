using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C4B RID: 3147
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets the number of children that a GameObject has.")]
	public class GetChildCount : FsmStateAction
	{
		// Token: 0x06009ED4 RID: 40660 RVA: 0x0032C901 File Offset: 0x0032AB01
		public override void Reset()
		{
			this.gameObject = null;
			this.storeResult = null;
		}

		// Token: 0x06009ED5 RID: 40661 RVA: 0x0032C911 File Offset: 0x0032AB11
		public override void OnEnter()
		{
			this.DoGetChildCount();
			base.Finish();
		}

		// Token: 0x06009ED6 RID: 40662 RVA: 0x0032C920 File Offset: 0x0032AB20
		private void DoGetChildCount()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			this.storeResult.Value = ownerDefaultTarget.transform.childCount;
		}

		// Token: 0x04008431 RID: 33841
		[RequiredField]
		[Tooltip("The GameObject to test.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008432 RID: 33842
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the number of children in an int variable.")]
		public FsmInt storeResult;
	}
}
