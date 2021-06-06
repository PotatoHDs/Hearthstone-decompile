using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F54 RID: 3924
	[ActionCategory("Pegasus")]
	[Tooltip("[DEPRECATED - Use CameraShakeStopAction instead.] Stops shaking the Main Camera over time.")]
	public class MainCameraShakeStopAction : FsmStateAction
	{
		// Token: 0x0600ACD9 RID: 44249 RVA: 0x0035EDDB File Offset: 0x0035CFDB
		public override void Reset()
		{
			this.m_Time = 0.5f;
			this.m_Delay = 0f;
			this.m_FinishedEvent = null;
		}

		// Token: 0x0600ACDA RID: 44250 RVA: 0x0035EE04 File Offset: 0x0035D004
		public override void OnEnter()
		{
			this.m_timerSec = 0f;
			this.m_shakeStopped = false;
		}

		// Token: 0x0600ACDB RID: 44251 RVA: 0x0035EE18 File Offset: 0x0035D018
		public override void OnUpdate()
		{
			this.m_timerSec += Time.deltaTime;
			if (this.m_timerSec >= this.m_Delay.Value && !this.m_shakeStopped)
			{
				this.StopShake();
			}
			if (this.m_timerSec >= this.m_Delay.Value + this.m_Time.Value)
			{
				base.Fsm.Event(this.m_FinishedEvent);
				base.Finish();
			}
		}

		// Token: 0x0600ACDC RID: 44252 RVA: 0x0035EE8E File Offset: 0x0035D08E
		private void StopShake()
		{
			CameraShakeMgr.Stop(Camera.main, this.m_Time.Value);
			this.m_shakeStopped = true;
		}

		// Token: 0x040093AF RID: 37807
		public FsmFloat m_Time;

		// Token: 0x040093B0 RID: 37808
		public FsmFloat m_Delay;

		// Token: 0x040093B1 RID: 37809
		public FsmEvent m_FinishedEvent;

		// Token: 0x040093B2 RID: 37810
		private float m_timerSec;

		// Token: 0x040093B3 RID: 37811
		private bool m_shakeStopped;
	}
}
