using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D71 RID: 3441
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Resets the GUI matrix. Useful if you've rotated or scaled the GUI and now want to reset it.")]
	public class ResetGUIMatrix : FsmStateAction
	{
		// Token: 0x0600A43B RID: 42043 RVA: 0x003427F8 File Offset: 0x003409F8
		public override void OnGUI()
		{
			PlayMakerGUI.GUIMatrix = (GUI.matrix = Matrix4x4.identity);
		}
	}
}
