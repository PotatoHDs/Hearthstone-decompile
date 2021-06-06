using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CA2 RID: 3234
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("Begin a GUILayout block of GUI controls in a fixed screen area. NOTE: Block must end with a corresponding GUILayoutEndArea.")]
	public class GUILayoutBeginArea : FsmStateAction
	{
		// Token: 0x0600A04F RID: 41039 RVA: 0x00330A7C File Offset: 0x0032EC7C
		public override void Reset()
		{
			this.screenRect = null;
			this.left = 0f;
			this.top = 0f;
			this.width = 1f;
			this.height = 1f;
			this.normalized = true;
			this.style = "";
		}

		// Token: 0x0600A050 RID: 41040 RVA: 0x00330AEC File Offset: 0x0032ECEC
		public override void OnGUI()
		{
			this.rect = ((!this.screenRect.IsNone) ? this.screenRect.Value : default(Rect));
			if (!this.left.IsNone)
			{
				this.rect.x = this.left.Value;
			}
			if (!this.top.IsNone)
			{
				this.rect.y = this.top.Value;
			}
			if (!this.width.IsNone)
			{
				this.rect.width = this.width.Value;
			}
			if (!this.height.IsNone)
			{
				this.rect.height = this.height.Value;
			}
			if (this.normalized.Value)
			{
				this.rect.x = this.rect.x * (float)Screen.width;
				this.rect.width = this.rect.width * (float)Screen.width;
				this.rect.y = this.rect.y * (float)Screen.height;
				this.rect.height = this.rect.height * (float)Screen.height;
			}
			GUILayout.BeginArea(this.rect, GUIContent.none, this.style.Value);
		}

		// Token: 0x040085D0 RID: 34256
		[UIHint(UIHint.Variable)]
		public FsmRect screenRect;

		// Token: 0x040085D1 RID: 34257
		public FsmFloat left;

		// Token: 0x040085D2 RID: 34258
		public FsmFloat top;

		// Token: 0x040085D3 RID: 34259
		public FsmFloat width;

		// Token: 0x040085D4 RID: 34260
		public FsmFloat height;

		// Token: 0x040085D5 RID: 34261
		public FsmBool normalized;

		// Token: 0x040085D6 RID: 34262
		public FsmString style;

		// Token: 0x040085D7 RID: 34263
		private Rect rect;
	}
}
