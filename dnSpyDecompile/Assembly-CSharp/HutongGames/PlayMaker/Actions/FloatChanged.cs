using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C2C RID: 3116
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if the value of a Float variable changed. Use this to send an event on change, or store a bool that can be used in other operations.")]
	public class FloatChanged : FsmStateAction
	{
		// Token: 0x06009E49 RID: 40521 RVA: 0x0032B207 File Offset: 0x00329407
		public override void Reset()
		{
			this.floatVariable = null;
			this.changedEvent = null;
			this.storeResult = null;
		}

		// Token: 0x06009E4A RID: 40522 RVA: 0x0032B21E File Offset: 0x0032941E
		public override void OnEnter()
		{
			if (this.floatVariable.IsNone)
			{
				base.Finish();
				return;
			}
			this.previousValue = this.floatVariable.Value;
		}

		// Token: 0x06009E4B RID: 40523 RVA: 0x0032B248 File Offset: 0x00329448
		public override void OnUpdate()
		{
			this.storeResult.Value = false;
			if (this.floatVariable.Value != this.previousValue)
			{
				this.previousValue = this.floatVariable.Value;
				this.storeResult.Value = true;
				base.Fsm.Event(this.changedEvent);
			}
		}

		// Token: 0x0400839C RID: 33692
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Float variable to watch for a change.")]
		public FsmFloat floatVariable;

		// Token: 0x0400839D RID: 33693
		[Tooltip("Event to send if the float variable changes.")]
		public FsmEvent changedEvent;

		// Token: 0x0400839E RID: 33694
		[UIHint(UIHint.Variable)]
		[Tooltip("Set to True if the float variable changes.")]
		public FsmBool storeResult;

		// Token: 0x0400839F RID: 33695
		private float previousValue;
	}
}
