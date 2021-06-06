using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D47 RID: 3399
	[ActionCategory(ActionCategory.Quaternion)]
	[Tooltip("Use a low pass filter to reduce the influence of sudden changes in a quaternion Variable.")]
	public class QuaternionLowPassFilter : QuaternionBaseAction
	{
		// Token: 0x0600A369 RID: 41833 RVA: 0x0033EFEC File Offset: 0x0033D1EC
		public override void Reset()
		{
			this.quaternionVariable = null;
			this.filteringFactor = 0.1f;
			this.everyFrame = true;
			this.everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		// Token: 0x0600A36A RID: 41834 RVA: 0x0033F014 File Offset: 0x0033D214
		public override void OnEnter()
		{
			this.filteredQuaternion = new Quaternion(this.quaternionVariable.Value.x, this.quaternionVariable.Value.y, this.quaternionVariable.Value.z, this.quaternionVariable.Value.w);
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A36B RID: 41835 RVA: 0x0033F07A File Offset: 0x0033D27A
		public override void OnUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.Update)
			{
				this.DoQuatLowPassFilter();
			}
		}

		// Token: 0x0600A36C RID: 41836 RVA: 0x0033F08A File Offset: 0x0033D28A
		public override void OnLateUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.LateUpdate)
			{
				this.DoQuatLowPassFilter();
			}
		}

		// Token: 0x0600A36D RID: 41837 RVA: 0x0033F09B File Offset: 0x0033D29B
		public override void OnFixedUpdate()
		{
			if (this.everyFrameOption == QuaternionBaseAction.everyFrameOptions.FixedUpdate)
			{
				this.DoQuatLowPassFilter();
			}
		}

		// Token: 0x0600A36E RID: 41838 RVA: 0x0033F0AC File Offset: 0x0033D2AC
		private void DoQuatLowPassFilter()
		{
			this.filteredQuaternion.x = this.quaternionVariable.Value.x * this.filteringFactor.Value + this.filteredQuaternion.x * (1f - this.filteringFactor.Value);
			this.filteredQuaternion.y = this.quaternionVariable.Value.y * this.filteringFactor.Value + this.filteredQuaternion.y * (1f - this.filteringFactor.Value);
			this.filteredQuaternion.z = this.quaternionVariable.Value.z * this.filteringFactor.Value + this.filteredQuaternion.z * (1f - this.filteringFactor.Value);
			this.filteredQuaternion.w = this.quaternionVariable.Value.w * this.filteringFactor.Value + this.filteredQuaternion.w * (1f - this.filteringFactor.Value);
			this.quaternionVariable.Value = new Quaternion(this.filteredQuaternion.x, this.filteredQuaternion.y, this.filteredQuaternion.z, this.filteredQuaternion.w);
		}

		// Token: 0x040089A1 RID: 35233
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("quaternion Variable to filter. Should generally come from some constantly updated input")]
		public FsmQuaternion quaternionVariable;

		// Token: 0x040089A2 RID: 35234
		[Tooltip("Determines how much influence new changes have. E.g., 0.1 keeps 10 percent of the unfiltered quaternion and 90 percent of the previously filtered value.")]
		public FsmFloat filteringFactor;

		// Token: 0x040089A3 RID: 35235
		private Quaternion filteredQuaternion;
	}
}
