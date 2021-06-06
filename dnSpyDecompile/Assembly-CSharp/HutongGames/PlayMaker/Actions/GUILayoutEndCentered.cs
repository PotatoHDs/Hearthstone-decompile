using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CAD RID: 3245
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("End a centered GUILayout block started with GUILayoutBeginCentered.")]
	public class GUILayoutEndCentered : FsmStateAction
	{
		// Token: 0x0600A071 RID: 41073 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x0600A072 RID: 41074 RVA: 0x003313D5 File Offset: 0x0032F5D5
		public override void OnGUI()
		{
			GUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
		}
	}
}
