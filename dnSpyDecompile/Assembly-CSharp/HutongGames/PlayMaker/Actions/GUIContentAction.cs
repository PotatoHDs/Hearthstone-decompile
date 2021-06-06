using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C9D RID: 3229
	[Tooltip("GUI base action - don't use!")]
	public abstract class GUIContentAction : GUIAction
	{
		// Token: 0x0600A03F RID: 41023 RVA: 0x0033065F File Offset: 0x0032E85F
		public override void Reset()
		{
			base.Reset();
			this.image = null;
			this.text = "";
			this.tooltip = "";
			this.style = "";
		}

		// Token: 0x0600A040 RID: 41024 RVA: 0x0033069E File Offset: 0x0032E89E
		public override void OnGUI()
		{
			base.OnGUI();
			this.content = new GUIContent(this.text.Value, this.image.Value, this.tooltip.Value);
		}

		// Token: 0x040085B9 RID: 34233
		public FsmTexture image;

		// Token: 0x040085BA RID: 34234
		public FsmString text;

		// Token: 0x040085BB RID: 34235
		public FsmString tooltip;

		// Token: 0x040085BC RID: 34236
		public FsmString style;

		// Token: 0x040085BD RID: 34237
		internal GUIContent content;
	}
}
