using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CB4 RID: 3252
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("A Horizontal Slider linked to a Float Variable.")]
	public class GUILayoutHorizontalSlider : GUILayoutAction
	{
		// Token: 0x0600A085 RID: 41093 RVA: 0x003315B0 File Offset: 0x0032F7B0
		public override void Reset()
		{
			base.Reset();
			this.floatVariable = null;
			this.leftValue = 0f;
			this.rightValue = 100f;
			this.changedEvent = null;
		}

		// Token: 0x0600A086 RID: 41094 RVA: 0x003315E8 File Offset: 0x0032F7E8
		public override void OnGUI()
		{
			bool changed = GUI.changed;
			GUI.changed = false;
			if (this.floatVariable != null)
			{
				this.floatVariable.Value = GUILayout.HorizontalSlider(this.floatVariable.Value, this.leftValue.Value, this.rightValue.Value, base.LayoutOptions);
			}
			if (GUI.changed)
			{
				base.Fsm.Event(this.changedEvent);
				GUIUtility.ExitGUI();
				return;
			}
			GUI.changed = changed;
		}

		// Token: 0x0400860A RID: 34314
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat floatVariable;

		// Token: 0x0400860B RID: 34315
		[RequiredField]
		public FsmFloat leftValue;

		// Token: 0x0400860C RID: 34316
		[RequiredField]
		public FsmFloat rightValue;

		// Token: 0x0400860D RID: 34317
		public FsmEvent changedEvent;
	}
}
