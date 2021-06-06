using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F67 RID: 3943
	[ActionCategory("Pegasus")]
	[Tooltip("Delays a State from finishing by a random time. NOTE: Other actions continue, but FINISHED can't happen before the delay.")]
	public class RandomWaitAction : FsmStateAction
	{
		// Token: 0x0600AD24 RID: 44324 RVA: 0x00360138 File Offset: 0x0035E338
		public override void Reset()
		{
			this.m_MinTime = 1f;
			this.m_MaxTime = 1f;
			this.m_FinishEvent = null;
			this.m_RealTime = false;
		}

		// Token: 0x0600AD25 RID: 44325 RVA: 0x00360168 File Offset: 0x0035E368
		public override void OnEnter()
		{
			if (this.m_MinTime.Value <= 0f && this.m_MaxTime.Value <= 0f)
			{
				base.Finish();
				if (this.m_FinishEvent != null)
				{
					base.Fsm.Event(this.m_FinishEvent);
				}
				return;
			}
			this.m_startTime = FsmTime.RealtimeSinceStartup;
			this.m_waitTime = UnityEngine.Random.Range(this.m_MinTime.Value, this.m_MaxTime.Value);
			this.m_updateTime = 0f;
		}

		// Token: 0x0600AD26 RID: 44326 RVA: 0x003601F0 File Offset: 0x0035E3F0
		public override void OnUpdate()
		{
			if (this.m_RealTime)
			{
				this.m_updateTime = FsmTime.RealtimeSinceStartup - this.m_startTime;
			}
			else
			{
				this.m_updateTime += Time.deltaTime;
			}
			if (this.m_updateTime > this.m_waitTime)
			{
				base.Finish();
				if (this.m_FinishEvent != null)
				{
					base.Fsm.Event(this.m_FinishEvent);
				}
			}
		}

		// Token: 0x04009408 RID: 37896
		public FsmFloat m_MinTime;

		// Token: 0x04009409 RID: 37897
		public FsmFloat m_MaxTime;

		// Token: 0x0400940A RID: 37898
		public FsmEvent m_FinishEvent;

		// Token: 0x0400940B RID: 37899
		public bool m_RealTime;

		// Token: 0x0400940C RID: 37900
		private float m_startTime;

		// Token: 0x0400940D RID: 37901
		private float m_waitTime;

		// Token: 0x0400940E RID: 37902
		private float m_updateTime;
	}
}
