using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CB1 RID: 3249
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("Inserts a flexible space element.")]
	public class GUILayoutFlexibleSpace : FsmStateAction
	{
		// Token: 0x0600A07C RID: 41084 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x0600A07D RID: 41085 RVA: 0x00331405 File Offset: 0x0032F605
		public override void OnGUI()
		{
			GUILayout.FlexibleSpace();
		}
	}
}
