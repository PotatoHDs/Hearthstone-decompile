using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CC4 RID: 3268
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if the value of an integer variable changed. Use this to send an event on change, or store a bool that can be used in other operations.")]
	public class IntChanged : FsmStateAction
	{
		// Token: 0x0600A0BD RID: 41149 RVA: 0x00332363 File Offset: 0x00330563
		public override void Reset()
		{
			this.intVariable = null;
			this.changedEvent = null;
			this.storeResult = null;
		}

		// Token: 0x0600A0BE RID: 41150 RVA: 0x0033237A File Offset: 0x0033057A
		public override void OnEnter()
		{
			if (this.intVariable.IsNone)
			{
				base.Finish();
				return;
			}
			this.previousValue = this.intVariable.Value;
		}

		// Token: 0x0600A0BF RID: 41151 RVA: 0x003323A4 File Offset: 0x003305A4
		public override void OnUpdate()
		{
			this.storeResult.Value = false;
			if (this.intVariable.Value != this.previousValue)
			{
				this.previousValue = this.intVariable.Value;
				this.storeResult.Value = true;
				base.Fsm.Event(this.changedEvent);
			}
		}

		// Token: 0x0400864E RID: 34382
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt intVariable;

		// Token: 0x0400864F RID: 34383
		public FsmEvent changedEvent;

		// Token: 0x04008650 RID: 34384
		[UIHint(UIHint.Variable)]
		public FsmBool storeResult;

		// Token: 0x04008651 RID: 34385
		private int previousValue;
	}
}
