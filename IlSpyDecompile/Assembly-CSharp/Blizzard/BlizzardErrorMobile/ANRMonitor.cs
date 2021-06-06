using System;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace Blizzard.BlizzardErrorMobile
{
	internal class ANRMonitor
	{
		private int m_waitLimitMilliSeconds = 10000;

		private int m_maxSleepTimeMilliSeconds;

		private float m_recordSeconds;

		private float m_throttle;

		private Thread m_thread;

		private bool m_paused;

		private bool m_happened;

		private DateTime m_calledTime = DateTime.UtcNow;

		private object m_timeLock = new object();

		private TimeSpan m_timeout = TimeSpan.FromMilliseconds(500.0);

		private ManualResetEventSlim ready = new ManualResetEventSlim();

		public bool IsTerminated { get; private set; } = true;


		public event Action Detected;

		public event Action FirstUpdateAfterANR;

		public ANRMonitor(float waitLimitSeconds, float throttle, MonoBehaviour monoBehaviour)
		{
			if (waitLimitSeconds < 3f)
			{
				ExceptionLogger.LogWarning("ANRMonitor: Wait time({0}) is too short, ANRMonitor is off", waitLimitSeconds);
				return;
			}
			if (throttle <= 0f)
			{
				ExceptionLogger.LogWarning("ANRMonitor: Throttle value({0}) is not positive, ANRMonitor is off", throttle);
				return;
			}
			SetWaitSeconds(waitLimitSeconds, throttle);
			try
			{
				monoBehaviour.StartCoroutine(TimeRecorderProc());
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("ANRMonitor: failed to run coroutine, ANRMonitor is off: {0}", ex.Message);
				IsTerminated = true;
				return;
			}
			m_thread = new Thread(MonitorLoop);
			m_thread.Start();
		}

		public void OnPause(bool pauseStatus)
		{
			m_paused = pauseStatus;
			if (!m_paused)
			{
				ready.Set();
			}
		}

		public void SetWaitSeconds(float waitLimitSeconds, float throttle)
		{
			if (waitLimitSeconds > 0f && throttle > 0f)
			{
				m_waitLimitMilliSeconds = (int)((double)waitLimitSeconds * 1000.0);
				m_maxSleepTimeMilliSeconds = m_waitLimitMilliSeconds - 1000;
				m_recordSeconds = Math.Min(waitLimitSeconds / 2f, 2f);
				m_throttle = throttle;
				IsTerminated = false;
			}
			else
			{
				ExceptionLogger.LogInfo("ANRMonitor: Terminated by wait-seconds({0}) and throttle({1})", waitLimitSeconds, throttle);
				IsTerminated = true;
			}
		}

		private void RefreshCalledTime()
		{
			if (Monitor.TryEnter(m_timeLock, m_timeout))
			{
				try
				{
					m_calledTime = DateTime.UtcNow;
				}
				finally
				{
					Monitor.Exit(m_timeLock);
				}
			}
			else
			{
				ExceptionLogger.LogWarning("ANRMonitor: m_timeLock is not released. ignore it.");
				m_calledTime = DateTime.UtcNow;
			}
		}

		private IEnumerator TimeRecorderProc()
		{
			while (!IsTerminated)
			{
				yield return new WaitForSecondsRealtime(m_recordSeconds);
				if (m_happened)
				{
					this.FirstUpdateAfterANR?.Invoke();
					m_happened = false;
				}
				RefreshCalledTime();
			}
		}

		private void MonitorLoop()
		{
			while (!IsTerminated)
			{
				if (m_paused)
				{
					ExceptionLogger.LogInfo("ANRMonitor: Paused");
					ready.Reset();
					ready.Wait();
					RefreshCalledTime();
				}
				if (!Monitor.TryEnter(m_timeLock, m_timeout))
				{
					ExceptionLogger.LogWarning("ANRMonitor: m_timeLock is not released. wait more time.");
					Thread.Sleep(m_waitLimitMilliSeconds);
					continue;
				}
				int num;
				try
				{
					num = (int)DateTime.UtcNow.Subtract(m_calledTime).TotalMilliseconds;
				}
				finally
				{
					Monitor.Exit(m_timeLock);
				}
				if (num > m_waitLimitMilliSeconds)
				{
					if (!m_happened && ShouldReport())
					{
						ExceptionLogger.LogInfo("ANRMonitor: No update during {0} ms", num);
						this.Detected?.Invoke();
						m_happened = true;
					}
					RefreshCalledTime();
				}
				else
				{
					Thread.Sleep((num > 0) ? (m_waitLimitMilliSeconds - num) : m_maxSleepTimeMilliSeconds);
				}
			}
		}

		private bool ShouldReport()
		{
			try
			{
				return Convert.ToDouble(m_throttle) > new System.Random().NextDouble();
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("ANRMonitor: Failed to compare ANR throttle({0}): {1}", m_throttle, ex.Message);
				return false;
			}
		}
	}
}
