using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D72 RID: 3442
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Resets all Input. After ResetInputAxes all axes return to 0 and all buttons return to 0 for one frame")]
	public class ResetInputAxes : FsmStateAction
	{
		// Token: 0x0600A43D RID: 42045 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x0600A43E RID: 42046 RVA: 0x0034280A File Offset: 0x00340A0A
		public override void OnEnter()
		{
			Input.ResetInputAxes();
			base.Finish();
		}
	}
}
