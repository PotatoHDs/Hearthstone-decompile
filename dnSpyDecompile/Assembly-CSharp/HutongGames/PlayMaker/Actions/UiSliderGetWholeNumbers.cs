using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E91 RID: 3729
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the wholeNumbers property of a UI Slider component. If true, the Slider is constrained to integer values")]
	public class UiSliderGetWholeNumbers : ComponentAction<Slider>
	{
		// Token: 0x0600A976 RID: 43382 RVA: 0x00352A86 File Offset: 0x00350C86
		public override void Reset()
		{
			this.gameObject = null;
			this.isShowingWholeNumbersEvent = null;
			this.isNotShowingWholeNumbersEvent = null;
			this.wholeNumbers = null;
		}

		// Token: 0x0600A977 RID: 43383 RVA: 0x00352AA4 File Offset: 0x00350CA4
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.slider = this.cachedComponent;
			}
			this.DoGetValue();
			base.Finish();
		}

		// Token: 0x0600A978 RID: 43384 RVA: 0x00352AE4 File Offset: 0x00350CE4
		private void DoGetValue()
		{
			bool flag = false;
			if (this.slider != null)
			{
				flag = this.slider.wholeNumbers;
			}
			this.wholeNumbers.Value = flag;
			base.Fsm.Event(flag ? this.isShowingWholeNumbersEvent : this.isNotShowingWholeNumbersEvent);
		}

		// Token: 0x0400901E RID: 36894
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the UI Slider component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400901F RID: 36895
		[UIHint(UIHint.Variable)]
		[Tooltip("Is the Slider constrained to integer values?")]
		public FsmBool wholeNumbers;

		// Token: 0x04009020 RID: 36896
		[Tooltip("Event sent if slider is showing integers")]
		public FsmEvent isShowingWholeNumbersEvent;

		// Token: 0x04009021 RID: 36897
		[Tooltip("Event sent if slider is showing floats")]
		public FsmEvent isNotShowingWholeNumbersEvent;

		// Token: 0x04009022 RID: 36898
		private Slider slider;
	}
}
