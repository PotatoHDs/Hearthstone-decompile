using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CC6 RID: 3270
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends Events based on the comparison of 2 Integers.")]
	public class IntCompare : FsmStateAction
	{
		// Token: 0x0600A0C6 RID: 41158 RVA: 0x0033246D File Offset: 0x0033066D
		public override void Reset()
		{
			this.integer1 = 0;
			this.integer2 = 0;
			this.equal = null;
			this.lessThan = null;
			this.greaterThan = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A0C7 RID: 41159 RVA: 0x003324A3 File Offset: 0x003306A3
		public override void OnEnter()
		{
			this.DoIntCompare();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A0C8 RID: 41160 RVA: 0x003324B9 File Offset: 0x003306B9
		public override void OnUpdate()
		{
			this.DoIntCompare();
		}

		// Token: 0x0600A0C9 RID: 41161 RVA: 0x003324C4 File Offset: 0x003306C4
		private void DoIntCompare()
		{
			if (this.integer1.Value == this.integer2.Value)
			{
				base.Fsm.Event(this.equal);
				return;
			}
			if (this.integer1.Value < this.integer2.Value)
			{
				base.Fsm.Event(this.lessThan);
				return;
			}
			if (this.integer1.Value > this.integer2.Value)
			{
				base.Fsm.Event(this.greaterThan);
			}
		}

		// Token: 0x0600A0CA RID: 41162 RVA: 0x0033254E File Offset: 0x0033074E
		public override string ErrorCheck()
		{
			if (FsmEvent.IsNullOrEmpty(this.equal) && FsmEvent.IsNullOrEmpty(this.lessThan) && FsmEvent.IsNullOrEmpty(this.greaterThan))
			{
				return "Action sends no events!";
			}
			return "";
		}

		// Token: 0x04008656 RID: 34390
		[RequiredField]
		public FsmInt integer1;

		// Token: 0x04008657 RID: 34391
		[RequiredField]
		public FsmInt integer2;

		// Token: 0x04008658 RID: 34392
		[Tooltip("Event sent if Int 1 equals Int 2")]
		public FsmEvent equal;

		// Token: 0x04008659 RID: 34393
		[Tooltip("Event sent if Int 1 is less than Int 2")]
		public FsmEvent lessThan;

		// Token: 0x0400865A RID: 34394
		[Tooltip("Event sent if Int 1 is greater than Int 2")]
		public FsmEvent greaterThan;

		// Token: 0x0400865B RID: 34395
		public bool everyFrame;
	}
}
