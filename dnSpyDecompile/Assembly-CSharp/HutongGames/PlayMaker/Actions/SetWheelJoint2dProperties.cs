using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D20 RID: 3360
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Sets the various properties of a WheelJoint2d component")]
	public class SetWheelJoint2dProperties : FsmStateAction
	{
		// Token: 0x0600A29A RID: 41626 RVA: 0x0033CBBC File Offset: 0x0033ADBC
		public override void Reset()
		{
			this.useMotor = new FsmBool
			{
				UseVariable = true
			};
			this.motorSpeed = new FsmFloat
			{
				UseVariable = true
			};
			this.maxMotorTorque = new FsmFloat
			{
				UseVariable = true
			};
			this.angle = new FsmFloat
			{
				UseVariable = true
			};
			this.dampingRatio = new FsmFloat
			{
				UseVariable = true
			};
			this.frequency = new FsmFloat
			{
				UseVariable = true
			};
			this.everyFrame = false;
		}

		// Token: 0x0600A29B RID: 41627 RVA: 0x0033CC3C File Offset: 0x0033AE3C
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this._wj2d = ownerDefaultTarget.GetComponent<WheelJoint2D>();
				if (this._wj2d != null)
				{
					this._motor = this._wj2d.motor;
					this._suspension = this._wj2d.suspension;
				}
			}
			this.SetProperties();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A29C RID: 41628 RVA: 0x0033CCB4 File Offset: 0x0033AEB4
		public override void OnUpdate()
		{
			this.SetProperties();
		}

		// Token: 0x0600A29D RID: 41629 RVA: 0x0033CCBC File Offset: 0x0033AEBC
		private void SetProperties()
		{
			if (this._wj2d == null)
			{
				return;
			}
			if (!this.useMotor.IsNone)
			{
				this._wj2d.useMotor = this.useMotor.Value;
			}
			if (!this.motorSpeed.IsNone)
			{
				this._motor.motorSpeed = this.motorSpeed.Value;
				this._wj2d.motor = this._motor;
			}
			if (!this.maxMotorTorque.IsNone)
			{
				this._motor.maxMotorTorque = this.maxMotorTorque.Value;
				this._wj2d.motor = this._motor;
			}
			if (!this.angle.IsNone)
			{
				this._suspension.angle = this.angle.Value;
				this._wj2d.suspension = this._suspension;
			}
			if (!this.dampingRatio.IsNone)
			{
				this._suspension.dampingRatio = this.dampingRatio.Value;
				this._wj2d.suspension = this._suspension;
			}
			if (!this.frequency.IsNone)
			{
				this._suspension.frequency = this.frequency.Value;
				this._wj2d.suspension = this._suspension;
			}
		}

		// Token: 0x040088FF RID: 35071
		[RequiredField]
		[Tooltip("The WheelJoint2d target")]
		[CheckForComponent(typeof(WheelJoint2D))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008900 RID: 35072
		[ActionSection("Motor")]
		[Tooltip("Should a motor force be applied automatically to the Rigidbody2D?")]
		public FsmBool useMotor;

		// Token: 0x04008901 RID: 35073
		[Tooltip("The desired speed for the Rigidbody2D to reach as it moves with the joint.")]
		public FsmFloat motorSpeed;

		// Token: 0x04008902 RID: 35074
		[Tooltip("The maximum force that can be applied to the Rigidbody2D at the joint to attain the target speed.")]
		public FsmFloat maxMotorTorque;

		// Token: 0x04008903 RID: 35075
		[ActionSection("Suspension")]
		[Tooltip("The world angle along which the suspension will move. This provides 2D constrained motion similar to a SliderJoint2D. This is typically how suspension works in the real world.")]
		public FsmFloat angle;

		// Token: 0x04008904 RID: 35076
		[Tooltip("The amount by which the suspension spring force is reduced in proportion to the movement speed.")]
		public FsmFloat dampingRatio;

		// Token: 0x04008905 RID: 35077
		[Tooltip("The frequency at which the suspension spring oscillates.")]
		public FsmFloat frequency;

		// Token: 0x04008906 RID: 35078
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		// Token: 0x04008907 RID: 35079
		private WheelJoint2D _wj2d;

		// Token: 0x04008908 RID: 35080
		private JointMotor2D _motor;

		// Token: 0x04008909 RID: 35081
		private JointSuspension2D _suspension;
	}
}
