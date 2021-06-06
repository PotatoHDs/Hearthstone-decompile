using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CB8 RID: 3256
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("GUILayout Password Field. Optionally send an event if the text has been edited.")]
	public class GUILayoutPasswordField : GUILayoutAction
	{
		// Token: 0x0600A091 RID: 41105 RVA: 0x003318DA File Offset: 0x0032FADA
		public override void Reset()
		{
			this.text = null;
			this.maxLength = 25;
			this.style = "TextField";
			this.mask = "*";
			this.changedEvent = null;
		}

		// Token: 0x0600A092 RID: 41106 RVA: 0x00331918 File Offset: 0x0032FB18
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

		// Token: 0x04008618 RID: 34328
		[UIHint(UIHint.Variable)]
		[Tooltip("The password Text")]
		public FsmString text;

		// Token: 0x04008619 RID: 34329
		[Tooltip("The Maximum Length of the field")]
		public FsmInt maxLength;

		// Token: 0x0400861A RID: 34330
		[Tooltip("The Style of the Field")]
		public FsmString style;

		// Token: 0x0400861B RID: 34331
		[Tooltip("Event sent when field content changed")]
		public FsmEvent changedEvent;

		// Token: 0x0400861C RID: 34332
		[Tooltip("Replacement character to hide the password")]
		public FsmString mask;
	}
}
