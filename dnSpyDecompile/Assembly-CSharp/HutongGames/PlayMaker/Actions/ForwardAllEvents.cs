using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C37 RID: 3127
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Forwards all event received by this FSM to another target. Optionally specify a list of events to ignore.")]
	public class ForwardAllEvents : FsmStateAction
	{
		// Token: 0x06009E7E RID: 40574 RVA: 0x0032BA14 File Offset: 0x00329C14
		public override void Reset()
		{
			this.forwardTo = new FsmEventTarget
			{
				target = FsmEventTarget.EventTarget.FSMComponent
			};
			this.exceptThese = new FsmEvent[]
			{
				FsmEvent.Finished
			};
			this.eatEvents = true;
		}

		// Token: 0x06009E7F RID: 40575 RVA: 0x0032BA44 File Offset: 0x00329C44
		public override bool Event(FsmEvent fsmEvent)
		{
			if (this.exceptThese != null)
			{
				FsmEvent[] array = this.exceptThese;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == fsmEvent)
					{
						return false;
					}
				}
			}
			base.Fsm.Event(this.forwardTo, fsmEvent);
			return this.eatEvents;
		}

		// Token: 0x040083D0 RID: 33744
		[Tooltip("Forward to this target.")]
		public FsmEventTarget forwardTo;

		// Token: 0x040083D1 RID: 33745
		[Tooltip("Don't forward these events.")]
		public FsmEvent[] exceptThese;

		// Token: 0x040083D2 RID: 33746
		[Tooltip("Should this action eat the events or pass them on.")]
		public bool eatEvents;
	}
}
