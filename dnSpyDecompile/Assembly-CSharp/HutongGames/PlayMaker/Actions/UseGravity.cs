using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E9D RID: 3741
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Sets whether a Game Object's Rigidbody is affected by Gravity.")]
	public class UseGravity : ComponentAction<Rigidbody>
	{
		// Token: 0x0600A9B5 RID: 43445 RVA: 0x00353536 File Offset: 0x00351736
		public override void Reset()
		{
			this.gameObject = null;
			this.useGravity = true;
		}

		// Token: 0x0600A9B6 RID: 43446 RVA: 0x0035354B File Offset: 0x0035174B
		public override void OnEnter()
		{
			this.DoUseGravity();
			base.Finish();
		}

		// Token: 0x0600A9B7 RID: 43447 RVA: 0x0035355C File Offset: 0x0035175C
		private void DoUseGravity()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				base.rigidbody.useGravity = this.useGravity.Value;
			}
		}

		// Token: 0x04009061 RID: 36961
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009062 RID: 36962
		[RequiredField]
		public FsmBool useGravity;
	}
}
