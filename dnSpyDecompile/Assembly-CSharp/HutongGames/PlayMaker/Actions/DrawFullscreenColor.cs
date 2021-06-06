using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C19 RID: 3097
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Fills the screen with a Color. NOTE: Uses OnGUI so you need a PlayMakerGUI component in the scene.")]
	public class DrawFullscreenColor : FsmStateAction
	{
		// Token: 0x06009DF5 RID: 40437 RVA: 0x0032A2E5 File Offset: 0x003284E5
		public override void Reset()
		{
			this.color = Color.white;
		}

		// Token: 0x06009DF6 RID: 40438 RVA: 0x0032A2F8 File Offset: 0x003284F8
		public override void OnGUI()
		{
			Color color = GUI.color;
			GUI.color = this.color.Value;
			GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), ActionHelpers.WhiteTexture);
			GUI.color = color;
		}

		// Token: 0x0400834C RID: 33612
		[RequiredField]
		[Tooltip("Color. NOTE: Uses OnGUI so you need a PlayMakerGUI component in the scene.")]
		public FsmColor color;
	}
}
