using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C9C RID: 3228
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("GUI button. Sends an Event when pressed. Optionally store the button state in a Bool Variable.")]
	public class GUIButton : GUIContentAction
	{
		// Token: 0x0600A03C RID: 41020 RVA: 0x003305D8 File Offset: 0x0032E7D8
		public override void Reset()
		{
			base.Reset();
			this.sendEvent = null;
			this.storeButtonState = null;
			this.style = "Button";
		}

		// Token: 0x0600A03D RID: 41021 RVA: 0x00330600 File Offset: 0x0032E800
		public override void OnGUI()
		{
			base.OnGUI();
			bool value = false;
			if (GUI.Button(this.rect, this.content, this.style.Value))
			{
				base.Fsm.Event(this.sendEvent);
				value = true;
			}
			if (this.storeButtonState != null)
			{
				this.storeButtonState.Value = value;
			}
		}

		// Token: 0x040085B7 RID: 34231
		public FsmEvent sendEvent;

		// Token: 0x040085B8 RID: 34232
		[UIHint(UIHint.Variable)]
		public FsmBool storeButtonState;
	}
}
