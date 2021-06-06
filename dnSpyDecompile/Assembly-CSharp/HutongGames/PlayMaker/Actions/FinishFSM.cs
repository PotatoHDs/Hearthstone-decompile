using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C27 RID: 3111
	[ActionCategory(ActionCategory.StateMachine)]
	[Note("Stop this FSM. If this FSM was launched by a Run FSM action, it will trigger a Finish event in that state.")]
	[Tooltip("Stop this FSM. If this FSM was launched by a Run FSM action, it will trigger a Finish event in that state.")]
	public class FinishFSM : FsmStateAction
	{
		// Token: 0x06009E34 RID: 40500 RVA: 0x0032AF92 File Offset: 0x00329192
		public override void OnEnter()
		{
			base.Fsm.Stop();
		}
	}
}
