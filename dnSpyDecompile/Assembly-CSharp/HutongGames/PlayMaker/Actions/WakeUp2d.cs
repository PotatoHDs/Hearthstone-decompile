using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D26 RID: 3366
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Forces a Game Object's Rigid Body 2D to wake up.")]
	public class WakeUp2d : ComponentAction<Rigidbody2D>
	{
		// Token: 0x0600A2C2 RID: 41666 RVA: 0x0033D74D File Offset: 0x0033B94D
		public override void Reset()
		{
			this.gameObject = null;
		}

		// Token: 0x0600A2C3 RID: 41667 RVA: 0x0033D756 File Offset: 0x0033B956
		public override void OnEnter()
		{
			this.DoWakeUp();
			base.Finish();
		}

		// Token: 0x0600A2C4 RID: 41668 RVA: 0x0033D764 File Offset: 0x0033B964
		private void DoWakeUp()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			base.rigidbody2d.WakeUp();
		}

		// Token: 0x04008927 RID: 35111
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject with a Rigidbody2d attached")]
		public FsmOwnerDefault gameObject;
	}
}
