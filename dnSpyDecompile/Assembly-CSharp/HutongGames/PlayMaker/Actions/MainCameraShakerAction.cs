using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F53 RID: 3923
	[ActionCategory("Pegasus")]
	[Tooltip("[DEPRECATED - Use CameraShakerAction instead.] Shakes the Main Camera a specified amount over time.")]
	public class MainCameraShakerAction : FsmStateAction
	{
		// Token: 0x0600ACD4 RID: 44244 RVA: 0x0035ECEC File Offset: 0x0035CEEC
		public override void Reset()
		{
			this.m_Amount = new FsmVector3
			{
				UseVariable = false
			};
			this.m_Time = 1f;
			this.m_Delay = 0f;
			this.m_FinishedEvent = null;
		}

		// Token: 0x0600ACD5 RID: 44245 RVA: 0x0035ED27 File Offset: 0x0035CF27
		public override void OnEnter()
		{
			this.m_timerSec = 0f;
			this.m_shakeFired = false;
		}

		// Token: 0x0600ACD6 RID: 44246 RVA: 0x0035ED3C File Offset: 0x0035CF3C
		public override void OnUpdate()
		{
			this.m_timerSec += Time.deltaTime;
			if (this.m_timerSec >= this.m_Delay.Value && !this.m_shakeFired)
			{
				this.Shake();
			}
			if (this.m_timerSec >= this.m_Delay.Value + this.m_Time.Value)
			{
				base.Fsm.Event(this.m_FinishedEvent);
				base.Finish();
			}
		}

		// Token: 0x0600ACD7 RID: 44247 RVA: 0x0035EDB2 File Offset: 0x0035CFB2
		private void Shake()
		{
			CameraShakeMgr.Shake(Camera.main, this.m_Amount.Value, this.m_Time.Value);
			this.m_shakeFired = true;
		}

		// Token: 0x040093A9 RID: 37801
		public FsmVector3 m_Amount;

		// Token: 0x040093AA RID: 37802
		public FsmFloat m_Time;

		// Token: 0x040093AB RID: 37803
		public FsmFloat m_Delay;

		// Token: 0x040093AC RID: 37804
		public FsmEvent m_FinishedEvent;

		// Token: 0x040093AD RID: 37805
		private float m_timerSec;

		// Token: 0x040093AE RID: 37806
		private bool m_shakeFired;
	}
}
