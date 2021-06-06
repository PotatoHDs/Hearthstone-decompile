using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("[DEPRECATED - Use CameraShakerAction instead.] Shakes the Main Camera a specified amount over time.")]
	public class MainCameraShakerAction : FsmStateAction
	{
		public FsmVector3 m_Amount;

		public FsmFloat m_Time;

		public FsmFloat m_Delay;

		public FsmEvent m_FinishedEvent;

		private float m_timerSec;

		private bool m_shakeFired;

		public override void Reset()
		{
			m_Amount = new FsmVector3
			{
				UseVariable = false
			};
			m_Time = 1f;
			m_Delay = 0f;
			m_FinishedEvent = null;
		}

		public override void OnEnter()
		{
			m_timerSec = 0f;
			m_shakeFired = false;
		}

		public override void OnUpdate()
		{
			m_timerSec += Time.deltaTime;
			if (m_timerSec >= m_Delay.Value && !m_shakeFired)
			{
				Shake();
			}
			if (m_timerSec >= m_Delay.Value + m_Time.Value)
			{
				base.Fsm.Event(m_FinishedEvent);
				Finish();
			}
		}

		private void Shake()
		{
			CameraShakeMgr.Shake(Camera.main, m_Amount.Value, m_Time.Value);
			m_shakeFired = true;
		}
	}
}
