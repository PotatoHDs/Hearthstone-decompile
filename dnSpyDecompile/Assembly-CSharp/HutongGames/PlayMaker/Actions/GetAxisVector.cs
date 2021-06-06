using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C46 RID: 3142
	[NoActionTargets]
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Gets a world direction Vector from 2 Input Axis. Typically used for a third person controller with Relative To set to the camera.")]
	public class GetAxisVector : FsmStateAction
	{
		// Token: 0x06009EC1 RID: 40641 RVA: 0x0032C444 File Offset: 0x0032A644
		public override void Reset()
		{
			this.horizontalAxis = "Horizontal";
			this.verticalAxis = "Vertical";
			this.multiplier = 1f;
			this.mapToPlane = GetAxisVector.AxisPlane.XZ;
			this.storeVector = null;
			this.storeMagnitude = null;
		}

		// Token: 0x06009EC2 RID: 40642 RVA: 0x0032C498 File Offset: 0x0032A698
		public override void OnUpdate()
		{
			Vector3 vector = default(Vector3);
			Vector3 a = default(Vector3);
			if (this.relativeTo.Value == null)
			{
				switch (this.mapToPlane)
				{
				case GetAxisVector.AxisPlane.XZ:
					vector = Vector3.forward;
					a = Vector3.right;
					break;
				case GetAxisVector.AxisPlane.XY:
					vector = Vector3.up;
					a = Vector3.right;
					break;
				case GetAxisVector.AxisPlane.YZ:
					vector = Vector3.up;
					a = Vector3.forward;
					break;
				}
			}
			else
			{
				Transform transform = this.relativeTo.Value.transform;
				GetAxisVector.AxisPlane axisPlane = this.mapToPlane;
				if (axisPlane != GetAxisVector.AxisPlane.XZ)
				{
					if (axisPlane - GetAxisVector.AxisPlane.XY <= 1)
					{
						vector = Vector3.up;
						vector.z = 0f;
						vector = vector.normalized;
						a = transform.TransformDirection(Vector3.right);
					}
				}
				else
				{
					vector = transform.TransformDirection(Vector3.forward);
					vector.y = 0f;
					vector = vector.normalized;
					a = new Vector3(vector.z, 0f, -vector.x);
				}
			}
			float d = (this.horizontalAxis.IsNone || string.IsNullOrEmpty(this.horizontalAxis.Value)) ? 0f : Input.GetAxis(this.horizontalAxis.Value);
			float d2 = (this.verticalAxis.IsNone || string.IsNullOrEmpty(this.verticalAxis.Value)) ? 0f : Input.GetAxis(this.verticalAxis.Value);
			Vector3 vector2 = d * a + d2 * vector;
			vector2 *= this.multiplier.Value;
			this.storeVector.Value = vector2;
			if (!this.storeMagnitude.IsNone)
			{
				this.storeMagnitude.Value = vector2.magnitude;
			}
		}

		// Token: 0x0400841D RID: 33821
		[Tooltip("The name of the horizontal input axis. See Unity Input Manager.")]
		public FsmString horizontalAxis;

		// Token: 0x0400841E RID: 33822
		[Tooltip("The name of the vertical input axis. See Unity Input Manager.")]
		public FsmString verticalAxis;

		// Token: 0x0400841F RID: 33823
		[Tooltip("Input axis are reported in the range -1 to 1, this multiplier lets you set a new range.")]
		public FsmFloat multiplier;

		// Token: 0x04008420 RID: 33824
		[RequiredField]
		[Tooltip("The world plane to map the 2d input onto.")]
		public GetAxisVector.AxisPlane mapToPlane;

		// Token: 0x04008421 RID: 33825
		[Tooltip("Make the result relative to a GameObject, typically the main camera.")]
		public FsmGameObject relativeTo;

		// Token: 0x04008422 RID: 33826
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the direction vector.")]
		public FsmVector3 storeVector;

		// Token: 0x04008423 RID: 33827
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the length of the direction vector.")]
		public FsmFloat storeMagnitude;

		// Token: 0x02002798 RID: 10136
		public enum AxisPlane
		{
			// Token: 0x0400F4C2 RID: 62658
			XZ,
			// Token: 0x0400F4C3 RID: 62659
			XY,
			// Token: 0x0400F4C4 RID: 62660
			YZ
		}
	}
}
