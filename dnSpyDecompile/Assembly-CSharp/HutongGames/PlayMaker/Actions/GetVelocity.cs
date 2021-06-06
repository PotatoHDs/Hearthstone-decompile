using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C96 RID: 3222
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets the Velocity of a Game Object and stores it in a Vector3 Variable or each Axis in a Float Variable. NOTE: The Game Object must have a Rigid Body.")]
	public class GetVelocity : ComponentAction<Rigidbody>
	{
		// Token: 0x0600A025 RID: 40997 RVA: 0x0033014D File Offset: 0x0032E34D
		public override void Reset()
		{
			this.gameObject = null;
			this.vector = null;
			this.x = null;
			this.y = null;
			this.z = null;
			this.space = Space.World;
			this.everyFrame = false;
		}

		// Token: 0x0600A026 RID: 40998 RVA: 0x00330180 File Offset: 0x0032E380
		public override void OnEnter()
		{
			this.DoGetVelocity();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A027 RID: 40999 RVA: 0x00330196 File Offset: 0x0032E396
		public override void OnUpdate()
		{
			this.DoGetVelocity();
		}

		// Token: 0x0600A028 RID: 41000 RVA: 0x003301A0 File Offset: 0x0032E3A0
		private void DoGetVelocity()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			Vector3 vector = base.rigidbody.velocity;
			if (this.space == Space.Self)
			{
				vector = ownerDefaultTarget.transform.InverseTransformDirection(vector);
			}
			this.vector.Value = vector;
			this.x.Value = vector.x;
			this.y.Value = vector.y;
			this.z.Value = vector.z;
		}

		// Token: 0x040085A1 RID: 34209
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;

		// Token: 0x040085A2 RID: 34210
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector;

		// Token: 0x040085A3 RID: 34211
		[UIHint(UIHint.Variable)]
		public FsmFloat x;

		// Token: 0x040085A4 RID: 34212
		[UIHint(UIHint.Variable)]
		public FsmFloat y;

		// Token: 0x040085A5 RID: 34213
		[UIHint(UIHint.Variable)]
		public FsmFloat z;

		// Token: 0x040085A6 RID: 34214
		public Space space;

		// Token: 0x040085A7 RID: 34215
		public bool everyFrame;
	}
}
