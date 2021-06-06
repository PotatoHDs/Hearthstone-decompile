using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D4E RID: 3406
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Delays a State from finishing by a random time. NOTE: Other actions continue, but FINISHED can't happen before Time.")]
	public class RandomWait : FsmStateAction
	{
		// Token: 0x0600A38D RID: 41869 RVA: 0x0033F6EE File Offset: 0x0033D8EE
		public override void Reset()
		{
			this.min = 0f;
			this.max = 1f;
			this.finishEvent = null;
			this.realTime = false;
		}

		// Token: 0x0600A38E RID: 41870 RVA: 0x0033F720 File Offset: 0x0033D920
		public override void OnEnter()
		{
			this.time = UnityEngine.Random.Range(this.min.Value, this.max.Value);
			if (this.time <= 0f)
			{
				base.Fsm.Event(this.finishEvent);
				base.Finish();
				return;
			}
			this.startTime = FsmTime.RealtimeSinceStartup;
			this.timer = 0f;
		}

		// Token: 0x0600A38F RID: 41871 RVA: 0x0033F78C File Offset: 0x0033D98C
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
			if (this.timer >= this.time)
			{
				base.Finish();
				if (this.finishEvent != null)
				{
					base.Fsm.Event(this.finishEvent);
				}
			}
		}

		// Token: 0x040089BC RID: 35260
		[RequiredField]
		[Tooltip("Minimum amount of time to wait.")]
		public FsmFloat min;

		// Token: 0x040089BD RID: 35261
		[RequiredField]
		[Tooltip("Maximum amount of time to wait.")]
		public FsmFloat max;

		// Token: 0x040089BE RID: 35262
		[Tooltip("Event to send when timer is finished.")]
		public FsmEvent finishEvent;

		// Token: 0x040089BF RID: 35263
		[Tooltip("Ignore time scale.")]
		public bool realTime;

		// Token: 0x040089C0 RID: 35264
		private float startTime;

		// Token: 0x040089C1 RID: 35265
		private float timer;

		// Token: 0x040089C2 RID: 35266
		private float time;
	}
}
