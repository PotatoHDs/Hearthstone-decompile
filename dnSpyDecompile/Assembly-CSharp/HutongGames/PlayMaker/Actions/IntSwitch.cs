using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CC8 RID: 3272
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends an Event based on the value of an Integer Variable.")]
	public class IntSwitch : FsmStateAction
	{
		// Token: 0x0600A0D1 RID: 41169 RVA: 0x00332674 File Offset: 0x00330874
		public override void Reset()
		{
			this.intVariable = null;
			this.compareTo = new FsmInt[1];
			this.sendEvent = new FsmEvent[1];
			this.everyFrame = false;
		}

		// Token: 0x0600A0D2 RID: 41170 RVA: 0x0033269C File Offset: 0x0033089C
		public override void OnEnter()
		{
			this.DoIntSwitch();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A0D3 RID: 41171 RVA: 0x003326B2 File Offset: 0x003308B2
		public override void OnUpdate()
		{
			this.DoIntSwitch();
		}

		// Token: 0x0600A0D4 RID: 41172 RVA: 0x003326BC File Offset: 0x003308BC
		private void DoIntSwitch()
		{
			if (this.intVariable.IsNone)
			{
				return;
			}
			for (int i = 0; i < this.compareTo.Length; i++)
			{
				if (this.intVariable.Value == this.compareTo[i].Value)
				{
					base.Fsm.Event(this.sendEvent[i]);
					return;
				}
			}
		}

		// Token: 0x04008661 RID: 34401
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt intVariable;

		// Token: 0x04008662 RID: 34402
		[CompoundArray("Int Switches", "Compare Int", "Send Event")]
		public FsmInt[] compareTo;

		// Token: 0x04008663 RID: 34403
		public FsmEvent[] sendEvent;

		// Token: 0x04008664 RID: 34404
		public bool everyFrame;
	}
}
