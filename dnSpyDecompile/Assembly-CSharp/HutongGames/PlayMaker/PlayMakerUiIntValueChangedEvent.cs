using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HutongGames.PlayMaker
{
	// Token: 0x02000B95 RID: 2965
	[AddComponentMenu("PlayMaker/UI/UI Int Value Changed Event")]
	public class PlayMakerUiIntValueChangedEvent : PlayMakerUiEventBase
	{
		// Token: 0x06009B7C RID: 39804 RVA: 0x0031F9DC File Offset: 0x0031DBDC
		protected override void Initialize()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			if (this.dropdown == null)
			{
				this.dropdown = base.GetComponent<Dropdown>();
			}
			if (this.dropdown != null)
			{
				this.dropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnValueChanged));
			}
		}

		// Token: 0x06009B7D RID: 39805 RVA: 0x0031FA3D File Offset: 0x0031DC3D
		protected void OnDisable()
		{
			this.initialized = false;
			if (this.dropdown != null)
			{
				this.dropdown.onValueChanged.RemoveListener(new UnityAction<int>(this.OnValueChanged));
			}
		}

		// Token: 0x06009B7E RID: 39806 RVA: 0x0031FA70 File Offset: 0x0031DC70
		private void OnValueChanged(int value)
		{
			Fsm.EventData.IntData = value;
			base.SendEvent(FsmEvent.UiIntValueChanged);
		}

		// Token: 0x040080DA RID: 32986
		public Dropdown dropdown;
	}
}
