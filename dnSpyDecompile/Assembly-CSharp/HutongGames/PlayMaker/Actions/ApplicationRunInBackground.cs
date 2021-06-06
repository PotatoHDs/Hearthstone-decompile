using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BB7 RID: 2999
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("Sets if the Application should play in the background. Useful for servers or testing network games on one machine.")]
	public class ApplicationRunInBackground : FsmStateAction
	{
		// Token: 0x06009C45 RID: 40005 RVA: 0x00324D3B File Offset: 0x00322F3B
		public override void Reset()
		{
			this.runInBackground = true;
		}

		// Token: 0x06009C46 RID: 40006 RVA: 0x00324D49 File Offset: 0x00322F49
		public override void OnEnter()
		{
			Application.runInBackground = this.runInBackground.Value;
			base.Finish();
		}

		// Token: 0x040081C3 RID: 33219
		public FsmBool runInBackground;
	}
}
