using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D21 RID: 3361
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Forces a Game Object's Rigid Body 2D to Sleep at least one frame.")]
	public class Sleep2d : ComponentAction<Rigidbody2D>
	{
		// Token: 0x0600A29F RID: 41631 RVA: 0x0033CDFF File Offset: 0x0033AFFF
		public override void Reset()
		{
			this.gameObject = null;
		}

		// Token: 0x0600A2A0 RID: 41632 RVA: 0x0033CE08 File Offset: 0x0033B008
		public override void OnEnter()
		{
			this.DoSleep();
			base.Finish();
		}

		// Token: 0x0600A2A1 RID: 41633 RVA: 0x0033CE18 File Offset: 0x0033B018
		private void DoSleep()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			base.rigidbody2d.Sleep();
		}

		// Token: 0x0400890A RID: 35082
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject with a Rigidbody2d attached")]
		public FsmOwnerDefault gameObject;
	}
}
