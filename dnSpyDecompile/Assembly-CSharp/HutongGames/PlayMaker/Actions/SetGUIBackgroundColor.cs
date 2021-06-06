using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DCE RID: 3534
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Sets the Tinting Color for all background elements rendered by the GUI. By default only effects GUI rendered by this FSM, check Apply Globally to effect all GUI controls.")]
	public class SetGUIBackgroundColor : FsmStateAction
	{
		// Token: 0x0600A5F3 RID: 42483 RVA: 0x003483E7 File Offset: 0x003465E7
		public override void Reset()
		{
			this.backgroundColor = Color.white;
		}

		// Token: 0x0600A5F4 RID: 42484 RVA: 0x003483F9 File Offset: 0x003465F9
		public override void OnGUI()
		{
			GUI.backgroundColor = this.backgroundColor.Value;
			if (this.applyGlobally.Value)
			{
				PlayMakerGUI.GUIBackgroundColor = GUI.backgroundColor;
			}
		}

		// Token: 0x04008C9F RID: 35999
		[RequiredField]
		public FsmColor backgroundColor;

		// Token: 0x04008CA0 RID: 36000
		public FsmBool applyGlobally;
	}
}
