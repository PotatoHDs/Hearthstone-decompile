using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CB0 RID: 3248
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("Close a group started with BeginVertical.")]
	public class GUILayoutEndVertical : FsmStateAction
	{
		// Token: 0x0600A079 RID: 41081 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x0600A07A RID: 41082 RVA: 0x003313FE File Offset: 0x0032F5FE
		public override void OnGUI()
		{
			GUILayout.EndVertical();
		}
	}
}
