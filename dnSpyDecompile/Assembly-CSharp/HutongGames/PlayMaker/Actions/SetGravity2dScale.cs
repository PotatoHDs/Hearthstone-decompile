using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D1A RID: 3354
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Sets The degree to which this object is affected by gravity.  NOTE: Game object must have a rigidbody 2D.")]
	public class SetGravity2dScale : ComponentAction<Rigidbody2D>
	{
		// Token: 0x0600A27E RID: 41598 RVA: 0x0033C6C9 File Offset: 0x0033A8C9
		public override void Reset()
		{
			this.gameObject = null;
			this.gravityScale = 1f;
		}

		// Token: 0x0600A27F RID: 41599 RVA: 0x0033C6E2 File Offset: 0x0033A8E2
		public override void OnEnter()
		{
			this.DoSetGravityScale();
			base.Finish();
		}

		// Token: 0x0600A280 RID: 41600 RVA: 0x0033C6F0 File Offset: 0x0033A8F0
		private void DoSetGravityScale()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			base.rigidbody2d.gravityScale = this.gravityScale.Value;
		}

		// Token: 0x040088E6 RID: 35046
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject with a Rigidbody 2d attached")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040088E7 RID: 35047
		[RequiredField]
		[Tooltip("The gravity scale effect")]
		public FsmFloat gravityScale;
	}
}
