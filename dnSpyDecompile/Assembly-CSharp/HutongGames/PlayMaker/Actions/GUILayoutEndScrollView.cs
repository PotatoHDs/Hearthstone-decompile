using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CAF RID: 3247
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("Close a group started with GUILayout Begin ScrollView.")]
	public class GUILayoutEndScrollView : FsmStateAction
	{
		// Token: 0x0600A077 RID: 41079 RVA: 0x003313F7 File Offset: 0x0032F5F7
		public override void OnGUI()
		{
			GUILayout.EndScrollView();
		}
	}
}
