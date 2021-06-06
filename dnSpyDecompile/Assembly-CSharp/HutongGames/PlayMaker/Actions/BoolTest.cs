using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BE3 RID: 3043
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends Events based on the value of a Boolean Variable.")]
	public class BoolTest : FsmStateAction
	{
		// Token: 0x06009CF6 RID: 40182 RVA: 0x00326FD5 File Offset: 0x003251D5
		public override void Reset()
		{
			this.boolVariable = null;
			this.isTrue = null;
			this.isFalse = null;
			this.everyFrame = false;
		}

		// Token: 0x06009CF7 RID: 40183 RVA: 0x00326FF3 File Offset: 0x003251F3
		public override void OnEnter()
		{
			base.Fsm.Event(this.boolVariable.Value ? this.isTrue : this.isFalse);
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009CF8 RID: 40184 RVA: 0x00327029 File Offset: 0x00325229
		public override void OnUpdate()
		{
			base.Fsm.Event(this.boolVariable.Value ? this.isTrue : this.isFalse);
		}

		// Token: 0x0400826B RID: 33387
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Readonly]
		[Tooltip("The Bool variable to test.")]
		public FsmBool boolVariable;

		// Token: 0x0400826C RID: 33388
		[Tooltip("Event to send if the Bool variable is True.")]
		public FsmEvent isTrue;

		// Token: 0x0400826D RID: 33389
		[Tooltip("Event to send if the Bool variable is False.")]
		public FsmEvent isFalse;

		// Token: 0x0400826E RID: 33390
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
	}
}
