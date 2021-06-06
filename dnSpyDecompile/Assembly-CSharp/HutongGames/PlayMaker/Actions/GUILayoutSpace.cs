using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CBA RID: 3258
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("Inserts a space in the current layout group.")]
	public class GUILayoutSpace : FsmStateAction
	{
		// Token: 0x0600A097 RID: 41111 RVA: 0x00331AA5 File Offset: 0x0032FCA5
		public override void Reset()
		{
			this.space = 10f;
		}

		// Token: 0x0600A098 RID: 41112 RVA: 0x00331AB7 File Offset: 0x0032FCB7
		public override void OnGUI()
		{
			GUILayout.Space(this.space.Value);
		}

		// Token: 0x04008623 RID: 34339
		public FsmFloat space;
	}
}
