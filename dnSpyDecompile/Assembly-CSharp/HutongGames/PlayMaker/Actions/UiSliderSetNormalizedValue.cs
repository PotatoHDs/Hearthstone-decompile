using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E95 RID: 3733
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the normalized value ( between 0 and 1) of a UI Slider component.")]
	public class UiSliderSetNormalizedValue : ComponentAction<Slider>
	{
		// Token: 0x0600A98A RID: 43402 RVA: 0x00352EBA File Offset: 0x003510BA
		public override void Reset()
		{
			this.gameObject = null;
			this.value = null;
			this.resetOnExit = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A98B RID: 43403 RVA: 0x00352ED8 File Offset: 0x003510D8
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.slider = this.cachedComponent;
			}
			this.originalValue = this.slider.normalizedValue;
			this.DoSetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A98C RID: 43404 RVA: 0x00352F31 File Offset: 0x00351131
		public override void OnUpdate()
		{
			this.DoSetValue();
		}

		// Token: 0x0600A98D RID: 43405 RVA: 0x00352F39 File Offset: 0x00351139
		private void DoSetValue()
		{
			if (this.slider != null)
			{
				this.slider.normalizedValue = this.value.Value;
			}
		}

		// Token: 0x0600A98E RID: 43406 RVA: 0x00352F5F File Offset: 0x0035115F
		public override void OnExit()
		{
			if (this.slider == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.slider.normalizedValue = this.originalValue;
			}
		}

		// Token: 0x04009036 RID: 36918
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the UI Slider component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009037 RID: 36919
		[RequiredField]
		[HasFloatSlider(0f, 1f)]
		[Tooltip("The normalized value ( between 0 and 1) of the UI Slider component.")]
		public FsmFloat value;

		// Token: 0x04009038 RID: 36920
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x04009039 RID: 36921
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x0400903A RID: 36922
		private Slider slider;

		// Token: 0x0400903B RID: 36923
		private float originalValue;
	}
}
