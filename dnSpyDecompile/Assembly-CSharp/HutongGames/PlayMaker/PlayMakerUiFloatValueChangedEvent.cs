using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HutongGames.PlayMaker
{
	// Token: 0x02000B94 RID: 2964
	[AddComponentMenu("PlayMaker/UI/UI Float Value Changed Event")]
	public class PlayMakerUiFloatValueChangedEvent : PlayMakerUiEventBase
	{
		// Token: 0x06009B78 RID: 39800 RVA: 0x0031F8B4 File Offset: 0x0031DAB4
		protected override void Initialize()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			if (this.slider == null)
			{
				this.slider = base.GetComponent<Slider>();
			}
			if (this.slider != null)
			{
				this.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChanged));
			}
			if (this.scrollbar == null)
			{
				this.scrollbar = base.GetComponent<UnityEngine.UI.Scrollbar>();
			}
			if (this.scrollbar != null)
			{
				this.scrollbar.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChanged));
			}
		}

		// Token: 0x06009B79 RID: 39801 RVA: 0x0031F95C File Offset: 0x0031DB5C
		protected void OnDisable()
		{
			this.initialized = false;
			if (this.slider != null)
			{
				this.slider.onValueChanged.RemoveListener(new UnityAction<float>(this.OnValueChanged));
			}
			if (this.scrollbar != null)
			{
				this.scrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.OnValueChanged));
			}
		}

		// Token: 0x06009B7A RID: 39802 RVA: 0x0031F9C4 File Offset: 0x0031DBC4
		private void OnValueChanged(float value)
		{
			Fsm.EventData.FloatData = value;
			base.SendEvent(FsmEvent.UiFloatValueChanged);
		}

		// Token: 0x040080D8 RID: 32984
		public Slider slider;

		// Token: 0x040080D9 RID: 32985
		public UnityEngine.UI.Scrollbar scrollbar;
	}
}
