using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DFB RID: 3579
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Sets a Game Object's Tag.")]
	public class SetTag : FsmStateAction
	{
		// Token: 0x0600A6B4 RID: 42676 RVA: 0x00349F68 File Offset: 0x00348168
		public override void Reset()
		{
			this.gameObject = null;
			this.tag = "Untagged";
		}

		// Token: 0x0600A6B5 RID: 42677 RVA: 0x00349F84 File Offset: 0x00348184
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				ownerDefaultTarget.tag = this.tag.Value;
			}
			base.Finish();
		}

		// Token: 0x04008D31 RID: 36145
		public FsmOwnerDefault gameObject;

		// Token: 0x04008D32 RID: 36146
		[UIHint(UIHint.Tag)]
		public FsmString tag;
	}
}
