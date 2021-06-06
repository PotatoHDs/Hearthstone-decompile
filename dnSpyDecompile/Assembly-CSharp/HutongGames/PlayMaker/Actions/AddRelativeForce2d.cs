using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CFF RID: 3327
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Adds a relative 2d force to a Game Object. Use Vector2 variable and/or Float variables for each axis.")]
	public class AddRelativeForce2d : ComponentAction<Rigidbody2D>
	{
		// Token: 0x0600A1EB RID: 41451 RVA: 0x00339810 File Offset: 0x00337A10
		public override void Reset()
		{
			this.gameObject = null;
			this.forceMode = ForceMode2D.Force;
			this.vector = null;
			this.vector3 = new FsmVector3
			{
				UseVariable = true
			};
			this.x = new FsmFloat
			{
				UseVariable = true
			};
			this.y = new FsmFloat
			{
				UseVariable = true
			};
			this.everyFrame = false;
		}

		// Token: 0x0600A1EC RID: 41452 RVA: 0x003201AC File Offset: 0x0031E3AC
		public override void OnPreprocess()
		{
			base.Fsm.HandleFixedUpdate = true;
		}

		// Token: 0x0600A1ED RID: 41453 RVA: 0x0033986F File Offset: 0x00337A6F
		public override void OnEnter()
		{
			this.DoAddRelativeForce();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A1EE RID: 41454 RVA: 0x00339885 File Offset: 0x00337A85
		public override void OnFixedUpdate()
		{
			this.DoAddRelativeForce();
		}

		// Token: 0x0600A1EF RID: 41455 RVA: 0x00339890 File Offset: 0x00337A90
		private void DoAddRelativeForce()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			Vector2 relativeForce = this.vector.IsNone ? new Vector2(this.x.Value, this.y.Value) : this.vector.Value;
			if (!this.vector3.IsNone)
			{
				relativeForce.x = this.vector3.Value.x;
				relativeForce.y = this.vector3.Value.y;
			}
			if (!this.x.IsNone)
			{
				relativeForce.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				relativeForce.y = this.y.Value;
			}
			base.rigidbody2d.AddRelativeForce(relativeForce, this.forceMode);
		}

		// Token: 0x040087FB RID: 34811
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject to apply the force to.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040087FC RID: 34812
		[Tooltip("Option for applying the force")]
		public ForceMode2D forceMode;

		// Token: 0x040087FD RID: 34813
		[UIHint(UIHint.Variable)]
		[Tooltip("A Vector2 force to add. Optionally override any axis with the X, Y parameters.")]
		public FsmVector2 vector;

		// Token: 0x040087FE RID: 34814
		[Tooltip("Force along the X axis. To leave unchanged, set to 'None'.")]
		public FsmFloat x;

		// Token: 0x040087FF RID: 34815
		[Tooltip("Force along the Y axis. To leave unchanged, set to 'None'.")]
		public FsmFloat y;

		// Token: 0x04008800 RID: 34816
		[Tooltip("A Vector3 force to add. z is ignored")]
		public FsmVector3 vector3;

		// Token: 0x04008801 RID: 34817
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
	}
}
