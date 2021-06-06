using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C52 RID: 3154
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Gets the last measured linear acceleration of a device and stores it in a Vector3 Variable.")]
	public class GetDeviceAcceleration : FsmStateAction
	{
		// Token: 0x06009EF3 RID: 40691 RVA: 0x0032CED2 File Offset: 0x0032B0D2
		public override void Reset()
		{
			this.storeVector = null;
			this.storeX = null;
			this.storeY = null;
			this.storeZ = null;
			this.multiplier = 1f;
			this.everyFrame = false;
		}

		// Token: 0x06009EF4 RID: 40692 RVA: 0x0032CF07 File Offset: 0x0032B107
		public override void OnEnter()
		{
			this.DoGetDeviceAcceleration();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009EF5 RID: 40693 RVA: 0x0032CF1D File Offset: 0x0032B11D
		public override void OnUpdate()
		{
			this.DoGetDeviceAcceleration();
		}

		// Token: 0x06009EF6 RID: 40694 RVA: 0x0032CF28 File Offset: 0x0032B128
		private void DoGetDeviceAcceleration()
		{
			Vector3 vector = new Vector3(Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
			if (!this.multiplier.IsNone)
			{
				vector *= this.multiplier.Value;
			}
			this.storeVector.Value = vector;
			this.storeX.Value = vector.x;
			this.storeY.Value = vector.y;
			this.storeZ.Value = vector.z;
		}

		// Token: 0x04008453 RID: 33875
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeVector;

		// Token: 0x04008454 RID: 33876
		[UIHint(UIHint.Variable)]
		public FsmFloat storeX;

		// Token: 0x04008455 RID: 33877
		[UIHint(UIHint.Variable)]
		public FsmFloat storeY;

		// Token: 0x04008456 RID: 33878
		[UIHint(UIHint.Variable)]
		public FsmFloat storeZ;

		// Token: 0x04008457 RID: 33879
		public FsmFloat multiplier;

		// Token: 0x04008458 RID: 33880
		public bool everyFrame;
	}
}
