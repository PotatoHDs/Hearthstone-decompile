using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D3D RID: 3389
	[ActionCategory(ActionCategory.Quaternion)]
	[Tooltip("Creates a rotation which rotates from fromDirection to toDirection. Usually you use this to rotate a transform so that one of its axes, e.g., the y-axis - follows a target direction toDirection in world space.")]
	public class GetQuaternionFromRotation : QuaternionBaseAction
	{
		// Token: 0x0600A328 RID: 41768 RVA: 0x0033E9A7 File Offset: 0x0033CBA7
		public override void Reset()
		{
			this.fromDirection = null;
			this.toDirection = null;
			this.result = null;
			this.everyFrame = false;
			this.everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		// Token: 0x0600A329 RID: 41769 RVA: 0x0033E9CC File Offset: 0x0033CBCC
		public override void OnEnter()
		{
			this.DoQuatFromRotation();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A32A RID: 41770 RVA: 0x0033E9E2 File Offset: 0x0033CBE2
		public override void OnUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.Update)
			{
				this.DoQuatFromRotation();
			}
		}

		// Token: 0x0600A32B RID: 41771 RVA: 0x0033E9F2 File Offset: 0x0033CBF2
		public override void OnLateUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.LateUpdate)
			{
				this.DoQuatFromRotation();
			}
		}

		// Token: 0x0600A32C RID: 41772 RVA: 0x0033EA03 File Offset: 0x0033CC03
		public override void OnFixedUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.FixedUpdate)
			{
				this.DoQuatFromRotation();
			}
		}

		// Token: 0x0600A32D RID: 41773 RVA: 0x0033EA14 File Offset: 0x0033CC14
		private void DoQuatFromRotation()
		{
			this.result.Value = Quaternion.FromToRotation(this.fromDirection.Value, this.toDirection.Value);
		}

		// Token: 0x04008983 RID: 35203
		[RequiredField]
		[Tooltip("the 'from' direction")]
		public FsmVector3 fromDirection;

		// Token: 0x04008984 RID: 35204
		[RequiredField]
		[Tooltip("the 'to' direction")]
		public FsmVector3 toDirection;

		// Token: 0x04008985 RID: 35205
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("the resulting quaternion")]
		public FsmQuaternion result;
	}
}
