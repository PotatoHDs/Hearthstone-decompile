using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CA5 RID: 3237
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("GUILayout BeginHorizontal.")]
	public class GUILayoutBeginHorizontal : GUILayoutAction
	{
		// Token: 0x0600A059 RID: 41049 RVA: 0x00330E49 File Offset: 0x0032F049
		public override void Reset()
		{
			base.Reset();
			this.text = "";
			this.image = null;
			this.tooltip = "";
			this.style = "";
		}

		// Token: 0x0600A05A RID: 41050 RVA: 0x00330E88 File Offset: 0x0032F088
		public override void OnGUI()
		{
			GUILayout.BeginHorizontal(new GUIContent(this.text.Value, this.image.Value, this.tooltip.Value), this.style.Value, base.LayoutOptions);
		}

		// Token: 0x040085DF RID: 34271
		public FsmTexture image;

		// Token: 0x040085E0 RID: 34272
		public FsmString text;

		// Token: 0x040085E1 RID: 34273
		public FsmString tooltip;

		// Token: 0x040085E2 RID: 34274
		public FsmString style;
	}
}
