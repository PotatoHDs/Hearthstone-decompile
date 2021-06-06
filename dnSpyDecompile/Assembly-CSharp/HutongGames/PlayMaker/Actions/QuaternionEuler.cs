using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D43 RID: 3395
	[ActionCategory(ActionCategory.Quaternion)]
	[Tooltip("Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order).")]
	public class QuaternionEuler : QuaternionBaseAction
	{
		// Token: 0x0600A34D RID: 41805 RVA: 0x0033ED3B File Offset: 0x0033CF3B
		public override void Reset()
		{
			this.eulerAngles = null;
			this.result = null;
			this.everyFrame = true;
			this.everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		// Token: 0x0600A34E RID: 41806 RVA: 0x0033ED59 File Offset: 0x0033CF59
		public override void OnEnter()
		{
			this.DoQuatEuler();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A34F RID: 41807 RVA: 0x0033ED6F File Offset: 0x0033CF6F
		public override void OnUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.Update)
			{
				this.DoQuatEuler();
			}
		}

		// Token: 0x0600A350 RID: 41808 RVA: 0x0033ED7F File Offset: 0x0033CF7F
		public override void OnLateUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.LateUpdate)
			{
				this.DoQuatEuler();
			}
		}

		// Token: 0x0600A351 RID: 41809 RVA: 0x0033ED90 File Offset: 0x0033CF90
		public override void OnFixedUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.FixedUpdate)
			{
				this.DoQuatEuler();
			}
		}

		// Token: 0x0600A352 RID: 41810 RVA: 0x0033EDA1 File Offset: 0x0033CFA1
		private void DoQuatEuler()
		{
			this.result.Value = Quaternion.Euler(this.eulerAngles.Value);
		}

		// Token: 0x04008996 RID: 35222
		[RequiredField]
		[Tooltip("The Euler angles.")]
		public FsmVector3 eulerAngles;

		// Token: 0x04008997 RID: 35223
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the euler angles of this quaternion variable.")]
		public FsmQuaternion result;
	}
}
