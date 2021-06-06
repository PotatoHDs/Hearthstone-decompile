using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CA6 RID: 3238
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("Begins a ScrollView. Use GUILayoutEndScrollView at the end of the block.")]
	public class GUILayoutBeginScrollView : GUILayoutAction
	{
		// Token: 0x0600A05C RID: 41052 RVA: 0x00330EDE File Offset: 0x0032F0DE
		public override void Reset()
		{
			base.Reset();
			this.scrollPosition = null;
			this.horizontalScrollbar = null;
			this.verticalScrollbar = null;
			this.useCustomStyle = null;
			this.horizontalStyle = null;
			this.verticalStyle = null;
			this.backgroundStyle = null;
		}

		// Token: 0x0600A05D RID: 41053 RVA: 0x00330F18 File Offset: 0x0032F118
		public override void OnGUI()
		{
			if (this.useCustomStyle.Value)
			{
				this.scrollPosition.Value = GUILayout.BeginScrollView(this.scrollPosition.Value, this.horizontalScrollbar.Value, this.verticalScrollbar.Value, this.horizontalStyle.Value, this.verticalStyle.Value, this.backgroundStyle.Value, base.LayoutOptions);
				return;
			}
			this.scrollPosition.Value = GUILayout.BeginScrollView(this.scrollPosition.Value, this.horizontalScrollbar.Value, this.verticalScrollbar.Value, base.LayoutOptions);
		}

		// Token: 0x040085E3 RID: 34275
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Assign a Vector2 variable to store the scroll position of this view.")]
		public FsmVector2 scrollPosition;

		// Token: 0x040085E4 RID: 34276
		[Tooltip("Always show the horizontal scrollbars.")]
		public FsmBool horizontalScrollbar;

		// Token: 0x040085E5 RID: 34277
		[Tooltip("Always show the vertical scrollbars.")]
		public FsmBool verticalScrollbar;

		// Token: 0x040085E6 RID: 34278
		[Tooltip("Define custom styles below. NOTE: You have to define all the styles if you check this option.")]
		public FsmBool useCustomStyle;

		// Token: 0x040085E7 RID: 34279
		[Tooltip("Named style in the active GUISkin for the horizontal scrollbars.")]
		public FsmString horizontalStyle;

		// Token: 0x040085E8 RID: 34280
		[Tooltip("Named style in the active GUISkin for the vertical scrollbars.")]
		public FsmString verticalStyle;

		// Token: 0x040085E9 RID: 34281
		[Tooltip("Named style in the active GUISkin for the background.")]
		public FsmString backgroundStyle;
	}
}
