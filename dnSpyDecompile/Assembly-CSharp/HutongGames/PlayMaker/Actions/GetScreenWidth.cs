using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C86 RID: 3206
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("Gets the Width of the Screen in pixels.")]
	public class GetScreenWidth : FsmStateAction
	{
		// Token: 0x06009FDC RID: 40924 RVA: 0x0032F727 File Offset: 0x0032D927
		public override void Reset()
		{
			this.storeScreenWidth = null;
		}

		// Token: 0x06009FDD RID: 40925 RVA: 0x0032F730 File Offset: 0x0032D930
		public override void OnEnter()
		{
			this.storeScreenWidth.Value = (float)Screen.width;
			base.Finish();
		}

		// Token: 0x04008567 RID: 34151
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeScreenWidth;
	}
}
