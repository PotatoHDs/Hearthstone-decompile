using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CBB RID: 3259
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("GUILayout Text Field. Optionally send an event if the text has been edited.")]
	public class GUILayoutTextField : GUILayoutAction
	{
		// Token: 0x0600A09A RID: 41114 RVA: 0x00331AC9 File Offset: 0x0032FCC9
		public override void Reset()
		{
			base.Reset();
			this.text = null;
			this.maxLength = 25;
			this.style = "TextField";
			this.changedEvent = null;
		}

		// Token: 0x0600A09B RID: 41115 RVA: 0x00331AFC File Offset: 0x0032FCFC
		public override void OnGUI()
		{
			bool changed = GUI.changed;
			GUI.changed = false;
			this.text.Value = GUILayout.TextField(this.text.Value, this.maxLength.Value, this.style.Value, base.LayoutOptions);
			if (GUI.changed)
			{
				base.Fsm.Event(this.changedEvent);
				GUIUtility.ExitGUI();
				return;
			}
			GUI.changed = changed;
		}

		// Token: 0x04008624 RID: 34340
		[UIHint(UIHint.Variable)]
		public FsmString text;

		// Token: 0x04008625 RID: 34341
		public FsmInt maxLength;

		// Token: 0x04008626 RID: 34342
		public FsmString style;

		// Token: 0x04008627 RID: 34343
		public FsmEvent changedEvent;
	}
}
