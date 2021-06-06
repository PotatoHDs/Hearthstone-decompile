using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CA7 RID: 3239
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("Begins a vertical control group. The group must be closed with GUILayoutEndVertical action.")]
	public class GUILayoutBeginVertical : GUILayoutAction
	{
		// Token: 0x0600A05F RID: 41055 RVA: 0x00330FD1 File Offset: 0x0032F1D1
		public override void Reset()
		{
			base.Reset();
			this.text = "";
			this.image = null;
			this.tooltip = "";
			this.style = "";
		}

		// Token: 0x0600A060 RID: 41056 RVA: 0x00331010 File Offset: 0x0032F210
		public override void OnGUI()
		{
			GUILayout.BeginVertical(new GUIContent(this.text.Value, this.image.Value, this.tooltip.Value), this.style.Value, base.LayoutOptions);
		}

		// Token: 0x040085EA RID: 34282
		public FsmTexture image;

		// Token: 0x040085EB RID: 34283
		public FsmString text;

		// Token: 0x040085EC RID: 34284
		public FsmString tooltip;

		// Token: 0x040085ED RID: 34285
		public FsmString style;
	}
}
