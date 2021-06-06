using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D3C RID: 3388
	[ActionCategory(ActionCategory.Quaternion)]
	[Tooltip("Gets a quaternion as euler angles.")]
	public class GetQuaternionEulerAngles : QuaternionBaseAction
	{
		// Token: 0x0600A321 RID: 41761 RVA: 0x0033E90D File Offset: 0x0033CB0D
		public override void Reset()
		{
			this.quaternion = null;
			this.eulerAngles = null;
			this.everyFrame = true;
			this.everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		// Token: 0x0600A322 RID: 41762 RVA: 0x0033E92B File Offset: 0x0033CB2B
		public override void OnEnter()
		{
			this.GetQuatEuler();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A323 RID: 41763 RVA: 0x0033E941 File Offset: 0x0033CB41
		public override void OnUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.Update)
			{
				this.GetQuatEuler();
			}
		}

		// Token: 0x0600A324 RID: 41764 RVA: 0x0033E951 File Offset: 0x0033CB51
		public override void OnLateUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.LateUpdate)
			{
				this.GetQuatEuler();
			}
		}

		// Token: 0x0600A325 RID: 41765 RVA: 0x0033E962 File Offset: 0x0033CB62
		public override void OnFixedUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.FixedUpdate)
			{
				this.GetQuatEuler();
			}
		}

		// Token: 0x0600A326 RID: 41766 RVA: 0x0033E974 File Offset: 0x0033CB74
		private void GetQuatEuler()
		{
			this.eulerAngles.Value = this.quaternion.Value.eulerAngles;
		}

		// Token: 0x04008981 RID: 35201
		[RequiredField]
		[Tooltip("The rotation")]
		public FsmQuaternion quaternion;

		// Token: 0x04008982 RID: 35202
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The euler angles of the quaternion.")]
		public FsmVector3 eulerAngles;
	}
}
