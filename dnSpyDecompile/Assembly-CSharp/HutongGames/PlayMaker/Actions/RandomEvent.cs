using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D4B RID: 3403
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sends a Random State Event after an optional delay. Use this to transition to a random state from the current state.")]
	public class RandomEvent : FsmStateAction
	{
		// Token: 0x0600A381 RID: 41857 RVA: 0x0033F3D9 File Offset: 0x0033D5D9
		public override void Reset()
		{
			this.delay = null;
			this.noRepeat = false;
		}

		// Token: 0x0600A382 RID: 41858 RVA: 0x0033F3F0 File Offset: 0x0033D5F0
		public override void OnEnter()
		{
			if (base.State.Transitions.Length == 0)
			{
				return;
			}
			if (this.lastEventIndex == -1)
			{
				this.lastEventIndex = UnityEngine.Random.Range(0, base.State.Transitions.Length);
			}
			if (this.delay.Value < 0.001f)
			{
				base.Fsm.Event(this.GetRandomEvent());
				base.Finish();
				return;
			}
			this.delayedEvent = base.Fsm.DelayedEvent(this.GetRandomEvent(), this.delay.Value);
		}

		// Token: 0x0600A383 RID: 41859 RVA: 0x0033F47A File Offset: 0x0033D67A
		public override void OnUpdate()
		{
			if (DelayedEvent.WasSent(this.delayedEvent))
			{
				base.Finish();
			}
		}

		// Token: 0x0600A384 RID: 41860 RVA: 0x0033F490 File Offset: 0x0033D690
		private FsmEvent GetRandomEvent()
		{
			do
			{
				this.randomEventIndex = UnityEngine.Random.Range(0, base.State.Transitions.Length);
			}
			while (this.noRepeat.Value && base.State.Transitions.Length > 1 && this.randomEventIndex == this.lastEventIndex);
			this.lastEventIndex = this.randomEventIndex;
			return base.State.Transitions[this.randomEventIndex].FsmEvent;
		}

		// Token: 0x040089AD RID: 35245
		[HasFloatSlider(0f, 10f)]
		[Tooltip("Delay before sending the event.")]
		public FsmFloat delay;

		// Token: 0x040089AE RID: 35246
		[Tooltip("Don't repeat the same event twice in a row.")]
		public FsmBool noRepeat;

		// Token: 0x040089AF RID: 35247
		private DelayedEvent delayedEvent;

		// Token: 0x040089B0 RID: 35248
		private int randomEventIndex;

		// Token: 0x040089B1 RID: 35249
		private int lastEventIndex = -1;
	}
}
