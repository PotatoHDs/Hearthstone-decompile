using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CFA RID: 3322
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Multiplies a Float by Time.deltaTime to use in frame-rate independent operations. E.g., 10 becomes 10 units per second.")]
	public class PerSecond : FsmStateAction
	{
		// Token: 0x0600A1D4 RID: 41428 RVA: 0x0033946B File Offset: 0x0033766B
		public override void Reset()
		{
			this.floatValue = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A1D5 RID: 41429 RVA: 0x00339482 File Offset: 0x00337682
		public override void OnEnter()
		{
			this.DoPerSecond();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A1D6 RID: 41430 RVA: 0x00339498 File Offset: 0x00337698
		public override void OnUpdate()
		{
			this.DoPerSecond();
		}

		// Token: 0x0600A1D7 RID: 41431 RVA: 0x003394A0 File Offset: 0x003376A0
		private void DoPerSecond()
		{
			if (this.storeResult == null)
			{
				return;
			}
			this.storeResult.Value = this.floatValue.Value * Time.deltaTime;
		}

		// Token: 0x040087E9 RID: 34793
		[RequiredField]
		public FsmFloat floatValue;

		// Token: 0x040087EA RID: 34794
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeResult;

		// Token: 0x040087EB RID: 34795
		public bool everyFrame;
	}
}
