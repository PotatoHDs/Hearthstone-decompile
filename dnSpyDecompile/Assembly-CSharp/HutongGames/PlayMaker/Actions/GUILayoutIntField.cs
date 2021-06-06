using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CB5 RID: 3253
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("GUILayout Text Field to edit an Int Variable. Optionally send an event if the text has been edited.")]
	public class GUILayoutIntField : GUILayoutAction
	{
		// Token: 0x0600A088 RID: 41096 RVA: 0x00331664 File Offset: 0x0032F864
		public override void Reset()
		{
			base.Reset();
			this.intVariable = null;
			this.style = "";
			this.changedEvent = null;
		}

		// Token: 0x0600A089 RID: 41097 RVA: 0x0033168C File Offset: 0x0032F88C
		public override void OnGUI()
		{
			bool changed = GUI.changed;
			GUI.changed = false;
			if (!string.IsNullOrEmpty(this.style.Value))
			{
				this.intVariable.Value = int.Parse(GUILayout.TextField(this.intVariable.Value.ToString(), this.style.Value, base.LayoutOptions));
			}
			else
			{
				this.intVariable.Value = int.Parse(GUILayout.TextField(this.intVariable.Value.ToString(), base.LayoutOptions));
			}
			if (GUI.changed)
			{
				base.Fsm.Event(this.changedEvent);
				GUIUtility.ExitGUI();
				return;
			}
			GUI.changed = changed;
		}

		// Token: 0x0400860E RID: 34318
		[UIHint(UIHint.Variable)]
		[Tooltip("Int Variable to show in the edit field.")]
		public FsmInt intVariable;

		// Token: 0x0400860F RID: 34319
		[Tooltip("Optional GUIStyle in the active GUISKin.")]
		public FsmString style;

		// Token: 0x04008610 RID: 34320
		[Tooltip("Optional event to send when the value changes.")]
		public FsmEvent changedEvent;
	}
}
