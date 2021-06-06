using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D1C RID: 3356
	[Obsolete("This action is obsolete; use Constraints instead.")]
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Controls whether the rigidbody 2D should be prevented from rotating")]
	public class SetIsFixedAngle2d : ComponentAction<Rigidbody2D>
	{
		// Token: 0x0600A287 RID: 41607 RVA: 0x0033C962 File Offset: 0x0033AB62
		public override void Reset()
		{
			this.gameObject = null;
			this.isFixedAngle = false;
			this.everyFrame = false;
		}

		// Token: 0x0600A288 RID: 41608 RVA: 0x0033C97E File Offset: 0x0033AB7E
		public override void OnEnter()
		{
			this.DoSetIsFixedAngle();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A289 RID: 41609 RVA: 0x0033C994 File Offset: 0x0033AB94
		public override void OnUpdate()
		{
			this.DoSetIsFixedAngle();
		}

		// Token: 0x0600A28A RID: 41610 RVA: 0x0033C99C File Offset: 0x0033AB9C
		private void DoSetIsFixedAngle()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			if (this.isFixedAngle.Value)
			{
				base.rigidbody2d.constraints = (base.rigidbody2d.constraints | RigidbodyConstraints2D.FreezeRotation);
				return;
			}
			base.rigidbody2d.constraints = (base.rigidbody2d.constraints & ~RigidbodyConstraints2D.FreezeRotation);
		}

		// Token: 0x040088F3 RID: 35059
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject with the Rigidbody2D attached")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040088F4 RID: 35060
		[RequiredField]
		[Tooltip("The flag value")]
		public FsmBool isFixedAngle;

		// Token: 0x040088F5 RID: 35061
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
