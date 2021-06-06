using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CA0 RID: 3232
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("GUI Label.")]
	public class GUILabel : GUIContentAction
	{
		// Token: 0x0600A04A RID: 41034 RVA: 0x003309C0 File Offset: 0x0032EBC0
		public override void OnGUI()
		{
			base.OnGUI();
			if (string.IsNullOrEmpty(this.style.Value))
			{
				GUI.Label(this.rect, this.content);
				return;
			}
			GUI.Label(this.rect, this.content, this.style.Value);
		}
	}
}
