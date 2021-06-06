using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C6B RID: 3179
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Gets the event that caused the transition to the current state, and stores it in a String Variable.")]
	public class GetLastEvent : FsmStateAction
	{
		// Token: 0x06009F6A RID: 40810 RVA: 0x0032E7F9 File Offset: 0x0032C9F9
		public override void Reset()
		{
			this.storeEvent = null;
		}

		// Token: 0x06009F6B RID: 40811 RVA: 0x0032E802 File Offset: 0x0032CA02
		public override void OnEnter()
		{
			this.storeEvent.Value = ((base.Fsm.LastTransition == null) ? "START" : base.Fsm.LastTransition.EventName);
			base.Finish();
		}

		// Token: 0x04008509 RID: 34057
		[UIHint(UIHint.Variable)]
		public FsmString storeEvent;
	}
}
