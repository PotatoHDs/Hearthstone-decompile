using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CBC RID: 3260
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("GUILayout Label for simple text.")]
	public class GUILayoutTextLabel : GUILayoutAction
	{
		// Token: 0x0600A09D RID: 41117 RVA: 0x00331B75 File Offset: 0x0032FD75
		public override void Reset()
		{
			base.Reset();
			this.text = "";
			this.style = "";
		}

		// Token: 0x0600A09E RID: 41118 RVA: 0x00331BA0 File Offset: 0x0032FDA0
		public override void OnGUI()
		{
			if (string.IsNullOrEmpty(this.style.Value))
			{
				GUILayout.Label(new GUIContent(this.text.Value), base.LayoutOptions);
				return;
			}
			GUILayout.Label(new GUIContent(this.text.Value), this.style.Value, base.LayoutOptions);
		}

		// Token: 0x04008628 RID: 34344
		[Tooltip("Text to display.")]
		public FsmString text;

		// Token: 0x04008629 RID: 34345
		[Tooltip("Optional GUIStyle in the active GUISkin.")]
		public FsmString style;
	}
}
