using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CBD RID: 3261
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("Makes an on/off Toggle Button and stores the button state in a Bool Variable.")]
	public class GUILayoutToggle : GUILayoutAction
	{
		// Token: 0x0600A0A0 RID: 41120 RVA: 0x00331C08 File Offset: 0x0032FE08
		public override void Reset()
		{
			base.Reset();
			this.storeButtonState = null;
			this.text = "";
			this.image = null;
			this.tooltip = "";
			this.style = "Toggle";
			this.changedEvent = null;
		}

		// Token: 0x0600A0A1 RID: 41121 RVA: 0x00331C60 File Offset: 0x0032FE60
		public override void OnGUI()
		{
			bool changed = GUI.changed;
			GUI.changed = false;
			this.storeButtonState.Value = GUILayout.Toggle(this.storeButtonState.Value, new GUIContent(this.text.Value, this.image.Value, this.tooltip.Value), this.style.Value, base.LayoutOptions);
			if (GUI.changed)
			{
				base.Fsm.Event(this.changedEvent);
				GUIUtility.ExitGUI();
				return;
			}
			GUI.changed = changed;
		}

		// Token: 0x0400862A RID: 34346
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmBool storeButtonState;

		// Token: 0x0400862B RID: 34347
		public FsmTexture image;

		// Token: 0x0400862C RID: 34348
		public FsmString text;

		// Token: 0x0400862D RID: 34349
		public FsmString tooltip;

		// Token: 0x0400862E RID: 34350
		public FsmString style;

		// Token: 0x0400862F RID: 34351
		public FsmEvent changedEvent;
	}
}
