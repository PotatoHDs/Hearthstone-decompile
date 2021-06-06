using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Delays a State from finishing by a random time. NOTE: Other actions continue, but FINISHED can't happen before the delay.")]
	public class RandomWaitAction : FsmStateAction
	{
		public FsmFloat m_MinTime;

		public FsmFloat m_MaxTime;

		public FsmEvent m_FinishEvent;

		public bool m_RealTime;

		private float m_startTime;

		private float m_waitTime;

		private float m_updateTime;

		public override void Reset()
		{
			m_MinTime = 1f;
			m_MaxTime = 1f;
			m_FinishEvent = null;
			m_RealTime = false;
		}

		public override void OnEnter()
		{
			if (m_MinTime.Value <= 0f && m_MaxTime.Value <= 0f)
			{
				Finish();
				if (m_FinishEvent != null)
				{
					base.Fsm.Event(m_FinishEvent);
				}
			}
			else
			{
				m_startTime = FsmTime.RealtimeSinceStartup;
				m_waitTime = Random.Range(m_MinTime.Value, m_MaxTime.Value);
				m_updateTime = 0f;
			}
		}

		public override void OnUpdate()
		{
			if (m_RealTime)
			{
				m_updateTime = FsmTime.RealtimeSinceStartup - m_startTime;
			}
			else
			{
				m_updateTime += Time.deltaTime;
			}
			if (m_updateTime > m_waitTime)
			{
				Finish();
				if (m_FinishEvent != null)
				{
					base.Fsm.Event(m_FinishEvent);
				}
			}
		}
	}
}
