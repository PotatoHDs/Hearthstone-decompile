using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DB6 RID: 3510
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sets the target FSM for all subsequent events sent by this state. The default 'Self' sends events to this FSM.")]
	public class SetEventTarget : FsmStateAction
	{
		// Token: 0x0600A582 RID: 42370 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x0600A583 RID: 42371 RVA: 0x00346D70 File Offset: 0x00344F70
		public override void OnEnter()
		{
			base.Fsm.EventTarget = this.eventTarget;
			base.Finish();
		}

		// Token: 0x04008C0F RID: 35855
		public FsmEventTarget eventTarget;
	}
}
