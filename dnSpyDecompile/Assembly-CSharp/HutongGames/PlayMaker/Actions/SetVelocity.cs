using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E01 RID: 3585
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Sets the Velocity of a Game Object. To leave any axis unchanged, set variable to 'None'. NOTE: Game object must have a rigidbody.")]
	public class SetVelocity : ComponentAction<Rigidbody>
	{
		// Token: 0x0600A6CF RID: 42703 RVA: 0x0034A570 File Offset: 0x00348770
		public override void Reset()
		{
			this.gameObject = null;
			this.vector = null;
			this.x = new FsmFloat
			{
				UseVariable = true
			};
			this.y = new FsmFloat
			{
				UseVariable = true
			};
			this.z = new FsmFloat
			{
				UseVariable = true
			};
			this.space = Space.Self;
			this.everyFrame = false;
		}

		// Token: 0x0600A6D0 RID: 42704 RVA: 0x003201AC File Offset: 0x0031E3AC
		public override void OnPreprocess()
		{
			base.Fsm.HandleFixedUpdate = true;
		}

		// Token: 0x0600A6D1 RID: 42705 RVA: 0x0034A5CF File Offset: 0x003487CF
		public override void OnEnter()
		{
			this.DoSetVelocity();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A6D2 RID: 42706 RVA: 0x0034A5CF File Offset: 0x003487CF
		public override void OnFixedUpdate()
		{
			this.DoSetVelocity();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A6D3 RID: 42707 RVA: 0x0034A5E8 File Offset: 0x003487E8
		private void DoSetVelocity()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			Vector3 vector;
			if (this.vector.IsNone)
			{
				vector = ((this.space == Space.World) ? base.rigidbody.velocity : ownerDefaultTarget.transform.InverseTransformDirection(base.rigidbody.velocity));
			}
			else
			{
				vector = this.vector.Value;
			}
			if (!this.x.IsNone)
			{
				vector.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				vector.y = this.y.Value;
			}
			if (!this.z.IsNone)
			{
				vector.z = this.z.Value;
			}
			base.rigidbody.velocity = ((this.space == Space.World) ? vector : ownerDefaultTarget.transform.TransformDirection(vector));
		}

		// Token: 0x04008D4C RID: 36172
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008D4D RID: 36173
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector;

		// Token: 0x04008D4E RID: 36174
		public FsmFloat x;

		// Token: 0x04008D4F RID: 36175
		public FsmFloat y;

		// Token: 0x04008D50 RID: 36176
		public FsmFloat z;

		// Token: 0x04008D51 RID: 36177
		public Space space;

		// Token: 0x04008D52 RID: 36178
		public bool everyFrame;
	}
}
