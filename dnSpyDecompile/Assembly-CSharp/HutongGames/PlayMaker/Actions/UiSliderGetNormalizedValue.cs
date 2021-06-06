using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E8F RID: 3727
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the normalized value (between 0 and 1) of a UI Slider component.")]
	public class UiSliderGetNormalizedValue : ComponentAction<Slider>
	{
		// Token: 0x0600A96C RID: 43372 RVA: 0x00352969 File Offset: 0x00350B69
		public override void Reset()
		{
			this.gameObject = null;
			this.value = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A96D RID: 43373 RVA: 0x00352980 File Offset: 0x00350B80
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.slider = this.cachedComponent;
			}
			this.DoGetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A96E RID: 43374 RVA: 0x003529C8 File Offset: 0x00350BC8
		public override void OnUpdate()
		{
			this.DoGetValue();
		}

		// Token: 0x0600A96F RID: 43375 RVA: 0x003529D0 File Offset: 0x00350BD0
		private void DoGetValue()
		{
			if (this.slider != null)
			{
				this.value.Value = this.slider.normalizedValue;
			}
		}

		// Token: 0x04009016 RID: 36886
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the UI Slider component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009017 RID: 36887
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The normalized value (between 0 and 1) of the UI Slider.")]
		public FsmFloat value;

		// Token: 0x04009018 RID: 36888
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04009019 RID: 36889
		private Slider slider;
	}
}
