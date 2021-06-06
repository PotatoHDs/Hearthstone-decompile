using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DD0 RID: 3536
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Sets the Tinting Color for all text rendered by the GUI. By default only effects GUI rendered by this FSM, check Apply Globally to effect all GUI controls.")]
	public class SetGUIContentColor : FsmStateAction
	{
		// Token: 0x0600A5F9 RID: 42489 RVA: 0x0034845D File Offset: 0x0034665D
		public override void Reset()
		{
			this.contentColor = Color.white;
		}

		// Token: 0x0600A5FA RID: 42490 RVA: 0x0034846F File Offset: 0x0034666F
		public override void OnGUI()
		{
			GUI.contentColor = this.contentColor.Value;
			if (this.applyGlobally.Value)
			{
				PlayMakerGUI.GUIContentColor = GUI.contentColor;
			}
		}

		// Token: 0x04008CA3 RID: 36003
		[RequiredField]
		public FsmColor contentColor;

		// Token: 0x04008CA4 RID: 36004
		public FsmBool applyGlobally;
	}
}
