using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C85 RID: 3205
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("Gets the Height of the Screen in pixels.")]
	public class GetScreenHeight : FsmStateAction
	{
		// Token: 0x06009FD9 RID: 40921 RVA: 0x0032F705 File Offset: 0x0032D905
		public override void Reset()
		{
			this.storeScreenHeight = null;
		}

		// Token: 0x06009FDA RID: 40922 RVA: 0x0032F70E File Offset: 0x0032D90E
		public override void OnEnter()
		{
			this.storeScreenHeight.Value = (float)Screen.height;
			base.Finish();
		}

		// Token: 0x04008566 RID: 34150
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeScreenHeight;
	}
}
