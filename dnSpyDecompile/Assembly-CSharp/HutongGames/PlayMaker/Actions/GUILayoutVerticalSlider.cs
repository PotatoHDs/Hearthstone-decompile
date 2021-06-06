using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CBF RID: 3263
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("A Vertical Slider linked to a Float Variable.")]
	public class GUILayoutVerticalSlider : GUILayoutAction
	{
		// Token: 0x0600A0AA RID: 41130 RVA: 0x00331FAA File Offset: 0x003301AA
		public override void Reset()
		{
			base.Reset();
			this.floatVariable = null;
			this.topValue = 100f;
			this.bottomValue = 0f;
			this.changedEvent = null;
		}

		// Token: 0x0600A0AB RID: 41131 RVA: 0x00331FE0 File Offset: 0x003301E0
		public override void OnGUI()
		{
			bool changed = GUI.changed;
			GUI.changed = false;
			if (this.floatVariable != null)
			{
				this.floatVariable.Value = GUILayout.VerticalSlider(this.floatVariable.Value, this.topValue.Value, this.bottomValue.Value, base.LayoutOptions);
			}
			if (GUI.changed)
			{
				base.Fsm.Event(this.changedEvent);
				GUIUtility.ExitGUI();
				return;
			}
			GUI.changed = changed;
		}

		// Token: 0x04008639 RID: 34361
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat floatVariable;

		// Token: 0x0400863A RID: 34362
		[RequiredField]
		public FsmFloat topValue;

		// Token: 0x0400863B RID: 34363
		[RequiredField]
		public FsmFloat bottomValue;

		// Token: 0x0400863C RID: 34364
		public FsmEvent changedEvent;
	}
}
