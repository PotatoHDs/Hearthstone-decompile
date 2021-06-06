using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D44 RID: 3396
	[ActionCategory(ActionCategory.Quaternion)]
	[Tooltip("Inverse a quaternion")]
	public class QuaternionInverse : QuaternionBaseAction
	{
		// Token: 0x0600A354 RID: 41812 RVA: 0x0033EDBE File Offset: 0x0033CFBE
		public override void Reset()
		{
			this.rotation = null;
			this.result = null;
			this.everyFrame = true;
			this.everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		// Token: 0x0600A355 RID: 41813 RVA: 0x0033EDDC File Offset: 0x0033CFDC
		public override void OnEnter()
		{
			this.DoQuatInverse();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A356 RID: 41814 RVA: 0x0033EDF2 File Offset: 0x0033CFF2
		public override void OnUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.Update)
			{
				this.DoQuatInverse();
			}
		}

		// Token: 0x0600A357 RID: 41815 RVA: 0x0033EE02 File Offset: 0x0033D002
		public override void OnLateUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.LateUpdate)
			{
				this.DoQuatInverse();
			}
		}

		// Token: 0x0600A358 RID: 41816 RVA: 0x0033EE13 File Offset: 0x0033D013
		public override void OnFixedUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.FixedUpdate)
			{
				this.DoQuatInverse();
			}
		}

		// Token: 0x0600A359 RID: 41817 RVA: 0x0033EE24 File Offset: 0x0033D024
		private void DoQuatInverse()
		{
			this.result.Value = Quaternion.Inverse(this.rotation.Value);
		}

		// Token: 0x04008998 RID: 35224
		[RequiredField]
		[Tooltip("the rotation")]
		public FsmQuaternion rotation;

		// Token: 0x04008999 RID: 35225
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the inverse of the rotation variable.")]
		public FsmQuaternion result;
	}
}
