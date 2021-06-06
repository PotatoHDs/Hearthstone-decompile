using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E96 RID: 3734
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the value of a UI Slider component.")]
	public class UiSliderSetValue : ComponentAction<Slider>
	{
		// Token: 0x0600A990 RID: 43408 RVA: 0x00352F8E File Offset: 0x0035118E
		public override void Reset()
		{
			this.gameObject = null;
			this.value = null;
			this.resetOnExit = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A991 RID: 43409 RVA: 0x00352FAC File Offset: 0x003511AC
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.slider = this.cachedComponent;
			}
			this.originalValue = this.slider.value;
			this.DoSetValue();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A992 RID: 43410 RVA: 0x00353005 File Offset: 0x00351205
		public override void OnUpdate()
		{
			this.DoSetValue();
		}

		// Token: 0x0600A993 RID: 43411 RVA: 0x0035300D File Offset: 0x0035120D
		private void DoSetValue()
		{
			if (this.slider != null)
			{
				this.slider.value = this.value.Value;
			}
		}

		// Token: 0x0600A994 RID: 43412 RVA: 0x00353033 File Offset: 0x00351233
		public override void OnExit()
		{
			if (this.slider == null)
			{
				return;
			}
			if (this.resetOnExit.Value)
			{
				this.slider.value = this.originalValue;
			}
		}

		// Token: 0x0400903C RID: 36924
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the UI Slider component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400903D RID: 36925
		[RequiredField]
		[Tooltip("The value of the UI Slider component.")]
		public FsmFloat value;

		// Token: 0x0400903E RID: 36926
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		// Token: 0x0400903F RID: 36927
		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		// Token: 0x04009040 RID: 36928
		private Slider slider;

		// Token: 0x04009041 RID: 36929
		private float originalValue;
	}
}
