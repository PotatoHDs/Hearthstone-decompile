using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E8E RID: 3726
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the minimum and maximum limits for the value of a UI Slider component.")]
	public class UiSliderGetMinMax : ComponentAction<Slider>
	{
		// Token: 0x0600A968 RID: 43368 RVA: 0x003528B3 File Offset: 0x00350AB3
		public override void Reset()
		{
			this.gameObject = null;
			this.minValue = null;
			this.maxValue = null;
		}

		// Token: 0x0600A969 RID: 43369 RVA: 0x003528CC File Offset: 0x00350ACC
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.slider = this.cachedComponent;
			}
			this.DoGetValue();
		}

		// Token: 0x0600A96A RID: 43370 RVA: 0x00352908 File Offset: 0x00350B08
		private void DoGetValue()
		{
			if (this.slider != null)
			{
				if (!this.minValue.IsNone)
				{
					this.minValue.Value = this.slider.minValue;
				}
				if (!this.maxValue.IsNone)
				{
					this.maxValue.Value = this.slider.maxValue;
				}
			}
		}

		// Token: 0x04009012 RID: 36882
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the UI Slider component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009013 RID: 36883
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the minimum value of the UI Slider.")]
		public FsmFloat minValue;

		// Token: 0x04009014 RID: 36884
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the maximum value of the UI Slider.")]
		public FsmFloat maxValue;

		// Token: 0x04009015 RID: 36885
		private Slider slider;
	}
}
