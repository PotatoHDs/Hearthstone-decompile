using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BB6 RID: 2998
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("Quits the player application.")]
	public class ApplicationQuit : FsmStateAction
	{
		// Token: 0x06009C42 RID: 40002 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x06009C43 RID: 40003 RVA: 0x00324D2E File Offset: 0x00322F2E
		public override void OnEnter()
		{
			Application.Quit();
			base.Finish();
		}
	}
}
