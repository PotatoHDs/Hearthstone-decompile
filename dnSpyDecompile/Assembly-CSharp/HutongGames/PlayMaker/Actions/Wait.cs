using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EC2 RID: 3778
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Delays a State from finishing by the specified time. NOTE: Other actions continue, but FINISHED can't happen before Time.")]
	public class Wait : FsmStateAction
	{
		// Token: 0x0600AA53 RID: 43603 RVA: 0x003550CD File Offset: 0x003532CD
		public override void Reset()
		{
			this.time = 1f;
			this.finishEvent = null;
			this.realTime = false;
		}

		// Token: 0x0600AA54 RID: 43604 RVA: 0x003550F0 File Offset: 0x003532F0
		public override void OnEnter()
		{
			if (this.time.Value <= 0f)
			{
				base.Fsm.Event(this.finishEvent);
				base.Finish();
				return;
			}
			this.startTime = FsmTime.RealtimeSinceStartup;
			this.timer = 0f;
		}

		// Token: 0x0600AA55 RID: 43605 RVA: 0x00355140 File Offset: 0x00353340
		public override void OnUpdate()
		{
			if (this.realTime)
			{
				this.timer = FsmTime.RealtimeSinceStartup - this.startTime;
			}
			else
			{
				this.timer += Time.deltaTime;
			}
			if (this.timer >= this.time.Value)
			{
				base.Finish();
				if (this.finishEvent != null)
				{
					base.Fsm.Event(this.finishEvent);
				}
			}
		}

		// Token: 0x040090EB RID: 37099
		[RequiredField]
		public FsmFloat time;

		// Token: 0x040090EC RID: 37100
		public FsmEvent finishEvent;

		// Token: 0x040090ED RID: 37101
		public bool realTime;

		// Token: 0x040090EE RID: 37102
		private float startTime;

		// Token: 0x040090EF RID: 37103
		private float timer;
	}
}
