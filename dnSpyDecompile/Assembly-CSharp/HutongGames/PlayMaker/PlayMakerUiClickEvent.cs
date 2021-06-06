using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HutongGames.PlayMaker
{
	// Token: 0x02000B8F RID: 2959
	[AddComponentMenu("PlayMaker/UI/UI Click Event")]
	public class PlayMakerUiClickEvent : PlayMakerUiEventBase
	{
		// Token: 0x06009B63 RID: 39779 RVA: 0x0031F654 File Offset: 0x0031D854
		protected override void Initialize()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			if (this.button == null)
			{
				this.button = base.GetComponent<Button>();
			}
			if (this.button != null)
			{
				this.button.onClick.AddListener(new UnityAction(this.DoOnClick));
			}
		}

		// Token: 0x06009B64 RID: 39780 RVA: 0x0031F6B5 File Offset: 0x0031D8B5
		protected void OnDisable()
		{
			this.initialized = false;
			if (this.button != null)
			{
				this.button.onClick.RemoveListener(new UnityAction(this.DoOnClick));
			}
		}

		// Token: 0x06009B65 RID: 39781 RVA: 0x0031F6E8 File Offset: 0x0031D8E8
		private void DoOnClick()
		{
			base.SendEvent(FsmEvent.UiClick);
		}

		// Token: 0x040080D4 RID: 32980
		public Button button;
	}
}
