using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C9B RID: 3227
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("GUI Box.")]
	public class GUIBox : GUIContentAction
	{
		// Token: 0x0600A03A RID: 41018 RVA: 0x00330578 File Offset: 0x0032E778
		public override void OnGUI()
		{
			base.OnGUI();
			if (string.IsNullOrEmpty(this.style.Value))
			{
				GUI.Box(this.rect, this.content);
				return;
			}
			GUI.Box(this.rect, this.content, this.style.Value);
		}
	}
}
