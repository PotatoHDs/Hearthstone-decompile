using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CB7 RID: 3255
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("GUILayout Label.")]
	public class GUILayoutLabel : GUILayoutAction
	{
		// Token: 0x0600A08E RID: 41102 RVA: 0x00331808 File Offset: 0x0032FA08
		public override void Reset()
		{
			base.Reset();
			this.text = "";
			this.image = null;
			this.tooltip = "";
			this.style = "";
		}

		// Token: 0x0600A08F RID: 41103 RVA: 0x00331848 File Offset: 0x0032FA48
		public override void OnGUI()
		{
			if (string.IsNullOrEmpty(this.style.Value))
			{
				GUILayout.Label(new GUIContent(this.text.Value, this.image.Value, this.tooltip.Value), base.LayoutOptions);
				return;
			}
			GUILayout.Label(new GUIContent(this.text.Value, this.image.Value, this.tooltip.Value), this.style.Value, base.LayoutOptions);
		}

		// Token: 0x04008614 RID: 34324
		public FsmTexture image;

		// Token: 0x04008615 RID: 34325
		public FsmString text;

		// Token: 0x04008616 RID: 34326
		public FsmString tooltip;

		// Token: 0x04008617 RID: 34327
		public FsmString style;
	}
}
