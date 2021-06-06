using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C9A RID: 3226
	[Tooltip("GUI base action - don't use!")]
	public abstract class GUIAction : FsmStateAction
	{
		// Token: 0x0600A037 RID: 41015 RVA: 0x003303E8 File Offset: 0x0032E5E8
		public override void Reset()
		{
			this.screenRect = null;
			this.left = 0f;
			this.top = 0f;
			this.width = 1f;
			this.height = 1f;
			this.normalized = true;
		}

		// Token: 0x0600A038 RID: 41016 RVA: 0x00330448 File Offset: 0x0032E648
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
		}

		// Token: 0x040085B0 RID: 34224
		[UIHint(UIHint.Variable)]
		public FsmRect screenRect;

		// Token: 0x040085B1 RID: 34225
		public FsmFloat left;

		// Token: 0x040085B2 RID: 34226
		public FsmFloat top;

		// Token: 0x040085B3 RID: 34227
		public FsmFloat width;

		// Token: 0x040085B4 RID: 34228
		public FsmFloat height;

		// Token: 0x040085B5 RID: 34229
		[RequiredField]
		public FsmBool normalized;

		// Token: 0x040085B6 RID: 34230
		internal Rect rect;
	}
}
