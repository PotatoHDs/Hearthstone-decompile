using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E17 RID: 3607
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if the value of a string variable has changed. Use this to send an event on change, or store a bool that can be used in other operations.")]
	public class StringChanged : FsmStateAction
	{
		// Token: 0x0600A725 RID: 42789 RVA: 0x0034B893 File Offset: 0x00349A93
		public override void Reset()
		{
			this.stringVariable = null;
			this.changedEvent = null;
			this.storeResult = null;
		}

		// Token: 0x0600A726 RID: 42790 RVA: 0x0034B8AA File Offset: 0x00349AAA
		public override void OnEnter()
		{
			if (this.stringVariable.IsNone)
			{
				base.Finish();
				return;
			}
			this.previousValue = this.stringVariable.Value;
		}

		// Token: 0x0600A727 RID: 42791 RVA: 0x0034B8D1 File Offset: 0x00349AD1
		public override void OnUpdate()
		{
			if (this.stringVariable.Value != this.previousValue)
			{
				this.storeResult.Value = true;
				base.Fsm.Event(this.changedEvent);
			}
		}

		// Token: 0x04008DA3 RID: 36259
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;

		// Token: 0x04008DA4 RID: 36260
		public FsmEvent changedEvent;

		// Token: 0x04008DA5 RID: 36261
		[UIHint(UIHint.Variable)]
		public FsmBool storeResult;

		// Token: 0x04008DA6 RID: 36262
		private string previousValue;
	}
}
