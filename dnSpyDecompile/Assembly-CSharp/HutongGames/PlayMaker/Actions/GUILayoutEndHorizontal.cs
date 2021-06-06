using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CAE RID: 3246
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("Close a group started with BeginHorizontal.")]
	public class GUILayoutEndHorizontal : FsmStateAction
	{
		// Token: 0x0600A074 RID: 41076 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x0600A075 RID: 41077 RVA: 0x003313F0 File Offset: 0x0032F5F0
		public override void OnGUI()
		{
			GUILayout.EndHorizontal();
		}
	}
}
