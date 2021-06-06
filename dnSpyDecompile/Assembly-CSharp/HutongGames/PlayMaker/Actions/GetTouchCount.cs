using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C90 RID: 3216
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Gets the number of Touches.")]
	public class GetTouchCount : FsmStateAction
	{
		// Token: 0x0600A009 RID: 40969 RVA: 0x0032FCA1 File Offset: 0x0032DEA1
		public override void Reset()
		{
			this.storeCount = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A00A RID: 40970 RVA: 0x0032FCB1 File Offset: 0x0032DEB1
		public override void OnEnter()
		{
			this.DoGetTouchCount();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A00B RID: 40971 RVA: 0x0032FCC7 File Offset: 0x0032DEC7
		public override void OnUpdate()
		{
			this.DoGetTouchCount();
		}

		// Token: 0x0600A00C RID: 40972 RVA: 0x0032FCCF File Offset: 0x0032DECF
		private void DoGetTouchCount()
		{
			this.storeCount.Value = Input.touchCount;
		}

		// Token: 0x04008586 RID: 34182
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the current number of touches in an Int Variable.")]
		public FsmInt storeCount;

		// Token: 0x04008587 RID: 34183
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
