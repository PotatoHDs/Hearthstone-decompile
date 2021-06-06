using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E30 RID: 3632
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Force all canvases to update their content.\nCode that relies on up-to-date layout or content can call this method to ensure it before executing code that relies on it.")]
	public class UiCanvasForceUpdateCanvases : FsmStateAction
	{
		// Token: 0x0600A7AC RID: 42924 RVA: 0x0034D456 File Offset: 0x0034B656
		public override void OnEnter()
		{
			Canvas.ForceUpdateCanvases();
			base.Finish();
		}
	}
}
