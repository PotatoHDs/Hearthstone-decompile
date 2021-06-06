using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F2C RID: 3884
	[ActionCategory("Pegasus")]
	[Tooltip("Stops a shaking camera, over time or immediately.")]
	public class CameraShakeStopAction : CameraAction
	{
		// Token: 0x0600AC36 RID: 44086 RVA: 0x0035BB3C File Offset: 0x00359D3C
		public override void Reset()
		{
			this.m_WhichCamera = CameraAction.WhichCamera.MAIN;
			this.m_SpecificCamera = new FsmGameObject
			{
				UseVariable = true
			};
			this.m_NamedCamera = new FsmString
			{
				UseVariable = false
			};
			this.m_Time = 0f;
			this.m_Delay = 0f;
			this.m_FinishedEvent = null;
		}

		// Token: 0x0600AC37 RID: 44087 RVA: 0x0035BB9B File Offset: 0x00359D9B
		public override void OnEnter()
		{
			this.m_timerSec = 0f;
			this.m_shakeStopped = false;
		}

		// Token: 0x0600AC38 RID: 44088 RVA: 0x0035BBB0 File Offset: 0x00359DB0
		public override void OnUpdate()
		{
			Camera camera = base.GetCamera(this.m_WhichCamera, this.m_SpecificCamera, this.m_NamedCamera);
			if (!camera)
			{
				Error.AddDevFatal("CameraShakeStopAction.OnUpdate() - Failed to get a camera. Owner={0}", new object[]
				{
					base.Owner
				});
				base.Finish();
			}
			this.m_timerSec += Time.deltaTime;
			float num = this.m_Delay.IsNone ? 0f : this.m_Delay.Value;
			if (this.m_timerSec < num)
			{
				return;
			}
			if (!this.m_shakeStopped)
			{
				this.StopShake(camera);
			}
			if (CameraShakeMgr.IsShaking(camera))
			{
				return;
			}
			base.Fsm.Event(this.m_FinishedEvent);
			base.Finish();
		}

		// Token: 0x0600AC39 RID: 44089 RVA: 0x0035BC68 File Offset: 0x00359E68
		private void StopShake(Camera camera)
		{
			float time = this.m_Time.IsNone ? 0f : this.m_Time.Value;
			CameraShakeMgr.Stop(camera, time);
			this.m_shakeStopped = true;
		}

		// Token: 0x04009304 RID: 37636
		public CameraAction.WhichCamera m_WhichCamera;

		// Token: 0x04009305 RID: 37637
		[CheckForComponent(typeof(Camera))]
		public FsmGameObject m_SpecificCamera;

		// Token: 0x04009306 RID: 37638
		public FsmString m_NamedCamera;

		// Token: 0x04009307 RID: 37639
		public FsmFloat m_Time;

		// Token: 0x04009308 RID: 37640
		public FsmFloat m_Delay;

		// Token: 0x04009309 RID: 37641
		public FsmEvent m_FinishedEvent;

		// Token: 0x0400930A RID: 37642
		private float m_timerSec;

		// Token: 0x0400930B RID: 37643
		private bool m_shakeStopped;
	}
}
