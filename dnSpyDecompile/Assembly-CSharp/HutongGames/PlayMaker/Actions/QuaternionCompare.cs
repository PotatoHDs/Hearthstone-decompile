using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D42 RID: 3394
	[ActionCategory(ActionCategory.Quaternion)]
	[Tooltip("Check if two quaternions are equals or not. Takes in account inversed representations of quaternions")]
	public class QuaternionCompare : QuaternionBaseAction
	{
		// Token: 0x0600A346 RID: 41798 RVA: 0x0033EC3C File Offset: 0x0033CE3C
		public override void Reset()
		{
			this.Quaternion1 = new FsmQuaternion
			{
				UseVariable = true
			};
			this.Quaternion2 = new FsmQuaternion
			{
				UseVariable = true
			};
			this.equal = null;
			this.equalEvent = null;
			this.notEqualEvent = null;
			this.everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		// Token: 0x0600A347 RID: 41799 RVA: 0x0033EC89 File Offset: 0x0033CE89
		public override void OnEnter()
		{
			this.DoQuatCompare();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A348 RID: 41800 RVA: 0x0033EC9F File Offset: 0x0033CE9F
		public override void OnUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.Update)
			{
				this.DoQuatCompare();
			}
		}

		// Token: 0x0600A349 RID: 41801 RVA: 0x0033ECAF File Offset: 0x0033CEAF
		public override void OnLateUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.LateUpdate)
			{
				this.DoQuatCompare();
			}
		}

		// Token: 0x0600A34A RID: 41802 RVA: 0x0033ECC0 File Offset: 0x0033CEC0
		public override void OnFixedUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.FixedUpdate)
			{
				this.DoQuatCompare();
			}
		}

		// Token: 0x0600A34B RID: 41803 RVA: 0x0033ECD4 File Offset: 0x0033CED4
		private void DoQuatCompare()
		{
			bool flag = Mathf.Abs(Quaternion.Dot(this.Quaternion1.Value, this.Quaternion2.Value)) > 0.999999f;
			this.equal.Value = flag;
			if (flag)
			{
				base.Fsm.Event(this.equalEvent);
				return;
			}
			base.Fsm.Event(this.notEqualEvent);
		}

		// Token: 0x04008991 RID: 35217
		[RequiredField]
		[Tooltip("First Quaternion")]
		public FsmQuaternion Quaternion1;

		// Token: 0x04008992 RID: 35218
		[RequiredField]
		[Tooltip("Second Quaternion")]
		public FsmQuaternion Quaternion2;

		// Token: 0x04008993 RID: 35219
		[Tooltip("true if Quaternions are equal")]
		public FsmBool equal;

		// Token: 0x04008994 RID: 35220
		[Tooltip("Event sent if Quaternions are equal")]
		public FsmEvent equalEvent;

		// Token: 0x04008995 RID: 35221
		[Tooltip("Event sent if Quaternions are not equal")]
		public FsmEvent notEqualEvent;
	}
}
