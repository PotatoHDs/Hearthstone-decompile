using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E18 RID: 3608
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Compares 2 Strings and sends Events based on the result.")]
	public class StringCompare : FsmStateAction
	{
		// Token: 0x0600A729 RID: 42793 RVA: 0x0034B908 File Offset: 0x00349B08
		public override void Reset()
		{
			this.stringVariable = null;
			this.compareTo = "";
			this.equalEvent = null;
			this.notEqualEvent = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A72A RID: 42794 RVA: 0x0034B93D File Offset: 0x00349B3D
		public override void OnEnter()
		{
			this.DoStringCompare();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A72B RID: 42795 RVA: 0x0034B953 File Offset: 0x00349B53
		public override void OnUpdate()
		{
			this.DoStringCompare();
		}

		// Token: 0x0600A72C RID: 42796 RVA: 0x0034B95C File Offset: 0x00349B5C
		private void DoStringCompare()
		{
			if (this.stringVariable == null || this.compareTo == null)
			{
				return;
			}
			bool flag = this.stringVariable.Value == this.compareTo.Value;
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

		// Token: 0x04008DA7 RID: 36263
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;

		// Token: 0x04008DA8 RID: 36264
		public FsmString compareTo;

		// Token: 0x04008DA9 RID: 36265
		public FsmEvent equalEvent;

		// Token: 0x04008DAA RID: 36266
		public FsmEvent notEqualEvent;

		// Token: 0x04008DAB RID: 36267
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the true/false result in a bool variable.")]
		public FsmBool storeResult;

		// Token: 0x04008DAC RID: 36268
		[Tooltip("Repeat every frame. Useful if any of the strings are changing over time.")]
		public bool everyFrame;
	}
}
