using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C21 RID: 3105
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Compares 2 Enum values and sends Events based on the result.")]
	public class EnumCompare : FsmStateAction
	{
		// Token: 0x06009E15 RID: 40469 RVA: 0x0032A9A1 File Offset: 0x00328BA1
		public override void Reset()
		{
			this.enumVariable = null;
			this.compareTo = null;
			this.equalEvent = null;
			this.notEqualEvent = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009E16 RID: 40470 RVA: 0x0032A9CD File Offset: 0x00328BCD
		public override void OnEnter()
		{
			this.DoEnumCompare();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E17 RID: 40471 RVA: 0x0032A9E3 File Offset: 0x00328BE3
		public override void OnUpdate()
		{
			this.DoEnumCompare();
		}

		// Token: 0x06009E18 RID: 40472 RVA: 0x0032A9EC File Offset: 0x00328BEC
		private void DoEnumCompare()
		{
			if (this.enumVariable == null || this.compareTo == null)
			{
				return;
			}
			bool flag = object.Equals(this.enumVariable.Value, this.compareTo.Value);
			if (this.storeResult != null)
			{
				this.storeResult.Value = flag;
			}
			if (flag && this.equalEvent != null)
			{
				base.Fsm.Event(this.equalEvent);
				return;
			}
			if (!flag && this.notEqualEvent != null)
			{
				base.Fsm.Event(this.notEqualEvent);
			}
		}

		// Token: 0x0400836C RID: 33644
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmEnum enumVariable;

		// Token: 0x0400836D RID: 33645
		[MatchFieldType("enumVariable")]
		public FsmEnum compareTo;

		// Token: 0x0400836E RID: 33646
		public FsmEvent equalEvent;

		// Token: 0x0400836F RID: 33647
		public FsmEvent notEqualEvent;

		// Token: 0x04008370 RID: 33648
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the true/false result in a bool variable.")]
		public FsmBool storeResult;

		// Token: 0x04008371 RID: 33649
		[Tooltip("Repeat every frame. Useful if the enum is changing over time.")]
		public bool everyFrame;
	}
}
