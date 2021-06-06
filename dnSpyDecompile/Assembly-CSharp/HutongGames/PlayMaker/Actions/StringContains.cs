using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E19 RID: 3609
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if a String contains another String.")]
	public class StringContains : FsmStateAction
	{
		// Token: 0x0600A72E RID: 42798 RVA: 0x0034B9E3 File Offset: 0x00349BE3
		public override void Reset()
		{
			this.stringVariable = null;
			this.containsString = "";
			this.trueEvent = null;
			this.falseEvent = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A72F RID: 42799 RVA: 0x0034BA18 File Offset: 0x00349C18
		public override void OnEnter()
		{
			this.DoStringContains();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A730 RID: 42800 RVA: 0x0034BA2E File Offset: 0x00349C2E
		public override void OnUpdate()
		{
			this.DoStringContains();
		}

		// Token: 0x0600A731 RID: 42801 RVA: 0x0034BA38 File Offset: 0x00349C38
		private void DoStringContains()
		{
			if (this.stringVariable.IsNone || this.containsString.IsNone)
			{
				return;
			}
			bool flag = this.stringVariable.Value.Contains(this.containsString.Value);
			if (this.storeResult != null)
			{
				this.storeResult.Value = flag;
			}
			if (flag && this.trueEvent != null)
			{
				base.Fsm.Event(this.trueEvent);
				return;
			}
			if (!flag && this.falseEvent != null)
			{
				base.Fsm.Event(this.falseEvent);
			}
		}

		// Token: 0x04008DAD RID: 36269
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The String variable to test.")]
		public FsmString stringVariable;

		// Token: 0x04008DAE RID: 36270
		[RequiredField]
		[Tooltip("Test if the String variable contains this string.")]
		public FsmString containsString;

		// Token: 0x04008DAF RID: 36271
		[Tooltip("Event to send if true.")]
		public FsmEvent trueEvent;

		// Token: 0x04008DB0 RID: 36272
		[Tooltip("Event to send if false.")]
		public FsmEvent falseEvent;

		// Token: 0x04008DB1 RID: 36273
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the true/false result in a bool variable.")]
		public FsmBool storeResult;

		// Token: 0x04008DB2 RID: 36274
		[Tooltip("Repeat every frame. Useful if any of the strings are changing over time.")]
		public bool everyFrame;
	}
}
