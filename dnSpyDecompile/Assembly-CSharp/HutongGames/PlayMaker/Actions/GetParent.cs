using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C7A RID: 3194
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets the Parent of a Game Object.")]
	public class GetParent : FsmStateAction
	{
		// Token: 0x06009FA9 RID: 40873 RVA: 0x0032EFAF File Offset: 0x0032D1AF
		public override void Reset()
		{
			this.gameObject = null;
			this.storeResult = null;
		}

		// Token: 0x06009FAA RID: 40874 RVA: 0x0032EFC0 File Offset: 0x0032D1C0
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this.storeResult.Value = ((ownerDefaultTarget.transform.parent == null) ? null : ownerDefaultTarget.transform.parent.gameObject);
			}
			else
			{
				this.storeResult.Value = null;
			}
			base.Finish();
		}

		// Token: 0x04008539 RID: 34105
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400853A RID: 34106
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeResult;
	}
}
