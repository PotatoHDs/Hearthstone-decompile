using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CAC RID: 3244
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("Close a GUILayout group started with BeginArea.")]
	public class GUILayoutEndArea : FsmStateAction
	{
		// Token: 0x0600A06E RID: 41070 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x0600A06F RID: 41071 RVA: 0x003313CE File Offset: 0x0032F5CE
		public override void OnGUI()
		{
			GUILayout.EndArea();
		}
	}
}
