using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DCC RID: 3532
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Sets the gravity vector, or individual axis.")]
	public class SetGravity : FsmStateAction
	{
		// Token: 0x0600A5EB RID: 42475 RVA: 0x00348290 File Offset: 0x00346490
		public override void Reset()
		{
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
			this.everyFrame = false;
		}

		// Token: 0x0600A5EC RID: 42476 RVA: 0x003482E1 File Offset: 0x003464E1
		public override void OnEnter()
		{
			this.DoSetGravity();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A5ED RID: 42477 RVA: 0x003482F7 File Offset: 0x003464F7
		public override void OnUpdate()
		{
			this.DoSetGravity();
		}

		// Token: 0x0600A5EE RID: 42478 RVA: 0x00348300 File Offset: 0x00346500
		private void DoSetGravity()
		{
			Vector3 value = this.vector.Value;
			if (!this.x.IsNone)
			{
				value.x = this.x.Value;
			}
			if (!this.y.IsNone)
			{
				value.y = this.y.Value;
			}
			if (!this.z.IsNone)
			{
				value.z = this.z.Value;
			}
			Physics.gravity = value;
		}

		// Token: 0x04008C98 RID: 35992
		public FsmVector3 vector;

		// Token: 0x04008C99 RID: 35993
		public FsmFloat x;

		// Token: 0x04008C9A RID: 35994
		public FsmFloat y;

		// Token: 0x04008C9B RID: 35995
		public FsmFloat z;

		// Token: 0x04008C9C RID: 35996
		public bool everyFrame;
	}
}
