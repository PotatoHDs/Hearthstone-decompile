using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BDF RID: 3039
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if the value of a Bool Variable has changed. Use this to send an event on change, or store a bool that can be used in other operations.")]
	public class BoolChanged : FsmStateAction
	{
		// Token: 0x06009CE5 RID: 40165 RVA: 0x00326DC3 File Offset: 0x00324FC3
		public override void Reset()
		{
			this.boolVariable = null;
			this.changedEvent = null;
			this.storeResult = null;
		}

		// Token: 0x06009CE6 RID: 40166 RVA: 0x00326DDA File Offset: 0x00324FDA
		public override void OnEnter()
		{
			if (this.boolVariable.IsNone)
			{
				base.Finish();
				return;
			}
			this.previousValue = this.boolVariable.Value;
		}

		// Token: 0x06009CE7 RID: 40167 RVA: 0x00326E01 File Offset: 0x00325001
		public override void OnUpdate()
		{
			this.storeResult.Value = false;
			if (this.boolVariable.Value != this.previousValue)
			{
				this.storeResult.Value = true;
				base.Fsm.Event(this.changedEvent);
			}
		}

		// Token: 0x0400825D RID: 33373
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Bool variable to watch for changes.")]
		public FsmBool boolVariable;

		// Token: 0x0400825E RID: 33374
		[Tooltip("Event to send if the variable changes.")]
		public FsmEvent changedEvent;

		// Token: 0x0400825F RID: 33375
		[UIHint(UIHint.Variable)]
		[Tooltip("Set to True if changed.")]
		public FsmBool storeResult;

		// Token: 0x04008260 RID: 33376
		private bool previousValue;
	}
}
