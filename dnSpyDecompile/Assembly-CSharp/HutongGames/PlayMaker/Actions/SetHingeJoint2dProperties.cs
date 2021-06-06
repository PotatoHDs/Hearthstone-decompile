using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D1B RID: 3355
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Sets the various properties of a HingeJoint2d component")]
	public class SetHingeJoint2dProperties : FsmStateAction
	{
		// Token: 0x0600A282 RID: 41602 RVA: 0x0033C730 File Offset: 0x0033A930
		public override void Reset()
		{
			this.useLimits = new FsmBool
			{
				UseVariable = true
			};
			this.min = new FsmFloat
			{
				UseVariable = true
			};
			this.max = new FsmFloat
			{
				UseVariable = true
			};
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
			this.everyFrame = false;
		}

		// Token: 0x0600A283 RID: 41603 RVA: 0x0033C7B0 File Offset: 0x0033A9B0
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this._joint = ownerDefaultTarget.GetComponent<HingeJoint2D>();
				if (this._joint != null)
				{
					this._motor = this._joint.motor;
					this._limits = this._joint.limits;
				}
			}
			this.SetProperties();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A284 RID: 41604 RVA: 0x0033C828 File Offset: 0x0033AA28
		public override void OnUpdate()
		{
			this.SetProperties();
		}

		// Token: 0x0600A285 RID: 41605 RVA: 0x0033C830 File Offset: 0x0033AA30
		private void SetProperties()
		{
			if (this._joint == null)
			{
				return;
			}
			if (!this.useMotor.IsNone)
			{
				this._joint.useMotor = this.useMotor.Value;
			}
			if (!this.motorSpeed.IsNone)
			{
				this._motor.motorSpeed = this.motorSpeed.Value;
				this._joint.motor = this._motor;
			}
			if (!this.maxMotorTorque.IsNone)
			{
				this._motor.maxMotorTorque = this.maxMotorTorque.Value;
				this._joint.motor = this._motor;
			}
			if (!this.useLimits.IsNone)
			{
				this._joint.useLimits = this.useLimits.Value;
			}
			if (!this.min.IsNone)
			{
				this._limits.min = this.min.Value;
				this._joint.limits = this._limits;
			}
			if (!this.max.IsNone)
			{
				this._limits.max = this.max.Value;
				this._joint.limits = this._limits;
			}
		}

		// Token: 0x040088E8 RID: 35048
		[RequiredField]
		[Tooltip("The HingeJoint2d target")]
		[CheckForComponent(typeof(HingeJoint2D))]
		public FsmOwnerDefault gameObject;

		// Token: 0x040088E9 RID: 35049
		[ActionSection("Limits")]
		[Tooltip("Should limits be placed on the range of rotation?")]
		public FsmBool useLimits;

		// Token: 0x040088EA RID: 35050
		[Tooltip("Lower angular limit of rotation.")]
		public FsmFloat min;

		// Token: 0x040088EB RID: 35051
		[Tooltip("Upper angular limit of rotation")]
		public FsmFloat max;

		// Token: 0x040088EC RID: 35052
		[ActionSection("Motor")]
		[Tooltip("Should a motor force be applied automatically to the Rigidbody2D?")]
		public FsmBool useMotor;

		// Token: 0x040088ED RID: 35053
		[Tooltip("The desired speed for the Rigidbody2D to reach as it moves with the joint.")]
		public FsmFloat motorSpeed;

		// Token: 0x040088EE RID: 35054
		[Tooltip("The maximum force that can be applied to the Rigidbody2D at the joint to attain the target speed.")]
		public FsmFloat maxMotorTorque;

		// Token: 0x040088EF RID: 35055
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		// Token: 0x040088F0 RID: 35056
		private HingeJoint2D _joint;

		// Token: 0x040088F1 RID: 35057
		private JointMotor2D _motor;

		// Token: 0x040088F2 RID: 35058
		private JointAngleLimits2D _limits;
	}
}
