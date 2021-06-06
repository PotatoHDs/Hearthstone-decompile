using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D9D RID: 3485
	[ActionCategory(ActionCategory.StateMachine)]
	[ActionTarget(typeof(PlayMakerFSM), "eventTarget", false)]
	[ActionTarget(typeof(GameObject), "eventTarget", false)]
	[Tooltip("Sends an Event after an optional delay. NOTE: To send events between FSMs they must be marked as Global in the Events Browser.")]
	public class SendEvent : FsmStateAction
	{
		// Token: 0x0600A514 RID: 42260 RVA: 0x003457FD File Offset: 0x003439FD
		public override void Reset()
		{
			this.eventTarget = null;
			this.sendEvent = null;
			this.delay = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A515 RID: 42261 RVA: 0x0034581C File Offset: 0x00343A1C
		public override void OnEnter()
		{
			if (this.delay.Value < 0.001f)
			{
				base.Fsm.Event(this.eventTarget, this.sendEvent);
				if (!this.everyFrame)
				{
					base.Finish();
					return;
				}
			}
			else
			{
				this.delayedEvent = base.Fsm.DelayedEvent(this.eventTarget, this.sendEvent, this.delay.Value);
			}
		}

		// Token: 0x0600A516 RID: 42262 RVA: 0x00345889 File Offset: 0x00343A89
		public override void OnUpdate()
		{
			if (!this.everyFrame)
			{
				if (DelayedEvent.WasSent(this.delayedEvent))
				{
					base.Finish();
					return;
				}
			}
			else
			{
				base.Fsm.Event(this.eventTarget, this.sendEvent);
			}
		}

		// Token: 0x04008BAA RID: 35754
		[Tooltip("Where to send the event.")]
		public FsmEventTarget eventTarget;

		// Token: 0x04008BAB RID: 35755
		[RequiredField]
		[Tooltip("The event to send. NOTE: Events must be marked Global to send between FSMs.")]
		public FsmEvent sendEvent;

		// Token: 0x04008BAC RID: 35756
		[HasFloatSlider(0f, 10f)]
		[Tooltip("Optional delay in seconds.")]
		public FsmFloat delay;

		// Token: 0x04008BAD RID: 35757
		[Tooltip("Repeat every frame. Rarely needed, but can be useful when sending events to other FSMs.")]
		public bool everyFrame;

		// Token: 0x04008BAE RID: 35758
		private DelayedEvent delayedEvent;
	}
}
