using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000B9F RID: 2975
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Adds a force to a Game Object. Use Vector3 variable and/or Float variables for each axis.")]
	public class AddForce : ComponentAction<Rigidbody>
	{
		// Token: 0x06009BAC RID: 39852 RVA: 0x00320264 File Offset: 0x0031E464
		public override void Reset()
		{
			this.gameObject = null;
			this.atPosition = new FsmVector3
			{
				UseVariable = true
			};
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
			this.space = Space.World;
			this.forceMode = ForceMode.Force;
			this.everyFrame = false;
		}

		// Token: 0x06009BAD RID: 39853 RVA: 0x003201AC File Offset: 0x0031E3AC
		public override void OnPreprocess()
		{
			base.Fsm.HandleFixedUpdate = true;
		}

		// Token: 0x06009BAE RID: 39854 RVA: 0x003202DC File Offset: 0x0031E4DC
		public override void OnEnter()
		{
			this.DoAddForce();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009BAF RID: 39855 RVA: 0x003202F2 File Offset: 0x0031E4F2
		public override void OnFixedUpdate()
		{
			this.DoAddForce();
		}

		// Token: 0x06009BB0 RID: 39856 RVA: 0x003202FC File Offset: 0x0031E4FC
		private void DoAddForce()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			Vector3 force = this.vector.IsNone ? default(Vector3) : this.vector.Value;
			if (!this.x.IsNone)
			{
				force.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				force.y = this.y.Value;
			}
			if (!this.z.IsNone)
			{
				force.z = this.z.Value;
			}
			if (this.space != Space.World)
			{
				base.rigidbody.AddRelativeForce(force, this.forceMode);
				return;
			}
			if (!this.atPosition.IsNone)
			{
				base.rigidbody.AddForceAtPosition(force, this.atPosition.Value, this.forceMode);
				return;
			}
			base.rigidbody.AddForce(force, this.forceMode);
		}

		// Token: 0x040080F9 RID: 33017
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		[Tooltip("The GameObject to apply the force to.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x040080FA RID: 33018
		[UIHint(UIHint.Variable)]
		[Tooltip("Optionally apply the force at a position on the object. This will also add some torque. The position is often returned from MousePick or GetCollisionInfo actions.")]
		public FsmVector3 atPosition;

		// Token: 0x040080FB RID: 33019
		[UIHint(UIHint.Variable)]
		[Tooltip("A Vector3 force to add. Optionally override any axis with the X, Y, Z parameters.")]
		public FsmVector3 vector;

		// Token: 0x040080FC RID: 33020
		[Tooltip("Force along the X axis. To leave unchanged, set to 'None'.")]
		public FsmFloat x;

		// Token: 0x040080FD RID: 33021
		[Tooltip("Force along the Y axis. To leave unchanged, set to 'None'.")]
		public FsmFloat y;

		// Token: 0x040080FE RID: 33022
		[Tooltip("Force along the Z axis. To leave unchanged, set to 'None'.")]
		public FsmFloat z;

		// Token: 0x040080FF RID: 33023
		[Tooltip("Apply the force in world or local space.")]
		public Space space;

		// Token: 0x04008100 RID: 33024
		[Tooltip("The type of force to apply. See Unity Physics docs.")]
		public ForceMode forceMode;

		// Token: 0x04008101 RID: 33025
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
	}
}
