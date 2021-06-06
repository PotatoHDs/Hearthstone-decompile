using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HutongGames.PlayMaker
{
	// Token: 0x02000B8E RID: 2958
	[AddComponentMenu("PlayMaker/UI/UI Bool Value Changed Event")]
	public class PlayMakerUiBoolValueChangedEvent : PlayMakerUiEventBase
	{
		// Token: 0x06009B5F RID: 39775 RVA: 0x0031F5A0 File Offset: 0x0031D7A0
		protected override void Initialize()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			if (this.toggle == null)
			{
				this.toggle = base.GetComponent<Toggle>();
			}
			if (this.toggle != null)
			{
				this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged));
			}
		}

		// Token: 0x06009B60 RID: 39776 RVA: 0x0031F601 File Offset: 0x0031D801
		protected void OnDisable()
		{
			this.initialized = false;
			if (this.toggle != null)
			{
				this.toggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.OnValueChanged));
			}
		}

		// Token: 0x06009B61 RID: 39777 RVA: 0x0031F634 File Offset: 0x0031D834
		private void OnValueChanged(bool value)
		{
			Fsm.EventData.BoolData = value;
			base.SendEvent(FsmEvent.UiBoolValueChanged);
		}

		// Token: 0x040080D3 RID: 32979
		public Toggle toggle;
	}
}
