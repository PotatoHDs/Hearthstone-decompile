using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CB9 RID: 3257
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("GUILayout Repeat Button. Sends an Event while pressed. Optionally store the button state in a Bool Variable.")]
	public class GUILayoutRepeatButton : GUILayoutAction
	{
		// Token: 0x0600A094 RID: 41108 RVA: 0x00331998 File Offset: 0x0032FB98
		public override void Reset()
		{
			base.Reset();
			this.sendEvent = null;
			this.storeButtonState = null;
			this.text = "";
			this.image = null;
			this.tooltip = "";
			this.style = "";
		}

		// Token: 0x0600A095 RID: 41109 RVA: 0x003319F0 File Offset: 0x0032FBF0
		public override void OnGUI()
		{
			bool flag;
			if (string.IsNullOrEmpty(this.style.Value))
			{
				flag = GUILayout.RepeatButton(new GUIContent(this.text.Value, this.image.Value, this.tooltip.Value), base.LayoutOptions);
			}
			else
			{
				flag = GUILayout.RepeatButton(new GUIContent(this.text.Value, this.image.Value, this.tooltip.Value), this.style.Value, base.LayoutOptions);
			}
			if (flag)
			{
				base.Fsm.Event(this.sendEvent);
			}
			this.storeButtonState.Value = flag;
		}

		// Token: 0x0400861D RID: 34333
		public FsmEvent sendEvent;

		// Token: 0x0400861E RID: 34334
		[UIHint(UIHint.Variable)]
		public FsmBool storeButtonState;

		// Token: 0x0400861F RID: 34335
		public FsmTexture image;

		// Token: 0x04008620 RID: 34336
		public FsmString text;

		// Token: 0x04008621 RID: 34337
		public FsmString tooltip;

		// Token: 0x04008622 RID: 34338
		public FsmString style;
	}
}
