using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E97 RID: 3735
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the wholeNumbers property of a UI Slider component. This defines if the slider will be constrained to integer values ")]
	public class UiSliderSetWholeNumbers : ComponentAction<Slider>
	{
		// Token: 0x0600A996 RID: 43414 RVA: 0x00353062 File Offset: 0x00351262
		public override void Reset()
		{
			this.gameObject = null;
			this.wholeNumbers = null;
			this.resetOnExit = null;
		}

		// Token: 0x0600A997 RID: 43415 RVA: 0x0035307C File Offset: 0x0035127C
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.slider = this.cachedComponent;
			}
			this.originalValue = this.slider.wholeNumbers;
			this.DoSetValue();
			base.Finish();
		}

		// Token: 0x0600A998 RID: 43416 RVA: 0x003530CD File Offset: 0x003512CD
		private void DoSetValue()
		{
			if (this.slider != null)
			{
				this.slider.wholeNumbers = this.wholeNumbers.Value;
			}
		}

		// Token: 0x0600A999 RID: 43417 RVA: 0x003530F3 File Offset: 0x003512F3
		public override void OnExit()
		{
			if (this.slider == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.slider.wholeNumbers = this.originalValue;
			}
		}

		// Token: 0x04009042 RID: 36930
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the UI Slider component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009043 RID: 36931
		[RequiredField]
		[Tooltip("Should the slider be constrained to integer values?")]
		public FsmBool wholeNumbers;

		// Token: 0x04009044 RID: 36932
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04009045 RID: 36933
		private Slider slider;

		// Token: 0x04009046 RID: 36934
		private bool originalValue;
	}
}
