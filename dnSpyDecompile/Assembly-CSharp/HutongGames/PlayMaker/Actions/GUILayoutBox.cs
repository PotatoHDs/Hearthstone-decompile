using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CA8 RID: 3240
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("GUILayout Box.")]
	public class GUILayoutBox : GUILayoutAction
	{
		// Token: 0x0600A062 RID: 41058 RVA: 0x0033105E File Offset: 0x0032F25E
		public override void Reset()
		{
			base.Reset();
			this.text = "";
			this.image = null;
			this.tooltip = "";
			this.style = "";
		}

		// Token: 0x0600A063 RID: 41059 RVA: 0x003310A0 File Offset: 0x0032F2A0
		public override void OnGUI()
		{
			if (string.IsNullOrEmpty(this.style.Value))
			{
				GUILayout.Box(new GUIContent(this.text.Value, this.image.Value, this.tooltip.Value), base.LayoutOptions);
				return;
			}
			GUILayout.Box(new GUIContent(this.text.Value, this.image.Value, this.tooltip.Value), this.style.Value, base.LayoutOptions);
		}

		// Token: 0x040085EE RID: 34286
		[Tooltip("Image to display in the Box.")]
		public FsmTexture image;

		// Token: 0x040085EF RID: 34287
		[Tooltip("Text to display in the Box.")]
		public FsmString text;

		// Token: 0x040085F0 RID: 34288
		[Tooltip("Optional Tooltip string.")]
		public FsmString tooltip;

		// Token: 0x040085F1 RID: 34289
		[Tooltip("Optional GUIStyle in the active GUISkin.")]
		public FsmString style;
	}
}
