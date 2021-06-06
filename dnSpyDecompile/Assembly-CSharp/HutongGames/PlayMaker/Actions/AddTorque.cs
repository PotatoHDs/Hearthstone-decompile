using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BA2 RID: 2978
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Adds torque (rotational force) to a Game Object.")]
	public class AddTorque : ComponentAction<Rigidbody>
	{
		// Token: 0x06009BBB RID: 39867 RVA: 0x00320580 File Offset: 0x0031E780
		public override void Reset()
		{
			this.gameObject = null;
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

		// Token: 0x06009BBC RID: 39868 RVA: 0x003201AC File Offset: 0x0031E3AC
		public override void OnPreprocess()
		{
			base.Fsm.HandleFixedUpdate = true;
		}

		// Token: 0x06009BBD RID: 39869 RVA: 0x003205DF File Offset: 0x0031E7DF
		public override void OnEnter()
		{
			this.DoAddTorque();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009BBE RID: 39870 RVA: 0x003205F5 File Offset: 0x0031E7F5
		public override void OnFixedUpdate()
		{
			this.DoAddTorque();
		}

		// Token: 0x06009BBF RID: 39871 RVA: 0x00320600 File Offset: 0x0031E800
		private void DoAddTorque()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			Vector3 torque = this.vector.IsNone ? new Vector3(this.x.Value, this.y.Value, this.z.Value) : this.vector.Value;
			if (!this.x.IsNone)
			{
				torque.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				torque.y = this.y.Value;
			}
			if (!this.z.IsNone)
			{
				torque.z = this.z.Value;
			}
			if (this.space == Space.World)
			{
				base.rigidbody.AddTorque(torque, this.forceMode);
				return;
			}
			base.rigidbody.AddRelativeTorque(torque, this.forceMode);
		}

		// Token: 0x0400810A RID: 33034
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		[Tooltip("The GameObject to add torque to.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400810B RID: 33035
		[UIHint(UIHint.Variable)]
		[Tooltip("A Vector3 torque. Optionally override any axis with the X, Y, Z parameters.")]
		public FsmVector3 vector;

		// Token: 0x0400810C RID: 33036
		[Tooltip("Torque around the X axis. To leave unchanged, set to 'None'.")]
		public FsmFloat x;

		// Token: 0x0400810D RID: 33037
		[Tooltip("Torque around the Y axis. To leave unchanged, set to 'None'.")]
		public FsmFloat y;

		// Token: 0x0400810E RID: 33038
		[Tooltip("Torque around the Z axis. To leave unchanged, set to 'None'.")]
		public FsmFloat z;

		// Token: 0x0400810F RID: 33039
		[Tooltip("Apply the force in world or local space.")]
		public Space space;

		// Token: 0x04008110 RID: 33040
		[Tooltip("The type of force to apply. See Unity Physics docs.")]
		public ForceMode forceMode;

		// Token: 0x04008111 RID: 33041
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
	}
}
