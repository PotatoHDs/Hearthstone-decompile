using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E90 RID: 3728
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Gets the value of a UI Slider component.")]
	public class UiSliderGetValue : ComponentAction<Slider>
	{
		// Token: 0x0600A971 RID: 43377 RVA: 0x003529F6 File Offset: 0x00350BF6
		public override void Reset()
		{
			this.gameObject = null;
			this.value = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A972 RID: 43378 RVA: 0x00352A10 File Offset: 0x00350C10
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

		// Token: 0x0600A973 RID: 43379 RVA: 0x00352A58 File Offset: 0x00350C58
		public override void OnUpdate()
		{
			this.DoGetValue();
		}

		// Token: 0x0600A974 RID: 43380 RVA: 0x00352A60 File Offset: 0x00350C60
		private void DoGetValue()
		{
			if (this.slider != null)
			{
				this.value.Value = this.slider.value;
			}
		}

		// Token: 0x0400901A RID: 36890
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the UI Slider component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400901B RID: 36891
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The value of the UI Slider component.")]
		public FsmFloat value;

		// Token: 0x0400901C RID: 36892
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x0400901D RID: 36893
		private Slider slider;
	}
}
