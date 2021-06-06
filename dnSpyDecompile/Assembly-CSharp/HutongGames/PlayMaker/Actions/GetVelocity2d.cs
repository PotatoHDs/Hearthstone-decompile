using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D0D RID: 3341
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Gets the 2d Velocity of a Game Object and stores it in a Vector2 Variable or each Axis in a Float Variable. NOTE: The Game Object must have a Rigid Body 2D.")]
	public class GetVelocity2d : ComponentAction<Rigidbody2D>
	{
		// Token: 0x0600A23D RID: 41533 RVA: 0x0033B3CC File Offset: 0x003395CC
		public override void Reset()
		{
			this.gameObject = null;
			this.vector = null;
			this.x = null;
			this.y = null;
			this.space = Space.World;
			this.everyFrame = false;
		}

		// Token: 0x0600A23E RID: 41534 RVA: 0x0033B3F8 File Offset: 0x003395F8
		public override void OnEnter()
		{
			this.DoGetVelocity();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A23F RID: 41535 RVA: 0x0033B40E File Offset: 0x0033960E
		public override void OnUpdate()
		{
			this.DoGetVelocity();
		}

		// Token: 0x0600A240 RID: 41536 RVA: 0x0033B418 File Offset: 0x00339618
		private void DoGetVelocity()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			Vector2 vector = base.rigidbody2d.velocity;
			if (this.space == Space.Self)
			{
				vector = base.rigidbody2d.transform.InverseTransformDirection(vector);
			}
			this.vector.Value = vector;
			this.x.Value = vector.x;
			this.y.Value = vector.y;
		}

		// Token: 0x0400887A RID: 34938
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject with the Rigidbody2D attached")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400887B RID: 34939
		[UIHint(UIHint.Variable)]
		[Tooltip("The velocity")]
		public FsmVector2 vector;

		// Token: 0x0400887C RID: 34940
		[UIHint(UIHint.Variable)]
		[Tooltip("The x value of the velocity")]
		public FsmFloat x;

		// Token: 0x0400887D RID: 34941
		[UIHint(UIHint.Variable)]
		[Tooltip("The y value of the velocity")]
		public FsmFloat y;

		// Token: 0x0400887E RID: 34942
		[Tooltip("The space reference to express the velocity")]
		public Space space;

		// Token: 0x0400887F RID: 34943
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
