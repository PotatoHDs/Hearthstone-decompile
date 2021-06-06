using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CFE RID: 3326
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Adds a 2d force to a Game Object. Use Vector2 variable and/or Float variables for each axis.")]
	public class AddForce2d : ComponentAction<Rigidbody2D>
	{
		// Token: 0x0600A1E5 RID: 41445 RVA: 0x00339660 File Offset: 0x00337860
		public override void Reset()
		{
			this.gameObject = null;
			this.atPosition = new FsmVector2
			{
				UseVariable = true
			};
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

		// Token: 0x0600A1E6 RID: 41446 RVA: 0x003201AC File Offset: 0x0031E3AC
		public override void OnPreprocess()
		{
			base.Fsm.HandleFixedUpdate = true;
		}

		// Token: 0x0600A1E7 RID: 41447 RVA: 0x003396D1 File Offset: 0x003378D1
		public override void OnEnter()
		{
			this.DoAddForce();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A1E8 RID: 41448 RVA: 0x003396E7 File Offset: 0x003378E7
		public override void OnFixedUpdate()
		{
			this.DoAddForce();
		}

		// Token: 0x0600A1E9 RID: 41449 RVA: 0x003396F0 File Offset: 0x003378F0
		private void DoAddForce()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			Vector2 force = this.vector.IsNone ? new Vector2(this.x.Value, this.y.Value) : this.vector.Value;
			if (!this.vector3.IsNone)
			{
				force.x = this.vector3.Value.x;
				force.y = this.vector3.Value.y;
			}
			if (!this.x.IsNone)
			{
				force.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				force.y = this.y.Value;
			}
			if (!this.atPosition.IsNone)
			{
				base.rigidbody2d.AddForceAtPosition(force, this.atPosition.Value, this.forceMode);
				return;
			}
			base.rigidbody2d.AddForce(force, this.forceMode);
		}

		// Token: 0x040087F3 RID: 34803
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject to apply the force to.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040087F4 RID: 34804
		[Tooltip("Option for applying the force")]
		public ForceMode2D forceMode;

		// Token: 0x040087F5 RID: 34805
		[UIHint(UIHint.Variable)]
		[Tooltip("Optionally apply the force at a position on the object. This will also add some torque. The position is often returned from MousePick or GetCollision2dInfo actions.")]
		public FsmVector2 atPosition;

		// Token: 0x040087F6 RID: 34806
		[UIHint(UIHint.Variable)]
		[Tooltip("A Vector2 force to add. Optionally override any axis with the X, Y parameters.")]
		public FsmVector2 vector;

		// Token: 0x040087F7 RID: 34807
		[Tooltip("Force along the X axis. To leave unchanged, set to 'None'.")]
		public FsmFloat x;

		// Token: 0x040087F8 RID: 34808
		[Tooltip("Force along the Y axis. To leave unchanged, set to 'None'.")]
		public FsmFloat y;

		// Token: 0x040087F9 RID: 34809
		[Tooltip("A Vector3 force to add. z is ignored")]
		public FsmVector3 vector3;

		// Token: 0x040087FA RID: 34810
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
	}
}
