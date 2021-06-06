using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C38 RID: 3128
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Forward an event received by this FSM to another target.")]
	public class ForwardEvent : FsmStateAction
	{
		// Token: 0x06009E81 RID: 40577 RVA: 0x0032BA8E File Offset: 0x00329C8E
		public override void Reset()
		{
			this.forwardTo = new FsmEventTarget
			{
				target = FsmEventTarget.EventTarget.FSMComponent
			};
			this.eventsToForward = null;
			this.eatEvents = true;
		}

		// Token: 0x06009E82 RID: 40578 RVA: 0x0032BAB0 File Offset: 0x00329CB0
		public override bool Event(FsmEvent fsmEvent)
		{
			if (this.eventsToForward != null)
			{
				FsmEvent[] array = this.eventsToForward;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == fsmEvent)
					{
						base.Fsm.Event(this.forwardTo, fsmEvent);
						return this.eatEvents;
					}
				}
			}
			return false;
		}

		// Token: 0x040083D3 RID: 33747
		[Tooltip("Forward to this target.")]
		public FsmEventTarget forwardTo;

		// Token: 0x040083D4 RID: 33748
		[Tooltip("The events to forward.")]
		public FsmEvent[] eventsToForward;

		// Token: 0x040083D5 RID: 33749
		[Tooltip("Should this action eat the events or pass them on.")]
		public bool eatEvents;
	}
}
