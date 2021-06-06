using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E92 RID: 3730
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Catches onValueChanged event for a UI Slider component. Store the new value and/or send events. Event float data will contain the new slider value")]
	public class UiSliderOnValueChangedEvent : ComponentAction<Slider>
	{
		// Token: 0x0600A97A RID: 43386 RVA: 0x00352B35 File Offset: 0x00350D35
		public override void Reset()
		{
			this.gameObject = null;
			this.eventTarget = FsmEventTarget.Self;
			this.sendEvent = null;
			this.value = null;
		}

		// Token: 0x0600A97B RID: 43387 RVA: 0x00352B58 File Offset: 0x00350D58
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (base.UpdateCache(ownerDefaultTarget))
			{
				this.slider = this.cachedComponent;
				if (this.slider != null)
				{
					this.slider.onValueChanged.AddListener(new UnityAction<float>(this.DoOnValueChanged));
				}
			}
		}

		// Token: 0x0600A97C RID: 43388 RVA: 0x00352BB6 File Offset: 0x00350DB6
		public override void OnExit()
		{
			if (this.slider != null)
			{
				this.slider.onValueChanged.RemoveListener(new UnityAction<float>(this.DoOnValueChanged));
			}
		}

		// Token: 0x0600A97D RID: 43389 RVA: 0x00352BE2 File Offset: 0x00350DE2
		public void DoOnValueChanged(float _value)
		{
			this.value.Value = _value;
			Fsm.EventData.FloatData = _value;
			base.SendEvent(this.eventTarget, this.sendEvent);
		}

		// Token: 0x04009023 RID: 36899
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the UI Slider component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009024 RID: 36900
		[Tooltip("Where to send the event.")]
		public FsmEventTarget eventTarget;

		// Token: 0x04009025 RID: 36901
		[Tooltip("Send this event when Clicked.")]
		public FsmEvent sendEvent;

		// Token: 0x04009026 RID: 36902
		[Tooltip("Store the new value in float variable.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat value;

		// Token: 0x04009027 RID: 36903
		private Slider slider;
	}
}
