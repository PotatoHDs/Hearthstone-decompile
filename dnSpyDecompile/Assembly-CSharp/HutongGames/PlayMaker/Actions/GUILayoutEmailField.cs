using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CAB RID: 3243
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("GUILayout Password Field. Optionally send an event if the text has been edited.")]
	public class GUILayoutEmailField : GUILayoutAction
	{
		// Token: 0x0600A06B RID: 41067 RVA: 0x00331327 File Offset: 0x0032F527
		public override void Reset()
		{
			this.text = null;
			this.maxLength = 25;
			this.style = "TextField";
			this.valid = true;
			this.changedEvent = null;
		}

		// Token: 0x0600A06C RID: 41068 RVA: 0x00331360 File Offset: 0x0032F560
		public override void OnGUI()
		{
			bool changed = GUI.changed;
			GUI.changed = false;
			this.text.Value = GUILayout.TextField(this.text.Value, this.style.Value, base.LayoutOptions);
			if (GUI.changed)
			{
				base.Fsm.Event(this.changedEvent);
				GUIUtility.ExitGUI();
				return;
			}
			GUI.changed = changed;
		}

		// Token: 0x040085FF RID: 34303
		[UIHint(UIHint.Variable)]
		[Tooltip("The email Text")]
		public FsmString text;

		// Token: 0x04008600 RID: 34304
		[Tooltip("The Maximum Length of the field")]
		public FsmInt maxLength;

		// Token: 0x04008601 RID: 34305
		[Tooltip("The Style of the Field")]
		public FsmString style;

		// Token: 0x04008602 RID: 34306
		[Tooltip("Event sent when field content changed")]
		public FsmEvent changedEvent;

		// Token: 0x04008603 RID: 34307
		[Tooltip("Email valid format flag")]
		public FsmBool valid;
	}
}
