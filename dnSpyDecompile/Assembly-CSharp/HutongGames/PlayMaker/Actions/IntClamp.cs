using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CC5 RID: 3269
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Clamp the value of an Integer Variable to a Min/Max range.")]
	public class IntClamp : FsmStateAction
	{
		// Token: 0x0600A0C1 RID: 41153 RVA: 0x003323FE File Offset: 0x003305FE
		public override void Reset()
		{
			this.intVariable = null;
			this.minValue = null;
			this.maxValue = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A0C2 RID: 41154 RVA: 0x0033241C File Offset: 0x0033061C
		public override void OnEnter()
		{
			this.DoClamp();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A0C3 RID: 41155 RVA: 0x00332432 File Offset: 0x00330632
		public override void OnUpdate()
		{
			this.DoClamp();
		}

		// Token: 0x0600A0C4 RID: 41156 RVA: 0x0033243A File Offset: 0x0033063A
		private void DoClamp()
		{
			this.intVariable.Value = Mathf.Clamp(this.intVariable.Value, this.minValue.Value, this.maxValue.Value);
		}

		// Token: 0x04008652 RID: 34386
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt intVariable;

		// Token: 0x04008653 RID: 34387
		[RequiredField]
		public FsmInt minValue;

		// Token: 0x04008654 RID: 34388
		[RequiredField]
		public FsmInt maxValue;

		// Token: 0x04008655 RID: 34389
		public bool everyFrame;
	}
}
