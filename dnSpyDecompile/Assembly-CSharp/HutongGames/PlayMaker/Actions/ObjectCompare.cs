using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CF8 RID: 3320
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Compare 2 Object Variables and send events based on the result.")]
	public class ObjectCompare : FsmStateAction
	{
		// Token: 0x0600A1CC RID: 41420 RVA: 0x00339390 File Offset: 0x00337590
		public override void Reset()
		{
			this.objectVariable = null;
			this.compareTo = null;
			this.storeResult = null;
			this.equalEvent = null;
			this.notEqualEvent = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A1CD RID: 41421 RVA: 0x003393BC File Offset: 0x003375BC
		public override void OnEnter()
		{
			this.DoObjectCompare();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A1CE RID: 41422 RVA: 0x003393D2 File Offset: 0x003375D2
		public override void OnUpdate()
		{
			this.DoObjectCompare();
		}

		// Token: 0x0600A1CF RID: 41423 RVA: 0x003393DC File Offset: 0x003375DC
		private void DoObjectCompare()
		{
			bool flag = this.objectVariable.Value == this.compareTo.Value;
			this.storeResult.Value = flag;
			base.Fsm.Event(flag ? this.equalEvent : this.notEqualEvent);
		}

		// Token: 0x040087E2 RID: 34786
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Readonly]
		public FsmObject objectVariable;

		// Token: 0x040087E3 RID: 34787
		[RequiredField]
		public FsmObject compareTo;

		// Token: 0x040087E4 RID: 34788
		[Tooltip("Event to send if the 2 object values are equal.")]
		public FsmEvent equalEvent;

		// Token: 0x040087E5 RID: 34789
		[Tooltip("Event to send if the 2 object values are not equal.")]
		public FsmEvent notEqualEvent;

		// Token: 0x040087E6 RID: 34790
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a variable.")]
		public FsmBool storeResult;

		// Token: 0x040087E7 RID: 34791
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
