using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E94 RID: 3732
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the minimum and maximum limits for the value of a UI Slider component. Optionally resets on exit")]
	public class UiSliderSetMinMax : ComponentAction<Slider>
	{
		// Token: 0x0600A984 RID: 43396 RVA: 0x00352D5C File Offset: 0x00350F5C
		public override void Reset()
		{
			this.gameObject = null;
			this.minValue = new FsmFloat
			{
				UseVariable = true
			};
			this.maxValue = new FsmFloat
			{
				UseVariable = true
			};
			this.resetOnExit = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A985 RID: 43397 RVA: 0x00352D98 File Offset: 0x00350F98
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.slider = this.cachedComponent;
			}
			if (this.resetOnExit.Value)
			{
				this.originalMinValue = this.slider.minValue;
				this.originalMaxValue = this.slider.maxValue;
			}
			this.DoSetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A986 RID: 43398 RVA: 0x00352E0F File Offset: 0x0035100F
		public override void OnUpdate()
		{
			this.DoSetValue();
		}

		// Token: 0x0600A987 RID: 43399 RVA: 0x00352E18 File Offset: 0x00351018
		private void DoSetValue()
		{
			if (this.slider == null)
			{
				return;
			}
			if (!this.minValue.IsNone)
			{
				this.slider.minValue = this.minValue.Value;
			}
			if (!this.maxValue.IsNone)
			{
				this.slider.maxValue = this.maxValue.Value;
			}
		}

		// Token: 0x0600A988 RID: 43400 RVA: 0x00352E7A File Offset: 0x0035107A
		public override void OnExit()
		{
			if (this.slider == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.slider.minValue = this.originalMinValue;
				this.slider.maxValue = this.originalMaxValue;
			}
		}

		// Token: 0x0400902E RID: 36910
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the UI Slider component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400902F RID: 36911
		[Tooltip("The minimum value of the UI Slider component. Leave as None for no effect")]
		public FsmFloat minValue;

		// Token: 0x04009030 RID: 36912
		[Tooltip("The maximum value of the UI Slider component. Leave as None for no effect")]
		public FsmFloat maxValue;

		// Token: 0x04009031 RID: 36913
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04009032 RID: 36914
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04009033 RID: 36915
		private Slider slider;

		// Token: 0x04009034 RID: 36916
		private float originalMinValue;

		// Token: 0x04009035 RID: 36917
		private float originalMaxValue;
	}
}
