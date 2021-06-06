using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("[DEPRECATED - Use CameraShakeStopAction instead.] Stops shaking the Main Camera over time.")]
	public class MainCameraShakeStopAction : FsmStateAction
	{
		public FsmFloat m_Time;

		public FsmFloat m_Delay;

		public FsmEvent m_FinishedEvent;

		private float m_timerSec;

		private bool m_shakeStopped;

		public override void Reset()
		{
			m_Time = 0.5f;
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
			m_timerSec += Time.deltaTime;
			if (m_timerSec >= m_Delay.Value && !m_shakeStopped)
			{
				StopShake();
			}
			if (m_timerSec >= m_Delay.Value + m_Time.Value)
			{
				base.Fsm.Event(m_FinishedEvent);
				Finish();
			}
		}

		private void StopShake()
		{
			CameraShakeMgr.Stop(Camera.main, m_Time.Value);
			m_shakeStopped = true;
		}
	}
}
