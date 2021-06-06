using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D40 RID: 3392
	[ActionCategory(ActionCategory.Quaternion)]
	[Tooltip("Creates a rotation which rotates angle degrees around axis.")]
	public class QuaternionAngleAxis : QuaternionBaseAction
	{
		// Token: 0x0600A33D RID: 41789 RVA: 0x0033EB66 File Offset: 0x0033CD66
		public override void Reset()
		{
			this.angle = null;
			this.axis = null;
			this.result = null;
			this.everyFrame = true;
			this.everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		// Token: 0x0600A33E RID: 41790 RVA: 0x0033EB8B File Offset: 0x0033CD8B
		public override void OnEnter()
		{
			this.DoQuatAngleAxis();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A33F RID: 41791 RVA: 0x0033EBA1 File Offset: 0x0033CDA1
		public override void OnUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.Update)
			{
				this.DoQuatAngleAxis();
			}
		}

		// Token: 0x0600A340 RID: 41792 RVA: 0x0033EBB1 File Offset: 0x0033CDB1
		public override void OnLateUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.LateUpdate)
			{
				this.DoQuatAngleAxis();
			}
		}

		// Token: 0x0600A341 RID: 41793 RVA: 0x0033EBC2 File Offset: 0x0033CDC2
		public override void OnFixedUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.FixedUpdate)
			{
				this.DoQuatAngleAxis();
			}
		}

		// Token: 0x0600A342 RID: 41794 RVA: 0x0033EBD3 File Offset: 0x0033CDD3
		private void DoQuatAngleAxis()
		{
			this.result.Value = Quaternion.AngleAxis(this.angle.Value, this.axis.Value);
		}

		// Token: 0x0400898C RID: 35212
		[RequiredField]
		[Tooltip("The angle.")]
		public FsmFloat angle;

		// Token: 0x0400898D RID: 35213
		[RequiredField]
		[Tooltip("The rotation axis.")]
		public FsmVector3 axis;

		// Token: 0x0400898E RID: 35214
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the rotation of this quaternion variable.")]
		public FsmQuaternion result;
	}
}
