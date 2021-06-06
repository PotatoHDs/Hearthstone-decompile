using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HutongGames.PlayMaker
{
	// Token: 0x02000B92 RID: 2962
	[AddComponentMenu("PlayMaker/UI/UI End Edit Event")]
	public class PlayMakerUiEndEditEvent : PlayMakerUiEventBase
	{
		// Token: 0x06009B6D RID: 39789 RVA: 0x0031F744 File Offset: 0x0031D944
		protected override void Initialize()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			if (this.inputField == null)
			{
				this.inputField = base.GetComponent<InputField>();
			}
			if (this.inputField != null)
			{
				this.inputField.onEndEdit.AddListener(new UnityAction<string>(this.DoOnEndEdit));
			}
		}

		// Token: 0x06009B6E RID: 39790 RVA: 0x0031F7A5 File Offset: 0x0031D9A5
		protected void OnDisable()
		{
			this.initialized = false;
			if (this.inputField != null)
			{
				this.inputField.onEndEdit.RemoveListener(new UnityAction<string>(this.DoOnEndEdit));
			}
		}

		// Token: 0x06009B6F RID: 39791 RVA: 0x0031F7D8 File Offset: 0x0031D9D8
		private void DoOnEndEdit(string value)
		{
			Fsm.EventData.StringData = value;
			base.SendEvent(FsmEvent.UiEndEdit);
		}

		// Token: 0x040080D5 RID: 32981
		public InputField inputField;
	}
}
