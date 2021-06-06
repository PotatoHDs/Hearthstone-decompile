using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DA2 RID: 3490
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sends a Random Event picked from an array of Events. Optionally set the relative weight of each event.")]
	public class SendRandomEvent : FsmStateAction
	{
		// Token: 0x0600A528 RID: 42280 RVA: 0x00345FEC File Offset: 0x003441EC
		public override void Reset()
		{
			this.events = new FsmEvent[3];
			this.weights = new FsmFloat[]
			{
				1f,
				1f,
				1f
			};
			this.delay = null;
		}

		// Token: 0x0600A529 RID: 42281 RVA: 0x00346040 File Offset: 0x00344240
		public override void OnEnter()
		{
			if (this.events.Length != 0)
			{
				int randomWeightedIndex = ActionHelpers.GetRandomWeightedIndex(this.weights);
				if (randomWeightedIndex != -1)
				{
					if (this.delay.Value < 0.001f)
					{
						base.Fsm.Event(this.events[randomWeightedIndex]);
						base.Finish();
						return;
					}
					this.delayedEvent = base.Fsm.DelayedEvent(this.events[randomWeightedIndex], this.delay.Value);
					return;
				}
			}
			base.Finish();
		}

		// Token: 0x0600A52A RID: 42282 RVA: 0x003460BD File Offset: 0x003442BD
		public override void OnUpdate()
		{
			if (DelayedEvent.WasSent(this.delayedEvent))
			{
				base.Finish();
			}
		}

		// Token: 0x04008BC3 RID: 35779
		[CompoundArray("Events", "Event", "Weight")]
		public FsmEvent[] events;

		// Token: 0x04008BC4 RID: 35780
		[HasFloatSlider(0f, 1f)]
		public FsmFloat[] weights;

		// Token: 0x04008BC5 RID: 35781
		public FsmFloat delay;

		// Token: 0x04008BC6 RID: 35782
		private DelayedEvent delayedEvent;
	}
}
