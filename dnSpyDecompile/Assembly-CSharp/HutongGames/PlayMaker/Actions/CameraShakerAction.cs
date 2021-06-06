using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F2B RID: 3883
	[ActionCategory("Pegasus")]
	[Tooltip("Shakes a camera over time.")]
	public class CameraShakerAction : CameraAction
	{
		// Token: 0x0600AC31 RID: 44081 RVA: 0x0035B94C File Offset: 0x00359B4C
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
			this.m_Amount = new FsmVector3
			{
				UseVariable = false
			};
			this.m_IntensityCurve = null;
			this.m_Delay = 0f;
			this.m_HoldAtTime = new FsmFloat
			{
				UseVariable = true
			};
			this.m_FinishedEvent = null;
		}

		// Token: 0x0600AC32 RID: 44082 RVA: 0x0035B9C6 File Offset: 0x00359BC6
		public override void OnEnter()
		{
			this.m_timerSec = 0f;
			this.m_shakeFired = false;
		}

		// Token: 0x0600AC33 RID: 44083 RVA: 0x0035B9DC File Offset: 0x00359BDC
		public override void OnUpdate()
		{
			Camera camera = base.GetCamera(this.m_WhichCamera, this.m_SpecificCamera, this.m_NamedCamera);
			if (!camera)
			{
				Error.AddDevFatal("CameraShakerAction.OnUpdate() - Failed to get a camera. Owner={0}", new object[]
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
			if (!this.m_shakeFired)
			{
				if (this.m_IntensityCurve == null || this.m_IntensityCurve.curve == null)
				{
					base.Fsm.Event(this.m_FinishedEvent);
					base.Finish();
					return;
				}
				this.Shake(camera);
			}
			if (CameraShakeMgr.IsShaking(camera))
			{
				return;
			}
			base.Fsm.Event(this.m_FinishedEvent);
			base.Finish();
		}

		// Token: 0x0600AC34 RID: 44084 RVA: 0x0035BAC4 File Offset: 0x00359CC4
		private void Shake(Camera camera)
		{
			Vector3 amount = this.m_Amount.IsNone ? Vector3.zero : this.m_Amount.Value;
			AnimationCurve curve = this.m_IntensityCurve.curve;
			float? holdAtTime = null;
			if (!this.m_HoldAtTime.IsNone)
			{
				holdAtTime = new float?(this.m_HoldAtTime.Value);
			}
			CameraShakeMgr.Shake(camera, amount, curve, holdAtTime);
			this.m_shakeFired = true;
		}

		// Token: 0x040092FA RID: 37626
		public CameraAction.WhichCamera m_WhichCamera;

		// Token: 0x040092FB RID: 37627
		[CheckForComponent(typeof(Camera))]
		public FsmGameObject m_SpecificCamera;

		// Token: 0x040092FC RID: 37628
		public FsmString m_NamedCamera;

		// Token: 0x040092FD RID: 37629
		public FsmVector3 m_Amount;

		// Token: 0x040092FE RID: 37630
		[RequiredField]
		public FsmAnimationCurve m_IntensityCurve;

		// Token: 0x040092FF RID: 37631
		public FsmFloat m_Delay;

		// Token: 0x04009300 RID: 37632
		[Tooltip("[Optional] Hold the shake forever once the shake passes this time.")]
		public FsmFloat m_HoldAtTime;

		// Token: 0x04009301 RID: 37633
		public FsmEvent m_FinishedEvent;

		// Token: 0x04009302 RID: 37634
		private float m_timerSec;

		// Token: 0x04009303 RID: 37635
		private bool m_shakeFired;
	}
}
