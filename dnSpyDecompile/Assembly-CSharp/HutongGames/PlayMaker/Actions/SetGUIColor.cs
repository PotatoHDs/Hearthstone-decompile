using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DCF RID: 3535
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Sets the Tinting Color for the GUI. By default only effects GUI rendered by this FSM, check Apply Globally to effect all GUI controls.")]
	public class SetGUIColor : FsmStateAction
	{
		// Token: 0x0600A5F6 RID: 42486 RVA: 0x00348422 File Offset: 0x00346622
		public override void Reset()
		{
			this.color = Color.white;
		}

		// Token: 0x0600A5F7 RID: 42487 RVA: 0x00348434 File Offset: 0x00346634
		public override void OnGUI()
		{
			GUI.color = this.color.Value;
			if (this.applyGlobally.Value)
			{
				PlayMakerGUI.GUIColor = GUI.color;
			}
		}

		// Token: 0x04008CA1 RID: 36001
		[RequiredField]
		public FsmColor color;

		// Token: 0x04008CA2 RID: 36002
		public FsmBool applyGlobally;
	}
}
