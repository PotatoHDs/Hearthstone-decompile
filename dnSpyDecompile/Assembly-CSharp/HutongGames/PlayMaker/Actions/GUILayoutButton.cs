using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CA9 RID: 3241
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("GUILayout Button. Sends an Event when pressed. Optionally stores the button state in a Bool Variable.")]
	public class GUILayoutButton : GUILayoutAction
	{
		// Token: 0x0600A065 RID: 41061 RVA: 0x00331134 File Offset: 0x0032F334
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

		// Token: 0x0600A066 RID: 41062 RVA: 0x0033118C File Offset: 0x0032F38C
		public override void OnGUI()
		{
			bool flag;
			if (string.IsNullOrEmpty(this.style.Value))
			{
				flag = GUILayout.Button(new GUIContent(this.text.Value, this.image.Value, this.tooltip.Value), base.LayoutOptions);
			}
			else
			{
				flag = GUILayout.Button(new GUIContent(this.text.Value, this.image.Value, this.tooltip.Value), this.style.Value, base.LayoutOptions);
			}
			if (flag)
			{
				base.Fsm.Event(this.sendEvent);
			}
			if (this.storeButtonState != null)
			{
				this.storeButtonState.Value = flag;
			}
		}

		// Token: 0x040085F2 RID: 34290
		public FsmEvent sendEvent;

		// Token: 0x040085F3 RID: 34291
		[UIHint(UIHint.Variable)]
		public FsmBool storeButtonState;

		// Token: 0x040085F4 RID: 34292
		public FsmTexture image;

		// Token: 0x040085F5 RID: 34293
		public FsmString text;

		// Token: 0x040085F6 RID: 34294
		public FsmString tooltip;

		// Token: 0x040085F7 RID: 34295
		public FsmString style;
	}
}
