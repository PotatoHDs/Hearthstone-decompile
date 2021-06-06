using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CEA RID: 3306
	[ActionCategory(ActionCategory.StateMachine)]
	[Note("Kill all queued delayed events.")]
	[Tooltip("Kill all queued delayed events. Normally delayed events are automatically killed when the active state is exited, but you can override this behaviour in FSM settings. If you choose to keep delayed events you can use this action to kill them when needed.")]
	public class KillDelayedEvents : FsmStateAction
	{
		// Token: 0x0600A17D RID: 41341 RVA: 0x00337BF3 File Offset: 0x00335DF3
		public override void OnEnter()
		{
			base.Fsm.KillDelayedEvents();
			base.Finish();
		}
	}
}
