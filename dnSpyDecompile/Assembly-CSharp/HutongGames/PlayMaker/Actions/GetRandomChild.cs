using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C7E RID: 3198
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets a Random Child of a Game Object.")]
	public class GetRandomChild : FsmStateAction
	{
		// Token: 0x06009FB8 RID: 40888 RVA: 0x0032F188 File Offset: 0x0032D388
		public override void Reset()
		{
			this.gameObject = null;
			this.storeResult = null;
		}

		// Token: 0x06009FB9 RID: 40889 RVA: 0x0032F198 File Offset: 0x0032D398
		public override void OnEnter()
		{
			this.DoGetRandomChild();
			base.Finish();
		}

		// Token: 0x06009FBA RID: 40890 RVA: 0x0032F1A8 File Offset: 0x0032D3A8
		private void DoGetRandomChild()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			int childCount = ownerDefaultTarget.transform.childCount;
			if (childCount == 0)
			{
				return;
			}
			this.storeResult.Value = ownerDefaultTarget.transform.GetChild(UnityEngine.Random.Range(0, childCount)).gameObject;
		}

		// Token: 0x04008545 RID: 34117
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008546 RID: 34118
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeResult;
	}
}
