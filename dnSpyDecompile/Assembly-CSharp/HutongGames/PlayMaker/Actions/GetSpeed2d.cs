using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D0B RID: 3339
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Gets the 2d Speed of a Game Object and stores it in a Float Variable. NOTE: The Game Object must have a rigid body 2D.")]
	public class GetSpeed2d : ComponentAction<Rigidbody2D>
	{
		// Token: 0x0600A234 RID: 41524 RVA: 0x0033B286 File Offset: 0x00339486
		public override void Reset()
		{
			this.gameObject = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A235 RID: 41525 RVA: 0x0033B29D File Offset: 0x0033949D
		public override void OnEnter()
		{
			this.DoGetSpeed();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A236 RID: 41526 RVA: 0x0033B2B3 File Offset: 0x003394B3
		public override void OnUpdate()
		{
			this.DoGetSpeed();
		}

		// Token: 0x0600A237 RID: 41527 RVA: 0x0033B2BC File Offset: 0x003394BC
		private void DoGetSpeed()
		{
			if (this.storeResult.IsNone)
			{
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			this.storeResult.Value = base.rigidbody2d.velocity.magnitude;
		}

		// Token: 0x04008874 RID: 34932
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject with the Rigidbody2D attached")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008875 RID: 34933
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The speed, or in technical terms: velocity magnitude")]
		public FsmFloat storeResult;

		// Token: 0x04008876 RID: 34934
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
