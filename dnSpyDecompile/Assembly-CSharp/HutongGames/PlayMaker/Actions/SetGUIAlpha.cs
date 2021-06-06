using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DCD RID: 3533
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Sets the global Alpha for the GUI. Useful for fading GUI up/down. By default only effects GUI rendered by this FSM, check Apply Globally to effect all GUI controls.")]
	public class SetGUIAlpha : FsmStateAction
	{
		// Token: 0x0600A5F0 RID: 42480 RVA: 0x0034837C File Offset: 0x0034657C
		public override void Reset()
		{
			this.alpha = 1f;
		}

		// Token: 0x0600A5F1 RID: 42481 RVA: 0x00348390 File Offset: 0x00346590
		public override void OnGUI()
		{
			GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, this.alpha.Value);
			if (this.applyGlobally.Value)
			{
				PlayMakerGUI.GUIColor = GUI.color;
			}
		}

		// Token: 0x04008C9D RID: 35997
		[RequiredField]
		public FsmFloat alpha;

		// Token: 0x04008C9E RID: 35998
		public FsmBool applyGlobally;
	}
}
