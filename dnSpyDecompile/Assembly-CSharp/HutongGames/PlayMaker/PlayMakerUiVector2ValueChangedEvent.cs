using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HutongGames.PlayMaker
{
	// Token: 0x02000B97 RID: 2967
	[AddComponentMenu("PlayMaker/UI/UI Vector2 Value Changed Event")]
	public class PlayMakerUiVector2ValueChangedEvent : PlayMakerUiEventBase
	{
		// Token: 0x06009B86 RID: 39814 RVA: 0x0031FAE8 File Offset: 0x0031DCE8
		protected override void Initialize()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			if (this.scrollRect == null)
			{
				this.scrollRect = base.GetComponent<ScrollRect>();
			}
			if (this.scrollRect != null)
			{
				this.scrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.OnValueChanged));
			}
		}

		// Token: 0x06009B87 RID: 39815 RVA: 0x0031FB49 File Offset: 0x0031DD49
		protected void OnDisable()
		{
			this.initialized = false;
			if (this.scrollRect != null)
			{
				this.scrollRect.onValueChanged.RemoveListener(new UnityAction<Vector2>(this.OnValueChanged));
			}
		}

		// Token: 0x06009B88 RID: 39816 RVA: 0x0031FB7C File Offset: 0x0031DD7C
		private void OnValueChanged(Vector2 value)
		{
			Fsm.EventData.Vector2Data = value;
			base.SendEvent(FsmEvent.UiVector2ValueChanged);
		}

		// Token: 0x040080DB RID: 32987
		public ScrollRect scrollRect;
	}
}
