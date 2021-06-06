using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CB2 RID: 3250
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("GUILayout Text Field to edit a Float Variable. Optionally send an event if the text has been edited.")]
	public class GUILayoutFloatField : GUILayoutAction
	{
		// Token: 0x0600A07F RID: 41087 RVA: 0x0033140C File Offset: 0x0032F60C
		public override void Reset()
		{
			base.Reset();
			this.floatVariable = null;
			this.style = "";
			this.changedEvent = null;
		}

		// Token: 0x0600A080 RID: 41088 RVA: 0x00331434 File Offset: 0x0032F634
		public override void OnGUI()
		{
			bool changed = GUI.changed;
			GUI.changed = false;
			if (!string.IsNullOrEmpty(this.style.Value))
			{
				this.floatVariable.Value = float.Parse(GUILayout.TextField(this.floatVariable.Value.ToString(), this.style.Value, base.LayoutOptions));
			}
			else
			{
				this.floatVariable.Value = float.Parse(GUILayout.TextField(this.floatVariable.Value.ToString(), base.LayoutOptions));
			}
			if (GUI.changed)
			{
				base.Fsm.Event(this.changedEvent);
				GUIUtility.ExitGUI();
				return;
			}
			GUI.changed = changed;
		}

		// Token: 0x04008604 RID: 34308
		[UIHint(UIHint.Variable)]
		[Tooltip("Float Variable to show in the edit field.")]
		public FsmFloat floatVariable;

		// Token: 0x04008605 RID: 34309
		[Tooltip("Optional GUIStyle in the active GUISKin.")]
		public FsmString style;

		// Token: 0x04008606 RID: 34310
		[Tooltip("Optional event to send when the value changes.")]
		public FsmEvent changedEvent;
	}
}
