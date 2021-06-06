using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CAA RID: 3242
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("GUILayout Password Field. Optionally send an event if the text has been edited.")]
	public class GUILayoutConfirmPasswordField : GUILayoutAction
	{
		// Token: 0x0600A068 RID: 41064 RVA: 0x0033124C File Offset: 0x0032F44C
		public override void Reset()
		{
			this.text = null;
			this.maxLength = 25;
			this.style = "TextField";
			this.mask = "*";
			this.changedEvent = null;
			this.confirm = false;
			this.password = null;
		}

		// Token: 0x0600A069 RID: 41065 RVA: 0x003312A8 File Offset: 0x0032F4A8
		public override void OnGUI()
		{
			bool changed = GUI.changed;
			GUI.changed = false;
			this.text.Value = GUILayout.PasswordField(this.text.Value, this.mask.Value[0], this.style.Value, base.LayoutOptions);
			if (GUI.changed)
			{
				base.Fsm.Event(this.changedEvent);
				GUIUtility.ExitGUI();
				return;
			}
			GUI.changed = changed;
		}

		// Token: 0x040085F8 RID: 34296
		[UIHint(UIHint.Variable)]
		[Tooltip("The password Text")]
		public FsmString text;

		// Token: 0x040085F9 RID: 34297
		[Tooltip("The Maximum Length of the field")]
		public FsmInt maxLength;

		// Token: 0x040085FA RID: 34298
		[Tooltip("The Style of the Field")]
		public FsmString style;

		// Token: 0x040085FB RID: 34299
		[Tooltip("Event sent when field content changed")]
		public FsmEvent changedEvent;

		// Token: 0x040085FC RID: 34300
		[Tooltip("Replacement character to hide the password")]
		public FsmString mask;

		// Token: 0x040085FD RID: 34301
		[Tooltip("GUILayout Password Field. Optionally send an event if the text has been edited.")]
		public FsmBool confirm;

		// Token: 0x040085FE RID: 34302
		[Tooltip("Confirmation content")]
		public FsmString password;
	}
}
