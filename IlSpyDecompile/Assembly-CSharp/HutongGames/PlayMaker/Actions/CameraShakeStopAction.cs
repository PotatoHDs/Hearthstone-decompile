using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Stops a shaking camera, over time or immediately.")]
	public class CameraShakeStopAction : CameraAction
	{
		public WhichCamera m_WhichCamera;

		[CheckForComponent(typeof(Camera))]
		public FsmGameObject m_SpecificCamera;

		public FsmString m_NamedCamera;

		public FsmFloat m_Time;

		public FsmFloat m_Delay;

		public FsmEvent m_FinishedEvent;

		private float m_timerSec;

		private bool m_shakeStopped;

		public override void Reset()
		{
			m_WhichCamera = WhichCamera.MAIN;
			m_SpecificCamera = new FsmGameObject
			{
				UseVariable = true
			};
			m_NamedCamera = new FsmString
			{
				UseVariable = false
			};
			m_Time = 0f;
			m_Delay = 0f;
			m_FinishedEvent = null;
		}

		public override void OnEnter()
		{
			m_timerSec = 0f;
			m_shakeStopped = false;
		}

		public override void OnUpdate()
		{
			Camera camera = GetCamera(m_WhichCamera, m_SpecificCamera, m_NamedCamera);
			if (!camera)
			{
				Error.AddDevFatal("CameraShakeStopAction.OnUpdate() - Failed to get a camera. Owner={0}", base.Owner);
				Finish();
			}
			m_timerSec += Time.deltaTime;
			float num = (m_Delay.IsNone ? 0f : m_Delay.Value);
			if (!(m_timerSec < num))
			{
				if (!m_shakeStopped)
				{
					StopShake(camera);
				}
				if (!CameraShakeMgr.IsShaking(camera))
				{
					base.Fsm.Event(m_FinishedEvent);
					Finish();
				}
			}
		}

		private void StopShake(Camera camera)
		{
			float time = (m_Time.IsNone ? 0f : m_Time.Value);
			CameraShakeMgr.Stop(camera, time);
			m_shakeStopped = true;
		}
	}
}
