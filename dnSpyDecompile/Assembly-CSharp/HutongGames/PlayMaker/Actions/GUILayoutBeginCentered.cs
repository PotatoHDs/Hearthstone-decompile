using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CA4 RID: 3236
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("Begin a centered GUILayout block. The block is centered inside a parent GUILayout Area. So to place the block in the center of the screen, first use a GULayout Area the size of the whole screen (the default setting). NOTE: Block must end with a corresponding GUILayoutEndCentered.")]
	public class GUILayoutBeginCentered : FsmStateAction
	{
		// Token: 0x0600A056 RID: 41046 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x0600A057 RID: 41047 RVA: 0x00330E1F File Offset: 0x0032F01F
		public override void OnGUI()
		{
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		}
	}
}
