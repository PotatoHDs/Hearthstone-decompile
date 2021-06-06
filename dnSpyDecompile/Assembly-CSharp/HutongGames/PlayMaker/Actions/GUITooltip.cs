using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CC0 RID: 3264
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Gets the Tooltip of the control the mouse is currently over and store it in a String Variable.")]
	public class GUITooltip : FsmStateAction
	{
		// Token: 0x0600A0AD RID: 41133 RVA: 0x0033205C File Offset: 0x0033025C
		public override void Reset()
		{
			this.storeTooltip = null;
		}

		// Token: 0x0600A0AE RID: 41134 RVA: 0x00332065 File Offset: 0x00330265
		public override void OnGUI()
		{
			this.storeTooltip.Value = GUI.tooltip;
		}

		// Token: 0x0400863D RID: 34365
		[UIHint(UIHint.Variable)]
		public FsmString storeTooltip;
	}
}
